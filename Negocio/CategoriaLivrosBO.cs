using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaLivrosBO
    {
        public List<CategoriaLivros> Listar()
        {
            return new CategoriaLivrosDAO().Listar();
        }

        public CategoriaLivros Consultar(int codigo)
        {
            return new CategoriaLivrosDAO().Consulta(codigo);
        }

        public bool Gravar(CategoriaLivros categoria)
        {
            return new CategoriaLivrosDAO().Grava(categoria);
        }

        public bool Excluir(int codigo)
        {
            return new CategoriaLivrosDAO().Exclui(codigo);
        }

        public int Proximo()
        {
            return new CategoriaLivrosDAO().Proximo();
        }
    }
}
