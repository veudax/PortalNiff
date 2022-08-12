using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class NotasFiscaisEscrituracaoGlobus
    {
        public int Id { get; set; }
        public int IdCodDoctoESF { get; set; }
        public int NumeroNF { get; set; }
        public string Serie { get; set; }
        public string Item { get; set; }
        public string Documento { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime EntradaSaida { get; set; }
        public string StatusNF { get; set; }
        public string ClienteFornecedor { get; set; }
        public int CFOP { get; set; }
        public int OperacaoFiscal { get; set; }
        public string SituacaoTributaria { get; set; }
        public string TipoDeDocumento { get; set; }
        public string DadosAdicionais { get; set; }
        public decimal BaseICMS { get; set; }
        public decimal ValorICMS { get; set; }
        public decimal IsentasICMS { get; set; }
        public decimal AliquotaICMS { get; set; }
        public decimal OutrasICMS { get; set; }
        public decimal TotalNF { get; set; }
        public decimal Frete { get; set; }
        public decimal Seguro { get; set; }
        public decimal Desconto { get; set; }
        public decimal Outros { get; set; }
        public decimal BaseICMSSubstituicao { get; set; }
        public decimal ValorICMSSubstituicao { get; set; }
        public decimal BaseIPI { get; set; }
        public decimal ValorIPI { get; set; }
        public decimal AliquotaIPI { get; set; }
        public decimal IsentaIPI { get; set; }
        public decimal OutrasIPI { get; set; }
        public decimal PIS { get; set; }
        public decimal BasePIS { get; set; }
        public decimal Cofins { get; set; }
        public decimal BaseCofins { get; set; }
        public string ChaveDeAcesso { get; set; }
        public string StatusSefaz { get; set; }
        public string UltimaMensagemSefaz { get; set; }
        public string ReciboSefaz { get; set; }
        public string ProtocoloSefaz { get; set; }
        public string DataSefaz { get; set; }
        public bool Existe { get; set; }
    }
}
