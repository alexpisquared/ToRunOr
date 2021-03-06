﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToRunOr.Vws.UCs
{
  public partial class ucAnalogClock_Fill : UserControl
  {
    DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

    public ucAnalogClock_Fill()
    {
      this.InitializeComponent();
      if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

      _timer.Tick += (s, e) => ontick();
      _timer.Start();
    }
    async void ontick()
    {
      try
      {
        const int bigPeriod = 15;
        var now = DateTime.Now;
        var ts = now - MainPage.StartTime;

        tH.Rotation = now.Hour * 30 + now.Minute / 2;
        //tM.Rotation = now.Minute * 6 + now.Second / 10;
        //tS.Rotation = now.Second * 6 + now.Millisecond * .006;

        edkfSecEnd.Value = 360 + (edkfSecBgn.Value = now.Second * 6);
        edkfMinEnd.Value = 360 + (edkfMinBgn.Value = now.Minute * 6 + now.Second * .1);
        //kfHouEnd.Value = 360 + (edkfHouBgn.Value = (now.Hour - 12) * 30 + now.Minute * .5);

        swSwch.Text = ts < TimeSpan.FromHours(1) ? $"{ts:m\\:ss}" : $"{ts:h\\:mm\\:ss}";
        swTime.Text = $"{now:H:mm:ss}";

        int sec = (int)ts.TotalSeconds;
        if (sec < 1) return;
        else if (sec % 60 == 0)
          await ucRadar.Speak0(media, $"{ts.Minutes} minute{(ts.Minutes > 1 ? "s" : "")}");
        else if (sec % bigPeriod == 0)
          await ucRadar.Speak0(media, sec <= 60 ? $"{sec} seconds" : $"{ts.Minutes} minute{(ts.Minutes > 1 ? "s" : "")} {ts.Seconds} seconds");
      }
      catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message, "time()"); if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break(); throw; }
    }
  }
}
