using System;

using Football.DAL.Models;

namespace Football.DAL.Repository.Abstract {
  public interface ISettingsRepository {
    Boolean Exists();
    void Save(Settings settings);
    Settings Load();
    void Delete();
  }
}
