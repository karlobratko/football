using System;
using System.Windows.Markup;

namespace Football.WPFUI.Binding {
  public class EnumBindingExtension : MarkupExtension {
    public Type EnumType { get; }

    public EnumBindingExtension(Type enumType) =>
      EnumType = enumType ?? throw new ArgumentNullException(nameof(enumType));

    public override Object ProvideValue(IServiceProvider serviceProvider) => Enum.GetValues(EnumType);
  }
}
