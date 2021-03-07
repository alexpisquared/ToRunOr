using AsLink;
using Cmn.AsLink;
using Cmn.Model;
using GpsFitCentral;
using HtmlAgilityPack;
using RadarAnimation.Cmn.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VMs;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using XSD.CLS;

namespace ToRunOr.Vws.UCs
{
  public sealed partial class UcEnvtCanHtml_PastFore24Hr_NonMvvm : UserControl
  {
    const int _scrHt = 1440, _lowerIconBarHt = 233, _crtHt = _scrHt - _lowerIconBarHt, _scrWh = 3465, _25hRangeInMin = 2000;
    const double _windDivisor = 2.5, _minDegMargin = 0.5, _maxDegMargin = 3, _pastK = .15;
    double _tdcMax = 42, _tdcMin = -1, _pxlPerDeg, _pxlPerMin, _pxlPerMi_, _25hRangeInMi_;
    EnvNormals _normalsForTheDay;
    readonly Brush
      brushEdg = new SolidColorBrush(Color.FromArgb(32, 255, 255, 255)),
      brushDay = CreateLinearGradientBrush_HorizontAcross(Color.FromArgb(64, 255, 255, 255), Color.FromArgb(16, 255, 255, 255), Color.FromArgb(64, 255, 255, 255)),
      brushNte = CreateLinearGradientBrush_HorizontAcross(Color.FromArgb(64, 000, 000, 000), Color.FromArgb(16, 000, 000, 000), Color.FromArgb(64, 000, 000, 000)),
      brushNowL = new SolidColorBrush(Colors.Yellow),
      brushSlvr = new SolidColorBrush(Colors.WhiteSmoke),
      brushWite = new SolidColorBrush(Colors.White),
      brushCFFF = new SolidColorBrush(Color.FromArgb(160, 255, 255, 255)),
      brushCFFg = new SolidColorBrush(Color.FromArgb(160, 255, 255, 255)),
      brushBlck = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
      brushCFFr = new SolidColorBrush(Color.FromArgb(192, 255, 0, 0)),
      brushGrid = new SolidColorBrush(Colors.Gray),
      brush_Red = new SolidColorBrush(Colors.LightPink),
      brushGray = new SolidColorBrush(Colors.Gray),
      brushWind = (Brush)App.Current.Resources["WindBaseBrush"],
      brushWndL = (Brush)App.Current.Resources["WindBrushLt3"],
      brushWndD = (Brush)App.Current.Resources["WindBrushDk3"],
      //#1e90ff,#1b81e5,#1873cc,#1564b2,#125699,#0f487f,#0c3966,#092b4c,#061c33,#030e19,#000000
      //#1e90ff,#349bff,#4aa6ff,#61b1ff,#78bcff,#8ec7ff,#a5d2ff,#bbddff,#d2e8ff,#e8f3ff,#ffffff
      brushRedT = new SolidColorBrush(Colors.Red),
      brushBlue = new SolidColorBrush(Colors.Blue),
      brushTmpC = (Brush)App.Current.Resources["ThermoScaleGradientBrushV"]; //CreateLinearGradientBrush_VerticalAcross(Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 255, 255, 200), Color.FromArgb(255, 0, 0, 255)),
    const string
#if true // toronto - pearson
      _Fxml24 = "https://dd.weather.gc.ca/citypage_weather/xml/ON/s0000458_e.xml",  // Toronto (descr: https://dd.weather.gc.ca/citypage_weather/xml/siteList.xml)
      _Fore24 = "https://weather.gc.ca/forecast/hourly/on-143_metric_e.html",                // Toronto - 143,
      _Past24 = "https://weather.gc.ca/past_conditions/index_e.html?station=yyz";            // Pearson - yyz,  
#else   // markham - buttonville
      _Fxml24 = "https://dd.weather.gc.ca/citypage_weather/xml/ON/s0000585_e.xml",  // markham
      _Fore24 = "https://weather.gc.ca/forecast/hourly/on-85_metric_e.html",                 // markham 
      _Past24 = "https://weather.gc.ca/past_conditions/index_e.html?station=ykz";            // Buttonville 
#endif

    public UcEnvtCanHtml_PastFore24Hr_NonMvvm()
    {
      InitializeComponent();
      if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

      Application.Current.Suspending += OnSuspending;
      Application.Current.Resuming += OnResuming;
      Loaded += OnResuming;
    }

