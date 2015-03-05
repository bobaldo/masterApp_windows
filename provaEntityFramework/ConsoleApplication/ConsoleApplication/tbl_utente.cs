namespace ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_utente
    {
        public tbl_utente()
        {
            tbl_utente_vacancy = new HashSet<tbl_utente_vacancy>();
            tbl_disponibilita = new HashSet<tbl_disponibilita>();
            tbl_disponibilita1 = new HashSet<tbl_disponibilita>();
            tbl_lingua = new HashSet<tbl_lingua>();
            tbl_settore = new HashSet<tbl_settore>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string email { get; set; }

        [Required]
        [StringLength(500)]
        public string nome { get; set; }

        [Required]
        [StringLength(500)]
        public string cognome { get; set; }

        public DateTime? data_nascita { get; set; }

        [StringLength(100)]
        public string nazionalita { get; set; }

        [StringLength(100)]
        public string citta { get; set; }

        [StringLength(10)]
        public string cap { get; set; }

        [StringLength(700)]
        public string via { get; set; }

        [StringLength(100)]
        public string nazione { get; set; }

        [StringLength(100)]
        public string telefono_casa { get; set; }

        [StringLength(20)]
        public string telefono_cellulare { get; set; }

        public int? id_livello_studio { get; set; }

        public int? id_esperienza { get; set; }

        [StringLength(100)]
        public string cv { get; set; }

        [StringLength(1)]
        public string sesso { get; set; }

        public DateTime last_date_operation { get; set; }

        public string note { get; set; }

        public virtual tbl_esperienza tbl_esperienza { get; set; }

        public virtual tbl_livello_studio tbl_livello_studio { get; set; }

        public virtual ICollection<tbl_utente_vacancy> tbl_utente_vacancy { get; set; }

        public virtual ICollection<tbl_disponibilita> tbl_disponibilita { get; set; }

        public virtual ICollection<tbl_disponibilita> tbl_disponibilita1 { get; set; }

        public virtual ICollection<tbl_lingua> tbl_lingua { get; set; }

        public virtual ICollection<tbl_settore> tbl_settore { get; set; }
    }
}
