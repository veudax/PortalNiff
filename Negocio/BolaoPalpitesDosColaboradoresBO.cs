using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BolaoPalpitesDosColaboradoresBO
    {
        public List<BolaoPalpitesDosColaboradores> Listar(int Ano, int Colaborador, bool naoCadastro)
        {
            return new BolaoPalpitesDosColaboradoresDAO().Listar(Ano, Colaborador, naoCadastro);
        }

        public List<BolaoPalpitesDosColaboradores> AcompanharJogos(int Ano)
        {
            return new BolaoPalpitesDosColaboradoresDAO().AcompanharJogos(Ano);
        }

        public List<BolaoPalpitesDosColaboradores> Listar(int IdJogos)
        {
            return new BolaoPalpitesDosColaboradoresDAO().Listar(IdJogos);
        }

        public List<BolaoPalpitesDosColaboradores> Ranking(int Ano)
        {
            return new BolaoPalpitesDosColaboradoresDAO().Ranking(Ano);
        }

        public BolaoPalpitesDosColaboradores Consultar(int IdColaborador, int IdJogo)
        {
            return new BolaoPalpitesDosColaboradoresDAO().Consultar(IdColaborador, IdJogo);
        }

        public bool Gravar(List<BolaoPalpitesDosColaboradores> listaPalpites)
        {
            return new BolaoPalpitesDosColaboradoresDAO().Grava(listaPalpites);
        }
        
        public bool GravarPontuacao(List<BolaoPalpitesDosColaboradores> listaPalpites)
        {
            return new BolaoPalpitesDosColaboradoresDAO().GravaPontuacao(listaPalpites);
        }

        public bool Excluir(int id)
        {
            return new BolaoPalpitesDosColaboradoresDAO().Exclui(id);
        }
    }
}
