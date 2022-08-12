using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CargosBO
    {
        public List<Cargos> Listar(bool apenasAtivos = false)
        {
            return new CargosDAO().Listar(apenasAtivos);
        }

        public Cargos Consultar(int codigo)
        {
            return new CargosDAO().Consulta(codigo);
        }

        public int Proximo()
        {
            return new CargosDAO().Proximo();
        }

        public bool Gravar(Cargos tipo, List<CompetenciasDoCargo> competencias, List<MetasDoCargo> metas)
        {
            return new CargosDAO().Gravar(tipo, competencias, metas);
        }

        public bool Excluir(Cargos tipo)
        {
            return new CargosDAO().Excluir(tipo);
        }
    }
}
