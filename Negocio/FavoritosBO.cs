using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FavoritosBO
    {
        public List<Favoritos> Listar()
        {
            return new FavoritosDAO().Listar();
        }

        public bool Gravar(Favoritos times)
        {
            return new FavoritosDAO().Grava(times);
        }

        public bool Excluir(int id)
        {
            return new FavoritosDAO().Exclui(id);
        }
    }
}
