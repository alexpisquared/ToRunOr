using AsLink;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ToRunOr.Vws;
using VMs;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.System;
using Windows.System.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace ToRunOr
{
  public sealed partial class MainPage : Page
  {
    //MainPage rootPage;               //C:\gh\Windows-universal-samples\Samples\BackButton\cs\Scenario1.xaml.cs
    public static MainPage Current;  //C:\gh\Windows-universal-samples\Samples\BackButton\cs\Scenario1.xaml.cs
    MainPageVM _vm = null;
    public static DateTime StartTime = DateTime.MinValue;

    public MainPage()
    {
      this.InitializeComponent();

      _vm = MainPageVM.Instance;
      DataContext = _vm;

      Application.Current.Resuming += OnResuming;

#if DEBUG
      Loaded += async (s, e) => { if (!WinTileHelper.IsPinned) await WinTileHelper.PinTile(s); };
      ApplicationView.GetForCurrentView().Title = tbVer.Text = $@"Dbg: {AppVersion}   ({DevOp.BuildTime(typeof(App)):yyyy-MM-dd})";
#else
            ApplicationView.GetForCurrentView().Title = tbVer.Text = $@"Version: {AppVersion}   ({DevOp.BuildTime(typeof(App)):yyyy-MM-dd})";
#endif

      this.NavigationCacheMode = NavigationCacheMode.Required; // I want this page to be always cached so that we don't have to add logic to save/restore state for the checkbox. //C:\gh\Windows - universal - samples\Samples\BackButton\cs\Scenario1.xaml.cs

      VisualStateManager.GoToState(this, "ucAnalogClock_VS_Max", false);
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      _disp.RequestActive();
      _since = DateTime.Now;
      //rootPage = MainPage.Current;
      Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
      OnResuming();
    }
    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
      _vm?.ScenarioCleanup();
      OnResuming();
    }
    protected override void OnKeyDown(KeyRoutedEventArgs e)
    {
      base.OnKeyDown(e);

      switch (e.Key)
      {
        case VirtualKey.Escape: CoreApplication.Exit(); break;
        //case VirtualKey.A: { l1.RenderTransform = new RotateTransform { Angle = +120 }; } break;
        //case VirtualKey.S: { l1.RenderTransform = new RotateTransform { Angle = -120 }; } break;
        //case VirtualKey.Space: { l1.RenderTransform = new RotateTransform { Angle = 120 }; } break;
        default: Debug.WriteLine($"case VirtualKey.{e.Key}:            break;"); break;
      }
    }

    void OnResuming(object sender = null, object o = null)
    {
      //no effect: if (ucAnalogClock.Opacity == 1) onMax(ucAnalogClock);      else        onMin(ucAnalogClock);

      var now = DateTime.Now;
      if ((now - _vm.ResumedAt).TotalMinutes < 15 && _vm.ResumeHist.Contains($"{_vm.ResumedAt:ddd HH\\:mm}\n")) // ignore close-by reuses.
        _vm.ResumeHist = _vm.ResumeHist.Replace($"{_vm.ResumedAt:ddd HH\\:mm}\n", $"{now:ddd HH\\:mm}\n");
      else
        _vm.ResumeHist += $"{now:ddd HH\\:mm}\n";

      _vm.ResumedAt = now;

#if DEBUG
      if (!string.IsNullOrEmpty(ApplicationData.Current.RoamingSettings.Values["Exn"]?.ToString()))
        tbError.Text = $"{ApplicationData.Current.RoamingSettings.Values["Exn"]}";
#endif
    }
    async void onMax(object sender, RoutedEventArgs e = null)
    {
      StartTime = DateTime.Now;
      var btn = ((AppBarToggleButton)sender);
      btn.Icon = new SymbolIcon(Symbol.ZoomOut);
      btn.Opacity = 1;

      while ((FrameworkElement)btn.Parent == null || ActualWidth <= 0 || ActualWidth <= 0) { await Task.Delay(100); Debug.Write($"--onMax--"); }

      //var prn = (FrameworkElement)btn.Parent;
      //prn.Width = ActualWidth;
      //prn.Height = ActualHeight;

      if (!string.IsNullOrEmpty(btn.Name))
      {
        var rv = VisualStateManager.GoToState(this, btn.Name + "_VS_Min", false);
      }

      //..Debug.WriteLine($"{btn.ActualWidth}x{btn.ActualHeight}"); // 68x61.5 on phone, 68x60 on PC
    }
    void onMin(object sender, RoutedEventArgs e = null)
    {
      StartTime = DateTime.MinValue;
      var btn = ((AppBarToggleButton)sender);
      btn.Icon = new SymbolIcon(Symbol.ZoomIn);
      btn.Opacity = .3;

      //var prn = ((FrameworkElement)btn.Parent);
      //prn.Width = btn.Width;
      //prn.Height = btn.Height;

      if (!string.IsNullOrEmpty(btn.Name))
        VisualStateManager.GoToState(this, btn.Name + "_VS_Max", false);
      else
        VisualStateManager.GoToState(this, "AbNormal_FadeIn", false);
    }

    public static readonly DependencyProperty MyPropertyProperty = DependencyProperty.Register("MyProperty", typeof(int), typeof(MainPage), new PropertyMetadata(0)); public int MyProperty { get { return (int)GetValue(MyPropertyProperty); } set { SetValue(MyPropertyProperty, value); } }

    DisplayRequest _disp = new DisplayRequest();
    DateTime _since;

    void onGoToPrivaPlicy(object sender, RoutedEventArgs e) { (Window.Current.Content as Frame).Navigate(typeof(PrivacyPolicy)); }
    void onAntiSceenSaver(object sender, RoutedEventArgs e)
    {
      if (tgAntiSceenSaver.IsChecked == true)
      {        
        _disp.RequestActive();
        _since = DateTime.Now;
        tgAntiSceenSaver.Text = $"On Since {_since:HH:mm}";
      }
      else
      {
        _disp.RequestRelease();
        tgAntiSceenSaver.Text = $"Was On for {(DateTime.Now-_since):hh\\:mm}";
      }
    }
    void onGoToXamlImgFle(object sender, RoutedEventArgs e) { (Window.Current.Content as Frame).Navigate(typeof(XamlToImageToFile)); }
    void onGoToZoomPanels(object sender, RoutedEventArgs e) { (Window.Current.Content as Frame).Navigate(typeof(ZoomPanels)); }
    void onGoToMeteoChart(object sender, RoutedEventArgs e) { (Window.Current.Content as Frame).Navigate(typeof(MeteoChart)); }
    void onGoToDevDbgView(object sender, RoutedEventArgs e) { (Window.Current.Content as Frame).Navigate(typeof(DevDbgView)); }
    void onGoToOrientView(object sender, RoutedEventArgs e) { (Window.Current.Content as Frame).Navigate(typeof(OrientView)); }

    void onSetAsLockscreen(object sender, RoutedEventArgs e) { }

    void onClearExeptionMsg(object sender, RoutedEventArgs e) => ApplicationData.Current.RoamingSettings.Values["Exn"] = tbError.Text = "";

    public static string AppVersion { get { var v = Package.Current.Id.Version; return $"{v.Major}.{v.Minor}.{v.Build}"; } } // .{v.Revision} is always 0.

    void tgAntiSceenSaver_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
      Debug.WriteLine($" *** isChecked: {tgAntiSceenSaver.IsChecked} \t {tgAntiSceenSaver.Text} ");
    }
  }
}