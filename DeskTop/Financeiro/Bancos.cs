using Classes;
using DynamicFilter;
using Negocio;
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

namespace Suportte.Financeiro
{
    public partial class Bancos : Form
    {
        public Bancos()
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
                    gridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl.BackColor = Publicas._panelTitulo;
                    SaldoInicialTextBox.PositiveColor = Publicas._fonte;
                    SaldoInicialTextBox.ZeroColor = Publicas._fonte;
                }
            }
            SaldoInicialTextBox.BackGroundColor = codigoTextBox.BackColor;
            Publicas._mensagemSistema = string.Empty;
        }

        #region Atributos
        Classes.Empresa _empresa;
        Classes.Empresa _empresaConsolidada;
        List<Classes.Empresa> _listaEmpresas;
        Classes.Financeiro.Colunas _colunas;
        Classes.Financeiro.Bancos _banco;
        Classes.Financeiro.Bancos _bancoConsolidado;
        Classes.Financeiro.BancosGlobus _bancoGlobus;
        Classes.Financeiro.AgenciaGlobus _agencia;
        Classes.Financeiro.ContaGlobus _conta;
        Classes.Financeiro.BancosGlobus _bancoCartoesGlobus;
        Classes.Financeiro.AgenciaGlobus _agenciaCartoes;
        Classes.Financeiro.ContaGlobus _contaCartoes;

        Classes.Financeiro.DespesaReceitaGlobus _despesas;
        List<Classes.Financeiro.ColunasDoBanco> _lista;
        List<Classes.Financeiro.ColunasDoBanco> _listaLog;

        bool _alterar = false;

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

        private void Bancos_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            EmpresaConsolidadaComboBox.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            EmpresaConsolidadaComboBox.DisplayMember = "CodigoeNome";
            EmpresaConsolidadaComboBox.Focus();

            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
            }

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
                this.gridGroupingControl.SetMetroStyle(metroColor);
                this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            //this.gridGroupingControl.Table.DefaultRecordRowHeight = 30;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            Bancos_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
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
            if (e.KeyCode == Keys.Down )
            {
                Publicas._setaParaBaixo = true;
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

        private void excluirColunasButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                limparButton.Focus();
            }
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

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void SaldoInicialTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
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

        private void excluirColunasButton_Enter(object sender, EventArgs e)
        {
            excluirButton.BackColor = Publicas._botaoFocado;
            excluirButton.ForeColor = Publicas._fonteBotaoFocado;
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
                new Pesquisas.Bancos().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _banco = new FinanceiroBO().ConsultarBancos(Convert.ToInt32(codigoTextBox.Text), _empresa.IdEmpresa);

            _lista = new List<Classes.Financeiro.ColunasDoBanco>();
            _listaLog = new List<Classes.Financeiro.ColunasDoBanco>();

            if (_banco.Existe)
            {
                ativoCheckBox.Checked = _banco.Ativo;
                ConsolidarCheckBox.Checked = _banco.Consolidar;

                nomeTextBox.Text = _banco.Nome;
                SaldoInicialTextBox.DecimalValue = _banco.SaldoInicial;
                if (_banco.CodigoBanco != 0)
                    CodigoBancoTextBox.Text = _banco.CodigoBanco.ToString();
                if (_banco.CodigoAgencia != 0)
                    CodigoAgenciaTextBox.Text = _banco.CodigoAgencia.ToString();

                if (_banco.IdEmpresaConsolidado != 0)
                {
                    for (int i = 0; i < EmpresaConsolidadaComboBox.Items.Count; i++)
                    {
                        _empresaConsolidada = new EmpresaBO().Consultar(_banco.IdEmpresaConsolidado);
                        EmpresaConsolidadaComboBox.SelectedIndex = i;
                        if (EmpresaConsolidadaComboBox.Text == _empresaConsolidada.CodigoeNome)
                        {
                            break;
                        }                        
                    }
                }

                if (_banco.IdBancoConsolidado != 0)
                {
                    _bancoConsolidado = new FinanceiroBO().ConsultarBancos(_banco.IdBancoConsolidado);
                    BancoConsolidadoTextBox.Text = _bancoConsolidado.Codigo.ToString();
                }

                CodigoContaTextBox.Text = _banco.CodigoConta;

                if (_banco.CodigoBancoCartoes != 0)
                    BancoCartoesTextBox.Text = _banco.CodigoBancoCartoes.ToString();
                if (_banco.CodigoAgenciaCartoes != 0)
                    AgenciaCartoesTextBox.Text = _banco.CodigoAgenciaCartoes.ToString();

                ContaCartoesTextBox.Text = _banco.CodigoContaCartoes;
                BancosCartoesCheckBox.Checked = _banco.CodigoBancoCartoes != 0;

                _lista = new FinanceiroBO().ListarColunasDoBanco(_banco.Id, false);
                _listaLog = new FinanceiroBO().ListarColunasDoBanco(_banco.Id, false);
            }

            gridGroupingControl.DataSource = _lista;
            gridGroupingControl.Table.ExpandAllGroups();
            excluirButton.Enabled = _banco.Existe;
            CopiarButton.Enabled = _banco.Existe;
            gravarButton.Enabled = true;

            if (Publicas._idRetornoPesquisa != 0)
                nomeTextBox.Focus();
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

        private void SaldoInicialTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void ColunaTextBox_Validating(object sender, CancelEventArgs e)
        {
            ColunaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (ColunaTextBox.Text.Trim() == "")
            {
                new Pesquisas.Colunas().ShowDialog();

                ColunaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (ColunaTextBox.Text.Trim() == "" || ColunaTextBox.Text.Trim() == "0")
                {
                    ColunaTextBox.Text = string.Empty;
                    ColunaTextBox.Focus();
                    return;
                }
            }

            _colunas = new FinanceiroBO().Consultar(Convert.ToInt32(ColunaTextBox.Text));

            if (!_colunas.Existe)
            {
                new Notificacoes.Mensagem("Coluna não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ColunaTextBox.Focus();
                return;
            }

            if (!_colunas.Ativo)
            {
                new Notificacoes.Mensagem("Coluna inativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ColunaTextBox.Focus();
                return;
            }

            NomeColunaTextBox.Text = _colunas.Nome;
            label5.Text = (_colunas.Tipo == "EN" || _colunas.Tipo == "TE" ? "Tipos de Receita" : "Tipos de Despesa");
            
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
                EditarColunasPanel.Visible = false;
                Publicas._setaParaBaixo = false;
                return;
            }

            if (TipoTextBox.Text.Trim() == "")
            {
                Pesquisas.Despesas _tela = new Pesquisas.Despesas();
                Publicas._tipoDespesaReceita = (_colunas.Tipo == "EN" || _colunas.Tipo == "TE" ? "R" : "D");
                _tela.tituloLabel.Text = "Pesquisa de " + (_colunas.Tipo == "EN" || _colunas.Tipo == "TE" ? "Receita" : "Despesa");
                _tela.ShowDialog();

                TipoTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (TipoTextBox.Text.Trim() == "" || TipoTextBox.Text.Trim() == "0")
                {
                    TipoTextBox.Text = string.Empty;
                    TipoTextBox.Focus();
                    return;
                }
            }

            _despesas = new FinanceiroBO().Consultar(TipoTextBox.Text, (_colunas.Tipo == "EN" || _colunas.Tipo == "TE" ? "R" : "D"));

            if (!_despesas.Existe)
            {
                new Notificacoes.Mensagem((_colunas.Tipo == "EN" || _colunas.Tipo == "TE" ? "Receita" : "Despesa") + " não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                TipoTextBox.Focus();
                return;
            }

            if (!_despesas.AceitaLancamento)
            {
                new Notificacoes.Mensagem((_colunas.Tipo == "EN" || _colunas.Tipo == "TE" ? "Receita" : "Despesa") + " não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                TipoTextBox.Focus();
                return;
            }

            NomeTipoTextBox.Text = _despesas.Descricao;
        }


        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
        }

        private void CopiaButton_Validating(object sender, CancelEventArgs e)
        {
            CopiarButton.BackColor = Publicas._botao;
            CopiarButton.ForeColor = Publicas._fonteBotao;
        }

        private void IncluirColunasButton_Click(object sender, EventArgs e)
        {
            bool encontrou = false;
            int id = 0;

            gridGroupingControl.DataSource = new List<Classes.Financeiro.ColunasDoBanco>();

            foreach (var item in _lista.Where(w => w.IdColuna == _colunas.Id && w.TipoCodigo == TipoTextBox.Text))
            {
                item.Ativo = ColumaAtivaCheckBox.Checked;
                item.TipoNome = NomeTipoTextBox.Text;
                item.Tipo = _colunas.Tipo;
                id = item.Id;
                encontrou = true;
            }

            if (!encontrou)
            {
                Classes.Financeiro.ColunasDoBanco _col = new Classes.Financeiro.ColunasDoBanco();
                _col.Ativo = ColumaAtivaCheckBox.Checked;
                _col.TipoNome = NomeTipoTextBox.Text;
                _col.TipoCodigo = TipoTextBox.Text;
                _col.IdColuna = _colunas.Id;
                _col.IdBanco = _banco.Id;
                _col.Nome = NomeColunaTextBox.Text;
                _col.Selecionado = true;
                _col.Tipo = _colunas.Tipo;
                _lista.Add(_col);
            }

            foreach (var item in _lista.Where(w => w.IdColuna == _colunas.Id))
            {
                if (item.Id != 0)
                {
                    encontrou = item.Existe;
                    id = item.Id;
                }

                item.Ativo = ColumaAtivaCheckBox.Checked;
                item.Id = id;
                item.Existe = encontrou;
            }

            gridGroupingControl.DataSource = _lista;
            gridGroupingControl.Table.ExpandAllGroups();
            NomeTipoTextBox.Text = "";
            TipoTextBox.Text = "";

            if (_alterar)
            {
                EditarColunasPanel.Visible = false;
                _alterar = false;
                return;
            }
            TipoTextBox.Focus();
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (codigoTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                new Pesquisas.Bancos().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaColunasButton_Click(object sender, EventArgs e)
        {
            if (ColunaTextBox.Text.Trim() == "")
            {
                new Pesquisas.Colunas().ShowDialog();

                ColunaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (ColunaTextBox.Text.Trim() == "" || ColunaTextBox.Text.Trim() == "0")
                {
                    ColunaTextBox.Text = string.Empty;
                    ColunaTextBox.Focus();
                    return;
                }

                ColunaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaTipoButton_Click(object sender, EventArgs e)
        {
            if (TipoTextBox.Text.Trim() == "")
            {
                Pesquisas.Despesas _tela = new Pesquisas.Despesas();
                Publicas._tipoDespesaReceita = (_colunas.Tipo == "EN" || _colunas.Tipo == "TE" ? "R" : "D");
                _tela.tituloLabel.Text = "Pesquisa de " + (_colunas.Tipo == "EN" || _colunas.Tipo == "TE" ? "Receita" : "Despesa");
                _tela.ShowDialog();

                TipoTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (TipoTextBox.Text.Trim() == "" || TipoTextBox.Text.Trim() == "0")
                {
                    TipoTextBox.Text = string.Empty;
                    TipoTextBox.Focus();
                    return;
                }

                TipoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new FinanceiroBO().ProximoBanco(_empresa.IdEmpresa).ToString();
            ativoCheckBox.Focus();
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl.DataSource = new List<Classes.Financeiro.ColunasDoBanco>();

            TipoTextBox.Text = string.Empty;
            NomeTipoTextBox.Text = string.Empty;
            ColunaTextBox.Text = string.Empty;
            NomeColunaTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            codigoTextBox.Text = string.Empty;
            SaldoInicialTextBox.DecimalValue = 0;
            EditarColunasPanel.Visible = false;
            CodigoBancoTextBox.Text = string.Empty;
            CodigoAgenciaTextBox.Text = string.Empty;
            CodigoContaTextBox.Text = string.Empty;
            BancoConsolidadoTextBox.Text = string.Empty;
            BancoCartoesTextBox.Text = string.Empty;
            AgenciaCartoesTextBox.Text = string.Empty;
            ContaCartoesTextBox.Text = string.Empty;
            ConsolidarCheckBox.Checked = false;
            BancosCartoesCheckBox.Checked = false;
            codigoTextBox.Focus();

            excluirButton.Enabled = false;
            gravarButton.Enabled = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditarColunasPanel.Visible = true;
            ColunaTextBox.Focus();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[gridGroupingControl.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    _alterar = true;
                    EditarColunasPanel.Visible = true;
                    ColunaTextBox.Text = Convert.ToString((int)dr["IdColuna"]);
                    ColunaTextBox_Validating(sender, new CancelEventArgs());
                    TipoTextBox.Text = (string)dr["TipoCodigo"];
                    TipoTextBox_Validating(sender, new CancelEventArgs());

                    ColumaAtivaCheckBox.Checked = (bool)dr["Ativo"];
                    TipoTextBox.Focus();
                }
            }
        }

        private void excluirTiposAssociadosAColunaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[gridGroupingControl.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            int IdColuna = 0;
            string Tipo = "";
            int posIniId = 0;
            int posFimId = 0;

            if (rec != null)
            {
                
                Record dr = rec.GetRecord() as Record;
                if (dr == null)
                {
                    new Notificacoes.Mensagem("Não foi possível excluir." + Environment.NewLine + "Tente selecionar novamente o item a ser excluído.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
                else
                {
                    try
                    {
                        ColunaTextBox.Text = Convert.ToString((int)dr["IdColuna"]);
                        TipoTextBox.Text = (string)dr["TipoCodigo"];
                    }
                    catch
                    {
                        posIniId = dr.Info.IndexOf("IdColuna =") + 10;
                        posFimId = dr.Info.IndexOf(", Nome");
                        IdColuna = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        ColunaTextBox.Text = IdColuna.ToString();

                        posIniId = dr.Info.IndexOf("TipoCodigo =") + 12;
                        posFimId = dr.Info.IndexOf(", IdAssociado");
                        Tipo = dr.Info.Substring(posIniId, posFimId - posIniId).Trim();
                        TipoTextBox.Text = Tipo;
                    }

                    if (ColunaTextBox.Text != "")
                        ColunaTextBox_Validating(sender, new CancelEventArgs());

                    if (TipoTextBox.Text != "")
                        TipoTextBox_Validating(sender, new CancelEventArgs());

                    if (new Notificacoes.Mensagem("Confirma a exclusão do código " + TipoTextBox.Text + " ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.Financeiro.ColunasDoBanco _excluirTipos = new Classes.Financeiro.ColunasDoBanco();
                    gridGroupingControl.DataSource = new List<Classes.Financeiro.ColunasDoBanco>();

                    foreach (var item in _lista.Where(w => w.IdColuna.ToString() == ColunaTextBox.Text && w.TipoCodigo == TipoTextBox.Text))
                    {
                        _excluirTipos = item;

                        if (item.IdAssociado != 0)
                        {
                            if (!new FinanceiroBO().ExcluirTipos(item.IdAssociado))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _lista.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu a Tipo " + TipoTextBox.Text + " " + NomeTipoTextBox.Text + " que estava associada a coluna " + (string)dr["Nome"] +
                        " do banco " + codigoTextBox.Text + " " + nomeTextBox.Text + " da empresa " + empresaComboBoxAdv.Text;
                    _log.Tela = "Financeiro - Cadastros - Bancos";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl.DataSource = _lista;
                gridGroupingControl.Table.ExpandAllGroups();
                NomeTipoTextBox.Text = "";
                TipoTextBox.Text = "";
            }
        }
        
        private void excluirColunasESeusTiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[gridGroupingControl.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                int IdColuna = 0;
                int posIniId = 0;
                int posFimId = 0;

                Record dr = rec.GetRecord() as Record;
                if (dr == null)
                {
                    new Notificacoes.Mensagem("Nãofoi possível excluir." + Environment.NewLine + "Tente selecionar novamente o item a ser excluído.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
                else
                {
                    try
                    {
                        ColunaTextBox.Text = Convert.ToString((int)dr["IdColuna"]);
                    }
                    catch
                    {
                        posIniId = dr.Info.IndexOf("IdColuna =") + 10;
                        posFimId = dr.Info.IndexOf(", Nome");
                        IdColuna = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        ColunaTextBox.Text = IdColuna.ToString();
                    }

                    if (ColunaTextBox.Text != "")
                        ColunaTextBox_Validating(sender, new CancelEventArgs());

                    if (new Notificacoes.Mensagem("Confirma a exclusão da coluna " + ColunaTextBox.Text + " " + (string)dr["Nome"] + " ?" +
                        Environment.NewLine + Environment.NewLine + "Irá excluir todos os tipos associados a essa coluna!", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    List<Classes.Financeiro.ColunasDoBanco> _excluirTipos = new List<Classes.Financeiro.ColunasDoBanco>();
                    gridGroupingControl.DataSource = new List<Classes.Financeiro.ColunasDoBanco>();

                    foreach (var item in _lista.Where(w => w.IdColuna.ToString() == ColunaTextBox.Text))
                    {
                        _excluirTipos.Add(item);

                        if (item.Id != 0)
                        {
                            if (!new FinanceiroBO().ExcluirColunas(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    if (!new FinanceiroBO().ExcluirColunas(Convert.ToInt32(ColunaTextBox.Text)))
                    {
                        new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }

                    string _descricao = "";
                    foreach (var item in _excluirTipos)
                    {
                        _descricao = _descricao + "[" + item.TipoCodigo + " " + item.TipoNome + "] ";
                        _lista.Remove(item);
                    }

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu a Coluna " + ColunaTextBox.Text + " " + (string)dr["Nome"] + " junto com os tipos " + _descricao +
                        " do banco " + codigoTextBox.Text + " " + nomeTextBox.Text + " da empresa " + empresaComboBoxAdv.Text;
                    _log.Tela = "Financeiro - Cadastros - Bancos";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl.DataSource = _lista;
                gridGroupingControl.Table.ExpandAllGroups();
                NomeTipoTextBox.Text = "";
                TipoTextBox.Text = "";
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CodigoBancoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe os dados bancários do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoBancoTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(CodigoAgenciaTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe os dados bancários do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoAgenciaTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(CodigoContaTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe os dados bancários do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoContaTextBox.Focus();
                return;
            }

            Log _log = new Log(); 
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (_banco.Existe ? "Alterou" : "Incluiu") + " o banco " + codigoTextBox.Text + " da empresa " + empresaComboBoxAdv.Text +
                  (_banco.Nome == nomeTextBox.Text ? "" : " [Nome] de " + _banco.Nome + " para " + nomeTextBox.Text) +
                  (_banco.Ativo == ativoCheckBox.Checked ? "" : " [Ativo] de " + _banco.Ativo + " para " + ativoCheckBox.Checked) +
                  (_banco.Consolidar == ConsolidarCheckBox.Checked ? "" : " [Consolidar] de " + _banco.Consolidar + " para " + ConsolidarCheckBox.Checked) +
                  (_empresaConsolidada == null ? "" : 
                  (_banco.IdEmpresaConsolidado == _empresaConsolidada.IdEmpresa ? "" : "[Empresa Consolidada] de " + _banco.IdEmpresaConsolidado + " para " + _empresaConsolidada.IdEmpresa)) +

                  (_bancoConsolidado == null ? "" :
                  (_banco.IdBancoConsolidado == _bancoConsolidado.Id ? "" : "[Empresa Consolidada] de " + _banco.IdBancoConsolidado + " para " + _bancoConsolidado.Id)) +

                  (_banco.CodigoBanco.ToString() == CodigoBancoTextBox.Text ? "" : " [Bancos] de " + _banco.CodigoBanco + " para " + CodigoBancoTextBox.Text) +
                  (_banco.CodigoAgencia.ToString() == CodigoAgenciaTextBox.Text ? "" : " [Agência] de " + _banco.CodigoAgencia + " para " + CodigoAgenciaTextBox.Text) +
                  (_banco.CodigoConta == CodigoContaTextBox.Text ? "" : " [Conta] de " + _banco.CodigoConta + " para " + CodigoContaTextBox.Text) +
                  (_banco.CodigoBancoCartoes.ToString() == BancoCartoesTextBox.Text ? "" : " [Bancos Cartao] de " + _banco.CodigoBancoCartoes + " para " + BancoCartoesTextBox.Text) +
                  (_banco.CodigoAgenciaCartoes.ToString() == AgenciaCartoesTextBox.Text ? "" : " [Agência Cartao] de " + _banco.CodigoAgenciaCartoes + " para " + AgenciaCartoesTextBox.Text) +
                  (_banco.CodigoContaCartoes == ContaCartoesTextBox.Text ? "" : " [Conta Cartao] de " + _banco.CodigoContaCartoes + " para " + ContaCartoesTextBox.Text);

            _banco.IdEmpresa = _empresa.IdEmpresa;
            _banco.Codigo = Convert.ToInt32(codigoTextBox.Text);
            _banco.Nome = nomeTextBox.Text;
            _banco.Ativo = ativoCheckBox.Checked;
            _banco.SaldoInicial = SaldoInicialTextBox.DecimalValue;
            
            _banco.CodigoBanco = 0;
            _banco.CodigoAgencia = 0;

            try
            {
                _banco.CodigoBanco = Convert.ToInt32(CodigoBancoTextBox.Text);
                _banco.CodigoAgencia = Convert.ToInt32(CodigoAgenciaTextBox.Text);
            }
            catch {}
            _banco.CodigoConta = CodigoContaTextBox.Text;
            _banco.Consolidar = ConsolidarCheckBox.Checked;

            _banco.CodigoBancoCartoes = 0;
            _banco.CodigoAgenciaCartoes = 0;

            try
            {
                _banco.CodigoBancoCartoes = Convert.ToInt32(BancoCartoesTextBox.Text);
                _banco.CodigoAgenciaCartoes = Convert.ToInt32(AgenciaCartoesTextBox.Text);
            }
            catch { }

            _banco.CodigoContaCartoes = ContaCartoesTextBox.Text;

            if (ConsolidarCheckBox.Checked)
            {
                _banco.IdEmpresaConsolidado = _empresaConsolidada.IdEmpresa;
                _banco.IdBancoConsolidado = _bancoConsolidado.Id;
            }

            string _descricao = "";

            foreach (var item in _listaLog.GroupBy(g => new { g.IdColuna, g.Ativo, g.Nome }))
            {
                _descricao = " Coluna " + item.Key.Nome;

                foreach (var itemL in _lista.GroupBy(g => new { g.IdColuna, g.Ativo, g.Nome })
                                            .Where(w => w.Key.IdColuna == item.Key.IdColuna && w.Key.Ativo != item.Key.Ativo))
                    _descricao = (item.Key.Ativo == itemL.Key.Ativo ? "" : " [Ativa] de " + item.Key.Ativo + " para " + itemL.Key.Ativo) ;

                foreach (var itemG in _lista.GroupBy(g => new { g.TipoCodigo, g.IdColuna, g.TipoNome })
                                            .Where(w => w.Key.IdColuna == item.Key.IdColuna))
                {
                    if (_listaLog.Where(w => w.IdColuna == item.Key.IdColuna && w.TipoCodigo == itemG.Key.TipoCodigo).Count() == 0)
                    {
                        if (!_descricao.Contains("Incluido o "))
                            _descricao = _descricao + " Incluido o Tipo ";
                        _descricao = _descricao + "[" + itemG.Key.TipoCodigo + " " + itemG.Key.TipoNome + "] ";
                    }
                }
            }

            if (!new FinanceiroBO().GravarBancos(_banco, _lista))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            _log.Descricao = _log.Descricao + _descricao;
            _log.Tela = "Financeiro - Cadastros - Bancos";

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

            if (!new FinanceiroBO().ExcluirBancos(_banco.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu o banco " + codigoTextBox.Text + " " + nomeTextBox.Text + " da empresa " + empresaComboBoxAdv.Text;

            string _descricao = "";

            foreach (var item in _listaLog.GroupBy(g => new { g.IdColuna, g.Ativo, g.Nome }))
            {
                _descricao = " Coluna " + item.Key.Nome;

                foreach (var itemG in _listaLog.GroupBy(g => new { g.TipoCodigo, g.IdColuna, g.TipoNome })
                                            .Where(w => w.Key.IdColuna == item.Key.IdColuna))
                {
                    if (_listaLog.Where(w => w.IdColuna == item.Key.IdColuna && w.TipoCodigo == itemG.Key.TipoCodigo).Count() == 0)
                    {
                        if (!_descricao.Contains("Excluido o "))
                            _descricao = _descricao + "Excluido o Tipo ";
                        _descricao = _descricao + "[" + itemG.Key.TipoCodigo + " " + itemG.Key.TipoNome + "] ";
                    }
                }
            }

            _log.Tela = "Financeiro - Cadastros - Bancos";
            _log.Descricao = _log.Descricao + _descricao;

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void ColunaTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaColunasButton.Enabled = string.IsNullOrEmpty(ColunaTextBox.Text.Trim());
        }

        private void TipoTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaTipoButton.Enabled = string.IsNullOrEmpty(TipoTextBox.Text.Trim());
        }

        private void Bancos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Publicas.AbrirFerramentaDeCapitura();
        }

        private void CopiarButton_Click(object sender, EventArgs e)
        {
            Financeiro.CopiaColunasETipos _tela = new CopiaColunasETipos();
            _tela._empresa = this._empresa;
            _tela._banco = this._banco;
            _tela._lista = this._lista;

            _tela.ShowDialog();
        }

        private void Bancos_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }

        private void CodigoBancoTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CodigoAgenciaTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CodigoContaTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CodigoBancoTextBox_Validating(object sender, CancelEventArgs e)
        {
            CodigoBancoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._idRetornoPesquisa = 0;
            if (ConsolidarCheckBox.Checked)
                Publicas._codigoEmpresaGlobus = _empresaConsolidada.CodigoEmpresaGlobus;
            else
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (CodigoBancoTextBox.Text.Trim() == "")
            {
                new Pesquisas.BancoGlobus().ShowDialog();

                CodigoBancoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CodigoBancoTextBox.Text.Trim() == "" || CodigoBancoTextBox.Text == "0")
                {
                    CodigoBancoTextBox.Text = string.Empty;
                    CodigoBancoTextBox.Focus();
                    return;
                }
            }

            _bancoGlobus = new FinanceiroBO().ConsultarBancosGlobus(Convert.ToInt32(CodigoBancoTextBox.Text));

            if (!_bancoGlobus.Existe)
            {
                new Notificacoes.Mensagem("Banco do Globus não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoBancoTextBox.Focus();
                return;
            }
        }

        private void CodigoAgenciaTextBox_Validating(object sender, CancelEventArgs e)
        {
            CodigoAgenciaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._idRetornoPesquisa = _bancoGlobus.Codigo;

            if (CodigoAgenciaTextBox.Text.Trim() == "")
            {
                new Pesquisas.AgenciaGlobus().ShowDialog();

                CodigoAgenciaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CodigoAgenciaTextBox.Text.Trim() == "" || CodigoAgenciaTextBox.Text == "0")
                {
                    CodigoAgenciaTextBox.Text = string.Empty;
                    CodigoAgenciaTextBox.Focus();
                    return;
                }
            }

            _agencia = new FinanceiroBO().ConsultarAgenciaGlobus(_bancoGlobus.Codigo, Convert.ToInt32(CodigoAgenciaTextBox.Text));

            if (!_agencia.Existe)
            {
                new Notificacoes.Mensagem("Agência do Globus não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoAgenciaTextBox.Focus();
                return;
            }
        }

        private void CodigoContaTextBox_Validating(object sender, CancelEventArgs e)
        {
            CodigoContaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._idRetornoPesquisa = _bancoGlobus.Codigo;
            Publicas._idInteiroAuxiliar = _agencia.Codigo;

            if (CodigoContaTextBox.Text.Trim() == "")
            {
                new Pesquisas.ContaBCOGlobus().ShowDialog();

                CodigoContaTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (CodigoContaTextBox.Text.Trim() == "" || CodigoContaTextBox.Text == "0")
                {
                    CodigoContaTextBox.Text = string.Empty;
                    CodigoContaTextBox.Focus();
                    return;
                }
            }

            string codigoEmpresaGlobus = "";

            if (ConsolidarCheckBox.Checked)
                codigoEmpresaGlobus = _empresaConsolidada.CodigoEmpresaGlobus;
            else
                codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;


            _conta = new FinanceiroBO().ConsultarContasGlobus(codigoEmpresaGlobus, _bancoGlobus.Codigo, _agencia.Codigo, CodigoContaTextBox.Text);

            if (!_conta.Existe)
            {
                new Notificacoes.Mensagem("Conta do Globus não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoContaTextBox.Focus();
                return;
            }
        }

        private void PesquisaBancosBcoButton_Click(object sender, EventArgs e)
        {
            Publicas._idRetornoPesquisa = 0;
            if (ConsolidarCheckBox.Checked)
                Publicas._codigoEmpresaGlobus = _empresaConsolidada.CodigoEmpresaGlobus;
            else
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (CodigoBancoTextBox.Text.Trim() == "")
            {
                new Pesquisas.BancoGlobus().ShowDialog();

                CodigoBancoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CodigoBancoTextBox.Text.Trim() == "" || CodigoBancoTextBox.Text == "0")
                {
                    CodigoBancoTextBox.Text = string.Empty;
                    CodigoBancoTextBox.Focus();
                    return;
                }

                CodigoBancoTextBox_Validating(sender, new CancelEventArgs());
            }

        }

        private void PesquisaAgenciaButton_Click(object sender, EventArgs e)
        {
            Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (CodigoAgenciaTextBox.Text.Trim() == "")
            {
                new Pesquisas.AgenciaGlobus().ShowDialog();

                CodigoAgenciaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CodigoAgenciaTextBox.Text.Trim() == "" || CodigoAgenciaTextBox.Text == "0")
                {
                    CodigoAgenciaTextBox.Text = string.Empty;
                    CodigoAgenciaTextBox.Focus();
                    return;
                }

                CodigoAgenciaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaContasButton_Click(object sender, EventArgs e)
        {
            if (ConsolidarCheckBox.Checked)
                Publicas._codigoEmpresaGlobus = _empresaConsolidada.CodigoEmpresaGlobus;
            else
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            Publicas._idRetornoPesquisa = _bancoGlobus.Codigo;
            Publicas._idInteiroAuxiliar = _agencia.Codigo;

            if (CodigoContaTextBox.Text.Trim() == "")
            {
                new Pesquisas.ContaBCOGlobus().ShowDialog();

                CodigoContaTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (CodigoContaTextBox.Text.Trim() == "" || CodigoContaTextBox.Text == "0")
                {
                    CodigoContaTextBox.Text = string.Empty;
                    CodigoContaTextBox.Focus();
                    return;
                }

                CodigoContaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void CodigoBancoTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaBancosBcoButton.Enabled = string.IsNullOrEmpty(CodigoBancoTextBox.Text.Trim());
        }

        private void CodigoAgenciaTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaAgenciaButton.Enabled = string.IsNullOrEmpty(CodigoAgenciaTextBox.Text.Trim());
        }

        private void CodigoContaTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaContasButton.Enabled = string.IsNullOrEmpty(CodigoContaTextBox.Text.Trim());
        }

        private void ConsolidarCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            ConsolidarPanel.Enabled = ConsolidarCheckBox.Checked;
        }

        private void EmpresaConsolidadaComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == EmpresaConsolidadaComboBox.Text))
            {
                _empresaConsolidada = item;
            }
        }

        private void BancoConsolidadoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (BancoConsolidadoTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresaConsolidada.IdEmpresa;
                new Pesquisas.Bancos().ShowDialog();

                BancoConsolidadoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (BancoConsolidadoTextBox.Text.Trim() == "" || BancoConsolidadoTextBox.Text.Trim() == "0")
                {
                    BancoConsolidadoTextBox.Text = string.Empty;
                    BancoConsolidadoTextBox.Focus();
                    return;
                }
            }

            _bancoConsolidado = new FinanceiroBO().ConsultarBancos(Convert.ToInt32(BancoConsolidadoTextBox.Text), _empresaConsolidada.IdEmpresa);

            if (!_bancoConsolidado.Existe)
            {
                new Notificacoes.Mensagem("Código do banco não cadastrado no suportte para a Empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                BancoConsolidadoTextBox.Focus();
                return;
            }

            if (!_bancoConsolidado.Ativo)
            {
                new Notificacoes.Mensagem("Código do banco não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                BancoConsolidadoTextBox.Focus();
                return;
            }
        }

        private void PesquisaBancoConsolidadoButton_Click(object sender, EventArgs e)
        {
                Publicas._idEmpresa = _empresaConsolidada.IdEmpresa;
                new Pesquisas.Bancos().ShowDialog();

                BancoConsolidadoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (BancoConsolidadoTextBox.Text.Trim() == "" || BancoConsolidadoTextBox.Text.Trim() == "0")
                {
                    BancoConsolidadoTextBox.Text = string.Empty;
                    BancoConsolidadoTextBox.Focus();
                    return;
                }

                BancoConsolidadoTextBox_Validating(sender, new CancelEventArgs());
        }

        private void BancoConsolidadoTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaBancoConsolidadoButton.Enabled = string.IsNullOrEmpty(BancoConsolidadoTextBox.Text.Trim());
        }

        private void BancosCartoesCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            BancoCartoesTextBox.Enabled = BancosCartoesCheckBox.Checked;
            AgenciaCartoesTextBox.Enabled = BancosCartoesCheckBox.Checked;
            ContaCartoesTextBox.Enabled = BancosCartoesCheckBox.Checked;
            PesquisaBancosCartoesButton.Enabled = BancosCartoesCheckBox.Checked;
            PesquisaAgenciaCartoesButton.Enabled = BancosCartoesCheckBox.Checked;
            PesquisaContaCartoesButton.Enabled = BancosCartoesCheckBox.Checked;
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void BancoCartoesTextBox_Validating(object sender, CancelEventArgs e)
        {
            BancoCartoesTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (ConsolidarCheckBox.Checked)
                Publicas._codigoEmpresaGlobus = _empresaConsolidada.CodigoEmpresaGlobus;
            else
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (BancoCartoesTextBox.Text.Trim() == "")
            {
                new Pesquisas.BancoGlobus().ShowDialog();

                BancoCartoesTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (BancoCartoesTextBox.Text.Trim() == "" || CodigoBancoTextBox.Text == "0")
                {
                    BancoCartoesTextBox.Text = string.Empty;
                    BancoCartoesTextBox.Focus();
                    return;
                }
            }

            _bancoCartoesGlobus = new FinanceiroBO().ConsultarBancosGlobus(Convert.ToInt32(BancoCartoesTextBox.Text));

            if (!_bancoCartoesGlobus.Existe)
            {
                new Notificacoes.Mensagem("Banco do Globus não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                BancoCartoesTextBox.Focus();
                return;
            }
        }

        private void PesquisaBancosCartoesButton_Click(object sender, EventArgs e)
        {
            Publicas._idRetornoPesquisa = 0;

            if (ConsolidarCheckBox.Checked)
                Publicas._codigoEmpresaGlobus = _empresaConsolidada.CodigoEmpresaGlobus;
            else
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (BancoCartoesTextBox.Text.Trim() == "")
            {
                new Pesquisas.BancoGlobus().ShowDialog();

                BancoCartoesTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (BancoCartoesTextBox.Text.Trim() == "" || BancoCartoesTextBox.Text == "0")
                {
                    BancoCartoesTextBox.Text = string.Empty;
                    BancoCartoesTextBox.Focus();
                    return;
                }

                BancoCartoesTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void AgenciaCartoesTextBox_Validating(object sender, CancelEventArgs e)
        {
            AgenciaCartoesTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._idRetornoPesquisa = _bancoCartoesGlobus.Codigo;

            if (AgenciaCartoesTextBox.Text.Trim() == "")
            {
                new Pesquisas.AgenciaGlobus().ShowDialog();

                AgenciaCartoesTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (AgenciaCartoesTextBox.Text.Trim() == "" || AgenciaCartoesTextBox.Text == "0")
                {
                    AgenciaCartoesTextBox.Text = string.Empty;
                    AgenciaCartoesTextBox.Focus();
                    return;
                }
            }

            _agenciaCartoes = new FinanceiroBO().ConsultarAgenciaGlobus(_bancoCartoesGlobus.Codigo, Convert.ToInt32(AgenciaCartoesTextBox.Text));

            if (!_agenciaCartoes.Existe)
            {
                new Notificacoes.Mensagem("Agência do Globus não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                AgenciaCartoesTextBox.Focus();
                return;
            }
        }

        private void PesquisaAgenciaCartoesButton_Click(object sender, EventArgs e)
        {
            Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (AgenciaCartoesTextBox.Text.Trim() == "")
            {
                new Pesquisas.AgenciaGlobus().ShowDialog();

                AgenciaCartoesTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (AgenciaCartoesTextBox.Text.Trim() == "" || AgenciaCartoesTextBox.Text == "0")
                {
                    AgenciaCartoesTextBox.Text = string.Empty;
                    AgenciaCartoesTextBox.Focus();
                    return;
                }

                AgenciaCartoesTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void ContaCartoesTextBox_Validating(object sender, CancelEventArgs e)
        {
            ContaCartoesTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._idRetornoPesquisa = _bancoCartoesGlobus.Codigo;
            Publicas._idInteiroAuxiliar = _agenciaCartoes.Codigo;

            if (ContaCartoesTextBox.Text.Trim() == "")
            {
                new Pesquisas.ContaBCOGlobus().ShowDialog();

                ContaCartoesTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (ContaCartoesTextBox.Text.Trim() == "" || ContaCartoesTextBox.Text == "0")
                {
                    ContaCartoesTextBox.Text = string.Empty;
                    ContaCartoesTextBox.Focus();
                    return;
                }
            }

            string codigoEmpresaGlobus = "";

            if (ConsolidarCheckBox.Checked)
                codigoEmpresaGlobus = _empresaConsolidada.CodigoEmpresaGlobus;
            else
                codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;


            _contaCartoes = new FinanceiroBO().ConsultarContasGlobus(codigoEmpresaGlobus, _bancoCartoesGlobus.Codigo, _agenciaCartoes.Codigo, ContaCartoesTextBox.Text);

            if (!_contaCartoes.Existe)
            {
                new Notificacoes.Mensagem("Conta do Globus não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaCartoesTextBox.Focus();
                return;
            }
        }

        private void PesquisaContaCartoesButton_Click(object sender, EventArgs e)
        {
            if (ConsolidarCheckBox.Checked)
                Publicas._codigoEmpresaGlobus = _empresaConsolidada.CodigoEmpresaGlobus;
            else
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            Publicas._idRetornoPesquisa = _bancoCartoesGlobus.Codigo;
            Publicas._idInteiroAuxiliar = _agenciaCartoes.Codigo;

            if (ContaCartoesTextBox.Text.Trim() == "")
            {
                new Pesquisas.ContaBCOGlobus().ShowDialog();

                ContaCartoesTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (ContaCartoesTextBox.Text.Trim() == "" || ContaCartoesTextBox.Text == "0")
                {
                    ContaCartoesTextBox.Text = string.Empty;
                    ContaCartoesTextBox.Focus();
                    return;
                }

                ContaCartoesTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void BancoCartoesTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaBancosCartoesButton.Enabled = string.IsNullOrEmpty(BancoCartoesTextBox.Text.Trim());
        }

        private void AgenciaCartoesTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaAgenciaCartoesButton.Enabled = string.IsNullOrEmpty(AgenciaCartoesTextBox.Text.Trim());
        }

        private void ContaCartoesTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaContasButton.Enabled = string.IsNullOrEmpty(ContaCartoesTextBox.Text.Trim());
        }
    }
}
