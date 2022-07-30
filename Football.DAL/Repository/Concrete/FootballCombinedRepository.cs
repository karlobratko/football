using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

using Football.DAL.Configuration;
using Football.DAL.Models;
using Football.DAL.Repository.Abstract;
using Football.Library.Models;

using Newtonsoft.Json;

using RestSharp;

namespace Football.DAL.Repository.Concrete {
  internal class FootballCombinedRepository : IFootballRepository {
    public Task<IEnumerable<Match>> ReadMatches(Gender gender) =>
      gender == Gender.Male
        ? Read<Match>(Endpoints.MALE_MATCHES_PATH, Endpoints.MALE_MATCHES_URL)
        : Read<Match>(Endpoints.FEMALE_MATCHES_PATH, Endpoints.FEMALE_MATCHES_URL);
    public Task<IEnumerable<Result>> ReadResults(Gender gender) =>
      gender == Gender.Male
        ? Read<Result>(Endpoints.MALE_RESULTS_PATH, Endpoints.MALE_RESULTS_URL)
        : Read<Result>(Endpoints.FEMALE_RESULTS_PATH, Endpoints.FEMALE_RESULTS_URL);
    public Task<IEnumerable<Country>> ReadCountries(Gender gender) =>
      gender == Gender.Male
        ? Read<Country>(Endpoints.MALE_COUNTRIES_PATH, Endpoints.MALE_RESULTS_URL)
        : Read<Country>(Endpoints.FEMALE_COUNTRIES_PATH, Endpoints.FEMALE_RESULTS_URL);

    private Task<IEnumerable<T>> Read<T>(String path, String url) =>
      Boolean.Parse(ConfigurationManager.AppSettings["ReadJsonFromFS"])
        ? Task.Run(() => ReadJsonFromFS<T>(path))
        : Task.Run(() => ReadJsonFromAPI<T>(url));

    private IEnumerable<T> ReadJsonFromFS<T>(String path) {
      using (var reader = new StreamReader(path)) {
        String json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<HashSet<T>>(json);
      }
    }

    private IEnumerable<T> ReadJsonFromAPI<T>(String url) {
      using (var apiClient = new RestClient(url)) {
        RestResponse<HashSet<T>> response = apiClient.Execute<HashSet<T>>(new RestRequest());
        return JsonConvert.DeserializeObject<HashSet<T>>(response.Content);
      }
    }
  }
}
