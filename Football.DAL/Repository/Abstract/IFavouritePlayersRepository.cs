using System;
using System.Collections.Generic;

using Football.DAL.Models;
using Football.Library.Models;

namespace Football.DAL.Repository.Abstract {
  public interface IFavouritePlayersRepository {
    Boolean Exists();
    void Save(IDictionary<Gender, List<Player>> dict);
    Dictionary<Gender, List<Player>> Load();
    void Delete();
  }
}
