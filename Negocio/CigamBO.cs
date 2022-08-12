using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CigamBO
    {
        public List<Cigam> ListarGlobus(int idEmpresa, string Empresa, DateTime Inicio, DateTime Fim)
        {
            return new CigamDAO().ListarGlobus(idEmpresa, Empresa, Inicio, Fim);
        }

        public List<Cigam> ListarCigam(string idEmpresa, DateTime Inicio, DateTime Fim)
        {
            return new CigamDAO().ListarCigam(idEmpresa, Inicio, Fim);
        }

        public bool Gravar(List<Cigam> _lista, string idEmpresa, DateTime Inicio, DateTime Fim)
        {
            return new CigamDAO().Gravar(_lista, idEmpresa, Inicio, Fim);
        }

        public bool VerificaSeFoiExcluido( string idEmpresa, DateTime Inicio, DateTime Fim)
        {
            return new CigamDAO().VerificaSeFoiExcluido(idEmpresa, Inicio, Fim);
        }

        public bool LimparBase(string idEmpresa, DateTime Inicio, DateTime Fim)
        {
            return new CigamDAO().LimparBase(idEmpresa, Inicio, Fim);
        }
    }
}
