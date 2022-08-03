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

    public Player Player { get; }

    public Boolean Selected {
      get => _selected;
      set {
        _selected = value;
        BackColor = _selected ? Color.DarkGray : Color.Transparent;
      }
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

      if (ImageHelper.ImageExists(fileName: Player.Name)) {
        ChangeImage();
      }
    }

    private void ChangeImage(Object sender, EventArgs e) {
      try {
        using (var ofd = new OpenFileDialog() {
          CheckFileExists = true,
          CheckPathExists = true,
          Multiselect = false,
          Filter = "JPG files (*.jpg)|*.jpg| PNG files (*.png)|*.png"
        }) {

          if (ofd.ShowDialog() == DialogResult.OK) {
            ImageHelper.ReplaceImage(srcPath: ofd.FileName, dstFileName: Player.Name);
            ChangeImage();
            OnImageChanged?.Invoke(sender,
                                   new ImageChangedEventArgs {
                                     ImagePath = ImageHelper.GetImagePath(fileName: Player.Name),
                                     Player = Player
                                   });
          }
        }
      }
      catch (Exception) {
        _ = MessageBox.Show(text: "An Error Occured",
                            caption: "Error",
                            buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Error);
      }
    }

    public void ChangeImage() {
      pbPlayer.ImageLocation = ImageHelper.GetImagePath(fileName: Player.Name);
      tsmiRemoveImage.Enabled = true;
    }

    private void RemoveImage(Object sender, EventArgs e) {
      ImageHelper.RemoveImage(fileName: Player.Name);
      DefaultImage();
      OnImageRemoved?.Invoke(sender, new ImageRemovedEventArgs { Player = Player });
    }

    public void DefaultImage() {
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
