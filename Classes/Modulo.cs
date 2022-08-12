using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_Modulos*/ 
    public class Modulo
    {
        public int IdModulo { get; set; }
        public int IdCategoria { get; set; }
        public string Nome { get; set; }
        public string Fornecedor { get; set; }
        public string Categoria { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
    }
}
