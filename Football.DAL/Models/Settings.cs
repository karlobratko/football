﻿using System;

using Football.Library.Models;

namespace Football.DAL.Models {
  public class Settings {
    private const Char DELIM = '|';

    public Language Language { get; set; }
    public Gender Gender { get; set; }
    public Tuple<Int32, Int32> Resolution { get; set; }
    public Country Country { get; set; }

    public static Settings DEFAULT =>
      new Settings {
        Language = Language.En,
        Gender = Gender.Male,
        Resolution = Tuple.Create(1000, 800),
        Country = null
      };

    public static Settings Parse(String line) {
      String[] data = line.Split(DELIM);

      return new Settings {
        Language = data.Length > 0 ? (Language)Enum.Parse(typeof(Language), data[0]) : DEFAULT.Language,
        Gender = data.Length > 1 ? (Gender)Enum.Parse(typeof(Gender), data[1]) : DEFAULT.Gender,
        Resolution = data.Length > 3 ? Tuple.Create(Int32.Parse(data[2]), Int32.Parse(data[3])) : DEFAULT.Resolution,
        Country = data.Length > 4 ? Country.Parse(data[4]) : DEFAULT.Country
      };
    }
    public String FormatForFile() => 
      $"{Language:d}{DELIM}{Gender:d}{DELIM}{Resolution.Item1}{DELIM}{Resolution.Item2}{DELIM}{Country?.FormatForFile()}";
  }
}
