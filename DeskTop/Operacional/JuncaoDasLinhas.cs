using System;
using Classes;
using Negocio;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Grouping;

namespace Suportte.Operacional
{
    public partial class JuncaoDasLinhas : Form
    {
        public JuncaoDasLinhas()
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

        Classes.Empresa _empresa;
        Classes.Linha _linhaPrincipal;
        Classes.Linha _linha;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Operacional.Linhas> _linhasAssociadas;

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

        private void JuncaoDasLinhas_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

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
                LinhaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void LinhaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CodigoLinhaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void CodigoLinhaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                LinhaTextBox.Focus();
            }
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gridGroupingControl1.Focus();
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

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void LinhaTextBox_Enter(object sender, EventArgs e)
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
            empresaComboBoxAdv.FlatBorderColor = Publicas._bordaSaida;

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

        private void LinhaTextBox_Validating(object sender, CancelEventArgs e)
        {
            LinhaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                PesquisaLinhaButton.Enabled = false;
                return;
            }

            Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (string.IsNullOrEmpty(LinhaTextBox.Text))
            {
                new Pesquisas.Linhas().ShowDialog();

                LinhaTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    LinhaTextBox.Focus();
                    return;
                }
            }

            _linhaPrincipal = new LinhaBO().Consultar(_empresa.CodigoEmpresaGlobus, LinhaTextBox.Text);

            if (!_linhaPrincipal.Existe)
            {
                new Notificacoes.Mensagem("Linha não está cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                LinhaTextBox.Focus();
                return;
            }

            if (!_linhaPrincipal.Ativo)
            {
                new Notificacoes.Mensagem("Linha não está Ativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                LinhaTextBox.Focus();
                return;
            }

            NomeLinhaTextBox.Text = _linhaPrincipal.Nome;

            _linhasAssociadas = new OperacionalBO().ListarLinhas(_linhaPrincipal.Id);
            gridGroupingControl1.DataSource = _linhasAssociadas;
            gravarButton.Enabled = _linhasAssociadas.Count() != 0;
            excluirButton.Enabled = _linhasAssociadas.Count() != 0;
        }

        private void CodigoLinhaTextBox_Validating(object sender, CancelEventArgs e)
        {
            CodigoLinhaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                PesquisaLinhaAssociadaButton.Enabled = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (string.IsNullOrEmpty(CodigoLinhaTextBox.Text))
            {
                new Pesquisas.Linhas().ShowDialog();

                CodigoLinhaTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    CodigoLinhaTextBox.Focus();
                    return;
                }
            }

            _linha = new LinhaBO().Consultar(_empresa.CodigoEmpresaGlobus, CodigoLinhaTextBox.Text);

            if (!_linha.Existe)
            {
                new Notificacoes.Mensagem("Linha não está cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoLinhaTextBox.Focus();
                return;
            }

            if (!_linha.Ativo)
            {
                new Notificacoes.Mensagem("Linha não está Ativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoLinhaTextBox.Focus();
                return;
            }

            DescrcaoLinhaTextBox.Text = _linha.Nome;

            gridGroupingControl1.DataSource = new List<Classes.Operacional.Linhas>();

            if (_linhasAssociadas.Where(w => w.CodigoInternoLinha == _linha.Id).Count() == 0)
                _linhasAssociadas.Add(new Classes.Operacional.Linhas()
                {
                    IdLinha = _linhaPrincipal.Id,
                    CodigoInternoLinha = _linha.Id,
                    NomeLinha = _linha.Nome,
                    CodigoLinha = _linha.Codigo
                });

            gridGroupingControl1.DataSource = _linhasAssociadas;

            CodigoLinhaTextBox.Text = string.Empty;
            DescrcaoLinhaTextBox.Text = string.Empty;
            CodigoLinhaTextBox.Focus();
            gravarButton.Enabled = _linhasAssociadas.Count() != 0;
        }

        private void LinhaTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaLinhaButton.Enabled = string.IsNullOrEmpty(LinhaTextBox.Text.Trim());
        }

        private void CodigoLinhaTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaLinhaAssociadaButton.Enabled = string.IsNullOrEmpty(CodigoLinhaTextBox.Text.Trim());
        }

        private void PesquisaLinhaButton_Click(object sender, EventArgs e)
        {
            Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (string.IsNullOrEmpty(LinhaTextBox.Text))
            {
                new Pesquisas.Linhas().ShowDialog();

                LinhaTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    LinhaTextBox.Focus();
                    return;
                }
            }
        }

        private void PesquisaLinhaAssociadaButton_Click(object sender, EventArgs e)
        {
            Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (string.IsNullOrEmpty(CodigoLinhaTextBox.Text))
            {
                new Pesquisas.Linhas().ShowDialog();

                CodigoLinhaTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    CodigoLinhaTextBox.Focus();
                    return;
                }
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_linhasAssociadas.Count() == 0)
            {
                new Notificacoes.Mensagem("Associe as linhas .", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoLinhaTextBox.Focus();
                return;
            }

            if (!new OperacionalBO().Gravar(_linhasAssociadas))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (_linhasAssociadas.Where(w => !w.Existe).Count() != 0)
            {
                Log _log = new Log();
                _log.IdUsuario = Publicas._usuario.Id;
                _log.Tela = "Agrupamento de linhas";

                foreach (var item in _linhasAssociadas.Where(w => !w.Existe))
                {
                    _log.Descricao = "Associou a linha " + item.CodigoLinha + " na linha principal " + LinhaTextBox.Text +
                        " da empresa " + empresaComboBoxAdv.Text;

                }

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = new List<Classes.Operacional.Linhas>();

            CodigoLinhaTextBox.Text = string.Empty;
            DescrcaoLinhaTextBox.Text = string.Empty;
            LinhaTextBox.Text = string.Empty;
            NomeLinhaTextBox.Text = string.Empty;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            LinhaTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão do agrupamento ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            
            if (!new OperacionalBO().ExcluirLinhas(_linha.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu o agrupamento da linha principal " + LinhaTextBox.Text + " da empresa " + empresaComboBoxAdv.Text
                + " com as linha associadas ";

            _log.Tela = "Agrupamento de linhas";

            foreach (var item in _linhasAssociadas.Where(w => !w.Existe))
            {
                _log.Descricao = _log.Descricao + item.CodigoLinha + "; ";
            }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void excluirEssaLinhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão da linha ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            try
            {
                Classes.Operacional.Linhas _linha = new Classes.Operacional.Linhas();
                int i = gridGroupingControl1.Table.CurrentRecord.GetRecord().GetRowIndex();
                GridRecordRow _registro = gridGroupingControl1.Table.DisplayElements[i] as GridRecordRow;

                if (_registro != null)
                {
                    Record dr = _registro.GetRecord() as Record;

                    if (!new OperacionalBO().ExcluirLinhaDaVigencia(Convert.ToInt32(dr["Id"].ToString())))
                    {
                        new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }

                    gridGroupingControl1.DataSource = new List<Classes.Operacional.Vigencia>();

                    foreach (var item in _linhasAssociadas.Where(w => w.Id == Convert.ToInt32(dr["Id"].ToString())))
                    {
                        _linha = item;
                    }

                    _linhasAssociadas.Remove(_linha);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu a linha " + _linha.CodigoLinha + " que estava associada a linha principal " + LinhaTextBox.Text +
                        " e da empresa " + empresaComboBoxAdv.Text;
                    _log.Tela = "Agrupamento de linhas";


                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _linhasAssociadas;
            }
            catch { }

        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
