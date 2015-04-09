namespace ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_vacancy
    {
        public tbl_vacancy()
        {
            tbl_utente_vacancy = new HashSet<tbl_utente_vacancy>();
        }

        public Guid id { get; set; }

        [Required]
        [StringLength(10)]
        public string codice { get; set; }

        public DateTime data_apertura { get; set; }

        public DateTime? data_chiusura { get; set; }

        public int ordine { get; set; }

        public bool visibile { get; set; }

        public int? numero_persone_cercate { get; set; }

        [Required]
        [StringLength(50)]
        public string lingua { get; set; }

        public string introduzione { get; set; }

        [Column("job_title")]
        public string jobTitle { get; set; }

        public string duty_station { get; set; }

        public string desidered_requirements { get; set; }

        public string job_requirements { get; set; }

        public string role_task_responsansabilities { get; set; }

        public string conditions { get; set; }

        public virtual ICollection<tbl_utente_vacancy> tbl_utente_vacancy { get; set; }
    }
}
