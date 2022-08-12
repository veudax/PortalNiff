using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class DescontoBeneficios : FuncionariosGlobus
    {
        public string Empresa { get; set; }
        public DateTime InicioFerias { get; set; }
        public DateTime FimFerias { get; set; }
        public DateTime InicioAquisicao { get; set; }
        public DateTime FimAquisicao { get; set; }
        public int QuantidadeFaltasJustificadas { get; set; }
        public int QuantidadeFaltasInJustificadas { get; set; }
        public int QuantidadeFaltasDescontadasJustificadas { get; set; }
        public int QuantidadeFaltasDescontadasInjustificadas { get; set; }
        public decimal ValorADescontarFaltasInjustificadas { get; set; }
        public decimal ValorADescontarFaltasJustificadas { get; set; }
        public decimal Total { get; set; }
    }
}
