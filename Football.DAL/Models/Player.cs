using System;
using System.Collections.Generic;
using System.Drawing;

using Newtonsoft.Json;

namespace Football.DAL.Models {
  public partial class Player {
    [JsonProperty("name")]
    public String Name { get; set; }

    [JsonProperty("captain")]
    public Boolean Captain { get; set; }

    [JsonProperty("shirt_number")]
    public Int32 ShirtNumber { get; set; }

    [JsonProperty("position")]
    public Position Position { get; set; }

    public Boolean IsFavourite { get; set; }
    public Image Image { get; set; }
    public String ImagePath { get; set; }

    public Int32 Goals { get; set; }
    public Int32 YellowCards { get; set; }

    public override Boolean Equals(Object obj) =>
      obj is Player o &&
      Name == o.Name &&
      ShirtNumber == o.ShirtNumber &&
      Position == o.Position;

    public override Int32 GetHashCode() {
      Int32 hashCode = 488022466;
      hashCode = (hashCode * -1521134295) + EqualityComparer<String>.Default.GetHashCode(Name);
      hashCode = (hashCode * -1521134295) + ShirtNumber.GetHashCode();
      hashCode = (hashCode * -1521134295) + Position.GetHashCode();
      return hashCode;
    }

    internal String FormatForFileLine() => $"{Name} {ImagePath}";
  }
}
