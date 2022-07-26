using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Football.DAL.Models;
using Football.DAL.Repository.Abstract;
using Football.DAL.Repository.Factory;
using Football.Library.Extensions;
using Football.Library.Models;

namespace Football.WinFormsUI.Forms {
  public partial class SettingsForm : Form {
    private readonly ISettingsRepository _settingsRepository = SettingsRepositoryFactory.GetRepository();
    private readonly Settings _settings;

    public SettingsForm() {
      _settings = _settingsRepository.Load();

      Thread.CurrentThread.SetLanguage(_settings.Language);
      InitializeComponent();
      Init();
    }

    private void PopulateLanguageDDL() {
      ddlLanguage.ValueMember = "Value";
      ddlLanguage.DisplayMember = "Display";
      ddlLanguage.DataSource = Enum.GetNames(typeof(Language))
                                  .Select(lang => new {
                                    Value = lang,
                                    Display = Properties.Resources.ResourceManager.GetString(lang)
                                  }).ToList();
      ddlLanguage.SelectedValue = _settings.Language.ToString();
    }

    private void PopulateGenderRBs() {
      rbMale.Checked = _settings.Gender == Gender.Male;
      rbFemale.Checked = _settings.Gender == Gender.Female;
    }
    private void Init() {
      PopulateLanguageDDL();
      PopulateGenderRBs();
    }

    private void OpenMainForm() {
      Visible = false;
      new MainForm(_settings).ShowDialog();
      Close();
    }

    private void SubmitSettings(Object sender, EventArgs e) {
      _settingsRepository.Save(new Settings {
        Gender = rbMale.Checked ? Gender.Male : Gender.Female,
        Language = (Language)Enum.Parse(typeof(Language), ddlLanguage.SelectedValue.ToString()),
        Resolution = _settings.Resolution
      });

      OpenMainForm();
    }

    private void CancelSettings(Object sender, EventArgs e) => OpenMainForm();

    private void UserPressedKey(Object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Escape) {
        CancelSettings(sender, e);
      }
      else if (e.KeyCode == Keys.Enter) {
        SubmitSettings(sender, e);
      }
    }
  }
}
