// a copy from   C:\C\Live\GpsFit\GpsFitCentral\HalHigdon_Nov2.cs
using System;
using System.Collections.Generic;

namespace GpsFitCentral
{
    public enum eHalHigdon { Nov2, Int1, Int2 }
    public enum eTrMode { Easy, Pace, Cros, Race }
    public class TrUnit
    {
        public TrUnit(int daysLeft, double miles) { DaysTo = daysLeft; DistMiles = miles; Mode = HalHigdon._easy; }
        public TrUnit(int daysLeft, double miles, string intencity) { DaysTo = daysLeft; DistMiles = miles == 0.0 ? 6.0 : miles; Mode = intencity; }
        public int DaysTo { get; set; }
        public double DistMiles { get; set; }
        public string Mode { get; set; }
        public eTrMode TrMode { get; set; }
    }
    public class HalHigdon
    {
        public static TrUnit TrainingPlanForTheDay(DateTime day)
        {
            var raceDay = new DateTime(DateTime.Today.Year, 10, 21);
            var daysTil = Intermediate1.Length - (int)((raceDay - day).TotalDays) - 1; // change 0 => 1 to have long runs on Sunday.

            if (0 < daysTil && daysTil < Intermediate1.Length) return Intermediate1[daysTil];
            else if (day.DayOfWeek == DayOfWeek.Tuesday) return new TrUnit(-180, 4.6, _easy);
            else if (day.DayOfWeek == DayOfWeek.Thursday) return new TrUnit(-180, 4.6, _pace);
            else if (day.DayOfWeek == DayOfWeek.Saturday) return new TrUnit(-180, 13.1, _easy);
            else return null;
        }

        public static TrUnit[] Intermediate1 { get { return int1; } }
        public static TrUnit[] Nov2 { get { return nov2; } }
        public static TrUnit[] Int1 { get { return int1; } }
        public static TrUnit[] Int2 { get { return int2; } }

        public const string _race = "race", _pace = "pace", _cros = "cros", _easy = "easy";

