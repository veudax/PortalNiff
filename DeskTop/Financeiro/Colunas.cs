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

namespace Suportte.Financeiro
{
    public partial class Colunas : Form
    {
        public Colunas()
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

        Classes.Financeiro.Colunas _colunas;

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

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Colunas_KeyDown(sender, e);
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
            Colunas_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Colunas_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                TipoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void TipoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Colunas_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                TransferenciaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void TransferenciaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Colunas_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OrigemComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                TipoComboBox.Focus();
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

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '+')
            {
                codigoTextBox.Text = string.Empty;
                proximoButton_Click(sender, e);
            }
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void TipoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
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
                new Pesquisas.Colunas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _colunas = new FinanceiroBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (_colunas.Existe)
            {
                ativoCheckBox.Checked = _colunas.Ativo;
                nomeTextBox.Text = _colunas.Nome;

                TipoComboBox.SelectedIndex = (_colunas.Tipo == "EN" ? 0 :
                    (_colunas.Tipo == "SA" ? 1 :
                    (_colunas.Tipo == "TE" ? 2 : 3)));

                TransferenciaComboBox.SelectedIndex = (_colunas.Transferencia == " " ? 0 : (_colunas.Transferencia == "B" ? 1 : 2));

                OrigemComboBox.SelectedIndex = (_colunas.Origem == "ARR" ? 0 : 
                                               (_colunas.Origem == "BCO" ? 1 : 
                                               (_colunas.Origem == "CRC" ? 2 : 
                                               (_colunas.Origem == "CPG" ? 3 :
                                               (_colunas.Origem == "BCRC" ? 4 :
                                               (_colunas.Origem == "BCPG" ? 5 :
                                               -1))))));

                OperacaoComboBox.SelectedIndex = (_colunas.TipoOperacao == "M" ? 0 : (_colunas.TipoOperacao == "I" ? 1 : (_colunas.Origem == "R" ? 2 : 3)));
            }

            excluirButton.Enabled = _colunas.Existe;
            gravarButton.Enabled = true;

            if (Publicas._idRetornoPesquisa != 0)
                nomeTextBox.Focus();
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void TipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

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

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Colunas().ShowDialog();

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

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new FinanceiroBO().Proximo().ToString();
            ativoCheckBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (_colunas.Existe ? "Alterou" : "Incluiu") + " a coluna " + codigoTextBox.Text +
                (_colunas.Nome == nomeTextBox.Text ? "" : " [Nome] de " + _colunas.Nome + " para " + nomeTextBox.Text) +
                (_colunas.Ativo == ativoCheckBox.Checked ? "" : " [Ativo] de " + _colunas.Ativo + " para " + ativoCheckBox.Checked) +
                (_colunas.Tipo == (TipoComboBox.SelectedIndex == 0 ? "EN" :
                                  (TipoComboBox.SelectedIndex == 1 ? "SA" :
                                  (TipoComboBox.SelectedIndex == 2 ? "TE" : "TS"))) ? "" : " [Tipo] de " + _colunas.Tipo + " para " + (TipoComboBox.SelectedIndex == 0 ? "EN" :
                                                                                                                                      (TipoComboBox.SelectedIndex == 1 ? "SA" :
                                                                                                                                      (TipoComboBox.SelectedIndex == 2 ? "TE" : "TS")))) +
                (_colunas.Transferencia == (TransferenciaComboBox.SelectedIndex == 0 ? " " :
                                           (TransferenciaComboBox.SelectedIndex == 1 ? "B" : "E")) ? "" : " [Transferencia] de " + _colunas.Ativo + " para " + (TransferenciaComboBox.SelectedIndex == 0 ? " " :
                                                                                                                                                               (TransferenciaComboBox.SelectedIndex == 1 ? "B" : "E"))) +

                                                                                                                                                                              
                (_colunas.Origem == (OrigemComboBox.SelectedIndex == 0 ? "ARR" :
                                    (OrigemComboBox.SelectedIndex == 1 ? "BCO" :
                                    (OrigemComboBox.SelectedIndex == 2 ? "CRC" : 
                                    (OrigemComboBox.SelectedIndex == 3 ? "CPG" :
                                    (OrigemComboBox.SelectedIndex == 4 ? "BCRC" : "BCPG"))))) ? "" : " [Origem] de " + _colunas.Origem + " para " + (OrigemComboBox.SelectedIndex == 0 ? "ARR" :
                                                                                                                                                    (OrigemComboBox.SelectedIndex == 1 ? "BCO" :
                                                                                                                                                    (OrigemComboBox.SelectedIndex == 2 ? "CRC" :
                                                                                                                                                    (OrigemComboBox.SelectedIndex == 3 ? "CPG" :
                                                                                                                                                    (OrigemComboBox.SelectedIndex == 4 ? "BCRC" : "BCPG")))))) +
                (_colunas.TipoOperacao == (OperacaoComboBox.SelectedIndex == 0 ? "M" :
                                          (OperacaoComboBox.SelectedIndex == 1 ? "I" :
                                          (OperacaoComboBox.SelectedIndex == 2 ? "R" : "T"))) ? "" : " [Operacao] de " + _colunas.TipoOperacao + " para " + (OperacaoComboBox.SelectedIndex == 0 ? "M" :
                                                                                                                                                      (OperacaoComboBox.SelectedIndex == 1 ? "I" :
                                                                                                                                                      (OperacaoComboBox.SelectedIndex == 2 ? "R" : "T"))));
                                                                                                                                                      