    async void OnSuspending(object s, SuspendingEventArgs e)
    {
      //if (Frame.CurrentSourcePageType == typeof(MainPage)) // Handle global application events only if this page is active
      {
        var deferral = e.SuspendingOperation.GetDeferral();
        try
        {
          await Task.Delay(9); // CleanupUiAsync();
        }
        finally
        {
          deferral.Complete();
        }
      }
    }
    async void OnResuming(object s, object o)
    {
      //if (Frame.CurrentSourcePageType == typeof(MainPage)) // Handle global application events only if this page is active
      {
        await ReLoadReDraw();
      }
    }
    void OnWheel(object s, Windows.UI.Xaml.Input.PointerRoutedEventArgs e) { var scv = (ScrollViewer)s; scv.ChangeView(scv.HorizontalOffset + 3 * e.GetCurrentPoint(this).Properties.MouseWheelDelta, null, null); e.Handled = true; }

    async Task ReLoadReDraw()
    {
      if (!Connectivity.IsInternet())
        return;

      try
      {
        var tdy = DateTime.Today;
        var now = DateTime.Now;
        var sd = await WebSerialHelper.UrlToInstnace<siteData>(_Fxml24, TimeSpan.FromMinutes(5));
        _normalsForTheDay = new EnvNormals
        {
          SunRze = sd.riseSet.dateTime.FirstOrDefault(r => r.zone == validTimeZones.UTC && r.name == dateStampNameType.sunrise).timeStamp.GetDateTimeLcl().TimeOfDay,   //new TimeSpan(sd.riseSet.dateTime[1].hour, sd.riseSet.dateTime[1].minute, 0),
          SunSet = sd.riseSet.dateTime.FirstOrDefault(r => r.zone == validTimeZones.UTC && r.name == dateStampNameType.sunset).timeStamp.GetDateTimeLcl().TimeOfDay,    //new TimeSpan(sd.riseSet.dateTime[3].hour, sd.riseSet.dateTime[3].minute, 0),
          NormHigh = (double)sd.almanac.temperature[2].Value.GetDecimal(),
          NormLow = (double)sd.almanac.temperature[3].Value.GetDecimal(),
        };
        Debug.WriteLine(sd);

        var hweb = new HtmlWeb();
        var p24 = await hweb.LoadFromWebAsync(_Past24);
        var f24 = await hweb.LoadFromWebAsync(_Fore24);
        var cco = EnvtCanXmlVM.Instance;
#if false
                if (Debugger.IsAttached)
                    using (var client = new System.Net.Http.HttpClient { BaseAddress = new Uri(_Past24) })
                    {
                        client.DefaultRequestHeaders.Accept.Clear(); //todo: would this clear MOBILE value from the headers?
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = await client.GetStringAsync(new Uri(_Fore24));
                        p24 = new HtmlDocument();
                        p24.LoadHtml(response);

                        Debug.WriteLine($"TRIED - DOES NOT HELP: {response}\r\n::>> Len: {response.Length} ==> {(response.Length > 500 ? "SUCCESS" : "FAILURE")} \r\n");
                        Debugger.Break();
                    }
#endif
        var ecsP = Cmn.Services.EnvtCanHtmlParser.Past24hourAtButtonville(p24);
        var ecsF = Cmn.Services.EnvtCanHtmlParser.Fore24hourAtButtonville(f24);

        await checkAddMostRecentMeasure(ecsP);

        var t12hr = .0625;
        var dTime = (sd.forecastGroup.forecast.FirstOrDefault()?.period?.Value.ToString().Length < _dayLen ? 1 : 1 + t12hr);
        foreach (var dayNightForecast in sd.forecastGroup.forecast.Skip(2)) // skip 2: today+tonight
        {
          ecsF.Add(new EnvtCanDto
          {
            ObserveT = now.AddDays(dTime),
            PeriodNm = dayNightForecast.period.Value.ToString(),
            TempActl = (double)(dayNightForecast.temperatures.temperature.First().Value.GetDecimal()),
            TempFeel = (int)(dayNightForecast.temperatures.temperature.First().Value.GetDecimal()), // no such for daily forecast; only for hourly.
            IconCode = $"{dayNightForecast.abbreviatedForecast.iconCode.Value:0#}",
            Descrptn = $"{dayNightForecast.period.Value}\t({dayNightForecast.abbreviatedForecast.iconCode.Value:#0})\r\n{dayNightForecast.abbreviatedForecast.textSummary}",
            WindKmHr = (dayNightForecast.winds.wind != null && dayNightForecast.winds.wind.Any()) ? (int)dayNightForecast.winds.wind.First()?.speed.Value.GetDecimal(0) : -1, // winds are there for today+tomorrow only.
            WindDirn = (dayNightForecast.winds.wind != null && dayNightForecast.winds.wind.Any()) ? dayNightForecast.winds.wind.First()?.direction.ToString() : "",
          });
          dTime += t12hr;
        }

        var xMin = ecsP.Any() ? ecsP.Min(r => r.ObserveT) : now.AddHours(-24);
        var xMax = ecsF.Any() ? ecsF.Max(r => r.ObserveT).AddHours(+48) : now.AddHours(+72);
        _tdcMin = Math.Min(0, Math.Min(ecsP.Any() ? Math.Min(ecsP.Min(r => r.TempFeel), ecsP.Min(r => r.TempActl)) : 0, ecsF.Any() ? Math.Min(ecsF.Min(r => r.TempFeel), ecsF.Min(r => r.TempActl)) : 0));
        _tdcMin = Math.Min(_tdcMin, _normalsForTheDay.NormLow) - _minDegMargin;

        _tdcMax = Math.Max(ecsP.Any() ? Math.Max(ecsP.Max(r => r.TempFeel), ecsP.Max(r => r.TempActl)) : 0, ecsF.Any() ? Math.Max(ecsF.Max(r => r.TempFeel), ecsF.Max(r => r.TempActl)) : 0);
        _tdcMax = Math.Max(_tdcMax, ecsF.Any() ? ecsF.Max(r => (r.WindKmHr / _windDivisor)) : 0);
        _tdcMax = Math.Max(_tdcMax, _normalsForTheDay.NormHigh) + _maxDegMargin;

        _pxlPerDeg = _tdcMax == _tdcMin ? 1 : _crtHt / (_tdcMax - _tdcMin);
        _pxlPerMin = (double)_scrWh / (_25hRangeInMin + _25hRangeInMin);
        _pxlPerMi_ = _pxlPerMin * _pastK;
        _25hRangeInMi_ = _25hRangeInMin / _pastK;

        Mus.GetUpdateKnownExtremum(_tdcMax, Extr.TempHistMax);
        Mus.GetUpdateKnownExtremum(_tdcMin, Extr.TempHistMin);

        var ptsFT = get25hTempPoints(ecsF, now, _pxlPerMin, _pxlPerDeg, _tdcMin);

        if (ecsP.Count < 10 && !ecsF.Any())
        {
          ApplicationData.Current.RoamingSettings.Values["Exn"] += "\r\n** leaving prev drawing intact **";
          return; //leave prev drawing intact.
        }

        canvasChart.Children.Clear();

        drawHorizontalGridTicks();
        drawVerticalGridTicks(now, xMin.AddHours(24), xMax);

        drawDayNightNormals(now, xMin, xMax); //drawRunPlan(now, xMin, xMax);

        if (ecsF.Any())
          drawIconsRare(now, ecsF, ptsFT);

        var pts = new PointCollection();

        ecsP.OrderBy(r => r.ObserveT).ToList().ForEach(e => pts.Add(new Point(_pxlPerMi_ * (_25hRangeInMi_ + (e.ObserveT - now).TotalMinutes), _crtHt - _pxlPerDeg * (e.TempActl - _tdcMin))));
        ecsF.OrderBy(r => r.ObserveT).ToList().ForEach(e => pts.Add(new Point(_pxlPerMin * (_25hRangeInMin + (e.ObserveT - now).TotalMinutes), _crtHt - _pxlPerDeg * (e.TempActl - _tdcMin))));

        var lne = new Polyline { Stroke = brushTmpC, StrokeThickness = 24, Points = pts, StrokeLineJoin = PenLineJoin.Round };
        lne.SetValue(ToolTipService.ToolTipProperty, $"Temperature - Toronto");
        canvasChart.Children.Add(lne);

        scrlVwr.ChangeView(_scrWh / 2.3, null, null); // now line offset: show a bit of past to see the trend, but emphasize the visibility of future: at least 2 days.

        if (ecsP.Any()) drawExtrDots(ecsP, now, 20, true);
        if (ecsF.Any())
        {
          drawExtrDots(ecsF, now, 30, false);
          drawWindDots(ecsF, now, 50);
          drawDayNames(ecsF, now, ptsFT);
        }

        drawTempDot(new EnvtCanDto { ObserveT = cco.LastUpdate, TempFeel = cco.TempActlDbl }, now, 20, 10, past: true); // 2021-03-07

#if ___
                ApplicationView.GetForCurrentView().Title = $"gChart.Children.Count(): {gChartM.Children.Count()}"; //..Debug.WriteLine($"{gChart.Children.Count()}"); //also: https://www.eternalcoding.com/?p=1952
#endif
      }
      catch (Exception ex) { if (Debugger.IsAttached) Debugger.Break(); ApplicationData.Current.RoamingSettings.Values["Exn"] += /*tx.Text =*/ $"{ex.GetInnermostException().Message}\r\n"; }
    }

