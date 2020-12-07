using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cmn.Model;
using Windows.Web.Syndication;

namespace Cmn.Services
{
    public class EnvtCanXmlParser
    {
        public static async Task<EnvtCanDto> ReadXmlOffGcCa()
        {
            try
            {
                var feed = await new SyndicationClient().RetrieveFeedAsync(new Uri(@"https://weather.gc.ca/rss/city/on-143_e.xml"));
                if (feed != null)
                {
                    Debug.Write("\n\nDate \t Title \t Summary: \n\n"); foreach (var i in feed.Items) Debug.Write($"{i.PublishedDate:MMM-dd HH}   {i.Title.Text,-62} {i.Summary.Text} \n"); // see C:\c\Lgc\WpfChart\xRssConsoleApp\Program.cs for more dev dbg.

                    var htm = feed.Items.FirstOrDefault(r => r.Title.Text.Contains("Current Conditions:"));
                    var ecd = EnvtCanDto.Parse(htm?.Summary?.Text.Split('\n'));
                    ecd.ObserveT = htm.PublishedDate.DateTime;
                    return ecd;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message, "System.Reflection.MethodInfo.GetCurrentMethod().Name"); if (Debugger.IsAttached) Debugger.Break(); /*else throw;*/ }

            return new EnvtCanDto();
        }
    }
}