            _colunas.Id = Convert.ToInt32(codigoTextBox.Text);
            _colunas.Ativo = ativoCheckBox.Checked;
            _colunas.Nome = nomeTextBox.Text;

            _colunas.Tipo = (TipoComboBox.SelectedIndex == 0 ? "EN" :
                (TipoComboBox.SelectedIndex == 1 ? "SA" :
                (TipoComboBox.SelectedIndex == 2 ? "TE" : "TS")));

            _colunas.Transferencia = (TransferenciaComboBox.SelectedIndex == 0 ? " " :
                (TransferenciaComboBox.SelectedIndex == 1 ? "B" : "E"));

            _colunas.TipoOperacao = (OperacaoComboBox.SelectedIndex == 0 ? "M" :
                                    (OperacaoComboBox.SelectedIndex == 1 ? "I" :
                                    (OperacaoComboBox.SelectedIndex == 2 ? "R" : "T")));

            _colunas.Origem = (OrigemComboBox.SelectedIndex == 0 ? "ARR" :
                              (OrigemComboBox.SelectedIndex == 1 ? "BCO" :
                              (OrigemComboBox.SelectedIndex == 2 ? "CRC" :
                              (OrigemComboBox.SelectedIndex == 3 ? "CPG" :
                              (OrigemComboBox.SelectedIndex == 4 ? "BCRC" : "BCPG")))));


            if (!new FinanceiroBO().Gravar(_colunas))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }
            
            _log.Tela = "Financeiro - Cadastros - Colunas";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {

            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            ativoCheckBox.Checked = false;
            TransferenciaComboBox.SelectedIndex = 0;
            TipoComboBox.SelectedIndex = 0;
            OperacaoComboBox.SelectedIndex = -1;
            OrigemComboBox.SelectedIndex = -1;
            TipoComboBox.Text = "";
            TransferenciaComboBox.Text = "";
            
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            codigoTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new FinanceiroBO().Excluir(_colunas.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu a coluna " + codigoTextBox.Text + " " + nomeTextBox.Text;
            _log.Tela = "Financeiro - Cadastros - Colunas";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void TipoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransferenciaComboBox.SelectedIndex = 0;
        }

        private void Colunas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Publicas.AbrirFerramentaDeCapitura();
        }

        private void OrigemComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Colunas_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OperacaoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                TransferenciaComboBox.Focus();
            }
        }

        private void OperacaoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Colunas_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OrigemComboBox.Focus();
            }
        }
    }
}
