using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class OcorrenciasGlobusBO
    {
        public List<OcorrenciasGlobus> Listar()
        {
            return new OcorrenciasGlobusDAO().Lista();
        }

        public List<AreaGlobus> ListarArea()
        {
            return new OcorrenciasGlobusDAO().ListarArea();
        }

        public List<FuncoesGlobus> ListarFuncoes()
        {
            return new OcorrenciasGlobusDAO().ListarFuncoes();
        }

        public List<TipoDeFrota> ListarTipoDeFrota()
        {
            return new OcorrenciasGlobusDAO().ListarTipoDeFrota();
        }
    }
}
