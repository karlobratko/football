using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Football.DAL.Models;
using Football.DAL.Repository.Abstract;
using Football.DAL.Repository.Factory;
using Football.Library.Extensions;
using Football.WinFormsUI.Controls;
using Football.WinFormsUI.Forms;

namespace Football.WinFormsUI {
  public partial class MainForm : Form {
    private Settings _settings;
    private readonly IFootballRepository _footballRepository = FootballRepositoryFactory.GetRepository();
    private readonly ISettingsRepository _settingsRepository = SettingsRepositoryFactory.GetRepository();

    private Int32 _progressPreviousPercentage = 0;
    private Int32 _progressCounter = 0;

    private String Status {
      set => lblStatus.Text = value;
    }

    public MainForm() {
      _settings = _settingsRepository.Load();

      Thread.CurrentThread.SetLanguage(_settings.Language);
      InitializeComponent();

      Init();
    }

    private void Init() {
      PopulateCountriesDDL();

      lblGender.Text = Properties.Resources.ResourceManager.GetString(_settings.Gender.ToString());

      if (_settings.Country != null) {
        PopulatePNLs();
      }
    }

    private async void PopulateCountriesDDL() {
      try {
        Status = Properties.Resources.ResourceManager.GetString("fetching-country-data");

        IEnumerable<Country> countries = await _footballRepository.ReadCountries(_settings.Gender);
        ddlCountry.Items.AddRange(items: countries.OrderBy(keySelector: country => country.Name).ToArray());
        ddlCountry.SelectedItem = _settings.Country;

        Status = Properties.Resources.ResourceManager.GetString("select-country");
      }
      catch (Exception e) {
        _ = MessageBox.Show(e.Message, Properties.Resources.ResourceManager.GetString("error-title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        Status = e.Message;
      }
    }

    private void CountrySelected(Object sender, EventArgs e) {
      var country = ddlCountry.SelectedItem as Country;

      _settings.Country = country;
      _settingsRepository.Save(_settings);

      PopulatePNLs();
    }

    private async void PopulatePNLs() {
      Status = Properties.Resources.ResourceManager.GetString("fetching-football-data");
      Controls.OfType<FlowLayoutPanel>().ToList().ForEach(control => control.Controls.Clear());

      try {
        UpdateProgress(6);
        var matches = (HashSet<Match>)await _footballRepository.ReadMatches(gender: _settings.Gender);

        UpdateProgress(6);
        IEnumerable<MatchEvent> events = matches.Aggregate(seed: new List<MatchEvent>(),
                                                           func: (res, x) => res.Concat(x.HomeEvents.Concat(x.AwayEvents)).ToList());

        UpdateProgress(6);
        var countryPlayers =
          matches.Aggregate(seed: new HashSet<Player>(),
                            func: (acc, match) => acc.Union(second: match.HomeCountry == _settings.Country.Name
                                                                      ? match.HomeLayout.GetTeam()
                                                                      : match.AwayCountry == _settings.Country.Name
                                                                        ? match.AwayLayout.GetTeam()
                                                                        : new HashSet<Player>()).ToHashSet())
                 .Select(selector: player => {
                   IEnumerable<MatchEvent> playerEvents = events.Where(predicate: e => e.Player.Contains(player.Name));

                   player.Goals = playerEvents.Where(predicate: e => e.TypeOfEvent == "goal" || e.TypeOfEvent == "goal-penalty").Count();
                   player.YellowCards = playerEvents.Where(predicate: e => e.TypeOfEvent == "yellow-card").Count();

                   return player;
                 })
                 .ToHashSet();

        UpdateProgress(6);
        pnlPlayers.Controls.AddRange(countryPlayers.Select(selector: player => {
                                                      var control = new PlayerControl(player);
                                                      return control;
                                                    })
                                                   .ToArray());

        UpdateProgress(6);
        pnlRankedPlayers.Controls.AddRange(countryPlayers.Where(player => player.Goals > 0 && player.YellowCards > 0)
                                                         .OrderByDescending(keySelector: player => player.Goals)
                                                         .ThenByDescending(keySelector: player => player.YellowCards)
                                                         .Select(selector: player => new RankedPlayerControl(player))
                                                         .ToArray());

        UpdateProgress(6);
        pnlRankedMatches.Controls.AddRange(matches.Where(predicate: match => match.HomeCountry == _settings.Country.Name || match.AwayCountry == _settings.Country.Name)
                                                  .OrderByDescending(keySelector: match => match.Attendance)
                                                  .Select(selector: match => new RankedMatchControl(match))
                                                  .ToArray());

        Status = Properties.Resources.ResourceManager.GetString("fetching-success");
      }
      catch (Exception e) {
        _ = MessageBox.Show(e.Message, Properties.Resources.ResourceManager.GetString("error-title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        Status = e.Message;
      }
    }

    private void OpenSettings(Object sender, EventArgs e) {
      if (new SettingsForm().ShowDialog() == DialogResult.OK) {
        Controls.Clear();
        _settings = _settingsRepository.Load();

        Thread.CurrentThread.SetLanguage(_settings.Language);
        InitializeComponent();

        Init();
      }
    }

    private void UpdateProgress(Int32 max) {
      if (max == 1 || max == 0) return;
      Int32 currentPercentage = (Int32)((Double)_progressCounter++ / (max - 1) * 100);
      if (currentPercentage != _progressPreviousPercentage) {
        bgWorker.ReportProgress(currentPercentage);
      }

      _progressPreviousPercentage = currentPercentage;

      if (_progressCounter == max) {
        _progressCounter = 0;
      }
    }

    private void ProgressChanged(Object sender, System.ComponentModel.ProgressChangedEventArgs e) =>
      progressBar.Value = e.ProgressPercentage;
  }
}
