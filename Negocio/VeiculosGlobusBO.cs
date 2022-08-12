using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VeiculosGlobusBO
    {
        public List<Classes.VeiculosGlobus> Listar(string empresa)
        {
            return new VeiculosGlobusDAO().Listar(empresa);
        }

        public Classes.VeiculosGlobus Consultar(string empresa, string prefixo)
        {
            return new VeiculosGlobusDAO().Consultar(empresa, prefixo);
        }
    }
}
