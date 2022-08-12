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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.prefeitura.sp.gov.br/nfe")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.prefeitura.sp.gov.br/nfe", IsNullable = false)]
    public partial class RetornoConsulta
    {

        private RetornoConsultaCabecalho cabecalhoField;

        private tpEvento[] alertaField;

        private tpEvento[] erroField;

        private tpNFe[] nFeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RetornoConsultaCabecalho Cabecalho
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
        [System.Xml.Serialization.XmlElementAttribute("Alerta", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpEvento[] Alerta
        {
            get
            {
                return this.alertaField;
            }
            set
            {
                this.alertaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Erro", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpEvento[] Erro
        {
            get
            {
                return this.erroField;
            }
            set
            {
                this.erroField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NFe", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpNFe[] NFe
        {
            get
            {
                return this.nFeField;
            }
            set
            {
                this.nFeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.prefeitura.sp.gov.br/nfe")]
    public partial class RetornoConsultaCabecalho
    {

        private bool sucessoField;

        private long versaoField;

        public RetornoConsultaCabecalho()
        {
            this.versaoField = ((long)(1));
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.prefeitura.sp.gov.br/nfe/tipos")]
    public partial class tpEvento : object, System.ComponentModel.INotifyPropertyChanged
    {

        private short codigoField;

        private string descricaoField;

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public short Codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
                this.RaisePropertyChanged("Codigo");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Descricao
        {
            get
            {
                return this.descricaoField;
            }
            set
            {
                this.descricaoField = value;
                this.RaisePropertyChanged("Descricao");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ChaveNFe", typeof(tpChaveNFe), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("ChaveRPS", typeof(tpChaveRPS), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
                this.RaisePropertyChanged("Item");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.prefeitura.sp.gov.br/nfe/tipos")]
    [System.Xml.Serialization.XmlRootAttribute("NFe", Namespace = "", IsNullable = false)]
    public partial class tpChaveNFe : object, System.ComponentModel.INotifyPropertyChanged
    {

        private long inscricaoPrestadorField;

        private long numeroNFeField;

        private string codigoVerificacaoField;

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
                this.RaisePropertyChanged("InscricaoPrestador");
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
                this.RaisePropertyChanged("NumeroNFe");
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
                this.RaisePropertyChanged("CodigoVerificacao");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.prefeitura.sp.gov.br/nfe/tipos")]
    [System.Xml.Serialization.XmlRootAttribute("NFe", Namespace = "", IsNullable = false)]
    public partial class tpChaveRPS : object, System.ComponentModel.INotifyPropertyChanged
    {

        private long inscricaoPrestadorField;

        private string serieRPSField;

        private long numeroRPSField;

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
                this.RaisePropertyChanged("InscricaoPrestador");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SerieRPS
        {
            get
            {
                return this.serieRPSField;
            }
            set
            {
                this.serieRPSField = value;
                this.RaisePropertyChanged("SerieRPS");
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
                this.RaisePropertyChanged("NumeroRPS");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.prefeitura.sp.gov.br/nfe/tipos")]
    [System.Xml.Serialization.XmlRootAttribute("NFe", Namespace = "", IsNullable = false)]
    public partial class tpNFe : object, System.ComponentModel.INotifyPropertyChanged
    {

        private byte[] assinaturaField;

        private tpChaveNFe chaveNFeField;

        private System.DateTime dataEmissaoNFeField;

        private long numeroLoteField;

        private bool numeroLoteFieldSpecified;

        private tpChaveRPS chaveRPSField;

        private tpTipoRPS tipoRPSField;

        private bool tipoRPSFieldSpecified;

        private System.DateTime dataEmissaoRPSField;

        private bool dataEmissaoRPSFieldSpecified;

        private tpCPFCNPJ cPFCNPJPrestadorField;

        private string razaoSocialPrestadorField;

        private tpEndereco enderecoPrestadorField;

        private string emailPrestadorField;

        private tpStatusNFe statusNFeField;

        private System.DateTime dataCancelamentoField;

        private bool dataCancelamentoFieldSpecified;

        private string tributacaoNFeField;

        private tpOpcaoSimples opcaoSimplesField;

        private long numeroGuiaField;

        private bool numeroGuiaFieldSpecified;

        private System.DateTime dataQuitacaoGuiaField;

        private bool dataQuitacaoGuiaFieldSpecified;

        private decimal valorServicosField;

        private decimal valorDeducoesField;

        private bool valorDeducoesFieldSpecified;

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

        private int codigoServicoField;

        private decimal aliquotaServicosField;

        private decimal valorISSField;

        private decimal valorCreditoField;

        private bool iSSRetidoField;

        private tpCPFCNPJ cPFCNPJTomadorField;

        private long inscricaoMunicipalTomadorField;

        private bool inscricaoMunicipalTomadorFieldSpecified;

        private long inscricaoEstadualTomadorField;

        private bool inscricaoEstadualTomadorFieldSpecified;

        private string razaoSocialTomadorField;

        private tpEndereco enderecoTomadorField;

        private string emailTomadorField;

        private tpCPFCNPJ cPFCNPJIntermediarioField;

        private long inscricaoMunicipalIntermediarioField;

        private bool inscricaoMunicipalIntermediarioFieldSpecified;

        private string iSSRetidoIntermediarioField;

        private string emailIntermediarioField;

        private string discriminacaoField;

        private decimal valorCargaTributariaField;

        private bool valorCargaTributariaFieldSpecified;

        private decimal percentualCargaTributariaField;

        private bool percentualCargaTributariaFieldSpecified;

        private string fonteCargaTributariaField;

        private long codigoCEIField;

        private bool codigoCEIFieldSpecified;

        private long matriculaObraField;

        private bool matriculaObraFieldSpecified;

        private int municipioPrestacaoField;

        private bool municipioPrestacaoFieldSpecified;

        private long numeroEncapsulamentoField;

        private bool numeroEncapsulamentoFieldSpecified;

        private decimal valorTotalRecebidoField;

        private bool valorTotalRecebidoFieldSpecified;

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
        public tpChaveNFe ChaveNFe
        {
            get
            {
                return this.chaveNFeField;
            }
            set
            {
                this.chaveNFeField = value;
                this.RaisePropertyChanged("ChaveNFe");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime DataEmissaoNFe
        {
            get
            {
                return this.dataEmissaoNFeField;
            }
            set
            {
                this.dataEmissaoNFeField = value;
                this.RaisePropertyChanged("DataEmissaoNFe");
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
                this.RaisePropertyChanged("NumeroLote");
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
                this.RaisePropertyChanged("NumeroLoteSpecified");
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
                this.RaisePropertyChanged("ChaveRPS");
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
                this.RaisePropertyChanged("TipoRPS");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TipoRPSSpecified
        {
            get
            {
                return this.tipoRPSFieldSpecified;
            }
            set
            {
                this.tipoRPSFieldSpecified = value;
                this.RaisePropertyChanged("TipoRPSSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime DataEmissaoRPS
        {
            get
            {
                return this.dataEmissaoRPSField;
            }
            set
            {
                this.dataEmissaoRPSField = value;
                this.RaisePropertyChanged("DataEmissaoRPS");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DataEmissaoRPSSpecified
        {
            get
            {
                return this.dataEmissaoRPSFieldSpecified;
            }
            set
            {
                this.dataEmissaoRPSFieldSpecified = value;
                this.RaisePropertyChanged("DataEmissaoRPSSpecified");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpCPFCNPJ CPFCNPJPrestador
        {
            get
            {
                return this.cPFCNPJPrestadorField;
            }
            set
            {
                this.cPFCNPJPrestadorField = value;
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
                this.RaisePropertyChanged("RazaoSocialPrestador");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpEndereco EnderecoPrestador
        {
            get
            {
                return this.enderecoPrestadorField;
            }
            set
            {
                this.enderecoPrestadorField = value;
                this.RaisePropertyChanged("EnderecoPrestador");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EmailPrestador
        {
            get
            {
                return this.emailPrestadorField;
            }
            set
            {
                this.emailPrestadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpStatusNFe StatusNFe
        {
            get
            {
                return this.statusNFeField;
            }
            set
            {
                this.statusNFeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime DataCancelamento
        {
            get
            {
                return this.dataCancelamentoField;
            }
            set
            {
                this.dataCancelamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DataCancelamentoSpecified
        {
            get
            {
                return this.dataCancelamentoFieldSpecified;
            }
            set
            {
                this.dataCancelamentoFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TributacaoNFe
        {
            get
            {
                return this.tributacaoNFeField;
            }
            set
            {
                this.tributacaoNFeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpOpcaoSimples OpcaoSimples
        {
            get
            {
                return this.opcaoSimplesField;
            }
            set
            {
                this.opcaoSimplesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroGuia
        {
            get
            {
                return this.numeroGuiaField;
            }
            set
            {
                this.numeroGuiaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NumeroGuiaSpecified
        {
            get
            {
                return this.numeroGuiaFieldSpecified;
            }
            set
            {
                this.numeroGuiaFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public System.DateTime DataQuitacaoGuia
        {
            get
            {
                return this.dataQuitacaoGuiaField;
            }
            set
            {
                this.dataQuitacaoGuiaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DataQuitacaoGuiaSpecified
        {
            get
            {
                return this.dataQuitacaoGuiaFieldSpecified;
            }
            set
            {
                this.dataQuitacaoGuiaFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorServicos
        {
            get
            {
                return this.valorServicosField;
            }
            set
            {
                this.valorServicosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorDeducoes
        {
            get
            {
                return this.valorDeducoesField;
            }
            set
            {
                this.valorDeducoesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorDeducoesSpecified
        {
            get
            {
                return this.valorDeducoesFieldSpecified;
            }
            set
            {
                this.valorDeducoesFieldSpecified = value;
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
        public int CodigoServico
        {
            get
            {
                return this.codigoServicoField;
            }
            set
            {
                this.codigoServicoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal AliquotaServicos
        {
            get
            {
                return this.aliquotaServicosField;
            }
            set
            {
                this.aliquotaServicosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorISS
        {
            get
            {
                return this.valorISSField;
            }
            set
            {
                this.valorISSField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorCredito
        {
            get
            {
                return this.valorCreditoField;
            }
            set
            {
                this.valorCreditoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public bool ISSRetido
        {
            get
            {
                return this.iSSRetidoField;
            }
            set
            {
                this.iSSRetidoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tpCPFCNPJ CPFCNPJTomador
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
        public long InscricaoMunicipalTomador
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InscricaoMunicipalTomadorSpecified
        {
            get
            {
                return this.inscricaoMunicipalTomadorFieldSpecified;
            }
            set
            {
                this.inscricaoMunicipalTomadorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long InscricaoEstadualTomador
        {
            get
            {
                return this.inscricaoEstadualTomadorField;
            }
            set
            {
                this.inscricaoEstadualTomadorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InscricaoEstadualTomadorSpecified
        {
            get
            {
                return this.inscricaoEstadualTomadorFieldSpecified;
            }
            set
            {
                this.inscricaoEstadualTomadorFieldSpecified = value;
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
        public tpEndereco EnderecoTomador
        {
            get
            {
                return this.enderecoTomadorField;
            }
            set
            {
                this.enderecoTomadorField = value;
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
        public tpCPFCNPJ CPFCNPJIntermediario
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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long InscricaoMunicipalIntermediario
        {
            get
            {
                return this.inscricaoMunicipalIntermediarioField;
            }
            set
            {
                this.inscricaoMunicipalIntermediarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InscricaoMunicipalIntermediarioSpecified
        {
            get
            {
                return this.inscricaoMunicipalIntermediarioFieldSpecified;
            }
            set
            {
                this.inscricaoMunicipalIntermediarioFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ISSRetidoIntermediario
        {
            get
            {
                return this.iSSRetidoIntermediarioField;
            }
            set
            {
                this.iSSRetidoIntermediarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EmailIntermediario
        {
            get
            {
                return this.emailIntermediarioField;
            }
            set
            {
                this.emailIntermediarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Discriminacao
        {
            get
            {
                return this.discriminacaoField;
            }
            set
            {
                this.discriminacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorCargaTributaria
        {
            get
            {
                return this.valorCargaTributariaField;
            }
            set
            {
                this.valorCargaTributariaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorCargaTributariaSpecified
        {
            get
            {
                return this.valorCargaTributariaFieldSpecified;
            }
            set
            {
                this.valorCargaTributariaFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal PercentualCargaTributaria
        {
            get
            {
                return this.percentualCargaTributariaField;
            }
            set
            {
                this.percentualCargaTributariaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PercentualCargaTributariaSpecified
        {
            get
            {
                return this.percentualCargaTributariaFieldSpecified;
            }
            set
            {
                this.percentualCargaTributariaFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FonteCargaTributaria
        {
            get
            {
                return this.fonteCargaTributariaField;
            }
            set
            {
                this.fonteCargaTributariaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long CodigoCEI
        {
            get
            {
                return this.codigoCEIField;
            }
            set
            {
                this.codigoCEIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CodigoCEISpecified
        {
            get
            {
                return this.codigoCEIFieldSpecified;
            }
            set
            {
                this.codigoCEIFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long MatriculaObra
        {
            get
            {
                return this.matriculaObraField;
            }
            set
            {
                this.matriculaObraField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MatriculaObraSpecified
        {
            get
            {
                return this.matriculaObraFieldSpecified;
            }
            set
            {
                this.matriculaObraFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int MunicipioPrestacao
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MunicipioPrestacaoSpecified
        {
            get
            {
                return this.municipioPrestacaoFieldSpecified;
            }
            set
            {
                this.municipioPrestacaoFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long NumeroEncapsulamento
        {
            get
            {
                return this.numeroEncapsulamentoField;
            }
            set
            {
                this.numeroEncapsulamentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NumeroEncapsulamentoSpecified
        {
            get
            {
                return this.numeroEncapsulamentoFieldSpecified;
            }
            set
            {
                this.numeroEncapsulamentoFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ValorTotalRecebido
        {
            get
            {
                return this.valorTotalRecebidoField;
            }
            set
            {
                this.valorTotalRecebidoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorTotalRecebidoSpecified
        {
            get
            {
                return this.valorTotalRecebidoFieldSpecified;
            }
            set
            {
                this.valorTotalRecebidoFieldSpecified = value;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.prefeitura.sp.gov.br/nfe/tipos")]
    public enum tpTipoRPS
    {

        /// <remarks/>
        RPS,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RPS-M")]
        RPSM,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("RPS-C")]
        RPSC,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.prefeitura.sp.gov.br/nfe/tipos")]
    public partial class tpCPFCNPJ : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string itemField;

        private ItemChoiceType itemElementNameField;
        public enum ItemChoiceType
        {
            /// <remarks/>
            CNPJ,

            /// <remarks/>
            CPF,
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CNPJ", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("CPF", typeof(string), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
                this.RaisePropertyChanged("Item");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
                this.RaisePropertyChanged("ItemElementName");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.prefeitura.sp.gov.br/nfe/tipos")]
    public partial class tpEndereco
    {

        private string tipoLogradouroField;

        private string logradouroField;

        private string numeroEnderecoField;

        private string complementoEnderecoField;

        private string bairroField;

        private int cidadeField;

        private bool cidadeFieldSpecified;

        private string ufField;

        private int cEPField;

        private bool cEPFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TipoLogradouro
        {
            get
            {
                return this.tipoLogradouroField;
            }
            set
            {
                this.tipoLogradouroField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Logradouro
        {
            get
            {
                return this.logradouroField;
            }
            set
            {
                this.logradouroField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NumeroEndereco
        {
            get
            {
                return this.numeroEnderecoField;
            }
            set
            {
                this.numeroEnderecoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ComplementoEndereco
        {
            get
            {
                return this.complementoEnderecoField;
            }
            set
            {
                this.complementoEnderecoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Bairro
        {
            get
            {
                return this.bairroField;
            }
            set
            {
                this.bairroField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int Cidade
        {
            get
            {
                return this.cidadeField;
            }
            set
            {
                this.cidadeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CidadeSpecified
        {
            get
            {
                return this.cidadeFieldSpecified;
            }
            set
            {
                this.cidadeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int CEP
        {
            get
            {
                return this.cEPField;
            }
            set
            {
                this.cEPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CEPSpecified
        {
            get
            {
                return this.cEPFieldSpecified;
            }
            set
            {
                this.cEPFieldSpecified = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.prefeitura.sp.gov.br/nfe/tipos")]
    public enum tpStatusNFe
    {

        /// <remarks/>
        N,

        /// <remarks/>
        C,

        /// <remarks/>
        E,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.prefeitura.sp.gov.br/nfe/tipos")]
    public enum tpOpcaoSimples
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0, // Não optante 

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1, //Optante pelo Simples Federal 1%

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,//Optante pelo Simples Federal 0,5%

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,//Optante pelo Simples Municipal

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,//Optante pelo Simples Nacional

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5, // procurar
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,//procurar
    }
    }
