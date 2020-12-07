using System;

namespace RadarAnimation.Cmn.Model
{
    public sealed class Shared
    {
        const string msg = "Msg";
        const int maxLogGedRows = 8; // 14 take 3665 ==> 30 for 8k or 15 for 4k (unicode?) ===> let's try the smallest to get it going first!!! //org: Value determined by how many max length event descriptors (91 chars) stored as a JSON string can fit in 8K (max allowed for local settings)
        const uint taskPeriod =
#if DEBUG
            15;
#else
		60;
#endif
        public static readonly double SpecDbl = 0.000777;
        public static TimeSpan LatencyEnvtCan = TimeSpan.FromSeconds(30);

        public static string Db { get { return "Geofence.SQLite.db"; } }

        public static string LogStrRecords { get { return "LogStrRecords"; } }
        public static string LogGedRecords { get { return "LogGedRecords"; } }
        public static string ExnDetails { get { return "GeoExnStatus"; } }
        public static string Msg { get { return msg; } }
        public static int MaxLogGedRows { get { return maxLogGedRows; } }
        public static uint TaskPeriod { get { return taskPeriod; } }
#if true
        public static uint GeoLocrPeriodInMs => 5000;
        public static uint GeoLocrMovementThresholdInM => 0;
#else
		public static uint GeoLocrPeriodInMs => 0;
		public static uint GeoLocrMovementThresholdInM => 50;
#endif
    }
}
