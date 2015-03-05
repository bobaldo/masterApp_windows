namespace ConsoleApplication
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Modello : DbContext
    {
        public Modello()
            : base("name=connDB")
        {
        }

        public virtual DbSet<tbl_area_geografica> tbl_area_geografica { get; set; }
        public virtual DbSet<tbl_disponibilita> tbl_disponibilita { get; set; }
        public virtual DbSet<tbl_esperienza> tbl_esperienza { get; set; }
        public virtual DbSet<tbl_lingua> tbl_lingua { get; set; }
        public virtual DbSet<tbl_livello_studio> tbl_livello_studio { get; set; }
        public virtual DbSet<tbl_owner> tbl_owner { get; set; }
        public virtual DbSet<tbl_settore> tbl_settore { get; set; }
        public virtual DbSet<tbl_utente> tbl_utente { get; set; }
        public virtual DbSet<tbl_utente_vacancy> tbl_utente_vacancy { get; set; }
        public virtual DbSet<tbl_vacancy> tbl_vacancy { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_disponibilita>()
                .HasMany(e => e.tbl_utente)
                .WithMany(e => e.tbl_disponibilita)
                .Map(m => m.ToTable("tbl_area_geografica_utente").MapLeftKey("id_area_geografica").MapRightKey("id_utente"));

            modelBuilder.Entity<tbl_disponibilita>()
                .HasMany(e => e.tbl_utente1)
                .WithMany(e => e.tbl_disponibilita1)
                .Map(m => m.ToTable("tbl_disponibilita_utente").MapLeftKey("id_disponibilita").MapRightKey("id_utente"));

            modelBuilder.Entity<tbl_esperienza>()
                .HasMany(e => e.tbl_utente)
                .WithOptional(e => e.tbl_esperienza)
                .HasForeignKey(e => e.id_esperienza);

            modelBuilder.Entity<tbl_lingua>()
                .HasMany(e => e.tbl_utente)
                .WithMany(e => e.tbl_lingua)
                .Map(m => m.ToTable("tbl_lingua_utente").MapLeftKey("id_lingua").MapRightKey("id_utente"));

            modelBuilder.Entity<tbl_livello_studio>()
                .HasMany(e => e.tbl_utente)
                .WithOptional(e => e.tbl_livello_studio)
                .HasForeignKey(e => e.id_livello_studio);

            modelBuilder.Entity<tbl_settore>()
                .HasMany(e => e.tbl_utente)
                .WithMany(e => e.tbl_settore)
                .Map(m => m.ToTable("tbl_settore_utente").MapLeftKey("id_settore").MapRightKey("id_utente"));

            modelBuilder.Entity<tbl_utente>()
                .HasMany(e => e.tbl_utente_vacancy)
                .WithRequired(e => e.tbl_utente)
                .HasForeignKey(e => e.id_utente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_vacancy>()
                .HasMany(e => e.tbl_utente_vacancy)
                .WithRequired(e => e.tbl_vacancy)
                .HasForeignKey(e => e.id_vacancy)
                .WillCascadeOnDelete(false);
        }
    }
}
