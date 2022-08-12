using Classes;
using DynamicFilter;
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

namespace Suportte.Contabilidade
{
    public partial class ResumoArquivei : Form
    {
        public ResumoArquivei()
        {
            InitializeComponent();
            inicialDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
            finalDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
            inicialDateTimePicker.Value = DateTime.Now;
            finalDateTimePicker.Value = DateTime.Now;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                if (Publicas._TemaBlack)
                {
                    gridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl.BackColor = Publicas._panelTitulo;
                    inicialDateTimePicker.Style = VisualStyle.Office2016Black;
                    finalDateTimePicker.Style = VisualStyle.Office2016Black;
                    ApenasConferidasCheckBox.ForeColor = Publicas._fonte;
                }
            }

            Publicas._mensagemSistema = string.Empty;
        }

        private class Resumo
        {
            public int CSTArquivei { get; set; }
            public int CFOPArquiviei { get; set; }
            public decimal TotalArquivei { get; set; }
            public int CSTGlobus { get; set; }
            public int CFOPGlobus { get; set; }
            public decimal TotalGlobus { get; set; }
            public List<ItensResumo> ItensResumo { get; set; }
        }

        private class ItensResumo
        {
            public int IdArquivei { get; set; }
            public string TipoDocumento { get; set; }
            public decimal NumeroNF { get; set; }
            public string Serie { get; set; }
            public DateTime Emissao { get; set; }
            public string CodigoFornecedor { get; set; }
            public string RazaoSocialFornecedor { get; set; }
            public decimal ValorNF { get; set; }
            public decimal ValorItem { get; set; }
            public decimal ValorItemGlobus { get; set; }
        }

        Classes.Empresa _empresa;
        Classes.ParametrosArquivei _parametro;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.ItensComparacao> _listaItens;
        List<Classes.NotasArquivei> _listaNotas;
        List<Resumo> _listaResumo;

        public string tipoProcessamento = "";