        static TrUnit[] nov2 = new TrUnit[] {
            new TrUnit(  -125, 0), new TrUnit( -124, 3), new TrUnit( -123, 5, _pace), new TrUnit( -122, 3), new TrUnit( -121, 0), new TrUnit( -120,  0.0, _cros), new TrUnit( -119,  8),
            new TrUnit(  -118, 0), new TrUnit( -117, 3), new TrUnit( -116, 5       ), new TrUnit( -115, 3), new TrUnit( -114, 0), new TrUnit( -113,  0.0, _cros), new TrUnit( -112,  9),
            new TrUnit(  -111, 0), new TrUnit( -110, 3), new TrUnit( -109, 5, _pace), new TrUnit( -108, 3), new TrUnit( -107, 0), new TrUnit( -106,  0.0, _cros), new TrUnit( -105,  6),
            new TrUnit(  -104, 0), new TrUnit( -103, 3), new TrUnit( -102, 6, _pace), new TrUnit( -101, 3), new TrUnit( -100, 0), new TrUnit(  -99,  0.0, _cros), new TrUnit(  -98, 11),
            new TrUnit(   -97, 0), new TrUnit(  -96, 3), new TrUnit(  -95, 6       ), new TrUnit(  -94, 3), new TrUnit(  -93, 0), new TrUnit(  -92,  0.0, _cros), new TrUnit(  -91, 12),
            new TrUnit(   -90, 0), new TrUnit(  -89, 3), new TrUnit(  -88, 6, _pace), new TrUnit(  -87, 3), new TrUnit(  -86, 0), new TrUnit(  -85,  0.0, _cros), new TrUnit(  -84,  9),
            new TrUnit(   -83, 0), new TrUnit(  -82, 4), new TrUnit(  -81, 7, _pace), new TrUnit(  -80, 4), new TrUnit(  -79, 0), new TrUnit(  -78,  0.0, _cros), new TrUnit(  -77, 14),
            new TrUnit(   -76, 0), new TrUnit(  -75, 4), new TrUnit(  -74, 7       ), new TrUnit(  -73, 4), new TrUnit(  -72, 0), new TrUnit(  -71,  0.0, _cros), new TrUnit(  -70, 15),
            new TrUnit(   -69, 0), new TrUnit(  -68, 4), new TrUnit(  -67, 7, _pace), new TrUnit(  -66, 4), new TrUnit(  -65, 0), new TrUnit(  -64, 13.1, _race), new TrUnit(  -63,  0),
            new TrUnit(   -62, 0), new TrUnit(  -61, 4), new TrUnit(  -60, 8, _pace), new TrUnit(  -59, 4), new TrUnit(  -58, 0), new TrUnit(  -57,  0.0, _cros), new TrUnit(  -56, 17),
            new TrUnit(   -55, 0), new TrUnit(  -54, 5), new TrUnit(  -53, 8       ), new TrUnit(  -52, 5), new TrUnit(  -51, 0), new TrUnit(  -50,  0.0, _cros), new TrUnit(  -49, 18),
            new TrUnit(   -48, 0), new TrUnit(  -47, 5), new TrUnit(  -46, 8, _pace), new TrUnit(  -45, 5), new TrUnit(  -44, 0), new TrUnit(  -43,  0.0, _cros), new TrUnit(  -42, 13),
            new TrUnit(   -41, 0), new TrUnit(  -40, 5), new TrUnit(  -39, 5, _pace), new TrUnit(  -38, 5), new TrUnit(  -37, 0), new TrUnit(  -36,  0.0, _cros), new TrUnit(  -35, 19),
            new TrUnit(   -34, 0), new TrUnit(  -33, 5), new TrUnit(  -32, 8       ), new TrUnit(  -31, 5), new TrUnit(  -30, 0), new TrUnit(  -29,  0.0, _cros), new TrUnit(  -28, 12),
            new TrUnit(   -27, 0), new TrUnit(  -26, 5), new TrUnit(  -25, 5, _pace), new TrUnit(  -24, 5), new TrUnit(  -23, 0), new TrUnit(  -22,  0.0, _cros), new TrUnit(  -21, 20),
            new TrUnit(   -20, 0), new TrUnit(  -19, 5), new TrUnit(  -18, 4, _pace), new TrUnit(  -17, 5), new TrUnit(  -16, 0), new TrUnit(  -15,  0.0, _cros), new TrUnit(  -14, 12),
            new TrUnit(   -13, 0), new TrUnit(  -12, 4), new TrUnit(  -11, 3       ), new TrUnit(  -10, 4), new TrUnit(   -9, 0), new TrUnit(   -8,  0.0, _cros), new TrUnit(   -7,  8),
            new TrUnit(    -6, 0), new TrUnit(   -5, 3), new TrUnit(   -4, 2       ), new TrUnit(   -3, 0), new TrUnit(   -2, 0), new TrUnit(   -1,  2.0),          new TrUnit(    0,  26.2, _race)
        };


