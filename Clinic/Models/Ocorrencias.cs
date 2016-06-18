namespace Clinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ocorrencias")]
    public partial class Ocorrencias
    {
        [Key]
        public int ID_OCORRENCIA { get; set; }

        [Required]
        [StringLength(100)]
        public string NOME_CLINICA { get; set; }

        [Required]
        [StringLength(100)]
        public string NOME_MEDICO { get; set; }

        [Required]
        [StringLength(100)]
        public string SITE_CLINICA { get; set; }

        [Required]
        [StringLength(100)]
        public string ATRASO_MEDIO { get; set; }

        [Required]
        public int NUMERO_OCORRENCIAS { get; set; }

        [Required]
        public DateTime DATA { get; set; }

        public int ID_END { get; set; }

        public virtual Endereco Endereco { get; set; }

        public string ID_USER { get; set; }

        public virtual ApplicationUser Usuario { get; set; }
    }
}
