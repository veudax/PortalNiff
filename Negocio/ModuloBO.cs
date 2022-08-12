using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ModuloBO
    {
        public List<Modulo> Listar(int idCategoria = 0, bool somenteAtivos = false)
        {
            return new ModuloDAO().Listar(idCategoria, somenteAtivos);
        }

        public Modulo Consultar(int codigo)
        {
            return new ModuloDAO().Consulta(codigo);
        }

        public bool Gravar(Modulo modulo)
        {
            return new ModuloDAO().Grava(modulo);
        }

        public bool Excluir(Modulo modulo)
        {
            return new ModuloDAO().Exclui(modulo);
        }

        public bool ExcluirTodosOsModulosDaCategoria(int codigo)
        {
            return new ModuloDAO().ExcluiTodosOsModulosDaCategoria(codigo);
        }

        public int Proximo()
        {
            return new ModuloDAO().Proximo();
        }
    }
}
