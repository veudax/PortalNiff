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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class ListaMensagemRetorno
    {

        private tcMensagemRetorno[] mensagemRetornoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MensagemRetorno")]
        public tcMensagemRetorno[] MensagemRetorno
        {
            get
            {
                return this.mensagemRetornoField;
            }
            set
            {
                this.mensagemRetornoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcMensagemRetorno
    {

        private string codigoField;

        private string mensagemField;

        private string correcaoField;

        /// <remarks/>
        public string Codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        /// <remarks/>
        public string Mensagem
        {
            get
            {
                return this.mensagemField;
            }
            set
            {
                this.mensagemField = value;
            }
        }

        /// <remarks/>
        public string Correcao
        {
            get
            {
                return this.correcaoField;
            }
            set
            {
                this.correcaoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class CanonicalizationMethodType
    {

        private System.Xml.XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
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
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureMethodType
    {

        private string hMACOutputLengthField;

        private System.Xml.XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string HMACOutputLength
        {
            get
            {
                return this.hMACOutputLengthField;
            }
            set
            {
                this.hMACOutputLengthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
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
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class DigestMethodType
    {

        private System.Xml.XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
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
            }
        }
    }
    

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("KeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class KeyValueType
    {

        private object itemField;

        private string[] textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DSAKeyValue", typeof(DSAKeyValueType))]
        [System.Xml.Serialization.XmlElementAttribute("RSAKeyValue", typeof(RSAKeyValueType))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("DSAKeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class DSAKeyValueType
    {

        private byte[] pField;

        private byte[] qField;

        private byte[] gField;

        private byte[] yField;

        private byte[] jField;

        private byte[] seedField;

        private byte[] pgenCounterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] P
        {
            get
            {
                return this.pField;
            }
            set
            {
                this.pField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Q
        {
            get
            {
                return this.qField;
            }
            set
            {
                this.qField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] G
        {
            get
            {
                return this.gField;
            }
            set
            {
                this.gField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] J
        {
            get
            {
                return this.jField;
            }
            set
            {
                this.jField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Seed
        {
            get
            {
                return this.seedField;
            }
            set
            {
                this.seedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] PgenCounter
        {
            get
            {
                return this.pgenCounterField;
            }
            set
            {
                this.pgenCounterField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("RSAKeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class RSAKeyValueType
    {

        private byte[] modulusField;

        private byte[] exponentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Modulus
        {
            get
            {
                return this.modulusField;
            }
            set
            {
                this.modulusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] Exponent
        {
            get
            {
                return this.exponentField;
            }
            set
            {
                this.exponentField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("PGPData", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class PGPDataType
    {

        private object[] itemsField;

        private ItemsChoiceType1G[] itemsElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("PGPKeyID", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlElementAttribute("PGPKeyPacket", typeof(byte[]), DataType = "base64Binary")]
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
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType1G[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType1G
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,

        /// <remarks/>
        PGPKeyID,

        /// <remarks/>
        PGPKeyPacket,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("RetrievalMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class RetrievalMethodType
    {

        private TransformType[] transformsField;

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
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("SPKIData", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SPKIDataType
    {

        private byte[][] sPKISexpField;

        private System.Xml.XmlElement anyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SPKISexp", DataType = "base64Binary")]
        public byte[][] SPKISexp
        {
            get
            {
                return this.sPKISexpField;
            }
            set
            {
                this.sPKISexpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class X509IssuerSerialType
    {

        private string x509IssuerNameField;

        private string x509SerialNumberField;

        /// <remarks/>
        public string X509IssuerName
        {
            get
            {
                return this.x509IssuerNameField;
            }
            set
            {
                this.x509IssuerNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string X509SerialNumber
        {
            get
            {
                return this.x509SerialNumberField;
            }
            set
            {
                this.x509SerialNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceTypeG
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,

        /// <remarks/>
        X509CRL,

        /// <remarks/>
        X509Certificate,

        /// <remarks/>
        X509IssuerSerial,

        /// <remarks/>
        X509SKI,

        /// <remarks/>
        X509SubjectName,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType2G
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,

        /// <remarks/>
        KeyName,

        /// <remarks/>
        KeyValue,

        /// <remarks/>
        MgmtData,

        /// <remarks/>
        PGPData,

        /// <remarks/>
        RetrievalMethod,

        /// <remarks/>
        SPKIData,

        /// <remarks/>
        X509Data,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("Object", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ObjectType
    {

        private System.Xml.XmlNode[] anyField;

        private string idField;

        private string mimeTypeField;

        private string encodingField;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
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
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MimeType
        {
            get
            {
                return this.mimeTypeField;
            }
            set
            {
                this.mimeTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Encoding
        {
            get
            {
                return this.encodingField;
            }
            set
            {
                this.encodingField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class TransformsType
    {

        private TransformType[] transformField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Transform")]
        public TransformType[] Transform
        {
            get
            {
                return this.transformField;
            }
            set
            {
                this.transformField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("Manifest", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ManifestType
    {

        private ReferenceType[] referenceField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Reference")]
        public ReferenceType[] Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
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
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("SignatureProperties", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignaturePropertiesType
    {

        private SignaturePropertyType[] signaturePropertyField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SignatureProperty")]
        public SignaturePropertyType[] SignatureProperty
        {
            get
            {
                return this.signaturePropertyField;
            }
            set
            {
                this.signaturePropertyField = value;
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
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRootAttribute("SignatureProperty", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignaturePropertyType
    {

        private System.Xml.XmlElement[] itemsField;

        private string[] textField;

        private string targetField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Target
        {
            get
            {
                return this.targetField;
            }
            set
            {
                this.targetField = value;
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
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ginfes.com.br/servico_consultar_nfse_resposta_v03.xsd")]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/servico_consultar_nfse_resposta_v03.xsd", IsNullable = false)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class ConsultarNfseResposta
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ListaNfse", typeof(ConsultarNfseRespostaListaNfse))]
        [System.Xml.Serialization.XmlElementAttribute("ListaMensagemRetorno", typeof(ListaMensagemRetorno), Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ginfes.com.br/servico_consultar_nfse_resposta_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class ConsultarNfseRespostaListaNfse
    {

        private tcCompNfse[] compNfseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CompNfse")]
        public tcCompNfse[] CompNfse
        {
            get
            {
                return this.compNfseField;
            }
            set
            {
                this.compNfseField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcCompNfse
    {

        private tcNfse nfseField;

        private tcCancelamentoNfse nfseCancelamentoField;

        private tcSubstituicaoNfse nfseSubstituicaoField;

        /// <remarks/>
        public tcNfse Nfse
        {
            get
            {
                return this.nfseField;
            }
            set
            {
                this.nfseField = value;
            }
        }

        /// <remarks/>
        public tcCancelamentoNfse NfseCancelamento
        {
            get
            {
                return this.nfseCancelamentoField;
            }
            set
            {
                this.nfseCancelamentoField = value;
            }
        }

        /// <remarks/>
        public tcSubstituicaoNfse NfseSubstituicao
        {
            get
            {
                return this.nfseSubstituicaoField;
            }
            set
            {
                this.nfseSubstituicaoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute("Nfse", Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcNfse
    {

        private tcInfNfse infNfseField;

        private SignatureType[] signatureField;

        /// <remarks/>
        public tcInfNfse InfNfse
        {
            get
            {
                return this.infNfseField;
            }
            set
            {
                this.infNfseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType[] Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcInfNfse
    {

        private string numeroField;

        private string codigoVerificacaoField;

        private System.DateTime dataEmissaoField;

        private tcIdentificacaoRps identificacaoRpsField;

        private System.DateTime dataEmissaoRpsField;

        private bool dataEmissaoRpsFieldSpecified;

        private sbyte naturezaOperacaoField;

        private sbyte regimeEspecialTributacaoField;

        private bool regimeEspecialTributacaoFieldSpecified;

        private sbyte optanteSimplesNacionalField;

        private sbyte incentivadorCulturalField;

        private System.DateTime competenciaField;

        private string nfseSubstituidaField;

        private string outrasInformacoesField;

        private tcDadosServico servicoField;

        private decimal valorCreditoField;

        private bool valorCreditoFieldSpecified;

        private tcDadosPrestador prestadorServicoField;

        private tcDadosTomador tomadorServicoField;

        private tcIdentificacaoIntermediarioServico intermediarioServicoField;

        private tcIdentificacaoOrgaoGerador orgaoGeradorField;

        private tcDadosConstrucaoCivil construcaoCivilField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string Numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
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
        public System.DateTime DataEmissao
        {
            get
            {
                return this.dataEmissaoField;
            }
            set
            {
                this.dataEmissaoField = value;
            }
        }

        /// <remarks/>
        public tcIdentificacaoRps IdentificacaoRps
        {
            get
            {
                return this.identificacaoRpsField;
            }
            set
            {
                this.identificacaoRpsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime DataEmissaoRps
        {
            get
            {
                return this.dataEmissaoRpsField;
            }
            set
            {
                this.dataEmissaoRpsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DataEmissaoRpsSpecified
        {
            get
            {
                return this.dataEmissaoRpsFieldSpecified;
            }
            set
            {
                this.dataEmissaoRpsFieldSpecified = value;
            }
        }

        /// <remarks/>
        public sbyte NaturezaOperacao
        {
            get
            {
                return this.naturezaOperacaoField;
            }
            set
            {
                this.naturezaOperacaoField = value;
            }
        }

        /// <remarks/>
        public sbyte RegimeEspecialTributacao
        {
            get
            {
                return this.regimeEspecialTributacaoField;
            }
            set
            {
                this.regimeEspecialTributacaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RegimeEspecialTributacaoSpecified
        {
            get
            {
                return this.regimeEspecialTributacaoFieldSpecified;
            }
            set
            {
                this.regimeEspecialTributacaoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public sbyte OptanteSimplesNacional
        {
            get
            {
                return this.optanteSimplesNacionalField;
            }
            set
            {
                this.optanteSimplesNacionalField = value;
            }
        }

        /// <remarks/>
        public sbyte IncentivadorCultural
        {
            get
            {
                return this.incentivadorCulturalField;
            }
            set
            {
                this.incentivadorCulturalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime Competencia
        {
            get
            {
                return this.competenciaField;
            }
            set
            {
                this.competenciaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string NfseSubstituida
        {
            get
            {
                return this.nfseSubstituidaField;
            }
            set
            {
                this.nfseSubstituidaField = value;
            }
        }

        /// <remarks/>
        public string OutrasInformacoes
        {
            get
            {
                return this.outrasInformacoesField;
            }
            set
            {
                this.outrasInformacoesField = value;
            }
        }

        /// <remarks/>
        public tcDadosServico Servico
        {
            get
            {
                return this.servicoField;
            }
            set
            {
                this.servicoField = value;
            }
        }

        /// <remarks/>
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorCreditoSpecified
        {
            get
            {
                return this.valorCreditoFieldSpecified;
            }
            set
            {
                this.valorCreditoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public tcDadosPrestador PrestadorServico
        {
            get
            {
                return this.prestadorServicoField;
            }
            set
            {
                this.prestadorServicoField = value;
            }
        }

        /// <remarks/>
        public tcDadosTomador TomadorServico
        {
            get
            {
                return this.tomadorServicoField;
            }
            set
            {
                this.tomadorServicoField = value;
            }
        }

        /// <remarks/>
        public tcIdentificacaoIntermediarioServico IntermediarioServico
        {
            get
            {
                return this.intermediarioServicoField;
            }
            set
            {
                this.intermediarioServicoField = value;
            }
        }

        /// <remarks/>
        public tcIdentificacaoOrgaoGerador OrgaoGerador
        {
            get
            {
                return this.orgaoGeradorField;
            }
            set
            {
                this.orgaoGeradorField = value;
            }
        }

        /// <remarks/>
        public tcDadosConstrucaoCivil ConstrucaoCivil
        {
            get
            {
                return this.construcaoCivilField;
            }
            set
            {
                this.construcaoCivilField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcIdentificacaoRps
    {

        private string numeroField;

        private string serieField;

        private sbyte tipoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string Numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
        public string Serie
        {
            get
            {
                return this.serieField;
            }
            set
            {
                this.serieField = value;
            }
        }

        /// <remarks/>
        public sbyte Tipo
        {
            get
            {
                return this.tipoField;
            }
            set
            {
                this.tipoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcDadosServico
    {

        private tcValores valoresField;

        private string itemListaServicoField;

        private int codigoCnaeField;

        private bool codigoCnaeFieldSpecified;

        private string codigoTributacaoMunicipioField;

        private string discriminacaoField;

        private int codigoMunicipioField;

        /// <remarks/>
        public tcValores Valores
        {
            get
            {
                return this.valoresField;
            }
            set
            {
                this.valoresField = value;
            }
        }

        /// <remarks/>
        public string ItemListaServico
        {
            get
            {
                return this.itemListaServicoField;
            }
            set
            {
                this.itemListaServicoField = value;
            }
        }

        /// <remarks/>
        public int CodigoCnae
        {
            get
            {
                return this.codigoCnaeField;
            }
            set
            {
                this.codigoCnaeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CodigoCnaeSpecified
        {
            get
            {
                return this.codigoCnaeFieldSpecified;
            }
            set
            {
                this.codigoCnaeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string CodigoTributacaoMunicipio
        {
            get
            {
                return this.codigoTributacaoMunicipioField;
            }
            set
            {
                this.codigoTributacaoMunicipioField = value;
            }
        }

        /// <remarks/>
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
        public int CodigoMunicipio
        {
            get
            {
                return this.codigoMunicipioField;
            }
            set
            {
                this.codigoMunicipioField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcValores
    {

        private decimal valorServicosField;

        private decimal valorDeducoesField;

        private bool valorDeducoesFieldSpecified;

        private decimal valorPisField;

        private bool valorPisFieldSpecified;

        private decimal valorCofinsField;

        private bool valorCofinsFieldSpecified;

        private decimal valorInssField;

        private bool valorInssFieldSpecified;

        private decimal valorIrField;

        private bool valorIrFieldSpecified;

        private decimal valorCsllField;

        private bool valorCsllFieldSpecified;

        private sbyte issRetidoField;

        private decimal valorIssField;

        private bool valorIssFieldSpecified;

        private decimal valorIssRetidoField;

        private bool valorIssRetidoFieldSpecified;

        private decimal outrasRetencoesField;

        private bool outrasRetencoesFieldSpecified;

        private decimal baseCalculoField;

        private bool baseCalculoFieldSpecified;

        private decimal aliquotaField;

        private bool aliquotaFieldSpecified;

        private decimal valorLiquidoNfseField;

        private bool valorLiquidoNfseFieldSpecified;

        private decimal descontoIncondicionadoField;

        private bool descontoIncondicionadoFieldSpecified;

        private decimal descontoCondicionadoField;

        private bool descontoCondicionadoFieldSpecified;

        /// <remarks/>
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
        public decimal ValorPis
        {
            get
            {
                return this.valorPisField;
            }
            set
            {
                this.valorPisField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorPisSpecified
        {
            get
            {
                return this.valorPisFieldSpecified;
            }
            set
            {
                this.valorPisFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorCofins
        {
            get
            {
                return this.valorCofinsField;
            }
            set
            {
                this.valorCofinsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorCofinsSpecified
        {
            get
            {
                return this.valorCofinsFieldSpecified;
            }
            set
            {
                this.valorCofinsFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorInss
        {
            get
            {
                return this.valorInssField;
            }
            set
            {
                this.valorInssField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorInssSpecified
        {
            get
            {
                return this.valorInssFieldSpecified;
            }
            set
            {
                this.valorInssFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorIr
        {
            get
            {
                return this.valorIrField;
            }
            set
            {
                this.valorIrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorIrSpecified
        {
            get
            {
                return this.valorIrFieldSpecified;
            }
            set
            {
                this.valorIrFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorCsll
        {
            get
            {
                return this.valorCsllField;
            }
            set
            {
                this.valorCsllField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorCsllSpecified
        {
            get
            {
                return this.valorCsllFieldSpecified;
            }
            set
            {
                this.valorCsllFieldSpecified = value;
            }
        }

        /// <remarks/>
        public sbyte IssRetido
        {
            get
            {
                return this.issRetidoField;
            }
            set
            {
                this.issRetidoField = value;
            }
        }

        /// <remarks/>
        public decimal ValorIss
        {
            get
            {
                return this.valorIssField;
            }
            set
            {
                this.valorIssField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorIssSpecified
        {
            get
            {
                return this.valorIssFieldSpecified;
            }
            set
            {
                this.valorIssFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorIssRetido
        {
            get
            {
                return this.valorIssRetidoField;
            }
            set
            {
                this.valorIssRetidoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorIssRetidoSpecified
        {
            get
            {
                return this.valorIssRetidoFieldSpecified;
            }
            set
            {
                this.valorIssRetidoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal OutrasRetencoes
        {
            get
            {
                return this.outrasRetencoesField;
            }
            set
            {
                this.outrasRetencoesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OutrasRetencoesSpecified
        {
            get
            {
                return this.outrasRetencoesFieldSpecified;
            }
            set
            {
                this.outrasRetencoesFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal BaseCalculo
        {
            get
            {
                return this.baseCalculoField;
            }
            set
            {
                this.baseCalculoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BaseCalculoSpecified
        {
            get
            {
                return this.baseCalculoFieldSpecified;
            }
            set
            {
                this.baseCalculoFieldSpecified = value;
            }
        }

        /// <remarks/>
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

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AliquotaSpecified
        {
            get
            {
                return this.aliquotaFieldSpecified;
            }
            set
            {
                this.aliquotaFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal ValorLiquidoNfse
        {
            get
            {
                return this.valorLiquidoNfseField;
            }
            set
            {
                this.valorLiquidoNfseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValorLiquidoNfseSpecified
        {
            get
            {
                return this.valorLiquidoNfseFieldSpecified;
            }
            set
            {
                this.valorLiquidoNfseFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal DescontoIncondicionado
        {
            get
            {
                return this.descontoIncondicionadoField;
            }
            set
            {
                this.descontoIncondicionadoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DescontoIncondicionadoSpecified
        {
            get
            {
                return this.descontoIncondicionadoFieldSpecified;
            }
            set
            {
                this.descontoIncondicionadoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal DescontoCondicionado
        {
            get
            {
                return this.descontoCondicionadoField;
            }
            set
            {
                this.descontoCondicionadoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DescontoCondicionadoSpecified
        {
            get
            {
                return this.descontoCondicionadoFieldSpecified;
            }
            set
            {
                this.descontoCondicionadoFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcDadosPrestador
    {

        private tcIdentificacaoPrestador identificacaoPrestadorField;

        private string razaoSocialField;

        private string nomeFantasiaField;

        private tcEndereco enderecoField;

        private tcContato contatoField;

        /// <remarks/>
        public tcIdentificacaoPrestador IdentificacaoPrestador
        {
            get
            {
                return this.identificacaoPrestadorField;
            }
            set
            {
                this.identificacaoPrestadorField = value;
            }
        }

        /// <remarks/>
        public string RazaoSocial
        {
            get
            {
                return this.razaoSocialField;
            }
            set
            {
                this.razaoSocialField = value;
            }
        }

        /// <remarks/>
        public string NomeFantasia
        {
            get
            {
                return this.nomeFantasiaField;
            }
            set
            {
                this.nomeFantasiaField = value;
            }
        }

        /// <remarks/>
        public tcEndereco Endereco
        {
            get
            {
                return this.enderecoField;
            }
            set
            {
                this.enderecoField = value;
            }
        }

        /// <remarks/>
        public tcContato Contato
        {
            get
            {
                return this.contatoField;
            }
            set
            {
                this.contatoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcIdentificacaoPrestador
    {

        private string cnpjField;

        private string inscricaoMunicipalField;

        /// <remarks/>
        public string Cnpj
        {
            get
            {
                return this.cnpjField;
            }
            set
            {
                this.cnpjField = value;
            }
        }

        /// <remarks/>
        public string InscricaoMunicipal
        {
            get
            {
                return this.inscricaoMunicipalField;
            }
            set
            {
                this.inscricaoMunicipalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcEndereco
    {

        private string enderecoField;

        private string numeroField;

        private string complementoField;

        private string bairroField;

        private int codigoMunicipioField;

        private bool codigoMunicipioFieldSpecified;

        private string ufField;

        private int cepField;

        private bool cepFieldSpecified;

        /// <remarks/>
        public string Endereco
        {
            get
            {
                return this.enderecoField;
            }
            set
            {
                this.enderecoField = value;
            }
        }

        /// <remarks/>
        public string Numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
        public string Complemento
        {
            get
            {
                return this.complementoField;
            }
            set
            {
                this.complementoField = value;
            }
        }

        /// <remarks/>
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
        public int CodigoMunicipio
        {
            get
            {
                return this.codigoMunicipioField;
            }
            set
            {
                this.codigoMunicipioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CodigoMunicipioSpecified
        {
            get
            {
                return this.codigoMunicipioFieldSpecified;
            }
            set
            {
                this.codigoMunicipioFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string Uf
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
        public int Cep
        {
            get
            {
                return this.cepField;
            }
            set
            {
                this.cepField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CepSpecified
        {
            get
            {
                return this.cepFieldSpecified;
            }
            set
            {
                this.cepFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcContato
    {

        private string telefoneField;

        private string emailField;

        /// <remarks/>
        public string Telefone
        {
            get
            {
                return this.telefoneField;
            }
            set
            {
                this.telefoneField = value;
            }
        }

        /// <remarks/>
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcDadosTomador
    {

        private tcIdentificacaoTomador identificacaoTomadorField;

        private string razaoSocialField;

        private tcEndereco enderecoField;

        private tcContato contatoField;

        /// <remarks/>
        public tcIdentificacaoTomador IdentificacaoTomador
        {
            get
            {
                return this.identificacaoTomadorField;
            }
            set
            {
                this.identificacaoTomadorField = value;
            }
        }

        /// <remarks/>
        public string RazaoSocial
        {
            get
            {
                return this.razaoSocialField;
            }
            set
            {
                this.razaoSocialField = value;
            }
        }

        /// <remarks/>
        public tcEndereco Endereco
        {
            get
            {
                return this.enderecoField;
            }
            set
            {
                this.enderecoField = value;
            }
        }

        /// <remarks/>
        public tcContato Contato
        {
            get
            {
                return this.contatoField;
            }
            set
            {
                this.contatoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcIdentificacaoTomador
    {

        private tcCpfCnpj cpfCnpjField;

        private string inscricaoMunicipalField;

        /// <remarks/>
        public tcCpfCnpj CpfCnpj
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
        public string InscricaoMunicipal
        {
            get
            {
                return this.inscricaoMunicipalField;
            }
            set
            {
                this.inscricaoMunicipalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcCpfCnpj
    {

        private string itemField;

        private ItemChoiceTypeG itemElementNameField;  

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cnpj", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Cpf", typeof(string))]
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
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceTypeG ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IncludeInSchema = false)]
    public enum ItemChoiceTypeG
    {

        /// <remarks/>
        Cnpj,

        /// <remarks/>
        Cpf,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcIdentificacaoIntermediarioServico
    {

        private string razaoSocialField;

        private tcCpfCnpj cpfCnpjField;

        private string inscricaoMunicipalField;

        /// <remarks/>
        public string RazaoSocial
        {
            get
            {
                return this.razaoSocialField;
            }
            set
            {
                this.razaoSocialField = value;
            }
        }

        /// <remarks/>
        public tcCpfCnpj CpfCnpj
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
        public string InscricaoMunicipal
        {
            get
            {
                return this.inscricaoMunicipalField;
            }
            set
            {
                this.inscricaoMunicipalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcIdentificacaoOrgaoGerador
    {

        private int codigoMunicipioField;

        private string ufField;

        /// <remarks/>
        public int CodigoMunicipio
        {
            get
            {
                return this.codigoMunicipioField;
            }
            set
            {
                this.codigoMunicipioField = value;
            }
        }

        /// <remarks/>
        public string Uf
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcDadosConstrucaoCivil
    {

        private string codigoObraField;

        private string artField;

        /// <remarks/>
        public string CodigoObra
        {
            get
            {
                return this.codigoObraField;
            }
            set
            {
                this.codigoObraField = value;
            }
        }

        /// <remarks/>
        public string Art
        {
            get
            {
                return this.artField;
            }
            set
            {
                this.artField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcCancelamentoNfse
    {

        private tcConfirmacaoCancelamento confirmacaoField;

        private SignatureType signatureField;

        /// <remarks/>
        public tcConfirmacaoCancelamento Confirmacao
        {
            get
            {
                return this.confirmacaoField;
            }
            set
            {
                this.confirmacaoField = value;
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
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcConfirmacaoCancelamento
    {

        private tcPedidoCancelamento pedidoField;

        private tcInfConfirmacaoCancelamento infConfirmacaoCancelamentoField;

        private string idField;

        /// <remarks/>
        public tcPedidoCancelamento Pedido
        {
            get
            {
                return this.pedidoField;
            }
            set
            {
                this.pedidoField = value;
            }
        }

        /// <remarks/>
        public tcInfConfirmacaoCancelamento InfConfirmacaoCancelamento
        {
            get
            {
                return this.infConfirmacaoCancelamentoField;
            }
            set
            {
                this.infConfirmacaoCancelamentoField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcPedidoCancelamento
    {

        private tcInfPedidoCancelamento infPedidoCancelamentoField;

        private SignatureType signatureField;

        /// <remarks/>
        public tcInfPedidoCancelamento InfPedidoCancelamento
        {
            get
            {
                return this.infPedidoCancelamentoField;
            }
            set
            {
                this.infPedidoCancelamentoField = value;
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
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcInfPedidoCancelamento
    {

        private tcIdentificacaoNfse identificacaoNfseField;

        private string codigoCancelamentoField;

        private string idField;

        /// <remarks/>
        public tcIdentificacaoNfse IdentificacaoNfse
        {
            get
            {
                return this.identificacaoNfseField;
            }
            set
            {
                this.identificacaoNfseField = value;
            }
        }

        /// <remarks/>
        public string CodigoCancelamento
        {
            get
            {
                return this.codigoCancelamentoField;
            }
            set
            {
                this.codigoCancelamentoField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcIdentificacaoNfse
    {

        private string numeroField;

        private string cnpjField;

        private string inscricaoMunicipalField;

        private int codigoMunicipioField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string Numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = value;
            }
        }

        /// <remarks/>
        public string Cnpj
        {
            get
            {
                return this.cnpjField;
            }
            set
            {
                this.cnpjField = value;
            }
        }

        /// <remarks/>
        public string InscricaoMunicipal
        {
            get
            {
                return this.inscricaoMunicipalField;
            }
            set
            {
                this.inscricaoMunicipalField = value;
            }
        }

        /// <remarks/>
        public int CodigoMunicipio
        {
            get
            {
                return this.codigoMunicipioField;
            }
            set
            {
                this.codigoMunicipioField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcInfConfirmacaoCancelamento
    {

        private bool sucessoField;

        private System.DateTime dataHoraField;

        /// <remarks/>
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
        public System.DateTime DataHora
        {
            get
            {
                return this.dataHoraField;
            }
            set
            {
                this.dataHoraField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcSubstituicaoNfse
    {

        private tcInfSubstituicaoNfse substituicaoNfseField;

        private SignatureType[] signatureField;

        /// <remarks/>
        public tcInfSubstituicaoNfse SubstituicaoNfse
        {
            get
            {
                return this.substituicaoNfseField;
            }
            set
            {
                this.substituicaoNfseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType[] Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class tcInfSubstituicaoNfse
    {

        private string nfseSubstituidoraField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string NfseSubstituidora
        {
            get
            {
                return this.nfseSubstituidoraField;
            }
            set
            {
                this.nfseSubstituidoraField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ginfes.com.br/servico_consultar_lote_rps_resposta_v03.xsd")]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/servico_consultar_lote_rps_resposta_v03.xsd", IsNullable = false)]
    [System.Xml.Serialization.XmlRootAttribute("Nfse", Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class ConsultarLoteRpsResposta
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ListaNfse", typeof(ConsultarLoteRpsRespostaListaNfse))]
        [System.Xml.Serialization.XmlElementAttribute("ListaMensagemRetorno", typeof(ListaMensagemRetorno), Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ginfes.com.br/servico_consultar_lote_rps_resposta_v03.xsd")]
    [System.Xml.Serialization.XmlRootAttribute("Nfse", Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class ConsultarLoteRpsRespostaListaNfse
    {

        private tcCompNfse[] compNfseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CompNfse")]
        public tcCompNfse[] CompNfse
        {
            get
            {
                return this.compNfseField;
            }
            set
            {
                this.compNfseField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ginfes.com.br/servico_consultar_nfse_rps_resposta_v03.xsd")]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ginfes.com.br/servico_consultar_nfse_rps_resposta_v03.xsd", IsNullable = false)]
    [System.Xml.Serialization.XmlRootAttribute("Nfse", Namespace = "http://www.ginfes.com.br/tipos_v03.xsd", IsNullable = false)]
    public partial class ConsultarNfseRpsResposta
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CompNfse", typeof(tcCompNfse))]
        [System.Xml.Serialization.XmlElementAttribute("ListaMensagemRetorno", typeof(ListaMensagemRetorno), Namespace = "http://www.ginfes.com.br/tipos_v03.xsd")]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

}
