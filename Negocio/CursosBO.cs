using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CursosBO
    {
        public List<Cursos> Listar(int idColaborador)
        {
            return new CursosDAO().Listar(idColaborador);
        }

        public bool Gravar(List<Cursos> _lista)
        {
            return new CursosDAO().Gravar(_lista);
        }

        public bool Excluir(int usuario)
        {
            return new CursosDAO().Excluir(usuario);
        }
    }
}
