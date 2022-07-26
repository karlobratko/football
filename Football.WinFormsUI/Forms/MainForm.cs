using System.Threading;
using System.Windows.Forms;

using Football.DAL.Models;
using Football.Library.Extensions;

namespace Football.WinFormsUI {
  public partial class MainForm : Form {
    private readonly Settings _settings;

    public MainForm() => InitializeComponent();

    public MainForm(Settings settings) {
      _settings = settings;

      Thread.CurrentThread.SetLanguage(_settings.Language);
      InitializeComponent();
    }
  }
}
