using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_SALAREUNIAO*/ 
    public class SalaDeReuniao : Empresa
    {
        public int IdSala { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int Capacidade { get; set; }
        public bool Existe { get; set; }
        public bool Ativo { get; set; }
    }
}
