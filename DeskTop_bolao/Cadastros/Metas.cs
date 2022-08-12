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
    public partial class Metas : Form
    {
        public Metas()
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

        Classes.Metas _metas;
        Publicas.RegraFormulaMetas _regraFormula;
        
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

        private void Metas_Shown(object sender, EventArgs e)
        {
            tipoComboBox.Items.AddRange(new object[] { "Resultados", "Crescimento" });
            perspectivaComboBox.Items.AddRange(new object[] { "Cliente", "Processos", "Financeiro", "Aprendizagem e crescimento" });

            tipoComboBox.SelectedIndex = 0;
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                perspectivaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void tipoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                perspectivaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void perspectivaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                TextoBITextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                perspectivaComboBox.Focus();
            }
        }

        private void cargoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
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

        private void cargoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }            
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new MetasBO().Proximo().ToString();
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

            if (_metas == null)
                _metas = new Classes.Metas();

            _metas.Id = Convert.ToInt32(codigoTextBox.Text);
            _metas.Descricao = nomeTextBox.Text;
            _metas.Ativo = ativoCheckBox.Checked;
            _metas.TextoBI = TextoBITextBox.Text;

            _metas.Perspectiva = (perspectivaComboBox.SelectedIndex == 0 ? Publicas.Perspectivas.Cliente :
                (perspectivaComboBox.SelectedIndex == 1 ? Publicas.Perspectivas.Processos :
                (perspectivaComboBox.SelectedIndex == 2 ? Publicas.Perspectivas.Financeira : Publicas.Perspectivas.Aprendizagem)));
            _metas.Tipo = Publicas.TipoDeMetas.Resultado;
            _metas.Regra = _regraFormula;

            if (formula1RadioButton.Checked)
                _metas.Formula = formula1RadioButton.Text;
            else
            {
                if (formula2RadioButton.Checked)
                    _metas.Formula = formula2RadioButton.Text;
                else
                    _metas.Formula = formula3RadioButton.Text;
            }

            if (!new MetasBO().Gravar(_metas))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            ativoCheckBox.Checked = false;
            perspectivaComboBox.SelectedIndex = -1;
            perspectivaComboBox.Text = string.Empty;
            TextoBITextBox.Text = string.Empty;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            codigoTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new MetasBO().Excluir(_metas))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Metas().ShowDialog();

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

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void tipoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
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
                new Pesquisas.Metas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _metas = new MetasBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (_metas.Existe)
            {
                ativoCheckBox.Checked = _metas.Ativo;
                nomeTextBox.Text = _metas.Descricao;
                TextoBITextBox.Text = _metas.TextoBI;

                perspectivaComboBox.SelectedIndex = (_metas.Perspectiva == Publicas.Perspectivas.Cliente ? 0 :
                    (_metas.Perspectiva == Publicas.Perspectivas.Processos ? 1 :
                    (_metas.Perspectiva == Publicas.Perspectivas.Financeira ? 2 : 3)));

                tipoComboBox.SelectedIndex = (_metas.Tipo == Publicas.TipoDeMetas.Crescimento ? 1 : 0);

                switch (_metas.Regra)
                {
                    case Publicas.RegraFormulaMetas.MaiorMelhor:
                        maiorMelhorButton_Click(sender, e);
                        break;
                    case Publicas.RegraFormulaMetas.MenorMelhor:
                        menorMelhorButton_Click(sender, e);
                        break;
                    case Publicas.RegraFormulaMetas.Igual:
                        igualMelhorButton_Click(sender, e);
                        break;
                }

                formula1RadioButton.Checked = formula1RadioButton.Text == _metas.Formula;
                formula2RadioButton.Checked = formula2RadioButton.Text == _metas.Formula;
                formula3RadioButton.Checked = formula3RadioButton.Text == _metas.Formula;

            }

            excluirButton.Enabled = _metas.Existe;
            gravarButton.Enabled = true;

            if (Publicas._idRetornoPesquisa != 0)
                ativoCheckBox.Focus();
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void tipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void TextoBITextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void maiorMelhorButton_Click(object sender, EventArgs e)
        {
            _regraFormula = Publicas.RegraFormulaMetas.MaiorMelhor;
            maiorMelhorButton.BackColor = codigoTextBox.BackColor;
            menorMelhorButton.BackColor = Publicas._botao;
            igualMelhorButton.BackColor = Publicas._botao;
        }

        private void menorMelhorButton_Click(object sender, EventArgs e)
        {
            _regraFormula = Publicas.RegraFormulaMetas.MenorMelhor;
            maiorMelhorButton.BackColor = Publicas._botao;
            menorMelhorButton.BackColor = codigoTextBox.BackColor; 
            igualMelhorButton.BackColor = Publicas._botao;
        }

        private void igualMelhorButton_Click(object sender, EventArgs e)
        {
            _regraFormula = Publicas.RegraFormulaMetas.Igual;
            maiorMelhorButton.BackColor = Publicas._botao;
            menorMelhorButton.BackColor = Publicas._botao;
            igualMelhorButton.BackColor = codigoTextBox.BackColor; 
        }
    }
}
