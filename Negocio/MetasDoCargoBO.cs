using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MetasDoCargoBO
    {
        public List<MetasDoCargo> Listar(int cargo, string tipo)
        {
            return new MetasDoCargoDAO().Listar(cargo, tipo);
        }

        public bool Gravar(List<MetasDoCargo> _lista)
        {
            return new MetasDoCargoDAO().Gravar(_lista);
        }

        public bool Excluir(int cargo)
        {
            return new MetasDoCargoDAO().Excluir(cargo);
        }
    }
}
