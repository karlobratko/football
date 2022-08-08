using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using Football.DAL.Models;
using Football.DAL.Repository.Abstract;
using Football.DAL.Repository.Factory;
using Football.Library.Extensions;

using Football.WPFUI.Controls;

using WpfAnimatedGif;

using WPFUI.Windows;

namespace Football.WPFUI.Windows {
  public partial class MainWindow : Window {
    private readonly ISettingsRepository _settingsRepository = SettingsRepositoryFactory.GetRepository();
    private readonly IFootballRepository _footballRepository = FootballRepositoryFactory.GetRepository();

    private Settings _settings;
    private IEnumerable<Country> _countries;
    private IEnumerable<Match> _matches;
    private IEnumerable<Result> _results;

    public MainWindow() {
      _settings = _settingsRepository.Load();

      Thread.CurrentThread.SetLanguage(language: _settings.Language);
      InitializeComponent();

      Init();
    }

    private async void Init() {
      SetResolution();

      StartLoading();
      _countries = await _footballRepository.ReadCountries(gender: _settings.Gender);
      _matches = await _footballRepository.ReadMatches(gender: _settings.Gender);
      _results = await _footballRepository.ReadResults(gender: _settings.Gender);
      StopLoading();

      ClearField();
      PopulateHomeCountryDDL();

      ddlHomeCountry.SelectedItem = _settings.HomeCountry;
    }

    private void SetResolution() {
      Width = _settings.Resolution.Width;
      Height = _settings.Resolution.Height;
    }

    private void StartLoading() => imgLoading.Visibility = Visibility.Visible;

    private void StopLoading() => imgLoading.Visibility = Visibility.Hidden;

    private void PopulateHomeCountryDDL() =>
      _countries.ToList()
                .ForEach(action: country => ddlHomeCountry.Items.Add(country));

    private void HomeCountrySelected(Object sender, SelectionChangedEventArgs e) {
      if (ddlHomeCountry.SelectedItem is null) {
        ClearField();
        return;
      }

      ClearAwayUIInfo();

      _settings.HomeCountry = (Country)ddlHomeCountry.SelectedItem;
      _settingsRepository.Save(_settings);

      StartLoading();

      lblHomeCountry.Content = _settings.HomeCountry.Name;
      PopulateAwayCountryDDL();
      ddlAwayCountry.SelectedItem = _settings.AwayCountry;

      StopLoading();
    }

    private void ClearAwayUIInfo() {
      ddlAwayCountry.Items.Clear();
      lblAwayCountry.Content = String.Empty;
      lblScore.Content = "0:0";
    }

    private void PopulateAwayCountryDDL() =>
      _matches.Where(predicate: match => match.HomeFrom(_settings.HomeCountry))
              .ToList()
              .ForEach(action: match => ddlAwayCountry.Items.Add(_countries.FirstOrDefault(predicate: country => match.AwayFrom(country))));

    private void AwayCountrySelected(Object sender, SelectionChangedEventArgs e) {
      if (ddlAwayCountry.SelectedItem is null) {
        ClearField();
        return;
      }

      _settings.AwayCountry = (Country)ddlAwayCountry.SelectedItem;
      _settingsRepository.Save(_settings);

      StartLoading();

      lblAwayCountry.Content = _settings.AwayCountry.Name;
      PopulatePlayersField();
      ShowScore();

      StopLoading();
    }

    private void PopulatePlayersField() {
      ClearField();

      Match theMatch =
        _matches.FirstOrDefault(predicate: match =>
                                  match.HomeFrom(_settings.HomeCountry) &&
                                  match.AwayFrom(_settings.AwayCountry));
      if (theMatch is null) return;

      PopulateFieldSide(country: _settings.HomeCountry, players: theMatch.HomeLayout.StartingEleven, events: theMatch.HomeEvents);
      PopulateFieldSide(country: _settings.AwayCountry, players: theMatch.AwayLayout.StartingEleven, events: theMatch.AwayEvents);
    }

    private void ClearField() {
      ClearHomeFieldSide();
      ClearAwayFieldSide();
    }

    private void ClearHomeFieldSide() {
      HomeGoalies.Children.Clear();
      HomeDefenders.Children.Clear();
      HomeMidfields.Children.Clear();
      HomeForwards.Children.Clear();
    }

    private void ClearAwayFieldSide() {
      AwayGoalies.Children.Clear();
      AwayDefenders.Children.Clear();
      AwayMidfields.Children.Clear();
      AwayForwards.Children.Clear();
    }

