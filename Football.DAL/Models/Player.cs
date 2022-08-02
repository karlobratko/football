using System;
using System.Collections.Generic;
using System.Drawing;

using Football.Library.Models;

using Newtonsoft.Json;

namespace Football.DAL.Models {
  [Serializable]
  public partial class Player {
    private const Char DELIM = '|';

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

    internal static Player Parse(String line) {
      if (line.Length == 0) return null;
      String[] data = line.Split(DELIM);

      return new Player {
        Name = data.Length > 0 ? data[0] : String.Empty,
        Captain = data.Length > 1 && Boolean.Parse(data[1]),
        ShirtNumber = data.Length > 2 ? Int32.Parse(data[2]) : -1,
        Position = data.Length > 3 ? (Position)Enum.Parse(typeof(Position), data[3]) : Position.Defender
      };
    }

    internal String FormatForFileLine() => $"{Name}{DELIM}{Captain}{DELIM}{ShirtNumber}{DELIM}{Position}";
  }
}
