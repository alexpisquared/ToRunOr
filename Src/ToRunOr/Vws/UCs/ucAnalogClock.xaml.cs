using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToRunOr.Vws.UCs
{
  public partial class ucAnalogClock : UserControl
  {
    DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

    public ucAnalogClock()
    {
      DataContext = this;
      this.InitializeComponent();
      if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

      _timer.Tick += (s, e) => ontick();
      _timer.Start();
      Application.Current.Suspending += OnSuspending;
      Application.Current.Resuming += OnResuming;
    }
    async void OnSuspending(object sender, SuspendingEventArgs e) { await Task.Delay(100); AllHands.Visibility = Visibility.Collapsed; }
    async void OnResuming(object sender, object o) { AllHands.Visibility = Visibility.Collapsed; await Task.Delay(100); ontick(); AllHands.Visibility = Visibility.Visible; }    //public static readonly DependencyProperty SWTimeProperty = DependencyProperty.Register("SWTime", typeof(string), typeof(ucAnalogClock), new PropertyMetadata("1:23:45")); public string SWTime { get { return (string)GetValue(SWTimeProperty); } set { SetValue(SWTimeProperty, value); } }

    void ontick()
    {
      var now = DateTime.Now;

      swTime.Text = $"{now:H:mm:ss}";

      tH.Rotation = now.Hour * 30 + now.Minute / 2;        //tM.Rotation = now.Minute * 6 + now.Second / 10;        //tS.Rotation = now.Second * 6 + now.Millisecond * .006;
      edkfMinEnd.Value = 360 + (edkfMinBgn.Value = now.Minute * 6 + now.Second * .1); //kfHouEnd.Value = 360 + (edkfHouBgn.Value = (now.Hour - 12) * 30 + now.Minute * .5);
      edkfSecEnd.Value = 360 + (edkfSecBgn.Value = now.Second * 6);
    }
  }
}
