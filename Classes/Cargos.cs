using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Cargos // tabela Niff_ADS_Cargos
    {
        public int Id { get; set; }
        public int IdEscolaridade { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool RequerExperiencia { get; set; }
        public decimal SalarioMinimo { get; set; }
        public decimal SalarioMaximo { get; set; }
        public string TipoDoCargo { get; set; }
        public bool Existe { get; set; } 
    }
}
