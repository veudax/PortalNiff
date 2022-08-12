using Classes;
using Negocio;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.BI
{
    public partial class EmailDeAcesso : Form
    {
        public EmailDeAcesso()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            DataDateTimePicker.BorderColor = Publicas._bordaSaida;
            DataDateTimePicker.BackColor = EmailTextBox.BackColor;
            DataDateTimePicker.Value = DateTime.Now;

            Publicas._mensagemSistema = string.Empty;
        }

        Classes.PowerBI.EmailDeAcesso _emails;
        
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

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                NomeTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EmailTextBox.Focus();
            }
        }

        private void NomeTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                DataDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void DataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                NomeTextBoxExt.Focus();
            }
        }

        private void GrupoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SenhaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DataDateTimePicker.Focus();
            }
        }

        private void SenhaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoTextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SenhaTextBox.Focus();
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

        private void excluirButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                limparButton.Focus();
            }
        }

        private void EmailTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void DataDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
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

        private void excluirButton_Enter(object sender, EventArgs e)
        {
            excluirButton.BackColor = Publicas._botaoFocado;
            excluirButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void EmailTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (EmailTextBox.Text.Trim() == "")
            {
                new Pesquisas.EmailBI().ShowDialog();

                EmailTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (EmailTextBox.Text.Trim() == "" || EmailTextBox.Text.Trim() == "0")
                {
                    EmailTextBox.Text = string.Empty;
                    EmailTextBox.Focus();
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!reg.IsMatch(EmailTextBox.Text))
                {
                    new Notificacoes.Mensagem("E-mail inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    EmailTextBox.Focus();
                    return;
                }
            }

            _emails = new PowerBIBO().Consultar(EmailTextBox.Text);

            if (_emails.Existe)
            {
                ativoCheckBox.Checked = _emails.Ativo;
                NomeTextBoxExt.Text = _emails.Nome;

                GrupoTextBox.Text = _emails.Grupo;

                DataDateTimePicker.Value = _emails.Data;

                SenhaTextBox.Text = _emails.Senha;
            }

            excluirButton.Enabled = _emails.Existe;
            gravarButton.Enabled = true;

            if (Publicas._idRetornoPesquisa != 0)
                ativoCheckBox.Focus();
        }

        private void NomeTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void DataDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(EmailTextBox.Text.Trim());
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_emails == null)
                _emails = new PowerBI.EmailDeAcesso();

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (_emails.Existe ? "Alterou" : "Incluiu") + " o e-nail de acesso ao PowerBI " + EmailTextBox.Text +
                (_emails.Nome == NomeTextBoxExt.Text ? "" : " [Nome] de " + _emails.Nome + " para " + NomeTextBoxExt.Text) +
                (_emails.Ativo == ativoCheckBox.Checked ? "" : " [Ativo] de " + _emails.Ativo + " para " + ativoCheckBox.Checked) +
                (_emails.Grupo == GrupoTextBox.Text ? "" : " [Grupo] de " + _emails.Grupo + " para " + GrupoTextBox.Text) +
                (_emails.Senha == SenhaTextBox.Text ? "" : " [Senha] de " + _emails.Senha + " para " + SenhaTextBox.Text) +
                (_emails.Data == DataDateTimePicker.Value ? "" : " [Data] de " + _emails.Data.ToShortDateString() + " para " + DataDateTimePicker.Value.ToShortDateString());

            _emails.Email = EmailTextBox.Text;
            _emails.Nome = NomeTextBoxExt.Text;
            _emails.Grupo = GrupoTextBox.Text;
            _emails.Ativo = ativoCheckBox.Checked;
            _emails.Senha = SenhaTextBox.Text;
            _emails.Data = DataDateTimePicker.Value;
            
            if (!new PowerBIBO().Gravar(_emails))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            _log.Tela = "TI - PowerBI - E-mail";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            EmailTextBox.Text = string.Empty;
            NomeTextBoxExt.Text = string.Empty;
            GrupoTextBox.Text = string.Empty;
            SenhaTextBox.Text = string.Empty;
            DataDateTimePicker.Value = DateTime.Now;

            EmailTextBox.Focus();
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new PowerBIBO().Excluir(_emails.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu o e-nail de acesso ao PowerBI " + EmailTextBox.Text;
            _log.Tela = "TI - PowerBI - E-mail";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (EmailTextBox.Text.Trim() == "")
            {
                new Pesquisas.EmailBI().ShowDialog();

                EmailTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (EmailTextBox.Text.Trim() == "" || EmailTextBox.Text.Trim() == "0")
                {
                    EmailTextBox.Text = string.Empty;
                    EmailTextBox.Focus();
                    return;
                }

                EmailTextBox_Validating(sender, new CancelEventArgs());
            }
        }
    }
}
