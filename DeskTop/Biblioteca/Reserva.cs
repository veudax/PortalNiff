using Negocio;
using Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace Suportte.Biblioteca
{
    public partial class Reserva : Form
    {
        public Reserva()
        {
            InitializeComponent();

            dataDateTimePicker.BackColor = codigoTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                    dataDateTimePicker.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Black;
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.CategoriaLivros> _categorias;
        Classes.Livros _livros;
        Classes.Colaboradores _colaborador;
        Classes.EmprestimoLivros _emprestimo;
        Classes.ReservaLivros _reserva;

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
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ColaboradorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void ColaboradorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataDateTimePicker.Focus();
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
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ColaboradorTextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataDateTimePicker.Focus();
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

        private void ColaboradorTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaColaboradorButton.Enabled = string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim());
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void dataDateTimePicker_Enter(object sender, EventArgs e)
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

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                codigoTextBox.Text = string.Empty;
                pesquisaCategoriaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                new Pesquisas.Livros().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _livros = new LivrosBO().Consultar(Convert.ToInt32(codigoTextBox.Text.Trim()));

            if (!_livros.Existe)
            {
                new Notificacoes.Mensagem("Livro não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            if (!_livros.Ativo)
            {
                new Notificacoes.Mensagem("Livro não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            NomeTextBox.Text = _livros.Nome;

            foreach (var item in _categorias.Where(w => w.Id == _livros.IdCategoria))
            {
                for (int i = 0; i < CategoriaComboBox.Items.Count; i++)
                {
                    CategoriaComboBox.SelectedIndex = i;
                    if (CategoriaComboBox.Text == item.Descricao)
                        break;
                }
            }
        }

        private void ColaboradorTextBox_Validating(object sender, CancelEventArgs e)
        {
            ColaboradorTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                ColaboradorTextBox.Text = string.Empty;
                PesquisaColaboradorButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim()))
            {
                new Pesquisas.Colaborador().ShowDialog();

                ColaboradorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(ColaboradorTextBox.Text) || ColaboradorTextBox.Text == "0")
                {
                    ColaboradorTextBox.Text = string.Empty;
                    ColaboradorTextBox.Focus();
                    return;
                }
            }

            _colaborador = new ColaboradoresBO().ConsultaColaborador(Convert.ToInt32(ColaboradorTextBox.Text.Trim()));

            if (!_colaborador.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ColaboradorTextBox.Focus();
                return;
            }

            NomeColaboradorTextBox.Text = _colaborador.Nome;

            // buscando dados para emprestimo
            _emprestimo = new EmprestimoLivrosBO().Consultar(_livros.Id);

            dataDateTimePicker.Value = DateTime.Now;

            if (_emprestimo.Existe && _emprestimo.IdColaborador != _colaborador.Id)
            {
                if (new Notificacoes.Mensagem("Livro emprestado para o colabolador " + _emprestimo.NomeColaborador + "." +
                        Environment.NewLine + "Confirma a reserva ? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    ColaboradorTextBox.Focus();
                    return;
                }
            }

            // verificar se já está reservado
            _reserva = new ReservaLivrosBO().Consultar(_livros.Id);
            if (_reserva.Existe && _emprestimo.IdColaborador != _colaborador.Id)
            {
                if (new Notificacoes.Mensagem("Livro reservado para o colabolador " + _emprestimo.NomeColaborador + "." +
                    Environment.NewLine + "Confirma a reserva ? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    ColaboradorTextBox.Focus();
                    return;
                }
            }

            _reserva = new ReservaLivrosBO().Consultar(_livros.Id, _colaborador.Id);

            if (_reserva.Existe && _reserva.DataEmprestimo == DateTime.MinValue)
            {
                new Notificacoes.Mensagem("Livro já reservado para esse colabolador. " + ".", Publicas.TipoMensagem.Alerta).ShowDialog();
                ColaboradorTextBox.Focus();
                return;
            }

            dataDateTimePicker.Value = DateTime.Now;

            gravarButton.Enabled = true;
        }

        private void dataDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void Reserva_Shown(object sender, EventArgs e)
        {
            _categorias = new CategoriaLivrosBO().Listar();

            foreach (var item in _categorias.Where(w => w.Ativo).OrderBy(o => o.Id))
            {
                CategoriaComboBox.Items.Add(item.Descricao);
            }

            if (!string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
                codigoTextBox_Validating(sender, new CancelEventArgs());

            if (!string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim()))
            {
                ColaboradorTextBox_Validating(sender, new CancelEventArgs());
                gravarButton.Focus();
            }
        }

        private void pesquisaCategoriaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                new Pesquisas.Livros().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            codigoTextBox_Validating(sender, new CancelEventArgs());
        }

        private void PesquisaColaboradorButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                new Pesquisas.Colaborador().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_reserva == null)
                _reserva = new ReservaLivros();

            _reserva.IdColaborador = _colaborador.Id;
            _reserva.IdLivro = _livros.Id;
            _reserva.DataSolicitado = dataDateTimePicker.Value;
            
            if (!new ReservaLivrosBO().Gravar(_reserva))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            NomeTextBox.Text = string.Empty;
            ColaboradorTextBox.Text = string.Empty;
            NomeColaboradorTextBox.Text = string.Empty;
            dataDateTimePicker.Value = DateTime.Now.Date;

            codigoTextBox.Focus();
            gravarButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new LivrosBO().Excluir(_livros.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void ColaboradorTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaColaboradorButton.Enabled = string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim());
        }
    }
}
