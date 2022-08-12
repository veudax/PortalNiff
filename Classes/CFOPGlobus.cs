using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class CFOPGlobus
    {
        public int CFOP { get; set; }
        public string Descricao { get; set; }
        public bool Existe { get; set; }
    }

    public class OperacaoGlobus
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Existe { get; set; }
    }

    public class CSTGlobus
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Existe { get; set; }
    }
}