    private void PopulateFieldSide(Country country, HashSet<Player> players, List<MatchEvent> events) =>
      players.Select(selector: player => {
        IEnumerable<MatchEvent> playerEvents =
              events.Where(predicate: e => e.Player == player.Name);

        player.Goals = playerEvents.Count(predicate: MatchEvent.IsGoal);
        player.YellowCards = playerEvents.Count(predicate: MatchEvent.IsYellowCard);

        return InitPlayerControl(player);
      })
             .ToList()
             .ForEach(action: control => {
               switch (control.Player.Position) {
                 case Position.Defender:
                   _ = country == _settings.HomeCountry
                     ? HomeDefenders.Children.Add(control)
                     : AwayDefenders.Children.Add(control);
                   break;
                 case Position.Forward:
                   _ = country == _settings.HomeCountry
                     ? HomeForwards.Children.Add(control)
                     : AwayForwards.Children.Add(control);
                   break;
                 case Position.Midfield:
                   _ = country == _settings.HomeCountry
                     ? HomeMidfields.Children.Add(control)
                     : AwayMidfields.Children.Add(control);
                   break;
                 case Position.Goalie:
                   _ = country == _settings.HomeCountry
                     ? HomeGoalies.Children.Add(control)
                     : AwayGoalies.Children.Add(control);
                   break;
               }
             });
    private PlayerControl InitPlayerControl(Player player) {
      var control = new PlayerControl(player);

      control.MouseLeftButtonDown += OpenPlayerWindow(player);

      return control;
    }

    private MouseButtonEventHandler OpenPlayerWindow(Player player) =>
      (Object sender, MouseButtonEventArgs e) => {
        var image = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/playerLoading.gif")));
        ImageBehavior.SetAnimatedSource(imgPlayerLoading, image);
        imgPlayerLoading.Visibility = Visibility.Visible;

        _ = Task.Factory
                .StartNew(() => Thread.Sleep(300))
                .ContinueWith(task => {
                  imgPlayerLoading.Visibility = Visibility.Hidden;

                  var playerWindow = new PlayerWindow(player) {
                    Owner = this
                  };
                  _ = playerWindow.ShowDialog();
                }, TaskScheduler.FromCurrentSynchronizationContext());
      };

    private void ShowScore() {
      Match theMatch = _matches.FirstOrDefault(predicate: match => match.HomeCountry.Contains(_settings.HomeCountry.Name) &&
                                                                   match.AwayCountry.Contains(_settings.AwayCountry.Name));
      if (theMatch is null) return;

      lblScore.Content = $"{theMatch.HomeResults.Goals} : {theMatch.AwayResults.Goals}";
    }

    private void OpenSettings(Object sender, RoutedEventArgs e) {
      if (new SettingsWindow(initialSettings: false).ShowDialog() == true) {
        _settings = _settingsRepository.Load();

        Thread.CurrentThread.SetLanguage(language: _settings.Language);
        InitializeComponent();

        ddlHomeCountry.SelectedItem = null;
        ddlAwayCountry.SelectedItem = null;
        ddlHomeCountry.Items.Clear();
        ddlAwayCountry.Items.Clear();

        Init();
      }
    }

    private void OpenHomeDetails(Object sender, RoutedEventArgs e) => OpenDetails(_settings.HomeCountry);

    private void OpenAwayDetails(Object sender, RoutedEventArgs e) => OpenDetails(_settings.AwayCountry);

    private void OpenDetails(Country country) {
      if (country is null) return;

      StartLoading();
      _ = Task.Factory
          .StartNew(() => Thread.Sleep(500))
          .ContinueWith(task => {
            StopLoading();

            Result countryResult =
              _results.FirstOrDefault(predicate: result =>
                                        result.Country.Contains(country.Name));
            if (countryResult != null) {
              var countryWindow = new CountryWindow(countryResult) {
                Owner = this
              };
              _ = countryWindow.ShowDialog();
            }
          }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void MainWindowClosing(Object sender, System.ComponentModel.CancelEventArgs e) {
      MessageBoxResult messageBoxResult = 
        MessageBox.Show(owner: this,
                        messageBoxText: Properties.Resources.ResourceManager.GetString("exit-text"),
                        caption: Properties.Resources.ResourceManager.GetString("exit-caption"),
                        button: MessageBoxButton.YesNo,
                        icon: MessageBoxImage.Question,
                        defaultResult: MessageBoxResult.No);

      e.Cancel = messageBoxResult == MessageBoxResult.No;
    }
  }
}
