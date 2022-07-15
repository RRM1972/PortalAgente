using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyalIGWEB.Models
{
    public class EnvioCorreo
    {
        public string Para { get; set; }
        public string Copia { get; set; }
        public string AsuntoCorreo { get; set; }
        public string CuerpoCorreo { get; set; }
    }
}