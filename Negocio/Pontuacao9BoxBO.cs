using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Pontuacao9BoxBO
    {
        public List<Pontuacao9Box> Listar(bool apenasAtivo)
        {
            return new Pontuacao9BoxDAO().Listar(apenasAtivo);
        }

        public List<Pontuacao9Box> Listar(bool apenasAtivo, int idCargo, int idColaborador)
        {
            return new Pontuacao9BoxDAO().Listar(apenasAtivo, idCargo, idColaborador);
        }

        public Pontuacao9Box Consultar(int id)
        {
            return new Pontuacao9BoxDAO().Consultar(id);
        }

        public Pontuacao9Box Consultar(string referencia, int IdCargo, int IdColaborador)
        {
            return new Pontuacao9BoxDAO().Consultar(referencia, IdCargo, IdColaborador);
        }

        public bool Gravar(Pontuacao9Box _pontuacao)
        {
            return new Pontuacao9BoxDAO().Gravar(_pontuacao);
        }

        public bool Excluir(int codigo)
        {
            return new Pontuacao9BoxDAO().Excluir(codigo);
        }
    }
}
