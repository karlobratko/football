using System;
using System.Drawing;
using System.Windows.Forms;

using Football.DAL.Models;
using Football.Library.Helpers;

namespace Football.WinFormsUI.Controls {
  public partial class PlayerControl : UserControl, IPlayerControl {
    private Boolean _selected = false;

    public event ImageChangedEvent OnImageChanged;
    public event ImageRemovedEvent OnImageRemoved;

    public Player Player { get; set; }
    public Boolean Selected {
      get => _selected;
      set {
        _selected = value;
        BackColor = _selected ? Color.DarkGray : Color.Transparent;
      }
    }
    public String ImagePath {
      get => ImageHelper.GetImagePath(Player.Name);
      set => pbPlayer.ImageLocation = value;
    }

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
        ChangeImage();
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
          ChangeImage();
          OnImageChanged?.Invoke(sender, new ImageChangedEventArgs { ImagePath = ImageHelper.GetImagePath(Player.Name), Player = Player });
        }
      }
      catch (Exception) {
        _ = MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    public void ChangeImage() {
      pbPlayer.ImageLocation = ImageHelper.GetImagePath(Player.Name);
      tsmiRemoveImage.Enabled = true;
    }

    private void RemoveImage(Object sender, EventArgs e) {
      ImageHelper.RemoveImage(Player.Name);
      RemoveImage();
      OnImageRemoved?.Invoke(sender, new ImageRemovedEventArgs { Player = Player });
    }

    public void RemoveImage() {
      pbPlayer.Image = (Image)Properties.Resources.ResourceManager.GetObject("user");
      tsmiRemoveImage.Enabled = false;
    }

    public class ImageChangedEventArgs : EventArgs {
      public String ImagePath { get; set; }
      public Player Player { get; set; }
    }
    public delegate void ImageChangedEvent(Object sender, ImageChangedEventArgs args);

    public class ImageRemovedEventArgs : EventArgs {
      public Player Player { get; set; }
    }
    public delegate void ImageRemovedEvent(Object sender, ImageRemovedEventArgs args);
  }
}
