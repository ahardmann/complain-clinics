namespace Clinic.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class ModeloDados : IdentityDbContext<ApplicationUser>
    {
        public ModeloDados()
            : base("name=ModeloDados")
        {
        }


        public static ModeloDados Create()
        {
            return new ModeloDados();
        }

        public virtual DbSet<Ong> Ong { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Necessidade> Necessidade { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.RUA)
                .IsUnicode(false);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.CIDADE)
                .IsUnicode(false);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.TELEFONE_PRIMARIO)
                .IsUnicode(false);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.TELEFONE_SECUNDARIO)
                .IsUnicode(false);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.ESTADO)
                .IsUnicode(false);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.BAIRRO)
                .IsUnicode(false);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.CEP)
                .IsUnicode(false);

            modelBuilder.Entity<Ong>()
                .Property(e => e.CNPJ)
                .IsUnicode(false);

            modelBuilder.Entity<Ong>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<Ong>()
                .Property(e => e.SITE)
                .IsUnicode(false);
            
            modelBuilder.Entity<Ong>()
            .Property(e => e.EMAIL);


            modelBuilder.Entity<Ong>()
                .HasRequired(e => e.Endereco)
                .WithMany()
                .HasForeignKey(e => e.ID_END);

            modelBuilder.Entity<Ong>()
                .HasRequired(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.ID_USER);

            modelBuilder.Entity<Ong>()
                .HasMany(e => e.Necessidades)
                .WithRequired(e => e.Ong);


            modelBuilder.Entity<Necessidade>()
               .Property(e => e.DESCRICAO)
               .IsUnicode(false);

            modelBuilder.Entity<Necessidade>()
                .Property(e => e.TITULO)
                .IsUnicode(false);

            modelBuilder.Entity<Necessidade>()
                .Property(e => e.PRIORIDADE)
                .IsUnicode(false);

            //modelBuilder.Entity<Necessidade>()
            //    .Property(e => e.CNPJ)
            //    .IsUnicode(false);

            modelBuilder.Entity<Necessidade>()
                .HasRequired(e => e.Ong)
                .WithMany()
                .HasForeignKey(e => e.ID_ONG);
        }

    }
}
