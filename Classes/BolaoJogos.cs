using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class BolaoJogos // Niff_Bol_Jogos
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int IdTime1 {get;set;}
        public int IdTime2 { get; set; }
        public int Placar1 { get; set; }
        public int Placar2 { get; set; }
        public string Sigla1 { get; set; }
        public string Sigla2 { get; set; }
        public string Nome1 { get; set; }
        public string Nome2 { get; set; }
        public DateTime LimitePalpite { get; set; }
        public string Localizacao { get; set; }
        public string Fase { get; set; }
        public bool Encerrado { get; set; }
        public bool Existe { get; set; }
        public byte[] Bandeira1 { get; set; }
        public byte[] Bandeira2 { get; set; }
        public int IdColaborador { get; set; }
        public string Empritado { get; set; }
        public int Penalti1 { get; set; }
        public int Penalti2 { get; set; }
    }
}
