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
    public partial class ConciliacaoFornecedores : Form
    {
        public ConciliacaoFornecedores()
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

                    EmissaoInicialDateTimePicker.Style = VisualStyle.Office2016Black;
                    //finalDateTimePicker.Style = VisualStyle.Office2016Black;
                }
            }

            EmissaoInicialDateTimePicker.BorderColor = Publicas._bordaSaida;

            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        List<Classes.ConciliacaoContabil.Fornecedores.Resumo> _listaResumo;
        List<Classes.ConciliacaoContabil.Fornecedores.Detalhe> _listaDetalhe;
        List<Classes.ConciliacaoContabil.Fornecedores.FornAssociados> _listaFornecedores;

        Classes.Empresa _empresa;
        Classes.RateioBeneficios.PlanoContabil _plano;

        DateTime _dataInicio;
        DateTime _data;
        int referencia;
        int referenciaAnterior;
        int referenciaInicial;
        string tipoDocto = "";
        string[] tipo;

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

        private void ConciliacaoFornecedores_Shown(object sender, EventArgs e)
        {
            this.Top = 60;
            #region coloca os botões centralizados
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { gravarButton, limparButton, GerarExcelButton };
            _botoes = Publicas.CentralizarBotoes(_botoes, this.Width, limparButton.Left - (gravarButton.Left + gravarButton.Width));

            for (int i = 0; i < _botoes.Count(); i++)
            {
                if (i == 0)
                    gravarButton.Left = _botoes[i].Left;
                if (i == 1)
                    limparButton.Left = _botoes[i].Left;
                if (i == 2)
                    GerarExcelButton.Left = _botoes[i].Left;
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

            #region grid 1
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
                gridGroupingControl1.TableDescriptor.Columns[i].Appearance.FilterBarCell.BackColor = Publicas._fundo;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.DisplayText;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;


            GridSummaryRowDescriptor _somagrid1;

            GridSummaryColumnDescriptor summaryColumnDescriptorG1 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG1.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG1.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG1.DataMember = "ValorCPG";
            summaryColumnDescriptorG1.Format = "{Sum}";
            summaryColumnDescriptorG1.Name = "ValorCPG";
            summaryColumnDescriptorG1.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG1_1 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG1_1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG1_1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG1_1.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG1_1.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG1_1.DataMember = "ValorCTB";
            summaryColumnDescriptorG1_1.Format = "{Sum}";
            summaryColumnDescriptorG1_1.Name = "ValorCTB";
            summaryColumnDescriptorG1_1.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG1_2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG1_2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG1_2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG1_2.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG1_2.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG1_2.DataMember = "Diferenca";
            summaryColumnDescriptorG1_2.Format = "{Sum}";
            summaryColumnDescriptorG1_2.Name = "Diferenca";
            summaryColumnDescriptorG1_2.SummaryType = SummaryType.DoubleAggregate;

            _somagrid1 = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptorG1, summaryColumnDescriptorG1_1, summaryColumnDescriptorG1_2 });

            _somagrid1.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _somagrid1.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _somagrid1.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _somagrid1.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _somagrid1.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            try
            {
                gridGroupingControl1.TableDescriptor.SummaryRows.Add(_somagrid1);
            }
            catch { }

            gridGroupingControl1.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl1.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl1.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl1.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl1.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl1.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl1.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";

            #endregion

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

            GridSummaryRowDescriptor _soma;

            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor.DataMember = "ValorCPG";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "ValorCPG";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;
            
            GridSummaryColumnDescriptor summaryColumnDescriptor2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor2.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor2.DataMember = "ValorCTB";
            summaryColumnDescriptor2.Format = "{Sum}";
            summaryColumnDescriptor2.Name = "ValorCTB";
            summaryColumnDescriptor2.SummaryType = SummaryType.DoubleAggregate;            

            _soma = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor2 });

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
            this.gridGroupingControl2.Table.DefaultRecordRowHeight = 35;
            #endregion
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
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
            
            try
            {
                string Arquivo = "";

                if (Environment.MachineName.ToUpper().Contains("CORPTS") || Environment.MachineName.ToUpper().Contains("CORPRDP"))
                    Arquivo = "ConciliacaoFornecedor_" +_empresa.CodigoEmpresaGlobus.Replace("/", "") + "_" + Environment.UserName + ".ini";
                else
                    Arquivo = "ConciliacaoFornecedor_" +  _empresa.CodigoEmpresaGlobus.Replace("/", "") + ".ini";

                string linha = "";

                StreamReader writer = new StreamReader(Publicas._caminhoPortal + Arquivo);
                linha = writer.ReadToEnd();

                int posIniId = 0;
                int posFimId = 0;                

                posIniId = linha.IndexOf("Tipo Documento:") + 16;
                posFimId = linha.IndexOf("Classificador Inicial:");
                TipoDoctoTextBox.Text = linha.Substring(posIniId, posFimId - posIniId).Trim();

                posIniId = linha.IndexOf("Classificador Inicial:") + 23;
                posFimId = linha.IndexOf("Classificador Final:");
                ClassInicialTextBox.Text = linha.Substring(posIniId, posFimId - posIniId).Trim();

                posIniId = linha.IndexOf("Classificador Final:") + 21;
                posFimId = linha.IndexOf("Plano:");
                ClassFinalTextBox.Text = linha.Substring(posIniId, posFimId - posIniId).Trim();

                posIniId = linha.IndexOf("Plano:") + 6;
                posFimId = linha.IndexOf("Consolidar:");
                PlanoTextBox.Text = linha.Substring(posIniId, posFimId - posIniId).Trim();

                posIniId = linha.IndexOf("Consolidar:") + 12;
                posFimId = linha.IndexOf("Docto Substituidos:");
                consolidarCheckBox.Checked = (linha.Substring(posIniId, posFimId - posIniId).Trim() == "S");

                posIniId = linha.IndexOf("Docto Substituidos:") + 20;
                posFimId = linha.Length;
                DoctoSubstituidosCheckBox.Checked = (linha.Substring(posIniId, posFimId - posIniId).Trim() == "S");

                writer.Close();

            }
            catch
            {
                
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

            if (!_plano.Existe || _plano == null)
            {
                new Notificacoes.Mensagem("Informe o plano.", Publicas.TipoMensagem.Alerta).ShowDialog();
                PlanoTextBox.Focus();
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
            referenciaInicial = Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4) + "01");

            if (referencia != referenciaInicial)
                referenciaAnterior = Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2)) - 1;
            else
                referenciaAnterior = referencia;

            mensagemSistemaLabel.Text = "Pesquisando, aguarde...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            #region GravaIni
            string texto = "Tipo Documento: " + TipoDoctoTextBox.Text + Environment.NewLine
                + "Classificador Inicial: " + ClassInicialTextBox.Text + Environment.NewLine
                + "Classificador Final: " + ClassFinalTextBox.Text + Environment.NewLine
                + "Plano: " + PlanoTextBox.Text + Environment.NewLine
                + "Consolidar: " + (consolidarCheckBox.Checked ? "S" : "N") + Environment.NewLine
                + "Docto Substituidos: " + (DoctoSubstituidosCheckBox.Checked ? "S" : "N")
                ;

            string Arquivo = "";
            if (Environment.MachineName.ToUpper().Contains("CORPTS") || Environment.MachineName.ToUpper().Contains("CORPRDP"))
                Arquivo = "ConciliacaoFornecedor_" + _empresa.CodigoEmpresaGlobus.Replace("/", "") + "_" + Environment.UserName + ".ini";
            else
                Arquivo = "ConciliacaoFornecedor_" + _empresa.CodigoEmpresaGlobus.Replace("/", "") + ".ini";

            StreamWriter writer = new StreamWriter(Publicas._caminhoPortal + Arquivo);
            writer.WriteLine(texto);

            writer.Close();
            #endregion

            if (!string.IsNullOrEmpty(TipoDoctoTextBox.Text))
            {
                tipo = TipoDoctoTextBox.Text.Split(',');
                foreach (var item in tipo)
                {
                    tipoDocto = tipoDocto + "'" + item.Trim() + "', ";
                }

                tipoDocto = tipoDocto.Substring(0, tipoDocto.Length - 2);
            }

            _listaResumo = new ConciliacaoContabilBO().Listar(_empresa.CodigoEmpresaGlobus, _plano.NumeroPlano, referencia.ToString(), referenciaInicial.ToString()
                                                             , tipoDocto, ClassInicialTextBox.Text, ClassFinalTextBox.Text
                                                             , _data, EmissaoInicialDateTimePicker.Value.Date
                                                             , consolidarCheckBox.Checked, ConferidosCheckBox.Checked, _empresa.IdEmpresa
                                                             , DoctoSubstituidosCheckBox.Checked);

            _listaDetalhe = new ConciliacaoContabilBO().ListarDetalhes(_empresa.CodigoEmpresaGlobus, _plano.NumeroPlano 
                                                             , tipoDocto, ClassInicialTextBox.Text, ClassFinalTextBox.Text
                                                             , _data, EmissaoInicialDateTimePicker.Value.Date
                                                             , consolidarCheckBox.Checked, ConferidosCheckBox.Checked, _empresa.IdEmpresa
                                                             , DoctoSubstituidosCheckBox.Checked) ;

            _listaFornecedores = new ConciliacaoContabilBO().ListarFornecedores(_plano.NumeroPlano);

            gridGroupingControl1.DataSource = _listaResumo;

            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();
            gridGroupingControl1.Focus();

            gravarButton.Enabled = _listaResumo.Count() > 0;
            GerarExcelButton.Enabled = _listaResumo.Count() > 0;

            

        }

        private void PlanoTextBox_Enter(object sender, EventArgs e)
        {
            PlanoTextBox.BorderColor = Publicas._bordaEntrada;
            PesquisaPlanoButton.Enabled = string.IsNullOrEmpty(PlanoTextBox.Text.Trim());
        }

        private void PlanoTextBox_Validating(object sender, CancelEventArgs e)
        {
            PlanoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                gridGroupingControl2.Focus();
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (PlanoTextBox.Text.Trim() == "")
            {
                new Pesquisas.PlanoContabil().ShowDialog();

                PlanoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (PlanoTextBox.Text.Trim() == "" || PlanoTextBox.Text == "0")
                {
                    PlanoTextBox.Text = string.Empty;
                    PlanoTextBox.Focus();
                    return;
                }
            }

            _plano = new RateioBeneficioBO().Consultar(Convert.ToInt32(PlanoTextBox.Text));

            if (!_plano.Existe)
            {
                new Notificacoes.Mensagem("Plano contábil não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                PlanoTextBox.Focus();
                return;
            }
        }

        private void EmissaoInicialDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (_listaDetalhe.Where(w => w.ContaContabil == (int)dr["ContaContabil"]).ToList().Count() == 0)
                    {
                        foreach (var item in _listaFornecedores.Where(w => w.ContaContabil == (int)dr["ContaContabil"]))
                        {
                            _listaDetalhe.Add(new ConciliacaoContabil.Fornecedores.Detalhe()
                            {
                                ContaContabil = item.ContaContabil,
                                Fornecedor = item.Fornecedor                                
                            });
                        }                        
                    }

                    gridGroupingControl2.DataSource = _listaDetalhe.Where(w => w.ContaContabil == (int)dr["ContaContabil"]).ToList();
                }
            }
        }

        private void gridGroupingControl1_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                if (e.Inner.KeyCode == Keys.Down || e.Inner.KeyCode == Keys.Up)
                {
                    int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                    GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[_rowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        Record dr = rec.GetRecord() as Record;
                        if (dr != null)
                        {
                            if (_listaDetalhe.Where(w => w.ContaContabil == (int)dr["ContaContabil"]).ToList().Count() == 0)
                            {
                                foreach (var item in _listaFornecedores.Where(w => w.ContaContabil == (int)dr["ContaContabil"]))
                                {
                                    _listaDetalhe.Add(new ConciliacaoContabil.Fornecedores.Detalhe()
                                    {
                                        ContaContabil = item.ContaContabil,
                                        Fornecedor = item.Fornecedor
                                    });
                                }
                            }

                            gridGroupingControl2.DataSource = _listaDetalhe.Where(w => w.ContaContabil == (int)dr["ContaContabil"]).ToList();
                        }
                    }
                }
            }
            catch { }
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
                    if (dr != null && (bool)dr["Diferencas"])
                        e.Style.TextColor = Color.DarkOrange;
                }
            }
            catch { }
        }

        private void gridGroupingControl2_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            {
                Record dr;
                GridRecordRow rec = this.gridGroupingControl2.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                if (rec != null)
                {   //marcar quando tem diferença
                    dr = rec.GetRecord() as Record;
                    if (dr != null && (decimal)dr["ValorCPG"] != (decimal)dr["ValorCTB"])
                        e.Style.TextColor = Color.DarkOrange;
                }
            }
            catch { }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {

            _listaResumo.ForEach(u => {
                u.IdEmpresa = _empresa.IdEmpresa;
                u.Referencia = referencia;
                u.Plano = _plano.NumeroPlano;
            });

            if (_listaResumo.Where(w => w.Conferido || w.Existe).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma conta contábil selecionada para confirmar.", Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new ConciliacaoContabilBO().Gravar(_listaResumo))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            string _descricaoC = "";
            string _descricaoN = "";
            foreach (var item in _listaResumo.Where(w => w.Conferido || w.Existe).OrderBy(o => o.Conferido))
            {
                if (item.Existe && !item.Conferido)
                    _descricaoN = _descricaoN + item.ContaContabil + " deixou de ser concialidado - ValorCPG " 
                                + item.ValorCPG + " ValorCTB " + item.ValorCTB + " Diferença " + item.Diferenca;
                else
                    _descricaoC = _descricaoC + item.ContaContabil + " - ValorCPG "
                                + item.ValorCPG + " ValorCTB " + item.ValorCTB + " Diferença " + item.Diferenca;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Efetuou a conciliaçáo de fornecedores da empresa " + empresaComboBoxAdv.Text +
                " referencia " + referenciaMaskedEditBox.Text + " para as Contas contábeis " + _descricaoC + 
                " com emissão inicial " + EmissaoInicialDateTimePicker.Value.ToShortDateString() +
                " ignorando os Tipos de Documentos " + TipoDoctoTextBox.Text + 
                " classificadores entre " + ClassInicialTextBox.Text + " a " + ClassFinalTextBox.Text;

            _log.Tela = "Contabilidade - Conciliações - Fornecedores";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Cancelou a conciliaçáo de fornecedores da empresa " + empresaComboBoxAdv.Text +
                " referencia " + referenciaMaskedEditBox.Text + " para as contas contábeis " + _descricaoN +
                " com emissão inicial " + EmissaoInicialDateTimePicker.Value.ToShortDateString() +
                " ignorando os Tipos de Documentos " + TipoDoctoTextBox.Text +
                " classificadores entre " + ClassInicialTextBox.Text + " a " + ClassFinalTextBox.Text;

            _log.Tela = "Contabilidade - Conciliações - Fornecedores";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            referenciaMaskedEditBox.Text = string.Empty;
            _listaResumo.Clear();
            _listaDetalhe.Clear();
            gridGroupingControl1.DataSource = new List<Classes.ConciliacaoContabil.Fornecedores.Resumo>();
            gridGroupingControl2.DataSource = new List<Classes.ConciliacaoContabil.Fornecedores.Detalhe>();

            gravarButton.Enabled = false;
            GerarExcelButton.Enabled = false;

            referenciaMaskedEditBox.Focus();
        }

        private void EmissaoInicialDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void EmissaoInicialDateTimePicker_Validating(object sender, CancelEventArgs e)
        {

            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void ClassInicialTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void ClassInicialTextBox_Validating(object sender, CancelEventArgs e)
        {

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void GerarExcelButton_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Publicas._caminhoAnexosRateioCTB))
                System.IO.Directory.CreateDirectory(Publicas._caminhoAnexosRateioCTB);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            decimal TotalCPG = 0;
            decimal TotalCTB = 0;
            decimal TotalGCPG = 0;
            decimal TotalGCTB = 0;

            object misValue = System.Reflection.Missing.Value;

            string nomeArquivo = "ConciliacaoFornecedores_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" + referenciaMaskedEditBox.ClipText;

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
                               

                foreach (var itemC in _listaResumo.OrderBy(o => o.ContaContabil))
                {
                    col = 1;
                    #region titulo colunas

                    xlWorkSheet.Cells[linha, col] = "Conta Contábil ";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Valor CPG";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Valor CTB";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Diferença";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Explicações";
                    col++;

                    #endregion

                    col = 1;
                    linha++;

                    #region Cabeçalho da Nota
                    
                    xlWorkSheet.Cells[linha, col] = itemC.ContaContabil;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.ValorCPG;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.ValorCTB;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Diferenca;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Explicacao;

                    linha++;
                    col = 1;

                    #region Cabeçalho itens nota
                    xlWorkSheet.Cells[linha, col] = "Origem ";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Tipo Docto";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Valor CPG";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Valor CTB";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Fornecedor";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Documento";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Entrada";
                    col++;                    
                    xlWorkSheet.Cells[linha, col] = "Emissão";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Vencimento";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Pagamento";
                    col++;                    
                    xlWorkSheet.Cells[linha, col] = "Documento CTB";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Documento BCO";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Observação";
                    col++;
                    #endregion
                    TotalCPG = 0;
                    TotalCTB = 0;

                    foreach (var item in _listaDetalhe.Where(w => w.ContaContabil == itemC.ContaContabil))
                    {
                        linha++;
                        col = 1;
                        TotalCPG = TotalCPG + item.ValorCPG;
                        TotalCTB = TotalCTB + item.ValorCTB;
                        TotalGCPG = TotalGCPG + item.ValorCPG;
                        TotalGCTB = TotalGCTB + item.ValorCTB;

                        xlWorkSheet.Cells[linha, col] = item.Origem;
                        col++;
                        xlWorkSheet.Cells[linha, col] = item.TipoDocto;
                        col++;
                        xlWorkSheet.Cells[linha, col] = item.ValorCPG;
                        col++;
                        xlWorkSheet.Cells[linha, col] = item.ValorCTB;
                        col++;
                        xlWorkSheet.Cells[linha, col] = item.Fornecedor;
                        col++;
                        xlWorkSheet.Cells[linha, col] = item.DoctoCPG;
                        col++;
                        xlWorkSheet.Cells[linha, col] = (item.Entrada ?? DateTime.MinValue) == DateTime.MinValue ? "" : (item.Entrada ?? DateTime.MinValue).ToShortDateString() + " '";
                        col++;                        
                        xlWorkSheet.Cells[linha, col] = (item.Emissao ?? DateTime.MinValue) == DateTime.MinValue ? "" : (item.Emissao ?? DateTime.MinValue).ToShortDateString() + " '";
                        col++;
                        xlWorkSheet.Cells[linha, col] = (item.Vencimento ?? DateTime.MinValue) == DateTime.MinValue ? "" : (item.Vencimento ?? DateTime.MinValue).ToShortDateString() + " '";
                        col++;
                        xlWorkSheet.Cells[linha, col] = (item.Pagamento ?? DateTime.MinValue) == DateTime.MinValue ? "" : (item.Pagamento ?? DateTime.MinValue).ToShortDateString() + " '";
                        col++;                        
                        xlWorkSheet.Cells[linha, col] = item.DoctoCTB;
                        col++;
                        xlWorkSheet.Cells[linha, col] = item.DoctoBCO;
                        col++;
                        xlWorkSheet.Cells[linha, col] = item.Observacao;
                    }
                    col = 1;
                    linha++;
                    xlWorkSheet.Cells[linha, col] = "Totais";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "";
                    col++;
                    xlWorkSheet.Cells[linha, col] = TotalCPG;
                    col++;
                    xlWorkSheet.Cells[linha, col] = TotalCTB;
                    col++;
                    xlWorkSheet.Cells[linha, col] = "";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "";
                    col++;                    
                    xlWorkSheet.Cells[linha, col] = "";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "";
                    #endregion

                    linha++;
                    linha++;
                }

                col = 1;
                linha++;
                xlWorkSheet.Cells[linha, col] = "Totais geral";
                col++;
                xlWorkSheet.Cells[linha, col] = "";
                col++;
                xlWorkSheet.Cells[linha, col] = TotalGCPG;
                col++;
                xlWorkSheet.Cells[linha, col] = TotalGCTB;
                col++;
                xlWorkSheet.Cells[linha, col] = "";
                col++;
                xlWorkSheet.Cells[linha, col] = "";
                col++;
                xlWorkSheet.Cells[linha, col] = "";
                col++;
                xlWorkSheet.Cells[linha, col] = "";
                col++;
                xlWorkSheet.Cells[linha, col] = "";
                col++;
                xlWorkSheet.Cells[linha, col] = "";
                col++;
                xlWorkSheet.Cells[linha, col] = "";
                col++;
                xlWorkSheet.Cells[linha, col] = "";
                col++;
                xlWorkSheet.Cells[linha, col] = "";

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
                this.Refresh();
                
                new Notificacoes.Mensagem("Não foi possível gerar o arquivo." + Environment.NewLine +
                    "Tente em outra maquina." + Environment.NewLine + ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
            }
        }
    }
}
