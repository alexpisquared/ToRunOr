using AsLink;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VMs;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources.Core;
using Windows.Media.SpeechSynthesis;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ToRunOr.Vws.UCs
{
  public sealed partial class ucRadar : UserControl
  {
    int _idx, _ctr = 400; // give 12.5 sec before voicing connection problem. 
    const int _maxPastInMin = 60, periodInMs = 125, _speakOnAttempt = 500; //62.500 sec
    const string _noINet = "Looks like no Internet ... check your connection.";
    DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(periodInMs) };

    public static Brush ExtrBrush { get; internal set; }

    public ucRadar()
    {
      this.InitializeComponent();
      if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

      //_timer.Tick += onTick;

      Application.Current.Suspending += OnSuspending;
      Application.Current.Resuming += OnResuming;
      Loaded += OnResuming;
    }
    async void OnSuspending(object sender, SuspendingEventArgs e)
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
    async void OnResuming(object sender, object o)
    {
      //if (Frame.CurrentSourcePageType == typeof(MainPage)) // Handle global application events only if this page is active  //todo: investigate the other pages vs. main vs. shell
      {
        await reLoad();
      }
    }

    protected override void OnKeyDown(KeyRoutedEventArgs e)
    {
      base.OnKeyDown(e);

      switch (e.Key)
      {
        case VirtualKey.Stop: _timer.Stop(); break;
        case VirtualKey.Pause:
        case VirtualKey.P: onPlay(null, null); break;
        case VirtualKey.Escape: CoreApplication.Exit(); break;
        default: Debug.WriteLine($"case VirtualKey.{e.Key}:            break;"); break;
      }
    }

    void OnWheel(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e) { var scv = (ScrollViewer)sender; scv.ChangeView(scv.HorizontalOffset + 3 * e.GetCurrentPoint(this).Properties.MouseWheelDelta, null, null); e.Handled = true; }
    void onItemClick(object sender, ItemClickEventArgs e)
    {
      _timer.Stop();
      btnPlayRadar.Opacity = _timer.IsEnabled ? 0 : 1; // Play2.Visibility = _timer.IsEnabled ? Visibility.Collapsed : Visibility.Visible;			Play2.Icon = _timer.IsEnabled ? new SymbolIcon(Symbol.Pause) : new SymbolIcon(Symbol.Play);


      //var ee = e;
      //var dt = ((Windows.UI.Xaml.FrameworkElement)ee.ClickedItem).Tag as DateTime;

      var ci = e.ClickedItem;
      var dd = ((Windows.UI.Xaml.FrameworkElement)ci).Tag;
      if (dd is DateTime)
      {
        var dt = (DateTime)((Windows.UI.Xaml.FrameworkElement)ci).Tag;
        tbMax.Text = (DateTime.Now - dt.ToLocalTime()).TotalMinutes.ToString("N0");
      }
    }
    async void onTick(object sender, object args)
    {
      if (!lvRadar.Items.Any(r => ((Image)r).ActualWidth > 0)) // if there are no non-zero width images: nothing to show but a message 
      {
        if (_ctr % 50 == 0) // 6.25 sec
          tbMax.Text = Connectivity.IsInternet() ? "Downloading... " : _noINet;

        if (_ctr % _speakOnAttempt == 0 && tbMax.Text.Contains(_noINet))
          await Speak0(media, _noINet);

        _ctr++;
        return;
      }

      if (tbMax.Text.Contains(_noINet)) { await Speak0(media, "Congratulations! The internet is back!"); }


      if (lvRadar.Items != null && ++_idx >= lvRadar.Items.Count + 5)
      {
        tbMax.Text = (DateTime.Now - lvRadar.Items.Where(r => ((Image)r).ActualWidth > 0).Max(r => (DateTime)((Image)r).Tag).ToLocalTime()).TotalMinutes.ToString("N0");
        _idx = 0;
      }

      var idx = _idx < lvRadar.Items.Count ? _idx : lvRadar.Items.Count - 1;

      var image = (Image)lvRadar.Items?[idx];
      if (image != null && image.ActualWidth > 0) lvRadar.SelectedIndex = idx;      //else Debug.WriteLine("00000000");

      lvRadar.ScrollIntoView(lvRadar.Items[lvRadar.Items.Count - 1]); // nogo:
    }
    void onPlay(object sender, RoutedEventArgs e)
    {
      if (_timer.IsEnabled)
        _timer.Stop();
      else
        _timer.Start();

      btnPlayRadar.Opacity = _timer.IsEnabled ? 0 : 1; // Play2.Visibility = _timer.IsEnabled ? Visibility.Collapsed : Visibility.Visible;			Play2.Icon = _timer.IsEnabled ? new SymbolIcon(Symbol.Pause) : new SymbolIcon(Symbol.Play);			btnPlayRadar.Icon = _timer.IsEnabled ? new SymbolIcon(Symbol.Pause) : new SymbolIcon(Symbol.Play);			btnPlayRadar.Visibility = _timer.IsEnabled ? Visibility.Collapsed : Visibility.Visible;
    }

    async Task reLoad()
    {
      try
      {
        await Task.Delay(1);

        _timer.Stop();
        lvRadar.Visibility = Visibility.Collapsed;
        lvRadar.Items?.Clear();
        var roundedBy10MinGmtNow = EnvCanRadarUrlHelper.RoundBy10min(DateTime.UtcNow);

        stateTextBox.Text = EnvtCanXmlVM.Instance.TempActlDbl < 0 ? "SNOW" : "RAIN";
        stateTextBox.Foreground = new SolidColorBrush(EnvtCanXmlVM.Instance.TempActlDbl < 0 ? Colors.LightYellow : Colors.DodgerBlue);

        for (int min = _maxPastInMin; min >= 0; min -= 10)
        {
          var t10 = roundedBy10MinGmtNow.AddMinutes(-min);
          var url = EnvCanRadarUrlHelper.GetRadarUrl(t10);//, stateTextBox.Text);
          lvRadar.Items?.Add(new Image { Source = new BitmapImage(new Uri(url)), Height = 48, Tag = t10 });
        }

        _timer.Start();

        btnPlayRadar.Opacity = _timer.IsEnabled ? 0 : 1; // Play2.Visibility = _timer.IsEnabled ? Visibility.Collapsed : Visibility.Visible;			Play2.Icon = _timer.IsEnabled ? new SymbolIcon(Symbol.Pause) : new SymbolIcon(Symbol.Play);				btnPlayRadar.Icon = new SymbolIcon(Symbol.Pause);				btnPlayRadar.Visibility = _timer.IsEnabled ? Visibility.Collapsed : Visibility.Visible;
      }
      catch (Exception ex)
      {
        tbMax.FontSize = 14; // reduce from 48 to accommodate whole message.
        tbMax.Text = ex.Message;
      }
      finally
      {
        lvRadar.Visibility = Visibility.Visible;
      }
    }

    static SpeechSynthesizer _synth = new SpeechSynthesizer();
    public static async Task Speak0(MediaElement medEl, string text)//_Click(object sender, RoutedEventArgs e)
    {
      if (medEl.CurrentState.Equals(MediaElementState.Playing)) // If the media is playing, the user has pressed the button to stop the playback.
      {
        medEl.Stop(); // btnSpeak_Content = "Speak";
      }
      else
      {
        if (!String.IsNullOrEmpty(text))
        {
          // btnSpeak_Content = "Stop"; // Change the button label. You could also just disable the button if you don't want any user control.

          try
          {
            var speechSynthesisStream = await _synth.SynthesizeTextToStreamAsync(text); // Create a stream from the text. This will be played using a media element.

            medEl.AutoPlay = true;
            medEl.SetSource(speechSynthesisStream, speechSynthesisStream.ContentType);
            medEl.Play();
            await Task.Delay(1_500);
          }
          catch (System.IO.FileNotFoundException)
          {
            // If media player components are unavailable, (eg, using a N SKU of windows), we won't be able to start media playback. Handle this gracefully
            // btnSpeak_Content = "Speak";
            //btnSpeak.IsEnabled = false;
            //textToSynthesize.IsEnabled = false;
            //listboxVoiceChooser.IsEnabled = false;
            var messageDialog = new Windows.UI.Popups.MessageDialog("Media player components unavailable");
            await messageDialog.ShowAsync();
          }
          catch (Exception)
          {
            // If the text is unable to be synthesized, throw an error message to the user.
            // btnSpeak_Content = "Speak";
            medEl.AutoPlay = false;
            var messageDialog = new Windows.UI.Popups.MessageDialog("Unable to synthesize text");
            await messageDialog.ShowAsync();
          }
        }
      }
    }
  }


}
/// BlurEffect : http://metulev.com/render-xaml-to-image-and-more/