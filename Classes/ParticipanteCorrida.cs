using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ParticipanteCorrida // NIFF_CHM_Participantes
    {
        public int Id { get; set; }
        public int IdDistancia { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioGrupo { get; set; }
        public string Nome { get; set; }
        public decimal CPF { get; set; }
        public decimal TempoBruto { get; set; }
        public decimal TempoLiquido { get; set; }
        public int KM { get; set; }
        public int Ritmo { get; set; }
        public int Classificacao { get; set; }
        public int ClassificacaoGeral { get; set; }
        public bool InscricaoPaga { get; set; }
        public bool InscricaoEmGrupo { get; set; }
        public bool Existe { get; set; }
        public string TempoBrutoFormatado { get; set; }
        public string TempoLiquidoFormatado { get; set; }
        public string RitmoFormatado { get; set; }
        public string Sexo { get; set; }
        public DateTime DataCorrida { get; set; }
        public bool Visualizado { get; set; }
        public decimal ValorInscrito { get; set; }
        public string CPFFormatado { get; set; }
        public int NumeroDePeito { get; set; }
    }
}
