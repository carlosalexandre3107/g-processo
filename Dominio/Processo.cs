using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GProcessos.Dominio
{
    [Serializable]
    public class Processo
    {
        public String Numero { get; set; }
        public String Estado { get; set; }
        public Decimal Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public Boolean Ativo { get; set; }
        public String Empresa { get; set; }
    }
}