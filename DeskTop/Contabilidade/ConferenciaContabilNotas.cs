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
    public partial class ConferenciaContabilNotas : Form
    {
        public ConferenciaContabilNotas()
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

                NaoConferidasCheckBox.ForeColor = empresaComboBoxAdv.ForeColor;
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.RateioBeneficios.PlanoContabil _plano;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.ConferenciaNotasPelaContabilidade.Conferencia> _listaNotas;
        List<Classes.ConferenciaNotasPelaContabilidade.ItensConferencia> _listaItensNota;
        DateTime _dataInicio;
        DateTime _dataFim;
        string _referencia;
        GridCurrentCell _colunaCorrente;
        decimal codigoNotaFiscal;
        decimal codDoctoESF;
        decimal codISSInt;

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

        private void ConferenciaContabilNotas_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);

            int espacoEntreBotoes = limparButton.Left - (gravarButton.Left + gravarButton.Width);

            #region coloca os botões centralizados
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { gravarButton, limparButton, EnviarEmailButton, GerarExcelButton };
            _botoes = Publicas.CentralizarBotoes(_botoes, this.Width, limparButton.Left - (gravarButton.Left + gravarButton.Width));

            for (int i = 0; i < _botoes.Count(); i++)
            {
                if (i == 0)
                    gravarButton.Left = _botoes[i].Left;
                if (i == 1)
                    limparButton.Left = _botoes[i].Left;
                if (i == 2)
                    EnviarEmailButton.Left = _botoes[i].Left;
                if (i == 3)
                    GerarExcelButton.Left = _botoes[i].Left;
            }
            #endregion

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(gridGroupingControl1);
            filter.WireGrid(gridGroupingControl2);

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

            #region notas
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

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 30;

            #endregion

            #region Itens
            gridGroupingControl2.DataSource = new List<ItensNotasFiscaisServico>();
            gridGroupingControl2.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl2.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl2.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl2.TableControl.CellToolTip.Active = true;
            gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl2.RecordNavigationBar.Label = "Itens";

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
            // para permitir editar dados.
            gridGroupingControl2.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            gridGroupingControl2.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            gridGroupingControl2.TableControl.CellToolTip.InitialDelay = 100;
            gridGroupingControl2.TableControl.CellToolTip.AutoPopDelay = 5000;
            #endregion

        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
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

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                NaoConferidasCheckBox.Focus();
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
                PlanoTextBox.Focus();
            }
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

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

            _referencia = referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2);


            gridGroupingControl1.DataSource = new List<Classes.ConferenciaNotasPelaContabilidade.Conferencia>();
            gridGroupingControl2.DataSource = new List<Classes.ConferenciaNotasPelaContabilidade.ItensConferencia>();


            mensagemSistemaLabel.Text = "Aguarde, Pesquisando ...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            _listaNotas = new ConferenciaNotasPelaContabilidadeBO().Listar(_empresa.IdEmpresa, _referencia.ToString());
            _listaItensNota = new ConferenciaNotasPelaContabilidadeBO().ListarItens(_empresa.IdEmpresa, _referencia.ToString(), _plano.NumeroPlano, NaoConferidasCheckBox.Checked);

            // Para mostrar apenas as Notas não conferidas
            if (NaoConferidasCheckBox.Checked)
            {
                foreach (var item in _listaItensNota.GroupBy(g => new { g.CodIntNF, g.CodDoctoESF, g.CodISSInt }))
                {
                    foreach (var itemL in _listaNotas.Where(w => w.CodIntNF == item.Key.CodIntNF && w.CodDoctoESF == item.Key.CodDoctoESF && w.CodISSInt == item.Key.CodISSInt))
                    {
                        itemL.Conferida = false;
                    }
                }

                _listaNotas = _listaNotas.Where(w => !w.Conferida).ToList();
            }

            gridGroupingControl1.DataSource = _listaNotas;

            gravarButton.Enabled = _listaNotas.Count() != 0;
            
            // habilita enviar Email quando estiver validado e hover problemas nos campos validos
            EnviarEmailButton.Enabled = _listaItensNota.Where(w => w.Validado &&
                                                                 (!w.Valido1 || !w.Valido2 || !w.Valido3 || !w.Valido4)).Count() != 0;

            GerarExcelButton.Enabled = _listaNotas.Count() != 0; 

            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();

        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    codigoNotaFiscal = (decimal)dr["CodIntNF"];
                    codDoctoESF = (decimal)dr["CodDoctoESF"];
                    codISSInt = (decimal)dr["CodISSInt"];
                    
                    gridGroupingControl2.DataSource = _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal && 
                                                                                 w.CodDoctoESF == codDoctoESF &&
                                                                                 w.CodISSInt == codISSInt).ToList();

                    if ((string)dr["ObservacaoCPG"] == null || (string)dr["ObservacaoCPG"] == "")
                        gridGroupingControl1.TableDescriptor.Columns["ObservacaoCPG"].ReadOnly = false;
                    else
                        gridGroupingControl1.TableDescriptor.Columns["ObservacaoCPG"].ReadOnly = true;
                }
            }
            _colunaCorrente = gridGroupingControl2.TableControl.CurrentCell;
        }

        private void gridGroupingControl2_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            if (e.Style.TableCellIdentity.Column == null)
                return;

            if (e.Style.TableCellIdentity.DisplayElement.Kind != Syncfusion.Grouping.DisplayElementKind.ColumnHeader &&
                e.Style.TableCellIdentity.DisplayElement.Kind != Syncfusion.Grouping.DisplayElementKind.Record)
                return;

            try
            {
                if (e.Style.TableCellIdentity.DisplayElement.Kind == Syncfusion.Grouping.DisplayElementKind.ColumnHeader)
                {
                    if (e.Style.TableCellIdentity.Column.Name.Contains("Valido1"))
                        e.Style.CellTipText = "Tipo de Despesa do material" + Environment.NewLine + "tem que ser igual ao informado na Nota fiscal";
                    if (e.Style.TableCellIdentity.Column.Name.Contains("Valido2"))
                        e.Style.CellTipText = "Tipo de Despesa informado na Nota fiscal" + Environment.NewLine + "deve estar cadastrado no parâmetro para o grupo de despesa";
                    if (e.Style.TableCellIdentity.Column.Name.Contains("Valido3"))
                        e.Style.CellTipText = "Conta contábil do material" + Environment.NewLine + "tem que ser igual a infomada na Nota fiscal";
                    if (e.Style.TableCellIdentity.Column.Name.Contains("Valido4"))
                        e.Style.CellTipText = "Conta contábil da Nota Fiscal" + Environment.NewLine + "tem que ser igual a do lançamento Contábil";
                }

                if (e.Style.TableCellIdentity.DisplayElement.Kind == Syncfusion.Grouping.DisplayElementKind.Record)
                {
                    try
                    {
                        Record dr;
                        GridRecordRow rec = this.gridGroupingControl2.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                        if (rec != null)
                        {
                            dr = rec.GetRecord() as Record;

                            if (dr != null && (!(bool)dr["Valido1"] || !(bool)dr["Valido2"] || !(bool)dr["Valido3"] || !(bool)dr["Valido4"]))
                                e.Style.TextColor = Color.DarkOrange;

                            if ((bool)dr["Conferido"])
                            {
                                if (e.Style.TableCellIdentity.Column.Name.Contains("Conferido"))
                                {
                                    if ((string)dr["UsuarioConferido"] != "")
                                        e.Style.CellTipText = "Conferido por " + (string)dr["UsuarioConferido"] + " em " + ((DateTime)dr["DataConferido"]).ToShortDateString();
                                    else
                                        e.Style.CellTipText = "Conferido diretamente pelo módulo Contabilidade do ERP Globus.";
                                }
                            }

                            if ((bool)dr["Validado"])
                            {
                                if (e.Style.TableCellIdentity.Column.Name.Contains("Validado"))
                                    e.Style.CellTipText = "Validado por " + (string)dr["UsuarioValidador"] + " em " +
                                        ((DateTime)dr["DataValidado"]).ToShortDateString() + Environment.NewLine +
                                        ((bool)dr["ValidadoOriginal"] ? "Validado nesta empresa/referência" :
                                        ((string)dr["CodigoGlobus"] == _empresa.CodigoEmpresaGlobus ?
                                          ((int)dr["ReferenciaValidado"] == (int)dr["Referencia"] ? "Validado nesta empresa/referência" :
                                             "Validado nesta empresa/referência " + (int)dr["ReferenciaValidado"]) :
                                             "Validado na empresa " + (string)dr["CodigoGlobus"] + "/referência " + (int)dr["ReferenciaValidado"]));
                            }
                        }
                    }
                    catch { }


                }
            }
            catch { }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            mensagemSistemaLabel.Text = "Aguarde, Gravando ...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            foreach (var item in _listaItensNota.Where(w => w.Conferido && w.IdUsuarioConferido == 0))
            {
                item.IdUsuarioConferido = Publicas._usuario.Id;
            }

            foreach (var item in _listaItensNota.Where(w => w.Validado && w.IdUsuarioValidador == 0))
            {
                item.IdUsuarioValidador = Publicas._usuario.Id;
            }

            if (!new ConferenciaNotasPelaContabilidadeBO().Gravar(_listaItensNota, _listaNotas))
            {
                new Notificacoes.Mensagem("Problemas durante a integração." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                referenciaMaskedEditBox.Focus();
                mensagemSistemaLabel.Text = "";
                this.Cursor = Cursors.WaitCursor;
                this.Refresh();
                return;
            }

            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();

            gridGroupingControl1.DataSource = new List<ConferenciaNotasPelaContabilidade.Conferencia>();
            gridGroupingControl2.DataSource = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();

        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            _listaNotas = new List<ConferenciaNotasPelaContabilidade.Conferencia>();
            referenciaMaskedEditBox.Text = string.Empty;
            referenciaMaskedEditBox.Focus();
            gridGroupingControl1.DataSource = new List<ConferenciaNotasPelaContabilidade.Conferencia>();
            gridGroupingControl2.DataSource = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();
            EnviarEmailButton.Enabled = false;
            gravarButton.Enabled = false;
        }

        private void PlanoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                NaoConferidasCheckBox.Focus();
            }
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

        private void gridGroupingControl1_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[_rowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    codigoNotaFiscal = (decimal)dr["CodIntNF"];
                    codDoctoESF = (decimal)dr["CodDoctoESF"];
                    codISSInt = (decimal)dr["CodISSInt"];

                    gridGroupingControl2.DataSource = _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                                                 w.CodDoctoESF == codDoctoESF && 
                                                                                 w.CodISSInt == codISSInt).ToList();

                    if ((string)dr["ObservacaoCPG"] == null || (string)dr["ObservacaoCPG"] == "")
                        gridGroupingControl1.TableDescriptor.Columns["ObservacaoCPG"].ReadOnly = false;
                    else
                        gridGroupingControl1.TableDescriptor.Columns["ObservacaoCPG"].ReadOnly = true;
                }
            }
            _colunaCorrente = gridGroupingControl2.TableControl.CurrentCell;
        }

        private void marcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // conferir
            foreach (var item in _listaItensNota.Where(w => w.ContaContabilCTB != "" && w.CodIntNF == codigoNotaFiscal &&
                                                                                 w.CodDoctoESF == codDoctoESF &&
                                                                                 w.CodISSInt == codISSInt &&
                                                                                 w.Validado))
            {
                item.Conferido = true;
            }
            gridGroupingControl2.DataSource = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();
            gridGroupingControl2.DataSource = _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                                         w.CodDoctoESF == codDoctoESF &&
                                                                         w.CodISSInt == codISSInt).ToList();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // conferir
            foreach (var item in _listaItensNota.Where(w => w.ContaContabilCTB != "" && w.CodIntNF == codigoNotaFiscal &&
                                                                                 w.CodDoctoESF == codDoctoESF &&
                                                                                 w.CodISSInt == codISSInt))
            {
                item.Conferido = false;
            }
            gridGroupingControl2.DataSource = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();
            gridGroupingControl2.DataSource = _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                                          w.CodDoctoESF == codDoctoESF &&
                                                                          w.CodISSInt == codISSInt).ToList();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // validado
            foreach (var item in _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                            w.CodDoctoESF == codDoctoESF &&
                                                            w.CodISSInt == codISSInt &&
                                                            w.Valido1 && w.Valido2 && w.Valido3 && w.Valido4))
            {
                item.Validado = true;
            }
            gridGroupingControl2.DataSource = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();
            gridGroupingControl2.DataSource = _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                                          w.CodDoctoESF == codDoctoESF &&
                                                                          w.CodISSInt == codISSInt).ToList();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            // validado
            foreach (var item in _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                                          w.CodDoctoESF == codDoctoESF &&
                                                                          w.CodISSInt == codISSInt))
            {
                item.Validado = false;
            }
            gridGroupingControl2.DataSource = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();
            gridGroupingControl2.DataSource = _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                                         w.CodDoctoESF == codDoctoESF &&
                                                                          w.CodISSInt == codISSInt).ToList();

        }

        private void gridGroupingControl2_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl2.Table.DisplayElements[_rowIndex] as GridRecordRow;

                string nomeColuna = gridGroupingControl2.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
                bool marcado = false;

                if (rec != null )
                {
                    Record dr = rec.GetRecord() as Record;
                    if (nomeColuna == "Conferido")
                    {
                        if (dr != null)
                        {
                            marcado = (bool)dr[nomeColuna];

                            if (marcado && !(bool)dr["Validado"])
                            {
                                // cancela a marcação
                                new Notificacoes.Mensagem("Material com problema nos validadores, não é permitido Conferir.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                gridGroupingControl2.DataSource = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();

                                gridGroupingControl2.DataSource = _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                                                             w.CodDoctoESF == codDoctoESF &&
                                                                                             w.CodISSInt == codISSInt).ToList();
                                gridGroupingControl2.Refresh();
                                return;
                            }

                            foreach (var item in _listaItensNota.Where(w => w.CodIntNF == (decimal)dr["CodIntNF"] &&
                                                                            w.CodDoctoESF == (decimal)dr["CodDoctoESF"] &&
                                                                            w.CodISSInt == (decimal)dr["CodISSInt"] &&
                                                                            w.CodMaterial == (decimal)dr["CodMaterial"]))
                            {
                                if (item.ContaContabilCTB == "" && marcado)
                                {
                                    item.Conferido = false;
                                    new Notificacoes.Mensagem("Material sem conta contábil encontrada, não será possível conferir automaticamente esse item.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                    gridGroupingControl2.DataSource = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();
                                    gridGroupingControl2.DataSource = _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                                                                         w.CodDoctoESF == codDoctoESF &&
                                                                                                         w.CodISSInt == codISSInt).ToList();

                                    return;
                                }
                                item.Conferido = marcado;
                            }

                            // Marcará como conferidos todos os materias que possuir a mesma conta contábil (no lançamento contabil)
                            foreach (var item in _listaItensNota.Where(w => w.CodIntNF == (decimal)dr["CodIntNF"] &&
                                                                            w.CodDoctoESF == (decimal)dr["CodDoctoESF"] &&
                                                                            w.CodISSInt == (decimal)dr["CodISSInt"] &&
                                                                            w.ContaContabilCTB == (string)dr["ContaContabilCTB"]))
                            {
                                item.Conferido = marcado;
                            }
                        }
                    }
                    else
                    {
                        if (dr != null)
                        {
                            marcado = (bool)dr[nomeColuna];

                            foreach (var item in _listaItensNota.Where(w => w.CodIntNF == (decimal)dr["CodIntNF"] &&
                                                                            w.CodDoctoESF == (decimal)dr["CodDoctoESF"] &&
                                                                            w.CodISSInt == (decimal)dr["CodISSInt"] &&
                                                                            w.CodMaterial == (decimal)dr["CodMaterial"]))
                            {
                                if ((!item.Valido1 || !item.Valido2 || !item.Valido3 || !item.Valido4) && marcado)
                                {
                                    item.Validado = false;
                                    new Notificacoes.Mensagem("Material com problema nos validadores.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                    gridGroupingControl2.DataSource = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();
                                    gridGroupingControl2.DataSource = _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                                                                 w.CodDoctoESF == codDoctoESF &&
                                                                                                 w.CodISSInt == codISSInt        ).ToList();

                                    return;
                                }
                                item.Validado = marcado;
                            }
                        }
                    }
                }
            }
            catch { }

            gridGroupingControl2.DataSource = new List<ConferenciaNotasPelaContabilidade.ItensConferencia>();

            gridGroupingControl2.DataSource = _listaItensNota.Where(w => w.CodIntNF == codigoNotaFiscal &&
                                                                         w.CodDoctoESF == codDoctoESF &&
                                                                         w.CodISSInt == codISSInt).ToList();
            gridGroupingControl2.Refresh();
        }

        private void NaoConferidasCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PlanoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            if (e.Style.TableCellIdentity.Column == null)
                return;

            if (e.Style.TableCellIdentity.DisplayElement.Kind != Syncfusion.Grouping.DisplayElementKind.ColumnHeader &&
                e.Style.TableCellIdentity.DisplayElement.Kind != Syncfusion.Grouping.DisplayElementKind.Record)
                return;

            try
            {
                if (NaoConferidasCheckBox.Checked)
                    return;

                if (e.Style.TableCellIdentity.DisplayElement.Kind == Syncfusion.Grouping.DisplayElementKind.Record)
                {
                    try
                    {
                        Record dr;
                        GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                        if (rec != null)
                        {
                            dr = rec.GetRecord() as Record;

                            if (_listaItensNota.Where(w => w.CodIntNF == (decimal)dr["CodIntNF"] &&
                                                           w.CodDoctoESF == (decimal)dr["CodDoctoESF"] &&
                                                           w.CodISSInt == (decimal)dr["CodISSInt"] &&
                                                           !w.Conferido).Count() != 0)
                                e.Style.TextColor = Color.DarkOrange;
                        }
                    }
                    catch { }


                }
            }
            catch { }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void EnviarEmailButton_Click(object sender, EventArgs e)
        {
            if (_listaNotas.Count() == 0)
                return;

            string dadosNota = "";
            string materiais = "";
            string textohtml = "";
            bool temItens = false;

            mensagemSistemaLabel.Text = "Enviando E-mail, aguarde...";
            Refresh();

            foreach (var item in _listaNotas.OrderBy(o => o.NumeroNF))
            {
                materiais = "";
                dadosNota = "<tr> " +
                                    "<td style='width: 10%; font-size: 13px; border-top: 1px solid #cccccc' align='Left'>" + item.NumeroNF + "&nbsp;</td> " +
                                    "<td style='width: 15%; font-size: 13px; border-top: 1px solid #cccccc' align='Left'>" + item.Fornecedor + "</td>" +
                                    "<td style='width: 75%; font-size: 13px; border-top: 1px solid #cccccc' align='Left'>" + item.CodTipoDocto +
                                    "&nbsp;" + item.Entrada.ToShortDateString() +
                                    "&nbsp;" + item.Origem +
                                    "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Documento + "&nbsp;</td> " +
                                "</tr>" +
                                "<tr>" +
                                    "<td style='width: 10%;font-size: 13px;' align='Left'><font color='4169e1'>Grupo</td>" +
                                    "<td style='width: 15%;font-size: 13px;' align='Left'><font color='4169e1'>Material &nbsp;</td>" +
                                    "<td style='width: 75%;font-size: 13px;' align='Left'><font color='4169e1'>Descrição do Problema</td>" +
                                 "</tr>";
                temItens = false;
                foreach (var itemI in _listaItensNota.Where(w => w.CodIntNF == item.CodIntNF && w.CodDoctoESF == item.CodDoctoESF &&
                                                                  w.CodISSInt == item.CodISSInt &&
                                                              w.Validado && (!w.Valido1 || !w.Valido2 || !w.Valido3 || !w.Valido4)))
                {
                    temItens = true;

                    materiais = materiais +
                        "<tr> " +
                            "<td style='width: 10%; font-size: 12px;' align='Left'> " + itemI.CodGrupo + "</td>" +
                            "<td style='width: 15%; font-size: 12px;' align='Left'>" + itemI.Material + "&nbsp;</td> " +
                            "<td style='width: 75%; font-size: 12px;' align='Left'> " +
                                (itemI.Valido1 ? "" : "Tipo de despesas do Material difere do informado no Item da Nota; ") +
                                (itemI.Valido2 ? "" : "Tipo de despesas do Item da Nota difere do informado na parametrização da conferência; ") +
                                (itemI.Valido3 ? "" : "Conta Contábil do Material difere do informado no Item da Nota; ") +
                                (itemI.Valido4 ? "" : "Conta Contábil do Item difere do informado no lançamento contábil.") + "</td>" +
                        "</tr>";
                }

                if (temItens)
                {
                    textohtml = textohtml + dadosNota + materiais;
                }
            }

            if (textohtml != "")
            {

                #region Envia Email

                string[] _dadosEmail = new string[50];
                _dadosEmail[0] = empresaComboBoxAdv.Text;
                _dadosEmail[1] = referenciaMaskedEditBox.Text;
                _dadosEmail[2] = textohtml;

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

                foreach (var item in _listaUsuarios.Where(w => w.AcessaContabilidade && w.AcessaCTBNotasFicais))
                {
                    emailDestino = emailDestino + item.Email + "; ";
                }

                //emailDestino = "mdmunoz@supportse.com.br";
                
                Publicas.mensagemDeErro = "";

                Classes.Publicas.EnviarEmailConferenciaCTB(_dadosEmail, Publicas._usuario.Email, emailDestino, emailCopia, "Conferência de Notas pela Contabilidade");

                if (Publicas.mensagemDeErro != "")
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Alerta).ShowDialog();
                else
                    new Notificacoes.Mensagem("E-mail enviado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();

                #endregion
            }

            mensagemSistemaLabel.Text = "";
            Refresh();
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

            string nomeArquivo = "ConferenciaNotasCTB_" + _empresa.CodigoEmpresaGlobus.Replace("/", "_") + "_" + referenciaMaskedEditBox.ClipText;
            
            xlApp = new Excel.Application();
            int item = 1;

            try
            {

                xlApp.DisplayAlerts = false;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


                int linha = 1;
                int col = 1;

                xlWorkSheet.Cells[linha, col] = "Empresa: ";
                col++;
                xlWorkSheet.Cells[linha, col] = empresaComboBoxAdv.Text;
                col++;
                
                foreach (var itemR in _listaNotas.OrderBy(o => o.NumeroNF))
                {
                    #region Cabeçalho
                    col = 1;
                    linha++;
                    linha++;

                    xlWorkSheet.Cells[linha, col] = "Numero NF ";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Entrada";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Tipo Docto";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Fornecedor";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Observação CPG";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Documento CTB";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Origem";
                    #endregion

                    linha++;
                    col = 1;
                    xlWorkSheet.Cells[linha, col] = itemR.NumeroNF;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemR.Entrada.ToShortDateString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemR.CodTipoDocto;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemR.Fornecedor;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemR.ObservacaoCPG;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemR.Documento;
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemR.Origem;

                    #region titulo colunas

                    linha++;
                    linha++;

                    col = 2;
                    xlWorkSheet.Cells[linha, col] = "Conferido";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Validado";
                    col++;                    

                    xlWorkSheet.Cells[linha, col] = "Conferido ESF";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Material";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Valor";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Grupo de Despesa";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Tp Despesa do Material";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Tp Despesa da Nota";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Valido 1";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Valido 2";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Conta CTB do Material";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Conta CTB da Nota";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Conta CTB Contábil";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Valido 3";
                    col++;
                    xlWorkSheet.Cells[linha, col] = "Valido 4";
                    col++;



                    #endregion

                    foreach (var itemC in _listaItensNota.Where(w => w.CodIntNF == itemR.CodIntNF && w.CodDoctoESF == itemR.CodDoctoESF && w.CodISSInt == itemR.CodISSInt))
                    {
                        linha++;

                        col = 2;
                        xlWorkSheet.Cells[linha, col] = (itemC.Conferido ? "SIM" : "NÃO");
                        col++;
                        xlWorkSheet.Cells[linha, col] = (itemC.Validado ? "SIM" : "NÃO");
                        col++;
                        xlWorkSheet.Cells[linha, col] = (itemC.ConferidoESF ? "SIM" : "NÃO");
                        col++;
                        xlWorkSheet.Cells[linha, col] = itemC.Material;
                        col++;
                        xlWorkSheet.Cells[linha, col] = itemC.Valor;
                        col++;
                        xlWorkSheet.Cells[linha, col] = itemC.GrupoDespesa;
                        col++;
                        xlWorkSheet.Cells[linha, col] = itemC.TipoDespesaItem;
                        col++;
                        xlWorkSheet.Cells[linha, col] = itemC.TipoDespesaNota;
                        col++;
                        xlWorkSheet.Cells[linha, col] = (itemC.Valido1 ? "SIM" : "NÃO");
                        col++;
                        xlWorkSheet.Cells[linha, col] = (itemC.Valido2 ? "SIM" : "NÃO");
                        col++;

                        xlWorkSheet.Cells[linha, col] = itemC.ContaContabilItem;
                        col++;
                        xlWorkSheet.Cells[linha, col] = itemC.ContaContabilNota;
                        col++;
                        xlWorkSheet.Cells[linha, col] = itemC.NomeContaContabilCTB;
                        col++;

                        xlWorkSheet.Cells[linha, col] = (itemC.Valido3 ? "SIM" : "NÃO");
                        col++;
                        xlWorkSheet.Cells[linha, col] = (itemC.Valido4 ? "SIM" : "NÃO");
                        col++;


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

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl2.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;
            gridGroupingControl2.TableDescriptor.Columns["Conferido"].ReadOnly = false;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (!(bool)dr["Validado"])
                        gridGroupingControl2.TableDescriptor.Columns["Conferido"].ReadOnly = true;

                }
            }
            _colunaCorrente = gridGroupingControl2.TableControl.CurrentCell;
        }

        private void gridGroupingControl2_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

            GridRecordRow rec = this.gridGroupingControl2.Table.DisplayElements[_rowIndex] as GridRecordRow;
            gridGroupingControl2.TableDescriptor.Columns["Conferido"].ReadOnly = false;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (!(bool)dr["Validado"])
                        gridGroupingControl2.TableDescriptor.Columns["Conferido"].ReadOnly = true;
                }
            }
            _colunaCorrente = gridGroupingControl2.TableControl.CurrentCell;
        }
    }
}
