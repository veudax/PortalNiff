using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{

    // OBSERVAÇÃO: o código gerado pode exigir pelo menos .NET Framework 4.5 ou .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "NFe")]
    [System.Xml.Serialization.XmlRootAttribute("SDTNotasExport", Namespace = "NFe", IsNullable = false)]
    public partial class SDTNotasExport
    {

        private object cpfCnpjField;

        private string dtIniField;

        private string dtFinField;

        private string tipoArqField;

        private decimal versaoField;

        private SDTNotasExportReg20Item[] reg20Field;
        
        private SDTNotasExportReg90 reg90Field;

        /// <remarks/>
        public object CpfCnpj
        {
            get
            {
                return this.cpfCnpjField;
            }
            set
            {
                this.cpfCnpjField = value;
            }
        }

        /// <remarks/>
        public string DtIni
        {
            get
            {
                return this.dtIniField;
            }
            set
            {
                this.dtIniField = value;
            }
        }

        /// <remarks/>
        public string DtFin
        {
            get
            {
                return this.dtFinField;
            }
            set
            {
                this.dtFinField = value;
            }
        }

        /// <remarks/>
        public string TipoArq
        {
            get
            {
                return this.tipoArqField;
            }
            set
            {
                this.tipoArqField = value;
            }
        }

        /// <remarks/>
        public decimal Versao
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
        [System.Xml.Serialization.XmlArrayItemAttribute("Reg20Item", IsNullable = false)]
        public SDTNotasExportReg20Item[] Reg20
        {
            get
            {
                return this.reg20Field;
            }
            set
            {
                this.reg20Field = value;
            }
        }

        /// <remarks/>
        public SDTNotasExportReg90 Reg90
        {
            get
            {
                return this.reg90Field;
            }
            set
            {
                this.reg90Field = value;
            }
        }
    }

    

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "NFe")]
    public partial class SDTNotasExportReg20Item
    {

        private string tipoNfField;

        private string numNfField;

        private string serNfField;

        private string dtEmiNfField;

        private string dtHrGerNfField;

        private string codVernfField;

        private string numRpsField;

        private object serRpsField;

        private object dtEmiRpsField;

        private string tipoCpfCnpjPreField;

        private string cpfCnpjPreField;

        private string razSocPreField;

        private string logPreField;

        private string numEndPreField;

        private object complEndPreField;

        private string bairroPreField;

        private string munPreField;

        private string siglaUFPreField;

        private string cepPreField;

        private string emailPreField;

        private string sitNfField;

        private string dataCncNfField;

        private string motivoCncNfField;

        private string tipoCpfCnpjTomField;

        private string cpfCnpjTomField;

        private string razSocTomField;

        private string logTomField;

        private string numEndTomField;

        private object complEndTomField;

        private string bairroTomField;

        private string munTomField;

        private string siglaUFTomField;

        private string cepTomField;

        private string munLocPreField;

        private string siglaUFLocpreField;

        private decimal codSrvField;

        private string discrSrvField;

        private decimal vlNFSField;

        private decimal vlDedField;

        private object discrDedField;

        private decimal vlBasCalcField;

        private decimal alqIssField;

        private decimal vlIssField;

        private decimal vlIssRetField;
        
        private SDTNotasExportReg30Item[] reg30Field;

        /// <remarks/>
        public string TipoNf
        {
            get
            {
                return this.tipoNfField;
            }
            set
            {
                this.tipoNfField = value;
            }
        }

        /// <remarks/>
        public string NumNf
        {
            get
            {
                return this.numNfField;
            }
            set
            {
                this.numNfField = value;
            }
        }

        /// <remarks/>
        public string SerNf
        {
            get
            {
                return this.serNfField;
            }
            set
            {
                this.serNfField = value;
            }
        }

        /// <remarks/>
        public string DtEmiNf
        {
            get
            {
                return this.dtEmiNfField;
            }
            set
            {
                this.dtEmiNfField = value;
            }
        }

        /// <remarks/>
        public string DtHrGerNf
        {
            get
            {
                return this.dtHrGerNfField;
            }
            set
            {
                this.dtHrGerNfField = value;
            }
        }

        /// <remarks/>
        public string CodVernf
        {
            get
            {
                return this.codVernfField;
            }
            set
            {
                this.codVernfField = value;
            }
        }

        /// <remarks/>
        public string NumRps
        {
            get
            {
                return this.numRpsField;
            }
            set
            {
                this.numRpsField = value;
            }
        }

        /// <remarks/>
        public object SerRps
        {
            get
            {
                return this.serRpsField;
            }
            set
            {
                this.serRpsField = value;
            }
        }

        /// <remarks/>
        public object DtEmiRps
        {
            get
            {
                return this.dtEmiRpsField;
            }
            set
            {
                this.dtEmiRpsField = value;
            }
        }

        /// <remarks/>
        public string TipoCpfCnpjPre
        {
            get
            {
                return this.tipoCpfCnpjPreField;
            }
            set
            {
                this.tipoCpfCnpjPreField = value;
            }
        }

        /// <remarks/>
        public string CpfCnpjPre
        {
            get
            {
                return this.cpfCnpjPreField;
            }
            set
            {
                this.cpfCnpjPreField = value;
            }
        }

        /// <remarks/>
        public string RazSocPre
        {
            get
            {
                return this.razSocPreField;
            }
            set
            {
                this.razSocPreField = value;
            }
        }

        /// <remarks/>
        public string LogPre
        {
            get
            {
                return this.logPreField;
            }
            set
            {
                this.logPreField = value;
            }
        }

        /// <remarks/>
        public string NumEndPre
        {
            get
            {
                return this.numEndPreField;
            }
            set
            {
                this.numEndPreField = value;
            }
        }

        /// <remarks/>
        public object ComplEndPre
        {
            get
            {
                return this.complEndPreField;
            }
            set
            {
                this.complEndPreField = value;
            }
        }

        /// <remarks/>
        public string BairroPre
        {
            get
            {
                return this.bairroPreField;
            }
            set
            {
                this.bairroPreField = value;
            }
        }

        /// <remarks/>
        public string MunPre
        {
            get
            {
                return this.munPreField;
            }
            set
            {
                this.munPreField = value;
            }
        }

        /// <remarks/>
        public string SiglaUFPre
        {
            get
            {
                return this.siglaUFPreField;
            }
            set
            {
                this.siglaUFPreField = value;
            }
        }

        /// <remarks/>
        public string CepPre
        {
            get
            {
                return this.cepPreField;
            }
            set
            {
                this.cepPreField = value;
            }
        }

        /// <remarks/>
        public string EmailPre
        {
            get
            {
                return this.emailPreField;
            }
            set
            {
                this.emailPreField = value;
            }
        }

        /// <remarks/>
        public string SitNf
        {
            get
            {
                return this.sitNfField;
            }
            set
            {
                this.sitNfField = value;
            }
        }

        /// <remarks/>
        public string DataCncNf
        {
            get
            {
                return this.dataCncNfField;
            }
            set
            {
                this.dataCncNfField = value;
            }
        }

        /// <remarks/>
        public string MotivoCncNf
        {
            get
            {
                return this.motivoCncNfField;
            }
            set
            {
                this.motivoCncNfField = value;
            }
        }

        /// <remarks/>
        public string TipoCpfCnpjTom
        {
            get
            {
                return this.tipoCpfCnpjTomField;
            }
            set
            {
                this.tipoCpfCnpjTomField = value;
            }
        }

        /// <remarks/>
        public string CpfCnpjTom
        {
            get
            {
                return this.cpfCnpjTomField;
            }
            set
            {
                this.cpfCnpjTomField = value;
            }
        }

        /// <remarks/>
        public string RazSocTom
        {
            get
            {
                return this.razSocTomField;
            }
            set
            {
                this.razSocTomField = value;
            }
        }

        /// <remarks/>
        public string LogTom
        {
            get
            {
                return this.logTomField;
            }
            set
            {
                this.logTomField = value;
            }
        }

        /// <remarks/>
        public string NumEndTom
        {
            get
            {
                return this.numEndTomField;
            }
            set
            {
                this.numEndTomField = value;
            }
        }

        /// <remarks/>
        public object ComplEndTom
        {
            get
            {
                return this.complEndTomField;
            }
            set
            {
                this.complEndTomField = value;
            }
        }

        /// <remarks/>
        public string BairroTom
        {
            get
            {
                return this.bairroTomField;
            }
            set
            {
                this.bairroTomField = value;
            }
        }

        /// <remarks/>
        public string MunTom
        {
            get
            {
                return this.munTomField;
            }
            set
            {
                this.munTomField = value;
            }
        }

        /// <remarks/>
        public string SiglaUFTom
        {
            get
            {
                return this.siglaUFTomField;
            }
            set
            {
                this.siglaUFTomField = value;
            }
        }

        /// <remarks/>
        public string CepTom
        {
            get
            {
                return this.cepTomField;
            }
            set
            {
                this.cepTomField = value;
            }
        }

        /// <remarks/>
        public string MunLocPre
        {
            get
            {
                return this.munLocPreField;
            }
            set
            {
                this.munLocPreField = value;
            }
        }

        /// <remarks/>
        public string SiglaUFLocpre
        {
            get
            {
                return this.siglaUFLocpreField;
            }
            set
            {
                this.siglaUFLocpreField = value;
            }
        }

        /// <remarks/>
        public decimal CodSrv
        {
            get
            {
                return this.codSrvField;
            }
            set
            {
                this.codSrvField = value;
            }
        }

        /// <remarks/>
        public string DiscrSrv
        {
            get
            {
                return this.discrSrvField;
            }
            set
            {
                this.discrSrvField = value;
            }
        }

        /// <remarks/>
        public decimal VlNFS
        {
            get
            {
                return this.vlNFSField;
            }
            set
            {
                this.vlNFSField = value;
            }
        }

        /// <remarks/>
        public decimal VlDed
        {
            get
            {
                return this.vlDedField;
            }
            set
            {
                this.vlDedField = value;
            }
        }

        /// <remarks/>
        public object DiscrDed
        {
            get
            {
                return this.discrDedField;
            }
            set
            {
                this.discrDedField = value;
            }
        }

        /// <remarks/>
        public decimal VlBasCalc
        {
            get
            {
                return this.vlBasCalcField;
            }
            set
            {
                this.vlBasCalcField = value;
            }
        }

        /// <remarks/>
        public decimal AlqIss
        {
            get
            {
                return this.alqIssField;
            }
            set
            {
                this.alqIssField = value;
            }
        }

        /// <remarks/>
        public decimal VlIss
        {
            get
            {
                return this.vlIssField;
            }
            set
            {
                this.vlIssField = value;
            }
        }

        /// <remarks/>
        public decimal VlIssRet
        {
            get
            {
                return this.vlIssRetField;
            }
            set
            {
                this.vlIssRetField = value;
            }
        }
    }


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "NFe")]
    public partial class SDTNotasExportReg30Item
    {

        private string tributoSiglaField;

        private object tributoAliquotaField;

        private object tributoValorField;

        /// <remarks/>
        public string TributoSilga
        {
            get
            {
                return this.tributoSiglaField;
            }
            set
            {
                this.tributoSiglaField = value;
            }
        }

        /// <remarks/>
        public object TributoAliquota
        {
            get
            {
                return this.tributoAliquotaField;
            }
            set
            {
                this.tributoAliquotaField = value;
            }
        }

        /// <remarks/>
        public object TributoValor
        {
            get
            {
                return this.tributoValorField;
            }
            set
            {
                this.tributoValorField = value;
            }
        }

    }



    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "NFe")]
    public partial class SDTNotasExportReg90
    {

        private string qtdRegNormalField;

        private object valorNFSField;

        private object valorISSField;

        private object valorDedField;

        private object valorIssRetField;

        /// <remarks/>
        public string QtdRegNormal
        {
            get
            {
                return this.qtdRegNormalField;
            }
            set
            {
                this.qtdRegNormalField = value;
            }
        }

        /// <remarks/>
        public object ValorNFS
        {
            get
            {
                return this.valorNFSField;
            }
            set
            {
                this.valorNFSField = value;
            }
        }

        /// <remarks/>
        public object ValorISS
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
        public object ValorDed
        {
            get
            {
                return this.valorDedField;
            }
            set
            {
                this.valorDedField = value;
            }
        }

        /// <remarks/>
        public object ValorIssRet
        {
            get
            {
                return this.valorIssRetField;
            }
            set
            {
                this.valorIssRetField = value;
            }
        }
    }


    // OBSERVAÇÃO: o código gerado pode exigir pelo menos .NET Framework 4.5 ou .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "NFe")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "NFe", IsNullable = false)]
    public partial class SdtNotasExport_TipoDeArquivo_3
    {

        private object cpfCnpjField;

        private string dtIniField;

        private string dtFinField;

        private string tipoArqField;

        private decimal versaoField;

        private SdtNotasExport_TipoDeArquivo_3Reg20Item[] reg20Field;

        private SdtNotasExport_TipoDeArquivo_3Reg90 reg90Field;

        /// <remarks/>
        public object CpfCnpj
        {
            get
            {
                return this.cpfCnpjField;
            }
            set
            {
                this.cpfCnpjField = value;
            }
        }

        /// <remarks/>
        public string DtIni
        {
            get
            {
                return this.dtIniField;
            }
            set
            {
                this.dtIniField = value;
            }
        }

        /// <remarks/>
        public string DtFin
        {
            get
            {
                return this.dtFinField;
            }
            set
            {
                this.dtFinField = value;
            }
        }

        /// <remarks/>
        public string TipoArq
        {
            get
            {
                return this.tipoArqField;
            }
            set
            {
                this.tipoArqField = value;
            }
        }

        /// <remarks/>
        public decimal Versao
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
        [System.Xml.Serialization.XmlArrayItemAttribute("Reg20Item", IsNullable = false)]
        public SdtNotasExport_TipoDeArquivo_3Reg20Item[] Reg20
        {
            get
            {
                return this.reg20Field;
            }
            set
            {
                this.reg20Field = value;
            }
        }

        /// <remarks/>
        public SdtNotasExport_TipoDeArquivo_3Reg90 Reg90
        {
            get
            {
                return this.reg90Field;
            }
            set
            {
                this.reg90Field = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "NFe")]
    public partial class SdtNotasExport_TipoDeArquivo_3Reg20Item
    {

        private string tipoNfField;

        private string numNfField;

        private string serNfField;

        private string dtEmiNfField;

        private string dtHrGerNfField;

        private string codVernfField;

        private string numRpsField;

        private object serRpsField;

        private object dtEmiRpsField;

        private string tipoCpfCnpjPreField;

        private string cpfCnpjPreField;

        private string razSocPreField;

        private string logPreField;

        private string numEndPreField;

        private object complEndPreField;

        private string bairroPreField;

        private string munPreField;

        private string siglaUFPreField;

        private string cepPreField;

        private string emailPreField;

        private string sitNfField;
        
        private string dataCncNfField;

        private string motivoCncNfField;


        private string tipoCpfCnpjTomField;

        private string cpfCnpjTomField;

        private string razSocTomField;

        private string logTomField;

        private string numEndTomField;

        private object complEndTomField;

        private string bairroTomField;

        private string munTomField;

        private string siglaUFTomField;

        private string cepTomField;

        private string munLocPreField;

        private string siglaUFLocpreField;

        private decimal codSrvField;

        private string discrSrvField;

        private decimal vlNFSField;

        private decimal vlDedField;

        private string discrDedField;

        private decimal vlBasCalcField;

        private decimal alqIssField;

        private decimal vlIssField;

        private decimal vlIssRetField;

        /// <remarks/>
        public string TipoNf
        {
            get
            {
                return this.tipoNfField;
            }
            set
            {
                this.tipoNfField = value;
            }
        }

        /// <remarks/>
        public string NumNf
        {
            get
            {
                return this.numNfField;
            }
            set
            {
                this.numNfField = value;
            }
        }

        /// <remarks/>
        public string SerNf
        {
            get
            {
                return this.serNfField;
            }
            set
            {
                this.serNfField = value;
            }
        }

        /// <remarks/>
        public string DtEmiNf
        {
            get
            {
                return this.dtEmiNfField;
            }
            set
            {
                this.dtEmiNfField = value;
            }
        }

        /// <remarks/>
        public string DtHrGerNf
        {
            get
            {
                return this.dtHrGerNfField;
            }
            set
            {
                this.dtHrGerNfField = value;
            }
        }

        /// <remarks/>
        public string CodVernf
        {
            get
            {
                return this.codVernfField;
            }
            set
            {
                this.codVernfField = value;
            }
        }

        /// <remarks/>
        public string NumRps
        {
            get
            {
                return this.numRpsField;
            }
            set
            {
                this.numRpsField = value;
            }
        }

        /// <remarks/>
        public object SerRps
        {
            get
            {
                return this.serRpsField;
            }
            set
            {
                this.serRpsField = value;
            }
        }

        /// <remarks/>
        public object DtEmiRps
        {
            get
            {
                return this.dtEmiRpsField;
            }
            set
            {
                this.dtEmiRpsField = value;
            }
        }

        /// <remarks/>
        public string TipoCpfCnpjPre
        {
            get
            {
                return this.tipoCpfCnpjPreField;
            }
            set
            {
                this.tipoCpfCnpjPreField = value;
            }
        }

        /// <remarks/>
        public string CpfCnpjPre
        {
            get
            {
                return this.cpfCnpjPreField;
            }
            set
            {
                this.cpfCnpjPreField = value;
            }
        }

        /// <remarks/>
        public string RazSocPre
        {
            get
            {
                return this.razSocPreField;
            }
            set
            {
                this.razSocPreField = value;
            }
        }

        /// <remarks/>
        public string LogPre
        {
            get
            {
                return this.logPreField;
            }
            set
            {
                this.logPreField = value;
            }
        }

        /// <remarks/>
        public string NumEndPre
        {
            get
            {
                return this.numEndPreField;
            }
            set
            {
                this.numEndPreField = value;
            }
        }

        /// <remarks/>
        public object ComplEndPre
        {
            get
            {
                return this.complEndPreField;
            }
            set
            {
                this.complEndPreField = value;
            }
        }

        /// <remarks/>
        public string BairroPre
        {
            get
            {
                return this.bairroPreField;
            }
            set
            {
                this.bairroPreField = value;
            }
        }

        /// <remarks/>
        public string MunPre
        {
            get
            {
                return this.munPreField;
            }
            set
            {
                this.munPreField = value;
            }
        }

        /// <remarks/>
        public string SiglaUFPre
        {
            get
            {
                return this.siglaUFPreField;
            }
            set
            {
                this.siglaUFPreField = value;
            }
        }

        /// <remarks/>
        public string CepPre
        {
            get
            {
                return this.cepPreField;
            }
            set
            {
                this.cepPreField = value;
            }
        }

        /// <remarks/>
        public string EmailPre
        {
            get
            {
                return this.emailPreField;
            }
            set
            {
                this.emailPreField = value;
            }
        }

        /// <remarks/>
        public string SitNf
        {
            get
            {
                return this.sitNfField;
            }
            set
            {
                this.sitNfField = value;
            }
        }

        /// <remarks/>
        public string DataCncNf
        {
            get
            {
                return this.dataCncNfField;
            }
            set
            {
                this.dataCncNfField = value;
            }
        }

        /// <remarks/>
        public string MotivoCncNf
        {
            get
            {
                return this.motivoCncNfField;
            }
            set
            {
                this.motivoCncNfField = value;
            }
        }

        /// <remarks/>
        public string TipoCpfCnpjTom
        {
            get
            {
                return this.tipoCpfCnpjTomField;
            }
            set
            {
                this.tipoCpfCnpjTomField = value;
            }
        }

        /// <remarks/>
        public string CpfCnpjTom
        {
            get
            {
                return this.cpfCnpjTomField;
            }
            set
            {
                this.cpfCnpjTomField = value;
            }
        }

        /// <remarks/>
        public string RazSocTom
        {
            get
            {
                return this.razSocTomField;
            }
            set
            {
                this.razSocTomField = value;
            }
        }

        /// <remarks/>
        public string LogTom
        {
            get
            {
                return this.logTomField;
            }
            set
            {
                this.logTomField = value;
            }
        }

        /// <remarks/>
        public string NumEndTom
        {
            get
            {
                return this.numEndTomField;
            }
            set
            {
                this.numEndTomField = value;
            }
        }

        /// <remarks/>
        public object ComplEndTom
        {
            get
            {
                return this.complEndTomField;
            }
            set
            {
                this.complEndTomField = value;
            }
        }

        /// <remarks/>
        public string BairroTom
        {
            get
            {
                return this.bairroTomField;
            }
            set
            {
                this.bairroTomField = value;
            }
        }

        /// <remarks/>
        public string MunTom
        {
            get
            {
                return this.munTomField;
            }
            set
            {
                this.munTomField = value;
            }
        }

        /// <remarks/>
        public string SiglaUFTom
        {
            get
            {
                return this.siglaUFTomField;
            }
            set
            {
                this.siglaUFTomField = value;
            }
        }

        /// <remarks/>
        public string CepTom
        {
            get
            {
                return this.cepTomField;
            }
            set
            {
                this.cepTomField = value;
            }
        }

        /// <remarks/>
        public string MunLocPre
        {
            get
            {
                return this.munLocPreField;
            }
            set
            {
                this.munLocPreField = value;
            }
        }

        /// <remarks/>
        public string SiglaUFLocpre
        {
            get
            {
                return this.siglaUFLocpreField;
            }
            set
            {
                this.siglaUFLocpreField = value;
            }
        }

        /// <remarks/>
        public decimal CodSrv
        {
            get
            {
                return this.codSrvField;
            }
            set
            {
                this.codSrvField = value;
            }
        }

        /// <remarks/>
        public string DiscrSrv
        {
            get
            {
                return this.discrSrvField;
            }
            set
            {
                this.discrSrvField = value;
            }
        }

        /// <remarks/>
        public decimal VlNFS
        {
            get
            {
                return this.vlNFSField;
            }
            set
            {
                this.vlNFSField = value;
            }
        }

        /// <remarks/>
        public decimal VlDed
        {
            get
            {
                return this.vlDedField;
            }
            set
            {
                this.vlDedField = value;
            }
        }

        /// <remarks/>
        public string DiscrDed
        {
            get
            {
                return this.discrDedField;
            }
            set
            {
                this.discrDedField = value;
            }
        }

        /// <remarks/>
        public decimal VlBasCalc
        {
            get
            {
                return this.vlBasCalcField;
            }
            set
            {
                this.vlBasCalcField = value;
            }
        }

        /// <remarks/>
        public decimal AlqIss
        {
            get
            {
                return this.alqIssField;
            }
            set
            {
                this.alqIssField = value;
            }
        }

        /// <remarks/>
        public decimal VlIss
        {
            get
            {
                return this.vlIssField;
            }
            set
            {
                this.vlIssField = value;
            }
        }

        /// <remarks/>
        public decimal VlIssRet
        {
            get
            {
                return this.vlIssRetField;
            }
            set
            {
                this.vlIssRetField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "NFe")]
    public partial class SdtNotasExport_TipoDeArquivo_3Reg90
    {

        private string qtdRegNormalField;

        private object valorNFSField;

        private object valorISSField;

        private object valorDedField;

        private object valorIssRetField;

        /// <remarks/>
        public string QtdRegNormal
        {
            get
            {
                return this.qtdRegNormalField;
            }
            set
            {
                this.qtdRegNormalField = value;
            }
        }

        /// <remarks/>
        public object ValorNFS
        {
            get
            {
                return this.valorNFSField;
            }
            set
            {
                this.valorNFSField = value;
            }
        }

        /// <remarks/>
        public object ValorISS
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
        public object ValorDed
        {
            get
            {
                return this.valorDedField;
            }
            set
            {
                this.valorDedField = value;
            }
        }

        /// <remarks/>
        public object ValorIssRet
        {
            get
            {
                return this.valorIssRetField;
            }
            set
            {
                this.valorIssRetField = value;
            }
        }
    }


}
