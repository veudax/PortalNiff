using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FornecedoresGlobusBO
    {
        public FornecedoresGlobus Consultar(string codigo)
        {
            return new FornecedoresGlobusDAO().Consulta(codigo);
        }

        public FornecedoresGlobus Consultar(decimal codigo)
        {
            return new FornecedoresGlobusDAO().Consulta(codigo);
        }

        public List<FornecedoresGlobus> Listar()
        {
            return new FornecedoresGlobusDAO().Listar();
        }
    }
}
