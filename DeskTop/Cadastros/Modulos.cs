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
    public partial class Modulos : Form
    {
        public Modulos()
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

        Modulo _modulo;
        Categoria _categoria;

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
            categoriaTextBox.Text = string.Empty;
            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            fornecedorTextBox.Text = string.Empty;
            ativoCheckBox.Checked = false;
            descricaoCategoriaTextBox.Text = string.Empty;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            categoriaTextBox.Focus();
        }

        private void categoriaTextBox_KeyDown(object sender, KeyEventArgs e)
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
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                categoriaTextBox.Focus();
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
                codigoTextBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                fornecedorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void fornecedorTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void categoriaTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new ModuloBO().Proximo().ToString();
            ativoCheckBox.Focus();
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

        private void categoriaTextBox_Validating(object sender, CancelEventArgs e)
        {

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
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
                codigoTextBox.Focus();
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
                Publicas._idRetornoPesquisa = _categoria.IdCategoria;

                new Pesquisas.Modulos().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _modulo = new ModuloBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (_modulo != null)
            {
                nomeTextBox.Text = _modulo.Nome;
                ativoCheckBox.Checked = _modulo.Ativo;
                fornecedorTextBox.Text = _modulo.Fornecedor;
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _modulo.Existe;
            
            if (Publicas._idRetornoPesquisa != 0)
                nomeTextBox.Focus();
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição!", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            _modulo.IdCategoria = Convert.ToInt32(categoriaTextBox.Text);
            _modulo.IdModulo = Convert.ToInt32(codigoTextBox.Text);
            _modulo.Nome = nomeTextBox.Text;
            _modulo.Fornecedor = fornecedorTextBox.Text;
            _modulo.Ativo = ativoCheckBox.Checked;

            if (!new ModuloBO().Gravar(_modulo))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new ModuloBO().Excluir(_modulo))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
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

        private void categoriaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                fornecedorTextBox.Focus();
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

        private void categoriaTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(categoriaTextBox.Text.Trim());            
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaModuloButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
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
            if (codigoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _categoria.IdCategoria;

                new Pesquisas.Modulos().ShowDialog();

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
