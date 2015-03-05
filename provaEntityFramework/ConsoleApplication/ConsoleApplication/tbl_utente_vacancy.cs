namespace ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_utente_vacancy
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_utente { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid id_vacancy { get; set; }

        public DateTime data_registrazione { get; set; }

        public bool letto { get; set; }

        [Required]
        [StringLength(50)]
        public string valutato { get; set; }

        [StringLength(500)]
        public string note { get; set; }

        public virtual tbl_utente tbl_utente { get; set; }

        public virtual tbl_vacancy tbl_vacancy { get; set; }
    }
}
