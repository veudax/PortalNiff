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
    public partial class ParametrosConciliacaoAtivo : Form
    {
        public ParametrosConciliacaoAtivo()
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
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        List<Classes.ConciliacaoContabil.Ativo.Parametros> _listaParametros;
        List<Classes.ConciliacaoContabil.Ativo.Parametros> _listaParametrosLog;
        List<Classes.ConciliacaoContabil.Ativo.Parametros> _listaParametrosAgrupado;

        Classes.Empresa _empresa;
        Classes.ConciliacaoContabil.Ativo.ItemAtivo _itemAtivo;
        Classes.ConciliacaoContabil.Ativo.Parametros _parametros;

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

        private void ParametrosConciliacaoAtivo_Shown(object sender, EventArgs e)
        {
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

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

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
                new Notificacoes.Mensagem("Selecione a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            _listaParametrosAgrupado = new ConciliacaoContabilBO().ListarParametros(_empresa.IdEmpresa, true, consolidarCheckBox.Checked);
            _listaParametros = new ConciliacaoContabilBO().ListarParametros(_empresa.IdEmpresa, false, consolidarCheckBox.Checked);
            _listaParametrosLog = new ConciliacaoContabilBO().ListarParametros(_empresa.IdEmpresa, false, consolidarCheckBox.Checked);
            gridGroupingControl1.DataSource = _listaParametros;

            gravarButton.Enabled = _listaParametros.Count() > 0;
            excluirButton.Enabled = false;
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void textBoxExt1_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (codigoTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                Publicas._consolidar = consolidarCheckBox.Checked;
                new Pesquisas.ParametrosAtivo().ShowDialog();

                codigoTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            gridGroupingControl1.DataSource = new List<Classes.ConciliacaoContabil.Ativo.Parametros>();
            gridGroupingControl1.DataSource = _listaParametros.Where(w => w.Codigo == Convert.ToInt32(codigoTextBox.Text)).ToList();

            excluirButton.Enabled = false;
            foreach (var item in _listaParametrosAgrupado.Where(w => w.Codigo == Convert.ToInt32(codigoTextBox.Text)))
            {
                GrupoTextBox.Text = item.CodigoGrupo;
                NomeGrupoTextBox.Text = item.Descricao;
                excluirButton.Enabled = true;
            }
        }

        private void ItemAtivoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ItemAtivoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                gridGroupingControl1.DataSource = new List<Classes.ConciliacaoContabil.Ativo.Parametros>();
                foreach (var item in _listaParametros.Where(w => w.Codigo == Convert.ToInt32(codigoTextBox.Text)))
                {
                    item.CodigoGrupo = GrupoTextBox.Text;
                    item.Descricao = NomeGrupoTextBox.Text;
                    item.Grupo = item.CodigoGrupo + " - " + item.Descricao;
                }
                gridGroupingControl1.DataSource = _listaParametros.Where(w => w.Codigo == Convert.ToInt32(codigoTextBox.Text)).ToList();
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (ItemAtivoTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                Publicas._consolidar = consolidarCheckBox.Checked;

                new Pesquisas.Ativo().ShowDialog();

                ItemAtivoTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (ItemAtivoTextBox.Text.Trim() == "" || ItemAtivoTextBox.Text == "0")
                {
                    ItemAtivoTextBox.Text = string.Empty;
                    ItemAtivoTextBox.Focus();
                    return;
                }
            }

            if (!consolidarCheckBox.Checked)
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            _itemAtivo = new ConciliacaoContabilBO().Consultar(_empresa.IdEmpresa, Convert.ToInt32(ItemAtivoTextBox.Text), consolidarCheckBox.Checked, Publicas._codigoEmpresaGlobus);

            if (!_itemAtivo.Existe)
            {
                new Notificacoes.Mensagem("Item de Ativo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ItemAtivoTextBox.Focus();
                return;
            }

            DescricaoItemTextBox.Text = _itemAtivo.Nome;
        }

        private void buttonAdv2_Click(object sender, EventArgs e)
        {

            gridGroupingControl1.DataSource = new List<Classes.ConciliacaoContabil.Ativo.Parametros>();

            if (_listaParametros.Where(w => w.Codigo == Convert.ToInt32(codigoTextBox.Text) && w.CodigoAtivo == _itemAtivo.Codigo).Count() == 0)
            {
                _parametros = new ConciliacaoContabil.Ativo.Parametros();
                _parametros.IdEmpresa = _empresa.IdEmpresa;
                _parametros.Codigo = Convert.ToInt32(codigoTextBox.Text);
                _parametros.CodigoAtivo = _itemAtivo.Codigo;
                _parametros.Descricao = NomeGrupoTextBox.Text;
                _parametros.Grupo = GrupoTextBox.Text + " - " + _parametros.Descricao;
                _parametros.CodigoGrupo = GrupoTextBox.Text;
                _parametros.NomeAtivo = _itemAtivo.Conta + " - " + _itemAtivo.Nome;
                _parametros.IdEmpresaAtivo = _itemAtivo.IdEmpresa;
                _listaParametros.Add(_parametros);

            }
            else
                foreach (var item in _listaParametros.Where(w => w.Codigo == Convert.ToInt32(codigoTextBox.Text) && w.CodigoAtivo == _itemAtivo.Codigo))
                {
                    item.Descricao = NomeGrupoTextBox.Text;
                    item.Grupo = GrupoTextBox.Text + " - " + _parametros.Descricao;
                    item.CodigoGrupo = GrupoTextBox.Text;
                    item.NomeAtivo = _itemAtivo.Conta + " - " + _itemAtivo.Nome;
                    _parametros.IdEmpresaAtivo = _itemAtivo.IdEmpresa;
                }

            gridGroupingControl1.DataSource = _listaParametros.Where(w => w.Codigo == Convert.ToInt32(codigoTextBox.Text)).ToList();

            ItemAtivoTextBox.Text = string.Empty;
            DescricaoItemTextBox.Text = string.Empty;
            ItemAtivoTextBox.Focus();

            gravarButton.Enabled = _listaParametros.Count() > 0;
        }

        private void ItemAtivoTextBox_KeyDown(object sender, KeyEventArgs e)
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
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
            }
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            NomeGrupoTextBox.Text = string.Empty;
            GrupoTextBox.Text = string.Empty;
            ItemAtivoTextBox.Text = string.Empty;
            DescricaoItemTextBox.Text = string.Empty;

            _listaParametros.Clear();
            _listaParametrosAgrupado.Clear();

            gridGroupingControl1.DataSource = new List<Classes.ConciliacaoContabil.Ativo.Parametros>();
            empresaComboBoxAdv.Focus();
            excluirButton.Enabled = false;
            gravarButton.Enabled = false;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (!new ConciliacaoContabilBO().Gravar(_listaParametros))
            { 
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }
            
            string descricao = "";
            foreach (var item in _listaParametros)
            {
                if (_listaParametrosLog.Where(w => w.Codigo == item.Codigo && w.CodigoAtivo == item.CodigoAtivo).Count() == 0)
                    descricao = descricao + item.NomeAtivo + ", ";
            }

            try
            {
                descricao = descricao.Substring(0, descricao.Length - 2);
            }
            catch { }


            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou os Itens de ativo '" + descricao
                           + "' associado ao grupo '" + codigoTextBox.Text + " " + GrupoTextBox.Text + " - " + NomeGrupoTextBox.Text +
                "' da empresa " + empresaComboBoxAdv.Text;
            _log.Tela = "Contabilidade - Conciliação - Parametros do Ativo";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {

            if (new Notificacoes.Mensagem("Confirma a exclusão de todos os itens associado ao código " + codigoTextBox.Text + " ?"
                , Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new ConciliacaoContabilBO().ExcluirTudo(Convert.ToInt32(codigoTextBox.Text), _empresa.IdEmpresa))
            {
                new Notificacoes.Mensagem("Problemas durante a Exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            string descricao = "";
            foreach (var item in _listaParametros)
            {
                descricao = descricao + item.NomeAtivo + ", ";
            }

            try
            {
                descricao = descricao.Substring(0, descricao.Length - 2);
            }
            catch { }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu os Itens de ativo '" + descricao
                           + "' associado ao grupo '" + codigoTextBox.Text + " " + GrupoTextBox.Text + " - " + NomeGrupoTextBox.Text +
                "' da empresa " + empresaComboBoxAdv.Text;
            _log.Tela = "Contabilidade - Conciliação - Parametros do Ativo";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void excluirItemDoAtivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;
            int codcusto = 0;
            int codigo = 0;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.ConciliacaoContabil.Ativo.Parametros _excluirTipos = new Classes.ConciliacaoContabil.Ativo.Parametros();

                    gridGroupingControl1.DataSource = new List<Classes.ConciliacaoContabil.Ativo.Parametros>();

                    try
                    {
                        codcusto = (int)dr["CodigoAtivo"];
                        codigo = (int)dr["Codigo"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("CodigoAtivo =") + 13;
                            posFimId = dr.Info.IndexOf(", Grupo");
                            codcusto = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }

                        posIniId = 0;
                        posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("Codigo =") + 9;
                            posFimId = dr.Info.IndexOf(", CodigoAtivo");
                            codigo = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }
                    }

                    foreach (var item in _listaParametros.Where(w => w.CodigoAtivo == codcusto && w.Codigo == codigo))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new ConciliacaoContabilBO().ExcluirItem(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaParametros.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu Item de ativo '" + _excluirTipos.NomeAtivo 
                                   + "' associado ao grupo '" + _excluirTipos.Codigo + " " + _excluirTipos.Grupo +
                        "' da empresa " + empresaComboBoxAdv.Text ;
                    _log.Tela = "Contabilidade - Conciliação - Parametros do Ativo";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }
                if (codigoTextBox.Text == "")
                    gridGroupingControl1.DataSource = _listaParametros;
                else
                gridGroupingControl1.DataSource = _listaParametros.Where(w => w.Codigo == Convert.ToInt32(codigoTextBox.Text)).ToList(); 
            }
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (codigoTextBox.Text == "")
                excluirButton.Enabled = false;
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new ConciliacaoContabilBO().Proximo().ToString();
            GrupoTextBox.Focus();
        }

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '+')
            {
                codigoTextBox.Text = string.Empty;
                proximoButton_Click(sender, e);
            }
        }

        private void consolidarCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (consolidarCheckBox.Checked)
            {
                gridGroupingControl1.DataSource = new List<Classes.ConciliacaoContabil.Ativo.Parametros>();
                empresaComboBoxAdv_Validating(sender, new CancelEventArgs());
            }
        }
    }
}
