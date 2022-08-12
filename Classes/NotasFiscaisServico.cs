using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class NotasFiscaisServico
    {
        public bool Marcado { get; set; }
        public decimal CodigoInternoNotaFiscal { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoFilial { get; set; }
        public string NumeroNotaFiscal { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataImportado { get; set; }
        public string Fornecedor { get; set; }
        public string CNPJFornecedor { get; set; }
        public int CodigoFornecedor { get; set; }
        public string Serie { get; set; }
        public decimal BasePIS { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal AliquotaPIS { get; set; }
        public decimal ValorPIS { get; set; }
        public decimal BaseCofins { get; set; }
        public decimal AliquotaCofins { get; set; }
        public decimal ValorCofins { get; set; }
        public decimal BaseINSS { get; set; }
        public decimal AliquotaINSS { get; set; }
        public decimal ValorINSS { get; set; }
        public decimal BaseISS { get; set; }
        public decimal AliquotaISS { get; set; }
        public decimal ValorISS { get; set; }
        public decimal BaseIR { get; set; }
        public decimal AliquotaIR { get; set; }
        public decimal ValorIR { get; set; }
        public decimal BaseCSLL { get; set; }
        public decimal AliquotaCSLL { get; set; }
        public decimal ValorCSLL { get; set; }
        public decimal ValorLiquido { get; set; }
        public decimal ValorLiquidoSemImpostos { get; set; }
        public bool PisRetido { get; set; }
        public bool CofinsRetido { get; set; }
        public bool IRRetido { get; set; }
        public bool IssRetido { get; set; }
        public bool InssRetido { get; set; }
        public bool CsllRetido { get; set; }
        public int CodigoPis { get; set; }
        public int CodigoCofins { get; set; }
        public int CodigoIR { get; set; }
        public int CodigoIss { get; set; }
        public int CodigoInss { get; set; }
        public int CodigoCsll { get; set; }
        public int GrupoCofins { get; set; }
        public int GrupoPis { get; set; }
        public int GrupoIR { get; set; }
        public int GrupoIss { get; set; }
        public int GrupoInss { get; set; }
        public int GrupoCsll { get; set; }
        public int CFOP { get; set; }
        public string Nome { get; set; }
        public int IdISS { get; set; }
        public bool Conferida { get; set; }
        public bool IntegradaCPG { get; set; }
        public bool IntegradaESF { get; set; }
        public decimal CodigoDoctoCPG { get; set; }
        public decimal BaseICMS { get; set; }
        public decimal AliquotaICMS { get; set; }
        public decimal ValorICMS { get; set; }
        public decimal IsentaICMS { get; set; }
        public decimal OutrasICMS { get; set; }
        public decimal BaseSubICMS { get; set; }
        public decimal CodDoctoEsf { get; set; }
        public decimal ValorSubICMS { get; set; }
        public string Usuario { get; set; }
        public string CidadeFornecedor { get; set; }

        public decimal ValorTotalXml { get; set; }
        public decimal ValorPISXml { get; set; }
        public decimal ValorCofinsXml { get; set; }
        public decimal ValorINSSXml { get; set; }
        public decimal AliquotaISSXml { get; set; } 
        public decimal ValorISSXml { get; set; }
        public decimal ValorIRXml { get; set; }
        public decimal ValorCSLLXml { get; set; }
        public decimal ValorLiquidoXml { get; set; }
        public bool IssRetidoXml { get; set; }
        public DateTime? DataEmissaoXml { get; set; }
        public DateTime DataCancelamento { get; set; }
        public string CNPJFornecedorXml { get; set; }
        public string FornecedorXml { get; set; }
        public string DiscriminacaoXml { get; set; }
        public bool Diferencas { get; set; }
        public bool ValidaTotal { get; set; }
        public bool ValidaPis { get; set; }
        public bool ValidaCofins { get; set; }
        public bool ValidaINSS { get; set; }
        public bool ValidaISS { get; set; }
        public bool ValidaIR { get; set; }
        public bool ValidaCSLL { get; set; }
        public bool ValidaLiquido { get; set; }
        public bool ValidaEmissao { get; set; }
        public bool ValidaFornecedor { get; set; }
        public string CodigoServico { get; set; }
        public decimal IdArquivei { get; set; }


    }

    public class ParametrosCodigoServico
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string CodigoServicoXML { get; set; }
        public string CodigoServicoGlobus { get; set; }
        public bool Existe { get; set; }
    }
}
