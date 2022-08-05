using System.ComponentModel;

using Football.Library.Attributes;
using Football.Library.Converters;

namespace Football.Library.Models {
  [TypeConverter(typeof(EnumDescriptionTypeConverter))]
  public enum Language {
    [LocalizedDescription("En", typeof(Resources.Global.Resources))]
    En,
    [LocalizedDescription("Hr", typeof(Resources.Global.Resources))]
    Hr
  }
}
