using System;
using System.Windows.Controls;

using Football.DAL.Models;
using Football.Library.Extensions;
using Football.Library.Helpers;

namespace Football.WPFUI.Controls {
  public partial class PlayerControl : UserControl {
    public Player Player { get; }

    public PlayerControl(Player player) {
      Player = player;
      InitializeComponent();

      Init();
    }

    private void Init() {
      lblName.Content = SimplifyName(Player.Name);
      if (ImageHelper.ImageExists(fileName: Player.Name))
        imgPlayer.Source = ImageHelper.LoadImage(fileName: Player.Name).ToBitmap();
    }

    private String SimplifyName(String fullName) {
      String[] parts = fullName.Split(' ');
      return parts[0] + " " + parts[1].Substring(0, 3);
    }
  }
}
