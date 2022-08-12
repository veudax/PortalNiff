using Classes;
using Negocio;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
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
    public partial class ParametrosConferenciaContabilNotas : Form
    {
        public ParametrosConferenciaContabilNotas()
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

        List<Classes.ConferenciaNotasPelaContabilidade.Parametros> _listaParametros;
        List<Classes.ConferenciaNotasPelaContabilidade.Parametros> _listaParametrosLog;
        Classes.ConferenciaNotasPelaContabilidade.Parametros _parametro;
        Classes.ConferenciaNotasPelaContabilidade.GrupoDespesas _grupo;
        Classes.Financeiro.DespesaReceitaGlobus _tipoDespesas;
        Classes.Financeiro.ContasContabeisDespesasReceitaGlobus _tipoDespesasCTB;
        Classes.RateioBeneficios.PlanoContabil _plano;
        Classes.RateioBeneficios.ContasContabeis _contas;

        bool _alterar = false;

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

        
        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
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


        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void ParametrosConferenciaContabilNotas_Shown(object sender, EventArgs e)
        {
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

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Parametros>();

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

            gridGroupingControl1.DataSource = new List<Classes.ConferenciaNotasPelaContabilidade.Parametros>();

            _listaParametros = new ConferenciaNotasPelaContabilidadeBO().Listar();
            _listaParametrosLog = new ConferenciaNotasPelaContabilidadeBO().Listar();

            gridGroupingControl1.DataSource = _listaParametros;
            gravarButton.Enabled = true;
        }

        private void incluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _parametro = null;
            _alterar = false;
            EdicaoPanel.Visible = true;
            GrupoTextBox.Enabled = true;
            GrupoTextBox.Focus();
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Publicas._escTeclado = true;

            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            int id = 0;

            if (rec == null)
            {
                new Notificacoes.Mensagem("Nenhum Tipo de despesa/Conta Contábil selecionado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                gridGroupingControl1.Focus();
                return;
            }
            EdicaoPanel.Visible = true;
            _alterar = true;
            Record dr = rec.GetRecord() as Record;
            if (dr != null)
            {
                _parametro = new Classes.ConferenciaNotasPelaContabilidade.Parametros();

                try
                {
                    id = (int)dr["Id"];
                }
                catch
                {
                    int posIniId = 0;
                    int posFimId = 0;

                    try
                    {
                        posIniId = dr.Info.IndexOf("Id =") + 4;
                        posFimId = dr.Info.IndexOf(", CodigoGrupo");
                        id = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                    }
                    catch { }
                }

                foreach (var item in _listaParametros.Where(w => w.Id == id))
                {
                    _parametro = item;
                    break;
                }

                GrupoTextBox.Text = _parametro.CodigoGrupo.ToString();
                TipoTextBox.Text = _parametro.CodigoTipo;
                PlanoTextBox.Text = _parametro.NumeroPlano.ToString();
                ContaTextBox.Text = _parametro.CodigoConta.ToString();

                GrupoTextBox_Validating(sender, new CancelEventArgs());
                TipoTextBox_Validating(sender, new CancelEventArgs());
                PlanoTextBox_Validating(sender, new CancelEventArgs());
                ContaTextBox_Validating(sender, new CancelEventArgs());
            }            

            GrupoTextBox.Enabled = false;

            TipoTextBox.Focus();
        }

        private void TipoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                EdicaoPanel.Visible = false;
                gridGroupingControl1.Focus();
            }
        }

        private void GrupoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                EdicaoPanel.Visible = false;
                gridGroupingControl1.Focus();
            }
        }

        private void GrupoTextBox_Enter(object sender, EventArgs e)
        {
            GrupoTextBox.BorderColor = Publicas._bordaEntrada;
            PesquisaGrupoButton.Enabled = string.IsNullOrEmpty(GrupoTextBox.Text.Trim());
        }

        private void TipoTextBox_Enter(object sender, EventArgs e)
        {
            TipoTextBox.BorderColor = Publicas._bordaEntrada;
            PesquisaTipoButton.Enabled = string.IsNullOrEmpty(TipoTextBox.Text.Trim());
        }

        private void PlanoTextBox_Enter(object sender, EventArgs e)
        {
            PlanoTextBox.BorderColor = Publicas._bordaEntrada;
            PesquisaPlanoButton.Enabled = string.IsNullOrEmpty(PlanoTextBox.Text.Trim());
        }

        private void ContaTextBox_Enter(object sender, EventArgs e)
        {
            ContaTextBox.BorderColor = Publicas._bordaEntrada;
            PesquisaContaButton.Enabled = string.IsNullOrEmpty(ContaTextBox.Text.Trim());
        }

        private void GrupoTextBox_Validating(object sender, CancelEventArgs e)
        {
            GrupoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (string.IsNullOrEmpty(GrupoTextBox.Text.Trim()))
            {
                new Pesquisas.GrupoDespesas().ShowDialog();

                GrupoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(GrupoTextBox.Text) || GrupoTextBox.Text == "0")
                {
                    GrupoTextBox.Text = string.Empty;
                    GrupoTextBox.Focus();
                    return;
                }
            }

            _grupo = new ConferenciaNotasPelaContabilidadeBO().ConsultarGrupo(Convert.ToInt32(GrupoTextBox.Text));

            if (!_grupo.Existe)
            {
                new Notificacoes.Mensagem("Grupo de despesa não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                GrupoTextBox.Focus();
                return;
            }

            NomeGrupoTextBox.Text = _grupo.Descricao;
        }

        private void TipoTextBox_Validating(object sender, CancelEventArgs e)
        {
            TipoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (string.IsNullOrEmpty(TipoTextBox.Text.Trim()))
            {
                Publicas._tipoDespesaReceita = "D";
                new Pesquisas.Despesas().ShowDialog();

                TipoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(TipoTextBox.Text) || TipoTextBox.Text == "0")
                {
                    TipoTextBox.Text = string.Empty;
                    TipoTextBox.Focus();
                    return;
                }
            }

            _tipoDespesas = new FinanceiroBO().Consultar(TipoTextBox.Text, "D");

            if (!_tipoDespesas.Existe)
            {
                new Notificacoes.Mensagem("Grupo de despesa não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                TipoTextBox.Focus();
                return;
            }

            NomeTipoTextBox.Text = _tipoDespesas.Descricao;

            if (_plano == null)
            {
                new Notificacoes.Mensagem("Informe o plano de contas.", Publicas.TipoMensagem.Alerta).ShowDialog();
                 PlanoTextBox.Focus();
                return;
            }

            _tipoDespesasCTB = new FinanceiroBO().Consultar(TipoTextBox.Text, "D", _plano.NumeroPlano);
            if (_tipoDespesasCTB.Existe)
            {
                ContaTextBox.Text = _tipoDespesasCTB.ContaContabil.ToString();
                ContaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PlanoTextBox_Validating(object sender, CancelEventArgs e)
        {
            PlanoTextBox.BorderColor = Publicas._bordaSaida;

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

            NomePlanoTextBox.Text = _plano.Nome;
        }

        private void ContaTextBox_Validating(object sender, CancelEventArgs e)
        {
            ContaTextBox.BorderColor = Publicas._bordaSaida;

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

            if (ContaTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                ContaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (ContaTextBox.Text.Trim() == "" || ContaTextBox.Text == "0")
                {
                    ContaTextBox.Text = string.Empty;
                    ContaTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(ContaTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaTextBox.Focus();
                return;
            }

            if (!_contas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaTextBox.Focus();
                return;
            }

            NomeContaTextBox.Text = _contas.Classificador + " " + _contas.Nome;
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            if (_parametro == null)
            {
                _parametro = new ConferenciaNotasPelaContabilidade.Parametros();

                _parametro.CodigoGrupo = Convert.ToInt32(GrupoTextBox.Text);
                _parametro.CodigoTipo = TipoTextBox.Text;
                _parametro.NumeroPlano = Convert.ToInt32(PlanoTextBox.Text);
                _parametro.CodigoConta = Convert.ToInt32(ContaTextBox.Text);
                _parametro.Grupo = _parametro.CodigoGrupo + " - " + NomeGrupoTextBox.Text;
                _parametro.NomeTipo = _parametro.CodigoTipo + " - " + NomeTipoTextBox.Text;
                _parametro.NomeConta = _parametro.CodigoConta + " - " + NomeContaTextBox.Text;

                _listaParametros.Add(_parametro);
            }
            else
            {
                foreach (var item in _listaParametros.Where(w => w.Id == _parametro.Id ))
                {
                    item.CodigoTipo = TipoTextBox.Text;
                    item.NumeroPlano = Convert.ToInt32(PlanoTextBox.Text);
                    item.CodigoConta = Convert.ToInt32(ContaTextBox.Text);
                    item.NomeTipo = _parametro.CodigoTipo + " - " + NomeTipoTextBox.Text;
                    item.NomeConta = _parametro.CodigoConta + " - " + NomeContaTextBox.Text;
                    item.Alterado = true;
                }
            }

            gridGroupingControl1.DataSource = new List<ConferenciaNotasPelaContabilidade.Parametros>();
            gridGroupingControl1.DataSource = _listaParametros;

            TipoTextBox.Text = string.Empty;
            ContaTextBox.Text = string.Empty;
            NomeTipoTextBox.Text = string.Empty;
            NomeContaTextBox.Text = string.Empty;
            _parametro = null;

            if (!_alterar)
                TipoTextBox.Focus();
            else
            {
                GrupoTextBox.Text = string.Empty;
                NomeGrupoTextBox.Text = string.Empty;
                EdicaoPanel.Visible = false;
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaParametros.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum Grupo Informado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }
            
            if (!new ConferenciaNotasPelaContabilidadeBO().Gravar(_listaParametros))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou parâmetro de conferência de Notas ";
            _log.Tela = "Contabilidade - Notas Fiscais - Parâmetros";

            foreach (var item in _listaParametros)
            {
                if (_listaParametrosLog.Where(w => w.CodigoGrupo == item.CodigoGrupo &&
                                                   w.CodigoTipo == item.CodigoTipo &&
                                                   w.CodigoConta == item.CodigoConta && !item.Alterado).Count() == 0)
                {
                    _log.Descricao = _log.Descricao + " Incluido Grupo de despesas " + item.CodigoGrupo +
                        " com Tipo de Despesa " + item.CodigoTipo +
                        " e Conta Contábil " + item.CodigoConta + " do plano " + item.NumeroPlano;
                }

                if (item.Alterado && (item.CodigoTipo != item.CodigoTipoOriginal || item.CodigoConta != item.CodigoContaOriginal))
                {
                    _log.Descricao = _log.Descricao + " Alterou Grupo de despesas " + item.CodigoGrupo +
                     (item.CodigoTipo != item.CodigoTipoOriginal ? " [Tipo de despesa] de " + item.CodigoTipoOriginal + " para " + item.CodigoTipo : "") +
                     (item.CodigoConta != item.CodigoContaOriginal ? " [Conta Contábil] de " + item.CodigoContaOriginal + " para " + item.CodigoConta : "") +
                     (item.NumeroPlano != item.NumeroPlanoOriginal ? " [Plano] de " + item.NumeroPlanoOriginal + " para " + item.NumeroPlano : "");
                }
            }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
                       
            _listaParametros = new ConferenciaNotasPelaContabilidadeBO().Listar();
            _listaParametrosLog = new ConferenciaNotasPelaContabilidadeBO().Listar();
            gridGroupingControl1.DataSource = new List<ConferenciaNotasPelaContabilidade.Parametros>();
            gridGroupingControl1.DataSource = _listaParametros;

        }

        private void excluirTipoDeDespesaEContaContábilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            int id = 0;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão do tipo de despesa/Conta contábil?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.ConferenciaNotasPelaContabilidade.Parametros _excluirTipos = new Classes.ConferenciaNotasPelaContabilidade.Parametros();

                    gridGroupingControl1.DataSource = new List<Classes.ConferenciaNotasPelaContabilidade.Parametros>();

                    try
                    {
                        id = (int)dr["Id"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("Id =") + 4;
                            posFimId = dr.Info.IndexOf(", CodigoGrupo");
                            id = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }
                    }

                    foreach (var item in _listaParametros.Where(w => w.Id == id))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new ConferenciaNotasPelaContabilidadeBO().ExcluirTipo(item.Id))
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
                    _log.Descricao = "Excluiu do Grupo " + _excluirTipos.CodigoGrupo +
                        " o Tipo de Despesa " + _excluirTipos.CodigoTipo + 
                        " e Conta Contábil " + _excluirTipos.CodigoConta;
                    _log.Tela = "Contabilidade - Notas Fiscais - Parâmetros";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaParametros;
            }
        }

        private void excluirGrupoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            int grupo = 0;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão do deste Grupo ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.ConferenciaNotasPelaContabilidade.Parametros _excluirTipos = new Classes.ConferenciaNotasPelaContabilidade.Parametros();
                    List<Classes.ConferenciaNotasPelaContabilidade.Parametros> _listaExcluir = new List<ConferenciaNotasPelaContabilidade.Parametros>();

                    gridGroupingControl1.DataSource = new List<Classes.ConferenciaNotasPelaContabilidade.Parametros>();

                    try
                    {
                        grupo = (int)dr["CodigoGrupo"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("CodigoGrupo =") + 13;
                            posFimId = dr.Info.IndexOf(", Grupo");
                            grupo = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }
                    }

                    foreach (var item in _listaParametros.Where(w => w.CodigoGrupo == grupo))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new ConferenciaNotasPelaContabilidadeBO().ExcluirTipo(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        _listaExcluir.Add(_excluirTipos);
                    }

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu o Grupo " + _excluirTipos.CodigoGrupo + " com todos os [Tipos Despesas/Plano/Conta Contabil] associado a ele";

                    foreach (var item in _listaExcluir)
                    {
                        _listaParametros.Remove(item);

                        _log.Descricao = _log.Descricao  + " [" + item.CodigoTipo + "/" + item.NumeroPlano + "/" + item.CodigoConta + "]";
                    }
                    _log.Descricao = _log.Descricao ;

                    _log.Tela = "Contabilidade - Notas Fiscais - Parâmetros";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaParametros;
            }
        }
    }
}
