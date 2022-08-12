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
    public partial class Corridas : Form
    {
        public Corridas()
        {
            InitializeComponent();

            dataDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataDateTimePicker.BackColor = codigoTextBox.BackColor;
            prazoDateTimePicker.BorderColor = Publicas._bordaSaida;
            prazoDateTimePicker.BackColor = codigoTextBox.BackColor;
            valorCurrencyTextBox.BackGroundColor = codigoTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Corridas _corridas = new Classes.Corridas();
        List<DistanciaCorrida> _listas;

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

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void pontuacaoCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void dataNascimentoDateTimePicker_Enter(object sender, EventArgs e)
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

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                valorCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void valorCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                valorGrupoCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                prazoDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void prazoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataDateTimePicker.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                localTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                prazoDateTimePicker.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                valorGrupoCurrencyTextBox.Focus();
            }
        }

        private void localTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                linkTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void linkTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cincoKMCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                localTextBox.Focus();
            }
        }

        private void cincoKMCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dezKmCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                linkTextBox.Focus();
            }
        }

        private void dezKmCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                quinzeKmCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cincoKMCheckBox.Focus();
            }
        }

        private void quinzeKmCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                vinteUmKmCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dezKmCheckBox.Focus();
            }
        }

        private void vinteUmKmCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                quarentaDoisKmCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                quinzeKmCheckBox.Focus();
            }
        }

        private void quarentaDoisKmCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                vinteUmKmCheckBox.Focus();
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

        private void limparButton_Click(object sender, EventArgs e)
        {
            if (_listas != null)
                _listas.Clear();

            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            ativoCheckBox.Checked = false;
            localTextBox.Text = string.Empty;
            valorCurrencyTextBox.DecimalValue = 1;
            linkTextBox.Text = string.Empty;
            dataDateTimePicker.Value = DateTime.Now.Date;
            prazoDateTimePicker.Value = DateTime.Now.Date;
            cincoKMCheckBox.Checked = false;
            dezKmCheckBox.Checked = false;
            quinzeKmCheckBox.Checked = false;
            vinteUmKmCheckBox.Checked = false;
            quarentaDoisKmCheckBox.Checked = false;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            codigoTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new CorridasBO().Excluir(_corridas))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            _corridas.Id = Convert.ToInt32(codigoTextBox.Text);
            _corridas.Ativo = ativoCheckBox.Checked;
            _corridas.Nome = nomeTextBox.Text;
            _corridas.Local = localTextBox.Text;
            _corridas.LinkWeb = linkTextBox.Text;
            _corridas.Valor = valorCurrencyTextBox.DecimalValue;
            _corridas.Data = dataDateTimePicker.Value;
            _corridas.PrazoLimite = prazoDateTimePicker.Value;
            _corridas.ValorGrupo = valorGrupoCurrencyTextBox.DecimalValue;

            DistanciaCorrida _distancia;

            if (_corridas.Existe)
                _listas.Clear();
            else
                _listas = new List<DistanciaCorrida>();

            if (cincoKMCheckBox.Checked)
            {
                _distancia = new DistanciaCorrida();
                _distancia.IdCorrida = _corridas.Id;
                _distancia.Km = 5;
                _listas.Add(_distancia);
            }

            if (dezKmCheckBox.Checked)
            {
                _distancia = new DistanciaCorrida();
                _distancia.IdCorrida = _corridas.Id;
                _distancia.Km = 10;
                _listas.Add(_distancia);
            }

            if (quinzeKmCheckBox.Checked)
            {
                _distancia = new DistanciaCorrida();
                _distancia.IdCorrida = _corridas.Id;
                _distancia.Km = 15;
                _listas.Add(_distancia);
            }

            if (vinteUmKmCheckBox.Checked)
            {
                _distancia = new DistanciaCorrida();
                _distancia.IdCorrida = _corridas.Id;
                _distancia.Km = 21;
                _listas.Add(_distancia);
            }

            if (quarentaDoisKmCheckBox.Checked)
            {
                _distancia = new DistanciaCorrida();
                _distancia.IdCorrida = _corridas.Id;
                _distancia.Km = 42;
                _listas.Add(_distancia);
            }
            
            if (!new CorridasBO().Gravar(_corridas, _listas))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new CorridasBO().Proximo().ToString();
            valorCurrencyTextBox.Focus();
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
                new Pesquisas.Corridas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _corridas = new CorridasBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (_corridas.Existe)
            {
                _listas = new CorridasBO().ListarDistancias(_corridas.Id);
                ativoCheckBox.Checked = _corridas.Ativo;
                nomeTextBox.Text = _corridas.Nome;
                localTextBox.Text = _corridas.Local;
                linkTextBox.Text = _corridas.LinkWeb;
                valorCurrencyTextBox.DecimalValue = _corridas.Valor;
                dataDateTimePicker.Value = _corridas.Data;
                prazoDateTimePicker.Value = _corridas.PrazoLimite;
                valorGrupoCurrencyTextBox.DecimalValue = _corridas.ValorGrupo;

                foreach (var item in _listas.OrderBy(o => o.Km))
                {
                    cincoKMCheckBox.Checked = cincoKMCheckBox.Checked || item.Km == 5;
                    dezKmCheckBox.Checked = dezKmCheckBox.Checked || item.Km == 10;
                    quinzeKmCheckBox.Checked = quinzeKmCheckBox.Checked || item.Km == 15;
                    vinteUmKmCheckBox.Checked = vinteUmKmCheckBox.Checked || item.Km == 21;
                    quarentaDoisKmCheckBox.Checked = quarentaDoisKmCheckBox.Checked || item.Km == 42;
                }
            }

            excluirButton.Enabled = _corridas.Existe;
            gravarButton.Enabled = true;

            if (Publicas._idRetornoPesquisa != 0)
                valorCurrencyTextBox.Focus();
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

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void dataDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void valorCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void valorGrupoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                valorCurrencyTextBox.Focus();
            }
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Corridas().ShowDialog();

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
    }
}
