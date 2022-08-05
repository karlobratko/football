using System;
using System.ComponentModel;
using System.Resources;

namespace Football.Library.Attributes {
  public class LocalizedDescriptionAttribute : DescriptionAttribute {
    private readonly String _resourceKey;
    private readonly ResourceManager _resourceManager;

    public LocalizedDescriptionAttribute(String resourceKey, Type resourceType) {
      _resourceKey = resourceKey;
      _resourceManager = new ResourceManager(resourceType);
    }

    public override String Description {
      get {
        String description = _resourceManager.GetString(_resourceKey);
        return String.IsNullOrWhiteSpace(description)
          ? $"[[{_resourceKey}]]"
          : description;
      }
    }
  }
}