    void drawRunPlan(DateTime now, DateTime xMin, DateTime xMax)
    {
      var runClrEasy = CreateLinearGradientBrush_HorizontalAcross(Color.FromArgb(196, 20, 255, 020), Color.FromArgb(196, 10, 155, 10));
      var runClrPace = CreateLinearGradientBrush_HorizontalAcross(Color.FromArgb(196, 255, 200, 20), Color.FromArgb(196, 200, 155, 10));
      var runClrCros = CreateLinearGradientBrush_HorizontalAcross(Color.FromArgb(196, 20, 100, 255), Color.FromArgb(196, 10, 55, 200));
      var minPerMile = 12;

      for (var date = xMin.Date; date <= xMax.Date; date = date.AddDays(1))
      {
        var tu = HalHigdon.TrainingPlanForTheDay(date.Date);
        if (tu != null)
        {
          var runSatrtMin = tu.DistMiles < 25 ? 360 : 540;
          var x = _pxlPerMin * (_25hRangeInMin + (date - now).TotalMinutes + runSatrtMin);

          var clr = tu.Mode == "pace" ? runClrPace : tu.Mode == "cros" ? runClrCros : runClrEasy;

          var bar = new Rectangle { Height = tu.DistMiles * 1.60934 * _pxlPerDeg, Width = _pxlPerMin * tu.DistMiles * minPerMile, StrokeThickness = 0, Fill = clr };
          bar.SetValue(Canvas.TopProperty, _crtHt - _pxlPerDeg * (0 - _tdcMin) - bar.Height);
          bar.SetValue(Canvas.LeftProperty, x);
          bar.SetValue(ToolTipService.ToolTipProperty, $"{tu.DistMiles * 1.6} km \r\n{tu.DistMiles} miles\r\n{tu.Mode}");

          canvasChart.Children.Add(bar);
        }
      }
    }
    void drawDayNightNormals(DateTime now, DateTime xMin, DateTime xMax)
    {
      if (_normalsForTheDay == null) return;

      //now = DateTime.Today.AddHours(15);

      var nftd = _normalsForTheDay;
      var barNorm = new Rectangle { Height = _pxlPerDeg * (nftd.NormHigh - nftd.NormLow), Width = _scrWh * 2, StrokeThickness = 0, Fill = CreateLinearGradientBrush_VerticalAcross(Color.FromArgb(64, 255, 020, 000), Color.FromArgb(64, 000, 020, 255)) };
      barNorm.SetValue(Canvas.TopProperty, _crtHt - _pxlPerDeg * (nftd.NormHigh - _tdcMin));
      barNorm.SetValue(Canvas.LeftProperty, 0);
      barNorm.SetValue(ToolTipService.ToolTipProperty, $"Daily Normals (°C): \r\n Low\t{nftd.NormLow}° \r\nHigh\t{nftd.NormHigh}°");
      canvasChart.Children.Add(barNorm);

      var dayLen = (nftd.SunSet - nftd.SunRze).TotalMinutes;
      var dayNte = new Rectangle { Height = _scrHt, Width = _pxlPerMin * dayLen, StrokeThickness = 0, Fill = brushDay };
      dayNte.SetValue(Canvas.TopProperty, _scrHt - dayNte.Height);
      dayNte.SetValue(ToolTipService.ToolTipProperty, $" Sunrize \t{nftd.SunRze:h\\:mm} \r\n Sunset \t{nftd.SunSet:hh\\:mm} \r\n Day Len.\t{nftd.SunSet - nftd.SunRze:hh\\:mm}");

      double x;
      if (now.TimeOfDay < nftd.SunRze)
      {
        x = _pxlPerMin * (_25hRangeInMin + nftd.SunRze.TotalMinutes - now.TimeOfDay.TotalMinutes);
      }
      else if (now.TimeOfDay < nftd.SunSet)
      {
        x = _pxlPerMin * (_25hRangeInMin + nftd.SunSet.TotalMinutes - now.TimeOfDay.TotalMinutes);
        dayNte.Fill = brushNte;
        dayNte.Width = _pxlPerMin * (1440 - dayLen);
      }
      else
      {
        x = _pxlPerMin * (_25hRangeInMin + nftd.SunRze.TotalMinutes - now.TimeOfDay.TotalMinutes + 1440);
      }

      dayNte.SetValue(Canvas.LeftProperty, x);
      canvasChart.Children.Add(dayNte);

      var leftBar = new Rectangle { Height = _scrHt, Width = _scrWh / 2, StrokeThickness = 0, Fill = brushEdg };
      leftBar.SetValue(Canvas.TopProperty, _scrHt - leftBar.Height);
      canvasChart.Children.Add(leftBar);

      var nowPlus24h = _pxlPerMin * (_25hRangeInMin + 24 * 60);
      var rghtBar = new Rectangle { Height = _scrHt, Width = _scrWh / 2, StrokeThickness = 0, Fill = brushEdg };
      rghtBar.SetValue(Canvas.TopProperty, _scrHt - rghtBar.Height);
      rghtBar.SetValue(Canvas.LeftProperty, nowPlus24h);
      canvasChart.Children.Add(rghtBar);





      //var plus24hLimit = xMin.AddDays(2);
      //for (var date = xMin.AddDays(1).Date; date <= plus24hLimit.Date; date = date.AddDays(1))
      //{
      //  Debug.WriteLine($"{xMin} - {xMax} == {(plus24hLimit - (date.Date + nftd.SunRze)) }"); // if((_25hRangeInMin + (date - now).TotalMinutes + tu.SunSet.TotalMinutes)> xMax.tot)

      //  var sunset = date + nftd.SunSet;

      //  var barDay = new Rectangle { Height = _scrHt, Width = _pxlPerMin * dayLen.TotalMinutes, StrokeThickness = 0, Fill = dayClr };
      //  barDay.SetValue(Canvas.TopProperty, _scrHt - barDay.Height);
      //  barDay.SetValue(Canvas.LeftProperty, _pxlPerMin * (_25hRangeInMin + (date - now).TotalMinutes + nftd.SunRze.TotalMinutes));
      //  barDay.SetValue(ToolTipService.ToolTipProperty, $" Sunrize \t{nftd.SunRze:h\\:mm} \r\n Sunset \t{nftd.SunSet:hh\\:mm} \r\n Day Len.\t{nftd.SunSet - nftd.SunRze:hh\\:mm}");
      //  canvasChart.Children.Add(barDay);
      //}
    }

