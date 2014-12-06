using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace LinqEx
{


    class Program
    {
        static void Main(string[] args)
        {
            var wp = new WebClient();
            var sites = new string[] { 
                "http://www.repubblica.it",
                "http://www.di.unipi.it/",
                "http://www.davidepatrizi.com"
            };

            //http://msdn.microsoft.com/it-it/library/hh191443.aspx

            var tb = new StringBuilder();
            foreach (var s in sites)
            {
                tb.Append(wp.DownloadString(s));
            }

            //@ disabilitita i caratteri speciali
            var words = Regex.Split(tb.ToString(), @"\W+");

            var q = from w in words
                    group w by w.ToLower() into counts
                    orderby counts.Count() descending
                    select new { Word = counts.Key, Counts = counts.Count() };

            //lamda expression equivalente di quella sopra
            //var q1 = words.GroupBy(s => s)
            //       .OrderBy(g => g.Key)
            //       .Select(g => new { Word = g.Key, Count = g.Count() });

            var l = "";
            while ((l = Console.ReadLine()) != String.Empty)
            {
                foreach (var e in q.Where(el => el.Word == l.ToLower()))
                {
                    Console.WriteLine(e);
                }
            }

            //group by per il numero di occorrenze
            Console.ReadLine();
        }
    }
}