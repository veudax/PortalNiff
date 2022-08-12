using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VaraBO
    {
        public List<Vara> Listar(bool apenasAtivos = false)
        {
            return new VaraDAO().Listar(apenasAtivos);
        }

        public Vara Consultar(int codigo)
        {
            return new VaraDAO().Consulta(codigo);
        }

        public int Proximo()
        {
            return new VaraDAO().Proximo();
        }

        public bool Gravar(Vara tipo)
        {
            return new VaraDAO().Gravar(tipo);
        }

        public bool Excluir(Vara tipo)
        {
            return new VaraDAO().Excluir(tipo);
        }
    }
}
