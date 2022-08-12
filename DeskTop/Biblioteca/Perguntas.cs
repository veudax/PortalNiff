using Classes;
using Negocio;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
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

namespace Suportte.Biblioteca
{
    public partial class Perguntas : Form
    {
        public Perguntas()
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
                {
                    dataDateTimePicker.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Black;
                    PerguntasGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    PerguntasGrid.ColorStyles = ColorStyles.Office2010Black;
                    PerguntasGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    PerguntasGrid.BackColor = Publicas._panelTitulo;
                    label6.ForeColor = Publicas._fonte;
                    ResenhaRichText.BackColor = Publicas._fundo;
                    ResenhaRichText.ForeColor = Publicas._fonte;
                }

            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.CategoriaLivros> _categorias;
        Classes.Livros _livros;
        Classes.Colaboradores _colaborador;
        Classes.EmprestimoLivros _emprestimo;
        Classes.ResenhaLivros _resenha;
        Classes.ResenhaLivros _resenhaAnterior;
        List<Classes.PerguntasLivros> _perguntas;
        List<Classes.PerguntasLivros> _perguntasAnteriores;

        bool _alterar = false;
        int _rowIndex = 0;
        string _perguntaOriginal = "";

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
            {
                if (PerguntasTextBox.Enabled)
                {
                    TabControl.SelectedTab = PerguntasTabPage;
                    PerguntasTextBox.Focus();
                }
                else
                {
                    TabControl.SelectedTab = ResenhaTabPage;
                    ResenhaRichText.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void PerguntasTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                RespostaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ColaboradorTextBox.Focus();
            }
        }

        private void RespostaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PerguntasTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PerguntasTextBox.Focus();
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

        private void PerguntasTextBox_Enter(object sender, EventArgs e)
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

            TabControl.SelectedTab = PerguntasTabPage;

            _colaborador = new ColaboradoresBO().ConsultaColaborador(Convert.ToInt32(ColaboradorTextBox.Text.Trim()));

            if (!_colaborador.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ColaboradorTextBox.Focus();
                return;
            }

            NomeColaboradorTextBox.Text = _colaborador.Nome;

            // buscando dados para emprestimo
            _emprestimo = new EmprestimoLivrosBO().Consultar(_livros.Id, _colaborador.Id);

            dataDateTimePicker.Value = DateTime.Now;
            if (!_emprestimo.Existe)
            {
                new Notificacoes.Mensagem("Livro não emprestado para o colaborador.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ColaboradorTextBox.Focus();
                return;
            }

            if (!_emprestimo.Devolvido)
            {
                new Notificacoes.Mensagem("Livro não consta como devolvido no sistema.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ColaboradorTextBox.Focus();
                return;
            }

            _resenha = new ResenhaLivrosBO().Consultar(_livros.Id, _colaborador.Id);

            _resenhaAnterior = new ResenhaLivrosBO().Consultar(_livros.Id, _colaborador.Id, true);
            _perguntasAnteriores = new ResenhaLivrosBO().Listar(_resenhaAnterior.Id);

            PerguntasTextBox.Enabled = true;
            RespostaTextBox.Enabled = true;
            PerguntasGrid.Enabled = true;
            ResenhaRichText.Enabled = true;

            if (_resenhaAnterior.Existe && _resenhaAnterior.IdColaborador != _colaborador.Id &&
                !string.IsNullOrEmpty(_resenhaAnterior.Resenha) && _perguntasAnteriores.Count() != 0)
            {
                PerguntasTextBox.Enabled = false;
                RespostaTextBox.Enabled = false;
                PerguntasGrid.Enabled = false;
                ResenhaRichText.Enabled = true;
                PerguntasTabPage.TabVisible = false;
                //new Notificacoes.Mensagem("Perguntas e Resenha já cadastradas para este livro.", Publicas.TipoMensagem.Alerta).ShowDialog();
                //codigoTextBox.Focus();
                //return;
            }

            //if (_resenhaAnterior.Existe && _resenhaAnterior.IdColaborador != _colaborador.Id &&
            //    !string.IsNullOrEmpty(_resenhaAnterior.Resenha) && _perguntasAnteriores.Count() == 0)
            //{
            //    if (new Notificacoes.Mensagem("Resenha cadastrada, deseja cadastrar as perguntas?", Publicas.TipoMensagem.Alerta).ShowDialog() == DialogResult.No)
            //    {
            //        codigoTextBox.Focus();
            //        return;
            //    }

            //    ResenhaRichText.Enabled = false;
            //}

            //if (_resenhaAnterior.Existe && _resenhaAnterior.IdColaborador != _colaborador.Id &&
            //    string.IsNullOrEmpty(_resenhaAnterior.Resenha) && _perguntasAnteriores.Count() != 0)
            //{
            //    if (new Notificacoes.Mensagem("Perguntas cadastradas. Deseja cadastrar a resenha?", Publicas.TipoMensagem.Alerta).ShowDialog() == DialogResult.No)
            //    {
            //        codigoTextBox.Focus();
            //        return;
            //    }

            //    PerguntasTextBox.Enabled = false;
            //    RespostaTextBox.Enabled = false;
            //    PerguntasGrid.Enabled = false;
            //    ResenhaRichText.Enabled = true;
            //}

            _perguntas = new List<PerguntasLivros>();

            if (_resenha.Existe)
            {
                _perguntas = new ResenhaLivrosBO().Listar(_resenha.Id);

                ResenhaRichText.Text = _resenha.Resenha;
                ativoCheckBox.Checked = _resenha.Ativo;

                PerguntasGrid.DataSource = _perguntas;
            }

            gravarButton.Enabled = true;
        }

        private void PerguntasTextBox_Validating(object sender, CancelEventArgs e)
        {
            PerguntasTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                PerguntasTextBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(PerguntasTextBox.Text.Trim()))
            {
                TabControl.SelectedTab = ResenhaTabPage;
                ResenhaRichText.Focus();
            }
        }

