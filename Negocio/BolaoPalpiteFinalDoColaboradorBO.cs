using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BolaoPalpiteFinalDoColaboradorBO
    {
        public List<BolaoPalpiteFinalDoColaborador> Listar(int Ano, int Colaborador)
        {
            return new BolaoPalpiteFinalDoColaboradorDAO().Listar(Ano, Colaborador);
        }

        public List<BolaoPalpiteFinalDoColaborador> Listar(int Ano)
        {
            return new BolaoPalpiteFinalDoColaboradorDAO().Listar(Ano);
        }

        public BolaoPalpiteFinalDoColaborador Consultar(int IdColaborador, int Ano)
        {
            return new BolaoPalpiteFinalDoColaboradorDAO().Consultar(IdColaborador, Ano);
        }

        public bool Gravar(BolaoPalpiteFinalDoColaborador times)
        {
           return new BolaoPalpiteFinalDoColaboradorDAO().Grava(times);
        }

        public bool GravarPontuacao(BolaoPalpiteFinalDoColaborador times)
        {
            return new BolaoPalpiteFinalDoColaboradorDAO().GravaPontuacao(times);
        }        

        public bool Excluir(int id)
        {
            return new BolaoPalpiteFinalDoColaboradorDAO().Exclui(id);
        }

    }
}
