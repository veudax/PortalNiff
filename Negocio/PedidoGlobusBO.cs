using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PedidoGlobusBO
    {

        public List<PedidoGlobus> Consultar(string pedido)
        {
            return new PedidoGlobusDAO().Consultar(pedido);
        }

        public bool Gravar(List<PedidoGlobus> lista)
        {
            return new PedidoGlobusDAO().Gravar(lista);
        }
    }
}
