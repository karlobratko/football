using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
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

    private readonly IFootballRepository _footballRepository = FootballRepositoryFactory.GetRepository();
    private readonly ISettingsRepository _settingsRepository = SettingsRepositoryFactory.GetRepository();
    private readonly IFavouritePlayersRepository _favouritePlayersRepository = FavouritePlayersRepositoryFactory.GetRepository();

    private readonly IDictionary<Gender, List<Player>> _favouritePlayers;
    private readonly IList<Player> _selectedPlayers = new List<Player>();

    private Settings _settings;
    private Int32 _progressPreviousPercentage = 0;
    private Int32 _progressCounter = 0;
    private Boolean _dropSuccessfull = false;

    private String Status {
      set => lblStatus.Text = value;
    }

    public MainForm() {
      _settings = _settingsRepository.Load();
      _favouritePlayers = LoadFavouritePlayers();

      Thread.CurrentThread.SetLanguage(language: _settings.Language);
      InitializeComponent();

      Init();
    }

    private IDictionary<Gender, List<Player>> LoadFavouritePlayers() =>
      _favouritePlayersRepository.Exists()
        ? _favouritePlayersRepository.Load()
        : new Dictionary<Gender, List<Player>>() {
            { Gender.Male, new List<Player>() },
            { Gender.Female, new List<Player>() }
          };

    private void Init() {
      PopulateCountriesDDL();

      lblGender.Text = Properties.Resources.ResourceManager.GetString(_settings.Gender.ToString());

      pnlFavPlayers.Controls
                   .AddRange(controls:
                     _favouritePlayers[_settings.Gender].Select(selector: player =>
                                                                  InitFavPlayerControl(player))
                                                        .ToArray());
    }

    private async void PopulateCountriesDDL() {
      try {
        SetLoading(true);

        Status = Properties.Resources.ResourceManager.GetString("fetching-country-data");

        IEnumerable<Country> countries = await _footballRepository.ReadCountries(gender: _settings.Gender);
        ddlCountry.Items
                  .AddRange(items:
                    countries.OrderBy(keySelector: country => country.Name)
                             .ToArray());
        SetLoading(false);

        ddlCountry.SelectedItem = _settings.Country;

        Status = Properties.Resources.ResourceManager.GetString("select-country");
      }
      catch (Exception e) {
        _ = MessageBox.Show(text: e.Message,
                            caption: Properties.Resources.ResourceManager.GetString("error-title"),
                            buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Error);
        Status = e.Message;
      }
    }

    private void CountrySelected(Object sender, EventArgs e) {
      var country = ddlCountry.SelectedItem as Country;

      _settings.Country = country;
      _settingsRepository.Save(settings: _settings);

      PopulatePNLs();
    }

    private async void PopulatePNLs() {
      Status = Properties.Resources.ResourceManager.GetString("fetching-football-data");
      Controls.OfType<FlowLayoutPanel>()
              .Where(predicate: pnl => pnl.Name != pnlFavPlayers.Name)
              .ToList()
              .ForEach(action: control => control.Controls.Clear());

      try {
        SetLoading(true);

        UpdateProgress(6);
        var matches = (HashSet<Match>)await _footballRepository.ReadMatches(gender: _settings.Gender);

        UpdateProgress(6);
        IEnumerable<MatchEvent> allEvents =
          matches.Aggregate(seed: new List<MatchEvent>(),
                            func: (events, match) =>
                              events.Concat(second: match.GetEvents())
                                       .ToList());

        UpdateProgress(6);
        var players =
          matches.Aggregate(seed: new HashSet<Player>(),
                            func: (acc, match) =>
                              acc.Union(second: match.HomeFrom(_settings.Country)
                                          ? match.HomeLayout.GetPlayers()
                                          : match.AwayFrom(_settings.Country)
                                            ? match.AwayLayout.GetPlayers()
                                            : new HashSet<Player>())
                                 .ToHashSet())
                 .Select(selector: player => {
                   IEnumerable<MatchEvent> playerEvents =
                     allEvents.Where(predicate: e => e.Player == player.Name);

                   player.Goals = playerEvents.Count(predicate: MatchEvent.IsGoal);
                   player.YellowCards = playerEvents.Count(predicate: MatchEvent.IsYellowCard);

                   return player;
                 })
                 .ToHashSet();

        UpdateProgress(6);
        pnlPlayers.Controls
                  .AddRange(controls: players.Select(selector: InitPlayerControl)
                                             .ToArray());

        UpdateProgress(6);
        pnlRankedPlayers.Controls
                        .AddRange(controls:
                          players.Where(Player.CanBeRanked)
                                 .OrderByDescending(keySelector: player => player.Goals)
                                 .ThenByDescending(keySelector: player => player.YellowCards)
                                 .Select(selector: player => new RankedPlayerControl(player))
                                 .ToArray());

        UpdateProgress(6);
        pnlRankedMatches.Controls
                        .AddRange(controls:
                          matches.Where(predicate: match =>
                                          match.WasPlayedIn(country: _settings.Country))
                                 .OrderByDescending(keySelector: match => match.Attendance)
                                 .Select(selector: match => new RankedMatchControl(match))
                                 .ToArray());

        Status = Properties.Resources.ResourceManager.GetString("fetching-success");

        SetLoading(false);
      }
      catch (Exception e) {
        _ = MessageBox.Show(text: e.Message,
                            caption: Properties.Resources.ResourceManager.GetString("error-title"),
                            buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Error);
        Status = e.Message;
      }
    }

    private void SetLoading(Boolean displayLoader) {
      pbLoading.Enabled = displayLoader;
      pbLoading.Visible = displayLoader;
      Cursor = displayLoader ? Cursors.WaitCursor : Cursors.Default;
    }

    private PlayerControl InitPlayerControl(Player player) {
      var control = new PlayerControl(player);

      control.MouseDown += PlayerControlMouseDown;
      control.Controls
             .OfType<Control>()
             .ToList()
             .ForEach(action: innerControl =>
               innerControl.MouseDown += (Object obj, MouseEventArgs args) =>
                                           PlayerControlMouseDown(sender: control,
                                                                  e: args));

      _ = control.ContextMenuStrip.Items.Add(value: CreateAddToFavouritesTsmi(control.Player));

      control.OnImageChanged += PlayerControlImageChanged(pnlFavPlayers, pnlRankedPlayers);
      control.OnImageRemoved += PlayerControlImageRemoved(pnlFavPlayers, pnlRankedPlayers);

      return control;
    }

    private PlayerControl.ImageRemovedEvent PlayerControlImageRemoved(params Panel[] playersPanels) =>
      (Object sender, PlayerControl.ImageRemovedEventArgs args) =>
        playersPanels.ToList()
                     .Select(selector: panel =>
                               panel.Controls
                                    .OfType<IPlayerControl>()
                                    .FirstOrDefault(predicate: control =>
                                                      control.Player == args.Player))
                      .Where(predicate: control => !(control is null))
                      .ToList()
                      .ForEach(action: control => control.DefaultImage());

    private PlayerControl.ImageChangedEvent PlayerControlImageChanged(params Panel[] playersPanels) =>
      (Object sender, PlayerControl.ImageChangedEventArgs args) =>
        playersPanels.ToList()
                     .Select(selector: panel =>
                               panel.Controls
                                    .OfType<IPlayerControl>()
                                    .FirstOrDefault(predicate: control =>
                                                      control.Player == args.Player))
                              .Where(predicate: control => !(control is null))
                              .ToList()
                              .ForEach(action: control => control.ChangeImage());

    private void PlayerControlMouseDown(Object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Right) return;

      var playerControl = sender as PlayerControl;
      if (_favouritePlayers[_settings.Gender].Count == 3 ||
          _favouritePlayers[_settings.Gender].Contains(item: playerControl.Player)) {
        return;
      }

      if (_selectedPlayers.Contains(item: playerControl.Player)) {
        _ = playerControl.DoDragDrop(data: _selectedPlayers.ToList(),
                                     allowedEffects: DragDropEffects.Copy |
                                                     DragDropEffects.None);

        if (3 - _favouritePlayers[_settings.Gender].Count > _selectedPlayers.Count ||
            playerControl.Selected) {
          playerControl.Selected = playerControl.Selected != true;

          if (playerControl.Selected)
            _selectedPlayers.Add(item: playerControl.Player);
          else
            _ = _selectedPlayers.Remove(item: playerControl.Player);
        }
      }
      else if (_selectedPlayers.Count < 3) {
        if (3 - _favouritePlayers[_settings.Gender].Count > _selectedPlayers.Count ||
            playerControl.Selected) {
          playerControl.Selected = playerControl.Selected != true;

          if (playerControl.Selected)
            _selectedPlayers.Add(item: playerControl.Player);
          else
            _ = _selectedPlayers.Remove(item: playerControl.Player);
        }

        _ = playerControl.DoDragDrop(data: _selectedPlayers.ToList(),
                                     allowedEffects: DragDropEffects.Copy |
                                                     DragDropEffects.None);
      }

      if (_dropSuccessfull) {
        pnlPlayers.Controls
                  .OfType<PlayerControl>()
                  .ToList()
                  .ForEach(action: control => control.Selected = false);

        _selectedPlayers.Clear();
        _dropSuccessfull = false;
      }
    }

    private ToolStripMenuItem CreateAddToFavouritesTsmi(Player player) {
      Boolean isFavourite = _favouritePlayers[_settings.Gender].Contains(item: player);

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
      e.Effect = _favouritePlayers[_settings.Gender].Count < 3
        ? DragDropEffects.Copy
        : DragDropEffects.None;

    private void DragFavPlayerDrop(Object sender, DragEventArgs e) {
      if (e.Data.GetData(format: typeof(List<Player>)) is List<Player> players) {
        AddToFavourites(players);
        _dropSuccessfull = true;
      }
      else {
        _dropSuccessfull = false;
      }
    }

    private void AddToFavourites(List<Player> players) {
      _favouritePlayers[_settings.Gender] =
        _favouritePlayers[_settings.Gender].Union(second: players)
                                           .ToList();

      pnlFavPlayers.Controls.Clear();
      pnlFavPlayers.Controls
                   .AddRange(controls:
                     _favouritePlayers[_settings.Gender].Select(selector: InitFavPlayerControl)
                                                        .ToArray());

      DisableAddToFavouritesTsmi(players);
    }

    private PlayerControl InitFavPlayerControl(Player player) {
      player.IsFavourite = true;

      var control = new PlayerControl(player);

      control.MouseDown += FavPlayerControlMouseDown;
      control.Controls
             .OfType<Control>()
             .ToList()
             .ForEach(action: innerControl =>
               innerControl.MouseDown += (Object obj, MouseEventArgs args) =>
                                           FavPlayerControlMouseDown(sender: control,
                                                                     e: args));

      _ = control.ContextMenuStrip.Items.Add(value: CreateRemoveFromFavouritesTsmi(control.Player));

      control.OnImageChanged += PlayerControlImageChanged(pnlPlayers, pnlRankedPlayers);
      control.OnImageRemoved += PlayerControlImageRemoved(pnlPlayers, pnlRankedPlayers);

      return control;
    }

    private ToolStripMenuItem CreateRemoveFromFavouritesTsmi(Player player) {
      Boolean isFavourite = _favouritePlayers[_settings.Gender].Contains(item: player);

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
      pnlPlayers.Controls
                .OfType<PlayerControl>()
                .ToList()
                .ForEach(control =>
                  players.ForEach(action: player => {
                    ToolStripItem tmp =
                      control.ContextMenuStrip
                             .Items
                             .Find(key: $"{ADD_TO_FAVOURITES_TSMI_NAME}_{player.ShirtNumber}",
                                   searchAllChildren: false)
                             .FirstOrDefault();

                    if (!(tmp is null))
                      tmp.Enabled = false;
                  }));

    private void FavPlayerControlMouseDown(Object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Right) return;

      var playerControl = sender as PlayerControl;
      playerControl.Selected = true;
      _ = playerControl.DoDragDrop(data: playerControl.Player,
                                   allowedEffects: DragDropEffects.Move);
      playerControl.Selected = false;
    }

    private void RemoveFromFavourites(Player player) {
      _ = _favouritePlayers[_settings.Gender].Remove(item: player);

      pnlFavPlayers.Controls.Clear();
      pnlFavPlayers.Controls
                   .AddRange(controls:
                     _favouritePlayers[_settings.Gender].Select(selector: InitFavPlayerControl)
                                                        .ToArray());

      EnableAddToFavouritesTsmi(player);
    }

    private void EnableAddToFavouritesTsmi(Player player) =>
      pnlPlayers.Controls
                .OfType<PlayerControl>()
                .ToList()
                .ForEach(action: control => {
                  ToolStripItem tmp =
                    control.ContextMenuStrip
                           .Items
                           .Find(key: $"{ADD_TO_FAVOURITES_TSMI_NAME}_{player.ShirtNumber}",
                                 searchAllChildren: false)
                           .FirstOrDefault();

                  if (!(tmp is null))
                    tmp.Enabled = true;
                });

    private void DragPlayerEnter(Object sender, DragEventArgs e) =>
      e.Effect = DragDropEffects.Move;

    private void DragPlayerDrop(Object sender, DragEventArgs e) {
      if (e.Data.GetData(format: typeof(Player)) is Player player && player.IsFavourite) {
        RemoveFromFavourites(player);
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

    private void ProgressChanged(Object sender, ProgressChangedEventArgs e) =>
      progressBar.Value = e.ProgressPercentage;

    private void ExitApplication(Object sender, FormClosingEventArgs e) {
      DialogResult result =
        MessageBox.Show(text: Properties.Resources.ResourceManager.GetString("exit-text"),
                        caption: Properties.Resources.ResourceManager.GetString("exit-title"),
                        buttons: MessageBoxButtons.YesNoCancel,
                        icon: MessageBoxIcon.Question);

      switch (result) {
        case DialogResult.Yes:
          _favouritePlayersRepository.Save(dict: _favouritePlayers);
          return;
        case DialogResult.Cancel:
          e.Cancel = true;
          return;
      }
    }

    private void PrintPlayersAsPage(Object sender, PrintPageEventArgs e) {
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.PageSettings.Margins = new Margins(left: 100,
                                           right: 100,
                                           top: 100,
                                           bottom: 100);

      var location = new PointF(x: 100, y: 100);
      var h1Font = new Font(family: Font.FontFamily,
                            emSize: Font.Size * 2f,
                            style: FontStyle.Bold);
      var h2Font = new Font(family: Font.FontFamily,
                            emSize: Font.Size * 1.5f,
                            style: FontStyle.Regular);
      var pFont = new Font(family: Font.FontFamily,
                           emSize: Font.Size * 1.2f,
                           style: FontStyle.Regular);

      e.Graphics
       .DrawString(s: Properties.Resources.ResourceManager.GetString("print-title"),
                   font: h1Font,
                   brush: Brushes.Black,
                   point: location);
      location.Y += h1Font.Height;

      location.Y += 20;

      e.Graphics
       .DrawString(s: Properties.Resources.ResourceManager.GetString("print-players-rank-title"),
                   font: h2Font,
                   brush: Brushes.Black,
                   point: location);
      location.Y += h2Font.Height;

      pnlRankedPlayers.Controls
                      .OfType<RankedPlayerControl>()
                      .Select(selector: control => control.Player)
                      .ToList()
                      .ForEach(action: player =>
                        e.Graphics
                         .DrawString(s:
                           String.Format(format: Properties.Resources.ResourceManager.GetString("print-players-rank-format"),
                                         player.Name,
                                         player.ShirtNumber,
                                         player.Position,
                                         player.Goals,
                                         player.YellowCards),
                                     font: pFont,
                                     brush: Brushes.Black,
                                     x: location.X,
                                     y: location.Y += pFont.Size + pFont.Height));

      location.Y += 50;

      e.Graphics
       .DrawString(s: Properties.Resources.ResourceManager.GetString("print-matches-rank-title"),
                   font: h2Font,
                   brush: Brushes.Black,
                   point: location);
      location.Y += h2Font.Height;

      pnlRankedMatches.Controls
                      .OfType<RankedMatchControl>()
                      .Select(selector: control => control.Stadium)
                      .ToList()
                      .ForEach(action: stadium =>
                        e.Graphics
                         .DrawString(s:
                           String.Format(format: Properties.Resources.ResourceManager.GetString("print-matches-rank-format"),
                                         stadium.HomeCountry,
                                         stadium.AwayCountry,
                                         stadium.Location,
                                         stadium.Attendance),
                                     font: pFont,
                                     brush: Brushes.Black,
                                     x: location.X,
                                     y: location.Y += pFont.Size + pFont.Height));
    }

    private void OpenPrintDialog(Object sender, EventArgs e) =>
      printDialog.ShowDialog();

    private void OpenPrintPreview(Object sender, EventArgs e) =>
      printPreviewDialog.ShowDialog();

    private void PrintDocument(Object sender, EventArgs e) =>
      printDocument.Print();
  }
}
