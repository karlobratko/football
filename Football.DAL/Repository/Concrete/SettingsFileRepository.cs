using System;
using System.IO;

using Football.DAL.Models;
using Football.DAL.Repository.Abstract;

namespace Football.DAL.Repository.Concrete {
  internal class SettingsFileRepository : ISettingsRepository {
    private readonly Boolean _settingsExists;

    private static readonly String DIR_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Settings");
    private static readonly String FILE_PATH = Path.Combine(DIR_PATH, "settings.txt");

    public SettingsFileRepository() {
      _settingsExists = File.Exists(FILE_PATH);

      if (!_settingsExists) {
        _ = Directory.CreateDirectory(DIR_PATH);
        File.WriteAllText(FILE_PATH, Settings.DEFAULT.FormatForFile());
      }
    }

    public Boolean Exists() => _settingsExists;

    public void Save(Settings settings) =>
      File.WriteAllText(FILE_PATH, settings.FormatForFile());

    public Settings Load() =>
      Settings.Parse(File.ReadAllText(FILE_PATH));
    public void Delete() =>
      File.Delete(FILE_PATH);
  }
}
