using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Football.DAL.Models;
using Football.DAL.Repository.Abstract;
using Football.DAL.Repository.Factory;
using Football.Library.Extensions;
using Football.Library.Models;
using Football.WinFormsUI.Controls;
using Football.WinFormsUI.Forms;

namespace Football.WinFormsUI {
  public partial class MainForm : Form {
    private const String ADD_TO_FAVOURITES_TSMI_NAME = "tsmiAddToFavourites";
    private const String REMOVE_TO_FAVOURITES_TSMI_NAME = "tsmiRemoveFromFavourites";
    private Settings _settings;
    private readonly IFootballRepository _footballRepository = FootballRepositoryFactory.GetRepository();
    private readonly ISettingsRepository _settingsRepository = SettingsRepositoryFactory.GetRepository();
    private readonly IFavouritePlayersRepository _favouritePlayersRepository = FavouritePlayersRepositoryFactory.GetRepository();

    private Int32 _progressPreviousPercentage = 0;
    private Int32 _progressCounter = 0;

    private readonly IList<Player> _selectedPlayers = new List<Player>();
    private readonly IDictionary<Gender, List<Player>> _favouritePlayers;
    private Boolean _dropSuccessfull = false;

    private String Status {
      set => lblStatus.Text = value;
    }

    public MainForm() {
      _settings = _settingsRepository.Load();
      _favouritePlayers = LoadFavouritePlayers();

      Thread.CurrentThread.SetLanguage(_settings.Language);
      InitializeComponent();

      Init();
    }

    private void Init() {
      PopulateCountriesDDL();

      lblGender.Text = Properties.Resources.ResourceManager.GetString(_settings.Gender.ToString());

      pnlFavPlayers.Controls.AddRange(_favouritePlayers[_settings.Gender].Select(player => InitFavPlayerControl(player)).ToArray());
      if (_settings.Country != null) {
        PopulatePNLs();
      }
    }

    private IDictionary<Gender, List<Player>> LoadFavouritePlayers() =>
      _favouritePlayersRepository.Exists()
        ? _favouritePlayersRepository.Load()
        : new Dictionary<Gender, List<Player>>() {
            { Gender.Male, new List<Player>() },
            { Gender.Female, new List<Player>() }
          };

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
      Controls.OfType<FlowLayoutPanel>().Where(pnl => pnl.Name != pnlFavPlayers.Name).ToList().ForEach(control => control.Controls.Clear());

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
        pnlPlayers.Controls.AddRange(countryPlayers.Select(selector: InitPlayerControl).ToArray());

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

    private PlayerControl InitPlayerControl(Player player) {
      var control = new PlayerControl(player);
      control.MouseDown += PlayerControlMouseDown;
      control.Controls.OfType<Control>()
                      .ToList()
                      .ForEach(action: innerControl => innerControl.MouseDown += (Object obj, MouseEventArgs args) => PlayerControlMouseDown(control, args));
      _ = control.ContextMenuStrip.Items.Add(CreateAddToFavouritesTsmi(control.Player));
      control.OnImageChanged += PlayerControlImageChanged(pnlFavPlayers);
      control.OnImageRemoved += PlayerControlImageRemoved(pnlFavPlayers);

      return control;
    }

    private PlayerControl.ImageRemovedEvent PlayerControlImageRemoved(Panel playersPanel) =>
      (Object sender, PlayerControl.ImageRemovedEventArgs args) => {
        PlayerControl playerControl = playersPanel.Controls.OfType<PlayerControl>()
                                                           .FirstOrDefault(control => control.Player == args.Player);
        if (playerControl is null) return;

        playerControl.RemoveImage();
      };

    private PlayerControl.ImageChangedEvent PlayerControlImageChanged(Panel playersPanel) => 
      (Object sender, PlayerControl.ImageChangedEventArgs args) => {
        PlayerControl playerControl = playersPanel.Controls.OfType<PlayerControl>()
                                                           .FirstOrDefault(control => control.Player == args.Player);
        if (playerControl is null) return;

        playerControl.ChangeImage();
      };

  private void PlayerControlMouseDown(Object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Right) return;

      var playerControl = sender as PlayerControl;

      if (_favouritePlayers.Count == 3 || (_favouritePlayers.TryGetValue(_settings.Gender, out List<Player> list) && list.Contains(playerControl.Player))) return;

      if (e.Clicks == 2) {
        AddToFavourites(new List<Player> { playerControl.Player });
        playerControl.Selected = false;
        return;
      }

      if (_selectedPlayers.Contains(playerControl.Player)) {
        _ = playerControl.DoDragDrop(_selectedPlayers.ToList(),
                                     DragDropEffects.Copy | DragDropEffects.None);

        playerControl.Selected = playerControl.Selected != true;

        if (playerControl.Selected)
          _selectedPlayers.Add(playerControl.Player);
        else
          _ = _selectedPlayers.Remove(playerControl.Player);
      }
      else if (_selectedPlayers.Count < 3) {
        playerControl.Selected = playerControl.Selected != true;

        if (playerControl.Selected)
          _selectedPlayers.Add(playerControl.Player);
        else
          _ = _selectedPlayers.Remove(playerControl.Player);

        _ = playerControl.DoDragDrop(_selectedPlayers.ToList(),
                                     DragDropEffects.Copy);
      }

      if (_dropSuccessfull) {
        pnlPlayers.Controls.OfType<PlayerControl>()
                           .ToList()
                           .ForEach(action: control => control.Selected = false);

        _selectedPlayers.Clear();
        _dropSuccessfull = false;
      }
    }

