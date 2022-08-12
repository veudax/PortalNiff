using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Classes
{
    public class CFOPeCST
    {
        public int Id { get; set; }
        public int CFOPSaida { get; set; }
        public int CFOPEntrada { get; set; }
        public int? CST { get; set; }
        public int? Operacao { get; set; }
        public string CFOP { get; set; }
        public string CFOPE { get; set; }
        public bool Existe { get; set; }
        public string Tipo { get; set; }
        
    }

    public class CFOPEmitidas
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int CFOPCodigo { get; set; }
        public int Lei { get; set; }
        public int? CST { get; set; }
        public int? Operacao { get; set; }
        public string Natureza { get; set; }
        public string TextoLei { get; set; }
        public string CFOP { get; set; }
        public bool Existe { get; set; }
        public bool Copiar { get; set; }
        public bool CopiarAnterior { get; set; }
        public string Empresa { get; set; }
        public string SerieGlobus{ get; set; }
        public string SerieComparar { get; set; }
    }

    public class Arquivei // NIFF_FIS_Arquivei
    {
        public int Id { get; set; }
        public DateTime DataImportado { get; set; }
        public string NomeArquivo { get; set; }
        public string CodDoctoESF { get; set; }
        public decimal CodIntNF { get; set; }
        public int IdEmpresa { get; set; }
        public int IdUsuarioVisualizou { get; set; }
        public DateTime DataVisualizou { get; set; }
        public string CNPJDestinatario { get; set; }
        public string IEDestinatario { get; set; }
        public string NumeroEndDestinatario { get; set; }
        public string EnderecoDestinatario { get; set; }
        public string BairroDestinatario { get; set; }
        public string CEPDestinatario { get; set; }
        public string RazaoSocialDestinatario { get; set; }
        public string CNPJEmitente { get; set; }
        public string IEEmitente { get; set; }
        public string RazaoSocialEmitente { get; set; }
        public string ChaveDeAcesso { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal NumeroNF { get; set; }
        public string ModeloNF { get; set; }
        public string Serie { get; set; }
        public string NaturezaOperacao { get; set; }
        public decimal ValorTotalNF { get; set; }
        public decimal ValorProduto { get; set; }
        public decimal BaseICMS { get; set; }
        public string DadosAdicionais { get; set; }
        public bool ComDiferencas { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public string Operacao { get; set; }
        public string NomeEmpresa { get; set; }
        public bool Existe { get; set; }
        public string Observacao { get; set; }
        public bool Liberado { get; set; }
        public bool Conferido { get; set; }
        public bool Marcado { get; set; }
        public DateTime DataConferido { get; set; }
        public int CFOP { get; set; }
        public string TipoArquivo { get; set; } // XML ou Excel
        public string MunicipioOrigem { get; set; }
        public string MunicipioDestino { get; set; }
        public string TipoDocto { get; set; }
        public string Origem { get; set; }
        public bool IntegradoESF { get; set; }
        public bool IntegradoCPG { get; set; }

        public string CNPJEmpresaGlobus { get; set; }
        public string CNPJTomador { get; set; }
        public string IETomador { get; set; }
        public string RazaoSocialTomador { get; set; }
        public string NumeroEndTomador { get; set; }
        public string EnderecoTomador { get; set; }
        public string BairroTomador { get; set; }
        public string CEPTomador { get; set; }
        public string TipoTomadorDestinatario { get; set; }

        public string Tributacao { get; set; }
        public string OpcaoSimples { get; set; }
        public decimal ValorServico { get; set; }
        public decimal ValorTotalRecebido { get; set; }
        public string CodigoServico { get; set; }
        public decimal AliquotaServico { get; set; }
        public decimal ValorISS { get; set; }
        public decimal ValorCredito { get; set; }
        public bool ISSRetido { get; set; }
        public string Discriminacao { get; set; }

        public decimal ValorPis { get; set; }
        public decimal ValorCofins { get; set; }
        public decimal ValorIR { get; set; }
        public decimal ValorCSLL { get; set; }
        public decimal ValorINSS { get; set; }

        public DateTime DataCancelamento { get; set; }
        public string CidadeEmitente { get; set; }
        public bool Selecionado { get; set; }
        public string StatusOld { get; set; }
        public string ComentarioUsuario { get; set; }
        public string TipoProcessamento { get; set; }
        public decimal IdNFEGlobus { get; set; }

    }

    public class NotasArquivei
    {
        public int IdArquivei { get; set; }
        public int IdEmpresa { get; set; }
        public string TipoDocumento { get; set; }
        public decimal NumeroNFGlobus { get; set; }
        public decimal NumeroNFArquivo { get; set; }
        public bool NumeroNFValido { get; set; }
        public string SerieGlobus { get; set; }
        public string SerieArquivo { get; set; }
        public String SerieComparar { get; set; }
        public bool SerieValida { get; set; }
        public DateTime EmissaoGlobus { get; set; }
        public DateTime EmissaoArquivo { get; set; }
        public bool EmissaoValida { get; set; }
        public bool IntegradaLivro { get; set; }
        public bool IntegradoCPG { get; set; }
        public DateTime Entrada { get; set; }
        public string Origem { get; set; }
        public string Observacao { get; set; }
        public bool Liberado { get; set; }
        public bool Conferido { get; set; }
        public DateTime DataConferido { get; set; }
        public DateTime DataImportado { get; set; }

        public string NaturezaOperacaoGlobus { get; set; }
        public string NaturezaOperacaoArquivo { get; set; }
        public bool NaturezaValida { get; set; }
        public decimal BaseICMSGlobus { get; set; }
        public decimal BaseICMSArquivo { get; set; }
        public bool BaseICMSValida { get; set; }

        public decimal ValorTotalNFGlobus { get; set; }
        public decimal ValorTotalNFArquivo { get; set; }
        public bool TotalNFValido { get; set; }
        public decimal ValorProdutosGlobus { get; set; }
        public decimal ValorProdutosArquivo { get; set; }
        public bool ValorProdutoValido { get; set; }
        public decimal ValorOutrasGlobus { get; set; }
        public decimal ValorIsentasGlobus { get; set; }
        public bool ValidaVlContabil { get; set; }

        public string ChaveAcessoGlobus { get; set; }
        public string ChaveAcessoArquivo { get; set; }
        public bool ChaveAcessoValida { get; set; }
        public string CodigoModeloGlobus { get; set; }
        public string CodigoModeloArquivo { get; set; }
        public bool ModeloValido { get; set; }

        public string Tipo { get; set; }
        public string Status { get; set; }
        public string StatusGlobus { get; set; }
        public string Operacao { get; set; }
        public string DadosAdicionais { get; set; }
        public string DadosAdicionaisGlobus { get; set; }
        public bool ValidaDadosAdicionais { get; set; }
        public string Lei { get; set; }

        public string CodDoctoEsf { get; set; }
        public decimal CodIntNF { get; set; }

        public string CodigoFornecedor { get; set; }
        public string RazaoSocialFornecedor { get; set; }
        public string RazaoSocialFornecedorArquivo { get; set; }
        public bool RazaoSocialFornecedorValida { get; set; }
        public string CNPJFornecedor { get; set; }
        public string CNPJFornecedorArquivo { get; set; }
        public bool CNPJFornecedorValido { get; set; }
        public string IEFornecedor { get; set; }
        public string IEFornecedorArquivo { get; set; }
        public bool IEFornecedorValido { get; set; }

        public string CNPJEmpresaGlobus { get; set; }
        public string CNPJEmpresaArquivo { get; set; }
        public bool CNPJEmpresaValido { get; set; }
        public string IEEmpresaGlobus { get; set; }
        public string IEEmpresaArquivo { get; set; }
        public bool IEEmpresaValido { get; set; }
        public string RazaoSocialEmpresaGlobus { get; set; }
        public string RazaoSocialEmpresaArquivo { get; set; }
        public bool RazaoSocialEmpresaValida { get; set; }
        public string EnderecoEmpresaGlobus { get; set; }
        public string EnderecoEmpresaArquivo { get; set; }
        public bool EnderecoValido { get; set; }
        public string NumeroEnderecoGlobus { get; set; }
        public string NumeroEnderecoEmpresaArquivo { get; set; }
        public bool NumeroEnderecoValido { get; set; }
        public string BairroEmpresaGlobus { get; set; }
        public string BairroEmpresaArquivo { get; set; }
        public bool BairroValido { get; set; }
        public string CEPEmpresaGlobus { get; set; }
        public string CEPEmpresaArquivo { get; set; }
        public bool CEPValido { get; set; }

        public string MunicipioOrigemGlobus { get; set; }
        public string MunicipioOrigemArquivo { get; set; }
        public bool MunicipioOrigemValido { get; set; }

        public string MunicipioDestinoGlobus { get; set; }
        public string MunicipioDestinoArquivo { get; set; }
        public bool MunicipioDestinoValido { get; set; }

        public bool ComDiferenca { get; set; }
        public bool Duplicidade { get; set; }
        public bool Existe { get; set; }

        public string TipoArquivo { get; set; }
        public int CFOP { get; set; }

        public string CNPJTomador { get; set; }
        public string IETomador { get; set; }
        public string RazaoSocialTomador { get; set; }
        public string NumeroEndTomador { get; set; }
        public string EnderecoTomador { get; set; }
        public string BairroTomador { get; set; }
        public string CEPTomador { get; set; }
        public string TipoTomadorDestinatario { get; set; }
        public string Usuario { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoFilial { get; set; }
        public string ComentarioUsuario { get; set; }
        public string TipoProcessamento { get; set; }
    }

    public class ImportandoArquivei
    {
        public int Id { get; set; }
        public bool Importando { get; set; }
        public string Arquivo { get; set; }
        public bool Existe { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Data { get; set; }
        public string NomeUsuario { get; set; }
        public decimal IdNFEGlobus { get; set; }
    }

    public class XmlNFe_Globus
    {
        public decimal IdNFEGlobus { get; set; }
        public string ChaveAcesso { get; set; }
        public string Xml { get; set; }
        public string Status { get; set; }
        public bool Existe { get; set; }
    }

    public class SequencialNFE
    {
        public decimal Numero { get; set; }
        public string Serie { get; set; }
        public bool Existe { get; set; }
    }

    public class LeiGlobus
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Existe { get; set; }
    }
}

