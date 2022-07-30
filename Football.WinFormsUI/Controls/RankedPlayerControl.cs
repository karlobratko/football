using System;
using System.Windows.Forms;

using Football.DAL.Models;
using Football.Library.Helpers;

namespace Football.WinFormsUI.Controls {
  public partial class RankedPlayerControl : UserControl {
    private Player Player { get; set; }

    public RankedPlayerControl(Player player) {
      InitializeComponent();
      Player = player;

      Init();
    }

    private void Init() {
      lblName.Text = Player.Name;
      lblShirtNumber.Text = Player.ShirtNumber.ToString();
      lblPosition.Text = Player.Position.ToString();
      lblGoals.Text = Player.Goals.ToString();
      lblYellowCards.Text = Player.YellowCards.ToString();

      if (ImageHelper.ImageExists(Player.Name)) {
        pbPlayer.ImageLocation = ImageHelper.GetImagePath(Player.Name);
      }
    }
  }
}
