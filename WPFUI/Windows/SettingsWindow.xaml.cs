using System;
using System.Linq;
using System.Threading;
using System.Windows;

using Football.DAL.Models;
using Football.DAL.Repository.Abstract;
using Football.DAL.Repository.Factory;
using Football.Library.Extensions;
using Football.Library.Models;

namespace Football.WPFUI.Windows {
  public partial class SettingsWindow : Window {
    private readonly ISettingsRepository _settingsRepository = SettingsRepositoryFactory.GetRepository();
    private readonly Settings _settings;

    public SettingsWindow() {
      _settings = _settingsRepository.Load();

      Thread.CurrentThread.SetLanguage(_settings.Language);
      InitializeComponent();

      Init();
    }

    private void PopulateLanguageDDL() => ddlLanguages.SelectedValue = _settings.Language;

    private void PopulateGenderRBs() {
      rbMale.IsChecked = _settings.Gender == Gender.Male;
      rbFemale.IsChecked = _settings.Gender == Gender.Female;
    }

    private void PopulateResolutionDDL() {
      ddlResolutions.ItemsSource =
        Settings.RESOLUTIONS
                .Append(new Resolution(width: (Int32)SystemParameters.WorkArea.Width,
                                       height: (Int32)SystemParameters.WorkArea.Height,
                                       isFullscreen: true));
      ddlResolutions.SelectedItem = _settings.Resolution;
    }

    private void Init() {
      PopulateLanguageDDL();
      PopulateGenderRBs();
      PopulateResolutionDDL();
    }

    private void OpenMainWindow() {
      Hide();
      _ = new MainWindow().ShowDialog();
      Close();
    }

    private void SubmitSettings(Object sender, RoutedEventArgs e) {
      _settingsRepository.Save(settings: new Settings {
        Gender = rbMale?.IsChecked ?? true ? Gender.Male : Gender.Female,
        Language = (Language)Enum.Parse(enumType: typeof(Language),
                                    value: ddlLanguages.SelectedValue.ToString()),
        Resolution = (Resolution)ddlResolutions.SelectedValue,
        Country = _settings.Country,
        HomeCountry = _settings.HomeCountry,
        AwayCountry = _settings.AwayCountry
      });

      OpenMainWindow();
    }

    private void CancelSettings(Object sender, RoutedEventArgs e) => OpenMainWindow();
  }
}