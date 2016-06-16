using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.ViewModel
{
    public class OngViewModel
    {
        public string CNPJ { get; set; }

        public string NOME { get; set; }

        public string SITE { get; set; }

        public string EMAIL { get; set; }


        public virtual EnderecoViewModel Endereco { get; set; }
    }
}