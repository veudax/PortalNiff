using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Lalur
    {
        public class Formulas
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int Codigo { get; set; }
            public bool Ativo { get; set; }
            public bool Totalizador { get; set; }
            public int Ordem { get; set; }
            public string Descricao { get; set; }
            public string Formula { get; set; }
            public bool Existe { get; set; }
            public bool Destacar { get; set; }
        }

        public class ContasDaFormula
        {
            public int Id { get; set; }
            public int IdFormula { get; set; }
            public int NumeroPlano { get; set; }
            public string Plano { get; set; }
            public int CodigoConta { get; set; }
            public string NomeConta { get; set; }
            public bool SelecionadoAnterior { get; set; }
            public bool Marcado { get; set; }
            public bool Existe { get; set; }
            public string Regra { get; set; }
        }

        public class Parametros
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public decimal PercentualCompensacaoNegativa { get; set; }
            public decimal PercentualCSLL { get; set; }
            public decimal PercentualIRPJ { get; set; }
            public decimal ValorParcelaIsenta { get; set; }
            public decimal PercentualAdicionalPagar { get; set; }
            public decimal PercentualPat { get; set; }
            public decimal LimiteIRPJ { get; set; }
            public decimal LimiteCSLL { get; set; }
            public bool Existe { get; set; }
        }

        public class Apuracao
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public string Referencia { get; set; }
            public bool Fechado { get; set; }
            public bool Existe { get; set; }                 
        }

        public class Valores
        {
            public int Id { get; set; }
            public int IdLalur { get; set; }
            public int IdFormula { get; set; }
            public int Ordem { get; set; }
            public string Descricao { get; set; }
            public decimal Valor { get; set; }
            public decimal Percentual { get; set; }
            public bool Existe { get; set; }
            public bool Destacar { get; set; }
            public List<ValoresContas> Contas { get; set; }
        }

        public class ValoresContas
        {
            public int Id { get; set; }
            public int IdLalur { get; set; }
            public int IdLalurValor { get; set; }
            public int IdFormula { get; set; }
            public int Plano { get; set; }
            public int Codigo { get; set; }
            public string NomeConta { get; set; }
            public decimal Valor { get; set; }
            public decimal ValorReal { get; set; }
            public bool Existe { get; set; }
        }
    }
}
