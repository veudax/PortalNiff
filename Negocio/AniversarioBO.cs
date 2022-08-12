using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AniversarioBO
    {
        public Aniversarios Consulta()
        {
            return new AniversarioDAO().Consulta();
        }

        public bool Gravar(Aniversarios aniversario)
        {
            return new AniversarioDAO().Grava(aniversario);
        }
    }
}
