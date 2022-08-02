using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Football.DAL.Models;
using Football.DAL.Repository.Abstract;
using Football.Library.Models;

namespace Football.DAL.Repository.Concrete {
  internal class FavouritePlayersRepository : IFavouritePlayersRepository {
    private readonly Boolean _favouritesExists;

    private static readonly String DIR_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Settings");
    private static readonly String FILE_PATH = Path.Combine(DIR_PATH, "favourites.txt");

    public FavouritePlayersRepository() {
      _favouritesExists = File.Exists(FILE_PATH);

      if (!_favouritesExists) {
        _ = Directory.CreateDirectory(DIR_PATH);
      }
    }

    public void Delete() => File.Delete(FILE_PATH);

    public Boolean Exists() => _favouritesExists;

    public Dictionary<Gender, List<Player>> Load() =>
      Enum.GetNames(typeof(Gender))
          .Zip(File.ReadAllText(FILE_PATH)
                   .Split('\n')
                   .Select(line => line.Split('$')
                                       .Select(Player.Parse)
                                       .Where(value => !(value is null))),
               (gender, players) => new KeyValuePair<Gender, List<Player>>((Gender)Enum.Parse(typeof(Gender), gender), players.ToList()))
          .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    public void Save(IDictionary<Gender, List<Player>> dict) =>
      File.WriteAllText(FILE_PATH,
                        String.Join(separator: "\n",
                                    dict.Keys.Select(selector: key => String.Join(separator: "$",
                                                                                  dict[key].Select(selector: player => player.FormatForFileLine()).ToArray()))));
  }
}
