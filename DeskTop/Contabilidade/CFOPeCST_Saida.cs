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
    public partial class CFOPeCST_Saida : Form
    {
        public CFOPeCST_Saida()
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

        Classes.LeiGlobus _lei;
        Classes.CFOPGlobus _CFOPSaida;
        Classes.OperacaoGlobus _Operacao;
        Classes.CSTGlobus _CST;
        Classes.CFOPEmitidas _cfopCst;
        List<Classes.CFOPEmitidas> _listaCFOPs;
        Classes.Empresa _empresa;
        Classes.ParametrosArquivei _parametro;
        List<Classes.Empresa> _listaEmpresas;
        Element _elementoGridClicado;
        string tipoOperacao = "I";
        string NaturezaAntes = "";
        bool populandoCampos = false;
        int _id = 0;

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

        }

        private void CFOPEntradaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CSTTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                NaturezaTextBox.Focus();
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
                LeiTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
            }
        }

        private void OperacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CSTTextBox.Focus();
            }
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
            //refere a entrada
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

        }

        private void CFOPEntradaTextBox_Validating(object sender, CancelEventArgs e)
        {
            //refere a lei
            LeiTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(LeiTextBox.Text.Trim()))
            {
                Publicas._cfopSaida = true;
                Publicas._cfopEntrada = false;

                new Pesquisas.LeiGlobus().ShowDialog();

                LeiTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(LeiTextBox.Text) || LeiTextBox.Text == "0")
                {
                    LeiTextBox.Text = string.Empty;
                    LeiTextBox.Focus();
                    return;
                }
            }

            _lei = new CFOPGlobusBO().ConsultarLeisGlobus(Convert.ToInt32(LeiTextBox.Text));

            if (!_lei.Existe)
            {
                new Notificacoes.Mensagem("Lei não cadastrado no Globus.", Publicas.TipoMensagem.Alerta);
                LeiTextBox.Focus();
                TextoLeiTextBox.Text = string.Empty;
                return;
            }

            if (_lei.Existe)
                TextoLeiTextBox.Text = _lei.Descricao;
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
            if (populandoCampos) return;

            if (tipoOperacao == "I")
            {
                _cfopCst = new Classes.CFOPEmitidas();
                _cfopCst.CFOPCodigo = Convert.ToInt32(CFOPSaidaTextBox.Text);
                _cfopCst.CFOP = _cfopCst.CFOPCodigo + " - " + nomeTextBox.Text;
                _cfopCst.Lei = Convert.ToInt32(LeiTextBox.Text);
                _cfopCst.Natureza = NaturezaTextBox.Text;
                _cfopCst.TextoLei = TextoLeiTextBox.Text ;
                if (CSTTextBox.Text != "")
                    _cfopCst.CST = Convert.ToInt32(CSTTextBox.Text);
                if (OperacaoTextBox.Text != "")
                    _cfopCst.Operacao = Convert.ToInt32(OperacaoTextBox.Text);

                _cfopCst.Empresa = empresaComboBoxAdv.Text;
                _cfopCst.IdEmpresa = _empresa.IdEmpresa;
                _cfopCst.SerieComparar = SerieCompararTextBox.Text;
                _cfopCst.SerieGlobus = SerieGlobusTextBox.Text;
                _listaCFOPs.Add(_cfopCst);
            }
            else
            {
                foreach (var item in _listaCFOPs.Where(w => w.Id == _id))
                {
                    item.CFOPCodigo = Convert.ToInt32(CFOPSaidaTextBox.Text);
                    item.CFOP = _cfopCst.CFOPCodigo + " - " + nomeTextBox.Text;
                    item.Lei = Convert.ToInt32(LeiTextBox.Text);
                    item.Natureza = NaturezaTextBox.Text;
                    item.TextoLei = TextoLeiTextBox.Text;
                    if (CSTTextBox.Text != "")
                        item.CST = Convert.ToInt32(CSTTextBox.Text);
                    if (OperacaoTextBox.Text != "")
                        item.Operacao = Convert.ToInt32(OperacaoTextBox.Text);

                    item.Empresa = empresaComboBoxAdv.Text;
                    item.IdEmpresa = _empresa.IdEmpresa;
                    item.SerieComparar = SerieCompararTextBox.Text;
                    item.SerieGlobus = SerieGlobusTextBox.Text;
                }
            }

            gridGroupingControl1.DataSource = new List<Classes.CFOPEmitidas>();
            gridGroupingControl1.DataSource = _listaCFOPs;

            CFOPSaidaTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            LeiTextBox.Text = string.Empty;
            TextoLeiTextBox.Text = string.Empty;
            LeiTextBox.Text = string.Empty;
            DescricaoCSTTextBox.Text = string.Empty;
            CSTTextBox.Text = string.Empty;
            OperacaoTextBox.Text = string.Empty;
            DescricaoOperacaoTextBox.Text = string.Empty;
            NaturezaTextBox.Text = string.Empty;
            CFOPSaidaTextBox.Enabled = true;
            Editarpanel.Size = new Size(904, 50);

            gravarButton.Enabled = _listaCFOPs.Count() != 0;
        }

        private void CFOPSaidaTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCFOPSaidaButton.Enabled = string.IsNullOrEmpty(CFOPSaidaTextBox.Text.Trim());
        }

        private void CFOPEntradaTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCFOPEntradaButton.Enabled = string.IsNullOrEmpty(LeiTextBox.Text.Trim());
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
            if (string.IsNullOrEmpty(LeiTextBox.Text.Trim()))
            {
                new Pesquisas.CFOPGlobus().ShowDialog();

                LeiTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(LeiTextBox.Text) || LeiTextBox.Text == "0")
                {
                    LeiTextBox.Text = string.Empty;
                    LeiTextBox.Focus();
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
           
            foreach (var item in _listaCFOPs)
            {
                if (!new ArquiveiBO().GravarCFOPEmitidas(item))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            if (!new ArquiveiBO().GravarSerieEmitidas(_empresa.IdEmpresa, SerieGlobusTextBox.Text, SerieCompararTextBox.Text))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            _listaCFOPs = new ArquiveiBO().ListarCFOPEmitidas(_empresa.IdEmpresa);

            gridGroupingControl1.DataSource = new List<Classes.CFOPEmitidas>();
            gridGroupingControl1.DataSource = _listaCFOPs;

            gravarButton.Enabled = _listaCFOPs.Count() != 0;
            empresaComboBoxAdv.Focus();
        }

        private void CFOPeCST_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

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
            gridGroupingControl1.Table.DefaultRecordRowHeight = 45;

            Editarpanel.Size = new Size(904, 50);
            Editarpanel.Visible = true;
            empresaComboBoxAdv.Focus();

        }

        private void incluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _id = 0;
            Editarpanel.Size = new Size(904, 350);
            Editarpanel.Visible = true;
            CFOPSaidaTextBox.Focus();
            tipoOperacao = "I";
            populandoCampos = false;
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecord rec = _elementoGridClicado as GridRecord;
            tipoOperacao = "A";
            populandoCampos = true;

            if (rec != null)
            {

                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    _id = (int)dr["Id"];
                    CFOPSaidaTextBox.Text = ((int)dr["CFOPCodigo"]).ToString();
                    CFOPSaidaTextBox_Validating(sender, new CancelEventArgs());

                    NaturezaTextBox.Text = (string)dr["Natureza"];

                    LeiTextBox.Text = ((int)dr["Lei"]).ToString();
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

                    foreach (var item in _listaCFOPs.Where(w => w.Id == _id))
                    {
                        _cfopCst = item;
                    }

                }
            }

            Editarpanel.Size = new Size(904, 350);
            Editarpanel.Visible = true;
            CFOPSaidaTextBox.Enabled = false;
            LeiTextBox.Focus();
            populandoCampos = false;
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
                    _id = (int)dr["Id"];
                    CFOPSaidaTextBox.Text = ((int)dr["CFOPCodigo"]).ToString();
                    CFOPSaidaTextBox_Validating(sender, new CancelEventArgs());

                    NaturezaTextBox.Text = (string)dr["Natureza"];
                    LeiTextBox.Text = ((int)dr["Lei"]).ToString();
                    CFOPEntradaTextBox_Validating(sender, new CancelEventArgs());

                    CSTTextBox.Text = ((int)dr["CST"]).ToString();


                    foreach (var item in _listaCFOPs.Where(w => w.Id == _id ))
                    {
                        _cfopCst = item;
                    }

                    if (!new ArquiveiBO().ExcluirCFOPEmitidas(_cfopCst.Id))
                    {
                        new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }

                    _listaCFOPs.Remove(_cfopCst);
                }
            }

            gridGroupingControl1.DataSource = new List<Classes.CFOPEmitidas>();
            gridGroupingControl1.DataSource = _listaCFOPs;

        }

        private void Editarpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NaturezaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LeiTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;   
                CFOPSaidaTextBox.Focus();
            }
        }

        private void NaturezaTextBox_Validating(object sender, CancelEventArgs e)
        {
            NaturezaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (tipoOperacao == "N" && NaturezaAntes != NaturezaTextBox.Text)
            {
                if (!new ArquiveiBO().GravarNatureza(NaturezaAntes, NaturezaTextBox.Text))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                NaturezaTextBox.Text = string.Empty;
                CFOPSaidaTextBox.Enabled = true;
                Editarpanel.Size = new Size(904, 50);
                NaturezaAntes = "";
                gridGroupingControl1.Focus();
                empresaComboBoxAdv_Validating(sender, new CancelEventArgs());
            }

        }

        private void CFOPSaidaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                NaturezaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SerieCompararTextBox.Focus();
            }
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SerieGlobusTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }


        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            SerieGlobusTextBox.Text = "";
            SerieCompararTextBox.Text = "";

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

            _parametro = new ParametrosArquiveiBO().Consultar(_empresa.IdEmpresa);
            if (!_parametro.Existe)
            {
                new Notificacoes.Mensagem("Empresa não parametrizada para o Arquivei.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            _listaCFOPs = new ArquiveiBO().ListarCFOPEmitidas(_empresa.IdEmpresa);

            foreach (var item in _listaCFOPs.Where( w=> w.SerieGlobus != null ))
            {
                SerieGlobusTextBox.Text = item.SerieGlobus;
                SerieCompararTextBox.Text = item.SerieComparar;
                break;
            }

            gridGroupingControl1.DataSource = new List<Classes.CFOPEmitidas>();
            gridGroupingControl1.DataSource = _listaCFOPs;

            gravarButton.Enabled = _listaCFOPs.Count() != 0;
            CopiarButton.Enabled = _listaCFOPs.Count() != 0;

        }

        private void CopiarButton_Click(object sender, EventArgs e)
        {
            CopiaCFOPDeNFEmitimos _tela = new CopiaCFOPDeNFEmitimos();
            _tela._empresa = this._empresa;
            _tela.ShowDialog();
        }

        private void SerieGlobusTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SerieCompararTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void SerieCompararTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SerieGlobusTextBox.Focus();
            }
        }

        private void SerieGlobusTextBox_Validating(object sender, CancelEventArgs e)
        {
            SerieGlobusTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void SerieCompararTextBox_Validating(object sender, CancelEventArgs e)
        {
            SerieCompararTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void alterarApenasANaturezaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecord rec = _elementoGridClicado as GridRecord;
            tipoOperacao = "N";
            populandoCampos = true;

            if (rec != null)
            {

                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    NaturezaTextBox.Text = (string)dr["Natureza"];
                    NaturezaAntes = NaturezaTextBox.Text;
                }
            }

            Editarpanel.Size = new Size(904, 350);
            Editarpanel.Visible = true;
            CFOPSaidaTextBox.Enabled = false;
            NaturezaTextBox.Enabled = true;
            NaturezaTextBox.Focus();
            populandoCampos = false;
        }
    }
}