    static LinearGradientBrush CreateLinearGradientBrush_HorizontalAcross(Color lft, Color rgt)
    {
      var lgb = new LinearGradientBrush
      {
        StartPoint = new Point(0, 0.5),
        EndPoint = new Point(1, 0.5)
      };
      lgb.GradientStops.Add(new GradientStop { Color = lft, Offset = 0.0 });
      lgb.GradientStops.Add(new GradientStop { Color = rgt, Offset = 1.0 });
      return lgb;
    }
    static LinearGradientBrush CreateLinearGradientBrush_HorizontAcross(Color lft, Color ctr, Color rgt)
    {
      var lgb = new LinearGradientBrush { StartPoint = new Point(0, 0.5), EndPoint = new Point(1, 0.5) };
      lgb.GradientStops.Add(new GradientStop { Color = lft, Offset = 0.0 });
      lgb.GradientStops.Add(new GradientStop { Color = ctr, Offset = 0.5 });
      lgb.GradientStops.Add(new GradientStop { Color = rgt, Offset = 1.0 });
      return lgb;
    }
    static LinearGradientBrush CreateLinearGradientBrush_VerticalAcross(Color top, Color ctr, Color btm)
    {
      var lgb = new LinearGradientBrush { StartPoint = new Point(0.5, 0), EndPoint = new Point(0.5, 1) };
      lgb.GradientStops.Add(new GradientStop { Color = top, Offset = 0.0 });
      lgb.GradientStops.Add(new GradientStop { Color = ctr, Offset = 0.5 });
      lgb.GradientStops.Add(new GradientStop { Color = btm, Offset = 1.0 });
      return lgb;
    }
    static LinearGradientBrush CreateLinearGradientBrush_VerticalAcross(Color top, Color btm)
    {
      var lgb = new LinearGradientBrush
      {
        StartPoint = new Point(0.5, 0),
        EndPoint = new Point(0.5, 1)
      };
      lgb.GradientStops.Add(new GradientStop { Color = top, Offset = 0.0 });
      lgb.GradientStops.Add(new GradientStop { Color = btm, Offset = 1.0 });
      return lgb;
    }

