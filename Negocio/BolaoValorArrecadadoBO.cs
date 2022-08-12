using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BolaoValorArrecadadoBO
    {
        public List<BolaoValorArrecadado> Listar(int Ano)
        {
            return new BolaoValorArrecadadoDAO().Listar(Ano);
        }

        public BolaoValorArrecadado Consultar(int Ano)
        {
            return new BolaoValorArrecadadoDAO().Consultar(Ano);
        }

        public BolaoValorArrecadado Consultar(int Ano, int idColaborador)
        {
            return new BolaoValorArrecadadoDAO().Consultar(Ano, idColaborador);
        }

        public bool Gravar(BolaoValorArrecadado times)
        {
            return new BolaoValorArrecadadoDAO().Grava(times);
        }

        public bool Excluir(int id)
        {
            return new BolaoValorArrecadadoDAO().Exclui(id);
        }
    }
}
