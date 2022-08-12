using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class BancoDeHoras // 
    {
        public int IdColaborador { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public DateTime Data { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }
        public DateTime SaidaAlmoco { get; set; }
        public DateTime EntradaAlmoco { get; set; }
        public string NomeColaborador { get; set; }
        public string TotalExtras { get; set; }
        public string TotalIncompletas { get; set; }
        public string TotalLiquido { get; set; }
        public string Tipo { get; set; } // Excedente / Faltante
        public string Periodo { get; set; }
        public bool Existe;
    }
}
