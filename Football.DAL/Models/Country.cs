using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Football.DAL.Models {
  public partial class Country {
    private const Char DELIM = '#';

    [JsonProperty("country")]
    public String Name { get; set; }

    [JsonProperty("fifa_code")]
    public String FifaCode { get; set; }

    public static Country Parse(String line) {
      String[] data = line.Split(DELIM);
      return data.Length == 2
        ? new Country {
          Name = data[0],
          FifaCode = data[1]
        }
        : null;
    }
    public String FormatForFile() => $"{Name}{DELIM}{FifaCode}";

    public override Boolean Equals(Object obj) => obj is Country o && Name == o.Name && FifaCode == o.FifaCode;
    public override Int32 GetHashCode() => -975964944 + EqualityComparer<String>.Default.GetHashCode(FifaCode);
    public override String ToString() => $"{Name} ({FifaCode})";
  }
}
