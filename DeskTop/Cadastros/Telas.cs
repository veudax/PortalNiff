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
    public partial class Telas : Form
    {
        public Telas()
        {
            InitializeComponent();
            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    this.BackColor = Publicas._fundo;
                    
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Modulo _modulo;
        Categoria _categoria;
        Tela _tela;

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

        private void Telas_Shown(object sender, EventArgs e)
        {
            tipoTelaComboBox.Items.AddRange(new object[] { "Cadastro", "Movimentação", "Integração", "Relatório", "Geração de Arquivos" });
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region KeyDown        
        private void categoriaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                moduloTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void moduloTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                categoriaTextBox.Focus();
            }
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoTelaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                moduloTextBox.Focus();
            }
        }

        private void tipoTelaComboBox_KeyDown(object sender, KeyEventArgs e)
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
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoTelaComboBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                caminhoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void caminhoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                caminhoTextBox.Focus();
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

        #endregion

        #region Enter
        private void categoriaTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void tipoTelaComboBox_Enter(object sender, EventArgs e)
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

        #endregion

        #region Validating
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

        private void categoriaTextBox_Validating(object sender, CancelEventArgs e)
        {
            categoriaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (categoriaTextBox.Text.Trim() == "")
            {               
                new Pesquisas.Categorias().ShowDialog();

                categoriaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (categoriaTextBox.Text.Trim() == "" || categoriaTextBox.Text == "0")
                {
                    categoriaTextBox.Text = string.Empty;
                    categoriaTextBox.Focus();
                    return;
                }
            }

            _categoria = new CategoriaBO().Consultar(Convert.ToInt32(categoriaTextBox.Text));

            if (!_categoria.Existe)
            {
                new Notificacoes.Mensagem("Categoria não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                categoriaTextBox.Focus();
                return;
            }

            if (!_categoria.Ativo)
            {
                new Notificacoes.Mensagem("Categoria inativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                categoriaTextBox.Focus();
                return;
            }

            descricaoCategoriaTextBox.Text = _categoria.Descricao;

            if (Publicas._idRetornoPesquisa != 0)
                moduloTextBox.Focus();
        }

        private void moduloTextBox_Validating(object sender, CancelEventArgs e)
        {
            moduloTextBox.BorderColor = Publicas._bordaSaida;


            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (moduloTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _categoria.IdCategoria;

                new Pesquisas.Modulos().ShowDialog();

                moduloTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (moduloTextBox.Text.Trim() == "" || moduloTextBox.Text == "0")
                {
                    moduloTextBox.Text = string.Empty;
                    moduloTextBox.Focus();
                    return;
                }
            }

            _modulo = new ModuloBO().Consultar(Convert.ToInt32(moduloTextBox.Text));

            if (!_modulo.Existe)
            {
                new Notificacoes.Mensagem("Módulo não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                moduloTextBox.Focus();
                return;
            }

            if (!_modulo.Ativo)
            {
                new Notificacoes.Mensagem("Módulo inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                moduloTextBox.Focus();
                return;
            }
            descricaoModuloTextBox.Text = _modulo.Nome;

            if (Publicas._idRetornoPesquisa != 0)
                codigoTextBox.Focus();
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (codigoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _modulo.IdModulo;

                new Pesquisas.Telas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _tela = new TelaBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (_tela != null)
            {
                nomeTextBox.Text = _tela.Nome;
                ativoCheckBox.Checked = _tela.Ativo;
                caminhoTextBox.Text = _tela.Caminho;
                tipoTelaComboBox.SelectedIndex = (_tela.Tipo == Publicas.TipoDeTela.Cadastro ? 0 :
                                                 (_tela.Tipo == Publicas.TipoDeTela.Movimentacao ? 1 :
                                                 (_tela.Tipo == Publicas.TipoDeTela.Integracao ? 2 :
                                                 (_tela.Tipo == Publicas.TipoDeTela.Relatorio ? 3 : 4))));
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _modulo.Existe;

            if (Publicas._idRetornoPesquisa != 0)
                tipoTelaComboBox.Focus();
        }

        private void tipoTelaComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        #endregion

        #region Click

        private void categoriaLabel_Click(object sender, EventArgs e)
        {
            new Cadastros.Categorias().ShowDialog();
        }

        private void moduloLabel_Click(object sender, EventArgs e)
        {
            new Cadastros.Modulos().ShowDialog();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o nome!", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            if (_tela == null)
                _tela = new Tela();

            _tela.Ativo = ativoCheckBox.Checked;
            _tela.IdModulo = _modulo.IdModulo;
            _tela.IdTela = Convert.ToInt32(codigoTextBox.Text);
            _tela.Nome = nomeTextBox.Text;
            _tela.Caminho = caminhoTextBox.Text;
            _tela.Tipo = (tipoTelaComboBox.SelectedIndex == 0 ? Publicas.TipoDeTela.Cadastro :
                         (tipoTelaComboBox.SelectedIndex == 1 ? Publicas.TipoDeTela.Movimentacao :
                         (tipoTelaComboBox.SelectedIndex == 2 ? Publicas.TipoDeTela.Integracao :
                         (tipoTelaComboBox.SelectedIndex == 3 ? Publicas.TipoDeTela.Relatorio : Publicas.TipoDeTela.GeracaoDeArquivo))));

            if (!new TelaBO().Gravar(_tela))
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
            caminhoTextBox.Text = string.Empty;
            tipoTelaComboBox.SelectedIndex = -1;
            ativoCheckBox.Checked = false;

            codigoTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new TelaBO().Excluir(_tela))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new TelaBO().Proximo().ToString();
            tipoTelaComboBox.Focus();
        }
        #endregion

        private void categoriaLabel_MouseHover(object sender, EventArgs e)
        {
            categoriaLabel.Cursor = Cursors.Hand;
        }

        private void categoriaLabel_MouseLeave(object sender, EventArgs e)
        {
            categoriaLabel.Cursor = Cursors.Default;
        }

        private void moduloLabel_MouseHover(object sender, EventArgs e)
        {
            moduloLabel.Cursor = Cursors.Hand;
        }

        private void moduloLabel_MouseLeave(object sender, EventArgs e)
        {
            moduloLabel.Cursor = Cursors.Default;
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

        private void categoriaTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(categoriaTextBox.Text.Trim());
        }

        private void moduloTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaModuloButton.Enabled = string.IsNullOrEmpty(moduloTextBox.Text.Trim());
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaTelaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void pesquisaCategoriaButton_Click(object sender, EventArgs e)
        {
            if (categoriaTextBox.Text.Trim() == "")
            {
                new Pesquisas.Categorias().ShowDialog();

                categoriaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (categoriaTextBox.Text.Trim() == "" || categoriaTextBox.Text == "0")
                {
                    categoriaTextBox.Text = string.Empty;
                    categoriaTextBox.Focus();
                    return;
                }

                categoriaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaModuloButton_Click(object sender, EventArgs e)
        {
            Publicas._idRetornoPesquisa = 0;

            if (moduloTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _categoria.IdCategoria;

                new Pesquisas.Modulos().ShowDialog();

                moduloTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (moduloTextBox.Text.Trim() == "" || moduloTextBox.Text == "0")
                {
                    moduloTextBox.Text = string.Empty;
                    moduloTextBox.Focus();
                    return;
                }

                moduloTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void moduloTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void categoriaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pesquisaTelaButton_Click(object sender, EventArgs e)
        {
            Publicas._idRetornoPesquisa = 0;

            if (codigoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _modulo.IdModulo;

                new Pesquisas.Telas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text == "0")
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
