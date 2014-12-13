using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;

namespace Spider
{
    class ProfSolution
    {
        static WebClient wc = new WebClient();

        static IEnumerable<string> ParseDoc(string doc)
        {
            var matches = Regex.Matches(doc, @"http://[\w+\./\?=\+\-\%]+");
            for (var i = 0; i < matches.Count; i++)
                yield return matches[i].Value;
        }

        static Queue<Tuple<string, int>> spiderq = new Queue<Tuple<string, int>>();

        static void Main(string[] args)
        {
            var maxdepth = 2;
            spiderq.Enqueue(new Tuple<string, int>("http://www.repubblica.it", 0));

            using (var ctxt = new SpiderEntities())
            {
                while (spiderq.Count > 0)
                {
                    var u = spiderq.Dequeue();
                    var c = wc.DownloadString(u.Item1);
                    var data = new Spider.url();
                    data.depth = u.Item2;
                    data.uri = u.Item1;
                    data.content = c;
                    data.type = "text/html";
                    ctxt.urls.Add(data);
                    ctxt.SaveChanges();
                    if (data.depth < maxdepth)
                    {
                        foreach (var url in ParseDoc(c))
                            spiderq.Enqueue(new Tuple<string, int>(url, data.depth + 1));
                    }
                }
            }
        }
    }
}