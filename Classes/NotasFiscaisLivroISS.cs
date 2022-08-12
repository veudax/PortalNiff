using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class NotasFiscaisLivroISS
    {
        public int CodigoInterno { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoFilial { get; set; }
        public int CodigoFornecedor { get; set; }
        public string DocumentoInicial { get; set; }
        public string DocumentoFinal { get; set; }
        public string Serie { get; set; }
        public string CodigoTipoDeDocumento { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorDoServico { get; set; }
        public decimal BaseISS { get; set; }
        public decimal AliquotaISS { get; set; }
        public decimal ValorISS { get; set; }
        public bool IRRetido { get; set; }
        public decimal BaseIR { get; set; }
        public decimal AliquotaIR { get; set; }
        public decimal ValorIR { get; set; }
        public string Observacoes { get; set; }
        public bool Cancelado { get; set; }
        public bool Conferido { get; set; }
        public string Sistema { get; set; }
        public string Usuario { get; set; }
        public string CodigoServico { get; set; }
        public int CodigoInternoNotaFiscal { get; set; }
        public int CodigoNaturezaPrestacao { get; set; }
        public bool INSSRetido { get; set; }
        public decimal BaseINSS { get; set; }
        public decimal AliquotaINSS { get; set; }
        public decimal ValorINSS { get; set; }
        public bool PISRetido { get; set; }
        public decimal BasePIS { get; set; }
        public decimal AliquotaPIS { get; set; }
        public decimal ValorPIS { get; set; }
        public bool CofinsRetido { get; set; }
        public decimal BaseCofins { get; set; }
        public decimal AliquotaCofins { get; set; }
        public decimal ValorCofins { get; set; }
        public bool CSLLRetido { get; set; }
        public decimal BaseCSLL { get; set; }
        public decimal AliquotaCSLL { get; set; }
        public decimal ValorCSLL { get; set; }
    }
}
