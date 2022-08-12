using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ReservaLivrosBO
    {
        public List<ReservaLivros> Listar()
        {
            return new ReservaLivrosDAO().Listar();
        }

        public ReservaLivros Consultar(int codigo, int colaborador)
        {
            return new ReservaLivrosDAO().Consulta(codigo, colaborador);
        }

        public ReservaLivros Consultar(int codigo)
        {
            return new ReservaLivrosDAO().Consulta(codigo);
        }

        public bool Gravar(ReservaLivros livro)
        {
            return new ReservaLivrosDAO().Grava(livro);
        }

        public bool Excluir(int codigo)
        {
            return new ReservaLivrosDAO().Exclui(codigo);
        }

        public int Proximo()
        {
            return new ReservaLivrosDAO().Proximo();
        }
    }
}
