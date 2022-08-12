using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://localhost:8080/WsNFe2/lote", IsNullable = false)]
    public partial class RetornoEnvioLoteRPS
    {

        private RetornoEnvioLoteRPSCabecalho cabecalhoField;

        private tpEvento[] alertasField;

        private tpEvento[] errosField;

        private tpChaveNFeRPS[] chavesNFSeRPSField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RetornoEnvioLoteRPSCabecalho Cabecalho
        {
            get
            {
                return this.cabecalhoField;
            }
            set
            {
                this.cabecalhoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Alerta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Alertas
        {
            get
            {
                return this.alertasField;
            }
            set
            {
                this.alertasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Erro", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Erros
        {
            get
            {
                return this.errosField;
            }
            set
            {
                this.errosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ChaveNFSeRPS", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpChaveNFeRPS[] ChavesNFSeRPS
        {
            get
            {
                return this.chavesNFSeRPSField;
            }
            set
            {
                this.chavesNFSeRPSField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    public partial class RetornoEnvioLoteRPSCabecalho
    {

        private uint codCidadeField;

        private bool sucessoField;

        private long numeroLoteField;

        private string cPFCNPJRemetenteField;

        private System.DateTime dataEnvioLoteField;

        private string qtdNotasProcessadasField;

        private long tempoProcessamentoField;

        private decimal valorTotalServicosField;

        private decimal valorTotalDeducoesField;

        private long versaoField;

        private tpAssincrono assincronoField;

        public RetornoEnvioLoteRPSCabecalho()
        {
            this.versaoField = ((long)(1));
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint CodCidade
        {
            get
            {
                return this.codCidadeField;
            }
            set
            {
                this.codCidadeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool Sucesso
        {
            get
            {
                return this.sucessoField;
            }
            set
            {
                this.sucessoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroLote
        {
            get
            {
                return this.numeroLoteField;
            }
            set
            {
                this.numeroLoteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CPFCNPJRemetente
        {
            get
            {
                return this.cPFCNPJRemetenteField;
            }
            set
            {
                this.cPFCNPJRemetenteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime DataEnvioLote
        {
            get
            {
                return this.dataEnvioLoteField;
            }
            set
            {
                this.dataEnvioLoteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string QtdNotasProcessadas
        {
            get
            {
                return this.qtdNotasProcessadasField;
            }
            set
            {
                this.qtdNotasProcessadasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long TempoProcessamento
        {
            get
            {
                return this.tempoProcessamentoField;
            }
            set
            {
                this.tempoProcessamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorTotalServicos
        {
            get
            {
                return this.valorTotalServicosField;
            }
            set
            {
                this.valorTotalServicosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorTotalDeducoes
        {
            get
            {
                return this.valorTotalDeducoesField;
            }
            set
            {
                this.valorTotalDeducoesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long Versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpAssincrono Assincrono
        {
            get
            {
                return this.assincronoField;
            }
            set
            {
                this.assincronoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public enum tpAssincrono
    {

        /// <remarks/>
        S,

        /// <remarks/>
        N,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public partial class tpChaveNFeRPS
    {

        private tpChaveNFe chaveNFeField;

        private tpChaveRPS chaveRPSField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpChaveNFe ChaveNFe
        {
            get
            {
                return this.chaveNFeField;
            }
            set
            {
                this.chaveNFeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpChaveRPS ChaveRPS
        {
            get
            {
                return this.chaveRPSField;
            }
            set
            {
                this.chaveRPSField = value;
            }
        }
    }
    /*
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public partial class tpChaveNFe
    {

        private long inscricaoPrestadorField;

        private long numeroNFeField;

        private string codigoVerificacaoField;

        private string razaoSocialPrestadorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long InscricaoPrestador
        {
            get
            {
                return this.inscricaoPrestadorField;
            }
            set
            {
                this.inscricaoPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroNFe
        {
            get
            {
                return this.numeroNFeField;
            }
            set
            {
                this.numeroNFeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CodigoVerificacao
        {
            get
            {
                return this.codigoVerificacaoField;
            }
            set
            {
                this.codigoVerificacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RazaoSocialPrestador
        {
            get
            {
                return this.razaoSocialPrestadorField;
            }
            set
            {
                this.razaoSocialPrestadorField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public partial class tpChaveRPS
    {

        private long inscricaoPrestadorField;

        private tpSerieRPS serieRPSField;

        private long numeroRPSField;

        private System.DateTime dataEmissaoRPSField;

        private string razaoSocialPrestadorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long InscricaoPrestador
        {
            get
            {
                return this.inscricaoPrestadorField;
            }
            set
            {
                this.inscricaoPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpSerieRPS SerieRPS
        {
            get
            {
                return this.serieRPSField;
            }
            set
            {
                this.serieRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroRPS
        {
            get
            {
                return this.numeroRPSField;
            }
            set
            {
                this.numeroRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime DataEmissaoRPS
        {
            get
            {
                return this.dataEmissaoRPSField;
            }
            set
            {
                this.dataEmissaoRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RazaoSocialPrestador
        {
            get
            {
                return this.razaoSocialPrestadorField;
            }
            set
            {
                this.razaoSocialPrestadorField = value;
            }
        }
    }*/

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public enum tpSerieRPS
    {

        /// <remarks/>
        NF,
    }
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://localhost:8080/WsNFe2/lote", IsNullable = false)]
    public partial class RetornoConsultaSeqRps
    {

        private RetornoConsultaSeqRpsCabecalho cabecalhoField;

        private tpEvento[] alertasField;

        private tpEvento[] errosField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RetornoConsultaSeqRpsCabecalho Cabecalho
        {
            get
            {
                return this.cabecalhoField;
            }
            set
            {
                this.cabecalhoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Alerta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Alertas
        {
            get
            {
                return this.alertasField;
            }
            set
            {
                this.alertasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Erro", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Erros
        {
            get
            {
                return this.errosField;
            }
            set
            {
                this.errosField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    public partial class RetornoConsultaSeqRpsCabecalho
    {

        private uint codCidField;

        private string cPFCNPJRemetenteField;

        private long iMPrestadorField;

        private long nroUltimoRpsField;

        private sbyte seriePrestacaoField;

        private bool seriePrestacaoFieldSpecified;

        private long versaoField;

        public RetornoConsultaSeqRpsCabecalho()
        {
            this.versaoField = ((long)(1));
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint CodCid
        {
            get
            {
                return this.codCidField;
            }
            set
            {
                this.codCidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CPFCNPJRemetente
        {
            get
            {
                return this.cPFCNPJRemetenteField;
            }
            set
            {
                this.cPFCNPJRemetenteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long IMPrestador
        {
            get
            {
                return this.iMPrestadorField;
            }
            set
            {
                this.iMPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NroUltimoRps
        {
            get
            {
                return this.nroUltimoRpsField;
            }
            set
            {
                this.nroUltimoRpsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public sbyte SeriePrestacao
        {
            get
            {
                return this.seriePrestacaoField;
            }
            set
            {
                this.seriePrestacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SeriePrestacaoSpecified
        {
            get
            {
                return this.seriePrestacaoFieldSpecified;
            }
            set
            {
                this.seriePrestacaoFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long Versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://localhost:8080/WsNFe2/lote", IsNullable = false)]
    public partial class RetornoConsultaNotas
    {

        private RetornoConsultaNotasCabecalho cabecalhoField;

        private tpNFSe[] notasField;

        private tpEvento[] errosField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RetornoConsultaNotasCabecalho Cabecalho
        {
            get
            {
                return this.cabecalhoField;
            }
            set
            {
                this.cabecalhoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Nota", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpNFSe[] Notas
        {
            get
            {
                return this.notasField;
            }
            set
            {
                this.notasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Erro", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Erros
        {
            get
            {
                return this.errosField;
            }
            set
            {
                this.errosField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    public partial class RetornoConsultaNotasCabecalho
    {

        private uint codCidadeField;

        private string cPFCNPJRemetenteField;

        private long inscricaoMunicipalPrestadorField;

        private System.DateTime dtInicioField;

        private System.DateTime dtFimField;

        private long versaoField;

        public RetornoConsultaNotasCabecalho()
        {
            this.versaoField = ((long)(1));
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint CodCidade
        {
            get
            {
                return this.codCidadeField;
            }
            set
            {
                this.codCidadeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CPFCNPJRemetente
        {
            get
            {
                return this.cPFCNPJRemetenteField;
            }
            set
            {
                this.cPFCNPJRemetenteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long InscricaoMunicipalPrestador
        {
            get
            {
                return this.inscricaoMunicipalPrestadorField;
            }
            set
            {
                this.inscricaoMunicipalPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime dtInicio
        {
            get
            {
                return this.dtInicioField;
            }
            set
            {
                this.dtInicioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime dtFim
        {
            get
            {
                return this.dtFimField;
            }
            set
            {
                this.dtFimField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long Versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public partial class tpNFSe
    {

        private long numeroNotaField;

        private System.DateTime dataProcessamentoField;

        private bool dataProcessamentoFieldSpecified;

        private long numeroLoteField;

        private bool numeroLoteFieldSpecified;

        private string codigoVerificacaoField;

        private byte[] assinaturaField;

        private long inscricaoMunicipalPrestadorField;

        private string razaoSocialPrestadorField;

        private tpTipoRPS tipoRPSField;

        private tpSerieRPS serieRPSField;

        private long numeroRPSField;

        private System.DateTime dataEmissaoRPSField;

        private tpSituacaoRPS situacaoRPSField;

        private string serieRPSSubstituidoField;

        private long numeroRPSSubstituidoField;

        private bool numeroRPSSubstituidoFieldSpecified;

        private long numeroNFSeSubstituidaField;

        private bool numeroNFSeSubstituidaFieldSpecified;

        private string dataEmissaoNFSeSubstituidaField;

        private sbyte seriePrestacaoField;

        private bool seriePrestacaoFieldSpecified;

        private string inscricaoMunicipalTomadorField;

        private string cPFCNPJTomadorField;

        private string razaoSocialTomadorField;

        private string docTomadorEstrangeiroField;

        private string tipoLogradouroTomadorField;

        private string logradouroTomadorField;

        private string numeroEnderecoTomadorField;

        private string complementoEnderecoTomadorField;

        private string tipoBairroTomadorField;

        private string bairroTomadorField;

        private uint cidadeTomadorField;

        private string cidadeTomadorDescricaoField;

        private string cEPTomadorField;

        private string emailTomadorField;

        private int codigoAtividadeField;

        private decimal aliquotaAtividadeField;

        private tpTipoRecolhimento tipoRecolhimentoField;

        private uint municipioPrestacaoField;

        private string municipioPrestacaoDescricaoField;

        private tpOperacao operacaoField;

        private tpTributacao tributacaoField;

        private decimal valorPISField;

        private bool valorPISFieldSpecified;

        private decimal valorCOFINSField;

        private bool valorCOFINSFieldSpecified;

        private decimal valorINSSField;

        private bool valorINSSFieldSpecified;

        private decimal valorIRField;

        private bool valorIRFieldSpecified;

        private decimal valorCSLLField;

        private bool valorCSLLFieldSpecified;

        private decimal aliquotaPISField;

        private bool aliquotaPISFieldSpecified;

        private decimal aliquotaCOFINSField;

        private bool aliquotaCOFINSFieldSpecified;

        private decimal aliquotaINSSField;

        private bool aliquotaINSSFieldSpecified;

        private decimal aliquotaIRField;

        private bool aliquotaIRFieldSpecified;

        private decimal aliquotaCSLLField;

        private bool aliquotaCSLLFieldSpecified;

        private string descricaoRPSField;

        private string dDDPrestadorField;

        private string telefonePrestadorField;

        private string dDDTomadorField;

        private string telefoneTomadorField;

        private string motCancelamentoField;

        private string cPFCNPJIntermediarioField;

        private tpDeducoes[] deducoesField;

        private tpItens[] itensField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroNota
        {
            get
            {
                return this.numeroNotaField;
            }
            set
            {
                this.numeroNotaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime DataProcessamento
        {
            get
            {
                return this.dataProcessamentoField;
            }
            set
            {
                this.dataProcessamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DataProcessamentoSpecified
        {
            get
            {
                return this.dataProcessamentoFieldSpecified;
            }
            set
            {
                this.dataProcessamentoFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroLote
        {
            get
            {
                return this.numeroLoteField;
            }
            set
            {
                this.numeroLoteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NumeroLoteSpecified
        {
            get
            {
                return this.numeroLoteFieldSpecified;
            }
            set
            {
                this.numeroLoteFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CodigoVerificacao
        {
            get
            {
                return this.codigoVerificacaoField;
            }
            set
            {
                this.codigoVerificacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary")]
        public byte[] Assinatura
        {
            get
            {
                return this.assinaturaField;
            }
            set
            {
                this.assinaturaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long InscricaoMunicipalPrestador
        {
            get
            {
                return this.inscricaoMunicipalPrestadorField;
            }
            set
            {
                this.inscricaoMunicipalPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RazaoSocialPrestador
        {
            get
            {
                return this.razaoSocialPrestadorField;
            }
            set
            {
                this.razaoSocialPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpTipoRPS TipoRPS
        {
            get
            {
                return this.tipoRPSField;
            }
            set
            {
                this.tipoRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpSerieRPS SerieRPS
        {
            get
            {
                return this.serieRPSField;
            }
            set
            {
                this.serieRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroRPS
        {
            get
            {
                return this.numeroRPSField;
            }
            set
            {
                this.numeroRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime DataEmissaoRPS
        {
            get
            {
                return this.dataEmissaoRPSField;
            }
            set
            {
                this.dataEmissaoRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpSituacaoRPS SituacaoRPS
        {
            get
            {
                return this.situacaoRPSField;
            }
            set
            {
                this.situacaoRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SerieRPSSubstituido
        {
            get
            {
                return this.serieRPSSubstituidoField;
            }
            set
            {
                this.serieRPSSubstituidoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroRPSSubstituido
        {
            get
            {
                return this.numeroRPSSubstituidoField;
            }
            set
            {
                this.numeroRPSSubstituidoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NumeroRPSSubstituidoSpecified
        {
            get
            {
                return this.numeroRPSSubstituidoFieldSpecified;
            }
            set
            {
                this.numeroRPSSubstituidoFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroNFSeSubstituida
        {
            get
            {
                return this.numeroNFSeSubstituidaField;
            }
            set
            {
                this.numeroNFSeSubstituidaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NumeroNFSeSubstituidaSpecified
        {
            get
            {
                return this.numeroNFSeSubstituidaFieldSpecified;
            }
            set
            {
                this.numeroNFSeSubstituidaFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DataEmissaoNFSeSubstituida
        {
            get
            {
                return this.dataEmissaoNFSeSubstituidaField;
            }
            set
            {
                this.dataEmissaoNFSeSubstituidaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public sbyte SeriePrestacao
        {
            get
            {
                return this.seriePrestacaoField;
            }
            set
            {
                this.seriePrestacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SeriePrestacaoSpecified
        {
            get
            {
                return this.seriePrestacaoFieldSpecified;
            }
            set
            {
                this.seriePrestacaoFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InscricaoMunicipalTomador
        {
            get
            {
                return this.inscricaoMunicipalTomadorField;
            }
            set
            {
                this.inscricaoMunicipalTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CPFCNPJTomador
        {
            get
            {
                return this.cPFCNPJTomadorField;
            }
            set
            {
                this.cPFCNPJTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RazaoSocialTomador
        {
            get
            {
                return this.razaoSocialTomadorField;
            }
            set
            {
                this.razaoSocialTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocTomadorEstrangeiro
        {
            get
            {
                return this.docTomadorEstrangeiroField;
            }
            set
            {
                this.docTomadorEstrangeiroField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TipoLogradouroTomador
        {
            get
            {
                return this.tipoLogradouroTomadorField;
            }
            set
            {
                this.tipoLogradouroTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LogradouroTomador
        {
            get
            {
                return this.logradouroTomadorField;
            }
            set
            {
                this.logradouroTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NumeroEnderecoTomador
        {
            get
            {
                return this.numeroEnderecoTomadorField;
            }
            set
            {
                this.numeroEnderecoTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ComplementoEnderecoTomador
        {
            get
            {
                return this.complementoEnderecoTomadorField;
            }
            set
            {
                this.complementoEnderecoTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TipoBairroTomador
        {
            get
            {
                return this.tipoBairroTomadorField;
            }
            set
            {
                this.tipoBairroTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BairroTomador
        {
            get
            {
                return this.bairroTomadorField;
            }
            set
            {
                this.bairroTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint CidadeTomador
        {
            get
            {
                return this.cidadeTomadorField;
            }
            set
            {
                this.cidadeTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CidadeTomadorDescricao
        {
            get
            {
                return this.cidadeTomadorDescricaoField;
            }
            set
            {
                this.cidadeTomadorDescricaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CEPTomador
        {
            get
            {
                return this.cEPTomadorField;
            }
            set
            {
                this.cEPTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EmailTomador
        {
            get
            {
                return this.emailTomadorField;
            }
            set
            {
                this.emailTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int CodigoAtividade
        {
            get
            {
                return this.codigoAtividadeField;
            }
            set
            {
                this.codigoAtividadeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal AliquotaAtividade
        {
            get
            {
                return this.aliquotaAtividadeField;
            }
            set
            {
                this.aliquotaAtividadeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpTipoRecolhimento TipoRecolhimento
        {
            get
            {
                return this.tipoRecolhimentoField;
            }
            set
            {
                this.tipoRecolhimentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint MunicipioPrestacao
        {
            get
            {
                return this.municipioPrestacaoField;
            }
            set
            {
                this.municipioPrestacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MunicipioPrestacaoDescricao
        {
            get
            {
                return this.municipioPrestacaoDescricaoField;
            }
            set
            {
                this.municipioPrestacaoDescricaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpOperacao Operacao
        {
            get
            {
                return this.operacaoField;
            }
            set
            {
                this.operacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpTributacao Tributacao
        {
            get
            {
                return this.tributacaoField;
            }
            set
            {
                this.tributacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorPIS
        {
            get
            {
                return this.valorPISField;
            }
            set
            {
                this.valorPISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorPISSpecified
        {
            get
            {
                return this.valorPISFieldSpecified;
            }
            set
            {
                this.valorPISFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorCOFINS
        {
            get
            {
                return this.valorCOFINSField;
            }
            set
            {
                this.valorCOFINSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorCOFINSSpecified
        {
            get
            {
                return this.valorCOFINSFieldSpecified;
            }
            set
            {
                this.valorCOFINSFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorINSS
        {
            get
            {
                return this.valorINSSField;
            }
            set
            {
                this.valorINSSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorINSSSpecified
        {
            get
            {
                return this.valorINSSFieldSpecified;
            }
            set
            {
                this.valorINSSFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorIR
        {
            get
            {
                return this.valorIRField;
            }
            set
            {
                this.valorIRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorIRSpecified
        {
            get
            {
                return this.valorIRFieldSpecified;
            }
            set
            {
                this.valorIRFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorCSLL
        {
            get
            {
                return this.valorCSLLField;
            }
            set
            {
                this.valorCSLLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorCSLLSpecified
        {
            get
            {
                return this.valorCSLLFieldSpecified;
            }
            set
            {
                this.valorCSLLFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal AliquotaPIS
        {
            get
            {
                return this.aliquotaPISField;
            }
            set
            {
                this.aliquotaPISField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AliquotaPISSpecified
        {
            get
            {
                return this.aliquotaPISFieldSpecified;
            }
            set
            {
                this.aliquotaPISFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal AliquotaCOFINS
        {
            get
            {
                return this.aliquotaCOFINSField;
            }
            set
            {
                this.aliquotaCOFINSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AliquotaCOFINSSpecified
        {
            get
            {
                return this.aliquotaCOFINSFieldSpecified;
            }
            set
            {
                this.aliquotaCOFINSFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal AliquotaINSS
        {
            get
            {
                return this.aliquotaINSSField;
            }
            set
            {
                this.aliquotaINSSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AliquotaINSSSpecified
        {
            get
            {
                return this.aliquotaINSSFieldSpecified;
            }
            set
            {
                this.aliquotaINSSFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal AliquotaIR
        {
            get
            {
                return this.aliquotaIRField;
            }
            set
            {
                this.aliquotaIRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AliquotaIRSpecified
        {
            get
            {
                return this.aliquotaIRFieldSpecified;
            }
            set
            {
                this.aliquotaIRFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal AliquotaCSLL
        {
            get
            {
                return this.aliquotaCSLLField;
            }
            set
            {
                this.aliquotaCSLLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AliquotaCSLLSpecified
        {
            get
            {
                return this.aliquotaCSLLFieldSpecified;
            }
            set
            {
                this.aliquotaCSLLFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DescricaoRPS
        {
            get
            {
                return this.descricaoRPSField;
            }
            set
            {
                this.descricaoRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DDDPrestador
        {
            get
            {
                return this.dDDPrestadorField;
            }
            set
            {
                this.dDDPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TelefonePrestador
        {
            get
            {
                return this.telefonePrestadorField;
            }
            set
            {
                this.telefonePrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DDDTomador
        {
            get
            {
                return this.dDDTomadorField;
            }
            set
            {
                this.dDDTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TelefoneTomador
        {
            get
            {
                return this.telefoneTomadorField;
            }
            set
            {
                this.telefoneTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MotCancelamento
        {
            get
            {
                return this.motCancelamentoField;
            }
            set
            {
                this.motCancelamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CPFCNPJIntermediario
        {
            get
            {
                return this.cPFCNPJIntermediarioField;
            }
            set
            {
                this.cPFCNPJIntermediarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Deducao", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpDeducoes[] Deducoes
        {
            get
            {
                return this.deducoesField;
            }
            set
            {
                this.deducoesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpItens[] Itens
        {
            get
            {
                return this.itensField;
            }
            set
            {
                this.itensField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public enum tpTipoRPSC
    {

        /// <remarks/>
        RPS,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public enum tpSituacaoRPS
    {

        /// <remarks/>
        N,

        /// <remarks/>
        C,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public enum tpTipoRecolhimento
    {

        /// <remarks/>
        A,

        /// <remarks/>
        R,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public enum tpOperacao
    {

        /// <remarks/>
        A,

        /// <remarks/>
        B,

        /// <remarks/>
        C,

        /// <remarks/>
        D,

        /// <remarks/>
        J,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public enum tpTributacao
    {

        /// <remarks/>
        C,

        /// <remarks/>
        F,

        /// <remarks/>
        K,

        /// <remarks/>
        E,

        /// <remarks/>
        T,

        /// <remarks/>
        H,

        /// <remarks/>
        G,

        /// <remarks/>
        N,

        /// <remarks/>
        M,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public partial class tpDeducoes
    {

        private tpDeducaoPor deducaoPorField;

        private tpTipoDeducao tipoDeducaoField;

        private string cPFCNPJReferenciaField;

        private long numeroNFReferenciaField;

        private bool numeroNFReferenciaFieldSpecified;

        private decimal valorTotalReferenciaField;

        private bool valorTotalReferenciaFieldSpecified;

        private decimal percentualDeduzirField;

        private decimal valorDeduzirField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpDeducaoPor DeducaoPor
        {
            get
            {
                return this.deducaoPorField;
            }
            set
            {
                this.deducaoPorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpTipoDeducao TipoDeducao
        {
            get
            {
                return this.tipoDeducaoField;
            }
            set
            {
                this.tipoDeducaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CPFCNPJReferencia
        {
            get
            {
                return this.cPFCNPJReferenciaField;
            }
            set
            {
                this.cPFCNPJReferenciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroNFReferencia
        {
            get
            {
                return this.numeroNFReferenciaField;
            }
            set
            {
                this.numeroNFReferenciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NumeroNFReferenciaSpecified
        {
            get
            {
                return this.numeroNFReferenciaFieldSpecified;
            }
            set
            {
                this.numeroNFReferenciaFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorTotalReferencia
        {
            get
            {
                return this.valorTotalReferenciaField;
            }
            set
            {
                this.valorTotalReferenciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorTotalReferenciaSpecified
        {
            get
            {
                return this.valorTotalReferenciaFieldSpecified;
            }
            set
            {
                this.valorTotalReferenciaFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal PercentualDeduzir
        {
            get
            {
                return this.percentualDeduzirField;
            }
            set
            {
                this.percentualDeduzirField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorDeduzir
        {
            get
            {
                return this.valorDeduzirField;
            }
            set
            {
                this.valorDeduzirField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public enum tpDeducaoPor
    {

        /// <remarks/>
        Valor,

        /// <remarks/>
        Percentual,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public enum tpTipoDeducao
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Despesas com Materiais")]
        DespesascomMateriais,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Despesas com Subempreitada")]
        DespesascomSubempreitada,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Despesas com Mercadorias")]
        DespesascomMercadorias,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Servicos de Veiculacao e Divulgacao")]
        ServicosdeVeiculacaoeDivulgacao,

        /// <remarks/>
        Servicos,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Mapa de Const. Civil")]
        MapadeConstCivil,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("")]
        Item,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public partial class tpItens
    {

        private string discriminacaoServicoField;

        private decimal quantidadeField;

        private decimal valorUnitarioField;

        private decimal valorTotalField;

        private tpItemTributavel tributavelField;

        private bool tributavelFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DiscriminacaoServico
        {
            get
            {
                return this.discriminacaoServicoField;
            }
            set
            {
                this.discriminacaoServicoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Quantidade
        {
            get
            {
                return this.quantidadeField;
            }
            set
            {
                this.quantidadeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorUnitario
        {
            get
            {
                return this.valorUnitarioField;
            }
            set
            {
                this.valorUnitarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorTotal
        {
            get
            {
                return this.valorTotalField;
            }
            set
            {
                this.valorTotalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpItemTributavel Tributavel
        {
            get
            {
                return this.tributavelField;
            }
            set
            {
                this.tributavelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TributavelSpecified
        {
            get
            {
                return this.tributavelFieldSpecified;
            }
            set
            {
                this.tributavelFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public enum tpItemTributavel
    {

        /// <remarks/>
        S,

        /// <remarks/>
        N,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://localhost:8080/WsNFe2/lote", IsNullable = false)]
    public partial class RetornoConsultaNFSeRPS
    {

        private RetornoConsultaNFSeRPSCabecalho cabecalhoField;

        private tpNFSe[] notasConsultadasField;

        private tpEvento[] alertasField;

        private tpEvento[] errosField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RetornoConsultaNFSeRPSCabecalho Cabecalho
        {
            get
            {
                return this.cabecalhoField;
            }
            set
            {
                this.cabecalhoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Nota", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpNFSe[] NotasConsultadas
        {
            get
            {
                return this.notasConsultadasField;
            }
            set
            {
                this.notasConsultadasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Alerta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Alertas
        {
            get
            {
                return this.alertasField;
            }
            set
            {
                this.alertasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Erro", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Erros
        {
            get
            {
                return this.errosField;
            }
            set
            {
                this.errosField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    public partial class RetornoConsultaNFSeRPSCabecalho
    {

        private uint codCidadeField;

        private string cPFCNPJRemetenteField;

        private bool transacaoField;

        private long versaoField;

        public RetornoConsultaNFSeRPSCabecalho()
        {
            this.transacaoField = true;
            this.versaoField = ((long)(1));
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint CodCidade
        {
            get
            {
                return this.codCidadeField;
            }
            set
            {
                this.codCidadeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CPFCNPJRemetente
        {
            get
            {
                return this.cPFCNPJRemetenteField;
            }
            set
            {
                this.cPFCNPJRemetenteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool transacao
        {
            get
            {
                return this.transacaoField;
            }
            set
            {
                this.transacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long Versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://localhost:8080/WsNFe2/lote", IsNullable = false)]
    public partial class RetornoConsultaLote
    {

        private RetornoConsultaLoteCabecalho cabecalhoField;

        private tpEvento[] alertasField;

        private tpEvento[] errosField;

        private tpConsultaNFSe[] listaNFSeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RetornoConsultaLoteCabecalho Cabecalho
        {
            get
            {
                return this.cabecalhoField;
            }
            set
            {
                this.cabecalhoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Alerta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Alertas
        {
            get
            {
                return this.alertasField;
            }
            set
            {
                this.alertasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Erro", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Erros
        {
            get
            {
                return this.errosField;
            }
            set
            {
                this.errosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ConsultaNFSe", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpConsultaNFSe[] ListaNFSe
        {
            get
            {
                return this.listaNFSeField;
            }
            set
            {
                this.listaNFSeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    public partial class RetornoConsultaLoteCabecalho
    {

        private uint codCidadeField;

        private bool sucessoField;

        private long numeroLoteField;

        private string cPFCNPJRemetenteField;

        private string razaoSocialRemetenteField;

        private string dataEnvioLoteField;

        private string qtdNotasProcessadasField;

        private long tempoProcessamentoField;

        private decimal valorTotalServicosField;

        private decimal valorTotalDeducoesField;

        private long versaoField;

        public RetornoConsultaLoteCabecalho()
        {
            this.versaoField = ((long)(1));
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint CodCidade
        {
            get
            {
                return this.codCidadeField;
            }
            set
            {
                this.codCidadeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool Sucesso
        {
            get
            {
                return this.sucessoField;
            }
            set
            {
                this.sucessoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroLote
        {
            get
            {
                return this.numeroLoteField;
            }
            set
            {
                this.numeroLoteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CPFCNPJRemetente
        {
            get
            {
                return this.cPFCNPJRemetenteField;
            }
            set
            {
                this.cPFCNPJRemetenteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RazaoSocialRemetente
        {
            get
            {
                return this.razaoSocialRemetenteField;
            }
            set
            {
                this.razaoSocialRemetenteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DataEnvioLote
        {
            get
            {
                return this.dataEnvioLoteField;
            }
            set
            {
                this.dataEnvioLoteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string QtdNotasProcessadas
        {
            get
            {
                return this.qtdNotasProcessadasField;
            }
            set
            {
                this.qtdNotasProcessadasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long TempoProcessamento
        {
            get
            {
                return this.tempoProcessamentoField;
            }
            set
            {
                this.tempoProcessamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorTotalServicos
        {
            get
            {
                return this.valorTotalServicosField;
            }
            set
            {
                this.valorTotalServicosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorTotalDeducoes
        {
            get
            {
                return this.valorTotalDeducoesField;
            }
            set
            {
                this.valorTotalDeducoesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long Versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public partial class tpConsultaNFSe
    {

        private long inscricaoPrestadorField;

        private long numeroNFeField;

        private string codigoVerificacaoField;

        private tpSerieRPS serieRPSField;

        private long numeroRPSField;

        private System.DateTime dataEmissaoRPSField;

        private string razaoSocialPrestadorField;

        private tpTipoRecolhimento tipoRecolhimentoField;

        private decimal valorDeduzirField;

        private bool valorDeduzirFieldSpecified;

        private decimal valorTotalField;

        private decimal aliquotaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long InscricaoPrestador
        {
            get
            {
                return this.inscricaoPrestadorField;
            }
            set
            {
                this.inscricaoPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroNFe
        {
            get
            {
                return this.numeroNFeField;
            }
            set
            {
                this.numeroNFeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CodigoVerificacao
        {
            get
            {
                return this.codigoVerificacaoField;
            }
            set
            {
                this.codigoVerificacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpSerieRPS SerieRPS
        {
            get
            {
                return this.serieRPSField;
            }
            set
            {
                this.serieRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroRPS
        {
            get
            {
                return this.numeroRPSField;
            }
            set
            {
                this.numeroRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime DataEmissaoRPS
        {
            get
            {
                return this.dataEmissaoRPSField;
            }
            set
            {
                this.dataEmissaoRPSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RazaoSocialPrestador
        {
            get
            {
                return this.razaoSocialPrestadorField;
            }
            set
            {
                this.razaoSocialPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpTipoRecolhimento TipoRecolhimento
        {
            get
            {
                return this.tipoRecolhimentoField;
            }
            set
            {
                this.tipoRecolhimentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorDeduzir
        {
            get
            {
                return this.valorDeduzirField;
            }
            set
            {
                this.valorDeduzirField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorDeduzirSpecified
        {
            get
            {
                return this.valorDeduzirFieldSpecified;
            }
            set
            {
                this.valorDeduzirFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorTotal
        {
            get
            {
                return this.valorTotalField;
            }
            set
            {
                this.valorTotalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Aliquota
        {
            get
            {
                return this.aliquotaField;
            }
            set
            {
                this.aliquotaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://localhost:8080/WsNFe2/lote", IsNullable = false)]
    public partial class RetornoCancelamentoNFSe
    {

        private RetornoCancelamentoNFSeCabecalho cabecalhoField;

        private tpNotaCancelamentoNFSe[] notasCanceladasField;

        private tpEvento[] alertasField;

        private tpEvento[] errosField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RetornoCancelamentoNFSeCabecalho Cabecalho
        {
            get
            {
                return this.cabecalhoField;
            }
            set
            {
                this.cabecalhoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Nota", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpNotaCancelamentoNFSe[] NotasCanceladas
        {
            get
            {
                return this.notasCanceladasField;
            }
            set
            {
                this.notasCanceladasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Alerta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Alertas
        {
            get
            {
                return this.alertasField;
            }
            set
            {
                this.alertasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Erro", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public tpEvento[] Erros
        {
            get
            {
                return this.errosField;
            }
            set
            {
                this.errosField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    public partial class RetornoCancelamentoNFSeCabecalho
    {

        private uint codCidadeField;

        private bool sucessoField;

        private string cPFCNPJRemetenteField;

        private long versaoField;

        public RetornoCancelamentoNFSeCabecalho()
        {
            this.sucessoField = true;
            this.versaoField = ((long)(1));
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint CodCidade
        {
            get
            {
                return this.codCidadeField;
            }
            set
            {
                this.codCidadeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool Sucesso
        {
            get
            {
                return this.sucessoField;
            }
            set
            {
                this.sucessoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CPFCNPJRemetente
        {
            get
            {
                return this.cPFCNPJRemetenteField;
            }
            set
            {
                this.cPFCNPJRemetenteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long Versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://localhost:8080/WsNFe2/tp")]
    public partial class tpNotaCancelamentoNFSe
    {

        private long inscricaoMunicipalPrestadorField;

        private long numeroNotaField;

        private string codigoVerificacaoField;

        private string motivoCancelamentoField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long InscricaoMunicipalPrestador
        {
            get
            {
                return this.inscricaoMunicipalPrestadorField;
            }
            set
            {
                this.inscricaoMunicipalPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroNota
        {
            get
            {
                return this.numeroNotaField;
            }
            set
            {
                this.numeroNotaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CodigoVerificacao
        {
            get
            {
                return this.codigoVerificacaoField;
            }
            set
            {
                this.codigoVerificacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MotivoCancelamento
        {
            get
            {
                return this.motivoCancelamentoField;
            }
            set
            {
                this.motivoCancelamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://localhost:8080/WsNFe2/lote", IsNullable = false)]
    public partial class ConsultaSeqRps
    {

        private ConsultaSeqRpsCabecalho cabecalhoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ConsultaSeqRpsCabecalho Cabecalho
        {
            get
            {
                return this.cabecalhoField;
            }
            set
            {
                this.cabecalhoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://localhost:8080/WsNFe2/lote")]
    public partial class ConsultaSeqRpsCabecalho
    {

        private uint codCidField;

        private long iMPrestadorField;

        private string cPFCNPJRemetenteField;

        private sbyte seriePrestacaoField;

        private bool seriePrestacaoFieldSpecified;

        private long versaoField;

        public ConsultaSeqRpsCabecalho()
        {
            this.versaoField = ((long)(1));
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public uint CodCid
        {
            get
            {
                return this.codCidField;
            }
            set
            {
                this.codCidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long IMPrestador
        {
            get
            {
                return this.iMPrestadorField;
            }
            set
            {
                this.iMPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CPFCNPJRemetente
        {
            get
            {
                return this.cPFCNPJRemetenteField;
            }
            set
            {
                this.cPFCNPJRemetenteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public sbyte SeriePrestacao
        {
            get
            {
                return this.seriePrestacaoField;
            }
            set
            {
                this.seriePrestacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SeriePrestacaoSpecified
        {
            get
            {
                return this.seriePrestacaoFieldSpecified;
            }
            set
            {
                this.seriePrestacaoFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long Versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }
    }


    // OBSERVAÇÃO: o código gerado pode exigir pelo menos .NET Framework 4.5 ou .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class NOTAS_FISCAIS
    {

        private NOTAS_FISCAISNOTA_FISCAL[] nOTA_FISCALField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NOTA_FISCAL")]
        public NOTAS_FISCAISNOTA_FISCAL[] NOTA_FISCAL
        {
            get
            {
                return this.nOTA_FISCALField;
            }
            set
            {
                this.nOTA_FISCALField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class NOTAS_FISCAISNOTA_FISCAL
    {

        private string tIPOField;

        private string nUM_NOTAField;

        private string dATA_HORA_EMISSAOField;

        private string dIA_EMISSAOField;

        private string mES_COMPETENCIAField;

        private string sITUACAO_NFField;

        private string cODIGO_CIDADEField;

        private string uSUARIO_CPF_CNPJField;

        private string uSUARIO_RAZAO_SOCIALField;

        private string dATA_HORA_CANCELAMENTOField;

        private string rPS_EMISSAOField;

        private string sUB_EMISSAOField;

        private ulong pRESTADOR_CPF_CNPJField;

        private uint pRESTADOR_INSCRICAO_MUNICIPALField;

        private string pRESTADOR_RAZAO_SOCIALField;

        private string pRESTADOR_NOME_FANTASIAField;

        private string pRESTADOR_TIPO_LOGRADOUROField;

        private string pRESTADOR_LOGRADOUROField;

        private string pRESTADOR_PREST_NUMEROField;

        private object pRESTADOR_COMPLEMENTOField;

        private string pRESTADOR_TIPO_BAIRROField;

        private string pRESTADOR_BAIRROField;

        private string pRESTADOR_CIDADE_CODIGOField;

        private string pRESTADOR_CIDADEField;

        private string pRESTADOR_UFField;

        private uint pRESTADOR_CEPField;

        private object pRESTADOR_DDD_TELEFONEField;

        private object pRESTADOR_TELEFONEField;

        private object pRESTADOR_DDD_FAXField;

        private object pRESTADOR_FAXField;

        private ulong tOMADOR_CPF_CNPJField;

        private uint tOMADOR_INSCRICAO_MUNICIPALField;

        private string tOMADOR_RAZAO_SOCIALField;

        private string tOMADOR_TIPO_LOGRADOUROField;

        private string tOMADOR_LOGRADOUROField;

        private string tOMADOR_NUMEROField;

        private string tOMADOR_COMPLEMENTOField;

        private string tOMADOR_TIPO_BAIRROField;

        private string tOMADOR_BAIRROField;

        private string tOMADOR_CIDADE_CODIGOField;

        private string tOMADOR_CIDADEField;

        private string tOMADOR_UFField;

        private uint tOMADOR_CEPField;

        private string tOMADOR_EMAILField;

        private string tOMADOR_OPTANTE_SIMPLESField;

        private string vALOR_NOTAField;

        private string vALOR_DEDUCAOField;

        private string vALOR_SERVICOField;

        private string vALOR_ISSField;

        private string vALOR_PISField;

        private string vALOR_COFINSField;

        private string vALOR_INSSField;

        private string vALOR_IRField;

        private string vALOR_CSLLField;

        private string aLIQUOTA_PISField;

        private string aLIQUOTA_COFINSField;

        private string aLIQUOTA_INSSField;

        private string aLIQUOTA_IRField;

        private string aLIQUOTA_CSLLField;

        private uint cODIGO_ATIVIDADEField;

        private string dESCRICAO_ATIVIDADEField;

        private string gRUPO_ATIVIDADEField;

        private string eNQUADRAMENTO_ATIVIDADEField;

        private string lOCAL_INCIDENCIA_ATIVIDADEField;

        private string tRIBUTAVEL_ATIVIDADEField;

        private decimal dEDUCAO_VALOR_ATIVIDADEField;

        private string dEDUCAO_ATIVIDADEField;

        private string aTV_ECON_ATVField;

        private ushort cOS_SERVICOField;

        private string dESCRICAO_SERVICOField;

        private string aLIQUOTAField;

        private string tIPO_RECOLHIMENTOField;

        private string oPERACAO_TRIBUTACAOField;

        private string mOTIVO_PAGAMENTOField;

        private string cODIGO_REGIMEField;

        private string cIDADE_CODIGO_PRESTACAOField;

        private string cIDADE_PRESTACAOField;

        private string uF_PRESTACAOField;

        private string dOCUMENTO_PRESTACAOField;

        private string sERIE_PRESTACAOField;

        private string tRIBUTACAO_PRESTACAOField;

        private string dESCRICAO_NOTAField;

        private string cODIGO_VERIFICACAOField;

        private uint iD_NOTA_FISCALField;

        private string vALOR_ISS_RETField;

        private string aLIQ_RETField;

        private string dESCONTO_RETField;

        private NOTAS_FISCAISNOTA_FISCALITENS iTENSField;

        private object dEDUCOESField;

        /// <remarks/>
        public string TIPO
        {
            get
            {
                return this.tIPOField;
            }
            set
            {
                this.tIPOField = value;
            }
        }

        /// <remarks/>
        public string NUM_NOTA
        {
            get
            {
                return this.nUM_NOTAField;
            }
            set
            {
                this.nUM_NOTAField = value;
            }
        }

        /// <remarks/>
        public string DATA_HORA_EMISSAO
        {
            get
            {
                return this.dATA_HORA_EMISSAOField;
            }
            set
            {
                this.dATA_HORA_EMISSAOField = value;
            }
        }

        /// <remarks/>
        public string DIA_EMISSAO
        {
            get
            {
                return this.dIA_EMISSAOField;
            }
            set
            {
                this.dIA_EMISSAOField = value;
            }
        }

        /// <remarks/>
        public string MES_COMPETENCIA
        {
            get
            {
                return this.mES_COMPETENCIAField;
            }
            set
            {
                this.mES_COMPETENCIAField = value;
            }
        }

        /// <remarks/>
        public string SITUACAO_NF
        {
            get
            {
                return this.sITUACAO_NFField;
            }
            set
            {
                this.sITUACAO_NFField = value;
            }
        }

        /// <remarks/>
        public string CODIGO_CIDADE
        {
            get
            {
                return this.cODIGO_CIDADEField;
            }
            set
            {
                this.cODIGO_CIDADEField = value;
            }
        }

        /// <remarks/>
        public string USUARIO_CPF_CNPJ
        {
            get
            {
                return this.uSUARIO_CPF_CNPJField;
            }
            set
            {
                this.uSUARIO_CPF_CNPJField = value;
            }
        }

        /// <remarks/>
        public string USUARIO_RAZAO_SOCIAL
        {
            get
            {
                return this.uSUARIO_RAZAO_SOCIALField;
            }
            set
            {
                this.uSUARIO_RAZAO_SOCIALField = value;
            }
        }

        /// <remarks/>
        public string DATA_HORA_CANCELAMENTO
        {
            get
            {
                return this.dATA_HORA_CANCELAMENTOField;
            }
            set
            {
                this.dATA_HORA_CANCELAMENTOField = value;
            }
        }

        /// <remarks/>
        public string RPS_EMISSAO
        {
            get
            {
                return this.rPS_EMISSAOField;
            }
            set
            {
                this.rPS_EMISSAOField = value;
            }
        }

        /// <remarks/>
        public string SUB_EMISSAO
        {
            get
            {
                return this.sUB_EMISSAOField;
            }
            set
            {
                this.sUB_EMISSAOField = value;
            }
        }

        /// <remarks/>
        public ulong PRESTADOR_CPF_CNPJ
        {
            get
            {
                return this.pRESTADOR_CPF_CNPJField;
            }
            set
            {
                this.pRESTADOR_CPF_CNPJField = value;
            }
        }

        /// <remarks/>
        public uint PRESTADOR_INSCRICAO_MUNICIPAL
        {
            get
            {
                return this.pRESTADOR_INSCRICAO_MUNICIPALField;
            }
            set
            {
                this.pRESTADOR_INSCRICAO_MUNICIPALField = value;
            }
        }

        /// <remarks/>
        public string PRESTADOR_RAZAO_SOCIAL
        {
            get
            {
                return this.pRESTADOR_RAZAO_SOCIALField;
            }
            set
            {
                this.pRESTADOR_RAZAO_SOCIALField = value;
            }
        }

        /// <remarks/>
        public string PRESTADOR_NOME_FANTASIA
        {
            get
            {
                return this.pRESTADOR_NOME_FANTASIAField;
            }
            set
            {
                this.pRESTADOR_NOME_FANTASIAField = value;
            }
        }

        /// <remarks/>
        public string PRESTADOR_TIPO_LOGRADOURO
        {
            get
            {
                return this.pRESTADOR_TIPO_LOGRADOUROField;
            }
            set
            {
                this.pRESTADOR_TIPO_LOGRADOUROField = value;
            }
        }

        /// <remarks/>
        public string PRESTADOR_LOGRADOURO
        {
            get
            {
                return this.pRESTADOR_LOGRADOUROField;
            }
            set
            {
                this.pRESTADOR_LOGRADOUROField = value;
            }
        }

        /// <remarks/>
        public string PRESTADOR_PREST_NUMERO
        {
            get
            {
                return this.pRESTADOR_PREST_NUMEROField;
            }
            set
            {
                this.pRESTADOR_PREST_NUMEROField = value;
            }
        }

        /// <remarks/>
        public object PRESTADOR_COMPLEMENTO
        {
            get
            {
                return this.pRESTADOR_COMPLEMENTOField;
            }
            set
            {
                this.pRESTADOR_COMPLEMENTOField = value;
            }
        }

        /// <remarks/>
        public string PRESTADOR_TIPO_BAIRRO
        {
            get
            {
                return this.pRESTADOR_TIPO_BAIRROField;
            }
            set
            {
                this.pRESTADOR_TIPO_BAIRROField = value;
            }
        }

        /// <remarks/>
        public string PRESTADOR_BAIRRO
        {
            get
            {
                return this.pRESTADOR_BAIRROField;
            }
            set
            {
                this.pRESTADOR_BAIRROField = value;
            }
        }

        /// <remarks/>
        public string PRESTADOR_CIDADE_CODIGO
        {
            get
            {
                return this.pRESTADOR_CIDADE_CODIGOField;
            }
            set
            {
                this.pRESTADOR_CIDADE_CODIGOField = value;
            }
        }

        /// <remarks/>
        public string PRESTADOR_CIDADE
        {
            get
            {
                return this.pRESTADOR_CIDADEField;
            }
            set
            {
                this.pRESTADOR_CIDADEField = value;
            }
        }

        /// <remarks/>
        public string PRESTADOR_UF
        {
            get
            {
                return this.pRESTADOR_UFField;
            }
            set
            {
                this.pRESTADOR_UFField = value;
            }
        }

        /// <remarks/>
        public uint PRESTADOR_CEP
        {
            get
            {
                return this.pRESTADOR_CEPField;
            }
            set
            {
                this.pRESTADOR_CEPField = value;
            }
        }

        /// <remarks/>
        public object PRESTADOR_DDD_TELEFONE
        {
            get
            {
                return this.pRESTADOR_DDD_TELEFONEField;
            }
            set
            {
                this.pRESTADOR_DDD_TELEFONEField = value;
            }
        }

        /// <remarks/>
        public object PRESTADOR_TELEFONE
        {
            get
            {
                return this.pRESTADOR_TELEFONEField;
            }
            set
            {
                this.pRESTADOR_TELEFONEField = value;
            }
        }

        /// <remarks/>
        public object PRESTADOR_DDD_FAX
        {
            get
            {
                return this.pRESTADOR_DDD_FAXField;
            }
            set
            {
                this.pRESTADOR_DDD_FAXField = value;
            }
        }

        /// <remarks/>
        public object PRESTADOR_FAX
        {
            get
            {
                return this.pRESTADOR_FAXField;
            }
            set
            {
                this.pRESTADOR_FAXField = value;
            }
        }

        /// <remarks/>
        public ulong TOMADOR_CPF_CNPJ
        {
            get
            {
                return this.tOMADOR_CPF_CNPJField;
            }
            set
            {
                this.tOMADOR_CPF_CNPJField = value;
            }
        }

        /// <remarks/>
        public uint TOMADOR_INSCRICAO_MUNICIPAL
        {
            get
            {
                return this.tOMADOR_INSCRICAO_MUNICIPALField;
            }
            set
            {
                this.tOMADOR_INSCRICAO_MUNICIPALField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_RAZAO_SOCIAL
        {
            get
            {
                return this.tOMADOR_RAZAO_SOCIALField;
            }
            set
            {
                this.tOMADOR_RAZAO_SOCIALField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_TIPO_LOGRADOURO
        {
            get
            {
                return this.tOMADOR_TIPO_LOGRADOUROField;
            }
            set
            {
                this.tOMADOR_TIPO_LOGRADOUROField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_LOGRADOURO
        {
            get
            {
                return this.tOMADOR_LOGRADOUROField;
            }
            set
            {
                this.tOMADOR_LOGRADOUROField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_NUMERO
        {
            get
            {
                return this.tOMADOR_NUMEROField;
            }
            set
            {
                this.tOMADOR_NUMEROField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_COMPLEMENTO
        {
            get
            {
                return this.tOMADOR_COMPLEMENTOField;
            }
            set
            {
                this.tOMADOR_COMPLEMENTOField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_TIPO_BAIRRO
        {
            get
            {
                return this.tOMADOR_TIPO_BAIRROField;
            }
            set
            {
                this.tOMADOR_TIPO_BAIRROField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_BAIRRO
        {
            get
            {
                return this.tOMADOR_BAIRROField;
            }
            set
            {
                this.tOMADOR_BAIRROField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_CIDADE_CODIGO
        {
            get
            {
                return this.tOMADOR_CIDADE_CODIGOField;
            }
            set
            {
                this.tOMADOR_CIDADE_CODIGOField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_CIDADE
        {
            get
            {
                return this.tOMADOR_CIDADEField;
            }
            set
            {
                this.tOMADOR_CIDADEField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_UF
        {
            get
            {
                return this.tOMADOR_UFField;
            }
            set
            {
                this.tOMADOR_UFField = value;
            }
        }

        /// <remarks/>
        public uint TOMADOR_CEP
        {
            get
            {
                return this.tOMADOR_CEPField;
            }
            set
            {
                this.tOMADOR_CEPField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_EMAIL
        {
            get
            {
                return this.tOMADOR_EMAILField;
            }
            set
            {
                this.tOMADOR_EMAILField = value;
            }
        }

        /// <remarks/>
        public string TOMADOR_OPTANTE_SIMPLES
        {
            get
            {
                return this.tOMADOR_OPTANTE_SIMPLESField;
            }
            set
            {
                this.tOMADOR_OPTANTE_SIMPLESField = value;
            }
        }

        /// <remarks/>
        public string VALOR_NOTA
        {
            get
            {
                return this.vALOR_NOTAField;
            }
            set
            {
                this.vALOR_NOTAField = value;
            }
        }

        /// <remarks/>
        public string VALOR_DEDUCAO
        {
            get
            {
                return this.vALOR_DEDUCAOField;
            }
            set
            {
                this.vALOR_DEDUCAOField = value;
            }
        }

        /// <remarks/>
        public string VALOR_SERVICO
        {
            get
            {
                return this.vALOR_SERVICOField;
            }
            set
            {
                this.vALOR_SERVICOField = value;
            }
        }

        /// <remarks/>
        public string VALOR_ISS
        {
            get
            {
                return this.vALOR_ISSField;
            }
            set
            {
                this.vALOR_ISSField = value;
            }
        }

        /// <remarks/>
        public string VALOR_PIS
        {
            get
            {
                return this.vALOR_PISField;
            }
            set
            {
                this.vALOR_PISField = value;
            }
        }

        /// <remarks/>
        public string VALOR_COFINS
        {
            get
            {
                return this.vALOR_COFINSField;
            }
            set
            {
                this.vALOR_COFINSField = value;
            }
        }

        /// <remarks/>
        public string VALOR_INSS
        {
            get
            {
                return this.vALOR_INSSField;
            }
            set
            {
                this.vALOR_INSSField = value;
            }
        }

        /// <remarks/>
        public string VALOR_IR
        {
            get
            {
                return this.vALOR_IRField;
            }
            set
            {
                this.vALOR_IRField = value;
            }
        }

        /// <remarks/>
        public string VALOR_CSLL
        {
            get
            {
                return this.vALOR_CSLLField;
            }
            set
            {
                this.vALOR_CSLLField = value;
            }
        }

        /// <remarks/>
        public string ALIQUOTA_PIS
        {
            get
            {
                return this.aLIQUOTA_PISField;
            }
            set
            {
                this.aLIQUOTA_PISField = value;
            }
        }

        /// <remarks/>
        public string ALIQUOTA_COFINS
        {
            get
            {
                return this.aLIQUOTA_COFINSField;
            }
            set
            {
                this.aLIQUOTA_COFINSField = value;
            }
        }

        /// <remarks/>
        public string ALIQUOTA_INSS
        {
            get
            {
                return this.aLIQUOTA_INSSField;
            }
            set
            {
                this.aLIQUOTA_INSSField = value;
            }
        }

        /// <remarks/>
        public string ALIQUOTA_IR
        {
            get
            {
                return this.aLIQUOTA_IRField;
            }
            set
            {
                this.aLIQUOTA_IRField = value;
            }
        }

        /// <remarks/>
        public string ALIQUOTA_CSLL
        {
            get
            {
                return this.aLIQUOTA_CSLLField;
            }
            set
            {
                this.aLIQUOTA_CSLLField = value;
            }
        }

        /// <remarks/>
        public uint CODIGO_ATIVIDADE
        {
            get
            {
                return this.cODIGO_ATIVIDADEField;
            }
            set
            {
                this.cODIGO_ATIVIDADEField = value;
            }
        }

        /// <remarks/>
        public string DESCRICAO_ATIVIDADE
        {
            get
            {
                return this.dESCRICAO_ATIVIDADEField;
            }
            set
            {
                this.dESCRICAO_ATIVIDADEField = value;
            }
        }

        /// <remarks/>
        public string GRUPO_ATIVIDADE
        {
            get
            {
                return this.gRUPO_ATIVIDADEField;
            }
            set
            {
                this.gRUPO_ATIVIDADEField = value;
            }
        }

        /// <remarks/>
        public string ENQUADRAMENTO_ATIVIDADE
        {
            get
            {
                return this.eNQUADRAMENTO_ATIVIDADEField;
            }
            set
            {
                this.eNQUADRAMENTO_ATIVIDADEField = value;
            }
        }

        /// <remarks/>
        public string LOCAL_INCIDENCIA_ATIVIDADE
        {
            get
            {
                return this.lOCAL_INCIDENCIA_ATIVIDADEField;
            }
            set
            {
                this.lOCAL_INCIDENCIA_ATIVIDADEField = value;
            }
        }

        /// <remarks/>
        public string TRIBUTAVEL_ATIVIDADE
        {
            get
            {
                return this.tRIBUTAVEL_ATIVIDADEField;
            }
            set
            {
                this.tRIBUTAVEL_ATIVIDADEField = value;
            }
        }

        /// <remarks/>
        public decimal DEDUCAO_VALOR_ATIVIDADE
        {
            get
            {
                return this.dEDUCAO_VALOR_ATIVIDADEField;
            }
            set
            {
                this.dEDUCAO_VALOR_ATIVIDADEField = value;
            }
        }

        /// <remarks/>
        public string DEDUCAO_ATIVIDADE
        {
            get
            {
                return this.dEDUCAO_ATIVIDADEField;
            }
            set
            {
                this.dEDUCAO_ATIVIDADEField = value;
            }
        }

        /// <remarks/>
        public string ATV_ECON_ATV
        {
            get
            {
                return this.aTV_ECON_ATVField;
            }
            set
            {
                this.aTV_ECON_ATVField = value;
            }
        }

        /// <remarks/>
        public ushort COS_SERVICO
        {
            get
            {
                return this.cOS_SERVICOField;
            }
            set
            {
                this.cOS_SERVICOField = value;
            }
        }

        /// <remarks/>
        public string DESCRICAO_SERVICO
        {
            get
            {
                return this.dESCRICAO_SERVICOField;
            }
            set
            {
                this.dESCRICAO_SERVICOField = value;
            }
        }

        /// <remarks/>
        public string ALIQUOTA
        {
            get
            {
                return this.aLIQUOTAField;
            }
            set
            {
                this.aLIQUOTAField = value;
            }
        }

        /// <remarks/>
        public string TIPO_RECOLHIMENTO
        {
            get
            {
                return this.tIPO_RECOLHIMENTOField;
            }
            set
            {
                this.tIPO_RECOLHIMENTOField = value;
            }
        }

        /// <remarks/>
        public string OPERACAO_TRIBUTACAO
        {
            get
            {
                return this.oPERACAO_TRIBUTACAOField;
            }
            set
            {
                this.oPERACAO_TRIBUTACAOField = value;
            }
        }

        /// <remarks/>
        public string MOTIVO_PAGAMENTO
        {
            get
            {
                return this.mOTIVO_PAGAMENTOField;
            }
            set
            {
                this.mOTIVO_PAGAMENTOField = value;
            }
        }

        /// <remarks/>
        public string CODIGO_REGIME
        {
            get
            {
                return this.cODIGO_REGIMEField;
            }
            set
            {
                this.cODIGO_REGIMEField = value;
            }
        }

        /// <remarks/>
        public string CIDADE_CODIGO_PRESTACAO
        {
            get
            {
                return this.cIDADE_CODIGO_PRESTACAOField;
            }
            set
            {
                this.cIDADE_CODIGO_PRESTACAOField = value;
            }
        }

        /// <remarks/>
        public string CIDADE_PRESTACAO
        {
            get
            {
                return this.cIDADE_PRESTACAOField;
            }
            set
            {
                this.cIDADE_PRESTACAOField = value;
            }
        }

        /// <remarks/>
        public string UF_PRESTACAO
        {
            get
            {
                return this.uF_PRESTACAOField;
            }
            set
            {
                this.uF_PRESTACAOField = value;
            }
        }

        /// <remarks/>
        public string DOCUMENTO_PRESTACAO
        {
            get
            {
                return this.dOCUMENTO_PRESTACAOField;
            }
            set
            {
                this.dOCUMENTO_PRESTACAOField = value;
            }
        }

        /// <remarks/>
        public string SERIE_PRESTACAO
        {
            get
            {
                return this.sERIE_PRESTACAOField;
            }
            set
            {
                this.sERIE_PRESTACAOField = value;
            }
        }

        /// <remarks/>
        public string TRIBUTACAO_PRESTACAO
        {
            get
            {
                return this.tRIBUTACAO_PRESTACAOField;
            }
            set
            {
                this.tRIBUTACAO_PRESTACAOField = value;
            }
        }

        /// <remarks/>
        public string DESCRICAO_NOTA
        {
            get
            {
                return this.dESCRICAO_NOTAField;
            }
            set
            {
                this.dESCRICAO_NOTAField = value;
            }
        }

        /// <remarks/>
        public string CODIGO_VERIFICACAO
        {
            get
            {
                return this.cODIGO_VERIFICACAOField;
            }
            set
            {
                this.cODIGO_VERIFICACAOField = value;
            }
        }

        /// <remarks/>
        public uint ID_NOTA_FISCAL
        {
            get
            {
                return this.iD_NOTA_FISCALField;
            }
            set
            {
                this.iD_NOTA_FISCALField = value;
            }
        }

        /// <remarks/>
        public string VALOR_ISS_RET
        {
            get
            {
                return this.vALOR_ISS_RETField;
            }
            set
            {
                this.vALOR_ISS_RETField = value;
            }
        }

        /// <remarks/>
        public string ALIQ_RET
        {
            get
            {
                return this.aLIQ_RETField;
            }
            set
            {
                this.aLIQ_RETField = value;
            }
        }

        /// <remarks/>
        public string DESCONTO_RET
        {
            get
            {
                return this.dESCONTO_RETField;
            }
            set
            {
                this.dESCONTO_RETField = value;
            }
        }

        /// <remarks/>
        public NOTAS_FISCAISNOTA_FISCALITENS ITENS
        {
            get
            {
                return this.iTENSField;
            }
            set
            {
                this.iTENSField = value;
            }
        }

        /// <remarks/>
        public object DEDUCOES
        {
            get
            {
                return this.dEDUCOESField;
            }
            set
            {
                this.dEDUCOESField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class NOTAS_FISCAISNOTA_FISCALITENS
    {

        private NOTAS_FISCAISNOTA_FISCALITENSITEM iTEMField;

        /// <remarks/>
        public NOTAS_FISCAISNOTA_FISCALITENSITEM ITEM
        {
            get
            {
                return this.iTEMField;
            }
            set
            {
                this.iTEMField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class NOTAS_FISCAISNOTA_FISCALITENSITEM
    {

        private string tRIBUTAVELField;

        private string dESCRICAOField;

        private string qUANTIDADEField;

        private string vALOR_UNITARIOField;

        private string vALOR_TOTALField;

        private string dEDUCAOField;

        private string vALOR_ISS_UNITARIOField;

        /// <remarks/>
        public string TRIBUTAVEL
        {
            get
            {
                return this.tRIBUTAVELField;
            }
            set
            {
                this.tRIBUTAVELField = value;
            }
        }

        /// <remarks/>
        public string DESCRICAO
        {
            get
            {
                return this.dESCRICAOField;
            }
            set
            {
                this.dESCRICAOField = value;
            }
        }

        /// <remarks/>
        public string QUANTIDADE
        {
            get
            {
                return this.qUANTIDADEField;
            }
            set
            {
                this.qUANTIDADEField = value;
            }
        }

        /// <remarks/>
        public string VALOR_UNITARIO
        {
            get
            {
                return this.vALOR_UNITARIOField;
            }
            set
            {
                this.vALOR_UNITARIOField = value;
            }
        }

        /// <remarks/>
        public string VALOR_TOTAL
        {
            get
            {
                return this.vALOR_TOTALField;
            }
            set
            {
                this.vALOR_TOTALField = value;
            }
        }

        /// <remarks/>
        public string DEDUCAO
        {
            get
            {
                return this.dEDUCAOField;
            }
            set
            {
                this.dEDUCAOField = value;
            }
        }

        /// <remarks/>
        public string VALOR_ISS_UNITARIO
        {
            get
            {
                return this.vALOR_ISS_UNITARIOField;
            }
            set
            {
                this.vALOR_ISS_UNITARIOField = value;
            }
        }
    }



}
