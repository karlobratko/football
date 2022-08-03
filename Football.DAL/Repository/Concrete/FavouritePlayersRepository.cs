using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Football.DAL.Models;
using Football.DAL.Repository.Abstract;
using Football.Library.Models;

namespace Football.DAL.Repository.Concrete {
  internal class FavouritePlayersRepository : IFavouritePlayersRepository {
    private const Char LIST_DELIM = '\n';
    private const Char ELEM_DELIM = '$';

    private static readonly String DIR_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Settings");
    private static readonly String FILE_PATH = Path.Combine(DIR_PATH, "favourites.txt");

    private readonly Boolean _favouritesExists;

    public FavouritePlayersRepository() {
      _favouritesExists = File.Exists(path: FILE_PATH);

      if (!_favouritesExists) {
        _ = Directory.CreateDirectory(path: DIR_PATH);
      }
    }

    public Boolean Exists() => _favouritesExists;

    public void Save(IDictionary<Gender, List<Player>> dict) =>
      File.WriteAllText(
        path: FILE_PATH,
        contents: String.Join(
          separator: LIST_DELIM.ToString(),
          values: dict.Keys.Select(selector: key =>
            String.Join(separator: ELEM_DELIM.ToString(),
                        values: dict[key].Select(selector: player =>
                          player.FormatForFile())))));

    public Dictionary<Gender, List<Player>> Load() =>
      Enum.GetNames(typeof(Gender))
          .Zip(second: File.ReadAllText(path: FILE_PATH)
                           .Split(separator: LIST_DELIM)
                           .Select(selector: line =>
                             line.Split(separator: ELEM_DELIM)
                                 .Select(selector: Player.Parse)
                                 .Where(predicate: value => !(value is null))),
               resultSelector: (gender, players) =>
                 new KeyValuePair<Gender, List<Player>>(key: (Gender)Enum.Parse(typeof(Gender), gender),
                                                        value: players.ToList()))
          .ToDictionary(keySelector: kvp => kvp.Key,
                        elementSelector: kvp => kvp.Value);

    public void Delete() => File.Delete(path: FILE_PATH);
  }
}
