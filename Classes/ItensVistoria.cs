using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_ItensVistoria*/
    public class ItensVistoria
    {
        public int IdItens { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool ItemCritico { get; set; }
    }
}