    const int gridLineThick = 4, gridDashThick = 4;
    readonly DoubleCollection _dash = new DoubleCollection() { 1, 5 };
    readonly DoubleCollection _das2 = new DoubleCollection() { 1, 5 };

    void drawVerticalGridTicks(DateTime now, DateTime xNow, DateTime xMax)
    {
      double xhr = 0;
      var hr24fromNow = xNow.AddHours(24);
      for (var h = xNow; h < hr24fromNow; h = h.AddHours(1))
      {
        xhr = _pxlPerMin * (_25hRangeInMin + (h - now).TotalMinutes);

        if (h.Hour % 6 > 0) // ticks every hour: 1, 3, 6, 24
        {
          canvasChart.Children.Add(new Polyline { Stroke = brushGrid, StrokeThickness = gridLineThick, Points = new PointCollection { new Point(xhr, _crtHt + _pxlPerDeg * _tdcMin), new Point(xhr, _crtHt + _pxlPerDeg * _tdcMin - (h.Hour % 3 == 0 ? 50 : 30)) } });
          continue;
        }

        canvasChart.Children.Add(new Polyline
        {
          Stroke = brushGrid,
          Points = new PointCollection { new Point(xhr, 0), new Point(xhr, _scrHt) },
          StrokeThickness = h.Hour == 0 ? gridLineThick : gridDashThick,
          StrokeDashArray = h.Hour == 0 ? null : new DoubleCollection() { 1, 5 },
        });

        var el = new TextBlock { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Text = h.Hour == 0 ? $"{h:ddd}" : $"{h:HH}", Foreground = brushWite, FontSize = 70 };
        el.SetValue(Canvas.LeftProperty, xhr);
        el.SetValue(Canvas.TopProperty, -10);
        canvasChart.Children.Add(el);
      }

      xhr -= _pxlPerMin * 360; // add 6 hours for coolest time 6:00, warmest 18:00.

      for (var h = hr24fromNow.AddHours(24); h <= xMax.AddDays(4); h = h.AddHours(24))
      {
        var x = xhr + _pxlPerMin * (_25hRangeInMin + (h - now).TotalMinutes) / 8;

        canvasChart.Children.Add(new Polyline
        {
          Stroke = brushGrid,
          Points = new PointCollection { new Point(x, 0), new Point(x, _crtHt) },
          StrokeThickness = gridDashThick,
          StrokeDashArray = new DoubleCollection() { 1, 5 },
        });

        drawDayOfWeek(h, x);
      }

      drawDayOfWeek(now.AddDays(-1), _scrWh / 2 - 250);
    }

