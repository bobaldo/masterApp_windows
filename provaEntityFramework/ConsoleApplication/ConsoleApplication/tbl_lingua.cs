namespace ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_lingua
    {
        public tbl_lingua()
        {
            tbl_utente = new HashSet<tbl_utente>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string descrizione_it { get; set; }

        [StringLength(100)]
        public string descrizione_en { get; set; }

        [StringLength(100)]
        public string descrizione_fr { get; set; }

        public virtual ICollection<tbl_utente> tbl_utente { get; set; }
    }
}
