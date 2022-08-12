using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ItensDaAutoAvaliacao  // tabela Niff_Ads_ItensAvaliacao
    {
        public int Id { get; set; }
        public int IdAutoAvaliacao { get; set; }
        public int IdSubCompetencia { get; set; }
        public int IdCompetencia { get; set; }
        public string Descricao { get; set; }
        public string Comentario { get; set; }
        public int Avaliacao { get; set; }
        public bool NaoAtende { get; set; }
        public bool AtendeParcialmente { get; set; }
        public bool AtendePlenamente { get; set; }
        public bool Supera { get; set; }
        public bool Existe { get; set; }
        public decimal Pontuacao { get; set; }
        public decimal TotalSubCompetencia { get; set; }
        public decimal TotalCompetencia { get; set; }
    }
}
