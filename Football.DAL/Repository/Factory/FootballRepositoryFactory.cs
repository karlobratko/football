﻿using Football.DAL.Repository.Abstract;
using Football.DAL.Repository.Concrete;

namespace Football.DAL.Repository.Factory {
  public static class FootballRepositoryFactory {
    public static IFootballRepository GetRepository() => new FootballCombinedRepository();
  }
}
