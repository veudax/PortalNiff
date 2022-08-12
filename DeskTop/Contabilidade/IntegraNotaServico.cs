using Classes;
using DynamicFilter;
using Negocio;
using Suportte.Notificacoes;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class IntegraNotaServico : Form
    {
        public IntegraNotaServico()
        {
            InitializeComponent();

            inicialDateTimePicker.BorderColor = Publicas._bordaSaida;
            inicialDateTimePicker.BackColor = tipoDocumentoTextBox.BackColor;
            inicialDateTimePicker.Value = DateTime.Now;
            finalDateTimePicker.BorderColor = Publicas._bordaSaida;
            finalDateTimePicker.BackColor = tipoDocumentoTextBox.BackColor;
            finalDateTimePicker.Value = DateTime.Now;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    notasGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    notasGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    notasGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    notasGridGroupingControl.BackColor = Publicas._panelTitulo;

                    itensGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    itensGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    itensGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    itensGridGroupingControl.BackColor = Publicas._panelTitulo;

                    notasRevogarGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    notasRevogarGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    notasRevogarGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    notasRevogarGridGroupingControl.BackColor = Publicas._panelTitulo;

                    itensRevogarGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    itensRevogarGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    itensRevogarGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    itensRevogarGridGroupingControl.BackColor = Publicas._panelTitulo;

                    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl1.BackColor = Publicas._panelTitulo;

                    gridGroupingControl2.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl2.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl2.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl2.BackColor = Publicas._panelTitulo;

                    XmlSPGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    XmlSPGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    XmlSPGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    XmlSPGridGroupingControl.BackColor = Publicas._panelTitulo;

                    ItensXmlGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    ItensXmlGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    ItensXmlGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    ItensXmlGridGroupingControl.BackColor = Publicas._panelTitulo;

                    gridGroupingControl3.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl3.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl3.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl3.BackColor = Publicas._panelTitulo;

                    DiscriminacaoRichText.BackColor = Publicas._panelTitulo;
                    DiscriminacaoRichText.ForeColor = Publicas._fonte;

                    DiscriminacaoConferenciaESFRichText.BackColor = Publicas._panelTitulo;
                    DiscriminacaoConferenciaESFRichText.ForeColor = Publicas._fonte;

                    inicialDateTimePicker.Style = VisualStyle.Office2016Black;
                    finalDateTimePicker.Style = VisualStyle.Office2016Black;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        #region Atributos
        Classes.Empresa _empresa;
        Classes.ParametrosArquivei _parametro;
        Classes.TipoDocumentoGlobus _tipoDocumento;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.NotasFiscaisServico> _listaDeNotasParaIntegrar;
        List<Classes.ItensNotasFiscaisServico> _listaItensParaIntegrar;

        List<Classes.NotasFiscaisServico> _listaDeNotasParaExcluir;
        List<Classes.NotasFiscaisServico> _listaDeNotasParaRevogar;
        List<Classes.ItensNotasFiscaisServico> _listaItensParaRevogar;

        List<Classes.NotasFiscaisServico> _listaDeNotasParaConferir;
        List<Classes.ItensNotasFiscaisServico> _listaItensParaConferir;

        List<Classes.NotasFiscaisServico> _listaDeNotasXMLSP;
        List<Classes.Arquivei> _listaNotasXmlArquivei;
        List<Classes.Arquivei> _listaNotasXmlArquivei_Excluir;

        List<Classes.ParametrosCodigoServico> _listaParametro;
        List<Classes.ParametrosCodigoServico> _listaParametroCodServicoSemRepeticao;

        GridCurrentCell _colunaCorrente;
 
        #endregion

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

        private void IntegraNotaServico_Shown(object sender, EventArgs e)
        {
            if (Publicas._TemaBlack)
                pesquisarButton.ForeColor = empresaComboBoxAdv.ForeColor;
            
            this.Location = new Point(this.Left, 60);

            int espacoEntreBotoes = limparButton.Left - (gravarButton.Left + gravarButton.Width);

            #region coloca os botões centralizados
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { gravarButton, limparButton };
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

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            StringCollection StatusNF = new StringCollection();
            StatusNF.AddRange(new string[] { "Normal", "Cancelada" });

            #region Define layout Grid
            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();
            GridDynamicFilter filter1 = new GridDynamicFilter();
            GridDynamicFilter filter2 = new GridDynamicFilter();
            GridDynamicFilter filter3 = new GridDynamicFilter();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(notasGridGroupingControl);
            filter.WireGrid(itensGridGroupingControl);

            #region Integrar
            #region Notas
            notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            notasGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            notasGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            notasGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            notasGridGroupingControl.TableControl.CellToolTip.Active = true;
            notasGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            notasGridGroupingControl.RecordNavigationBar.Label = "Notas";

            for (int i = 0; i < notasGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (i <= 1)
                    notasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                else
                    notasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                notasGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                notasGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                notasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                notasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                notasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            //changes the header text color on mouse hovering.
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
                this.notasGridGroupingControl.SetMetroStyle(metroColor);
                this.notasGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.notasGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.notasGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.notasGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.notasGridGroupingControl.Table.DefaultRecordRowHeight = 30;
            notasGridGroupingControl.Refresh();

            #endregion

            #region Itens
            itensGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();

            itensGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            itensGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            itensGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            itensGridGroupingControl.TableControl.CellToolTip.Active = true;
            itensGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            notasGridGroupingControl.RecordNavigationBar.Label = "Itens";

            for (int i = 0; i < itensGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (itensGridGroupingControl.TableDescriptor.Columns[i].MappingName == "CodigoServico")
                    itensGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                else
                    itensGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                itensGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                itensGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                itensGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                itensGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                itensGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.itensGridGroupingControl.SetMetroStyle(metroColor);
                this.itensGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.itensGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }
            // para permitir editar dados.
            this.itensGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.itensGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;


            itensGridGroupingControl.Refresh();

            #endregion
            #endregion

            #region revogar
            filter3.ApplyFilterOnlyOnCellLostFocus = true;
            filter3.WireGrid(notasRevogarGridGroupingControl);
            filter3.WireGrid(itensRevogarGridGroupingControl);
            #region Notas
            notasRevogarGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            notasRevogarGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            notasRevogarGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            notasRevogarGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            notasRevogarGridGroupingControl.TableControl.CellToolTip.Active = true;
            notasRevogarGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            notasRevogarGridGroupingControl.RecordNavigationBar.Label = "Notas";

            for (int i = 0; i < notasRevogarGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0)
                    notasRevogarGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                else
                    notasRevogarGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                notasRevogarGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                notasRevogarGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                notasRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                notasRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                notasRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.notasRevogarGridGroupingControl.SetMetroStyle(metroColor);
                this.notasRevogarGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.notasRevogarGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.notasRevogarGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.notasRevogarGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.notasRevogarGridGroupingControl.Table.DefaultRecordRowHeight = 35;
            notasRevogarGridGroupingControl.Refresh();

            #endregion

            #region Itens
            itensRevogarGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();

            itensRevogarGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            itensRevogarGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            itensRevogarGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            itensRevogarGridGroupingControl.TableControl.CellToolTip.Active = true;
            itensRevogarGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            itensRevogarGridGroupingControl.RecordNavigationBar.Label = "Itens";

            for (int i = 0; i < itensRevogarGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.itensRevogarGridGroupingControl.SetMetroStyle(metroColor);
                this.itensRevogarGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.itensRevogarGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.itensRevogarGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.itensRevogarGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            itensRevogarGridGroupingControl.Refresh();

            #endregion

            #endregion

            #region Conferir

            filter1.ApplyFilterOnlyOnCellLostFocus = true;
            filter1.WireGrid(gridGroupingControl1);
            filter1.WireGrid(gridGroupingControl2);
            #region Notas

            gridGroupingControl1.DataSource = new List<NotasFiscaisServico>();

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

            // para permitir editar dados.
            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 30;

            #endregion

            #region Itens
            gridGroupingControl2.DataSource = new List<ItensNotasFiscaisServico>();
            gridGroupingControl2.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl2.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl2.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl2.TableControl.CellToolTip.Active = true;
            gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl2.RecordNavigationBar.Label = "Itens";

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
                gridGroupingControl2.SetMetroStyle(metroColor);
                gridGroupingControl2.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                gridGroupingControl2.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }
            // para permitir editar dados.
            gridGroupingControl2.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            gridGroupingControl2.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            #endregion
            #endregion

            #region Conferir XML SP
            filter2.ApplyFilterOnlyOnCellLostFocus = true;
            filter2.WireGrid(XmlSPGridGroupingControl);
            filter2.WireGrid(ItensXmlGridGroupingControl);
            filter2.WireGrid(gridGroupingControl3);
            #region Notas
            XmlSPGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            XmlSPGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            XmlSPGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            XmlSPGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            XmlSPGridGroupingControl.TableControl.CellToolTip.Active = true;
            XmlSPGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            XmlSPGridGroupingControl.RecordNavigationBar.Label = "";

            for (int i = 0; i < XmlSPGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                XmlSPGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                XmlSPGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                XmlSPGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                XmlSPGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                XmlSPGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.XmlSPGridGroupingControl.SetMetroStyle(metroColor);
                this.XmlSPGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.XmlSPGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.XmlSPGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.XmlSPGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.XmlSPGridGroupingControl.Table.DefaultRecordRowHeight = 30;

            #endregion

            #region Itens
            ItensXmlGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();

            ItensXmlGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            ItensXmlGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            ItensXmlGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            ItensXmlGridGroupingControl.TableControl.CellToolTip.Active = true;
            ItensXmlGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            ItensXmlGridGroupingControl.RecordNavigationBar.Label = "Itens";

            for (int i = 0; i < ItensXmlGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (ItensXmlGridGroupingControl.TableDescriptor.Columns[i].MappingName == "CodigoServico")
                    ItensXmlGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                else
                    ItensXmlGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                ItensXmlGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                ItensXmlGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                ItensXmlGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                ItensXmlGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                ItensXmlGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.ItensXmlGridGroupingControl.SetMetroStyle(metroColor);
                this.ItensXmlGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.ItensXmlGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }
            // para permitir editar dados.
            this.ItensXmlGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.ItensXmlGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;


            ItensXmlGridGroupingControl.Refresh();

            #endregion

            #region Não encontrados
            gridGroupingControl3.DataSource = new List<ItensNotasFiscaisServico>();

            gridGroupingControl3.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl3.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl3.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl3.TableControl.CellToolTip.Active = true;
            gridGroupingControl3.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl3.RecordNavigationBar.Label = "Itens";

            for (int i = 0; i < gridGroupingControl3.TableDescriptor.Columns.Count; i++)
            {
                    
                gridGroupingControl3.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl3.TableDescriptor.Columns[i].AllowSort = true;
                gridGroupingControl3.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl3.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl3.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            gridGroupingControl3.TableDescriptor.Columns["Status"].Appearance.AnyRecordFieldCell.CellType = GridCellTypeName.ComboBox;
            gridGroupingControl3.TableDescriptor.Columns["Status"].Appearance.AnyRecordFieldCell.ChoiceList = StatusNF;


            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl3.SetMetroStyle(metroColor);
                this.gridGroupingControl3.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl3.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }
            // para permitir editar dados.
            this.gridGroupingControl3.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl3.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;


            gridGroupingControl3.Refresh();
            #endregion
            #endregion
            #endregion
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            tipoDocumentoTextBox.BorderColor = Publicas._bordaEntrada;
            pesquisaTipoButton.Enabled = string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim());
        }

        private void finalDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void pesquisarButton_Enter(object sender, EventArgs e)
        {
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoDocumentoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void usuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inicialDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void inicialDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                finalDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoDocumentoTextBox.Focus();
            }
            if (e.KeyCode == Keys.T)
                inicialDateTimePicker.Value = DateTime.Now.Date;
            if (e.KeyCode == Keys.U)
                inicialDateTimePicker.Value = new DateTime(inicialDateTimePicker.Value.Year, inicialDateTimePicker.Value.Month + 1, 1).AddDays(-1);
            if (e.KeyCode == Keys.P)
                inicialDateTimePicker.Value = new DateTime(inicialDateTimePicker.Value.Year, inicialDateTimePicker.Value.Month + 1, 1);
        }

        private void finalDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pesquisarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                inicialDateTimePicker.Focus();
            }
            if (e.KeyCode == Keys.T)
                finalDateTimePicker.Value = DateTime.Now.Date;
            if (e.KeyCode == Keys.U)
                finalDateTimePicker.Value = new DateTime(finalDateTimePicker.Value.Year, finalDateTimePicker.Value.Month + 1, 1).AddDays(-1);
            if (e.KeyCode == Keys.P)
                finalDateTimePicker.Value = new DateTime(finalDateTimePicker.Value.Year, finalDateTimePicker.Value.Month + 1, 1);

        }

        private void pesquisarButton_Validating(object sender, CancelEventArgs e)
        {
        }

        private void pesquisarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                notasGridGroupingControl.Focus();
            }
        }

        private void pesquisarButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(empresaComboBoxAdv.Text))
            {
                new Notificacoes.Mensagem("Selecione a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o Tipo do documento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoDocumentoTextBox.Focus();
                return;
            }

            mensagemSistemaLabel.Text = "Aguarde, Pesquisando ...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            itensGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();
            itensRevogarGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();
            notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();
            notasRevogarGridGroupingControl.DataSource = new List<NotasFiscaisServico>();
            gridGroupingControl1.DataSource = new List<NotasFiscaisServico>();
            gridGroupingControl3.DataSource = new List<Arquivei>();
            XmlSPGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            _listaDeNotasXMLSP = new List<NotasFiscaisServico>();
            _listaNotasXmlArquivei = new List<Arquivei>();
            _listaNotasXmlArquivei_Excluir = new List<Arquivei>();
                                 
            _listaDeNotasParaIntegrar = new NotaFiscalServicoBO().ListarNotaFiscais(tipoDocumentoTextBox.Text, _empresa.CodigoEmpresaGlobus, "E", inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date, false, _parametro.Id);
            _listaItensParaIntegrar = new NotaFiscalServicoBO().ListarItensNotaFiscais(tipoDocumentoTextBox.Text, _empresa.CodigoEmpresaGlobus, "E", inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date, false);

            if (_tipoDocumento.Codigo == "NFS")
                _listaNotasXmlArquivei = new NotaFiscalServicoBO().ListarXmlNFSe(_empresa.CodigoEmpresaGlobus, inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date);

            foreach (var item in _listaDeNotasParaIntegrar)
            {
                decimal valorItem = 0;
                decimal diferenca = 0;
                decimal valorItemU = 0;

                foreach (var itemI in _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == item.CodigoInternoNotaFiscal))
                {
                    valorItem = valorItem + Math.Round(itemI.ValorTotal, 2);
                    valorItemU = valorItemU + Math.Round(itemI.ValorUnitario, 2);
                }
                if (valorItem != item.ValorTotal)
                {
                    if (valorItem < item.ValorTotal)
                    {
                        diferenca = item.ValorTotal - valorItem;
                        foreach (var itemI in _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == item.CodigoInternoNotaFiscal))
                        {
                            itemI.ValorTotal = itemI.ValorTotal + diferenca;
                            break;
                        }
                    }
                    else
                    {
                        diferenca = valorItem - item.ValorTotal;
                        foreach (var itemI in _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == item.CodigoInternoNotaFiscal))
                        {
                            itemI.ValorTotal = itemI.ValorTotal - diferenca;
                            break;
                        }
                    }
                }

            }

            if (_tipoDocumento.Codigo == "NFS")
            {
                //só busca  do xml se for nfs
                _listaDeNotasXMLSP.AddRange(_listaDeNotasParaIntegrar.Where(w => w.IdArquivei != 0).ToList());

                foreach (var item in _listaDeNotasParaIntegrar)
                {
                    Classes.Arquivei _arquivei = new Arquivei();

                    foreach (var itemA in _listaNotasXmlArquivei.Where(w => w.Id == item.IdArquivei))
                    {
                        //_arquivei = itemA;
                        _listaNotasXmlArquivei_Excluir.Add(itemA);
                        //break;
                    }

                }

            }

            foreach (var item in _listaDeNotasXMLSP)
            {
                Classes.NotasFiscaisServico _arquivei = new NotasFiscaisServico();

                foreach (var itemA in _listaDeNotasParaIntegrar.Where(w => w.IdArquivei == item.IdArquivei))
                {
                    _arquivei = itemA;
                    //break;
                }

                _listaDeNotasParaIntegrar.Remove(_arquivei);

            }

            _listaDeNotasParaRevogar = new NotaFiscalServicoBO().ListarNotaFiscaisIntegradas(tipoDocumentoTextBox.Text, _empresa.CodigoEmpresaGlobus, "E", inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date, false);
            _listaItensParaRevogar = new NotaFiscalServicoBO().ListarItensNotaFiscaisIntegrados(tipoDocumentoTextBox.Text, _empresa.CodigoEmpresaGlobus, "E", inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date, false);

            if (_tipoDocumento.Codigo == "NFS")
            {
                foreach (var item in _listaDeNotasParaRevogar)
                {
                    Classes.Arquivei _arquivei = new Arquivei();

                    foreach (var itemA in _listaNotasXmlArquivei.Where(w => w.Id == item.IdArquivei))
                    {
                        _listaNotasXmlArquivei_Excluir.Add(itemA);
                    }
                }

                _listaDeNotasParaExcluir = new List<NotasFiscaisServico>();

                foreach (var item in _listaDeNotasXMLSP)
                {
                    Classes.NotasFiscaisServico _notas = new NotasFiscaisServico();

                    foreach (var itemA in _listaDeNotasParaRevogar.Where(w => w.IdArquivei == item.IdArquivei))
                    {
                        _notas = item;
                        break;
                    }

                    _listaDeNotasParaExcluir.Add(_notas);
                }

                foreach (var item in _listaDeNotasParaExcluir)
                {
                    _listaDeNotasXMLSP.Remove(item);
                }
            }

            XmlSPGridGroupingControl.DataSource = _listaDeNotasXMLSP;
            notasGridGroupingControl.DataSource = _listaDeNotasParaIntegrar.Where(w => w.IdArquivei == 0).ToList();
            notasRevogarGridGroupingControl.DataSource = _listaDeNotasParaRevogar;

            _listaDeNotasParaConferir = new NotaFiscalServicoBO().ListarNotaFiscaisEscrituracao(tipoDocumentoTextBox.Text, _empresa.CodigoEmpresaGlobus, "E", inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date, _parametro.Id);
            _listaItensParaConferir = new NotaFiscalServicoBO().ListarItensNotaFiscaisEscrituracao(tipoDocumentoTextBox.Text, _empresa.CodigoEmpresaGlobus, "E", inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date);
            
            if (_tipoDocumento.Codigo == "NFS")
            {
                foreach (var item in _listaDeNotasParaConferir)
                {
                    Classes.Arquivei _arquivei = new Arquivei();

                    foreach (var itemA in _listaNotasXmlArquivei.Where(w => w.Id == item.IdArquivei))
                    {
                        _listaNotasXmlArquivei_Excluir.Add(itemA);
                        //break;
                    }

                }
            }

            foreach (var item in _listaNotasXmlArquivei_Excluir)
            {
                _listaNotasXmlArquivei.Remove(item);
            }

            gridGroupingControl1.DataSource = _listaDeNotasParaConferir; 
            gridGroupingControl3.DataSource = _listaNotasXmlArquivei;

            tabControl.SelectedTab = IntegrarTabPage;

            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();
            gravarButton.Enabled = _listaDeNotasParaIntegrar.Count() != 0 || _listaDeNotasParaRevogar.Count != 0 || _listaDeNotasParaConferir.Count != 0;

            if (_listaDeNotasParaIntegrar.Count() == 0 && _listaDeNotasParaRevogar.Count() != 0 && _listaDeNotasParaConferir.Count() != 0)
            {
                new Notificacoes.Mensagem("Nenhuma Nota Fiscal encontrada para integrar nesse período.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tabControl.SelectedTab = IntegradasTabPage;
                return;
            }

            if (_listaDeNotasParaIntegrar.Count() == 0 && _listaDeNotasParaRevogar.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma Nota Fiscal encontrada integrada e revogar nesse período.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
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

            _parametro = new ParametrosArquiveiBO().Consultar(_empresa.IdEmpresa);
            if (!_parametro.Existe)
            {
                new Notificacoes.Mensagem("Empresa não parametrizada para o Arquivei.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            ExportaExcelPictureBox.Enabled = !string.IsNullOrEmpty(_parametro.DiretorioExportacao);
            ExportarExcelPictureBox.Enabled = !string.IsNullOrEmpty(_parametro.DiretorioExportacao);

            _listaParametro = new NotaFiscalServicoBO().Listar(_empresa.IdEmpresa, false);
            _listaParametroCodServicoSemRepeticao = new NotaFiscalServicoBO().Listar(_empresa.IdEmpresa, true);

        }

        private void tipoDocumentoTextBox_Validating(object sender, CancelEventArgs e)
        {
            tipoDocumentoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

                new Pesquisas.TipoDocumentoGlobus().ShowDialog();

                tipoDocumentoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text) || tipoDocumentoTextBox.Text == "0")
                {
                    tipoDocumentoTextBox.Text = string.Empty;
                    tipoDocumentoTextBox.Focus();
                    return;
                }
            }

            _tipoDocumento = new TipoDocumentoGlobusBO().Consultar(_empresa.CodigoEmpresaGlobus, tipoDocumentoTextBox.Text);

            if (!_tipoDocumento.Existe)
            {
                new Notificacoes.Mensagem("Tipo do documento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoDocumentoTextBox.Focus();
                return;
            }

            if (!_tipoDocumento.IntegraComLivroDeISS)
            {
                new Notificacoes.Mensagem("Tipo do documento não integra com o Livro de ISS.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoDocumentoTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _tipoDocumento.Descricao;
            XmlSPTabPage.TabVisible = _tipoDocumento.Codigo == "NFS";
            XmlSPNaoEncontradoTabPage.TabVisible = _tipoDocumento.Codigo == "NFS";
            
        }

        private void pesquisaTipoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

                new Pesquisas.TipoDocumentoGlobus().ShowDialog();

                tipoDocumentoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text) || tipoDocumentoTextBox.Text == "0")
                {
                    tipoDocumentoTextBox.Text = string.Empty;
                    tipoDocumentoTextBox.Focus();
                    return;
                }

                tipoDocumentoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void notasGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.notasGridGroupingControl.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null) 
                { 
                    decimal codigoNotaFiscal = (decimal)dr["CodigoInternoNotaFiscal"];

                    itensGridGroupingControl.DataSource = _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();

                    itensGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DataSource = _listaParametroCodServicoSemRepeticao;
                    itensGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                }
            }
            _colunaCorrente = notasGridGroupingControl.TableControl.CurrentCell;
        }

        private void notasGridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {

            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.notasGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        decimal codigoNotaFiscal = (decimal)dr["CodigoInternoNotaFiscal"];

                        itensGridGroupingControl.DataSource = _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();

                        itensGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DataSource = _listaParametroCodServicoSemRepeticao;
                        itensGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                    }
                }
            }
            catch { }
        }

        private void inicialDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void finalDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (finalDateTimePicker.Value.Date < inicialDateTimePicker.Value.Date)
            {
                new Notificacoes.Mensagem("Data final não pode ser menor que a data inicial.", Publicas.TipoMensagem.Alerta).ShowDialog();
                inicialDateTimePicker.Focus();
                return;
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaDeNotasParaIntegrar.Where(w => w.Marcado).Count() != 0 || _listaDeNotasXMLSP.Where(w => w.Marcado).Count() != 0)
            {
                foreach (var item in _listaDeNotasParaIntegrar.Where(w => w.Marcado))
                {
                    if (_listaItensParaIntegrar.Where(w => w.CodigoServico == "" && w.CodigoInternoNotaFiscal == item.CodigoInternoNotaFiscal).Count() != 0)
                    {
                        new Notificacoes.Mensagem("Existem itens Sem Código de serviço, verifique antes de Integrar. (Aba 'Notas para Integrar')" + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        notasGridGroupingControl.Focus();
                        return;
                    }
                }

                foreach (var item in _listaDeNotasXMLSP.Where(w => w.Marcado))
                {
                    if (_listaItensParaIntegrar.Where(w => w.CodigoServico == "" && w.CodigoInternoNotaFiscal == item.CodigoInternoNotaFiscal).Count() != 0)
                    {
                        new Notificacoes.Mensagem("Existem itens Sem Código de serviço, verifique antes de Integrar. (Aba 'Compara e Integra XML de Prefeituras')" + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        notasGridGroupingControl.Focus();
                        return;
                    }
                }
            }

            if (!new NotaFiscalServicoBO().Integrar(tipoDocumentoTextBox.Text, _listaDeNotasParaIntegrar.Where(w => w.Marcado).ToList(), _listaItensParaIntegrar))
            {
                    new Notificacoes.Mensagem("Problemas durante a integração." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    notasGridGroupingControl.Focus();
                    return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;

            if (_listaDeNotasParaIntegrar.Where(w => w.Marcado).Count() != 0)
            {
                _log.Descricao = "Integrou as notas abaixo da empresa " + empresaComboBoxAdv.Text + " [ ";

                foreach (var item in _listaDeNotasParaIntegrar.Where(w => w.Marcado))
                {
                    _log.Descricao = _log.Descricao + "NF " + item.NumeroNotaFiscal + " emissao " + item.DataEmissao.ToShortDateString() + " serie " + item.Serie + " Fornecedor " + item.Fornecedor +
                        " Conferida " + (item.Conferida ? "SIM e Liberado para Pagamento no CPG" : "NAO e cancelou liberação do Pagamento no CPG") + "; ";
                }

                _log.Descricao = _log.Descricao.Substring(0, _log.Descricao.Length - 1) + " ]";

                _log.Tela = "Escrituração Fiscal - Integra Nota Fiscal de Serviço";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }
            }

            if (!new NotaFiscalServicoBO().Integrar(tipoDocumentoTextBox.Text, _listaDeNotasXMLSP.Where(w => w.Marcado).ToList(), _listaItensParaIntegrar))
            {
                new Notificacoes.Mensagem("Problemas durante a integração." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                notasGridGroupingControl.Focus();
                return;
            }

            _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;

            if (_listaDeNotasXMLSP.Where(w => w.Marcado).Count() != 0)
            {
                _log.Descricao = "Integrou as notas abaixo da empresa " + empresaComboBoxAdv.Text + " [ ";

                foreach (var item in _listaDeNotasXMLSP.Where(w => w.Marcado))
                {
                    _log.Descricao = _log.Descricao + "NF " + item.NumeroNotaFiscal + " emissao " + item.DataEmissao.ToShortDateString() + " serie " + item.Serie + " Fornecedor " + item.Fornecedor +
                        " Conferida " + (item.Conferida ? "SIM e Liberado para Pagamento no CPG" : "NAO e cancelou liberação do Pagamento no CPG") + "; ";
                }

                _log.Descricao = _log.Descricao.Substring(0, _log.Descricao.Length - 1) + " ]";

                _log.Tela = "Escrituração Fiscal - Integra Nota Fiscal de Serviço";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }
            }

            if (!new NotaFiscalServicoBO().Revogar(_listaDeNotasParaRevogar.Where(w => w.Marcado).ToList()))
            {
                new Notificacoes.Mensagem("Problemas durante a revogação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                notasGridGroupingControl.Focus();
                return;
            }

            _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;

            if (_listaDeNotasParaRevogar.Where(w => w.Marcado).Count() != 0)
            {
                _log.Descricao = "Revogou as notas abaixo da empresa " + empresaComboBoxAdv.Text + " [ ";

                foreach (var item in _listaDeNotasParaRevogar.Where(w => w.Marcado))
                {
                    _log.Descricao = _log.Descricao + "NF " + item.NumeroNotaFiscal + " emissao " + item.DataEmissao.ToShortDateString() + " serie " + item.Serie + " Fornecedor " + item.Fornecedor +
                        " Conferida " + (item.Conferida ? "SIM e Liberado para Pagamento no CPG" : "NAO e cancelou liberação do Pagamento no CPG") + "; ";
                }

                _log.Descricao = _log.Descricao.Substring(0, _log.Descricao.Length - 2) + " ]";

                _log.Tela = "Escrituração Fiscal - Integra Nota Fiscal de Serviço";
                
                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }
            }

            if (!new NotaFiscalServicoBO().Conferir(_listaDeNotasParaConferir, _listaItensParaConferir))
            {
                new Notificacoes.Mensagem("Problemas durante a Conferência." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                notasGridGroupingControl.Focus();
                return;
            }

            _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;

            if (_listaDeNotasParaConferir.Where(w => w.Marcado).Count() != 0)
            {
                _log.Descricao = "Conferiu as notas abaixo da empresa " + empresaComboBoxAdv.Text + " [ ";

                foreach (var item in _listaDeNotasParaRevogar.Where(w => w.Marcado))
                {
                    _log.Descricao = _log.Descricao + "NF " + item.NumeroNotaFiscal + " emissao " + item.DataEmissao.ToShortDateString() + " serie " + item.Serie + " Fornecedor " + item.Fornecedor +
                        " Conferida " + (item.Conferida ? "SIM e Liberado para Pagamento no CPG" : "NAO e cancelou liberação do Pagamento no CPG") + "; ";
                }

                _log.Descricao = _log.Descricao.Substring(0, _log.Descricao.Length - 2) + " ]";

                _log.Tela = "Escrituração Fiscal - Integra Nota Fiscal de Serviço";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }
            }

            if (_listaNotasXmlArquivei.Count != 0)
            {
                if (!new NotaFiscalServicoBO().AtualizaStatusNFArquivei(_listaNotasXmlArquivei))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação do status e comentário das notas não encontradas." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    notasGridGroupingControl.Focus();
                    return;
                }

                _log = new Log();
                _log.IdUsuario = Publicas._usuario.Id;

                if (_listaNotasXmlArquivei.Where(w => w.StatusOld != w.Status).Count() != 0)
                {
                    _log.Descricao = "Alterou as notas não encontradas abaixo da empresa " + empresaComboBoxAdv.Text + " [ ";

                    foreach (var item in _listaNotasXmlArquivei.Where(w => w.StatusOld != w.Status))
                    {
                        _log.Descricao = _log.Descricao + "NF " + item.NumeroNF + " emissao " + item.DataEmissao.ToShortDateString() +
                            " serie " + item.Serie + " Fornecedor " + item.RazaoSocialEmitente +
                            " Status de " + item.StatusOld + " para " + item.Status +
                            " comentário adicionado -> " + item.ComentarioUsuario;
                    }

                    _log.Descricao = _log.Descricao.Substring(0, _log.Descricao.Length - 2) + " ]";

                    _log.Tela = "Escrituração Fiscal - Integra Nota Fiscal de Serviço";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }
            }

            new Notificacoes.Mensagem("Processo finalizado.", Publicas.TipoMensagem.Sucesso).ShowDialog();
            pesquisarButton_Click(sender, e);
        }

        private void notasRevogarGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.notasRevogarGridGroupingControl.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    decimal codigoNotaFiscal = (decimal)dr["CodigoInternoNotaFiscal"];

                    itensRevogarGridGroupingControl.DataSource = _listaItensParaRevogar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();
                }
            }
            _colunaCorrente = notasRevogarGridGroupingControl.TableControl.CurrentCell;
        }

        private void notasRevogarGridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.notasRevogarGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        decimal codigoNotaFiscal = (decimal)dr["CodigoInternoNotaFiscal"];

                        itensRevogarGridGroupingControl.DataSource = _listaItensParaRevogar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();
                    }
                }
            }
            catch { }
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();
            notasRevogarGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            itensGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();
            itensRevogarGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();

            gridGroupingControl1.DataSource = new List<ItensNotasFiscaisServico>();
            gridGroupingControl2.DataSource = new List<ItensNotasFiscaisServico>();
            gridGroupingControl3.DataSource = new List<ItensNotasFiscaisServico>();

            XmlSPGridGroupingControl.DataSource = new List<NotasFiscaisServico>();
            ItensXmlGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();
            DiscriminacaoRichText.Text = string.Empty;

            tipoDocumentoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;

            tipoDocumentoTextBox.Focus();
        }

        private void marcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            GridTable reg = notasGridGroupingControl.Table; 

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodigoInternoNotaFiscal =") + 25;
                posFimId = itemR.Info.IndexOf(", CodigoEmpresa");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == arquivei && !w.Marcado))
                {
                    item.Marcado = true;
                }
            }

            notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            notasGridGroupingControl.DataSource = _listaDeNotasParaIntegrar;
            notasGridGroupingControl.Refresh();
        }

        private void desmarcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listaDeNotasParaIntegrar.ForEach(u => u.Marcado = false);

            GridTable reg = notasGridGroupingControl.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodigoInternoNotaFiscal =") + 25;
                posFimId = itemR.Info.IndexOf(", CodigoEmpresa");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == arquivei && w.Marcado))
                {
                    item.Marcado = false;
                }
            }

            notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            notasGridGroupingControl.DataSource = _listaDeNotasParaIntegrar;
            notasGridGroupingControl.Refresh();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            GridTable reg = notasRevogarGridGroupingControl.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodigoInternoNotaFiscal =") + 25;
                posFimId = itemR.Info.IndexOf(", CodigoEmpresa");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasParaRevogar.Where(w => w.CodigoInternoNotaFiscal == arquivei && !w.Marcado))
                {
                    item.Marcado = true;
                }
            }

            notasRevogarGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            notasRevogarGridGroupingControl.DataSource = _listaDeNotasParaRevogar;
            notasRevogarGridGroupingControl.Refresh();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //_listaDeNotasParaRevogar.ForEach(u => u.Marcado = false);

            GridTable reg = notasRevogarGridGroupingControl.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodigoInternoNotaFiscal =") + 25;
                posFimId = itemR.Info.IndexOf(", CodigoEmpresa");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasParaRevogar.Where(w => w.CodigoInternoNotaFiscal == arquivei && w.Marcado))
                {
                    item.Marcado = false;
                }
            }
            notasRevogarGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            notasRevogarGridGroupingControl.DataSource = _listaDeNotasParaRevogar;
            notasRevogarGridGroupingControl.Refresh();
        }

        private void tipoDocumentoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaTipoButton.Enabled = string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim());
        }

        private void IntegraNotaServico_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }

        private void ExportaExcelPictureBox_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(_parametro.DiretorioExportacao))
            {
                new Notificacoes.Mensagem("Diretório '" + _parametro.DiretorioExportacao + "' não existe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            mensagemSistemaLabel.Text = "Exportando dados para o Excel, aguarde...";
            this.Cursor = Cursors.WaitCursor;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;
            string nomeArquivo = "NFS_NaoIntegradas" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" + inicialDateTimePicker.Value.ToShortDateString().Replace("/", "_")
                               + "_" + finalDateTimePicker.Value.ToShortDateString().Replace("/", "_");
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            List<Classes.NotasFiscaisServico> _Notas = new List<NotasFiscaisServico>();

            _Notas = _listaDeNotasParaIntegrar;

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int linha = 1;
            int col = 1;


            #region titulo colunas
            xlWorkSheet.Cells[linha, col] = "Tipo ";
            col++;

            xlWorkSheet.Cells[linha, col] = "Número";
            col++;
            xlWorkSheet.Cells[linha, col] = "Série";
            col++;
            xlWorkSheet.Cells[linha, col] = "Data de Entrada";
            col++;
            xlWorkSheet.Cells[linha, col] = "Data de Emissão";
            col++;
            xlWorkSheet.Cells[linha, col] = "Vencimento";
            col++;
            xlWorkSheet.Cells[linha, col] = "Fornecedor";
            col++;
            xlWorkSheet.Cells[linha, col] = "CFOP";
            col++;
            xlWorkSheet.Cells[linha, col] = "Total Da NF";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor Líquido";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base PIS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. PIS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor PIS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base Cofins";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. Cofins";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor Cofins";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base INSS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. INSS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor INSS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base ISS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. ISS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor ISS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base IR";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. IR";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor IR";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base CSLL";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. CSLL";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor CSLL";
            col++;
            #endregion

            foreach (var itemC in _Notas)
            {
                col = 1;
                linha++;

                #region Cabeçalho da Nota
                xlWorkSheet.Cells[linha, col] = tipoDocumentoTextBox.Text;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.NumeroNotaFiscal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Serie;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.DataEntrada.ToShortDateString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.DataEmissao.ToShortDateString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.DataVencimento.ToShortDateString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Fornecedor;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CFOP;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorTotal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorLiquido;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BasePIS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaPIS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorPIS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseCofins;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaCofins;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorCofins;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseINSS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaINSS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorINSS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseISS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaISS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorISS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseIR;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaIR;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorIR;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseCSLL;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaCSLL;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorCSLL;
                col++;
                #endregion

            }

            xlWorkSheet.Columns.AutoFit();
            xlWorkBook.SaveAs(_parametro.DiretorioExportacao + @"\" + nomeArquivo + ".xlsx",
                            Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
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
            new Notificacoes.Mensagem("Arquivo " + nomeArquivo + " gerado com sucesso." + Environment.NewLine +
                " Salvo em: " + _parametro.DiretorioExportacao, Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void ExportarExcelPictureBox_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(_parametro.DiretorioExportacao))
            {
                new Notificacoes.Mensagem("Diretório '" + _parametro.DiretorioExportacao + "' não existe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            mensagemSistemaLabel.Text = "Exportando dados para o Excel, aguarde...";
            this.Cursor = Cursors.WaitCursor;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;
            string nomeArquivo = "NFS_NaoConferidas" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" + inicialDateTimePicker.Value.ToShortDateString().Replace("/", "_")
                               + "_" + finalDateTimePicker.Value.ToShortDateString().Replace("/", "_");
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            List<Classes.NotasFiscaisServico> _Notas = new List<NotasFiscaisServico>();

            _Notas = _listaDeNotasParaRevogar.Where(w => !w.Conferida).ToList();

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int linha = 1;
            int col = 1;


            #region titulo colunas
            xlWorkSheet.Cells[linha, col] = "Tipo ";
            col++;

            xlWorkSheet.Cells[linha, col] = "Número";
            col++;
            xlWorkSheet.Cells[linha, col] = "Série";
            col++;
            xlWorkSheet.Cells[linha, col] = "Data de Entrada";
            col++;
            xlWorkSheet.Cells[linha, col] = "Data de Emissão";
            col++;
            xlWorkSheet.Cells[linha, col] = "Vencimento";
            col++;
            xlWorkSheet.Cells[linha, col] = "Fornecedor";
            col++;
            xlWorkSheet.Cells[linha, col] = "CFOP";
            col++;
            xlWorkSheet.Cells[linha, col] = "Total Da NF";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor Líquido";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base PIS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. PIS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor PIS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base Cofins";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. Cofins";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor Cofins";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base INSS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. INSS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor INSS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base ISS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. ISS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor ISS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base IR";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. IR";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor IR";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base CSLL";
            col++;
            xlWorkSheet.Cells[linha, col] = "Aliq. CSLL";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor CSLL";
            col++;
            #endregion

            foreach (var itemC in _Notas)
            {
                col = 1;
                linha++;

                #region Cabeçalho da Nota
                xlWorkSheet.Cells[linha, col] = tipoDocumentoTextBox.Text;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.NumeroNotaFiscal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Serie;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.DataEntrada.ToShortDateString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.DataEmissao.ToShortDateString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.DataVencimento.ToShortDateString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Fornecedor;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CFOP;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorTotal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorLiquido;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BasePIS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaPIS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorPIS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseCofins;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaCofins;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorCofins;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseINSS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaINSS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorINSS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseISS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaISS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorISS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseIR;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaIR;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorIR;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseCSLL;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AliquotaCSLL;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorCSLL;
                col++;
                #endregion
            }

            xlWorkSheet.Columns.AutoFit();
            xlWorkBook.SaveAs(_parametro.DiretorioExportacao + @"\" + nomeArquivo + ".xlsx",
                            Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
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
            new Notificacoes.Mensagem("Arquivo " + nomeArquivo + " gerado com sucesso." + Environment.NewLine +
                " Salvo em: " + _parametro.DiretorioExportacao, Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)  
                {
                    int codigoNotaFiscal = (int)dr["IdISS"];
                     
                    DiscriminacaoConferenciaESFRichText.Text = (string)dr["DiscriminacaoXml"];

                    if ((string)dr["CodigoServico"] != "")
                    {
                        foreach (var item in _listaItensParaConferir.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal))
                        {
                            item.CodigoServicoXml = (string)dr["CodigoServico"];
                        }
                        foreach (var item in _listaItensParaConferir.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal))
                        {
                            foreach (var itemP in _listaParametro.Where(w => w.CodigoServicoXML == item.CodigoServicoXml))
                            {
                                //if (item.CodigoServico == "") conforme solicitado no chamado
                                item.CodigoServico = itemP.CodigoServicoGlobus;
                            }
                        }
                    }
                    gridGroupingControl2.DataSource = _listaItensParaConferir.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();

                    gridGroupingControl2.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DataSource = _listaParametroCodServicoSemRepeticao;
                    gridGroupingControl2.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";

                }
            }
        }

        private void gridGroupingControl1_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
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
                        int codigoNotaFiscal = (int)dr["IdISS"];

                        DiscriminacaoConferenciaESFRichText.Text = (string)dr["DiscriminacaoXml"];

                        if ((string)dr["CodigoServico"] != "")
                        {
                            foreach (var item in _listaItensParaConferir.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal))
                            {
                                item.CodigoServicoXml = (string)dr["CodigoServico"];
                            }
                            foreach (var item in _listaItensParaConferir.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal))
                            {
                                foreach (var itemP in _listaParametro.Where(w => w.CodigoServicoXML == item.CodigoServicoXml))
                                {
                                    //if (item.CodigoServico == "") conforme solicitado no chamado
                                    item.CodigoServico = itemP.CodigoServicoGlobus;
                                }
                            }
                        }

                        gridGroupingControl2.DataSource = _listaItensParaConferir.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();
                        gridGroupingControl2.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DataSource = _listaParametroCodServicoSemRepeticao;
                        gridGroupingControl2.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                    }
                }
            }
            catch { }
        }

        private void gridGroupingControl1_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[_rowIndex] as GridRecordRow;

                string nomeColuna = "";

                GridTableCellStyleInfoIdentity style = this.gridGroupingControl1.TableModel[_colunaCorrente.RowIndex, _colunaCorrente.ColIndex].TableCellIdentity;

                if (style.TableCellType == GridTableCellType.RecordFieldCell || style.TableCellType == GridTableCellType.AlternateRecordFieldCell)
                    nomeColuna = style.Column.Name;
                
                bool marcado = false;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr[nomeColuna];
                        
                        foreach (var item in _listaDeNotasParaConferir.Where(w => w.CodDoctoEsf == (decimal)dr["CodDoctoEsf"] && w.IdISS == (int)dr["IdISS"]))
                        {
                            if (!item.IntegradaCPG && marcado)
                            {
                                new Notificacoes.Mensagem("Documento não integrado com Contas a Pagar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                return;
                            }

                            if (marcado)
                                item.Conferida = true;
                            else
                                item.Conferida = false;
                        }
                    }
                }
            }
            catch { }

            gridGroupingControl1.DataSource = new List<NotasFiscaisServico>();

            gridGroupingControl1.DataSource = _listaDeNotasParaConferir;
            gridGroupingControl1.Refresh();
        }

        private void gridGroupingControl1_TableControlCellMouseDown(object sender, GridTableControlCellMouseEventArgs e)
        {
            _colunaCorrente = gridGroupingControl1.TableControl.CurrentCell;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {// marcar todos conferir aba conferencias
            
            GridTable reg = gridGroupingControl1.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodDoctoEsf =") + 13;
                posFimId = itemR.Info.IndexOf(", ValorSubICMS");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasParaConferir.Where(w => w.CodDoctoEsf == arquivei && w.IntegradaCPG))
                {
                    item.Conferida = true;
                }
            }

            gridGroupingControl1.DataSource = new List<NotasFiscaisServico>();
            gridGroupingControl1.DataSource = _listaDeNotasParaConferir;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {// desmarcar todos conferir aba conferencias
            
            GridTable reg = gridGroupingControl1.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodDoctoEsf =") + 13;
                posFimId = itemR.Info.IndexOf(", ValorSubICMS");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasParaConferir.Where(w => w.IdISS == arquivei && w.Conferida))
                {
                    item.Conferida = false;
                }
            }

            gridGroupingControl1.DataSource = new List<NotasFiscaisServico>();

            gridGroupingControl1.DataSource = _listaDeNotasParaConferir;

        }

        private void notasGridGroupingControl_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            try
            {
                string nomeColuna = "";

                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.notasGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                GridTableCellStyleInfoIdentity style = this.notasGridGroupingControl.TableModel[_colunaCorrente.RowIndex, _colunaCorrente.ColIndex].TableCellIdentity;

                if (style.TableCellType == GridTableCellType.RecordFieldCell || style.TableCellType == GridTableCellType.AlternateRecordFieldCell)
                    nomeColuna = style.Column.Name;

                bool marcado = false;

                if (rec != null && nomeColuna.Equals("Conferida"))
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr[nomeColuna];

                        foreach (var item in _listaDeNotasParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == (decimal)dr["CodigoInternoNotaFiscal"]))
                        {
                            if (!item.IntegradaCPG && marcado)
                            {
                                new Notificacoes.Mensagem("Documento não integrado com Contas a Pagar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                return;
                            }

                            if (marcado)
                                item.Conferida = true;
                            else
                                item.Conferida = false;
                        }

                        notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

                        notasGridGroupingControl.DataSource = _listaDeNotasParaIntegrar;
                        notasGridGroupingControl.Refresh();
                    }
                }
            }
            catch { }

        }

        private void marcarTodosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // só marcar que estiverem integradas

            GridTable reg = notasGridGroupingControl.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodigoInternoNotaFiscal =") + 25;
                posFimId = itemR.Info.IndexOf(", CodigoEmpresa");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == arquivei && w.IntegradaCPG))
                {
                    item.Conferida = true;
                }
            }
            
            notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            notasGridGroupingControl.DataSource = _listaDeNotasParaIntegrar;
            notasGridGroupingControl.Refresh();
        }

        private void demarcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            GridTable reg = notasGridGroupingControl.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodigoInternoNotaFiscal =") + 25;
                posFimId = itemR.Info.IndexOf(", CodigoEmpresa");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == arquivei && w.Conferida))
                {
                    item.Conferida = false;
                }
            }

            notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            notasGridGroupingControl.DataSource = _listaDeNotasParaIntegrar;
            notasGridGroupingControl.Refresh();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl2.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if ((string)dr["CodigoServicoXml"] != "")
                    {
                        gridGroupingControl2.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == (string)dr["CodigoServicoXml"]).ToList();
                        gridGroupingControl2.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                    }
                }
            }
        }

        private void XmlSPGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.XmlSPGridGroupingControl.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                { 
                    DiscriminacaoRichText.Text = (string)dr["DiscriminacaoXml"];
                    decimal codigoNotaFiscal = (decimal)dr["CodigoInternoNotaFiscal"];

                    foreach (var item in _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal))
                    {
                        item.CodigoServicoXml = (string)dr["CodigoServico"];
                    }
                    foreach (var item in _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal))
                    {
                        foreach (var itemP in _listaParametro.Where(w => w.CodigoServicoXML == item.CodigoServicoXml))
                        {
                            //if (item.CodigoServico == "") conforme solicitado no chamado
                                item.CodigoServico = itemP.CodigoServicoGlobus;
                        }
                    }
                    ItensXmlGridGroupingControl.DataSource = _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();

                    string cod = "";

                    foreach (var item in _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal))
                    {
                        cod = item.CodigoServicoXml;
                        break;
                    }

                    ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == cod).ToList();
                    ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                }
                _colunaCorrente = XmlSPGridGroupingControl.TableControl.CurrentCell;
            }
        }

        private void XmlSPGridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {

            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.XmlSPGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        DiscriminacaoRichText.Text = (string)dr["DiscriminacaoXml"];
                        decimal codigoNotaFiscal = (decimal)dr["CodigoInternoNotaFiscal"];

                        foreach (var item in _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal))
                        {
                            item.CodigoServicoXml = (string)dr["CodigoServico"];
                        }
                         
                        foreach (var item in _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal))
                        {
                            foreach (var itemP in _listaParametro.Where(w => w.CodigoServicoXML == item.CodigoServicoXml))
                            {
                                //if (item.CodigoServico == "") conforme solicitado no chamado
                                    item.CodigoServico = itemP.CodigoServicoGlobus;
                            }
                        }
                        ItensXmlGridGroupingControl.DataSource = _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();

                        string cod = "";

                        foreach (var item in _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal))
                        {
                            cod = item.CodigoServicoXml;
                            break;
                        }

                        ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == cod).ToList();
                        ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                    }
                }
            }
            catch { }
                        
        }

        private void XmlSPGridGroupingControl_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            try
            {
                string nomeColuna = "";

                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.XmlSPGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                GridTableCellStyleInfoIdentity style = this.XmlSPGridGroupingControl.TableModel[_colunaCorrente.RowIndex, _colunaCorrente.ColIndex].TableCellIdentity;

                if (style.TableCellType == GridTableCellType.RecordFieldCell || style.TableCellType == GridTableCellType.AlternateRecordFieldCell)
                    nomeColuna = style.Column.Name;

                bool marcado = false;

                if (rec != null && nomeColuna.Equals("Conferida"))
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr[nomeColuna];

                        foreach (var item in _listaDeNotasXMLSP.Where(w => w.CodigoInternoNotaFiscal == (decimal)dr["CodigoInternoNotaFiscal"]))
                        {
                            if (!item.IntegradaCPG && marcado)
                            {
                                new Notificacoes.Mensagem("Documento não integrado com Contas a Pagar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                return;
                            }

                            if (marcado)
                                item.Conferida = true;
                            else
                                item.Conferida = false;
                        }

                        XmlSPGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

                        XmlSPGridGroupingControl.DataSource = _listaDeNotasXMLSP;
                        XmlSPGridGroupingControl.Refresh();
                    }
                }
                if (rec != null && nomeColuna.Equals("Marcado"))
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr[nomeColuna];

                        foreach (var item in _listaDeNotasXMLSP.Where(w => w.CodigoInternoNotaFiscal == (decimal)dr["CodigoInternoNotaFiscal"]))
                        {
                            if (marcado)
                                item.Marcado = true;
                            else
                                item.Marcado = false;

                            if (item.Diferencas && marcado)
                            {
                                if (new Notificacoes.Mensagem("Documento com diferenças. Confirma a Integração? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                                    item.Marcado = false;                                
                            }
                        }

                        XmlSPGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

                        XmlSPGridGroupingControl.DataSource = _listaDeNotasXMLSP;
                        XmlSPGridGroupingControl.Refresh();
                    }
                }
            }
            catch { }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // exporta notas não encontradas XML SP
            if (!System.IO.Directory.Exists(_parametro.DiretorioExportacao))
            {
                new Notificacoes.Mensagem("Diretório '" + _parametro.DiretorioExportacao + "' não existe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            mensagemSistemaLabel.Text = "Exportando dados para o Excel, aguarde...";
            this.Cursor = Cursors.WaitCursor;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;
            string nomeArquivo = "NFSe_XmlSPNaoEncontradoNoGlobus" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" + inicialDateTimePicker.Value.ToShortDateString().Replace("/", "_")
                               + "_" + finalDateTimePicker.Value.ToShortDateString().Replace("/", "_");
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            List<Classes.Arquivei> _Notas = new List<Arquivei>();

            _Notas = _listaNotasXmlArquivei;

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int linha = 1;
            int col = 1;


            #region titulo colunas
            xlWorkSheet.Cells[linha, col] = "Tipo ";
            col++;

            xlWorkSheet.Cells[linha, col] = "Número";
            col++;
            xlWorkSheet.Cells[linha, col] = "Data de Emissão";
            col++;
            xlWorkSheet.Cells[linha, col] = "Fornecedor";
            col++;
            xlWorkSheet.Cells[linha, col] = "Total Da NF";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor PIS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor Cofins";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor INSS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor ISS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor IR";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor CSLL";
            col++;
            #endregion

            foreach (var itemC in _Notas)
            {
                col = 1;
                linha++;

                #region Cabeçalho da Nota
                xlWorkSheet.Cells[linha, col] = tipoDocumentoTextBox.Text;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.NumeroNF;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.DataEmissao.ToShortDateString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RazaoSocialEmitente;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorTotalNF;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorPis;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorCofins;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorINSS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorISS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorIR;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorCSLL;
                #endregion

            }

            xlWorkSheet.Columns.AutoFit();
            xlWorkBook.SaveAs(_parametro.DiretorioExportacao + @"\" + nomeArquivo + ".xlsx",
                            Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
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
            new Notificacoes.Mensagem("Arquivo " + nomeArquivo + " gerado com sucesso." + Environment.NewLine +
                " Salvo em: " + _parametro.DiretorioExportacao, Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void enviarEmailPictureBox_Click(object sender, EventArgs e)
        {
            string detalhe = "";
            List<Arquivei> _listaNfEmail = new List<Arquivei>();

            if (_listaNotasXmlArquivei.Where(w => w.Selecionado).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma nota selecionada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            foreach (var item in _listaNotasXmlArquivei.Where(w => w.Selecionado)
                                                       .OrderBy(o => o.DataEmissao))
            {

                detalhe = detalhe + "<tr> " +
                                    "<td style='font-size: 12px'; align='Left='>" + item.DataEmissao.ToShortDateString() + "&nbsp;</td> " +
                                    "<td style='font-size: 12px'; align='Left'> " + item.NumeroNF + "&nbsp;</td> " +
                                    "<td style='font-size: 12px'; align='Left'> " + item.CidadeEmitente + "&nbsp;</td> " +
                                    "<td style='font-size: 12px'; align='Left'> " + item.TipoDocto + "&nbsp;</td> " +
                                    "<td style='font-size: 11px'; align='Left'> " + item.CNPJEmitente + "&nbsp;" + item.RazaoSocialEmitente + "</td> " +
                                    "</tr>";
            }

            #region Envia Email

            string[] _dadosEmail = new string[50];
            _dadosEmail[0] = empresaComboBoxAdv.Text;
            _dadosEmail[1] = detalhe;

            string emailDestino = "";
            string emailCopia = "";

            List<Usuario> _listaUsuarios = new List<Usuario>();

            List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;

            if (TestarEmailCheckBox.Checked)
                emailDestino = Publicas._usuario.Email;
            else
            {
                _listaEmpresasAutorizadas = new UsuarioBO().ConsultaUsuarioPorEmpresaAutorizada(_empresa.IdEmpresa);
                try
                {
                    _listaUsuarios = new UsuarioBO().ListarUsuarios(true);
                }
                catch
                {
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }

                foreach (var item in _listaEmpresasAutorizadas.Where(w => w.EmpresaAutoriza))
                {

                    foreach (var itemU in _listaUsuarios.Where(w => w.RecebeEmailNotaFiscal && w.Id == item.IdUsuario && w.Ativo))
                    {
                        if (itemU.IdDepartamento == 15 || itemU.UsuarioAcesso == "AVSOUZA") // Gerencia
                            emailCopia = emailCopia + itemU.Email + "; ";
                        else
                            emailDestino = emailDestino + itemU.Email + "; ";
                    }
                }
            }

            if (_listaNotasXmlArquivei.Count != 0)
            {
                Publicas.mensagemDeErro = "";
                Classes.Publicas.EnviarEmailNotasNaoLancadas(_dadosEmail, Publicas._usuario.Email, emailDestino, emailCopia, "Notas Fiscais de Serviços não lançadas no Globus", true);

                if (Publicas.mensagemDeErro != "")
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Alerta).ShowDialog();
                else
                    new Notificacoes.Mensagem("E-mail enviado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();
            }

            #endregion

            
        }

        private void XmlSPGridGroupingControl_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            Record dr;
            try
            {
                if (e.TableCellIdentity.RowIndex != -1)
                {

                    GridRecordRow rec = this.XmlSPGridGroupingControl.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        dr = rec.GetRecord() as Record;
                        if (dr != null && (bool)dr["Diferencas"])
                        {
                            e.Style.TextColor = Color.DarkOrange;
                        }

                    }
                }
            }
            catch { }
        }

        private void ItensXmlGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.ItensXmlGridGroupingControl.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {

                    ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == (string)dr["CodigoServicoXml"]).ToList();
                    ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                    //ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                               //.Appearance.AnyRecordFieldCell.
                }
                
            }            

        }

        private void ItensXmlGridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {

            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.ItensXmlGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == (string)dr["CodigoServicoXml"]).ToList();
                        ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                    }
                }
            }
            catch { }

        }

        private void ItensXmlGridGroupingControl_TableControlCellButtonClicked(object sender, GridTableControlCellButtonClickedEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.ItensXmlGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                               .Appearance.AnyRecordFieldCell.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == (string)dr["CodigoServicoXml"]).ToList();
                        ItensXmlGridGroupingControl.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                    }
                }
                ItensXmlGridGroupingControl.Refresh();
            }
            catch { }
        }

        private void toolStripMenuItem6_Click_1(object sender, EventArgs e)
        {
            //integrar todos xml
            GridTable reg = XmlSPGridGroupingControl.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodigoInternoNotaFiscal =") + 25;
                posFimId = itemR.Info.IndexOf(", CodigoEmpresa");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasXMLSP.Where(w => w.CodigoInternoNotaFiscal == arquivei && !w.Marcado && !w.Diferencas))
                {
                    item.Marcado = true;
                }
            }

            XmlSPGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            XmlSPGridGroupingControl.DataSource = _listaDeNotasXMLSP;
            XmlSPGridGroupingControl.Refresh();
        }

        private void toolStripMenuItem7_Click_1(object sender, EventArgs e)
        {
            // desfazer todos integração xml
            GridTable reg = XmlSPGridGroupingControl.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodigoInternoNotaFiscal =") + 25;
                posFimId = itemR.Info.IndexOf(", CodigoEmpresa");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasXMLSP.Where(w => w.CodigoInternoNotaFiscal == arquivei && w.Marcado))
                {
                    item.Marcado = false;
                }
            }

            XmlSPGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            XmlSPGridGroupingControl.DataSource = _listaDeNotasXMLSP;
            XmlSPGridGroupingControl.Refresh();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            // conferir todos xml
            // só marcar que estiverem integradas

            GridTable reg = XmlSPGridGroupingControl.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodigoInternoNotaFiscal =") + 25;
                posFimId = itemR.Info.IndexOf(", CodigoEmpresa");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasXMLSP.Where(w => w.CodigoInternoNotaFiscal == arquivei && w.IntegradaCPG))
                {
                    item.Conferida = true;
                }
            }

            XmlSPGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            XmlSPGridGroupingControl.DataSource = _listaDeNotasXMLSP;
            XmlSPGridGroupingControl.Refresh();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            // desconferir todos xml
            // só marcar que estiverem integradas

            GridTable reg = XmlSPGridGroupingControl.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                decimal arquivei = 0;

                posIniId = itemR.Info.IndexOf("CodigoInternoNotaFiscal =") + 25;
                posFimId = itemR.Info.IndexOf(", CodigoEmpresa");
                arquivei = Convert.ToDecimal(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaDeNotasXMLSP.Where(w => w.CodigoInternoNotaFiscal == arquivei && w.Conferida))
                {
                    item.Conferida = false;
                }
            }

            XmlSPGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            XmlSPGridGroupingControl.DataSource = _listaDeNotasXMLSP;
            XmlSPGridGroupingControl.Refresh();
        }

        private void gridGroupingControl2_TableControlCellButtonClicked(object sender, GridTableControlCellButtonClickedEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl2.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        if ((string)dr["CodigoServicoXml"] != "")
                        {
                            gridGroupingControl2.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == (string)dr["CodigoServicoXml"]).ToList();
                            gridGroupingControl2.TableDescriptor.Columns["CodigoServico"]
                                                       .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                        }
                    }
                }
                gridGroupingControl2.Refresh();
            }
            catch { }
        }

        private void gridGroupingControl2_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {

            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl2.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        if ((string)dr["CodigoServicoXml"] != "")
                        {
                            gridGroupingControl2.TableDescriptor.Columns["CodigoServico"]
                                                   .Appearance.AnyRecordFieldCell.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == (string)dr["CodigoServicoXml"]).ToList();
                            gridGroupingControl2.TableDescriptor.Columns["CodigoServico"]
                                                       .Appearance.AnyRecordFieldCell.DisplayMember = "CodigoServicoGlobus";
                        }
                    }
                }
            }
            catch { }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            // Selecionar Todos Não encontrados
            GridTable reg = gridGroupingControl3.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                int arquivei = 0;

                posIniId = itemR.Info.IndexOf("Id =") + 4;
                posFimId = itemR.Info.IndexOf(", DataImportado");
                arquivei = Convert.ToInt32(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaNotasXmlArquivei.Where(w => w.Id == arquivei && !w.Selecionado))
                {
                    item.Selecionado = true;
                }
            }

            gridGroupingControl3.DataSource = new List<Classes.Arquivei>();

            gridGroupingControl3.DataSource = _listaNotasXmlArquivei;
            gridGroupingControl3.Refresh();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            // DesSelecionar Todos XML Não encontrados
            GridTable reg = gridGroupingControl3.Table;

            foreach (var itemR in reg.FilteredRecords)
            {
                int posIniId = 0;
                int posFimId = 0;
                int arquivei = 0;

                posIniId = itemR.Info.IndexOf("Id =") + 4;
                posFimId = itemR.Info.IndexOf(", DataImportado");
                arquivei = Convert.ToInt32(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                foreach (var item in _listaNotasXmlArquivei.Where(w => w.Id == arquivei && w.Selecionado))
                {
                    item.Selecionado = false;
                }
            }

            gridGroupingControl3.DataSource = new List<Classes.Arquivei>();

            gridGroupingControl3.DataSource = _listaNotasXmlArquivei;
            gridGroupingControl3.Refresh();
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            Record dr;
            try
            {
                if (e.TableCellIdentity.RowIndex != -1)
                {

                    GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        dr = rec.GetRecord() as Record;
                        if (dr != null && (bool)dr["Diferencas"])
                        {
                            e.Style.TextColor = Color.Red;
                        }

                    }
                }
            }
            catch { }
        }
    }
}