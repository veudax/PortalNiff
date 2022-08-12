using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BolaoPontuacaoBO
    {
        public List<BolaoPontuacao> Listar(int Ano)
        {
            return new BolaoPontuacaoDAO().Listar(Ano);
        }

        public BolaoPontuacao Consultar(int Ano, string Nome)
        {
            return new BolaoPontuacaoDAO().Consultar(Ano, Nome);
        }

        public bool Gravar(BolaoPontuacao times)
        {
            return new BolaoPontuacaoDAO().Grava(times);
        }

        public bool Excluir(int Id)
        {
            return new BolaoPontuacaoDAO().Exclui(Id);
        }
    }
}
