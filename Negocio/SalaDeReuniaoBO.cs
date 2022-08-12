using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SalaDeReuniaoBO
    {
        public List<SalaDeReuniao> Listar()
        {
            return new SalaDeReuniaoDAO().Listar();
        }

        public SalaDeReuniao Consultar(int codigo)
        {            
            return new SalaDeReuniaoDAO().Consultar(codigo);
        }

        public bool Gravar(SalaDeReuniao tipo)
        {
            return new SalaDeReuniaoDAO().Grava(tipo);
        }

        public bool Excluir(SalaDeReuniao tipo)
        {
            return new SalaDeReuniaoDAO().Exclui(tipo);
        }

        public int Proximo()
        {
            return new SalaDeReuniaoDAO().Proximo();
        }
    }
}
