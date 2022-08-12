using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class SIGOM
    {
        public int IdEmpresa { get; set; }
        public string CodigoLinha { get; set; }
        public string Prefixo { get; set; }
        public decimal CatracaInicial { get; set; }
        public decimal CatracaFinal { get; set; }
        public DateTime InicioJornada { get; set; }
        public DateTime InicioJornadaSigom { get; set; }
        public DateTime FimJornadaGlobus { get; set; }
        public DateTime FimJornadaSigom { get; set; }

        public string MotoristaSigom { get; set; }
        public int IdTipoPagtoSigom { get; set; }
        public string TipoPagtoSigom { get; set; }
        public int QuantidadeGirosSigom { get; set; }
        public decimal ValorSigom { get; set; }

        public string MotoristaGlobus { get; set; }
        public string IdTipoPagtoGlobus { get; set; }
        public string TipoPagtoGlobus { get; set; }
        public int QuantidadeGirosGlobus { get; set; }
        public decimal ValorGlobus { get; set; }
        public string GuiaGlobus { get; set; }
        public int CodigoImportacaoGlobus { get; set; }

        public bool DiferencaQuantidade { get; set; }
        public bool DiferencaValor { get; set; }
        public bool Lido { get; set; }
        public string Tipo { get; set; }
        public decimal ValorTarifa { get; set; }
        public bool DiferencaNoVeiculo { get; set; }
        public bool DiferencaNaLinha { get; set; }
    }
}
