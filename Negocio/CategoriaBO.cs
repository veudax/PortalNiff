using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaBO
    {
        public List<Categoria> Listar(bool somenteAtivo = false)
        {
            return new CategoriaDAO().Listar(somenteAtivo);
        }

        public Categoria Consultar(int codigo)
        {
            return new CategoriaDAO().Consulta(codigo);
        }

        public bool Gravar(Categoria categoria)
        {
            return new CategoriaDAO().Grava(categoria);
        }

        public bool Excluir(Categoria categoria)
        {
            return new CategoriaDAO().Exclui(categoria);
        }

        public int Proximo()
        {
            return new CategoriaDAO().Proximo();
        }
    }
}
