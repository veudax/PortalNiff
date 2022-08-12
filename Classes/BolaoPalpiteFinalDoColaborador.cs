using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class BolaoPalpiteFinalDoColaborador // Niff_bol_PalpiteFinal
    {
        public int Id { get; set; }
        public int IdColaborador { get; set; }
        public int IdTimeCampeao { get; set; }
        public int IdTimeVice { get; set; }
        public int IdTime3Lugar { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Sigla1 { get; set; }
        public string Sigla2 { get; set; }
        public string Sigla3 { get; set; }
        public string Nome1 { get; set; }
        public string Nome2 { get; set; }
        public string Nome3 { get; set; }
        public string NomeColaborador { get; set; }
        public bool Existe { get; set; }
        public int Pontuacao { get; set; }
        public bool AcertouCampeao { get; set; }
        public bool AcertouViceCampeao { get; set; }
        public bool Acertou3Lugar { get; set; }
        public string Empritado { get; set; }
    }
}
