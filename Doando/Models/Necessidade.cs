namespace Clinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Necessidade")]
    public partial class Necessidade
    {
        [Key]
        public int ID_NECESSIDADE { get; set; }

        [Required]
        [StringLength(100)]
        public string DESCRICAO { get; set; }

        [Required]
        [StringLength(80)]
        public string TITULO { get; set; }

        [Required]
        [StringLength(100)]
        public string PRIORIDADE { get; set; }

        public DateTime DATA { get; set; }

        public int ID_ONG { get; set; }
        
        public virtual Ong Ong { get; set; }
    }
}
