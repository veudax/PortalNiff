using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ParametrosBO
    {
        public Parametro Consultar()
        {
            return new ParametrosDAO().Consulta();
        }

        public bool Gravar(Parametro parametro)
        {
            return new ParametrosDAO().Grava(parametro);
        }

        public bool Excluir(Parametro parametro)
        {
            return new ParametrosDAO().Exclui(parametro);
        }
    }
}
