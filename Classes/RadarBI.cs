using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class RadarBI
    {
        public string Grupo { get; set; }
        public string EmpresaFilial { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public decimal Percentual { get; set; }
        public int Ordem { get; set; }
        public DateTime DataAlterado { get; set; }
        public decimal ValorAnterior { get; set; }
        public decimal PercentualAnterior { get; set; }
        public string Tipo { get; set; }
        public bool Existe { get; set; }
    }
}
