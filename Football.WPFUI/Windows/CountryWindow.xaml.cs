using System.Windows;

using Football.DAL.Models;

namespace WPFUI.Windows {
  public partial class CountryWindow : Window {
    public Result CountryResult { get; private set; }

    public CountryWindow(Result countryResult) {
      CountryResult = countryResult;

      InitializeComponent();

      Init();
    }

    private void Init() {
      lblCountry.Content = CountryResult.ToString();
      lblMatches.Content = CountryResult.GamesPlayed;
      lblWins.Content = CountryResult.Wins;
      lblLost.Content = CountryResult.Losses;
      lblDraws.Content = CountryResult.Draws;
      lblGoalsFor.Content = CountryResult.GoalsFor;
      lblGoalsAgainst.Content = CountryResult.GoalsAgainst;
      lblGoalsDiff.Content = CountryResult.GoalDifferential;
    }

    private void CloseWindow(System.Object sender, RoutedEventArgs e) => Close();
  }
}
