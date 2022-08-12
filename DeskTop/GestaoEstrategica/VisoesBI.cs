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

namespace Suportte.GestaoEstrategica
{
    public partial class VisoesBI : Form
    {
        public VisoesBI()
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

        Classes.VisoesBI _visao;
        List<Classes.DetalheVisoes> _listaDetalhes;
        string _descricaoAnterior;
        int _rowIndex;

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
                ExclusivoCheckBox.Focus();
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
                RubricaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ExclusivoCheckBox.Focus();
            }
        }

        private void RubricaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
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

        private void nomeTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
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

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new VisoesBIBO().Proximo().ToString();
            ativoCheckBox.Focus();
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
                new Pesquisas.VisoesBI().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _visao = new VisoesBIBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (_visao != null && _visao.Existe)
            {
                ativoCheckBox.Checked = _visao.Ativo;
                nomeTextBox.Text = _visao.Descricao;

                _listaDetalhes = new VisoesBIBO().Listar(_visao.Id);

                gridGroupingControl.DataSource = _listaDetalhes;
            }

            excluirButton.Enabled = _visao.Existe;
            gravarButton.Enabled = true;

            if (Publicas._idRetornoPesquisa != 0)
                nomeTextBox.Focus();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            if (_listaDetalhes != null)
                _listaDetalhes.Clear();

            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            RubricaTextBox.Text = string.Empty;
            ativoCheckBox.Checked = false;
            gridGroupingControl.DataSource = new List<DetalheVisoes>();
            codigoTextBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição!", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            _visao.Id = Convert.ToInt32(codigoTextBox.Text);
            _visao.Ativo = ativoCheckBox.Checked;
            _visao.Descricao = nomeTextBox.Text;

            if (!new VisoesBIBO().Gravar(_visao, _listaDetalhes))
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

            if (!new VisoesBIBO().Excluir(_visao.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RubricaTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (_listaDetalhes == null)
                _listaDetalhes = new List<DetalheVisoes>();

            bool temRubrica = false;
            if (!string.IsNullOrEmpty(RubricaTextBox.Text.Trim()))
            {
                gridGroupingControl.DataSource = new List<DetalheVisoes>();

                if (_descricaoAnterior == "")
                {
                    foreach (var item in _listaDetalhes.Where(w => w.Descricao == RubricaTextBox.Text.Trim()))
                    {
                        item.Descricao = RubricaTextBox.Text.Trim();
                        temRubrica = true;
                        break;
                    }

                    if (!temRubrica)
                    {
                        DetalheVisoes _det = new DetalheVisoes();
                        _det.Id = Convert.ToInt32(codigoTextBox.Text);
                        _det.Descricao = RubricaTextBox.Text.Trim();

                        _listaDetalhes.Add(_det);
                    }
                }
                else
                {
                    foreach (var item in _listaDetalhes.Where(w => w.Descricao == _descricaoAnterior.Trim()))
                    {
                        item.Descricao = RubricaTextBox.Text.Trim();
                        temRubrica = true;
                        break;
                    }

                    if (!temRubrica)
                    {
                        DetalheVisoes _det = new DetalheVisoes();
                        _det.Id = Convert.ToInt32(codigoTextBox.Text);
                        _det.Descricao = RubricaTextBox.Text.Trim();

                        _listaDetalhes.Add(_det);
                    }
                }
                RubricaTextBox.Text = string.Empty;
                RubricaTextBox.Focus();
                gridGroupingControl.DataSource = _listaDetalhes;
                _descricaoAnterior = "";
                return;
            }

            gridGroupingControl.Focus();
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _rowIndex = gridGroupingControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;

                    if (dr != null)
                    {
                        _descricaoAnterior = ((string)dr["Descricao"]);
                        RubricaTextBox.Text = _descricaoAnterior;
                        RubricaTextBox.Focus();
                    }
                }
            }
            catch { }
        }

        private void VisoesBI_Shown(object sender, EventArgs e)
        {

            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                gridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
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

            this.gridGroupingControl.SetMetroStyle(metroColor);

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl.Table.DefaultRecordRowHeight = 25;
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _rowIndex = gridGroupingControl.Table.CurrentRecord.GetRecord().GetRowIndex();
                int id; 
                GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    gridGroupingControl.DataSource = new List<DetalheVisoes>();

                    if (dr != null)
                    {
                        id = ((int)dr["IdDetalhe"]);

                        foreach (var item in _listaDetalhes.Where(w => w.IdDetalhe == id))
                        {
                            item.Excluir = true;
                        }
                    }

                    gridGroupingControl.DataSource = _listaDetalhes;
                }
            }
            catch { }
        }

        private void gridGroupingControl_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            Record dr;
            try
            { // buscar da empresa do usuario
                if (e.TableCellIdentity.RowIndex != -1)
                {
                    GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        dr = rec.GetRecord() as Record;
                        if (dr != null && (bool)dr["Excluir"])
                        {
                            e.Style.TextColor = Color.Red;
                        }
                    }
                }
            }
            catch { }
        }

        private void ExclusivoCheckBox_KeyDown(object sender, KeyEventArgs e)
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

        private void pesquisaCategoriaButton_Click(object sender, EventArgs e)
        {

            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.VisoesBI().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }
        }
    }
}
