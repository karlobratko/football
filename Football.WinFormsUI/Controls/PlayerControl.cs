using System;
using System.Drawing;
using System.Windows.Forms;

using Football.DAL.Models;
using Football.Library.Helpers;

namespace Football.WinFormsUI.Controls {
  public partial class PlayerControl : UserControl {
    private Player Player { get; set; }

    public PlayerControl(Player player) {
      InitializeComponent();

      Player = player;
      Init();
    }

    private void Init() {
      lblName.Text = Player.Name;
      lblShirtNumber.Text = Player.ShirtNumber.ToString();
      lblPosition.Text = Player.Position.ToString();
      lblCaptain.Text = Player.Captain ? Properties.Resources.ResourceManager.GetString("player-captain") : "";
      pbFavourite.Visible = Player.IsFavourite;

      if (ImageHelper.ImageExists(Player.Name)) {
        pbPlayer.ImageLocation = ImageHelper.GetImagePath(Player.Name);
        tsmiRemoveImage.Enabled = true;
      }
    }

    private void ChangeImage(Object sender, EventArgs e) {
      try {
        var ofd = new OpenFileDialog() {
          CheckFileExists = true,
          CheckPathExists = true,
          Multiselect = false,
          Filter = "JPG files (*.jpg)|*.jpg| PNG files (*.png)|*.png"
        };

        if (ofd.ShowDialog() == DialogResult.OK) {
          ImageHelper.ReplaceImage(fileName: Player.Name, path: ofd.FileName);
          pbPlayer.ImageLocation = ImageHelper.GetImagePath(Player.Name);
          tsmiRemoveImage.Enabled = true;
        }
      }
      catch (Exception) {
        _ = MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      if (ImageHelper.ImageExists(Player.Name)) {
        pbPlayer.Image = ImageHelper.LoadImage(Player.Name);
      }
    }

    private void RemoveImage(Object sender, EventArgs e) {
      ImageHelper.RemoveImage(Player.Name);
      pbPlayer.Image = (Image)Properties.Resources.ResourceManager.GetObject("user");
      tsmiRemoveImage.Enabled = false;
    }

    private void PlayerControl_Load(Object sender, EventArgs e) {

    }
  }
}
