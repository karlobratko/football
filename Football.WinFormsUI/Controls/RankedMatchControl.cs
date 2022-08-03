using System.Windows.Forms;

using Football.DAL.Models;

namespace Football.WinFormsUI.Controls {
  public partial class RankedMatchControl : UserControl {
    public Match Stadium { get; private set; }

    public RankedMatchControl(Match stadium) {
      InitializeComponent();
      Stadium = stadium;

      Init();
    }

    private void Init() {
      lblHomeTeam.Text = Stadium.HomeCountry;
      lblAwayTeam.Text = Stadium.AwayCountry;
      lblLocation.Text = Stadium.Location;
      lblAttendance.Text = Stadium.Attendance.ToString();
    }
  }
}
