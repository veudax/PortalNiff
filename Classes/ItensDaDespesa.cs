using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_ItensDespesas*/
    public class ItensDaDespesa
    {
        public int IdItens { get; set; }
        public int IdDespesas { get; set; }
        public int IdTipoDespesa { get; set; }
        public Publicas.TipoDespesa Tipo { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public byte[] Imagem { get; set; }
        public int IdUsuario { get; set; }
    }
}
