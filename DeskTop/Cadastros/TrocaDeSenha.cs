using Classes;
using Negocio;
using Suportte.Notificacoes;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.Cadastros
{
    public partial class TrocaDeSenha : Form
    {
        public TrocaDeSenha()
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

        #region Move tela
        bool clicouNoPanel;
        int posIniX;
        int posIniY;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            clicouNoPanel = true;
            posIniX = e.X;
            posIniY = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            clicouNoPanel = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicouNoPanel)
            {
                this.SetDesktopLocation(MousePosition.X - posIniX, MousePosition.Y - posIniY);
            }
        }
        #endregion

        private void atualTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void novaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                confirmacaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void confirmacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                novaTextBox.Focus();
            }
        }

        private void novaTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                novaTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(novaTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe a nova senha.", Publicas.TipoMensagem.Alerta).ShowDialog();
                novaTextBox.Focus();
                return;
            }

            if (novaTextBox.Text.Trim().Length < 6)
            {
                new Notificacoes.Mensagem("Senha deve ter no mínimo 6 caracteres.", Publicas.TipoMensagem.Alerta).ShowDialog();
                novaTextBox.Focus();
                return;
            }

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void confirmacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(confirmacaoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Confirme a senha.", Publicas.TipoMensagem.Alerta).ShowDialog();
                confirmacaoTextBox.Focus();
                return;
            }

            if (confirmacaoTextBox.Text.Trim() != novaTextBox.Text.Trim())
            {
                new Notificacoes.Mensagem("Senhas não conferem.", Publicas.TipoMensagem.Alerta).ShowDialog();
                confirmacaoTextBox.Focus();
                return;
            }

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            novaTextBox.Text = string.Empty;
            confirmacaoTextBox.Text = string.Empty;

            novaTextBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(confirmacaoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Confirme a senha.", Publicas.TipoMensagem.Alerta).ShowDialog();
                confirmacaoTextBox.Focus();
                return;
            }

            if (confirmacaoTextBox.Text.Trim() != novaTextBox.Text.Trim())
            {
                new Notificacoes.Mensagem("Senhas não conferem.", Publicas.TipoMensagem.Alerta).ShowDialog();
                confirmacaoTextBox.Focus();
                return;
            }

            Usuario _usuario = new Usuario();
            _usuario.Id = Publicas._idUsuario;
            _usuario.Senha = novaTextBox.Text;

            if (!new UsuarioBO().TrocarSenha(_usuario))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
            Close();
        }


        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void limparButton_Enter(object sender, EventArgs e)
        {
            limparButton.BackColor = Publicas._botaoFocado;
            limparButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                confirmacaoTextBox.Focus();
            }
        }

        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gravarButton.Focus();
            }
        }
    }
}