    void drawDayOfWeek(DateTime h, double x)
    {
      var wd = $"{h:ddd}";
      var el = new TextBlock { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Text = wd, Foreground = wd[0] == 'S' ? brush_Red : brushWite, FontSize = 70 };
      el.SetValue(Canvas.LeftProperty, x + 15);
      el.SetValue(Canvas.TopProperty, -10);
      canvasChart.Children.Add(el);
    }

    void drawHorizontalGridTicks()
    {
      gChart0.Children.Clear();
      gChart5.Children.Clear();
      for (var t = -100; t < 100; t += 5)
      {
        if (_tdcMin >= t || t >= _tdcMax) continue;

        var y = _crtHt - _pxlPerDeg * (t - _tdcMin);
        canvasChart.Children.Add(new Polyline
        {
          Stroke = brushGrid,
          StrokeThickness = t % 10 == 0 ? gridLineThick : gridDashThick,
          Points = new PointCollection { new Point(0, y), new Point(_scrWh * 2, y) },
          StrokeDashArray = t % 10 == 0 ? null : new DoubleCollection() { 1, 5 },
        });

        if (t % 10 != 0)
          continue;

        var gradLeft = new TextBlock { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Text = $"{t}°   ", Foreground = brushWite, FontSize = 80 };
        var gradRght = new TextBlock { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Right, Text = $"{t,2}°", Foreground = brushWite, FontSize = 80 };

        gradLeft.SetValue(Canvas.LeftProperty, 3);
        gradLeft.SetValue(Canvas.TopProperty, y - 15);
        gradRght.SetValue(Canvas.TopProperty, y - 15);

        gChart0.Children.Add(gradLeft);
        gChart5.Children.Add(gradRght);
      }
    }

