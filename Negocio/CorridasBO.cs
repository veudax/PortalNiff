using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CorridasBO
    {
        public List<Corridas> Listar(bool apenasAtivos = false, int usuario = 0)
        {
            return new CorridasDAO().Listar(apenasAtivos, usuario);
        }

        public Corridas Consultar(int codigo)
        {
            return new CorridasDAO().Consulta(codigo);
        }

        public int Proximo()
        {
            return new CorridasDAO().Proximo();
        }

        public bool Gravar(Corridas tipo, List<DistanciaCorrida> _lista)
        {
            return new CorridasDAO().Gravar(tipo, _lista);
        }

        public bool Excluir(Corridas tipo)
        {
            return new CorridasDAO().Excluir(tipo);
        }

        public List<DistanciaCorrida> ListarDistancias(int codigo)
        {
            return new DistanciaCorridaDAO().Listar(codigo);
        }
    }
}
