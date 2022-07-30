using System.Collections.Generic;
using System.Threading.Tasks;

using Football.DAL.Models;
using Football.Library.Models;

namespace Football.DAL.Repository.Abstract {
  public interface IFootballRepository {
    Task<IEnumerable<Country>> ReadCountries(Gender gender);
    Task<IEnumerable<Match>> ReadMatches(Gender gender);
    Task<IEnumerable<Result>> ReadResults(Gender gender);
  }
}
