using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BolaoNotificacaoBO
    {
        public bool Gravar(BolaoNotificacao times)
        {
            return new BolaoNotificacaoDAO().Grava(times);
        }
    }
}