        private void ResumoArquivei_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
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

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl.SetMetroStyle(metroColor);
                this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

        }

        private void pesquisarButton_Click(object sender, EventArgs e)
        {
            mensagemSistemaLabel.Text = "Pesquisando, Aguarde ...";
            pesquisarButton.Cursor = Cursors.WaitCursor;
            pesquisarButton.Enabled = false;
            Refresh();

            gridGroupingControl.DataSource = new List<Resumo>();

            string _conferidas = (ApenasConferidasCheckBox.Checked ? "S" : (ApenasNaoConferidasCheckBox.Checked ? "N" : "T"));

            _listaNotas = new ArquiveiBO().ListarParaComparar(_empresa.IdEmpresa, inicialDateTimePicker.Value, finalDateTimePicker.Value, _conferidas, tipoProcessamento, true);
            _listaItens = new ArquiveiBO().ListarItensArquivei(_empresa.IdEmpresa, inicialDateTimePicker.Value, finalDateTimePicker.Value, _parametro.Id, tipoProcessamento, true);

            _listaResumo = new List<Resumo>();
            List<ItensResumo> _listaItensResumo = new List<ItensResumo>();

            bool encontrou = false;

            foreach (var itemG in _listaItens.GroupBy(g => new { g.CSTComparar, g.CFOPComparar, g.CSTGlobus, g.CFOPGlobus }))
            {
                decimal _total = 0;
                decimal _totalG = 0;

                _listaItensResumo = new List<ItensResumo>();
                foreach (var item in _listaItens.Where(w => w.CSTComparar == itemG.Key.CSTComparar && w.CFOPComparar == itemG.Key.CFOPComparar &&
                                                            w.CSTGlobus == itemG.Key.CSTGlobus && w.CFOPGlobus == itemG.Key.CFOPGlobus) )
                {
                    encontrou = false;
                    foreach (var itemN in _listaNotas.Where(w => w.IdArquivei == item.IdArquivei))
                    {
                        foreach (var itemD in _listaItensResumo.Where(w => w.IdArquivei == item.IdArquivei))
                        {
                            itemD.ValorItem = itemD.ValorItem + item.ValorTotal;
                            itemD.ValorItemGlobus = itemD.ValorItemGlobus + item.ValorTotalGlobus;
                            encontrou = true;
                        }

                        if (!encontrou)
                        {
                            ItensResumo _itens = new ItensResumo();

                            _itens.IdArquivei = item.IdArquivei;
                            _itens.CodigoFornecedor = itemN.CodigoFornecedor;
                            _itens.RazaoSocialFornecedor = itemN.RazaoSocialFornecedor;
                            _itens.NumeroNF = itemN.NumeroNFArquivo;
                            _itens.Serie = itemN.SerieArquivo;
                            _itens.Emissao = itemN.EmissaoArquivo;
                            _itens.TipoDocumento = itemN.TipoDocumento;
                            _itens.ValorNF = itemN.ValorTotalNFArquivo;
                            _itens.ValorItem = item.ValorTotal;
                            _itens.ValorItemGlobus = item.ValorTotalGlobus;
                            _listaItensResumo.Add(_itens);
                        }
                    }

                    _total = _total + item.ValorTotal;
                    _totalG = _totalG + item.ValorTotalGlobus;
                }

                Resumo _arq = new Resumo();
                //_arq.CFOPArquiviei = itemG.Key.CFOPComparar;
                //_arq.CSTArquivei = itemG.Key.CSTComparar;
                if (!itemG.Key.CSTComparar.Contains(",") && itemG.Key.CSTComparar != "")
                    _arq.CSTArquivei = Convert.ToInt32(itemG.Key.CSTComparar);
                else
                {
                    if (itemG.Key.CSTComparar.Contains(itemG.Key.CSTGlobus.ToString()))
                        _arq.CSTArquivei = itemG.Key.CSTGlobus;
                }

                if (!itemG.Key.CFOPComparar.Contains(",") && itemG.Key.CSTComparar != "")
                    _arq.CFOPArquiviei = Convert.ToInt32(itemG.Key.CFOPComparar);
                else
                {
                    if (itemG.Key.CFOPComparar.Contains(itemG.Key.CFOPGlobus.ToString()))
                        _arq.CFOPArquiviei = itemG.Key.CFOPGlobus;
                }

                _arq.CFOPGlobus = itemG.Key.CFOPGlobus;
                _arq.CSTGlobus = itemG.Key.CSTGlobus;

                _arq.TotalArquivei = _total;
                _arq.TotalGlobus = _totalG;
                _arq.ItensResumo = new List<ItensResumo>();
                _arq.ItensResumo.AddRange(_listaItensResumo);
                _listaResumo.Add(_arq);
            }

            gridGroupingControl.DataSource = _listaResumo;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Relations.Count; i++)
            {

                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.ShowRowHeader = false;
                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowEdit = false;
                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowNew = false;
                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowRemove = false;

                for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns.Count; j++)
                {
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].AllowFilter = false;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName == "IdArquivei")
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                    else
                    {
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                        if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName == "TipoDocumento")
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Tipo";
                        }

                        if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("NumeroNF"))
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Número";
                        }

                        if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Serie"))
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Série";
                        }

                        if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Emissao"))
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Emissão";
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Width = 100;
                        }

                        if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("CodigoFornecedor"))
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Código";
                        }

                        if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("RazaoSocialFornecedor"))
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Razão Social";
                        }
                        if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("ValorNF"))
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Total NF";
                        }
                        if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("ValorItem"))
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Itens Arquivei";
                        }
                        if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("ItemGlobus"))
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Itens Globus";
                        }
                    }
                }

                if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.SummaryRows.Count == 0)
                {
                    GridSummaryRowDescriptor _soma;

                    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                    summaryColumnDescriptor.DataMember = "ValorNF";
                    summaryColumnDescriptor.Format = "{Sum}";
                    summaryColumnDescriptor.Name = "ValorNF";
                    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                    GridSummaryColumnDescriptor summaryColumnDescriptor2 = new GridSummaryColumnDescriptor();
                    summaryColumnDescriptor2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    summaryColumnDescriptor2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    summaryColumnDescriptor2.Appearance.AnySummaryCell.Format = "n2";
                    summaryColumnDescriptor2.Appearance.GroupCaptionSummaryCell.Format = "n2";
                    summaryColumnDescriptor2.DataMember = "ValorItem";
                    summaryColumnDescriptor2.Format = "{Sum}";
                    summaryColumnDescriptor2.Name = "ValorItem";
                    summaryColumnDescriptor2.SummaryType = SummaryType.DoubleAggregate;

                    GridSummaryColumnDescriptor summaryColumnDescriptor3 = new GridSummaryColumnDescriptor();
                    summaryColumnDescriptor3.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    summaryColumnDescriptor3.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    summaryColumnDescriptor3.Appearance.AnySummaryCell.Format = "n2";
                    summaryColumnDescriptor3.Appearance.GroupCaptionSummaryCell.Format = "n2";
                    summaryColumnDescriptor3.DataMember = "ValorItemGlobus";
                    summaryColumnDescriptor3.Format = "{Sum}";
                    summaryColumnDescriptor3.Name = "ValorItemGlobus";
                    summaryColumnDescriptor3.SummaryType = SummaryType.DoubleAggregate;

                    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor2, summaryColumnDescriptor3 });

                    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                    try
                    {
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.SummaryRows.Add(_soma);
                    }
                    catch { }

                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                }
            }

            mensagemSistemaLabel.Text = "";
            pesquisarButton.Cursor = Cursors.Default;
            pesquisarButton.Enabled = true;
            this.Refresh();
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

        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ApenasConferidasCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gridGroupingControl.Focus();
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

        private void inicialDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                finalDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ApenasConferidasCheckBox.Focus();
            }
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
        }

        private void pesquisarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gridGroupingControl.Focus();
            }
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void inicialDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }
        
        private void limparButton_Enter(object sender, EventArgs e)
        {
            limparButton.BackColor = Publicas._botaoFocado;
            limparButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void pesquisarButton_Enter(object sender, EventArgs e)
        {
            
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
        }

        private void ResumoArquivei_Load(object sender, EventArgs e)
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

        private void limparButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl.DataSource = new List<Resumo>();
            inicialDateTimePicker.Focus();
        }
    }
}
