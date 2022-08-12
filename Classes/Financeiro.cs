using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Financeiro
    {
        public class Colunas
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Tipo { get; set; }
            public string Transferencia { get; set; }
            public bool Ativo { get; set; }
            public string TipoOperacao { get; set; }
            public string Origem { get; set; }
            public bool Existe { get; set; }
            public bool Marcado { get; set; }
        }

        public class Bancos
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public decimal SaldoInicial { get; set; }
            public int CodigoBanco { get; set; }// globus
            public int CodigoAgencia { get; set; } // Globus
            public string CodigoConta { get; set; } // Globus

            public int CodigoBancoCartoes { get; set; }// globus
            public int CodigoAgenciaCartoes { get; set; } // Globus
            public string CodigoContaCartoes { get; set; } // Globus

            public bool Ativo { get; set; }
            public bool Consolidar { get; set; }
            public int IdBancoConsolidado { get; set; }
            public int IdEmpresaConsolidado { get; set; }
            public string CodigoEmpresaGlobus { get; set; }

            public bool Existe { get; set; }
        }

        public class ColunasDoBanco
        {
            public int Id { get; set; }
            public int IdBanco { get; set; }
            public int IdColuna { get; set; }
            public string Nome { get; set; }
            public bool Ativo { get; set; }
            public bool Existe { get; set; }
            public string Tipo { get; set; }

            public int IdAssociado { get; set; }
            public bool Selecionado { get; set; }
            public string TipoCodigo { get; set; }
            public string TipoNome { get; set; }
            public bool TipoExiste { get; set; }
        }

        public class DespesasReceitasDasColunasDoBanco
        {
            public int Id { get; set; }
            public int IdColunaBanco { get; set; }
            public bool Selecionado { get; set; }
            public string CodigoTipoDespesa { get; set; }
            public string CodigoTipoReceita { get; set; }
            public string NomeDespesa { get; set; }
            public string NomeReceita { get; set; }
            public bool Existe { get; set; }
        }

        public class DespesaReceitaGlobus
        {
            public string Codigo { get; set; }
            public string Descricao { get; set; }
            public string Classificador { get; set; }
            public bool AceitaLancamento { get; set; }
            public bool Existe { get; set; }
        }

        public class ContasContabeisDespesasReceitaGlobus
        {
            public string Codigo { get; set; }
            public int Plano { get; set; }
            public int ContaContabil { get; set; }
            public int ContaContabilFornecedor { get; set; }
            public int ContaContabilPis { get; set; }
            public int ContaContabilCofins { get; set; }
            public int? CentroCustoPis { get; set; }
            public int? CentroCustoCofins { get; set; }
            public int? CentroCusto { get; set; }
            public bool Existe { get; set; }
            public string NomeConta { get; set; }
        }

        public class Variaveis
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int IdColuna { get; set; }
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public string CalculoPor { get; set; }
            public int Quantidade { get; set; }
            public bool FeriadoPorAno { get; set; }
            public bool EmendaSabado { get; set; }
            public bool FeriaDinheiro { get; set; }
            public bool Reduzir { get; set; }
            public bool Aumentar { get; set; }
            public decimal PercentualReduzir { get; set; }
            public decimal PercentualAumentar { get; set; }
            public bool Ativo { get; set; }
            public bool CalcularFinaisDeSemana { get; set; }
            public bool Existe { get; set; }
        }

        public class Demonstrativo
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int IdBanco { get; set; }
            public string Referencia { get; set; }
            public decimal SaldoInicial { get; set; }
            public decimal SaldoFinal { get; set; }
            public bool Existe { get; set; }
        }

        public class ColunasDemonstrativo
        {
            public int Id { get; set; }
            public int IdDemonstrativo { get; set; }
            public int IdColuna { get; set; }
            public DateTime Data { get; set; }
            public decimal Previsto { get; set; }
            public decimal RealizadoBCO { get; set; }
            public decimal Realizado { get; set; }
            public string Percentual { get; set; }
            public string Auxiliar { get; set; }
            public string Docto { get; set; }
            public string Serie { get; set; }
            public string Status { get; set; }
            public string Razao { get; set; }
            public decimal IdDocto { get; set; }
            public bool Existe { get; set; }
            public DateTime VencimentoAnterior { get; set; }
        }

        public class HistoricoDemonstrativo
        {
            public int Id { get; set; }
            public int IdColunaDemonstrativo { get; set; }
            public int IdUsuario { get; set; }
            public DateTime Data { get; set; }
            public DateTime DataAlteracao { get; set; }
            public decimal Previsto { get; set; }
            public decimal Realizado { get; set; }
            public decimal RealizadoBCO { get; set; }
            public string MotivoPrevisto { get; set; }
            public string MotivoRealizado { get; set; }
            public string NomeUsuario { get; set; }
            public bool Existe { get; set; }
        }

        public class BancosGlobus
        {
            public int Codigo { get; set; }
            public int Numero { get; set; }
            public string Nome { get; set; }
            public bool Existe { get; set; }
        }

        public class AgenciaGlobus
        {
            public int Banco { get; set; }
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public bool Existe { get; set; }
        }

        public class ContaGlobus
        {
            public int IdEmpresa { get; set; }
            public int Banco { get; set; }
            public int Agencia { get; set; }
            public string Conta { get; set; }
            public string Nome { get; set; }
            public bool Ativa { get; set; }
            public bool Existe { get; set; }
        }

        public class Resumo
        {
            public decimal SaldoInicial { get; set; }
            public decimal SaldoFinal { get; set; }
            public decimal AcumuladoMes { get; set; }
            public int IdEmpresa { get; set; }
            public int Ordem { get; set; }
            public string Empresa { get; set; }
            public string Descricao { get; set; }

            public decimal Valor01 { get; set; }
            public decimal Valor02 { get; set; }
            public decimal Valor03 { get; set; }
            public decimal Valor04 { get; set; }
            public decimal Valor05 { get; set; }
            public decimal Valor06 { get; set; }
            public decimal Valor07 { get; set; }
            public decimal Valor08 { get; set; }
            public decimal Valor09 { get; set; }
            public decimal Valor10 { get; set; }
            public decimal Valor11 { get; set; }
            public decimal Valor12 { get; set; }
            public decimal Valor13 { get; set; }
            public decimal Valor14 { get; set; }
            public decimal Valor15 { get; set; }
            public decimal Valor16 { get; set; }
            public decimal Valor17 { get; set; }
            public decimal Valor18 { get; set; }
            public decimal Valor19 { get; set; }
            public decimal Valor20 { get; set; }
            public decimal Valor21 { get; set; }
            public decimal Valor22 { get; set; }
            public decimal Valor23 { get; set; }
            public decimal Valor24 { get; set; }
            public decimal Valor25 { get; set; }
            public decimal Valor26 { get; set; }
            public decimal Valor27 { get; set; }
            public decimal Valor28 { get; set; }
            public decimal Valor29 { get; set; }
            public decimal Valor30 { get; set; }
            public decimal Valor31 { get; set; }
        }
    }
}
