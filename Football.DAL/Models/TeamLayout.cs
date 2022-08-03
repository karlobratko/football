using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace Football.DAL.Models {
  public partial class TeamLayout {
    [JsonProperty("country")]
    public String Country { get; set; }

    [JsonProperty("starting_eleven")]
    public HashSet<Player> StartingEleven { get; set; }

    [JsonProperty("substitutes")]
    public HashSet<Player> Substitutes { get; set; }

    public HashSet<Player> GetPlayers() => StartingEleven.Concat(Substitutes).ToHashSet();
  }
}
