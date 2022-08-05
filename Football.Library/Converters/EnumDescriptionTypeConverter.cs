using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Football.Library.Converters {
  public class EnumDescriptionTypeConverter : EnumConverter {
    public EnumDescriptionTypeConverter(Type type)
      : base(type) {
    }

    public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType) {
      if (destinationType == typeof(String)) {
        if (!(value is null)) {
          FieldInfo fi = value.GetType().GetField(value.ToString());
          if (!(fi is null)) {
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 && !String.IsNullOrEmpty(attributes[0].Description)
              ? attributes[0].Description
              : value.ToString();
          }
        }
      }

      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
