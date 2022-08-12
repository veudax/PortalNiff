using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FuncionariosGlobusBO
    {
        public List<FuncionariosGlobus> Listar(string empresa, decimal idSuperior = 0, bool participaAvaliacao = false)
        {
            return new FuncionariosGlobusDAO().Lista(empresa, idSuperior, participaAvaliacao);
        }

        public List<FuncionariosGlobus> Listar(string empresa, bool somenteAtivos)
        {
            return new FuncionariosGlobusDAO().Listar(empresa, somenteAtivos);
        }

        public FuncionariosGlobus ConsultarFuncionarioGlobus(string registro, string empresa)
        {
            return new FuncionariosGlobusDAO().ConsultaFuncionarioGlobus(registro, empresa);
        }

        public FuncionariosGlobus ConsultaFuncionarioGlobus(decimal codintFunc)
        {
            return new FuncionariosGlobusDAO().ConsultaFuncionarioGlobus(codintFunc);
        }
    }
}
