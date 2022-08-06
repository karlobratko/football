using System;

using Football.Library.Models;

namespace Football.DAL.Models {
  public class Settings {
    private const Char DELIM = '|';

    public Language Language { get; set; }
    public Gender Gender { get; set; }
    public Resolution Resolution { get; set; }
    public Country Country { get; set; }
    public Country HomeCountry { get; set; }
    public Country AwayCountry { get; set; }

    public static Settings DEFAULT =>
      new Settings {
        Language = Language.En,
        Gender = Gender.Male,
        Resolution = Resolution.DEFAULT,
        Country = null,
        HomeCountry = null,
        AwayCountry = null
      };

    public static Resolution[] RESOLUTIONS =>
      new Resolution[] {
        new Resolution(800, 600),
        new Resolution(1280, 720),
        new Resolution(1920, 1080),
      };

    public static Settings Parse(String line) {
      String[] data = line.Split(DELIM);

      return new Settings {
        Language = data.Length > 0 ? (Language)Enum.Parse(typeof(Language), data[0]) : DEFAULT.Language,
        Gender = data.Length > 1 ? (Gender)Enum.Parse(typeof(Gender), data[1]) : DEFAULT.Gender,
        Resolution = data.Length > 2 ? Resolution.Parse(data[2]) : Resolution.DEFAULT,
        Country = data.Length > 3 ? Country.Parse(data[3]) : DEFAULT.Country,
        HomeCountry = data.Length > 4 ? Country.Parse(data[4]) : DEFAULT.HomeCountry,
        AwayCountry = data.Length > 5 ? Country.Parse(data[5]) : DEFAULT.AwayCountry
      };
    }
    public String FormatForFile() =>
      $"{Language:d}{DELIM}" +
      $"{Gender:d}{DELIM}" +
      $"{Resolution.FormatForFile()}{DELIM}" +
      $"{Country?.FormatForFile()}{DELIM}" +
      $"{HomeCountry?.FormatForFile()}{DELIM}" +
      $"{AwayCountry?.FormatForFile()}";
  }
}
