using Classes;
using Negocio;
using Suportte.Notificacoes;
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
    public partial class IntegraNotaServico : Form
    {
        public IntegraNotaServico()
        {
            InitializeComponent();

            inicialDateTimePicker.BorderColor = Publicas._bordaSaida;
            inicialDateTimePicker.BackColor = tipoDocumentoTextBox.BackColor;
            inicialDateTimePicker.Value = DateTime.Now;
            finalDateTimePicker.BorderColor = Publicas._bordaSaida;
            finalDateTimePicker.BackColor = tipoDocumentoTextBox.BackColor;
            finalDateTimePicker.Value = DateTime.Now;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        #region Atributos
        Classes.Empresa _empresa;
        Classes.TipoDocumentoGlobus _tipoDocumento;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.NotasFiscaisServico> _listaDeNotasParaIntegrar;
        List<Classes.ItensNotasFiscaisServico> _listaItensParaIntegrar;

        List<Classes.NotasFiscaisServico> _listaDeNotasParaRevogar;
        List<Classes.ItensNotasFiscaisServico> _listaItensParaRevogar;
        #endregion

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

        private void IntegraNotaServico_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);

            int espacoEntreBotoes = limparButton.Left - (gravarButton.Left + gravarButton.Width);

            #region coloca os botões centralizados
            gravarButton.Left = (botoesPanel.Width - (gravarButton.Width + limparButton.Width + espacoEntreBotoes)) / 2;
            limparButton.Left = ((botoesPanel.Width - (gravarButton.Width + limparButton.Width + espacoEntreBotoes)) / 2) + gravarButton.Width + espacoEntreBotoes;
            #endregion

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            #region Define layout Grid
            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(notasGridGroupingControl);
            filter.WireGrid(itensGridGroupingControl);

            #region Integrar
            #region Notas
            notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            notasGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            notasGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            notasGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            notasGridGroupingControl.TableControl.CellToolTip.Active = true;
            notasGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            notasGridGroupingControl.RecordNavigationBar.Label = "Notas";

            for (int i = 0; i < notasGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0)
                    notasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                else
                    notasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                notasGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                notasGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                notasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                notasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                notasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

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

            notasGridGroupingControl.SetMetroStyle(metroColor);

            // para permitir editar dados.
            this.notasGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.notasGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.notasGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.notasGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.notasGridGroupingControl.Table.DefaultRecordRowHeight = 30;
            notasGridGroupingControl.Refresh();

            #endregion

            #region Itens
            itensGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();

            itensGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            itensGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            itensGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            itensGridGroupingControl.TableControl.CellToolTip.Active = true;
            itensGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            notasGridGroupingControl.RecordNavigationBar.Label = "Itens";

            for (int i = 0; i < itensGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                itensGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                itensGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                itensGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                itensGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                itensGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                itensGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            itensGridGroupingControl.SetMetroStyle(metroColor);

            // para permitir editar dados.
            this.itensGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.itensGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.itensGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.itensGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            

            itensGridGroupingControl.Refresh();

            #endregion
            #endregion

            #region revogar
            #region Notas
            notasRevogarGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            notasRevogarGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            notasRevogarGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            notasRevogarGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            notasRevogarGridGroupingControl.TableControl.CellToolTip.Active = true;
            notasRevogarGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            notasRevogarGridGroupingControl.RecordNavigationBar.Label = "Notas";

            for (int i = 0; i < notasRevogarGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0)
                    notasRevogarGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                else
                    notasRevogarGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                notasRevogarGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                notasRevogarGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                notasRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                notasRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                notasRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            notasRevogarGridGroupingControl.SetMetroStyle(metroColor);

            // para permitir editar dados.
            this.notasRevogarGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.notasRevogarGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.notasRevogarGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.notasRevogarGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.notasRevogarGridGroupingControl.Table.DefaultRecordRowHeight = 35;
            notasRevogarGridGroupingControl.Refresh();

            #endregion

            #region Itens
            itensRevogarGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();

            itensRevogarGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            itensRevogarGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            itensRevogarGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            itensRevogarGridGroupingControl.TableControl.CellToolTip.Active = true;
            itensRevogarGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            itensRevogarGridGroupingControl.RecordNavigationBar.Label = "Itens";

            for (int i = 0; i < itensGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                itensRevogarGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            itensRevogarGridGroupingControl.SetMetroStyle(metroColor);

            // para permitir editar dados.
            this.itensRevogarGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.itensRevogarGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.itensRevogarGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.itensRevogarGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            itensRevogarGridGroupingControl.Refresh();

            #endregion
            #endregion

            #endregion
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            tipoDocumentoTextBox.BorderColor = Publicas._bordaEntrada;
            pesquisaTipoButton.Enabled = string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim());
        }

        private void finalDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void pesquisarButton_Enter(object sender, EventArgs e)
        {
            pesquisarButton.BackColor = Publicas._botaoFocado;
            pesquisarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoDocumentoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void usuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inicialDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
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
                tipoDocumentoTextBox.Focus();
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

        private void pesquisarButton_Validating(object sender, CancelEventArgs e)
        {
            pesquisarButton.BackColor = tipoDocumentoTextBox.BackColor;
            pesquisarButton.ForeColor = Publicas._fonteBotao;
        }

        private void pesquisarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                notasGridGroupingControl.Focus();
            }
        }

        private void pesquisarButton_Click(object sender, EventArgs e)
        {
            itensGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();
            itensRevogarGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();

            if (string.IsNullOrEmpty(empresaComboBoxAdv.Text))
            {
                new Notificacoes.Mensagem("Selecione a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o Tipo do documento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoDocumentoTextBox.Focus();
                return;
            }

            mensagemSistemaLabel.Text = "Pesquisando ...";
            this.Refresh();
            notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();
            notasRevogarGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            _listaDeNotasParaIntegrar = new NotaFiscalServicoBO().ListarNotaFiscais(tipoDocumentoTextBox.Text, _empresa.CodigoEmpresaGlobus, "E", inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date, false);
            _listaItensParaIntegrar = new NotaFiscalServicoBO().ListarItensNotaFiscais(tipoDocumentoTextBox.Text, _empresa.CodigoEmpresaGlobus, "E", inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date, false);

            _listaDeNotasParaRevogar = new NotaFiscalServicoBO().ListarNotaFiscaisIntegradas(tipoDocumentoTextBox.Text, _empresa.CodigoEmpresaGlobus, "E", inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date, false);
            _listaItensParaRevogar = new NotaFiscalServicoBO().ListarItensNotaFiscaisIntegrados(tipoDocumentoTextBox.Text, _empresa.CodigoEmpresaGlobus, "E", inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date, false);

            notasGridGroupingControl.DataSource = _listaDeNotasParaIntegrar;
            notasRevogarGridGroupingControl.DataSource = _listaDeNotasParaRevogar;

            tabControl.SelectedTab = IntegrarTabPage ;


            mensagemSistemaLabel.Text = "";

            if (_listaDeNotasParaIntegrar.Count() == 0 && _listaDeNotasParaRevogar.Count() != 0)
            {
                new Notificacoes.Mensagem("Nenhuma Nota Fiscal encontrada para integrar nesse período.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tabControl.SelectedTab = IntegradasTabPage;
                return;
            }

            if (_listaDeNotasParaIntegrar.Count() == 0 && _listaDeNotasParaRevogar.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma Nota Fiscal encontrada integrada e revogar nesse período.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }
            this.Refresh();
            gravarButton.Enabled = _listaDeNotasParaIntegrar.Count() != 0;
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }
        }

        private void tipoDocumentoTextBox_Validating(object sender, CancelEventArgs e)
        {
            tipoDocumentoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                
                new Pesquisas.TipoDocumentoGlobus().ShowDialog();

                tipoDocumentoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text) || tipoDocumentoTextBox.Text == "0")
                {
                    tipoDocumentoTextBox.Text = string.Empty;
                    tipoDocumentoTextBox.Focus();
                    return;
                }
            }

            _tipoDocumento = new TipoDocumentoGlobusBO().Consultar(_empresa.CodigoEmpresaGlobus, tipoDocumentoTextBox.Text);

            if (!_tipoDocumento.Existe)
            {
                new Notificacoes.Mensagem("Tipo do documento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoDocumentoTextBox.Focus();
                return;
            }

            if (!_tipoDocumento.IntegraComLivroDeISS)
            {
                new Notificacoes.Mensagem("Tipo do documento não integra com o Livro de ISS.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoDocumentoTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _tipoDocumento.Descricao;
        }

        private void pesquisaTipoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

                new Pesquisas.TipoDocumentoGlobus().ShowDialog();

                tipoDocumentoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text) || tipoDocumentoTextBox.Text == "0")
                {
                    tipoDocumentoTextBox.Text = string.Empty;
                    tipoDocumentoTextBox.Focus();
                    return;
                }

                tipoDocumentoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void notasGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.notasGridGroupingControl.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    int codigoNotaFiscal = (int)dr["CodigoInternoNotaFiscal"];

                    itensGridGroupingControl.DataSource = _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();
                }
            }
        }

        private void notasGridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.notasGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        int codigoNotaFiscal = (int)dr["CodigoInternoNotaFiscal"];

                        itensGridGroupingControl.DataSource = _listaItensParaIntegrar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();
                    }
                }
            }
            catch { }
        }

        private void inicialDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void finalDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (finalDateTimePicker.Value.Date < inicialDateTimePicker.Value.Date)
            {
                new Notificacoes.Mensagem("Data final não pode ser menor que a data inicial.", Publicas.TipoMensagem.Alerta).ShowDialog();
                inicialDateTimePicker.Focus();
                return;
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaDeNotasParaIntegrar.Where(w => w.Marcado).Count() == 0 && _listaDeNotasParaRevogar.Where(w => w.Marcado).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma Nota Fiscal selecionada para integrar ou revogar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                notasGridGroupingControl.Focus();
                return;
            }

            if (!new NotaFiscalServicoBO().Integrar(tipoDocumentoTextBox.Text, _listaDeNotasParaIntegrar.Where(w => w.Marcado).ToList(), _listaItensParaIntegrar))
            {
                new Notificacoes.Mensagem("Problemas durante a integração." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                notasGridGroupingControl.Focus();
                return;
            }

            if (!new NotaFiscalServicoBO().Revogar(_listaDeNotasParaRevogar.Where(w => w.Marcado).ToList()))
            {
                new Notificacoes.Mensagem("Problemas durante a revogação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                notasGridGroupingControl.Focus();
                return;
            }

        }

        private void notasRevogarGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.notasRevogarGridGroupingControl.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    int codigoNotaFiscal = (int)dr["CodigoInternoNotaFiscal"];

                    itensRevogarGridGroupingControl.DataSource = _listaItensParaRevogar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();
                }
            }
        }

        private void notasRevogarGridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.notasRevogarGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        int codigoNotaFiscal = (int)dr["CodigoInternoNotaFiscal"];

                        itensRevogarGridGroupingControl.DataSource = _listaItensParaRevogar.Where(w => w.CodigoInternoNotaFiscal == codigoNotaFiscal).ToList();
                    }
                }
            }
            catch { }
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            notasGridGroupingControl.DataSource = new List<NotasFiscaisServico>();
            notasRevogarGridGroupingControl.DataSource = new List<NotasFiscaisServico>();

            itensGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();
            itensRevogarGridGroupingControl.DataSource = new List<ItensNotasFiscaisServico>();

            tipoDocumentoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;

            tipoDocumentoTextBox.Focus();
        }

        private void marcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listaDeNotasParaIntegrar.ForEach(u => u.Marcado = true);
        }

        private void desmarcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listaDeNotasParaIntegrar.ForEach(u => u.Marcado = false);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _listaDeNotasParaRevogar.ForEach(u => u.Marcado = true);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _listaDeNotasParaRevogar.ForEach(u => u.Marcado = false);
        }

        private void tipoDocumentoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaTipoButton.Enabled = string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim());
        }
    }
}
