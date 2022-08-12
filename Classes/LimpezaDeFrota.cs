using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_LimpezaFrota*/
    public class LimpezaDeFrota
    {
        public int IdLimpeza { get; set; }
        public int IdEmpresa { get; set; }
        public DateTime data { get; set; }
        public int CodigoInternoLinha { get; set; } // Cóigo do Globus
        public int CodigoVeiculo { get; set; } // Código do Globus
        public int IdUsuario { get; set; }
    }
}
