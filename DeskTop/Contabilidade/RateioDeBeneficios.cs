using Classes;
using DynamicFilter;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace Suportte.Contabilidade
{
    public partial class RateioDeBeneficios : Form
    {
        public RateioDeBeneficios()
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

                    gridGroupingControl3.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl3.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl3.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl3.BackColor = Publicas._panelTitulo;

                    gridGroupingControl4.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl4.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl4.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl4.BackColor = Publicas._panelTitulo;

                    gridGroupingControl5.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl5.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl5.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl5.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        private class Resumo
        {
            public int CodigoConta { get; set; }
            public string NomeConta { get; set; }
            public decimal Valor { get; set; }
        }

        Classes.Empresa _empresa;
        Classes.RateioBeneficios.Parametros _param;
        Classes.RateioBeneficios.Rateio _rateio;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;
        List<Classes.RateioBeneficios.Associacao> _listaAssociacoes;

        List<Classes.RateioBeneficios.RateioPercentualCustoSetor> _listaPercentual;
        List<Classes.RateioBeneficios.RateioPercentualCustoSetor> _listaPercentualVT;
        List<Classes.RateioBeneficios.RateioPercentualCustoSetor> _listaPercentualConvenio;
        List<RateioBeneficios.ValoresRateados> _listaValores;
        List<Resumo> _listaResumo;

        int referencia;
        bool arquivoGerado = false;
        DateTime _dataInicio = DateTime.MaxValue;
        DateTime _data = DateTime.MaxValue;

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

        private void RateioDeBeneficios_Shown(object sender, EventArgs e)
        {
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { gravarButton, LimparButton, excluirButton, CopiarButton };
            _botoes = Publicas.CentralizarBotoes(_botoes, this.Width, LimparButton.Left - (gravarButton.Left + gravarButton.Width));

            for (int i = 0; i < _botoes.Count(); i++)
            {
                if (i == 0)
                    gravarButton.Left = _botoes[i].Left;
                if (i == 1)
                    LimparButton.Left = _botoes[i].Left;
                if (i == 2)
                    excluirButton.Left = _botoes[i].Left;
                if (i == 3)
                    CopiarButton.Left = _botoes[i].Left;
            }

            this.Top = 60;
            _listaEmpresas = new EmpresaBO().Listar(false);
            _listaEmpresasAutorizadas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._idUsuario);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

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

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 22;

            gridGroupingControl2.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl2.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl2.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

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
            this.gridGroupingControl2.Table.DefaultRecordRowHeight = 22;

            gridGroupingControl4.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl4.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl4.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl4.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl4.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl4.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl4.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl4.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl4.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl4.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl4.SetMetroStyle(metroColor);
                this.gridGroupingControl4.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl4.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl4.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl4.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl4.Table.DefaultRecordRowHeight = 22;

            gridGroupingControl4.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl4.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl4.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl4.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl4.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl4.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl4.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl4.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl4.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl4.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl4.SetMetroStyle(metroColor);
                this.gridGroupingControl4.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl4.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl4.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl4.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl4.Table.DefaultRecordRowHeight = 22;


            gridGroupingControl5.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl5.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl5.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl5.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl5.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl5.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl5.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl5.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl5.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl5.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl5.SetMetroStyle(metroColor);
                this.gridGroupingControl5.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl5.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl5.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl5.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl5.Table.DefaultRecordRowHeight = 22;

            gridGroupingControl5.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl5.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl5.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl5.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl5.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl5.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl5.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl5.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl5.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl5.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl5.SetMetroStyle(metroColor);
                this.gridGroupingControl5.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl5.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl5.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl5.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl5.Table.DefaultRecordRowHeight = 22;

            GridSummaryRowDescriptor _soma;

            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor.DataMember = "Quantidade";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "Quantidade";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor1 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor1.Appearance.AnySummaryCell.Format = "n3";
            summaryColumnDescriptor1.Appearance.GroupCaptionSummaryCell.Format = "n3";
            summaryColumnDescriptor1.DataMember = "Percentual";
            summaryColumnDescriptor1.Format = "{Sum}";
            summaryColumnDescriptor1.Name = "Percentual";
            summaryColumnDescriptor1.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor2.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor2.DataMember = "Debito";
            summaryColumnDescriptor2.Format = "{Sum}";
            summaryColumnDescriptor2.Name = "Debito";
            summaryColumnDescriptor2.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor3 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor3.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor3.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor3.DataMember = "Credito";
            summaryColumnDescriptor3.Format = "{Sum}";
            summaryColumnDescriptor3.Name = "Credito";
            summaryColumnDescriptor3.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor4 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor4.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor4.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor4.DataMember = "Valor";
            summaryColumnDescriptor4.Format = "{Sum}";
            summaryColumnDescriptor4.Name = "Valor";
            summaryColumnDescriptor4.SummaryType = SummaryType.DoubleAggregate;

            _soma = new GridSummaryRowDescriptor("Sum", "Total",
                    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1 });

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
            
            _soma = new GridSummaryRowDescriptor("Sum", "Total",
                    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor2, summaryColumnDescriptor3 });

            _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;
            try
            {
                gridGroupingControl2.TableDescriptor.SummaryRows.Add(_soma);
            }
            catch { }

            gridGroupingControl2.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl2.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl2.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl2.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";

            _soma = new GridSummaryRowDescriptor("Sum", "Total",
                    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor4 });

            _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            try
            {
                gridGroupingControl3.TableDescriptor.SummaryRows.Add(_soma);
            }
            catch { }

            gridGroupingControl3.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl3.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl3.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl3.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl3.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl3.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl3.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";

            _soma = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1 });

            _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            try
            {
                gridGroupingControl4.TableDescriptor.SummaryRows.Add(_soma);
            }
            catch { }

            gridGroupingControl4.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl4.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl4.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl4.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl4.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl4.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl4.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";

            try
            {
                gridGroupingControl5.TableDescriptor.SummaryRows.Add(_soma);
            }
            catch { }

            gridGroupingControl5.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl5.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl5.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl5.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl5.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl5.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl5.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
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

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            tabControlAdv1.SelectedTab = tabPageAdv1;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gridGroupingControl1.Focus();
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
                LimparButton.Focus();
            }
        }

        private void CopiarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                excluirButton.Focus();
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

        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void limparButton_Enter(object sender, EventArgs e)
        {
            LimparButton.BackColor = Publicas._botaoFocado;
            LimparButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void excluirButton_Enter(object sender, EventArgs e)
        {
            excluirButton.BackColor = Publicas._botaoFocado;
            excluirButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void CopiarButton_Enter(object sender, EventArgs e)
        {
            CopiarButton.BackColor = Publicas._botaoFocado;
            CopiarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void CopiarButton_Validating(object sender, CancelEventArgs e)
        {
            CopiarButton.BackColor = Publicas._botao;
            CopiarButton.ForeColor = Publicas._fonteBotao;
        }

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            LimparButton.BackColor = Publicas._botao;
            LimparButton.ForeColor = Publicas._fonteBotao;
        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            empresaComboBoxAdv.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _empresa = null;

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }

            if (_empresa == null)
            {
                new Notificacoes.Mensagem("Selecione a Empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            if (_listaEmpresasAutorizadas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.EmpresaAutoriza).Count() == 0)
            {
                new Notificacoes.Mensagem("Usuário não autorizado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            _param = new RateioBeneficioBO().ConsultarParametro(_empresa.IdEmpresa);

            if (!_param.Existe)
            {
                new Notificacoes.Mensagem("Parâmetros de rateio não cadastrado para essa empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            RateioRegraVTTabPage.TabVisible = _param.RegraEspecificaVT;
            tabPageAdv5.TabVisible = _param.RegraEspecificaConvenios;

            _listaAssociacoes = new RateioBeneficioBO().ListarAssociacoes(_empresa.IdEmpresa, _param.Id);
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            // Para pegar o último dia do mês selecionado.
            try
            {
                if (referenciaMaskedEditBox.ClipText.Trim().Length != 6)
                {
                    new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }
                _dataInicio = Convert.ToDateTime("01/" + referenciaMaskedEditBox.Text);
                _data = _dataInicio.AddMonths(1).AddDays(-1);
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }
                        
            referencia = Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2));

            _rateio = new RateioBeneficioBO().ConsultarRateio(_empresa.IdEmpresa, referencia);

            mensagemSistemaLabel.Text = "Pesquisando, aguarde...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            if (!_rateio.Existe)
            {
                _listaPercentual = new RateioBeneficioBO().ListarFuncionariosSetor(_empresa.CodigoEmpresaGlobus, _data, _listaAssociacoes, _param.CodigoFuncoes);

                if (_param.RegraEspecificaVT)
                    _listaPercentualVT = new RateioBeneficioBO().ListarFuncionariosSetorPorEvento(_empresa.CodigoEmpresaGlobus, _data, _listaAssociacoes, _param.CodigoFuncoes, _param.CodigoEventosValeTransporte);

                if (_param.RegraEspecificaConvenios)
                    _listaPercentualConvenio = new RateioBeneficioBO().ListarFuncionariosComConvenio(_empresa.CodigoEmpresaGlobus, _data, _listaAssociacoes, _param.CodigoFuncoes, _param.IgnorarFuncionarioSemConvenioMedico, _param.IgnorarFuncionarioSemConvenioOdontologico);

                _listaValores = new RateioBeneficioBO().ListarLancamentosCTBComCustoTransitorio(_empresa.CodigoEmpresaGlobus, _dataInicio, _data, _empresa.IdEmpresa, 
                    _listaAssociacoes, _listaPercentual, _listaPercentualVT, _listaPercentualConvenio);
                _listaValores.ForEach(u => u.Historico = _param.HistoricoPadrao);
            }
            else
            {
                _listaPercentual = new RateioBeneficioBO().ListarPercentual(_rateio.Id, _param.NumeroPlano, "PD");

                if (_param.RegraEspecificaVT)
                    _listaPercentualVT = new RateioBeneficioBO().ListarPercentual(_rateio.Id, _param.NumeroPlano, "VT");

                if (_param.RegraEspecificaConvenios)
                    _listaPercentualConvenio = new RateioBeneficioBO().ListarPercentual(_rateio.Id, _param.NumeroPlano, "CM");

                _listaValores = new RateioBeneficioBO().ListarRateio(_rateio.Id, _param.NumeroPlano);
            }

            _listaResumo = new List<Resumo>();

            foreach (var item in _listaValores.GroupBy(g => new { g.CodigoConta, g.Conta }))
            {
                decimal _Valor = 0;

                foreach (var itemV in _listaValores.Where(w => w.CodigoConta == item.Key.CodigoConta))
                {
                    _Valor = _Valor + itemV.Debito;
                }

                _listaResumo.Add(new Resumo() { CodigoConta = item.Key.CodigoConta, Valor = _Valor, NomeConta = item.Key.Conta });
            }
            
            gridGroupingControl1.DataSource = _listaPercentual;
            gridGroupingControl4.DataSource = _listaPercentualVT;
            gridGroupingControl5.DataSource = _listaPercentualConvenio;
            gridGroupingControl2.DataSource = _listaValores;
            gridGroupingControl3.DataSource = _listaResumo;

            gravarButton.Enabled = true;
            excluirButton.Enabled = _rateio.Existe;
            CopiarButton.Enabled = _listaValores.Count() != 0;

            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            if (gridGroupingControl2.TableDescriptor.Columns[e.Inner.ColIndex - 1].MappingName == "Historico")
                gridGroupingControl2.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            else
                gridGroupingControl2.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
        }

        private void RateioDeBeneficios_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            mensagemSistemaLabel.Text = "Gravando, aguarde...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            if (_rateio == null)
                _rateio = new RateioBeneficios.Rateio();

            if (_listaPercentualVT == null)
                _listaPercentualVT = new List<RateioBeneficios.RateioPercentualCustoSetor>();

            if (_listaPercentualConvenio == null)
                _listaPercentualConvenio = new List<RateioBeneficios.RateioPercentualCustoSetor>();

            if (_listaPercentual == null)
                _listaPercentual = new List<RateioBeneficios.RateioPercentualCustoSetor>();

            _rateio.IdEmpresa = _empresa.IdEmpresa;
            _rateio.IdUsuario = Publicas._usuario.Id;
            _rateio.Referencia = referencia;
            _rateio.ArquivoGerado = arquivoGerado;

            _listaPercentual.ForEach(u => u.Regra = "PD");
            _listaPercentualVT.ForEach(u => u.Regra = "VT");
            _listaPercentualConvenio.ForEach(u => u.Regra = "CM");

            if (_param.RegraEspecificaVT)
                _listaPercentual.AddRange(_listaPercentualVT);

            if (_param.RegraEspecificaConvenios)
                _listaPercentual.AddRange(_listaPercentualConvenio);

            if (!new RateioBeneficioBO().Gravar(_rateio, _listaPercentual, _listaValores)) 
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            this.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";
            this.Refresh();
            LimparButton_Click(sender, e);
        }

        private void LimparButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = new List<RateioBeneficios.RateioPercentualCustoSetor>();
            gridGroupingControl2.DataSource = new List<RateioBeneficios.ValoresRateados>();
            gridGroupingControl3.DataSource = new List<Resumo>();
            gridGroupingControl4.DataSource = new List<RateioBeneficios.RateioPercentualCustoSetor>();
            gridGroupingControl5.DataSource = new List<RateioBeneficios.RateioPercentualCustoSetor>();
            referenciaMaskedEditBox.Text = string.Empty;
            referenciaMaskedEditBox.Focus();

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            CopiarButton.Enabled = false;

            tabControlAdv1.SelectedTab = tabPageAdv1;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new RateioBeneficioBO().ExcluirRateio(_rateio.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            LimparButton_Click(sender, e);
        }

        private void CopiarButton_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Publicas._caminhoAnexosRateioCTB))
                System.IO.Directory.CreateDirectory(Publicas._caminhoAnexosRateioCTB);                

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;

            string nomeArquivo = "Rateio_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" + referencia.ToString();

            xlApp = new Excel.Application();

            try
            {

                xlApp.DisplayAlerts = false;

                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int linha = 1;
                int col = 1;

                mensagemSistemaLabel.Text = "Exportando dados para o Excel, aguarde...";
                this.Cursor = Cursors.WaitCursor;
                this.Refresh();

                #region titulo colunas
                xlWorkSheet.Cells[linha, col] = "Débito ";
                col++;

                xlWorkSheet.Cells[linha, col] = "Crédito";
                col++;
                xlWorkSheet.Cells[linha, col] = "c/custo débito";
                col++;
                xlWorkSheet.Cells[linha, col] = "c/custo crédito";
                col++;
                xlWorkSheet.Cells[linha, col] = "histórico deb";
                col++;
                xlWorkSheet.Cells[linha, col] = "histórico créd";
                col++;
                xlWorkSheet.Cells[linha, col] = " valor débito";
                col++;
                xlWorkSheet.Cells[linha, col] = "valor crédito";
                col++;
                xlWorkSheet.Cells[linha, col] = "lote";
                col++;
                xlWorkSheet.Cells[linha, col] = "data lcto";
                col++;
                xlWorkSheet.Cells[linha, col] = "TIPO DE LANCTO";

                #endregion

                foreach (var itemC in _listaValores)
                {
                    col = 1;
                    linha++;

                    #region Cabeçalho da Nota
                    xlWorkSheet.Cells[linha, col] = itemC.CodigoConta;
                    col++;
                    xlWorkSheet.Cells[linha, col] = (itemC.ContraPartida == 0 ? itemC.CodigoConta : itemC.ContraPartida);
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.CodigoCusto;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.CodigoCustoCredito;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Historico;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Historico;
                    col++;
                    xlWorkSheet.Cells[linha, col] = (itemC.Debito == 0 ? itemC.Credito.ToString() : itemC.Debito.ToString());
                    col++;
                    xlWorkSheet.Cells[linha, col] = (itemC.Credito == 0 ? itemC.Debito.ToString() : itemC.Credito.ToString());
                    col++;
                    xlWorkSheet.Cells[linha, col] = _param.Lote;
                    col++;
                    xlWorkSheet.Cells[linha, col] = _data;
                    col++;
                    xlWorkSheet.Cells[linha, col] = "1";
                    #endregion

                }

                xlWorkSheet.Columns.AutoFit();
                xlWorkBook.SaveAs(Publicas._caminhoAnexosRateioCTB + nomeArquivo + ".xlsx", Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
                                Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                try
                {
                    xlWorkBook.Close(true, misValue, misValue);
                }
                catch
                {
                }

                try
                {
                    xlApp.Quit();
                }
                catch { }

                this.Cursor = Cursors.Default;
                mensagemSistemaLabel.Text = "";
                gravarButton_Click(sender, e);
                new Notificacoes.Mensagem("Arquivo gerado com sucesso." + Environment.NewLine +
                    "Salvo na pasta " + Publicas._caminhoAnexosRateioCTB, Publicas.TipoMensagem.Sucesso).ShowDialog();
                arquivoGerado = true;
                this.Refresh();
            }
            catch 
            {
                gravarButton_Click(sender, e);
                new Notificacoes.Mensagem("Não foi possível gerar o arquivo." + Environment.NewLine +
                    "Tente em outra maquina.", Publicas.TipoMensagem.Erro).ShowDialog();
            }            
        }

    }
}
