using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace EsempioSQLite.Model
{
    [Table("UserLocation")]
    public class UserLocation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Latidute { get; set; }
        public double Longitude { get; set; }
    }
}