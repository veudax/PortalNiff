using Classes;
using Negocio;
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
    public partial class Variaveis : Form
    {
        public Variaveis()
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

                    AumentarPercentualCurrencyTextBox.PositiveColor = Publicas._fonte;
                    ReduzirPercentualCurrencyTextBox.PositiveColor = Publicas._fonte;
                    QuantidadeTextBox.PositiveColor = Publicas._fonte;
                    AumentarPercentualCurrencyTextBox.ZeroColor = Publicas._fonte;
                    ReduzirPercentualCurrencyTextBox.ZeroColor = Publicas._fonte;
                    QuantidadeTextBox.ZeroColor = Publicas._fonte;
                }
            }
            AumentarPercentualCurrencyTextBox.BackGroundColor = codigoTextBox.BackColor;
            ReduzirPercentualCurrencyTextBox.BackGroundColor = codigoTextBox.BackColor;
            QuantidadeTextBox.BackGroundColor = codigoTextBox.BackColor;
            Publicas._mensagemSistema = string.Empty;
        }

        #region Atributos
        Classes.Empresa _empresa;
        List<Classes.Empresa> _listaEmpresas;
        Classes.Financeiro.Colunas _colunas;
        List<Classes.Financeiro.Colunas> _listaColunas;
        Classes.Financeiro.Variaveis _variaveis;
        List<Classes.Financeiro.Variaveis> _listaVariaveis;
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

        private void Variaveis_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

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

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultCaptionRowHeight = 27;
            
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void Variaveis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Publicas.AbrirFerramentaDeCapitura();
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ColunaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void ColunaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                MesesRadioButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void MesesRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                QuantidadeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ColunaTextBox.Focus();
            }
        }

        private void QuantidadeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (FeriadoPorAnoCheckBox.Enabled)
                    FeriadoPorAnoCheckBox.Focus();
                else
                {
                    if (EmendaSabadoCheckBox.Enabled)
                        EmendaSabadoCheckBox.Focus();
                    else
                    {
                        if (FeriaDinheiroCheckBox.Enabled)
                            FeriaDinheiroCheckBox.Focus();
                        else
                            ReduzirRadioButton.Focus();
                    }
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                MesesRadioButton.Focus();
            }
        }

        private void FeriadoPorAnoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (EmendaSabadoCheckBox.Enabled)
                    EmendaSabadoCheckBox.Focus();
                else
                {
                    if (FeriaDinheiroCheckBox.Enabled)
                        FeriaDinheiroCheckBox.Focus();
                    else
                        ReduzirRadioButton.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                QuantidadeTextBox.Focus();
            }
        }

        private void EmendaSabadoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (FeriaDinheiroCheckBox.Enabled)
                    FeriaDinheiroCheckBox.Focus();
                else
                    ReduzirRadioButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (FeriadoPorAnoCheckBox.Enabled)
                    FeriadoPorAnoCheckBox.Focus();
                else
                    QuantidadeTextBox.Focus();
            }
        }

        private void FeriaDinheiroCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReduzirRadioButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (EmendaSabadoCheckBox.Enabled)
                    EmendaSabadoCheckBox.Focus();
                else
                {
                    if (FeriadoPorAnoCheckBox.Enabled)
                        FeriadoPorAnoCheckBox.Focus();
                    else
                        QuantidadeTextBox.Focus();
                }
            }
        }

        private void ReduzirRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (ReduzirPercentualCurrencyTextBox.Enabled)
                    ReduzirPercentualCurrencyTextBox.Focus();
                else
                    AumentarPercentualCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (FeriaDinheiroCheckBox.Enabled)
                    FeriaDinheiroCheckBox.Focus();
                else
                {
                    if (EmendaSabadoCheckBox.Enabled)
                        EmendaSabadoCheckBox.Focus();
                    else
                    {
                        if (FeriadoPorAnoCheckBox.Enabled)
                            FeriadoPorAnoCheckBox.Focus();
                        else
                            QuantidadeTextBox.Focus();
                    }
                }
            }
        }

        private void AumentarRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (AumentarPercentualCurrencyTextBox.Enabled)
                    AumentarPercentualCurrencyTextBox.Focus();
                else
                    ReduzirPercentualCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (FeriaDinheiroCheckBox.Enabled)
                    FeriaDinheiroCheckBox.Focus();
                else
                {
                    if (EmendaSabadoCheckBox.Enabled)
                        EmendaSabadoCheckBox.Focus();
                    else
                    {
                        if (FeriadoPorAnoCheckBox.Enabled)
                            FeriadoPorAnoCheckBox.Focus();
                        else
                            QuantidadeTextBox.Focus();
                    }
                }
            }
        }

        private void ReduzirPercentualCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReduzirRadioButton.Focus();
            }
        }

        private void AumentarPercentualCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AumentarRadioButton.Focus();
            }
        }

        private void AnoRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                QuantidadeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ColunaTextBox.Focus();
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

        private void QuantidadeTextBox_Enter(object sender, EventArgs e)
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
                new Pesquisas.Variaveis().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _variaveis = new FinanceiroBO().Consultar( _empresa.IdEmpresa, Convert.ToInt32(codigoTextBox.Text));
            _listaVariaveis = new FinanceiroBO().Listar(_empresa.IdEmpresa, Convert.ToInt32(codigoTextBox.Text), true);

            _listaColunas = new FinanceiroBO().Listar(true);

            foreach (var item in _listaVariaveis)
            {
                foreach (var itemc in _listaColunas.Where(w => w.Id == item.IdColuna))
                {
                    itemc.Marcado = true;
                }
            }

            gridGroupingControl1.DataSource = _listaColunas;

            if (_variaveis.Existe)
            {
                ativoCheckBox.Checked = _variaveis.Ativo;
                nomeTextBox.Text = _variaveis.Nome;
                QuantidadeTextBox.DecimalValue = _variaveis.Quantidade;
                MesesRadioButton.Checked = _variaveis.CalculoPor == "M";
                AnoRadioButton.Checked = _variaveis.CalculoPor == "A";
                FeriadoPorAnoCheckBox.Checked = _variaveis.FeriadoPorAno;
                EmendaSabadoCheckBox.Checked = _variaveis.EmendaSabado;
                FeriaDinheiroCheckBox.Checked = _variaveis.FeriaDinheiro;
                ReduzirRadioButton.Checked = _variaveis.Reduzir;
                AumentarRadioButton.Checked = _variaveis.Aumentar;
                FinalDeSemanaCheckBox.Checked = _variaveis.CalcularFinaisDeSemana;
                NenhumRadioButton.Checked = !ReduzirRadioButton.Checked && !AumentarRadioButton.Checked;
                ReduzirPercentualCurrencyTextBox.DecimalValue = _variaveis.PercentualReduzir;
                AumentarPercentualCurrencyTextBox.DecimalValue = _variaveis.PercentualAumentar;
                ColunaTextBox.Text = _variaveis.IdColuna.ToString();

                //ColunaTextBox_Validating(sender, e);
            }

            excluirButton.Enabled = _variaveis.Existe;
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
            FeriaDinheiroCheckBox.Enabled = _colunas.Origem == "ARR";
        }

        private void QuantidadeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void ReduzirPercentualCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void AumentarPercentualCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
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

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void ColunaTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaColunasButton.Enabled = string.IsNullOrEmpty(ColunaTextBox.Text.Trim());
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new FinanceiroBO().ProximoCodigoVariavel(_empresa.IdEmpresa).ToString();
            ativoCheckBox.Focus();
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (codigoTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                new Pesquisas.Variaveis().ShowDialog();

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

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (_variaveis.Existe ? "Alterou" : "Incluiu") + " a regra de cálculo de média " + codigoTextBox.Text +
                (_variaveis.Nome == nomeTextBox.Text ? "" : " [Nome] de " + _variaveis.Nome + " para " + nomeTextBox.Text) +
                (_variaveis.Ativo == ativoCheckBox.Checked ? "" : " [Ativo] de " + _variaveis.Ativo + " para " + ativoCheckBox.Checked) +
                (_variaveis.CalculoPor == (MesesRadioButton.Checked ? "M" : "A") ? "" : " [CalculoPor] de " + _variaveis.CalculoPor + " para " + (MesesRadioButton.Checked ? "M" : "A")) +
                (_variaveis.Quantidade == QuantidadeTextBox.DecimalValue ? "" : " [Quantidade] de " + _variaveis.Quantidade + " para " + QuantidadeTextBox.DecimalValue) +
                (_variaveis.FeriadoPorAno == FeriadoPorAnoCheckBox.Checked ? "" : " [FeriadoPorAno] de " + _variaveis.FeriadoPorAno + " para " + FeriadoPorAnoCheckBox.Checked) +
                (_variaveis.EmendaSabado == EmendaSabadoCheckBox.Checked ? "" : " [Emenda] de " + _variaveis.FeriadoPorAno + " para " + EmendaSabadoCheckBox.Checked) +
                (_variaveis.FeriaDinheiro == FeriaDinheiroCheckBox.Checked ? "" : " [FeriaDinheiro] de " + _variaveis.FeriaDinheiro + " para " + FeriaDinheiroCheckBox.Checked) +
                (_variaveis.Reduzir == ReduzirRadioButton.Checked ? "" : " [Reduzir] de " + _variaveis.Reduzir + " para " + ReduzirRadioButton.Checked) +
                (_variaveis.Aumentar == AumentarRadioButton.Checked ? "" : " [Aumentar] de " + _variaveis.Aumentar + " para " + AumentarRadioButton.Checked) +
                (_variaveis.PercentualReduzir == ReduzirPercentualCurrencyTextBox.DecimalValue ? "" : " [PercentualReduzir] de " + _variaveis.PercentualReduzir + " para " + ReduzirPercentualCurrencyTextBox.DecimalValue) +
                (_variaveis.PercentualAumentar == AumentarPercentualCurrencyTextBox.DecimalValue ? "" : " [PercentualAumentar] de " + _variaveis.Aumentar + " para " + AumentarPercentualCurrencyTextBox.DecimalValue) +
                (_variaveis.CalcularFinaisDeSemana == FinalDeSemanaCheckBox.Checked ? "" : " [CalcularFinaisDeSemana] de " + _variaveis.CalcularFinaisDeSemana + " para " + FinalDeSemanaCheckBox.Checked);
                ;

            foreach (var item in _listaColunas.Where(w => w.Marcado))
            {
                _variaveis = new FinanceiroBO().Consultar(_empresa.IdEmpresa, Convert.ToInt32(codigoTextBox.Text), item.Id);

                _variaveis.Ativo = ativoCheckBox.Checked;
                _variaveis.Nome = nomeTextBox.Text;
                _variaveis.IdColuna = item.Id;
                _variaveis.IdEmpresa = _empresa.IdEmpresa;
                _variaveis.Codigo = Convert.ToInt32(codigoTextBox.Text);
                _variaveis.CalculoPor = (MesesRadioButton.Checked ? "M" : "A");
                _variaveis.Quantidade = Convert.ToInt32(QuantidadeTextBox.DecimalValue);
                _variaveis.FeriadoPorAno = FeriadoPorAnoCheckBox.Checked;
                _variaveis.EmendaSabado = EmendaSabadoCheckBox.Checked;
                _variaveis.FeriaDinheiro = FeriaDinheiroCheckBox.Checked;
                _variaveis.CalcularFinaisDeSemana = FinalDeSemanaCheckBox.Checked;
                _variaveis.Reduzir = ReduzirRadioButton.Checked;
                _variaveis.Aumentar = AumentarRadioButton.Checked;
                _variaveis.PercentualAumentar = AumentarPercentualCurrencyTextBox.DecimalValue;
                _variaveis.PercentualReduzir = ReduzirPercentualCurrencyTextBox.DecimalValue;

                if (!new FinanceiroBO().Gravar(_variaveis))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            foreach (var item in _listaColunas.Where(w => !w.Marcado))
            {
                _variaveis = new FinanceiroBO().Consultar(_empresa.IdEmpresa, Convert.ToInt32(codigoTextBox.Text), item.Id);

                if (_variaveis.Existe)
                {
                    _variaveis.Ativo = false;
                    _variaveis.Nome = nomeTextBox.Text;
                    _variaveis.IdColuna = item.Id;
                    _variaveis.IdEmpresa = _empresa.IdEmpresa;
                    _variaveis.Codigo = Convert.ToInt32(codigoTextBox.Text);
                    _variaveis.CalculoPor = (MesesRadioButton.Checked ? "M" : "A");
                    _variaveis.Quantidade = Convert.ToInt32(QuantidadeTextBox.DecimalValue);
                    _variaveis.FeriadoPorAno = FeriadoPorAnoCheckBox.Checked;
                    _variaveis.EmendaSabado = EmendaSabadoCheckBox.Checked;
                    _variaveis.FeriaDinheiro = FeriaDinheiroCheckBox.Checked;
                    _variaveis.CalcularFinaisDeSemana = FinalDeSemanaCheckBox.Checked;
                    _variaveis.Reduzir = ReduzirRadioButton.Checked;
                    _variaveis.Aumentar = AumentarRadioButton.Checked;
                    _variaveis.PercentualAumentar = AumentarPercentualCurrencyTextBox.DecimalValue;
                    _variaveis.PercentualReduzir = ReduzirPercentualCurrencyTextBox.DecimalValue;

                    if (!new FinanceiroBO().Gravar(_variaveis))
                    {
                        new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }
                }
            }


            _log.Tela = "Financeiro - Cadastros - Variaveis";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            ColunaTextBox.Text = string.Empty;
            NomeColunaTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            codigoTextBox.Text = string.Empty;
            QuantidadeTextBox.DecimalValue = 0;
            EmendaSabadoCheckBox.Checked = false;
            FeriaDinheiroCheckBox.Checked = false;
            FeriadoPorAnoCheckBox.Checked = false;
            FinalDeSemanaCheckBox.Checked = false;
            AumentarPercentualCurrencyTextBox.DecimalValue = 0;
            ReduzirPercentualCurrencyTextBox.DecimalValue = 0;
            _listaColunas.Clear();

            gridGroupingControl1.DataSource = new List<Financeiro.Colunas>();

            codigoTextBox.Focus();

            excluirButton.Enabled = false;
            gravarButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new FinanceiroBO().ExcluirVariavel(_variaveis.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu a regra de cálculo de média " + codigoTextBox.Text + " " + nomeTextBox.Text + " da empresa " + empresaComboBoxAdv.Text;

            _log.Tela = "Financeiro - Cadastros - Variaveis";


            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void MesesRadioButton_CheckChanged(object sender, EventArgs e)
        {
            FeriadoPorAnoCheckBox.Enabled = MesesRadioButton.Checked;
            FeriadoPorAnoCheckBox.Checked = FeriadoPorAnoCheckBox.Checked || AnoRadioButton.Checked;
            MesesRetroativoLabel.Text = (MesesRadioButton.Checked ? "meses" : "anos") + " retroativos";
        }

        private void ReduzirRadioButton_CheckChanged(object sender, EventArgs e)
        {
            ReduzirPercentualCurrencyTextBox.Enabled = ReduzirRadioButton.Checked;
            AumentarPercentualCurrencyTextBox.Enabled = AumentarRadioButton.Checked;
            if (!AumentarRadioButton.Checked)
                AumentarPercentualCurrencyTextBox.DecimalValue = 0;
            if (!ReduzirRadioButton.Checked)
                ReduzirPercentualCurrencyTextBox.DecimalValue = 0;
        }

        private void NenhumRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            Variaveis_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (ReduzirPercentualCurrencyTextBox.Enabled)
                    ReduzirPercentualCurrencyTextBox.Focus();
                else
                {
                    if (AumentarPercentualCurrencyTextBox.Enabled)
                        AumentarPercentualCurrencyTextBox.Focus();
                    else
                        gravarButton.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (FeriaDinheiroCheckBox.Enabled)
                    FeriaDinheiroCheckBox.Focus();
                else
                {
                    if (EmendaSabadoCheckBox.Enabled)
                        EmendaSabadoCheckBox.Focus();
                    else
                    {
                        if (FeriadoPorAnoCheckBox.Enabled)
                            FeriadoPorAnoCheckBox.Focus();
                        else
                            QuantidadeTextBox.Focus();
                    }
                }
            }
        }
    }
}
