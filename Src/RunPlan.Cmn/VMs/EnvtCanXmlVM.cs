using AsLink;
using Cmn.Misc;
using Cmn.Model;
using Cmn.Services;
using RadarAnimation.Cmn.Model;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media;

namespace VMs
{
  public class EnvtCanXmlVM : BindableBase, IRefreshable
  {
    static EnvtCanXmlVM _inst = null;
    private EnvtCanXmlVM()
    {
      ObsdAtHH = ObsdAtMM = TempFeel = AgeInMin = TempActl = Wind360d = "0";
      WindKmph = WindGust = -1;
    }
    public static EnvtCanXmlVM Instance => _inst ?? (_inst = new EnvtCanXmlVM());

    EnvtCanDto _ec; public EnvtCanDto EC => _ec;
    public DateTime LastUpdate { get; private set; } = DateTime.MinValue;

    string _ObsdAtMM;       /**/  public string ObsdAtMM { get => _ObsdAtMM; set => Set(ref _ObsdAtMM, value); }
    string _ObsdAtHH;       /**/  public string ObsdAtHH { get => _ObsdAtHH; set => Set(ref _ObsdAtHH, value); }
    string _TempFeel;       /**/  public string TempFeel { get => _TempFeel; set => Set(ref _TempFeel, value); }
    string _AgeInMin;       /**/  public string AgeInMin { get => _AgeInMin; set => Set(ref _AgeInMin, value); }
    string _TempActl;       /**/  public string TempActl { get => _TempActl; set => Set(ref _TempActl, value); }
    int _WindKmph;          /**/  public int WindKmph { get => _WindKmph; set => Set(ref _WindKmph, value); }
    int _WindGust;          /**/  public int WindGust { get => _WindGust; set => Set(ref _WindGust, value); }
    string _Wind360d;       /**/  public string Wind360d { get => _Wind360d; set => Set(ref _Wind360d, value); }
    string _WindTTip;       /**/  public string WindTTip { get => _WindTTip; set => Set(ref _WindTTip, value); }
    string _Conditns;       /**/  public string Conditns { get => _Conditns; set => Set(ref _Conditns, value); }
    string _Value = "Gas";  /**/  public string Value { get => _Value; set => Set(ref _Value, value); }
    Brush _ExtrBrush;       /**/  public Brush ExtrBrush { get => _ExtrBrush; set => Set(ref _ExtrBrush, value); }


    public int TempFeelI => _ec.TempFeel;

    public async Task Refresh()
    {
      if ((DateTime.Now - LastUpdate) < Shared.LatencyEnvtCan) return;

      await ReLoad();
    }

    public async Task ReLoad()
    {
      if (!Connectivity.IsInternet())
        return;

      _ec = await EnvtCanXmlParser.ReadXmlOffGcCa();
      LastUpdate = DateTime.Now;
      TempActlDbl = _ec.TempActl;

      ObsdAtHH = $"{_ec.ObserveT.Hour}";
      ObsdAtMM = $"{_ec.ObserveT:\\:mm}"; //  (_lastUpdate - ec.ObsdAt).TotalMinutes:N0}";
      TempFeel = $"{_ec.TempFeel:+#;-#;0}°";
      TempActl = $"{_ec.TempActl:+#.#;-#.#;0}°";
      WindKmph = _ec.WindKmHr;
      WindGust = _ec.WindGust;
      Wind360d = $"{_ec.Wind360d - 135}";
      WindTTip = $"Wind\t {_ec.WindDirn}\r\nkm/h\t{_ec.WindKmHr}\r\nGust\t{_ec.WindGust}";
      Conditns = _ec.Descrptn;

      //todo: ExtrBrush = new SolidColorBrush(Mus.TmprClr(_ec.TempFeel, -50, +50));
    }

    const string _key = "sTempActlDbl";
    public double TempActlDbl
    {
      get
      {
        return ApplicationData.Current.RoamingSettings.Values.Keys.Contains(_key) && ApplicationData.Current.RoamingSettings.Values[_key] is double
          ? (double)ApplicationData.Current.RoamingSettings.Values[_key]
          : 0d;
      }
      set => ApplicationData.Current.RoamingSettings.Values[_key] = value;
    }
  }
}
