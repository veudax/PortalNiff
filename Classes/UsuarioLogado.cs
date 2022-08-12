using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_UsuarioLogado */
    public class UsuarioLogado : Usuario
    {
        public DateTime DataLogado { get; set; }
        public Publicas.StatusUsuario Status { get; set; }
        public string Empresa { get; set; }
    }
}
