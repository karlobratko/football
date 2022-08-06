using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

using Football.DAL.Models;
using Football.DAL.Repository.Abstract;
using Football.DAL.Repository.Factory;
using Football.Library.Extensions;

using Football.WPFUI.Controls;

namespace Football.WPFUI.Windows {
  public partial class MainWindow : Window {
    private readonly ISettingsRepository _settingsRepository = SettingsRepositoryFactory.GetRepository();
    private readonly IFootballRepository _footballRepository = FootballRepositoryFactory.GetRepository();
    private readonly Settings _settings;

    private IEnumerable<Country> _countries;
    private IEnumerable<Match> _matches;

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
      StopLoading();

      PopulateHomeCountryDDL();
      SetSelectedCountries();
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

    private void SetSelectedCountries() {
      ddlHomeCountry.SelectedItem = _settings.HomeCountry;
      ddlAwayCountry.SelectedItem = _settings.AwayCountry;
    }

    private void HomeCountrySelected(Object sender, SelectionChangedEventArgs e) {
      ddlAwayCountry.Items.Clear();

      _settings.HomeCountry = (Country)ddlHomeCountry.SelectedItem;
      _settingsRepository.Save(_settings);

      StartLoading();

      PopulateAwayCountryDDL();
      ddlAwayCountry.SelectedItem = null;
      lblHomeCountry.Content = _settings.HomeCountry.Name;

      StopLoading();
    }

    private void PopulateAwayCountryDDL() =>
      _matches.Where(predicate: match => match.HomeFrom(_settings.HomeCountry))
              .ToList()
              .ForEach(action: match => ddlAwayCountry.Items.Add(_countries.FirstOrDefault(predicate: country => match.AwayFrom(country))));

    private void AwayCountrySelected(Object sender, SelectionChangedEventArgs e) {
      _settings.AwayCountry = (Country)ddlAwayCountry.SelectedItem;
      _settingsRepository.Save(_settings);

      if (ddlAwayCountry.SelectedItem is null) {
        ClearField();
        return;
      }

      StartLoading();

      PopulatePlayersField();
      lblAwayCountry.Content = _settings.AwayCountry.Name;

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
      ClearHome();
      ClearAway();
    }

    private void ClearHome() {
      HomeGoalies.Children.Clear();
      HomeDefenders.Children.Clear();
      HomeMidfields.Children.Clear();
      HomeForwards.Children.Clear();
    }

    private void ClearAway() {
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
             
               return new PlayerControl(player);
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

    private void OpenSettings(Object sender, RoutedEventArgs e) {

    }

    private void OpenHomeDetails(Object sender, RoutedEventArgs e) {

    }

    private void OpenAwayDetails(Object sender, RoutedEventArgs e) {

    }
  }
}
