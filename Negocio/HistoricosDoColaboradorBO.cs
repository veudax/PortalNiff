using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class HistoricosDoColaboradorBO
    {
        public List<HistoricosDoColaborador> Listar(int IdColaborador)
        {
            return new HistoricosDoColaboradorDAO().Listar(IdColaborador);
        }

        public bool Gravar(HistoricosDoColaborador _historico)
        {
            return new HistoricosDoColaboradorDAO().Gravar(_historico);
        }

        public bool Excluir(int IdColaborador)
        {
            return new HistoricosDoColaboradorDAO().Excluir(IdColaborador);
        }
    }
}
