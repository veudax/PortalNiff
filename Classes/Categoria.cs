using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_Categorias*/
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool PossuiModulos { get; set; }
        public bool Existe { get; set; }
    }
}
