using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_TIPODespesa*/ 
    public class TipoDeDespesa
    {
        public int IdTipoDespesa { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public Publicas.TipoDespesa Tipo { get; set; }
        public decimal ValorMaximo { get; set; }
        public bool PedeAutorizacao { get; set; }
    }
}
