using System;
using System.IO;

using Football.DAL.Models;
using Football.DAL.Repository.Abstract;

namespace Football.DAL.Repository.Concrete {
  internal class SettingsFileRepository : ISettingsRepository {
    private static readonly String DIR_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Settings");
    private static readonly String FILE_PATH = Path.Combine(DIR_PATH, "settings.txt");

    private readonly Boolean _settingsExists;

    public SettingsFileRepository() {
      _settingsExists = File.Exists(path: FILE_PATH);

      if (!_settingsExists) {
        _ = Directory.CreateDirectory(DIR_PATH);
        File.WriteAllText(path: FILE_PATH,
                          contents: Settings.DEFAULT.FormatForFile());
      }
    }

    public Boolean Exists() => _settingsExists;

    public void Save(Settings settings) =>
      File.WriteAllText(path: FILE_PATH,
                        contents: settings.FormatForFile());

    public Settings Load() =>
      Settings.Parse(line: File.ReadAllText(path: FILE_PATH));
    public void Delete() =>
      File.Delete(path: FILE_PATH);
  }
}
