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
    public partial class FormulasLalur : Form
    {
        public FormulasLalur()
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
        List<Classes.Lalur.ContasDaFormula> _listaContas;
        List<Classes.Lalur.ContasDaFormula> _listaContasLog;
        Classes.Empresa _empresa;
        Classes.Lalur.Formulas _formula;
        Classes.Lalur.Formulas _formulaLog;
        Classes.RateioBeneficios.PlanoContabil _plano;
        Classes.RateioBeneficios.ContasContabeis _contas;

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

        private void FormulasLalur_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

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
            gridGroupingControl1.RecordNavigationBar.Label = "Contas";

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl1.TableDescriptor.Columns[i].AllowSort = true;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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
            RegraComboBox.SelectedIndex = 0;
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
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
                codigoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
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
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (codigoTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                new Pesquisas.FormulasLalur().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _formula = new LalurBO().Consultar(_empresa.IdEmpresa, Convert.ToInt32(codigoTextBox.Text));
            _formulaLog = new LalurBO().Consultar(_empresa.IdEmpresa, Convert.ToInt32(codigoTextBox.Text));

            gridGroupingControl1.DataSource = new List<MetasContasContabeis>();

            _listaContasLog = new List<Lalur.ContasDaFormula>();

            if (_formula.Existe)
            {
                ativoCheckBox.Checked = _formula.Ativo;
                nomeTextBox.Text = _formula.Descricao;

                _listaContas = new LalurBO().ListarContas(_formula.Id);
                _listaContasLog = new LalurBO().ListarContas(_formula.Id);

                gridGroupingControl1.DataSource = _listaContas;

                TotalizadorCheckBox.Checked = _formula.Totalizador;
                DestacarCheckBox.Checked = _formula.Destacar;
                FormulaTotalizadorTextBox.Text = _formula.Formula;
                OrdemTextBox.Text = _formula.Ordem.ToString();
            }

            excluirButton.Enabled = _formula.Existe;
            gravarButton.Enabled = true;

            if (Publicas._idRetornoPesquisa != 0)
                ativoCheckBox.Focus();
        }

        private void OrdemTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void FormulaTotalizadorTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
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

        private void ContasTextBox_KeyDown(object sender, KeyEventArgs e)
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
                IncluirButton.Focus();
            }
        }

        private void TotalizadorCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            ContasPanel.Enabled = !TotalizadorCheckBox.Checked;
            PesquisaFormulaButton.Enabled = TotalizadorCheckBox.Checked;
            FormulaTotalizadorTextBox.Enabled = TotalizadorCheckBox.Checked;
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new LalurBO().Proximo(_empresa.IdEmpresa).ToString();
            ativoCheckBox.Focus();
        }

        private void PesquisaPlanoButton_Click(object sender, EventArgs e)
        {
            new Pesquisas.PlanoContabil().ShowDialog();

            PlanoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

            if (PlanoTextBox.Text.Trim() == "" || PlanoTextBox.Text == "0")
            {
                PlanoTextBox.Text = string.Empty;
                PlanoTextBox.Focus();
                return;
            }

            PlanoTextBox_Validating(sender, new CancelEventArgs());
        }

        private void PesquisaCustoButton_Click(object sender, EventArgs e)
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

            ContasTextBox_Validating(sender, new CancelEventArgs());
        }

        private void IncluirButton_Click(object sender, EventArgs e)
        {
            if (_listaContas == null)
                _listaContas = new List<Lalur.ContasDaFormula>();

            if (string.IsNullOrEmpty(ContasTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Conta Contábil não informada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContasTextBox.Focus();
                return;
            }

            if (_listaContas.Where(w => w.CodigoConta == _contas.Codigo && w.NumeroPlano == _plano.NumeroPlano).Count() == 0)
            {
                _listaContas.Add(new Lalur.ContasDaFormula()
                {
                    IdFormula = _formula.Id,
                    NomeConta = _contas.Classificador + " " + _contas.Nome,
                    CodigoConta = _contas.Codigo,
                    NumeroPlano = _plano.NumeroPlano,
                    Regra = RegraComboBox.Text.Substring(0, 3)
                });
            }
            else
            {
                foreach (var item in _listaContas.Where(w => w.CodigoConta == _contas.Codigo && w.NumeroPlano == _plano.NumeroPlano))
                {
                    item.Regra = RegraComboBox.Text.Substring(0, 3);
                }
            }

            gridGroupingControl1.DataSource = new List<Lalur.ContasDaFormula>();
            gridGroupingControl1.DataSource = _listaContas;

            ContasTextBox.Text = string.Empty;
            NomeContaTextBox.Text = string.Empty;
            ContasTextBox.Focus();
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = new List<Lalur.ContasDaFormula>();

            if (_listaContas != null)
                _listaContas.Clear();
            
            ContasTextBox.Text = string.Empty;
            NomeContaTextBox.Text = string.Empty;
            PlanoTextBox.Text = string.Empty;
            NomePlanoTextBox.Text = string.Empty;
            FormulaTotalizadorTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            OrdemTextBox.Text = string.Empty;
            codigoTextBox.Text = string.Empty;
            codigoTextBox.Focus();
            TotalizadorCheckBox.Checked = false;
            DestacarCheckBox.Checked = false;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaContas == null)
                _listaContas = new List<Lalur.ContasDaFormula>();

            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            string _descricao = "";

            if (_formula == null)
                _formula = new Classes.Lalur.Formulas();

            _formula.Ativo = ativoCheckBox.Checked;
            _formula.Codigo = Convert.ToInt32(codigoTextBox.Text);
            _formula.Descricao = nomeTextBox.Text;
            _formula.Formula = FormulaTotalizadorTextBox.Text;
            _formula.IdEmpresa = _empresa.IdEmpresa;
            _formula.Ordem = Convert.ToInt32(OrdemTextBox.Text);
            _formula.Totalizador = TotalizadorCheckBox.Checked;
            _formula.Destacar = DestacarCheckBox.Checked;

            if (!new LalurBO().Gravar(_formula, _listaContas))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (_formula.Existe)
            {
                _descricao = _descricao +
                    (_formula.Ativo == _formulaLog.Ativo ? "" : " [Ativo] de " + _formulaLog.Ativo.ToString() + " para " + _formula.Ativo.ToString() + "") +
                    (_formula.Descricao == _formulaLog.Descricao ? "" : " [Descricao] de " + _formulaLog.Descricao.ToString() + " para " + _formula.Descricao.ToString() + "") +
                    (_formula.Formula == _formulaLog.Formula ? "" : " [Formula] de " + _formulaLog.Formula.ToString() + " para " + _formula.Formula.ToString() + "") +
                    (_formula.Ordem == _formulaLog.Ordem ? "" : " [Ordem] de " + _formulaLog.Ordem.ToString() + " para " + _formula.Ordem.ToString() + "") +
                    (_formula.Totalizador == _formulaLog.Totalizador ? "" : " [Totalizador] de " + _formulaLog.Totalizador.ToString() + " para " + _formula.Totalizador.ToString() + "") +
                    (_formula.Destacar == _formulaLog.Destacar ? "" : " [Destacar] de " + _formulaLog.Destacar.ToString() + " para " + _formula.Destacar.ToString() + "")
                    ;
            }

            foreach (var item in _listaContas)
            {
                if (_listaContasLog.Where(w => w.CodigoConta == item.CodigoConta && w.NumeroPlano == item.NumeroPlano).Count() == 0)
                {
                    _descricao = _descricao + " Plano " + item.NumeroPlano + " Conta " + item.CodigoConta + " Regra " + item.Regra;
                }
            }


            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou a Formula " + codigoTextBox.Text + " da empresa " + empresaComboBoxAdv.Text + " " + _descricao;
            _log.Tela = "Contabilidade - Lalur - Formulas";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new LalurBO().Excluir(_formula.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu a Formula " + codigoTextBox.Text + " da empresa " + empresaComboBoxAdv.Text + " " + nomeTextBox.Text +
                " com as contas contábeis [Plano_Conta_Regra] [";
            _log.Tela = "Contabilidade - Lalur - Formulas";

            foreach (var item in _listaContas)
            {
                _log.Descricao = _log.Descricao + item.NumeroPlano + "_" + item.CodigoConta + "_" + item.Regra + ", ";
            }

            try
            {
                _log.Descricao = _log.Descricao.Substring(0, _log.Descricao.Length - 2) + "]";
            }
            catch { }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            limparButton_Click(sender, e);
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

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            Publicas._idEmpresa = _empresa.IdEmpresa;
            new Pesquisas.FormulasLalur().ShowDialog();

            codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

            if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
            {
                codigoTextBox.Text = string.Empty;
                codigoTextBox.Focus();
                return;
            }

            codigoTextBox_Validating(sender, new CancelEventArgs());
        }

        private void PesquisaFormulaButton_Click(object sender, EventArgs e)
        {
            Publicas._idEmpresa = _empresa.IdEmpresa;
            new Pesquisas.FormulasLalur().ShowDialog();

            Publicas._idRetornoPesquisa.ToString();

            if (Publicas._idRetornoPesquisa == 0)
            {
                FormulaTotalizadorTextBox.Focus();
                return;
            }

            Classes.Lalur.Formulas _formula = new LalurBO().Consultar(_empresa.IdEmpresa, Publicas._idRetornoPesquisa);

            FormulaTotalizadorTextBox.Text = FormulaTotalizadorTextBox.Text + " [Formula " + _formula.Id + "] ";

        }

        private void excluirContaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.Lalur.ContasDaFormula _excluirTipos = new Classes.Lalur.ContasDaFormula();

                    gridGroupingControl1.DataSource = new List<Classes.Lalur.ContasDaFormula>();

                    int conta = 0;
                    int plano = 0;
                    int posIniId = 0;
                    int posFimId = 0;

                    try
                    {
                        conta = (int)dr["CodigoConta"];
                    }
                    catch
                    {
                        posIniId = dr.Info.IndexOf("CodigoConta =") + 13;
                        posFimId = dr.Info.IndexOf(", NomeConta");
                        conta = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                    }


                    try
                    {
                        plano = (int)dr["NumeroPlano"];
                    }
                    catch
                    {
                        posIniId = dr.Info.IndexOf("NumeroPlano =") + 13;
                        posFimId = dr.Info.IndexOf(", Plano");
                        plano = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                    }
                    
                    foreach (var item in _listaContas.Where(w => w.CodigoConta == conta &&
                                                                w.NumeroPlano == plano))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new LalurBO().ExcluirConta(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaContas.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu a conta contábil " + _excluirTipos.CodigoConta +
                        " do plano " + _excluirTipos.Plano +
                        " da empresa " + empresaComboBoxAdv.Text + " da formula " + codigoTextBox.Text;
                    _log.Tela = "Contabilidade - Lalur - Formulas";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaContas;
            }
        }

        private void CopiarButton_Click(object sender, EventArgs e)
        {
            if (_listaContas == null)
                _listaContas = new List<Lalur.ContasDaFormula>();

            Contabilidade.CopiaContasLalur _tela = new CopiaContasLalur();

            _tela._listaAssociacoesDestino = _listaContas;
            _tela.ShowDialog();

            bool encontrou = false;

            foreach (var item in _tela._listaAssociacoesOrigem.Where(w => w.Marcado))
            {
                foreach (var itemD in _listaContas.Where(w => w.CodigoConta == item.CodigoConta &&
                                                              w.NumeroPlano == item.NumeroPlano ))
                {
                    encontrou = true;
                    break;
                }

                if (!encontrou)
                {
                    _listaContas.Add(new Lalur.ContasDaFormula()
                    {
                        IdFormula = _formula.Id,
                        CodigoConta = item.CodigoConta,
                        NomeConta = item.NomeConta,
                        NumeroPlano = item.NumeroPlano,
                        Regra = item.Regra,
                        Existe = false
                    });
                }
            }

            gridGroupingControl1.DataSource = new List<Classes.Lalur.ContasDaFormula>();
            gridGroupingControl1.DataSource = _listaContas;
        }

        private void RegraComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            codigoTextBox_KeyDown(sender, e);
        }
    }
}
