//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Este código-fonte foi gerado automaticamente por Microsoft.VSDesigner, Versão 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Site.wsLogin {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="LoginWSSoap", Namespace="http://niff.com.br/suportte/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Usuario))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(object[]))]
    public partial class LoginWS : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ValidarUsuarioOperationCompleted;
        
        private System.Threading.SendOrPostCallback AlterarStatusUsuarioOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public LoginWS() {
            this.Url = global::Site.Properties.Settings.Default.Site_wsLogin_LoginWS;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ValidarUsuarioCompletedEventHandler ValidarUsuarioCompleted;
        
        /// <remarks/>
        public event AlterarStatusUsuarioCompletedEventHandler AlterarStatusUsuarioCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://niff.com.br/suportte/ValidarUsuario", RequestNamespace="http://niff.com.br/suportte/", ResponseNamespace="http://niff.com.br/suportte/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public object[] ValidarUsuario(string usuario, string senha) {
            object[] results = this.Invoke("ValidarUsuario", new object[] {
                        usuario,
                        senha});
            return ((object[])(results[0]));
        }
        
        /// <remarks/>
        public void ValidarUsuarioAsync(string usuario, string senha) {
            this.ValidarUsuarioAsync(usuario, senha, null);
        }
        
        /// <remarks/>
        public void ValidarUsuarioAsync(string usuario, string senha, object userState) {
            if ((this.ValidarUsuarioOperationCompleted == null)) {
                this.ValidarUsuarioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnValidarUsuarioOperationCompleted);
            }
            this.InvokeAsync("ValidarUsuario", new object[] {
                        usuario,
                        senha}, this.ValidarUsuarioOperationCompleted, userState);
        }
        
        private void OnValidarUsuarioOperationCompleted(object arg) {
            if ((this.ValidarUsuarioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ValidarUsuarioCompleted(this, new ValidarUsuarioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://niff.com.br/suportte/AlterarStatusUsuario", RequestNamespace="http://niff.com.br/suportte/", ResponseNamespace="http://niff.com.br/suportte/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool AlterarStatusUsuario(int idUsuario, object status) {
            object[] results = this.Invoke("AlterarStatusUsuario", new object[] {
                        idUsuario,
                        status});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void AlterarStatusUsuarioAsync(int idUsuario, object status) {
            this.AlterarStatusUsuarioAsync(idUsuario, status, null);
        }
        
        /// <remarks/>
        public void AlterarStatusUsuarioAsync(int idUsuario, object status, object userState) {
            if ((this.AlterarStatusUsuarioOperationCompleted == null)) {
                this.AlterarStatusUsuarioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAlterarStatusUsuarioOperationCompleted);
            }
            this.InvokeAsync("AlterarStatusUsuario", new object[] {
                        idUsuario,
                        status}, this.AlterarStatusUsuarioOperationCompleted, userState);
        }
        
        private void OnAlterarStatusUsuarioOperationCompleted(object arg) {
            if ((this.AlterarStatusUsuarioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AlterarStatusUsuarioCompleted(this, new AlterarStatusUsuarioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://niff.com.br/suportte/")]
    public partial class Usuario {
        
        private int idField;
        
        private string usuarioAcessoField;
        
        private TipoUsuario tipoField;
        
        private TipoUsuarioSAC tipoSacField;
        
        private string nomeField;
        
        private bool ativoField;
        
        private bool administradorField;
        
        private string ipMaquinaField;
        
        private string nomeMaquinaField;
        
        private int telefoneField;
        
        private int ramalField;
        
        private string emailField;
        
        private string senhaField;
        
        private string cargoField;
        
        private System.DateTime dataNascimentoField;
        
        private byte[] fotoField;
        
        private bool acessaAgendaField;
        
        private bool acessaChatField;
        
        private bool permiteExcluirChatField;
        
        private bool acessaBIField;
        
        private bool permiteIncluirExcluirFotoField;
        
        private string emailAcessoPowerBiField;
        
        private string setorField;
        
        private bool existeField;
        
        private int idEmpresaField;
        
        private string empresaField;
        
        private decimal codigoInternoFuncionarioGlobusField;
        
        private string registroFuncionarioField;
        
        private bool acessaSacField;
        
        private int idDepartamentoField;
        
        private string departamentoField;
        
        private decimal cPFField;
        
        private string emailDepartamentoField;
        
        private bool acessaDescontoBeneficioField;
        
        private bool acessaJuridicoField;
        
        private bool acessaCadastroJuridicoField;
        
        private bool permiteAprovarComunicadoField;
        
        private bool permiteReprovarComunicadoField;
        
        private bool permiteCancelarComunicadoField;
        
        private int idCargoField;
        
        private bool permiteAlterarComunicadoField;
        
        private bool permiteFinalizarComunicadoField;
        
        private bool acessaDashBoardChamadosField;
        
        private bool acessaAvaliacaoDesempenhoField;
        
        private bool acessoDeRHField;
        
        private bool acessoDeGestorField;
        
        private bool acessoDeColaboradorField;
        
        private bool acessoDeControladoriaField;
        
        private bool naoNotificaCorridasField;
        
        private bool aniversariantesApenasDaEmpresaField;
        
        private bool visualizaRadarCompletoField;
        
        /// <remarks/>
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string UsuarioAcesso {
            get {
                return this.usuarioAcessoField;
            }
            set {
                this.usuarioAcessoField = value;
            }
        }
        
        /// <remarks/>
        public TipoUsuario Tipo {
            get {
                return this.tipoField;
            }
            set {
                this.tipoField = value;
            }
        }
        
        /// <remarks/>
        public TipoUsuarioSAC TipoSac {
            get {
                return this.tipoSacField;
            }
            set {
                this.tipoSacField = value;
            }
        }
        
        /// <remarks/>
        public string Nome {
            get {
                return this.nomeField;
            }
            set {
                this.nomeField = value;
            }
        }
        
        /// <remarks/>
        public bool Ativo {
            get {
                return this.ativoField;
            }
            set {
                this.ativoField = value;
            }
        }
        
        /// <remarks/>
        public bool Administrador {
            get {
                return this.administradorField;
            }
            set {
                this.administradorField = value;
            }
        }
        
        /// <remarks/>
        public string IpMaquina {
            get {
                return this.ipMaquinaField;
            }
            set {
                this.ipMaquinaField = value;
            }
        }
        
        /// <remarks/>
        public string NomeMaquina {
            get {
                return this.nomeMaquinaField;
            }
            set {
                this.nomeMaquinaField = value;
            }
        }
        
        /// <remarks/>
        public int Telefone {
            get {
                return this.telefoneField;
            }
            set {
                this.telefoneField = value;
            }
        }
        
        /// <remarks/>
        public int Ramal {
            get {
                return this.ramalField;
            }
            set {
                this.ramalField = value;
            }
        }
        
        /// <remarks/>
        public string Email {
            get {
                return this.emailField;
            }
            set {
                this.emailField = value;
            }
        }
        
        /// <remarks/>
        public string Senha {
            get {
                return this.senhaField;
            }
            set {
                this.senhaField = value;
            }
        }
        
        /// <remarks/>
        public string Cargo {
            get {
                return this.cargoField;
            }
            set {
                this.cargoField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime DataNascimento {
            get {
                return this.dataNascimentoField;
            }
            set {
                this.dataNascimentoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] Foto {
            get {
                return this.fotoField;
            }
            set {
                this.fotoField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessaAgenda {
            get {
                return this.acessaAgendaField;
            }
            set {
                this.acessaAgendaField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessaChat {
            get {
                return this.acessaChatField;
            }
            set {
                this.acessaChatField = value;
            }
        }
        
        /// <remarks/>
        public bool PermiteExcluirChat {
            get {
                return this.permiteExcluirChatField;
            }
            set {
                this.permiteExcluirChatField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessaBI {
            get {
                return this.acessaBIField;
            }
            set {
                this.acessaBIField = value;
            }
        }
        
        /// <remarks/>
        public bool PermiteIncluirExcluirFoto {
            get {
                return this.permiteIncluirExcluirFotoField;
            }
            set {
                this.permiteIncluirExcluirFotoField = value;
            }
        }
        
        /// <remarks/>
        public string EmailAcessoPowerBi {
            get {
                return this.emailAcessoPowerBiField;
            }
            set {
                this.emailAcessoPowerBiField = value;
            }
        }
        
        /// <remarks/>
        public string Setor {
            get {
                return this.setorField;
            }
            set {
                this.setorField = value;
            }
        }
        
        /// <remarks/>
        public bool Existe {
            get {
                return this.existeField;
            }
            set {
                this.existeField = value;
            }
        }
        
        /// <remarks/>
        public int IdEmpresa {
            get {
                return this.idEmpresaField;
            }
            set {
                this.idEmpresaField = value;
            }
        }
        
        /// <remarks/>
        public string Empresa {
            get {
                return this.empresaField;
            }
            set {
                this.empresaField = value;
            }
        }
        
        /// <remarks/>
        public decimal CodigoInternoFuncionarioGlobus {
            get {
                return this.codigoInternoFuncionarioGlobusField;
            }
            set {
                this.codigoInternoFuncionarioGlobusField = value;
            }
        }
        
        /// <remarks/>
        public string RegistroFuncionario {
            get {
                return this.registroFuncionarioField;
            }
            set {
                this.registroFuncionarioField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessaSac {
            get {
                return this.acessaSacField;
            }
            set {
                this.acessaSacField = value;
            }
        }
        
        /// <remarks/>
        public int IdDepartamento {
            get {
                return this.idDepartamentoField;
            }
            set {
                this.idDepartamentoField = value;
            }
        }
        
        /// <remarks/>
        public string Departamento {
            get {
                return this.departamentoField;
            }
            set {
                this.departamentoField = value;
            }
        }
        
        /// <remarks/>
        public decimal CPF {
            get {
                return this.cPFField;
            }
            set {
                this.cPFField = value;
            }
        }
        
        /// <remarks/>
        public string EmailDepartamento {
            get {
                return this.emailDepartamentoField;
            }
            set {
                this.emailDepartamentoField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessaDescontoBeneficio {
            get {
                return this.acessaDescontoBeneficioField;
            }
            set {
                this.acessaDescontoBeneficioField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessaJuridico {
            get {
                return this.acessaJuridicoField;
            }
            set {
                this.acessaJuridicoField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessaCadastroJuridico {
            get {
                return this.acessaCadastroJuridicoField;
            }
            set {
                this.acessaCadastroJuridicoField = value;
            }
        }
        
        /// <remarks/>
        public bool PermiteAprovarComunicado {
            get {
                return this.permiteAprovarComunicadoField;
            }
            set {
                this.permiteAprovarComunicadoField = value;
            }
        }
        
        /// <remarks/>
        public bool PermiteReprovarComunicado {
            get {
                return this.permiteReprovarComunicadoField;
            }
            set {
                this.permiteReprovarComunicadoField = value;
            }
        }
        
        /// <remarks/>
        public bool PermiteCancelarComunicado {
            get {
                return this.permiteCancelarComunicadoField;
            }
            set {
                this.permiteCancelarComunicadoField = value;
            }
        }
        
        /// <remarks/>
        public int IdCargo {
            get {
                return this.idCargoField;
            }
            set {
                this.idCargoField = value;
            }
        }
        
        /// <remarks/>
        public bool PermiteAlterarComunicado {
            get {
                return this.permiteAlterarComunicadoField;
            }
            set {
                this.permiteAlterarComunicadoField = value;
            }
        }
        
        /// <remarks/>
        public bool PermiteFinalizarComunicado {
            get {
                return this.permiteFinalizarComunicadoField;
            }
            set {
                this.permiteFinalizarComunicadoField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessaDashBoardChamados {
            get {
                return this.acessaDashBoardChamadosField;
            }
            set {
                this.acessaDashBoardChamadosField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessaAvaliacaoDesempenho {
            get {
                return this.acessaAvaliacaoDesempenhoField;
            }
            set {
                this.acessaAvaliacaoDesempenhoField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessoDeRH {
            get {
                return this.acessoDeRHField;
            }
            set {
                this.acessoDeRHField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessoDeGestor {
            get {
                return this.acessoDeGestorField;
            }
            set {
                this.acessoDeGestorField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessoDeColaborador {
            get {
                return this.acessoDeColaboradorField;
            }
            set {
                this.acessoDeColaboradorField = value;
            }
        }
        
        /// <remarks/>
        public bool AcessoDeControladoria {
            get {
                return this.acessoDeControladoriaField;
            }
            set {
                this.acessoDeControladoriaField = value;
            }
        }
        
        /// <remarks/>
        public bool NaoNotificaCorridas {
            get {
                return this.naoNotificaCorridasField;
            }
            set {
                this.naoNotificaCorridasField = value;
            }
        }
        
        /// <remarks/>
        public bool AniversariantesApenasDaEmpresa {
            get {
                return this.aniversariantesApenasDaEmpresaField;
            }
            set {
                this.aniversariantesApenasDaEmpresaField = value;
            }
        }
        
        /// <remarks/>
        public bool VisualizaRadarCompleto {
            get {
                return this.visualizaRadarCompletoField;
            }
            set {
                this.visualizaRadarCompletoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://niff.com.br/suportte/")]
    public enum TipoUsuario {
        
        /// <remarks/>
        Socilitante,
        
        /// <remarks/>
        Atendente,
        
        /// <remarks/>
        BI,
        
        /// <remarks/>
        Todos,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://niff.com.br/suportte/")]
    public enum TipoUsuarioSAC {
        
        /// <remarks/>
        Administrador,
        
        /// <remarks/>
        Atendente,
        
        /// <remarks/>
        UsuarioComum,
        
        /// <remarks/>
        Finalizador,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void ValidarUsuarioCompletedEventHandler(object sender, ValidarUsuarioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ValidarUsuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ValidarUsuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public object[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((object[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void AlterarStatusUsuarioCompletedEventHandler(object sender, AlterarStatusUsuarioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AlterarStatusUsuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AlterarStatusUsuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591