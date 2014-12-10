using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;

namespace ExWithAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CHIAMATA ASYNC");
            calls();
            Console.ReadLine();
        }

        async static void calls()
        {
            StringBuilder sb = new StringBuilder();
            var result = await CallUrl();
            sb.Append(result);

            var words = Regex.Split(sb.ToString(), @"\W+");

            var q = from w in words
                    where w.Length > 5
                    group w by w.ToLower() into g
                    orderby g.Count() descending
                    select new { Word = g.Key, Counts = g.Count() };

            foreach (var item in q)
            {
                Console.WriteLine(item);
            }
        }

        private static async Task<string> CallUrl()
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            var urls = new Uri[] { 
                new Uri("http://www.repubblica.it"),
                new Uri("http://www.corriere.it"),
                new Uri("http://www.gazzetta.it")
            };

            string aux = String.Empty;
            foreach (var url in urls)
            {
                Task<string> getStringTask = client.GetStringAsync(url);
                Console.WriteLine("url chiamata: " + url);
                aux += await getStringTask;
            }

            return aux;
        }
    }
}