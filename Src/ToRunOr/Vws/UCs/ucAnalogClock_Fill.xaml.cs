using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToRunOr.Vws.UCs
{
  public partial class ucAnalogClock_Fill : UserControl
  {
    DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
    private bool _isTalking;

    public ucAnalogClock_Fill()
    {
      this.InitializeComponent();
      if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

      _timer.Tick += (s, e) => OnTick();
      _timer.Start();
    }
    async void OnTick()
    {
      try
      {
        const int playPeriodInMin =
#if DEBUG
          10;
#else
         15;
#endif
        var now = DateTime.Now;

        tH.Rotation = now.Hour * 30 + now.Minute / 2;
        //tM.Rotation = now.Minute * 6 + now.Second / 10;
        //tS.Rotation = now.Second * 6 + now.Millisecond * .006;

        edkfSecEnd.Value = 360 + (edkfSecBgn.Value = now.Second * 6);
        edkfMinEnd.Value = 360 + (edkfMinBgn.Value = now.Minute * 6 + now.Second * .1);
        //kfHouEnd.Value = 360 + (edkfHouBgn.Value = (now.Hour - 12) * 30 + now.Minute * .5);

        swSwch.Text = "· ·";
        swTime.Text = $"{now:H:mm:ss}";

        var secondsLeft = (playPeriodInMin - now.Minute % playPeriodInMin) * 60 + 60 - now.Second;

        pb1.Maximum = playPeriodInMin * 60;
        pb1.Value = now.Minute % playPeriodInMin * 60 + now.Second;

        if (now.Second > 5 || _isTalking)
          return;

        _isTalking = true;
        var minutesLeft = playPeriodInMin - now.Minute % playPeriodInMin;
        if (now.Minute % playPeriodInMin != 0)
          await ucRadar.Speak0(media, $"{minutesLeft} minute{(minutesLeft > 1 ? "s" : "")} left");
        else
        {
          await ucRadar.Speak0(media, "Time to change!");
          // play Alarm01.wav file from Assets folder:
          var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
          var file = await folder.GetFileAsync("Good - Fanfare.wav");
          var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
          media.SetSource(stream, file.ContentType);
          media.Play();
          await Task.Delay(5_000);
          await ucRadar.Speak0(media, "Time to change!");
        }

        await Task.Delay(5_000);
        _isTalking = false;

      }
      catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message, "time()"); if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break(); throw; }
    }
  }
}
