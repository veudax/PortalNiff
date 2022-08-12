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
using Excel = Microsoft.Office.Interop.Excel;

namespace Suportte.Contabilidade
{
    public partial class Endividamento : Form
    {
        public Endividamento()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                this.BackColor = Publicas._fundo;
                referenciaMaskedEditBox.BackColor = empresaComboBoxAdv.BackColor;
                referenciaMaskedEditBox.ForeColor = empresaComboBoxAdv.ForeColor;
                ModalidadeComboBox.BackColor = empresaComboBoxAdv.BackColor;
                ModalidadeComboBox.ForeColor = empresaComboBoxAdv.ForeColor;
                TipoComboBox.BackColor = empresaComboBoxAdv.BackColor;
                TipoComboBox.ForeColor = empresaComboBoxAdv.ForeColor;

                PrevistoCurrency.DecimalValue = 0;
                PrevistoCurrency.Tag = null;
                PrevistoCurrency.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                PrevistoCurrency.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                PrevistoCurrency.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                PrevistoCurrency.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                PrevistoCurrency.BackGroundColor = Publicas._fundo;

                RealizadoCurrency.DecimalValue = 0;
                RealizadoCurrency.Tag = null;
                RealizadoCurrency.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                RealizadoCurrency.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                RealizadoCurrency.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                RealizadoCurrency.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                RealizadoCurrency.BackGroundColor = Publicas._fundo;

                JurosCurrency.DecimalValue = 0;
                JurosCurrency.Tag = null;
                JurosCurrency.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                JurosCurrency.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                JurosCurrency.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                JurosCurrency.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                JurosCurrency.BackGroundColor = Publicas._fundo;

                //ProjecaoCPGCurrencyText.DecimalValue = 0;
                //ProjecaoCPGCurrencyText.Tag = null;
                //ProjecaoCPGCurrencyText.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                //ProjecaoCPGCurrencyText.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                //ProjecaoCPGCurrencyText.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                //ProjecaoCPGCurrencyText.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                //ProjecaoCPGCurrencyText.BackGroundColor = Publicas._fundo;

                //ProjecaoCTBCurrencyText.DecimalValue = 0;
                //ProjecaoCTBCurrencyText.Tag = null;
                //ProjecaoCTBCurrencyText.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                //ProjecaoCTBCurrencyText.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                //ProjecaoCTBCurrencyText.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                //ProjecaoCTBCurrencyText.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                //ProjecaoCTBCurrencyText.BackGroundColor = Publicas._fundo;

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

                    consolidarCheckBox.ForeColor = Publicas._fonte;
                    EncerradoCheckBox.ForeColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;

        Classes.Empresa _empresa;
        Classes.Empresa _empresaImportacao;
        Classes.FornecedoresGlobus _fornecedor;
        List<Classes.Endividamento.Parametros> _listaParametros;
        List<Classes.Endividamento.Parametros> _listaParametrosTipo;
        List<Classes.Endividamento.Valores> _valoresConciliacao;
        List<Classes.Endividamento.Valores> _valoresEmpresas;
        List<Classes.Endividamento.Valores> _valoresCPG;
        List<Classes.Endividamento.Valores> _valoresCPGAtual;
        List<Classes.Endividamento.Valores> _valoresCPGImportacao;
        List<Classes.Endividamento.Valores> _valoresCPGLog;
        List<Classes.Endividamento.Valores> _valoresCPGCancelados;
        List<Classes.Endividamento.Conciliado> _listaConciliados;
        List<Classes.Endividamento.Valores> _valoresEmpresasAntecipados;

        Classes.Endividamento.Valores _valores;
        Classes.Endividamento.Valores _valoresCancelados;

        Classes.Endividamento.Valores _valoresEdicao;

        GridCurrentCell _colunaCorrente;

        bool saiuDaModalidade = false;
        bool saiuDoTipo = false;
        bool temAlteracao = false;

        DateTime _dataInicio;
        DateTime _dataFim;
        string _referencia;

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

        private void Endividamento_Shown(object sender, EventArgs e)
        {
            this.Top = 60;

            #region coloca os botões centralizados
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { gravarButton, limparButton, excluirButton, GerarExecelButton, ImportarJurosButton, EncerraCancelaConciliacaoButton };
            _botoes = Publicas.CentralizarBotoes(_botoes, this.Width, limparButton.Left - (gravarButton.Left + gravarButton.Width));

            for (int i = 0; i < _botoes.Count(); i++)
            {
                if (i == 0)
                    gravarButton.Left = _botoes[i].Left;
                if (i == 1)
                    limparButton.Left = _botoes[i].Left;
                if (i == 2)
                    excluirButton.Left = _botoes[i].Left;
                if (i == 3)
                    GerarExecelButton.Left = _botoes[i].Left;
                if (i == 4)
                    ImportarJurosButton.Left = _botoes[i].Left;
                if (i == 5)
                    EncerraCancelaConciliacaoButton.Left = _botoes[i].Left;
            }
            #endregion

            ModalidadeComboBox.Items.AddRange(new object[] { "Capital de Giro", "Cessão de Títulos", "Financiamento", "Refrota", "Guarupas", "Parcelamento", "Pert", "PMG" });
            ModalidadeComboBox.SelectedIndex = -1;

            TipoComboBox.Items.AddRange(new object[] { "Bacen", "P + Z" });
            TipoComboBox.SelectedIndex = -1;

            _listaEmpresas = new EmpresaBO().Listar(false);

            _empresaDoColaborador = new ColaboradoresBO().Listar(Publicas._idColaborador);

            if (Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.IdEmpresa == 19)
                empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            else
            {
                _listaEmpresasAutorizadas = new List<Empresa>();
                foreach (var item in _empresaDoColaborador.Where(w => w.Inicio != DateTime.MinValue.Date &&
                                                                      (w.Fim == DateTime.MinValue.Date || w.Fim <= DateTime.Now.Date)))
                {
                    _listaEmpresasAutorizadas.AddRange(_listaEmpresas.Where(w => w.IdEmpresa == item.IdEmpresa));
                }

                empresaComboBoxAdv.DataSource = _listaEmpresasAutorizadas.OrderBy(o => o.CodigoeNome).ToList();
            }

            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(gridGroupingControl1);
            filter.WireGrid(gridGroupingControl2);
            filter.WireGrid(gridGroupingControl3);
            filter.WireGrid(gridGroupingControl4);
            filter.WireGrid(gridGroupingControl5);

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

            #region Grid 1 - Valores do Fornecedor, modalidade e Tipo na referencia
            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Parametros>();

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl1.TableControl.CellToolTip.Active = true;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl1.RecordNavigationBar.Label = "";

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl1.TableDescriptor.Columns[i].AllowSort = true;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }
            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            #region  Totalizador do Grid 1 - Valores
            GridSummaryRowDescriptor _soma;

            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor.DataMember = "Previsto";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "Previsto";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor1 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor1.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor1.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor1.DataMember = "Realizado";
            summaryColumnDescriptor1.Format = "{Sum}";
            summaryColumnDescriptor1.Name = "Realizado";
            summaryColumnDescriptor1.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor2.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor2.DataMember = "Variacao";
            summaryColumnDescriptor2.Format = "{Sum}";
            summaryColumnDescriptor2.Name = "Variacao";
            summaryColumnDescriptor2.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor3 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor3.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor3.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor3.DataMember = "Juros";
            summaryColumnDescriptor3.Format = "{Sum}";
            summaryColumnDescriptor3.Name = "Juros";
            summaryColumnDescriptor3.SummaryType = SummaryType.DoubleAggregate;


