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

namespace Suportte.Contabilidade
{
    public partial class ConciliacaoBancaria : Form
    {
        public ConciliacaoBancaria()
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

                    //inicialDateTimePicker.Style = VisualStyle.Office2016Black;
                    //finalDateTimePicker.Style = VisualStyle.Office2016Black;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        List<Classes.ConciliacaoContabil.Bancaria.Resumo> _listaResumo;
        List<Classes.ConciliacaoContabil.Bancaria.Detalhe> _listaDetalhe;

        Classes.Empresa _empresa;
        Classes.RateioBeneficios.PlanoContabil _plano;

        DateTime _dataInicio;
        DateTime _data;
        int referencia;
        int referenciaAnterior;
        int referenciaInicial;

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

        private void excluirButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                limparButton.Focus();
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

        private void ConciliacaoBancaria_Shown(object sender, EventArgs e)
        {
            this.Top = 60;
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


            GridSummaryColumnDescriptor summaryColumnDescriptorG1F = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG1F.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG1F.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG1F.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG1F.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG1F.DataMember = "SaldoInicialBCO";
            summaryColumnDescriptorG1F.Format = "{Sum}";
            summaryColumnDescriptorG1F.Name = "SaldoInicialBCO";
            summaryColumnDescriptorG1F.SummaryType = SummaryType.DoubleAggregate;



            GridSummaryColumnDescriptor summaryColumnDescriptorG1 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG1.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG1.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG1.DataMember = "SaldoFinalBCO";
            summaryColumnDescriptorG1.Format = "{Sum}";
            summaryColumnDescriptorG1.Name = "SaldoFinalBCO";
            summaryColumnDescriptorG1.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG1_1 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG1_1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG1_1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG1_1.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG1_1.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG1_1.DataMember = "SaldoFinalGBCO";
            summaryColumnDescriptorG1_1.Format = "{Sum}";
            summaryColumnDescriptorG1_1.Name = "SaldoFinalGBCO";
            summaryColumnDescriptorG1_1.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG1_2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG1_2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG1_2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG1_2.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG1_2.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG1_2.DataMember = "SaldoFinalCTB";
            summaryColumnDescriptorG1_2.Format = "{Sum}";
            summaryColumnDescriptorG1_2.Name = "SaldoFinalCTB";
            summaryColumnDescriptorG1_2.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorG1_3 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorG1_3.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorG1_3.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorG1_3.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptorG1_3.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptorG1_3.DataMember = "Diferença";
            summaryColumnDescriptorG1_3.Format = "{Sum}";
            summaryColumnDescriptorG1_3.Name = "Diferença";
            summaryColumnDescriptorG1_3.SummaryType = SummaryType.DoubleAggregate;


            _somagrid1 = new GridSummaryRowDescriptor("Sum", "Total",
                   new GridSummaryColumnDescriptor[] { summaryColumnDescriptorG1F, summaryColumnDescriptorG1, summaryColumnDescriptorG1_1, summaryColumnDescriptorG1_2, summaryColumnDescriptorG1_3 });

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
                gridGroupingControl2.TableDescriptor.SummaryRows.Add(_soma);
            }
            catch { }

            gridGroupingControl2.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl2.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl2.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl2.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl2.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";

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
            referenciaInicial = Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4) + "01");

            if (referencia != referenciaInicial)
                referenciaAnterior = Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2)) - 1;
            else
                referenciaAnterior = referencia;

            mensagemSistemaLabel.Text = "Pesquisando, aguarde...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            _listaResumo = new ConciliacaoContabilBO().Listar(_empresa.CodigoEmpresaGlobus, _plano.NumeroPlano, referencia.ToString(), referenciaInicial.ToString()
                                                             , referenciaAnterior.ToString(), _dataInicio, _data
                                                             , consolidarCheckBox.Checked, ConferidosCheckBox.Checked, _empresa.IdEmpresa);
            _listaDetalhe = new ConciliacaoContabilBO().Listar(_empresa.CodigoEmpresaGlobus, _plano.NumeroPlano, _dataInicio, _data, consolidarCheckBox.Checked);


            decimal saldo = 0;
            foreach (var item in _listaResumo)
            {
                saldo = item.SaldoInicialBCO;
                foreach (var itemD in _listaDetalhe.Where(w => w.Banco == item.Banco && w.Agencia == item.Agencia && w.Conta == item.Conta))
                {
                    saldo = saldo + itemD.Valor;
                }
                item.SaldoFinalGBCO = saldo;
            }

            gridGroupingControl1.DataSource = _listaResumo;

            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();
            gridGroupingControl1.Focus();

            gravarButton.Enabled = _listaResumo.Count() > 0 && !Publicas._usuario.AcessaConciliacaoBCOApenasConsulta;
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    
                    gridGroupingControl2.DataSource = _listaDetalhe.Where(w => w.Banco == (int)dr["Banco"] && 
                                                                              w.Agencia == (int)dr["Agencia"] &&
                                                                              w.Conta == (string)dr["Conta"]).ToList();
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
                            gridGroupingControl2.DataSource = _listaDetalhe.Where(w => w.Banco == (int)dr["Banco"] &&
                                                                                 w.Agencia == (int)dr["Agencia"] &&
                                                                                 w.Conta == (string)dr["Conta"]).ToList();
                        }
                    }
                }
            }
            catch { }
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            referenciaMaskedEditBox.Text = string.Empty;
            _listaResumo.Clear();
            _listaDetalhe.Clear();
            gridGroupingControl1.DataSource = new List<Classes.ConciliacaoContabil.Bancaria.Resumo>();
            gridGroupingControl2.DataSource = new List<Classes.ConciliacaoContabil.Bancaria.Detalhe>();

            gravarButton.Enabled = false;
            referenciaMaskedEditBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            _listaResumo.ForEach(u => {
                u.IdEmpresa = _empresa.IdEmpresa;
                u.Referencia = referencia;
            });

            if (_listaResumo.Where(w => w.Conferido || w.Existe).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma conta selecionada para confirmar.", Publicas.TipoMensagem.Erro).ShowDialog();
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
                    _descricaoN = _descricaoN + item.Banco + "_" + item.Agencia + "_" + item.Conta + " deixou de ser concialidado";
                else
                    _descricaoC = _descricaoC + item.Banco + "_" + item.Agencia + "_" + item.Conta;
            } 

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Efetuou a conciliaçáo bancária da empresa " + empresaComboBoxAdv.Text +
                " referencia " + referenciaMaskedEditBox.Text + " para os bancos_agencias_contas " + _descricaoC;

            _log.Tela = "Contabilidade - Conciliações - Bancária";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Cancelou a conciliaçáo bancária da empresa " + empresaComboBoxAdv.Text +
                " referencia " + referenciaMaskedEditBox.Text + " para os bancos_agencias_contas " + _descricaoN;

            _log.Tela = "Contabilidade - Conciliações - Bancária";

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
    }
}
