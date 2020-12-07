using System;
using System.Linq;

namespace Cmn.Model
{
  public struct EnvtCanDto
  {
    public static EnvtCanDto Parse(string[] ary) // _MicrosoftToolkitParsersRss contains leading spaces => Trim or replace with Contains(), if there are perf issues.
    {
      var ec = new EnvtCanDto();
      string s;

      //var x2 = ary.FirstOrDefault(r => r.Trim().StartsWith("<b>Pressure"));
      //var x6 = ary.FirstOrDefault(r => r.Trim().StartsWith("<b>Dewpoint"));
      //var x3 = ary.FirstOrDefault(r => r.Trim().StartsWith("<b>Visibili"));
      //var x1 = ary.FirstOrDefault(r => r.Trim().StartsWith("<b>Humidity"));

      if (!string.IsNullOrEmpty(s = ary.FirstOrDefault(r => r.Trim().StartsWith("<b>Temperat")))) if (double.TryParse(s.Trim().Split(' ')[1].Split('&')[0], out var dbl)) ec.TempFeel = (int)Math.Round(ec.TempActl = dbl);
      if (!string.IsNullOrEmpty(s = ary.FirstOrDefault(r => r.Trim().StartsWith("<b>Wind Chi")))) if (int.TryParse(s.Trim().Split(' ')[2], out var i3)) ec.TempFeel = i3;
      if (!string.IsNullOrEmpty(s = ary.FirstOrDefault(r => r.Trim().StartsWith("<b>Humidex:")))) if (int.TryParse(s.Trim().Split(' ')[1], out var i3)) ec.TempFeel = i3;
      if (!string.IsNullOrEmpty(s = ary.FirstOrDefault(r => r.Trim().StartsWith("<b>Conditio")))) ec.Descrptn = s.Replace(" ", "\n").Replace("<b>Condition:</b>", "").Replace("<br/>", "").Trim();
      if (!string.IsNullOrEmpty(s = ary.FirstOrDefault(r => r.Trim().StartsWith("<b>Wind:</b"))))
      {
        var wrd = s.Trim().Split(' ');
        if (wrd.Length == 4)
        {
          ec.WindDirn = wrd[1];
          if (int.TryParse(wrd[2], out var i32)) ec.WindKmHr = ec.WindGust = i32;
        }
        else if (wrd.Length == 7)
        {
          ec.WindDirn = wrd[1];
          if (int.TryParse(wrd[2], out var i32)) ec.WindKmHr = i32;
          if (int.TryParse(wrd[5], out i32)) ec.WindGust = i32;
        }
      }

      return ec;
    }

    public string IconUrl => IconUrlLarge;
    public string IconUrlLarge => $"https://weather.gc.ca/weathericons/{IconCode}.gif";
    //blic string IconUrlSmall => $"https://weather.gc.ca/weathericons/small/{IconCode}.gif";
    public string WindFull => $"{WindKmHr} k/h {WindDirn}";
    public string TempRnge => TempDayH == 0 ? $" ► {TempDayL}°" : TempDayL == 0 ? $"{TempDayH}° ► " : $"{TempDayH}° ► {TempDayL}°";

    public string Location { get; set; }
    public string PeriodNm { get; set; }        // eg: Sunday, SundayNight (which is actually Monday morning), Monday
    public DateTime ObserveT { get; set; }      // datetime of PeriodNm
    public double TempActl { get; set; }
    public int TempFeel { get; set; }
    public string WindDirn { get; set; }
    public int WindKmHr { get; set; }
    public int WindGust { get; set; }
    public int Wind360d
    {
      get
      {
        const int d = 25;
        switch (WindDirn)
        {
          default: return 301;
          case "N": return 0;
          case "W": return 270;
          case "S": return 180;
          case "E": return 90;

          case "NE": return 45;
          case "NW": return 315;
          case "SE": return 180 - 45;
          case "SW": return 180 + 45;

          case "ENE": return 90 - d;
          case "ESE": return 90 + d;
          case "WNW": return 270 + d;
          case "WSW": return 270 - d;

          case "NEN":
          case "NNE": return 0 + d;
          case "NWN":
          case "NNW": return 0 - d;
          case "SES":
          case "SSE": return 180 - d;
          case "SWS":
          case "SSW": return 180 + d;
        }
      }
    }

    public string Descrptn { get; set; }
    public string IconCode { get; set; }
    public double Humidity { get; set; }
    public double Pressure { get; set; }
    public double Visibility { get; set; }
    public int TempDayH { get; set; }
    public int TempDayL { get; set; }
  }
}