    void drawIcons(DateTime now, List<EnvtCanDto> ecsP, PointCollection ptsP)
    {
      for (var i = 0; i < ptsP.Count; i++)
      {
        var x = _pxlPerMin * (_25hRangeInMin + (ecsP[i].ObserveT - now).TotalMinutes);

        var img = new Image { Height = 51, Width = 60, Source = new BitmapImage { UriSource = new Uri(ecsP[i].IconUrl) } };
        var eps = new Ellipse { Height = 53, Width = 62, StrokeThickness = 0, Fill = brushCFFr };

        img.SetValue(ToolTipService.ToolTipProperty, $"{ecsP[i].ObserveT:HH:mm}-{ecsP[i].Descrptn}");

        eps.SetValue(Canvas.TopProperty, 1374);
        img.SetValue(Canvas.TopProperty, 1375);

        eps.SetValue(Canvas.LeftProperty, x - 32);
        img.SetValue(Canvas.LeftProperty, x - 30);

        canvasChart.Children.Add(eps);
        canvasChart.Children.Add(img);
      }
    }
    void drawIconsRare(DateTime now, List<EnvtCanDto> ec, PointCollection pt)
    {
      const int halfHt = 80, top = _scrHt + 23 - _lowerIconBarHt;
      var ttl = 0;
      for (var i = 0; i < 24/*pt.Count*/; i++)
      {
        var theHr = _pxlPerMin * (_25hRangeInMin + (ec[i].ObserveT - now).TotalMinutes);
        var widthOfHr = _pxlPerMin * 60;

        var sameLen = sameRepeatCount(ec, i);
        ttl += (sameLen + 1);        //;Debug.WriteLine($"{same,3} of '{ec[i].IconNum}' => {ttl,2}/{pt.Count}.");


        var img = new Image { Source = new BitmapImage { UriSource = new Uri(ec[i].IconUrl) } };
        var bkg = new Ellipse { Height = 156, Width = widthOfHr * (sameLen + 1) - 3, StrokeThickness = 0, Fill = brushCFFg };

        var spr = new Ellipse { Height = 72, Width = 2, StrokeThickness = 8, Stroke = brushBlck };
        spr.SetValue(Canvas.TopProperty, top + halfHt / 2);
        spr.SetValue(Canvas.LeftProperty, theHr - _pxlPerMin * 30);

        var smallMediumTop = top + halfHt - 102 * (ec[i].ObserveT.Hour % 2);
        switch (sameLen)
        {
          case 00: //small - 1 hr
            bkg.Height = img.Height = 102;
            bkg.Width = ((img.Width = 120) - 10);// + 3;
            img.SetValue(Canvas.TopProperty, smallMediumTop);
            bkg.SetValue(Canvas.TopProperty, smallMediumTop);
            break;
          case 01: //mdedium - 2 hr
            bkg.Height = img.Height = 127;
            bkg.Width = (img.Width = 150) - 12;// 8;
            img.SetValue(Canvas.TopProperty, smallMediumTop);
            bkg.SetValue(Canvas.TopProperty, smallMediumTop);
            break;
          default: // large - 3 hr ++
            bkg.Height = img.Height = 153;
            bkg.Width = (img.Width = 180) - 18;// 8;
            img.SetValue(Canvas.TopProperty, top + halfHt - img.Height * .5);
            bkg.SetValue(Canvas.TopProperty, top + halfHt - img.Height * .5);
            break;
        }

        var ttip = sameLen == 0 ?
            $"{ec[i].ObserveT:HH:mm}  {ec[i].Descrptn}" :
            $"{ec[i].ObserveT:HH:mm} - {ec[i].ObserveT.AddHours(sameLen):HH:mm} \r\n {ec[i].Descrptn}";

        var lft = theHr + widthOfHr * sameLen / 2 - img.Width / 2 + 6;
        img.SetValue(Canvas.LeftProperty, lft);
        bkg.SetValue(Canvas.LeftProperty, lft);

        img.SetValue(ToolTipService.ToolTipProperty, ttip);
        spr.SetValue(ToolTipService.ToolTipProperty, ttip);

        canvasChart.Children.Add(spr);
        //canvasChart.Children.Add(bkg);
        canvasChart.Children.Add(img);

        if (sameLen > 0)
          i += sameLen;
      }
    }
    int sameRepeatCount(List<EnvtCanDto> ec, int i0) // returns 0 is only one.
    {
      var sc = 0;
      for (var i = i0 + 1; i < ec.Count; i++)
      {
        if (ec[i].IconCode == ec[i0].IconCode)
          sc++;
        else
          break;
      }

      return sc;
    }

    async Task checkAddMostRecentMeasure(List<EnvtCanDto> ecsP)
    {
      var ecvm = EnvtCanXmlVM.Instance;
      if (ecvm.LastUpdate == DateTime.MinValue)
        await ecvm.Refresh();
      if (ecsP.All(r => r.ObserveT != ecvm.EC.ObserveT))
        ecsP.Add(ecvm.EC);

      ecvm.ExtrBrush = new SolidColorBrush(Mus.TmprClr(ecvm.TempFeelI, (int)_tdcMin, (int)_tdcMax));
    }

    void drawExtrDots(List<EnvtCanDto> ecs, DateTime now, int diam, bool past)
    {
      var rad = diam / 2;
      foreach (var ec in ecs.Where(r => Math.Abs(r.TempFeel - r.TempActl) > 1).OrderBy(r => r.ObserveT))
      {
        drawTempDot(ec, now, diam, rad, past);
      }
    }

    void drawTempDot(EnvtCanDto ec, DateTime now, int diam, int rad, bool past)
    {
      var el = new Ellipse { Height = diam, Width = diam, Fill = ec.TempFeel < 0 ? brushBlue : brushRedT }; // new SolidColorBrush(Mus.TmprClr(ec.TempFeel, _tdcMin, _tdcMax)) };

      if (past)
        el.SetValue(Canvas.LeftProperty, _pxlPerMi_ * (_25hRangeInMi_ + (ec.ObserveT - now).TotalMinutes) - rad);
      else
        el.SetValue(Canvas.LeftProperty, _pxlPerMin * (_25hRangeInMin + (ec.ObserveT - now).TotalMinutes) - rad);

      el.SetValue(Canvas.TopProperty, _crtHt - _pxlPerDeg * (ec.TempFeel - _tdcMin) - rad);
      canvasChart.Children.Add(el);
    }

