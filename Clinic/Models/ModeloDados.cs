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

        public virtual DbSet<Ocorrencias> Ocorrencia { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        //public virtual DbSet<Necessidade> Necessidade { get; set; }

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

            modelBuilder.Entity<Ocorrencias>()
                .Property(e => e.NOME_CLINICA)
                .IsUnicode(false);

            modelBuilder.Entity<Ocorrencias>()
                .Property(e => e.NOME_MEDICO)
                .IsUnicode(false);

            modelBuilder.Entity<Ocorrencias>()
                .Property(e => e.SITE_CLINICA)
                .IsUnicode(false);

            modelBuilder.Entity<Ocorrencias>()
                .Property(e => e.ATRASO_MEDIO)
                .IsUnicode(false);

            modelBuilder.Entity<Ocorrencias>()
            .Property(e => e.NUMERO_OCORRENCIAS);

            modelBuilder.Entity<Ocorrencias>()
            .Property(e => e.DATA);

            modelBuilder.Entity<Ocorrencias>()
                .HasRequired(e => e.Endereco)
                .WithMany()
                .HasForeignKey(e => e.ID_END);

            modelBuilder.Entity<Ocorrencias>()
                .HasRequired(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.ID_USER);
        }

    }
}
