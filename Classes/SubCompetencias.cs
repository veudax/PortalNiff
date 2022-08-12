using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class SubCompetencias //tabela NIFF_ADS_SubCompetencias
    {
        public int Id { get; set; }
        public int IdCompetencia { get; set; }
        public string Descricao { get; set; }
        public decimal Pontuacao { get; set; }
        public bool Ativo { get; set; }
        public bool ExibeNaAutoAvaliacao { get; set; }
        public bool ExibeNaAvaliacaoRH { get; set; }
        public bool ExibeNaAvaliacaoGestor { get; set; }
        public bool Existe { get; set; }

    }
}
