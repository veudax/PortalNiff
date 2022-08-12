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

namespace Suportte.Suprimentos
{
    public partial class TiposDePedido : Form
    {
        public TiposDePedido()
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
                    gridGroupingControl2.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl2.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl2.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl2.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        string[] _listaTipoPedido = new string[] { " ", "Mensal", "Emergencial" };

        Classes.Empresa _empresa;
        
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasUsuario;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.Suprimentos.Pedidos> _listaPedidos;
        List<Classes.Suprimentos.Pedidos> _listaPedidosLog;
        List<Classes.Suprimentos.ItensPedido> _listaItensPedidos;
        GridCurrentCell _colunaCorrente;

        DateTime _dataInicio;
        DateTime _dataFim;
        string _referencia;
        string _numeroPedido;

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

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
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


        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                //PatText.Focus();
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

        private void TiposDePedido_Shown(object sender, EventArgs e)
        {
            this.Top = 60;
            int espacoEntreBotoes = limparButton.Left - (gravarButton.Left + gravarButton.Width);

            #region coloca os botões centralizados
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { gravarButton, limparButton};
            _botoes = Publicas.CentralizarBotoes(_botoes, this.Width, limparButton.Left - (gravarButton.Left + gravarButton.Width));

            for (int i = 0; i < _botoes.Count(); i++)
            {
                if (i == 0)
                    gravarButton.Left = _botoes[i].Left;
                if (i == 1)
                    limparButton.Left = _botoes[i].Left;                
            }

            #endregion

            _listaEmpresas = new EmpresaBO().Listar(false);
            _listaEmpresasUsuario = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._usuario.Id);
            _listaEmpresasAutorizadas = new List<Empresa>();

            foreach (var item in _listaEmpresasUsuario.Where(w => w.EmpresaAutoriza))
                _listaEmpresasAutorizadas.AddRange(_listaEmpresas.Where(w => w.IdEmpresa == item.IdEmpresa));

            empresaComboBoxAdv.DataSource = _listaEmpresasAutorizadas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            gridGroupingControl1.DataSource = new List<Classes.Suprimentos.Pedidos>();
            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl1.TableControl.CellToolTip.Active = true;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl1.RecordNavigationBar.Label = "Metas";

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].ReadOnly = true;