        private void RespostaTextBox_Validating(object sender, CancelEventArgs e)
        {
            RespostaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                RespostaTextBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

            PerguntasLivros _pergunta = new PerguntasLivros();

            _pergunta.Pergunta = PerguntasTextBox.Text.Trim();
            _pergunta.Resposta = RespostaTextBox.Text.Trim();

            PerguntasGrid.DataSource = new List<PerguntasLivros>();

            if (!_alterar)
                _perguntas.Add(_pergunta);
            else
            {
                foreach (var item in _perguntas.Where(w => w.Pergunta == _perguntaOriginal))
                {
                    item.Pergunta = PerguntasTextBox.Text;
                    item.Resposta = RespostaTextBox.Text;
                }
            }

            _alterar = false;
            _perguntaOriginal = "";
            PerguntasTextBox.Text = string.Empty;
            RespostaTextBox.Text = string.Empty;

            PerguntasGrid.DataSource = _perguntas;
        }

        private void Perguntas_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);
            _categorias = new CategoriaLivrosBO().Listar();

            foreach (var item in _categorias.Where(w => w.Ativo).OrderBy(o => o.Id))
            {
                CategoriaComboBox.Items.Add(item.Descricao);
            }

            if (!string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                codigoTextBox_Validating(sender, new CancelEventArgs());
                ColaboradorTextBox_Validating(sender, new CancelEventArgs());

                dataDateTimePicker.Focus();
            }

            ativoCheckBox.Enabled = (Publicas._usuario.Administrador || 
                Publicas._usuario.Departamento.Contains("RH") || Publicas._usuario.Departamento.ToUpper().Contains("Recursos Humanos".ToUpper()));

            GridMetroColors metroColor = new GridMetroColors();

            PerguntasGrid.SortIconPlacement = SortIconPlacement.Left;
            PerguntasGrid.TopLevelGroupOptions.ShowFilterBar = false;
            PerguntasGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            PerguntasGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            PerguntasGrid.TableControl.CellToolTip.Active = true;
            PerguntasGrid.RecordNavigationBar.Label = "Perguntas";

            for (int i = 0; i < PerguntasGrid.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0 || i == 5 || i == 6)
                    PerguntasGrid.TableDescriptor.Columns[i].ReadOnly = true;
                else
                    PerguntasGrid.TableDescriptor.Columns[i].ReadOnly = false;

                PerguntasGrid.TableDescriptor.Columns[i].AllowSort = true;
            }

            metroColor.HeaderBottomBorderColor = Publicas._bordaEntrada;
            metroColor.HeaderColor.HoverColor = Publicas._bordaEntrada;
            metroColor.HeaderColor.PressedColor = Publicas._bordaEntrada;

            metroColor.CheckBoxColor.BorderColor = Publicas._bordaEntrada;
            metroColor.PushButtonColor.PushedBackColor = Publicas._bordaEntrada;
            metroColor.PushButtonColor.HoverBackColor = Publicas._bordaEntrada;
            metroColor.PushButtonColor.NormalBackColor = Color.WhiteSmoke;
            metroColor.ComboboxColor.NormalBorderColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.HoverBorderColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.HoverBackColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.PressedBackColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.NormalBackColor = Color.WhiteSmoke;

            if (!Publicas._TemaBlack)
            {
                this.PerguntasGrid.SetMetroStyle(metroColor);
                this.PerguntasGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.PerguntasGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.PerguntasGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.PerguntasGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.PerguntasGrid.Table.DefaultRecordRowHeight = 45;
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

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            NomeTextBox.Text = string.Empty;
            ColaboradorTextBox.Text = string.Empty;
            NomeColaboradorTextBox.Text = string.Empty;
            dataDateTimePicker.Value = DateTime.Now.Date;
            PerguntasTextBox.Text = string.Empty;
            RespostaTextBox.Text = string.Empty;
            ResenhaRichText.Text = string.Empty;

            PerguntasTextBox.Enabled = true;
            RespostaTextBox.Enabled = true;
            PerguntasGrid.Enabled = true;
            ResenhaRichText.Enabled = true;

            PerguntasGrid.DataSource = new List<PerguntasLivros>();
            codigoTextBox.Focus();
            gravarButton.Enabled = false;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_resenha == null)
                _resenha = new ResenhaLivros();

            _resenha.IdColaborador = _colaborador.Id;
            _resenha.IdLivros = _livros.Id;
            _resenha.Resenha = ResenhaRichText.Text;
            _resenha.Ativo = ativoCheckBox.Checked;
            _resenha.Data = dataDateTimePicker.Value;
            _resenha.TemPerguntas = _perguntas.Count() != 0;
            _resenha.TemResenha = !string.IsNullOrEmpty(_resenha.Resenha.Trim());

            if (!new ResenhaLivrosBO().Gravar(_resenha, _perguntas))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
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

        private void AlterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GridRecordRow rec = PerguntasGrid.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;

                    if (dr != null)
                    {
                        _alterar = true;
                        PerguntasTextBox.Text = (string)dr["Pergunta"];
                        RespostaTextBox.Text = (string)dr["Resposta"];

                        _perguntaOriginal = PerguntasTextBox.Text;
                    }
                }
            }
            catch { }
        }

        private void PerguntasGrid_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();
        }
    }
}
