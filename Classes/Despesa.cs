using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /*tabela NIFF_CHM_Despesas*/
    public class Despesa
    {
        public int IdDespesas { get; set; }
        public int IdAgenda { get; set; }
        public DateTime DataReembolso { get; set; }
        public decimal ValorReembolso { get; set; }
        public int IdUsuarioAutorizou { get; set; }
    }
}
