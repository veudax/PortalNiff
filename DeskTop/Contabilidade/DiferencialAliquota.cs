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
    public partial class DiferencialAliquota : Form
    {
        public DiferencialAliquota()
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

                    AliquotaExternaCurrency.ForeColor = empresaComboBoxAdv.ForeColor;
                    AliquotaExternaCurrency.PositiveColor = empresaComboBoxAdv.ForeColor;
                    AliquotaExternaCurrency.ZeroColor = empresaComboBoxAdv.ForeColor;

                    AliquotaPadraoCurrencyText.ForeColor = empresaComboBoxAdv.ForeColor;
                    AliquotaPadraoCurrencyText.PositiveColor = empresaComboBoxAdv.ForeColor;
                    AliquotaPadraoCurrencyText.ZeroColor = empresaComboBoxAdv.ForeColor;

                    BaseCurrencyTextBox.ForeColor = empresaComboBoxAdv.ForeColor;
                    BaseCurrencyTextBox.PositiveColor = empresaComboBoxAdv.ForeColor;
                    BaseCurrencyTextBox.ZeroColor = empresaComboBoxAdv.ForeColor;

                    DebitoCurrencyTextBox.ForeColor = empresaComboBoxAdv.ForeColor;
                    DebitoCurrencyTextBox.PositiveColor = empresaComboBoxAdv.ForeColor;
                    DebitoCurrencyTextBox.ZeroColor = empresaComboBoxAdv.ForeColor;

                    CreditoCurrencyTextBox.ForeColor = empresaComboBoxAdv.ForeColor;
                    CreditoCurrencyTextBox.PositiveColor = empresaComboBoxAdv.ForeColor;
                    CreditoCurrencyTextBox.ZeroColor = empresaComboBoxAdv.ForeColor;

                    DiferenciaCurrencyTextBox.ForeColor = empresaComboBoxAdv.ForeColor;
                    DiferenciaCurrencyTextBox.PositiveColor = empresaComboBoxAdv.ForeColor;
                    DiferenciaCurrencyTextBox.ZeroColor = empresaComboBoxAdv.ForeColor;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;

        Classes.Empresa _empresa;
        //Classes.FornecedoresGlobus _fornecedor;
        List<Classes.DiferencialAliquota.Diferencial> _diferencial;
        List<Classes.DiferencialAliquota.Documento> _documento;

        DateTime _dataInicio;
        DateTime _dataFim;
        string _referencia;
        int countId;
        
        GridSummaryRowDescriptor _somaI;
        GridCurrentCell _colunaCorrente;
        Element _elementoGridClicado;
        Element _elementoGrid2Clicado;
        decimal aliq = 0;

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

        private void excluirButton_Enter(object sender, EventArgs e)
        {
            excluirButton.BackColor = Publicas._botaoFocado;
            excluirButton.ForeColor = Publicas._fonteBotaoFocado;
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

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CFOPTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
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
                SelectNextControl(ActiveControl, false, true, true, true);
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

        private void ReceitaBrutaPrevLabel_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
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

        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;
            referenciaMaskedEditBox.ThemeStyle.BorderColor = Publicas._bordaSaida;

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

            try
            {
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.SummaryRows.Remove(_somaI);
            }
            catch { }

            gridGroupingControl1.DataSource = new List<Classes.DiferencialAliquota.Diferencial>();

            _referencia = referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2);

            _documento = new DiferencialAliquotaBO().Listar(_empresa.IdEmpresa, _referencia, CFOPTextBox.Text);

            if (_documento.Count != 0) // Existe gravado
                _diferencial = new DiferencialAliquotaBO().Listar(_empresa.IdEmpresa, _referencia, CFOPTextBox.Text, AliquotasValidasTextBox.Text, AliquotaPadraoCurrencyText.DecimalValue);
            else
            { 
                _diferencial = new DiferencialAliquotaBO().Listar(_empresa.IdEmpresa, _dataInicio, _dataFim, AliquotaExternaCurrency.DecimalValue, CFOPTextBox.Text, AliquotasValidasTextBox.Text, AliquotaPadraoCurrencyText.DecimalValue);

                countId = 1;
                foreach (var itemG in _diferencial.GroupBy(g => new { g.CodigoEmpresa, g.CodigoFl, g.CodigoForn, g.Documento, g.CodTipoDoc, g.Serie }))
                {
                    Classes.DiferencialAliquota.Documento _tipo = new Classes.DiferencialAliquota.Documento();

                    foreach (var item in _diferencial.Where(w => w.CodigoEmpresa == itemG.Key.CodigoEmpresa &&
                                                               w.CodigoFl == itemG.Key.CodigoFl &&
                                                               w.CodigoForn == itemG.Key.CodigoForn &&
                                                               w.Documento == itemG.Key.Documento &&
                                                               w.CodTipoDoc == itemG.Key.CodTipoDoc &&
                                                               w.Serie == itemG.Key.Serie))
                    {
                        _tipo.IdEmpresa = _empresa.IdEmpresa;
                        _tipo.Referencia = _referencia;
                        _tipo.CodigoEmpresa = item.CodigoEmpresa;
                        _tipo.CodigoFl = item.CodigoFl;
                        _tipo.CodigoForn = item.CodigoForn;
                        _tipo.Numero = item.Documento;
                        _tipo.CodTipoDoc = item.CodTipoDoc;
                        _tipo.Serie = item.Serie;
                        _tipo.Base = _tipo.Base + item.Valor;
                        _tipo.Debito = _tipo.Debito + item.Debito;
                        _tipo.Credito = _tipo.Credito + item.Credito;
                        _tipo.Diferenca = _tipo.Diferenca + item.Diferenca;
                        _tipo.Fornecedor = item.Fornecedor;
                        _tipo.Emissao = item.Emissao;
                        _tipo.Entrada = item.Entrada;
                        _tipo.Id = countId;
                        item.IdDiferencial = countId;
                    }

                    decimal baseItens = new DiferencialAliquotaBO().ValorNFEscrituracao(_tipo.Numero, _tipo.Serie, _tipo.CodTipoDoc, 0, _tipo.CodigoForn, _tipo.CodigoEmpresa, _tipo.CodigoFl, CFOPTextBox.Text);
                    _tipo.ValorESF = baseItens;

                    _documento.Add(_tipo);
                    countId++;
                }
            }

            gridGroupingControl1.DataSource = _documento;

            gravarButton.Enabled = true;
            excluirButton.Enabled = _documento.Where(w => w.Existe).Count() != 0;
            GerarExecelButton.Enabled = _documento.Count() != 0;

        }

        private void DiferencialAliquota_Shown(object sender, EventArgs e)
        {
            this.Top = 60;
            #region coloca os botões centralizados
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { gravarButton, limparButton, excluirButton, GerarExecelButton };
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
            }
            #endregion

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

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

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

            this.gridGroupingControl2.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl2.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;


            gridGroupingControl1.TableControl.CellToolTip.InitialDelay = 100;
            gridGroupingControl1.TableControl.CellToolTip.AutoPopDelay = 10000;

            #region Soma grid principal 1
            GridSummaryRowDescriptor _soma;

            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor.DataMember = "Base";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "Base";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

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
            summaryColumnDescriptor4.DataMember = "Diferenca";
            summaryColumnDescriptor4.Format = "{Sum}";
            summaryColumnDescriptor4.Name = "Diferenca";
            summaryColumnDescriptor4.SummaryType = SummaryType.DoubleAggregate;


            _soma = new GridSummaryRowDescriptor("Sum", "",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor2, summaryColumnDescriptor3, summaryColumnDescriptor4 });

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

            #region Soma grid principal 2
            GridSummaryRowDescriptor _soma2;

            GridSummaryColumnDescriptor summaryColumnDescriptorg2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorg2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorg2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorg2.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorg2.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorg2.DataMember = "Valor";
            summaryColumnDescriptorg2.Format = "{Sum}";
            summaryColumnDescriptorg2.Name = "Base";
            summaryColumnDescriptorg2.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorg22 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorg22.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorg22.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorg22.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorg22.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorg22.DataMember = "Debito";
            summaryColumnDescriptorg22.Format = "{Sum}";
            summaryColumnDescriptorg22.Name = "Debito";
            summaryColumnDescriptorg22.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorg23 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorg23.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorg23.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorg23.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorg23.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorg23.DataMember = "Credito";
            summaryColumnDescriptorg23.Format = "{Sum}";
            summaryColumnDescriptorg23.Name = "Credito";
            summaryColumnDescriptorg23.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorg24 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorg24.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorg24.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorg24.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorg24.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorg24.DataMember = "Diferenca";
            summaryColumnDescriptorg24.Format = "{Sum}";
            summaryColumnDescriptorg24.Name = "Diferenca";
            summaryColumnDescriptorg24.SummaryType = SummaryType.DoubleAggregate;


            _soma2 = new GridSummaryRowDescriptor("Sum", "",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptorg2, summaryColumnDescriptorg22, summaryColumnDescriptorg23, summaryColumnDescriptorg24 });

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
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl1.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
            #endregion

            #region Soma grid interno

            GridSummaryColumnDescriptor summaryColumnDescriptorI = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorI.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorI.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorI.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorI.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorI.DataMember = "Base";
            summaryColumnDescriptorI.Format = "{Sum}";
            summaryColumnDescriptorI.Name = "Base";
            summaryColumnDescriptorI.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorI2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorI2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorI2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorI2.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorI2.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorI2.DataMember = "ICMS";
            summaryColumnDescriptorI2.Format = "{Sum}";
            summaryColumnDescriptorI2.Name = "Debito";
            summaryColumnDescriptorI2.SummaryType = SummaryType.DoubleAggregate;

            _somaI = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptorI, summaryColumnDescriptorI2});

            _somaI.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _somaI.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _somaI.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _somaI.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _somaI.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            #endregion
        }

        private void CFOPTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void CFOPTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            referenciaMaskedEditBox.Text = string.Empty;

            try
            {
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.SummaryRows.Remove(_somaI);
            }
            catch { }

            gridGroupingControl1.DataSource = new List<Classes.DiferencialAliquota.Documento>();
            gridGroupingControl2.DataSource = new List<Classes.DiferencialAliquota.Diferencial>();
            referenciaMaskedEditBox.Focus();

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            GerarExecelButton.Enabled = false;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_diferencial.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum cálculo aplicado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            _diferencial.ForEach(u => { u.IdEmpresa = _empresa.IdEmpresa;
                u.Referencia = _referencia;
                u.AliquotaExterna = AliquotaExternaCurrency.DecimalValue;});

            if (!new DiferencialAliquotaBO().Gravar(_documento, _diferencial))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou o diferencial de aliquota  da empresa " + empresaComboBoxAdv.Text +
                " para a referencia " + referenciaMaskedEditBox.Text + " com a aliquota externa de " + AliquotaExternaCurrency.DecimalValue.ToString();

            _log.Tela = "Escrituração - Diferencial Aliquota";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);

        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new DiferencialAliquotaBO().Excluir(_empresa.IdEmpresa, _referencia))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu o diferencial de aliquota  da empresa " + empresaComboBoxAdv.Text +
                " para a referencia " + referenciaMaskedEditBox.Text + " com a aliquota externa de " + AliquotaExternaCurrency.DecimalValue.ToString();

            _log.Tela = "Escrituração - Diferencial Aliquota";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {

            try
            {
                if (e.TableCellIdentity.TableCellType == GridTableCellType.FilterBarCell)
                    return;

                        //muda cor dos totais
                        Record dr;
                        GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                if (rec != null)
                {
                    dr = rec.GetRecord() as Record;
                    if (dr != null && (decimal)dr["Base"] != (decimal)dr["ValorESF"])
                    {
                        e.Style.TextColor = Color.OrangeRed;

                        if (e.TableCellIdentity.Column.MappingName.Equals("Base"))
                            e.Style.CellTipText = "Valor desta Nota na Escrituração é de " +
                                String.Format("{0:0.00}", (decimal)dr["ValorESF"]);
                    }
                }
            }
            catch { }
        }

        private void AliquotasValidasTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }
        }

        private void AliquotaExternaCurrency_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }
        }

        private void AliquotaPadraoCurrencyText_Validating(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool diferencas = false;
            try
            {
                GridRecord rec = _elementoGridClicado as GridRecord;
                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        diferencas = (decimal)dr["Base"] != (decimal)dr["ValorESF"];
                    }
                }
            }
            catch { }
            excluirCFOPToolStripMenuItem.Enabled = diferencas;
            try
            {
                alterarValoresDoCFOPToolStripMenuItem.Enabled = diferencas; //&& _elementoGrid2Clicado.ParentChildTable.Name == "Detalhamento";
            }
            catch
            {
                alterarValoresDoCFOPToolStripMenuItem.Enabled = false;
            }

        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    gridGroupingControl2.DataSource = _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                                              w.CodigoFl == (int)dr["CodigoFl"] &&
                                                                              w.Documento == (string)dr["Numero"] &&
                                                                              w.Serie == (string)dr["Serie"] &&
                                                                              w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                                              w.CodigoForn == (decimal)dr["CodigoForn"]).ToList();
                }
            }
            _colunaCorrente = gridGroupingControl2.TableControl.CurrentCell;
            AjusteRelacionamento();
            _elementoGridClicado = this.gridGroupingControl1.Table.GetInnerMostCurrentElement();
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
                    gridGroupingControl2.DataSource = _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                                              w.CodigoFl == (int)dr["CodigoFl"] &&
                                                                              w.Documento == (string)dr["Numero"] &&
                                                                              w.Serie == (string)dr["Serie"] &&
                                                                              w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                                              w.CodigoForn == (decimal)dr["CodigoForn"]).ToList();
                }
            }
            _colunaCorrente = gridGroupingControl2.TableControl.CurrentCell;
            AjusteRelacionamento();
            _elementoGridClicado = this.gridGroupingControl1.Table.GetInnerMostCurrentElement();
        }

        private void AjusteRelacionamento()
        {
            try
            {
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.SummaryRows.Remove(_somaI);
            }
            catch { }

            #region Ajusta grid interno
            for (int i = 0; i < gridGroupingControl2.TableDescriptor.Relations.Count; i++)
            {
                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.ShowRowHeader = false;
                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.AllowEdit = false;
                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.AllowNew = false;
                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.AllowRemove = false;

                try
                {
                    gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.SummaryRows.Add(_somaI);
                }
                catch { }

                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl2.TableDescriptor.Appearance.RecordFieldCell.BackColor;
                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
            }

            for (int j = 0; j < gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns.Count; j++)
            {
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].AllowFilter = false;
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;

                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                try
                {
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("CodDoctoESF") ||
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("Id") ||
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Existe") ||
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Documento") ||
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("TipoDocto") ||
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Fornecedor") ||
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Entrada") ||
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Emissao"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName);
                }
                catch { }

                if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Base") ||
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("ICMS"))
                {
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 100;
                }

                if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("CFOPNota"))
                {
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "CFOP Nota";
                }

                if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Aliquota"))
                {
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Alíquota";
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                }

            }
            #endregion

        }

        private void gridGroupingControl2_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            {

                if (e.TableCellIdentity.TableCellType == GridTableCellType.FilterBarCell)
                    return;

                {
                    //muda cor dos totais
                    Record dr;
                    GridRecordRow rec = this.gridGroupingControl2.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    if (e.TableCellIdentity.Column.MappingName.Equals("Aliquota") ||
                        e.TableCellIdentity.Column.MappingName.Equals("Credito") ||
                        e.TableCellIdentity.Column.MappingName.Equals("Diferenca"))
                    {
                        if (rec != null)
                        {
                            dr = rec.GetRecord() as Record;
                            if (dr != null && (bool)dr["AliquotaZerada"])
                                e.Style.TextColor = Color.OrangeRed;
                        }
                    }

                }
            }
            catch { }
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            
            _elementoGrid2Clicado = this.gridGroupingControl2.Table.GetInnerMostCurrentElement();
        }

        private void gridGroupingControl2_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
           
            _elementoGrid2Clicado = this.gridGroupingControl2.Table.GetInnerMostCurrentElement();
        }

        private void gridGroupingControl2_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            

        }

        private void currencyTextBox2_TextChanged(object sender, EventArgs e)
        {
            DebitoCurrencyTextBox.DecimalValue = BaseCurrencyTextBox.DecimalValue * (AliquotaExternaCurrency.DecimalValue / 100);
            CreditoCurrencyTextBox.DecimalValue = BaseCurrencyTextBox.DecimalValue * (aliq / 100);
            DiferenciaCurrencyTextBox.DecimalValue = DebitoCurrencyTextBox.DecimalValue - CreditoCurrencyTextBox.DecimalValue;
        }

        private void BaseCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            GridRecord rec = _elementoGrid2Clicado as GridRecord;
            decimal valor = 0;
            decimal debito = 0;
            decimal credito = 0;
            if (rec != null)
            {

                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    valor = (decimal)dr["Valor"];
                    foreach (var item in _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                                 w.CodigoFl == (int)dr["CodigoFl"] &&
                                                                 w.Documento == (string)dr["Documento"] &&
                                                                 w.Serie == (string)dr["Serie"] &&
                                                                 w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                                 w.CodigoForn == (decimal)dr["CodigoForn"] &&
                                                                 w.CFOP == (int)dr["CFOP"] && w.Aliquota == (decimal)dr["Aliquota"]))
                    {
                        item.Valor = BaseCurrencyTextBox.DecimalValue;
                        item.Debito = DebitoCurrencyTextBox.DecimalValue;
                        item.Credito = CreditoCurrencyTextBox.DecimalValue;
                        item.Diferenca = DiferenciaCurrencyTextBox.DecimalValue;
                    }
                }
            }

            rec = _elementoGridClicado as GridRecord;
            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    valor = _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                    w.CodigoFl == (int)dr["CodigoFl"] &&
                                                    w.Documento == (string)dr["Numero"] &&
                                                    w.Serie == (string)dr["Serie"] &&
                                                    w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                    w.CodigoForn == (decimal)dr["CodigoForn"])
                                         .Sum(s => s.Valor);

                    debito = _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                    w.CodigoFl == (int)dr["CodigoFl"] &&
                                                    w.Documento == (string)dr["Numero"] &&
                                                    w.Serie == (string)dr["Serie"] &&
                                                    w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                    w.CodigoForn == (decimal)dr["CodigoForn"])
                                         .Sum(s => s.Debito);

                    credito = _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                    w.CodigoFl == (int)dr["CodigoFl"] &&
                                                    w.Documento == (string)dr["Numero"] &&
                                                    w.Serie == (string)dr["Serie"] &&
                                                    w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                    w.CodigoForn == (decimal)dr["CodigoForn"])
                                         .Sum(s => s.Credito);

                    foreach (var item in _documento.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                    w.CodigoFl == (int)dr["CodigoFl"] &&
                                                    w.Numero == (string)dr["Numero"] &&
                                                    w.Serie == (string)dr["Serie"] &&
                                                    w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                    w.CodigoForn == (decimal)dr["CodigoForn"]))
                    {
                        item.Base = valor;
                        item.Debito = debito;
                        item.Credito = credito;
                        item.Diferenca = debito - credito;
                    }
                }
                EditarPanel.Visible = false;
                BaseCurrencyTextBox.DecimalValue = 0;
                CreditoCurrencyTextBox.DecimalValue = 0;
                DebitoCurrencyTextBox.DecimalValue = 0;
                DiferenciaCurrencyTextBox.DecimalValue = 0;

                gridGroupingControl1.DataSource = new List<Classes.DiferencialAliquota.Documento>();
                gridGroupingControl1.DataSource = _documento;

            }
        }

        private void alterarValoresDoCFOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecord rec = _elementoGrid2Clicado as GridRecord;

            if (rec != null)
            {

                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    BaseCurrencyTextBox.DecimalValue = (decimal)dr["Valor"];
                    DebitoCurrencyTextBox.DecimalValue = (decimal)dr["Debito"];
                    CreditoCurrencyTextBox.DecimalValue = (decimal)dr["Credito"];
                    DiferenciaCurrencyTextBox.DecimalValue = (decimal)dr["Diferenca"];
                    aliq = (decimal)dr["Aliquota"];
                }
            }

            EditarPanel.Visible = true;
            BaseCurrencyTextBox.Focus();

        }

        private void excluirCFOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecord rec = _elementoGrid2Clicado as GridRecord;
            Classes.DiferencialAliquota.Diferencial itemSelecionado = new Classes.DiferencialAliquota.Diferencial();
            decimal valor = 0;
            decimal debito = 0;
            decimal credito = 0;

            if (rec != null)
            {

                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    foreach (var item in _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                                 w.CodigoFl == (int)dr["CodigoFl"] &&
                                                                 w.Documento == (string)dr["Documento"] &&
                                                                 w.Serie == (string)dr["Serie"] &&
                                                                 w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                                 w.CodigoForn == (decimal)dr["CodigoForn"] &&
                                                                 w.CFOP == (int)dr["CFOP"] && w.Aliquota == (decimal)dr["Aliquota"]))
                    {
                        itemSelecionado = item;
                    }
                }

                gridGroupingControl2.DataSource = new List<Classes.DiferencialAliquota.Diferencial>();
                
                foreach (var item in _documento.Where(w => w.Id == itemSelecionado.IdDiferencial))
                {
                    if (item.Existe)
                    {
                        if (!new DiferencialAliquotaBO().Excluir(itemSelecionado.Id))
                        {
                            new Notificacoes.Mensagem("Problemas durante a exclusão."+ Environment.NewLine + Publicas.mensagemDeErro, 
                                Publicas.TipoMensagem.Erro).ShowDialog();
                            return;
                        }
                    }
                    _diferencial.Remove(itemSelecionado);
                } 

                rec = _elementoGridClicado as GridRecord;
                if (rec != null)
                {
                    dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        valor = _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                        w.CodigoFl == (int)dr["CodigoFl"] &&
                                                        w.Documento == (string)dr["Numero"] &&
                                                        w.Serie == (string)dr["Serie"] &&
                                                        w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                        w.CodigoForn == (decimal)dr["CodigoForn"])
                                             .Sum(s => s.Valor);

                        debito = _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                        w.CodigoFl == (int)dr["CodigoFl"] &&
                                                        w.Documento == (string)dr["Numero"] &&
                                                        w.Serie == (string)dr["Serie"] &&
                                                        w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                        w.CodigoForn == (decimal)dr["CodigoForn"])
                                             .Sum(s => s.Debito);

                        credito = _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                        w.CodigoFl == (int)dr["CodigoFl"] &&
                                                        w.Documento == (string)dr["Numero"] &&
                                                        w.Serie == (string)dr["Serie"] &&
                                                        w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                        w.CodigoForn == (decimal)dr["CodigoForn"])
                                             .Sum(s => s.Credito);

                        foreach (var item in _documento.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                        w.CodigoFl == (int)dr["CodigoFl"] &&
                                                        w.Numero == (string)dr["Numero"] &&
                                                        w.Serie == (string)dr["Serie"] &&
                                                        w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                        w.CodigoForn == (decimal)dr["CodigoForn"]))
                        {
                            item.Base = valor;
                            item.Debito = debito;
                            item.Credito = credito;
                            item.Diferenca = debito - credito;
                        }

                        gridGroupingControl2.DataSource = _diferencial.Where(w => w.CodigoEmpresa == (int)dr["CodigoEmpresa"] &&
                                                                              w.CodigoFl == (int)dr["CodigoFl"] &&
                                                                              w.Documento == (string)dr["Numero"] &&
                                                                              w.Serie == (string)dr["Serie"] &&
                                                                              w.CodTipoDoc == (string)dr["CodTipoDoc"] &&
                                                                              w.CodigoForn == (decimal)dr["CodigoForn"]).ToList();
                    }

                }
            }

            gridGroupingControl1.DataSource = new List<Classes.DiferencialAliquota.Documento>();
            gridGroupingControl1.DataSource = _documento;

        }

        private void GerarExecelButton_Click(object sender, EventArgs e)
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

            string nomeArquivo = "DiferenciaAliquota_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_"
                               + _referencia.ToString();

            xlApp = new Excel.Application();

            try
            {

                xlApp.DisplayAlerts = false;

                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int linha = 0;
                int col = 1;               

                foreach (var itemC in _documento)
                {
                    col = 1;
                    linha++;
                    #region titulo colunas
                    xlWorkSheet.Cells[linha, col] = "Número NF ";
                    col++;

                    xlWorkSheet.Cells[linha, col] = "Fornecedor";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Tipo";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Emissão";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Entrada";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Base";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Total Débito";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Total Crédito";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Total Diferencial";

                    xlWorkSheet.Cells[linha, col] = "CFOP ";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Base";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Aliq. Externa";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Aliq. Interna";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Débito";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Crédito";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Diferencial";

                    #endregion
                    col = 1;
                    linha++;
                    
                    xlWorkSheet.Cells[linha, col] = itemC.Numero;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Fornecedor;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.CodTipoDoc;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Emissao.ToShortDateString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Entrada.ToShortDateString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemC.Base);
                    col++;
                    xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemC.Debito);
                    col++;
                    xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemC.Credito); 
                    col++;
                    xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemC.Diferenca);

                    foreach (var itemA in _diferencial.Where(w => w.CodigoEmpresa == itemC.CodigoEmpresa &&
                                                                              w.CodigoFl == itemC.CodigoFl &&
                                                                              w.Documento == itemC.Numero &&
                                                                              w.Serie == itemC.Serie &&
                                                                              w.CodTipoDoc == itemC.CodTipoDoc &&
                                                                              w.CodigoForn == itemC.CodigoForn))
                    {
                        col = 9;

                        xlWorkSheet.Cells[linha, col] = itemA.CFOP;
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemA.Valor);
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemA.Aliquota);
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemA.AliquotaExterna);
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemA.Debito);
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemA.Credito);
                        col++;
                        xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", itemA.Diferenca);

                        linha++;

                    }

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
    }
}
