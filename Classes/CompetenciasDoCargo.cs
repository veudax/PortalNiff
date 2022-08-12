using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class CompetenciasDoCargo : Competencias // tabela Niff_ADS_CompetenciasDoCargo
    {
        public int IdComp { get; set; }
        public int IdCargo { get; set; }
        public bool Marcado { get; set; }
        public decimal Total { get; set; }
        public decimal TotalBase100 { get; set; }
    }
}
