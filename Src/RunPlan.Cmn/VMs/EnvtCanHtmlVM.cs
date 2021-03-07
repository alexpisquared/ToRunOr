using AsLink;
using Cmn.Misc;
using Cmn.Services;
using RadarAnimation.Cmn.Model;
using System;
using System.Threading.Tasks;

namespace VMs
{
  public class EnvtCanHtmlVM : BindableBase, IRefreshable
  {
    static EnvtCanHtmlVM _inst = null;
    EnvtCanHtmlVM() => TimeAgo_ = Value = "";
    public static EnvtCanHtmlVM Instance => _inst ?? (_inst = new EnvtCanHtmlVM());


    DateTime _lastUpdate = DateTime.MinValue;
    string _TimeAgo_;       /**/public string TimeAgo_ { get => _TimeAgo_; set => Set(ref _TimeAgo_, value); }
    string _Value = "Gas";  /**/public string Value { get => _Value; set => Set(ref _Value, value); }

    public async Task Refresh()
    {
      var now = DateTime.Now;
      if ((now - _lastUpdate) < Shared.LatencyEnvtCan) return;

      await ReLoad();
    }

    public async Task ReLoad()
    {
      if (!Connectivity.IsInternet())
      {
        return;
      }

      var ec = await EnvtCanXmlParser.ReadXmlOffGcCa();
      _lastUpdate = DateTime.Now;

      TimeAgo_ = $"{(_lastUpdate - ec.ObserveT).TotalMinutes:N0}";
      //TempFeel = $"{ec.TempFeel:+#;-#;0}°";
      //TempActl = $"{ec.TempActl:+#.#;-#.#;0}°";
      //WindKmph = $"{ec.WindKmH}";
      //Wind360d = $"{45 + ec.WindDeg}";

      //todo: MainPage.ExtrBrush = tTempFeel.Foreground = new SolidColorBrush(Mus.TmprClr(ec.TempFeel));
    }
  }
}
