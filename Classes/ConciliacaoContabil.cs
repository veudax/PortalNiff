using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ConciliacaoContabil
    {
        public class Ativo
        {
            public class ItemAtivo
            {
                public int Codigo { get; set; }
                public string Conta { get; set; }
                public string Nome { get; set; }
                public string Empresa { get; set; }
                public int IdEmpresa { get; set; }
                public bool Existe {get;set;}
            }

            public class Parametros
            {
                public int Id { get; set; }
                public int IdEmpresa { get; set; }
                public int Codigo { get; set; }
                public int CodigoAtivo { get; set; }
                public string Grupo { get; set; }
                public string CodigoGrupo { get; set; }
                public string Descricao { get; set; }
                public string NomeAtivo { get; set; }
                public int IdEmpresaAtivo { get; set; }
                public bool Existe { get; set; }
            }

            public class Resumo
            {
                public int Id { get; set; }
                public int IdEmpresa { get; set; }
                public int IdUsuario { get; set; }
                public int Referencia { get; set; }

                public string Grupo { get; set; }
                public string ContaItem { get; set; }
                public string DescricaoConta { get; set; }

                public decimal Correcao { get; set; }
                public decimal DepreciacaoAcumulada { get; set; }
                public decimal SaldoATF { get; set; }
                public decimal SaldoCTB { get; set; }

                public decimal Diferenca { get; set; }

                public bool Conferido { get; set; }
                public bool Diferencas { get; set; }
                public string Explicacao { get; set; }
                public bool Existe { get; set; }
            }

            public class DetalheATF
            {
                public string Grupo { get; set; }
                public string Conta { get; set; }
                public string ContaItem { get; set; }
                
                public decimal Aquisicao { get; set; }
                public decimal Correcao { get; set; }
                public decimal Baixa { get; set; }
                public decimal Depreciacao { get; set; }
                public decimal DepreciacaoAcumulada { get; set; }

                public decimal SaldoATF { get; set; }
            }

            public class DetalheCTB
            {
                public string Grupo { get; set; }
                public string Classificador { get; set; }

                public decimal SaldoCTB { get; set; }
                public decimal SaldoIni { get; set; }
                public decimal Debito { get; set; }
                public decimal Credito { get; set; }
            }

            public class DetalheESF
            {
                public string Numero { get; set; }
                public string Serie { get; set; }
                public string Material { get; set; }
                public string Origem { get; set; }
                public string Tipo { get; set; }
                public string TpDocto { get; set; }
                public DateTime Entrada { get; set; }
                public DateTime Emissao { get; set; }
                public int CFOP { get; set; }
                public int Quantidade { get; set; }
                public decimal TotalItem { get; set; }
                public decimal TotalNF { get; set; }

            }
        }

        public class Bancaria
        {
            public class Resumo
            {
                public int Id { get; set; }
                public int IdEmpresa { get; set; }
                public int IdUsuario { get; set; }
                public int Referencia { get; set; }

                public int Banco { get; set; }
                public int Agencia { get; set; }
                public string Conta { get; set; }

                public decimal SaldoInicialBCO { get; set; }
                public decimal DebitoBCO { get; set; }
                public decimal CreditoBCO { get; set; }
                public decimal SaldoFinalBCO { get; set; }
                public decimal SaldoFinalGBCO { get; set; }

                public int ContaContabil { get; set; }
                public string Descricao { get; set; }
                public decimal SaldoInicialCTB { get; set; }
                public decimal DebitoCTB { get; set; }
                public decimal CreditoCTB { get; set; }
                public decimal SaldoFinalCTB { get; set; }
                public decimal Diferença { get; set; }

                public bool Conferido { get; set; }
                public bool Diferencas { get; set; }
                public string Explicacao { get; set; }
                public bool Existe { get; set; }
            }

            public class Detalhe
            {
                public int Banco { get; set; }
                public int Agencia { get; set; }
                public string Conta { get; set; }
                public string DoctoBCO { get; set; }
                public string DoctoCTB { get; set; }
                public DateTime Data { get; set; }
                public DateTime? DataConciliado { get; set; }
                public decimal Valor { get; set; }
                public string Conciliado { get; set; }
                public string Historico { get; set; }
                public string Origem { get; set; }
                public decimal CodMovtoBCO { get; set; }
                public decimal CodLanca { get; set; }
                public int ContaContabil { get; set; }

            }
        }

        public class Fornecedores
        {
            public class FornAssociados
            {
                public int Id { get; set; }

                public int ContaContabil { get; set; }
                public int Plano { get; set; }
                public string Fornecedor { get; set; }
            }

            public class Resumo
            {
                public int Id { get; set; }
                public int IdEmpresa { get; set; }
                public int IdUsuario { get; set; }
                public int Referencia { get; set; }

                public decimal ValorCPG { get; set; }

                public int ContaContabil { get; set; }
                public int Plano { get; set; }
                public string Descricao { get; set; }
                public decimal ValorCTB { get; set; }
                public decimal Diferenca { get; set; }

                public bool Conferido { get; set; }
                public bool Diferencas { get; set; }
                public string Explicacao { get; set; }
                public bool Existe { get; set; }
            }

            public class Detalhe
            {
                public int ContaContabil { get; set; }
                public string Fornecedor { get; set; }
                public string Docto { get; set; }
                public string DoctoCPG { get; set; }
                public string DoctoCTB { get; set; }
                public DateTime? Entrada { get; set; }
                public DateTime? Emissao { get; set; }
                public DateTime? Vencimento { get; set; }
                public DateTime? Pagamento { get; set; }
                public decimal ValorCPG { get; set; }
                public decimal ValorCTB { get; set; }
                public string Observacao { get; set; }
                public string Origem { get; set; }
                public string DoctoBCO { get; set; }
                public string TipoDocto { get; set; }
                public decimal ValorBCO { get; set; }
                public decimal CodLanca { get; set; }
                public decimal CodDoctoESF { get; set; }
                public decimal CodDoctoCPG { get; set; }
                public decimal CodLancaBCO { get; set; }
            }
                            
        }

        public class Clientes
        {
            public class CliAssociados
            {
                public int Id { get; set; }

                public int ContaContabil { get; set; }
                public int Plano { get; set; }
                public string Cliente { get; set; }
            }

            public class Resumo
            {
                public int Id { get; set; }
                public int IdEmpresa { get; set; }
                public int IdUsuario { get; set; }
                public int Referencia { get; set; }

                public decimal ValorCRC { get; set; }

                public int ContaContabil { get; set; }
                public int Plano { get; set; }
                public string Descricao { get; set; }
                public decimal ValorCTB { get; set; }
                public decimal Diferenca { get; set; }

                public bool Conferido { get; set; }
                public bool Diferencas { get; set; }
                public string Explicacao { get; set; }
                public bool Existe { get; set; }
            }

            public class Detalhe
            {
                public int ContaContabil { get; set; }
                public string Cliente { get; set; }
                public string Docto { get; set; }
                public string DoctoCRC { get; set; }
                public string DoctoCTB { get; set; }
                public DateTime? Saida { get; set; }
                public DateTime? Emissao { get; set; }
                public DateTime? Vencimento { get; set; }
                public DateTime? Recebimento { get; set; }
                public decimal ValorCRC { get; set; }
                public decimal ValorCTB { get; set; }
                public string Observacao { get; set; }
                public string Origem { get; set; }
                public string DoctoBCO { get; set; }
                public string TipoDocto { get; set; }
                public decimal ValorBCO { get; set; }
                public decimal CodLanca { get; set; }
                public decimal CodDoctoESF { get; set; }
                public decimal CodDoctoCRC { get; set; }
                public decimal CodLancaBCO { get; set; }
            }
        }

        public class Folha
        {

        }

    }
}
