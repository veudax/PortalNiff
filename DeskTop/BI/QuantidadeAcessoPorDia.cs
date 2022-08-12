using Classes;
using Negocio;
using Syncfusion.Windows.Forms;
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
    public partial class QuantidadeAcessoPorDia : Form
    {
        public QuantidadeAcessoPorDia()
        {
            InitializeComponent();
            
            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                inicialDateTimePicker.Style = VisualStyle.Office2016Black;
            }
            QuantidadeCurrencyTextBox.BackGroundColor = EmailTextBox.BackColor;
            inicialDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._TemaBlack)
                QuantidadeCurrencyTextBox.PositiveColor = Publicas._fonte;

            Publicas._mensagemSistema = string.Empty;
        }

        Classes.PowerBI.EmailDeAcesso _emails;
        Classes.PowerBI.Relatorios _relatorio;
        Classes.PowerBI.Acessos _acessos;

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

        private void EmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inicialDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EmailTextBox.Focus();
            }
        }

        private void inicialDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                QuantidadeCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void QuantidadeCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                inicialDateTimePicker.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
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

        private void inicialDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void QuantidadeCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
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

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
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

            if (!_emails.Existe)
            {
                new Notificacoes.Mensagem("E-mail não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                EmailTextBox.Focus();
                return;
            }

            if (!_emails.Ativo)
            {
                new Notificacoes.Mensagem("E-mail não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                EmailTextBox.Focus();
                return;
            }


        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.RelatoriosBI().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _relatorio = new PowerBIBO().ConsultarRelatorios(Convert.ToInt32(codigoTextBox.Text));

            if (!_relatorio.Existe)
            {
                new Notificacoes.Mensagem("Relatório não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            if (!_relatorio.Ativo)
            {
                new Notificacoes.Mensagem("Relatório não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _relatorio.Nome;
        }

        private void inicialDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _acessos = new PowerBIBO().ConsultarAcesso(_emails.Id, _relatorio.Id, inicialDateTimePicker.Value.Date);

            gravarButton.Enabled = true;
            excluirButton.Enabled = _acessos.Existe;

            QuantidadeCurrencyTextBox.DecimalValue = _acessos.Quantidade;
        }

        private void QuantidadeCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(EmailTextBox.Text.Trim());
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaRelButtonAdv.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
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

        private void PesquisaRelButtonAdv_Click(object sender, EventArgs e)
        {
            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.RelatoriosBI().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (_acessos.Existe ? "Alterou" : "Incluiu") + " a quantidade de acessos para o e-mail " + EmailTextBox.Text + 
                " relatório " + nomeTextBox.Text + " na data " + inicialDateTimePicker.Value.ToShortDateString() + 
                (_acessos.Quantidade == QuantidadeCurrencyTextBox.DecimalValue ? "" : " [Quantidade] de " + _acessos.Quantidade + " para " + QuantidadeCurrencyTextBox.DecimalValue);

            _acessos.IdEmail = _emails.Id;
            _acessos.IdRelatorios = _relatorio.Id;
            _acessos.Data = inicialDateTimePicker.Value;
            _acessos.Quantidade = Convert.ToInt32(QuantidadeCurrencyTextBox.DecimalValue);

            if (!new PowerBIBO().Gravar(_acessos))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            _log.Tela = "TI - Power BI - Acessos";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            inicialDateTimePicker.Value = inicialDateTimePicker.Value.AddDays(1);
            QuantidadeCurrencyTextBox.DecimalValue = 0;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            inicialDateTimePicker.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new PowerBIBO().ExcluirAcessos(_acessos.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu a quantidade de acessos para o e-mail " + EmailTextBox.Text +
                " relatório " + nomeTextBox.Text + " na data " + inicialDateTimePicker.Value.ToShortDateString();

            _log.Tela = "TI - Power BI - Acessos";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }
    }
}
