using Classes;
using Negocio;
using Suportte.Notificacoes;
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

namespace Suportte.Cadastros
{
    public partial class Pontuacao : Form
    {
        public Pontuacao()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Pontuacao _pontuacao;
        List<PontuacaoFatorEmpresa> _fatores;
        List<Classes.Empresa> _listaEmpresas;

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

        private void quantidadeFaltasAplicarCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void maskedEditBox1_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
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

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (copiarCheckBox.Enabled)
                    copiarCheckBox.Focus();
                else
                    base100CurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void copiarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (referenciaCopiaMaskedEditBox.Visible)
                    referenciaCopiaMaskedEditBox.Focus();
                else
                    base100CurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }

        }

        private void referenciaCopiaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                base100CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                copiarCheckBox.Focus();
            }
        }

        private void naoAtendeCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                atendeParcialmenteCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pesoNumericaCurrencyTextBox.Focus();
            }
        }

        private void base100CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pisoCurrencyText.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (referenciaCopiaMaskedEditBox.Visible)
                    referenciaCopiaMaskedEditBox.Focus();
                else
                    referenciaMaskedEditBox.Focus();
            }
        }

        private void atendeParcialmenteCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                atendePlenamenteCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                naoAtendeCurrencyTextBox.Focus();
            }
        }

        private void atendePlenamenteCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                superaCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                atendeParcialmenteCurrencyTextBox.Focus();
            }
        }

        private void superaCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                atendePlenamenteCurrencyTextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pesoQualitativaCurrencyText.Focus();
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

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {

                Pesquisas.Pontuacao _pesquisa = new Pesquisas.Pontuacao();
                _pesquisa.ShowDialog();

                referenciaMaskedEditBox.Text = Publicas._idRetornoPesquisa.ToString("000000");

                if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()) || referenciaMaskedEditBox.ClipText == "000000")
                {
                    referenciaMaskedEditBox.Text = string.Empty;
                    referenciaMaskedEditBox.Focus();
                    return;
                }
            }

            _pontuacao = new PontuacaoBO().Consultar(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim()));
            _fatores = new List<PontuacaoFatorEmpresa>();

            if (_pontuacao.Existe)
            {
                _fatores = new PontuacaoBO().Listar(_pontuacao.Id);
                base100CurrencyTextBox.DecimalValue = _pontuacao.Base100;
                naoAtendeCurrencyTextBox.DecimalValue = _pontuacao.NaoAtende;
                atendeParcialmenteCurrencyTextBox.DecimalValue = _pontuacao.AtendeParcialmente;
                atendePlenamenteCurrencyTextBox.DecimalValue = _pontuacao.AtendePlenamente;
                superaCurrencyTextBox.DecimalValue = _pontuacao.Supera;
                pisoCurrencyText.DecimalValue = _pontuacao.Piso;
                pesoQualitativaCurrencyText.DecimalValue = _pontuacao.PesoQualitativa;
                pesoNumericaCurrencyTextBox.DecimalValue = _pontuacao.PesoNumerica;
            }

            _listaEmpresas = new EmpresaBO().Listar(true);

            if (_fatores == null || _fatores.Count() == 0)
            {
                if (_fatores == null)
                    _fatores = new List<PontuacaoFatorEmpresa>();

                foreach (var item in _listaEmpresas.Where(w => w.AvaliaColaboradores))
                {
                    PontuacaoFatorEmpresa _fator = new PontuacaoFatorEmpresa();

                    _fator.IdEmpresa = item.IdEmpresa;
                    _fator.Nome = item.NomeAbreviado;

                    _fatores.Add(_fator);
                }
            }

            gridGroupingControl.DataSource = _fatores.OrderBy(o => o.Nome).ToList();
            copiarCheckBox.Enabled = !_pontuacao.Existe;
            gravarButton.Enabled = true;
            excluirButton.Enabled = _pontuacao.Existe;
        }

        private void referenciaCopiaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {

            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaCopiaMaskedEditBox.ClipText.Trim()))
            {

                Pesquisas.Pontuacao _pesquisa = new Pesquisas.Pontuacao();
                _pesquisa.ShowDialog();

                referenciaCopiaMaskedEditBox.Text = Publicas._idRetornoPesquisa.ToString("000000");

                if (string.IsNullOrEmpty(referenciaCopiaMaskedEditBox.ClipText.Trim()) || referenciaCopiaMaskedEditBox.ClipText == "000000")
                {
                    referenciaCopiaMaskedEditBox.Text = string.Empty;
                    referenciaCopiaMaskedEditBox.Focus();
                    return;
                }
            }

            _pontuacao = new PontuacaoBO().Consultar(Convert.ToInt32(referenciaCopiaMaskedEditBox.ClipText.Trim()));

            if (!_pontuacao.Existe)
            {
                new Notificacoes.Mensagem("Pontuação não cadastrado para essa referência.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaCopiaMaskedEditBox.Focus();
                return;
            }
            
            base100CurrencyTextBox.DecimalValue = _pontuacao.Base100;
            naoAtendeCurrencyTextBox.DecimalValue = _pontuacao.NaoAtende;
            atendeParcialmenteCurrencyTextBox.DecimalValue = _pontuacao.AtendeParcialmente;
            atendePlenamenteCurrencyTextBox.DecimalValue = _pontuacao.AtendePlenamente;
            superaCurrencyTextBox.DecimalValue = _pontuacao.Supera;
            _fatores = new PontuacaoBO().Listar(_pontuacao.Id);

            _listaEmpresas = new EmpresaBO().Listar(true);

            if (_fatores == null || _fatores.Count() == 0)
            {
                if (_fatores == null)
                    _fatores = new List<PontuacaoFatorEmpresa>();

                foreach (var item in _listaEmpresas.Where(w => w.AvaliaColaboradores))
                {
                    PontuacaoFatorEmpresa _fator = new PontuacaoFatorEmpresa();

                    _fator.IdEmpresa = item.IdEmpresa;
                    _fator.Nome = item.NomeAbreviado;

                    _fatores.Add(_fator);
                }
            }

            gridGroupingControl.DataSource = _fatores.OrderBy(o => o.Nome).ToList();
        }

        private void copiarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            copiarLabel.Visible = copiarCheckBox.Checked;
            referenciaCopiaMaskedEditBox.Visible = copiarCheckBox.Checked;
            pesquisaReferenciaCopiaButton.Visible = copiarCheckBox.Checked;
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            referenciaCopiaMaskedEditBox.Text = string.Empty;
            referenciaMaskedEditBox.Text = string.Empty;
            copiarCheckBox.Checked = false;

            base100CurrencyTextBox.Text = "0";
            naoAtendeCurrencyTextBox.Text = "0";
            atendeParcialmenteCurrencyTextBox.Text = "0";
            atendePlenamenteCurrencyTextBox.Text = "0";
            superaCurrencyTextBox.Text = "0";
            pisoCurrencyText.Text = "0";
            pesoQualitativaCurrencyText.Text = "0";
            pesoNumericaCurrencyTextBox.Text = "0";
            base100CurrencyTextBox.DecimalValue = 0;
            naoAtendeCurrencyTextBox.DecimalValue = 0;
            atendeParcialmenteCurrencyTextBox.DecimalValue = 0;
            atendePlenamenteCurrencyTextBox.DecimalValue = 0;
            superaCurrencyTextBox.DecimalValue = 0;
            pisoCurrencyText.DecimalValue = 0;
            pesoQualitativaCurrencyText.DecimalValue = 0;
            pesoNumericaCurrencyTextBox.DecimalValue = 0;
            copiarCheckBox.Enabled = true;
            referenciaMaskedEditBox.Focus();
            gridGroupingControl.DataSource = new List<PontuacaoFatorEmpresa>();

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new PontuacaoBO().Excluir(_pontuacao.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_pontuacao == null)
                _pontuacao = new Classes.Pontuacao();

            _pontuacao.Existe = !copiarCheckBox.Checked && _pontuacao.Existe;

            _pontuacao.MesReferencia = Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim());
            _pontuacao.Base100 = Convert.ToInt32(base100CurrencyTextBox.DecimalValue); //Teto
            _pontuacao.NaoAtende = Convert.ToInt32(naoAtendeCurrencyTextBox.DecimalValue);
            _pontuacao.AtendeParcialmente = Convert.ToInt32(atendeParcialmenteCurrencyTextBox.DecimalValue);
            _pontuacao.AtendePlenamente = Convert.ToInt32(atendePlenamenteCurrencyTextBox.DecimalValue);
            _pontuacao.Supera = Convert.ToInt32(superaCurrencyTextBox.DecimalValue);
            _pontuacao.Piso = Convert.ToInt32(pisoCurrencyText.DecimalValue);
            _pontuacao.PesoQualitativa = pesoQualitativaCurrencyText.DecimalValue;
            _pontuacao.PesoNumerica = pesoNumericaCurrencyTextBox.DecimalValue;

            if (!new PontuacaoBO().Gravar(_pontuacao, _fatores))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void referenciaMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void pesquisaReferenciaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {

                Pesquisas.Pontuacao _pesquisa = new Pesquisas.Pontuacao();
                _pesquisa.ShowDialog();

                referenciaMaskedEditBox.Text = Publicas._idRetornoPesquisa.ToString("000000");

                if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()) || referenciaMaskedEditBox.ClipText == "000000")
                {
                    referenciaMaskedEditBox.Text = string.Empty;
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                referenciaMaskedEditBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void referenciaCopiaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void pisoCurrencyText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pesoQualitativaCurrencyText.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pisoCurrencyText.Focus();
            }
        }

        private void fatorcurrencyText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pesoNumericaCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pisoCurrencyText.Focus();
            }
        }

        private void Pontuacao_Shown(object sender, EventArgs e)
        {

            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0)
                    gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                else
                    gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;

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

            this.gridGroupingControl.SetMetroStyle(metroColor);

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl.Table.DefaultRecordRowHeight = 25;

        }

        private void pesoNumericaCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                naoAtendeCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pesoQualitativaCurrencyText.Focus();
            }
        }

        private void superaCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
        }
    }
}
