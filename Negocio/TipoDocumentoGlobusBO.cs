using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TipoDocumentoGlobusBO
    {
        public TipoDocumentoGlobus Consultar(string empresa, string codigo)
        {
            return new TipoDocumentoGlobusDAO().Consulta(empresa, codigo);
        }

        public List<TipoDocumentoGlobus> Listar(string empresa)
        {
            return new TipoDocumentoGlobusDAO().Listar(empresa);
        }
    }
}