        static TrUnit[] nov2org = new TrUnit[] {
            new TrUnit(  -125, 0), new TrUnit( -124, 3), new TrUnit( -123, 5, _pace), new TrUnit( -122, 3), new TrUnit( -121, 0), new TrUnit( -120,  8), new TrUnit( -119,  0.0, _cros),
            new TrUnit(  -118, 0), new TrUnit( -117, 3), new TrUnit( -116, 5       ), new TrUnit( -115, 3), new TrUnit( -114, 0), new TrUnit( -113,  9), new TrUnit( -112,  0.0, _cros),
            new TrUnit(  -111, 0), new TrUnit( -110, 3), new TrUnit( -109, 5, _pace), new TrUnit( -108, 3), new TrUnit( -107, 0), new TrUnit( -106,  6), new TrUnit( -105,  0.0, _cros),
            new TrUnit(  -104, 0), new TrUnit( -103, 3), new TrUnit( -102, 6, _pace), new TrUnit( -101, 3), new TrUnit( -100, 0), new TrUnit(  -99, 11), new TrUnit(  -98,  0.0, _cros),
            new TrUnit(   -97, 0), new TrUnit(  -96, 3), new TrUnit(  -95, 6       ), new TrUnit(  -94, 3), new TrUnit(  -93, 0), new TrUnit(  -92, 12), new TrUnit(  -91,  0.0, _cros),
            new TrUnit(   -90, 0), new TrUnit(  -89, 3), new TrUnit(  -88, 6, _pace), new TrUnit(  -87, 3), new TrUnit(  -86, 0), new TrUnit(  -85,  9), new TrUnit(  -84,  0.0, _cros),
            new TrUnit(   -83, 0), new TrUnit(  -82, 4), new TrUnit(  -81, 7, _pace), new TrUnit(  -80, 4), new TrUnit(  -79, 0), new TrUnit(  -78, 14), new TrUnit(  -77,  0.0, _cros),
            new TrUnit(   -76, 0), new TrUnit(  -75, 4), new TrUnit(  -74, 7       ), new TrUnit(  -73, 4), new TrUnit(  -72, 0), new TrUnit(  -71, 15), new TrUnit(  -70,  0.0, _cros),
            new TrUnit(   -69, 0), new TrUnit(  -68, 4), new TrUnit(  -67, 7, _pace), new TrUnit(  -66, 4), new TrUnit(  -65, 0), new TrUnit(  -64,  0), new TrUnit(  -63, 13.1, _race),
            new TrUnit(   -62, 0), new TrUnit(  -61, 4), new TrUnit(  -60, 8, _pace), new TrUnit(  -59, 4), new TrUnit(  -58, 0), new TrUnit(  -57, 17), new TrUnit(  -56,  0.0, _cros),
            new TrUnit(   -55, 0), new TrUnit(  -54, 5), new TrUnit(  -53, 8       ), new TrUnit(  -52, 5), new TrUnit(  -51, 0), new TrUnit(  -50, 18), new TrUnit(  -49,  0.0, _cros),
            new TrUnit(   -48, 0), new TrUnit(  -47, 5), new TrUnit(  -46, 8, _pace), new TrUnit(  -45, 5), new TrUnit(  -44, 0), new TrUnit(  -43, 13), new TrUnit(  -42,  0.0, _cros),
            new TrUnit(   -41, 0), new TrUnit(  -40, 5), new TrUnit(  -39, 5, _pace), new TrUnit(  -38, 5), new TrUnit(  -37, 0), new TrUnit(  -36, 19), new TrUnit(  -35,  0.0, _cros),
            new TrUnit(   -34, 0), new TrUnit(  -33, 5), new TrUnit(  -32, 8       ), new TrUnit(  -31, 5), new TrUnit(  -30, 0), new TrUnit(  -29, 12), new TrUnit(  -28,  0.0, _cros),
            new TrUnit(   -27, 0), new TrUnit(  -26, 5), new TrUnit(  -25, 5, _pace), new TrUnit(  -24, 5), new TrUnit(  -23, 0), new TrUnit(  -22, 20), new TrUnit(  -21,  0.0, _cros),
            new TrUnit(   -20, 0), new TrUnit(  -19, 5), new TrUnit(  -18, 4, _pace), new TrUnit(  -17, 5), new TrUnit(  -16, 0), new TrUnit(  -15, 12), new TrUnit(  -14,  0.0, _cros),
            new TrUnit(   -13, 0), new TrUnit(  -12, 4), new TrUnit(  -11, 3       ), new TrUnit(  -10, 4), new TrUnit(   -9, 0), new TrUnit(   -8,  8), new TrUnit(   -7,  0.0, _cros),
            new TrUnit(    -6, 0), new TrUnit(   -5, 3), new TrUnit(   -4, 2       ), new TrUnit(   -3, 0), new TrUnit(   -2, 0), new TrUnit(   -1,  2), new TrUnit(    0, 26.2, _race)
        };
        static TrUnit[] int1 = new TrUnit[] {
            new TrUnit(-125, 0.0, _cros),new TrUnit(-124, 3),new TrUnit(-123, 5),new TrUnit(-122, 3),new TrUnit(-121, 0),new TrUnit(-120, 5, _pace),new TrUnit(-119, 8),
            new TrUnit(-118, 0.0, _cros),new TrUnit(-117, 3),new TrUnit(-116, 5),new TrUnit(-115, 3),new TrUnit(-114, 0),new TrUnit(-113, 5),new TrUnit(-112, 9),
            new TrUnit(-111, 0.0, _cros),new TrUnit(-110, 3),new TrUnit(-109, 5),new TrUnit(-108, 3),new TrUnit(-107, 0),new TrUnit(-106, 5, _pace),new TrUnit(-105, 6),
            new TrUnit(-104, 0.0, _cros),new TrUnit(-103, 3),new TrUnit(-102, 6),new TrUnit(-101, 3),new TrUnit(-100, 0),new TrUnit( -99, 6, _pace), new TrUnit(-98, 11),
            new TrUnit( -97, 0.0, _cros),new TrUnit( -96, 3),new TrUnit( -95, 6),new TrUnit( -94, 3),new TrUnit( -93, 0),new TrUnit( -92, 6),    new TrUnit(-91, 12),
            new TrUnit( -90, 0.0, _cros),new TrUnit( -89, 3),new TrUnit( -88, 5),new TrUnit( -87, 3),new TrUnit( -86, 0),new TrUnit( -85, 6, _pace),    new TrUnit(-84, 9),
            new TrUnit( -83, 0.0, _cros),new TrUnit( -82, 4),new TrUnit( -81, 7),new TrUnit( -80, 4),new TrUnit( -79, 0),new TrUnit( -78, 7, _pace),    new TrUnit(-77, 14),
            new TrUnit( -76, 0.0, _cros),new TrUnit( -75, 4),new TrUnit( -74, 7),new TrUnit( -73, 4),new TrUnit( -72, 0),new TrUnit( -71, 7),    new TrUnit(-70, 15),
            new TrUnit( -69, 0.0, _cros),new TrUnit( -68, 4),new TrUnit( -67, 5),new TrUnit( -66, 4),new TrUnit( -65, 0),new TrUnit( -64, 0),    new TrUnit(-63, 13.1, _race),
            new TrUnit( -62, 0.0, _cros),new TrUnit( -61, 4),new TrUnit( -60, 8),new TrUnit( -59, 4),new TrUnit( -58, 0),new TrUnit( -57, 8, _pace),    new TrUnit(-56, 17),
            new TrUnit( -55, 0.0, _cros),new TrUnit( -54, 5),new TrUnit( -53, 8),new TrUnit( -52, 5),new TrUnit( -51, 0),new TrUnit( -50, 8),    new TrUnit(-49, 18),
            new TrUnit( -48, 0.0, _cros),new TrUnit( -47, 5),new TrUnit( -46, 5),new TrUnit( -45, 5),new TrUnit( -44, 0),new TrUnit( -43, 8, _pace),    new TrUnit(-42, 13),
            new TrUnit( -41, 0.0, _cros),new TrUnit( -40, 5),new TrUnit( -39, 8),new TrUnit( -38, 5),new TrUnit( -37, 0),new TrUnit( -36, 5, _pace),    new TrUnit(-35, 20),
            new TrUnit( -34, 0.0, _cros),new TrUnit( -33, 5),new TrUnit( -32, 5),new TrUnit( -31, 5),new TrUnit( -30, 0),new TrUnit( -29, 8),    new TrUnit(-28, 12),
            new TrUnit( -27, 0.0, _cros),new TrUnit( -26, 5),new TrUnit( -25, 8),new TrUnit( -24, 5),new TrUnit( -23, 0),new TrUnit( -22, 5, _pace),    new TrUnit(-21, 20),
            new TrUnit( -20, 0.0, _cros),new TrUnit( -19, 5),new TrUnit( -18, 6),new TrUnit( -17, 5),new TrUnit( -16, 0),new TrUnit( -15, 4, _pace),    new TrUnit(-14, 12),
            new TrUnit( -13, 0.0, _cros),new TrUnit( -12, 4),new TrUnit( -11, 5),new TrUnit( -10, 4),new TrUnit(  -9, 0),new TrUnit(  -8, 3),      new TrUnit(-7, 8),
            new TrUnit(  -6, 0.0, _cros),new TrUnit(  -5, 3),new TrUnit(  -4, 4),new TrUnit(  -3, 0),new TrUnit(  -2, 0),new TrUnit(  -1, 2),        new TrUnit(0, 26.2)
        };

