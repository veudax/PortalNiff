﻿using System;
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [System.Xml.Serialization.XmlRootAttribute("nfeProc", Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public partial class TNfeProc : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFe nFeField;

        private TProtNFe protNFeField;

        private string versaoField;

        /// <remarks/>
        public TNFe NFe
        {
            get
            {
                return this.nFeField;
            }
            set
            {
                this.nFeField = value;
                this.RaisePropertyChanged("NFe");
            }
        }

        /// <remarks/>
        public TProtNFe protNFe
        {
            get
            {
                return this.protNFeField;
            }
            set
            {
                this.protNFeField = value;
                this.RaisePropertyChanged("protNFe");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
                this.RaisePropertyChanged("versao");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [System.Xml.Serialization.XmlRootAttribute("NFe", Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public partial class TNFe : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFe infNFeField;

        private TNFeInfNFeSupl infNFeSuplField;

        private SignatureType signatureField;

        /// <remarks/>
        public TNFeInfNFe infNFe
        {
            get
            {
                return this.infNFeField;
            }
            set
            {
                this.infNFeField = value;
                this.RaisePropertyChanged("infNFe");
            }
        }

        /// <remarks/>
        public TNFeInfNFeSupl infNFeSupl
        {
            get
            {
                return this.infNFeSuplField;
            }
            set
            {
                this.infNFeSuplField = value;
                this.RaisePropertyChanged("infNFeSupl");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
                this.RaisePropertyChanged("Signature");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFe : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeIde ideField;

        private TNFeInfNFeEmit emitField;

        private TNFeInfNFeAvulsa avulsaField;

        private TNFeInfNFeDest destField;

        private TLocal retiradaField;

        private TLocal entregaField;

        private TNFeInfNFeAutXML[] autXMLField;

        private TNFeInfNFeDet[] detField;

        private TNFeInfNFeTotal totalField;

        private TNFeInfNFeTransp transpField;

        private TNFeInfNFeCobr cobrField;

        private TNFeInfNFePag pagField;

        private TNFeInfNFeInfAdic infAdicField;

        private TNFeInfNFeExporta exportaField;

        private TNFeInfNFeCompra compraField;

        private TNFeInfNFeCana canaField;

        private TInfRespTec infRespTecField;

        private string versaoField;

        private string idField;

        /// <remarks/>
        public TNFeInfNFeIde ide
        {
            get
            {
                return this.ideField;
            }
            set
            {
                this.ideField = value;
                this.RaisePropertyChanged("ide");
            }
        }

        /// <remarks/>
        public TNFeInfNFeEmit emit
        {
            get
            {
                return this.emitField;
            }
            set
            {
                this.emitField = value;
                this.RaisePropertyChanged("emit");
            }
        }

        /// <remarks/>
        public TNFeInfNFeAvulsa avulsa
        {
            get
            {
                return this.avulsaField;
            }
            set
            {
                this.avulsaField = value;
                this.RaisePropertyChanged("avulsa");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDest dest
        {
            get
            {
                return this.destField;
            }
            set
            {
                this.destField = value;
                this.RaisePropertyChanged("dest");
            }
        }

        /// <remarks/>
        public TLocal retirada
        {
            get
            {
                return this.retiradaField;
            }
            set
            {
                this.retiradaField = value;
                this.RaisePropertyChanged("retirada");
            }
        }

        /// <remarks/>
        public TLocal entrega
        {
            get
            {
                return this.entregaField;
            }
            set
            {
                this.entregaField = value;
                this.RaisePropertyChanged("entrega");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("autXML")]
        public TNFeInfNFeAutXML[] autXML
        {
            get
            {
                return this.autXMLField;
            }
            set
            {
                this.autXMLField = value;
                this.RaisePropertyChanged("autXML");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("det")]
        public TNFeInfNFeDet[] det
        {
            get
            {
                return this.detField;
            }
            set
            {
                this.detField = value;
                this.RaisePropertyChanged("det");
            }
        }

        /// <remarks/>
        public TNFeInfNFeTotal total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
                this.RaisePropertyChanged("total");
            }
        }

        /// <remarks/>
        public TNFeInfNFeTransp transp
        {
            get
            {
                return this.transpField;
            }
            set
            {
                this.transpField = value;
                this.RaisePropertyChanged("transp");
            }
        }

        /// <remarks/>
        public TNFeInfNFeCobr cobr
        {
            get
            {
                return this.cobrField;
            }
            set
            {
                this.cobrField = value;
                this.RaisePropertyChanged("cobr");
            }
        }

        /// <remarks/>
        public TNFeInfNFePag pag
        {
            get
            {
                return this.pagField;
            }
            set
            {
                this.pagField = value;
                this.RaisePropertyChanged("pag");
            }
        }

        /// <remarks/>
        public TNFeInfNFeInfAdic infAdic
        {
            get
            {
                return this.infAdicField;
            }
            set
            {
                this.infAdicField = value;
                this.RaisePropertyChanged("infAdic");
            }
        }

        /// <remarks/>
        public TNFeInfNFeExporta exporta
        {
            get
            {
                return this.exportaField;
            }
            set
            {
                this.exportaField = value;
                this.RaisePropertyChanged("exporta");
            }
        }

        /// <remarks/>
        public TNFeInfNFeCompra compra
        {
            get
            {
                return this.compraField;
            }
            set
            {
                this.compraField = value;
                this.RaisePropertyChanged("compra");
            }
        }

        /// <remarks/>
        public TNFeInfNFeCana cana
        {
            get
            {
                return this.canaField;
            }
            set
            {
                this.canaField = value;
                this.RaisePropertyChanged("cana");
            }
        }

        /// <remarks/>
        public TInfRespTec infRespTec
        {
            get
            {
                return this.infRespTecField;
            }
            set
            {
                this.infRespTecField = value;
                this.RaisePropertyChanged("infRespTec");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
                this.RaisePropertyChanged("versao");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("Id");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeIde : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TCodUfIBGE cUFField;

        private string cNFField;

        private string natOpField;

        private TMod modField;

        private string serieField;

        private string nNFField;

        private string dhEmiField;

        private string dhSaiEntField;

        private TNFeInfNFeIdeTpNF tpNFField;

        private TNFeInfNFeIdeIdDest idDestField;

        private string cMunFGField;

        private TNFeInfNFeIdeTpImp tpImpField;

        private TNFeInfNFeIdeTpEmis tpEmisField;

        private string cDVField;

        private TAmb tpAmbField;

        private TFinNFe finNFeField;

        private TNFeInfNFeIdeIndFinal indFinalField;

        private TNFeInfNFeIdeIndPres indPresField;

        private TProcEmi procEmiField;

        private string verProcField;

        private string dhContField;

        private string xJustField;

        private TNFeInfNFeIdeNFref[] nFrefField;

        /// <remarks/>
        public TCodUfIBGE cUF
        {
            get
            {
                return this.cUFField;
            }
            set
            {
                this.cUFField = value;
                this.RaisePropertyChanged("cUF");
            }
        }

        /// <remarks/>
        public string cNF
        {
            get
            {
                return this.cNFField;
            }
            set
            {
                this.cNFField = value;
                this.RaisePropertyChanged("cNF");
            }
        }

        /// <remarks/>
        public string natOp
        {
            get
            {
                return this.natOpField;
            }
            set
            {
                this.natOpField = value;
                this.RaisePropertyChanged("natOp");
            }
        }

        /// <remarks/>
        public TMod mod
        {
            get
            {
                return this.modField;
            }
            set
            {
                this.modField = value;
                this.RaisePropertyChanged("mod");
            }
        }

        /// <remarks/>
        public string serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
                this.RaisePropertyChanged("serie");
            }
        }

        /// <remarks/>
        public string nNF
        {
            get
            {
                return this.nNFField;
            }
            set
            {
                this.nNFField = value;
                this.RaisePropertyChanged("nNF");
            }
        }

        /// <remarks/>
        public string dhEmi
        {
            get
            {
                return this.dhEmiField;
            }
            set
            {
                this.dhEmiField = value;
                this.RaisePropertyChanged("dhEmi");
            }
        }

        /// <remarks/>
        public string dhSaiEnt
        {
            get
            {
                return this.dhSaiEntField;
            }
            set
            {
                this.dhSaiEntField = value;
                this.RaisePropertyChanged("dhSaiEnt");
            }
        }

        /// <remarks/>
        public TNFeInfNFeIdeTpNF tpNF
        {
            get
            {
                return this.tpNFField;
            }
            set
            {
                this.tpNFField = value;
                this.RaisePropertyChanged("tpNF");
            }
        }

        /// <remarks/>
        public TNFeInfNFeIdeIdDest idDest
        {
            get
            {
                return this.idDestField;
            }
            set
            {
                this.idDestField = value;
                this.RaisePropertyChanged("idDest");
            }
        }

        /// <remarks/>
        public string cMunFG
        {
            get
            {
                return this.cMunFGField;
            }
            set
            {
                this.cMunFGField = value;
                this.RaisePropertyChanged("cMunFG");
            }
        }

        /// <remarks/>
        public TNFeInfNFeIdeTpImp tpImp
        {
            get
            {
                return this.tpImpField;
            }
            set
            {
                this.tpImpField = value;
                this.RaisePropertyChanged("tpImp");
            }
        }

        /// <remarks/>
        public TNFeInfNFeIdeTpEmis tpEmis
        {
            get
            {
                return this.tpEmisField;
            }
            set
            {
                this.tpEmisField = value;
                this.RaisePropertyChanged("tpEmis");
            }
        }

        /// <remarks/>
        public string cDV
        {
            get
            {
                return this.cDVField;
            }
            set
            {
                this.cDVField = value;
                this.RaisePropertyChanged("cDV");
            }
        }

        /// <remarks/>
        public TAmb tpAmb
        {
            get
            {
                return this.tpAmbField;
            }
            set
            {
                this.tpAmbField = value;
                this.RaisePropertyChanged("tpAmb");
            }
        }

        /// <remarks/>
        public TFinNFe finNFe
        {
            get
            {
                return this.finNFeField;
            }
            set
            {
                this.finNFeField = value;
                this.RaisePropertyChanged("finNFe");
            }
        }

        /// <remarks/>
        public TNFeInfNFeIdeIndFinal indFinal
        {
            get
            {
                return this.indFinalField;
            }
            set
            {
                this.indFinalField = value;
                this.RaisePropertyChanged("indFinal");
            }
        }

        /// <remarks/>
        public TNFeInfNFeIdeIndPres indPres
        {
            get
            {
                return this.indPresField;
            }
            set
            {
                this.indPresField = value;
                this.RaisePropertyChanged("indPres");
            }
        }

        /// <remarks/>
        public TProcEmi procEmi
        {
            get
            {
                return this.procEmiField;
            }
            set
            {
                this.procEmiField = value;
                this.RaisePropertyChanged("procEmi");
            }
        }

        /// <remarks/>
        public string verProc
        {
            get
            {
                return this.verProcField;
            }
            set
            {
                this.verProcField = value;
                this.RaisePropertyChanged("verProc");
            }
        }

        /// <remarks/>
        public string dhCont
        {
            get
            {
                return this.dhContField;
            }
            set
            {
                this.dhContField = value;
                this.RaisePropertyChanged("dhCont");
            }
        }

        /// <remarks/>
        public string xJust
        {
            get
            {
                return this.xJustField;
            }
            set
            {
                this.xJustField = value;
                this.RaisePropertyChanged("xJust");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NFref")]
        public TNFeInfNFeIdeNFref[] NFref
        {
            get
            {
                return this.nFrefField;
            }
            set
            {
                this.nFrefField = value;
                this.RaisePropertyChanged("NFref");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TCodUfIBGE
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17")]
        Item17,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("21")]
        Item21,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("22")]
        Item22,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("23")]
        Item23,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("24")]
        Item24,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25")]
        Item25,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("26")]
        Item26,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("27")]
        Item27,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("28")]
        Item28,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("29")]
        Item29,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("31")]
        Item31,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("32")]
        Item32,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("33")]
        Item33,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("35")]
        Item35,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("41")]
        Item41,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("42")]
        Item42,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("43")]
        Item43,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("50")]
        Item50,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("51")]
        Item51,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("52")]
        Item52,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("53")]
        Item53,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TMod
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("55")]
        Item55,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("65")]
        Item65,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeIdeTpNF
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeIdeIdDest
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeIdeTpImp
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeIdeTpEmis
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Item7,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TAmb
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TFinNFe
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeIdeIndFinal
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeIdeIndPres
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TProcEmi
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeIdeNFref : object, System.ComponentModel.INotifyPropertyChanged
    {

        private object itemField;

        private ItemChoiceType1 itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("refCTe", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("refECF", typeof(TNFeInfNFeIdeNFrefRefECF))]
        [System.Xml.Serialization.XmlElementAttribute("refNF", typeof(TNFeInfNFeIdeNFrefRefNF))]
        [System.Xml.Serialization.XmlElementAttribute("refNFP", typeof(TNFeInfNFeIdeNFrefRefNFP))]
        [System.Xml.Serialization.XmlElementAttribute("refNFe", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType1 ItemElementName
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeIdeNFrefRefECF : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeIdeNFrefRefECFMod modField;

        private string nECFField;

        private string nCOOField;

        /// <remarks/>
        public TNFeInfNFeIdeNFrefRefECFMod mod
        {
            get
            {
                return this.modField;
            }
            set
            {
                this.modField = value;
                this.RaisePropertyChanged("mod");
            }
        }

        /// <remarks/>
        public string nECF
        {
            get
            {
                return this.nECFField;
            }
            set
            {
                this.nECFField = value;
                this.RaisePropertyChanged("nECF");
            }
        }

        /// <remarks/>
        public string nCOO
        {
            get
            {
                return this.nCOOField;
            }
            set
            {
                this.nCOOField = value;
                this.RaisePropertyChanged("nCOO");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeIdeNFrefRefECFMod
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2B")]
        Item2B,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2C")]
        Item2C,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2D")]
        Item2D,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TProtNFe : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TProtNFeInfProt infProtField;

        private SignatureType signatureField;

        private string versaoField;

        /// <remarks/>
        public TProtNFeInfProt infProt
        {
            get
            {
                return this.infProtField;
            }
            set
            {
                this.infProtField = value;
                this.RaisePropertyChanged("infProt");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
                this.RaisePropertyChanged("Signature");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
                this.RaisePropertyChanged("versao");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TProtNFeInfProt : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TAmb tpAmbField;

        private string verAplicField;

        private string chNFeField;

        private string dhRecbtoField;

        private string nProtField;

        private byte[] digValField;

        private string cStatField;

        private string xMotivoField;

        private string cMsgField;

        private string xMsgField;

        private string idField;

        /// <remarks/>
        public TAmb tpAmb
        {
            get
            {
                return this.tpAmbField;
            }
            set
            {
                this.tpAmbField = value;
                this.RaisePropertyChanged("tpAmb");
            }
        }

        /// <remarks/>
        public string verAplic
        {
            get
            {
                return this.verAplicField;
            }
            set
            {
                this.verAplicField = value;
                this.RaisePropertyChanged("verAplic");
            }
        }

        /// <remarks/>
        public string chNFe
        {
            get
            {
                return this.chNFeField;
            }
            set
            {
                this.chNFeField = value;
                this.RaisePropertyChanged("chNFe");
            }
        }

        /// <remarks/>
        public string dhRecbto
        {
            get
            {
                return this.dhRecbtoField;
            }
            set
            {
                this.dhRecbtoField = value;
                this.RaisePropertyChanged("dhRecbto");
            }
        }

        /// <remarks/>
        public string nProt
        {
            get
            {
                return this.nProtField;
            }
            set
            {
                this.nProtField = value;
                this.RaisePropertyChanged("nProt");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] digVal
        {
            get
            {
                return this.digValField;
            }
            set
            {
                this.digValField = value;
                this.RaisePropertyChanged("digVal");
            }
        }

        /// <remarks/>
        public string cStat
        {
            get
            {
                return this.cStatField;
            }
            set
            {
                this.cStatField = value;
                this.RaisePropertyChanged("cStat");
            }
        }

        /// <remarks/>
        public string xMotivo
        {
            get
            {
                return this.xMotivoField;
            }
            set
            {
                this.xMotivoField = value;
                this.RaisePropertyChanged("xMotivo");
            }
        }

        /// <remarks/>
        public string cMsg
        {
            get
            {
                return this.cMsgField;
            }
            set
            {
                this.cMsgField = value;
                this.RaisePropertyChanged("cMsg");
            }
        }

        /// <remarks/>
        public string xMsg
        {
            get
            {
                return this.xMsgField;
            }
            set
            {
                this.xMsgField = value;
                this.RaisePropertyChanged("xMsg");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("Id");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private SignedInfoType signedInfoField;

        private SignatureValueType signatureValueField;

        private KeyInfoType keyInfoField;

        private string idField;

        /// <remarks/>
        public SignedInfoType SignedInfo
        {
            get
            {
                return this.signedInfoField;
            }
            set
            {
                this.signedInfoField = value;
                this.RaisePropertyChanged("SignedInfo");
            }
        }

        /// <remarks/>
        public SignatureValueType SignatureValue
        {
            get
            {
                return this.signatureValueField;
            }
            set
            {
                this.signatureValueField = value;
                this.RaisePropertyChanged("SignatureValue");
            }
        }

        /// <remarks/>
        public KeyInfoType KeyInfo
        {
            get
            {
                return this.keyInfoField;
            }
            set
            {
                this.keyInfoField = value;
                this.RaisePropertyChanged("KeyInfo");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("Id");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignedInfoType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private SignedInfoTypeCanonicalizationMethod canonicalizationMethodField;

        private SignedInfoTypeSignatureMethod signatureMethodField;

        private ReferenceType referenceField;

        private string idField;

        /// <remarks/>
        public SignedInfoTypeCanonicalizationMethod CanonicalizationMethod
        {
            get
            {
                return this.canonicalizationMethodField;
            }
            set
            {
                this.canonicalizationMethodField = value;
                this.RaisePropertyChanged("CanonicalizationMethod");
            }
        }

        /// <remarks/>
        public SignedInfoTypeSignatureMethod SignatureMethod
        {
            get
            {
                return this.signatureMethodField;
            }
            set
            {
                this.signatureMethodField = value;
                this.RaisePropertyChanged("SignatureMethod");
            }
        }

        /// <remarks/>
        public ReferenceType Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
                this.RaisePropertyChanged("Reference");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("Id");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignedInfoTypeCanonicalizationMethod : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string algorithmField;

        public SignedInfoTypeCanonicalizationMethod()
        {
            this.algorithmField = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
                this.RaisePropertyChanged("Algorithm");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignedInfoTypeSignatureMethod : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string algorithmField;

        public SignedInfoTypeSignatureMethod()
        {
            this.algorithmField = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
                this.RaisePropertyChanged("Algorithm");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class ReferenceType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TransformType[] transformsField;

        private ReferenceTypeDigestMethod digestMethodField;

        private byte[] digestValueField;

        private string idField;

        private string uRIField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Transform", IsNullable = false)]
        public TransformType[] Transforms
        {
            get
            {
                return this.transformsField;
            }
            set
            {
                this.transformsField = value;
                this.RaisePropertyChanged("Transforms");
            }
        }

        /// <remarks/>
        public ReferenceTypeDigestMethod DigestMethod
        {
            get
            {
                return this.digestMethodField;
            }
            set
            {
                this.digestMethodField = value;
                this.RaisePropertyChanged("DigestMethod");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] DigestValue
        {
            get
            {
                return this.digestValueField;
            }
            set
            {
                this.digestValueField = value;
                this.RaisePropertyChanged("DigestValue");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("Id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
                this.RaisePropertyChanged("URI");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                this.RaisePropertyChanged("Type");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class TransformType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string[] xPathField;

        private TTransformURI algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("XPath")]
        public string[] XPath
        {
            get
            {
                return this.xPathField;
            }
            set
            {
                this.xPathField = value;
                this.RaisePropertyChanged("XPath");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public TTransformURI Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
                this.RaisePropertyChanged("Algorithm");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public enum TTransformURI
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("http://www.w3.org/2000/09/xmldsig#enveloped-signature")]
        httpwwww3org200009xmldsigenvelopedsignature,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("http://www.w3.org/TR/2001/REC-xml-c14n-20010315")]
        httpwwww3orgTR2001RECxmlc14n20010315,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class ReferenceTypeDigestMethod : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string algorithmField;

        public ReferenceTypeDigestMethod()
        {
            this.algorithmField = "http://www.w3.org/2000/09/xmldsig#sha1";
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
                this.RaisePropertyChanged("Algorithm");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class SignatureValueType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string idField;

        private byte[] valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("Id");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType = "base64Binary")]
        public byte[] Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class KeyInfoType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private X509DataType x509DataField;

        private string idField;

        /// <remarks/>
        public X509DataType X509Data
        {
            get
            {
                return this.x509DataField;
            }
            set
            {
                this.x509DataField = value;
                this.RaisePropertyChanged("X509Data");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
                this.RaisePropertyChanged("Id");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class X509DataType : object, System.ComponentModel.INotifyPropertyChanged
    {

        private byte[] x509CertificateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] X509Certificate
        {
            get
            {
                return this.x509CertificateField;
            }
            set
            {
                this.x509CertificateField = value;
                this.RaisePropertyChanged("X509Certificate");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TInfRespTec : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string cNPJField;

        private string xContatoField;

        private string emailField;

        private string foneField;

        private string idCSRTField;

        private byte[] hashCSRTField;

        /// <remarks/>
        public string CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
                this.RaisePropertyChanged("CNPJ");
            }
        }

        /// <remarks/>
        public string xContato
        {
            get
            {
                return this.xContatoField;
            }
            set
            {
                this.xContatoField = value;
                this.RaisePropertyChanged("xContato");
            }
        }

        /// <remarks/>
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
                this.RaisePropertyChanged("email");
            }
        }

        /// <remarks/>
        public string fone
        {
            get
            {
                return this.foneField;
            }
            set
            {
                this.foneField = value;
                this.RaisePropertyChanged("fone");
            }
        }

        /// <remarks/>
        public string idCSRT
        {
            get
            {
                return this.idCSRTField;
            }
            set
            {
                this.idCSRTField = value;
                this.RaisePropertyChanged("idCSRT");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] hashCSRT
        {
            get
            {
                return this.hashCSRTField;
            }
            set
            {
                this.hashCSRTField = value;
                this.RaisePropertyChanged("hashCSRT");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TVeiculo : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string placaField;

        private TUf ufField;

        private string rNTCField;

        /// <remarks/>
        public string placa
        {
            get
            {
                return this.placaField;
            }
            set
            {
                this.placaField = value;
                this.RaisePropertyChanged("placa");
            }
        }

        /// <remarks/>
        public TUf UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
                this.RaisePropertyChanged("UF");
            }
        }

        /// <remarks/>
        public string RNTC
        {
            get
            {
                return this.rNTCField;
            }
            set
            {
                this.rNTCField = value;
                this.RaisePropertyChanged("RNTC");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TUf
    {

        /// <remarks/>
        AC,

        /// <remarks/>
        AL,

        /// <remarks/>
        AM,

        /// <remarks/>
        AP,

        /// <remarks/>
        BA,

        /// <remarks/>
        CE,

        /// <remarks/>
        DF,

        /// <remarks/>
        ES,

        /// <remarks/>
        GO,

        /// <remarks/>
        MA,

        /// <remarks/>
        MG,

        /// <remarks/>
        MS,

        /// <remarks/>
        MT,

        /// <remarks/>
        PA,

        /// <remarks/>
        PB,

        /// <remarks/>
        PE,

        /// <remarks/>
        PI,

        /// <remarks/>
        PR,

        /// <remarks/>
        RJ,

        /// <remarks/>
        RN,

        /// <remarks/>
        RO,

        /// <remarks/>
        RR,

        /// <remarks/>
        RS,

        /// <remarks/>
        SC,

        /// <remarks/>
        SE,

        /// <remarks/>
        SP,

        /// <remarks/>
        TO,

        /// <remarks/>
        EX,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TIpi : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string cNPJProdField;

        private string cSeloField;

        private string qSeloField;

        private string cEnqField;

        private object itemField;

        /// <remarks/>
        public string CNPJProd
        {
            get
            {
                return this.cNPJProdField;
            }
            set
            {
                this.cNPJProdField = value;
                this.RaisePropertyChanged("CNPJProd");
            }
        }

        /// <remarks/>
        public string cSelo
        {
            get
            {
                return this.cSeloField;
            }
            set
            {
                this.cSeloField = value;
                this.RaisePropertyChanged("cSelo");
            }
        }

        /// <remarks/>
        public string qSelo
        {
            get
            {
                return this.qSeloField;
            }
            set
            {
                this.qSeloField = value;
                this.RaisePropertyChanged("qSelo");
            }
        }

        /// <remarks/>
        public string cEnq
        {
            get
            {
                return this.cEnqField;
            }
            set
            {
                this.cEnqField = value;
                this.RaisePropertyChanged("cEnq");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IPINT", typeof(TIpiIPINT))]
        [System.Xml.Serialization.XmlElementAttribute("IPITrib", typeof(TIpiIPITrib))]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TIpiIPINT : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TIpiIPINTCST cSTField;

        /// <remarks/>
        public TIpiIPINTCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TIpiIPINTCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("51")]
        Item51,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("52")]
        Item52,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("53")]
        Item53,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("54")]
        Item54,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("55")]
        Item55,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TIpiIPITrib : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TIpiIPITribCST cSTField;

        private string[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        private string vIPIField;

        /// <remarks/>
        public TIpiIPITribCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("pIPI", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("qUnid", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vBC", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vUnid", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public string[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
                this.RaisePropertyChanged("ItemsElementName");
            }
        }

        /// <remarks/>
        public string vIPI
        {
            get
            {
                return this.vIPIField;
            }
            set
            {
                this.vIPIField = value;
                this.RaisePropertyChanged("vIPI");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TIpiIPITribCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("00")]
        Item00,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("49")]
        Item49,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("50")]
        Item50,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("99")]
        Item99,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        pIPI,

        /// <remarks/>
        qUnid,

        /// <remarks/>
        vBC,

        /// <remarks/>
        vUnid,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TLocal : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string itemField;

        private ItemChoiceType4 itemElementNameField;

        private string xNomeField;

        private string xLgrField;

        private string nroField;

        private string xCplField;

        private string xBairroField;

        private string cMunField;

        private string xMunField;

        private TUf ufField;

        private string cEPField;

        private string cPaisField;

        private string xPaisField;

        private string foneField;

        private string emailField;

        private string ieField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CNPJ", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CPF", typeof(string))]
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
        public ItemChoiceType4 ItemElementName
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

        /// <remarks/>
        public string xNome
        {
            get
            {
                return this.xNomeField;
            }
            set
            {
                this.xNomeField = value;
                this.RaisePropertyChanged("xNome");
            }
        }

        /// <remarks/>
        public string xLgr
        {
            get
            {
                return this.xLgrField;
            }
            set
            {
                this.xLgrField = value;
                this.RaisePropertyChanged("xLgr");
            }
        }

        /// <remarks/>
        public string nro
        {
            get
            {
                return this.nroField;
            }
            set
            {
                this.nroField = value;
                this.RaisePropertyChanged("nro");
            }
        }

        /// <remarks/>
        public string xCpl
        {
            get
            {
                return this.xCplField;
            }
            set
            {
                this.xCplField = value;
                this.RaisePropertyChanged("xCpl");
            }
        }

        /// <remarks/>
        public string xBairro
        {
            get
            {
                return this.xBairroField;
            }
            set
            {
                this.xBairroField = value;
                this.RaisePropertyChanged("xBairro");
            }
        }

        /// <remarks/>
        public string cMun
        {
            get
            {
                return this.cMunField;
            }
            set
            {
                this.cMunField = value;
                this.RaisePropertyChanged("cMun");
            }
        }

        /// <remarks/>
        public string xMun
        {
            get
            {
                return this.xMunField;
            }
            set
            {
                this.xMunField = value;
                this.RaisePropertyChanged("xMun");
            }
        }

        /// <remarks/>
        public TUf UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
                this.RaisePropertyChanged("UF");
            }
        }

        /// <remarks/>
        public string CEP
        {
            get
            {
                return this.cEPField;
            }
            set
            {
                this.cEPField = value;
                this.RaisePropertyChanged("CEP");
            }
        }

        /// <remarks/>
        public string cPais
        {
            get
            {
                return this.cPaisField;
            }
            set
            {
                this.cPaisField = value;
                this.RaisePropertyChanged("cPais");
            }
        }

        /// <remarks/>
        public string xPais
        {
            get
            {
                return this.xPaisField;
            }
            set
            {
                this.xPaisField = value;
                this.RaisePropertyChanged("xPais");
            }
        }

        /// <remarks/>
        public string fone
        {
            get
            {
                return this.foneField;
            }
            set
            {
                this.foneField = value;
                this.RaisePropertyChanged("fone");
            }
        }

        /// <remarks/>
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
                this.RaisePropertyChanged("email");
            }
        }

        /// <remarks/>
        public string IE
        {
            get
            {
                return this.ieField;
            }
            set
            {
                this.ieField = value;
                this.RaisePropertyChanged("IE");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemChoiceType4
    {

        /// <remarks/>
        CNPJ,

        /// <remarks/>
        CPF,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TEndereco : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string xLgrField;

        private string nroField;

        private string xCplField;

        private string xBairroField;

        private string cMunField;

        private string xMunField;

        private TUf ufField;

        private string cEPField;

        private string cPaisField;

        private string xPaisField;

        private string foneField;

        /// <remarks/>
        public string xLgr
        {
            get
            {
                return this.xLgrField;
            }
            set
            {
                this.xLgrField = value;
                this.RaisePropertyChanged("xLgr");
            }
        }

        /// <remarks/>
        public string nro
        {
            get
            {
                return this.nroField;
            }
            set
            {
                this.nroField = value;
                this.RaisePropertyChanged("nro");
            }
        }

        /// <remarks/>
        public string xCpl
        {
            get
            {
                return this.xCplField;
            }
            set
            {
                this.xCplField = value;
                this.RaisePropertyChanged("xCpl");
            }
        }

        /// <remarks/>
        public string xBairro
        {
            get
            {
                return this.xBairroField;
            }
            set
            {
                this.xBairroField = value;
                this.RaisePropertyChanged("xBairro");
            }
        }

        /// <remarks/>
        public string cMun
        {
            get
            {
                return this.cMunField;
            }
            set
            {
                this.cMunField = value;
                this.RaisePropertyChanged("cMun");
            }
        }

        /// <remarks/>
        public string xMun
        {
            get
            {
                return this.xMunField;
            }
            set
            {
                this.xMunField = value;
                this.RaisePropertyChanged("xMun");
            }
        }

        /// <remarks/>
        public TUf UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
                this.RaisePropertyChanged("UF");
            }
        }

        /// <remarks/>
        public string CEP
        {
            get
            {
                return this.cEPField;
            }
            set
            {
                this.cEPField = value;
                this.RaisePropertyChanged("CEP");
            }
        }

        /// <remarks/>
        public string cPais
        {
            get
            {
                return this.cPaisField;
            }
            set
            {
                this.cPaisField = value;
                this.RaisePropertyChanged("cPais");
            }
        }

        /// <remarks/>
        public string xPais
        {
            get
            {
                return this.xPaisField;
            }
            set
            {
                this.xPaisField = value;
                this.RaisePropertyChanged("xPais");
            }
        }

        /// <remarks/>
        public string fone
        {
            get
            {
                return this.foneField;
            }
            set
            {
                this.foneField = value;
                this.RaisePropertyChanged("fone");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TEnderEmi : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string xLgrField;

        private string nroField;

        private string xCplField;

        private string xBairroField;

        private string cMunField;

        private string xMunField;

        private TUfEmi ufField;

        private string cEPField;

        private TEnderEmiCPais cPaisField;

        private bool cPaisFieldSpecified;

        private TEnderEmiXPais xPaisField;

        private bool xPaisFieldSpecified;

        private string foneField;

        /// <remarks/>
        public string xLgr
        {
            get
            {
                return this.xLgrField;
            }
            set
            {
                this.xLgrField = value;
                this.RaisePropertyChanged("xLgr");
            }
        }

        /// <remarks/>
        public string nro
        {
            get
            {
                return this.nroField;
            }
            set
            {
                this.nroField = value;
                this.RaisePropertyChanged("nro");
            }
        }

        /// <remarks/>
        public string xCpl
        {
            get
            {
                return this.xCplField;
            }
            set
            {
                this.xCplField = value;
                this.RaisePropertyChanged("xCpl");
            }
        }

        /// <remarks/>
        public string xBairro
        {
            get
            {
                return this.xBairroField;
            }
            set
            {
                this.xBairroField = value;
                this.RaisePropertyChanged("xBairro");
            }
        }

        /// <remarks/>
        public string cMun
        {
            get
            {
                return this.cMunField;
            }
            set
            {
                this.cMunField = value;
                this.RaisePropertyChanged("cMun");
            }
        }

        /// <remarks/>
        public string xMun
        {
            get
            {
                return this.xMunField;
            }
            set
            {
                this.xMunField = value;
                this.RaisePropertyChanged("xMun");
            }
        }

        /// <remarks/>
        public TUfEmi UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
                this.RaisePropertyChanged("UF");
            }
        }

        /// <remarks/>
        public string CEP
        {
            get
            {
                return this.cEPField;
            }
            set
            {
                this.cEPField = value;
                this.RaisePropertyChanged("CEP");
            }
        }

        /// <remarks/>
        public TEnderEmiCPais cPais
        {
            get
            {
                return this.cPaisField;
            }
            set
            {
                this.cPaisField = value;
                this.RaisePropertyChanged("cPais");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cPaisSpecified
        {
            get
            {
                return this.cPaisFieldSpecified;
            }
            set
            {
                this.cPaisFieldSpecified = value;
                this.RaisePropertyChanged("cPaisSpecified");
            }
        }

        /// <remarks/>
        public TEnderEmiXPais xPais
        {
            get
            {
                return this.xPaisField;
            }
            set
            {
                this.xPaisField = value;
                this.RaisePropertyChanged("xPais");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool xPaisSpecified
        {
            get
            {
                return this.xPaisFieldSpecified;
            }
            set
            {
                this.xPaisFieldSpecified = value;
                this.RaisePropertyChanged("xPaisSpecified");
            }
        }

        /// <remarks/>
        public string fone
        {
            get
            {
                return this.foneField;
            }
            set
            {
                this.foneField = value;
                this.RaisePropertyChanged("fone");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TUfEmi
    {

        /// <remarks/>
        AC,

        /// <remarks/>
        AL,

        /// <remarks/>
        AM,

        /// <remarks/>
        AP,

        /// <remarks/>
        BA,

        /// <remarks/>
        CE,

        /// <remarks/>
        DF,

        /// <remarks/>
        ES,

        /// <remarks/>
        GO,

        /// <remarks/>
        MA,

        /// <remarks/>
        MG,

        /// <remarks/>
        MS,

        /// <remarks/>
        MT,

        /// <remarks/>
        PA,

        /// <remarks/>
        PB,

        /// <remarks/>
        PE,

        /// <remarks/>
        PI,

        /// <remarks/>
        PR,

        /// <remarks/>
        RJ,

        /// <remarks/>
        RN,

        /// <remarks/>
        RO,

        /// <remarks/>
        RR,

        /// <remarks/>
        RS,

        /// <remarks/>
        SC,

        /// <remarks/>
        SE,

        /// <remarks/>
        SP,

        /// <remarks/>
        TO,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TEnderEmiCPais
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1058")]
        Item1058,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TEnderEmiXPais
    {

        /// <remarks/>
        Brasil,

        /// <remarks/>
        BRASIL,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeIdeNFrefRefNF : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TCodUfIBGE cUFField;

        private string aAMMField;

        private string cNPJField;

        private TNFeInfNFeIdeNFrefRefNFMod modField;

        private string serieField;

        private string nNFField;

        /// <remarks/>
        public TCodUfIBGE cUF
        {
            get
            {
                return this.cUFField;
            }
            set
            {
                this.cUFField = value;
                this.RaisePropertyChanged("cUF");
            }
        }

        /// <remarks/>
        public string AAMM
        {
            get
            {
                return this.aAMMField;
            }
            set
            {
                this.aAMMField = value;
                this.RaisePropertyChanged("AAMM");
            }
        }

        /// <remarks/>
        public string CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
                this.RaisePropertyChanged("CNPJ");
            }
        }

        /// <remarks/>
        public TNFeInfNFeIdeNFrefRefNFMod mod
        {
            get
            {
                return this.modField;
            }
            set
            {
                this.modField = value;
                this.RaisePropertyChanged("mod");
            }
        }

        /// <remarks/>
        public string serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
                this.RaisePropertyChanged("serie");
            }
        }

        /// <remarks/>
        public string nNF
        {
            get
            {
                return this.nNFField;
            }
            set
            {
                this.nNFField = value;
                this.RaisePropertyChanged("nNF");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeIdeNFrefRefNFMod
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeIdeNFrefRefNFP : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TCodUfIBGE cUFField;

        private string aAMMField;

        private string itemField;

        private ItemChoiceType itemElementNameField;

        private string ieField;

        private TNFeInfNFeIdeNFrefRefNFPMod modField;

        private string serieField;

        private string nNFField;

        /// <remarks/>
        public TCodUfIBGE cUF
        {
            get
            {
                return this.cUFField;
            }
            set
            {
                this.cUFField = value;
                this.RaisePropertyChanged("cUF");
            }
        }

        /// <remarks/>
        public string AAMM
        {
            get
            {
                return this.aAMMField;
            }
            set
            {
                this.aAMMField = value;
                this.RaisePropertyChanged("AAMM");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CNPJ", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CPF", typeof(string))]
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

        /// <remarks/>
        public string IE
        {
            get
            {
                return this.ieField;
            }
            set
            {
                this.ieField = value;
                this.RaisePropertyChanged("IE");
            }
        }

        /// <remarks/>
        public TNFeInfNFeIdeNFrefRefNFPMod mod
        {
            get
            {
                return this.modField;
            }
            set
            {
                this.modField = value;
                this.RaisePropertyChanged("mod");
            }
        }

        /// <remarks/>
        public string serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
                this.RaisePropertyChanged("serie");
            }
        }

        /// <remarks/>
        public string nNF
        {
            get
            {
                return this.nNFField;
            }
            set
            {
                this.nNFField = value;
                this.RaisePropertyChanged("nNF");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemChoiceType
    {

        /// <remarks/>
        CNPJ,

        /// <remarks/>
        CPF,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeIdeNFrefRefNFPMod
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemChoiceType1
    {

        /// <remarks/>
        refCTe,

        /// <remarks/>
        refECF,

        /// <remarks/>
        refNF,

        /// <remarks/>
        refNFP,

        /// <remarks/>
        refNFe,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeEmit : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string itemField;

        private ItemChoiceType2 itemElementNameField;

        private string xNomeField;

        private string xFantField;

        private TEnderEmi enderEmitField;

        private string ieField;

        private string iESTField;

        private string imField;

        private string cNAEField;

        private TNFeInfNFeEmitCRT cRTField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CNPJ", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CPF", typeof(string))]
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
        public ItemChoiceType2 ItemElementName
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

        /// <remarks/>
        public string xNome
        {
            get
            {
                return this.xNomeField;
            }
            set
            {
                this.xNomeField = value;
                this.RaisePropertyChanged("xNome");
            }
        }

        /// <remarks/>
        public string xFant
        {
            get
            {
                return this.xFantField;
            }
            set
            {
                this.xFantField = value;
                this.RaisePropertyChanged("xFant");
            }
        }

        /// <remarks/>
        public TEnderEmi enderEmit
        {
            get
            {
                return this.enderEmitField;
            }
            set
            {
                this.enderEmitField = value;
                this.RaisePropertyChanged("enderEmit");
            }
        }

        /// <remarks/>
        public string IE
        {
            get
            {
                return this.ieField;
            }
            set
            {
                this.ieField = value;
                this.RaisePropertyChanged("IE");
            }
        }

        /// <remarks/>
        public string IEST
        {
            get
            {
                return this.iESTField;
            }
            set
            {
                this.iESTField = value;
                this.RaisePropertyChanged("IEST");
            }
        }

        /// <remarks/>
        public string IM
        {
            get
            {
                return this.imField;
            }
            set
            {
                this.imField = value;
                this.RaisePropertyChanged("IM");
            }
        }

        /// <remarks/>
        public string CNAE
        {
            get
            {
                return this.cNAEField;
            }
            set
            {
                this.cNAEField = value;
                this.RaisePropertyChanged("CNAE");
            }
        }

        /// <remarks/>
        public TNFeInfNFeEmitCRT CRT
        {
            get
            {
                return this.cRTField;
            }
            set
            {
                this.cRTField = value;
                this.RaisePropertyChanged("CRT");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemChoiceType2
    {

        /// <remarks/>
        CNPJ,

        /// <remarks/>
        CPF,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeEmitCRT
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeAvulsa : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string cNPJField;

        private string xOrgaoField;

        private string matrField;

        private string xAgenteField;

        private string foneField;

        private TUfEmi ufField;

        private string nDARField;

        private string dEmiField;

        private string vDARField;

        private string repEmiField;

        private string dPagField;

        /// <remarks/>
        public string CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
                this.RaisePropertyChanged("CNPJ");
            }
        }

        /// <remarks/>
        public string xOrgao
        {
            get
            {
                return this.xOrgaoField;
            }
            set
            {
                this.xOrgaoField = value;
                this.RaisePropertyChanged("xOrgao");
            }
        }

        /// <remarks/>
        public string matr
        {
            get
            {
                return this.matrField;
            }
            set
            {
                this.matrField = value;
                this.RaisePropertyChanged("matr");
            }
        }

        /// <remarks/>
        public string xAgente
        {
            get
            {
                return this.xAgenteField;
            }
            set
            {
                this.xAgenteField = value;
                this.RaisePropertyChanged("xAgente");
            }
        }

        /// <remarks/>
        public string fone
        {
            get
            {
                return this.foneField;
            }
            set
            {
                this.foneField = value;
                this.RaisePropertyChanged("fone");
            }
        }

        /// <remarks/>
        public TUfEmi UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
                this.RaisePropertyChanged("UF");
            }
        }

        /// <remarks/>
        public string nDAR
        {
            get
            {
                return this.nDARField;
            }
            set
            {
                this.nDARField = value;
                this.RaisePropertyChanged("nDAR");
            }
        }

        /// <remarks/>
        public string dEmi
        {
            get
            {
                return this.dEmiField;
            }
            set
            {
                this.dEmiField = value;
                this.RaisePropertyChanged("dEmi");
            }
        }

        /// <remarks/>
        public string vDAR
        {
            get
            {
                return this.vDARField;
            }
            set
            {
                this.vDARField = value;
                this.RaisePropertyChanged("vDAR");
            }
        }

        /// <remarks/>
        public string repEmi
        {
            get
            {
                return this.repEmiField;
            }
            set
            {
                this.repEmiField = value;
                this.RaisePropertyChanged("repEmi");
            }
        }

        /// <remarks/>
        public string dPag
        {
            get
            {
                return this.dPagField;
            }
            set
            {
                this.dPagField = value;
                this.RaisePropertyChanged("dPag");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDest : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string itemField;

        private ItemChoiceType3 itemElementNameField;

        private string xNomeField;

        private TEndereco enderDestField;

        private TNFeInfNFeDestIndIEDest indIEDestField;

        private string ieField;

        private string iSUFField;

        private string imField;

        private string emailField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CNPJ", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CPF", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("idEstrangeiro", typeof(string))]
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
        public ItemChoiceType3 ItemElementName
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

        /// <remarks/>
        public string xNome
        {
            get
            {
                return this.xNomeField;
            }
            set
            {
                this.xNomeField = value;
                this.RaisePropertyChanged("xNome");
            }
        }

        /// <remarks/>
        public TEndereco enderDest
        {
            get
            {
                return this.enderDestField;
            }
            set
            {
                this.enderDestField = value;
                this.RaisePropertyChanged("enderDest");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDestIndIEDest indIEDest
        {
            get
            {
                return this.indIEDestField;
            }
            set
            {
                this.indIEDestField = value;
                this.RaisePropertyChanged("indIEDest");
            }
        }

        /// <remarks/>
        public string IE
        {
            get
            {
                return this.ieField;
            }
            set
            {
                this.ieField = value;
                this.RaisePropertyChanged("IE");
            }
        }

        /// <remarks/>
        public string ISUF
        {
            get
            {
                return this.iSUFField;
            }
            set
            {
                this.iSUFField = value;
                this.RaisePropertyChanged("ISUF");
            }
        }

        /// <remarks/>
        public string IM
        {
            get
            {
                return this.imField;
            }
            set
            {
                this.imField = value;
                this.RaisePropertyChanged("IM");
            }
        }

        /// <remarks/>
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
                this.RaisePropertyChanged("email");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemChoiceType3
    {

        /// <remarks/>
        CNPJ,

        /// <remarks/>
        CPF,

        /// <remarks/>
        idEstrangeiro,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDestIndIEDest
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeAutXML : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string itemField;

        private ItemChoiceType5 itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CNPJ", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CPF", typeof(string))]
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
        public ItemChoiceType5 ItemElementName
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemChoiceType5
    {

        /// <remarks/>
        CNPJ,

        /// <remarks/>
        CPF,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDet : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetProd prodField;

        private TNFeInfNFeDetImposto impostoField;

        private TNFeInfNFeDetImpostoDevol impostoDevolField;

        private string infAdProdField;

        private string nItemField;

        /// <remarks/>
        public TNFeInfNFeDetProd prod
        {
            get
            {
                return this.prodField;
            }
            set
            {
                this.prodField = value;
                this.RaisePropertyChanged("prod");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImposto imposto
        {
            get
            {
                return this.impostoField;
            }
            set
            {
                this.impostoField = value;
                this.RaisePropertyChanged("imposto");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoDevol impostoDevol
        {
            get
            {
                return this.impostoDevolField;
            }
            set
            {
                this.impostoDevolField = value;
                this.RaisePropertyChanged("impostoDevol");
            }
        }

        /// <remarks/>
        public string infAdProd
        {
            get
            {
                return this.infAdProdField;
            }
            set
            {
                this.infAdProdField = value;
                this.RaisePropertyChanged("infAdProd");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nItem
        {
            get
            {
                return this.nItemField;
            }
            set
            {
                this.nItemField = value;
                this.RaisePropertyChanged("nItem");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProd : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string cProdField;

        private string cEANField;

        private string xProdField;

        private string nCMField;

        private string[] nVEField;

        private string cESTField;

        private TNFeInfNFeDetProdIndEscala indEscalaField;

        private bool indEscalaFieldSpecified;

        private string cNPJFabField;

        private string cBenefField;

        private string eXTIPIField;

        private string cFOPField;

        private string uComField;

        private string qComField;

        private string vUnComField;

        private string vProdField;

        private string cEANTribField;

        private string uTribField;

        private string qTribField;

        private string vUnTribField;

        private string vFreteField;

        private string vSegField;

        private string vDescField;

        private string vOutroField;

        private TNFeInfNFeDetProdIndTot indTotField;

        private TNFeInfNFeDetProdDI[] diField;

        private TNFeInfNFeDetProdDetExport[] detExportField;

        private string xPedField;

        private string nItemPedField;

        private string nFCIField;

        private TNFeInfNFeDetProdRastro[] rastroField;

        private object[] itemsField;

        /// <remarks/>
        public string cProd
        {
            get
            {
                return this.cProdField;
            }
            set
            {
                this.cProdField = value;
                this.RaisePropertyChanged("cProd");
            }
        }

        /// <remarks/>
        public string cEAN
        {
            get
            {
                return this.cEANField;
            }
            set
            {
                this.cEANField = value;
                this.RaisePropertyChanged("cEAN");
            }
        }

        /// <remarks/>
        public string xProd
        {
            get
            {
                return this.xProdField;
            }
            set
            {
                this.xProdField = value;
                this.RaisePropertyChanged("xProd");
            }
        }

        /// <remarks/>
        public string NCM
        {
            get
            {
                return this.nCMField;
            }
            set
            {
                this.nCMField = value;
                this.RaisePropertyChanged("NCM");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("NVE")]
        public string[] NVE
        {
            get
            {
                return this.nVEField;
            }
            set
            {
                this.nVEField = value;
                this.RaisePropertyChanged("NVE");
            }
        }

        /// <remarks/>
        public string CEST
        {
            get
            {
                return this.cESTField;
            }
            set
            {
                this.cESTField = value;
                this.RaisePropertyChanged("CEST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetProdIndEscala indEscala
        {
            get
            {
                return this.indEscalaField;
            }
            set
            {
                this.indEscalaField = value;
                this.RaisePropertyChanged("indEscala");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool indEscalaSpecified
        {
            get
            {
                return this.indEscalaFieldSpecified;
            }
            set
            {
                this.indEscalaFieldSpecified = value;
                this.RaisePropertyChanged("indEscalaSpecified");
            }
        }

        /// <remarks/>
        public string CNPJFab
        {
            get
            {
                return this.cNPJFabField;
            }
            set
            {
                this.cNPJFabField = value;
                this.RaisePropertyChanged("CNPJFab");
            }
        }

        /// <remarks/>
        public string cBenef
        {
            get
            {
                return this.cBenefField;
            }
            set
            {
                this.cBenefField = value;
                this.RaisePropertyChanged("cBenef");
            }
        }

        /// <remarks/>
        public string EXTIPI
        {
            get
            {
                return this.eXTIPIField;
            }
            set
            {
                this.eXTIPIField = value;
                this.RaisePropertyChanged("EXTIPI");
            }
        }

        /// <remarks/>
        public string CFOP
        {
            get
            {
                return this.cFOPField;
            }
            set
            {
                this.cFOPField = value;
                this.RaisePropertyChanged("CFOP");
            }
        }

        /// <remarks/>
        public string uCom
        {
            get
            {
                return this.uComField;
            }
            set
            {
                this.uComField = value;
                this.RaisePropertyChanged("uCom");
            }
        }

        /// <remarks/>
        public string qCom
        {
            get
            {
                return this.qComField;
            }
            set
            {
                this.qComField = value;
                this.RaisePropertyChanged("qCom");
            }
        }

        /// <remarks/>
        public string vUnCom
        {
            get
            {
                return this.vUnComField;
            }
            set
            {
                this.vUnComField = value;
                this.RaisePropertyChanged("vUnCom");
            }
        }

        /// <remarks/>
        public string vProd
        {
            get
            {
                return this.vProdField;
            }
            set
            {
                this.vProdField = value;
                this.RaisePropertyChanged("vProd");
            }
        }

        /// <remarks/>
        public string cEANTrib
        {
            get
            {
                return this.cEANTribField;
            }
            set
            {
                this.cEANTribField = value;
                this.RaisePropertyChanged("cEANTrib");
            }
        }

        /// <remarks/>
        public string uTrib
        {
            get
            {
                return this.uTribField;
            }
            set
            {
                this.uTribField = value;
                this.RaisePropertyChanged("uTrib");
            }
        }

        /// <remarks/>
        public string qTrib
        {
            get
            {
                return this.qTribField;
            }
            set
            {
                this.qTribField = value;
                this.RaisePropertyChanged("qTrib");
            }
        }

        /// <remarks/>
        public string vUnTrib
        {
            get
            {
                return this.vUnTribField;
            }
            set
            {
                this.vUnTribField = value;
                this.RaisePropertyChanged("vUnTrib");
            }
        }

        /// <remarks/>
        public string vFrete
        {
            get
            {
                return this.vFreteField;
            }
            set
            {
                this.vFreteField = value;
                this.RaisePropertyChanged("vFrete");
            }
        }

        /// <remarks/>
        public string vSeg
        {
            get
            {
                return this.vSegField;
            }
            set
            {
                this.vSegField = value;
                this.RaisePropertyChanged("vSeg");
            }
        }

        /// <remarks/>
        public string vDesc
        {
            get
            {
                return this.vDescField;
            }
            set
            {
                this.vDescField = value;
                this.RaisePropertyChanged("vDesc");
            }
        }

        /// <remarks/>
        public string vOutro
        {
            get
            {
                return this.vOutroField;
            }
            set
            {
                this.vOutroField = value;
                this.RaisePropertyChanged("vOutro");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetProdIndTot indTot
        {
            get
            {
                return this.indTotField;
            }
            set
            {
                this.indTotField = value;
                this.RaisePropertyChanged("indTot");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DI")]
        public TNFeInfNFeDetProdDI[] DI
        {
            get
            {
                return this.diField;
            }
            set
            {
                this.diField = value;
                this.RaisePropertyChanged("DI");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("detExport")]
        public TNFeInfNFeDetProdDetExport[] detExport
        {
            get
            {
                return this.detExportField;
            }
            set
            {
                this.detExportField = value;
                this.RaisePropertyChanged("detExport");
            }
        }

        /// <remarks/>
        public string xPed
        {
            get
            {
                return this.xPedField;
            }
            set
            {
                this.xPedField = value;
                this.RaisePropertyChanged("xPed");
            }
        }

        /// <remarks/>
        public string nItemPed
        {
            get
            {
                return this.nItemPedField;
            }
            set
            {
                this.nItemPedField = value;
                this.RaisePropertyChanged("nItemPed");
            }
        }

        /// <remarks/>
        public string nFCI
        {
            get
            {
                return this.nFCIField;
            }
            set
            {
                this.nFCIField = value;
                this.RaisePropertyChanged("nFCI");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("rastro")]
        public TNFeInfNFeDetProdRastro[] rastro
        {
            get
            {
                return this.rastroField;
            }
            set
            {
                this.rastroField = value;
                this.RaisePropertyChanged("rastro");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("arma", typeof(TNFeInfNFeDetProdArma))]
        [System.Xml.Serialization.XmlElementAttribute("comb", typeof(TNFeInfNFeDetProdComb))]
        [System.Xml.Serialization.XmlElementAttribute("med", typeof(TNFeInfNFeDetProdMed))]
        [System.Xml.Serialization.XmlElementAttribute("nRECOPI", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("veicProd", typeof(TNFeInfNFeDetProdVeicProd))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetProdIndEscala
    {

        /// <remarks/>
        S,

        /// <remarks/>
        N,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetProdIndTot
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdDI : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nDIField;

        private string dDIField;

        private string xLocDesembField;

        private TUfEmi uFDesembField;

        private string dDesembField;

        private TNFeInfNFeDetProdDITpViaTransp tpViaTranspField;

        private string vAFRMMField;

        private TNFeInfNFeDetProdDITpIntermedio tpIntermedioField;

        private string cNPJField;

        private TUfEmi uFTerceiroField;

        private bool uFTerceiroFieldSpecified;

        private string cExportadorField;

        private TNFeInfNFeDetProdDIAdi[] adiField;

        /// <remarks/>
        public string nDI
        {
            get
            {
                return this.nDIField;
            }
            set
            {
                this.nDIField = value;
                this.RaisePropertyChanged("nDI");
            }
        }

        /// <remarks/>
        public string dDI
        {
            get
            {
                return this.dDIField;
            }
            set
            {
                this.dDIField = value;
                this.RaisePropertyChanged("dDI");
            }
        }

        /// <remarks/>
        public string xLocDesemb
        {
            get
            {
                return this.xLocDesembField;
            }
            set
            {
                this.xLocDesembField = value;
                this.RaisePropertyChanged("xLocDesemb");
            }
        }

        /// <remarks/>
        public TUfEmi UFDesemb
        {
            get
            {
                return this.uFDesembField;
            }
            set
            {
                this.uFDesembField = value;
                this.RaisePropertyChanged("UFDesemb");
            }
        }

        /// <remarks/>
        public string dDesemb
        {
            get
            {
                return this.dDesembField;
            }
            set
            {
                this.dDesembField = value;
                this.RaisePropertyChanged("dDesemb");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetProdDITpViaTransp tpViaTransp
        {
            get
            {
                return this.tpViaTranspField;
            }
            set
            {
                this.tpViaTranspField = value;
                this.RaisePropertyChanged("tpViaTransp");
            }
        }

        /// <remarks/>
        public string vAFRMM
        {
            get
            {
                return this.vAFRMMField;
            }
            set
            {
                this.vAFRMMField = value;
                this.RaisePropertyChanged("vAFRMM");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetProdDITpIntermedio tpIntermedio
        {
            get
            {
                return this.tpIntermedioField;
            }
            set
            {
                this.tpIntermedioField = value;
                this.RaisePropertyChanged("tpIntermedio");
            }
        }

        /// <remarks/>
        public string CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
                this.RaisePropertyChanged("CNPJ");
            }
        }

        /// <remarks/>
        public TUfEmi UFTerceiro
        {
            get
            {
                return this.uFTerceiroField;
            }
            set
            {
                this.uFTerceiroField = value;
                this.RaisePropertyChanged("UFTerceiro");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UFTerceiroSpecified
        {
            get
            {
                return this.uFTerceiroFieldSpecified;
            }
            set
            {
                this.uFTerceiroFieldSpecified = value;
                this.RaisePropertyChanged("UFTerceiroSpecified");
            }
        }

        /// <remarks/>
        public string cExportador
        {
            get
            {
                return this.cExportadorField;
            }
            set
            {
                this.cExportadorField = value;
                this.RaisePropertyChanged("cExportador");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("adi")]
        public TNFeInfNFeDetProdDIAdi[] adi
        {
            get
            {
                return this.adiField;
            }
            set
            {
                this.adiField = value;
                this.RaisePropertyChanged("adi");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetProdDITpViaTransp
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Item7,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8")]
        Item8,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetProdDITpIntermedio
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdDIAdi : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nAdicaoField;

        private string nSeqAdicField;

        private string cFabricanteField;

        private string vDescDIField;

        private string nDrawField;

        /// <remarks/>
        public string nAdicao
        {
            get
            {
                return this.nAdicaoField;
            }
            set
            {
                this.nAdicaoField = value;
                this.RaisePropertyChanged("nAdicao");
            }
        }

        /// <remarks/>
        public string nSeqAdic
        {
            get
            {
                return this.nSeqAdicField;
            }
            set
            {
                this.nSeqAdicField = value;
                this.RaisePropertyChanged("nSeqAdic");
            }
        }

        /// <remarks/>
        public string cFabricante
        {
            get
            {
                return this.cFabricanteField;
            }
            set
            {
                this.cFabricanteField = value;
                this.RaisePropertyChanged("cFabricante");
            }
        }

        /// <remarks/>
        public string vDescDI
        {
            get
            {
                return this.vDescDIField;
            }
            set
            {
                this.vDescDIField = value;
                this.RaisePropertyChanged("vDescDI");
            }
        }

        /// <remarks/>
        public string nDraw
        {
            get
            {
                return this.nDrawField;
            }
            set
            {
                this.nDrawField = value;
                this.RaisePropertyChanged("nDraw");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdDetExport : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nDrawField;

        private TNFeInfNFeDetProdDetExportExportInd exportIndField;

        /// <remarks/>
        public string nDraw
        {
            get
            {
                return this.nDrawField;
            }
            set
            {
                this.nDrawField = value;
                this.RaisePropertyChanged("nDraw");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetProdDetExportExportInd exportInd
        {
            get
            {
                return this.exportIndField;
            }
            set
            {
                this.exportIndField = value;
                this.RaisePropertyChanged("exportInd");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdDetExportExportInd : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nREField;

        private string chNFeField;

        private string qExportField;

        /// <remarks/>
        public string nRE
        {
            get
            {
                return this.nREField;
            }
            set
            {
                this.nREField = value;
                this.RaisePropertyChanged("nRE");
            }
        }

        /// <remarks/>
        public string chNFe
        {
            get
            {
                return this.chNFeField;
            }
            set
            {
                this.chNFeField = value;
                this.RaisePropertyChanged("chNFe");
            }
        }

        /// <remarks/>
        public string qExport
        {
            get
            {
                return this.qExportField;
            }
            set
            {
                this.qExportField = value;
                this.RaisePropertyChanged("qExport");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdRastro : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nLoteField;

        private string qLoteField;

        private string dFabField;

        private string dValField;

        private string cAgregField;

        /// <remarks/>
        public string nLote
        {
            get
            {
                return this.nLoteField;
            }
            set
            {
                this.nLoteField = value;
                this.RaisePropertyChanged("nLote");
            }
        }

        /// <remarks/>
        public string qLote
        {
            get
            {
                return this.qLoteField;
            }
            set
            {
                this.qLoteField = value;
                this.RaisePropertyChanged("qLote");
            }
        }

        /// <remarks/>
        public string dFab
        {
            get
            {
                return this.dFabField;
            }
            set
            {
                this.dFabField = value;
                this.RaisePropertyChanged("dFab");
            }
        }

        /// <remarks/>
        public string dVal
        {
            get
            {
                return this.dValField;
            }
            set
            {
                this.dValField = value;
                this.RaisePropertyChanged("dVal");
            }
        }

        /// <remarks/>
        public string cAgreg
        {
            get
            {
                return this.cAgregField;
            }
            set
            {
                this.cAgregField = value;
                this.RaisePropertyChanged("cAgreg");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdArma : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetProdArmaTpArma tpArmaField;

        private string nSerieField;

        private string nCanoField;

        private string descrField;

        /// <remarks/>
        public TNFeInfNFeDetProdArmaTpArma tpArma
        {
            get
            {
                return this.tpArmaField;
            }
            set
            {
                this.tpArmaField = value;
                this.RaisePropertyChanged("tpArma");
            }
        }

        /// <remarks/>
        public string nSerie
        {
            get
            {
                return this.nSerieField;
            }
            set
            {
                this.nSerieField = value;
                this.RaisePropertyChanged("nSerie");
            }
        }

        /// <remarks/>
        public string nCano
        {
            get
            {
                return this.nCanoField;
            }
            set
            {
                this.nCanoField = value;
                this.RaisePropertyChanged("nCano");
            }
        }

        /// <remarks/>
        public string descr
        {
            get
            {
                return this.descrField;
            }
            set
            {
                this.descrField = value;
                this.RaisePropertyChanged("descr");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetProdArmaTpArma
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdComb : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string cProdANPField;

        private string descANPField;

        private string pGLPField;

        private string pGNnField;

        private string pGNiField;

        private string vPartField;

        private string cODIFField;

        private string qTempField;

        private TUf uFConsField;

        private TNFeInfNFeDetProdCombCIDE cIDEField;

        private TNFeInfNFeDetProdCombEncerrante encerranteField;

        /// <remarks/>
        public string cProdANP
        {
            get
            {
                return this.cProdANPField;
            }
            set
            {
                this.cProdANPField = value;
                this.RaisePropertyChanged("cProdANP");
            }
        }

        /// <remarks/>
        public string descANP
        {
            get
            {
                return this.descANPField;
            }
            set
            {
                this.descANPField = value;
                this.RaisePropertyChanged("descANP");
            }
        }

        /// <remarks/>
        public string pGLP
        {
            get
            {
                return this.pGLPField;
            }
            set
            {
                this.pGLPField = value;
                this.RaisePropertyChanged("pGLP");
            }
        }

        /// <remarks/>
        public string pGNn
        {
            get
            {
                return this.pGNnField;
            }
            set
            {
                this.pGNnField = value;
                this.RaisePropertyChanged("pGNn");
            }
        }

        /// <remarks/>
        public string pGNi
        {
            get
            {
                return this.pGNiField;
            }
            set
            {
                this.pGNiField = value;
                this.RaisePropertyChanged("pGNi");
            }
        }

        /// <remarks/>
        public string vPart
        {
            get
            {
                return this.vPartField;
            }
            set
            {
                this.vPartField = value;
                this.RaisePropertyChanged("vPart");
            }
        }

        /// <remarks/>
        public string CODIF
        {
            get
            {
                return this.cODIFField;
            }
            set
            {
                this.cODIFField = value;
                this.RaisePropertyChanged("CODIF");
            }
        }

        /// <remarks/>
        public string qTemp
        {
            get
            {
                return this.qTempField;
            }
            set
            {
                this.qTempField = value;
                this.RaisePropertyChanged("qTemp");
            }
        }

        /// <remarks/>
        public TUf UFCons
        {
            get
            {
                return this.uFConsField;
            }
            set
            {
                this.uFConsField = value;
                this.RaisePropertyChanged("UFCons");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetProdCombCIDE CIDE
        {
            get
            {
                return this.cIDEField;
            }
            set
            {
                this.cIDEField = value;
                this.RaisePropertyChanged("CIDE");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetProdCombEncerrante encerrante
        {
            get
            {
                return this.encerranteField;
            }
            set
            {
                this.encerranteField = value;
                this.RaisePropertyChanged("encerrante");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdCombCIDE : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string qBCProdField;

        private string vAliqProdField;

        private string vCIDEField;

        /// <remarks/>
        public string qBCProd
        {
            get
            {
                return this.qBCProdField;
            }
            set
            {
                this.qBCProdField = value;
                this.RaisePropertyChanged("qBCProd");
            }
        }

        /// <remarks/>
        public string vAliqProd
        {
            get
            {
                return this.vAliqProdField;
            }
            set
            {
                this.vAliqProdField = value;
                this.RaisePropertyChanged("vAliqProd");
            }
        }

        /// <remarks/>
        public string vCIDE
        {
            get
            {
                return this.vCIDEField;
            }
            set
            {
                this.vCIDEField = value;
                this.RaisePropertyChanged("vCIDE");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdCombEncerrante : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nBicoField;

        private string nBombaField;

        private string nTanqueField;

        private string vEncIniField;

        private string vEncFinField;

        /// <remarks/>
        public string nBico
        {
            get
            {
                return this.nBicoField;
            }
            set
            {
                this.nBicoField = value;
                this.RaisePropertyChanged("nBico");
            }
        }

        /// <remarks/>
        public string nBomba
        {
            get
            {
                return this.nBombaField;
            }
            set
            {
                this.nBombaField = value;
                this.RaisePropertyChanged("nBomba");
            }
        }

        /// <remarks/>
        public string nTanque
        {
            get
            {
                return this.nTanqueField;
            }
            set
            {
                this.nTanqueField = value;
                this.RaisePropertyChanged("nTanque");
            }
        }

        /// <remarks/>
        public string vEncIni
        {
            get
            {
                return this.vEncIniField;
            }
            set
            {
                this.vEncIniField = value;
                this.RaisePropertyChanged("vEncIni");
            }
        }

        /// <remarks/>
        public string vEncFin
        {
            get
            {
                return this.vEncFinField;
            }
            set
            {
                this.vEncFinField = value;
                this.RaisePropertyChanged("vEncFin");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdMed : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string cProdANVISAField;

        private string xMotivoIsencaoField;

        private string vPMCField;

        /// <remarks/>
        public string cProdANVISA
        {
            get
            {
                return this.cProdANVISAField;
            }
            set
            {
                this.cProdANVISAField = value;
                this.RaisePropertyChanged("cProdANVISA");
            }
        }

        /// <remarks/>
        public string xMotivoIsencao
        {
            get
            {
                return this.xMotivoIsencaoField;
            }
            set
            {
                this.xMotivoIsencaoField = value;
                this.RaisePropertyChanged("xMotivoIsencao");
            }
        }

        /// <remarks/>
        public string vPMC
        {
            get
            {
                return this.vPMCField;
            }
            set
            {
                this.vPMCField = value;
                this.RaisePropertyChanged("vPMC");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetProdVeicProd : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetProdVeicProdTpOp tpOpField;

        private string chassiField;

        private string cCorField;

        private string xCorField;

        private string potField;

        private string cilinField;

        private string pesoLField;

        private string pesoBField;

        private string nSerieField;

        private string tpCombField;

        private string nMotorField;

        private string cMTField;

        private string distField;

        private string anoModField;

        private string anoFabField;

        private string tpPintField;

        private string tpVeicField;

        private string espVeicField;

        private TNFeInfNFeDetProdVeicProdVIN vINField;

        private TNFeInfNFeDetProdVeicProdCondVeic condVeicField;

        private string cModField;

        private string cCorDENATRANField;

        private string lotaField;

        private TNFeInfNFeDetProdVeicProdTpRest tpRestField;

        /// <remarks/>
        public TNFeInfNFeDetProdVeicProdTpOp tpOp
        {
            get
            {
                return this.tpOpField;
            }
            set
            {
                this.tpOpField = value;
                this.RaisePropertyChanged("tpOp");
            }
        }

        /// <remarks/>
        public string chassi
        {
            get
            {
                return this.chassiField;
            }
            set
            {
                this.chassiField = value;
                this.RaisePropertyChanged("chassi");
            }
        }

        /// <remarks/>
        public string cCor
        {
            get
            {
                return this.cCorField;
            }
            set
            {
                this.cCorField = value;
                this.RaisePropertyChanged("cCor");
            }
        }

        /// <remarks/>
        public string xCor
        {
            get
            {
                return this.xCorField;
            }
            set
            {
                this.xCorField = value;
                this.RaisePropertyChanged("xCor");
            }
        }

        /// <remarks/>
        public string pot
        {
            get
            {
                return this.potField;
            }
            set
            {
                this.potField = value;
                this.RaisePropertyChanged("pot");
            }
        }

        /// <remarks/>
        public string cilin
        {
            get
            {
                return this.cilinField;
            }
            set
            {
                this.cilinField = value;
                this.RaisePropertyChanged("cilin");
            }
        }

        /// <remarks/>
        public string pesoL
        {
            get
            {
                return this.pesoLField;
            }
            set
            {
                this.pesoLField = value;
                this.RaisePropertyChanged("pesoL");
            }
        }

        /// <remarks/>
        public string pesoB
        {
            get
            {
                return this.pesoBField;
            }
            set
            {
                this.pesoBField = value;
                this.RaisePropertyChanged("pesoB");
            }
        }

        /// <remarks/>
        public string nSerie
        {
            get
            {
                return this.nSerieField;
            }
            set
            {
                this.nSerieField = value;
                this.RaisePropertyChanged("nSerie");
            }
        }

        /// <remarks/>
        public string tpComb
        {
            get
            {
                return this.tpCombField;
            }
            set
            {
                this.tpCombField = value;
                this.RaisePropertyChanged("tpComb");
            }
        }

        /// <remarks/>
        public string nMotor
        {
            get
            {
                return this.nMotorField;
            }
            set
            {
                this.nMotorField = value;
                this.RaisePropertyChanged("nMotor");
            }
        }

        /// <remarks/>
        public string CMT
        {
            get
            {
                return this.cMTField;
            }
            set
            {
                this.cMTField = value;
                this.RaisePropertyChanged("CMT");
            }
        }

        /// <remarks/>
        public string dist
        {
            get
            {
                return this.distField;
            }
            set
            {
                this.distField = value;
                this.RaisePropertyChanged("dist");
            }
        }

        /// <remarks/>
        public string anoMod
        {
            get
            {
                return this.anoModField;
            }
            set
            {
                this.anoModField = value;
                this.RaisePropertyChanged("anoMod");
            }
        }

        /// <remarks/>
        public string anoFab
        {
            get
            {
                return this.anoFabField;
            }
            set
            {
                this.anoFabField = value;
                this.RaisePropertyChanged("anoFab");
            }
        }

        /// <remarks/>
        public string tpPint
        {
            get
            {
                return this.tpPintField;
            }
            set
            {
                this.tpPintField = value;
                this.RaisePropertyChanged("tpPint");
            }
        }

        /// <remarks/>
        public string tpVeic
        {
            get
            {
                return this.tpVeicField;
            }
            set
            {
                this.tpVeicField = value;
                this.RaisePropertyChanged("tpVeic");
            }
        }

        /// <remarks/>
        public string espVeic
        {
            get
            {
                return this.espVeicField;
            }
            set
            {
                this.espVeicField = value;
                this.RaisePropertyChanged("espVeic");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetProdVeicProdVIN VIN
        {
            get
            {
                return this.vINField;
            }
            set
            {
                this.vINField = value;
                this.RaisePropertyChanged("VIN");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetProdVeicProdCondVeic condVeic
        {
            get
            {
                return this.condVeicField;
            }
            set
            {
                this.condVeicField = value;
                this.RaisePropertyChanged("condVeic");
            }
        }

        /// <remarks/>
        public string cMod
        {
            get
            {
                return this.cModField;
            }
            set
            {
                this.cModField = value;
                this.RaisePropertyChanged("cMod");
            }
        }

        /// <remarks/>
        public string cCorDENATRAN
        {
            get
            {
                return this.cCorDENATRANField;
            }
            set
            {
                this.cCorDENATRANField = value;
                this.RaisePropertyChanged("cCorDENATRAN");
            }
        }

        /// <remarks/>
        public string lota
        {
            get
            {
                return this.lotaField;
            }
            set
            {
                this.lotaField = value;
                this.RaisePropertyChanged("lota");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetProdVeicProdTpRest tpRest
        {
            get
            {
                return this.tpRestField;
            }
            set
            {
                this.tpRestField = value;
                this.RaisePropertyChanged("tpRest");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetProdVeicProdTpOp
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetProdVeicProdVIN
    {

        /// <remarks/>
        R,

        /// <remarks/>
        N,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetProdVeicProdCondVeic
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetProdVeicProdTpRest
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImposto : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string vTotTribField;

        private object[] itemsField;

        private TNFeInfNFeDetImpostoPIS pISField;

        private TNFeInfNFeDetImpostoPISST pISSTField;

        private TNFeInfNFeDetImpostoCOFINS cOFINSField;

        private TNFeInfNFeDetImpostoCOFINSST cOFINSSTField;

        private TNFeInfNFeDetImpostoICMSUFDest iCMSUFDestField;

        /// <remarks/>
        public string vTotTrib
        {
            get
            {
                return this.vTotTribField;
            }
            set
            {
                this.vTotTribField = value;
                this.RaisePropertyChanged("vTotTrib");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ICMS", typeof(TNFeInfNFeDetImpostoICMS))]
        [System.Xml.Serialization.XmlElementAttribute("II", typeof(TNFeInfNFeDetImpostoII))]
        [System.Xml.Serialization.XmlElementAttribute("IPI", typeof(TIpi))]
        [System.Xml.Serialization.XmlElementAttribute("ISSQN", typeof(TNFeInfNFeDetImpostoISSQN))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoPIS PIS
        {
            get
            {
                return this.pISField;
            }
            set
            {
                this.pISField = value;
                this.RaisePropertyChanged("PIS");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoPISST PISST
        {
            get
            {
                return this.pISSTField;
            }
            set
            {
                this.pISSTField = value;
                this.RaisePropertyChanged("PISST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoCOFINS COFINS
        {
            get
            {
                return this.cOFINSField;
            }
            set
            {
                this.cOFINSField = value;
                this.RaisePropertyChanged("COFINS");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoCOFINSST COFINSST
        {
            get
            {
                return this.cOFINSSTField;
            }
            set
            {
                this.cOFINSSTField = value;
                this.RaisePropertyChanged("COFINSST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSUFDest ICMSUFDest
        {
            get
            {
                return this.iCMSUFDestField;
            }
            set
            {
                this.iCMSUFDestField = value;
                this.RaisePropertyChanged("ICMSUFDest");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMS : object, System.ComponentModel.INotifyPropertyChanged
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ICMS00", typeof(TNFeInfNFeDetImpostoICMSICMS00))]
        [System.Xml.Serialization.XmlElementAttribute("ICMS10", typeof(TNFeInfNFeDetImpostoICMSICMS10))]
        [System.Xml.Serialization.XmlElementAttribute("ICMS20", typeof(TNFeInfNFeDetImpostoICMSICMS20))]
        [System.Xml.Serialization.XmlElementAttribute("ICMS30", typeof(TNFeInfNFeDetImpostoICMSICMS30))]
        [System.Xml.Serialization.XmlElementAttribute("ICMS40", typeof(TNFeInfNFeDetImpostoICMSICMS40))]
        [System.Xml.Serialization.XmlElementAttribute("ICMS51", typeof(TNFeInfNFeDetImpostoICMSICMS51))]
        [System.Xml.Serialization.XmlElementAttribute("ICMS60", typeof(TNFeInfNFeDetImpostoICMSICMS60))]
        [System.Xml.Serialization.XmlElementAttribute("ICMS70", typeof(TNFeInfNFeDetImpostoICMSICMS70))]
        [System.Xml.Serialization.XmlElementAttribute("ICMS90", typeof(TNFeInfNFeDetImpostoICMSICMS90))]
        [System.Xml.Serialization.XmlElementAttribute("ICMSPart", typeof(TNFeInfNFeDetImpostoICMSICMSPart))]
        [System.Xml.Serialization.XmlElementAttribute("ICMSSN101", typeof(TNFeInfNFeDetImpostoICMSICMSSN101))]
        [System.Xml.Serialization.XmlElementAttribute("ICMSSN102", typeof(TNFeInfNFeDetImpostoICMSICMSSN102))]
        [System.Xml.Serialization.XmlElementAttribute("ICMSSN201", typeof(TNFeInfNFeDetImpostoICMSICMSSN201))]
        [System.Xml.Serialization.XmlElementAttribute("ICMSSN202", typeof(TNFeInfNFeDetImpostoICMSICMSSN202))]
        [System.Xml.Serialization.XmlElementAttribute("ICMSSN500", typeof(TNFeInfNFeDetImpostoICMSICMSSN500))]
        [System.Xml.Serialization.XmlElementAttribute("ICMSSN900", typeof(TNFeInfNFeDetImpostoICMSICMSSN900))]
        [System.Xml.Serialization.XmlElementAttribute("ICMSST", typeof(TNFeInfNFeDetImpostoICMSICMSST))]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMS00 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMS00CST cSTField;

        private TNFeInfNFeDetImpostoICMSICMS00ModBC modBCField;

        private string vBCField;

        private string pICMSField;

        private string vICMSField;

        private string pFCPField;

        private string vFCPField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS00CST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS00ModBC modBC
        {
            get
            {
                return this.modBCField;
            }
            set
            {
                this.modBCField = value;
                this.RaisePropertyChanged("modBC");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string pICMS
        {
            get
            {
                return this.pICMSField;
            }
            set
            {
                this.pICMSField = value;
                this.RaisePropertyChanged("pICMS");
            }
        }

        /// <remarks/>
        public string vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
                this.RaisePropertyChanged("vICMS");
            }
        }

        /// <remarks/>
        public string pFCP
        {
            get
            {
                return this.pFCPField;
            }
            set
            {
                this.pFCPField = value;
                this.RaisePropertyChanged("pFCP");
            }
        }

        /// <remarks/>
        public string vFCP
        {
            get
            {
                return this.vFCPField;
            }
            set
            {
                this.vFCPField = value;
                this.RaisePropertyChanged("vFCP");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum Torig
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Item7,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8")]
        Item8,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS00CST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("00")]
        Item00,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS00ModBC
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMS10 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMS10CST cSTField;

        private TNFeInfNFeDetImpostoICMSICMS10ModBC modBCField;

        private string vBCField;

        private string pICMSField;

        private string vICMSField;

        private string vBCFCPField;

        private string pFCPField;

        private string vFCPField;

        private TNFeInfNFeDetImpostoICMSICMS10ModBCST modBCSTField;

        private string pMVASTField;

        private string pRedBCSTField;

        private string vBCSTField;

        private string pICMSSTField;

        private string vICMSSTField;

        private string vBCFCPSTField;

        private string pFCPSTField;

        private string vFCPSTField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS10CST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS10ModBC modBC
        {
            get
            {
                return this.modBCField;
            }
            set
            {
                this.modBCField = value;
                this.RaisePropertyChanged("modBC");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string pICMS
        {
            get
            {
                return this.pICMSField;
            }
            set
            {
                this.pICMSField = value;
                this.RaisePropertyChanged("pICMS");
            }
        }

        /// <remarks/>
        public string vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
                this.RaisePropertyChanged("vICMS");
            }
        }

        /// <remarks/>
        public string vBCFCP
        {
            get
            {
                return this.vBCFCPField;
            }
            set
            {
                this.vBCFCPField = value;
                this.RaisePropertyChanged("vBCFCP");
            }
        }

        /// <remarks/>
        public string pFCP
        {
            get
            {
                return this.pFCPField;
            }
            set
            {
                this.pFCPField = value;
                this.RaisePropertyChanged("pFCP");
            }
        }

        /// <remarks/>
        public string vFCP
        {
            get
            {
                return this.vFCPField;
            }
            set
            {
                this.vFCPField = value;
                this.RaisePropertyChanged("vFCP");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS10ModBCST modBCST
        {
            get
            {
                return this.modBCSTField;
            }
            set
            {
                this.modBCSTField = value;
                this.RaisePropertyChanged("modBCST");
            }
        }

        /// <remarks/>
        public string pMVAST
        {
            get
            {
                return this.pMVASTField;
            }
            set
            {
                this.pMVASTField = value;
                this.RaisePropertyChanged("pMVAST");
            }
        }

        /// <remarks/>
        public string pRedBCST
        {
            get
            {
                return this.pRedBCSTField;
            }
            set
            {
                this.pRedBCSTField = value;
                this.RaisePropertyChanged("pRedBCST");
            }
        }

        /// <remarks/>
        public string vBCST
        {
            get
            {
                return this.vBCSTField;
            }
            set
            {
                this.vBCSTField = value;
                this.RaisePropertyChanged("vBCST");
            }
        }

        /// <remarks/>
        public string pICMSST
        {
            get
            {
                return this.pICMSSTField;
            }
            set
            {
                this.pICMSSTField = value;
                this.RaisePropertyChanged("pICMSST");
            }
        }

        /// <remarks/>
        public string vICMSST
        {
            get
            {
                return this.vICMSSTField;
            }
            set
            {
                this.vICMSSTField = value;
                this.RaisePropertyChanged("vICMSST");
            }
        }

        /// <remarks/>
        public string vBCFCPST
        {
            get
            {
                return this.vBCFCPSTField;
            }
            set
            {
                this.vBCFCPSTField = value;
                this.RaisePropertyChanged("vBCFCPST");
            }
        }

        /// <remarks/>
        public string pFCPST
        {
            get
            {
                return this.pFCPSTField;
            }
            set
            {
                this.pFCPSTField = value;
                this.RaisePropertyChanged("pFCPST");
            }
        }

        /// <remarks/>
        public string vFCPST
        {
            get
            {
                return this.vFCPSTField;
            }
            set
            {
                this.vFCPSTField = value;
                this.RaisePropertyChanged("vFCPST");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS10CST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS10ModBC
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS10ModBCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMS20 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMS20CST cSTField;

        private TNFeInfNFeDetImpostoICMSICMS20ModBC modBCField;

        private string pRedBCField;

        private string vBCField;

        private string pICMSField;

        private string vICMSField;

        private string vBCFCPField;

        private string pFCPField;

        private string vFCPField;

        private string vICMSDesonField;

        private TNFeInfNFeDetImpostoICMSICMS20MotDesICMS motDesICMSField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS20CST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS20ModBC modBC
        {
            get
            {
                return this.modBCField;
            }
            set
            {
                this.modBCField = value;
                this.RaisePropertyChanged("modBC");
            }
        }

        /// <remarks/>
        public string pRedBC
        {
            get
            {
                return this.pRedBCField;
            }
            set
            {
                this.pRedBCField = value;
                this.RaisePropertyChanged("pRedBC");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string pICMS
        {
            get
            {
                return this.pICMSField;
            }
            set
            {
                this.pICMSField = value;
                this.RaisePropertyChanged("pICMS");
            }
        }

        /// <remarks/>
        public string vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
                this.RaisePropertyChanged("vICMS");
            }
        }

        /// <remarks/>
        public string vBCFCP
        {
            get
            {
                return this.vBCFCPField;
            }
            set
            {
                this.vBCFCPField = value;
                this.RaisePropertyChanged("vBCFCP");
            }
        }

        /// <remarks/>
        public string pFCP
        {
            get
            {
                return this.pFCPField;
            }
            set
            {
                this.pFCPField = value;
                this.RaisePropertyChanged("pFCP");
            }
        }

        /// <remarks/>
        public string vFCP
        {
            get
            {
                return this.vFCPField;
            }
            set
            {
                this.vFCPField = value;
                this.RaisePropertyChanged("vFCP");
            }
        }

        /// <remarks/>
        public string vICMSDeson
        {
            get
            {
                return this.vICMSDesonField;
            }
            set
            {
                this.vICMSDesonField = value;
                this.RaisePropertyChanged("vICMSDeson");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS20MotDesICMS motDesICMS
        {
            get
            {
                return this.motDesICMSField;
            }
            set
            {
                this.motDesICMSField = value;
                this.RaisePropertyChanged("motDesICMS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS20CST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("20")]
        Item20,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS20ModBC
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS20MotDesICMS
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMS30 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMS30CST cSTField;

        private TNFeInfNFeDetImpostoICMSICMS30ModBCST modBCSTField;

        private string pMVASTField;

        private string pRedBCSTField;

        private string vBCSTField;

        private string pICMSSTField;

        private string vICMSSTField;

        private string vBCFCPSTField;

        private string pFCPSTField;

        private string vFCPSTField;

        private string vICMSDesonField;

        private TNFeInfNFeDetImpostoICMSICMS30MotDesICMS motDesICMSField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS30CST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS30ModBCST modBCST
        {
            get
            {
                return this.modBCSTField;
            }
            set
            {
                this.modBCSTField = value;
                this.RaisePropertyChanged("modBCST");
            }
        }

        /// <remarks/>
        public string pMVAST
        {
            get
            {
                return this.pMVASTField;
            }
            set
            {
                this.pMVASTField = value;
                this.RaisePropertyChanged("pMVAST");
            }
        }

        /// <remarks/>
        public string pRedBCST
        {
            get
            {
                return this.pRedBCSTField;
            }
            set
            {
                this.pRedBCSTField = value;
                this.RaisePropertyChanged("pRedBCST");
            }
        }

        /// <remarks/>
        public string vBCST
        {
            get
            {
                return this.vBCSTField;
            }
            set
            {
                this.vBCSTField = value;
                this.RaisePropertyChanged("vBCST");
            }
        }

        /// <remarks/>
        public string pICMSST
        {
            get
            {
                return this.pICMSSTField;
            }
            set
            {
                this.pICMSSTField = value;
                this.RaisePropertyChanged("pICMSST");
            }
        }

        /// <remarks/>
        public string vICMSST
        {
            get
            {
                return this.vICMSSTField;
            }
            set
            {
                this.vICMSSTField = value;
                this.RaisePropertyChanged("vICMSST");
            }
        }

        /// <remarks/>
        public string vBCFCPST
        {
            get
            {
                return this.vBCFCPSTField;
            }
            set
            {
                this.vBCFCPSTField = value;
                this.RaisePropertyChanged("vBCFCPST");
            }
        }

        /// <remarks/>
        public string pFCPST
        {
            get
            {
                return this.pFCPSTField;
            }
            set
            {
                this.pFCPSTField = value;
                this.RaisePropertyChanged("pFCPST");
            }
        }

        /// <remarks/>
        public string vFCPST
        {
            get
            {
                return this.vFCPSTField;
            }
            set
            {
                this.vFCPSTField = value;
                this.RaisePropertyChanged("vFCPST");
            }
        }

        /// <remarks/>
        public string vICMSDeson
        {
            get
            {
                return this.vICMSDesonField;
            }
            set
            {
                this.vICMSDesonField = value;
                this.RaisePropertyChanged("vICMSDeson");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS30MotDesICMS motDesICMS
        {
            get
            {
                return this.motDesICMSField;
            }
            set
            {
                this.motDesICMSField = value;
                this.RaisePropertyChanged("motDesICMS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS30CST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("30")]
        Item30,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS30ModBCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS30MotDesICMS
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Item7,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMS40 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMS40CST cSTField;

        private string vICMSDesonField;

        private TNFeInfNFeDetImpostoICMSICMS40MotDesICMS motDesICMSField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS40CST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public string vICMSDeson
        {
            get
            {
                return this.vICMSDesonField;
            }
            set
            {
                this.vICMSDesonField = value;
                this.RaisePropertyChanged("vICMSDeson");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS40MotDesICMS motDesICMS
        {
            get
            {
                return this.motDesICMSField;
            }
            set
            {
                this.motDesICMSField = value;
                this.RaisePropertyChanged("motDesICMS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS40CST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("40")]
        Item40,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("41")]
        Item41,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("50")]
        Item50,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS40MotDesICMS
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Item7,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8")]
        Item8,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("90")]
        Item90,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMS51 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMS51CST cSTField;

        private TNFeInfNFeDetImpostoICMSICMS51ModBC modBCField;

        private bool modBCFieldSpecified;

        private string pRedBCField;

        private string vBCField;

        private string pICMSField;

        private string vICMSOpField;

        private string pDifField;

        private string vICMSDifField;

        private string vICMSField;

        private string vBCFCPField;

        private string pFCPField;

        private string vFCPField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS51CST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS51ModBC modBC
        {
            get
            {
                return this.modBCField;
            }
            set
            {
                this.modBCField = value;
                this.RaisePropertyChanged("modBC");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool modBCSpecified
        {
            get
            {
                return this.modBCFieldSpecified;
            }
            set
            {
                this.modBCFieldSpecified = value;
                this.RaisePropertyChanged("modBCSpecified");
            }
        }

        /// <remarks/>
        public string pRedBC
        {
            get
            {
                return this.pRedBCField;
            }
            set
            {
                this.pRedBCField = value;
                this.RaisePropertyChanged("pRedBC");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string pICMS
        {
            get
            {
                return this.pICMSField;
            }
            set
            {
                this.pICMSField = value;
                this.RaisePropertyChanged("pICMS");
            }
        }

        /// <remarks/>
        public string vICMSOp
        {
            get
            {
                return this.vICMSOpField;
            }
            set
            {
                this.vICMSOpField = value;
                this.RaisePropertyChanged("vICMSOp");
            }
        }

        /// <remarks/>
        public string pDif
        {
            get
            {
                return this.pDifField;
            }
            set
            {
                this.pDifField = value;
                this.RaisePropertyChanged("pDif");
            }
        }

        /// <remarks/>
        public string vICMSDif
        {
            get
            {
                return this.vICMSDifField;
            }
            set
            {
                this.vICMSDifField = value;
                this.RaisePropertyChanged("vICMSDif");
            }
        }

        /// <remarks/>
        public string vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
                this.RaisePropertyChanged("vICMS");
            }
        }

        /// <remarks/>
        public string vBCFCP
        {
            get
            {
                return this.vBCFCPField;
            }
            set
            {
                this.vBCFCPField = value;
                this.RaisePropertyChanged("vBCFCP");
            }
        }

        /// <remarks/>
        public string pFCP
        {
            get
            {
                return this.pFCPField;
            }
            set
            {
                this.pFCPField = value;
                this.RaisePropertyChanged("pFCP");
            }
        }

        /// <remarks/>
        public string vFCP
        {
            get
            {
                return this.vFCPField;
            }
            set
            {
                this.vFCPField = value;
                this.RaisePropertyChanged("vFCP");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS51CST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("51")]
        Item51,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS51ModBC
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMS60 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMS60CST cSTField;

        private string vBCSTRetField;

        private string pSTField;

        private string vICMSSubstitutoField;

        private string vICMSSTRetField;

        private string vBCFCPSTRetField;

        private string pFCPSTRetField;

        private string vFCPSTRetField;

        private string pRedBCEfetField;

        private string vBCEfetField;

        private string pICMSEfetField;

        private string vICMSEfetField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS60CST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public string vBCSTRet
        {
            get
            {
                return this.vBCSTRetField;
            }
            set
            {
                this.vBCSTRetField = value;
                this.RaisePropertyChanged("vBCSTRet");
            }
        }

        /// <remarks/>
        public string pST
        {
            get
            {
                return this.pSTField;
            }
            set
            {
                this.pSTField = value;
                this.RaisePropertyChanged("pST");
            }
        }

        /// <remarks/>
        public string vICMSSubstituto
        {
            get
            {
                return this.vICMSSubstitutoField;
            }
            set
            {
                this.vICMSSubstitutoField = value;
                this.RaisePropertyChanged("vICMSSubstituto");
            }
        }

        /// <remarks/>
        public string vICMSSTRet
        {
            get
            {
                return this.vICMSSTRetField;
            }
            set
            {
                this.vICMSSTRetField = value;
                this.RaisePropertyChanged("vICMSSTRet");
            }
        }

        /// <remarks/>
        public string vBCFCPSTRet
        {
            get
            {
                return this.vBCFCPSTRetField;
            }
            set
            {
                this.vBCFCPSTRetField = value;
                this.RaisePropertyChanged("vBCFCPSTRet");
            }
        }

        /// <remarks/>
        public string pFCPSTRet
        {
            get
            {
                return this.pFCPSTRetField;
            }
            set
            {
                this.pFCPSTRetField = value;
                this.RaisePropertyChanged("pFCPSTRet");
            }
        }

        /// <remarks/>
        public string vFCPSTRet
        {
            get
            {
                return this.vFCPSTRetField;
            }
            set
            {
                this.vFCPSTRetField = value;
                this.RaisePropertyChanged("vFCPSTRet");
            }
        }

        /// <remarks/>
        public string pRedBCEfet
        {
            get
            {
                return this.pRedBCEfetField;
            }
            set
            {
                this.pRedBCEfetField = value;
                this.RaisePropertyChanged("pRedBCEfet");
            }
        }

        /// <remarks/>
        public string vBCEfet
        {
            get
            {
                return this.vBCEfetField;
            }
            set
            {
                this.vBCEfetField = value;
                this.RaisePropertyChanged("vBCEfet");
            }
        }

        /// <remarks/>
        public string pICMSEfet
        {
            get
            {
                return this.pICMSEfetField;
            }
            set
            {
                this.pICMSEfetField = value;
                this.RaisePropertyChanged("pICMSEfet");
            }
        }

        /// <remarks/>
        public string vICMSEfet
        {
            get
            {
                return this.vICMSEfetField;
            }
            set
            {
                this.vICMSEfetField = value;
                this.RaisePropertyChanged("vICMSEfet");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS60CST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("60")]
        Item60,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMS70 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMS70CST cSTField;

        private TNFeInfNFeDetImpostoICMSICMS70ModBC modBCField;

        private string pRedBCField;

        private string vBCField;

        private string pICMSField;

        private string vICMSField;

        private string vBCFCPField;

        private string pFCPField;

        private string vFCPField;

        private TNFeInfNFeDetImpostoICMSICMS70ModBCST modBCSTField;

        private string pMVASTField;

        private string pRedBCSTField;

        private string vBCSTField;

        private string pICMSSTField;

        private string vICMSSTField;

        private string vBCFCPSTField;

        private string pFCPSTField;

        private string vFCPSTField;

        private string vICMSDesonField;

        private TNFeInfNFeDetImpostoICMSICMS70MotDesICMS motDesICMSField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS70CST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS70ModBC modBC
        {
            get
            {
                return this.modBCField;
            }
            set
            {
                this.modBCField = value;
                this.RaisePropertyChanged("modBC");
            }
        }

        /// <remarks/>
        public string pRedBC
        {
            get
            {
                return this.pRedBCField;
            }
            set
            {
                this.pRedBCField = value;
                this.RaisePropertyChanged("pRedBC");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string pICMS
        {
            get
            {
                return this.pICMSField;
            }
            set
            {
                this.pICMSField = value;
                this.RaisePropertyChanged("pICMS");
            }
        }

        /// <remarks/>
        public string vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
                this.RaisePropertyChanged("vICMS");
            }
        }

        /// <remarks/>
        public string vBCFCP
        {
            get
            {
                return this.vBCFCPField;
            }
            set
            {
                this.vBCFCPField = value;
                this.RaisePropertyChanged("vBCFCP");
            }
        }

        /// <remarks/>
        public string pFCP
        {
            get
            {
                return this.pFCPField;
            }
            set
            {
                this.pFCPField = value;
                this.RaisePropertyChanged("pFCP");
            }
        }

        /// <remarks/>
        public string vFCP
        {
            get
            {
                return this.vFCPField;
            }
            set
            {
                this.vFCPField = value;
                this.RaisePropertyChanged("vFCP");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS70ModBCST modBCST
        {
            get
            {
                return this.modBCSTField;
            }
            set
            {
                this.modBCSTField = value;
                this.RaisePropertyChanged("modBCST");
            }
        }

        /// <remarks/>
        public string pMVAST
        {
            get
            {
                return this.pMVASTField;
            }
            set
            {
                this.pMVASTField = value;
                this.RaisePropertyChanged("pMVAST");
            }
        }

        /// <remarks/>
        public string pRedBCST
        {
            get
            {
                return this.pRedBCSTField;
            }
            set
            {
                this.pRedBCSTField = value;
                this.RaisePropertyChanged("pRedBCST");
            }
        }

        /// <remarks/>
        public string vBCST
        {
            get
            {
                return this.vBCSTField;
            }
            set
            {
                this.vBCSTField = value;
                this.RaisePropertyChanged("vBCST");
            }
        }

        /// <remarks/>
        public string pICMSST
        {
            get
            {
                return this.pICMSSTField;
            }
            set
            {
                this.pICMSSTField = value;
                this.RaisePropertyChanged("pICMSST");
            }
        }

        /// <remarks/>
        public string vICMSST
        {
            get
            {
                return this.vICMSSTField;
            }
            set
            {
                this.vICMSSTField = value;
                this.RaisePropertyChanged("vICMSST");
            }
        }

        /// <remarks/>
        public string vBCFCPST
        {
            get
            {
                return this.vBCFCPSTField;
            }
            set
            {
                this.vBCFCPSTField = value;
                this.RaisePropertyChanged("vBCFCPST");
            }
        }

        /// <remarks/>
        public string pFCPST
        {
            get
            {
                return this.pFCPSTField;
            }
            set
            {
                this.pFCPSTField = value;
                this.RaisePropertyChanged("pFCPST");
            }
        }

        /// <remarks/>
        public string vFCPST
        {
            get
            {
                return this.vFCPSTField;
            }
            set
            {
                this.vFCPSTField = value;
                this.RaisePropertyChanged("vFCPST");
            }
        }

        /// <remarks/>
        public string vICMSDeson
        {
            get
            {
                return this.vICMSDesonField;
            }
            set
            {
                this.vICMSDesonField = value;
                this.RaisePropertyChanged("vICMSDeson");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS70MotDesICMS motDesICMS
        {
            get
            {
                return this.motDesICMSField;
            }
            set
            {
                this.motDesICMSField = value;
                this.RaisePropertyChanged("motDesICMS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS70CST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("70")]
        Item70,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS70ModBC
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS70ModBCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS70MotDesICMS
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMS90 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMS90CST cSTField;

        private TNFeInfNFeDetImpostoICMSICMS90ModBC modBCField;

        private string vBCField;

        private string pRedBCField;

        private string pICMSField;

        private string vICMSField;

        private string vBCFCPField;

        private string pFCPField;

        private string vFCPField;

        private TNFeInfNFeDetImpostoICMSICMS90ModBCST modBCSTField;

        private string pMVASTField;

        private string pRedBCSTField;

        private string vBCSTField;

        private string pICMSSTField;

        private string vICMSSTField;

        private string vBCFCPSTField;

        private string pFCPSTField;

        private string vFCPSTField;

        private string vICMSDesonField;

        private TNFeInfNFeDetImpostoICMSICMS90MotDesICMS motDesICMSField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS90CST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS90ModBC modBC
        {
            get
            {
                return this.modBCField;
            }
            set
            {
                this.modBCField = value;
                this.RaisePropertyChanged("modBC");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string pRedBC
        {
            get
            {
                return this.pRedBCField;
            }
            set
            {
                this.pRedBCField = value;
                this.RaisePropertyChanged("pRedBC");
            }
        }

        /// <remarks/>
        public string pICMS
        {
            get
            {
                return this.pICMSField;
            }
            set
            {
                this.pICMSField = value;
                this.RaisePropertyChanged("pICMS");
            }
        }

        /// <remarks/>
        public string vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
                this.RaisePropertyChanged("vICMS");
            }
        }

        /// <remarks/>
        public string vBCFCP
        {
            get
            {
                return this.vBCFCPField;
            }
            set
            {
                this.vBCFCPField = value;
                this.RaisePropertyChanged("vBCFCP");
            }
        }

        /// <remarks/>
        public string pFCP
        {
            get
            {
                return this.pFCPField;
            }
            set
            {
                this.pFCPField = value;
                this.RaisePropertyChanged("pFCP");
            }
        }

        /// <remarks/>
        public string vFCP
        {
            get
            {
                return this.vFCPField;
            }
            set
            {
                this.vFCPField = value;
                this.RaisePropertyChanged("vFCP");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS90ModBCST modBCST
        {
            get
            {
                return this.modBCSTField;
            }
            set
            {
                this.modBCSTField = value;
                this.RaisePropertyChanged("modBCST");
            }
        }

        /// <remarks/>
        public string pMVAST
        {
            get
            {
                return this.pMVASTField;
            }
            set
            {
                this.pMVASTField = value;
                this.RaisePropertyChanged("pMVAST");
            }
        }

        /// <remarks/>
        public string pRedBCST
        {
            get
            {
                return this.pRedBCSTField;
            }
            set
            {
                this.pRedBCSTField = value;
                this.RaisePropertyChanged("pRedBCST");
            }
        }

        /// <remarks/>
        public string vBCST
        {
            get
            {
                return this.vBCSTField;
            }
            set
            {
                this.vBCSTField = value;
                this.RaisePropertyChanged("vBCST");
            }
        }

        /// <remarks/>
        public string pICMSST
        {
            get
            {
                return this.pICMSSTField;
            }
            set
            {
                this.pICMSSTField = value;
                this.RaisePropertyChanged("pICMSST");
            }
        }

        /// <remarks/>
        public string vICMSST
        {
            get
            {
                return this.vICMSSTField;
            }
            set
            {
                this.vICMSSTField = value;
                this.RaisePropertyChanged("vICMSST");
            }
        }

        /// <remarks/>
        public string vBCFCPST
        {
            get
            {
                return this.vBCFCPSTField;
            }
            set
            {
                this.vBCFCPSTField = value;
                this.RaisePropertyChanged("vBCFCPST");
            }
        }

        /// <remarks/>
        public string pFCPST
        {
            get
            {
                return this.pFCPSTField;
            }
            set
            {
                this.pFCPSTField = value;
                this.RaisePropertyChanged("pFCPST");
            }
        }

        /// <remarks/>
        public string vFCPST
        {
            get
            {
                return this.vFCPSTField;
            }
            set
            {
                this.vFCPSTField = value;
                this.RaisePropertyChanged("vFCPST");
            }
        }

        /// <remarks/>
        public string vICMSDeson
        {
            get
            {
                return this.vICMSDesonField;
            }
            set
            {
                this.vICMSDesonField = value;
                this.RaisePropertyChanged("vICMSDeson");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMS90MotDesICMS motDesICMS
        {
            get
            {
                return this.motDesICMSField;
            }
            set
            {
                this.motDesICMSField = value;
                this.RaisePropertyChanged("motDesICMS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS90CST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("90")]
        Item90,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS90ModBC
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS90ModBCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMS90MotDesICMS
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMSPart : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMSPartCST cSTField;

        private TNFeInfNFeDetImpostoICMSICMSPartModBC modBCField;

        private string vBCField;

        private string pRedBCField;

        private string pICMSField;

        private string vICMSField;

        private TNFeInfNFeDetImpostoICMSICMSPartModBCST modBCSTField;

        private string pMVASTField;

        private string pRedBCSTField;

        private string vBCSTField;

        private string pICMSSTField;

        private string vICMSSTField;

        private string pBCOpField;

        private TUf uFSTField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSPartCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSPartModBC modBC
        {
            get
            {
                return this.modBCField;
            }
            set
            {
                this.modBCField = value;
                this.RaisePropertyChanged("modBC");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string pRedBC
        {
            get
            {
                return this.pRedBCField;
            }
            set
            {
                this.pRedBCField = value;
                this.RaisePropertyChanged("pRedBC");
            }
        }

        /// <remarks/>
        public string pICMS
        {
            get
            {
                return this.pICMSField;
            }
            set
            {
                this.pICMSField = value;
                this.RaisePropertyChanged("pICMS");
            }
        }

        /// <remarks/>
        public string vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
                this.RaisePropertyChanged("vICMS");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSPartModBCST modBCST
        {
            get
            {
                return this.modBCSTField;
            }
            set
            {
                this.modBCSTField = value;
                this.RaisePropertyChanged("modBCST");
            }
        }

        /// <remarks/>
        public string pMVAST
        {
            get
            {
                return this.pMVASTField;
            }
            set
            {
                this.pMVASTField = value;
                this.RaisePropertyChanged("pMVAST");
            }
        }

        /// <remarks/>
        public string pRedBCST
        {
            get
            {
                return this.pRedBCSTField;
            }
            set
            {
                this.pRedBCSTField = value;
                this.RaisePropertyChanged("pRedBCST");
            }
        }

        /// <remarks/>
        public string vBCST
        {
            get
            {
                return this.vBCSTField;
            }
            set
            {
                this.vBCSTField = value;
                this.RaisePropertyChanged("vBCST");
            }
        }

        /// <remarks/>
        public string pICMSST
        {
            get
            {
                return this.pICMSSTField;
            }
            set
            {
                this.pICMSSTField = value;
                this.RaisePropertyChanged("pICMSST");
            }
        }

        /// <remarks/>
        public string vICMSST
        {
            get
            {
                return this.vICMSSTField;
            }
            set
            {
                this.vICMSSTField = value;
                this.RaisePropertyChanged("vICMSST");
            }
        }

        /// <remarks/>
        public string pBCOp
        {
            get
            {
                return this.pBCOpField;
            }
            set
            {
                this.pBCOpField = value;
                this.RaisePropertyChanged("pBCOp");
            }
        }

        /// <remarks/>
        public TUf UFST
        {
            get
            {
                return this.uFSTField;
            }
            set
            {
                this.uFSTField = value;
                this.RaisePropertyChanged("UFST");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSPartCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("90")]
        Item90,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSPartModBC
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSPartModBCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMSSN101 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMSSN101CSOSN cSOSNField;

        private string pCredSNField;

        private string vCredICMSSNField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSN101CSOSN CSOSN
        {
            get
            {
                return this.cSOSNField;
            }
            set
            {
                this.cSOSNField = value;
                this.RaisePropertyChanged("CSOSN");
            }
        }

        /// <remarks/>
        public string pCredSN
        {
            get
            {
                return this.pCredSNField;
            }
            set
            {
                this.pCredSNField = value;
                this.RaisePropertyChanged("pCredSN");
            }
        }

        /// <remarks/>
        public string vCredICMSSN
        {
            get
            {
                return this.vCredICMSSNField;
            }
            set
            {
                this.vCredICMSSNField = value;
                this.RaisePropertyChanged("vCredICMSSN");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSN101CSOSN
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("101")]
        Item101,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMSSN102 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMSSN102CSOSN cSOSNField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSN102CSOSN CSOSN
        {
            get
            {
                return this.cSOSNField;
            }
            set
            {
                this.cSOSNField = value;
                this.RaisePropertyChanged("CSOSN");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSN102CSOSN
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("102")]
        Item102,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("103")]
        Item103,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("300")]
        Item300,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("400")]
        Item400,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMSSN201 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMSSN201CSOSN cSOSNField;

        private TNFeInfNFeDetImpostoICMSICMSSN201ModBCST modBCSTField;

        private string pMVASTField;

        private string pRedBCSTField;

        private string vBCSTField;

        private string pICMSSTField;

        private string vICMSSTField;

        private string vBCFCPSTField;

        private string pFCPSTField;

        private string vFCPSTField;

        private string pCredSNField;

        private string vCredICMSSNField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSN201CSOSN CSOSN
        {
            get
            {
                return this.cSOSNField;
            }
            set
            {
                this.cSOSNField = value;
                this.RaisePropertyChanged("CSOSN");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSN201ModBCST modBCST
        {
            get
            {
                return this.modBCSTField;
            }
            set
            {
                this.modBCSTField = value;
                this.RaisePropertyChanged("modBCST");
            }
        }

        /// <remarks/>
        public string pMVAST
        {
            get
            {
                return this.pMVASTField;
            }
            set
            {
                this.pMVASTField = value;
                this.RaisePropertyChanged("pMVAST");
            }
        }

        /// <remarks/>
        public string pRedBCST
        {
            get
            {
                return this.pRedBCSTField;
            }
            set
            {
                this.pRedBCSTField = value;
                this.RaisePropertyChanged("pRedBCST");
            }
        }

        /// <remarks/>
        public string vBCST
        {
            get
            {
                return this.vBCSTField;
            }
            set
            {
                this.vBCSTField = value;
                this.RaisePropertyChanged("vBCST");
            }
        }

        /// <remarks/>
        public string pICMSST
        {
            get
            {
                return this.pICMSSTField;
            }
            set
            {
                this.pICMSSTField = value;
                this.RaisePropertyChanged("pICMSST");
            }
        }

        /// <remarks/>
        public string vICMSST
        {
            get
            {
                return this.vICMSSTField;
            }
            set
            {
                this.vICMSSTField = value;
                this.RaisePropertyChanged("vICMSST");
            }
        }

        /// <remarks/>
        public string vBCFCPST
        {
            get
            {
                return this.vBCFCPSTField;
            }
            set
            {
                this.vBCFCPSTField = value;
                this.RaisePropertyChanged("vBCFCPST");
            }
        }

        /// <remarks/>
        public string pFCPST
        {
            get
            {
                return this.pFCPSTField;
            }
            set
            {
                this.pFCPSTField = value;
                this.RaisePropertyChanged("pFCPST");
            }
        }

        /// <remarks/>
        public string vFCPST
        {
            get
            {
                return this.vFCPSTField;
            }
            set
            {
                this.vFCPSTField = value;
                this.RaisePropertyChanged("vFCPST");
            }
        }

        /// <remarks/>
        public string pCredSN
        {
            get
            {
                return this.pCredSNField;
            }
            set
            {
                this.pCredSNField = value;
                this.RaisePropertyChanged("pCredSN");
            }
        }

        /// <remarks/>
        public string vCredICMSSN
        {
            get
            {
                return this.vCredICMSSNField;
            }
            set
            {
                this.vCredICMSSNField = value;
                this.RaisePropertyChanged("vCredICMSSN");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSN201CSOSN
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("201")]
        Item201,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSN201ModBCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMSSN202 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMSSN202CSOSN cSOSNField;

        private TNFeInfNFeDetImpostoICMSICMSSN202ModBCST modBCSTField;

        private string pMVASTField;

        private string pRedBCSTField;

        private string vBCSTField;

        private string pICMSSTField;

        private string vICMSSTField;

        private string vBCFCPSTField;

        private string pFCPSTField;

        private string vFCPSTField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSN202CSOSN CSOSN
        {
            get
            {
                return this.cSOSNField;
            }
            set
            {
                this.cSOSNField = value;
                this.RaisePropertyChanged("CSOSN");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSN202ModBCST modBCST
        {
            get
            {
                return this.modBCSTField;
            }
            set
            {
                this.modBCSTField = value;
                this.RaisePropertyChanged("modBCST");
            }
        }

        /// <remarks/>
        public string pMVAST
        {
            get
            {
                return this.pMVASTField;
            }
            set
            {
                this.pMVASTField = value;
                this.RaisePropertyChanged("pMVAST");
            }
        }

        /// <remarks/>
        public string pRedBCST
        {
            get
            {
                return this.pRedBCSTField;
            }
            set
            {
                this.pRedBCSTField = value;
                this.RaisePropertyChanged("pRedBCST");
            }
        }

        /// <remarks/>
        public string vBCST
        {
            get
            {
                return this.vBCSTField;
            }
            set
            {
                this.vBCSTField = value;
                this.RaisePropertyChanged("vBCST");
            }
        }

        /// <remarks/>
        public string pICMSST
        {
            get
            {
                return this.pICMSSTField;
            }
            set
            {
                this.pICMSSTField = value;
                this.RaisePropertyChanged("pICMSST");
            }
        }

        /// <remarks/>
        public string vICMSST
        {
            get
            {
                return this.vICMSSTField;
            }
            set
            {
                this.vICMSSTField = value;
                this.RaisePropertyChanged("vICMSST");
            }
        }

        /// <remarks/>
        public string vBCFCPST
        {
            get
            {
                return this.vBCFCPSTField;
            }
            set
            {
                this.vBCFCPSTField = value;
                this.RaisePropertyChanged("vBCFCPST");
            }
        }

        /// <remarks/>
        public string pFCPST
        {
            get
            {
                return this.pFCPSTField;
            }
            set
            {
                this.pFCPSTField = value;
                this.RaisePropertyChanged("pFCPST");
            }
        }

        /// <remarks/>
        public string vFCPST
        {
            get
            {
                return this.vFCPSTField;
            }
            set
            {
                this.vFCPSTField = value;
                this.RaisePropertyChanged("vFCPST");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSN202CSOSN
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("202")]
        Item202,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("203")]
        Item203,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSN202ModBCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Item7,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMSSN500 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMSSN500CSOSN cSOSNField;

        private string vBCSTRetField;

        private string pSTField;

        private string vICMSSubstitutoField;

        private string vICMSSTRetField;

        private string vBCFCPSTRetField;

        private string pFCPSTRetField;

        private string vFCPSTRetField;

        private string pRedBCEfetField;

        private string vBCEfetField;

        private string pICMSEfetField;

        private string vICMSEfetField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSN500CSOSN CSOSN
        {
            get
            {
                return this.cSOSNField;
            }
            set
            {
                this.cSOSNField = value;
                this.RaisePropertyChanged("CSOSN");
            }
        }

        /// <remarks/>
        public string vBCSTRet
        {
            get
            {
                return this.vBCSTRetField;
            }
            set
            {
                this.vBCSTRetField = value;
                this.RaisePropertyChanged("vBCSTRet");
            }
        }

        /// <remarks/>
        public string pST
        {
            get
            {
                return this.pSTField;
            }
            set
            {
                this.pSTField = value;
                this.RaisePropertyChanged("pST");
            }
        }

        /// <remarks/>
        public string vICMSSubstituto
        {
            get
            {
                return this.vICMSSubstitutoField;
            }
            set
            {
                this.vICMSSubstitutoField = value;
                this.RaisePropertyChanged("vICMSSubstituto");
            }
        }

        /// <remarks/>
        public string vICMSSTRet
        {
            get
            {
                return this.vICMSSTRetField;
            }
            set
            {
                this.vICMSSTRetField = value;
                this.RaisePropertyChanged("vICMSSTRet");
            }
        }

        /// <remarks/>
        public string vBCFCPSTRet
        {
            get
            {
                return this.vBCFCPSTRetField;
            }
            set
            {
                this.vBCFCPSTRetField = value;
                this.RaisePropertyChanged("vBCFCPSTRet");
            }
        }

        /// <remarks/>
        public string pFCPSTRet
        {
            get
            {
                return this.pFCPSTRetField;
            }
            set
            {
                this.pFCPSTRetField = value;
                this.RaisePropertyChanged("pFCPSTRet");
            }
        }

        /// <remarks/>
        public string vFCPSTRet
        {
            get
            {
                return this.vFCPSTRetField;
            }
            set
            {
                this.vFCPSTRetField = value;
                this.RaisePropertyChanged("vFCPSTRet");
            }
        }

        /// <remarks/>
        public string pRedBCEfet
        {
            get
            {
                return this.pRedBCEfetField;
            }
            set
            {
                this.pRedBCEfetField = value;
                this.RaisePropertyChanged("pRedBCEfet");
            }
        }

        /// <remarks/>
        public string vBCEfet
        {
            get
            {
                return this.vBCEfetField;
            }
            set
            {
                this.vBCEfetField = value;
                this.RaisePropertyChanged("vBCEfet");
            }
        }

        /// <remarks/>
        public string pICMSEfet
        {
            get
            {
                return this.pICMSEfetField;
            }
            set
            {
                this.pICMSEfetField = value;
                this.RaisePropertyChanged("pICMSEfet");
            }
        }

        /// <remarks/>
        public string vICMSEfet
        {
            get
            {
                return this.vICMSEfetField;
            }
            set
            {
                this.vICMSEfetField = value;
                this.RaisePropertyChanged("vICMSEfet");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSN500CSOSN
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("500")]
        Item500,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMSSN900 : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMSSN900CSOSN cSOSNField;

        private TNFeInfNFeDetImpostoICMSICMSSN900ModBC modBCField;

        private string vBCField;

        private string pRedBCField;

        private string pICMSField;

        private string vICMSField;

        private TNFeInfNFeDetImpostoICMSICMSSN900ModBCST modBCSTField;

        private string pMVASTField;

        private string pRedBCSTField;

        private string vBCSTField;

        private string pICMSSTField;

        private string vICMSSTField;

        private string vBCFCPSTField;

        private string pFCPSTField;

        private string vFCPSTField;

        private string pCredSNField;

        private string vCredICMSSNField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSN900CSOSN CSOSN
        {
            get
            {
                return this.cSOSNField;
            }
            set
            {
                this.cSOSNField = value;
                this.RaisePropertyChanged("CSOSN");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSN900ModBC modBC
        {
            get
            {
                return this.modBCField;
            }
            set
            {
                this.modBCField = value;
                this.RaisePropertyChanged("modBC");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string pRedBC
        {
            get
            {
                return this.pRedBCField;
            }
            set
            {
                this.pRedBCField = value;
                this.RaisePropertyChanged("pRedBC");
            }
        }

        /// <remarks/>
        public string pICMS
        {
            get
            {
                return this.pICMSField;
            }
            set
            {
                this.pICMSField = value;
                this.RaisePropertyChanged("pICMS");
            }
        }

        /// <remarks/>
        public string vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
                this.RaisePropertyChanged("vICMS");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSN900ModBCST modBCST
        {
            get
            {
                return this.modBCSTField;
            }
            set
            {
                this.modBCSTField = value;
                this.RaisePropertyChanged("modBCST");
            }
        }

        /// <remarks/>
        public string pMVAST
        {
            get
            {
                return this.pMVASTField;
            }
            set
            {
                this.pMVASTField = value;
                this.RaisePropertyChanged("pMVAST");
            }
        }

        /// <remarks/>
        public string pRedBCST
        {
            get
            {
                return this.pRedBCSTField;
            }
            set
            {
                this.pRedBCSTField = value;
                this.RaisePropertyChanged("pRedBCST");
            }
        }

        /// <remarks/>
        public string vBCST
        {
            get
            {
                return this.vBCSTField;
            }
            set
            {
                this.vBCSTField = value;
                this.RaisePropertyChanged("vBCST");
            }
        }

        /// <remarks/>
        public string pICMSST
        {
            get
            {
                return this.pICMSSTField;
            }
            set
            {
                this.pICMSSTField = value;
                this.RaisePropertyChanged("pICMSST");
            }
        }

        /// <remarks/>
        public string vICMSST
        {
            get
            {
                return this.vICMSSTField;
            }
            set
            {
                this.vICMSSTField = value;
                this.RaisePropertyChanged("vICMSST");
            }
        }

        /// <remarks/>
        public string vBCFCPST
        {
            get
            {
                return this.vBCFCPSTField;
            }
            set
            {
                this.vBCFCPSTField = value;
                this.RaisePropertyChanged("vBCFCPST");
            }
        }

        /// <remarks/>
        public string pFCPST
        {
            get
            {
                return this.pFCPSTField;
            }
            set
            {
                this.pFCPSTField = value;
                this.RaisePropertyChanged("pFCPST");
            }
        }

        /// <remarks/>
        public string vFCPST
        {
            get
            {
                return this.vFCPSTField;
            }
            set
            {
                this.vFCPSTField = value;
                this.RaisePropertyChanged("vFCPST");
            }
        }

        /// <remarks/>
        public string pCredSN
        {
            get
            {
                return this.pCredSNField;
            }
            set
            {
                this.pCredSNField = value;
                this.RaisePropertyChanged("pCredSN");
            }
        }

        /// <remarks/>
        public string vCredICMSSN
        {
            get
            {
                return this.vCredICMSSNField;
            }
            set
            {
                this.vCredICMSSNField = value;
                this.RaisePropertyChanged("vCredICMSSN");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSN900CSOSN
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("900")]
        Item900,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSN900ModBC
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSN900ModBCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSICMSST : object, System.ComponentModel.INotifyPropertyChanged
    {

        private Torig origField;

        private TNFeInfNFeDetImpostoICMSICMSSTCST cSTField;

        private string vBCSTRetField;

        private string pSTField;

        private string vICMSSubstitutoField;

        private string vICMSSTRetField;

        private string vBCFCPSTRetField;

        private string pFCPSTRetField;

        private string vFCPSTRetField;

        private string vBCSTDestField;

        private string vICMSSTDestField;

        private string pRedBCEfetField;

        private string vBCEfetField;

        private string pICMSEfetField;

        private string vICMSEfetField;

        /// <remarks/>
        public Torig orig
        {
            get
            {
                return this.origField;
            }
            set
            {
                this.origField = value;
                this.RaisePropertyChanged("orig");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSICMSSTCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public string vBCSTRet
        {
            get
            {
                return this.vBCSTRetField;
            }
            set
            {
                this.vBCSTRetField = value;
                this.RaisePropertyChanged("vBCSTRet");
            }
        }

        /// <remarks/>
        public string pST
        {
            get
            {
                return this.pSTField;
            }
            set
            {
                this.pSTField = value;
                this.RaisePropertyChanged("pST");
            }
        }

        /// <remarks/>
        public string vICMSSubstituto
        {
            get
            {
                return this.vICMSSubstitutoField;
            }
            set
            {
                this.vICMSSubstitutoField = value;
                this.RaisePropertyChanged("vICMSSubstituto");
            }
        }

        /// <remarks/>
        public string vICMSSTRet
        {
            get
            {
                return this.vICMSSTRetField;
            }
            set
            {
                this.vICMSSTRetField = value;
                this.RaisePropertyChanged("vICMSSTRet");
            }
        }

        /// <remarks/>
        public string vBCFCPSTRet
        {
            get
            {
                return this.vBCFCPSTRetField;
            }
            set
            {
                this.vBCFCPSTRetField = value;
                this.RaisePropertyChanged("vBCFCPSTRet");
            }
        }

        /// <remarks/>
        public string pFCPSTRet
        {
            get
            {
                return this.pFCPSTRetField;
            }
            set
            {
                this.pFCPSTRetField = value;
                this.RaisePropertyChanged("pFCPSTRet");
            }
        }

        /// <remarks/>
        public string vFCPSTRet
        {
            get
            {
                return this.vFCPSTRetField;
            }
            set
            {
                this.vFCPSTRetField = value;
                this.RaisePropertyChanged("vFCPSTRet");
            }
        }

        /// <remarks/>
        public string vBCSTDest
        {
            get
            {
                return this.vBCSTDestField;
            }
            set
            {
                this.vBCSTDestField = value;
                this.RaisePropertyChanged("vBCSTDest");
            }
        }

        /// <remarks/>
        public string vICMSSTDest
        {
            get
            {
                return this.vICMSSTDestField;
            }
            set
            {
                this.vICMSSTDestField = value;
                this.RaisePropertyChanged("vICMSSTDest");
            }
        }

        /// <remarks/>
        public string pRedBCEfet
        {
            get
            {
                return this.pRedBCEfetField;
            }
            set
            {
                this.pRedBCEfetField = value;
                this.RaisePropertyChanged("pRedBCEfet");
            }
        }

        /// <remarks/>
        public string vBCEfet
        {
            get
            {
                return this.vBCEfetField;
            }
            set
            {
                this.vBCEfetField = value;
                this.RaisePropertyChanged("vBCEfet");
            }
        }

        /// <remarks/>
        public string pICMSEfet
        {
            get
            {
                return this.pICMSEfetField;
            }
            set
            {
                this.pICMSEfetField = value;
                this.RaisePropertyChanged("pICMSEfet");
            }
        }

        /// <remarks/>
        public string vICMSEfet
        {
            get
            {
                return this.vICMSEfetField;
            }
            set
            {
                this.vICMSEfetField = value;
                this.RaisePropertyChanged("vICMSEfet");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSICMSSTCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("41")]
        Item41,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("60")]
        Item60,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoII : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string vBCField;

        private string vDespAduField;

        private string vIIField;

        private string vIOFField;

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string vDespAdu
        {
            get
            {
                return this.vDespAduField;
            }
            set
            {
                this.vDespAduField = value;
                this.RaisePropertyChanged("vDespAdu");
            }
        }

        /// <remarks/>
        public string vII
        {
            get
            {
                return this.vIIField;
            }
            set
            {
                this.vIIField = value;
                this.RaisePropertyChanged("vII");
            }
        }

        /// <remarks/>
        public string vIOF
        {
            get
            {
                return this.vIOFField;
            }
            set
            {
                this.vIOFField = value;
                this.RaisePropertyChanged("vIOF");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoISSQN : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string vBCField;

        private string vAliqField;

        private string vISSQNField;

        private string cMunFGField;

        private TCListServ cListServField;

        private string vDeducaoField;

        private string vOutroField;

        private string vDescIncondField;

        private string vDescCondField;

        private string vISSRetField;

        private TNFeInfNFeDetImpostoISSQNIndISS indISSField;

        private string cServicoField;

        private string cMunField;

        private string cPaisField;

        private string nProcessoField;

        private TNFeInfNFeDetImpostoISSQNIndIncentivo indIncentivoField;

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string vAliq
        {
            get
            {
                return this.vAliqField;
            }
            set
            {
                this.vAliqField = value;
                this.RaisePropertyChanged("vAliq");
            }
        }

        /// <remarks/>
        public string vISSQN
        {
            get
            {
                return this.vISSQNField;
            }
            set
            {
                this.vISSQNField = value;
                this.RaisePropertyChanged("vISSQN");
            }
        }

        /// <remarks/>
        public string cMunFG
        {
            get
            {
                return this.cMunFGField;
            }
            set
            {
                this.cMunFGField = value;
                this.RaisePropertyChanged("cMunFG");
            }
        }

        /// <remarks/>
        public TCListServ cListServ
        {
            get
            {
                return this.cListServField;
            }
            set
            {
                this.cListServField = value;
                this.RaisePropertyChanged("cListServ");
            }
        }

        /// <remarks/>
        public string vDeducao
        {
            get
            {
                return this.vDeducaoField;
            }
            set
            {
                this.vDeducaoField = value;
                this.RaisePropertyChanged("vDeducao");
            }
        }

        /// <remarks/>
        public string vOutro
        {
            get
            {
                return this.vOutroField;
            }
            set
            {
                this.vOutroField = value;
                this.RaisePropertyChanged("vOutro");
            }
        }

        /// <remarks/>
        public string vDescIncond
        {
            get
            {
                return this.vDescIncondField;
            }
            set
            {
                this.vDescIncondField = value;
                this.RaisePropertyChanged("vDescIncond");
            }
        }

        /// <remarks/>
        public string vDescCond
        {
            get
            {
                return this.vDescCondField;
            }
            set
            {
                this.vDescCondField = value;
                this.RaisePropertyChanged("vDescCond");
            }
        }

        /// <remarks/>
        public string vISSRet
        {
            get
            {
                return this.vISSRetField;
            }
            set
            {
                this.vISSRetField = value;
                this.RaisePropertyChanged("vISSRet");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoISSQNIndISS indISS
        {
            get
            {
                return this.indISSField;
            }
            set
            {
                this.indISSField = value;
                this.RaisePropertyChanged("indISS");
            }
        }

        /// <remarks/>
        public string cServico
        {
            get
            {
                return this.cServicoField;
            }
            set
            {
                this.cServicoField = value;
                this.RaisePropertyChanged("cServico");
            }
        }

        /// <remarks/>
        public string cMun
        {
            get
            {
                return this.cMunField;
            }
            set
            {
                this.cMunField = value;
                this.RaisePropertyChanged("cMun");
            }
        }

        /// <remarks/>
        public string cPais
        {
            get
            {
                return this.cPaisField;
            }
            set
            {
                this.cPaisField = value;
                this.RaisePropertyChanged("cPais");
            }
        }

        /// <remarks/>
        public string nProcesso
        {
            get
            {
                return this.nProcessoField;
            }
            set
            {
                this.nProcessoField = value;
                this.RaisePropertyChanged("nProcesso");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoISSQNIndIncentivo indIncentivo
        {
            get
            {
                return this.indIncentivoField;
            }
            set
            {
                this.indIncentivoField = value;
                this.RaisePropertyChanged("indIncentivo");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TCListServ
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01.01")]
        Item0101,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01.02")]
        Item0102,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01.03")]
        Item0103,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01.04")]
        Item0104,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01.05")]
        Item0105,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01.06")]
        Item0106,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01.07")]
        Item0107,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01.08")]
        Item0108,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01.09")]
        Item0109,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02.01")]
        Item0201,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03.02")]
        Item0302,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03.03")]
        Item0303,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03.04")]
        Item0304,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03.05")]
        Item0305,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.01")]
        Item0401,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.02")]
        Item0402,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.03")]
        Item0403,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.04")]
        Item0404,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.05")]
        Item0405,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.06")]
        Item0406,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.07")]
        Item0407,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.08")]
        Item0408,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.09")]
        Item0409,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.10")]
        Item0410,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.11")]
        Item0411,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.12")]
        Item0412,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.13")]
        Item0413,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.14")]
        Item0414,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.15")]
        Item0415,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.16")]
        Item0416,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.17")]
        Item0417,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.18")]
        Item0418,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.19")]
        Item0419,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.20")]
        Item0420,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.21")]
        Item0421,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.22")]
        Item0422,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04.23")]
        Item0423,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05.01")]
        Item0501,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05.02")]
        Item0502,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05.03")]
        Item0503,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05.04")]
        Item0504,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05.05")]
        Item0505,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05.06")]
        Item0506,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05.07")]
        Item0507,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05.08")]
        Item0508,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05.09")]
        Item0509,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06.01")]
        Item0601,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06.02")]
        Item0602,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06.03")]
        Item0603,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06.04")]
        Item0604,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06.05")]
        Item0605,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06.06")]
        Item0606,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.01")]
        Item0701,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.02")]
        Item0702,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.03")]
        Item0703,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.04")]
        Item0704,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.05")]
        Item0705,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.06")]
        Item0706,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.07")]
        Item0707,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.08")]
        Item0708,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.09")]
        Item0709,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.10")]
        Item0710,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.11")]
        Item0711,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.12")]
        Item0712,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.13")]
        Item0713,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.16")]
        Item0716,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.17")]
        Item0717,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.18")]
        Item0718,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.19")]
        Item0719,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.20")]
        Item0720,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.21")]
        Item0721,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07.22")]
        Item0722,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08.01")]
        Item0801,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08.02")]
        Item0802,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09.01")]
        Item0901,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09.02")]
        Item0902,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09.03")]
        Item0903,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10.01")]
        Item1001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10.02")]
        Item1002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10.03")]
        Item1003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10.04")]
        Item1004,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10.05")]
        Item1005,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10.06")]
        Item1006,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10.07")]
        Item1007,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10.08")]
        Item1008,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10.09")]
        Item1009,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10.10")]
        Item1010,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11.01")]
        Item1101,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11.02")]
        Item1102,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11.03")]
        Item1103,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11.04")]
        Item1104,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.01")]
        Item1201,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.02")]
        Item1202,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.03")]
        Item1203,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.04")]
        Item1204,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.05")]
        Item1205,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.06")]
        Item1206,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.07")]
        Item1207,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.08")]
        Item1208,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.09")]
        Item1209,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.10")]
        Item1210,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.11")]
        Item1211,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.12")]
        Item1212,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.13")]
        Item1213,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.14")]
        Item1214,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.15")]
        Item1215,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.16")]
        Item1216,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.17")]
        Item1217,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13.02")]
        Item1302,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13.03")]
        Item1303,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13.04")]
        Item1304,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13.05")]
        Item1305,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.01")]
        Item1401,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.02")]
        Item1402,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.03")]
        Item1403,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.04")]
        Item1404,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.05")]
        Item1405,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.06")]
        Item1406,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.07")]
        Item1407,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.08")]
        Item1408,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.09")]
        Item1409,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.10")]
        Item1410,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.11")]
        Item1411,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.12")]
        Item1412,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.13")]
        Item1413,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14.14")]
        Item1414,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.01")]
        Item1501,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.02")]
        Item1502,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.03")]
        Item1503,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.04")]
        Item1504,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.05")]
        Item1505,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.06")]
        Item1506,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.07")]
        Item1507,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.08")]
        Item1508,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.09")]
        Item1509,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.10")]
        Item1510,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.11")]
        Item1511,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.12")]
        Item1512,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.13")]
        Item1513,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.14")]
        Item1514,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.15")]
        Item1515,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.16")]
        Item1516,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.17")]
        Item1517,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15.18")]
        Item1518,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16.01")]
        Item1601,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16.02")]
        Item1602,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.01")]
        Item1701,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.02")]
        Item1702,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.03")]
        Item1703,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.04")]
        Item1704,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.05")]
        Item1705,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.06")]
        Item1706,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.08")]
        Item1708,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.09")]
        Item1709,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.10")]
        Item1710,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.11")]
        Item1711,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.12")]
        Item1712,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.13")]
        Item1713,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.14")]
        Item1714,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.15")]
        Item1715,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.16")]
        Item1716,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.17")]
        Item1717,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.18")]
        Item1718,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.19")]
        Item1719,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.20")]
        Item1720,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.21")]
        Item1721,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.22")]
        Item1722,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.23")]
        Item1723,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.24")]
        Item1724,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17.25")]
        Item1725,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("18.01")]
        Item1801,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("19.01")]
        Item1901,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("20.01")]
        Item2001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("20.02")]
        Item2002,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("20.03")]
        Item2003,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("21.01")]
        Item2101,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("22.01")]
        Item2201,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("23.01")]
        Item2301,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("24.01")]
        Item2401,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25.01")]
        Item2501,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25.02")]
        Item2502,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25.03")]
        Item2503,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25.04")]
        Item2504,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25.05")]
        Item2505,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("26.01")]
        Item2601,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("27.01")]
        Item2701,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("28.01")]
        Item2801,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("29.01")]
        Item2901,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("30.01")]
        Item3001,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("31.01")]
        Item3101,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("32.01")]
        Item3201,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("33.01")]
        Item3301,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("34.01")]
        Item3401,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("35.01")]
        Item3501,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("36.01")]
        Item3601,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("37.01")]
        Item3701,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("38.01")]
        Item3801,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("39.01")]
        Item3901,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("40.01")]
        Item4001,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoISSQNIndISS
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        Item7,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoISSQNIndIncentivo
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoPIS : object, System.ComponentModel.INotifyPropertyChanged
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PISAliq", typeof(TNFeInfNFeDetImpostoPISPISAliq))]
        [System.Xml.Serialization.XmlElementAttribute("PISNT", typeof(TNFeInfNFeDetImpostoPISPISNT))]
        [System.Xml.Serialization.XmlElementAttribute("PISOutr", typeof(TNFeInfNFeDetImpostoPISPISOutr))]
        [System.Xml.Serialization.XmlElementAttribute("PISQtde", typeof(TNFeInfNFeDetImpostoPISPISQtde))]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoPISPISAliq : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetImpostoPISPISAliqCST cSTField;

        private string vBCField;

        private string pPISField;

        private string vPISField;

        /// <remarks/>
        public TNFeInfNFeDetImpostoPISPISAliqCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string pPIS
        {
            get
            {
                return this.pPISField;
            }
            set
            {
                this.pPISField = value;
                this.RaisePropertyChanged("pPIS");
            }
        }

        /// <remarks/>
        public string vPIS
        {
            get
            {
                return this.vPISField;
            }
            set
            {
                this.vPISField = value;
                this.RaisePropertyChanged("vPIS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoPISPISAliqCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoPISPISNT : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetImpostoPISPISNTCST cSTField;

        /// <remarks/>
        public TNFeInfNFeDetImpostoPISPISNTCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoPISPISNTCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09")]
        Item09,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoPISPISOutr : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetImpostoPISPISOutrCST cSTField;

        private string[] itemsField;

        private ItemsChoiceType1[] itemsElementNameField;

        private string vPISField;

        /// <remarks/>
        public TNFeInfNFeDetImpostoPISPISOutrCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("pPIS", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("qBCProd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vAliqProd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vBC", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public string[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType1[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
                this.RaisePropertyChanged("ItemsElementName");
            }
        }

        /// <remarks/>
        public string vPIS
        {
            get
            {
                return this.vPISField;
            }
            set
            {
                this.vPISField = value;
                this.RaisePropertyChanged("vPIS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoPISPISOutrCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("49")]
        Item49,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("50")]
        Item50,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("51")]
        Item51,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("52")]
        Item52,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("53")]
        Item53,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("54")]
        Item54,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("55")]
        Item55,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("56")]
        Item56,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("60")]
        Item60,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("61")]
        Item61,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("62")]
        Item62,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("63")]
        Item63,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("64")]
        Item64,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("65")]
        Item65,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("66")]
        Item66,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("67")]
        Item67,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("70")]
        Item70,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("71")]
        Item71,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("72")]
        Item72,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("73")]
        Item73,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("74")]
        Item74,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("75")]
        Item75,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("98")]
        Item98,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("99")]
        Item99,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemsChoiceType1
    {

        /// <remarks/>
        pPIS,

        /// <remarks/>
        qBCProd,

        /// <remarks/>
        vAliqProd,

        /// <remarks/>
        vBC,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoPISPISQtde : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetImpostoPISPISQtdeCST cSTField;

        private string qBCProdField;

        private string vAliqProdField;

        private string vPISField;

        /// <remarks/>
        public TNFeInfNFeDetImpostoPISPISQtdeCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public string qBCProd
        {
            get
            {
                return this.qBCProdField;
            }
            set
            {
                this.qBCProdField = value;
                this.RaisePropertyChanged("qBCProd");
            }
        }

        /// <remarks/>
        public string vAliqProd
        {
            get
            {
                return this.vAliqProdField;
            }
            set
            {
                this.vAliqProdField = value;
                this.RaisePropertyChanged("vAliqProd");
            }
        }

        /// <remarks/>
        public string vPIS
        {
            get
            {
                return this.vPISField;
            }
            set
            {
                this.vPISField = value;
                this.RaisePropertyChanged("vPIS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoPISPISQtdeCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoPISST : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string[] itemsField;

        private ItemsChoiceType2[] itemsElementNameField;

        private string vPISField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("pPIS", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("qBCProd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vAliqProd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vBC", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public string[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType2[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
                this.RaisePropertyChanged("ItemsElementName");
            }
        }

        /// <remarks/>
        public string vPIS
        {
            get
            {
                return this.vPISField;
            }
            set
            {
                this.vPISField = value;
                this.RaisePropertyChanged("vPIS");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemsChoiceType2
    {

        /// <remarks/>
        pPIS,

        /// <remarks/>
        qBCProd,

        /// <remarks/>
        vAliqProd,

        /// <remarks/>
        vBC,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoCOFINS : object, System.ComponentModel.INotifyPropertyChanged
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("COFINSAliq", typeof(TNFeInfNFeDetImpostoCOFINSCOFINSAliq))]
        [System.Xml.Serialization.XmlElementAttribute("COFINSNT", typeof(TNFeInfNFeDetImpostoCOFINSCOFINSNT))]
        [System.Xml.Serialization.XmlElementAttribute("COFINSOutr", typeof(TNFeInfNFeDetImpostoCOFINSCOFINSOutr))]
        [System.Xml.Serialization.XmlElementAttribute("COFINSQtde", typeof(TNFeInfNFeDetImpostoCOFINSCOFINSQtde))]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoCOFINSCOFINSAliq : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST cSTField;

        private string vBCField;

        private string pCOFINSField;

        private string vCOFINSField;

        /// <remarks/>
        public TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string pCOFINS
        {
            get
            {
                return this.pCOFINSField;
            }
            set
            {
                this.pCOFINSField = value;
                this.RaisePropertyChanged("pCOFINS");
            }
        }

        /// <remarks/>
        public string vCOFINS
        {
            get
            {
                return this.vCOFINSField;
            }
            set
            {
                this.vCOFINSField = value;
                this.RaisePropertyChanged("vCOFINS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoCOFINSCOFINSNT : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetImpostoCOFINSCOFINSNTCST cSTField;

        /// <remarks/>
        public TNFeInfNFeDetImpostoCOFINSCOFINSNTCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoCOFINSCOFINSNTCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09")]
        Item09,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoCOFINSCOFINSOutr : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST cSTField;

        private string[] itemsField;

        private ItemsChoiceType3[] itemsElementNameField;

        private string vCOFINSField;

        /// <remarks/>
        public TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("pCOFINS", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("qBCProd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vAliqProd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vBC", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public string[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType3[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
                this.RaisePropertyChanged("ItemsElementName");
            }
        }

        /// <remarks/>
        public string vCOFINS
        {
            get
            {
                return this.vCOFINSField;
            }
            set
            {
                this.vCOFINSField = value;
                this.RaisePropertyChanged("vCOFINS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("49")]
        Item49,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("50")]
        Item50,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("51")]
        Item51,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("52")]
        Item52,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("53")]
        Item53,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("54")]
        Item54,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("55")]
        Item55,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("56")]
        Item56,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("60")]
        Item60,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("61")]
        Item61,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("62")]
        Item62,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("63")]
        Item63,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("64")]
        Item64,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("65")]
        Item65,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("66")]
        Item66,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("67")]
        Item67,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("70")]
        Item70,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("71")]
        Item71,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("72")]
        Item72,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("73")]
        Item73,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("74")]
        Item74,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("75")]
        Item75,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("98")]
        Item98,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("99")]
        Item99,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemsChoiceType3
    {

        /// <remarks/>
        pCOFINS,

        /// <remarks/>
        qBCProd,

        /// <remarks/>
        vAliqProd,

        /// <remarks/>
        vBC,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoCOFINSCOFINSQtde : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeDetImpostoCOFINSCOFINSQtdeCST cSTField;

        private string qBCProdField;

        private string vAliqProdField;

        private string vCOFINSField;

        /// <remarks/>
        public TNFeInfNFeDetImpostoCOFINSCOFINSQtdeCST CST
        {
            get
            {
                return this.cSTField;
            }
            set
            {
                this.cSTField = value;
                this.RaisePropertyChanged("CST");
            }
        }

        /// <remarks/>
        public string qBCProd
        {
            get
            {
                return this.qBCProdField;
            }
            set
            {
                this.qBCProdField = value;
                this.RaisePropertyChanged("qBCProd");
            }
        }

        /// <remarks/>
        public string vAliqProd
        {
            get
            {
                return this.vAliqProdField;
            }
            set
            {
                this.vAliqProdField = value;
                this.RaisePropertyChanged("vAliqProd");
            }
        }

        /// <remarks/>
        public string vCOFINS
        {
            get
            {
                return this.vCOFINSField;
            }
            set
            {
                this.vCOFINSField = value;
                this.RaisePropertyChanged("vCOFINS");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoCOFINSCOFINSQtdeCST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoCOFINSST : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string[] itemsField;

        private ItemsChoiceType4[] itemsElementNameField;

        private string vCOFINSField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("pCOFINS", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("qBCProd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vAliqProd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("vBC", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public string[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType4[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
                this.RaisePropertyChanged("ItemsElementName");
            }
        }

        /// <remarks/>
        public string vCOFINS
        {
            get
            {
                return this.vCOFINSField;
            }
            set
            {
                this.vCOFINSField = value;
                this.RaisePropertyChanged("vCOFINS");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemsChoiceType4
    {

        /// <remarks/>
        pCOFINS,

        /// <remarks/>
        qBCProd,

        /// <remarks/>
        vAliqProd,

        /// <remarks/>
        vBC,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoICMSUFDest : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string vBCUFDestField;

        private string vBCFCPUFDestField;

        private string pFCPUFDestField;

        private string pICMSUFDestField;

        private TNFeInfNFeDetImpostoICMSUFDestPICMSInter pICMSInterField;

        private string pICMSInterPartField;

        private string vFCPUFDestField;

        private string vICMSUFDestField;

        private string vICMSUFRemetField;

        /// <remarks/>
        public string vBCUFDest
        {
            get
            {
                return this.vBCUFDestField;
            }
            set
            {
                this.vBCUFDestField = value;
                this.RaisePropertyChanged("vBCUFDest");
            }
        }

        /// <remarks/>
        public string vBCFCPUFDest
        {
            get
            {
                return this.vBCFCPUFDestField;
            }
            set
            {
                this.vBCFCPUFDestField = value;
                this.RaisePropertyChanged("vBCFCPUFDest");
            }
        }

        /// <remarks/>
        public string pFCPUFDest
        {
            get
            {
                return this.pFCPUFDestField;
            }
            set
            {
                this.pFCPUFDestField = value;
                this.RaisePropertyChanged("pFCPUFDest");
            }
        }

        /// <remarks/>
        public string pICMSUFDest
        {
            get
            {
                return this.pICMSUFDestField;
            }
            set
            {
                this.pICMSUFDestField = value;
                this.RaisePropertyChanged("pICMSUFDest");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoICMSUFDestPICMSInter pICMSInter
        {
            get
            {
                return this.pICMSInterField;
            }
            set
            {
                this.pICMSInterField = value;
                this.RaisePropertyChanged("pICMSInter");
            }
        }

        /// <remarks/>
        public string pICMSInterPart
        {
            get
            {
                return this.pICMSInterPartField;
            }
            set
            {
                this.pICMSInterPartField = value;
                this.RaisePropertyChanged("pICMSInterPart");
            }
        }

        /// <remarks/>
        public string vFCPUFDest
        {
            get
            {
                return this.vFCPUFDestField;
            }
            set
            {
                this.vFCPUFDestField = value;
                this.RaisePropertyChanged("vFCPUFDest");
            }
        }

        /// <remarks/>
        public string vICMSUFDest
        {
            get
            {
                return this.vICMSUFDestField;
            }
            set
            {
                this.vICMSUFDestField = value;
                this.RaisePropertyChanged("vICMSUFDest");
            }
        }

        /// <remarks/>
        public string vICMSUFRemet
        {
            get
            {
                return this.vICMSUFRemetField;
            }
            set
            {
                this.vICMSUFRemetField = value;
                this.RaisePropertyChanged("vICMSUFRemet");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeDetImpostoICMSUFDestPICMSInter
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4.00")]
        Item400,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7.00")]
        Item700,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12.00")]
        Item1200,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoDevol : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string pDevolField;

        private TNFeInfNFeDetImpostoDevolIPI iPIField;

        /// <remarks/>
        public string pDevol
        {
            get
            {
                return this.pDevolField;
            }
            set
            {
                this.pDevolField = value;
                this.RaisePropertyChanged("pDevol");
            }
        }

        /// <remarks/>
        public TNFeInfNFeDetImpostoDevolIPI IPI
        {
            get
            {
                return this.iPIField;
            }
            set
            {
                this.iPIField = value;
                this.RaisePropertyChanged("IPI");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeDetImpostoDevolIPI : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string vIPIDevolField;

        /// <remarks/>
        public string vIPIDevol
        {
            get
            {
                return this.vIPIDevolField;
            }
            set
            {
                this.vIPIDevolField = value;
                this.RaisePropertyChanged("vIPIDevol");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeTotal : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeTotalICMSTot iCMSTotField;

        private TNFeInfNFeTotalISSQNtot iSSQNtotField;

        private TNFeInfNFeTotalRetTrib retTribField;

        /// <remarks/>
        public TNFeInfNFeTotalICMSTot ICMSTot
        {
            get
            {
                return this.iCMSTotField;
            }
            set
            {
                this.iCMSTotField = value;
                this.RaisePropertyChanged("ICMSTot");
            }
        }

        /// <remarks/>
        public TNFeInfNFeTotalISSQNtot ISSQNtot
        {
            get
            {
                return this.iSSQNtotField;
            }
            set
            {
                this.iSSQNtotField = value;
                this.RaisePropertyChanged("ISSQNtot");
            }
        }

        /// <remarks/>
        public TNFeInfNFeTotalRetTrib retTrib
        {
            get
            {
                return this.retTribField;
            }
            set
            {
                this.retTribField = value;
                this.RaisePropertyChanged("retTrib");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeTotalICMSTot : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string vBCField;

        private string vICMSField;

        private string vICMSDesonField;

        private string vFCPUFDestField;

        private string vICMSUFDestField;

        private string vICMSUFRemetField;

        private string vFCPField;

        private string vBCSTField;

        private string vSTField;

        private string vFCPSTField;

        private string vFCPSTRetField;

        private string vProdField;

        private string vFreteField;

        private string vSegField;

        private string vDescField;

        private string vIIField;

        private string vIPIField;

        private string vIPIDevolField;

        private string vPISField;

        private string vCOFINSField;

        private string vOutroField;

        private string vNFField;

        private string vTotTribField;

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string vICMS
        {
            get
            {
                return this.vICMSField;
            }
            set
            {
                this.vICMSField = value;
                this.RaisePropertyChanged("vICMS");
            }
        }

        /// <remarks/>
        public string vICMSDeson
        {
            get
            {
                return this.vICMSDesonField;
            }
            set
            {
                this.vICMSDesonField = value;
                this.RaisePropertyChanged("vICMSDeson");
            }
        }

        /// <remarks/>
        public string vFCPUFDest
        {
            get
            {
                return this.vFCPUFDestField;
            }
            set
            {
                this.vFCPUFDestField = value;
                this.RaisePropertyChanged("vFCPUFDest");
            }
        }

        /// <remarks/>
        public string vICMSUFDest
        {
            get
            {
                return this.vICMSUFDestField;
            }
            set
            {
                this.vICMSUFDestField = value;
                this.RaisePropertyChanged("vICMSUFDest");
            }
        }

        /// <remarks/>
        public string vICMSUFRemet
        {
            get
            {
                return this.vICMSUFRemetField;
            }
            set
            {
                this.vICMSUFRemetField = value;
                this.RaisePropertyChanged("vICMSUFRemet");
            }
        }

        /// <remarks/>
        public string vFCP
        {
            get
            {
                return this.vFCPField;
            }
            set
            {
                this.vFCPField = value;
                this.RaisePropertyChanged("vFCP");
            }
        }

        /// <remarks/>
        public string vBCST
        {
            get
            {
                return this.vBCSTField;
            }
            set
            {
                this.vBCSTField = value;
                this.RaisePropertyChanged("vBCST");
            }
        }

        /// <remarks/>
        public string vST
        {
            get
            {
                return this.vSTField;
            }
            set
            {
                this.vSTField = value;
                this.RaisePropertyChanged("vST");
            }
        }

        /// <remarks/>
        public string vFCPST
        {
            get
            {
                return this.vFCPSTField;
            }
            set
            {
                this.vFCPSTField = value;
                this.RaisePropertyChanged("vFCPST");
            }
        }

        /// <remarks/>
        public string vFCPSTRet
        {
            get
            {
                return this.vFCPSTRetField;
            }
            set
            {
                this.vFCPSTRetField = value;
                this.RaisePropertyChanged("vFCPSTRet");
            }
        }

        /// <remarks/>
        public string vProd
        {
            get
            {
                return this.vProdField;
            }
            set
            {
                this.vProdField = value;
                this.RaisePropertyChanged("vProd");
            }
        }

        /// <remarks/>
        public string vFrete
        {
            get
            {
                return this.vFreteField;
            }
            set
            {
                this.vFreteField = value;
                this.RaisePropertyChanged("vFrete");
            }
        }

        /// <remarks/>
        public string vSeg
        {
            get
            {
                return this.vSegField;
            }
            set
            {
                this.vSegField = value;
                this.RaisePropertyChanged("vSeg");
            }
        }

        /// <remarks/>
        public string vDesc
        {
            get
            {
                return this.vDescField;
            }
            set
            {
                this.vDescField = value;
                this.RaisePropertyChanged("vDesc");
            }
        }

        /// <remarks/>
        public string vII
        {
            get
            {
                return this.vIIField;
            }
            set
            {
                this.vIIField = value;
                this.RaisePropertyChanged("vII");
            }
        }

        /// <remarks/>
        public string vIPI
        {
            get
            {
                return this.vIPIField;
            }
            set
            {
                this.vIPIField = value;
                this.RaisePropertyChanged("vIPI");
            }
        }

        /// <remarks/>
        public string vIPIDevol
        {
            get
            {
                return this.vIPIDevolField;
            }
            set
            {
                this.vIPIDevolField = value;
                this.RaisePropertyChanged("vIPIDevol");
            }
        }

        /// <remarks/>
        public string vPIS
        {
            get
            {
                return this.vPISField;
            }
            set
            {
                this.vPISField = value;
                this.RaisePropertyChanged("vPIS");
            }
        }

        /// <remarks/>
        public string vCOFINS
        {
            get
            {
                return this.vCOFINSField;
            }
            set
            {
                this.vCOFINSField = value;
                this.RaisePropertyChanged("vCOFINS");
            }
        }

        /// <remarks/>
        public string vOutro
        {
            get
            {
                return this.vOutroField;
            }
            set
            {
                this.vOutroField = value;
                this.RaisePropertyChanged("vOutro");
            }
        }

        /// <remarks/>
        public string vNF
        {
            get
            {
                return this.vNFField;
            }
            set
            {
                this.vNFField = value;
                this.RaisePropertyChanged("vNF");
            }
        }

        /// <remarks/>
        public string vTotTrib
        {
            get
            {
                return this.vTotTribField;
            }
            set
            {
                this.vTotTribField = value;
                this.RaisePropertyChanged("vTotTrib");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeTotalISSQNtot : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string vServField;

        private string vBCField;

        private string vISSField;

        private string vPISField;

        private string vCOFINSField;

        private string dCompetField;

        private string vDeducaoField;

        private string vOutroField;

        private string vDescIncondField;

        private string vDescCondField;

        private string vISSRetField;

        private TNFeInfNFeTotalISSQNtotCRegTrib cRegTribField;

        private bool cRegTribFieldSpecified;

        /// <remarks/>
        public string vServ
        {
            get
            {
                return this.vServField;
            }
            set
            {
                this.vServField = value;
                this.RaisePropertyChanged("vServ");
            }
        }

        /// <remarks/>
        public string vBC
        {
            get
            {
                return this.vBCField;
            }
            set
            {
                this.vBCField = value;
                this.RaisePropertyChanged("vBC");
            }
        }

        /// <remarks/>
        public string vISS
        {
            get
            {
                return this.vISSField;
            }
            set
            {
                this.vISSField = value;
                this.RaisePropertyChanged("vISS");
            }
        }

        /// <remarks/>
        public string vPIS
        {
            get
            {
                return this.vPISField;
            }
            set
            {
                this.vPISField = value;
                this.RaisePropertyChanged("vPIS");
            }
        }

        /// <remarks/>
        public string vCOFINS
        {
            get
            {
                return this.vCOFINSField;
            }
            set
            {
                this.vCOFINSField = value;
                this.RaisePropertyChanged("vCOFINS");
            }
        }

        /// <remarks/>
        public string dCompet
        {
            get
            {
                return this.dCompetField;
            }
            set
            {
                this.dCompetField = value;
                this.RaisePropertyChanged("dCompet");
            }
        }

        /// <remarks/>
        public string vDeducao
        {
            get
            {
                return this.vDeducaoField;
            }
            set
            {
                this.vDeducaoField = value;
                this.RaisePropertyChanged("vDeducao");
            }
        }

        /// <remarks/>
        public string vOutro
        {
            get
            {
                return this.vOutroField;
            }
            set
            {
                this.vOutroField = value;
                this.RaisePropertyChanged("vOutro");
            }
        }

        /// <remarks/>
        public string vDescIncond
        {
            get
            {
                return this.vDescIncondField;
            }
            set
            {
                this.vDescIncondField = value;
                this.RaisePropertyChanged("vDescIncond");
            }
        }

        /// <remarks/>
        public string vDescCond
        {
            get
            {
                return this.vDescCondField;
            }
            set
            {
                this.vDescCondField = value;
                this.RaisePropertyChanged("vDescCond");
            }
        }

        /// <remarks/>
        public string vISSRet
        {
            get
            {
                return this.vISSRetField;
            }
            set
            {
                this.vISSRetField = value;
                this.RaisePropertyChanged("vISSRet");
            }
        }

        /// <remarks/>
        public TNFeInfNFeTotalISSQNtotCRegTrib cRegTrib
        {
            get
            {
                return this.cRegTribField;
            }
            set
            {
                this.cRegTribField = value;
                this.RaisePropertyChanged("cRegTrib");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cRegTribSpecified
        {
            get
            {
                return this.cRegTribFieldSpecified;
            }
            set
            {
                this.cRegTribFieldSpecified = value;
                this.RaisePropertyChanged("cRegTribSpecified");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeTotalISSQNtotCRegTrib
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeTotalRetTrib : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string vRetPISField;

        private string vRetCOFINSField;

        private string vRetCSLLField;

        private string vBCIRRFField;

        private string vIRRFField;

        private string vBCRetPrevField;

        private string vRetPrevField;

        /// <remarks/>
        public string vRetPIS
        {
            get
            {
                return this.vRetPISField;
            }
            set
            {
                this.vRetPISField = value;
                this.RaisePropertyChanged("vRetPIS");
            }
        }

        /// <remarks/>
        public string vRetCOFINS
        {
            get
            {
                return this.vRetCOFINSField;
            }
            set
            {
                this.vRetCOFINSField = value;
                this.RaisePropertyChanged("vRetCOFINS");
            }
        }

        /// <remarks/>
        public string vRetCSLL
        {
            get
            {
                return this.vRetCSLLField;
            }
            set
            {
                this.vRetCSLLField = value;
                this.RaisePropertyChanged("vRetCSLL");
            }
        }

        /// <remarks/>
        public string vBCIRRF
        {
            get
            {
                return this.vBCIRRFField;
            }
            set
            {
                this.vBCIRRFField = value;
                this.RaisePropertyChanged("vBCIRRF");
            }
        }

        /// <remarks/>
        public string vIRRF
        {
            get
            {
                return this.vIRRFField;
            }
            set
            {
                this.vIRRFField = value;
                this.RaisePropertyChanged("vIRRF");
            }
        }

        /// <remarks/>
        public string vBCRetPrev
        {
            get
            {
                return this.vBCRetPrevField;
            }
            set
            {
                this.vBCRetPrevField = value;
                this.RaisePropertyChanged("vBCRetPrev");
            }
        }

        /// <remarks/>
        public string vRetPrev
        {
            get
            {
                return this.vRetPrevField;
            }
            set
            {
                this.vRetPrevField = value;
                this.RaisePropertyChanged("vRetPrev");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeTransp : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeTranspModFrete modFreteField;

        private TNFeInfNFeTranspTransporta transportaField;

        private TNFeInfNFeTranspRetTransp retTranspField;

        private object[] itemsField;

        private ItemsChoiceType5[] itemsElementNameField;

        private TNFeInfNFeTranspVol[] volField;

        /// <remarks/>
        public TNFeInfNFeTranspModFrete modFrete
        {
            get
            {
                return this.modFreteField;
            }
            set
            {
                this.modFreteField = value;
                this.RaisePropertyChanged("modFrete");
            }
        }

        /// <remarks/>
        public TNFeInfNFeTranspTransporta transporta
        {
            get
            {
                return this.transportaField;
            }
            set
            {
                this.transportaField = value;
                this.RaisePropertyChanged("transporta");
            }
        }

        /// <remarks/>
        public TNFeInfNFeTranspRetTransp retTransp
        {
            get
            {
                return this.retTranspField;
            }
            set
            {
                this.retTranspField = value;
                this.RaisePropertyChanged("retTransp");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("balsa", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("reboque", typeof(TVeiculo))]
        [System.Xml.Serialization.XmlElementAttribute("vagao", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("veicTransp", typeof(TVeiculo))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType5[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
                this.RaisePropertyChanged("ItemsElementName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("vol")]
        public TNFeInfNFeTranspVol[] vol
        {
            get
            {
                return this.volField;
            }
            set
            {
                this.volField = value;
                this.RaisePropertyChanged("vol");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeTranspModFrete
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeTranspTransporta : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string itemField;

        private ItemChoiceType6 itemElementNameField;

        private string xNomeField;

        private string ieField;

        private string xEnderField;

        private string xMunField;

        private TUf ufField;

        private bool ufFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CNPJ", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CPF", typeof(string))]
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
        public ItemChoiceType6 ItemElementName
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

        /// <remarks/>
        public string xNome
        {
            get
            {
                return this.xNomeField;
            }
            set
            {
                this.xNomeField = value;
                this.RaisePropertyChanged("xNome");
            }
        }

        /// <remarks/>
        public string IE
        {
            get
            {
                return this.ieField;
            }
            set
            {
                this.ieField = value;
                this.RaisePropertyChanged("IE");
            }
        }

        /// <remarks/>
        public string xEnder
        {
            get
            {
                return this.xEnderField;
            }
            set
            {
                this.xEnderField = value;
                this.RaisePropertyChanged("xEnder");
            }
        }

        /// <remarks/>
        public string xMun
        {
            get
            {
                return this.xMunField;
            }
            set
            {
                this.xMunField = value;
                this.RaisePropertyChanged("xMun");
            }
        }

        /// <remarks/>
        public TUf UF
        {
            get
            {
                return this.ufField;
            }
            set
            {
                this.ufField = value;
                this.RaisePropertyChanged("UF");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UFSpecified
        {
            get
            {
                return this.ufFieldSpecified;
            }
            set
            {
                this.ufFieldSpecified = value;
                this.RaisePropertyChanged("UFSpecified");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemChoiceType6
    {

        /// <remarks/>
        CNPJ,

        /// <remarks/>
        CPF,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeTranspRetTransp : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string vServField;

        private string vBCRetField;

        private string pICMSRetField;

        private string vICMSRetField;

        private string cFOPField;

        private string cMunFGField;

        /// <remarks/>
        public string vServ
        {
            get
            {
                return this.vServField;
            }
            set
            {
                this.vServField = value;
                this.RaisePropertyChanged("vServ");
            }
        }

        /// <remarks/>
        public string vBCRet
        {
            get
            {
                return this.vBCRetField;
            }
            set
            {
                this.vBCRetField = value;
                this.RaisePropertyChanged("vBCRet");
            }
        }

        /// <remarks/>
        public string pICMSRet
        {
            get
            {
                return this.pICMSRetField;
            }
            set
            {
                this.pICMSRetField = value;
                this.RaisePropertyChanged("pICMSRet");
            }
        }

        /// <remarks/>
        public string vICMSRet
        {
            get
            {
                return this.vICMSRetField;
            }
            set
            {
                this.vICMSRetField = value;
                this.RaisePropertyChanged("vICMSRet");
            }
        }

        /// <remarks/>
        public string CFOP
        {
            get
            {
                return this.cFOPField;
            }
            set
            {
                this.cFOPField = value;
                this.RaisePropertyChanged("CFOP");
            }
        }

        /// <remarks/>
        public string cMunFG
        {
            get
            {
                return this.cMunFGField;
            }
            set
            {
                this.cMunFGField = value;
                this.RaisePropertyChanged("cMunFG");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ItemsChoiceType5
    {

        /// <remarks/>
        balsa,

        /// <remarks/>
        reboque,

        /// <remarks/>
        vagao,

        /// <remarks/>
        veicTransp,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeTranspVol : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string qVolField;

        private string espField;

        private string marcaField;

        private string nVolField;

        private string pesoLField;

        private string pesoBField;

        private TNFeInfNFeTranspVolLacres[] lacresField;

        /// <remarks/>
        public string qVol
        {
            get
            {
                return this.qVolField;
            }
            set
            {
                this.qVolField = value;
                this.RaisePropertyChanged("qVol");
            }
        }

        /// <remarks/>
        public string esp
        {
            get
            {
                return this.espField;
            }
            set
            {
                this.espField = value;
                this.RaisePropertyChanged("esp");
            }
        }

        /// <remarks/>
        public string marca
        {
            get
            {
                return this.marcaField;
            }
            set
            {
                this.marcaField = value;
                this.RaisePropertyChanged("marca");
            }
        }

        /// <remarks/>
        public string nVol
        {
            get
            {
                return this.nVolField;
            }
            set
            {
                this.nVolField = value;
                this.RaisePropertyChanged("nVol");
            }
        }

        /// <remarks/>
        public string pesoL
        {
            get
            {
                return this.pesoLField;
            }
            set
            {
                this.pesoLField = value;
                this.RaisePropertyChanged("pesoL");
            }
        }

        /// <remarks/>
        public string pesoB
        {
            get
            {
                return this.pesoBField;
            }
            set
            {
                this.pesoBField = value;
                this.RaisePropertyChanged("pesoB");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("lacres")]
        public TNFeInfNFeTranspVolLacres[] lacres
        {
            get
            {
                return this.lacresField;
            }
            set
            {
                this.lacresField = value;
                this.RaisePropertyChanged("lacres");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeTranspVolLacres : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nLacreField;

        /// <remarks/>
        public string nLacre
        {
            get
            {
                return this.nLacreField;
            }
            set
            {
                this.nLacreField = value;
                this.RaisePropertyChanged("nLacre");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeCobr : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFeCobrFat fatField;

        private TNFeInfNFeCobrDup[] dupField;

        /// <remarks/>
        public TNFeInfNFeCobrFat fat
        {
            get
            {
                return this.fatField;
            }
            set
            {
                this.fatField = value;
                this.RaisePropertyChanged("fat");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("dup")]
        public TNFeInfNFeCobrDup[] dup
        {
            get
            {
                return this.dupField;
            }
            set
            {
                this.dupField = value;
                this.RaisePropertyChanged("dup");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeCobrFat : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nFatField;

        private string vOrigField;

        private string vDescField;

        private string vLiqField;

        /// <remarks/>
        public string nFat
        {
            get
            {
                return this.nFatField;
            }
            set
            {
                this.nFatField = value;
                this.RaisePropertyChanged("nFat");
            }
        }

        /// <remarks/>
        public string vOrig
        {
            get
            {
                return this.vOrigField;
            }
            set
            {
                this.vOrigField = value;
                this.RaisePropertyChanged("vOrig");
            }
        }

        /// <remarks/>
        public string vDesc
        {
            get
            {
                return this.vDescField;
            }
            set
            {
                this.vDescField = value;
                this.RaisePropertyChanged("vDesc");
            }
        }

        /// <remarks/>
        public string vLiq
        {
            get
            {
                return this.vLiqField;
            }
            set
            {
                this.vLiqField = value;
                this.RaisePropertyChanged("vLiq");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeCobrDup : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nDupField;

        private string dVencField;

        private string vDupField;

        /// <remarks/>
        public string nDup
        {
            get
            {
                return this.nDupField;
            }
            set
            {
                this.nDupField = value;
                this.RaisePropertyChanged("nDup");
            }
        }

        /// <remarks/>
        public string dVenc
        {
            get
            {
                return this.dVencField;
            }
            set
            {
                this.dVencField = value;
                this.RaisePropertyChanged("dVenc");
            }
        }

        /// <remarks/>
        public string vDup
        {
            get
            {
                return this.vDupField;
            }
            set
            {
                this.vDupField = value;
                this.RaisePropertyChanged("vDup");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFePag : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFePagDetPag[] detPagField;

        private string vTrocoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("detPag")]
        public TNFeInfNFePagDetPag[] detPag
        {
            get
            {
                return this.detPagField;
            }
            set
            {
                this.detPagField = value;
                this.RaisePropertyChanged("detPag");
            }
        }

        /// <remarks/>
        public string vTroco
        {
            get
            {
                return this.vTrocoField;
            }
            set
            {
                this.vTrocoField = value;
                this.RaisePropertyChanged("vTroco");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFePagDetPag : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFePagDetPagIndPag indPagField;

        private bool indPagFieldSpecified;

        private TNFeInfNFePagDetPagTPag tPagField;

        private string vPagField;

        private TNFeInfNFePagDetPagCard cardField;

        /// <remarks/>
        public TNFeInfNFePagDetPagIndPag indPag
        {
            get
            {
                return this.indPagField;
            }
            set
            {
                this.indPagField = value;
                this.RaisePropertyChanged("indPag");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool indPagSpecified
        {
            get
            {
                return this.indPagFieldSpecified;
            }
            set
            {
                this.indPagFieldSpecified = value;
                this.RaisePropertyChanged("indPagSpecified");
            }
        }

        /// <remarks/>
        public TNFeInfNFePagDetPagTPag tPag
        {
            get
            {
                return this.tPagField;
            }
            set
            {
                this.tPagField = value;
                this.RaisePropertyChanged("tPag");
            }
        }

        /// <remarks/>
        public string vPag
        {
            get
            {
                return this.vPagField;
            }
            set
            {
                this.vPagField = value;
                this.RaisePropertyChanged("vPag");
            }
        }

        /// <remarks/>
        public TNFeInfNFePagDetPagCard card
        {
            get
            {
                return this.cardField;
            }
            set
            {
                this.cardField = value;
                this.RaisePropertyChanged("card");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFePagDetPagIndPag
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFePagDetPagTPag
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("90")]
        Item90,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("99")]
        Item99,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFePagDetPagCard : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TNFeInfNFePagDetPagCardTpIntegra tpIntegraField;

        private string cNPJField;

        private TNFeInfNFePagDetPagCardTBand tBandField;

        private bool tBandFieldSpecified;

        private string cAutField;

        /// <remarks/>
        public TNFeInfNFePagDetPagCardTpIntegra tpIntegra
        {
            get
            {
                return this.tpIntegraField;
            }
            set
            {
                this.tpIntegraField = value;
                this.RaisePropertyChanged("tpIntegra");
            }
        }

        /// <remarks/>
        public string CNPJ
        {
            get
            {
                return this.cNPJField;
            }
            set
            {
                this.cNPJField = value;
                this.RaisePropertyChanged("CNPJ");
            }
        }

        /// <remarks/>
        public TNFeInfNFePagDetPagCardTBand tBand
        {
            get
            {
                return this.tBandField;
            }
            set
            {
                this.tBandField = value;
                this.RaisePropertyChanged("tBand");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool tBandSpecified
        {
            get
            {
                return this.tBandFieldSpecified;
            }
            set
            {
                this.tBandFieldSpecified = value;
                this.RaisePropertyChanged("tBandSpecified");
            }
        }

        /// <remarks/>
        public string cAut
        {
            get
            {
                return this.cAutField;
            }
            set
            {
                this.cAutField = value;
                this.RaisePropertyChanged("cAut");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFePagDetPagCardTpIntegra
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFePagDetPagCardTBand
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("07")]
        Item07,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("09")]
        Item09,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("99")]
        Item99,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeInfAdic : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string infAdFiscoField;

        private string infCplField;

        private TNFeInfNFeInfAdicObsCont[] obsContField;

        private TNFeInfNFeInfAdicObsFisco[] obsFiscoField;

        private TNFeInfNFeInfAdicProcRef[] procRefField;

        /// <remarks/>
        public string infAdFisco
        {
            get
            {
                return this.infAdFiscoField;
            }
            set
            {
                this.infAdFiscoField = value;
                this.RaisePropertyChanged("infAdFisco");
            }
        }

        /// <remarks/>
        public string infCpl
        {
            get
            {
                return this.infCplField;
            }
            set
            {
                this.infCplField = value;
                this.RaisePropertyChanged("infCpl");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("obsCont")]
        public TNFeInfNFeInfAdicObsCont[] obsCont
        {
            get
            {
                return this.obsContField;
            }
            set
            {
                this.obsContField = value;
                this.RaisePropertyChanged("obsCont");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("obsFisco")]
        public TNFeInfNFeInfAdicObsFisco[] obsFisco
        {
            get
            {
                return this.obsFiscoField;
            }
            set
            {
                this.obsFiscoField = value;
                this.RaisePropertyChanged("obsFisco");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("procRef")]
        public TNFeInfNFeInfAdicProcRef[] procRef
        {
            get
            {
                return this.procRefField;
            }
            set
            {
                this.procRefField = value;
                this.RaisePropertyChanged("procRef");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeInfAdicObsCont : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string xTextoField;

        private string xCampoField;

        /// <remarks/>
        public string xTexto
        {
            get
            {
                return this.xTextoField;
            }
            set
            {
                this.xTextoField = value;
                this.RaisePropertyChanged("xTexto");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string xCampo
        {
            get
            {
                return this.xCampoField;
            }
            set
            {
                this.xCampoField = value;
                this.RaisePropertyChanged("xCampo");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeInfAdicObsFisco : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string xTextoField;

        private string xCampoField;

        /// <remarks/>
        public string xTexto
        {
            get
            {
                return this.xTextoField;
            }
            set
            {
                this.xTextoField = value;
                this.RaisePropertyChanged("xTexto");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string xCampo
        {
            get
            {
                return this.xCampoField;
            }
            set
            {
                this.xCampoField = value;
                this.RaisePropertyChanged("xCampo");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeInfAdicProcRef : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string nProcField;

        private TNFeInfNFeInfAdicProcRefIndProc indProcField;

        /// <remarks/>
        public string nProc
        {
            get
            {
                return this.nProcField;
            }
            set
            {
                this.nProcField = value;
                this.RaisePropertyChanged("nProc");
            }
        }

        /// <remarks/>
        public TNFeInfNFeInfAdicProcRefIndProc indProc
        {
            get
            {
                return this.indProcField;
            }
            set
            {
                this.indProcField = value;
                this.RaisePropertyChanged("indProc");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeInfAdicProcRefIndProc
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        Item9,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeExporta : object, System.ComponentModel.INotifyPropertyChanged
    {

        private TUfEmi uFSaidaPaisField;

        private string xLocExportaField;

        private string xLocDespachoField;

        /// <remarks/>
        public TUfEmi UFSaidaPais
        {
            get
            {
                return this.uFSaidaPaisField;
            }
            set
            {
                this.uFSaidaPaisField = value;
                this.RaisePropertyChanged("UFSaidaPais");
            }
        }

        /// <remarks/>
        public string xLocExporta
        {
            get
            {
                return this.xLocExportaField;
            }
            set
            {
                this.xLocExportaField = value;
                this.RaisePropertyChanged("xLocExporta");
            }
        }

        /// <remarks/>
        public string xLocDespacho
        {
            get
            {
                return this.xLocDespachoField;
            }
            set
            {
                this.xLocDespachoField = value;
                this.RaisePropertyChanged("xLocDespacho");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeCompra : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string xNEmpField;

        private string xPedField;

        private string xContField;

        /// <remarks/>
        public string xNEmp
        {
            get
            {
                return this.xNEmpField;
            }
            set
            {
                this.xNEmpField = value;
                this.RaisePropertyChanged("xNEmp");
            }
        }

        /// <remarks/>
        public string xPed
        {
            get
            {
                return this.xPedField;
            }
            set
            {
                this.xPedField = value;
                this.RaisePropertyChanged("xPed");
            }
        }

        /// <remarks/>
        public string xCont
        {
            get
            {
                return this.xContField;
            }
            set
            {
                this.xContField = value;
                this.RaisePropertyChanged("xCont");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeCana : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string safraField;

        private string refField;

        private TNFeInfNFeCanaForDia[] forDiaField;

        private string qTotMesField;

        private string qTotAntField;

        private string qTotGerField;

        private TNFeInfNFeCanaDeduc[] deducField;

        private string vForField;

        private string vTotDedField;

        private string vLiqForField;

        /// <remarks/>
        public string safra
        {
            get
            {
                return this.safraField;
            }
            set
            {
                this.safraField = value;
                this.RaisePropertyChanged("safra");
            }
        }

        /// <remarks/>
        public string @ref
        {
            get
            {
                return this.refField;
            }
            set
            {
                this.refField = value;
                this.RaisePropertyChanged("ref");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("forDia")]
        public TNFeInfNFeCanaForDia[] forDia
        {
            get
            {
                return this.forDiaField;
            }
            set
            {
                this.forDiaField = value;
                this.RaisePropertyChanged("forDia");
            }
        }

        /// <remarks/>
        public string qTotMes
        {
            get
            {
                return this.qTotMesField;
            }
            set
            {
                this.qTotMesField = value;
                this.RaisePropertyChanged("qTotMes");
            }
        }

        /// <remarks/>
        public string qTotAnt
        {
            get
            {
                return this.qTotAntField;
            }
            set
            {
                this.qTotAntField = value;
                this.RaisePropertyChanged("qTotAnt");
            }
        }

        /// <remarks/>
        public string qTotGer
        {
            get
            {
                return this.qTotGerField;
            }
            set
            {
                this.qTotGerField = value;
                this.RaisePropertyChanged("qTotGer");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("deduc")]
        public TNFeInfNFeCanaDeduc[] deduc
        {
            get
            {
                return this.deducField;
            }
            set
            {
                this.deducField = value;
                this.RaisePropertyChanged("deduc");
            }
        }

        /// <remarks/>
        public string vFor
        {
            get
            {
                return this.vForField;
            }
            set
            {
                this.vForField = value;
                this.RaisePropertyChanged("vFor");
            }
        }

        /// <remarks/>
        public string vTotDed
        {
            get
            {
                return this.vTotDedField;
            }
            set
            {
                this.vTotDedField = value;
                this.RaisePropertyChanged("vTotDed");
            }
        }

        /// <remarks/>
        public string vLiqFor
        {
            get
            {
                return this.vLiqForField;
            }
            set
            {
                this.vLiqForField = value;
                this.RaisePropertyChanged("vLiqFor");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeCanaForDia : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string qtdeField;

        private string diaField;

        /// <remarks/>
        public string qtde
        {
            get
            {
                return this.qtdeField;
            }
            set
            {
                this.qtdeField = value;
                this.RaisePropertyChanged("qtde");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string dia
        {
            get
            {
                return this.diaField;
            }
            set
            {
                this.diaField = value;
                this.RaisePropertyChanged("dia");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeCanaDeduc : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string xDedField;

        private string vDedField;

        /// <remarks/>
        public string xDed
        {
            get
            {
                return this.xDedField;
            }
            set
            {
                this.xDedField = value;
                this.RaisePropertyChanged("xDed");
            }
        }

        /// <remarks/>
        public string vDed
        {
            get
            {
                return this.vDedField;
            }
            set
            {
                this.vDedField = value;
                this.RaisePropertyChanged("vDed");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public partial class TNFeInfNFeSupl : object, System.ComponentModel.INotifyPropertyChanged
    {

        private string qrCodeField;

        private string urlChaveField;

        /// <remarks/>
        public string qrCode
        {
            get
            {
                return this.qrCodeField;
            }
            set
            {
                this.qrCodeField = value;
                this.RaisePropertyChanged("qrCode");
            }
        }

        /// <remarks/>
        public string urlChave
        {
            get
            {
                return this.urlChaveField;
            }
            set
            {
                this.urlChaveField = value;
                this.RaisePropertyChanged("urlChave");
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

}
