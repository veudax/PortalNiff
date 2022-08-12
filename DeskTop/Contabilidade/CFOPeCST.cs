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
    public partial class CFOPeCST : Form
    {
        public CFOPeCST()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);

                    if (Publicas._TemaBlack)
                    {
                        gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                        gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                        gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                        gridGroupingControl1.BackColor = Publicas._panelTitulo;

                    }
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.CFOPGlobus _CFOPEntrada;
        Classes.CFOPGlobus _CFOPSaida;
        Classes.OperacaoGlobus _Operacao;
        Classes.CSTGlobus _CST;
        Classes.CFOPeCST _cfopCst;
        List<Classes.CFOPeCST> _listaCFOPs;
        Element _elementoGridClicado;
        string tipoOperacao = "I";

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

        private void tipoDocumentoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }
        
        private void tipoDocumentoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CFOPEntradaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CFOPSaidaTextBox.Focus();
            }
        }

        private void CFOPEntradaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CSTTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CFOPSaidaTextBox.Focus();
            }
        }

        private void CSTTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OperacaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CFOPEntradaTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                OperacaoTextBox.Focus();
            }
        }

        private void OperacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CSTTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gravarButton.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OperacaoTextBox.Focus();
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

        private void CFOPSaidaTextBox_Validating(object sender, CancelEventArgs e)
        {
            CFOPSaidaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(CFOPSaidaTextBox.Text.Trim()))
            {
                Publicas._cfopSaida = true;
                Publicas._cfopEntrada = false;
                new Pesquisas.CFOPGlobus().ShowDialog();

                CFOPSaidaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(CFOPSaidaTextBox.Text) || CFOPSaidaTextBox.Text == "0")
                {
                    CFOPSaidaTextBox.Text = string.Empty;
                    CFOPSaidaTextBox.Focus();
                    return;
                }
            }

            _CFOPSaida = new CFOPGlobusBO().ConsultaCFOP(Convert.ToInt32(CFOPSaidaTextBox.Text));

            if (!_CFOPSaida.Existe)
            {
                if (new Notificacoes.Mensagem("CFOP não cadastrado no Globus." +
                    "Deseja continuar ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    CFOPSaidaTextBox.Focus();
                    return;
                }
                nomeTextBox.Text = string.Empty;
            }

            if (_CFOPSaida.Existe)
                nomeTextBox.Text = _CFOPSaida.Descricao;

            /*
            _cfopCst = new ArquiveiBO().ConsultaCFOP(Convert.ToInt32(CFOPSaidaTextBox.Text.Trim()));

            if (_cfopCst.Existe)
            {
                CFOPEntradaTextBox.Text = _cfopCst.CFOPEntrada.ToString();
                CFOPEntradaTextBox_Validating(sender, e);

                if (_cfopCst.CST != 0)
                {
                    CSTTextBox.Text = _cfopCst.CST.ToString();
                    CSTTextBox_Validating(sender, e);
                }

                if (_cfopCst.Operacao != 0)
                {
                    OperacaoTextBox.Text = _cfopCst.Operacao.ToString();
                    OperacaoTextBox_Validating(sender, e);
                }
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _cfopCst.Existe;*/
        }

        private void CFOPEntradaTextBox_Validating(object sender, CancelEventArgs e)
        {
            CFOPEntradaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(CFOPEntradaTextBox.Text.Trim()))
            {
                Publicas._cfopEntrada = true;
                Publicas._cfopSaida = true;

                new Pesquisas.CFOPGlobus().ShowDialog();

                CFOPEntradaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(CFOPEntradaTextBox.Text) || CFOPEntradaTextBox.Text == "0")
                {
                    CFOPEntradaTextBox.Text = string.Empty;
                    CFOPEntradaTextBox.Focus();
                    return;
                }
            }

            _CFOPEntrada = new CFOPGlobusBO().ConsultaCFOP(Convert.ToInt32(CFOPEntradaTextBox.Text));

            if (!_CFOPEntrada.Existe)
            {
                if (new Notificacoes.Mensagem("CFOP não cadastrado no Globus." +
                    "Deseja continuar ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    CFOPEntradaTextBox.Focus();
                    return;
                }
                DescricaoCFOPEntradaTextBox.Text = string.Empty;
            }

            if (_CFOPEntrada.Existe)
                DescricaoCFOPEntradaTextBox.Text = _CFOPEntrada.Descricao;
        }

        private void CSTTextBox_Validating(object sender, CancelEventArgs e)
        {
            CSTTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Atualizagrid();
                Publicas._setaParaBaixo = false;
                return;
            }

            if (string.IsNullOrEmpty(CSTTextBox.Text.Trim()))
            {
                new Pesquisas.CSTGlobus().ShowDialog();

                CSTTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(CSTTextBox.Text) || CSTTextBox.Text == "0")
                {
                    CSTTextBox.Text = string.Empty;
                    CSTTextBox.Focus();
                    return;
                }
            }

            _CST = new CFOPGlobusBO().ConsultaCST(Convert.ToInt32(CSTTextBox.Text));

            if (!_CST.Existe)
            {
                if (new Notificacoes.Mensagem("Situação tributária não cadastrada no Globus." +
                    "Deseja continuar ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    CSTTextBox.Focus();
                    return;
                }
                DescricaoCSTTextBox.Text = string.Empty;
            }

            if (_CST.Existe)
                DescricaoCSTTextBox.Text = _CST.Descricao;
        }

        private void OperacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            OperacaoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Atualizagrid();
                Publicas._setaParaBaixo = false;
                return;
            }

            if (string.IsNullOrEmpty(OperacaoTextBox.Text.Trim()))
            {
                new Pesquisas.OperacaoGlobus().ShowDialog();

                OperacaoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(OperacaoTextBox.Text) || OperacaoTextBox.Text == "0")
                {
                    OperacaoTextBox.Text = string.Empty;
                    OperacaoTextBox.Focus();
                    return;
                }
            }

            _Operacao = new CFOPGlobusBO().ConsultaOperacao(Convert.ToInt32(OperacaoTextBox.Text));

            if (!_Operacao.Existe)
            {
                if (new Notificacoes.Mensagem("Operação fiscal não cadastrada no Globus." +
                    "Deseja continuar ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    OperacaoTextBox.Focus();
                    return;
                }
                DescricaoOperacaoTextBox.Text = string.Empty;
            }

            if (_Operacao.Existe)
                DescricaoOperacaoTextBox.Text = _Operacao.Descricao;

            Atualizagrid();
        }

        private void Atualizagrid()
        {
            if (tipoOperacao == "I")
            {
                _cfopCst = new Classes.CFOPeCST();
                _cfopCst.CFOPSaida = Convert.ToInt32(CFOPSaidaTextBox.Text);
                _cfopCst.CFOP = _cfopCst.CFOPSaida + " - " + nomeTextBox.Text;
                _cfopCst.CFOPEntrada = Convert.ToInt32(CFOPEntradaTextBox.Text);
                _cfopCst.CFOPE = _cfopCst.CFOPEntrada + " - " + DescricaoCFOPEntradaTextBox.Text;

                if (CSTTextBox.Text != "")
                    _cfopCst.CST = Convert.ToInt32(CSTTextBox.Text);
                if (OperacaoTextBox.Text != "")
                    _cfopCst.Operacao = Convert.ToInt32(OperacaoTextBox.Text);

                _listaCFOPs.Add(_cfopCst);
            }
            else
            {
                foreach (var item in _listaCFOPs.Where(w => w.CFOPEntrada == _cfopCst.CFOPEntrada &&
                                                            w.CFOPSaida.ToString() == CFOPSaidaTextBox.Text))
                {
                    item.CFOPEntrada = Convert.ToInt32(CFOPEntradaTextBox.Text);
                    _cfopCst.CFOPE = _cfopCst.CFOPEntrada + " - " + DescricaoCFOPEntradaTextBox.Text;

                    if (CSTTextBox.Text != "")
                        item.CST = Convert.ToInt32(CSTTextBox.Text);
                    if (OperacaoTextBox.Text != "")
                        item.Operacao = Convert.ToInt32(OperacaoTextBox.Text);
                }
            }

            gridGroupingControl1.DataSource = new List<Classes.CFOPeCST>();
            gridGroupingControl1.DataSource = _listaCFOPs;

            CFOPSaidaTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            CFOPEntradaTextBox.Text = string.Empty;
            DescricaoCFOPEntradaTextBox.Text = string.Empty;
            CFOPEntradaTextBox.Text = string.Empty;
            DescricaoCSTTextBox.Text = string.Empty;
            CSTTextBox.Text = string.Empty;
            OperacaoTextBox.Text = string.Empty;
            DescricaoOperacaoTextBox.Text = string.Empty;
            CFOPSaidaTextBox.Enabled = true;
            Editarpanel.Visible = false;

            gravarButton.Enabled = _listaCFOPs.Count() != 0;
        }

        private void CFOPSaidaTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCFOPSaidaButton.Enabled = string.IsNullOrEmpty(CFOPSaidaTextBox.Text.Trim());
        }

        private void CFOPEntradaTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCFOPEntradaButton.Enabled = string.IsNullOrEmpty(CFOPEntradaTextBox.Text.Trim());
        }

        private void CSTTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCSTButton.Enabled = string.IsNullOrEmpty(CSTTextBox.Text.Trim());
        }

        private void OperacaoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaOperacaoButton.Enabled = string.IsNullOrEmpty(OperacaoTextBox.Text.Trim());
        }

        private void pesquisaCFOPSaidaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CFOPSaidaTextBox.Text.Trim()))
            {
                new Pesquisas.CFOPGlobus().ShowDialog();

                CFOPSaidaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(CFOPSaidaTextBox.Text) || CFOPSaidaTextBox.Text == "0")
                {
                    CFOPSaidaTextBox.Text = string.Empty;
                    CFOPSaidaTextBox.Focus();
                    return;
                }

                CFOPSaidaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaCFOPEntradaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CFOPEntradaTextBox.Text.Trim()))
            {
                new Pesquisas.CFOPGlobus().ShowDialog();

                CFOPEntradaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(CFOPEntradaTextBox.Text) || CFOPEntradaTextBox.Text == "0")
                {
                    CFOPEntradaTextBox.Text = string.Empty;
                    CFOPEntradaTextBox.Focus();
                    return;
                }

                CFOPEntradaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaCSTButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CSTTextBox.Text.Trim()))
            {
                new Pesquisas.CSTGlobus().ShowDialog();

                CSTTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(CSTTextBox.Text) || CSTTextBox.Text == "0")
                {
                    CSTTextBox.Text = string.Empty;
                    CSTTextBox.Focus();
                    return;
                }

                CSTTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaOperacaoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(OperacaoTextBox.Text.Trim()))
            {
                new Pesquisas.OperacaoGlobus().ShowDialog();

                OperacaoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(OperacaoTextBox.Text) || OperacaoTextBox.Text == "0")
                {
                    OperacaoTextBox.Text = string.Empty;
                    OperacaoTextBox.Focus();
                    return;
                }

                OperacaoTextBox_Validating(sender, new CancelEventArgs());
            }
        }


        private void gravarButton_Click(object sender, EventArgs e)
        {
            /*if (_cfopCst == null)
                _cfopCst = new Classes.CFOPeCST();

            _cfopCst.CFOPEntrada = Convert.ToInt32(CFOPEntradaTextBox.Text);
            _cfopCst.CFOPSaida = Convert.ToInt32(CFOPSaidaTextBox.Text);

            _cfopCst.CST = 0;

            if (!string.IsNullOrEmpty(CSTTextBox.Text.Trim()))
                _cfopCst.CST = Convert.ToInt32(CSTTextBox.Text);

            _cfopCst.Operacao = 0;
            if (!string.IsNullOrEmpty(OperacaoTextBox.Text.Trim()))
                _cfopCst.Operacao = Convert.ToInt32(OperacaoTextBox.Text);
                */
            foreach (var item in _listaCFOPs)
            {
                if (!new ArquiveiBO().GravarCFOP(item))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            _listaCFOPs = new ArquiveiBO().ListarCFOP("E");
            gridGroupingControl1.DataSource = new List<Classes.CFOPeCST>();
            gridGroupingControl1.DataSource = _listaCFOPs;

            gravarButton.Enabled = _listaCFOPs.Count() != 0;
        }

        private void CFOPeCST_Shown(object sender, EventArgs e)
        {
            _listaCFOPs = new ArquiveiBO().ListarCFOP("E");

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

            gridGroupingControl1.DataSource = _listaCFOPs;

            gravarButton.Enabled = _listaCFOPs.Count() != 0;
        }

        private void incluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editarpanel.Visible = true;
            CFOPSaidaTextBox.Focus();
            tipoOperacao = "I";
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecord rec = _elementoGridClicado as GridRecord;

            if (rec != null)
            {

                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    CFOPSaidaTextBox.Text = ((int)dr["CFOPSaida"]).ToString();
                    CFOPSaidaTextBox_Validating(sender, new CancelEventArgs());

                    CFOPEntradaTextBox.Text = ((int)dr["CFOPEntrada"]).ToString();
                    CFOPEntradaTextBox_Validating(sender, new CancelEventArgs());

                    CSTTextBox.Text = ((int)dr["CST"]).ToString();

                    if (CSTTextBox.Text == "0")
                        CSTTextBox.Text = string.Empty;
                    else
                        CSTTextBox_Validating(sender, new CancelEventArgs());

                    OperacaoTextBox.Text = ((int)dr["Operacao"]).ToString();

                    if (OperacaoTextBox.Text == "0")
                        OperacaoTextBox.Text = string.Empty;
                    else
                        OperacaoTextBox_Validating(sender, new CancelEventArgs());

                    foreach (var item in _listaCFOPs.Where(w => w.CFOPEntrada.ToString() == CFOPEntradaTextBox.Text &&
                                                                w.CFOPSaida.ToString() == CFOPSaidaTextBox.Text))
                    {
                        _cfopCst = item;
                    }
                    
                }
            }

            tipoOperacao = "A";
            Editarpanel.Visible = true;
            CFOPSaidaTextBox.Enabled = false;
            CFOPEntradaTextBox.Focus();
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            _elementoGridClicado = this.gridGroupingControl1.Table.GetInnerMostCurrentElement();
        }

        private void gridGroupingControl1_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            _elementoGridClicado = this.gridGroupingControl1.Table.GetInnerMostCurrentElement();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecord rec = _elementoGridClicado as GridRecord;

            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (rec != null)
            {

                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    CFOPSaidaTextBox.Text = ((int)dr["CFOPSaida"]).ToString();
                    CFOPSaidaTextBox_Validating(sender, new CancelEventArgs());

                    CFOPEntradaTextBox.Text = ((int)dr["CFOPEntrada"]).ToString();
                    CFOPEntradaTextBox_Validating(sender, new CancelEventArgs());

                    foreach (var item in _listaCFOPs.Where(w => w.CFOPEntrada.ToString() == CFOPEntradaTextBox.Text &&
                                                                w.CFOPSaida.ToString() == CFOPSaidaTextBox.Text))
                    {
                        _cfopCst = item;
                    }

                    if (!new ArquiveiBO().ExcluirCFOP(_cfopCst.Id))
                    {
                        new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }

                    _listaCFOPs.Remove(_cfopCst);
                }
            }

            gridGroupingControl1.DataSource = new List<Classes.CFOPeCST>();
            gridGroupingControl1.DataSource = _listaCFOPs;

        }
    }
}
