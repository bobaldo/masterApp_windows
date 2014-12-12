using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spider
{
    /*scrivere uno spider web che prende un'insieme di url web, analizza le pagina cercando le url 
    e le vado a vedere */
    class Program
    {
        private const string EXPRESSION = @"http://[w+\./\?=\+\-\%]+";
        private static HttpClient wp = new HttpClient();

        static async Task<string[]> GetUrlsAsync(string url)
        {
            
            var tb = new StringBuilder();
            var tasks = wp.GetStringAsync(url);
            tb.Append(await tasks);
            var mat  = Regex.Matches(tb.ToString(), EXPRESSION);

            foreach (var m in mat)
            {
                write
            }
        }

        static void Main(string[] args)
        {
            var maxDepth = 5;
            var sites = new string[] { "http://www.davidepatrizi.com" };



            foreach (var i in sites)
            {
                var urls = GetUrlsAsync(i);

                foreach (var a in urls.Result)
                {
                    write(1, a, "aaa", "bbb");
                }
            }
        }

        private static void write(int depth, string uri, string type, string content)
        {
            using (var ctxt = new SpiderEntities())
            {
                var u = new Spider.url();
                u.depth = depth;
                u.type = type;
                u.uri = uri;
                u.content = content;
                ctxt.urls.Add(u);
                ctxt.SaveChanges();

                //query
                //var q = from uri in ctxt.urls where uri.depth == 0 select uri;
            }
        }
    }
}