        static TrUnit[] int2 = new TrUnit[] {
            new TrUnit(-125, 0.0, _cros), new TrUnit(-124, 3),new TrUnit(-123, 5),new TrUnit(-122, 3),new TrUnit(-121,0),new TrUnit(-120, 5, _pace),new TrUnit(-119, 10),
            new TrUnit(-118, 0.0, _cros), new TrUnit(-117, 3),new TrUnit(-116, 5),new TrUnit(-115, 3),new TrUnit(-114,0),new TrUnit(-113, 5, _pace),new TrUnit(-112, 11),
            new TrUnit(-111, 0.0, _cros), new TrUnit(-110, 3),new TrUnit(-109, 6),new TrUnit(-108, 3),new TrUnit(-107,0),new TrUnit(-106, 6, _pace),new TrUnit(-105, 8),
            new TrUnit(-104, 0.0, _cros), new TrUnit(-103, 3),new TrUnit(-102, 6),new TrUnit(-101, 3),new TrUnit(-100,0),new TrUnit(-99,  6, _pace),new TrUnit(-98, 13),
                new TrUnit(-97, 0.0, _cros),new TrUnit( -96, 3),new TrUnit(-95, 7),new TrUnit(-94, 3),new TrUnit(-93,0),   new TrUnit(-92,  7, _pace),new TrUnit(-91, 14),
                new TrUnit(-90, 0.0, _cros),new TrUnit( -89, 3),new TrUnit(-88, 7),new TrUnit(-87, 3),new TrUnit(-86,0),   new TrUnit(-85,  7, _pace),new TrUnit(-84, 10),
                new TrUnit(-83, 0.0, _cros),new TrUnit( -82, 4),new TrUnit(-81, 8),new TrUnit(-80, 4),new TrUnit(-79,0),   new TrUnit(-78,  8, _pace),new TrUnit(-77, 16),
                new TrUnit(-76, 0.0, _cros),new TrUnit( -75, 4),new TrUnit(-74, 8),new TrUnit(-73, 4),new TrUnit(-72,0),   new TrUnit(-71,  8, _pace),new TrUnit(-70, 17),
                new TrUnit(-69, 0.0, _cros),new TrUnit( -68, 4),new TrUnit(-67, 9),new TrUnit(-66, 4),new TrUnit(-65,0),   new TrUnit(-64,  0       ),new TrUnit(-63, 13.1, _race),
                new TrUnit(-62, 0.0, _cros),new TrUnit( -61, 4),new TrUnit(-60, 9),new TrUnit(-59, 4),new TrUnit(-58,0),   new TrUnit(-57,  9, _pace),new TrUnit(-56, 19),
                new TrUnit(-55, 0.0, _cros),new TrUnit( -54, 5),new TrUnit(-53, 10),new TrUnit(-52, 5),new TrUnit(-51,0),   new TrUnit(-50, 10, _pace),new TrUnit(-49, 20),
                new TrUnit(-48, 0.0, _cros),new TrUnit( -47, 5),new TrUnit(-46, 6),new TrUnit(-45, 5),new TrUnit(-44,0),   new TrUnit(-43,  6, _pace),new TrUnit(-42, 12),
                new TrUnit(-41, 0.0, _cros),new TrUnit( -40, 5),new TrUnit(-39, 10),new TrUnit(-38, 5),new TrUnit(-37,0),   new TrUnit(-36, 10, _pace),new TrUnit(-35, 20),
                new TrUnit(-34, 0.0, _cros),new TrUnit( -33, 5),new TrUnit(-32, 6),new TrUnit(-31, 5),new TrUnit(-30,0),   new TrUnit(-29,  6, _pace),new TrUnit(-28, 12),
                new TrUnit(-27, 0.0, _cros),new TrUnit( -26, 5),new TrUnit(-25, 10),new TrUnit(-24, 5),new TrUnit(-23,0),   new TrUnit(-22, 10, _pace),new TrUnit(-21, 20),
                new TrUnit(-20, 0.0, _cros),new TrUnit( -19, 5),new TrUnit(-18, 8),new TrUnit(-17, 5),new TrUnit(-16,0),   new TrUnit(-15,  4, _pace),new TrUnit(-14, 12),
                new TrUnit(-13, 0.0, _cros),new TrUnit( -12, 4),new TrUnit(-11, 6),new TrUnit(-10, 4),new TrUnit(-9,0),    new TrUnit(-8,   4),new TrUnit(-7, 8),
            new TrUnit(-6, 0.0, _cros),   new TrUnit(  -5, 3),new TrUnit(-4, 4),new TrUnit(-3,0),new TrUnit(-2,0),       new TrUnit(-1,   2),new TrUnit(0, 26.2)
        };

        List<TrUnit[]> ltu = new List<TrUnit[]>();
    }
}