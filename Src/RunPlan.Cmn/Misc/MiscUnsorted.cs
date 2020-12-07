using System.Diagnostics;
using Windows.Storage;
using Windows.UI;

namespace RadarAnimation.Cmn.Misc
{
    public static class Mus
    {
        public static Color TmprClr(int tf, int dtMin, int dtMax)
        {
            byte r, g, b, k;
            var trnge = dtMax - dtMin;
            if (trnge == 0) trnge = 1;
            var mdl = dtMin + .5 * trnge;

            if (tf < mdl)
            {
                k = (byte)(510.0 * (dtMax - tf) / trnge);
                b = 255;
                r = (byte)(byte.MaxValue - k);
                g = (byte)(byte.MaxValue - k);
            }
            else if (tf > mdl)
            {
                k = (byte)(510.0 * (tf - dtMin) / trnge);
                r = 255;
                b = (byte)(byte.MaxValue - k);
                g = (byte)(byte.MaxValue - k);
            }
            else
            {
                r = g = b = 255;
            }

            //..Debug.WriteLine($"{tf,3}: {r,3} {g,3} {b,3}");
            return Color.FromArgb(255, r, g, b);
        }
        public static Color TmprClrFromKnwnExtr(int tf)
        {
            byte r, g, b, k;
            var dtMax = GetUpdateKnownExtremum(tf, Extr.TempHistMax);
            var dtMin = GetUpdateKnownExtremum(tf, Extr.TempHistMin);
            var trnge = dtMax - dtMin;
            if (trnge == 0) trnge = 1;
            var mdl = dtMin + .5 * trnge;

            if (tf < mdl)
            {
                k = (byte)(510.0 * (dtMax - tf) / trnge);
                b = 255;
                r = (byte)(byte.MaxValue - k);
                g = (byte)(byte.MaxValue - k);
            }
            else if (tf > mdl)
            {
                k = (byte)(510.0 * (tf - dtMin) / trnge);
                r = 255;
                b = (byte)(byte.MaxValue - k);
                g = (byte)(byte.MaxValue - k);
            }
            else
            {
                r = g = b = 255;
            }

            //..Debug.WriteLine($"{tf,3}: {r,3} {g,3} {b,3}");
            return Color.FromArgb(255, r, g, b);
        }

        public static double GetUpdateKnownExtremum(double dt, Extr extr)
        {
            var xtr = extr.ToString();
            if (!(ApplicationData.Current.RoamingSettings.Values.Keys.Contains(xtr) &&
                     (ApplicationData.Current.RoamingSettings.Values[xtr] is double) &&
                        (extr == Extr.TempHistMax ?
                        (double)ApplicationData.Current.RoamingSettings.Values[xtr] > dt :
                        (double)ApplicationData.Current.RoamingSettings.Values[xtr] < dt))
            )
                ApplicationData.Current.RoamingSettings.Values[xtr] = dt;

            return (double)ApplicationData.Current.RoamingSettings.Values[xtr];
        }

    }
    public enum Extr { TempHistMin, TempHistMax };
}
