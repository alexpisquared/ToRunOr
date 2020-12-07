using AsLink;
using Cmn.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Cmn.Services
{
  public class EnvtCanHtmlParser
  {
    const double TOLERANCE = .01;

    public static List<EnvtCanDto> Past24hourAtButtonville(HtmlDocument doc)
    {
      var ecdList = new List<EnvtCanDto>();
      // http://embedded101.com/Blogs/David-Jones/entryid/739/Universal-Windows-10-Screen-Scraping-a-Table-into-a-List
      //..Debug.WriteLine(doc.GetElementbyId("past24Table").Elements("tr").Count());
      //..Debug.WriteLine(doc.GetElementbyId("past24Table").Descendants("tr").Count());
      //..Debug.WriteLine(doc.GetElementbyId("past24Table").Descendants("tr").Where(tr => tr.ChildNodes.Any()).Count());
      //..Debug.WriteLine(doc.GetElementbyId("past24Table").ChildNodes.Count());
      //..Debug.WriteLine(doc.GetElementbyId("past24Table").Elements("tbody").Count());
      //..Debug.WriteLine(doc.GetElementbyId("past24Table").Descendants("tbody").Count());
      //..Debug.WriteLine(doc.GetElementbyId("past24Table").ChildNodes.Count());
      // http://www.webscrape.net/
      //..Debug.WriteLine(doc.DocumentNode.Ancestors("//table/tbody").Count());
      //..Debug.WriteLine(doc.DocumentNode.Descendants("//table/tbody").Count());
      //..Debug.WriteLine(doc.DocumentNode.Descendants("tr").Count());

      //explore(doc, t1a);

      try
      {
        var sDate = "";
        var dd = doc.GetElementbyId("past24Table");
        if (dd != null)
          foreach (var tr in dd.Descendants("tr"))
          {
            var c = tr.Descendants("td");
            var a = c.ToArray();
            var cnt = c.Count();

            //// Debug.WriteLine($"\n== tr.*.Count:   ChildNodes:{tr.ChildNodes.Count()}:   Descendants:{tr.Descendants().Count()}:   th:{tr.Descendants("th").Count()}:   td:{tr.Descendants("td").Count()}:   {tr.InnerHtml}");
            //Debug.Write($"\n::> ttl: {cnt}:"); foreach (HtmlNode t in c) Debug.Write(($"  {i++}:'{t?.InnerText?.tx()}' "));

            if (tr.Descendants("th").Count() == 1 && (tr.Descendants("th").ElementAt(0).InnerText.Trim().EndsWith(DateTime.Today.Year.ToString()) || tr.Descendants("th").ElementAt(0).InnerText.Trim().EndsWith((DateTime.Today.Year - 1).ToString())))
            {
              sDate = tr.Descendants("th").First().InnerText.Trim();
            }
            else if (cnt >= 8)
            {
              try
              {
                var e4 = new EnvtCanDto { TempActl = -999 };
                e4.ObserveT = Convert.ToDateTime(sDate + ' ' + a[0].InnerText.tx());

                e4.Descrptn = a[1].InnerText.tx();

                var r1 = a[1]?.FirstChild?.InnerHtml.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries); // <img class="media-object" height="35" width="35" src="/weathericons/small/01.png">
                if (r1.Length > 7)
                {
                  var r2 = r1[7].Split(new char[] { '/', '.' }, StringSplitOptions.RemoveEmptyEntries);
                  if (r2.Length > 2)
                    e4.IconCode = r2[2];
                }



                var c5 = a[2].InnerText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                e4.TempActl = c5.Count() > 1
                  ? double.Parse(c5[1].Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim('↑').Trim('↓').Trim('(').Trim(')').Replace("(", "").Trim())
                  : double.Parse(a[2].InnerText.Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim('↑').Trim('↓').Trim());

                int int32;
                if (cnt == 13) // no humidex
                {
                  e4.TempFeel = (int)Math.Round(e4.TempActl);
                  if (int.TryParse(a[6].InnerText.tx(), out int32))
                    e4.Humidity = int32;
                }
                else
                {
                  e4.TempFeel = int.TryParse(a[6].InnerText.tx(), out int32) ? int32 : (int)Math.Round(e4.TempActl);
                }


                //var c7 = a[7].InnerText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                //if (c7.Count() > 1)
                //	e4.Humidity = double.Parse(c7[1].Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim('↑').Trim('↓').Trim('(').Trim(')').Replace("(", "").Trim());
                //else
                e4.Humidity = double.Parse(a[8].InnerText.tx());

                ////var c9 = a[9].InnerText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                ////if (c9.Count() > 1)
                ////  e4.DewPoint = double.Parse(c9[1].Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim('↑').Trim('↓').Trim('(').Trim(')').Replace("(", "").Trim());
                ////else
                ////  e4.DewPoint = double.Parse(a[9].InnerText);

                //e4.Pressure   /**/ = double.Parse(a[cnt - 8].InnerText);
                //e4.Visibility /**/ = double.Parse(a[cnt - 4].InnerText);
                //if (cnt != 27 && !a[13].InnerText.Contains("*"))
                //	e4.TempFeel    /**/ = int.Parse(a[13].InnerText);

                var s = a[5].InnerText.Trim();
                var w = s.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                switch (w.Length)
                {
                  case 1: e4.WindDirn = w[0]; e4.WindKmHr = 0; break;
                  case 2: e4.WindDirn = w[0]; e4.WindKmHr = Convert.ToInt32(w[1]); break;
                  case 3: break;
                  case 4: e4.WindDirn = w[0]; e4.WindKmHr = Convert.ToInt32(w[1]); e4.WindGust = Convert.ToInt32(w[3]); break;
                  default: break;
                }

                if (Math.Abs(e4.TempActl - (-999)) > TOLERANCE)
                  ecdList.Add(e4);
              }
              catch (Exception ex) { DevOp.ExHrT(ex); }
            }
          }
        //old: for (int curpos = 0, i = 0; i < 25; i++)			{				var  e = process1hourButtonvilleLikeEntry(ref s, ref sDate, ref html, ref curpos);				if (e != null && e.Pressure > 0)					ecdList.Add(e);			}
        //`for (int i = 0; i < ecdList.Count; i++) Console.WriteLine("{0,2}) {1}", i, ecdList[i].ToString());
      }
      catch (Exception ex) { DevOp.ExHrT(ex); }

      return ecdList;
    }
    public static List<EnvtCanDto> Fore24hourAtButtonville(HtmlDocument doc)
    {
      var sDate = "";
      var ecdList = new List<EnvtCanDto>();            //..Debug.WriteLine(doc.DocumentNode.Descendants("tr").Count());
      foreach (var tr in doc.DocumentNode.Descendants("tr"))
      {
        var c = tr.Descendants("td");
        var a = c.ToArray();
        var cnt = c.Count();
        ////Debug.WriteLine($"\n== tr.*.Count:   ChildNodes:{tr.ChildNodes.Count()}:   Descendants:{tr.Descendants().Count()}:   th:{tr.Descendants("th").Count()}:   td:{tr.Descendants("td").Count()}:   {tr.InnerHtml}");
        //Debug.Write($"\n::> ttl: {cnt}:"); foreach (HtmlNode t in c) Debug.Write(($"  {i++}:'{t?.InnerText?.tx()}' "));

        if (tr.Descendants("th").Count() == 1 && (tr.Descendants("th").ElementAt(0).InnerText.Trim().EndsWith(DateTime.Today.Year.ToString()) || tr.Descendants("th").ElementAt(0).InnerText.Trim().EndsWith((DateTime.Today.Year - 1).ToString())))
        {
          sDate = tr.Descendants("th").First().InnerText.Trim();
        }
        else if (cnt >= 5)
        {
          try
          {
            var e4 = new EnvtCanDto { TempActl = -999 };
            e4.ObserveT = Convert.ToDateTime(sDate + ' ' + a[0].InnerText.tx());

            e4.Descrptn = a[2].InnerText.tx();

            var r1 = a[2]?.FirstChild?.InnerHtml.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries); // <img class="media-object" height="35" width="35" src="/weathericons/small/01.png">
            if (r1.Length > 7)
            {
              var r2 = r1[7].Split(new char[] { '/', '.' }, StringSplitOptions.RemoveEmptyEntries);
              if (r2.Length > 2)
                e4.IconCode = r2[2];
            }
            else
            {
              r1 = a[2]?.InnerHtml.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries); // <img class="media-object" height="35" width="35" src="/weathericons/small/01.png">
              if (r1.Length > 9)
              {
                var r2 = r1[9].Split(new char[] { '/', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (r2.Length > 2)
                  e4.IconCode = r2[2];
              }
            }


            var c5 = a[1].InnerText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            e4.TempActl = c5.Count() > 1
              ? double.Parse(c5[1].Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim('↑').Trim('↓').Trim('(').Trim(')').Replace("(", "").Trim())
              : double.Parse(a[1].InnerText.Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim('↑').Trim('↓').Trim());

            e4.TempFeel = cnt > 5 && int.TryParse(a[5].InnerText.tx(), out var int32) ? int32 : (int)Math.Round(e4.TempActl);

            //var c7 = a[7].InnerText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //if (c7.Count() > 1)
            //	e4.Humidity = double.Parse(c7[1].Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim('↑').Trim('↓').Trim('(').Trim(')').Replace("(", "").Trim());
            //else
            //e4.Humidity = double.Parse(a[8].InnerText.tx());

            ////var c9 = a[9].InnerText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            ////if (c9.Count() > 1)
            ////  e4.DewPoint = double.Parse(c9[1].Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim('↑').Trim('↓').Trim('(').Trim(')').Replace("(", "").Trim());
            ////else
            ////  e4.DewPoint = double.Parse(a[9].InnerText);

            //e4.Pressure   /**/ = double.Parse(a[cnt - 8].InnerText);
            //e4.Visibility /**/ = double.Parse(a[cnt - 4].InnerText);
            //if (cnt != 27 && !a[13].InnerText.Contains("*"))
            //	e4.TempFeel    /**/ = int.Parse(a[13].InnerText);

            var s = a[4].InnerText.Trim();
            var w = s.Split(new char[] { ' ', '\n', (char)160 }, StringSplitOptions.RemoveEmptyEntries);
            switch (w.Length)
            {
              case 1: e4.WindDirn = w[0]; e4.WindKmHr = 0; break;
              case 2: e4.WindDirn = w[0]; e4.WindKmHr = Convert.ToInt32(w[1]); break;
              case 3: break;
              case 4: e4.WindDirn = w[0]; e4.WindKmHr = Convert.ToInt32(w[1]); e4.WindGust = Convert.ToInt32(w[3]); break;
              default: break;
            }

            if (Math.Abs(e4.TempActl - (-999)) > TOLERANCE)
              ecdList.Add(e4);
          }
          catch (Exception ex) { DevOp.ExHrT(ex); }
        }
      }

      //old: for (int curpos = 0, i = 0; i < 25; i++)			{				var  e = process1hourButtonvilleLikeEntry(ref s, ref sDate, ref html, ref curpos);				if (e != null && e.Pressure > 0)					ecdList.Add(e);			}

      //`for (int i = 0; i < ecdList.Count; i++) Console.WriteLine("{0,2}) {1}", i, ecdList[i].ToString());

      return ecdList;
    }



    public static List<EnvtCanDto> Fore24hourAtButtonville_(HtmlDocument doc)
    {
      var ecdList = new List<EnvtCanDto>();

      foreach (var tr in doc.DocumentNode.Ancestors("//table/tbody"))
      {
        //..Debug.WriteLine($"\n============ {tr.ChildNodes.Count()}:");

        foreach (var r in tr.ChildNodes)//.Where(n => n.ChildNodes.Count() > 0))
        {
          var c = r.ChildNodes;
          //..Debug.WriteLine($"\n------------        {c.Count()}:");
          var sDate = "";
          //'foreach (HtmlNode t in c) Debug.Write($"{i++,3}:{t.InnerText.Trim()} \t");
          if (c.Count() == 3)
          {
            sDate = c[1].InnerText.Trim();
          }
          else if (c.Count() >= 11)
          {
            try
            {
              var e4 = new EnvtCanDto { TempActl = -999 };
              e4.ObserveT = Convert.ToDateTime(sDate + ' ' + c[1].InnerText);

              e4.TempActl = double.Parse(c[3].InnerText.Trim(' ').Trim('\n').Trim(' ').Trim('\n').Trim('↑').Trim('↓').Trim());
              if (c.Count() > 11 && double.TryParse(c[11].InnerText, out var dbl)) e4.TempFeel = (int)Math.Round(dbl);
              e4.Descrptn = c[5].InnerText.Trim();

              //e4.Humidity = double.Parse(c[7].InnerText);
              //e4.DewPoint = double.Parse(c[9].InnerText);
              //e4.Pressure = double.Parse(c[13].InnerText);
              //e4.Visibility = double.Parse(c[15].InnerText);

              var s = c[9].InnerText.Trim();
              var w = s.Split(new char[] { ' ', ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
              switch (w.Length)
              {
                case 1: e4.WindDirn = w[0]; e4.WindKmHr = 0; break;
                case 2: e4.WindDirn = w[0]; e4.WindKmHr = Convert.ToInt32(w[1]); break;
                case 3: break;
                case 4: e4.WindDirn = w[0]; e4.WindKmHr = Convert.ToInt32(w[1]); e4.WindGust = Convert.ToInt32(w[3]); break;
                default: break;
              }

              if (Math.Abs(e4.TempActl - (-999)) > TOLERANCE) ecdList.Add(e4);
            }
            catch (Exception ex) { DevOp.ExHrT(ex); }
          }
        }
      }

      //`for (int i = 0; i < ecdList.Count; i++) Console.WriteLine("{0,2}) {1}", i, ecdList[i].ToString());

      return ecdList;
    }
    private static void explore(HtmlDocument doc, IEnumerable<HtmlNode> t1a)
    {
      foreach (var tr in t1a)
      {
        //..Debug.WriteLine($"*/>{tr.InnerText.Trim(),-22}   {tr.InnerHtml.Trim()}");
        foreach (var td in doc.DocumentNode?.Descendants("td"))
        {
          if (td != null)
          {
            //..Debug.WriteLine($"*/>{td.InnerText.Trim(),-22}   {td.InnerHtml.Trim()}");
          }
        }
      }
    }
  }

  public static class StrExt
  {
    public static string tx(this string t) => t.Replace("\n", " ").Replace("\t", " ").Replace("    ", " ").Replace("   ", " ").Replace("  ", " ").Trim();
  }
}
