    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Model
{
    public class Vacancy
    {

        public Guid ID
        {
            get;
            set;
        }

        public DateTime DataApertua
        {
            get;
            set;
        }

        public string jobTitle
        {
            get;
            set;
        }
    }
}