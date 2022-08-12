using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VisoesBIBO
    {
        public List<VisoesBI> Listar(bool ApenasOsAtivos)
        {
            return new VisoesBIDAO().Listar(ApenasOsAtivos);
        }

        public List<DetalheVisoes> Listar(int Id)
        {
            return new VisoesBIDAO().Listar(Id);
        }

        public VisoesBI Consultar(int id)
        {
            return new VisoesBIDAO().Consultar(id);
        }

        public bool Gravar(VisoesBI times, List<DetalheVisoes> _lista)
        {
            return new VisoesBIDAO().Grava(times, _lista);
        }

        public bool Excluir(int id)
        {
            return new VisoesBIDAO().Exclui(id);
        }

        public int Proximo()
        {
            return new VisoesBIDAO().Proximo();
        }
    }
}
