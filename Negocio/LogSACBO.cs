using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LogSACBO
    {
        public bool Gravar(LogSAC log)
        {
            return new LogSACDAO().Gravar(log);
        }
    }
}
