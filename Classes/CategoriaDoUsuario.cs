using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{   /* tabela NIFF_CHM_UsuRespCateg*/

    public class CategoriaDoUsuario : Categoria
    {
        public int IdUsuario { get; set; }
        public bool CategoriaAutoriza { get; set; }
    }
}
