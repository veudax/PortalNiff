using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TelaBO
    {
        public List<Tela> Listar(int idModulo = 0, bool somenteAtivos = false)
        {
            return new TelaDAO().Listar(idModulo, somenteAtivos);
        }

        public Tela Consultar(int codigo)
        {
            return new TelaDAO().Consulta(codigo);
        }

        public bool Gravar(Tela tela)
        {
            return new TelaDAO().Grava(tela);
        }

        public bool Excluir(Tela tela)
        {
            return new TelaDAO().Exclui(tela);
        }

        public int Proximo()
        {
            return new TelaDAO().Proximo();
        }
    }
}
