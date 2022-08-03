using System;

using Newtonsoft.Json;

namespace Football.DAL.Models {
  public partial class MatchEvent {
    [JsonProperty("type_of_event")]
    public String TypeOfEvent { get; set; }

    [JsonProperty("player")]
    public String Player { get; set; }

    public static Boolean IsGoal(MatchEvent e) => 
      e.TypeOfEvent == "goal" || e.TypeOfEvent == "goal-penalty";

    public static Boolean IsYellowCard(MatchEvent e) => 
      e.TypeOfEvent == "yellow-card";
  }
}
