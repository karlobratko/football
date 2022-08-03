using Football.DAL.Repository.Abstract;
using Football.DAL.Repository.Concrete;

namespace Football.DAL.Repository.Factory {
  public static class SettingsRepositoryFactory {
    public static ISettingsRepository GetRepository() => new SettingsFileRepository();
  }
}
