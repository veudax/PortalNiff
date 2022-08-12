using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CompetenciasDoCargoBO
    {
        public List<CompetenciasDoCargo> Listar(int cargo, string tipo, bool avaliacao = false, int colaborador = 0, string referencia = "", Publicas.TipoPrazos tipoAvaliacao = Publicas.TipoPrazos.AutoAvaliacao)
        {
            return new CompetenciasDoCargosDAO().Listar(cargo, tipo, avaliacao, colaborador, referencia, tipoAvaliacao);
        }

        public bool Gravar(List<CompetenciasDoCargo> _lista)
        {
            return new CompetenciasDoCargosDAO().Gravar(_lista);
        }

        public bool Excluir(int cargo)
        {
            return new CompetenciasDoCargosDAO().Excluir(cargo);
        }
    }
}
