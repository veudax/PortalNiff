using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CentroDeCustoFinanceiroBO
    {
        public List<CentroDeCustoFinanceiro> Listar()
        {
            return new CentroDeCustoFinanceiroDAO().Listar();
        }

        public CentroDeCustoFinanceiro Consulta(int codigo)
        {
            return new CentroDeCustoFinanceiroDAO().Consulta(codigo);
        }
    }
}
