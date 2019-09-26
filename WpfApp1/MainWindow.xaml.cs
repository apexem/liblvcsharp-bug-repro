using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      Core.Initialize();
      InitializeComponent();
      vlc.Loaded += Vlc_Loaded;
    }

    private void Vlc_Loaded(object sender, RoutedEventArgs e)
    {
      var libvlc = new LibVLC();
      libvlc.SetLogFile("logs");
      vlc.MediaPlayer = new MediaPlayer(libvlc);

      // handler used used to change state of media player;
      // rewinding video is not possible when state is "Nothing Special"
      vlc.MediaPlayer.Playing += async (s, ev) =>
      {
        await vlc.Dispatcher.InvokeAsync(() =>
        {
          vlc.MediaPlayer.Pause();
        });
      };
      var path = "BigBuckBunny.mp4";
      vlc.MediaPlayer.Play(
        new Media(libvlc, path, FromType.FromPath));
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      vlc.MediaPlayer.Position = (float)e.NewValue / 1000;
    }
  }
}
