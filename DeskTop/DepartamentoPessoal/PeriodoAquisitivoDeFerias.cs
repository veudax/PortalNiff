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

namespace Suportte.DepartamentoPessoal
{
    public partial class PeriodoAquisitivoDeFerias : Form
    {
        public PeriodoAquisitivoDeFerias()
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

                    inicialDateTimePicker.Style = VisualStyle.Office2016Black;
                    FimDateTimePicker.Style = VisualStyle.Office2016Black;
                    LimiteDateTimePicker.Style = VisualStyle.Office2016Black;
                    this.BackColor = inicialDateTimePicker.BackColor;
                }

                inicialDateTimePicker.Value = DateTime.Now;
                FimDateTimePicker.Value = DateTime.Now;

                inicialDateTimePicker.BorderColor = Publicas._bordaSaida;
                FimDateTimePicker.BorderColor = Publicas._bordaSaida;
                LimiteDateTimePicker.BorderColor = Publicas._bordaSaida;

            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Usuario _usuario;
        Classes.FuncionariosGlobus _funcionariosGlobus;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.PeriodoAquisitivo> _listaPeriodos;
        List<Classes.PeriodoAquisitivo> _listaPeriodosLog;

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
                FimDateTimePicker.Focus();
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

        private void PeriodoAquisitivoDeFerias_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);
            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();

            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
            {
                empresaComboBoxAdv.SelectedIndex = i;
                if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                {
                    break;
                }
            }
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

        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                usuarioTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void usuarioTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void inicialDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                FimDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                usuarioTextBox.Focus();
            }
        }

        private void FimDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LimiteDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                inicialDateTimePicker.Focus();
            }
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void inicialDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void FimDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
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

        private void usuarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            usuarioTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                usuarioTextBox.Text = string.Empty;
                nomeTextBox.Text = string.Empty;
                pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._participaAvaliacao = false;

                new Pesquisas.Funcionarios().ShowDialog();

                usuarioTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(usuarioTextBox.Text) || usuarioTextBox.Text == "0")
                {
                    usuarioTextBox.Text = string.Empty;
                    usuarioTextBox.Focus();
                    return;
                }
            }

            usuarioTextBox.Text = usuarioTextBox.Text.PadLeft(6, '0');

            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(usuarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            if (!_funcionariosGlobus.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            _usuario = new UsuarioBO().ConsultaUsuarioPorCodigoFuncionarioGlobus(_funcionariosGlobus.Id);

            if (_usuario.DataAdmissao == DateTime.MinValue)
            {
                new Notificacoes.Mensagem("Colaborador sem data de admissão." + Environment.NewLine +
                    "Informe ao TI para corrigir o cadastro no Sistema Interno.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }
                        
            nomeTextBox.Text = _usuario.Nome;

            // buscar os periodos cadastrados
            _listaPeriodos = new ProgramacaoFeriasBO().Listar(_empresa.IdEmpresa, _funcionariosGlobus.Id);
            _listaPeriodosLog = new ProgramacaoFeriasBO().Listar(_empresa.IdEmpresa, _funcionariosGlobus.Id);

            gridGroupingControl1.DataSource = _listaPeriodos;

            gravarButton.Enabled = _listaPeriodos.Count() > 0;
            excluirButton.Enabled = _listaPeriodos.Count() > 0;
        }

        private void inicialDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (inicialDateTimePicker.Value == DateTime.MinValue)
            {
                new Notificacoes.Mensagem("Inicio solicitado inválido", Publicas.TipoMensagem.Alerta).ShowDialog();
                inicialDateTimePicker.Focus();
                return;
            }

        }

        private void usuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void FimDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (FimDateTimePicker.Value == DateTime.MinValue)
            {
                new Notificacoes.Mensagem("Inicio solicitado inválido", Publicas.TipoMensagem.Alerta).ShowDialog();
                FimDateTimePicker.Focus();
                return;
            }

            if (FimDateTimePicker.Value.Date < inicialDateTimePicker.Value.Date)
            {
                new Notificacoes.Mensagem("Data Fim menor que data inicial", Publicas.TipoMensagem.Alerta).ShowDialog();
                FimDateTimePicker.Focus();
                return;
            }

        }

        private void LimiteDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FimDateTimePicker.Focus();
            }
        }

        private void LimiteDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (LimiteDateTimePicker.Value == DateTime.MinValue)
            {
                new Notificacoes.Mensagem("Inicio solicitado inválido", Publicas.TipoMensagem.Alerta).ShowDialog();
                FimDateTimePicker.Focus();
                return;
            }

            if (LimiteDateTimePicker.Value.Date < FimDateTimePicker.Value.Date)
            {
                new Notificacoes.Mensagem("Data Limite menor que data fim", Publicas.TipoMensagem.Alerta).ShowDialog();
                LimiteDateTimePicker.Focus();
                return;
            }

            // adicionar no grid
            if (_listaPeriodos.Where(w => w.Inicio == inicialDateTimePicker.Value.Date).Count() == 0)
            {
                PeriodoAquisitivo _per = new PeriodoAquisitivo();
                _per.IdEmpresa = _empresa.IdEmpresa;
                _per.CodIntFunc = _funcionariosGlobus.Id;
                _per.Inicio = inicialDateTimePicker.Value.Date;
                _per.Fim = FimDateTimePicker.Value.Date;
                _per.Limite = LimiteDateTimePicker.Value.Date;

                _listaPeriodos.Add(_per);
            }
            else
            {
                foreach (var item in _listaPeriodos.Where(w => w.Inicio == inicialDateTimePicker.Value.Date))
                {
                    item.Fim = FimDateTimePicker.Value.Date;
                    item.Limite = LimiteDateTimePicker.Value.Date;
                }
            }
            gridGroupingControl1.DataSource = new List<PeriodoAquisitivo>();
            gridGroupingControl1.DataSource = _listaPeriodos;

            gravarButton.Enabled = _listaPeriodos.Count() != 0;
            inicialDateTimePicker.Value = DateTime.MinValue;
            FimDateTimePicker.Value = DateTime.MinValue;
            LimiteDateTimePicker.Value = DateTime.MinValue;
            gridGroupingControl1.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {

            if (!new ProgramacaoFeriasBO().Gravar(_listaPeriodos))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou Periodo Aquisitivo de Férias do Colaborador " +  usuarioTextBox.Text + " " + nomeTextBox.Text +
                " da empresa " + empresaComboBoxAdv.Text;

            foreach (var itemL in _listaPeriodosLog)
            {
                foreach (var item in _listaPeriodos.Where(w => w.Inicio == itemL.Inicio))
                {
                    if (item.Fim != itemL.Fim || item.Limite != itemL.Limite)
                    {
                        _log.Descricao = _log.Descricao + "[Alterado] INICIO " + item.Inicio.ToShortDateString() +
                            (item.Fim == itemL.Fim ? "" : " FIM de " + itemL.Fim.ToShortDateString() + " para " + item.Fim.ToShortDateString()) +
                            (item.Limite == itemL.Limite ? "" : " LIMITE de " + itemL.Limite.ToShortDateString() + " para " + item.Limite.ToShortDateString());
                    }
                }
            }

            foreach (var item in _listaPeriodos)
            {
                if (_listaPeriodosLog.Where(w => w.Inicio == item.Inicio).Count() == 0)
                {
                    _log.Descricao = _log.Descricao + "[Novo] INICIO " + item.Inicio.ToShortDateString() +
                              " FIM de " + item.Fim.ToShortDateString() +
                            " LIMITE de " + item.Limite.ToShortDateString();
                }
            }


            _log.Tela = "Recursos Humanos - Período Aquisitivo de Férias";
            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }


            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            usuarioTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            inicialDateTimePicker.Value = DateTime.MinValue;
            FimDateTimePicker.Value = DateTime.MinValue;
            LimiteDateTimePicker.Value = DateTime.MinValue;

            gridGroupingControl1.DataSource = new List<PeriodoAquisitivo>();
            usuarioTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new ProgramacaoFeriasBO().ExcluirTodosPeriodosAquisitivos(_funcionariosGlobus.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu Periodo Aquisitivo de Férias do Colaborador " + usuarioTextBox.Text + " " + nomeTextBox.Text +
                " da empresa " + empresaComboBoxAdv.Text;

            foreach (var itemL in _listaPeriodosLog)
            {
                _log.Descricao = _log.Descricao + " INICIO " + itemL.Inicio.ToShortDateString() +
                            " FIM de " + itemL.Fim.ToShortDateString() +
                            " LIMITE de " + itemL.Limite.ToShortDateString();
            }

            _log.Tela = "Recursos Humanos - Período Aquisitivo de Férias";
            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            decimal id = 0;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Classes.PeriodoAquisitivo _excluirTipos = new Classes.PeriodoAquisitivo();

                    try
                    {
                        id = (decimal)dr["Id"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("Id =") + 4;
                            posFimId = dr.Info.IndexOf(", IdEmpresa");
                            id = Convert.ToDecimal(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }
                    }

                    foreach (var item in _listaPeriodos.Where(w => w.Id == id))
                    {
                        inicialDateTimePicker.Value = item.Inicio;
                        FimDateTimePicker.Value = item.Fim;
                        LimiteDateTimePicker.Value = item.Limite;
                        FimDateTimePicker.Focus();
                        break;
                    }
                    
                }

            }

        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            decimal id = 0;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão do período selecionado ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.PeriodoAquisitivo _excluirTipos = new Classes.PeriodoAquisitivo();

                    gridGroupingControl1.DataSource = new List<Classes.PeriodoAquisitivo>();

                    try
                    {
                        id = (decimal)dr["Id"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("Id =") + 4;
                            posFimId = dr.Info.IndexOf(", IdEmpresa");
                            id = Convert.ToDecimal(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }
                    }

                    foreach (var item in _listaPeriodos.Where(w => w.Id == id))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new ProgramacaoFeriasBO().ExcluirPeriodoAquisitivo(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaPeriodos.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu Periodo Aquisitivo de Férias do Colaborador " + usuarioTextBox.Text + " " + nomeTextBox.Text +
                    " da empresa " + empresaComboBoxAdv.Text;

                     _log.Descricao = _log.Descricao + " INICIO " + _excluirTipos.Inicio.ToShortDateString() +
                                    " FIM de " + _excluirTipos.Fim.ToShortDateString() +
                                    " LIMITE de " + _excluirTipos.Limite.ToShortDateString();

                    _log.Tela = "Recursos Humanos - Período Aquisitivo de Férias";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaPeriodos;
                gravarButton.Enabled = _listaPeriodos.Count() != 0 ;
            }

        }
    }
}
