using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CompetenciasBO
    {
        public List<Competencias> Listar(bool apenasAtivos = false)
        {
            return new CompetenciasDAO().Listar(apenasAtivos);
        }

        public Competencias Consultar(int codigo)
        {
            return new CompetenciasDAO().Consulta(codigo);
        }

        public int Proximo()
        {
            return new CompetenciasDAO().Proximo();
        }

        public bool Gravar(Competencias tipo, List<SubCompetencias> _listas)
        {
            return new CompetenciasDAO().Gravar(tipo, _listas);
        }

        public bool Excluir(Competencias tipo)
        {
            return new CompetenciasDAO().Excluir(tipo);
        }

        public List<SubCompetencias> ListarSubCompetencias(bool apenasAtivos = false, int idCompetencias = 0)
        {
            return new SubCompetenciasDAO().Listar(apenasAtivos, idCompetencias);
        }
    }
}
