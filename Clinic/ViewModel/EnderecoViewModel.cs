using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.ViewModel
{
    public class EnderecoViewModel
    {
        public string RUA { get; set; }

        public string CIDADE { get; set; }

        public string TELEFONE_PRIMARIO { get; set; }

        public string TELEFONE_SECUNDARIO { get; set; }

        public string ESTADO { get; set; }

        public string BAIRRO { get; set; }

        public string CEP { get; set; }
    }
}