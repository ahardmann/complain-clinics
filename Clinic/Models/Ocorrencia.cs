namespace Clinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ocorrencia")]
    public partial class Ocorrencia
    {
        [Key]
        public int ID_OCORRENCIA { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome Da Clinica")]
        public string NOME_CLINICA { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome Do Medico")]
        public string NOME_MEDICO { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Site Da Clinica")]
        public string SITE_CLINICA { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Atraso Do Medico")]
        public string ATRASO_MEDIO { get; set; }

        [Required]
        [Display(Name = "Numero De Ocorrências")]
        public int NUMERO_OCORRENCIAS { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        [Display(Name = "Data")]
        public DateTime DATA { get; set; }

        public int ID_END { get; set; }

        public virtual Endereco Endereco { get; set; }

        public string ID_USER { get; set; }

        public virtual ApplicationUser Usuario { get; set; }
    }
}
