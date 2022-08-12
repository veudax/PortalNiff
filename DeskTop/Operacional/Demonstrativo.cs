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

namespace Suportte.Operacional
{
    public partial class Demonstrativo : Form
    {
        public Demonstrativo()
        {
            InitializeComponent();

            VigenciaDateTimePicker.Value = DateTime.Now.Date;
            if (Publicas._alterouSkin)
            {
                VigenciaDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;

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
                    VigenciaDateTimePicker.Style = VisualStyle.Office2016Black;
                    RecolherCheckBox.ForeColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;
        List<Classes.Operacional.Demonstrativo> _lista;
        DateTime _dataInicio;
        DateTime _dataFim;

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

        private void Demonstrativo_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);
            ativoCheckBox_CheckStateChanged(sender, new EventArgs());
            VigenciaDateTimePicker.Value = DateTime.Now.Date;

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

            GridSummaryRowDescriptor _soma;

            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor.DataMember = "Quadro";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "Quadro";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor2.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor2.DataMember = "FrotaP1";
            summaryColumnDescriptor2.Format = "{Sum}";
            summaryColumnDescriptor2.Name = "FrotaP1";
            summaryColumnDescriptor2.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor3 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor3.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor3.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor3.DataMember = "FrotaR1";
            summaryColumnDescriptor3.Format = "{Sum}";
            summaryColumnDescriptor3.Name = "FrotaR1";
            summaryColumnDescriptor3.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor4 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor4.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor4.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor4.DataMember = "FrotaP2";
            summaryColumnDescriptor4.Format = "{Sum}";
            summaryColumnDescriptor4.Name = "FrotaP2";
            summaryColumnDescriptor4.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor5 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor5.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor5.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor5.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor5.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor5.DataMember = "FrotaR2";
            summaryColumnDescriptor5.Format = "{Sum}";
            summaryColumnDescriptor5.Name = "FrotaR2";
            summaryColumnDescriptor5.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor6 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor6.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor6.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor6.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor6.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor6.DataMember = "FCVProg";
            summaryColumnDescriptor6.Format = "{Sum}";
            summaryColumnDescriptor6.Name = "FCVProg";
            summaryColumnDescriptor6.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor7 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor7.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor7.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor7.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor7.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor7.DataMember = "FCVReal";
            summaryColumnDescriptor7.Format = "{Sum}";
            summaryColumnDescriptor7.Name = "FCVReal";
            summaryColumnDescriptor7.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor8 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor8.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor8.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor8.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor8.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor8.DataMember = "FCVPercentual";
            summaryColumnDescriptor8.Format = "{Average}";
            summaryColumnDescriptor8.Name = "FCVPercentual";
            summaryColumnDescriptor8.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor9 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor9.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor9.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor9.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor9.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor9.DataMember = "FCVPerda";
            summaryColumnDescriptor9.Format = "{Sum}";
            summaryColumnDescriptor9.Name = "FCVPerda";
            summaryColumnDescriptor9.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor10 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor10.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor10.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor10.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor10.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor10.DataMember = "Pontual";
            summaryColumnDescriptor10.Format = "{Sum}";
            summaryColumnDescriptor10.Name = "Pontual";
            summaryColumnDescriptor10.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor11 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor11.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor11.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor11.Appearance.AnySummaryCell.Format = "n1";
            summaryColumnDescriptor11.Appearance.GroupCaptionSummaryCell.Format = "n1";
            summaryColumnDescriptor11.DataMember = "PontualPercentual";
            summaryColumnDescriptor11.Format = "{Average}";
            summaryColumnDescriptor11.Name = "PontualPercentual";
            summaryColumnDescriptor11.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor12 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor12.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor12.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor12.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor12.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor12.DataMember = "SAC";
            summaryColumnDescriptor12.Format = "{Sum}";
            summaryColumnDescriptor12.Name = "SAC";
            summaryColumnDescriptor12.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor13 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor13.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor13.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor13.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor13.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor13.DataMember = "Acidente";
            summaryColumnDescriptor13.Format = "{Sum}";
            summaryColumnDescriptor13.Name = "Acidente";
            summaryColumnDescriptor13.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor14 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor14.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor14.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor14.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor14.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor14.DataMember = "RA";
            summaryColumnDescriptor14.Format = "{Sum}";
            summaryColumnDescriptor14.Name = "RA";
            summaryColumnDescriptor14.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor15 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor15.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor15.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor15.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor15.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor15.DataMember = "SOS";
            summaryColumnDescriptor15.Format = "{Sum}";
            summaryColumnDescriptor15.Name = "SOS";
            summaryColumnDescriptor15.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor16 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor16.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor16.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor16.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor16.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor16.DataMember = "AbsenteismoMot";
            summaryColumnDescriptor16.Format = "{Sum}";
            summaryColumnDescriptor16.Name = "AbsenteismoMot";
            summaryColumnDescriptor16.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor17 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor17.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor17.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor17.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor17.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor17.DataMember = "AbsenteismoCob";
            summaryColumnDescriptor17.Format = "{Sum}";
            summaryColumnDescriptor17.Name = "AbsenteismoCob";
            summaryColumnDescriptor17.SummaryType = SummaryType.DoubleAggregate;
            
            GridSummaryColumnDescriptor summaryColumnDescriptor18 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor18.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor18.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor18.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor18.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor18.DataMember = "TotalAbsenteismo";
            summaryColumnDescriptor18.Format = "{Sum}";
            summaryColumnDescriptor18.Name = "TotalAbsenteismo";
            summaryColumnDescriptor18.SummaryType = SummaryType.DoubleAggregate;
            
            GridSummaryColumnDescriptor summaryColumnDescriptor19 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor19.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor19.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor19.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor19.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor19.DataMember = "Indice";
            summaryColumnDescriptor19.Format = "{Average}";
            summaryColumnDescriptor19.Name = "Indice";
            summaryColumnDescriptor19.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor20 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor20.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor20.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor20.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor20.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor20.DataMember = "RefeicaoProg";
            summaryColumnDescriptor20.Format = "{Sum}";
            summaryColumnDescriptor20.Name = "RefeicaoProg";
            summaryColumnDescriptor20.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor21 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor21.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor21.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor21.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor21.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor21.DataMember = "RefeicaoReal";
            summaryColumnDescriptor21.Format = "{Sum}";
            summaryColumnDescriptor21.Name = "RefeicaoReal";
            summaryColumnDescriptor21.SummaryType = SummaryType.DoubleAggregate;
            
            GridSummaryColumnDescriptor summaryColumnDescriptor22 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor22.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor22.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor22.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor22.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor22.DataMember = "RefeicaoPercentual";
            summaryColumnDescriptor22.Format = "{Average}";
            summaryColumnDescriptor22.Name = "RefeicaoPercentual";
            summaryColumnDescriptor22.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor23 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor23.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor23.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor23.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor23.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor23.DataMember = "PAX";
            summaryColumnDescriptor23.Format = "{Sum}";
            summaryColumnDescriptor23.Name = "PAX";
            summaryColumnDescriptor23.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor24 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor24.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor24.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor24.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor24.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor24.DataMember = "PVD";
            summaryColumnDescriptor24.Format = "{Average}";
            summaryColumnDescriptor24.Name = "PVD";
            summaryColumnDescriptor24.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor25 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor25.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor25.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor25.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor25.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor25.DataMember = "ReceitaTotal";
            summaryColumnDescriptor25.Format = "{Sum}";
            summaryColumnDescriptor25.Name = "ReceitaTotal";
            summaryColumnDescriptor25.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor26 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor26.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor26.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor26.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor26.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor26.DataMember = "ReceitaPorCarro";
            summaryColumnDescriptor26.Format = "{Average}";
            summaryColumnDescriptor26.Name = "ReceitaPorCarro";
            summaryColumnDescriptor26.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor27 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor27.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor27.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor27.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor27.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor27.DataMember = "Km";
            summaryColumnDescriptor27.Format = "{Sum}";
            summaryColumnDescriptor27.Name = "Km";
            summaryColumnDescriptor27.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor28 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor28.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor28.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor28.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor28.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor28.DataMember = "Consumo";
            summaryColumnDescriptor28.Format = "{Sum}";
            summaryColumnDescriptor28.Name = "Consumo";
            summaryColumnDescriptor28.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor29 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor29.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor29.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor29.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor29.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor29.DataMember = "Media";
            summaryColumnDescriptor29.Format = "{Average}";
            summaryColumnDescriptor29.Name = "Media";
            summaryColumnDescriptor29.SummaryType = SummaryType.DoubleAggregate;

            _soma = new GridSummaryRowDescriptor("Sum", "Total geral",
                new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor2, summaryColumnDescriptor3, summaryColumnDescriptor4,
                                                    summaryColumnDescriptor5, summaryColumnDescriptor6, summaryColumnDescriptor7, summaryColumnDescriptor8,
                                                    summaryColumnDescriptor9, summaryColumnDescriptor10, summaryColumnDescriptor11,
                                                    summaryColumnDescriptor12, summaryColumnDescriptor13, summaryColumnDescriptor14,
                                                    summaryColumnDescriptor15, summaryColumnDescriptor16, summaryColumnDescriptor17,
                                                    summaryColumnDescriptor18, summaryColumnDescriptor19, summaryColumnDescriptor20,
                                                    summaryColumnDescriptor21, summaryColumnDescriptor22, summaryColumnDescriptor23, summaryColumnDescriptor24,
                                                    summaryColumnDescriptor25, summaryColumnDescriptor26, summaryColumnDescriptor27, summaryColumnDescriptor28, summaryColumnDescriptor29

                });

