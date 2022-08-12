using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Torneio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Minimo { get; set; }
        public int Maximo { get; set; }
        public bool Ativo { get; set; }
        public bool MarioGrandPrix { get; set; }
        public bool MarioBattle { get; set; }
        public bool Arms { get; set; }
        public bool Pinball { get; set; }
        public bool Existe { get; set; }
    }

    public class Participantes
    {
        public int IdUsuario { get; set; }
        public int Classificacao { get; set; }
        public string NomeColaborador { get; set; }
        public string Sexo { get; set; }
        public decimal Total { get; set; }
        public bool Existe { get; set; }
        public List<PartidasDoTorneio> Partidas { get; set; }
    }

    public class PartidasDoTorneio
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdTorneio { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime Data { get; set; }
        public string Torneio { get; set; }
        public string NomePartida { get; set; }
        public decimal Round1 { get; set; }
        public decimal Round2 { get; set; }
        public decimal Round3 { get; set; }
        public decimal Round4 { get; set; }
        public decimal Total { get; set; }
        public bool Existe { get; set; }
    }

    public class AgendaTorneio
    {
        public DateTime Data { get; set; }
        public string Partida { get; set; }
        public string Nome1 {get;set;}
        public string Nome2 { get; set; }
        public string Nome3 { get; set; }
        public string Nome4 { get; set; }
    }
}
