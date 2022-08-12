using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Pontuacao9Box // Niff_Ads_PontuacaoNineBox
    {
        public int Id { get; set; }
        public int Referencia { get; set; }
        public int IdCargo { get; set; }
        public int IdColaborador { get; set; }
        public bool Ativo { get; set; }
        public decimal Descontar { get; set; }
        public decimal PontoDominancia { get; set; }
        public decimal ToleranciaDominancia { get; set; }
        public decimal PontoExtroversao { get; set; }
        public decimal ToleranciaExtroversao { get; set; }
        public decimal PontoPaciencia { get; set; }
        public decimal ToleranciaPaciencia { get; set; }
        public decimal PontoFormalidade { get; set; }
        public decimal ToleranciaFormalidade { get; set; }
        public bool Existe { get; set; }
        public string ReferenciaFormatada { get; set; }
    }
}
