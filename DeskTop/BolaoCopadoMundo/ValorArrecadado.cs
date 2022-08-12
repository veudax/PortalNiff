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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.BolaoCopadoMundo
{
    public partial class ValorArrecadado : Form
    {
        public ValorArrecadado()
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

        Classes.BolaoValorArrecadado _valor;
        Classes.Colaboradores _colaborador;

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

        private void limparButton_Click(object sender, EventArgs e)
        {
            GrupoBJ1Placar2CurrencyTextBox.DecimalValue = 0;
            SiglaTextBox.Text = string.Empty;
            NomeTextBox.Text = string.Empty;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            SiglaTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new BolaoValorArrecadadoBO().Excluir(_valor.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_valor == null)
                _valor = new BolaoValorArrecadado();

            _valor.IdColaborador = _colaborador.Id;
            _valor.Valor = GrupoBJ1Placar2CurrencyTextBox.DecimalValue;
            
            if (!new BolaoValorArrecadadoBO().Gravar(_valor))
            {
                new Notificacoes.Mensagem("Problemas durante a gravar." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void PesquisaSiglaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SiglaTextBox.Text.Trim()))
            {
                new Pesquisas.Colaborador().ShowDialog();

                SiglaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(SiglaTextBox.Text) || SiglaTextBox.Text == "0")
                {
                    SiglaTextBox.Text = string.Empty;
                    SiglaTextBox.Focus();
                    return;
                }

                SiglaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void SiglaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SiglaTextBox.Focus();
            }
        }

        private void GrupoBJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SiglaTextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ1Placar2CurrencyTextBox.Focus();
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

        private void SiglaTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaSiglaButton.Enabled = string.IsNullOrEmpty(SiglaTextBox.Text.Trim());
        }

        private void GrupoBJ1Placar2CurrencyTextBox_Enter(object sender, EventArgs e)
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

        private void SiglaTextBox_Validating(object sender, CancelEventArgs e)
        {
            SiglaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                SiglaTextBox.Text = string.Empty;
                PesquisaSiglaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(SiglaTextBox.Text.Trim()))
            {
                new Pesquisas.Colaborador().ShowDialog();

                SiglaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(SiglaTextBox.Text) || SiglaTextBox.Text == "0")
                {
                    SiglaTextBox.Text = string.Empty;
                    SiglaTextBox.Focus();
                    return;
                }
            }

            _colaborador = new ColaboradoresBO().Consultar(Convert.ToInt32(SiglaTextBox.Text.Trim()));

            if (!_colaborador.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                SiglaTextBox.Focus();
                return;
            }

            NomeTextBox.Text = _colaborador.Nome;

            _valor = new BolaoValorArrecadadoBO().Consultar(DateTime.Now.Year, _colaborador.Id);
            GrupoBJ1Placar2CurrencyTextBox.DecimalValue = _valor.Valor;

            gravarButton.Enabled = true;
            excluirButton.Enabled = _valor.Existe;
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

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
