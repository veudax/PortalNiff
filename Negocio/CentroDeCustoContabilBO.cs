using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CentroDeCustoContabilBO
    {
        public List<CentroDeCustoContabil> Listar(string empresaGlobus)
        {
            return new CentroDeCustoContabilDAO().Listar(empresaGlobus);
        }

        public CentroDeCustoContabil Consultar(int codigo, string empresaGlobus)
        {
            return new CentroDeCustoContabilDAO().Consulta(codigo, empresaGlobus);
        }

        public CentroDeCustoContabil Consultar(int codigo, int plano)
        {
            return new CentroDeCustoContabilDAO().Consulta(codigo, plano);
        }
    }
}
