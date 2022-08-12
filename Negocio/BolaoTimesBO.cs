using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BolaoTimesBO
    {
        public List<BolaoTimes> Listar(int Ano)
        {
            return new BolaoTimesDAO().Listar(Ano);
        }

        public BolaoTimes Consultar(int Ano, string Sigla)
        {
            return new BolaoTimesDAO().Consultar(Ano, Sigla);
        }

        public BolaoTimes Consultar(int Id)
        {
            return new BolaoTimesDAO().Consultar(Id);
        }

        public bool Gravar(BolaoTimes times)
        {
            return new BolaoTimesDAO().Grava(times);
        }

        public bool Excluir(int id)
        {
            return new BolaoTimesDAO().Exclui(id);
        }
    }
}
