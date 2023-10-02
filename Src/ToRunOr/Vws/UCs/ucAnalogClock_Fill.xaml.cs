using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Power;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToRunOr.Vws.UCs
{
  public partial class ucAnalogClock_Fill : UserControl
  {
    readonly DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
    private bool _isTalking;

    public ucAnalogClock_Fill()
    {
      this.InitializeComponent();
      if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

#if !DEBUG
      rbDbg3.Visibility = Visibility.Collapsed;
#endif

      _timer.Tick += (s, e) => OnTick();
      _timer.Start();
    }
    async void OnTick()
    {
      try
      {
        const int audioWindowSec = 8;

        var now = DateTime.Now;

        tH.Rotation = (now.Hour * 30) + (now.Minute / 2);
        //tM.Rotation = now.Minute * 6 + now.Second / 10;
        //tS.Rotation = now.Second * 6 + now.Millisecond * .006;

        edkfSecEnd.Value = 360 + (edkfSecBgn.Value = now.Second * 6);
        edkfMinEnd.Value = 360 + (edkfMinBgn.Value = (now.Minute * 6) + (now.Second * .1));
        //kfHouEnd.Value = 360 + (edkfHouBgn.Value = (now.Hour - 12) * 30 + now.Minute * .5);

        swSwch.Text = "· ·";
        swTime.Text = $"{now:H:mm:ss}";

        var secondsLeft = ((playPeriodInMin - (now.Minute % playPeriodInMin)) * 60) - 60 + now.Second;

        pb1.Maximum = playPeriodInMin * 60;
        pb1.Value = (now.Minute % playPeriodInMin * 60) + now.Second;

        if (!_isTalking)
          pb1.Foreground = secondsLeft < 60 ? new Windows.UI.Xaml.Media.SolidColorBrush(secondsLeft % 2 == 0 ? Windows.UI.Colors.Red : Windows.UI.Colors.DarkOrange) : new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.DarkMagenta);

        if (now.Second > audioWindowSec || _isTalking)
          return;

        _isTalking = true;
        var minutesLeft = playPeriodInMin - (now.Minute % playPeriodInMin);
        if (now.Minute % playPeriodInMin == playPeriodInMin - 1)
        {
          await PlayWav("Start - Arcade Power Up.wav");
          await Task.Delay(0_780);
          await ucRadar.Speak0(media, $"Last minute!");
        }
        else if (now.Minute % playPeriodInMin == 0)
        {
          await PlayWav("Good - Fanfare.wav");
          await Task.Delay(5_800);
          await ucRadar.Speak0(media, "Time to change!");
        }
        else if (now.Minute % playPeriodInMin != 0 && chSayMinutes.IsChecked == true)
        {
          await PlayWav("PopBest.wav");
          await Task.Delay(0_780);
          await ucRadar.Speak0(media, $"{minutesLeft} minute{(minutesLeft > 1 ? "s" : "")} left");
        }

        tbBattery.Text = GetBattery();

        await Task.Delay(audioWindowSec * 1_000);
        _isTalking = false;
      }
      catch (Exception ex) { Debug.WriteLine(ex.Message, "time()"); if (Debugger.IsAttached) Debugger.Break(); throw; }
    }

    string GetBattery()
    {
      var batteryReport = Battery.AggregateBattery.GetReport();
      var percentLeft = batteryReport.FullChargeCapacityInMilliwattHours == null ||
          batteryReport.FullChargeCapacityInMilliwattHours.Value == 0 ||
          batteryReport.RemainingCapacityInMilliwattHours == null ? 0 : 100d * batteryReport.RemainingCapacityInMilliwattHours.Value / batteryReport.FullChargeCapacityInMilliwattHours.Value;

      pbBattery.Value = 100 - percentLeft;

      return $"{percentLeft,3:N0} %";
    }

    async Task PlayWav(string v)
    {
      // play Alarm01.wav file from Assets folder:
      var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
      var file = await folder.GetFileAsync(v);
      var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
      media.SetSource(stream, file.ContentType);
      //media.Play();
    }

    void OnChangePeriod(object s, RoutedEventArgs e)
    {
      playPeriodInMin = int.Parse(((RadioButton)s).Content.ToString());
      pb1.Maximum = playPeriodInMin * 60;
    }

    int playPeriodInMin = 10;
  }
}