    void drawWindDots(List<EnvtCanDto> ecs, DateTime now, int diam)
    {
      var rad = diam / 2;
      var len = diam * 1.5;
      foreach (var ec in ecs.Where(r => r.WindKmHr >= 0).OrderBy(r => r.ObserveT))
      {
        var ttp = $"Wind {ec.WindDirn} {ec.WindKmHr} km/h" + (ec.WindGust > ec.WindKmHr ? $" (gusts {ec.WindGust})" : "");
        var y = _crtHt - _pxlPerDeg * (ec.WindKmHr / _windDivisor - _tdcMin) - rad;
        var x = _pxlPerMin * (_25hRangeInMin + (ec.ObserveT - now).TotalMinutes) - rad;

        var el = new Ellipse { Height = diam, Width = diam, Fill = ec.Wind360d != 301 ? brushWndD : brushWndL, Stroke = ec.Wind360d == 301 ? brushWndD : brushWndL, StrokeThickness = 4 };
        el.SetValue(Canvas.LeftProperty, x);
        el.SetValue(Canvas.TopProperty, y);
        el.SetValue(ToolTipService.ToolTipProperty, ttp);
        canvasChart.Children.Add(el);

        if (ec.Wind360d != 301)
        {
          var ln = new Line { Height = len, Width = len, Fill = brushWite, Stroke = brushWndL, X1 = rad, Y1 = rad, X2 = len, Y2 = len, StrokeThickness = 8 };
          ln.RenderTransform = new RotateTransform { Angle = ec.Wind360d + 45, CenterX = rad, CenterY = rad };
          ln.SetValue(Canvas.LeftProperty, x);
          ln.SetValue(Canvas.TopProperty, y);
          ln.SetValue(ToolTipService.ToolTipProperty, ttp);
          canvasChart.Children.Add(ln);
        }
      }
    }

    readonly int _dayLen = 11;
    void drawDayNames(List<EnvtCanDto> ecs, DateTime now, PointCollection pt)
    {
      foreach (var ec in ecs.Where(r => !string.IsNullOrEmpty(r.PeriodNm) && r.PeriodNm.Length < _dayLen).OrderBy(r => r.ObserveT))
      {
        var x = _pxlPerMin * (_25hRangeInMin + (ec.ObserveT - now).TotalMinutes);
        var y = _crtHt - _pxlPerDeg * (ec.TempFeel - _tdcMin);
        var wd = ec.PeriodNm.Substring(0, 1);
        var tb = new TextBlock { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Text = wd, Foreground = wd == "S" ? brush_Red : brushGrid, FontSize = 70 };
        tb.SetValue(Canvas.LeftProperty, x - 25);
        tb.SetValue(Canvas.TopProperty, y - 120);
        canvasChart.Children.Add(tb);
      }
      for (var i = 24; i < pt.Count; i++)
      {
        var top = 32 + _crtHt - _pxlPerDeg * (ecs[i].TempFeel - _tdcMin);
        var theHr = _pxlPerMin * (_25hRangeInMin + (ecs[i].ObserveT - now).TotalMinutes);
        var widthOfHr = _pxlPerMin * 60;
        var img = new Image { Height = 127, Width = 127, Source = new BitmapImage { UriSource = new Uri(ecs[i].IconUrl) } };
        var bkg = new Ellipse { Height = 127, Width = 127, StrokeThickness = 0, Fill = brushCFFF };

        img.SetValue(Canvas.TopProperty, top);
        bkg.SetValue(Canvas.TopProperty, top);

        var lft = theHr - img.Width / 2 + 6;
        img.SetValue(Canvas.LeftProperty, lft);
        bkg.SetValue(Canvas.LeftProperty, lft);

        img.SetValue(ToolTipService.ToolTipProperty, $"{ecs[i].ObserveT:HH:mm}  {ecs[i].Descrptn}");

        canvasChart.Children.Add(bkg);
        canvasChart.Children.Add(img);
      }
    }

    static PointCollection get25hTempPoints(List<EnvtCanDto> ec, DateTime now, double kx, double ky, double min)
    {
      var pts = new PointCollection();
      ec.OrderBy(r => r.ObserveT).ToList().ForEach(e => pts.Add(new Point(kx * (_25hRangeInMin + (e.ObserveT - now).TotalMinutes), _crtHt - ky * (e.TempActl - min))));
      return pts;
    }
    static PointCollection get25hWindPoints(List<EnvtCanDto> ec, DateTime now, double kx, double ky)
    {
      var pts = new PointCollection();
      ec.OrderBy(r => r.ObserveT).ToList().ForEach(e => pts.Add(new Point(kx * (_25hRangeInMin + (e.ObserveT - now).TotalMinutes), _crtHt - ky * e.WindKmHr)));
      return pts;
    }
  }
}

/// <uap:SplashScreen Image="Assets\SplashScreen.png" BackgroundColor="beige" />
/// C:\Program Files (x86)\Microsoft Visual Studio 14.0\xml\Schemas\AppxManifestTypes.xsd