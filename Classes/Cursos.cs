using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes 
{
    public class Cursos // Tabela NIFF_Ads_Cursos
    {
        public int Id { get; set; }
        public int IdColaborador { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Duracao { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public bool Obrigatorio { get; set; }
        public string Opniao { get; set; }
        public bool Existe { get; set; }
    }
}
