using Classes;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte
{
    public partial class TelaLogin : Form
    {
        public TelaLogin()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }


        private void usuarioLoginTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                senhaLoginTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void senhaLoginTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                acessarLoginButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                usuarioLoginTextBox.Focus();
            }
        }

        private void usuarioLoginTextBox_Enter(object sender, EventArgs e)
        {
            usuarioLoginTextBox.BorderColor = Publicas._bordaEntrada;
        }

        private void senhaLoginTextBox_Enter(object sender, EventArgs e)
        {
            senhaLoginTextBox.BorderColor = Publicas._bordaEntrada;
        }

        private void usuarioLoginTextBox_Validating(object sender, CancelEventArgs e)
        {
            usuarioLoginTextBox.BorderColor = Publicas._bordaSaida;
        }

        private void senhaLoginTextBox_Validating(object sender, CancelEventArgs e)
        {
            senhaLoginTextBox.BorderColor = Publicas._bordaSaida;
        }

        private void esqueceuSenhaLabel_MouseHover(object sender, EventArgs e)
        {
            esqueceuSenhaLabel.ForeColor = Color.DarkGreen;
        }

        private void esqueceuSenhaLabel_MouseLeave(object sender, EventArgs e)
        {
            esqueceuSenhaLabel.ForeColor = label27.ForeColor;
        }

        private void esqueceuSenhaLabel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuarioLoginTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe seu usuário.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioLoginTextBox.Focus();
                return;
            }

            Publicas._usuario = new UsuarioBO().Consultar(usuarioLoginTextBox.Text);

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Esqueceu a senha.";
            _log.Tela = "Principal - Login";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            EsqueceuASenha _tela = new EsqueceuASenha();
            _tela.Location = new Point(this.Left, this.Top + this.Height);
            _tela.TopMost = true;
            _tela.Show();            
        }

        private void acessarLoginButton_Click(object sender, EventArgs e)
        {
            Publicas.ValidacaoUsuario retorno;

            acessarLoginButton.BackColor = Publicas.padraoNIFFClaro_Botao;
            acessarLoginButton.ForeColor = Publicas.padraoNIFFClaro_FonteBotao;

            #region Validação do usuário 
            if (string.IsNullOrEmpty(usuarioLoginTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o usuário.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioLoginTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(senhaLoginTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a senha.", Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaLoginTextBox.Focus();
                return;
            }

            retorno = new LoginBO().ValidarUsuario(usuarioLoginTextBox.Text, senhaLoginTextBox.Text, Publicas._conexaoString);

            if (retorno == Publicas.ValidacaoUsuario.ProblemaAoConectar)
            {
                new Notificacoes.Mensagem(Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                senhaLoginTextBox.Text = string.Empty;
                usuarioLoginTextBox.Focus();
                return;
            }
            if (retorno == Publicas.ValidacaoUsuario.ErroConsulta)
            {
                new Notificacoes.Mensagem("Problemas durante a consulta." +
                        Environment.NewLine +
                        Publicas.mensagemDeErro, Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaLoginTextBox.Text = string.Empty;
                usuarioLoginTextBox.Focus();
                return;
            }
            if (retorno == Publicas.ValidacaoUsuario.UsuarioNaoCadastrado)
            {
                new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaLoginTextBox.Text = string.Empty;
                usuarioLoginTextBox.Focus();
                return;
            }

            if (retorno == Publicas.ValidacaoUsuario.UsuarioInativo)
            {
                new Notificacoes.Mensagem("Usuário inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaLoginTextBox.Text = string.Empty;
                senhaLoginTextBox.Focus();
                return;
            }

            if (retorno == Publicas.ValidacaoUsuario.SenhaInvalida)
            {
                new Notificacoes.Mensagem("Senha inválida.", Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaLoginTextBox.Text = string.Empty;
                senhaLoginTextBox.Focus();
                return;
            }

            Publicas._senhaLogin = senhaLoginTextBox.Text;
            Close();
            #endregion
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Publicas._encerrarSemLogar = true;
            Close();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.Cursor = Cursors.Hand;
            label1.BackColor = Color.LightGray;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Cursor = Cursors.Default;
            label1.BackColor = Color.WhiteSmoke;
        }
    }
}
