using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EscolaridadeBO
    {
        public List<Escolaridade> Listar(bool apenasAtivos = false)
        {
            return new EscolaridadeDAO().Listar(apenasAtivos);
        }

        public Escolaridade Consultar(int codigo)
        {
            return new EscolaridadeDAO().Consulta(codigo);
        }

        public int Proximo()
        {
            return new EscolaridadeDAO().Proximo();
        }

        public bool Gravar(Escolaridade tipo)
        {
            return new EscolaridadeDAO().Gravar(tipo);
        }

        public bool Excluir(Escolaridade tipo)
        {
            return new EscolaridadeDAO().Excluir(tipo);
        }
    }
}
