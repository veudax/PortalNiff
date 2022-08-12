using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /*Tabela NIFF_CHM_LOCALIZACAO*/
    public class Localizacao
    {
        public int IdLocalizacao { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdEmpresa { get; set; }
    }
}
