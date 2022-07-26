using System.Globalization;
using System.Threading;

using Football.Library.Models;

namespace Football.Library.Extensions {
  public static class ThreadExtensions {
    public static void SetLanguage(this Thread thread, Language language) {
      var culture = new CultureInfo(language.ToString());

      thread.CurrentUICulture = culture;
      thread.CurrentCulture = culture;
    }
  }
}
