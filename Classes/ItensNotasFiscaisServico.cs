using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ItensNotasFiscaisServico
    {
        public decimal CodigoInternoNotaFiscal { get; set; }
        public string NumeroNotaFiscal { get; set; }
        public string Serie { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal AliquotaPIS { get; set; }
        public decimal ValorPIS { get; set; }
        public decimal AliquotaCofins { get; set; }
        public decimal ValorCofins { get; set; }
        public decimal AliquotaINSS { get; set; }
        public decimal ValorINSS { get; set; }
        public decimal AliquotaISS { get; set; }
        public decimal ValorISS { get; set; }
        public decimal AliquotaIR { get; set; }
        public decimal ValorIR { get; set; }
        public decimal AliquotaCSLL { get; set; }
        public decimal ValorCSLL { get; set; }
        public string CodigoTipoDeDespesa { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public string SituacaoTributaria { get; set; }
        public int OperacaoFiscal { get; set; }
        public int CFOP { get; set; }
        public int CodContaContabil { get; set; }
        public string DescricaoProduto { get; set; }
        public string Material { get; set; }
        public string CodigoServico { get; set; }
        public string CodigoServicoXml { get; set; }
        public string CodCusto { get; set; }
        public int IdItensISS { get; set; }

    }
}
