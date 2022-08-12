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
    public partial class Parcelamento : Form
    {
        public Parcelamento() //Inicio Tela Parcelamento
        {
            InitializeComponent();

            inicialDateTimePicker.BorderColor = Publicas._bordaSaida;
            inicialDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
            inicialDateTimePicker.Value = DateTime.Now;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                this.BackColor = Publicas._fundo;

                foreach (Control item in this.Controls)
                {
                    if (item is CurrencyTextBox)
                    {
                        ((CurrencyTextBox)item).DecimalValue = 0;
                        ((CurrencyTextBox)item).Tag = null;
                        ((CurrencyTextBox)item).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                        ((CurrencyTextBox)item).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                        ((CurrencyTextBox)item).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                        ((CurrencyTextBox)item).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                        ((CurrencyTextBox)item).BackGroundColor = Publicas._fundo;
                        ((CurrencyTextBox)item).MinValue = (decimal)-999999999999.99;
                        ((CurrencyTextBox)item).MaxValue = (decimal)999999999999.99;
                    }
                }
            
                if (Publicas._TemaBlack)
                {
                    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl1.BackColor = Publicas._panelTitulo;
                }
            }

            AplicarSelicCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            JurosMultaDiferenciadasCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            ModalidadeComboBox.BackColor = empresaComboBoxAdv.BackColor;
            ModalidadeComboBox.ForeColor = empresaComboBoxAdv.ForeColor;
            ProximoCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            ParcelasDiferenciadasCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            ZerarParcelasCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            AplicarUFGCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            ReprocessarCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            VencimentoDiaUtilCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            VencimentoDiaUtilAnteriorCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            CustaApenas1ParcelaCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            ReducaoApenas1ParcelaCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;

            foreach (Control item in this.Controls)
            {
                if (item is Panel)
                {
                    foreach (Control itemP in item.Controls)
                    {
                        if (itemP is TabControlAdv)
                        {
                            foreach (Control itemT in itemP.Controls)
                            {
                                if (itemT is TabPageAdv)
                                {
                                    foreach (Control itemX in itemT.Controls)
                                    {
                                        if (itemX is CurrencyTextBox)
                                        {
                                            ((CurrencyTextBox)itemX).DecimalValue = 0;
                                            ((CurrencyTextBox)itemX).Tag = null;
                                            ((CurrencyTextBox)itemX).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                            ((CurrencyTextBox)itemX).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                            ((CurrencyTextBox)itemX).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                                            ((CurrencyTextBox)itemX).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                            ((CurrencyTextBox)itemX).BackGroundColor = empresaComboBoxAdv.BackColor;
                                            ((CurrencyTextBox)itemX).MinValue = (decimal)-999999999.99;
                                            ((CurrencyTextBox)itemX).MaxValue = (decimal)999999999.99;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }

            Publicas._mensagemSistema = string.Empty;
        } // fim Tela Parcelamento

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;

        List<Classes.Endividamento.Parcelamento> _listaParcelas;
        List<Classes.Endividamento.Parcelamento> _listaParcelasLog;
        List<Classes.Endividamento.Arquivo> _listaArquivos;
        List<Classes.Endividamento.Arquivo> _listaArquivosLog;
        List<Classes.Endividamento.Selic> _listaSelic;
        List<Classes.Endividamento.Parametros> _listaParametros;
        List<Classes.Endividamento.Contrato> _listaContratos;

        Classes.Empresa _empresa;
        Classes.FornecedoresGlobus _fornecedor;
        Classes.Endividamento.Contrato _contrato;
        
        //DateTime _dataInicio;
        //bool encontrou;
        int _topArquivos = 8;
        int _qtd = 0;
        int posicaoProximo;
        bool temAlteracao = false;
        bool valorAjustado = false;
        bool saiuDaModalidade = false;
        bool naoCalcularSaldoDevedor = false;
        bool continuacaoDeParcelamento = false;
        bool reprocessarValores = false;

        string[] arquivo;
        DateTime _data = DateTime.Now;
        DateTime _dataselic = DateTime.Now;
        decimal _valor = 0;
        decimal _juros = 0;
        decimal _multa = 0;
        decimal _honorarios = 0;
        decimal _correcao = 0;
        decimal _reducao = 0;
        decimal _custas = 0;
        decimal _encargos = 0;
        decimal _saldoDevedor = 0;
        decimal _saldoAtual = 0;
        decimal _jurosMes = 0;
        decimal total = 0;
        decimal totalJuros = 0;
        decimal totalMulta = 0;
        decimal totalHonorario = 0;
        decimal _parcelaEmUFG = 0;


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

        private void Parcelamento_Shown(object sender, EventArgs e)
        {
            this.Top = 60;
            _listaEmpresas = new EmpresaBO().Listar(false);

            ModalidadeComboBox.Items.AddRange(new object[] { "Parcelamento RFB - Previdenciário", "Parcelamento RFB - Demais Débitos", "Parcelamento PGFN - Previdenciário", "Parcelamento PGFN - Demais Débitos", "Parcelamento", "Pert", "PMG" });
            ModalidadeComboBox.SelectedIndex = -1;

            JurosMultaDiferenciadasCheckBox_CheckStateChanged(sender, e);

            #region coloca os botões centralizados
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { gravarButton, limparButton, excluirButton, GerarExcelButton };
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
                    GerarExcelButton.Left = _botoes[i].Left;
            }
            #endregion

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

            GridSummaryRowDescriptor _soma;

            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor.DataMember = "ValorParcelado";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "ValorParcelado";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor1 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor1.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor1.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor1.DataMember = "JurosEMulta";
            summaryColumnDescriptor1.Format = "{Sum}";
            summaryColumnDescriptor1.Name = "JurosEMulta";
            summaryColumnDescriptor1.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor2.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor2.DataMember = "ValorConsolidado";
            summaryColumnDescriptor2.Format = "{Sum}";
            summaryColumnDescriptor2.Name = "ValorConsolidado";
            summaryColumnDescriptor2.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor3 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor3.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor3.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor3.DataMember = "Juros";
            summaryColumnDescriptor3.Format = "{Sum}";
            summaryColumnDescriptor3.Name = "Juros";
            summaryColumnDescriptor3.SummaryType = SummaryType.DoubleAggregate;
            
            GridSummaryColumnDescriptor summaryColumnDescriptor4 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor4.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor4.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor4.DataMember = "ValorPagar";
            summaryColumnDescriptor4.Format = "{Sum}";
            summaryColumnDescriptor4.Name = "ValorPagar";
            summaryColumnDescriptor4.SummaryType = SummaryType.DoubleAggregate;
                                   
            _soma = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1, summaryColumnDescriptor2, summaryColumnDescriptor3, summaryColumnDescriptor4 });

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

            ReprocessarCheckBox.Visible = Publicas._usuario.ReprocessaParcelamento || Publicas._usuario.Desenvolvedor;
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void FornecedorTextBox_Enter(object sender, EventArgs e)
        {
            FornecedorTextBox.BorderColor = Publicas._bordaEntrada;
            pesquisaFornecedorButton.Enabled = string.IsNullOrEmpty(FornecedorTextBox.Text.Trim());
        }

        private void PrevistoCurrency_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void ContratoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;

            if (saiuDaModalidade)
            {
                ContratoTextBox.Focus();
                saiuDaModalidade = false;
                return;
            }
        }

        private void inicialDateTimePicker_Enter(object sender, EventArgs e)
        {
            inicialDateTimePicker.BorderColor = Publicas._bordaEntrada;
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
                ProximoCheckBox.Focus();
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

        private void inicialDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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
                new Notificacoes.Mensagem("Selecione a Empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            mensagemSistemaLabel.Text = "Pesquisando Parâmetros, aguarde...";
            Refresh();

            _listaSelic = new EndividamentoBO().Listar();
            _listaParametros = new EndividamentoBO().Listar(_empresa.IdEmpresa, "F");

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

        }

        private void PrincipalCurrency_Validating(object sender, CancelEventArgs e)
        {
            PrincipalCurrency.BorderColor = Publicas._bordaSaida;

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
        }

        private void MultaCurrency_Validating(object sender, CancelEventArgs e)
        {
            MultaCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void ConsolidadoCurrency_Validating(object sender, CancelEventArgs e)
        {
            ConsolidadoCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void QuantidadeCurrency_Validating(object sender, CancelEventArgs e)
        {
            QuantidadeCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void CalculaParcelas(int QtdParcela, int parcelaInicial, decimal _saldoDevedor, decimal _saldoAtual, decimal _jurosMes, bool diferenciada = false, bool zeraParcelasAdesao = false, int qtdParcelaZerar = 0)
        {
            
            valorAjustado = false;
            decimal _valor1ParcelaAdesao = 0;
            int Qtd = (QtdParcela - qtdParcelaZerar) + 1;

            for (int i = parcelaInicial; i <= QtdParcela; i++)
            {
                Classes.Endividamento.Parcelamento _par = new Classes.Endividamento.Parcelamento();

                _par.Parcela = i;

                _par.ValorParcelado = _valor;
                _par.JurosParcelado = _juros;
                _par.MultaParcelada = _multa;
                _par.Honorarios = _honorarios;
                _par.Diferenciada = diferenciada;
                
                if (!ReducaoApenas1ParcelaCheckBox.Checked)
                    _par.Reducao = _reducao;
                else
                {
                    if (_par.Parcela == 1)
                        _par.Reducao = ReducaoCurrency.DecimalValue;
                    else
                        _par.Reducao = 0;
                }

                if (!CustaApenas1ParcelaCheckBox.Checked)
                    _par.Custas = _custas;
                else
                {
                    if (_par.Parcela == 1)
                        _par.Custas = CustasCurrency.DecimalValue;
                    else
                        _par.Custas = 0;
                }

                _par.Correcao = _correcao;


                if (diferenciada && zeraParcelasAdesao)
                {
                    if (qtdParcelaZerar <= i)
                    {
                        _par.ValorParcelado = 0;
                        _par.JurosParcelado = 0;
                        _par.MultaParcelada = 0;
                        _par.Honorarios = 0;
                        _par.Correcao = 0;
                        _par.Custas = 0;
                        _par.Reducao = 0;
                    }
                    else
                    {
                        if (i <= 2)
                        {
                            _par.ValorParcelado = _par.ValorParcelado + ((_valor * Qtd) / 2);
                            _par.JurosParcelado = _par.JurosParcelado + ((_juros * Qtd) / 2);
                            _par.MultaParcelada = _par.MultaParcelada + ((_multa * Qtd) / 2);
                            _par.Honorarios = _par.Honorarios + ((_honorarios * Qtd) / 2);


                            if (i == 1)
                                _valor1ParcelaAdesao = _par.ValorParcelado;
                        }
                    }
                }

                if (!diferenciada)
                {
                    if (i == parcelaInicial)
                    { 
                        if (total != PrincipalCurrency.DecimalValue || totalJuros != JurosCurrency.DecimalValue || 
                            totalMulta != MultaCurrency.DecimalValue || totalHonorario != HonorariosCurrency.DecimalValue)
                        {
                            if (new Notificacoes.Mensagem("Existe diferença entre o total calculado com o total informado." + Environment.NewLine +
                                Environment.NewLine + "Deseja recalcular a primeira parcela?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                                valorAjustado = true;
                        }

                        if (valorAjustado)
                        {
                            if (total != PrincipalCurrency.DecimalValue)
                                _par.ValorParcelado = _valor - (total - PrincipalCurrency.DecimalValue);

                            if (!AplicarUFGCheckBox.Checked)
                            {
                                if (totalJuros != JurosCurrency.DecimalValue)
                                    _par.JurosParcelado = _juros - (totalJuros - JurosCurrency.DecimalValue);

                                if (totalMulta != MultaCurrency.DecimalValue)
                                    _par.MultaParcelada = _multa - (totalMulta - MultaCurrency.DecimalValue);

                                if (totalHonorario != HonorariosCurrency.DecimalValue)
                                    _par.Honorarios = _honorarios - (totalHonorario - HonorariosCurrency.DecimalValue);
                            }
                        }
                    }
                }

                _par.JurosEMulta = (_par.JurosParcelado + _par.MultaParcelada + _par.Honorarios + _par.Correcao + 
                    (CustaApenas1ParcelaCheckBox.Checked && _par.Parcela == 1 ? _par.Custas : (!CustaApenas1ParcelaCheckBox.Checked ? _par.Custas : 0) ) ) -  
                    (ReducaoApenas1ParcelaCheckBox.Checked && _par.Parcela == 1 ? _par.Reducao : (!ReducaoApenas1ParcelaCheckBox.Checked ? _par.Reducao : 0)) ;

                _par.ValorConsolidado = _par.ValorParcelado + _par.JurosEMulta;

                //                _parcelaEmUFG = _par.ValorConsolidado; // 31.03.2021 - removido, a variável _parcelaEmUFG não é igula a _par.ValorConsolidado

                if (!AplicarUFGCheckBox.Checked)
                {
                    foreach (var item in _listaSelic.Where(w => w.Ano == _data.Year && w.ValorUFG > 0))
                    {
                        _parcelaEmUFG = Math.Round(_parcelaEmUFG / item.ValorUFG, 4);
                        break;
                    }

                    if (_parcelaEmUFG == _par.ValorConsolidado)
                    {
                        try
                        {
                            string ano = _listaSelic.Where(w => w.Ano <= _data.Year && w.ValorUFG > 0)
                                                        .Max(m => m.Ano).ToString();

                            foreach (var item in _listaSelic.Where(w => w.Ano == Convert.ToInt32(ano) && w.ValorUFG > 0))
                            {
                                _parcelaEmUFG = Math.Round(_parcelaEmUFG / item.ValorUFG, 4);
                                break;
                            }
                        }
                        catch { }
                    }
                    _par.ParcelaEmUFG = 0;
                    _par.ParcelaEmUFG = _parcelaEmUFG; //verificar
                }

                _par.Vencimento = _data;

                if (VencimentoDiaUtilCheckBox.Checked)
                {
                    FeriadoEmenda _feriado = new FeriadoBO().Consultar(_par.Vencimento, _empresa.IdEmpresa);

                    if (_feriado.Existe && _feriado.Tipo == "F")
                        _par.Vencimento = _par.Vencimento.AddDays(1);

                    if (_par.Vencimento.DayOfWeek == DayOfWeek.Saturday || _par.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (_par.Vencimento.DayOfWeek == DayOfWeek.Saturday)
                            _par.Vencimento = _par.Vencimento.AddDays(2);
                        else
                        {
                            if (_par.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                                _par.Vencimento = _par.Vencimento.AddDays(1);
                        }

                        _feriado = new FeriadoBO().Consultar(_par.Vencimento, _empresa.IdEmpresa);

                        if (_feriado.Existe && _feriado.Tipo == "F")
                            _par.Vencimento = _data.AddDays(1);
                    }
                }
                else
                {
                    if (VencimentoDiaUtilAnteriorCheckBox.Checked)
                    {
                        FeriadoEmenda _feriado = new FeriadoBO().Consultar(_par.Vencimento, _empresa.IdEmpresa);

                        if (_feriado.Existe && _feriado.Tipo == "F")
                            _par.Vencimento = _par.Vencimento.AddDays(-1);

                        if (_par.Vencimento.DayOfWeek == DayOfWeek.Saturday || _par.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                        {
                            if (_par.Vencimento.DayOfWeek == DayOfWeek.Saturday)
                                _par.Vencimento = _par.Vencimento.AddDays(-1);
                            else
                            {
                                if (_par.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                                    _par.Vencimento = _par.Vencimento.AddDays(-2);
                            }

                            _feriado = new FeriadoBO().Consultar(_par.Vencimento, _empresa.IdEmpresa);

                            if (_feriado.Existe && _feriado.Tipo == "F")
                                _par.Vencimento = _data.AddDays(-1);
                        }
                    }
                }

                _par.Vencimento2 = Convert.ToDateTime("01/" + _par.Vencimento.Month + "/" + _par.Vencimento.Year).AddMonths(1).AddDays(-1); // ultimo dia do mes.

                if (((i == 2 && !JurosMultaDiferenciadasCheckBox.Checked) ||
                     (JurosMultaDiferenciadasCheckBox.Checked && i == 1)) &&
                     !continuacaoDeParcelamento)
                {
                    _par.PercentualJuros = PercentualJurosCurrency.DecimalValue;
                    _encargos = _par.PercentualJuros;
                }

                if (AplicarUFGCheckBox.Checked)
                {
                    bool _encontrou = false;
                    foreach (var item in _listaSelic.Where(w => w.Ano == _data.Year && w.ValorUFG > 0))
                    {
                        _encontrou = true;
                        _par.UFG = item.ValorUFG;
                        break;
                    }
                    if (!_encontrou)
                    {
                        string ano = _listaSelic.Where(w => w.Ano <= _data.Year && w.ValorUFG > 0)
                                                .Max(m => m.Ano).ToString();

                        foreach (var item in _listaSelic.Where(w => w.Ano == Convert.ToInt32(ano) && w.ValorUFG > 0))
                        {
                            _par.UFG = item.ValorUFG;
                            break;
                        }
                    }
                }
                else
                {
                    if (((i > 2 && AplicarSelicCheckBox.Checked && !JurosMultaDiferenciadasCheckBox.Checked) ||
                         (JurosMultaDiferenciadasCheckBox.Checked && i > 1 && AplicarSelicCheckBox.Checked)) ||
                         continuacaoDeParcelamento)
                    {
                        foreach (var item in _listaSelic.Where(w => w.MesAno == _dataselic.Month.ToString("00") + "/" + _dataselic.Year.ToString()))
                        {
                            _par.Selic = item.Valor;
                        }
                    }
                }
                _par.Encargos = _encargos + _par.Selic;
                if (!AplicarUFGCheckBox.Checked)
                {
                    if (i == 1 && !diferenciada)
                    {
                        _saldoDevedor = ConsolidadoCurrency.DecimalValue - _par.ValorConsolidado;
                        _saldoAtual = _saldoDevedor;
                        if (!ParcelasDiferenciadasCheckBox.Checked)
                            _par.ValorPagar = _par.ValorConsolidado;
                        else
                        {
                            _par.Juros = _par.ValorConsolidado * (_par.Encargos / 100);

                            if (diferenciada && zeraParcelasAdesao && _par.ValorParcelado != 0)
                                _par.Juros = _valor1ParcelaAdesao * (_par.Encargos / 100);

                            _par.ValorPagar = _par.ValorConsolidado + _par.Juros;
                            _saldoAtual = _saldoDevedor + (_saldoDevedor * (_par.Encargos / 100));
                            _par.JurosMes = _saldoAtual - (ConsolidadoCurrency.DecimalValue - _par.ValorConsolidado);
                        }
                    }
                    else
                    {
                        if (!diferenciada)
                        {
                            _par.Juros = _par.ValorConsolidado * (_par.Encargos / 100);

                            if (diferenciada && zeraParcelasAdesao && _par.ValorParcelado != 0)
                                _par.Juros = _valor1ParcelaAdesao * (_par.Encargos / 100);

                            _par.ValorPagar = _par.ValorConsolidado + _par.Juros;

                            _saldoDevedor = _saldoDevedor - _par.ValorConsolidado;

                            _jurosMes = _saldoAtual;
                            _saldoAtual = _saldoDevedor + (_saldoDevedor * (_par.Encargos / 100));
                            _par.JurosMes = _saldoAtual - (_jurosMes - _par.ValorConsolidado);
                        }
                    }
                }
                else
                {
                    _par.ParcelaEmUFG = 0; //teste
                    _par.ParcelaEmUFG = _parcelaEmUFG; //teste
                    _par.ValorPagar = Math.Round(_par.ParcelaEmUFG * _par.UFG,2);
                    _par.Juros = _par.ValorPagar - _par.ValorParcelado;
                }

                if (!naoCalcularSaldoDevedor) // calcular
                {
                    _par.SaldoDevedor = _saldoDevedor;
                    _par.SaldoDevedorAtual = _saldoAtual;
                }

                _par.PercentualJurosDiferenciado = JurosDiferenciadoCurrency.DecimalValue;
                _par.PercentualMultaDiferenciada = MultaDiferenciadaCurrency.DecimalValue;
                _par.PercentualSelicDiferenciada = SelicDiferenciadaCurrency.DecimalValue;

                _par.ValorPrincipalAtualizado = Math.Round(_par.ValorParcelado + (_par.ValorParcelado * (_par.Encargos / 100)), 2);
                _par.MultaDiferenciada = Math.Round(_par.ValorPrincipalAtualizado * (_par.PercentualMultaDiferenciada / 100), 2);

                if (JurosMultaDiferenciadasCheckBox.Checked)
                {
                    _par.Juros = Math.Round(_par.ValorPrincipalAtualizado * (_par.PercentualSelicDiferenciada / 100), 2);

                    _par.JurosDiferenciado = Math.Round((_par.ValorPrincipalAtualizado + _par.Juros) * (_par.PercentualJurosDiferenciado / 100), 2);
                    _par.ValorPagar = _par.ValorPrincipalAtualizado + _par.MultaDiferenciada + _par.JurosDiferenciado + _par.Juros;
                }
                _encargos = _par.Encargos;

                _dataselic = _data;
                _data = _data.AddMonths(1);

                if (DiaCurrency.DecimalValue != 0)
                {
                    if (_data.Month == 2 && DiaCurrency.DecimalValue > 28)
                        _data = Convert.ToDateTime("28/" + _data.Month + "/" + _data.Year);
                    else
                    {
                        if ((_data.Month == 4 || _data.Month == 6 || _data.Month == 9 || _data.Month == 11) && DiaCurrency.DecimalValue > 30)
                            _data = Convert.ToDateTime("30/" + _data.Month + "/" + _data.Year);
                        else
                            _data = Convert.ToDateTime((int)DiaCurrency.DecimalValue + "/" + _data.Month + "/" + _data.Year);
                    }
                }
                
                if (reprocessarValores)
                {
                    foreach (var item in _listaParcelasLog.Where(w => w.Parcela == _par.Parcela && w.Diferenciada == _par.Diferenciada))
                    {
                        _par.Id = item.Id;
                        _par.IdContrato = item.IdContrato;
                        _par.Existe = true;
                    } 
                }
                
                _listaParcelas.Add(_par);
            }
        }

        private void inicialDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            inicialDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            decimal _valor1 = 0;
            decimal _juros1 = 0;
            decimal _multa1 = 0;
            decimal _honorarios1 = 0;

            _valor = 0;
            _juros = 0;
            _multa = 0;
            _honorarios = 0;
            _custas = 0;
            _reducao = 0;
            _correcao = 0;
            _encargos = 0;
            _saldoDevedor = 0;
            _saldoAtual = 0;
            _jurosMes = 0;
            total = 0;
            totalJuros = 0;
            totalMulta = 0;
            int QtdParcela = 0;
            int parcelaInicial = 1;
            valorAjustado = false;

            _data = inicialDateTimePicker.Value.Date;
            _dataselic = inicialDateTimePicker.Value.Date.AddDays(-30);
            _listaParcelas = new List<Classes.Endividamento.Parcelamento>(); //Qtde Parcelas

            mensagemSistemaLabel.Text = "Calculando as parcelas, aguarde...";
            Refresh();

            if (!_contrato.Existe || reprocessarValores)
            {
                naoCalcularSaldoDevedor = false;
                continuacaoDeParcelamento = false;

                if (ParcelasDiferenciadasCheckBox.Checked)
                {
                    if (PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue > 0)
                    {
                        #region Primeira parcela
                        _valor1 = PrincipalCurrency.DecimalValue * (PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue / 100);
                        _juros1 = JurosCurrency.DecimalValue * (PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue / 100);
                        _multa1 = MultaCurrency.DecimalValue * (PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue / 100);
                        _honorarios1 = HonorariosCurrency.DecimalValue * (PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue / 100);

                        _listaParcelas = new List<Classes.Endividamento.Parcelamento>();

                        Classes.Endividamento.Parcelamento _par = new Classes.Endividamento.Parcelamento();

                        _par.ValorParcelado = _valor1;
                        _par.JurosParcelado = _juros1;
                        _par.MultaParcelada = _multa1;
                        _par.Honorarios = _honorarios1;
                        _par.Diferenciada = true;

                        _par.JurosEMulta = _par.JurosParcelado + _par.MultaParcelada;
                        _par.ValorConsolidado = _par.ValorParcelado + _par.JurosParcelado + _par.MultaParcelada;
                        _par.Parcela = 1;
                        _par.Vencimento = _data;

                        if (VencimentoDiaUtilCheckBox.Checked)
                        {
                            FeriadoEmenda _feriado = new FeriadoBO().Consultar(_par.Vencimento, _empresa.IdEmpresa);

                            if (_feriado.Existe && _feriado.Tipo == "F")
                                _par.Vencimento = _par.Vencimento.AddDays(1);

                            if (_par.Vencimento.DayOfWeek == DayOfWeek.Saturday || _par.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                            {
                                if (_par.Vencimento.DayOfWeek == DayOfWeek.Saturday)
                                    _par.Vencimento = _par.Vencimento.AddDays(2);
                                else
                                {
                                    if (_par.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                                        _par.Vencimento = _par.Vencimento.AddDays(1);
                                }

                                _feriado = new FeriadoBO().Consultar(_par.Vencimento, _empresa.IdEmpresa);

                                if (_feriado.Existe && _feriado.Tipo == "F")
                                    _par.Vencimento = _data.AddDays(1);
                            }
                        }
                        else
                        {
                            if (VencimentoDiaUtilAnteriorCheckBox.Checked)
                            {
                                FeriadoEmenda _feriado = new FeriadoBO().Consultar(_par.Vencimento, _empresa.IdEmpresa);

                                if (_feriado.Existe && _feriado.Tipo == "F")
                                    _par.Vencimento = _par.Vencimento.AddDays(-1);

                                if (_par.Vencimento.DayOfWeek == DayOfWeek.Saturday || _par.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    if (_par.Vencimento.DayOfWeek == DayOfWeek.Saturday)
                                        _par.Vencimento = _par.Vencimento.AddDays(-1);
                                    else
                                    {
                                        if (_par.Vencimento.DayOfWeek == DayOfWeek.Sunday)
                                            _par.Vencimento = _par.Vencimento.AddDays(-2);
                                    }

                                    _feriado = new FeriadoBO().Consultar(_par.Vencimento, _empresa.IdEmpresa);

                                    if (_feriado.Existe && _feriado.Tipo == "F")
                                        _par.Vencimento = _data.AddDays(-1);
                                }
                            }
                        }

                        _par.Vencimento2 = Convert.ToDateTime("01/" + _par.Vencimento.Month + "/" + _par.Vencimento.Year).AddMonths(1).AddDays(-1); // ultimo dia do mes.


                        if (JurosMultaDiferenciadasCheckBox.Checked)
                        {
                            _par.PercentualJuros = PercentualJurosCurrency.DecimalValue;
                            _encargos = _par.PercentualJuros;
                        }

                        _par.Encargos = _encargos + _par.Selic;

                        _saldoDevedor = ConsolidadoCurrency.DecimalValue - _par.ValorConsolidado;
                        _saldoAtual = _saldoDevedor;
                        _par.ValorPagar = _par.ValorConsolidado;

                        _par.SaldoDevedor = _saldoDevedor;
                        _par.SaldoDevedorAtual = _saldoAtual;

                        _par.PercentualJurosDiferenciado = JurosDiferenciadoCurrency.DecimalValue;
                        _par.PercentualMultaDiferenciada = MultaDiferenciadaCurrency.DecimalValue;
                        _par.PercentualSelicDiferenciada = SelicDiferenciadaCurrency.DecimalValue;

                        _par.ValorPrincipalAtualizado = Math.Round(_par.ValorParcelado + (_par.ValorParcelado * (_par.Encargos / 100)), 2);
                        _par.MultaDiferenciada = Math.Round(_par.ValorPrincipalAtualizado * (_par.PercentualMultaDiferenciada / 100), 2);

                        if (JurosMultaDiferenciadasCheckBox.Checked)
                        {
                            _par.Juros = Math.Round(_par.ValorPrincipalAtualizado * (_par.PercentualSelicDiferenciada / 100), 2);

                            _par.JurosDiferenciado = Math.Round((_par.ValorPrincipalAtualizado + _par.Juros) * (_par.PercentualJurosDiferenciado / 100), 2);
                            _par.ValorPagar = _par.ValorPrincipalAtualizado + _par.MultaDiferenciada + _par.JurosDiferenciado + _par.Juros;
                        }
                        _encargos = _par.Encargos;

                        _dataselic = _data;
                        _data = _data.AddMonths(1);

                        if (DiaCurrency.DecimalValue != 0)
                        {
                            if (_data.Month == 2 && DiaCurrency.DecimalValue > 28)
                                _data = Convert.ToDateTime("28/" + _data.Month + "/" + _data.Year);
                            else
                            {
                                if ((_data.Month == 4 || _data.Month == 6 || _data.Month == 9 || _data.Month == 11) && DiaCurrency.DecimalValue > 30)
                                    _data = Convert.ToDateTime("30/" + _data.Month + "/" + _data.Year);
                                else
                                    _data = Convert.ToDateTime((int)DiaCurrency.DecimalValue + "/" + _data.Month + "/" + _data.Year);
                            }
                        }
                        
                        if (reprocessarValores)
                        {
                            foreach (var item in _listaParcelasLog.Where(w => w.Parcela == _par.Parcela && w.Diferenciada == _par.Diferenciada))
                            {
                                _par.Id = item.Id;
                                _par.IdContrato = item.IdContrato;
                                _par.Existe = true;
                            }
                        }
                        _listaParcelas.Add(_par);
                        #endregion

                        QtdParcela = (int)QuantidadeCurrency.DecimalValue;
                        parcelaInicial = 2;
                        _valor = (PrincipalCurrency.DecimalValue - _valor1) / ((int)QuantidadeCurrency.DecimalValue - 1);
                        _juros = (JurosCurrency.DecimalValue - _juros1) / ((int)QuantidadeCurrency.DecimalValue - 1);
                        _multa = (MultaCurrency.DecimalValue - _multa1) / ((int)QuantidadeCurrency.DecimalValue - 1);
                        _honorarios = (HonorariosCurrency.DecimalValue - _honorarios1) / ((int)QuantidadeCurrency.DecimalValue - 1);

                        // ver aqui
                        total = (_valor * (QtdParcela-1)) + _valor1;
                        totalJuros = (_juros * (QtdParcela-1)) +_juros1 ;
                        totalMulta = (_multa * (QtdParcela-1)) + _multa1;
                        totalHonorario = (_honorarios * (QtdParcela-1)) + _honorarios1;
                    }
                    else
                    {
                        _valor = AdesaoCurrency.DecimalValue / QtdeParcelasDiferenciadaCurrencyTextBox.DecimalValue;
                        _juros = 0;
                        _multa = 0;
                        _honorarios = 0;

                        parcelaInicial = 1;
                        QtdParcela = (int)QtdeParcelasDiferenciadaCurrencyTextBox.DecimalValue;
                        naoCalcularSaldoDevedor = true;
                        CalculaParcelas(QtdParcela, parcelaInicial, _saldoDevedor, _saldoAtual, _jurosMes, true, ZerarParcelasCheckBox.Checked, (int)ParcelasAZeraCurrency.DecimalValue);
                        naoCalcularSaldoDevedor = false;

                        continuacaoDeParcelamento = true;
                        _saldoDevedor = 0;
                        _saldoAtual = 0;
                        _jurosMes = 0;

                        QtdParcela = (int)QuantidadeCurrency.DecimalValue;
                        _valor = PrincipalCurrency.DecimalValue / QtdParcela;
                        total = _valor * QtdParcela;
                        _juros = JurosCurrency.DecimalValue / QtdParcela;
                        totalJuros = _juros * QtdParcela;
                        _multa = MultaCurrency.DecimalValue / QtdParcela;
                        totalMulta = _multa * QtdParcela;
                        _honorarios = HonorariosCurrency.DecimalValue / QtdParcela;
                        totalHonorario = _honorarios * QtdParcela;
                        parcelaInicial = 1;
                    }

                }
                else
                {
                    parcelaInicial = 1;

                    QtdParcela = (int)QuantidadeCurrency.DecimalValue;

                    if (!AplicarUFGCheckBox.Checked)
                    {
                        _valor = PrincipalCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;
                        _juros = JurosCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;
                        _multa = MultaCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;
                        _honorarios = HonorariosCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;

                        if (Math.Round(_valor, 2) < ParcelaMinimaCurrency.DecimalValue)
                        {
                            int parcela = (int)(PrincipalCurrency.DecimalValue / ParcelaMinimaCurrency.DecimalValue);

                            QtdParcela = parcela;
                            QuantidadeCurrency.DecimalValue = QtdParcela;

                            _valor = Math.Round(PrincipalCurrency.DecimalValue / parcela, 2);
                            total = _valor * parcela;

                            _juros = Math.Round(JurosCurrency.DecimalValue / parcela, 2);
                            totalJuros = _juros * parcela;

                            _multa = Math.Round(MultaCurrency.DecimalValue / parcela, 2);
                            totalMulta = _multa * parcela;

                            _honorarios = Math.Round(HonorariosCurrency.DecimalValue / parcela, 2);
                            totalHonorario = _honorarios * QtdParcela;
                        }
                        else
                        {
                            _valor = Math.Round(PrincipalCurrency.DecimalValue / QtdParcela, 2);
                            total = _valor * QtdParcela;

                            _juros = Math.Round(JurosCurrency.DecimalValue / QtdParcela, 2);
                            totalJuros = _juros * QtdParcela;

                            _multa = Math.Round(MultaCurrency.DecimalValue / QtdParcela, 2);
                            totalMulta = _multa * QtdParcela;

                            _honorarios = Math.Round(HonorariosCurrency.DecimalValue / QtdParcela, 2);
                            totalHonorario = _honorarios * QtdParcela;
                        }
                    }
                    else
                    {
                        _valor = PrincipalCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;
                        _juros = JurosCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;
                        _multa = MultaCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;
                        _honorarios = HonorariosCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;
                        _custas = CustasCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;
                        _reducao = ReducaoCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;
                        _correcao = CorrecaoCurrency.DecimalValue / QuantidadeCurrency.DecimalValue;

                        total = _valor * QtdParcela;
                        totalJuros = _juros * QtdParcela;
                        totalMulta = _multa * QtdParcela;
                        totalHonorario = _honorarios * QtdParcela;

                        _parcelaEmUFG = ConsolidadoCurrency.DecimalValue / QtdParcela; // ok!

                        foreach (var item in _listaSelic.Where(w => w.Ano == _data.Year && w.ValorUFG > 0))
                        {
                            _parcelaEmUFG = Math.Round(_parcelaEmUFG / item.ValorUFG,4); // ok!
                            break;
                        }
                        
                    }
                }
                
                CalculaParcelas(QtdParcela, parcelaInicial, _saldoDevedor, _saldoAtual, _jurosMes);

                int qtd = 12;
                if (AdesaoCurrency.DecimalValue == 0)
                {
                    foreach (var item in _listaParcelas.OrderBy(o => o.Parcela))
                    {
                        List<Classes.Endividamento.Parcelamento> listaC = new List<Classes.Endividamento.Parcelamento>();

                        DateTime dataAuxiliar = item.Vencimento2.AddMonths(1);
                        DateTime dataAuxiliar2 = item.Vencimento2.AddMonths(qtd);

                        listaC = _listaParcelas.Where(w => Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) >=
                            Convert.ToInt32(dataAuxiliar.Year.ToString("0000") + dataAuxiliar.Month.ToString("00")) &&
                            Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) <= Convert.ToInt32(dataAuxiliar2.Year.ToString("0000") + dataAuxiliar2.Month.ToString("00"))).ToList();

                        if (AplicarUFGCheckBox.Checked)
                            item.SaldoCurto = listaC.Sum(s => s.ValorPagar);
                        else
                            item.SaldoCurto = listaC.Sum(s => s.ValorConsolidado);

                        if (item.Parcela > 1)
                            item.SaldoCurto = item.SaldoCurto + ((item.SaldoCurto * item.Encargos) / 100);

                        listaC = _listaParcelas.Where(w => Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) > Convert.ToInt32(dataAuxiliar2.Year.ToString("0000") + dataAuxiliar2.Month.ToString("00"))).ToList();

                        if (AplicarUFGCheckBox.Checked)
                            item.SaldoLongo = listaC.Sum(s => s.ValorPagar);
                        else
                            item.SaldoLongo = listaC.Sum(s => s.ValorConsolidado);

                        if (item.Parcela > 1)
                            item.SaldoLongo = item.SaldoLongo + (item.SaldoLongo * item.Encargos / 100);
                    }
                }
                else
                {
                    foreach (var item in _listaParcelas.Where(w => w.Diferenciada)
                                                       .OrderBy(o => o.Vencimento))
                    {
                        item.JurosMes = 0;
                        item.SaldoCurto = 0;
                        item.SaldoLongo = 0;
                    }

                    foreach (var item in _listaParcelas.Where(w => !w.Diferenciada)
                                                       .OrderBy(o => o.Vencimento))
                    {
                        List<Classes.Endividamento.Parcelamento> listaC = new List<Classes.Endividamento.Parcelamento>();

                        DateTime dataAuxiliar = item.Vencimento2.AddMonths(1);
                        DateTime dataAuxiliar2 = item.Vencimento2.AddMonths(qtd);

                        listaC = _listaParcelas.Where(w => !w.Diferenciada &&
                            Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) >=
                            Convert.ToInt32(dataAuxiliar.Year.ToString("0000") + dataAuxiliar.Month.ToString("00")) &&
                            Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) <= Convert.ToInt32(dataAuxiliar2.Year.ToString("0000") + dataAuxiliar2.Month.ToString("00"))).ToList();

                        if (AplicarUFGCheckBox.Checked)
                            item.SaldoCurto = listaC.Sum(s => s.ValorPagar);
                        else
                            item.SaldoCurto = listaC.Sum(s => s.ValorConsolidado);

                        if (item.Parcela > 1)
                            item.SaldoCurto = item.SaldoCurto + (item.SaldoCurto * item.Encargos / 100);

                        listaC = _listaParcelas.Where(w => !w.Diferenciada && 
                                                            Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) > Convert.ToInt32(dataAuxiliar2.Year.ToString("0000") + dataAuxiliar2.Month.ToString("00"))).ToList();

                        if (AplicarUFGCheckBox.Checked)
                            item.SaldoLongo = listaC.Sum(s => s.ValorPagar);
                        else
                            item.SaldoLongo = listaC.Sum(s => s.ValorConsolidado);

                        if (item.Parcela > 1)
                            item.SaldoLongo = item.SaldoLongo + (item.SaldoLongo * item.Encargos / 100);
                    }

                }

                if (AplicarUFGCheckBox.Checked)
                {
                    decimal _totalaPagar = _listaParcelas.Sum(s => s.ValorPagar);

                    foreach (var item in _listaParcelas.OrderBy(o => o.Parcela))
                    {
                        item.SaldoDevedor = Math.Round(_totalaPagar, 2) - Math.Round(item.ValorPagar, 2);
                        _totalaPagar = item.SaldoDevedor;
                    }
                }
            }

            gridGroupingControl1.DataSource = _listaParcelas;
            gravarButton.Enabled = true;

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                if (gridGroupingControl1.TableDescriptor.Columns[i].MappingName == "MultaDiferenciada")
                    gridGroupingControl1.TableDescriptor.Columns[i].HeaderText = "Multa " + Math.Round(MultaDiferenciadaCurrency.DecimalValue,0) + "%";
                if (gridGroupingControl1.TableDescriptor.Columns[i].MappingName == "JurosDiferenciado")
                    gridGroupingControl1.TableDescriptor.Columns[i].HeaderText = "Juros " + Math.Round(JurosDiferenciadoCurrency.DecimalValue, 0) + "%";
            }

            GerarExcelButton.Enabled = true;
            mensagemSistemaLabel.Text = "";
            Refresh();

        }

        private void ContratoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ContratoTextBox.BorderColor = Publicas._bordaSaida;
            
            if (saiuDaModalidade)
            {
                ContratoTextBox.Focus();
                saiuDaModalidade = false;
                return;
            }
            saiuDaModalidade = false;
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
            
            if (string.IsNullOrEmpty(ContratoTextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                Publicas._codigoPesquisa = _fornecedor.CodigoFornecedor;
                Publicas._tipoDespesaReceita = ModalidadeComboBox.Text;

                new Pesquisas.Parcelamento().ShowDialog();

                ContratoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(ContratoTextBox.Text) || ContratoTextBox.Text == "0")
                {
                    ContratoTextBox.Text = string.Empty;
                    ContratoTextBox.Focus();
                    return;
                }
            }

            _contrato = new EndividamentoBO().Consultar(_empresa.IdEmpresa, _fornecedor.CodigoFornecedor, ContratoTextBox.Text.Trim(), ModalidadeComboBox.Text);
            _listaArquivos = new List<Classes.Endividamento.Arquivo>();
            _listaArquivosLog = new List<Classes.Endividamento.Arquivo>();

            _listaParcelas = new List<Classes.Endividamento.Parcelamento>(); 
            listBox1.Items.Clear();

            PrincipalCurrency.DecimalValue = 0;
            JurosCurrency.DecimalValue = 0;
            MultaCurrency.DecimalValue = 0;
            ConsolidadoCurrency.DecimalValue = 0;
            QuantidadeCurrency.DecimalValue = 0;
            DiaCurrency.DecimalValue = 0;
            inicialDateTimePicker.Value = DateTime.Now.Date;
            PercentualJurosCurrency.DecimalValue = 0;
            QtdeParcelasDiferenciadaCurrencyTextBox.DecimalValue = 0;
            AdesaoCurrency.DecimalValue = 0;
            PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue = 0;
            JurosDiferenciadoCurrency.DecimalValue = 0;
            MultaDiferenciadaCurrency.DecimalValue = 0;
            SelicDiferenciadaCurrency.DecimalValue = 0;
            ParcelaMinimaCurrency.DecimalValue = 0;

            editarToolStripMenuItem.Enabled = false;

            if (_contrato.Existe)
            {
                PedidoTextBox.Text = _contrato.Pedido;
                PercentualJurosCurrency.DecimalValue = _contrato.PercentualJuros;
                PrincipalCurrency.DecimalValue = _contrato.Valor;
                JurosCurrency.DecimalValue = _contrato.Juros;
                MultaCurrency.DecimalValue = _contrato.Multa;
                ConsolidadoCurrency.DecimalValue = _contrato.Consolidado;
                QuantidadeCurrency.DecimalValue = _contrato.Quantidade;
                DiaCurrency.DecimalValue = _contrato.Dia;
                inicialDateTimePicker.Value = _contrato.Vencimento;

                AplicarSelicCheckBox.Checked = _contrato.AplicarSelic;
                JurosMultaDiferenciadasCheckBox.Checked = _contrato.AplicarJurosDiferenciado;

                JurosDiferenciadoCurrency.DecimalValue = _contrato.PercentualJurosDiferenciado;
                MultaDiferenciadaCurrency.DecimalValue = _contrato.PercentualMultaDiferenciada;
                SelicDiferenciadaCurrency.DecimalValue = _contrato.PercentualSelicDiferenciada;
                ParcelaMinimaCurrency.DecimalValue = _contrato.ParcelaMinima;

                ParcelasDiferenciadasCheckBox.Checked = _contrato.AplicarParcelasDiferenciado;
                PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue = _contrato.PercentualAVista;
                AdesaoCurrency.DecimalValue = _contrato.ValorAdesao;
                QtdeParcelasDiferenciadaCurrencyTextBox.DecimalValue = _contrato.QtdeParcelasAdesao;

                HonorariosCurrency.DecimalValue = _contrato.Honorarios;
                CorrecaoCurrency.DecimalValue = _contrato.Correcao;
                CustasCurrency.DecimalValue = _contrato.Custas;
                ReducaoCurrency.DecimalValue = _contrato.Reducao;
                AplicarUFGCheckBox.Checked = _contrato.AplicarUFG;
                ParcelasAZeraCurrency.DecimalValue = _contrato.ZerarParcelaApartirDe;
                ZerarParcelasCheckBox.Checked = _contrato.ZerarParcelas;

                _listaParcelas = new EndividamentoBO().ListarParcelamento(_contrato.Id, ConsolidadoCurrency.DecimalValue, _contrato.AplicarSelic);
                _listaParcelasLog = new EndividamentoBO().ListarParcelamento(_contrato.Id, ConsolidadoCurrency.DecimalValue, _contrato.AplicarSelic);

                _listaArquivos = new EndividamentoBO().Arquivos(_contrato.Id);
                _listaArquivosLog = new EndividamentoBO().Arquivos(_contrato.Id);

                foreach (var item in _listaArquivos)
                {
                    listBox1.Items.Add(item.NomeArquivo);
                    CriaPanelsArquivo(item.NomeArquivo);
                }

                
                gravarButton.Enabled = true;

                editarToolStripMenuItem.Enabled = true;
            }

            anexoLabel.Text = listBox1.Items.Count.ToString();
            anexoPanel.Enabled = true;
            excluirButton.Enabled = _contrato.Existe;
            gridGroupingControl1.DataSource = _listaParcelas;
            GerarExcelButton.Enabled = true;

            temAlteracao = _listaParcelas.Where(w => w.Atualizada).Count() != 0;
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

        private void pesquisaFornecedorButton_Click(object sender, EventArgs e)
        {

        }

        private void PercentualJurosCurrency_Validating(object sender, CancelEventArgs e)
        {
            PercentualJurosCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void DiaCurrency_Validating(object sender, CancelEventArgs e)
        {
            DiaCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaParcelas.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma parcela calculada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                PrincipalCurrency.Focus();
                return;
            }

            _contrato.IdEmpresa = _empresa.IdEmpresa;
            _contrato.CodigoFornecedor = _fornecedor.CodigoFornecedor;
            _contrato.NumeroContrato = ContratoTextBox.Text;
            _contrato.Pedido = PedidoTextBox.Text;
            _contrato.Modalidade = ModalidadeComboBox.Text;
            _contrato.Valor = PrincipalCurrency.DecimalValue;
            _contrato.Juros = JurosCurrency.DecimalValue;
            _contrato.Multa = MultaCurrency.DecimalValue;
            _contrato.Consolidado = ConsolidadoCurrency.DecimalValue;
            _contrato.Quantidade = Convert.ToInt32(QuantidadeCurrency.DecimalValue);
            _contrato.Dia = Convert.ToInt32(DiaCurrency.DecimalValue);
            _contrato.Vencimento = inicialDateTimePicker.Value.Date;
            _contrato.PercentualJuros = PercentualJurosCurrency.DecimalValue;
            _contrato.AplicarSelic = AplicarSelicCheckBox.Checked;
            
            _contrato.PercentualJurosDiferenciado = JurosDiferenciadoCurrency.DecimalValue;
            _contrato.PercentualMultaDiferenciada = MultaDiferenciadaCurrency.DecimalValue;
            _contrato.PercentualSelicDiferenciada = SelicDiferenciadaCurrency.DecimalValue;
            _contrato.AplicarJurosDiferenciado = JurosMultaDiferenciadasCheckBox.Checked;
            _contrato.AplicarParcelasDiferenciado = ParcelasDiferenciadasCheckBox.Checked;

            _contrato.ValorAdesao = AdesaoCurrency.DecimalValue;
            _contrato.PercentualAVista = PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue;
            _contrato.QtdeParcelasAdesao = QtdeParcelasDiferenciadaCurrencyTextBox.DecimalValue;

            _contrato.Honorarios = HonorariosCurrency.DecimalValue;
            _contrato.Correcao = CorrecaoCurrency.DecimalValue;
            _contrato.Custas = CustasCurrency.DecimalValue;
            _contrato.Reducao = ReducaoCurrency.DecimalValue;
            _contrato.AplicarUFG = AplicarUFGCheckBox.Checked;
            _contrato.ZerarParcelaApartirDe = (int)ParcelasAZeraCurrency.DecimalValue;
            _contrato.ZerarParcelas = ZerarParcelasCheckBox.Checked;

            _contrato.ParcelaMinima = ParcelaMinimaCurrency.DecimalValue;

            List<Classes.Endividamento.Arquivo> _listaAnexosGravar = new List<Classes.Endividamento.Arquivo>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                Classes.Endividamento.Arquivo _anexos = new Classes.Endividamento.Arquivo();

                try
                {
                    StreamReader oStreamReader = new StreamReader(listBox1.Items[i].ToString());

                    byte[] buffer = new byte[oStreamReader.BaseStream.Length];

                    oStreamReader.BaseStream.Read(buffer, 0, buffer.Length);

                    oStreamReader.Close();
                    oStreamReader.Dispose();

                    _anexos.Imagem = buffer;
                    _anexos.NomeArquivo = Path.GetFileName(listBox1.Items[i].ToString());
                }
                catch
                {
                    foreach (var item in _listaArquivosLog.Where(w => listBox1.Items[i].ToString().Contains(w.NomeArquivo)))
                    {
                        _anexos.Imagem = item.Imagem;
                        _anexos.Id = item.Id;
                        _anexos.NomeArquivo = item.NomeArquivo;
                        _anexos.IdContrato = item.IdContrato;
                        _anexos.Existe = item.Existe;
                    }
                }
                                
                _listaAnexosGravar.Add(_anexos);
            }

            if (!new EndividamentoBO().Gravar(_contrato, _listaParcelas, _listaAnexosGravar, _listaArquivosLog))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou o processo de parcelamento " + ContratoTextBox.Text + " da empresa " + empresaComboBoxAdv.Text +
                " Fornecedor " + _fornecedor.Numero + " " + NomeFornecedorTextBox.Text + " Modalidade " + ModalidadeComboBox.Text + 
                (valorAjustado ? "Recalculou a primeira parcela para o total ficar igual ao informado" : "") + 
                (reprocessarValores ? "Reprocessou os valores de todas as parcelas" : "");

            _log.Tela = "Contabilidade - Parcelamento - Valores";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);

            if (ProximoCheckBox.Checked)
            {
                int posicao = 0;

                if (posicaoProximo + 1 > _listaContratos.Count())
                {
                    new Notificacoes.Mensagem("Chegou ao fim do parcelamentos desta empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }

                foreach (var item in _listaContratos.OrderBy(o => o.NomeFantasia))
                {
                    posicao++;

                    if (posicao == posicaoProximo + 1)
                    {
                        _fornecedor = new FornecedoresGlobusBO().Consultar(item.CodigoFornecedor);
                        FornecedorTextBox.Text = _fornecedor.Numero;
                        NomeFornecedorTextBox.Text = _fornecedor.NomeFantasia;

                        for (int i = 0; i < ModalidadeComboBox.Items.Count; i++)
                        {
                            ModalidadeComboBox.SelectedIndex = i;
                            if (ModalidadeComboBox.Text == item.Modalidade)
                                break;
                        }

                        ContratoTextBox.Text = item.NumeroContrato;
                        ContratoTextBox_Validating(sender, new CancelEventArgs());
                        posicaoProximo++;
                        posicaoTextBox.Text = posicaoProximo + " / " + _listaContratos.Count();
                        break;
                    }
                }

                AnteriorButton.Enabled = posicaoProximo != 1;
                ProximoButton.Enabled = posicaoProximo != _listaContratos.Count();

            }
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new EndividamentoBO().ExcluirParcelamento(_contrato.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu o processo de parcelamento " + ContratoTextBox.Text + " da empresa " + empresaComboBoxAdv.Text +
                " Fornecedor " + _fornecedor.Numero + " " + NomeFornecedorTextBox.Text + " Modalidade " + ModalidadeComboBox.Text;

            _log.Tela = "Contabilidade - Parcelamento - Valores";

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
            listBox1.Items.Clear();
            anexoLabel.Text = "0";
            ContratoTextBox.Text = string.Empty;
            PedidoTextBox.Text = string.Empty;
            PrincipalCurrency.DecimalValue = 0;
            JurosCurrency.DecimalValue = 0;
            MultaCurrency.DecimalValue = 0;
            ConsolidadoCurrency.DecimalValue = 0;
            QuantidadeCurrency.DecimalValue = 0;
            DiaCurrency.DecimalValue = 0;
            inicialDateTimePicker.Value = DateTime.Now.Date;
            PercentualJurosCurrency.DecimalValue = 0;
            QtdeParcelasDiferenciadaCurrencyTextBox.DecimalValue = 0;
            AdesaoCurrency.DecimalValue = 0;
            PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue = 0;
            JurosDiferenciadoCurrency.DecimalValue = 0;
            MultaDiferenciadaCurrency.DecimalValue = 0;
            SelicDiferenciadaCurrency.DecimalValue = 0;
            ParcelaMinimaCurrency.DecimalValue = 0;
            CorrecaoCurrency.DecimalValue = 0;
            HonorariosCurrency.DecimalValue = 0;
            ReducaoCurrency.DecimalValue = 0;
            CustasCurrency.DecimalValue = 0;
            ReprocessarCheckBox.Checked = false;

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Parcelamento>();
            _listaArquivos = new List<Classes.Endividamento.Arquivo>();
            _listaParcelas = new List<Classes.Endividamento.Parcelamento>();

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            GerarExcelButton.Enabled = false;
            ContratoTextBox.Focus();
        }

        private void ContratoTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void ModalidadeComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ContratoTextBox.Focus();
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void clipAnexoPictureBox_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Title = "Selecione o arquivo a ser anexado.";
            anexoImagensPanel.Visible = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] arquivos = openFileDialog1.FileNames;

                if (arquivos.Count() != 0)
                {
                    foreach (var item in arquivos)
                    {
                        if (item.ToString().Length > 100)
                        {
                            new Notificacoes.Mensagem("Nome do arquivo muito grande. Renomeio o arquivo!", Publicas.TipoMensagem.Alerta).ShowDialog();
                            return;
                        }

                        if (!item.ToString().ToLower().Contains(".pdf") && !item.ToString().ToLower().Contains(".txt") &&
                            !item.ToString().ToLower().Contains(".jpg") && !item.ToString().ToLower().Contains(".jpeg") &&
                            !item.ToString().ToLower().Contains(".png"))
                        {
                            new Notificacoes.Mensagem("Extensão do arquivo inválido." + Environment.NewLine +
                                item.ToString() +
                                Environment.NewLine + Environment.NewLine +
                                "Extensões permitidas: pdf, png, jpeg e txt"
                                , Publicas.TipoMensagem.Alerta).ShowDialog();
                            return;
                        }
                        listBox1.Items.Add(item);
                        CriaPanelsArquivo(item);
                    }
                    anexoLabel.Text = listBox1.Items.Count.ToString();

                }
            }
        }

        private void CriaPanelsArquivo(string arquivo)
        {
            string caminho = "";// Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string tipo = "";

            switch (Path.GetExtension(arquivo).ToUpper())
            {
                case ".TXT":
                    tipo = caminho + @"Imagens\txt.png";
                    break;
                case ".RAR":
                    tipo = caminho + @"Imagens\rar.png";
                    break;
                case ".EXE":
                    tipo = caminho + @"Imagens\exe.png";
                    break;
                case ".XLSX":
                    tipo = caminho + @"Imagens\xlsx.png";
                    break;
                case ".PPT":
                    tipo = caminho + @"Imagens\ppt.png";
                    break;
                case ".JPG":
                    tipo = caminho + @"Imagens\jpg.png";
                    break;
                case ".SQL":
                    tipo = caminho + @"Imagens\sql.png";
                    break;
                case ".ZIP":
                    tipo = caminho + @"Imagens\zip.png";
                    break;
                case ".HTML":
                    tipo = caminho + @"Imagens\html.png";
                    break;
                case ".XLS":
                    tipo = caminho + @"Imagens\xls.png";
                    break;
                case ".XML":
                    tipo = caminho + @"Imagens\xml.png";
                    break;
                case ".DOC":
                    tipo = caminho + @"Imagens\doc.png";
                    break;
                case ".PDF":
                    tipo = caminho + @"Imagens\pdf.png";
                    break;
                case ".PNG":
                    tipo = caminho + @"Imagens\png.png";
                    break;
                default:
                    tipo = caminho + @"Imagens\File.png";
                    break;
            }

            if (_qtd == 0)
            {
                arquivoPanel.Location = new Point(arquivoPanel.Left, _topArquivos = 8);
                nomeArquivoLabel.Text = Path.GetFileNameWithoutExtension(arquivo);
                removerLabel.Tag = nomeArquivoLabel.Text;
                imagemArquivoPictureBox.ImageLocation = tipo;
                arquivoPanel.Visible = true;
            }
            else
            {
                Panel panel = new Panel();
                panel.Name = "arquivoPanel" + _qtd.ToString();

                Label labelNomeArquivo = new Label();
                Label labelRemover = new Label();
                PictureBox imagem = new PictureBox();

                panel.Controls.Add(imagem);
                panel.Controls.Add(labelNomeArquivo);
                panel.Controls.Add(labelRemover);

                imagem.Size = imagemArquivoPictureBox.Size;
                imagem.Location = imagemArquivoPictureBox.Location;
                imagem.Image = imagemArquivoPictureBox.Image;
                imagem.SizeMode = PictureBoxSizeMode.StretchImage;
                imagem.ImageLocation = tipo;

                panel.Size = arquivoPanel.Size;
                panel.Location = new Point(arquivoPanel.Left, _topArquivos);

                labelNomeArquivo.ForeColor = nomeArquivoLabel.ForeColor;
                labelNomeArquivo.Font = nomeArquivoLabel.Font;
                labelNomeArquivo.TextAlign = ContentAlignment.MiddleCenter;
                labelNomeArquivo.Text = Path.GetFileNameWithoutExtension(arquivo);
                labelNomeArquivo.AutoSize = false;
                labelNomeArquivo.Site = nomeArquivoLabel.Site;
                labelNomeArquivo.Location = nomeArquivoLabel.Location;

                labelRemover.TextAlign = ContentAlignment.MiddleCenter;
                labelRemover.AutoSize = false;
                labelRemover.Size = removerLabel.Size;
                labelRemover.ForeColor = removerLabel.ForeColor;
                labelRemover.Font = removerLabel.Font;
                labelRemover.Location = removerLabel.Location;
                labelRemover.Text = removerLabel.Text;
                labelRemover.Tag = labelNomeArquivo.Text;

                labelRemover.Click += new System.EventHandler(this.removerLabel_Click);
                labelRemover.MouseLeave += new System.EventHandler(this.removerLabel_MouseLeave);
                labelRemover.MouseHover += new System.EventHandler(this.removerLabel_MouseHover);

                panel.Visible = true;
                anexoImagensPanel.Controls.Add(panel);

                panel.BringToFront();

            }

            _topArquivos = _topArquivos + (arquivoPanel.Height + 5);

            Refresh();
            _qtd++;
        }

        private void removerLabel_Click(object sender, EventArgs e)
        {
            _qtd = 0;
            Control[] controle;
            string nome = "";
            string arquivo = ((Label)sender).Tag.ToString();

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                nome = "arquivoPanel" + (i == 0 ? "" : i.ToString());

                controle = this.Controls.Find(nome, true);

                if (i == 0)
                    controle[0].Visible = false; // o primeiro não é criado em tempo de execução por isso fica invisivel
                else
                {
                    try
                    {
                        controle[0].Dispose();
                    }
                    catch { }
                }
            }

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString().Contains(arquivo))
                {
                    nome = listBox1.Items[i].ToString();
                    break;
                }
            }
            listBox1.Items.Remove(nome);

            _topArquivos = 8;
            anexoImagensPanel.Visible = false;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                CriaPanelsArquivo(listBox1.Items[i].ToString());
            }

            anexoImagensPanel.Visible = listBox1.Items.Count > 0;
            anexoLabel.Text = listBox1.Items.Count.ToString();
        }

        private void removerLabel_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Hand;
        }

        private void removerLabel_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Default;
        }
        
        private void anexoPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void anexoPanel_DragDrop(object sender, DragEventArgs e)
        {
            arquivo = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            anexoImagensPanel.Visible = false;
            for (i = 0; i < arquivo.Length; i++)
            {
                if (arquivo[i].ToString().Length > 100)
                {
                    new Notificacoes.Mensagem("Nome do arquivo muito grande. Renomeio o arquivo!", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
                if (!arquivo[i].ToString().ToLower().Contains(".pdf") && !arquivo[i].ToString().ToLower().Contains(".txt") &&
                            !arquivo[i].ToString().ToLower().Contains(".jpg") && !arquivo[i].ToString().ToLower().Contains(".jpeg") &&
                            !arquivo[i].ToString().ToLower().Contains(".xls") && !arquivo[i].ToString().ToLower().Contains(".rem") &&
                            !arquivo[i].ToString().ToLower().Contains(".ret") && !arquivo[i].ToString().ToLower().Contains(".png") &&
                            !arquivo[i].ToString().ToLower().Contains(".xml"))
                {
                    new Notificacoes.Mensagem("Extensão do arquivo inválido." + Environment.NewLine +
                        arquivo[i].ToString() +
                        Environment.NewLine + Environment.NewLine +
                        "Extensões permitidas: pdf, png, jpeg, txt e xls"
                        , Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
                listBox1.Items.Add(arquivo[i]);
                CriaPanelsArquivo(arquivo[i]);
            }

            anexoLabel.Text = listBox1.Items.Count.ToString();
        }

        private void anexoPanel_MouseHover(object sender, EventArgs e)
        {
            anexoPanel.BackColor = Color.Silver;
            anexoPanel.Cursor = Cursors.Hand;
        }

        private void anexoPanel_MouseLeave(object sender, EventArgs e)
        {
            anexoPanel.BackColor = empresaComboBoxAdv.BackColor;
            anexoPanel.Cursor = Cursors.Default;
        }

        private void verPictureBox_Click(object sender, EventArgs e)
        {
            anexoImagensPanel.Left = 1171;
            anexoImagensPanel.Top = anexoPanel.Top + anexoPanel.Height + 5;
            anexoImagensPanel.Visible = !anexoImagensPanel.Visible;
        }

        private void verPictureBox_MouseHover(object sender, EventArgs e)
        {
            verPictureBox.BackColor = Color.Silver;
            verPictureBox.Cursor = Cursors.Hand;
        }

        private void verPictureBox_MouseLeave(object sender, EventArgs e)
        {
            verPictureBox.BackColor = empresaComboBoxAdv.BackColor;
            verPictureBox.Cursor = Cursors.Default;
        }

        private void clipAnexoPictureBox_MouseHover(object sender, EventArgs e)
        {
            clipAnexoPictureBox.BackColor = Color.Silver;
            clipAnexoPictureBox.Cursor = Cursors.Hand;
        }

        private void clipAnexoPictureBox_MouseLeave(object sender, EventArgs e)
        {
            clipAnexoPictureBox.BackColor = empresaComboBoxAdv.BackColor;
            clipAnexoPictureBox.Cursor = Cursors.Default;
        }

        private void JurosMultaDiferenciadasCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            JurosDifenrenciadoTabPage.TabVisible = JurosMultaDiferenciadasCheckBox.Checked;

            JurosDiferenciadoCurrency.Visible = JurosMultaDiferenciadasCheckBox.Checked;
            JurosDiferenciadoLabel.Visible = JurosMultaDiferenciadasCheckBox.Checked;
            MultaDiferenciadaCurrency.Visible = JurosMultaDiferenciadasCheckBox.Checked;
            MultaDiferenciadaLabel.Visible = JurosMultaDiferenciadasCheckBox.Checked;
            SelicDiferenciadaCurrency.Visible = JurosMultaDiferenciadasCheckBox.Checked;
            SelicDiferenciadaLabel.Visible = JurosMultaDiferenciadasCheckBox.Checked;

            if (JurosMultaDiferenciadasCheckBox.Checked)
            {
                try
                {
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("ValorPrincipalAtualizado");
                }
                catch { }
            
                try
                {
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("JurosDiferenciado");
                }
                catch { }

                try
                {
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("MultaDiferenciada");
                }
                catch { }

                try
                {
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Add("PercentualSelicDiferenciada");
                }
                catch { }

                gridGroupingControl1.TableDescriptor.VisibleColumns.Move(gridGroupingControl1.TableDescriptor.GetColumnDescriptor("ValorPrincipalAtualizado").GetRelativeColumnIndex(), 8);
                gridGroupingControl1.TableDescriptor.VisibleColumns.Move(gridGroupingControl1.TableDescriptor.GetColumnDescriptor("MultaDiferenciada").GetRelativeColumnIndex(), 9);
                gridGroupingControl1.TableDescriptor.VisibleColumns.Move(gridGroupingControl1.TableDescriptor.GetColumnDescriptor("PercentualSelicDiferenciada").GetRelativeColumnIndex(), 10);
                gridGroupingControl1.TableDescriptor.VisibleColumns.Move(gridGroupingControl1.TableDescriptor.GetColumnDescriptor("Juros").GetRelativeColumnIndex(), 11);
                gridGroupingControl1.TableDescriptor.VisibleColumns.Move(gridGroupingControl1.TableDescriptor.GetColumnDescriptor("JurosDiferenciado").GetRelativeColumnIndex(), 12);
                gridGroupingControl1.TableDescriptor.VisibleColumns.Move(gridGroupingControl1.TableDescriptor.GetColumnDescriptor("ValorPagar").GetRelativeColumnIndex(), 13);
            }
            else
            {
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("ValorPrincipalAtualizado");
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("JurosDiferenciado");
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("MultaDiferenciada");
                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("PercentualSelicDiferenciada");
            }

        }

        private void PrincipalCurrency_DecimalValueChanged(object sender, EventArgs e)
        {
            ConsolidadoCurrency.DecimalValue = (PrincipalCurrency.DecimalValue + JurosCurrency.DecimalValue + MultaCurrency.DecimalValue 
                + HonorariosCurrency.DecimalValue 
                + CorrecaoCurrency.DecimalValue + CustasCurrency.DecimalValue) - ReducaoCurrency.DecimalValue;
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            {
                Record dr;
                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                if (rec != null)
                {
                    dr = rec.GetRecord() as Record;
                    if (dr != null && (bool)dr["Atualizada"])
                        e.Style.TextColor = Color.DarkOrange;
                    if (dr != null && (bool)dr["Diferenciada"])
                    {
                        if (!Publicas._TemaBlack)
                            e.Style.BackColor = Color.Gainsboro;
                        else
                            e.Style.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
                    }
                }
            }
            catch { }
        }

        private void ParcelasDiferenciadasCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            AdesaoTabPage.TabVisible = ParcelasDiferenciadasCheckBox.Checked;
            PercentualPrimeiraParcelaCurrencyTextBox.Visible = ParcelasDiferenciadasCheckBox.Checked;
            label11.Visible = ParcelasDiferenciadasCheckBox.Checked;
            QtdeParcelasDiferenciadaCurrencyTextBox.Visible = ParcelasDiferenciadasCheckBox.Checked;
            label12.Visible = ParcelasDiferenciadasCheckBox.Checked;
            AdesaoCurrency.Visible = ParcelasDiferenciadasCheckBox.Checked;
            label13.Visible = ParcelasDiferenciadasCheckBox.Checked;
            ZerarParcelasCheckBox.Visible = ParcelasDiferenciadasCheckBox.Checked;
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            //PesquisaContratoButton
            Publicas._idEmpresa = _empresa.IdEmpresa;
            Publicas._codigoPesquisa = _fornecedor.CodigoFornecedor;
            Publicas._tipoDespesaReceita = ModalidadeComboBox.Text;

            new Pesquisas.Parcelamento().ShowDialog();

            ContratoTextBox.Text = Publicas._codigoRetornoPesquisa;

            if (string.IsNullOrEmpty(ContratoTextBox.Text) || ContratoTextBox.Text == "0")
            {
                ContratoTextBox.Text = string.Empty;
                ContratoTextBox.Focus();
                return;
            }

            ContratoTextBox_Validating(sender, new CancelEventArgs());
        }

        private void ProximoCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            ProximoButton.Enabled = ProximoCheckBox.Checked;

            if (ProximoCheckBox.Checked)
            {
                _listaContratos = new EndividamentoBO().Listar(_empresa.IdEmpresa);

                posicaoProximo = 1;
                foreach (var item in _listaContratos.OrderBy(o => o.NomeFantasia))
                {
                    _fornecedor = new FornecedoresGlobusBO().Consultar(item.CodigoFornecedor);
                    FornecedorTextBox.Text = _fornecedor.Numero;
                    NomeFornecedorTextBox.Text = _fornecedor.NomeFantasia;

                    for (int i = 0; i < ModalidadeComboBox.Items.Count; i++)
                    {
                        ModalidadeComboBox.SelectedIndex = i;
                        if (ModalidadeComboBox.Text == item.Modalidade)
                            break;
                    }

                    ContratoTextBox.Text = item.NumeroContrato;
                    ContratoTextBox_Validating(sender, new CancelEventArgs());
                    posicaoTextBox.Text = posicaoProximo + " / " + _listaContratos.Count();
                    break;
                }

                AnteriorButton.Enabled = posicaoProximo != 1;
                ProximoButton.Enabled = posicaoProximo != _listaContratos.Count();
            }
        }

        private void ProximoButton_Click(object sender, EventArgs e)
        {
            if (ProximoCheckBox.Checked)
            {
                int posicao = 0;

                if (posicaoProximo+1 > _listaContratos.Count())
                {
                    new Notificacoes.Mensagem("Chegou ao fim do parcelamentos desta empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    limparButton_Click(sender, e);
                    return;
                }

                foreach (var item in _listaContratos.OrderBy(o => o.NomeFantasia))
                {
                    posicao++;

                    if (posicao == posicaoProximo + 1)
                    {
                        _fornecedor = new FornecedoresGlobusBO().Consultar(item.CodigoFornecedor);
                        FornecedorTextBox.Text = _fornecedor.Numero;
                        NomeFornecedorTextBox.Text = _fornecedor.NomeFantasia;

                        for (int i = 0; i < ModalidadeComboBox.Items.Count; i++)
                        {
                            ModalidadeComboBox.SelectedIndex = i;
                            if (ModalidadeComboBox.Text == item.Modalidade)
                                break;
                        }

                        ContratoTextBox.Text = item.NumeroContrato;
                        ContratoTextBox_Validating(sender, new CancelEventArgs());
                        PedidoTextBox.Focus();
                        posicaoProximo++;
                        posicaoTextBox.Text = posicaoProximo + " / " + _listaContratos.Count();
                        break;
                    }
                }

                AnteriorButton.Enabled = posicaoProximo != 1;
                ProximoButton.Enabled = posicaoProximo != _listaContratos.Count();

            }
        }

        private void AnteriorButton_Click(object sender, EventArgs e)
        {
            if (ProximoCheckBox.Checked)
            {
                int posicao = 0;

                foreach (var item in _listaContratos.OrderBy(o => o.NomeFantasia))
                {
                    posicao++;

                    if (posicao == posicaoProximo -1)
                    {
                        _fornecedor = new FornecedoresGlobusBO().Consultar(item.CodigoFornecedor);
                        FornecedorTextBox.Text = _fornecedor.Numero;
                        NomeFornecedorTextBox.Text = _fornecedor.NomeFantasia;

                        for (int i = 0; i < ModalidadeComboBox.Items.Count; i++)
                        {
                            ModalidadeComboBox.SelectedIndex = i;
                            if (ModalidadeComboBox.Text == item.Modalidade)
                                break;
                        }

                        ContratoTextBox.Text = item.NumeroContrato;
                        ContratoTextBox_Validating(sender, new CancelEventArgs());
                        PedidoTextBox.Focus();
                        posicaoProximo--;
                        posicaoTextBox.Text = posicaoProximo + " / " + _listaContratos.Count();
                        break;
                    }
                }

                AnteriorButton.Enabled = posicaoProximo != 1;
                ProximoButton.Enabled = posicaoProximo != _listaContratos.Count();
            }
        }

        private void JurosDiferenciadoCurrency_Validating(object sender, CancelEventArgs e)
        {
            JurosDiferenciadoCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void MultaDiferenciadaCurrency_Validating(object sender, CancelEventArgs e)
        {
            MultaDiferenciadaCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void SelicDiferenciadaCurrency_Validating(object sender, CancelEventArgs e)
        {
            SelicDiferenciadaCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void ParcelaMinimaCurrency_Validating(object sender, CancelEventArgs e)
        {
            ParcelaMinimaCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void PercentualPrimeiraParcelaCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            PercentualPrimeiraParcelaCurrencyTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void QtdeParcelasDiferenciadaCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            QtdeParcelasDiferenciadaCurrencyTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void PercentualPrimeiraParcelaCurrencyTextBox_DecimalValueChanged(object sender, EventArgs e)
        {
            AdesaoCurrency.Enabled = PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue == 0;
            QtdeParcelasDiferenciadaCurrencyTextBox.Enabled = PercentualPrimeiraParcelaCurrencyTextBox.DecimalValue == 0;
        }

        private void AdesaoCurrency_Validating(object sender, CancelEventArgs e)
        {
            AdesaoCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void ZerarParcelasCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            ParcelasAZeraCurrency.Visible = ZerarParcelasCheckBox.Checked;
        }

        private void ParcelasAZeraCurrency_Validating(object sender, CancelEventArgs e)
        {
            ParcelasAZeraCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void HonorariosCurrency_Validating(object sender, CancelEventArgs e)
        {
            HonorariosCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void CorrecaoCurrency_Validating(object sender, CancelEventArgs e)
        {
            CorrecaoCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void ReducaoCurrency_Validating(object sender, CancelEventArgs e)
        {
            ReducaoCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void CustasCurrency_Validating(object sender, CancelEventArgs e)
        {
            CustasCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void AplicarUFGCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            label16.Enabled = AplicarUFGCheckBox.Checked;
            label18.Enabled = AplicarUFGCheckBox.Checked;
            label20.Enabled = AplicarUFGCheckBox.Checked;

            CorrecaoCurrency.Enabled = AplicarUFGCheckBox.Checked;
            CustasCurrency.Enabled = AplicarUFGCheckBox.Checked;
            ReducaoCurrency.Enabled = AplicarUFGCheckBox.Checked;
            if (AplicarUFGCheckBox.Checked)
                AplicarSelicCheckBox.Checked = false;
        }

        private void AplicarUFGCheckBox_KeyDown(object sender, KeyEventArgs e)
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

        private void ZerarParcelasCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (ParcelasAZeraCurrency.Visible)
                    SelectNextControl(ActiveControl, true, true, true, true);
                else
                {
                    if (JurosDifenrenciadoTabPage.TabVisible)
                    {
                        tabControlAdv1.SelectedTab = JurosDifenrenciadoTabPage;
                        JurosDiferenciadoCurrency.Focus();
                    }
                    else
                    {
                        tabControlAdv1.SelectedTab = PrincipalTabPage;
                        PrincipalCurrency.Focus();
                    }
                }
            }
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void ParcelasAZeraCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (JurosDifenrenciadoTabPage.TabVisible)
                {
                    tabControlAdv1.SelectedTab = JurosDifenrenciadoTabPage;
                    JurosDiferenciadoCurrency.Focus();
                }
                else
                {
                    tabControlAdv1.SelectedTab = PrincipalTabPage;
                    PrincipalCurrency.Focus();
                }
            }
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void SelicDiferenciadaCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                tabControlAdv1.SelectedTab = PrincipalTabPage;
                PrincipalCurrency.Focus();
            }
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void PrincipalCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (JurosDifenrenciadoTabPage.TabVisible)
                {
                    tabControlAdv1.SelectedTab = JurosDifenrenciadoTabPage;
                    JurosDiferenciadoCurrency.Focus();
                }
                else
                {
                    if (AdesaoTabPage.TabVisible)
                    {
                        tabControlAdv1.SelectedTab = AdesaoTabPage;
                        PercentualPrimeiraParcelaCurrencyTextBox.Focus();
                    }
                    else
                        AplicarUFGCheckBox.Focus();
                }
            }
        }

        private void JurosDiferenciadoCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (AdesaoTabPage.TabVisible)
                {
                    tabControlAdv1.SelectedTab = AdesaoTabPage;
                    PercentualPrimeiraParcelaCurrencyTextBox.Focus();
                }
                else
                    AplicarUFGCheckBox.Focus();
            }
        }

        private void PercentualPrimeiraParcelaCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AplicarUFGCheckBox.Focus();
            }
        }

        private void GerarExcelButton_Click(object sender, EventArgs e)
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

            string nomeArquivo = "ContratoDeParcelamento_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               ;

            xlApp = new Excel.Application();
            int item = 1;

            try
            {

                xlApp.DisplayAlerts = false;

                if (_listaContratos == null || _listaContratos.Count == 0)
                {
                    _listaContratos = new List<Classes.Endividamento.Contrato>();
                    _listaContratos.Add(_contrato);
                }

                xlWorkBook = xlApp.Workbooks.Add(misValue);

                foreach (var itemR in _listaContratos.OrderBy(o => o.NomeFantasia))
                {
                    _fornecedor = new FornecedoresGlobusBO().Consultar(itemR.CodigoFornecedor);
                    FornecedorTextBox.Text = _fornecedor.Numero;
                    NomeFornecedorTextBox.Text = _fornecedor.NomeFantasia;

                    for (int i = 0; i < ModalidadeComboBox.Items.Count; i++)
                    {
                        ModalidadeComboBox.SelectedIndex = i;
                        if (ModalidadeComboBox.Text == itemR.Modalidade)
                            break;
                    }

                    ContratoTextBox.Text = itemR.NumeroContrato;
                    ContratoTextBox_Validating(sender, new CancelEventArgs());
                    posicaoTextBox.Text = item + " / " + _listaContratos.Count();

                    try
                    {
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(item);
                    }
                    catch
                    {
                        int qtde = xlWorkBook.Worksheets.Count;
                        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Add(Type.Missing, xlWorkBook.Worksheets[qtde], Type.Missing, Type.Missing);
                    }

                    xlWorkSheet.Name = (itemR.NumeroContrato.Length > 25 ? itemR.NumeroContrato.Substring(0, 25).Replace("/", "_").Replace("-", "_") : itemR.NumeroContrato.Replace("/", "_").Replace("-", "_"));

                    int linha = 1;
                    int col = 2;

                    #region Cabeçalho
                    xlWorkSheet.Cells[linha, col] = "Empresa: ";
                    col++;
                    xlWorkSheet.Cells[linha, col] = empresaComboBoxAdv.Text;
                    col++;
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Fornecedor: ";
                    col++;
                    xlWorkSheet.Cells[linha, col] = FornecedorTextBox.Text + "-" + NomeFornecedorTextBox.Text;

                    col = 2;
                    linha++;
                    xlWorkSheet.Cells[linha, col] = "Modalidade: ";
                    col++;
                    xlWorkSheet.Cells[linha, col] = ModalidadeComboBox.Text;
                    col++;
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Contrato: ";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "' " + ContratoTextBox.Text + " '";
                    linha++;
                    linha++;

                    col = 2;

                    if (AplicarUFGCheckBox.Checked)
                    {
                        xlWorkSheet.Cells[linha, col] = "Principal ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Correção ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Multa ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Juros ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Honorários ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Total ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Redução ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Custas ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Consolidado ";
                        col++;

                        linha++;
                        col = 2;

                        xlWorkSheet.Cells[linha, col] = PrincipalCurrency.DecimalValue;
                        col++;
                        xlWorkSheet.Cells[linha, col] = CorrecaoCurrency.DecimalValue;
                        col++;
                        xlWorkSheet.Cells[linha, col] = MultaCurrency.DecimalValue;
                        col++;
                        xlWorkSheet.Cells[linha, col] = JurosCurrency.DecimalValue;
                        col++;
                        xlWorkSheet.Cells[linha, col] = HonorariosCurrency.DecimalValue;
                        col++;
                        xlWorkSheet.Cells[linha, col] = (PrincipalCurrency.DecimalValue + CorrecaoCurrency.DecimalValue +
                                                       MultaCurrency.DecimalValue + JurosCurrency.DecimalValue + HonorariosCurrency.DecimalValue);
                        col++;
                        xlWorkSheet.Cells[linha, col] = ReducaoCurrency.DecimalValue;
                        col++;
                        xlWorkSheet.Cells[linha, col] = CustasCurrency.DecimalValue;
                        col++;
                        xlWorkSheet.Cells[linha, col] = ConsolidadoCurrency.DecimalValue;
                        col++;
                    }
                    else
                    {
                        xlWorkSheet.Cells[linha, col] = "Principal ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = PrincipalCurrency.DecimalValue;

                        col++;
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Vencimento";
                        col++;
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Pedido";


                        linha++;
                        col = 2;
                        xlWorkSheet.Cells[linha, col] = "Juros ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = JurosCurrency.DecimalValue;
                        col++;
                        col++;
                        xlWorkSheet.Cells[linha, col] = inicialDateTimePicker.Value.ToShortDateString();
                        col++;
                        col++;
                        xlWorkSheet.Cells[linha, col] = PedidoTextBox.Text;

                        linha++;
                        col = 2;
                        xlWorkSheet.Cells[linha, col] = "Multa ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = MultaCurrency.DecimalValue;

                        if (HonorariosCurrency.DecimalValue != 0)
                        {
                            linha++;
                            col = 2;
                            xlWorkSheet.Cells[linha, col] = "Honorários ";
                            col++;
                            xlWorkSheet.Cells[linha, col] = HonorariosCurrency.DecimalValue;
                        }

                        linha++;
                        col = 2;
                        xlWorkSheet.Cells[linha, col] = "Consolidado ";
                        col++;
                        xlWorkSheet.Cells[linha, col] = ConsolidadoCurrency.DecimalValue;
                    }
                    #endregion

                    #region titulo colunas

                    linha++;
                    linha++;

                    col = 1;
                    xlWorkSheet.Cells[linha, col] = "Parcela";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Vencimento";
                    col++;

                    if (AplicarUFGCheckBox.Checked)
                    {
                        xlWorkSheet.Cells[linha, col] = "Parcela Em UFG";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Valor do UFG";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Valor a pagar";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Principal";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Juros";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Saldo Contábil";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Curto Prazo";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Longo Prazo";
                        col++;
                    }
                    else
                    {
                        xlWorkSheet.Cells[linha, col] = "Principal";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Juros/Multa";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Consolidado";
                        col++;

                        if (JurosMultaDiferenciadasCheckBox.Checked)
                        {
                            xlWorkSheet.Cells[linha, col] = "% Juros";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "Principal Atualizado";
                            col++;

                            xlWorkSheet.Cells[linha, col] = "% Juros";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "% Multa";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "Selic Acumulada";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "Juros";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "Juros Diferenciado";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "Valor a Pagar";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "% Selic";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "Encargos";
                            col++;

                        }
                        else
                        {
                            xlWorkSheet.Cells[linha, col] = "% Juros";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "% Selic";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "Encargos";
                            col++;

                            xlWorkSheet.Cells[linha, col] = "Juros";
                            col++;
                            xlWorkSheet.Cells[linha, col] = "Valor Pagar";
                            col++;
                        }

                        xlWorkSheet.Cells[linha, col] = "Saldo Devedor";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Saldo Devedor Atualizado";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Curto Prazo";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Longo Prazo";
                        col++;
                        xlWorkSheet.Cells[linha, col] = "Juros do Mês";
                        col++;

                    }

                    #endregion

                    foreach (var itemC in _listaParcelas.OrderBy(o => o.Parcela))
                    {
                        linha++;

                        col = 1;
                        xlWorkSheet.Cells[linha, col] = itemC.Parcela.ToString();
                        col++;
                        xlWorkSheet.Cells[linha, col] = itemC.Vencimento.ToShortDateString() + " '";
                        col++;

                        if (AplicarUFGCheckBox.Checked)
                        {
                            xlWorkSheet.Cells[linha, col] = itemC.ParcelaEmUFG;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.UFG;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.ValorPagar;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.ValorParcelado;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.Juros;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.SaldoDevedor;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.SaldoCurto;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.SaldoLongo;
                            col++;
                        }
                        else
                        {
                            xlWorkSheet.Cells[linha, col] = itemC.ValorParcelado;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.JurosEMulta;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.ValorConsolidado;
                            col++;

                            if (JurosMultaDiferenciadasCheckBox.Checked)
                            {
                                xlWorkSheet.Cells[linha, col] = itemC.PercentualJuros;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.ValorPrincipalAtualizado;
                                col++;

                                xlWorkSheet.Cells[linha, col] = itemC.MultaDiferenciada;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.PercentualSelicDiferenciada;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.Juros;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.JurosDiferenciado;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.ValorPagar;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.Selic;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.Encargos;
                                col++;

                            }
                            else
                            {
                                xlWorkSheet.Cells[linha, col] = itemC.PercentualJuros;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.Selic;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.Encargos;
                                col++;

                                xlWorkSheet.Cells[linha, col] = itemC.Juros;
                                col++;
                                xlWorkSheet.Cells[linha, col] = itemC.ValorPagar;
                                col++;
                            }

                            xlWorkSheet.Cells[linha, col] = itemC.SaldoDevedor;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.SaldoDevedorAtual;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.SaldoCurto;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.SaldoLongo;
                            col++;
                            xlWorkSheet.Cells[linha, col] = itemC.JurosMes;
                        }

                    }

                    xlWorkSheet.Columns.AutoFit();
                    item++;
                }

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

        private void ReprocessarCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            reprocessarValores = ReprocessarCheckBox.Checked;

            if (reprocessarValores)
            {
                inicialDateTimePicker_Validating(sender, new CancelEventArgs());
                reprocessarValores = false;
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contabilidade.EditarProcessoParcelamento _tela = new Contabilidade.EditarProcessoParcelamento();

            Publicas._novoContratoParcelamento = "";

            _tela.empresaComboBoxAdv.Text = this.empresaComboBoxAdv.Text;
            _tela.FornecedorTextBox.Text = this.FornecedorTextBox.Text;
            _tela.NomeFornecedorTextBox.Text = this.NomeFornecedorTextBox.Text;
            _tela.ModalidadeComboBox.Text = this.ModalidadeComboBox.Text;
            _tela.ContratoTextBox.Text = this.ContratoTextBox.Text;
            _tela._editarContrato = this._contrato;
            _tela.ShowDialog();

            if (Publicas._novoContratoParcelamento != "")
            {
                ContratoTextBox.Text = Publicas._novoContratoParcelamento;
                _contrato.NumeroContrato = ContratoTextBox.Text;
            }
        }

        private void ReducaoApenas1ParcelaCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (AdesaoTabPage.TabVisible)
                {
                    tabControlAdv1.SelectedTab = AdesaoTabPage;
                    PercentualPrimeiraParcelaCurrencyTextBox.Focus();
                }
                else
                {
                    if (JurosDifenrenciadoTabPage.TabVisible)
                    {
                        tabControlAdv1.SelectedTab = JurosDifenrenciadoTabPage;
                        JurosDiferenciadoCurrency.Focus();
                    }
                    else
                    {
                        tabControlAdv1.SelectedTab = PrincipalTabPage;
                        PrincipalCurrency.Focus();
                    }
                }
            }
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }
    }
}
