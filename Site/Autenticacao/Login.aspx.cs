using Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Site.wsLogin;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Serialization.Advanced;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace Suportte
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
              usuarioTextBox.Focus();
        }


        protected void entrarButton_Click1(object sender, EventArgs e)
        {
            Publicas.stringConexao = ConfigurationManager.ConnectionStrings["OraConnStr"].ConnectionString;

            Publicas.ValidacaoUsuario retorno;
            Object retornoValidaUsuario;

            if (string.IsNullOrEmpty(usuarioTextBox.Text))
            {
                this.ExibirMensagens(Publicas.TipoMensagem.Alerta, "Informe o usuário.");
                usuarioTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(senhaTextBox.Text))
            {
                this.ExibirMensagens(Publicas.TipoMensagem.Alerta, "Informe a senha.");
                senhaTextBox.Focus();
                return;
            }

            

            retornoValidaUsuario = (Object[])new LoginWS().ValidarUsuario(usuarioTextBox.Text, senhaTextBox.Text);

            retorno = (Publicas.ValidacaoUsuario)((object[])retornoValidaUsuario)[0];
            //Site.wsLogin.Usuario _usu = ((Site.wsLogin.Usuario)((object[])retornoValidaUsuario)[1]);

            Classes.Usuario _usu = ((Classes.Usuario)((object[])retornoValidaUsuario)[1]);

            //Publicas._usuario = Convert.ChangeType(_usu, typeof(Classes.Usuario),);
            Publicas._idUsuario = (int)((object[])retornoValidaUsuario)[2];

            //retorno = new LoginBO().ValidarUsuario(usuarioTextBox.Text, senhaTextBox.Text, Publicas._conexaoString);

            if (retorno == Publicas.ValidacaoUsuario.UsuarioNaoCadastrado)
            {
                this.ExibirMensagens(Publicas.TipoMensagem.Alerta, "Usuário não cadastrado.");
                senhaTextBox.Text = string.Empty;
                usuarioTextBox.Focus();
                return;
            }

            if (retorno == Publicas.ValidacaoUsuario.UsuarioInativo)
            {
                this.ExibirMensagens(Publicas.TipoMensagem.Alerta, "Usuario não está ativo.");
                senhaTextBox.Text = string.Empty;
                senhaTextBox.Focus();
                return;
            }

            if (retorno == Publicas.ValidacaoUsuario.SenhaInvalida)
            {
                this.ExibirMensagens(Publicas.TipoMensagem.Alerta, "Senha inválida.");
                senhaTextBox.Text = string.Empty;
                senhaTextBox.Focus();
                return;
            }

            if (!new LoginWS().AlterarStatusUsuario(Publicas._idUsuario, Publicas.StatusUsuario.OnLine))
            {
                this.ExibirMensagens(Publicas.TipoMensagem.Alerta, "Problemas ao atualizar status do usuário. " + Publicas.mensagemDeErro);
                senhaTextBox.Text = string.Empty;
                senhaTextBox.Focus();
                return;
            }

            Response.Redirect("../Principal.aspx");
        }
    }
}