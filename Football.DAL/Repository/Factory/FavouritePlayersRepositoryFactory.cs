using Football.DAL.Repository.Abstract;
using Football.DAL.Repository.Concrete;

namespace Football.DAL.Repository.Factory {
  public static class FavouritePlayersRepositoryFactory {
    public static IFavouritePlayersRepository GetRepository() => new FavouritePlayersRepository();
  }
}
