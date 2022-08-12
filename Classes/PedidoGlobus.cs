using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class PedidoGlobus
    {
        public string Pedido { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Sequencial { get; set; }
        public string Material { get; set; }
        public Publicas.TipoPedidoGlobus Tipo { get; set; }
        public Publicas.TipoPedidoGlobus TipoOriginal { get; set; }
        public decimal Desconto { get; set; }
        public decimal IPI { get; set; }
        public decimal ICMS { get; set; }
        public decimal Seguro { get; set; }
        public decimal Frete { get; set; }
        public decimal ISS { get; set; }
        public decimal ICMSSubstituicao { get; set; }

    }
}