            GridSummaryColumnDescriptor summaryColumnDescriptor4 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor4.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor4.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor4.DataMember = "RealizadoAtual";
            summaryColumnDescriptor4.Format = "{Sum}";
            summaryColumnDescriptor4.Name = "RealizadoAtual";
            summaryColumnDescriptor4.SummaryType = SummaryType.DoubleAggregate;

            
            _soma = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1,
                       summaryColumnDescriptor2, summaryColumnDescriptor3, summaryColumnDescriptor4 });

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
            #endregion
            #endregion

            #region Grid 2 - Conciliação da empresa
            gridGroupingControl2.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl2.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl2.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl2.TableControl.CellToolTip.Active = true;
            gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl2.RecordNavigationBar.Label = "";

            for (int i = 0; i < gridGroupingControl2.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl2.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl2.TableDescriptor.Columns[i].AllowSort = true;
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

            // para permitir editar dados.
            this.gridGroupingControl2.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl2.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            #region Totalizador do Grid 2 - Conciliação 
            GridSummaryRowDescriptor _soma2;

            GridSummaryColumnDescriptor summaryColumnDescriptorG2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG2.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG2.Appearance.GroupCaptionSummaryCell.Format = "n2";
            //summaryColumnDescriptorG2.Appearance.AnySummaryCell.Font.
            summaryColumnDescriptorG2.DataMember = "Previsto";
            summaryColumnDescriptorG2.Format = "{Sum}";
            summaryColumnDescriptorG2.Name = "Previsto";
            summaryColumnDescriptorG2.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG21 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG21.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG21.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG21.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG21.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG21.DataMember = "Realizado";
            summaryColumnDescriptorG21.Format = "{Sum}";
            summaryColumnDescriptorG21.Name = "Realizado";
            summaryColumnDescriptorG21.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG22 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG22.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG22.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG22.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG22.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG22.DataMember = "Variacao";
            summaryColumnDescriptorG22.Format = "{Sum}";
            summaryColumnDescriptorG22.Name = "Variacao";
            summaryColumnDescriptorG22.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG23 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG23.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG23.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG23.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG23.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG23.DataMember = "Juros";
            summaryColumnDescriptorG23.Format = "{Sum}";
            summaryColumnDescriptorG23.Name = "Juros";
            summaryColumnDescriptorG23.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG24 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG24.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG24.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG24.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG24.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG24.DataMember = "CPGCurto";
            summaryColumnDescriptorG24.Format = "{Sum}";
            summaryColumnDescriptorG24.Name = "CPGCurto";
            summaryColumnDescriptorG24.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG25 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG25.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG25.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG25.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG25.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG25.DataMember = "CTBCurto";
            summaryColumnDescriptorG25.Format = "{Sum}";
            summaryColumnDescriptorG25.Name = "CTBCurto";
            summaryColumnDescriptorG25.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG26 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG26.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG26.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG26.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG26.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG26.DataMember = "VariacaoCurto";
            summaryColumnDescriptorG26.Format = "{Sum}";
            summaryColumnDescriptorG26.Name = "VariacaoCurto";
            summaryColumnDescriptorG26.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG27 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG27.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG27.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG27.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG27.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG27.DataMember = "CPGLongo";
            summaryColumnDescriptorG27.Format = "{Sum}";
            summaryColumnDescriptorG27.Name = "CPGLongo";
            summaryColumnDescriptorG27.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG28 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG28.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG28.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG28.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG28.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG28.DataMember = "CTBLongo";
            summaryColumnDescriptorG28.Format = "{Sum}";
            summaryColumnDescriptorG28.Name = "CTBLongo";
            summaryColumnDescriptorG28.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG29 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG29.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG29.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG29.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG29.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG29.DataMember = "VariacaoLongo";
            summaryColumnDescriptorG29.Format = "{Sum}";
            summaryColumnDescriptorG29.Name = "VariacaoLongo";
            summaryColumnDescriptorG29.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG210 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG210.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG210.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG210.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG210.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG210.DataMember = "CPGJurosCurto";
            summaryColumnDescriptorG210.Format = "{Sum}";
            summaryColumnDescriptorG210.Name = "CPGJurosCurto";
            summaryColumnDescriptorG210.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG211 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG211.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG211.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG211.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG211.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG211.DataMember = "CTBJurosCurto";
            summaryColumnDescriptorG211.Format = "{Sum}";
            summaryColumnDescriptorG211.Name = "CTBJurosCurto";
            summaryColumnDescriptorG211.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG212 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG212.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG212.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG212.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG212.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG212.DataMember = "VariacaoJurosCurto";
            summaryColumnDescriptorG212.Format = "{Sum}";
            summaryColumnDescriptorG212.Name = "VariacaoJurosCurto";
            summaryColumnDescriptorG212.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG213 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG213.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG213.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG213.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG213.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG213.DataMember = "CPGJurosLongo";
            summaryColumnDescriptorG213.Format = "{Sum}";
            summaryColumnDescriptorG213.Name = "CPGJurosLongo";
            summaryColumnDescriptorG213.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG214 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG214.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG214.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG214.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG214.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG214.DataMember = "CTBJurosLongo";
            summaryColumnDescriptorG214.Format = "{Sum}";
            summaryColumnDescriptorG214.Name = "CTBJurosLongo";
            summaryColumnDescriptorG214.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG215 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG215.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG215.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG215.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG215.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG215.DataMember = "VariacaoJurosLongo";
            summaryColumnDescriptorG215.Format = "{Sum}";
            summaryColumnDescriptorG215.Name = "VariacaoJurosLongo";
            summaryColumnDescriptorG215.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG216 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG216.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG216.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG216.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG216.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG216.DataMember = "CPGPrevisto";
            summaryColumnDescriptorG216.Format = "{Sum}";
            summaryColumnDescriptorG216.Name = "CPGPrevisto";
            summaryColumnDescriptorG216.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG217 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG217.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG217.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG217.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG217.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG217.DataMember = "CPGJuros";
            summaryColumnDescriptorG217.Format = "{Sum}";
            summaryColumnDescriptorG217.Name = "CPGJuros";
            summaryColumnDescriptorG217.SummaryType = SummaryType.DoubleAggregate;

            _soma2 = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptorG2, summaryColumnDescriptorG21, summaryColumnDescriptorG22, summaryColumnDescriptorG23
                       , summaryColumnDescriptorG24, summaryColumnDescriptorG25, summaryColumnDescriptorG26, summaryColumnDescriptorG27
                       , summaryColumnDescriptorG28, summaryColumnDescriptorG29, summaryColumnDescriptorG210, summaryColumnDescriptorG211
                       , summaryColumnDescriptorG212, summaryColumnDescriptorG213, summaryColumnDescriptorG214, summaryColumnDescriptorG215
                       , summaryColumnDescriptorG216, summaryColumnDescriptorG217
                   });

            _soma2.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma2.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma2.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma2.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma2.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            try
            {
                gridGroupingControl2.TableDescriptor.SummaryRows.Add(_soma2);
            }
            catch { }

            gridGroupingControl2.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl2.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl2.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl2.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
            #endregion
            #endregion

            #region Grid 3 - Valores da Empresa na referencia
            gridGroupingControl3.DataSource = new List<Classes.Endividamento.Parametros>();

            gridGroupingControl3.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl3.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl3.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl3.TableControl.CellToolTip.Active = true;
            gridGroupingControl3.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl3.RecordNavigationBar.Label = "";

            for (int i = 0; i < gridGroupingControl3.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl3.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl3.TableDescriptor.Columns[i].AllowSort = true;
                gridGroupingControl3.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl3.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl3.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl3.SetMetroStyle(metroColor);
                this.gridGroupingControl3.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl3.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }
            this.gridGroupingControl3.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl3.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            #region  Totalizador do Grid 3 - Valores
            // usa as mesmas colunas do grid 3 por serem iguais
            GridSummaryRowDescriptor _soma3;

            _soma3 = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1, summaryColumnDescriptor2, summaryColumnDescriptor3 });

            _soma3.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma3.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma3.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma3.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma3.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            try
            {
                gridGroupingControl3.TableDescriptor.SummaryRows.Add(_soma3);
            }
            catch { }

            gridGroupingControl3.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl3.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl3.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl3.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl3.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl3.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl3.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
            #endregion

            #endregion

            #region Grid 4 - Cancelados
            gridGroupingControl4.DataSource = new List<Classes.Endividamento.Valores>();

            gridGroupingControl4.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl4.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl4.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl4.TableControl.CellToolTip.Active = true;
            gridGroupingControl4.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl4.RecordNavigationBar.Label = "";
                               
            for (int i = 0; i < gridGroupingControl4.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl4.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl4.TableDescriptor.Columns[i].AllowSort = true;
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
            this.gridGroupingControl4.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl4.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            #region  Totalizador do Grid 4 - contratos cancelados
            GridSummaryRowDescriptor _soma4;

            GridSummaryColumnDescriptor summaryColumnDescriptorG4 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG4.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG4.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG4.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptorG4.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptorG4.DataMember = "Previsto";
            summaryColumnDescriptorG4.Format = "{Sum}";
            summaryColumnDescriptorG4.Name = "Previsto";
            summaryColumnDescriptorG4.SummaryType = SummaryType.DoubleAggregate;
            
            GridSummaryColumnDescriptor summaryColumnDescriptorG41 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG41.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG41.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG41.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptorG41.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptorG41.DataMember = "Realizado";
            summaryColumnDescriptorG41.Format = "{Sum}";
            summaryColumnDescriptorG41.Name = "Realizado";
            summaryColumnDescriptorG41.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG42 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG42.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG42.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG42.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptorG42.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptorG42.DataMember = "Variacao";
            summaryColumnDescriptorG42.Format = "{Sum}";
            summaryColumnDescriptorG42.Name = "Variacao";
            summaryColumnDescriptorG42.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG43 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG43.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG43.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG43.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptorG43.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptorG43.DataMember = "Juros";
            summaryColumnDescriptorG43.Format = "{Sum}";
            summaryColumnDescriptorG43.Name = "Juros";
            summaryColumnDescriptorG43.SummaryType = SummaryType.DoubleAggregate;


            GridSummaryColumnDescriptor summaryColumnDescriptorG44 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG44.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG44.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG44.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptorG44.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptorG44.DataMember = "RealizadoAtual";
            summaryColumnDescriptorG44.Format = "{Sum}";
            summaryColumnDescriptorG44.Name = "RealizadoAtual";
            summaryColumnDescriptorG44.SummaryType = SummaryType.DoubleAggregate;


            _soma4 = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptorG4, summaryColumnDescriptorG41,
                       summaryColumnDescriptorG42, summaryColumnDescriptorG43, summaryColumnDescriptorG44 });

            _soma4.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma4.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma4.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma4.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma4.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            try
            {
                gridGroupingControl4.TableDescriptor.SummaryRows.Add(_soma4);
            }
            catch { }

            gridGroupingControl4.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl4.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl4.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl4.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl4.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl4.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl4.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
            #endregion
            #endregion

            #region Grid 5 - Antecipados
            gridGroupingControl5.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl5.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl5.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl5.TableControl.CellToolTip.Active = true;
            gridGroupingControl5.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl5.RecordNavigationBar.Label = "";

            for (int i = 0; i < gridGroupingControl5.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl5.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl5.TableDescriptor.Columns[i].AllowSort = true;
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

            // para permitir editar dados.
            this.gridGroupingControl5.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl5.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            
            #endregion

        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ImportarJurosButton.Enabled = true;
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void FornecedorTextBox_Enter(object sender, EventArgs e)
        {
            FornecedorTextBox.BorderColor = Publicas._bordaEntrada;
            pesquisaFornecedorButton.Enabled = string.IsNullOrEmpty(FornecedorTextBox.Text.Trim());
        }

        private void PrevistoCurrency_Enter(object sender, EventArgs e)
        {
            PrevistoCurrency.BorderColor = Publicas._bordaEntrada;
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

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                consolidarCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void FornecedorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ModalidadeComboBox.Focus();
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void ModalidadeComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                TipoComboBox.Focus();
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void TipoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            // desta forma pois nao está navegando corretamente
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                referenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ModalidadeComboBox.Focus();
            }
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                TipoComboBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FornecedorTextBox.Focus();
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

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

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
                new Notificacoes.Mensagem("Selecione a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            mensagemSistemaLabel.Text = "Pesquisando Parâmetros, aguarde...";
            Refresh();

            _listaParametros = new EndividamentoBO().Listar(_empresa.IdEmpresa, "F");
            _listaParametrosTipo = new EndividamentoBO().Listar(_empresa.IdEmpresa, "T");

            mensagemSistemaLabel.Text = "";
            Refresh();

        }

        private void FornecedorTextBox_Validating(object sender, CancelEventArgs e)
        {
            FornecedorTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            if (string.IsNullOrEmpty(FornecedorTextBox.Text.Trim()))
            {
                new Pesquisas.Fornecedores().ShowDialog();

                FornecedorTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(FornecedorTextBox.Text) || FornecedorTextBox.Text == "0")
                {
                    FornecedorTextBox.Text = string.Empty;
                    FornecedorTextBox.Focus();
                    return;
                }
            }

            try
            {
                FornecedorTextBox.Text = Convert.ToDecimal(FornecedorTextBox.Text).ToString("000000");
            }
            catch { }

            _fornecedor = new FornecedoresGlobusBO().Consultar(FornecedorTextBox.Text);

            if (!_fornecedor.Existe)
            {
                new Notificacoes.Mensagem("Fornecedor não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                FornecedorTextBox.Focus();
                return;
            }

            if (!_fornecedor.Ativo)
            {
                new Notificacoes.Mensagem("Fornecedor não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                FornecedorTextBox.Focus();
                return;
            }

            NomeFornecedorTextBox.Text = _fornecedor.NomeFantasia;

            ImportarJurosButton.Enabled = false;

            if (_listaParametros.Where(w => w.CodigoFornecedor == _fornecedor.CodigoFornecedor).Count() == 0)
            {
                new Notificacoes.Mensagem("Fornecedor não parametrizado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                FornecedorTextBox.Focus();
                return;
            }
        }

        private void ModalidadeComboBox_Validating(object sender, CancelEventArgs e)
        {
            ModalidadeComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            saiuDaModalidade = true;

            if (_listaParametros.Where(w => w.CodigoFornecedor == _fornecedor.CodigoFornecedor &&
                                            w.Modalidade == ModalidadeComboBox.Text).Count() == 0)
            {
                new Notificacoes.Mensagem("Modalidade não parametrizada para este fornecedor.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ModalidadeComboBox.Focus();
                return;
            }
        }

        private void TipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            TipoComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (saiuDaModalidade)
            {
                TipoComboBox.Focus();
                saiuDaModalidade = false;
                return;
            }

            saiuDaModalidade = false;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            saiuDoTipo = true;
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;
            referenciaMaskedEditBox.ThemeStyle.BorderColor = Publicas._bordaSaida;

            if (saiuDoTipo)
            {
                referenciaMaskedEditBox.Focus();
                saiuDoTipo = false;
                return;
            }

            saiuDoTipo = false;

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
            
            mensagemSistemaLabel.Text = "Aguarde, Pesquisando ...";  
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl2.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl3.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl4.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl5.DataSource = new List<Classes.Endividamento.Valores>();

            _referencia = referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2);
            _valores = new EndividamentoBO().Consultar(_empresa.IdEmpresa, _fornecedor.CodigoFornecedor, ModalidadeComboBox.Text, TipoComboBox.Text, _referencia);

            _valoresEmpresas = new EndividamentoBO().ListarValoresDaEmpresa(_empresa.IdEmpresa, _referencia);
            _valoresEmpresasAntecipados = new List<Classes.Endividamento.Valores>();

            foreach (var item in _valoresEmpresas)
            {
                if (item.Pagamento < _dataInicio && item.Pagamento != DateTime.MinValue)
                    _valoresEmpresasAntecipados.Add(item);
            }

            _valoresCPGLog = new List<Classes.Endividamento.Valores>();
            _valoresCPG = new List<Classes.Endividamento.Valores>();
            _valoresCPGAtual = new List<Classes.Endividamento.Valores>();
            _valoresCPGCancelados = new EndividamentoBO().ListarValoresFuturosCancelados(_empresa.IdEmpresa, _referencia);

            tabPageAdv4.TabVisible = _valoresCPGCancelados.Count() != 0;

            NovoLabel.ForeColor = (Publicas._TemaBlack ? Color.OrangeRed : Color.Maroon);

            if (!_valores.Existe)
            {
                NovoLabel.Visible = true;
                NovoLabel.Text = "Ainda não gravado";
                _valoresConciliacao = new EndividamentoBO().Consultar(_empresa.IdEmpresa, TipoComboBox.Text, _referencia, _empresa, _dataFim, consolidarCheckBox.Checked);

                if (TipoComboBox.SelectedIndex == 1) // P+Z
                    _valoresCPG = new EndividamentoBO().BuscarDocumentoContasPagar(_empresa, _fornecedor.CodigoFornecedor, ModalidadeComboBox.Text, _dataInicio, _dataFim);

                EncerraCancelaConciliacaoButton.Enabled = false;
            }
            else
            {
                _valoresCPG = new EndividamentoBO().ListarValores(_valores.Id);
                _valoresCPGLog = new EndividamentoBO().ListarValores(_valores.Id);
                EncerradoCheckBox.Checked = _valores.Encerrado;

                if (TipoComboBox.SelectedIndex == 1) // P+Z
                    _valoresCPGAtual = new EndividamentoBO().BuscarDocumentoContasPagar(_empresa, _fornecedor.CodigoFornecedor, ModalidadeComboBox.Text, _dataInicio, _dataFim);

                _listaConciliados = new EndividamentoBO().ListarConciliacao(_empresa.IdEmpresa, _referencia);

                if (_valores.Encerrado)
                {
                    NovoLabel.Text = "Conciliação Encerrada";
                    NovoLabel.Visible = true;
                    _valoresConciliacao = new EndividamentoBO().ListarValorConciliado(_empresa.IdEmpresa, _referencia);
                    EncerraCancelaConciliacaoButton.Text = "Cancelar &Conciliação";
                }
                else
                {
                    NovoLabel.Visible = false;
                    _valoresConciliacao = new EndividamentoBO().Consultar(_empresa.IdEmpresa, TipoComboBox.Text, _referencia, _empresa, _dataFim, consolidarCheckBox.Checked);
                    EncerraCancelaConciliacaoButton.Text = "Encerrar &Conciliação";
                }
                EncerraCancelaConciliacaoButton.Enabled = true;
            }
            
            foreach (var item in _valoresEmpresasAntecipados)
            {
                _valoresEmpresas.Remove(item);
            }

            List<Classes.Endividamento.Valores> _excluir = new List<Classes.Endividamento.Valores>();
            foreach (var item in _valoresCPG)
            {
                if (item.Pagamento < _dataInicio && item.Pagamento != DateTime.MinValue)
                    _excluir.Add(item);
            }
            
            foreach (var item in _excluir)
            {
                _valoresCPG.Remove(item);
                _valoresCPGLog.Remove(item);
                _valoresCPGAtual.Remove(item);
            }

            gridGroupingControl1.DataSource = _valoresCPG;
            gridGroupingControl2.DataSource = _valoresConciliacao;
            gridGroupingControl3.DataSource = _valoresEmpresas;
            gridGroupingControl4.DataSource = _valoresCPGCancelados;
            gridGroupingControl5.DataSource = _valoresEmpresasAntecipados;

            gravarButton.Enabled = true;
            excluirButton.Enabled = _valores.Existe && !_valores.Encerrado;

            if (_valoresCPG.Count == 0)
                incluirToolStripMenuItem_Click(sender, new EventArgs());

            Classes.Endividamento.Parametros _parametro = new Classes.Endividamento.Parametros();

            foreach (var item in _listaParametros.Where(w => w.CodigoFornecedor == _fornecedor.CodigoFornecedor &&
                                                             w.Modalidade == ModalidadeComboBox.Text))
            {
                _parametro = item;
            }

            DateTime _refAux = _dataInicio.AddMonths(12);

            for (int i = 0; i < gridGroupingControl2.TableDescriptor.StackedHeaderRows[0].Headers.Count; i++)
            {
                if (gridGroupingControl2.TableDescriptor.StackedHeaderRows[0].Headers[i].HeaderText.Contains("Conciliação"))
                    gridGroupingControl2.TableDescriptor.StackedHeaderRows[0].Headers[i].HeaderText = "Conciliação"
                           + " " + _refAux.Month.ToString("00") + "/" + _refAux.Year.ToString();
            }
            
            ImportarJurosButton.Enabled = false;

            ToolTipInfo _tool = new ToolTipInfo();
            _tool.Footer.Text = "Contas contábeis" + Environment.NewLine 
                + "Curtos prazo: " + _parametro.CodigoContaCurtoPrevisto.ToString() 
                  + Environment.NewLine
                  + " Longo Prazo: " + _parametro.CodigoContaLongoPrevisto.ToString();
            superToolTip1.SetToolTip(label8, _tool);
            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();
        }

        private void FornecedorTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaFornecedorButton.Enabled = FornecedorTextBox.Text.Trim() == "";
        }

        private void referenciaMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            Publicas._escTeclado = false;
            saiuDoTipo = false;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            if (temAlteracao)
            {
                if (new Notificacoes.Mensagem("Deseja realmente fechar a tela?" + Environment.NewLine +
                    "Existem alterações não gravadas ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                    Close();
            }
            else
                Close();
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            int id = 0;
            decimal cpg = 0;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {

                    try
                    {
                        id = (int)dr["Id"];
                        cpg = (decimal)dr["CodigoInternoCPG"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("Id =") + 4;
                            posFimId = dr.Info.IndexOf(", IdEndividamento");
                            id = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());

                            posIniId = dr.Info.IndexOf("CodigoInternoCPG =") + 18;
                            posFimId = dr.Info.IndexOf(", CodigoFornecedor");
                            cpg = Convert.ToDecimal(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());

                        }
                        catch { }
                    }

                    foreach (var item in _valoresCPG.Where(w => w.Id == id && w.CodigoInternoCPG == cpg))
                    {
                        _valoresEdicao = item;
                        break;
                    }
                }
            }

            PrevistoCurrency.DecimalValue = _valoresEdicao.Previsto;
            RealizadoCurrency.DecimalValue = _valoresEdicao.Realizado;
            JurosCurrency.DecimalValue = Math.Round(_valoresEdicao.Juros,2);
            ContratoTextBox.Text = _valoresEdicao.Contrato;

            EditarPanel.Visible = true;
            PrevistoCurrency.Enabled = true;
            RealizadoCurrency.Enabled = TipoComboBox.SelectedIndex == 0; // só altera se for o Bacen
            JurosCurrency.Enabled = true;
            ContratoTextBox.Enabled = true;
            ContratoTextBox.Focus();
        }

        private void PrevistoCurrency_KeyDown(object sender, KeyEventArgs e)
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

        private void PrevistoCurrency_Validating(object sender, CancelEventArgs e)
        {
            PrevistoCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void RealizadoCurrency_Validating(object sender, CancelEventArgs e)
        {
            RealizadoCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void JurosCurrency_Validating(object sender, CancelEventArgs e)
        {
            JurosCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            EditarPanel.Visible = false;
            PrevistoCurrency.Enabled = false;
            RealizadoCurrency.Enabled = false;
            JurosCurrency.Enabled = false;
            ContratoTextBox.Enabled = false;

            if (_valoresEdicao.Id == 0 && _valoresEdicao.CodigoInternoCPG == 0)
            {
                _valoresEdicao.CodigoFornecedor = _fornecedor.CodigoFornecedor;
                _valoresEdicao.Modalidade = ModalidadeComboBox.Text;
                _valoresEdicao.Tipo = TipoComboBox.Text;
                _valoresEdicao.IdEmpresa = _empresa.IdEmpresa;
                _valoresEdicao.Referencia = _referencia;
                _valoresEdicao.Id = _valoresCPG.Count() + 1;
                _valoresEdicao.IdEndividamento = _valores.Id;

                _valoresEdicao.Previsto = PrevistoCurrency.DecimalValue;
                _valoresEdicao.Realizado = RealizadoCurrency.DecimalValue;
                _valoresEdicao.Juros = Math.Round(JurosCurrency.DecimalValue,2);
                _valoresEdicao.Contrato = ContratoTextBox.Text;

                _valoresCPG.Add(_valoresEdicao);
            }
            else
            {
                if (ModalidadeComboBox.SelectedIndex == 0)
                {
                    foreach (var item in _valoresCPG.Where(w => w.Id == _valoresEdicao.Id))
                    { 
                        item.Previsto = PrevistoCurrency.DecimalValue;
                        item.Realizado = RealizadoCurrency.DecimalValue;
                        item.Juros = Math.Round(JurosCurrency.DecimalValue,2);
                        item.Contrato = ContratoTextBox.Text;
                    }
                }
                else
                {
                    foreach (var item in _valoresCPG.Where(w => w.CodigoInternoCPG == _valoresEdicao.CodigoInternoCPG && w.Contrato == _valoresEdicao.Contrato))
                    {
                        item.Previsto = PrevistoCurrency.DecimalValue;
                        item.Realizado = RealizadoCurrency.DecimalValue;
                        item.Juros = Math.Round(JurosCurrency.DecimalValue,2);
                        item.Contrato = ContratoTextBox.Text;
                    }
                }
            }

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl1.DataSource = _valoresCPG;
            gridGroupingControl1.Focus();

            temAlteracao = true;
            PrevistoCurrency.DecimalValue = 0;
            RealizadoCurrency.DecimalValue = 0;
            JurosCurrency.DecimalValue = 0;
            ContratoTextBox.Text = string.Empty;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {

            if (_valoresCPG.Count() == 0 && _valoresCPGCancelados.Where(w => w.Excluir).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum documento informado para gravar ou selecionado excluir.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            foreach (var item in _valoresCPG)
            {
                if (item.RealizadoAlterado && item.RealizadoAtual != 0)
                    item.Realizado = item.RealizadoAtual;
            }

            _valores.IdEmpresa = _empresa.IdEmpresa;
            _valores.Referencia = _referencia;
            _valores.Modalidade = ModalidadeComboBox.Text;
            _valores.CodigoFornecedor = _fornecedor.CodigoFornecedor;
            _valores.Tipo = TipoComboBox.Text;
            _valores.Previsto = _valoresCPG.Where(w => !w.Excluir).Sum(s => s.Previsto);
            _valores.Realizado = _valoresCPG.Where(w => !w.Excluir).Sum(s => s.Realizado);
            _valores.Juros = Math.Round(_valoresCPG.Where(w => !w.Excluir).Sum(s => s.Juros),2);
            _valores.Variacao = _valoresCPG.Where(w => !w.Excluir).Sum(s => s.Variacao);
            _valores.RealizadoAtual = _valoresCPG.Where(w => !w.Excluir).Sum(s => s.RealizadoAtual);
            _valores.RealizadoAlterado = _valores.RealizadoAtual != 0;
            
            _valores.Encerrado = EncerradoCheckBox.Checked;

            if (!new EndividamentoBO().Gravar(_valores, _valoresCPG))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            foreach (var item in _valoresCPGCancelados.Where(w => w.Excluir).GroupBy(g => g.IdEndividamento))
            {
                _valoresCancelados = new EndividamentoBO().ConsultarPorId(item.Key);

                _valoresCancelados.Previsto = _valoresCPGCancelados.Where(w => !w.Excluir && w.IdEndividamento == item.Key).Sum(s => s.Previsto);
                _valoresCancelados.Realizado = _valoresCPGCancelados.Where(w => !w.Excluir && w.IdEndividamento == item.Key).Sum(s => s.Realizado);
                _valoresCancelados.Juros = Math.Round(_valoresCPGCancelados.Where(w => !w.Excluir && w.IdEndividamento == item.Key).Sum(s => s.Juros), 2);
                _valoresCancelados.Variacao = _valoresCPGCancelados.Where(w => !w.Excluir && w.IdEndividamento == item.Key).Sum(s => s.Variacao);

                if (!new EndividamentoBO().Gravar(_valoresCancelados, _valoresCPGCancelados.Where(w => w.IdEndividamento == item.Key).ToList()))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou os valores de endividamento da empresa " + empresaComboBoxAdv.Text +
                " Fornecedor " + _fornecedor.Numero + " " + NomeFornecedorTextBox.Text +
                " Modalidade " + ModalidadeComboBox.Text +
                " Tipo " + TipoComboBox.Text +
                " Referencia " + referenciaMaskedEditBox.Text +
                " no total de Previsto " + _valores.Previsto.ToString() +
                " no total de Realizado " + _valores.Realizado.ToString() +
                " no total de Juros " + Math.Round(_valores.Juros,2).ToString() +
                " no total de Variacao " + _valores.Variacao.ToString();

            string _excluidos = "";
            foreach (var item in _valoresCPG.Where(w => w.Excluir))
            {
                _excluidos = _excluidos + item.Documento + " " + item.Vencimento.ToShortDateString() + ", ";
            }

            foreach (var item in _valoresCPGCancelados.Where(w => w.Excluir))
            {
                _excluidos = _excluidos + item.Documento + " " + item.Vencimento.ToShortDateString() + ", ";
            }

            if (_excluidos != "")
            {
                _excluidos = _excluidos.Substring(0, _excluidos.Length - 2);
                _log.Descricao = _log.Descricao + " Documento(s) excluido(s) -> " + _excluidos;
            }

            string _descricao = "";

            bool encontrou = false;
            foreach (var item in _valoresCPG.Where(w => !w.Excluir))
            {

                foreach (var itemL in _valoresCPGLog.Where(w => w.Id == item.Id))
                {
                    encontrou = true;

                    if (itemL.Contrato != item.Contrato || itemL.Previsto != item.Previsto || itemL.Realizado != item.Realizado || itemL.Juros != item.Juros)
                    {
                        _descricao = _descricao +
                            " Alterado o documento " + item.Documento +
                            (itemL.Previsto != item.Previsto ? " previsto de " + itemL.Previsto.ToString() + " para " + item.Previsto.ToString() : "") +
                            (itemL.Realizado != item.Realizado ? " realizado de " + itemL.Realizado.ToString() + " para " + item.Realizado.ToString() : "") +
                            (itemL.Juros != item.Juros ? " juros de " + itemL.Juros.ToString() + " para " + item.Juros.ToString() : "") +
                            (itemL.Contrato != item.Contrato ? " contrato de " + itemL.Contrato.ToString() + " para " + item.Contrato.ToString() : "");
                            
                    }
                }

                if (!encontrou)
                    _descricao = _descricao + " Incluido o documento " + item.Documento +
                                    " previsto " + item.Previsto.ToString() +
                                    " realizado " + item.Realizado.ToString() +
                                    " juros " + item.Juros.ToString() +
                                    " contrato " + item.Contrato +
                                    " vencimento " + item.Vencimento.ToShortDateString();

            }

            _log.Tela = "Contabilidade - Endividamento - Valores";
            _log.Descricao = _log.Descricao + _descricao;

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void ContratoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PrevistoCurrency.Focus();
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new EndividamentoBO().Excluir(_valores.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu os valores de endividamento da empresa " + empresaComboBoxAdv.Text +
                " Fornecedor " + _fornecedor.Numero + " " + NomeFornecedorTextBox.Text +
                " Modalidade " + ModalidadeComboBox.Text +
                " Tipo " + TipoComboBox.Text +
                " Referencia " + referenciaMaskedEditBox.Text +
                " no total de Previsto " + _valores.Previsto.ToString() +
                " no total de Realizado " + _valores.Realizado.ToString() +
                " no total de Juros " + Math.Round(_valores.Juros,2).ToString() +
                " no total de Variacao " + _valores.Variacao.ToString();

            string _descricao = "";

            
            foreach (var item in _valoresCPG)
            {
                _descricao = _descricao + " Excluido o documento " + item.Documento + 
                    " previsto " + item.Previsto.ToString() + 
                    " realizado " + item.Realizado.ToString() + 
                    " juros " + Math.Round(item.Juros,2).ToString() +
                    " contrato " + item.Contrato +
                    " vencimento " + item.Vencimento.ToShortDateString();
            }

            _log.Tela = "Contabilidade - Endividamento - Valores";
            _log.Descricao = _log.Descricao + _descricao;

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            temAlteracao = false;
            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl2.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl3.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl4.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl5.DataSource = new List<Classes.Endividamento.Valores>();

            NovoLabel.Visible = false;
            FornecedorTextBox.Focus();

            PrevistoCurrency.DecimalValue = 0;
            RealizadoCurrency.DecimalValue = 0;
            JurosCurrency.DecimalValue = 0;
            ContratoTextBox.Text = string.Empty;

            PrevistoCurrency.Enabled = false;
            RealizadoCurrency.Enabled = false;
            JurosCurrency.Enabled = false;
            ContratoTextBox.Enabled = false;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            EncerraCancelaConciliacaoButton.Enabled = false;
        }

        private void incluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditarPanel.Visible = true;
            _valoresEdicao = new Classes.Endividamento.Valores();
            PrevistoCurrency.Enabled = true;
            RealizadoCurrency.Enabled = true;
            JurosCurrency.Enabled = true;
            ContratoTextBox.Enabled = true;
            ContratoTextBox.Focus();
        }

        private void TipoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //incluirToolStripMenuItem.Enabled = TipoComboBox.SelectedIndex == 0; 

            GerarExecelButton.Enabled = TipoComboBox.SelectedIndex != 0;

            if (TipoComboBox.SelectedIndex == 0)
            {
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Vencimento");
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Pagamentos");
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("CodigoTipoDocumento");
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Documento");
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Previsto");
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Juros");
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Variacao");
            }
            else
            {
                try
                {
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("Previsto");
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("Variacao");
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("Juros");
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("Vencimento");
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("Pagamentos");
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("CodigoTipoDocumento");
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("Documento");
                }
                catch { }
            }
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            mensagemSistemaLabel.Text = "Exportando dados para o Excel, aguarde...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            if (!System.IO.Directory.Exists(Publicas._caminhoAnexosRateioCTB))
                System.IO.Directory.CreateDirectory(Publicas._caminhoAnexosRateioCTB);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;

            string nomeArquivo = "EndividamentoCTB_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" 
                               + FornecedorTextBox.Text
                               + "_" 
                               + ModalidadeComboBox.Text
                               + "_"
                               + _referencia.ToString();

            xlApp = new Excel.Application();

            decimal juros = 0;
            decimal variacao = 0;

            try
            {

                xlApp.DisplayAlerts = false;

                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int linha = 1;
                int col = 1;

                
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

                foreach (var itemC in _valoresConciliacao)
                {
                    col = 1;
                    linha++;
                    Classes.Endividamento.Parametros _parametro = new Classes.Endividamento.Parametros();

                    foreach (var item in _listaParametros.Where(w => w.CodigoFornecedor == itemC.CodigoFornecedor &&
                                                                     w.Modalidade == itemC.Modalidade))
                    {
                        _parametro = item;
                    }

                    #region Juros
                    juros = itemC.Juros;

                    if (_parametro.CodigoContaJurosDebito != 0)
                    {
                        xlWorkSheet.Cells[linha, col] = _parametro.CodigoContaJurosDebito;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CodigoContaJurosCredito;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CustoJuros;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CustoJuros;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.HistoricoJuros;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.HistoricoJuros;
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", juros);
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", juros);
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.Lote;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _dataFim;
                        col++;
                        xlWorkSheet.Cells[linha, col] = "1";
                    }
                    #endregion

                    #region Variacao
                    variacao = itemC.Variacao;

                    if (_parametro.CodigoContaVariacaoCredito != 0)
                    {
                        linha++;
                        col = 1;
                        if (variacao <= 0)
                        {
                            xlWorkSheet.Cells[linha, col] = _parametro.CodigoContaVariacaoCredito;
                            col++;
                            xlWorkSheet.Cells[linha, col] = _parametro.CodigoContaVariacaoDebito;
                        }
                        else
                        {
                            xlWorkSheet.Cells[linha, col] = _parametro.CodigoContaVariacaoDebito;
                            col++;
                            xlWorkSheet.Cells[linha, col] = _parametro.CodigoContaVariacaoCredito;
                        }
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CustoVariacao;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CustoVariacao;
                        col++;

                        xlWorkSheet.Cells[linha, col] = _parametro.HistoricoVariacao;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.HistoricoVariacao;
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", variacao);
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", variacao);
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.Lote;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _dataFim;
                        col++;
                        xlWorkSheet.Cells[linha, col] = "1";
                    }
                    #endregion

                    #region Previsto Conciliação

                    if (_parametro.CodigoContaLongoPrevisto != 0)
                    {
                        linha++;
                        col = 1;
                        xlWorkSheet.Cells[linha, col] = _parametro.CodigoContaLongoPrevisto;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CodigoContaCurtoPrevisto;

                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CustoPrevsto;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CustoPrevsto;
                        col++;

                        xlWorkSheet.Cells[linha, col] = _parametro.HistoricoPrevisto;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.HistoricoPrevisto;
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemC.CPGPrevisto);
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemC.CPGPrevisto);
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.Lote;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _dataFim;
                        col++;
                        xlWorkSheet.Cells[linha, col] = "1";

                    }
                    #endregion

                    #region Juros Conciliação

                    if (_parametro.CodigoContaCurtoPrazo != 0)
                    {
                        linha++;
                        col = 1;
                        xlWorkSheet.Cells[linha, col] = _parametro.CodigoContaCurtoPrazo;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CodigoContaLongoPrazo;

                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CustoJurosConciliacao;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.CustoJurosConciliacao;
                        col++;

                        xlWorkSheet.Cells[linha, col] = _parametro.HistoricoJurosConciliacao;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.HistoricoJurosConciliacao;
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemC.CPGJuros);
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemC.CPGJuros);
                        col++;
                        xlWorkSheet.Cells[linha, col] = _parametro.Lote;
                        col++;
                        xlWorkSheet.Cells[linha, col] = _dataFim;
                        col++;
                        xlWorkSheet.Cells[linha, col] = "1";
                    }
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
                new Notificacoes.Mensagem("Arquivo gerado com sucesso." + Environment.NewLine +
                    "Salvo na pasta " + Publicas._caminhoAnexosRateioCTB, Publicas.TipoMensagem.Sucesso).ShowDialog();
                this.Refresh();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                mensagemSistemaLabel.Text = "";
                new Notificacoes.Mensagem("Não foi possível gerar o arquivo." + Environment.NewLine +
                    "Tente em outra maquina." + Environment.NewLine + ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
            }
        }

        private void ImportarJurosButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                new Notificacoes.Mensagem("Nenhum arquivo selecionado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            if (DateTime.Now.Date < Convert.ToDateTime("30/11/2019"))
            {
                if (new Notificacoes.Mensagem("Verifique se existe a coluna Série no arquivo." + Environment.NewLine +
                                              "A coluna série é necessária para distinguir os documentos." + Environment.NewLine +
                                              "Ela deve estar após a coluna 'Parcela' e antes da coluna 'Tipo de Documento'" + Environment.NewLine +
                                              "Continuar a Importação ?"
                                    , Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    return;
            }

            mensagemSistemaLabel.Text = "Importando arquivo, aguarde...";
            Refresh();

            string[] arquivos = openFileDialog1.FileNames;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            Excel.Range range = null;

            string str = "";
            int rCnt;
            int cCnt;
            int rw = 0;
            int cl = 0;
            string docto = "";
            int parcela = 0;
            string serie = "";
            string tpDocto = "";
            int qtdLinhas = 0;

            List<Classes.Endividamento.Valores> _listaImporta = new List<Classes.Endividamento.Valores>();
            List<Classes.Endividamento.Valores> _itensImporta = new List<Classes.Endividamento.Valores>();

            foreach (var itemA in arquivos)
            {
                xlApp = new Excel.Application();

                try
                {
                    xlApp.DisplayAlerts = false;
                    xlWorkBook = xlApp.Workbooks.Open(itemA , 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 1);

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    range = xlWorkSheet.UsedRange;
                    rw = range.Rows.Count;
                    cl = range.Columns.Count;
                    // linha um é o cabeçalho
                    str = (Convert.ToString((range.Cells[1, 1] as Excel.Range).Value2));
                }
                catch
                {
                    return;
                }

                Classes.Endividamento.Valores _val = new Classes.Endividamento.Valores();

                for (rCnt = 2; rCnt <= rw; rCnt++)
                {
                    _valoresEdicao = new Classes.Endividamento.Valores();

                    docto = "";
                    parcela = 0;
                    tpDocto = "";
                    qtdLinhas++;

                    if (Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2) == null)
                    {
                        // coomo esta em branco subtrai
                        qtdLinhas--;
                        break;
                    }

                    for (cCnt = 1; cCnt <= cl; cCnt++)
                    {
                        try
                        {
                            str = Convert.ToString((range.Cells[rCnt, cCnt] as Excel.Range).Value2);

                            switch (cCnt)
                            {
                                case 1: // Empresa
                                    _empresaImportacao = new EmpresaBO().ConsultarPeloCodigoGlobus(str);
                                    _valoresEdicao.IdEmpresa = _empresaImportacao.IdEmpresa;
                                    break;
                                case 2: // Fornecedor
                                    _fornecedor = new FornecedoresGlobusBO().Consultar(str.PadLeft(6, '0'));
                                    _valoresEdicao.CodigoFornecedor = _fornecedor.CodigoFornecedor;
                                    break;
                                case 3: // Modalidade
                                    _valoresEdicao.Modalidade = str;
                                    break;
                                case 4: // Tipo
                                    _valoresEdicao.Tipo = str;
                                    break;
                                case 5: // Referencia
                                    _valoresEdicao.Referencia = str.Substring(3, 4) + str.Substring(0, 2);

                                    _dataInicio = new DateTime(Convert.ToInt32(str.Trim().Substring(3, 4)), Convert.ToInt32(str.Trim().Substring(0, 2)), 1);
                                    _dataFim = _dataInicio.AddMonths(1).AddDays(-1);

                                    _valores = new EndividamentoBO().Consultar(_valoresEdicao.IdEmpresa, _fornecedor.CodigoFornecedor, _valoresEdicao.Modalidade, _valoresEdicao.Tipo, _valoresEdicao.Referencia);
                                    _valoresCPG = new EndividamentoBO().ListarValores(_valores.Id);

                                    _valoresEdicao.IdEndividamento = _valores.Id;
                                    break;
                                case 6:// documento
                                    _valoresEdicao.Documento = str;
                                    docto = str;
                                    break;
                                case 7: // parcela
                                    _valoresEdicao.Documento = _valoresEdicao.Documento + " parcela " + str;
                                    parcela = Convert.ToInt32(str);
                                    break;
                                case 8: // série
                                    _valoresEdicao.Documento = _valoresEdicao.Documento + " Série " + str;
                                    serie = str;
                                    break;
                                case 9: // tipo documento CPG
                                    _valoresEdicao.CodigoTipoDocumento = str;
                                    tpDocto = str;

                                    if (docto != null && docto != "")
                                    {
                                        _val = new EndividamentoBO().BuscarDocumentoContasPagar(_empresaImportacao, _fornecedor.CodigoFornecedor, _valoresEdicao.Modalidade, tpDocto, docto.PadLeft(10, '0'), parcela, serie);

                                        _valoresEdicao.CodigoInternoCPG = _val.CodigoInternoCPG;
                                        _valoresEdicao.Realizado = _val.Realizado;
                                        _valoresEdicao.Previsto = _val.Previsto;
                                        _valoresEdicao.Variacao = _val.Variacao;
                                        _valoresEdicao.Vencimento = _val.Vencimento;
                                        _valoresEdicao.Pagamentos = _val.Pagamentos;

                                        if (_valoresEdicao.CodigoInternoCPG != 0)
                                        {
                                            foreach (var item in _valoresCPG.Where(w => w.CodigoInternoCPG == _valoresEdicao.CodigoInternoCPG))
                                            {
                                                _valoresEdicao.Existe = true;
                                                _valoresEdicao.Id = item.Id;
                                            }
                                        }
                                    }
                                    break;
                                case 10: // Valor Juros
                                    _valoresEdicao.Juros = Math.Round(Convert.ToDecimal(str.Trim().Replace(".", "").Replace(" ", "")),2);
                                    break;
                                case 11: // contrato
                                    _valoresEdicao.Contrato = str;

                                    if (_valoresEdicao.CodigoInternoCPG == 0)
                                    {
                                        foreach (var item in _valoresCPG.Where(w => w.CodigoInternoCPG == _valoresEdicao.CodigoInternoCPG && 
                                                                                    w.Contrato == _valoresEdicao.Contrato))
                                        {
                                            _valoresEdicao.Existe = true;
                                            _valoresEdicao.Id = item.Id;
                                        }
                                    }
                                    break;
                            }

                        }
                        catch (Exception)
                        {
                            break;
                            //new Notificacoes.Mensagem(ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
                        }
                    }

                    if (_valoresEdicao != null)
                        _listaImporta.Add(_valoresEdicao);
                }
            }

            int qtdEncontrada = _listaImporta.Where(w => w.CodigoInternoCPG != 0 ).Count();

            /*if (qtdEncontrada < qtdLinhas)
            {
                new Notificacoes.Mensagem("Quantidade de doctos encontrados (" + qtdEncontrada + ") não bate com o arquivo (" + qtdLinhas + ")."
                    + Environment.NewLine + "Verifique se as colunas 'Documento, Parcela, Série e Tipo Docto' existem no Contas a Pagar."
                    + Environment.NewLine + "Importação cancelada.", Publicas.TipoMensagem.Alerta).ShowDialog();

                mensagemSistemaLabel.Text = "";
                Refresh();
                return;
            }*/

            foreach (var item in _listaImporta.GroupBy(g => new { g.IdEmpresa, g.CodigoFornecedor, g.Referencia}))
            {
                _itensImporta = _listaImporta.Where(w => w.IdEmpresa == item.Key.IdEmpresa &&
                                                               w.CodigoFornecedor == item.Key.CodigoFornecedor &&
                                                               w.Referencia == item.Key.Referencia).ToList();

                _valores = new Classes.Endividamento.Valores();

                foreach (var itemI in _itensImporta)
                {
                    _valores = new EndividamentoBO().Consultar(itemI.IdEmpresa, itemI.CodigoFornecedor, itemI.Modalidade, itemI.Tipo, itemI.Referencia);
                    _valoresCPG = new EndividamentoBO().ListarValores(_valores.Id);

                    if (!_valores.Existe)
                    {
                        _valores.IdEmpresa = itemI.IdEmpresa;
                        _valores.Referencia = itemI.Referencia;
                        _valores.Modalidade = itemI.Modalidade;
                        _valores.CodigoFornecedor = itemI.CodigoFornecedor;
                        _valores.Tipo = itemI.Tipo;
                        _valores.Id = 0;
                        _valores.Existe = false;
                    }

                    _dataInicio = new DateTime(Convert.ToInt32(itemI.Referencia.Substring(0, 4)), Convert.ToInt32(itemI.Referencia.Substring(4, 2)), 1);
                    _dataFim = _dataInicio.AddMonths(1).AddDays(-1);

                    _valoresCPGImportacao = new EndividamentoBO().BuscarDocumentoContasPagar(_empresa, itemI.CodigoFornecedor, itemI.Modalidade, _dataInicio, _dataFim);
                    
                    foreach (var itemV in _valoresCPGImportacao)
                    {
                        if (_valoresCPG.Where(w => w.CodigoInternoCPG == itemV.CodigoInternoCPG).Count() == 0 &&
                            _itensImporta.Where(w => w.CodigoInternoCPG == itemV.CodigoInternoCPG).Count() == 0)
                            _itensImporta.Add(itemV);
                    }

                    _valores.Previsto = _itensImporta.Sum(s => s.Previsto);
                    _valores.Realizado = _itensImporta.Sum(s => s.Realizado);
                    _valores.Juros = Math.Round(_itensImporta.Sum(s => s.Juros), 2);
                    _valores.Variacao = _itensImporta.Sum(s => s.Variacao);

                    if (!new EndividamentoBO().Gravar(_valores, _itensImporta))
                    {
                        new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }

                    break;
                }

                Log _log = new Log();
                _log.IdUsuario = Publicas._usuario.Id;
                _log.Descricao = "Gravou os valores de endividamento da empresa " + empresaComboBoxAdv.Text +
                    " Fornecedor " + _fornecedor.Numero + " " + NomeFornecedorTextBox.Text +
                    " Modalidade " + ModalidadeComboBox.Text +
                    " Tipo " + TipoComboBox.Text +
                    " Referencia " + referenciaMaskedEditBox.Text +
                    " no total de Previsto " + _valores.Previsto.ToString() +
                    " no total de Realizado " + _valores.Realizado.ToString() +
                    " no total de Juros " + Math.Round(_valores.Juros,2).ToString() +
                    " no total de Variacao " + _valores.Variacao.ToString()+
                    " por importação" ;
            }

            mensagemSistemaLabel.Text = "";
            Refresh();

            new Notificacoes.Mensagem("Importação concluída.", Publicas.TipoMensagem.Sucesso).ShowDialog();

            empresaComboBoxAdv.Focus();
        }

        private void pesquisaFornecedorButton_Click(object sender, EventArgs e)
        {

        }

        private void gridGroupingControl2_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            {
                if (e.TableCellIdentity.TableCellType == GridTableCellType.FilterBarCell)
                    return;

                {
                    //muda cor dos totais
                    if ((e.TableCellIdentity.TableCellType == GridTableCellType.GroupCaptionSummaryCell ||
                        e.TableCellIdentity.TableCellType == GridTableCellType.SummaryRowHeaderCell ||
                        e.TableCellIdentity.TableCellType == GridTableCellType.SummaryFieldCell) &&
                        (e.Style.TableCellIdentity.ColIndex == 5 ||
                        e.Style.TableCellIdentity.ColIndex == 6||
                        e.Style.TableCellIdentity.ColIndex == 9 ||
                        e.Style.TableCellIdentity.ColIndex == 12 ||
                        e.Style.TableCellIdentity.ColIndex == 15 ||
                        e.Style.TableCellIdentity.ColIndex >= 18))
                        e.Style.TextColor = Color.DarkOrange;
                    else
                    if (e.TableCellIdentity.Column.MappingName.Contains("Variacao") ||
                       (e.TableCellIdentity.Column.MappingName.Equals("Juros")) ||
                       (e.TableCellIdentity.Column.MappingName.Equals("CPGPrevisto")) ||
                       (e.TableCellIdentity.Column.MappingName.Equals("CPGJuros")))
                        e.Style.TextColor = Color.DarkOrange;
                    
                    Record dr;
                    GridRecordRow rec = this.gridGroupingControl2.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;
                    
                    if (e.TableCellIdentity.Column.MappingName.Equals( "Variacao" ))
                    {
                        if (rec != null)
                        {
                            dr = rec.GetRecord() as Record;
                            if (dr != null && !(bool)dr["TemContaVariacao"])
                                e.Style.TextColor = Color.OrangeRed;

                        }
                    }

                    if (e.TableCellIdentity.Column.MappingName == "Juros")
                    {
                        if (rec != null)
                        {
                            dr = rec.GetRecord() as Record;
                            if (dr != null && !(bool)dr["TemContaJuros"])
                                e.Style.TextColor = Color.OrangeRed;

                        }
                    }
                    
                    if (e.TableCellIdentity.Column.MappingName == "CPGJuros")
                    {
                        if (rec != null)
                        {
                            dr = rec.GetRecord() as Record;
                            if (dr != null && !(bool)dr["TemContaJurosPrazo"])
                                e.Style.TextColor = Color.OrangeRed;

                        }
                    }

                    if (e.TableCellIdentity.Column.MappingName == "CPGPrevisto")
                    {
                        if (rec != null)
                        {
                            dr = rec.GetRecord() as Record;
                            if (dr != null && !(bool)dr["TemContaPrevistoPrazo"])
                                e.Style.TextColor = Color.OrangeRed;
                        }
                    }
                }
            }
            catch { }
        }

        private void exportarParaExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mensagemSistemaLabel.Text = "Exportando dados para o Excel, aguarde...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            if (!System.IO.Directory.Exists(Publicas._caminhoAnexosRateioCTB))
                System.IO.Directory.CreateDirectory(Publicas._caminhoAnexosRateioCTB);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;

            string nomeArquivo = "Endividamento_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_"
                               + _referencia.ToString();

            xlApp = new Excel.Application();

            try
            {

                xlApp.DisplayAlerts = false;

                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int linha = 1;
                int col = 1;

                foreach (var itemG in _valoresEmpresas.GroupBy(g => new { g.Tipo, g.Fornecedor}))
                {
                    col = 1;
                    linha++;

                    xlWorkSheet.Cells[linha, col] = "Tipo: " + itemG.Key.Tipo;
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Fornecedor: " + itemG.Key.Fornecedor;

                    foreach (var itemM in _valoresEmpresas.Where(w => w.Tipo == itemG.Key.Tipo
                                                                   && w.Fornecedor == itemG.Key.Fornecedor)
                                                          .GroupBy(g => new { g.Modalidade }))
                    {
                        col = 1;
                        linha++;

                        xlWorkSheet.Cells[linha, col] = "Modalidade: " + itemM.Key.Modalidade;
                        linha++;
                        linha++;

                        xlWorkSheet.Cells[linha, col] = "Contrato";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Realizado";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Previsto";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Variação";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Juros";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Tipo Docto CPG";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Documento CPG";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Vencimento";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Pagamento";

                        foreach (var itemC in _valoresEmpresas.Where(w => w.Tipo == itemG.Key.Tipo
                                                                       && w.Fornecedor == itemG.Key.Fornecedor
                                                                       && w.Modalidade == itemM.Key.Modalidade))
                        {
                            linha++;
                            col = 1;
                            xlWorkSheet.Cells[linha, col] = itemC.Contrato;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.Realizado; 
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.Previsto;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.Variacao;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.Juros;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.CodigoTipoDocumento;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.Documento.ToString();
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.Vencimento.ToShortDateString();
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.Pagamentos;
                            col++;
                        }

                    }

                    linha++;
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
                new Notificacoes.Mensagem("Arquivo gerado com sucesso." + Environment.NewLine +
                    "Salvo na pasta " + Publicas._caminhoAnexosRateioCTB, Publicas.TipoMensagem.Sucesso).ShowDialog();
                this.Refresh();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                mensagemSistemaLabel.Text = "";
                new Notificacoes.Mensagem("Não foi possível gerar o arquivo." + Environment.NewLine +
                    "Tente em outra maquina." + Environment.NewLine + ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
            }
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            if (e.Style.TableCellIdentity.DisplayElement.Kind == Syncfusion.Grouping.DisplayElementKind.Record)
            {
                try
                {
                    Record dr;
                    GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        dr = rec.GetRecord() as Record;

                        if ((decimal)dr["Realizado"] != (decimal)dr["RealizadoAtual"] && (decimal)dr["RealizadoAtual"] != 0)
                            e.Style.TextColor = Color.DarkOrange;

                        if ((bool)dr["Cancelada"])
                        {
                            if (Publicas._TemaBlack)
                                e.Style.TextColor = Color.Yellow;
                            else
                                e.Style.TextColor = Color.Maroon;
                        }
                    }
                }
                catch { }
            }
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[_rowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    atualizarRealizadoToolStripMenuItem.Enabled = (decimal)dr["Realizado"] != (decimal)dr["RealizadoAtual"];
                }
            }
            //
        }

        private void gridGroupingControl1_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[_rowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    atualizarRealizadoToolStripMenuItem.Enabled = (decimal)dr["Realizado"] != (decimal)dr["RealizadoAtual"];
                }
            }
            _colunaCorrente = gridGroupingControl1.TableControl.CurrentCell;
        }

        private void atualizarRealizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a Atualização dos valores Realizados ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;
            
            foreach (var item in _valoresCPG)
            {
                foreach (var itemA in _valoresCPGAtual.Where(w => w.CodigoInternoCPG == item.CodigoInternoCPG))
                {
                    item.RealizadoAlterado = item.Realizado != itemA.Realizado;
                    temAlteracao = true || item.RealizadoAlterado;
                    item.RealizadoAtual = itemA.Realizado;                    
                }
            }

            bool _novoDocto = false;
            foreach (var item in _valoresCPGAtual)
            {
                if (_valoresCPG.Where(w => w.CodigoInternoCPG == item.CodigoInternoCPG).Count() == 0)
                {
                    item.IdEmpresa = _empresa.IdEmpresa;
                    item.IdEndividamento = _valores.Id;
                    _valoresCPG.Add(item);
                    _novoDocto = true;
                }
            }

            foreach (var itemA in _valoresCPG)
            {
                foreach (var item in _valoresEmpresas.Where(w => w.IdEndividamento == itemA.IdEndividamento &&
                                                                 w.Id == itemA.Id))
                {
                    item.Realizado = itemA.RealizadoAtual;
                }
            }

            foreach (var item in _valoresConciliacao.Where(w => w.CodigoFornecedor == _fornecedor.CodigoFornecedor
                                                         && w.Modalidade == ModalidadeComboBox.Text
                                                         && w.Tipo == TipoComboBox.Text))
            {
                item.Realizado = _valoresCPG.Sum(s => s.RealizadoAtual);
            }

            

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl2.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl3.DataSource = new List<Classes.Endividamento.Valores>();
            gridGroupingControl1.DataSource = _valoresCPG;
            gridGroupingControl2.DataSource = _valoresConciliacao;
            gridGroupingControl3.DataSource = _valoresEmpresas;

            if (_novoDocto)
                new Notificacoes.Mensagem("Incluído(s) novo(s) contrato(s), além de atualizar os valores realizados.", Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void consolidarCheckBox_KeyDown(object sender, KeyEventArgs e)
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

        private void gridGroupingControl1_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[_rowIndex] as GridRecordRow;

                string nomeColuna = gridGroupingControl1.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
                bool marcado = false;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr[nomeColuna];

                        foreach (var item in _valoresCPG.Where(w => w.Id == (int)dr["Id"]))
                        {
                                item.Excluir = marcado;
                        }
                    }
                }
            }
            catch { }

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Valores>();

            gridGroupingControl1.DataSource = _valoresCPG;
            gridGroupingControl1.Refresh();
            excluirButton.Enabled = _valoresCPG.Where(w => w.Excluir).Count() == 0 && _valoresCPGCancelados.Where(w => w.Excluir).Count() == 0;
        }

        private void gridGroupingControl1_TableControlCellMouseDown(object sender, GridTableControlCellMouseEventArgs e)
        {
            _colunaCorrente = gridGroupingControl1.TableControl.CurrentCell;
        }

        private void marcarTodosComStatusDeCanceladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in _valoresCPG.Where(w => w.Cancelada))
            {
                item.Excluir = true;
            }

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Valores>();

            gridGroupingControl1.DataSource = _valoresCPG;
            gridGroupingControl1.Refresh();
            excluirButton.Enabled = _valoresCPG.Where(w => w.Excluir).Count() == 0 && _valoresCPGCancelados.Where(w => w.Excluir).Count() == 0;
        }

        private void desmarcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in _valoresCPG.Where(w => w.Excluir))
            {
                item.Excluir = false;
            }

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Valores>();

            gridGroupingControl1.DataSource = _valoresCPG;
            gridGroupingControl1.Refresh();
            excluirButton.Enabled = _valoresCPG.Where(w => w.Excluir).Count() == 0 && _valoresCPGCancelados.Where(w => w.Excluir).Count() == 0;
        }

        private void gridGroupingControl4_TableControlCellMouseDown(object sender, GridTableControlCellMouseEventArgs e)
        {
            _colunaCorrente = gridGroupingControl4.TableControl.CurrentCell;
        }

        private void gridGroupingControl4_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            _colunaCorrente = gridGroupingControl4.TableControl.CurrentCell;
        }

        private void gridGroupingControl4_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl4.Table.DisplayElements[_rowIndex] as GridRecordRow;

                string nomeColuna = gridGroupingControl4.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
                bool marcado = false;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr[nomeColuna];

                        foreach (var item in _valoresCPGCancelados.Where(w => w.Id == (int)dr["Id"]))
                        {
                            item.Excluir = marcado;
                        }
                    }
                }
            }
            catch { }

            gridGroupingControl4.DataSource = new List<Classes.Endividamento.Valores>();

            gridGroupingControl4.DataSource = _valoresCPGCancelados;
            gridGroupingControl4.Refresh();
            excluirButton.Enabled = _valoresCPG.Where(w => w.Excluir).Count() == 0 && _valoresCPGCancelados.Where(w => w.Excluir).Count() == 0;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
           
           GridTable reg = gridGroupingControl4.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                int arquivei = 0;

                posIniId = itemR.Info.IndexOf("Id =") + 5;
                posFimId = itemR.Info.IndexOf(", IdEndividamento");
                arquivei = Convert.ToInt32(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());
                
                foreach (var item in _valoresCPGCancelados.Where(w => w.Cancelada && w.Id == arquivei))
                {
                    item.Excluir = true;
                }

            }

            gridGroupingControl4.DataSource = new List<Classes.Endividamento.Valores>();

            gridGroupingControl4.DataSource = _valoresCPGCancelados;
            gridGroupingControl4.Refresh();
            excluirButton.Enabled = _valoresCPG.Where(w => w.Excluir).Count() == 0 && _valoresCPGCancelados.Where(w => w.Excluir).Count() == 0;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            GridTable reg = gridGroupingControl4.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                int arquivei = 0;

                posIniId = itemR.Info.IndexOf("Id =") + 5;
                posFimId = itemR.Info.IndexOf(", IdEndividamento");
                arquivei = Convert.ToInt32(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _valoresCPGCancelados.Where(w => w.Cancelada && w.Id == arquivei))
                {
                    item.Excluir = false;
                }

            }

            gridGroupingControl4.DataSource = new List<Classes.Endividamento.Valores>();

            gridGroupingControl4.DataSource = _valoresCPGCancelados;
            gridGroupingControl4.Refresh();
            excluirButton.Enabled = _valoresCPG.Where(w => w.Excluir).Count() == 0 && _valoresCPGCancelados.Where(w => w.Excluir).Count() == 0;
        }

        private void EncerraCancelaConciliacaoButton_Click(object sender, EventArgs e)
        {

            if (!_valores.Encerrado)
            {
                if (new Notificacoes.Mensagem("Confirma o encerramento ?"
                    , Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    return;
            }
            else
            {
                if (new Notificacoes.Mensagem("Confirma o cancelamento do encerramento ?"
                    , Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    return;
            }

            // Grava o conciliado
            string _textoLogConciliado = "";

            if (!_valores.Encerrado && _listaConciliados.Count() == 0)
            {
                _textoLogConciliado = " Encerrou a conciliação com os valores da empresa " + empresaComboBoxAdv.Text + " Referencia " + referenciaMaskedEditBox.Text;
                List<Classes.Endividamento.Conciliado> _listaGravarConciliados = new List<Classes.Endividamento.Conciliado>();
                foreach (var item in _valoresConciliacao)
                {
                    _textoLogConciliado = _textoLogConciliado
                                + " Fornecedor " + item.Fornecedor
                                + " Previsto " + item.Previsto.ToString()
                                + " Realizado " + item.Realizado.ToString()
                                + " Variacao " + item.Variacao.ToString()
                                + " Juros " + item.Juros.ToString()
                                + " [ Previsto CPG Curto " + item.CPGCurto.ToString()
                                + " CTB Curto " + item.CTBCurto.ToString()
                                + " Variacao " + item.VariacaoCurto.ToString()
                                + " CPG Longo " + item.CPGLongo.ToString()
                                + " CTB Longo " + item.CTBLongo.ToString()
                                + " Variacao " + item.VariacaoLongo.ToString()
                                + " ] [ Juros CPG Curto " + item.CPGJurosCurto.ToString()
                                + " CTB Curto " + item.CTBJurosCurto.ToString()
                                + " Variacao " + item.VariacaoJurosCurto.ToString()
                                + " CPG Longo " + item.CPGJurosLongo.ToString()
                                + " CTB Longo " + item.CTBJurosLongo.ToString()
                                + " Variacao " + item.VariacaoJurosLongo.ToString()
                                + " ] Conciliação [ " + (Convert.ToInt32(referenciaMaskedEditBox.ClipText) + 1).ToString("00/0000") + " Previsto " + item.CPGPrevisto.ToString()
                                + " Juros " + item.CPGJuros.ToString() + " ];";

                    _listaGravarConciliados.Add(new Classes.Endividamento.Conciliado()
                    {
                        IdEmpresa = _empresa.IdEmpresa,
                        CodigoFornecedor = item.CodigoFornecedor,
                        Referencia = _valores.Referencia,
                        Modalidade = _valores.Modalidade,
                        Tipo = _valores.Tipo,
                        Previsto = item.Previsto,
                        Realizado = item.Realizado,
                        Juros = item.Juros,
                        PrevistoCPGCurto = item.CPGCurto,
                        PrevistoCPGLongo = item.CPGLongo,
                        PrevistoCTBCurto = item.CTBCurto,
                        PrevistoCTBLongo = item.CTBLongo,
                        PrevistoConciliado = item.CPGPrevisto,
                        JurosCPGCurto = item.CPGJurosCurto,
                        JurosCPGLongo = item.CPGJurosLongo,
                        JurosCTBCurto = item.CTBJurosCurto,
                        JurosCTBLongo = item.CTBJurosLongo,
                        JurosConciliado = item.CPGJuros
                    });
                }

                if (!new EndividamentoBO().GravarConciliacao(_listaGravarConciliados))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }
            else // Cancelou o encerramento
            {
                if (_valores.Encerrado && _listaConciliados.Count() != 0)
                {
                    _textoLogConciliado = " Cancelou o Encerramento da conciliação com os valores da empresa " + empresaComboBoxAdv.Text + " Referencia " + referenciaMaskedEditBox.Text ;

                    foreach (var item in _listaConciliados)
                    {
                        _textoLogConciliado = _textoLogConciliado
                                    + " Fornecedor " + item.Fornecedor
                                    + " Previsto " + item.Previsto.ToString()
                                    + " Realizado " + item.Realizado.ToString()
                                    + " Variacao " + (item.Realizado - item.Previsto).ToString()
                                    + " Juros " + item.Juros.ToString()
                                    + " [ Previsto CPG Curto " + item.PrevistoCPGCurto.ToString()
                                    + " CTB Curto " + item.PrevistoCTBCurto.ToString()
                                    + " Variacao " + (item.PrevistoCPGCurto - item.PrevistoCTBCurto).ToString()
                                    + " CPG Longo " + item.PrevistoCPGLongo.ToString()
                                    + " CTB Longo " + item.PrevistoCTBLongo.ToString()
                                    + " Variacao " + (item.PrevistoCPGLongo - item.PrevistoCTBLongo).ToString()
                                    + " ] [ Juros CPG Curto " + item.JurosCPGCurto.ToString()
                                    + " CTB Curto " + item.JurosCTBCurto.ToString()
                                    + " Variacao " + (item.JurosCPGCurto - item.JurosCTBCurto).ToString()
                                    + " CPG Longo " + item.JurosCPGLongo.ToString()
                                    + " CTB Longo " + item.JurosCTBLongo.ToString()
                                    + " Variacao " + (item.JurosCPGLongo - item.JurosCTBLongo).ToString()
                                    + " ] Conciliação [ " + (Convert.ToInt32(referenciaMaskedEditBox.ClipText) + 1).ToString("00/0000") + " Previsto " + item.PrevistoConciliado.ToString()
                                    + " Juros " + item.JurosConciliado.ToString() + " ];";
                    }

                    if (!new EndividamentoBO().GravarConciliacao(_listaConciliados))
                    {
                        new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }
                }
            }
            
            _valores.Encerrado = EncerraCancelaConciliacaoButton.Text.Contains("Encerrar");

            if (!new EndividamentoBO().Gravar(_valores, _valoresCPG))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            
            if (_valores.Encerrado)
            {
                if (!new EndividamentoBO().Encerra_Cancela_Conciliacao(_empresa.IdEmpresa, _referencia, "S"))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }
            else
            {
                
                if (!new EndividamentoBO().Encerra_Cancela_Conciliacao(_empresa.IdEmpresa, _referencia, "N"))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;

            _log.Tela = "Contabilidade - Endividamento - Valores";
            _log.Descricao = _textoLogConciliado;

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }
    }
}
