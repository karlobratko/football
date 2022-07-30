using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Football.DAL.Models {
  public partial class Match {
    [JsonProperty("fifa_id")]
    public Int64 Id { get; set; }

    [JsonProperty("venue")]
    public String Venue { get; set; }

    [JsonProperty("location")]
    public String Location { get; set; }

    [JsonProperty("attendance")]
    public Int64 Attendance { get; set; }

    [JsonProperty("home_team_country")]
    public String HomeCountry { get; set; }

    [JsonProperty("away_team_country")]
    public String AwayCountry { get; set; }

    [JsonProperty("datetime")]
    public DateTimeOffset Datetime { get; set; }

    [JsonProperty("winner")]
    public String Winner { get; set; }

    [JsonProperty("home_team")]
    public TeamResults HomeResults { get; set; }

    [JsonProperty("away_team")]
    public TeamResults AwayResults { get; set; }

    [JsonProperty("home_team_events")]
    public List<MatchEvent> HomeEvents { get; set; }

    [JsonProperty("away_team_events")]
    public List<MatchEvent> AwayEvents { get; set; }

    [JsonProperty("home_team_statistics")]
    public TeamLayout HomeLayout { get; set; }

    [JsonProperty("away_team_statistics")]
    public TeamLayout AwayLayout { get; set; }

    public override Boolean Equals(Object obj) => obj is Match o && Id == o.Id;
    public override Int32 GetHashCode() => 2108858624 + Id.GetHashCode();
    public override String ToString() => $"{AwayCountry}";
  }
}
