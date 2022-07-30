using System;
using System.IO;

namespace Football.DAL.Configuration {
  internal static class Endpoints {
    public const String MALE_RESULTS_URL = "https://world-cup-json-2018.herokuapp.com/teams/results";
    public const String MALE_MATCHES_URL = "https://world-cup-json-2018.herokuapp.com/matches";
    public const String FEMALE_RESULTS_URL = "https://worldcup.sfg.io/teams/results";
    public const String FEMALE_MATCHES_URL = "https://worldcup.sfg.io/matches";
    public static readonly String MALE_MATCHES_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Resources/men/matches.json");
    public static readonly String MALE_RESULTS_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Resources/men/results.json");
    public static readonly String MALE_COUNTRIES_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Resources/men/teams.json");
    public static readonly String FEMALE_MATCHES_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Resources/women/matches.json");
    public static readonly String FEMALE_RESULTS_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Resources/women/results.json");
    public static readonly String FEMALE_COUNTRIES_PATH = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Football.Resources/women/teams.json");
  }
}
