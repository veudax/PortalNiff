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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Suportte.Contabilidade
{
    public partial class ConciliacaoParcelamento : Form
    {
        public ConciliacaoParcelamento()
        {
            InitializeComponent();
            
            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                this.BackColor = Publicas._fundo;
                            
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

                }
                consolidarCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            }

            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        List<Classes.Endividamento.ResumoParcelamento> _listaResumoPar;

        List<Classes.Endividamento.Parcelamento> _listaParcelas;
        List<Classes.Endividamento.Parcelamento> _listaResumo;
        List<Classes.Endividamento.Parametros> _listaParametros;
        List<Classes.Endividamento.Parametros> _listaParametrosEmail;

        Classes.Empresa _empresa;

        DateTime _dataInicio;
        DateTime _data;
        int referencia;
        
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

        private void ConciliacaoParcelamento_Shown(object sender, EventArgs e)
        {
            this.Top = 60;
            #region coloca os botões centralizados
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { gravarButton, limparButton, EnviarEmailButton };
            _botoes = Publicas.CentralizarBotoes(_botoes, this.Width, limparButton.Left - (gravarButton.Left + gravarButton.Width));

            for (int i = 0; i < _botoes.Count(); i++)
            {
                if (i == 0)
                    gravarButton.Left = _botoes[i].Left;
                if (i == 1)
                    limparButton.Left = _botoes[i].Left;
                if (i == 2)
                    EnviarEmailButton.Left = _botoes[i].Left;
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
            filter.WireGrid(gridGroupingControl2);

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

            #region Grid2

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

            #endregion

            #region Grid3

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

            #region  Totalizador do Grid 1 - Valores
            GridSummaryRowDescriptor _soma;

            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "#,##0.00;-#,##0.00; #.##";
            summaryColumnDescriptor.DataMember = "Valor";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "Valor";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

            _soma = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptor });

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
            gridGroupingControl3.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl1.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl3.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl3.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
            #endregion
            #endregion
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
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
                gravarButton.Focus();
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
            _listaParametrosEmail = new EndividamentoBO().Listar(_empresa.IdEmpresa, "U");

            mensagemSistemaLabel.Text = "";
            Refresh();
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
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

            string nomeArquivo = "Parcelamento_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_"
                               + referencia.ToString();

            xlApp = new Excel.Application();

            decimal juros = 0;

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

                foreach (var itemC in _listaResumo)
                {
                    if (itemC.Vencimento.Month == _dataInicio.Month && itemC.Vencimento.Year == _dataInicio.Year)
                    {
                        col = 1;
                        linha++;
                        Classes.Endividamento.Parametros _parametro = new Classes.Endividamento.Parametros();

                        foreach (var item in _listaParametros.Where(w => w.Modalidade == itemC.Modalidade))
                        {
                            _parametro = item;
                            break;
                        }

                        #region Juros
                        juros = itemC.JurosMes;

                        if (_parametro.CodigoContaJurosDebito != 0)
                        {
                            if (itemC.JurosMes >= 0) // desfeita a alteração do Chamado 202003-0009
                            {
                                xlWorkSheet.Cells[linha, col] = itemC.CTBJurosDebito;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.CTBJurosCredito;
                                col++;
                                xlWorkSheet.Cells[linha, col] = _parametro.CustoJuros;
                                col++;
                                xlWorkSheet.Cells[linha, col] = "";
                            }
                            else 
                            {
                                xlWorkSheet.Cells[linha, col] = itemC.CTBJurosCredito;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.CTBJurosDebito;
                                col++;
                                xlWorkSheet.Cells[linha, col] = "";
                                col++;
                                xlWorkSheet.Cells[linha, col] = _parametro.CustoJuros;
                            }

                            col++;
                            xlWorkSheet.Cells[linha, col] = _parametro.HistoricoJuros;
                            col++;
                            xlWorkSheet.Cells[linha, col] = _parametro.HistoricoJuros;
                            col++;
                            xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", Math.Abs(juros));
                            col++;
                            xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", Math.Abs(juros));
                            col++;
                            xlWorkSheet.Cells[linha, col] = _parametro.Lote;
                            col++;
                            xlWorkSheet.Cells[linha, col] = _data;
                            col++;
                            xlWorkSheet.Cells[linha, col] = "1";
                        }
                        #endregion

                        #region Previsto Conciliação

                        if (_parametro.CodigoContaLongoPrevisto != 0)
                        {
                            linha++;
                            col = 1;

                            if (itemC.ValorTransferencia < 0)
                            {
                                xlWorkSheet.Cells[linha, col] = itemC.CTBCurto;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.CTBLongo;
                                col++;
                            }
                            else
                            {
                                xlWorkSheet.Cells[linha, col] = itemC.CTBLongo;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.CTBCurto;
                                col++;
                            }

                            xlWorkSheet.Cells[linha, col] = "";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "";
                            col++;

                            xlWorkSheet.Cells[linha, col] = _parametro.HistoricoPrevisto;
                            col++;
                            xlWorkSheet.Cells[linha, col] = _parametro.HistoricoPrevisto;
                            col++;
                            xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", Math.Abs(itemC.ValorTransferencia));
                            col++;
                            xlWorkSheet.Cells[linha, col] = string.Format("{0:0.00}", Math.Abs(itemC.ValorTransferencia));
                            col++;
                            xlWorkSheet.Cells[linha, col] = _parametro.Lote;
                            col++;
                            xlWorkSheet.Cells[linha, col] = _data;
                            col++;
                            xlWorkSheet.Cells[linha, col] = "1";

                        }
                        #endregion
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

        
        private void limparButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Parcelamento>();
            gridGroupingControl2.DataSource = new List<Classes.Endividamento.Parcelamento>();

            gravarButton.Enabled = false;
            EnviarEmailButton.Enabled = false;
            referenciaMaskedEditBox.Focus();
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

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
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

            mensagemSistemaLabel.Text = "Pesquisando, aguarde...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            _listaParcelas = new EndividamentoBO().ListarParcelamento(_empresa.IdEmpresa, _data, consolidarCheckBox.Checked);
            _listaResumo = new List<Classes.Endividamento.Parcelamento>();

            decimal devedorAtual = 0;
            decimal devedor = 0;
            decimal curto = 0;
            decimal longo = 0;
            decimal longoFuturo = 0;
            decimal juros = 0;
            decimal longoMesAnterior = 0;
            decimal valorPagar = 0;
            DateTime _dataTeste;
            string modalidade = "";

            if (_listaParcelas.Where(w => w.Atualizada && w.Vencimento >= _dataInicio && w.Vencimento <= _data).Count() != 0)
            {
                new Notificacoes.Mensagem("Existem parcelamentos não atualizado." + Environment.NewLine + 
                    "Volte na tela do Parcelamento e grave o parcelamento corretamente!", Publicas.TipoMensagem.Alerta).ShowDialog();
                mensagemSistemaLabel.Text = "";
                this.Cursor = Cursors.Default;
                this.Refresh();
                referenciaMaskedEditBox.Focus();
                return;
            }

            foreach (var itemG in _listaParcelas.Where(w => w.Vencimento2 <= _data && w.Vencimento2.Year == _data.Year )
                                        //     && w.Modalidade == "Parcelamento RFB - Demais Débitos" && w.Vencimento2.Month == 5) // colocado para debugar os valores do mes 5
                                                .GroupBy(g => new { g.Modalidade, g.Vencimento2, g.CTBCurto, g.CTBLongo, g.CTBJurosDebito, g.CTBJurosCredito, g.TemContaCurtoLongo, g.TemContaJuros })
                                                .OrderBy(o => o.Key.Modalidade))
            {
                longoMesAnterior = longo;

                if (modalidade != itemG.Key.Modalidade)
                    longoMesAnterior = 0;

                modalidade = itemG.Key.Modalidade;

                devedorAtual = 0;
                devedor = 0;
                curto = 0;
                longo = 0;
                juros = 0;
                longoFuturo = 0;
                valorPagar = 0;

                foreach (var item in _listaParcelas.Where(w => w.Modalidade == itemG.Key.Modalidade && 
                                                               w.Vencimento2 == itemG.Key.Vencimento2 &&
                                                               w.CTBCurto == itemG.Key.CTBCurto &&
                                                               w.CTBLongo == itemG.Key.CTBLongo &&
                                                               w.CTBJurosDebito == itemG.Key.CTBJurosDebito &&
                                                               w.CTBJurosCredito == itemG.Key.CTBJurosCredito)
                                                   .OrderBy(o => o.Vencimento2))
                {
                    devedorAtual = devedorAtual + item.SaldoDevedorAtual;
                    devedor = devedor + item.SaldoDevedor;
                    curto = curto + item.SaldoCurto;
                    longo = longo + item.SaldoLongo;
                    juros = juros + item.JurosMes;
                    valorPagar = valorPagar + item.ValorPagar;

                    _dataTeste = item.Vencimento2.AddMonths(-1);
                    if (longoMesAnterior == 0)
                    {
                        foreach (var itemT in _listaParcelas.Where(w => w.Modalidade == itemG.Key.Modalidade &&
                                                                w.Vencimento2 == _dataTeste &&
                                                                w.CTBCurto == itemG.Key.CTBCurto &&
                                                                w.CTBLongo == itemG.Key.CTBLongo &&
                                                                w.CTBJurosDebito == itemG.Key.CTBJurosDebito &&
                                                                w.CTBJurosCredito == itemG.Key.CTBJurosCredito)
                                                    .OrderBy(o => o.Vencimento2))
                        {
                            longoMesAnterior = longoMesAnterior + itemT.SaldoLongo;
                        }
                    }  

                    if (item.Parcela == 1 && item.Vencimento2.Month == _dataInicio.Month && item.Vencimento2.Year == _dataInicio.Year)
                    {
                        // encontrou novo parcelamento no mês. Com isso irá somar todos os valores de longo prazo apartir da 14 parcela. 

                        foreach (var itemL in _listaParcelas.Where(w => w.Modalidade == itemG.Key.Modalidade &&
                                                                   w.CTBCurto == itemG.Key.CTBCurto &&
                                                                   w.CTBLongo == itemG.Key.CTBLongo &&
                                                                   w.CTBJurosDebito == itemG.Key.CTBJurosDebito &&
                                                                   w.CTBJurosCredito == itemG.Key.CTBJurosCredito &&
                                                                   w.Parcela > 13 &&
                                                                   w.IdContrato == item.IdContrato))
                        {
                            longoFuturo = longoFuturo + itemL.ValorConsolidado;
                        }
                    }
                }

                Classes.Endividamento.Parcelamento _par = new Classes.Endividamento.Parcelamento();
                _par.Modalidade = itemG.Key.Modalidade;
                _par.Vencimento = itemG.Key.Vencimento2;
                _par.CTBCurto = itemG.Key.CTBCurto;
                _par.CTBLongo = itemG.Key.CTBLongo;
                _par.CTBJurosCredito = itemG.Key.CTBJurosCredito;
                _par.CTBJurosDebito = itemG.Key.CTBJurosDebito;
                _par.SaldoCurto = curto;
                _par.SaldoDevedor = devedor;
                _par.SaldoDevedorAtual = devedorAtual;
                _par.SaldoLongo = longo;
                _par.JurosMes = juros;
                _par.ValorTransferencia = (longoMesAnterior - longo) + longoFuturo;
                _par.TemContaCurtoLongo = itemG.Key.TemContaCurtoLongo;
                _par.TemContaJuros = itemG.Key.TemContaJuros;
                _par.ValorPagar = valorPagar;

                decimal _valorCTB = new EndividamentoBO().SaldoContabil(_empresa.CodigoEmpresaGlobus, _par.Vencimento.Year.ToString("0000") + _par.Vencimento.Month.ToString("00"), itemG.Key.CTBCurto, consolidarCheckBox.Checked);
                _par.SaldoCTBCurto = Math.Abs(_valorCTB);

                _valorCTB = new EndividamentoBO().SaldoContabil(_empresa.CodigoEmpresaGlobus, _par.Vencimento.Year.ToString("0000") + _par.Vencimento.Month.ToString("00"), itemG.Key.CTBLongo, consolidarCheckBox.Checked);
                _par.SaldoCTBLongo = Math.Abs(_valorCTB);

                _par.VariacaoCurto = _par.SaldoCurto - _par.SaldoCTBCurto;
                _par.VariacaoLongo = _par.SaldoLongo - _par.SaldoCTBLongo;

                _listaResumo.Add(_par);

            }
            
            // monta o resumo
            _listaResumoPar = new List<Classes.Endividamento.ResumoParcelamento>();
            foreach (var itemG in _listaParcelas.Where(w => w.Vencimento2 >= _dataInicio))
            {

                Classes.Endividamento.ResumoParcelamento _par = new Classes.Endividamento.ResumoParcelamento();
                _par.Modalidade = itemG.Modalidade;
                _par.Fornecedor = itemG.Fornecedor;
                _par.Mes = itemG.Vencimento2.Month;
                _par.MesAno = Publicas.MesExtenso(itemG.Vencimento2.Month.ToString("00")) + "/" + itemG.Vencimento2.Year;
                _par.Ano = itemG.Vencimento2.Year.ToString();
                _par.Valor = itemG.ValorPagar;

                if (_listaResumoPar.Where(w => w.Modalidade == _par.Modalidade && w.Fornecedor == _par.Fornecedor && w.MesAno == _par.MesAno).Count() == 0)
                    _listaResumoPar.Add(_par);
                else
                {
                    foreach (var item in _listaResumoPar.Where(w => w.Modalidade == _par.Modalidade && w.Fornecedor == _par.Fornecedor && w.MesAno == _par.MesAno))
                    {
                        item.Valor = item.Valor + _par.Valor;
                    }
                }
               
            }

            gridGroupingControl1.DataSource = _listaResumo;
            gridGroupingControl2.DataSource = _listaResumo;
            gridGroupingControl3.DataSource = _listaResumoPar;

            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();
            gridGroupingControl1.Focus();

            gravarButton.Enabled = _listaResumo.Count() > 0;
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            {
                //if (e.TableCellIdentity.TableCellType == GridTableCellType.RecordFieldCell)
                {
                    Record dr;
                    GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    if (e.TableCellIdentity.Column.MappingName.Contains("JurosMes"))
                    {
                        if (rec != null)
                        {
                            dr = rec.GetRecord() as Record;
                            if (dr != null && !(bool)dr["TemContaJuros"])
                                e.Style.TextColor = Color.OrangeRed;

                        }
                    }

                    if (e.TableCellIdentity.Column.MappingName.Contains("ValorTransferencia"))
                    {
                        if (rec != null)
                        {
                            dr = rec.GetRecord() as Record;
                            if (dr != null && !(bool)dr["TemContaCurtoLongo"])
                                e.Style.TextColor = Color.OrangeRed;
                        }
                    }
                }
            }
            catch { }
        }

        private void gridGroupingControl2_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {

        }

        private void gridGroupingControl2_QueryCellStyleInfo_1(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            {
                if (e.TableCellIdentity.TableCellType == GridTableCellType.FilterBarCell)
                    return;

                if (e.TableCellIdentity.Column.MappingName.Contains("Variacao"))
                    e.Style.TextColor = Color.DarkOrange;
            }
            catch { }
        }

        private void EnviarEmailButton_Click(object sender, EventArgs e)
        {
            string detalhe = "";
            decimal valorInicial = 0;
            bool dataNaoDiaUtil = false;
            bool antecipar = false;
            bool postergar = false;

            foreach (var item in _listaParcelas.Where(w => w.Vencimento.Year == _dataInicio.Year && w.Vencimento.Month == _dataInicio.Month)
                                               .OrderBy(o => o.Vencimento)
                                               .OrderBy(o => o.Fornecedor))
            {
                FeriadoEmenda _feriado = new FeriadoBO().Consultar(item.Vencimento, _empresa.IdEmpresa);

                if (_feriado.Existe && _feriado.Tipo == "F")
                    dataNaoDiaUtil = true;

                if (item.Vencimento.DayOfWeek == DayOfWeek.Saturday || item.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                {
                    dataNaoDiaUtil = true;
                }
            }

            if (dataNaoDiaUtil)
            {
                if (new Notificacoes.Mensagem("Existe(m) Vencimento(s) em possível Feriado ou Final de semana." + Environment.NewLine
                    + "Deseja antecipar o vencimento ? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    antecipar = false;
                    if (new Notificacoes.Mensagem("Deseja postergar o vencimento ? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        postergar = false;
                    else
                        postergar = true;
                }
                else
                    antecipar = true;
            }

            mensagemSistemaLabel.Text = "Enviando E-mail, aguarde...";
            Refresh();

            foreach (var item in _listaParcelas.Where(w => w.Vencimento.Year == _dataInicio.Year && w.Vencimento.Month == _dataInicio.Month)
                                               .OrderBy(o => o.Vencimento)
                                               .OrderBy(o => o.Fornecedor)) 
            {
                valorInicial = item.ValorConsolidado;
                //foreach (var itemI in _listaParcelas.Where(w => w.IdContrato == item.IdContrato && w.Parcela == 1))
                //    = itemI.ValorPagar;

                FeriadoEmenda _feriado = new FeriadoBO().Consultar(item.Vencimento, _empresa.IdEmpresa);

                if (antecipar)
                {
                    if (_feriado.Existe && _feriado.Tipo == "F")
                        item.Vencimento = item.Vencimento.AddDays(-1);

                    if (item.Vencimento.DayOfWeek == DayOfWeek.Saturday || item.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (item.Vencimento.DayOfWeek == DayOfWeek.Saturday)
                            item.Vencimento = item.Vencimento.AddDays(-1);
                        else
                        {
                            if (item.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                                item.Vencimento = item.Vencimento.AddDays(-2);
                        }

                        _feriado = new FeriadoBO().Consultar(item.Vencimento, _empresa.IdEmpresa);

                        if (_feriado.Existe && _feriado.Tipo == "F")
                            item.Vencimento = _data.AddDays(-1);
                    }
                }

                if (postergar)
                {
                    if (_feriado.Existe && _feriado.Tipo == "F")
                        item.Vencimento = item.Vencimento.AddDays(1);

                    if (item.Vencimento.DayOfWeek == DayOfWeek.Saturday || item.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (item.Vencimento.DayOfWeek == DayOfWeek.Saturday)
                            item.Vencimento = item.Vencimento.AddDays(2);
                        else
                        {
                            if (item.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                                item.Vencimento = item.Vencimento.AddDays(1);
                        }

                        _feriado = new FeriadoBO().Consultar(item.Vencimento, _empresa.IdEmpresa);

                        if (_feriado.Existe && _feriado.Tipo == "F")
                            item.Vencimento = _data.AddDays(1);
                    }
                }
                detalhe = detalhe + "<tr> " +
                                    "<td style='font-size: 12px'; align='Left='>" + item.Fornecedor + "&nbsp;</td> " +
                                    "<td style='font-size: 12px'; align='Left'> " + item.Contrato + "</td>" +
                                    "<td style='font-size: 12px'; align='center'> " + item.Parcela + "</td>" +
                                    "<td style='font-size: 12px'; align='right'> " + string.Format("{0:#,##0.00}", valorInicial) + "&nbsp;</td> " +
                                    "<td style='font-size: 12px'; align='right'> " + string.Format("{0:#,##0.00}", item.ValorPagar) + "&nbsp;</td> " +
                                    "<td style='font-size: 12px'; align='right'> " + item.Vencimento.ToShortDateString() + "&nbsp;</td> " +
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
                mensagemSistemaLabel.Text = "";
                Refresh();

                new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            foreach (var item in _listaParametrosEmail)
            {
                if (_listaUsuarios.Where(w => w.Id == item.IdUsuario).Count() != 0)
                    emailDestino = emailDestino + item.Email + "; ";
            }

            //emailDestino = "mdmunoz@supportse.com.br";
            Publicas.mensagemDeErro = "";
            Classes.Publicas.EnviarEmailParcelamento(_dadosEmail, Publicas._usuario.Email, emailDestino, emailCopia, "Valores a recolher no mês - Parcelamentos");

            if (Publicas.mensagemDeErro != "")
                new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Alerta).ShowDialog();
            else
                new Notificacoes.Mensagem("E-mail enviado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();

            mensagemSistemaLabel.Text = "";
            Refresh();

            #endregion
        }

        private void consolidarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                referenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void gridGroupingControl3_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {

        }

        private void gerarExcelToolStripMenuItem_Click(object sender, EventArgs e)
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

            string nomeArquivo = "Parcelamento_ResumoMensal_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_");

            xlApp = new Excel.Application();

            try
            {

                xlApp.DisplayAlerts = false;

                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Name = "Detalhado";

                int linha = 1;
                int col = 1;
                int linhaAno = 0;
                int linhaModalidade = 0;
                int linhaFornecedor = 0;
                int colModalidade = 0;
                int colAno = 0;
                int colFornecedor = 0;
                decimal TotalAno = 0;
                decimal TotalModalidade = 0;
                decimal TotalFornecedor = 0;
                decimal total = 0;

                // resumo detalhado
                foreach (var item in _listaResumoPar.GroupBy(o => o.Ano))
                {
                    if (colAno != 0)
                        xlWorkSheet.Cells[linhaAno, colAno] = TotalAno;

                    TotalAno = 0;
                    col = 1;
                    linhaAno = linha;
                    xlWorkSheet.Cells[linha, col] = "Ano ";
                    col++;
                    xlWorkSheet.Cells[linha, col] = item.Key;
                    linha++;
                    colAno = col+1;

                    col = 1;

                    foreach (var itemM in _listaResumoPar.Where(w => w.Ano == item.Key)
                                                         .GroupBy(o => o.Modalidade))
                    {
                        if (colModalidade != 0)
                            xlWorkSheet.Cells[linhaModalidade, colModalidade] = TotalModalidade;

                        TotalModalidade = 0;
                        col = 1;
                        linhaModalidade = linha;
                        xlWorkSheet.Cells[linha, col] = "Modalidade ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = itemM.Key;
                        linha++;
                        colModalidade = col+1;


                        foreach (var itemF in _listaResumoPar.Where(w => w.Modalidade == itemM.Key && w.Ano == item.Key)
                                                             .GroupBy(o => o.Fornecedor))
                        {
                            if (colFornecedor != 0)
                                xlWorkSheet.Cells[linhaFornecedor, colFornecedor] = TotalFornecedor;

                            TotalFornecedor = 0;

                            col = 1;
                            xlWorkSheet.Cells[linha, col] = "Fornecedor ";
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemF.Key;
                            col++;
                            colFornecedor = col;
                            linhaFornecedor = linha;

                            linha++;
                            
                            foreach (var itemV in _listaResumoPar.Where(w => w.Modalidade == itemM.Key && w.Ano == item.Key && w.Fornecedor == itemF.Key)
                                                                 .OrderBy(o => o.Mes))
                            {
                                col = 2;
                                xlWorkSheet.Cells[linha, col] = itemV.MesAno;
                                col++;

                                xlWorkSheet.Cells[linha, col] = itemV.Valor;
                                col++;

                                total = total + itemV.Valor;
                                TotalAno = TotalAno + itemV.Valor;
                                TotalModalidade = TotalModalidade + itemV.Valor;
                                TotalFornecedor = TotalFornecedor + itemV.Valor;
                                linha++;
                            }

                            linha++;
                        }

                        linha++;
                    }
                }

                linha++;
                col = 1;
                xlWorkSheet.Cells[linhaFornecedor, colFornecedor] = TotalFornecedor;
                xlWorkSheet.Cells[linhaModalidade, colModalidade] = TotalModalidade;
                xlWorkSheet.Cells[linhaAno, colAno] = TotalAno;

                xlWorkSheet.Cells[linha, 2] = "Total Geral";
                xlWorkSheet.Cells[linha, 3] = total;
                xlWorkSheet.Columns.AutoFit();

                // resumo ano
                int qtde = xlWorkBook.Worksheets.Count;
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, xlWorkBook.Worksheets[qtde], Type.Missing, Type.Missing);
                xlWorkSheet.Name = "Total por Ano";

                linha = 1;
                foreach (var item in _listaResumoPar.GroupBy(o => o.Ano))
                {
                    TotalAno = _listaResumoPar.Where(w => w.Ano == item.Key)
                                              .Sum(s => s.Valor);

                    xlWorkSheet.Cells[linha, 1] = item.Key;
                    xlWorkSheet.Cells[linha, 2] = TotalAno;
                    linha++;
                }
                xlWorkSheet.Cells[linha, 1] = "Total Geral";
                xlWorkSheet.Cells[linha, 2] = total;
                linha++;

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
