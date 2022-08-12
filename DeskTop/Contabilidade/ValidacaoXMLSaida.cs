﻿using Classes;
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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace Suportte.Contabilidade
{
    public partial class ValidacaoXMLSaida : Form
    {
        public ValidacaoXMLSaida()
        {
            InitializeComponent();

            inicialDateTimePicker.BorderColor = Publicas._bordaSaida;
            inicialDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
            inicialDateTimePicker.Value = DateTime.Now;
            finalDateTimePicker.BorderColor = Publicas._bordaSaida;
            finalDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
            finalDateTimePicker.Value = DateTime.Now;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    CabecalhoEncontradaGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    CabecalhoEncontradaGrid.ColorStyles = ColorStyles.Office2010Black;
                    CabecalhoEncontradaGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    CabecalhoEncontradaGrid.BackColor = Publicas._panelTitulo;

                    ItensEncontradosGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    ItensEncontradosGrid.ColorStyles = ColorStyles.Office2010Black;
                    ItensEncontradosGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    ItensEncontradosGrid.BackColor = Publicas._panelTitulo;

                    NFNaoEncontradaGrid_Novo.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    NFNaoEncontradaGrid_Novo.ColorStyles = ColorStyles.Office2010Black;
                    NFNaoEncontradaGrid_Novo.GridVisualStyles = GridVisualStyles.Office2016Black;
                    NFNaoEncontradaGrid_Novo.BackColor = Publicas._panelTitulo;

                    CanceladasGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    CanceladasGrid.ColorStyles = ColorStyles.Office2010Black;
                    CanceladasGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    CanceladasGrid.BackColor = Publicas._panelTitulo;

                    SequenciaGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    SequenciaGrid.ColorStyles = ColorStyles.Office2010Black;
                    SequenciaGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    SequenciaGrid.BackColor = Publicas._panelTitulo;

                    inicialDateTimePicker.Style = VisualStyle.Office2016Black;
                    finalDateTimePicker.Style = VisualStyle.Office2016Black;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        #region Atributos
        Classes.Empresa _empresa;
        //Classes.TipoDocumentoGlobus _tipoDocumento;
        Classes.ParametrosArquivei _parametro;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.NotasArquivei> _listaNotas;
        List<Classes.SequencialNFE> _listaSequencia;
         List<Classes.ItensParametrosArquivei> _itensParametros;
        List<Classes.ItensComparacao> _listaItens;
        List<Arquivei> _arquivei = new List<Arquivei>();
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;

        System.Xml.XmlTextWriter gravaColunasGridCabecalhoEncontradas;
        System.Xml.XmlTextWriter gravaCoresGridCabecalhoEncontradas;
        System.Xml.XmlTextWriter gravaColunasGridItensEncontradas;
        System.Xml.XmlTextWriter gravaCoresGridItensEncontradas;
        System.Xml.XmlReader leColunasGridCabecalhoEncontradas;
        System.Xml.XmlReader leCorGridCabecalhoEncontradas;
        System.Xml.XmlReader leColunasGridItensEncontradas;
        System.Xml.XmlReader leCorGridItensEncontradas;

        string _diretorioCorEncontrada = "";
        string _diretorioCabEncontrada = "";
        string _diretorioCorItensEncontrada = "";
        string _diretorioCabItensEncontrada = "";

        int codigoNotaFiscal = 0;

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

        private void ValidacaoXMLSaida_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            #region Define layout Grid
            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();
            GridDynamicFilter filter1 = new GridDynamicFilter();
            GridDynamicFilter filter2 = new GridDynamicFilter();
            GridDynamicFilter filter3 = new GridDynamicFilter();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(CabecalhoEncontradaGrid);

            filter1.ApplyFilterOnlyOnCellLostFocus = true;
            filter1.WireGrid(NFNaoEncontradaGrid_Novo);

            filter2.ApplyFilterOnlyOnCellLostFocus = true;
            filter2.WireGrid(CanceladasGrid);

            filter3.ApplyFilterOnlyOnCellLostFocus = true;
            filter3.WireGrid(SequenciaGrid);

            StringCollection StatusNF = new StringCollection();
            StatusNF.AddRange(new string[] { "Autorizado o uso da NF-e", "Cancelada", "Manifestada" });


            #region Integrar
            #region Notas
            CabecalhoEncontradaGrid.DataSource = new List<NotasArquivei>();

            CabecalhoEncontradaGrid.SortIconPlacement = SortIconPlacement.Left;
            CabecalhoEncontradaGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            CabecalhoEncontradaGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            CabecalhoEncontradaGrid.TableControl.CellToolTip.Active = true;
            CabecalhoEncontradaGrid.TopLevelGroupOptions.ShowFilterBar = true;
            CabecalhoEncontradaGrid.RecordNavigationBar.Label = "Notas";

            for (int i = 0; i < CabecalhoEncontradaGrid.TableDescriptor.Columns.Count; i++)
            {
                CabecalhoEncontradaGrid.TableDescriptor.Columns[i].ReadOnly = false;
                CabecalhoEncontradaGrid.TableDescriptor.Columns[i].AllowFilter = true;
                CabecalhoEncontradaGrid.TableDescriptor.Columns[i].AllowSort = true;
                CabecalhoEncontradaGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                CabecalhoEncontradaGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                CabecalhoEncontradaGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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
                this.CabecalhoEncontradaGrid.SetMetroStyle(metroColor);
                this.CabecalhoEncontradaGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.CabecalhoEncontradaGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.CabecalhoEncontradaGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.CabecalhoEncontradaGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.CabecalhoEncontradaGrid.Table.DefaultRecordRowHeight = 35;
            this.CabecalhoEncontradaGrid.TableControl.CellToolTip.InitialDelay = 100;
            this.CabecalhoEncontradaGrid.TableControl.CellToolTip.AutoPopDelay = 5000;
            CabecalhoEncontradaGrid.Refresh();

            #endregion

            #region Itens
            ItensEncontradosGrid.DataSource = new List<ItensNotasFiscaisServico>();

            ItensEncontradosGrid.SortIconPlacement = SortIconPlacement.Left;
            ItensEncontradosGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            ItensEncontradosGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            ItensEncontradosGrid.TableControl.CellToolTip.Active = true;
            ItensEncontradosGrid.TopLevelGroupOptions.ShowFilterBar = true;
            ItensEncontradosGrid.RecordNavigationBar.Label = "Itens";

            for (int i = 0; i < ItensEncontradosGrid.TableDescriptor.Columns.Count; i++)
            {
                ItensEncontradosGrid.TableDescriptor.Columns[i].ReadOnly = false;
                ItensEncontradosGrid.TableDescriptor.Columns[i].AllowFilter = true;
                ItensEncontradosGrid.TableDescriptor.Columns[i].AllowSort = true;
                ItensEncontradosGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                ItensEncontradosGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                ItensEncontradosGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.ItensEncontradosGrid.SetMetroStyle(metroColor);
                this.ItensEncontradosGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.ItensEncontradosGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }
            // para permitir editar dados.
            this.ItensEncontradosGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.ItensEncontradosGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            ItensEncontradosGrid.Refresh();

            #endregion
            #endregion

            #region Não encontradas
            #region Notas
            NFNaoEncontradaGrid_Novo.DataSource = new List<Arquivei>();

            NFNaoEncontradaGrid_Novo.SortIconPlacement = SortIconPlacement.Left;
            NFNaoEncontradaGrid_Novo.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            NFNaoEncontradaGrid_Novo.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            NFNaoEncontradaGrid_Novo.TableControl.CellToolTip.Active = true;
            NFNaoEncontradaGrid_Novo.TopLevelGroupOptions.ShowFilterBar = true;
            NFNaoEncontradaGrid_Novo.RecordNavigationBar.Label = "Notas";

            for (int i = 0; i < NFNaoEncontradaGrid_Novo.TableDescriptor.Columns.Count; i++)
            {
                NFNaoEncontradaGrid_Novo.TableDescriptor.Columns[i].AllowFilter = true;
                NFNaoEncontradaGrid_Novo.TableDescriptor.Columns[i].AllowSort = true;
                NFNaoEncontradaGrid_Novo.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                NFNaoEncontradaGrid_Novo.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                NFNaoEncontradaGrid_Novo.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            NFNaoEncontradaGrid_Novo.TableDescriptor.Columns["Status"].Appearance.AnyRecordFieldCell.CellType = GridCellTypeName.ComboBox;
            NFNaoEncontradaGrid_Novo.TableDescriptor.Columns["Status"].Appearance.AnyRecordFieldCell.ChoiceList = StatusNF;

            if (!Publicas._TemaBlack)
            {
                this.NFNaoEncontradaGrid_Novo.SetMetroStyle(metroColor);
                this.NFNaoEncontradaGrid_Novo.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.NFNaoEncontradaGrid_Novo.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.NFNaoEncontradaGrid_Novo.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.NFNaoEncontradaGrid_Novo.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.NFNaoEncontradaGrid_Novo.Table.DefaultRecordRowHeight = 35;
            NFNaoEncontradaGrid_Novo.Refresh();

            #endregion

            #region Itens
            //itensRevogarGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();

            //itensRevogarGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            //itensRevogarGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            //itensRevogarGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            //itensRevogarGridGroupingControl.TableControl.CellToolTip.Active = true;
            //itensRevogarGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            //itensRevogarGridGroupingControl.RecordNavigationBar.Label = "Itens";

            //for (int i = 0; i < itensGridGroupingControl.TableDescriptor.Columns.Count; i++)
            //{
            //    itensRevogarGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
            //    itensRevogarGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
            //    itensRevogarGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
            //    itensRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
            //    itensRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
            //    itensRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            //}

            //if (!Publicas._TemaBlack)
            //{
            //    this.itensRevogarGridGroupingControl.SetMetroStyle(metroColor);
            //    this.itensRevogarGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            //    this.itensRevogarGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            //}

            //// para permitir editar dados.
            //this.itensRevogarGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            //this.itensRevogarGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            //itensRevogarGridGroupingControl.Refresh();

            #endregion
            #endregion

            #region Canceladas
            CanceladasGrid.DataSource = new List<NotasArquivei>();

            CanceladasGrid.SortIconPlacement = SortIconPlacement.Left;
            CanceladasGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            CanceladasGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            CanceladasGrid.TableControl.CellToolTip.Active = true;
            CanceladasGrid.TopLevelGroupOptions.ShowFilterBar = true;
            CanceladasGrid.RecordNavigationBar.Label = "Notas";

            for (int i = 0; i < CanceladasGrid.TableDescriptor.Columns.Count; i++)
            {
                CanceladasGrid.TableDescriptor.Columns[i].ReadOnly = false;
                CanceladasGrid.TableDescriptor.Columns[i].AllowFilter = true;
                CanceladasGrid.TableDescriptor.Columns[i].AllowSort = true;
                CanceladasGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                CanceladasGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                CanceladasGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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
                this.CanceladasGrid.SetMetroStyle(metroColor);
                this.CanceladasGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.CanceladasGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.CanceladasGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.CanceladasGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.CanceladasGrid.Table.DefaultRecordRowHeight = 35;
            CanceladasGrid.Refresh();
            #endregion

            #region Sequencia
            SequenciaGrid.DataSource = new List<NotasArquivei>();

            SequenciaGrid.SortIconPlacement = SortIconPlacement.Left;
            SequenciaGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            SequenciaGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            SequenciaGrid.TableControl.CellToolTip.Active = true;
            SequenciaGrid.TopLevelGroupOptions.ShowFilterBar = true;
            SequenciaGrid.RecordNavigationBar.Label = "Notas";

            for (int i = 0; i < SequenciaGrid.TableDescriptor.Columns.Count; i++)
            {
                SequenciaGrid.TableDescriptor.Columns[i].ReadOnly = false;
                SequenciaGrid.TableDescriptor.Columns[i].AllowFilter = true;
                SequenciaGrid.TableDescriptor.Columns[i].AllowSort = true;
                SequenciaGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                SequenciaGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                SequenciaGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.SequenciaGrid.SetMetroStyle(metroColor);
                this.SequenciaGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.SequenciaGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.SequenciaGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.SequenciaGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.SequenciaGrid.Table.DefaultRecordRowHeight = 35;
            SequenciaGrid.Refresh();
            #endregion

            #region Guarda a estrutura do grid

            if (Environment.MachineName.ToUpper().Contains("CORPTS") || Environment.MachineName.ToUpper().Contains("CORPRDP"))
            {
                _diretorioCabEncontrada = Path.GetDirectoryName(Application.ExecutablePath) + @"\xml\GridCabecalhoEncontradas_saida" + Publicas._usuario.Id + ".xml";
                _diretorioCorEncontrada = Path.GetDirectoryName(Application.ExecutablePath) + @"\xml\CorGridCabecalhoEncontradas_saida" + Publicas._usuario.Id + ".xml";
                _diretorioCabItensEncontrada = Path.GetDirectoryName(Application.ExecutablePath) + @"\xml\GridItensEncontradas_saida" + Publicas._usuario.Id + ".xml";
                _diretorioCorItensEncontrada = Path.GetDirectoryName(Application.ExecutablePath) + @"\xml\CorGridItensEncontradas_saida" + Publicas._usuario.Id + ".xml";
            }
            else
            {
                _diretorioCabEncontrada = Publicas._caminhoPortal + "GridCabecalhoEncontradas_saida.xml";
                _diretorioCorEncontrada = Publicas._caminhoPortal + "CorGridCabecalhoEncontradas_saida.xml";
                _diretorioCabItensEncontrada = Publicas._caminhoPortal + "GridItensEncontradas_saida.xml";
                _diretorioCorItensEncontrada = Publicas._caminhoPortal + "CorGridItensEncontradas_saida.xml";
            }

            gravaColunasGridCabecalhoEncontradas = new System.Xml.XmlTextWriter(_diretorioCabEncontrada, System.Text.Encoding.UTF8);
            gravaCoresGridCabecalhoEncontradas = new System.Xml.XmlTextWriter(_diretorioCorEncontrada, System.Text.Encoding.UTF8);

            gravaColunasGridCabecalhoEncontradas.Formatting = System.Xml.Formatting.Indented;
            gravaCoresGridCabecalhoEncontradas.Formatting = System.Xml.Formatting.Indented;

            CabecalhoEncontradaGrid.WriteXmlSchema(gravaColunasGridCabecalhoEncontradas);
            CabecalhoEncontradaGrid.WriteXmlLookAndFeel(gravaCoresGridCabecalhoEncontradas);
            gravaColunasGridCabecalhoEncontradas.Close();
            gravaCoresGridCabecalhoEncontradas.Close();

            gravaColunasGridItensEncontradas = new System.Xml.XmlTextWriter(_diretorioCabItensEncontrada, System.Text.Encoding.UTF8);
            gravaCoresGridItensEncontradas = new System.Xml.XmlTextWriter(_diretorioCorItensEncontrada, System.Text.Encoding.UTF8);

            gravaColunasGridItensEncontradas.Formatting = System.Xml.Formatting.Indented;
            gravaCoresGridItensEncontradas.Formatting = System.Xml.Formatting.Indented;

            ItensEncontradosGrid.WriteXmlSchema(gravaColunasGridItensEncontradas);
            ItensEncontradosGrid.WriteXmlLookAndFeel(gravaCoresGridItensEncontradas);
            gravaColunasGridItensEncontradas.Close();
            gravaCoresGridItensEncontradas.Close();

            #endregion

            #endregion
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void inicialDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
            if (e.KeyCode == Keys.T)
                inicialDateTimePicker.Value = DateTime.Now.Date;
            if (e.KeyCode == Keys.U)
                inicialDateTimePicker.Value = new DateTime(inicialDateTimePicker.Value.Year, inicialDateTimePicker.Value.Month + 1, 1).AddDays(-1);
            if (e.KeyCode == Keys.P)
                inicialDateTimePicker.Value = new DateTime(inicialDateTimePicker.Value.Year, inicialDateTimePicker.Value.Month + 1, 1);
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void ApenasConferidasCheckBox_KeyDown(object sender, KeyEventArgs e)
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

        private void inicialDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void finalDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
            if (e.KeyCode == Keys.T)
                finalDateTimePicker.Value = DateTime.Now.Date;
            if (e.KeyCode == Keys.U)
                finalDateTimePicker.Value = new DateTime(finalDateTimePicker.Value.Year, finalDateTimePicker.Value.Month + 1, 1).AddDays(-1);
            if (e.KeyCode == Keys.P)
                finalDateTimePicker.Value = new DateTime(finalDateTimePicker.Value.Year, finalDateTimePicker.Value.Month + 1, 1);
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

            _parametro = new ParametrosArquiveiBO().Consultar(_empresa.IdEmpresa);
            if (!_parametro.Existe)
            {
                new Notificacoes.Mensagem("Empresa não parametrizada para o Arquivei.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            ExportaExcelPictureBox.Enabled = !string.IsNullOrEmpty(_parametro.DiretorioExportacao);
            ExportarExcelPictureBox.Enabled = !string.IsNullOrEmpty(_parametro.DiretorioExportacao);

            _itensParametros = new ItensParametrosArquiveiBO().Listar(_parametro.Id);
        }

        private void inicialDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            finalDateTimePicker.Value = inicialDateTimePicker.Value;
        }

        private void finalDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (finalDateTimePicker.Value.Date < inicialDateTimePicker.Value.Date) 
            {
                new Notificacoes.Mensagem("Data final não pode ser menor que a data inicial.", Publicas.TipoMensagem.Alerta).ShowDialog();
                inicialDateTimePicker.Focus();
                return;
            }

            if (string.IsNullOrEmpty(empresaComboBoxAdv.Text))
            {
                new Notificacoes.Mensagem("Selecione a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            mensagemSistemaLabel.Text = "Pesquisando, Aguarde ...";
            this.Cursor = Cursors.WaitCursor;
            Refresh();

            string _conferidas = (ApenasConferidasCheckBox.Checked ? "S" : (ApenasNaoConferidasCheckBox.Checked ? "N" : "T"));

            CabecalhoEncontradaGrid.DataSource = new List<Classes.NotasArquivei>();
            CanceladasGrid.DataSource = new List<Classes.NotasArquivei>();
            ItensEncontradosGrid.DataSource = new List<Classes.ItensComparacao>();
            NFNaoEncontradaGrid_Novo.DataSource = new List<Classes.Arquivei>();
            SequenciaGrid.DataSource = new List<Classes.Arquivei>();

            _listaNotas = new ArquiveiBO().ListarParaComparar(_empresa.IdEmpresa, inicialDateTimePicker.Value, finalDateTimePicker.Value, _conferidas, "Emitidos");
            _listaSequencia = new ArquiveiBO().SequencialNFe_Emitidas(_empresa.IdEmpresa, inicialDateTimePicker.Value, finalDateTimePicker.Value);
            SequenciaGrid.DataSource = _listaSequencia;
            _listaItens = new ArquiveiBO().ListarItensArquivei(_empresa.IdEmpresa, inicialDateTimePicker.Value, finalDateTimePicker.Value, _parametro.Id, "Emitidos");
                       
            leColunasGridCabecalhoEncontradas = new System.Xml.XmlTextReader(_diretorioCabEncontrada);
            CabecalhoEncontradaGrid.ApplyXmlSchema(leColunasGridCabecalhoEncontradas);

            leCorGridCabecalhoEncontradas = new System.Xml.XmlTextReader(_diretorioCorEncontrada);
            CabecalhoEncontradaGrid.ApplyXmlLookAndFeel(leCorGridCabecalhoEncontradas);

            leColunasGridCabecalhoEncontradas.Close();
            leCorGridCabecalhoEncontradas.Close();

            leColunasGridItensEncontradas = new System.Xml.XmlTextReader(_diretorioCabItensEncontrada);
            ItensEncontradosGrid.ApplyXmlSchema(leColunasGridItensEncontradas);

            leCorGridItensEncontradas = new System.Xml.XmlTextReader(_diretorioCorItensEncontrada);
            ItensEncontradosGrid.ApplyXmlLookAndFeel(leCorGridItensEncontradas);

            leColunasGridItensEncontradas.Close();
            leCorGridItensEncontradas.Close();

            //definir as colunas do grig conforme itens de parametros 
            #region cabeçalho notas encontradas
            foreach (var item in _itensParametros.Where(w => w.Tipo == "C"))
            {
                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.BairroDestinatario, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("BairroEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("BairroEmpresaGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("BairroValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("BairroEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("BairroEmpresaGlobus");

                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("BairroValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("BairroValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.BaseICMS, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("BaseICMSArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("BaseICMSGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("BaseICMSValida");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("BaseICMSArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("BaseICMSGlobus");

                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("BaseICMSValida");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("BaseICMSValida");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.CEPDestinatario, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CEPEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CEPEmpresaRapido");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CEPValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CEPEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CEPEmpresaRapido");

                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CEPValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CEPValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ChaveDeAcesso, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ChaveAcessoArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ChaveAcessoGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ChaveAcessoValida");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("ChaveAcessoArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("ChaveAcessoGlobus");

                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("ChaveAcessoValida");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ChaveAcessoValida");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.CNPJDestinatario, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CNPJEmpresaValido");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CNPJEmpresaGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CNPJEmpresaValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CNPJEmpresaValido");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CNPJEmpresaGlobus");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CNPJEmpresaValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CNPJEmpresaValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.CNPJEmitente, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CNPJFornecedorArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CNPJFornecedor");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CNPJFornecedorValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CNPJFornecedorArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CNPJFornecedor");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CNPJFornecedorValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CNPJFornecedorValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.DadosAdicionais, ""))
                {
                    if (!item.ExibirCampo)
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("DadosAdicionais");
                    else
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("DadosAdicionais");
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.DataEmissao, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("EmissaoArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("EmissaoGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("EmissaoValida");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("EmissaoArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("EmissaoGlobus");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("EmissaoValida");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("EmissaoValida");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.EnderecoDestinatario, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("EnderecoEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("EnderecoEmpresaGlobus");

                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("NumeroEnderecoEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("NumeroEnderecoGlobus");

                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("EnderecoValido");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("NumeroEnderecoValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("EnderecoEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("EnderecoEmpresaGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("NumeroEnderecoEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("NumeroEnderecoGlobus");

                        if (item.ValidarCampo)
                        {
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("EnderecoValido");
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("NumeroEnderecoValido");
                        }
                        else
                        {
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("EnderecoValido");
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("NumeroEnderecoValido");
                        }
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.IEDestinatario, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("IEEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("IEEmpresaGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("IEEmpresaValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("IEEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("IEEmpresaGlobus");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("IEEmpresaValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("IEEmpresaValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.IEEmitente, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("IEFornecedorArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("IEFornecedor");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("IEFornecedorValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("IEFornecedorArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("IEFornecedor");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("IEFornecedorValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("IEFornecedorValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ModeloNF, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CodigoModeloArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("CodigoModeloGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ModeloValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CodigoModeloArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("CodigoModeloGlobus");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("ModeloValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ModeloValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.NaturezaOperacao, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("NaturezaOperacaoArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("NaturezaOperacaoGlobus");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("NaturezaOperacaoArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("NaturezaOperacaoGlobus");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.NumeroNF, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("NumeroNFArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("NumeroNFGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("NumeroNFValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("NumeroNFArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("NumeroNFGlobus");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("NumeroNFValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("NumeroNFValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.RazaoSocialDestinatario, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("RazaoSocialEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("RazaoSocialEmpresaGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("RazaoSocialEmpresaValida");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("RazaoSocialEmpresaArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("RazaoSocialEmpresaGlobus");

                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("RazaoSocialEmpresaValida");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("RazaoSocialEmpresaValida");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.RazaoSocialEmitente, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("RazaoSocialFornecedorArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("RazaoSocialFornecedor");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("RazaoSocialFornecedorValida");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("RazaoSocialFornecedorArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("RazaoSocialFornecedor");

                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("RazaoSocialFornecedorValida");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("RazaoSocialFornecedorValida");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.Serie, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("SerieArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("SerieGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("SerieValida");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("SerieArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("SerieGlobus");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("SerieValida");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("SerieValida");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ValorProduto, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ValorProdutosArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ValorProdutosGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ValorProdutoValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("ValorProdutosArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("ValorProdutosGlobus");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("ValorProdutoValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ValorProdutoValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ValorTotalNF, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ValorTotalNFArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("ValorTotalNFGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("TotalNFValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("ValorTotalNFArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("ValorTotalNFGlobus");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("TotalNFValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("TotalNFValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.MunicipioOrigem, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("MunicipioOrigemGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("MunicipioOrigemArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("MunicipioOrigemValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("MunicipioOrigemArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("MunicipioOrigemGlobus");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("MunicipioOrigemValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("MunicipioOrigemValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.MunicipioDestino, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("MunicipioDestinoGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("MunicipioDestinoArquivo");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("MunicipioDestinoValido");
                    }
                    else
                    {
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("MunicipioDestinoGlobus");
                        CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("MunicipioDestinoArquivo");
                        if (item.ValidarCampo)
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Add("MunicipioDestinoValido");
                        else
                            CabecalhoEncontradaGrid.TableDescriptor.VisibleColumns.Remove("MunicipioDestinoValido");
                    }
                }
            }
            #endregion

            #region Itens notas encontradas
            foreach (var item in _itensParametros.Where(w => w.Tipo == "I"))
            {
                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.CFOP, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CFOP");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CFOPGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CFOPComparar");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CFOPValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("CFOP");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("CFOPComparar");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("CFOPGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("CFOPValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CFOPValido");
                    }
                }
                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.CSTICMS, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CSTICMS");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CSTGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CSTComparar");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CSTValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("CSTICMS");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("CSTComparar");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("CSTGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("CSTValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CSTValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.OperacaoFiscal, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("Operacao");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("OperacaoGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("OperacaoComparar");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("OperacaoValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("Operacao");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("OperacaoComparar");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("OperacaoGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("OperacaoValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("OperacaoValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorICMS, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ValorICMS");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ValorICMSGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ICMSValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("ValorICMS");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("ValorICMSGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("ICMSValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ICMSValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.AliquotaICMS, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("AliquotaICMS");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("AliquotaICMSGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("AliquotaValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("AliquotaICMS");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("AliquotaICMSGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("AliquotaValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("AliquotaValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorICMSST, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ValorICMSSub");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ValorICMSSubGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ICMSSTValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("ValorICMSSub");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("ValorICMSSubGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("ICMSSTValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ICMSSTValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorIPI, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ValorIPI");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ValorIPIGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("IPIValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("ValorIPI");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("ValorIPIGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("IPIValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("IPIValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.Desconto, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("Desconto");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("DescontoGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("DescontoValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("Desconto");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("DescontoGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("DescontoValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("DescontoValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.Seguro, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("Seguro");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("SeguroGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("SeguroValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("Seguro");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("SeguroGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("SeguroValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("SeguroValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.OutrasDespesas, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("OutrasDespesas");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("OutrasDespesasGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("OutrasDespesasValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("OutrasDespesas");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("OutrasDespesasGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("OutrasDespesasValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("OutrasDespesasValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorFrete, ""))
                {
                    if (!item.ExibirCampo)
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ValorFrete");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("ValorFreteGlobus");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("FreteValido");
                    }
                    else
                    {
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("ValorFrete");
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("ValorFreteGlobus");

                        if (item.ValidarCampo)
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("FreteValido");
                        else
                            ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("FreteValido");
                    }
                }

                if (item.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.CCe, ""))
                {
                    if (!item.ExibirCampo)
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Remove("CCe");
                    else
                        ItensEncontradosGrid.TableDescriptor.VisibleColumns.Add("CCe");
                }
            }
            #endregion

            _arquivei = new List<Arquivei>();

            foreach (var item in _listaNotas)
            {
                Arquivei _arq = new Arquivei();

                _arq.Id = item.IdArquivei;
                _arq.Existe = true;
                _arq.ComDiferencas = item.ComDiferenca;
                _arq.CodDoctoESF = item.CodDoctoEsf;
                _arq.CodIntNF = item.CodIntNF;
                _arq.IdUsuarioVisualizou = Publicas._idUsuario;
                _arq.Liberado = item.Liberado;
                _arq.Observacao = item.Observacao;
                _arquivei.Add(_arq);
            }

            List<ItensArquivei> _itensArquivo = new List<ItensArquivei>();
            foreach (var item in _listaItens)
            {
                if (item.ComDiferencas)
                {
                    foreach (var itemUp in _listaNotas.Where(w => w.IdArquivei == item.IdArquivei))
                    {
                        itemUp.ComDiferenca = true;
                    }
                }
                ItensArquivei _itens = new ItensArquivei();

                _itens.Id = item.Id;
                _itens.Existe = true;
                _itens.IdArquivei = item.IdArquivei;
                _itens.ComDiferencas = item.ComDiferencas;

                _itensArquivo.Add(_itens);
            }

            new ArquiveiBO().Gravar(_arquivei, _itensArquivo);

            if (ComDiferencaRadioButton.Checked)
                CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

            if (ComDiferencaRadioButton.Checked)
                CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

            if (NaoIntegradasRadioButton.Checked)
                CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.IntegradaLivro && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

            if (TodasRadioButton.Checked)
                CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

            CanceladasGrid.DataSource = _listaNotas.Where(w => w.Status.Contains("Cancel") || w.Status.Contains("Inutil")).ToList();

            // Lista os não encontrados
            _arquivei = new ArquiveiBO().Listar(inicialDateTimePicker.Value, finalDateTimePicker.Value, _empresa.IdEmpresa);

            NFNaoEncontradaGrid_Novo.DataSource = _arquivei.Where(w => w.TipoProcessamento == "Emitidos").ToList();

            tabPageAdv2.TabVisible = _listaSequencia.Count() != 0;
            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            ComDiferencaRadioButton.Enabled = true;
            SemDiferencaRadioButton.Enabled = true;
            NaoIntegradasRadioButton.Enabled = true;
            TodasRadioButton.Enabled = true;

            CabecalhoEncontradaGrid.Focus();
            Refresh();
        }

        private void CabecalhoEncontradaGrid_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            Record dr;
            try
            {
                if (e.TableCellIdentity.RowIndex != -1)
                {

                    GridRecordRow rec = this.CabecalhoEncontradaGrid.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        dr = rec.GetRecord() as Record;
                        if (dr != null && (bool)dr["ComDiferenca"])
                        {
                            e.Style.TextColor = Color.Red;
                        }

                        if (e.TableCellIdentity.Column.MappingName == "ChaveAcessoGlobus" || e.TableCellIdentity.Column.MappingName == "ChaveAcessoArquivo")
                        {
                            if (dr != null && (string)dr["ChaveAcessoGlobus"] != (string)dr["ChaveAcessoArquivo"])
                                e.Style.BackColor = Color.Khaki;
                        }

                        if (e.TableCellIdentity.Column.MappingName.Contains("EmpresaArquivo"))
                        {
                            e.Style.CellTipText = (string)dr["TipoTomadorDestinatario"];
                        }
                        
                        if (e.TableCellIdentity.Column.MappingName.Contains("Conferido"))
                        {

                            e.Style.CellTipText = "Para Conferir as Notas:" + Environment.NewLine + Environment.NewLine +
                                                   "1º Devem estar integradas com ESF." + Environment.NewLine + Environment.NewLine +
                                                   "2º Notas do Tipo DAN, além de integrada com ESF" + Environment.NewLine +
                                                   "devem estar integradas com CRC." + Environment.NewLine + Environment.NewLine +
                                                   "3º Não deve existir diferenças";
                        }

                        if (e.TableCellIdentity.Column.MappingName.Contains("IntegradoCPG"))
                        {

                            if ((string)dr["TipoDocumento"] == "DAN" && !(bool)dr["IntegradoCPG"])
                                e.Style.CellTipText = "Notas do Tipo DAN, além de integrada com ESF" + Environment.NewLine +
                                                   "devem estar integradas com CRC.";
                        }
                    }
                }
            }
            catch { }
        }

        private void CabecalhoEncontradaGrid_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.CabecalhoEncontradaGrid.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    codigoNotaFiscal = (int)dr["IdArquivei"];

                    ItensEncontradosGrid.DataSource = _listaItens.Where(w => w.IdArquivei == codigoNotaFiscal).ToList();

                    conferirToolStripMenuItem.Enabled = (((bool)dr["IntegradoCPG"] || (string)dr["TipoDocumento"] != "DAN") &&
                         !(bool)dr["Diferenca"] && (bool)dr["IntegradaLivro"]);
                    
                }
            }
        }

        private void CabecalhoEncontradaGrid_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.CabecalhoEncontradaGrid.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        codigoNotaFiscal = (int)dr["IdArquivei"];

                        ItensEncontradosGrid.DataSource = _listaItens.Where(w => w.IdArquivei == codigoNotaFiscal).ToList();

                        conferirToolStripMenuItem.Enabled = (((bool)dr["IntegradoCPG"] || (string)dr["TipoDocumento"] != "DAN") &&
                         !(bool)dr["Diferenca"] && (bool)dr["IntegradaLivro"]);
                    }
                }
            }
            catch { }
        }

        private void ItensEncontradosGrid_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            Record dr;
            try
            {
                if (e.TableCellIdentity.RowIndex != -1)
                {
                    GridRecordRow rec = this.ItensEncontradosGrid.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        dr = rec.GetRecord() as Record;
                        if (dr != null && (bool)dr["ComDiferencas"])
                        {
                            e.Style.TextColor = Color.Red;
                        }
                    }
                }
            }
            catch { }
        }

        private void ComDiferencaRadioButton_CheckChanged(object sender, EventArgs e)
        {
            ItensEncontradosGrid.DataSource = new List<Classes.ItensComparacao>();
            if (_listaNotas != null)
            {
                if (ComDiferencaRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (SemDiferencaRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (NaoIntegradasRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.IntegradaLivro && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (TodasRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.Status.Contains("Cancel") & !w.Status.Contains("Inutil")).ToList();

            }
        }


        private void ExportaExcelPictureBox_MouseHover(object sender, EventArgs e)
        {
            ExportaExcelPictureBox.Cursor = Cursors.Hand;
            ExportaExcelPictureBox.BackColor = Publicas._panelTituloFoco;
        }

        private void ExportaExcelPictureBox_MouseLeave(object sender, EventArgs e)
        {
            ExportaExcelPictureBox.Cursor = Cursors.Default;
            ExportaExcelPictureBox.BackColor = Publicas._panelTitulo;
        }

        private void ExportaExcelPictureBox_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(_parametro.DiretorioExportacao))
            {
                new Notificacoes.Mensagem("Diretório '" + _parametro.DiretorioExportacao + "' não existe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;
            string nomeArquivo = "Saidas_Encontradas_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" + inicialDateTimePicker.Value.ToShortDateString().Replace("/", "_")
                               + "_" + finalDateTimePicker.Value.ToShortDateString().Replace("/", "_");
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            List<Classes.NotasArquivei> _Notas = new List<NotasArquivei>();

            if (ComDiferencaRadioButton.Checked)
                _Notas = _listaNotas.Where(w => w.ComDiferenca).ToList();

            if (SemDiferencaRadioButton.Checked)
                _Notas = _listaNotas.Where(w => !w.ComDiferenca).ToList();

            if (NaoIntegradasRadioButton.Checked)
                _Notas = _listaNotas.Where(w => !w.IntegradaLivro).ToList();

            if (TodasRadioButton.Checked)
                _Notas = _listaNotas;


            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int linha = 1;
            int col = 1;

            mensagemSistemaLabel.Text = "Exportando dados para o Excel, aguarde...";
            this.Cursor = Cursors.WaitCursor;

            #region titulo colunas
            xlWorkSheet.Cells[linha, col] = "Tipo ";
            col++;

            xlWorkSheet.Cells[linha, col] = "Tipo Docto not Globus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Origem no Globus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Entrada no Globus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Chave de Acesso no Arquivo";
            col++;
            xlWorkSheet.Cells[linha, col] = "Chave de Acesso no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "CNPJ Emitente no Arquivo";
            col++;
            xlWorkSheet.Cells[linha, col] = "CNPJ Emitente no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "IE Emitente no Arquivo";
            col++;
            xlWorkSheet.Cells[linha, col] = "IE Emitente no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Razão Social no Arquivo";
            col++;
            xlWorkSheet.Cells[linha, col] = "Razão Social no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Número NF no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Número NF no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Série no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Série no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Data de Emissão no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Data de Emissão no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base ICMS no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base ICMS no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Total NF no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Total NF no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor Produto no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor Produto no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Modelo NF no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Modelo NF no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Status no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Operação no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Dados Adicionais";
            col++;
            xlWorkSheet.Cells[linha, col] = "CNPJ Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "CNPJ Destinátario no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "IE Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "IE Destinátario no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Razão Social Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Razão Social Destinátario no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Endereço Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Endereço Destinátario no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Número Endereço Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Número Endereço Destinátario no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Bairro Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Bairro Destinátario no Gobus";
            col++;
            xlWorkSheet.Cells[linha, col] = "CEP Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "CEP Destinátario no Gobus";
            col++;

            #endregion

            foreach (var itemC in _Notas)
            {
                col = 1;
                linha++;

                #region Cabeçalho da Nota
                xlWorkSheet.Cells[linha, col] = "Cabeçalho Nota Fiscal";
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.TipoDocumento;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Origem;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Entrada;
                col++;
                xlWorkSheet.Cells[linha, col] = "' " + itemC.ChaveAcessoArquivo.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = "' " + itemC.ChaveAcessoGlobus.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CNPJFornecedorArquivo.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CNPJFornecedor.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = "' " + itemC.IEFornecedorArquivo.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = "' " + itemC.IEFornecedor.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RazaoSocialFornecedorArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RazaoSocialFornecedor;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.NumeroNFArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.NumeroNFGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.SerieArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.SerieGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.EmissaoArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.EmissaoGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseICMSArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseICMSGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorTotalNFArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorTotalNFGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorProdutosArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorProdutosGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CodigoModeloArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CodigoModeloGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Status;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Operacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.DadosAdicionais;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CNPJEmpresaArquivo.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CNPJEmpresaGlobus.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = "' " + itemC.IEEmpresaArquivo.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = "' " + itemC.IEEmpresaGlobus.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.EnderecoEmpresaArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.EnderecoEmpresaGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.NumeroEnderecoEmpresaArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.NumeroEnderecoGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BairroEmpresaArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BairroEmpresaGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CEPEmpresaArquivo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CEPEmpresaGlobus;
                #endregion

                linha++;

                col = 2;
                #region Itens

                #region titulo colunas
                xlWorkSheet.Cells[linha, col] = "CFOP no Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "CFOP no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "Situação Tributária No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "Situação Tributária no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "Operação Fiscal No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "Operacao Fiscal no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "ICMS No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "ICMS no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "ICMS Substituição No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "ICMS Substituição no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "Aliquota No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "Aliquota no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "IPI No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "IPI no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "Desconto No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "Desconto no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "Seguro No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "Seguro no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "Outras Despesas No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "Outras Despesas no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "Frete No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "Frete no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "Total Itens No Arquivei";
                col++;
                xlWorkSheet.Cells[linha, col] = "Total Itens no Globus";
                col++;
                xlWorkSheet.Cells[linha, col] = "CCe No Arquivei";
                #endregion
                linha++;

                foreach (var itemI in _listaItens.Where(w => w.IdArquivei == itemC.IdArquivei))
                {
                    #region itens
                    col = 1;
                    xlWorkSheet.Cells[linha, col] = "Itens Nota Fiscal";
                    col++;

                    xlWorkSheet.Cells[linha, col] = itemI.CFOP;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.CFOPGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.CST;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.CSTGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.Operacao;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.OperacaoGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.ValorICMS;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.ValorICMSGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.ValorICMSSub;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.ValorICMSSubGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.AliquotaICMS;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.AliquotaICMSGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.ValorIPI;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.ValorIPIGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.Desconto;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.DescontoGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.Seguro;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.SeguroGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.OutrasDespesas;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.OutrasDespesasGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.ValorFrete;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.ValorFreteGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.ValorTotal;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.ValorTotalGlobus;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemI.CCe;

                    #endregion  
                    linha++;
                }
                #endregion

                linha++;
            }

            xlWorkSheet.Columns.AutoFit();
            xlWorkBook.SaveAs(_parametro.DiretorioExportacao + @"\" + nomeArquivo + ".xlsx", Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
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
            new Notificacoes.Mensagem("Arquivo gerado com sucesso.", Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void ExportarExcelPictureBox_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(_parametro.DiretorioExportacao))
            {
                new Notificacoes.Mensagem("Diretório '" + _parametro.DiretorioExportacao + "' não existe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;
            string nomeArquivo = "Saidas_NaoEncontradas_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_") + "_" + inicialDateTimePicker.Value.ToShortDateString().Replace("/", "_") + "_" + finalDateTimePicker.Value.ToShortDateString().Replace("/", "_");
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(Type.Missing);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int linha = 1;
            int col = 1;

            mensagemSistemaLabel.Text = "Exportando dados para o Excel, aguarde...";
            this.Cursor = Cursors.WaitCursor;

            #region titulo colunas
            xlWorkSheet.Cells[linha, col] = "Chave de Acesso no Arquivo";
            col++;
            xlWorkSheet.Cells[linha, col] = "CNPJ Emitente no Arquivo";
            col++;
            xlWorkSheet.Cells[linha, col] = "IE Emitente no Arquivo";
            col++;
            xlWorkSheet.Cells[linha, col] = "Razão Social no Arquivo";
            col++;
            xlWorkSheet.Cells[linha, col] = "Número NF no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Série no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Data de Emissão no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Base ICMS no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Total NF no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor Produto no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Modelo NF no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Status no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Operação no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Dados Adicionais";
            col++;
            xlWorkSheet.Cells[linha, col] = "CNPJ Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "IE Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Razão Social Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Endereço Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Número Endereço Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "Bairro Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "CEP Destinátario no Arquivei";
            col++;
            xlWorkSheet.Cells[linha, col] = "CFOP";
            col++;
            xlWorkSheet.Cells[linha, col] = "Natureza Operação";
            col++;
            #endregion

            foreach (var itemC in _arquivei)
            {
                col = 1;

                #region Cabeçalho da Nota
                linha++;

                xlWorkSheet.Cells[linha, col] = "' " + itemC.ChaveDeAcesso.ToString() + " ";
                //xlWorkSheet.Cells[linha, col].NumberFormat = "@";
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CNPJEmitente.ToString();
                //xlWorkSheet.Cells[linha, col].NumberFormat = "@";
                col++;
                xlWorkSheet.Cells[linha, col] = "' " + itemC.IEEmitente.ToString() + " ";
                //xlWorkSheet.Cells[linha, col].NumberFormat = "@";
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RazaoSocialEmitente;
                //xlWorkSheet.Cells[linha, col].NumberFormat = "@";
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.NumeroNF;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Serie;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.DataEmissao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BaseICMS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorTotalNF;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorProduto;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ModeloNF;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Status;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Operacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.DadosAdicionais;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CNPJDestinatario.ToString();
                col++;
                xlWorkSheet.Cells[linha, col] = "' " + itemC.IEDestinatario.ToString() + " ";
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RazaoSocialDestinatario;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.EnderecoDestinatario;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.NumeroEndDestinatario;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.BairroDestinatario;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CEPDestinatario;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CFOP;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.NaturezaOperacao;
                #endregion

                linha++;
            }

            xlWorkSheet.Columns.AutoFit();

            xlWorkBook.SaveAs(_parametro.DiretorioExportacao + @"\" + nomeArquivo + ".xlsx", Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
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
            new Notificacoes.Mensagem("Arquivo gerado com sucesso.", Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void ValidacaoArquivei_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }

        private void enviarEmailPictureBox_Click(object sender, EventArgs e)
        {
            string detalhe = "";
            List<Arquivei> _listaNfEmail = new List<Arquivei>();

            if (_arquivei.Where(w => w.Marcado).Count() == 0)
            {
                new Notificacoes.Mensagem("Selecione a nota que deve ser incluída no E-mail.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            _listaNfEmail.AddRange(_arquivei.Where(w => w.Marcado));

            foreach (var item in _listaNfEmail.GroupBy(g => new { g.NumeroNF, g.ChaveDeAcesso, g.Serie, g.DataEmissao, g.CNPJEmitente, g.TipoDocto }).OrderBy(o => o.Key.DataEmissao))
            {

                detalhe = detalhe + "<tr> " +
                                    "<td style='font-size: 12px'; align='Left='>" + item.Key.DataEmissao.ToShortDateString() + "&nbsp;</td> " +
                                    "<td style='font-size: 12px'; align='Left'> " + item.Key.ChaveDeAcesso + "</td>" +
                                    "<td style='font-size: 12px'; align='Left'> " + item.Key.NumeroNF + "&nbsp;</td> " +
                                    "<td style='font-size: 12px'; align='Left'> " + item.Key.Serie + "&nbsp;</td> " +
                                    "<td style='font-size: 12px'; align='Left'> " + item.Key.TipoDocto + "&nbsp;</td> " +
                                    "<td style='font-size: 12px'; align='Left'> " + item.Key.CNPJEmitente + "&nbsp;</td> " +
                                    "</tr>";


            }

            #region Envia Email

            string[] _dadosEmail = new string[50];
            _dadosEmail[0] = empresaComboBoxAdv.Text;
            _dadosEmail[1] = detalhe;

            string emailDestino = "";
            string emailCopia = "";

            List<Usuario> _listaUsuarios = new List<Usuario>();

            try
            {
                _listaUsuarios = new UsuarioBO().ListarUsuarios(true);
            }
            catch
            {
                new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            _listaEmpresasAutorizadas = new UsuarioBO().ConsultaUsuarioPorEmpresaAutorizada(_empresa.IdEmpresa);

            if (TestarEmailCheckBox.Checked)
                emailDestino = Publicas._usuario.Email;
            else
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

            if (_listaNfEmail.Count != 0)
            {
                Publicas.mensagemDeErro = "";
                Classes.Publicas.EnviarEmailNotasNaoLancadas(_dadosEmail, Publicas._usuario.Email, emailDestino, emailCopia, "Notas Fiscais não lançadas no Globus");

                if (Publicas.mensagemDeErro != "")
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Alerta).ShowDialog();
                else
                    new Notificacoes.Mensagem("E-mail enviado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();
            }

            #endregion

            //Atualiza o status das notas não encontradas
            new ArquiveiBO().GravarStatus(_arquivei);

            new Notificacoes.Mensagem("Processo finalizado.", Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new ArquiveiBO().GravarStatus(_arquivei);

            new Notificacoes.Mensagem("Gravado com sucesso.", Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void AplicarFiltroEmailCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AplicarFiltroEmailCheckBox.Checked)
            {
                NFNaoEncontradaGrid_Novo.DataSource = new List<Arquivei>();
                NFNaoEncontradaGrid_Novo.DataSource = _arquivei.Where(w => ((w.CFOP >= 1900 && w.CFOP <= 1999) || (w.CFOP >= 2900 && w.CFOP <= 2999)) &&
                                           w.Status.ToUpper().Contains("AUTOR") &&
                                           w.Tipo.ToUpper().Contains("sa".ToUpper()) &&
                                           w.TipoProcessamento == "Emitidos").ToList();
            }
            else
            {
                NFNaoEncontradaGrid_Novo.DataSource = new List<Arquivei>();
                NFNaoEncontradaGrid_Novo.DataSource = _arquivei;
            }
        }

        private void NFNaoEncontradaGrid_Novo_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            Record dr;
            try
            {
                if (e.TableCellIdentity.RowIndex != -1)
                {

                    GridRecordRow rec = this.NFNaoEncontradaGrid_Novo.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        dr = rec.GetRecord() as Record;

                        if (e.TableCellIdentity.Column.MappingName.Contains("Emitente"))
                        {
                            e.Style.CellTipText = (string)dr["TipoTomadorDestinatario"];
                        }
                    }
                }
            }
            catch { }
        }

        private void NFNaoEncontradaGrid_Novo_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridTableCellStyleInfoIdentity style = this.NFNaoEncontradaGrid_Novo.TableModel[_colunaCorrente.RowIndex, _colunaCorrente.ColIndex].TableCellIdentity;

            string nomeColuna = "";

            if (style.TableCellType == GridTableCellType.RecordFieldCell || style.TableCellType == GridTableCellType.AlternateRecordFieldCell)
                nomeColuna = style.Column.Name;
        }

        private void NFNaoEncontradaGrid_Novo_TableControlCellMouseDown(object sender, GridTableControlCellMouseEventArgs e)
        {
            _colunaCorrente = NFNaoEncontradaGrid_Novo.TableControl.CurrentCell;
        }

        private void LiberarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ObservacaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ObservacaoPanel.Visible = false;
            }
        }

        private void ObservacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ConferircheckBox.Focus();
            }
        }

        private void ObservacaoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void ObservacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void ObservacaoTextBox_TextChanged(object sender, EventArgs e)
        {
            totalLabel.Text = ObservacaoTextBox.TextLength + " / " + ObservacaoTextBox.MaxLength;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            Arquivei _arq = new Arquivei();

            ItensEncontradosGrid.DataSource = new List<Classes.ItensComparacao>();
            CabecalhoEncontradaGrid.DataSource = new List<NotasArquivei>();
            bool liberadoAntes = false;
            bool conferidoAntes = false;

            foreach (var item in _listaNotas.Where(w => w.IdArquivei == codigoNotaFiscal))
            {
                if (!item.IntegradaLivro )
                {
                    new Notificacoes.Mensagem("Documento não integrado com Escrituração fiscal.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }


                conferidoAntes = item.Conferido;
                liberadoAntes = item.Liberado;
                item.Liberado = LiberarCheckBox.Checked;
                item.Observacao = ObservacaoTextBox.Text;
                item.Conferido = ConferircheckBox.Checked;

                _arq = new ArquiveiBO().Consultar(_empresa.IdEmpresa, "", codigoNotaFiscal);
                _arq.Liberado = LiberarCheckBox.Checked;
                _arq.Observacao = ObservacaoTextBox.Text;
                _arq.Conferido = ConferircheckBox.Checked;
                _arq.NumeroNF = item.NumeroNFArquivo;
                _arq.Origem = item.Origem;
                _arq.IntegradoCPG = item.IntegradoCPG;
                _arq.IntegradoESF = item.IntegradaLivro;
            }

            List<Arquivei> _lista = new List<Arquivei>();
            _lista.Add(_arq);

            ObservacaoTextBox.Text = string.Empty;
            ObservacaoPanel.Visible = false;

            if (_listaNotas != null)
            {
                if (ComDiferencaRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (SemDiferencaRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (NaoIntegradasRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.IntegradaLivro && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (TodasRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

            }

            CabecalhoEncontradaGrid.Refresh();

            if (!new ArquiveiBO().Gravar(_lista, new List<ItensArquivei>(), true))
            {
                new Notificacoes.Mensagem("Problemas durante a atualização.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (_arq.Conferido && !conferidoAntes ? "Conferiu nota fiscal " + _arq.NumeroNF  : "")
                           + (!_arq.Conferido && conferidoAntes ? "Cancelou a conferência da nota fiscal " : "")
                           + " empresa " + empresaComboBoxAdv.Text + " período de " + inicialDateTimePicker.Value.ToShortDateString() + " até " + finalDateTimePicker.Value.ToShortDateString();

            _log.Tela = "Escrituração Fiscal - Arquivei - Validação XML x Globus - Saida";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
        }

        private void conferirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Arquivei _arq = new Arquivei();
            bool liberadoAntes = false;
            bool conferidoAntes = false;

            foreach (var item in _listaNotas.Where(w => w.IdArquivei == codigoNotaFiscal))
            {
                if (!item.IntegradaLivro)
                {
                    new Notificacoes.Mensagem("Documento não integrado com Escrituração fiscal.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
                if (item.ComDiferenca)
                {
                    new Notificacoes.Mensagem("Documento com diferenças.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }

                if (!item.IntegradoCPG && item.TipoDocumento == "DAN")
                {
                    new Notificacoes.Mensagem("Documento 'DAN' não integrado com Contas a Receber.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
                conferidoAntes = item.Conferido;
                liberadoAntes = item.Liberado;

                item.Conferido = !item.Conferido;

                _arq = new ArquiveiBO().Consultar(_empresa.IdEmpresa, "", codigoNotaFiscal);
                _arq.Conferido = item.Conferido;
                _arq.Liberado = item.Liberado;
                _arq.Observacao = item.Observacao;
                _arq.Origem = item.Origem;
                _arq.IntegradoCPG = item.IntegradoCPG;
                _arq.IntegradoESF = item.IntegradaLivro;
            }

            ItensEncontradosGrid.DataSource = new List<Classes.ItensComparacao>();
            CabecalhoEncontradaGrid.DataSource = new List<NotasArquivei>();

            List<Arquivei> _lista = new List<Arquivei>();
            _lista.Add(_arq);

            ObservacaoTextBox.Text = string.Empty;
            ObservacaoPanel.Visible = false;

            if (_listaNotas != null)
            {
                if (ComDiferencaRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (SemDiferencaRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (NaoIntegradasRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.IntegradaLivro && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (TodasRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

            }

            CabecalhoEncontradaGrid.Refresh();

            if (!new ArquiveiBO().Gravar(_lista, new List<ItensArquivei>(), true))
            {
                new Notificacoes.Mensagem("Problemas durante a atualização.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (_arq.Conferido && !conferidoAntes ? "Conferiu nota fiscal " + _arq.NumeroNF  : "")
                           + (!_arq.Conferido && conferidoAntes ? "Cancelou a conferência da nota fiscal " + _arq.NumeroNF  : "")
                           + " empresa " + empresaComboBoxAdv.Text + " período de " + inicialDateTimePicker.Value.ToShortDateString() + " até " + finalDateTimePicker.Value.ToShortDateString();


            _log.Tela = "Escrituração Fiscal - Arquivei - Validação XML x Globus - Saida";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // marcar todos conferir
            Arquivei _arq = new Arquivei();
            bool liberadoAntes = false;
            bool conferidoAntes = false;
            string descricao = "";

            List<Arquivei> _lista = new List<Arquivei>();

            GridTable reg = CabecalhoEncontradaGrid.Table;

            if (reg.FilteredRecords.Count != 0)
            {
                foreach (var itemR in reg.FilteredRecords)
                {

                    int posIniId = 0;
                    int posFimId = 0;
                    int arquivei = 0;

                    try
                    {
                        posIniId = itemR.Info.IndexOf("IdArquivei =") + 12;
                        posFimId = itemR.Info.IndexOf(", IdEmpresa");
                        arquivei = Convert.ToInt32(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                        foreach (var item in _listaNotas.Where(w => w.IdArquivei == arquivei &&
                                             !w.ComDiferenca && w.IntegradaLivro && !w.Conferido &&
                                             (w.IntegradoCPG || w.TipoDocumento != "DAN")))
                        {
                            conferidoAntes = item.Conferido;
                            liberadoAntes = item.Liberado;

                            item.Conferido = true;

                            _arq = new ArquiveiBO().Consultar(_empresa.IdEmpresa, "", item.IdArquivei);
                            _arq.Conferido = item.Conferido;
                            _arq.Liberado = item.Liberado;
                            _arq.Observacao = item.Observacao;
                            _arq.Origem = item.Origem;
                            _arq.IntegradoCPG = item.IntegradoCPG;
                            _arq.IntegradoESF = item.IntegradaLivro;

                            _lista.Add(_arq);

                            descricao = descricao + "Conferiu nota fiscal " + _arq.NumeroNF +
                            Environment.NewLine;

                        }
                    }
                    catch { }
                }
            }
            else
            {
                foreach (var item in _listaNotas.Where(w => !w.ComDiferenca && w.IntegradaLivro && !w.Conferido &&
                                             (w.IntegradoCPG || w.TipoDocumento != "DAN")))
                {
                    conferidoAntes = item.Conferido;
                    liberadoAntes = item.Liberado;

                    item.Conferido = true;

                    _arq = new ArquiveiBO().Consultar(_empresa.IdEmpresa, "", item.IdArquivei);
                    _arq.Conferido = item.Conferido;
                    _arq.Liberado = item.Liberado;
                    _arq.Observacao = item.Observacao;
                    _arq.Origem = item.Origem;
                    _arq.IntegradoCPG = item.IntegradoCPG;
                    _arq.IntegradoESF = item.IntegradaLivro;

                    _lista.Add(_arq);

                    descricao = descricao + "Conferiu nota fiscal " + _arq.NumeroNF +
                               Environment.NewLine;

                }
            }
            ItensEncontradosGrid.DataSource = new List<Classes.ItensComparacao>();
            CabecalhoEncontradaGrid.DataSource = new List<NotasArquivei>();

            if (_listaNotas != null)
            {
                if (ComDiferencaRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (SemDiferencaRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (NaoIntegradasRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.IntegradaLivro && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (TodasRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

            }

            CabecalhoEncontradaGrid.Refresh();

            if (_lista.Count() == 0)
            {
                new Notificacoes.Mensagem("Documentos devem estar sem diferença e integrado com ESF para conferir usando a opção 'Marcar Todos'.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            if (!new ArquiveiBO().Gravar(_lista, new List<ItensArquivei>(), true))
            {
                new Notificacoes.Mensagem("Problemas durante a atualização.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = descricao +
                " empresa " + empresaComboBoxAdv.Text + " período de " + inicialDateTimePicker.Value.ToShortDateString() + " até " + finalDateTimePicker.Value.ToShortDateString();

            _log.Tela = "Escrituração Fiscal - Arquivei - Validação XML x Globus - Saida";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // demarcar todos conferir
            Arquivei _arq = new Arquivei();
            bool liberadoAntes = false;
            bool conferidoAntes = false;
            string descricao = "";

            List<Arquivei> _lista = new List<Arquivei>();

            GridTable reg = CabecalhoEncontradaGrid.Table;

            if (reg.FilteredRecords.Count != 0)
            {
                foreach (var itemR in reg.FilteredRecords)
                {

                    int posIniId = 0;
                    int posFimId = 0;
                    int arquivei = 0;

                    try
                    {
                        posIniId = itemR.Info.IndexOf("IdArquivei =") + 12;
                        posFimId = itemR.Info.IndexOf(", IdEmpresa");
                        arquivei = Convert.ToInt32(itemR.Info.Substring(posIniId, posFimId - posIniId).Trim());

                        foreach (var item in _listaNotas.Where(w => w.IdArquivei == arquivei && w.Conferido))
                        {
                            conferidoAntes = item.Conferido;
                            liberadoAntes = item.Liberado;

                            item.Conferido = false;

                            _arq = new ArquiveiBO().Consultar(_empresa.IdEmpresa, "", item.IdArquivei);
                            _arq.Conferido = item.Conferido;
                            _arq.Liberado = item.Liberado;
                            _arq.Observacao = item.Observacao;
                            _arq.Origem = item.Origem;
                            _arq.IntegradoCPG = item.IntegradoCPG;
                            _arq.IntegradoESF = item.IntegradaLivro;

                            _lista.Add(_arq);

                            descricao = descricao + "Cancelou conferencia da nota fiscal " + _arq.NumeroNF  +
                                       Environment.NewLine;

                        }

                    }
                    catch
                    { }
                }
            }
            else
            {
                foreach (var item in _listaNotas.Where(w => w.Conferido))
                {
                    conferidoAntes = item.Conferido;
                    liberadoAntes = item.Liberado;

                    item.Conferido = false;

                    _arq = new ArquiveiBO().Consultar(_empresa.IdEmpresa, "", item.IdArquivei);
                    _arq.Conferido = item.Conferido;
                    _arq.Liberado = item.Liberado;
                    _arq.Observacao = item.Observacao;
                    _arq.Origem = item.Origem;
                    _arq.IntegradoCPG = item.IntegradoCPG;
                    _arq.IntegradoESF = item.IntegradaLivro;

                    _lista.Add(_arq);

                    descricao = descricao + "Cancelou conferencia da nota fiscal " + _arq.NumeroNF  +
                               Environment.NewLine;

                }
            }

            ItensEncontradosGrid.DataSource = new List<Classes.ItensComparacao>();
            CabecalhoEncontradaGrid.DataSource = new List<NotasArquivei>();

            if (_listaNotas != null)
            {
                if (ComDiferencaRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (SemDiferencaRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.ComDiferenca && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (NaoIntegradasRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.IntegradaLivro && !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

                if (TodasRadioButton.Checked)
                    CabecalhoEncontradaGrid.DataSource = _listaNotas.Where(w => !w.Status.Contains("Cancel") && !w.Status.Contains("Inutil")).ToList();

            }

            CabecalhoEncontradaGrid.Refresh();

            if (!new ArquiveiBO().Gravar(_lista, new List<ItensArquivei>(), true))
            {
                new Notificacoes.Mensagem("Problemas durante a atualização.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = descricao +
                " empresa " + empresaComboBoxAdv.Text + " período de " + inicialDateTimePicker.Value.ToShortDateString() + " até " + finalDateTimePicker.Value.ToShortDateString();

            _log.Tela = "Escrituração Fiscal - Arquivei - Validação XML x Globus - Saida";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
        }
    }
}
