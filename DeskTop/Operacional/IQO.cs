﻿using Classes;
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
    public partial class IQO : Form
    {
        public IQO()
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
                    RecolherCheckBox.ForeColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;
        List<Classes.Operacional.IQO> _lista;

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

        private void IQO_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);

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
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor.DataMember = "FrotaMeta";
            summaryColumnDescriptor.Format = "{Average}";
            summaryColumnDescriptor.Name = "FrotaMeta";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor2.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor2.DataMember = "FrotaReal";
            summaryColumnDescriptor2.Format = "{Average}";
            summaryColumnDescriptor2.Name = "FrotaReal";
            summaryColumnDescriptor2.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor3 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor3.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor3.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor3.DataMember = "FrotaPontuacao";
            summaryColumnDescriptor3.Format = "{Average}";
            summaryColumnDescriptor3.Name = "FrotaPontuacao";
            summaryColumnDescriptor3.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor4 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor4.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor4.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor4.DataMember = "FCVMeta";
            summaryColumnDescriptor4.Format = "{Average}";
            summaryColumnDescriptor4.Name = "FCVMeta";
            summaryColumnDescriptor4.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor5 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor5.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor5.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor5.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor5.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor5.DataMember = "FCVReal";
            summaryColumnDescriptor5.Format = "{Average}";
            summaryColumnDescriptor5.Name = "FCVReal";
            summaryColumnDescriptor5.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor6 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor6.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor6.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor6.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor6.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor6.DataMember = "FCVPontuacao";
            summaryColumnDescriptor6.Format = "{Average}";
            summaryColumnDescriptor6.Name = "FCVPontuacao";
            summaryColumnDescriptor6.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor7 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor7.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor7.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor7.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor7.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor7.DataMember = "PontualidadeMeta";
            summaryColumnDescriptor7.Format = "{Average}";
            summaryColumnDescriptor7.Name = "PontualidadeMeta";
            summaryColumnDescriptor7.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor8 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor8.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor8.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor8.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor8.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor8.DataMember = "PontualidadeReal";
            summaryColumnDescriptor8.Format = "{Average}";
            summaryColumnDescriptor8.Name = "PontualidadeReal";
            summaryColumnDescriptor8.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor9 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor9.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor9.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor9.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor9.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor9.DataMember = "PontualidadePontuacao";
            summaryColumnDescriptor9.Format = "{Average}";
            summaryColumnDescriptor9.Name = "PontualidadePontuacao";
            summaryColumnDescriptor9.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor10 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor10.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor10.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor10.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor10.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor10.DataMember = "SACMeta";
            summaryColumnDescriptor10.Format = "{Average}";
            summaryColumnDescriptor10.Name = "SACMeta";
            summaryColumnDescriptor10.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor11 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor11.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor11.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor11.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor11.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor11.DataMember = "SACReal";
            summaryColumnDescriptor11.Format = "{Average}";
            summaryColumnDescriptor11.Name = "SACReal";
            summaryColumnDescriptor11.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor12 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor12.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor12.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor12.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor12.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor12.DataMember = "SACPontuacao";
            summaryColumnDescriptor12.Format = "{Average}";
            summaryColumnDescriptor12.Name = "SACPontuacao";
            summaryColumnDescriptor12.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor13 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor13.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor13.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor13.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor13.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor13.DataMember = "AcidenteMeta";
            summaryColumnDescriptor13.Format = "{Average}";
            summaryColumnDescriptor13.Name = "AcidenteMeta";
            summaryColumnDescriptor13.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor14 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor14.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor14.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor14.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor14.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor14.DataMember = "AcidenteReal";
            summaryColumnDescriptor14.Format = "{Average}";
            summaryColumnDescriptor14.Name = "AcidenteReal";
            summaryColumnDescriptor14.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor15 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor15.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor15.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor15.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor15.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor15.DataMember = "AcidentePontuacao";
            summaryColumnDescriptor15.Format = "{Average}";
            summaryColumnDescriptor15.Name = "AcidentePontuacao";
            summaryColumnDescriptor15.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor16 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor16.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor16.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor16.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor16.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor16.DataMember = "KMMeta";
            summaryColumnDescriptor16.Format = "{Average}";
            summaryColumnDescriptor16.Name = "KMMeta";
            summaryColumnDescriptor16.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor17 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor17.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor17.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor17.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor17.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor17.DataMember = "KMReal";
            summaryColumnDescriptor17.Format = "{Average}";
            summaryColumnDescriptor17.Name = "KMReal";
            summaryColumnDescriptor17.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor18 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor18.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor18.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor18.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor18.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor18.DataMember = "KMPontuacao";
            summaryColumnDescriptor18.Format = "{Average}";
            summaryColumnDescriptor18.Name = "KMPontuacao";
            summaryColumnDescriptor18.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor19 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor19.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor19.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor19.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor19.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor19.DataMember = "AbsenteismoMeta";
            summaryColumnDescriptor19.Format = "{Average}";
            summaryColumnDescriptor19.Name = "AbsenteismoMeta";
            summaryColumnDescriptor19.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor20 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor20.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor20.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor20.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor20.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor20.DataMember = "AbsenteismoReal";
            summaryColumnDescriptor20.Format = "{Average}";
            summaryColumnDescriptor20.Name = "AbsenteismoReal";
            summaryColumnDescriptor20.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor21 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor21.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor21.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor21.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor21.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor21.DataMember = "AbsenteismoPontuacao";
            summaryColumnDescriptor21.Format = "{Average}";
            summaryColumnDescriptor21.Name = "AbsenteismoPontuacao";
            summaryColumnDescriptor21.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor22 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor22.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor22.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor22.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor22.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor22.DataMember = "RefeicaoMeta";
            summaryColumnDescriptor22.Format = "{Average}";
            summaryColumnDescriptor22.Name = "RefeicaoMeta";
            summaryColumnDescriptor22.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor23 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor23.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor23.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor23.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor23.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor23.DataMember = "RefeicaoReal";
            summaryColumnDescriptor23.Format = "{Average}";
            summaryColumnDescriptor23.Name = "RefeicaoReal";
            summaryColumnDescriptor23.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor24 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor24.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor24.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor24.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor24.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor24.DataMember = "RefeicaoPontuacao";
            summaryColumnDescriptor24.Format = "{Average}";
            summaryColumnDescriptor24.Name = "RefeicaoPontuacao";
            summaryColumnDescriptor24.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor25 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor25.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor25.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor25.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor25.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor25.DataMember = "LimpezaMeta";
            summaryColumnDescriptor25.Format = "{Average}";
            summaryColumnDescriptor25.Name = "LimpezaMeta";
            summaryColumnDescriptor25.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor26 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor26.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor26.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor26.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor26.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor26.DataMember = "LimpezaReal";
            summaryColumnDescriptor26.Format = "{Average}";
            summaryColumnDescriptor26.Name = "LimpezaReal";
            summaryColumnDescriptor26.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor27 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor27.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor27.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor27.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor27.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor27.DataMember = "LimpezaPontuacao";
            summaryColumnDescriptor27.Format = "{Average}";
            summaryColumnDescriptor27.Name = "LimpezaPontuacao";
            summaryColumnDescriptor27.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor28 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor28.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor28.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor28.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor28.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor28.DataMember = "AvariaMeta";
            summaryColumnDescriptor28.Format = "{Average}";
            summaryColumnDescriptor28.Name = "AvariaMeta";
            summaryColumnDescriptor28.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor29 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor29.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor29.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor29.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor29.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor29.DataMember = "AvariaReal";
            summaryColumnDescriptor29.Format = "{Average}";
            summaryColumnDescriptor29.Name = "AvariaReal";
            summaryColumnDescriptor29.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor30 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor30.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor30.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor30.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor30.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor30.DataMember = "AvariaPontuacao";
            summaryColumnDescriptor30.Format = "{Average}";
            summaryColumnDescriptor30.Name = "AvariaPontuacao";
            summaryColumnDescriptor30.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor31 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor31.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor31.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor31.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor31.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor31.DataMember = "PontuacaoMeta";
            summaryColumnDescriptor31.Format = "{Average}";
            summaryColumnDescriptor31.Name = "PontuacaoMeta";
            summaryColumnDescriptor31.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor32 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor32.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor32.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor32.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor32.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor32.DataMember = "PontuacaoReal";
            summaryColumnDescriptor32.Format = "{Average}";
            summaryColumnDescriptor32.Name = "PontuacaoReal";
            summaryColumnDescriptor32.SummaryType = SummaryType.DoubleAggregate;

            _soma = new GridSummaryRowDescriptor("Sum", "Total geral",
                new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor2, summaryColumnDescriptor3, summaryColumnDescriptor4,
                                                    summaryColumnDescriptor5, summaryColumnDescriptor6, summaryColumnDescriptor7, summaryColumnDescriptor8,
                                                    summaryColumnDescriptor9, summaryColumnDescriptor10, summaryColumnDescriptor11,
                                                    summaryColumnDescriptor12, summaryColumnDescriptor13, summaryColumnDescriptor14,
                                                    summaryColumnDescriptor15, summaryColumnDescriptor16, summaryColumnDescriptor17,
                                                    summaryColumnDescriptor18, summaryColumnDescriptor19, summaryColumnDescriptor20,
                                                    summaryColumnDescriptor21, summaryColumnDescriptor22, summaryColumnDescriptor23, summaryColumnDescriptor24,
                                                    summaryColumnDescriptor25, summaryColumnDescriptor26, summaryColumnDescriptor27, summaryColumnDescriptor28,
                                                    summaryColumnDescriptor29, summaryColumnDescriptor30, summaryColumnDescriptor31, summaryColumnDescriptor32

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
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
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

            _lista = new List<Classes.Operacional.IQO>();
            mensagemSistemaLabel.Text = "Aguarde," + Environment.NewLine + "Pesquisando mês " + referenciaMaskedEditBox.Text;

            this.Refresh();
            try
            {
                _lista.AddRange(new OperacionalBO().ListarIQO(_empresa.IdEmpresa, _dataInicio, _dataFim));
            }
            catch { }
                
            gridGroupingControl1.DataSource = _lista.Where(w => w.CodigoLinha != null).ToList();
            mensagemSistemaLabel.Text = "";

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Consultou o IQO da empresa " + empresaComboBoxAdv.Text +
                " periodo de " + referenciaMaskedEditBox.Text;
            _log.Tela = "IQO";

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

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            {
                if (e.TableCellIdentity.Column.MappingName == "Resultado")
                {
                    string texto = e.Style.Text;

                    if (texto == "RUIM")
                        e.Style.TextColor = Color.Red;
                    else
                    {
                        if (texto == "REGULAR")
                        {
                            if (Publicas._TemaBlack)
                                e.Style.TextColor = Color.Yellow;
                            else
                                e.Style.BackColor = Color.Khaki;
                        }
                        else
                        {
                            if (texto == "BOM")
                                e.Style.TextColor = Color.Blue;
                            else
                                e.Style.TextColor = Color.Green;
                        }
                    }
                }
            }
            catch { }
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
            string nomeArquivo = "IQO_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" + referenciaMaskedEditBox.ClipText;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int linha = 1;
            int col = 1;

            #region titulo colunas

            xlWorkSheet.Cells[linha, col] = "Setor";
            col++;

            xlWorkSheet.Cells[linha, col] = "Linha ";
            col++;

            xlWorkSheet.Cells[linha, col] = "FCF Meta ";
            col++;
            xlWorkSheet.Cells[linha, col] = "FCF Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "FCF Pontuação";
            col++;
            xlWorkSheet.Cells[linha, col] = "FCV Meta";
            col++;
            xlWorkSheet.Cells[linha, col] = "FCV Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "FCV Pontuação";
            col++;
            xlWorkSheet.Cells[linha, col] = "Pontualidade Partida Meta";
            col++;
            xlWorkSheet.Cells[linha, col] = "Pontualidade Partida Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "Pontualidade Partida Pontuação";
            col++;
            xlWorkSheet.Cells[linha, col] = "Reclamação por Passageiro Meta";
            col++;
            xlWorkSheet.Cells[linha, col] = "Reclamação por Passageiro Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "Reclamação por Passageiro Pontuação";
            col++;

            xlWorkSheet.Cells[linha, col] = "Acidente por KM Meta";
            col++;
            xlWorkSheet.Cells[linha, col] = "Acidente por KM Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "Acidente por KM Pontuação";
            col++;
            xlWorkSheet.Cells[linha, col] = "Absenteímo Meta";
            col++;
            xlWorkSheet.Cells[linha, col] = "Absenteímo Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "Absenteímo Pontuação";
            col++;

            xlWorkSheet.Cells[linha, col] = "Refeição Meta.";
            col++;
            xlWorkSheet.Cells[linha, col] = "Refeição Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "Refeição Pontuação";
            col++;
            xlWorkSheet.Cells[linha, col] = "Limpeza Meta";
            col++;
            xlWorkSheet.Cells[linha, col] = "Limpeza Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "Limpeza Pontuação";
            col++;
            xlWorkSheet.Cells[linha, col] = "Avarias Meta";
            col++;
            xlWorkSheet.Cells[linha, col] = "Avarias Real";
            col++;

            xlWorkSheet.Cells[linha, col] = "Avarias Pontuação";
            col++;
            xlWorkSheet.Cells[linha, col] = "Pontuação Meta";
            col++;
            xlWorkSheet.Cells[linha, col] = "Pontuação Real";
            col++;
            xlWorkSheet.Cells[linha, col] = "Resultado";
            col++;

            #endregion

            foreach (var itemC in _lista)
            {
                col = 1;
                linha++;

                #region Cabeçalho da Nota
                xlWorkSheet.Cells[linha, col] = itemC.Setor;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.CodigoLinha;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FrotaMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FrotaReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FrotaPontuacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FCVMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FCVReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.FCVPontuacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.PontualidadeMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.PontualidadeReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.PontualidadePontuacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.SACMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.SACReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.SACPontuacao;

                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AcidenteMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AcidenteReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AcidentePontuacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.KMMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.KMReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.KMPontuacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AbsenteismoMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AbsenteismoReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AbsenteismoPontuacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RefeicaoMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RefeicaoReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.RefeicaoPontuacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.LimpezaMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.LimpezaReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.LimpezaPontuacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AvariaMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AvariaReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AvariaPontuacao;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.PontuacaoMeta;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.PontuacaoReal;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Resultado;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.AvariaReal;
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
            _log.Descricao = "Exportou IQO da empresa " + empresaComboBoxAdv.Text +
                " periodo de " + referenciaMaskedEditBox.Text + " para excel";
            _log.Tela = "IQO";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            new Notificacoes.Mensagem("Arquivo " + nomeArquivo + " gerado com sucesso." + Environment.NewLine +
                " Salvo em: " + caminho, Publicas.TipoMensagem.Alerta).ShowDialog();
        }
    }
}