    private ToolStripMenuItem CreateAddToFavouritesTsmi(Player player) {
      Boolean isFavourite = _favouritePlayers.TryGetValue(_settings.Gender, out List<Player> list) && list.Contains(player);

      var tsmi = new ToolStripMenuItem() {
        Text = Properties.Resources.ResourceManager.GetString("favourites-add"),
        Name = $"{ADD_TO_FAVOURITES_TSMI_NAME}_{player.ShirtNumber}",
        Enabled = !isFavourite
      };
      tsmi.Click += (Object sender, EventArgs args) => {
        if (!isFavourite)
          AddToFavourites(new List<Player> { player });
      };
      return tsmi;
    }

    private void DragFavPlayerEnter(Object sender, DragEventArgs e) =>
      e.Effect = _favouritePlayers.Count < 3 ? DragDropEffects.Copy : DragDropEffects.None;

    private void DragFavPlayerDrop(Object sender, DragEventArgs e) {
      if (e.Data.GetData(typeof(List<Player>)) is List<Player> players) {
        AddToFavourites(players);
        _dropSuccessfull = true;
      }
      else {
        _dropSuccessfull = false;
      }
    }

    private void AddToFavourites(List<Player> players) {
      if (!_favouritePlayers.TryGetValue(_settings.Gender, out List<Player> list)) return;
      list = list.Union(players).ToList();
      _favouritePlayers[_settings.Gender] = list;

      pnlFavPlayers.Controls.Clear();
      pnlFavPlayers.Controls.AddRange(list.Select(selector: InitFavPlayerControl).ToArray());

      DisableAddToFavouritesTsmi(players);
    }

    private PlayerControl InitFavPlayerControl(Player player) {
      player.IsFavourite = true;
      var control = new PlayerControl(player);
      control.MouseDown += FavPlayerControlMouseDown;
      control.Controls.OfType<Control>()
                 .ToList()
                 .ForEach(action: innerControl => innerControl.MouseDown += (Object obj, MouseEventArgs args) => FavPlayerControlMouseDown(control, args));
      _ = control.ContextMenuStrip.Items.Add(CreateRemoveFromFavouritesTsmi(control.Player));
      control.OnImageChanged += PlayerControlImageChanged(pnlPlayers);
      control.OnImageRemoved += PlayerControlImageRemoved(pnlPlayers);

      return control;
    }

    private ToolStripMenuItem CreateRemoveFromFavouritesTsmi(Player player) {
      Boolean isFavourite = _favouritePlayers.TryGetValue(_settings.Gender, out List<Player> list) && list.Contains(player);

      var tsmi = new ToolStripMenuItem() {
        Text = Properties.Resources.ResourceManager.GetString("favourites-remove"),
        Name = $"{REMOVE_TO_FAVOURITES_TSMI_NAME}_{player.ShirtNumber}"
      };
      tsmi.Click += (Object sender, EventArgs args) => {
        if (isFavourite)
          RemoveFromFavourites(player);
      };
      return tsmi;
    }

    private void DisableAddToFavouritesTsmi(List<Player> players) =>
      pnlPlayers.Controls.OfType<PlayerControl>()
                         .ToList()
                         .ForEach(control =>
                           players.ForEach(player => {
                             ToolStripItem tmp = control.ContextMenuStrip.Items.Find($"{ADD_TO_FAVOURITES_TSMI_NAME}_{player.ShirtNumber}", false).FirstOrDefault();
                             if (!(tmp is null))
                               tmp.Enabled = false;
                           }));

    private void FavPlayerControlMouseDown(Object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Right) return;

      var playerControl = sender as PlayerControl;

      if (e.Clicks == 2) {
        RemoveFromFavourites(playerControl.Player);
        playerControl.Selected = false;
        return;
      }

      playerControl.Selected = true;
      _ = playerControl.DoDragDrop(playerControl.Player, DragDropEffects.Move);
      playerControl.Selected = false;
    }

    private void RemoveFromFavourites(Player player) {
      if (!_favouritePlayers.TryGetValue(_settings.Gender, out List<Player> list)) return;
      _ = list.Remove(player);
      _favouritePlayers[_settings.Gender] = list;

      pnlFavPlayers.Controls.Clear();
      pnlFavPlayers.Controls.AddRange(list.Select(selector: InitFavPlayerControl).ToArray());

      EnableAddToFavouritesTsmi(player);
    }

    private void EnableAddToFavouritesTsmi(Player player) =>
      pnlPlayers.Controls.OfType<PlayerControl>()
                     .ToList()
                     .ForEach(control => {
                       ToolStripItem tmp = control.ContextMenuStrip.Items.Find($"{ADD_TO_FAVOURITES_TSMI_NAME}_{player.ShirtNumber}", false).FirstOrDefault();
                       if (!(tmp is null))
                         tmp.Enabled = true;
                     });

    private void DragPlayerEnter(Object sender, DragEventArgs e) => e.Effect = DragDropEffects.Move;

    private void DragPlayerDrop(Object sender, DragEventArgs e) {
      if (e.Data.GetData(typeof(Player)) is Player favPlayer && favPlayer.IsFavourite) {
        RemoveFromFavourites(favPlayer);
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

    private void ExitApplication(Object sender, FormClosingEventArgs e) {
      DialogResult result = MessageBox.Show(Properties.Resources.ResourceManager.GetString("exit-text"),
                                            Properties.Resources.ResourceManager.GetString("exit-title"),
                                            MessageBoxButtons.YesNoCancel,
                                            MessageBoxIcon.Question);

      switch (result) {
        case DialogResult.Yes:
          _favouritePlayersRepository.Save(_favouritePlayers);
          break;
        case DialogResult.Cancel:
          e.Cancel = true;
          break;
      }
    }
  }
}
