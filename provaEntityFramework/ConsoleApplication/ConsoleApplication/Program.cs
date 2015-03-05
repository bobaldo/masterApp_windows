using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //insertVacancy();
            insertVacancyAsync();
            foreach (var item in selectVacancy())
            {
                Console.WriteLine("codice: \t" + item.codice + "\t job title: \t" + item.job_title);

            }
            foreach (var item in selectSettore())
            {
                Console.WriteLine("id:\t" + item.id + "\tdescrizione:\t" + item.descrizione_it);
            }
            foreach (var item in selectLingua())
            {
                Console.WriteLine("id:\t" + item.id + "\tdescrizione:\t" + item.descrizione_it);
            }
            Console.ReadLine();
        }

        private static List<tbl_settore> selectSettore()
        {
            List<tbl_settore> ret;
            using (var ctx = new Modello())
            {
                ret = ctx.tbl_settore.ToList();
            }
            return ret;
        }

        private static List<tbl_lingua> selectLingua()
        {
            List<tbl_lingua> ret;
            using (var ctx = new Modello())
            {
                ret = ctx.tbl_lingua.ToList();
            }
            return ret;
        }

        private static List<tbl_vacancy> selectVacancy()
        {
            List<tbl_vacancy> ret;
            using (var ctx = new ConsoleApplication.Modello())
            {
                ret = ctx.tbl_vacancy.ToList();
            }
            return ret;
        }

        private static void insertVacancyAsync()
        {
            using (var ctx = new ConsoleApplication.Modello())
            {
                DbSet<tbl_lingua> lingua = ctx.tbl_lingua;
                lingua.First();

                var item = new tbl_vacancy();
                item.codice = "ASYNC";
                item.id = Guid.NewGuid();
                item.data_apertura = DateTime.Now;
                item.job_title = "PD";
                item.lingua = lingua.First().descrizione_it;
                item.numero_persone_cercate = 0;
                item.ordine = 99;
                item.visibile = true;

                ctx.tbl_vacancy.Add(item);
                ctx.SaveChangesAsync();
            }
        }

        private static void insertVacancy()
        {
            using (var ctx = new ConsoleApplication.Modello())
            {
                DbSet<tbl_lingua> lingua = ctx.tbl_lingua;
                lingua.First();

                var item = new tbl_vacancy();
                item.codice = "ABACO";
                item.id = Guid.NewGuid();
                item.data_apertura = DateTime.Now;
                item.job_title = "PD";
                item.lingua = lingua.First().descrizione_it;
                item.numero_persone_cercate = 0;
                item.ordine = 99;
                item.visibile = true;

                ctx.tbl_vacancy.Add(item);
                ctx.SaveChanges();
            }
        }
    }
}