using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_EmpAutoUsuario*/
    public class EmpresaDoUsuario : Empresa
    {
        public int IdUsuario { get; set; }
        public bool EmpresaAutoriza { get; set; }
    }
}
