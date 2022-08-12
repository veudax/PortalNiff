using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ItensAvaliacaoMetas : Metas // tabela Niff_Ads_ItensAvaliacaoMetas
    {
        public int IdMetas { get; set; }
        public int IdAvaliacao { get; set; }
        public decimal Peso { get; set; }
        public decimal ValorEsperado { get; set; }
        public decimal Realizado { get; set; }
        public decimal Eficiencia { get; set; }
        public decimal Resultado { get; set; }
        public decimal EficienciaPonderada { get; set; }
        public decimal ResultadoPonderado { get; set; }
        public string DescricaoPerspectiva { get; set; }
        public string Referencia { get; set; }
        public int OrdemPerpectiva { get; set; }
        public DateTime DataCorteFinanceiro { get; set; }
        public DateTime DataCorteOperacional { get; set; }

    }
}
