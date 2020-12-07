using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmn.AsLink
{
	public class EnvNormals
	{
		public TimeSpan SunRze { get; set; } = TimeSpan.FromHours(6);
		public TimeSpan SunSet { get; set; } = TimeSpan.FromHours(18);
		public double NormHigh { get; set; } = 20;
		public double NormLow { get; set; } = -20;
		public double ExtrHigh { get; set; } = +5;
		public double ExtrLow { get; set; } = -5;
	}
}


/// http://climate.weather.gc.ca/climate_normals/results_1981_2010_e.html?searchType=stnProx&txtRadius=25&optProxType=city&selCity=43%7C47%7C79%7C35%7CVaughan&selPark=&txtCentralLatDeg=&txtCentralLatMin=0&txtCentralLatSec=0&txtCentralLongDeg=&txtCentralLongMin=0&txtCentralLongSec=0&stnID=5097&dispBack=0
/// 