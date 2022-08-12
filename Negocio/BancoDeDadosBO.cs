using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BancoDeDadosBO
    {
        public DateTime DataHoraDoBanco()
        {
            return new BancoDeDadosDAO().DataBanco();
        }
    }
}
