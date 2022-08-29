using System;
using System.Windows;

using Football.DAL.Models;
using Football.Library.Extensions;
using Football.Library.Helpers;

namespace Football.WPFUI.Windows {
  public partial class PlayerWindow : Window {
    public Player Player { get; private set; }

    public PlayerWindow(Player player) {
      Player = player;

      InitializeComponent();
      Init();
    }

    private void Init() {
      Title = Player.Name;
      lblName.Content = Player.Name;
      lblShirtNumber.Content = Player.ShirtNumber.ToString();
      lblCaptain.Content = Player.Captain ? "Yes" : "No";
      lblPosition.Content = Player.Position;
      lblGoals.Content = Player.Goals.ToString();
      lblYellowCards.Content = Player.YellowCards.ToString();

      if (ImageHelper.ImageExists(fileName: Player.Name))
        imgPlayer.Source = ImageHelper.LoadImage(fileName: Player.Name).ToBitmap();
    }

    private void CloseWindow(Object sender, RoutedEventArgs e) => Close();
  }
}
