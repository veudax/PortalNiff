using Classes;
using Negocio;
using Syncfusion.GridHelperClasses;
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

namespace Suportte.Contabilidade
{
    public partial class CopiaContasLalur : Form
    {
        public CopiaContasLalur()
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
                    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl1.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Lalur.Formulas _formula;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;
        public List<Classes.Lalur.ContasDaFormula> _listaAssociacoesOrigem;
        public List<Classes.Lalur.ContasDaFormula> _listaAssociacoesDestino;
        GridCurrentCell _colunaCorrente;

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

        private void CopiaContasLalur_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);
            _listaEmpresasAutorizadas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._idUsuario);

            EmpresaOrigemComboBox.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            EmpresaOrigemComboBox.DisplayMember = "CodigoeNome";
            EmpresaOrigemComboBox.Focus();

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.gridGroupingControl1);

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl1.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            GridMetroColors metroColor = new GridMetroColors();
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
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 22;

            gridGroupingControl1.DataSource = _listaAssociacoesOrigem;
        }

        private void EmpresaOrigemComboBox_KeyDown(object sender, KeyEventArgs e)
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
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
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

        private void EmpresaOrigemComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
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

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void EmpresaOrigemComboBox_Validating(object sender, CancelEventArgs e)
        {
            EmpresaOrigemComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == EmpresaOrigemComboBox.Text))
            {
                _empresa = item;
            }

            if (_listaEmpresasAutorizadas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.EmpresaAutoriza).Count() == 0)
            {
                new Notificacoes.Mensagem("Usuário não autorizado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                EmpresaOrigemComboBox.Focus();
                return;
            }
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            { // buscar da empresa do usuario
                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;
                Record dr = null;

                if (rec != null)
                {
                    dr = rec.GetRecord() as Record;

                    if ((bool)dr["SelecionadoAnterior"])
                        e.Style.TextColor = Color.Gray;
                }
            }
            catch { }
        }

        private void gridGroupingControl1_TableControlCellMouseDown(object sender, GridTableControlCellMouseEventArgs e)
        {
            _colunaCorrente = gridGroupingControl1.TableControl.CurrentCell;
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
                Publicas._idEmpresa = _empresa.IdEmpresa;
                new Pesquisas.FormulasLalur().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _formula = new LalurBO().Consultar(_empresa.IdEmpresa, Convert.ToInt32(codigoTextBox.Text));

            if (!_formula.Existe)
            {
                new Notificacoes.Mensagem("Fórmula não cadastrada", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            if (!_formula.Ativo)
            {
                new Notificacoes.Mensagem("Fórmula não está ativa", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _formula.Descricao;

            _listaAssociacoesOrigem = new LalurBO().ListarContas(_formula.Id);

            if (_listaAssociacoesDestino != null)
            {
                foreach (var item in _listaAssociacoesOrigem)
                {
                    foreach (var itemO in _listaAssociacoesDestino.Where(w => w.CodigoConta == item.CodigoConta &&
                                                                             w.NumeroPlano == item.NumeroPlano))
                    {
                        item.Marcado = true;
                        item.SelecionadoAnterior = true;
                    }
                }
            }

            gridGroupingControl1.DataSource = new List<Lalur.ContasDaFormula>();
            gridGroupingControl1.DataSource = _listaAssociacoesOrigem;
            gravarButton.Enabled = true;
        }

        private void gridGroupingControl1_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            try
            {

                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex()] as GridRecordRow;

                bool marcado = false;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr["Marcado"];

                        foreach (var item in _listaAssociacoesOrigem.Where(w => w.CodigoConta == (int)dr["CodigoConta"] &&
                                                                                w.NumeroPlano == (int)dr["NumeroPlano"]))
                        {
                            if (!marcado)
                                item.Marcado = false;
                            else
                                item.Marcado = marcado;
                        }
                    }
                }
            }
            catch { }

            gridGroupingControl1.DataSource = new List<Lalur.ContasDaFormula>();
            gridGroupingControl1.DataSource = _listaAssociacoesOrigem;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaAssociacoesOrigem.Where(w => w.Marcado).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma conta selecionada para copiar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            Close();
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
