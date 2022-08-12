using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ItensParametrosArquivei // NIFF_FIS_ItensParamArquivei
    {
        public int Id { get; set; }
        public int IdParametro { get; set; }
        public bool ValidarCampo { get; set; }
        public bool ExibirCampo { get; set; }
        public string NomeCampo { get; set; }
        public string Tipo { get; set; }
        public bool Existe { get; set; }
    }
}
