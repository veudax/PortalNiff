using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Pontuacao // tabela Niff_Ads_Pontuacao
    {
        public int Id { get; set; }
        public int MesReferencia { get; set; }
        public string MesReferenciaFormatada { get; set; }
        public int OrdemReferencia { get; set; }
        public int Base100 { get; set; }
        public int Piso { get; set; }
        public decimal PesoQualitativa { get; set; }
        public decimal PesoNumerica { get; set; }
        public int NaoAtende { get; set; }
        public int AtendeParcialmente { get; set; }
        public int AtendePlenamente { get; set; }
        public int Supera { get; set; }
        public bool Existe { get; set; } 
    }
}
