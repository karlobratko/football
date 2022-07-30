using System;

using Newtonsoft.Json;

namespace Football.DAL.Models {
  public partial class TeamResults {
    [JsonProperty("goals")]
    public Int32 Goals { get; set; }
  }
}