                /*if (gridGroupingControl1.TableDescriptor.Columns[i].MappingName == "TipoPedido")
                {
                    gridGroupingControl1.TableDescriptor.Columns[i].ReadOnly = false;
                    //gridGroupingControl1.TableDescriptor.Columns[i].Appearance.AnyRecordFieldCell.DataSource = _listaTipoPedido;
                }*/

                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = true;
                
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }
            GridDynamicFilter filter = new GridDynamicFilter();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(gridGroupingControl1);
            filter.WireGrid(gridGroupingControl2);

            GridMetroColors metroColor = new GridMetroColors();

            metroColor.HeaderBottomBorderColor = Publicas._bordaEntrada;
            metroColor.HeaderBottomBorderWeight = GridBottomBorderWeight.ExtraThin;

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

            GridSummaryRowDescriptor _soma;

            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor.DataMember = "Total";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "Total";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor1 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor1.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor1.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor1.DataMember = "TotalGrupo500e510";
            summaryColumnDescriptor1.Format = "{Sum}";
            summaryColumnDescriptor1.Name = "TotalGrupo500e510";
            summaryColumnDescriptor1.SummaryType = SummaryType.DoubleAggregate;


            _soma = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1});

            _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            try
            {
                gridGroupingControl1.TableDescriptor.SummaryRows.Add(_soma);
            }
            catch { }

            gridGroupingControl1.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl1.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl1.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl1.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl1.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl1.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl1.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            gridGroupingControl2.DataSource = new List<Suprimentos.MetasAprovadores>();
            gridGroupingControl2.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl2.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl2.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl2.TableControl.CellToolTip.Active = true;
            gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl2.RecordNavigationBar.Label = "Metas";

            for (int i = 0; i < gridGroupingControl2.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl2.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl2.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl2.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl2.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl2.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl2.SetMetroStyle(metroColor);
                this.gridGroupingControl2.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl2.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl2.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl2.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                referenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }           
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _empresa = null;

            foreach (var item in _listaEmpresasAutorizadas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }

            if (_empresa == null)
            {
                new Notificacoes.Mensagem("Selecione a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._setaParaBaixo)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._setaParaBaixo = false;
                return;
            }

            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                referenciaMaskedEditBox.Text = string.Empty;
                referenciaMaskedEditBox.Focus();
                return;
            }

            try
            {
                if (referenciaMaskedEditBox.ClipText.Trim().Length != 6)
                {
                    new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }
                _dataInicio = new DateTime(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(2, 4)), Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(0, 2)), 1);
                _dataFim = _dataInicio.AddMonths(1).AddDays(-1);
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            _referencia = referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2);

            //buscar pedidos e itens

            mensagemSistemaLabel.Text = "Aguarde, Pesquisando ...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            _listaPedidos = new SuprimentosBO().Listar(_empresa.IdEmpresa, _referencia, "T");
            _listaPedidosLog = new SuprimentosBO().Listar(_empresa.IdEmpresa, _referencia, "T");
            _listaItensPedidos = new SuprimentosBO().ListarItens(_empresa.IdEmpresa, _referencia, "T");

            foreach (var item in _listaPedidos)
            {
                foreach (var itemI in _listaItensPedidos.Where(w => w.NumeroPedido == item.NumeroPedido))
                {
                    itemI.Total = (itemI.Quantidade * itemI.ValorUnitario);
                    item.Total = item.Total + itemI.Total;

                    if (itemI.GrupoDespesa == 500 || itemI.GrupoDespesa == 510)
                        item.TotalGrupo500e510 = item.TotalGrupo500e510 + itemI.Total;
                }
            }

            GrupoDespesasCheckBox.Enabled = true;
            gridGroupingControl1.DataSource = _listaPedidos;
            gravarButton.Enabled = _listaPedidos.Count() != 0;
            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();

        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            
            if (!new SuprimentosBO().Gravar(_listaPedidos))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            string _descricao = "";
            foreach (var itemL in _listaPedidosLog)
            {
                foreach (var item in _listaPedidos.Where(w => w.Referencia == itemL.Referencia && w.NumeroPedido == itemL.NumeroPedido))
                {
                    if (item.TipoPedido != itemL.TipoPedido)
                        _descricao = _descricao + "Pedido " + item.NumeroPedido + " Tipo de " + itemL.TipoPedido.ToString() + " para " + item.TipoPedido.ToString() + ", ";
                }
            }

            if (!string.IsNullOrEmpty(_descricao))
                _descricao = _descricao.Substring(0, _descricao.Length - 2);

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou Tipo do Pedido da empresa " + empresaComboBoxAdv.Text + " referencia " +referenciaMaskedEditBox.Text +
                " [" + _descricao + "]";

            _log.Tela = "Suprimentos - Metas Aprovadores - Tipos Pedido";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            GrupoDespesasCheckBox.Checked = false;
            GrupoDespesasCheckBox.Enabled = false;
            referenciaMaskedEditBox.Text = string.Empty;
            gridGroupingControl1.DataSource = new List<Classes.Suprimentos.Pedidos>();
            gridGroupingControl2.DataSource = new List<Classes.Suprimentos.ItensPedido>();

            referenciaMaskedEditBox.Focus();
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        _numeroPedido = (string)dr["NumeroPedido"];

                        gridGroupingControl2.DataSource = _listaItensPedidos.Where(w => w.NumeroPedido == _numeroPedido).ToList();
                    }
                }
                _colunaCorrente = gridGroupingControl2.TableControl.CurrentCell;
            }
            catch { }
        }

        private void gridGroupingControl1_TableControlKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        _numeroPedido = (string)dr["NumeroPedido"];

                        gridGroupingControl2.DataSource = _listaItensPedidos.Where(w => w.NumeroPedido == _numeroPedido).ToList();
                    }
                }
                _colunaCorrente = gridGroupingControl2.TableControl.CurrentCell;
            }
            catch { }
        }

        private void AbertosRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                referenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void GrupoDespesasCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = new List<Classes.Suprimentos.Pedidos>();
            string Pedidos = "";
            if (GrupoDespesasCheckBox.Checked)
            {
                if (_listaPedidos.Count() != 0)
                {
                    foreach (var item in _listaItensPedidos.Where(w => w.GrupoDespesa == 500 || w.GrupoDespesa == 510))
                    {
                        Pedidos = Pedidos + item.NumeroPedido + "; ";
                    }

                    gridGroupingControl1.DataSource = _listaPedidos.Where(w => Pedidos.Contains(w.NumeroPedido)).ToList();
                }
            }
            else
            {
                gridGroupingControl1.DataSource = _listaPedidos;

            }
        }
    }
}
