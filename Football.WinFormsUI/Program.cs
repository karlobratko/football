using System;
using System.Windows.Forms;

using Football.DAL.Repository.Abstract;
using Football.DAL.Repository.Factory;
using Football.WinFormsUI.Forms;

namespace Football.WinFormsUI {
  internal static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      ISettingsRepository settingsRepository = SettingsRepositoryFactory.GetRepository();
      Application.Run(settingsRepository.Exists() ? new MainForm() : (Form)new SettingsForm(initialSettings: true));
    }
  }
}
