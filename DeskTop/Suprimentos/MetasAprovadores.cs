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

namespace Suportte.Suprimentos
{
    public partial class MetasAprovadores : Form
    {
        public MetasAprovadores()
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
                }

                MetaCurrency.DecimalValue = 0;
                MetaCurrency.Tag = null;
                MetaCurrency.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                MetaCurrency.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                MetaCurrency.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                MetaCurrency.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                MetaCurrency.BackGroundColor = Publicas._fundo;
                MetaCurrency.MinValue = (decimal)-999999999999.99;
                MetaCurrency.MaxValue = (decimal)999999999999.99;
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.FuncionariosGlobus _funcionariosGlobus;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasUsuario;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.Suprimentos.Metas> _listaMetas;
        List<Classes.Suprimentos.Metas> _listaMetasLog;
        DateTime _dataInicio;
        DateTime _dataFim;
        string _referencia;
        int _ano;
        bool _alterar;

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

        private void MetasAprovadores_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);
            _listaEmpresasUsuario = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._usuario.Id);
            _listaEmpresasAutorizadas = new List<Empresa>();

            foreach (var item in _listaEmpresasUsuario.Where(w => w.EmpresaAutoriza))
                _listaEmpresasAutorizadas.AddRange(_listaEmpresas.Where(w => w.IdEmpresa == item.IdEmpresa));
            
            empresaComboBoxAdv.DataSource = _listaEmpresasAutorizadas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            gridGroupingControl1.DataSource = new List<Suprimentos.MetasAprovadores>();
            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl1.TableControl.CellToolTip.Active = true;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl1.RecordNavigationBar.Label = "Metas";

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            GridMetroColors metroColor = new GridMetroColors();

            metroColor.HeaderBottomBorderColor = Publicas._bordaEntrada;
            metroColor.HeaderBottomBorderWeight = GridBottomBorderWeight.ExtraThin;

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
                gridGroupingControl1.Focus();
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
                SelectNextControl(ActiveControl, false, true, true, true);
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

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void MetaCurrency_Enter(object sender, EventArgs e)
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

            foreach (var item in _listaEmpresasAutorizadas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }

            if (_empresa == null)
            {
                new Notificacoes.Mensagem("Selecione a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            _listaMetas = new SuprimentosBO().Listar(_empresa.IdEmpresa);
            _listaMetasLog = new SuprimentosBO().Listar(_empresa.IdEmpresa);
            //_listaMetas = new SuprimentosBO().Listar(_funcionariosGlobus.Id);
            //_listaMetasLog = new SuprimentosBO().Listar(_funcionariosGlobus.Id);
            gridGroupingControl1.DataSource = _listaMetas;

            gravarButton.Enabled = _listaMetas.Count() != 0;
            excluirButton.Enabled = _listaMetas.Count() != 0 && _listaMetas.Where(w => w.Existe).Count() != 0;

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

            nomeTextBox.Text = _funcionariosGlobus.Nome;

            // carregar metas
            _listaMetas = new SuprimentosBO().Listar(_funcionariosGlobus.Id);
            _listaMetasLog = new SuprimentosBO().Listar(_funcionariosGlobus.Id);
            gridGroupingControl1.DataSource = _listaMetas;

            gravarButton.Enabled = _listaMetas.Count() != 0;
            excluirButton.Enabled = _listaMetas.Count() != 0 && _listaMetas.Where(w => w.Existe).Count() != 0;
    
        }

        private void MetaCurrency_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._setaParaBaixo)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._setaParaBaixo = false;
                return;
            }

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
            _ano = Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4));

        }

        private void pesquisaUsuarioButton_Click(object sender, EventArgs e)
        {
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

                usuarioTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            if (_alterar || _listaMetas.Where(w => w.ReferenciaFormatada == referenciaMaskedEditBox.Text).Count() != 0)
            {
                foreach (var item in _listaMetas.Where(w => w.ReferenciaFormatada == referenciaMaskedEditBox.Text))
                    item.ValorMeta = MetaCurrency.DecimalValue;
            }
            else
            {
                Classes.Suprimentos.Metas _metas = new Classes.Suprimentos.Metas();
                //_metas.CodIntFunc = _funcionariosGlobus.Id;
                _metas.IdEmpresa = _empresa.IdEmpresa;
                _metas.Referencia = Convert.ToInt32(_referencia);
                _metas.ReferenciaFormatada = referenciaMaskedEditBox.Text;
                _metas.ValorMeta = MetaCurrency.DecimalValue;
                _metas.Ano = _ano;

                _listaMetas.Add(_metas);
            }

            gridGroupingControl1.DataSource = new List<Classes.Suprimentos.Metas>();
            gridGroupingControl1.DataSource = _listaMetas;
            _alterar = false;
            gravarButton.Enabled = _listaMetas.Count() != 0;
            excluirButton.Enabled = _listaMetas.Count() != 0 && _listaMetas.Where(w => w.Existe).Count() != 0;

            referenciaMaskedEditBox.Text = string.Empty;
            MetaCurrency.DecimalValue = 0;

            if (_alterar)
                gridGroupingControl1.Focus();
            else
                referenciaMaskedEditBox.Focus();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão desta referência ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Classes.Suprimentos.Metas _excluirTipos = new Classes.Suprimentos.Metas();

                    gridGroupingControl1.DataSource = new List<Classes.Suprimentos.Metas>();

                    int posIniId = dr.Info.IndexOf("Referencia =") + 12;
                    int posFimId = dr.Info.IndexOf(", Ano");

                    int referencia = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());

                    foreach (var item in _listaMetas.Where(w => w.Referencia == referencia))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new SuprimentosBO().ExcluirReferencia(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaMetas.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu Metas de aprovador da empresa " + empresaComboBoxAdv.Text + " do aprovador " + usuarioTextBox.Text + " " + nomeTextBox.Text +
                        " Referencia " +_excluirTipos.ReferenciaFormatada + " com o valor " + _excluirTipos.ValorMeta.ToString() ;
                    _log.Tela = "Suprimentos - Metas Aprovadores - Gastos Aprovadores";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaMetas;
                gravarButton.Enabled = _listaMetas.Count() != 0;
                excluirButton.Enabled = _listaMetas.Count() != 0 && _listaMetas.Where(w => w.Existe).Count() != 0;
            }
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Classes.Suprimentos.Metas _excluirTipos = new Classes.Suprimentos.Metas();

                    int posIniId = dr.Info.IndexOf("Referencia =") + 12;
                    int posFimId = dr.Info.IndexOf(", Ano");

                    int referencia = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());

                    foreach (var item in _listaMetas.Where(w => w.Referencia == referencia))
                    {
                        _excluirTipos = item;

                        referenciaMaskedEditBox.Text = _excluirTipos.ReferenciaFormatada.Replace("/", "");
                        MetaCurrency.DecimalValue = _excluirTipos.ValorMeta;
                        MetaCurrency.Focus();
                        break;
                    }

                    _alterar = true;
                }
            }

            gravarButton.Enabled = _listaMetas.Count() != 0;
            excluirButton.Enabled = _listaMetas.Count() != 0 && _listaMetas.Where(w => w.Existe).Count() != 0;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {

            if (_listaMetas.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum meta informada." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            if (!new SuprimentosBO().Gravar(_listaMetas))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            string _descricao = "";
            foreach (var itemL in _listaMetasLog)
            {
                foreach (var item in _listaMetas.Where(w => w.Referencia == itemL.Referencia))
                {
                    if (item.ValorMeta != itemL.ValorMeta)
                        _descricao = _descricao + "Referencia " + item.ReferenciaFormatada + " [ValorMeta] de " + itemL.ValorMeta.ToString() + " para " + item.ValorMeta.ToString() + ", ";
                }
            }

            foreach (var item in _listaMetas)
            {
                if (_listaMetasLog.Where(w => w.Referencia == item.Referencia).Count() == 0)
                {
                    _descricao = _descricao + " Incluiu Referencia " + item.ReferenciaFormatada + " Valor " + item.ValorMeta + ", ";
                }
            }

            if (!string.IsNullOrEmpty(_descricao))
                _descricao = _descricao.Substring(0, _descricao.Length - 2);

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou Metas de aprovador da empresa " + empresaComboBoxAdv.Text + " do aprovador " + usuarioTextBox.Text + " " + nomeTextBox.Text + 
                " [" + _descricao + "]";

            _log.Tela = "Suprimentos - Metas Aprovadores - Gastos Aprovadores";

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
            referenciaMaskedEditBox.Text = string.Empty;
            MetaCurrency.DecimalValue = 0;
            usuarioTextBox.Focus();
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            _alterar = false;
            _listaMetas = new List<Classes.Suprimentos.Metas>();
            gridGroupingControl1.DataSource = new List<Classes.Suprimentos.Metas>();

        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new SuprimentosBO().ExcluirTodos(_funcionariosGlobus.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu Metas de aprovador da empresa " + empresaComboBoxAdv.Text + " do aprovador " + usuarioTextBox.Text + " " + nomeTextBox.Text;
            foreach (var item in _listaMetas)
            {
                _log.Descricao = _log.Descricao + "[ Referencia " + item.ReferenciaFormatada + " Valor " + item.ValorMeta.ToString() + "] ";
            }
            _log.Tela = "Suprimentos - Metas Aprovadores - Gastos Aprovadores";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
            }
        }

        private void MetaCurrency_KeyDown(object sender, KeyEventArgs e)
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
    }
}
