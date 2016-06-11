namespace Clinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Endereco")]
    public partial class Endereco
    {
        [Key]
        public int ID_END { get; set; }

        [Required]
        [StringLength(100)]
        public string RUA { get; set; }

        [Required]
        [StringLength(100)]
        public string CIDADE { get; set; }

        [Required]
        [StringLength(100)]
        public string TELEFONE_PRIMARIO { get; set; }

        [Required]
        [StringLength(100)]
        public string TELEFONE_SECUNDARIO { get; set; }

        [Required]
        [StringLength(100)]
        public string ESTADO { get; set; }

        [Required]
        [StringLength(100)]
        public string BAIRRO { get; set; }

        [Required]
        [StringLength(100)]
        public string CEP { get; set; }

    }
}