            _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            gridGroupingControl1.TableDescriptor.SummaryRows.Add(_soma);

            this.gridGroupingControl1.ChildGroupOptions.ShowCaptionSummaryCells = true;
            this.gridGroupingControl1.ChildGroupOptions.ShowSummaries = false;
            this.gridGroupingControl1.ChildGroupOptions.CaptionSummaryRow = "Sum";
            this.gridGroupingControl1.Appearance.GroupCaptionCell.BackColor = this.gridGroupingControl1.Appearance.RecordFieldCell.BackColor;
            this.gridGroupingControl1.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            this.gridGroupingControl1.Appearance.GroupCaptionCell.CellType = "Static";
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gridGroupingControl1.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (VigenciaDateTimePicker.Visible)
                    VigenciaDateTimePicker.Focus();
                else
                    referenciaMaskedEditBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void VigenciaDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
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

        private void VigenciaDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
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

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            empresaComboBoxAdv.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }

            if (_listaEmpresasAutorizadas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.EmpresaAutoriza).Count() == 0)
            {
                new Notificacoes.Mensagem("Usuário não autorizado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                new Notificacoes.Mensagem("Informe o mês/Ano desejado.", Publicas.TipoMensagem.Alerta).ShowDialog();
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
                _dataInicio = Convert.ToDateTime("01/" + referenciaMaskedEditBox.Text);
                _dataFim = _dataInicio.AddMonths(1).AddDays(-1);
                    //Convert.ToDateTime("01/" + referenciaMaskedEditBox.Text).AddMonths(1).AddDays(-1);
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            DateTime _data = _dataInicio;

            _lista = new List<Classes.Operacional.Demonstrativo>();
            
            while (_data <= _dataFim)
            {
                mensagemSistemaLabel.Text = "Aguarde," + Environment.NewLine + "Pesquisando dia " + _data.ToShortDateString();
                this.Refresh();
                try
                {
                    _lista.AddRange(new OperacionalBO().ListarDemonstrativoMensal(_empresa.IdEmpresa, _data, _data));
                }
                catch { }
                _data = _data.AddDays(1);
            }
            gridGroupingControl1.DataSource = _lista.Where(w => w.CodigoLinha != null).ToList();
            mensagemSistemaLabel.Text = "";

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Consultou o demonstrativo operacional da empresa " + empresaComboBoxAdv.Text +
                " periodo de " + referenciaMaskedEditBox.Text;
            _log.Tela = "Demonstrativo Operacional";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
        }


        private void VigenciaDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            VigenciaDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _lista = new List<Classes.Operacional.Demonstrativo>();

            mensagemSistemaLabel.Text = "Aguarde," + Environment.NewLine + "Pesquisando dia " + VigenciaDateTimePicker.Value.ToShortDateString();
            this.Refresh();
            try
            {
                _lista.AddRange(new OperacionalBO().ListarDemonstrativo(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, VigenciaDateTimePicker.Value));
            }
            catch { }

            gridGroupingControl1.DataSource = _lista.Where(w => w.CodigoLinha != null).ToList();
            mensagemSistemaLabel.Text = "";

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Consultou o demonstrativo operacional da empresa " + empresaComboBoxAdv.Text +
                " para a data " + VigenciaDateTimePicker.Value.ToShortDateString();
            _log.Tela = "Demonstrativo Operacional";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            referenciaMaskedEditBox.Text = string.Empty;
            referenciaMaskedEditBox.Focus();
            gridGroupingControl1.DataSource = new List<Classes.Operacional.Demonstrativo>();
        }

        private void ativoCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            VigenciaDateTimePicker.Visible = !ativoCheckBox.Checked;
            label2.Visible = !ativoCheckBox.Checked;
            referenciaMaskedEditBox.Visible = ativoCheckBox.Checked;
            label1.Visible = ativoCheckBox.Checked;
        }

        private void RecolherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RecolherCheckBox.Checked)
                gridGroupingControl1.Table.CollapseAllGroups();
            else
                gridGroupingControl1.Table.ExpandAllGroups();
        }

        private void ExportaExcelPictureBox_Click(object sender, EventArgs e)
        {
            string caminho = (Environment.MachineName.ToUpper().Contains("CORPTS") || Environment.MachineName.ToUpper().Contains("CORPRDP") ? @"d:\portalNiff\Excel\" : @"c:\portalNiff\Excel\");

            if (!System.IO.Directory.Exists(caminho))
            {
                System.IO.Directory.CreateDirectory(caminho);
            }

            mensagemSistemaLabel.Text = "Aguarde, Exportando dados para o Excel";
            this.Cursor = Cursors.WaitCursor;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;
            string nomeArquivo = "DemonstrativoOperacional_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" + (ativoCheckBox.Checked ? referenciaMaskedEditBox.ClipText : VigenciaDateTimePicker.Value.ToShortDateString().Replace("/", "_"));

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);


            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int linha = 1;
            int col = 1;

            #region titulo colunas
            xlWorkSheet.Cells[linha, col] = "Data ";
            col++;

            xlWorkSheet.Cells[linha, col] = "Setor";
            col++;

            xlWorkSheet.Cells[linha, col] = "Linha ";
            col++;

            xlWorkSheet.Cells[linha, col] = "Quadro Funcionários ";
            col++;
            xlWorkSheet.Cells[linha, col] = "Frota Prog. 1P";
            col++;
            xlWorkSheet.Cells[linha, col] = "Frota Real 1P";
            col++;
            xlWorkSheet.Cells[linha, col] = "Frota Prog. 2P";
            col++;
            xlWorkSheet.Cells[linha, col] = "Frota Real 2P";
            col++;
            xlWorkSheet.Cells[linha, col] = "FCV Prog.";
            col++;
            xlWorkSheet.Cells[linha, col] = "FCV Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "Real %";
            col++;
            xlWorkSheet.Cells[linha, col] = "Perda";
            col++;
            xlWorkSheet.Cells[linha, col] = "Pontual. Saida Frota";
            col++;
            xlWorkSheet.Cells[linha, col] = "SAC";
            col++;
            xlWorkSheet.Cells[linha, col] = "Acid.";
            col++;

            xlWorkSheet.Cells[linha, col] = "RA";
            col++;
            xlWorkSheet.Cells[linha, col] = "SOS";
            col++;
            xlWorkSheet.Cells[linha, col] = "Absenteímo Mot";
            col++;
            xlWorkSheet.Cells[linha, col] = "Absenteímo Cob";
            col++;
            xlWorkSheet.Cells[linha, col] = "Absenteímo Total";
            col++;
            xlWorkSheet.Cells[linha, col] = "Absenteímo Índice";
            col++;

            xlWorkSheet.Cells[linha, col] = "Refeição Prog.";
            col++;
            xlWorkSheet.Cells[linha, col] = "Refeição Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "Refeição %";
            col++;
            xlWorkSheet.Cells[linha, col] = "PAX";
            col++;
            xlWorkSheet.Cells[linha, col] = "PVD";
            col++;
            xlWorkSheet.Cells[linha, col] = "Receita Total";
            col++;
            xlWorkSheet.Cells[linha, col] = "Receita por Carro";
            col++;
            xlWorkSheet.Cells[linha, col] = "KM ";
            col++;

            xlWorkSheet.Cells[linha, col] = "Consumo";
            col++;
            xlWorkSheet.Cells[linha, col] = "Média";
            col++;

            #endregion

            foreach (var itemC in _lista)
            {
                col = 1;
                linha++;

                #region Cabeçalho da Nota
                xlWorkSheet.Cells[linha, col] = itemC.DataGrupo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Setor;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CodigoLinha;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Quadro;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FrotaP1;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FrotaR1;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FrotaP2;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FrotaR2;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FCVProg;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FCVReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FCVPercentual;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FCVPerda;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Pontual;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.PontualPercentual;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.SAC;

                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Acidente;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RA;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.SOS;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AbsenteismoMot;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AbsenteismoCob;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.TotalAbsenteismo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Indice;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RefeicaoProg;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RefeicaoReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RefeicaoPercentual;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.PAX;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.PVD;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ReceitaTotal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ReceitaPorCarro;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Km;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Consumo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Media;
                #endregion

            }

            xlWorkSheet.Columns.AutoFit();
            xlWorkBook.SaveAs(caminho + nomeArquivo + ".xlsx",
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

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Exportou Demonstrativo Operacional da empresa " + empresaComboBoxAdv.Text +
                " periodo de " + (ativoCheckBox.Checked ? referenciaMaskedEditBox.Text : VigenciaDateTimePicker.Value.ToShortDateString())
                + " para excel";
            _log.Tela = "Demonstrativo Operacional";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            new Notificacoes.Mensagem("Arquivo " + nomeArquivo + " gerado com sucesso." + Environment.NewLine +
                " Salvo em: " + caminho, Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void Demonstrativo_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }
    }
}
