using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.ViewModel
{
    public class OcorrenciasViewModel
    {
        public string NOME_CLINICA { get; set; }

        public string NOME_MEDICO { get; set; }

        public string SITE_CLINICA { get; set; }

        public string ATRASO_MEDIO { get; set; }

        public int NUMERO_OCORRENCIAS { get; set; }

        public DateTime DATA { get; set; }

        public virtual EnderecoViewModel Endereco { get; set; }
    }
}