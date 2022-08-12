using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LogBO
    {
        public bool Gravar(Log log)
        {
            return new LogDAO().Gravar(log);
        }
    }
}
