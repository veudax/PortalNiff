using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DescontoBeneficioBO
    {

        public List<DescontoBeneficios> CalcularDescontoPorFerias(DateTime inicio, DateTime fim, List<EmpresaDoUsuario> empresas, List<OcorrenciasGlobus> justificadas, List<OcorrenciasGlobus> injustificadas)
        {
            return new DescontoBeneficioDAO().CalculaDescontoPorFerias(inicio, fim, empresas, justificadas, injustificadas);
        }

        public List<DescontoBeneficios> CalcularDesconto(DateTime inicio, DateTime fim, List<EmpresaDoUsuario> empresas, List<OcorrenciasGlobus> justificadas, List<OcorrenciasGlobus> injustificadas)
        {
            return new DescontoBeneficioDAO().CalculaDesconto(inicio, fim, empresas, justificadas, injustificadas);
        }
    }
}
