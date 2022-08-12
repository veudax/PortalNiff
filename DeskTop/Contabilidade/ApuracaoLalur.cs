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
    public partial class ApuracaoLalur : Form
    {
        public ApuracaoLalur()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;
                referenciaMaskedEditBox.UseBorderColorOnFocus = false;
                RecolherCheckBox.ForeColor = PeriodoEncerradoCheckBox.ForeColor;
                ConsolidarCheckBox.ForeColor = PeriodoEncerradoCheckBox.ForeColor;

                referenciaMaskedEditBox.BackColor = empresaComboBoxAdv.BackColor;

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
                }

                ValorAtualCurrencyTextBox.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                ValorAtualCurrencyTextBox.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                ValorAtualCurrencyTextBox.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                ValorAtualCurrencyTextBox.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                ValorAtualCurrencyTextBox.BackGroundColor = Publicas._fundo;

            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Lalur.Apuracao _apuracao;
        Classes.Lalur.Parametros _parametros;
        Classes.Lalur.ValoresContas _valoresContas;
        Classes.RateioBeneficios.PlanoContabil _plano;
        Classes.RateioBeneficios.ContasContabeis _contas;

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        List<Classes.Lalur.Valores> _listaValores;
        List<Classes.Lalur.Valores> _listaValoresLog;
        List<Classes.Lalur.Formulas> _listaFormulas;
        List<Acumulado> _listaAcumulado;

        class Acumulado
        {
            public int IdFormula { get; set; }
            public string Descricao { get; set; }
            public int Ordem { get; set; }
            public bool Destacar { get; set; }
            public decimal Valor01 { get; set; }
            public decimal Valor02 { get; set; }
            public decimal Valor03 { get; set; }
            public decimal Valor04 { get; set; }
            public decimal Valor05 { get; set; }
            public decimal Valor06 { get; set; }
            public decimal Valor07 { get; set; }
            public decimal Valor08 { get; set; }
            public decimal Valor09 { get; set; }
            public decimal Valor10 { get; set; }
            public decimal Valor11 { get; set; }
            public decimal Valor12 { get; set; }
            public List<AcumuladoContabil> Contas { get; set; }
        }

        class AcumuladoContabil
        {
            public int Codigo { get; set; }
            public string NomeConta { get; set; }
            public decimal Valor01 { get; set; }
            public decimal Valor02 { get; set; }
            public decimal Valor03 { get; set; }
            public decimal Valor04 { get; set; }
            public decimal Valor05 { get; set; }
            public decimal Valor06 { get; set; }
            public decimal Valor07 { get; set; }
            public decimal Valor08 { get; set; }
            public decimal Valor09 { get; set; }
            public decimal Valor10 { get; set; }
            public decimal Valor11 { get; set; }
            public decimal Valor12 { get; set; }
        }

        DateTime _dataInicio;
        string _referencia;
        GridCurrentCell _colunaCorrente;
        Element _elementoGridClicado;
        int idFormulaCsllMesAnterior = 0;
        int IdFormulaIrpjMesAnterior = 0;
        decimal _lucroRealAntes = 0;
        decimal _compensacao = 0;

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

        private void ApuracaoLalur_Shown(object sender, EventArgs e)
        {
            this.Top = 60;
            #region Empresas autorizadas para o colaborador
            _listaEmpresas = new EmpresaBO().Listar(false);

            _empresaDoColaborador = new ColaboradoresBO().Listar(Publicas._idColaborador);

            if (!Publicas._usuario.ApenasConsultaDRE || Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.IdEmpresa == 19)
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
            #endregion

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


            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(gridGroupingControl1);

            gridGroupingControl1.DataSource = new List<Lalur.ContasDaFormula>();
            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl1.TableControl.CellToolTip.Active = true;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl1.RecordNavigationBar.Label = "Apuração";

            GridConditionalFormatDescriptor format1 = new GridConditionalFormatDescriptor();
            format1.Appearance.AnyRecordFieldCell.Font.Bold = true;
            format1.Expression = "[Destacar] like 'true'";
            format1.Name = "ConditionalFormat 1";


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

            gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(format1);
            gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(format1);

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
                gridGroupingControl1.SetMetroStyle(metroColor);
                gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            gridGroupingControl1.Refresh();

            gridGroupingControl2.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl2.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl2.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl2.TableControl.CellToolTip.Active = true;
            gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl2.RecordNavigationBar.Label = "Apuração";

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

            gridGroupingControl2.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            gridGroupingControl2.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            gridGroupingControl2.Refresh();

        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
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
                referenciaMaskedEditBox.Focus();
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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PeriodoEncerradoCheckBox.Focus();
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
            empresaComboBoxAdv.FlatBorderColor = Publicas._bordaSaida;

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

            _parametros = new LalurBO().ConsultarParametros(_empresa.IdEmpresa);

            if (!_parametros.Existe)
            {
                new Notificacoes.Mensagem("Parâmetro não cadastrado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            _listaFormulas = new LalurBO().Listar(_empresa.IdEmpresa);

            if (_listaFormulas.Count() == 0)
            {
                new Notificacoes.Mensagem("Fórmulas não cadastradas para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
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
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            referenciaMaskedEditBox.Cursor = Cursors.WaitCursor;
            mensagemSistemaLabel.Text = "Aguarde, Pesquisando...";
            this.Refresh();

            _referencia = referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2);
            _apuracao = new LalurBO().Consultar(_empresa.IdEmpresa, _referencia);

            PeriodoEncerradoCheckBox.Checked = false;

            if (_listaValores == null)
                _listaValores = new List<Lalur.Valores>();
            else
                _listaValores.Clear();

            _listaValoresLog = new List<Lalur.Valores>();

            if (_apuracao.Existe)
            {
                PeriodoEncerradoCheckBox.Checked = _apuracao.Fechado;
                _listaValores = new LalurBO().ListarApuracao(_apuracao.Id);
                _listaValoresLog = new LalurBO().ListarApuracao(_apuracao.Id);
                
            }
            else
            {
                decimal _valor = 0;
                string _formula = "";
                string[] id;

                foreach (var item in _listaFormulas.Where(w => w.Ativo)
                                                   .OrderBy(o=> o.Ordem))
                {
                    Classes.Lalur.Valores _val = new Classes.Lalur.Valores();

                    _val.IdFormula = item.Id;
                    _val.Ordem = item.Ordem;
                    _val.Descricao = item.Descricao;
                    _val.Destacar = item.Destacar;

                    if (_val.Descricao.ToUpper().Contains("%Compensacao".ToUpper()))
                        _val.Percentual = _parametros.PercentualCompensacaoNegativa;
                    else
                    {
                        if (_val.Descricao.ToUpper().Contains("%CSLL".ToUpper()))
                            _val.Percentual = _parametros.PercentualCSLL;
                        else
                        {
                            if (_val.Descricao.ToUpper().Contains("%IRPJ".ToUpper()))
                                _val.Percentual = _parametros.PercentualIRPJ;
                            else
                            {
                                if (_val.Descricao.ToUpper().Contains("%ParcIsenta".ToUpper()))
                                    _val.Percentual = _parametros.ValorParcelaIsenta;
                                else
                                {
                                    if (_val.Descricao.ToUpper().Contains("%Adicional".ToUpper()))
                                        _val.Percentual = _parametros.PercentualAdicionalPagar;
                                    else
                                        if (_val.Descricao.ToUpper().Contains("%PAT".ToUpper()))
                                        _val.Percentual = _parametros.PercentualPat;
                                }
                            }
                        }
                    }

                    if (!item.Totalizador)
                    {
                        _valor = new LalurBO().ValoresContabeis(_empresa.CodigoEmpresaGlobus, _referencia.Substring(0, 4) + "01", _referencia, item.Id, _empresa.IdEmpresa, ConsolidarCheckBox.Checked);
                        _val.Contas = new LalurBO().ValoresContabeisDetalhado(item.Id, _empresa.CodigoEmpresaGlobus, _referencia.Substring(0,4)+"01", _referencia, item.Id, _empresa.IdEmpresa, ConsolidarCheckBox.Checked);
                        
                    }
                    else
                    {
                        _valor = 0;

                        if (_lucroRealAntes >= 0 || (item.Descricao.ToUpper().Contains("Resultado Liquido".ToUpper()) || item.Ordem == 12))
                        {
                            if (item.Descricao.ToUpper().Contains("Mês Anterior".ToUpper()))
                            {
                                if (item.Descricao.ToUpper().Contains("CSLL".ToUpper()))
                                    idFormulaCsllMesAnterior = item.Id;

                                if (item.Descricao.ToUpper().Contains("IRPJ".ToUpper()))
                                    IdFormulaIrpjMesAnterior = item.Id;

                                if (referenciaMaskedEditBox.Text.Substring(0, 2) == "01")
                                    _valor = 0;
                                else
                                {
                                    id = item.Formula.Split(';');

                                    int idF = Convert.ToInt32(Publicas.OnlyNumbers(id[0].Trim()));
                                    _valor = new LalurBO().MesAnterior(idF, (Convert.ToInt32(_referencia) - 1).ToString());
                                }
                            }
                            else
                            {

                                _formula = item.Formula;

                                if (_formula != "")
                                {
                                    if (_val.Descricao.ToUpper().Equals("Base de Cálculo".ToUpper()))
                                    {
                                        id = item.Formula.Split(';');
                                        _formula = ValoresFormula(id, _formula, _val.Descricao);
                                    }

                                    id = item.Formula.Split('+');
                                    _formula = ValoresFormula(id, _formula, _val.Descricao.ToUpper());

                                    id = item.Formula.Split('-');
                                    _formula = ValoresFormula(id, _formula, _val.Descricao.ToUpper());

                                    id = item.Formula.Split('*');
                                    _formula = ValoresFormula(id, _formula, _val.Descricao.ToUpper());

                                    _valor = new ItensAvaliacaoMetaBO().CalculoFormula(_formula);
                                    if (Publicas.mensagemDeErro != "")
                                    {
                                        new Notificacoes.Mensagem("Problemas durante o calculo da fórmula." + Environment.NewLine +
                                            item.Formula + " " + _val.Descricao, Publicas.TipoMensagem.Alerta).ShowDialog();

                                        return;
                                    }

                                    if ((item.Descricao.ToUpper().Equals("Base de Cálculo Após Parcela Isenta".ToUpper()) || item.Descricao.ToUpper().Contains("Adicional".ToUpper())) && _valor < 0)
                                        _valor = 0;
                                    if (item.Descricao.ToUpper().Equals("Parcela Isenta".ToUpper()) && (referenciaMaskedEditBox.Text.Substring(0, 2) != "01"))
                                        _valor = _valor * Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(0, 2));
                                }
                            }
                        }
                    }

                    if (item.Descricao.ToUpper().Equals("Compensação de Base de Cálculo Negativa".ToUpper()))
                    {
                        _compensacao = Math.Round(_valor,2);
                        _val.Percentual = _parametros.PercentualCompensacaoNegativa;

                        if (_compensacao > _parametros.LimiteCSLL && _parametros.LimiteCSLL > 0)
                            _valor = _parametros.LimiteCSLL;
                    }

                    //if (!item.Descricao.ToUpper().Contains("Resultado".ToUpper()) && !item.Totalizador)
                        //_val.Valor = Math.Abs(Math.Round(_valor, 2));
                    //else
                        _val.Valor = Math.Round(_valor, 2);

                    _listaValores.Add(_val);

                    if (item.Descricao.ToUpper().Equals("Lucro Real Antes das Compensações".ToUpper()))
                        _lucroRealAntes = _valor;

                    if (item.Descricao.ToUpper().EndsWith("a Pagar".ToUpper()) || item.Descricao.ToUpper().EndsWith("a Recolher".ToUpper()))
                    {
                        decimal _valorMesAnterior = 0;
                        int idF = 0;

                        _valorMesAnterior = new LalurBO().MesAnterior(item.Id, (Convert.ToInt32(_referencia) - 1).ToString());

                        if (_valor < 0 || _valorMesAnterior < 0)
                        {
                            if (item.Descricao.ToUpper().EndsWith("a Pagar".ToUpper()))
                            {
                                foreach (var itemX in _listaFormulas.Where(w => w.Id == idFormulaCsllMesAnterior))
                                {
                                    _formula = itemX.Formula;
                                }

                                id = _formula.Split(';');

                                if (_valorMesAnterior < 0)
                                    idF = Convert.ToInt32(Publicas.OnlyNumbers(id[1]));
                                else
                                    idF = Convert.ToInt32(Publicas.OnlyNumbers(id[0]));

                                _valor = new LalurBO().MesAnterior(idF, (Convert.ToInt32(_referencia) - 1).ToString());
                                foreach (var itemV in _listaValores.Where(w => w.IdFormula == idFormulaCsllMesAnterior))
                                    itemV.Valor = _valor;
                            }
                            else
                            {
                                foreach (var itemX in _listaFormulas.Where(w => w.Id == IdFormulaIrpjMesAnterior))
                                {
                                    _formula = itemX.Formula;
                                }
                                
                                id = _formula.Split(';');

                                try
                                {
                                    if (_valorMesAnterior < 0)
                                        idF = Convert.ToInt32(Publicas.OnlyNumbers(id[1]));
                                    else
                                        idF = Convert.ToInt32(Publicas.OnlyNumbers(id[0]));

                                    _valor = new LalurBO().MesAnterior(idF, (Convert.ToInt32(_referencia) - 1).ToString());

                                    foreach (var itemV in _listaValores.Where(w => w.IdFormula == IdFormulaIrpjMesAnterior))
                                        itemV.Valor = _valor;
                                }
                                catch { }

                            }
                        }
                    }
                }                
            }

            RecalculaTotalizadores();

            gridGroupingControl1.DataSource = _listaValores.OrderBy(o => o.Ordem).ToList();

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Relations.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.ShowRowHeader = false;
                gridGroupingControl1.TableDescriptor.Relations[i].ChildTableDescriptor.AllowEdit = false;
                gridGroupingControl1.TableDescriptor.Relations[i].ChildTableDescriptor.AllowNew = false;
                gridGroupingControl1.TableDescriptor.Relations[i].ChildTableDescriptor.AllowRemove = false;
            }
            
            for (int j = 0; j < gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns.Count; j++)
            {
                if (gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("NomeConta"))
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].ReadOnly = true;
                else
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].ReadOnly = false;

                gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].AllowFilter = false;
                gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;

                gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                try
                {
                    if (gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("ValorReal") ||
                        gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("Id") ||
                        gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Plano") ||
                        gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Existe"))                        
                        gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName);
                }
                catch { }
                
                if (gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Codigo"))
                {
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n0";
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Código";
                }

                if (gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                {
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "#,##0.00;(#,##0.00);#.##";
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                    gridGroupingControl1.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 120;
                }                
            }

            #region buscar o aculumado
            _listaAcumulado = new List<Acumulado>();

            bool _encontrou = false;

            for (int i = 1; i <= 12; i++)
            {
                Classes.Lalur.Apuracao _apu = new LalurBO().Consultar(_empresa.IdEmpresa, referenciaMaskedEditBox.Text.Substring(3, 4) + i.ToString("00"));
                if (_apu.Existe)
                {
                    List<Classes.Lalur.Valores> _listaV = new LalurBO().ListarApuracao(_apu.Id);

                    foreach (var item in _listaV)
                    {
                        foreach (var itemV in _listaAcumulado.Where(w => w.IdFormula == item.IdFormula))
                        {
                            _encontrou = true;

                            switch (i)
                            {
                                case 1:
                                    itemV.Valor01 = item.Valor;
                                    break;
                                case 2:
                                    itemV.Valor02 = item.Valor;
                                    break;
                                case 3:
                                    itemV.Valor03 = item.Valor;
                                    break;
                                case 4:
                                    itemV.Valor04 = item.Valor;
                                    break;
                                case 5:
                                    itemV.Valor05 = item.Valor;
                                    break;
                                case 6:
                                    itemV.Valor06 = item.Valor;
                                    break;
                                case 7:
                                    itemV.Valor07 = item.Valor;
                                    break;
                                case 8:
                                    itemV.Valor08 = item.Valor;
                                    break;
                                case 9:
                                    itemV.Valor09 = item.Valor;
                                    break;
                                case 10:
                                    itemV.Valor10 = item.Valor;
                                    break;
                                case 11:
                                    itemV.Valor11 = item.Valor;
                                    break;
                                case 12:
                                    itemV.Valor12 = item.Valor;
                                    break;
                            }

                            foreach (var itemC in item.Contas)
                            {
                                foreach (var itemX in itemV.Contas.Where(w => w.Codigo == itemC.Codigo))
                                {
                                    switch (i)
                                    {
                                        case 1:
                                            itemX.Valor01 = itemC.Valor;
                                            break;
                                        case 2:
                                            itemX.Valor02 = itemC.Valor;
                                            break;
                                        case 3:
                                            itemX.Valor03 = itemC.Valor;
                                            break;
                                        case 4:
                                            itemX.Valor04 = itemC.Valor;
                                            break;
                                        case 5:
                                            itemX.Valor05 = itemC.Valor;
                                            break;
                                        case 6:
                                            itemX.Valor06 = itemC.Valor;
                                            break;
                                        case 7:
                                            itemX.Valor07 = itemC.Valor;
                                            break;
                                        case 8:
                                            itemX.Valor08 = itemC.Valor;
                                            break;
                                        case 9:
                                            itemX.Valor09 = itemC.Valor;
                                            break;
                                        case 10:
                                            itemX.Valor10 = itemC.Valor;
                                            break;
                                        case 11:
                                            itemX.Valor11 = itemC.Valor;
                                            break;
                                        case 12:
                                            itemX.Valor12 = itemC.Valor;
                                            break;
                                    }
                                }
                            }

                            break;
                        }

                        if (!_encontrou)
                        {
                            Acumulado _acu = new Acumulado();
                            _acu.Descricao = item.Descricao;
                            _acu.IdFormula = item.IdFormula;
                            _acu.Ordem = item.Ordem;
                            _acu.Destacar = item.Destacar;

                            switch (i)
                            {
                                case 1:
                                    _acu.Valor01 = item.Valor;
                                    break;
                                case 2:
                                    _acu.Valor02 = item.Valor;
                                    break;
                                case 3:
                                    _acu.Valor03 = item.Valor;
                                    break;
                                case 4:
                                    _acu.Valor04 = item.Valor;
                                    break;
                                case 5:
                                    _acu.Valor05 = item.Valor;
                                    break;
                                case 6:
                                    _acu.Valor06 = item.Valor;
                                    break;
                                case 7:
                                    _acu.Valor07 = item.Valor;
                                    break;
                                case 8:
                                    _acu.Valor08 = item.Valor;
                                    break;
                                case 9:
                                    _acu.Valor09 = item.Valor;
                                    break;
                                case 10:
                                    _acu.Valor10 = item.Valor;
                                    break;
                                case 11:
                                    _acu.Valor11 = item.Valor;
                                    break;
                                case 12:
                                    _acu.Valor12 = item.Valor;
                                    break;
                            }

                            _acu.Contas = new List<AcumuladoContabil>();

                            foreach (var itemC in item.Contas)
                            {

                                AcumuladoContabil conta = new AcumuladoContabil();

                                conta.Codigo = itemC.Codigo;
                                conta.NomeConta = itemC.NomeConta;

                                switch (i)
                                {
                                    case 1:
                                        conta.Valor01 = itemC.Valor;
                                        break;
                                    case 2:
                                        conta.Valor02 = itemC.Valor;
                                        break;
                                    case 3:
                                        conta.Valor03 = itemC.Valor;
                                        break;
                                    case 4:
                                        conta.Valor04 = itemC.Valor;
                                        break;
                                    case 5:
                                        conta.Valor05 = itemC.Valor;
                                        break;
                                    case 6:
                                        conta.Valor06 = itemC.Valor;
                                        break;
                                    case 7:
                                        conta.Valor07 = itemC.Valor;
                                        break;
                                    case 8:
                                        conta.Valor08 = itemC.Valor;
                                        break;
                                    case 9:
                                        conta.Valor09 = itemC.Valor;
                                        break;
                                    case 10:
                                        conta.Valor10 = itemC.Valor;
                                        break;
                                    case 11:
                                        conta.Valor11 = itemC.Valor;
                                        break;
                                    case 12:
                                        conta.Valor12 = itemC.Valor;
                                        break;
                                }

                                _acu.Contas.Add(conta);
                            }

                            _listaAcumulado.Add(_acu);
                        }
                    }
                }
            }
            #endregion

            gridGroupingControl2.DataSource = _listaAcumulado;

            for (int i = 0; i < gridGroupingControl2.TableDescriptor.Relations.Count; i++)
            {
                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.ShowRowHeader = false;
                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.AllowEdit = false;
                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.AllowNew = false;
                gridGroupingControl2.TableDescriptor.Relations[i].ChildTableDescriptor.AllowRemove = false;
            }

            for (int j = 0; j < gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns.Count; j++)
            {
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].ReadOnly = true;

                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].AllowFilter = false;
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;

                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Codigo"))
                {
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n0";
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Código";
                }

                if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("NomeConta"))
                {
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Nome";
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 187;
                }

                if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("Valor"))
                {
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "#,##0.00;(#,##0.00);#.##";

                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("01"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Janeiro";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("02"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Fevereiro";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("03"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Março";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("04"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Abril";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("05"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Maio";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("06"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Junho";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("07"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Julho";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("08"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Agosto";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("09"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Setembro";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("10"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Outubro";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("11"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Novembro";
                    if (gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("12"))
                        gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Dezembro";

                    gridGroupingControl2.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 120;
                }
            }

            gravarButton.Text = (_apuracao.Existe ? "&Alterar" : "&Gravar");

            gravarButton.Enabled = !_apuracao.Fechado || (_apuracao.Fechado && Publicas._usuario.PermiteReabrirDRE);
            excluirButton.Enabled = _apuracao.Existe && (!_apuracao.Fechado || (_apuracao.Fechado && Publicas._usuario.PermiteReabrirDRE));
            GerarExcelButton.Enabled = true;

            referenciaMaskedEditBox.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";           

            gridGroupingControl1.Table.ExpandAllRecords();
            gridGroupingControl1.Refresh();

            gridGroupingControl2.Table.ExpandAllRecords();
            gridGroupingControl2.Refresh();
            //this.Refresh();
        }

        private string ValoresFormula(string[] id, string _formula, string descMeta = "" )
        {
            int idFormula = 0;
            int i = 0;

            foreach (var itemA in id)
            {
                i++;
                if (string.IsNullOrEmpty(itemA))
                    continue;

                if (descMeta.ToUpper().Equals("Base de Cálculo".ToUpper()))
                {
                    if (_lucroRealAntes < 0)
                        _formula = "0";

                    if (_compensacao > _parametros.LimiteIRPJ && _parametros.LimiteIRPJ > 0)
                    {
                        if (i == 1)
                        {
                            _formula = _formula.Replace(itemA, "").Replace(";","");
                            continue;
                        }
                    }
                    else
                    {
                        //if (i == 2)
                        {
                            try
                            {
                                _formula = _formula.Replace(id[1], "").Replace(";", "");
                            }
                            catch { }
                            //continue;
                        }
                    }
                }

                if (itemA.Contains("-"))
                {
                    id = itemA.Split('-');
                    _formula = ValoresFormula(id, _formula);
                    break;
                }

                if (itemA.Contains("*"))
                {
                    id = itemA.Split('*');
                    _formula = ValoresFormula(id, _formula);
                    break;
                }

                if (!itemA.Contains("%"))
                {
                    idFormula = Convert.ToInt32(Publicas.OnlyNumbers(itemA.Trim()));

                    foreach (var itemV in _listaValores.Where(w => w.IdFormula == idFormula))
                    {
                        _formula = _formula.Replace("Formula " + idFormula, itemV.Valor.ToString()).Replace("[", "").Replace("]", "").Replace(",",".");
                    }
                }
                else
                {
                    if (itemA.ToUpper().Contains("Compensacao".ToUpper()))
                    {
                        _formula = _formula.Replace("%Compensacao", _parametros.PercentualCompensacaoNegativa.ToString()).Replace("[", "").Replace("]", "").Replace(",", ".");
                        
                    }
                    else
                    {
                        if (itemA.ToUpper().Contains("LimiteCS".ToUpper()))
                            _formula = _formula.Replace("%LimiteCSLL", _parametros.LimiteCSLL.ToString()).Replace("[", "").Replace("]", "").Replace(",", ".");
                        else
                        {
                            if (itemA.ToUpper().Contains("CSLL".ToUpper()))
                                _formula = _formula.Replace("%CSLL", _parametros.PercentualCSLL.ToString()).Replace("[", "").Replace("]", "").Replace(",", ".");
                            else
                            {
                                if (itemA.ToUpper().Contains("LimiteIR".ToUpper()))
                                    _formula = _formula.Replace("%LimiteIRPJ", _parametros.LimiteIRPJ.ToString()).Replace("[", "").Replace("]", "").Replace(",", ".");
                                else
                                {
                                    if (itemA.ToUpper().Contains("IRPJ".ToUpper()))
                                        _formula = _formula.Replace("%IRPJ", _parametros.PercentualIRPJ.ToString()).Replace("[", "").Replace("]", "").Replace(",", ".");
                                    else
                                    {
                                        if (itemA.ToUpper().Contains("ParcIsenta".ToUpper()))
                                            _formula = _formula.Replace("%ParcIsenta", _parametros.ValorParcelaIsenta.ToString()).Replace("[", "").Replace("]", "").Replace(",", ".");
                                        else
                                        {
                                            if (itemA.ToUpper().Contains("Adicional".ToUpper()))
                                                _formula = _formula.Replace("%Adicional", _parametros.PercentualAdicionalPagar.ToString()).Replace("[", "").Replace("]", "").Replace(",", ".");
                                            else
                                            {
                                                if (itemA.ToUpper().Contains("PAT".ToUpper()))
                                                    _formula = _formula.Replace("%PAT", _parametros.PercentualPat.ToString()).Replace("[", "").Replace("]", "").Replace(",", ".");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return _formula;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            if (e.Style.TableCellIdentity.DisplayElement.Kind != Syncfusion.Grouping.DisplayElementKind.Record)
                return;

            try
            {
                if (e.TableCellIdentity.TableCellType == GridTableCellType.RecordFieldCell)
                {
                    if (e.TableCellIdentity.Column.MappingName == "Valor")
                    {
                        if (Convert.ToDecimal(e.Style.Text) < 0)
                            e.Style.TextColor = (Publicas._TemaBlack ? Color.DarkOrange : Color.DarkRed);
                    }
                }
            }
            catch { }
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            _elementoGridClicado = this.gridGroupingControl1.Table.GetInnerMostCurrentElement();
            int id = 0;
            int codigo = 0;

            gridGroupingControl1.ContextMenuStrip = null;

            try
            {
                if (_elementoGridClicado.ChildTableGroupLevel != 0)
                {
                    gridGroupingControl1.ContextMenuStrip = contextMenuStrip1;

                    try
                    {
                        GridRecord rec = _elementoGridClicado as GridRecord;

                        if (rec != null)
                        {
                            Record dr = rec.GetRecord() as Record;

                            if (dr != null)
                            {
                                id = (int)dr["IdFormula"];
                                codigo = (int)dr["Codigo"];

                                foreach (var item in _listaValores.Where(w => w.IdFormula == id))
                                {
                                    foreach (var itemC in item.Contas.Where(w => w.Codigo == codigo))
                                        _valoresContas = itemC;
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            catch
            { }

            gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

        }

        private void gridGroupingControl1_TableControlCellMouseDown(object sender, GridTableControlCellMouseEventArgs e)
        {
            _colunaCorrente = gridGroupingControl1.TableControl.CurrentCell;

            _elementoGridClicado = this.gridGroupingControl1.Table.GetInnerMostCurrentElement();

            try
            {
                if (_elementoGridClicado.ChildTableGroupLevel != 0)
                {
                    gridGroupingControl1.ContextMenuStrip = contextMenuStrip1;
                }
            }
            catch
            {
                gridGroupingControl1.Refresh();
            }
        }
             
        private void gravarButton_Click(object sender, EventArgs e)
        {
            _apuracao.IdEmpresa = _empresa.IdEmpresa;
            _apuracao.Referencia = _referencia;
            _apuracao.Fechado = PeriodoEncerradoCheckBox.Checked;

            if (!new LalurBO().Gravar(_apuracao, _listaValores))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            string _descricao = "";

            if (!_apuracao.Existe)
            {
                foreach (var item in _listaValores)
                {
                    foreach (var itemL in _listaValoresLog.Where(w => w.IdFormula == item.IdFormula))
                    {
                        if (item.Valor != itemL.Valor)
                            _descricao = _descricao + " Descrição " + item.Descricao + " Valor de " + itemL.Valor.ToString() + " para " + item.Valor;
                    }
                }
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou a apuração do Lalur da empresa " + empresaComboBoxAdv.Text + " referência " + referenciaMaskedEditBox.Text + _descricao;
            _log.Tela = "Contabilidade - Lalur - Apuração";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            new Notificacoes.Mensagem("Gravado com Sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();

            limparButton_Click(sender, new CancelEventArgs());
        }
        
        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GridRecord rec = _elementoGridClicado as GridRecord;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;

                    ContasTextBox.Text = ((int)dr["Codigo"]).ToString();
                    NomeContaTextBox.Text = (string)dr["NomeConta"];
                    PlanoTextBox.Text = ((int)dr["Plano"]).ToString();
                    ValorAtualCurrencyTextBox.DecimalValue = (decimal)dr["ValorReal"];
                }
            }
            catch { }

            EdicaoPanel.Visible = true;
            PlanoTextBox.Focus();
        }

        private void RecalculaTotalizadores()
        {
            int id = 0;
            decimal _valor = 0;
            string _formula = "";
            string[] idF;

            _lucroRealAntes = 0;
            _compensacao = 0;
            foreach (var itemF in _listaFormulas.Where(w => w.Ativo && w.Totalizador)
                                                .OrderBy(o => o.Ordem))
            {
                foreach (var item in _listaValores.Where(w => w.IdFormula == itemF.Id))
                {
                    if (itemF.Formula.ToUpper().Contains("%Compensacao".ToUpper()))
                        item.Percentual = _parametros.PercentualCompensacaoNegativa;
                    else
                    {
                        if (itemF.Formula.ToUpper().Contains("%CSLL".ToUpper()))
                            item.Percentual = _parametros.PercentualCSLL;
                        else
                        {
                            if (itemF.Formula.ToUpper().Contains("%IRPJ".ToUpper()))
                                item.Percentual = _parametros.PercentualIRPJ;
                            else
                            {
                                if (itemF.Formula.ToUpper().Contains("%ParcIsenta".ToUpper()))
                                    item.Percentual = _parametros.ValorParcelaIsenta;
                                else
                                {
                                    if (itemF.Formula.ToUpper().Contains("%Adicional".ToUpper()))
                                        item.Percentual = _parametros.PercentualAdicionalPagar;
                                    else
                                        if (itemF.Formula.ToUpper().Contains("%PAT".ToUpper()))
                                        item.Percentual = _parametros.PercentualPat;
                                }
                            }
                        }
                    }

                    if (itemF.Descricao.ToUpper().Contains("Mês Anterior".ToUpper()))
                    {

                        if (item.Descricao.ToUpper().Contains("CSLL".ToUpper()))
                            idFormulaCsllMesAnterior = item.IdFormula;

                        if (item.Descricao.ToUpper().Contains("IRPJ".ToUpper()))
                            IdFormulaIrpjMesAnterior = item.IdFormula;

                        if (referenciaMaskedEditBox.Text.Substring(0, 2) == "01")
                        {
                            item.Valor = 0;
                            continue;
                        }
                        else
                        {
                            id = Convert.ToInt32(Publicas.OnlyNumbers(itemF.Formula.Trim()));
                            _valor = new LalurBO().MesAnterior(id, (Convert.ToInt32(_referencia) - 1).ToString());
                            continue;
                        }
                    }

                    if (_lucroRealAntes >= 0 || 
                        (itemF.Descricao.ToUpper().Contains("Resultado Liquido".ToUpper()) || 
                         itemF.Descricao.ToUpper().Equals("Base de Cálculo".ToUpper()) || 
                         itemF.Descricao.ToUpper().Equals("Lucro Real".ToUpper())))
                    {
                        _formula = itemF.Formula;

                        if (_formula != "")
                        {
                            if (itemF.Descricao.ToUpper().Equals("Base de Cálculo".ToUpper()))
                            {
                                idF = itemF.Formula.Split(';');
                                _formula = ValoresFormula(idF, _formula, itemF.Descricao);
                            }
                            else
                            {
                                idF = itemF.Formula.Split('+');

                                _formula = ValoresFormula(idF, _formula);

                                idF = itemF.Formula.Split('-');

                                _formula = ValoresFormula(idF, _formula);

                                idF = itemF.Formula.Split('*');

                                _formula = ValoresFormula(idF, _formula);

                            }
                            _valor = new ItensAvaliacaoMetaBO().CalculoFormula(_formula);
                            if (Publicas.mensagemDeErro != "")
                            {
                                new Notificacoes.Mensagem("Problemas durante o calculo da fórmula." + Environment.NewLine +
                                    itemF.Formula + " " + itemF.Descricao, Publicas.TipoMensagem.Alerta).ShowDialog();
                                
                                return;
                            }

                            if ((item.Descricao.ToUpper().Equals("Base de Cálculo Após Parcela Isenta".ToUpper()) || 
                                 item.Descricao.ToUpper().Contains("Adicional".ToUpper())) && _valor < 0)
                                _valor = 0;
                            if (item.Descricao.ToUpper().Equals("Parcela Isenta".ToUpper()) && (referenciaMaskedEditBox.Text.Substring(0, 2) != "01"))
                                _valor = _valor * Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(0, 2));                            
                        }

                        item.Valor = _valor;
                    }

                    decimal _valorMesAnterior = 0;
                    if (itemF.Descricao.ToUpper().EndsWith("a Pagar".ToUpper()) || itemF.Descricao.ToUpper().EndsWith("a Recolher".ToUpper()))
                    {
                        if (_valor < 0)
                        {

                            // Verifica se o mes anterior foi prejuizo 
                            _valorMesAnterior = new LalurBO().MesAnterior(item.IdFormula, (Convert.ToInt32(_referencia) - 1).ToString());

                            if (itemF.Descricao.ToUpper().EndsWith("a Pagar".ToUpper()))
                            {
                                foreach (var itemX in _listaFormulas.Where(w => w.Id == idFormulaCsllMesAnterior))
                                {
                                    _formula = itemX.Formula;
                                }

                                idF = _formula.Split(';');

                                if (_valorMesAnterior < 0)
                                    id = Convert.ToInt32(Publicas.OnlyNumbers(idF[1]));
                                else
                                    id = Convert.ToInt32(Publicas.OnlyNumbers(idF[0]));

                                _valor = new LalurBO().MesAnterior(id, (Convert.ToInt32(_referencia) - 1).ToString());
                                foreach (var itemV in _listaValores.Where(w => w.IdFormula == idFormulaCsllMesAnterior))
                                    itemV.Valor = _valor;
                            }
                            else
                            {
                                foreach (var itemX in _listaFormulas.Where(w => w.Id == IdFormulaIrpjMesAnterior))
                                {
                                    _formula = itemX.Formula;
                                }
                                idF = _formula.Split(';');

                                if (_valorMesAnterior < 0)
                                    id = Convert.ToInt32(Publicas.OnlyNumbers(idF[1]));
                                else
                                    id = Convert.ToInt32(Publicas.OnlyNumbers(idF[0]));

                                _valor = new LalurBO().MesAnterior(id, (Convert.ToInt32(_referencia) - 1).ToString());
                                foreach (var itemV in _listaValores.Where(w => w.IdFormula == IdFormulaIrpjMesAnterior))
                                    itemV.Valor = _valor;
                            }
                        }
                    }

                    if (item.Descricao.ToUpper().Equals("Lucro Real Antes das Compensações".ToUpper()))
                        _lucroRealAntes = _valor;

                    if (item.Descricao.ToUpper().Equals("Compensação de Base de Cálculo Negativa".ToUpper()))
                    {
                        _compensacao = _valor;
                        if (_compensacao > _parametros.LimiteCSLL && _parametros.LimiteCSLL > 0)
                        {
                            _valor = _parametros.LimiteCSLL;
                            item.Valor = _valor;
                        }
                    }
                }
            }
        }

        private void IncluirButton_Click(object sender, EventArgs e)
        {
            int id = 0;
            int codigo = 0;
            decimal valor = 0;

            try
            {
                GridRecord rec = _elementoGridClicado as GridRecord;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;

                    id = (int)dr["IdFormula"];
                    codigo = (int)dr["Codigo"];
                }
            }
            catch { }

            if (_valoresContas == null)
            {
                foreach (var item in _listaValores.Where(w => w.IdFormula == id))
                {
                    if (item.Contas.Where(w => w.Codigo == Convert.ToInt32(ContasTextBox.Text)).Count() == 0)
                    {
                        item.Contas.Add(new Lalur.ValoresContas()
                        {
                            IdLalur = item.IdLalur,
                            IdLalurValor = item.Id,
                            IdFormula = id,
                            Valor = Math.Abs(ValorAtualCurrencyTextBox.DecimalValue),
                            ValorReal = ValorAtualCurrencyTextBox.DecimalValue,
                            Codigo = Convert.ToInt32(ContasTextBox.Text),
                            NomeConta = NomeContaTextBox.Text,
                            Plano = _plano.NumeroPlano
                        });
                    }
                    else
                    {
                        foreach (var itemC in item.Contas.Where(w => w.Codigo == Convert.ToInt32(ContasTextBox.Text)))
                        {
                            itemC.Valor = Math.Abs(ValorAtualCurrencyTextBox.DecimalValue);
                            itemC.ValorReal = ValorAtualCurrencyTextBox.DecimalValue;
                            itemC.Plano = _plano.NumeroPlano;
                        }
                    }

                    foreach (var itemC in item.Contas)
                    {
                        valor = valor + itemC.ValorReal;
                    }
                    item.Valor = Math.Abs(valor);
                }
            }
            else
            {
                foreach (var item in _listaValores.Where(w => w.IdFormula == id))
                {
                    foreach (var itemC in item.Contas.Where(w => w.Codigo == _valoresContas.Codigo))
                    {
                        itemC.Valor = Math.Abs(ValorAtualCurrencyTextBox.DecimalValue);
                        itemC.ValorReal = ValorAtualCurrencyTextBox.DecimalValue;
                        itemC.Codigo = Convert.ToInt32(ContasTextBox.Text);
                        itemC.NomeConta = NomeContaTextBox.Text;
                        itemC.Plano = _plano.NumeroPlano;
                    }

                    foreach (var itemC in item.Contas)
                    {
                        valor = valor + itemC.ValorReal;
                    }
                    item.Valor = valor;
                }
            }

            RecalculaTotalizadores();

            gridGroupingControl1.DataSource = new List<Lalur.Valores>();
            gridGroupingControl1.DataSource = _listaValores;

            gridGroupingControl1.Table.ExpandAllRecords();
            gridGroupingControl1.Refresh();

            EdicaoPanel.Visible = false;
        }

        private void PlanoTextBox_Validating(object sender, CancelEventArgs e)
        {
            PlanoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
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

        private void ContasTextBox_Validating(object sender, CancelEventArgs e)
        {
            ContasTextBox.BorderColor = Publicas._bordaSaida;

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

            Publicas._idRetornoPesquisa = 0;

            if (ContasTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                ContasTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (ContasTextBox.Text.Trim() == "" || ContasTextBox.Text == "0")
                {
                    ContasTextBox.Text = string.Empty;
                    ContasTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(ContasTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContasTextBox.Focus();
                return;
            }

            //if (!_contas.AceitaLancamento)
            //{
            //    new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    ContasTextBox.Focus();
            //    return;
            //}

            NomeContaTextBox.Text = _contas.Classificador + " " + _contas.Nome;
        }

        private void PlanoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void ValorAtualCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void incluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _valoresContas = null;
            }
            catch { }

            EdicaoPanel.Visible = true;
            PlanoTextBox.Focus();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            if (_listaValores != null)
                _listaValores.Clear();

            gridGroupingControl1.DataSource = new List<Lalur.Valores>();
            gridGroupingControl2.DataSource = new List<Acumulado>();
            referenciaMaskedEditBox.Text = string.Empty;
            PeriodoEncerradoCheckBox.Checked = false;
            referenciaMaskedEditBox.Focus();

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new LalurBO().ExcluirApuracao(_apuracao.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu a apuração do Lalur da empresa " + empresaComboBoxAdv.Text + " referência " + referenciaMaskedEditBox.Text;
            _log.Tela = "Contabilidade - Lalur - Apuração";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            limparButton_Click(sender, e);
        }

        private void RecolherCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (RecolherCheckBox.Checked)
            {
                gridGroupingControl1.Table.ExpandAllRecords();
                gridGroupingControl2.Table.ExpandAllRecords();
            }
            else
            {
                gridGroupingControl1.Table.CollapseAllRecords();
                gridGroupingControl2.Table.CollapseAllRecords();
            }

            gridGroupingControl1.Refresh();
            gridGroupingControl2.Refresh();

        }

        private void gridGroupingControl2_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            if (e.Style.TableCellIdentity.DisplayElement.Kind != Syncfusion.Grouping.DisplayElementKind.Record)
                return;

            try
            {
                if (e.TableCellIdentity.Column.MappingName.Contains("Valor"))
                {
                    if (Convert.ToDecimal(e.Style.Text) < 0)
                        e.Style.TextColor = (Publicas._TemaBlack ? Color.DarkOrange : Color.DarkRed);
                }
            }
            catch { }
        }

        private void GerarExcelButton_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Publicas._caminhoAnexosRateioCTB))
                System.IO.Directory.CreateDirectory(Publicas._caminhoAnexosRateioCTB);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;

            string nomeArquivo = "Lalur_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
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

                #region titulo colunas
                xlWorkSheet.Cells[linha, col] = "";
                col++;

                xlWorkSheet.Cells[linha, col] = "Descrição ";
                col++;
                xlWorkSheet.Cells[linha, col] = "Parâmetros";
                col++;
                xlWorkSheet.Cells[linha, col] = "Valor";
                col++;
                #endregion

                foreach (var itemC in _listaValores.OrderBy( o => o.Ordem))
                {
                    col = 1;
                    linha++;

                    #region Cabeçalho da Nota
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Descricao;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Percentual;
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor,2);

                    if (itemC.Contas != null)
                    {
                        foreach (var item in itemC.Contas)
                        {
                            col = 1;
                            linha++;

                            xlWorkSheet.Cells[linha, col] = item.Codigo;
                            col++;
                            xlWorkSheet.Cells[linha, col] = item.NomeConta;
                            col++;
                            col++;
                            xlWorkSheet.Cells[linha, col] = Math.Round(item.ValorReal, 2);
                            col++;
                        }
                    }
                    #endregion

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
                gravarButton_Click(sender, e);

                new Notificacoes.Mensagem("Arquivo gerado com sucesso." + Environment.NewLine +
                    "Salvo na pasta " + Publicas._caminhoAnexosRateioCTB, Publicas.TipoMensagem.Sucesso).ShowDialog();
                
                this.Refresh();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                mensagemSistemaLabel.Text = "";
                this.Refresh();
                //gravarButton_Click(sender, e);
                new Notificacoes.Mensagem("Não foi possível gerar o arquivo." + Environment.NewLine +
                    "Tente em outra maquina." + Environment.NewLine + ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {// exportar excel
            if (!System.IO.Directory.Exists(Publicas._caminhoAnexosRateioCTB))
                System.IO.Directory.CreateDirectory(Publicas._caminhoAnexosRateioCTB);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;

            string nomeArquivo = "Lalur_Acumulado_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" + referenciaMaskedEditBox.ClipText.Substring(2,4);

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

                #region titulo colunas

                xlWorkSheet.Cells[linha, col] = "Descrição ";
                col++;

                for (int i = 1; i <= 12; i++)
                {
                    xlWorkSheet.Cells[linha, col] = i.ToString("00") + "/" + referenciaMaskedEditBox.Text.Substring(3, 4);
                    col++;
                }
                #endregion

                foreach (var itemC in _listaAcumulado.OrderBy(o => o.Ordem))
                {
                    col = 1;
                    linha++;

                    #region Detalhe da Nota
                    xlWorkSheet.Cells[linha, col] = itemC.Descricao;
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor01, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor02, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor03, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor04, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor05, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor06, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor07, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor08, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor09, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor10, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor11, 2);
                    col++;
                    xlWorkSheet.Cells[linha, col] = Math.Round(itemC.Valor12, 2);
                    #endregion

                    // inclui conta contabeis
                    foreach (var item in itemC.Contas)
                    {
                        col = 1;
                        linha++;

                        xlWorkSheet.Cells[linha, col] = "     " + item.Codigo + " - " + item.NomeConta;
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor01, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor02, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor03, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor04, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor05, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor06, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor07, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor08, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor09, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor10, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor11, 2);
                        col++;
                        xlWorkSheet.Cells[linha, col] = Math.Round(item.Valor12, 2);
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
                this.Refresh();
                //gravarButton_Click(sender, e);
                new Notificacoes.Mensagem("Não foi possível gerar o arquivo." + Environment.NewLine +
                    "Tente em outra maquina." + Environment.NewLine + ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
            }
        }
    }

    
}
