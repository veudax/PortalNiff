using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CompetenciasDoColaboradorBO
    {
        public List<CompetenciasDoColaborador> Listar(int cargo, int usuario, string tipo)
        {
            return new CompetenciasDoColaboradorDAO().Listar(cargo, usuario, tipo);
        }

        public bool Gravar(List<CompetenciasDoColaborador> _lista)
        {
            return new CompetenciasDoColaboradorDAO().Gravar(_lista);
        }

        public bool Excluir(int usuario)
        {
            return new CompetenciasDoColaboradorDAO().Excluir(usuario);
        }
    }
}
