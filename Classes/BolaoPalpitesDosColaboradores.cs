using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class BolaoPalpitesDosColaboradores // Niff_bol_Palpites
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataDoPalpite { get; set; }
        public DateTime DataAlteracao { get; set; }
        public DateTime DataLimite { get; set; }
        public int IdColaborador { get; set; }
        public string NomeColaborador { get; set; }
        public int IdJogo { get; set; }
        public int IdTime1 { get; set; }
        public int IdTime2 { get; set; }
        public int Placar1 { get; set; }
        public int Placar2 { get; set; }
        public int PlacarOficial1 { get; set; }
        public int PlacarOficial2 { get; set; }
        public int Penalti1 { get; set; }
        public int Penalti2 { get; set; }
        public string Sigla1 { get; set; }
        public string Sigla2 { get; set; }
        public string Nome1 { get; set; }
        public string Nome2 { get; set; }
        public string Grupo1 { get; set; }
        public string Grupo2 { get; set; }
        public string Localizacao { get; set; }
        public string Fase { get; set; }
        public int Pontuacao { get; set; }
        public Byte[] Bandeira1 { get; set; }
        public Byte[] Bandeira2 { get; set; }
        public bool Existe { get; set; }
        public bool Encerrado { get; set; }
        public int QuantidadeAcertosPlacarExato { get; set; }
        public int QuantidadeAcertosGanhadorEPlacar { get; set; }
        public int QuantidadeAcertosGanhador { get; set; }
        public int QuantidadeAcertosEmpates { get; set; }
        public int QuantidadeAcertos1Placar { get; set; }
        public bool AcetouCampeao { get; set; }
        public bool AcetouViceCampeao { get; set; }
        public bool Acetou3Lugar { get; set; }
        public int Classificacao { get; set; }
        public string Empritado { get; set; }
        public decimal ValorPremio { get; set; }

    }
}
