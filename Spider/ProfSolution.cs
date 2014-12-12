using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    class ProfSolution
    {
        static WebClient wc = new WebClient();
        static Queue<Tuple<string, int>> spiderq = new Queue<Tuple<string, int>>();

        static IEnumerable<string> ParseDoc(string url, int i)
        {
            var data = wc.DownloadString(url);
            var match = 
            foreach (var item in collection)
            {
                
            }
        }   

        static void Main(string[] args)
        {
            var maxDepth =2;
            spiderq.Enqueue(new Tuple<string, int>("http://repubblica.it/,",0));

            using (var ctxt = new SpiderEntities())
            {
                while (spiderq.Count > 0)
                {
                    var u = spiderq.Dequeue();
                    wc.DownloadString(u.Item1);
                    var data = new Spider.url();
                    data.depth = 1;
                    data.content = c;
                    data.type = "text/html";
                    ctxt.urls.Add(data);
                    ctxt.SaveChanges();
                    if (data.depth < maxDepth)
                        foreach (var u in ParseDoc(c))
                            spiderq.Enqueue(new Tuple<string, int>(u, data.depth + 1));
                }
            }
        }
    }
}