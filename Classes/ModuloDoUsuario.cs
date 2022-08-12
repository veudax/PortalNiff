using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_ModAutoUsuario */ 
    public class ModuloDoUsuario : Modulo
    {
        public int IdUsuario { get; set; }
        public bool ModuloAutoriza { get; set; }
        public string DescricaoCategoria { get; set; }
    }
}
