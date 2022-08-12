using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class PontuacaoFatorEmpresa // Niff_Ads_FatorEmpresa
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public decimal Fator { get; set; }
        public string Nome { get; set; }
        public bool Existe { get; set; }
    }
}
