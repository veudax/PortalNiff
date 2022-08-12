using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Endividamento
    {
        public class Parametros
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public decimal CodigoFornecedor { get; set; }
            public string CodigoTipoDocumento { get; set; }
            public string NomeFantasia { get; set; }
            public string NomeEmpresa { get; set; }
            public string Modalidade { get; set; }
            public string CodigoDespesa { get; set; }
            public string TipoDocto { get; set; }
            public string TipoDespesa { get; set; }
            public int Plano { get; set; }

            public int CodigoContaJurosDebito { get; set; }
            public int CodigoContaJurosCredito { get; set; }
            public int CodigoContaVariacaoDebito { get; set; }
            public int CodigoContaVariacaoCredito { get; set; }
            public int CodigoContaCurtoPrazo { get; set; }
            public int CodigoContaLongoPrazo { get; set; }

            public int CodigoContaCurtoPrevisto { get; set; }
            public int CodigoContaLongoPrevisto { get; set; }

            public int? CustoJuros { get; set; }
            public int? CustoVariacao { get; set; }
            public int? CustoJurosConciliacao { get; set; }
            public int? CustoPrevsto { get; set; }

            public string ContaJurosDebito { get; set; }
            public string ContaJurosCredito { get; set; }
            public string ContaVariacaoDebito { get; set; }
            public string ContaVariacaoCredito { get; set; }
            public string ContaCurtoPrazo { get; set; }
            public string ContaLongoPrazo { get; set; }

            public string ContaCurtoPrevisto { get; set; }
            public string ContaLongoPrevisto { get; set; }

            public string HistoricoJuros { get; set; }
            public string HistoricoVariacao { get; set; }
            public string HistoricoJurosConciliacao { get; set; }
            public string HistoricoPrevisto { get; set; }
            public string Lote { get; set; }

            public int IdUsuario { get; set; }
            public string NomeUsuario { get; set; }
            public string Email { get; set; }

            public bool Existe { get; set; }
        }

        public class Valores
        {
            public int Id { get; set; }
            public int IdEndividamento { get; set; }
            public int IdEmpresa { get; set; }
            public decimal CodigoInternoCPG { get; set; }
            public decimal CodigoFornecedor { get; set; }
            public string Referencia { get; set; }
            public string Tipo { get; set; }
            public string Contrato { get; set; }
            public string CodigoTipoDocumento { get; set; }
            public string Modalidade { get; set; }
            public decimal Previsto { get; set; }
            public decimal PrevistoCPG { get; set; }
            public decimal Realizado { get; set; }
            public decimal Variacao { get; set; }
            public decimal Juros { get; set; }
            public string Documento { get; set; }
            public string Fornecedor { get; set; }
            public DateTime Vencimento { get; set; }
            public DateTime Pagamento { get; set; }
            public string Pagamentos { get; set; }
            public bool Encerrado { get; set; }
            public bool Existe { get; set; }
            public decimal CPGCurto { get; set; }
            public decimal CPGLongo { get; set; }
            public decimal CTBCurto { get; set; }
            public decimal CTBLongo { get; set; }
            public decimal VariacaoCurto { get; set; }
            public decimal VariacaoJurosCurto { get; set; }
            public decimal VariacaoLongo { get; set; }
            public decimal VariacaoJurosLongo { get; set; }
            public decimal CPGJuros { get; set; }
            public decimal CPGPrevisto { get; set; }
            public decimal CPGJurosCurto { get; set; }
            public decimal CPGJurosLongo { get; set; }
            public decimal CTBJurosCurto { get; set; }
            public decimal CTBJurosLongo { get; set; }
            public bool TemContaJuros { get; set; }
            public bool TemContaVariacao { get; set; }
            public bool TemContaJurosPrazo { get; set; }
            public bool TemContaPrevistoPrazo { get; set; }
            public decimal RealizadoAtual { get; set; }
            public bool RealizadoAlterado { get; set; }
            public bool Excluir { get; set; }
            public bool Cancelada { get; set; }
            public bool ExcluidoNoCPG { get; set; }
        }

        public class Selic
        {
            public int Id { get; set; }
            public int Referencia { get; set; }
            public int Ano { get; set; }
            public string MesAno { get; set; }
            public decimal Valor { get; set; }
            public decimal ValorUFG { get; set; }
            public bool Existe { get; set; }
        }

        public class Contrato
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public decimal CodigoFornecedor { get; set; }
            public string Modalidade { get; set; }
            public string NomeFantasia { get; set; }
            public string NumeroContrato { get; set; }
            public string Pedido { get; set; }
            public decimal Valor { get; set; }
            public decimal Juros { get; set; }
            public decimal Multa { get; set; }
            public decimal Consolidado { get; set; }
            public int Quantidade { get; set; }
            public int Dia { get; set; }
            public DateTime Vencimento { get; set; }
            public decimal PercentualJuros { get; set; }
            public bool AplicarSelic { get; set; }
            public bool AplicarJurosDiferenciado { get; set; }
            public bool AplicarParcelasDiferenciado { get; set; }
            public decimal PercentualJurosDiferenciado { get; set; }
            public decimal PercentualMultaDiferenciada { get; set; }
            public decimal PercentualSelicDiferenciada { get; set; }
            public decimal ParcelaMinima { get; set; }
            public decimal PercentualAVista { get; set; }
            public decimal ValorAdesao { get; set; }
            public decimal QtdeParcelasAdesao { get; set; }
            public decimal Honorarios { get; set; }
            public decimal Correcao { get; set; }
            public decimal Reducao { get; set; }
            public decimal Custas { get; set; }
            public bool AplicarUFG { get; set; }
            public bool ZerarParcelas { get; set; }
            public int ZerarParcelaApartirDe { get; set; }
            public bool Existe { get; set; }
        }

        public class Parcelamento
        {
            public int Id { get; set; }
            public int IdContrato { get; set; }
            public int Parcela { get; set; }
            public DateTime Vencimento { get; set; }
            public DateTime Vencimento2 { get; set; } // ultimo dia do mes
            public decimal ValorParcelado { get; set; }
            public decimal JurosParcelado { get; set; }
            public decimal MultaParcelada { get; set; }
            public decimal PercentualJuros { get; set; }
            public decimal Selic { get; set; }
            public decimal Juros { get; set; }
            public decimal JurosEMulta { get; set; }
            public decimal ValorConsolidado { get; set; }
            public decimal Encargos { get; set; }
            public decimal ValorPagar { get; set; }
            public decimal SaldoDevedor { get; set; }
            public decimal SaldoDevedorAtual { get; set; }
            public decimal SaldoCurto { get; set; }
            public decimal SaldoLongo { get; set; }
            public decimal JurosMes { get; set; }
            public decimal SaldoCTBCurto { get; set; }
            public decimal SaldoCTBLongo { get; set; }
            public decimal VariacaoCurto { get; set; }
            public decimal VariacaoLongo { get; set; }

            // quando aplica juros/multa diferenciados
            public decimal ValorPrincipalAtualizado { get; set; }
            public decimal PercentualJurosDiferenciado { get; set; }
            public decimal JurosDiferenciado { get; set; }
            public decimal PercentualMultaDiferenciada { get; set; }
            public decimal MultaDiferenciada { get; set; }
            public decimal PercentualSelicDiferenciada { get; set; }
            public bool Atualizada { get; set; }
            public bool Diferenciada { get; set; }
            public string Modalidade { get; set; }
            public string Fornecedor { get; set; }
            public string Contrato { get; set; }

            public decimal CTBJurosDebito { get; set; }
            public decimal CTBJurosCredito { get; set; }
            public decimal CTBCurto { get; set; }
            public decimal CTBLongo { get; set; }
            public decimal ValorTransferencia { get; set; }
            public decimal CodigoFornecedor { get; set; }
            public bool TemContaCurtoLongo { get; set; }
            public bool TemContaJuros { get; set; }
            public decimal UFG { get; set; }
            public decimal ParcelaEmUFG { get; set; }
            public decimal Honorarios { get; set; }
            public decimal Correcao { get; set; }
            public decimal Reducao { get; set; }
            public decimal Custas { get; set; }
            public bool Existe { get; set; }
        }

        public class ResumoParcelamento
        {
            public string Modalidade { get; set; }
            public string Fornecedor { get; set; }
            public string Ano { get; set; }
            public string MesAno { get; set; }
            public int Mes { get; set; }
            public decimal Valor { get; set; }
        }

        public class Arquivo
        {
            public int Id { get; set; }
            public int IdContrato { get; set; }
            public Byte[] Imagem { get; set; }
            public string NomeArquivo { get; set; }
            public bool Existe { get; set; }
        }

        public class Conciliado
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public decimal CodigoFornecedor { get; set; }
            public string Referencia { get; set; }
            public string Tipo { get; set; }
            public string Modalidade { get; set; }
            public decimal Previsto { get; set; }
            public decimal Realizado { get; set; }
            public decimal Juros { get; set; }
            public decimal PrevistoCPGCurto { get; set; }
            public decimal PrevistoCPGLongo { get; set; }
            public decimal PrevistoCTBCurto { get; set; }
            public decimal PrevistoCTBLongo { get; set; }
            public decimal JurosCPGCurto { get; set; }
            public decimal JurosCPGLongo { get; set; }
            public decimal JurosCTBCurto { get; set; }
            public decimal JurosCTBLongo { get; set; }
            public decimal JurosConciliado { get; set; }
            public decimal PrevistoConciliado { get; set; }
            public bool Existe { get; set; }
            public string Fornecedor { get; set; }
        }
    }

}

