using Football.DAL.Models;

namespace Football.WinFormsUI.Controls {
  public interface IPlayerControl {
    Player Player { get; set; }
    void RemoveImage();
    void ChangeImage();
  }
}