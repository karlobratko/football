using System;

using Newtonsoft.Json;

namespace Football.DAL.Models {
  public partial class Result {
    [JsonProperty("id")]
    public Int64 Id { get; set; }

    [JsonProperty("country")]
    public String Country { get; set; }

    [JsonProperty("alternate_name")]
    public String AlternateName { get; set; }

    [JsonProperty("fifa_code")]
    public String FifaCode { get; set; }

    [JsonProperty("group_id")]
    public Int64 GroupId { get; set; }

    [JsonProperty("group_letter")]
    public String GroupLetter { get; set; }

    [JsonProperty("wins")]
    public Int32 Wins { get; set; }

    [JsonProperty("draws")]
    public Int32 Draws { get; set; }

    [JsonProperty("losses")]
    public Int32 Losses { get; set; }

    [JsonProperty("games_played")]
    public Int32 GamesPlayed { get; set; }

    [JsonProperty("points")]
    public Int32 Points { get; set; }

    [JsonProperty("goals_for")]
    public Int32 GoalsFor { get; set; }

    [JsonProperty("goals_against")]
    public Int32 GoalsAgainst { get; set; }

    [JsonProperty("goal_differential")]
    public Int32 GoalDifferential { get; set; }

    public override Boolean Equals(Object obj) => obj is Result o && Id == o.Id;
    public override Int32 GetHashCode() => 2108858624 + Id.GetHashCode();
    public override String ToString() => $"{Country} ({FifaCode})";
  }
}
