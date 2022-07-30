using System;
using System.Drawing;

using Newtonsoft.Json;

namespace Football.DAL.Models {
  public partial class MatchEvent {
    [JsonProperty("type_of_event")]
    public String TypeOfEvent { get; set; }

    [JsonProperty("player")]
    public String Player { get; set; }
  }
}
