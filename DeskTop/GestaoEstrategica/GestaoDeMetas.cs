using Classes;
using Negocio;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
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

namespace Suportte.GestaoEstrategica
{
    public partial class GestaoDeMetas : Form
    {
        public GestaoDeMetas()
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

                    label4.BackColor = Publicas._botao;
                    label3.BackColor = Publicas._botao;
                    label161.BackColor = Publicas._botao;
                    label5.BackColor = Publicas._botao;

                    foreach (Control item in AprendizadoPanel.Controls)
                    {
                        if (item is Panel)
                        {
                            foreach (Control itemP in item.Controls)
                            {
                                if (itemP is CurrencyTextBox)
                                {
                                    ((CurrencyTextBox)itemP).DecimalValue = 0;
                                    ((CurrencyTextBox)itemP).Tag = null;
                                    ((CurrencyTextBox)itemP).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                                    ((CurrencyTextBox)itemP).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).BackGroundColor = Publicas._fundo;
                                    ToolTipInfo _tool = new ToolTipInfo();
                                    _tool.Footer.Text = "";
                                    superToolTip1.SetToolTip((CurrencyTextBox)itemP, _tool);
                                }
                            }
                        }
                    }

                    foreach (Control item in ClientePanel.Controls)
                    {
                        if (item is Panel)
                        {
                            foreach (Control itemP in item.Controls)
                            {
                                if (itemP is CurrencyTextBox)
                                {
                                    ((CurrencyTextBox)itemP).DecimalValue = 0;
                                    ((CurrencyTextBox)itemP).Tag = null;
                                    ((CurrencyTextBox)itemP).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                                    ((CurrencyTextBox)itemP).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).BackGroundColor = Publicas._fundo;
                                    ToolTipInfo _tool = new ToolTipInfo();
                                    _tool.Footer.Text = "";
                                    superToolTip1.SetToolTip((CurrencyTextBox)itemP, _tool);
                                }
                            }
                        }
                    }

                    foreach (Control item in ProcessosInternosPanel.Controls)
                    {
                        if (item is Panel)
                        {
                            foreach (Control itemP in item.Controls)
                            {
                                if (itemP is CurrencyTextBox)
                                {
                                    ((CurrencyTextBox)itemP).DecimalValue = 0;
                                    ((CurrencyTextBox)itemP).Tag = null;
                                    ((CurrencyTextBox)itemP).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                                    ((CurrencyTextBox)itemP).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).BackGroundColor = Publicas._fundo;
                                    ToolTipInfo _tool = new ToolTipInfo();
                                    _tool.Footer.Text = "";
                                    superToolTip1.SetToolTip((CurrencyTextBox)itemP, _tool);
                                }
                            }
                        }
                    }

                    foreach (Control item in FinanceiroPanel.Controls)
                    {
                        if (item is Panel)
                        {
                            foreach (Control itemP in item.Controls)
                            {
                                if (itemP is CurrencyTextBox)
                                {
                                    ((CurrencyTextBox)itemP).DecimalValue = 0;
                                    ((CurrencyTextBox)itemP).Tag = null;
                                    ((CurrencyTextBox)itemP).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                                    ((CurrencyTextBox)itemP).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                                    ((CurrencyTextBox)itemP).BackGroundColor = Publicas._fundo;
                                    ToolTipInfo _tool = new ToolTipInfo();
                                    _tool.Footer.Text = "";
                                    superToolTip1.SetToolTip((CurrencyTextBox)itemP, _tool);
                                }
                            }
                        }
                    }
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.ValoresDasMetas> _listaValoresMetas;
        List<Classes.ValoresDasMetas> _listaValoresMetasLog;
        List<Classes.ValoresDasMetas> _listaValoresMetasMesAnterior;
        List<Classes.ValoresDasMetas> _retornoConsulta;
        List<Classes.Metas> _listaMetas;
        List<Classes.ItensAvaliacaoMetas> _listaItensAvaliacao;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        Classes.BSCEmEdicao _bscEmEdicao;

        CurrencyTextBox _componenteFocado;
        string _referencia;
        string _referenciaInicio;
        string _arquivoAmarelo = @"Imagens\Amarelo_Solido.png";
        string _arquivoVerde = @"Imagens\Verde_Solido.png";
        string _arquivoVermelho = @"Imagens\Vermelho_Solido.png";
        string _arquivoCinza = @"Imagens\Cinza.png";
        int _mes;
        int _mesAux;
        int _ano;
        int _diasCorridos;
        //int _diasUteis;
        int _alturaOriginal;
        int _topOriginalCabProcessos;
        int _topOriginalProcessos;
        int _topOriginalCabAprendizado;
        int _topOriginalAprendizado;
        bool _buscouRealizadoFinanceiro;
        bool _buscouRealizadoOperacional;
        decimal _valor;
        Panel panelChamador;
        Control parente;
        DateTime _dataCorteOperacional;
        DateTime _dataCorteFinanceiro;

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

        private void GestaoDeMetas_Shown(object sender, EventArgs e)
        {
            BuscarPrevistoButton.ForeColor = empresaComboBoxAdv.ForeColor;
            BuscarRealizadoButton.Visible = false;// (Publicas._usuario.PermiteBuscarResultado || Publicas._usuario.Administrador) && Publicas._usuario.PermiteAlterarBSC;
            BuscarPrevistoButton.Visible = (Publicas._usuario.PermiteBuscarResultado || Publicas._usuario.Administrador) && Publicas._usuario.PermiteAlterarBSC;
            this.Location = new Point(this.Left, 60);
            grupospanel.Width = this.Width - 5;

            grupospanel.Height = dadospanel.Height - panel51.Height;
            DivisoriaPanel.Width = this.Width - 2;
            FinanceiroPanel.Width = grupospanel.Width - 20;
            ClientePanel.Width = grupospanel.Width - 20;
            ProcessosInternosPanel.Width = grupospanel.Width - 20;
            AprendizadoPanel.Width = grupospanel.Width - 20;

            _listaEmpresas = new EmpresaBO().Listar(false);
            _empresaDoColaborador = new ColaboradoresBO().Listar(Publicas._idColaborador);

            if (Publicas._usuario.PermiteAlterarBSC || Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.IdEmpresa == 19)
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
                        
            #region coloca os botões centralizados
            int espacoEntreBotoes = limparButton.Left - (gravarButton.Left + gravarButton.Width);

            gravarButton.Left = botoesPanel.Width / 3;
            limparButton.Left = gravarButton.Left + limparButton.Width + espacoEntreBotoes;
            excluirButton.Left = limparButton.Left + limparButton.Width + espacoEntreBotoes;
            #endregion

            gravarButton.Visible = Publicas._usuario.PermiteAlterarBSC;
            excluirButton.Visible = Publicas._usuario.PermiteAlterarBSC;
            ContratoDeMetasButton.Visible = !Publicas._usuario.PermiteAlterarBSC;

            if (Publicas._usuario.PermiteAlterarBSC && Publicas._usuario.AlteraBSCIndicadoresManuais)
            {

                foreach (Control item in AprendizadoPanel.Controls)
                {
                    if (item is Panel)
                    {
                       item.Enabled = item.Name == "LimpezaFrotaPanel" || item.Name == "ReclamacaoPanel" || item.Name == "CumprimentoPanel"
                             || item.Name == "CarrosRetidosPanel" || item.Name == "ComprasEmergenciaisPanel" || item.Name == "ExamesVencidosPanel" 
                             || item.Name == "PartidaPanel" || item.Name == "PontualidadePanel";
                    }
                }

                foreach (Control item in ClientePanel.Controls)
                {
                    if (item is Panel)
                    {
                        item.Enabled = item.Name == "LimpezaFrotaPanel" || item.Name == "ReclamacaoPanel" || item.Name == "CumprimentoPanel"
                           || item.Name == "CarrosRetidosPanel" || item.Name == "ComprasEmergenciaisPanel" || item.Name == "ExamesVencidosPanel"
                           || item.Name == "PartidaPanel" || item.Name == "PontualidadePanel";
                    }
                }

                foreach (Control item in ProcessosInternosPanel.Controls)
                {
                    if (item is Panel)
                    {
                        item.Enabled = item.Name == "LimpezaFrotaPanel" || item.Name == "ReclamacaoPanel" || item.Name == "CumprimentoPanel"
                           || item.Name == "CarrosRetidosPanel" || item.Name == "ComprasEmergenciaisPanel" || item.Name == "ExamesVencidosPanel"
                           || item.Name == "PartidaPanel" || item.Name == "PontualidadePanel";
                    }
                }

                foreach (Control item in FinanceiroPanel.Controls)
                {
                    if (item is Panel)
                    {
                        item.Enabled = item.Name == "LimpezaFrotaPanel" || item.Name == "ReclamacaoPanel" || item.Name == "CumprimentoPanel"
                         || item.Name == "CarrosRetidosPanel" || item.Name == "ComprasEmergenciaisPanel" || item.Name == "ExamesVencidosPanel"
                         || item.Name == "PartidaPanel" || item.Name == "PontualidadePanel";
                    }
                }
            }

            if (Publicas._TemaBlack)
            {
                ReceitaBruta1PrevLabel.BackGroundColor = Publicas._fundo;
                ReceitaBruta1RealLabel.BackGroundColor = Publicas._fundo;
                DeducoesReceitaPrevLabel.BackGroundColor = Publicas._fundo;
                DeducoesReceitaRealLabel.BackGroundColor = Publicas._fundo;
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            if (_bscEmEdicao != null)
                new MetasBO().Excluir(_bscEmEdicao);
            Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = ((Panel)sender).ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle((Publicas._TemaBlack ? Pens.Black : Pens.DarkOliveGreen), rect);
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            grupospanel.AutoScrollPosition = new Point(0);
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                //Publicas._idSuperior = 0;

                //Pesquisas.Feedback _pesquisa = new Pesquisas.Feedback("MN");
                //_pesquisa.tituloLabel.Text = "Pesquisa de " + tituloLabel.Text;
                //_pesquisa.ShowDialog();

                //referenciaMaskedEditBox.Text = Publicas._idRetornoPesquisa.ToString("000000");

                //if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()) || referenciaMaskedEditBox.ClipText == "000000")
                //{
                    referenciaMaskedEditBox.Text = string.Empty;
                    referenciaMaskedEditBox.Focus();
                    return;
                //}
            }

            try
            {
                if (referenciaMaskedEditBox.ClipText.Trim().Length != 6)
                {
                    new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }
                DateTime _dataIni = new DateTime(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(2, 4)), Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(0, 2)), 1);
                DateTime _dataFim = _dataIni.AddMonths(1).AddDays(-1);
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            if (_bscEmEdicao != null)
                new MetasBO().Excluir(_bscEmEdicao);

            _bscEmEdicao = new MetasBO().Consultar(_empresa.IdEmpresa, referenciaMaskedEditBox.ClipText);

            if (_bscEmEdicao.Existe && _bscEmEdicao.IdUsuario != Publicas._usuario.Id && Publicas._usuario.PermiteAlterarBSC && !Publicas._usuario.Administrador)
            {
                new Notificacoes.Mensagem("Empresa e período em edição com " + _bscEmEdicao.NomeUsuario + " na tela " + _bscEmEdicao.Tela, Publicas.TipoMensagem.Alerta).ShowDialog();
                _bscEmEdicao = null;
                referenciaMaskedEditBox.Focus();
                return;
            }
            else
            {
                _bscEmEdicao.IdUsuario = Publicas._usuario.Id;
                _bscEmEdicao.IdEmpresa = _empresa.IdEmpresa;
                _bscEmEdicao.Referencia = referenciaMaskedEditBox.ClipText;
                _bscEmEdicao.Tela = "BSC - Indicadores de Desempenho";

                if (Publicas._usuario.PermiteAlterarBSC && !Publicas._usuario.Administrador)
                {
                    new MetasBO().Gravar(_bscEmEdicao);

                    _bscEmEdicao = new MetasBO().Consultar(_empresa.IdEmpresa, referenciaMaskedEditBox.ClipText);
                }
            }

            referenciaMaskedEditBox.Cursor = Cursors.WaitCursor;
            mensagemSistemaLabel.Text = "Aguarde, Pesquisando...";
            this.Refresh();

            _referencia = referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2);
            _listaValoresMetas = new MetasBO().Listar(false, _empresa.IdEmpresa, _referencia);
            _listaValoresMetasLog = new MetasBO().Listar(false, _empresa.IdEmpresa, _referencia);

            _listaItensAvaliacao = new AutoAvaliacaoBO().ListarContratoMetas(referenciaMaskedEditBox.ClipText.Trim(), _empresa.IdEmpresa);

            _listaMetas = new MetasBO().Listar(false);

            _mes = Convert.ToInt32(_referencia.Substring(4, 2));
            _ano = Convert.ToInt32(_referencia.Substring(0, 4));

            if (_mes == 12)
            {
                _diasCorridos = Convert.ToInt32(Convert.ToDateTime("01/01/" + (_ano + 1).ToString()).Subtract(Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString())).TotalDays);
                //_diasUteis = new FeriadoBO().DiasUteis(_empresa.IdEmpresa, Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString()), Convert.ToDateTime("01/01/" + (_ano + 1).ToString()).AddDays(-1));
            }
            else
            {
                _diasCorridos = Convert.ToInt32(Convert.ToDateTime("01/" + (_mes + 1).ToString("00") + "/" + _ano.ToString()).Subtract(Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString())).TotalDays);
                ///_diasUteis = new FeriadoBO().DiasUteis(_empresa.IdEmpresa, Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString()), Convert.ToDateTime("01/" + (_mes + 1).ToString("00") + "/" + _ano.ToString()).AddDays(-1));
            }

            DiasCorridosCurrencyTextBox.DecimalValue = _diasCorridos;
            //DiasUteisCurrencyTextBox.DecimalValue = _diasUteis;

            if (_listaValoresMetasMesAnterior == null)
                _listaValoresMetasMesAnterior = new List<ValoresDasMetas>();
            else
                _listaValoresMetasMesAnterior.Clear();

            foreach (var item in _listaMetas.Where( w => w.UsaNaGestao))
            {
                #region Busca mês anteriores para calculo previsto
                _mesAux = 1;
                _referenciaInicio = _ano.ToString() + _mesAux.ToString("00");

                _retornoConsulta = new MetasBO().Listar(false, _empresa.IdEmpresa, _referencia, _referenciaInicio, item.Id);

                if (_retornoConsulta.Count != 0)
                {
                    _listaValoresMetasMesAnterior.AddRange(_retornoConsulta);
                }
                #endregion

                #region Financeiro

                if (item.Descricao.ToUpper().Equals("EBITDA Financeiro".ToUpper()))
                {
                    EbitdaFPanel.Tag = item.Id;
                    Ebitda2FPrevLabel.Tag = item.Id;
                    Ebitda2FRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Ebitda".ToUpper()))
                {
                    EbitdaPanel.Tag = item.Id;
                    EbitdaPrevLabel.Tag = item.Id;
                    EbitdaRealLabel.Tag = item.Id;
                    //EbitdaPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //Ebitda2PrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //EbitdaRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    //Ebitda2RealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Receita".ToUpper()) && item.Descricao.ToUpper().Contains("Líquida".ToUpper()))
                {
                    ReceitaLiquidaPanel.Tag = item.Id;
                    ReceitaLiquidaPrevLabel.Tag = item.Id;
                    ReceitaLiquidaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Custo Manutenção Frota".ToUpper()))
                {
                    CustoManutencaoFrotaPanel.Tag = item.Id;
                    CustoManutencaoFrotaPrevLabel.Tag = item.Id;
                    CustoManutencaoFrotaRealLabel.Tag = item.Id;
                    //CustoManutencaoFrotaPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //CustoManutencaoFrotaRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Outros".ToUpper()) && item.Descricao.ToUpper().Contains("Custos".ToUpper()))
                {
                    OutrosCustosPanel.Tag = item.Id;
                    OutrosCustosPrevLabel.Tag = item.Id;
                    OutrosCustosRealLabel.Tag = item.Id;
                    //OutrosCustosPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //OutrosCustosRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Bruta".ToUpper()) && item.Descricao.ToUpper().Contains("Receita".ToUpper()) && item.Descricao.ToUpper().Contains("Total".ToUpper()))
                {
                    ReceitaBrutaTotalPanel.Tag = item.Id;
                    ReceitaBruta1PrevLabel.Tag = item.Id;
                    ReceitaBruta1RealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Bruta".ToUpper()) && item.Descricao.ToUpper().Contains("Receita".ToUpper()))
                {
                    ReceitaBruta2Panel.Tag = item.Id;
                    ReceitaBruta2PrevLabel.Tag = item.Id;
                    ReceitaBruta2RealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Receita Subsídio".ToUpper()))
                {
                    ReceitaSubsidioPanel.Tag = item.Id;
                    ReceitaSubsidioPrevLabel.Tag = item.Id;
                    ReceitaSubsidioRealLabel.Tag = item.Id;
                    ReceitaSubsidio2PrevLabel.Enabled = false;// item.RealizadoPermiteAlterar;
                    ReceitaSubsidio2RealLabel.Enabled = false;// item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Folha".ToUpper()))
                {
                    if (item.Descricao.ToUpper().Contains("Op".ToUpper()) && item.Descricao.ToUpper().Contains("Custo".ToUpper()))
                    {
                        CustoFolhaOperacaoPanel.Tag = item.Id;
                        CustoFolhaOp1PrevLabel.Tag = item.Id;
                        CustoFolhaOp1RealLabel.Tag = item.Id;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Adm".ToUpper()) && item.Descricao.ToUpper().Contains("Custo".ToUpper()))
                    {
                        CustoFolhaAdministracaoPanel.Tag = item.Id;
                        CustoFolhaAdm1PrevLabel.Tag = item.Id;
                        CustoFolhaAdm1RealLabel.Tag = item.Id;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Man".ToUpper()) && item.Descricao.ToUpper().Contains("Custo".ToUpper()))
                    {
                        CustoFolhaManutencaoPanel.Tag = item.Id;
                        CustoFolhaMan1PrevLabel.Tag = item.Id;
                        CustoFolhaMan1RealLabel.Tag = item.Id;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Man".ToUpper()) && item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
                    {
                        EficienciaFolhaManPanel.Tag = item.Id;
                        EficienciaFolhaManPrevLabel.Tag = item.Id;
                        EficienciaFolhaManRealLabel.Tag = item.Id;
                        //EficienciaFolhaManPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //EficienciaFolhaManRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Op".ToUpper()) && item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
                    {
                        EficienciaFolhaOpPanel.Tag = item.Id;
                        EficienciaFolhaOpPrevLabel.Tag = item.Id;
                        EficienciaFolhaOpRealLabel.Tag = item.Id;
                        //EficienciaFolhaOpPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //EficienciaFolhaOpRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Adm".ToUpper()) && item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
                    {
                        EficienciaFolhaAdmPanel.Tag = item.Id;
                        EficienciaFolhaAdmPrevLabel.Tag = item.Id;
                        EficienciaFolhaAdmRealLabel.Tag = item.Id;
                        //EficienciaFolhaAdmPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //EficienciaFolhaAdmRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Custo".ToUpper()))
                    {
                        CustoFolhaPanel.Tag = item.Id;
                        CustoFolhaTotalPrevLabel.Tag = item.Id;
                        CustoFolhaTotalRealLabel.Tag = item.Id;
                        //CustoFolhaTotalPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //CustoFolhaTotalRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
                    {
                        EficienciaFolhaTotalPanel.Tag = item.Id;
                        EficienciaFolhaTotalPrevLabel.Tag = item.Id;
                        EficienciaFolhaTotalRealLabel.Tag = item.Id;
                        //EficienciaFolhaTotalPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //EficienciaFolhaTotalRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                }

                if (item.Descricao.ToUpper().Contains("Eficiência de Man".ToUpper()))
                {
                    EficienciaManutencaoFrotaPanel.Tag = item.Id;
                    EficienciaManutencaoFrotaPrevLabel.Tag = item.Id;
                    EficienciaManutencaoFrotaRealLabel.Tag = item.Id;
                    //EficienciaManutencaoFrotaPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //EficienciaManutencaoFrotaRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Km".ToUpper()) && item.Descricao.ToUpper().Contains("Rodado".ToUpper()))
                {
                    KmRodadoPanel.Tag = item.Id;
                    KmRodadoPrevLabel.Tag = item.Id;
                    KmRodadoRealLabel.Tag = item.Id;
                    //KmRodadoPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //KmRodadoRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Pneus".ToUpper()))
                {
                    CustosPneusPanel.Tag = item.Id;
                    CustoPneuPrevLabel.Tag = item.Id;
                    CustoPneuRealLabel.Tag = item.Id;
                    //CustoPneuPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //CustoPneuRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Peças".ToUpper()))
                {
                    CustosPecasPanel.Tag = item.Id;
                    CustoPecasPrevLabel.Tag = item.Id;
                    CustoPecasRealLabel.Tag = item.Id;
                    //CustoPecasPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //CustoPecasRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Custo Hora".ToUpper()) && item.Descricao.ToUpper().Contains("Extra".ToUpper()))
                {
                    if (item.Descricao.ToUpper().Contains("+".ToUpper()))
                    {
                        CustoHorasExtrasSomaPrevLabel.Tag = item.Id;
                        CustoHorasExtrasSomaRealLabel.Tag = item.Id;
                        //CustoHorasExtrasSomaPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //CustoHorasExtrasSomaRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Op".ToUpper()))
                    {
                        CustoHorasExtraOpPanel.Tag = item.Id;
                        CustoHorasExtrasOpPrevLabel.Tag = item.Id;
                        CustoHorasExtrasOpRealLabel.Tag = item.Id;
                        //CustoHorasExtrasOpPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //CustoHorasExtrasOpRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Man".ToUpper()))
                    {
                        CustoHorasExtraManPanel.Tag = item.Id;
                        CustoHorasExtrasManPrevLabel.Tag = item.Id;
                        CustoHorasExtrasManRealLabel.Tag = item.Id;
                        //CustoHorasExtrasManPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //CustoHorasExtrasManRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Adm".ToUpper()))
                    {
                        CustoHorasExtraAdmPanel.Tag = item.Id;
                        CustoHorasExtrasAdmPrevLabel.Tag = item.Id;
                        CustoHorasExtrasAdmRealLabel.Tag = item.Id;
                        //CustoHorasExtrasAdmPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //CustoHorasExtrasAdmRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else                    
                    {
                        CustoHoraExtraPanel.Tag = item.Id;
                        CustoHorasExtrasPrevLabel.Tag = item.Id;
                        CustoHorasExtrasRealLabel.Tag = item.Id;
                        //CustoHorasExtrasPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //CustoHorasExtrasRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                }

                if (item.Descricao.ToUpper().Contains("Multa".ToUpper()) && item.Descricao.ToUpper().Contains("Gestor".ToUpper()))
                {
                    CustoMultasOrgaoPanel.Tag = item.Id;
                    CustoMultasOrgaoGestorPrevLabel.Tag = item.Id;
                    CustoMultasOrgaoGestorRealLabel.Tag = item.Id;
                    //CustoMultasOrgaoGestorPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //CustoMultasOrgaoGestorRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Deduções".ToUpper()))
                {
                    DeducoesReceitaPanel.Tag = item.Id;
                    DeducoesReceitaPrevLabel.Tag = item.Id;
                    DeducoesReceitaRealLabel.Tag = item.Id;
                    //DeducoesReceitaPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //DeducoesReceitaRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                #endregion

                #region Cliente
                if (item.Descricao.ToUpper().Contains("Limpeza".ToUpper()))
                {
                    LimpezaFrotaPanel.Tag = item.Id;
                    IndiceLimpezaFrotaPrevLabel.Tag = item.Id;
                    IndiceLimpezaFrotaRealLabel.Tag = item.Id;
                    //IndiceLimpezaFrotaPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //IndiceLimpezaFrotaRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }
                if (item.Descricao.ToUpper().Contains("Índice KM/MKBF".ToUpper()))
                {
                    MkbfPanel.Tag = item.Id;
                    MKBFPrevLabel.Tag = item.Id;
                    MKBFRealLabel.Tag = item.Id;
                    //MKBFPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //MKBFRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("MKBF Rodoviário".ToUpper()))
                {
                    MkbfRodoviarioPanel.Tag = item.Id;
                    MKBFRodoviarioPrevLabel.Tag = item.Id;
                    MKBFRodoviarioRealLabel.Tag = item.Id;
                }
                if (item.Descricao.ToUpper().Contains("MKBF Urbano".ToUpper()))
                {
                    MkbfUrbanoPanel.Tag = item.Id;
                    MKBFUrbanoPrevLabel.Tag = item.Id;
                    MKBFUrbanoRealLabel.Tag = item.Id;
                }
                if (item.Descricao.ToUpper().Contains("MKBF Suburbano".ToUpper()))
                {
                    MkbfSubUrbanoPanel.Tag = item.Id;
                    MKBFSubUrbanoPrevLabel.Tag = item.Id;
                    MKBFSubUrbanoRealLabel.Tag = item.Id;
                }
                if (item.Descricao.ToUpper().Contains("MKBF Intermunicipal".ToUpper()))
                {
                    MkbfIntermunicipalPanel.Tag = item.Id;
                    MKBFIntermunicipalPrevLabel.Tag = item.Id;
                    MKBFIntermunicipalRealLabel.Tag = item.Id;
                }
                if (item.Descricao.ToUpper().Contains("MKBF Municipal".ToUpper()))
                {
                    MkbfMunicipalPanel.Tag = item.Id;
                    MKBFMunicipalPrevLabel.Tag = item.Id;
                    MKBFMunicipalRealLabel.Tag = item.Id;
                }
                if (item.Descricao.ToUpper().Contains("MKBF Escolar".ToUpper()))
                {
                    MkbfEscolarPanel.Tag = item.Id;
                    MKBFEscolarPrevLabel.Tag = item.Id;
                    MKBFEscolarRealLabel.Tag = item.Id;
                }
                if (item.Descricao.ToUpper().Contains("MKBF Fretamento".ToUpper()))
                {
                    MkbfFretamentoPanel.Tag = item.Id;
                    MKBFFretamentoPrevLabel.Tag = item.Id;
                    MKBFFretamentoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Reclamação".ToUpper()))
                {
                    ReclamacaoPanel.Tag = item.Id;
                    ReclamacaoPrevLabel.Tag = item.Id;
                    ReclamacaoRealLabel.Tag = item.Id;
                    //ReclamacaoPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //ReclamacaoRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }
                #endregion

                #region Processos internos
                if (item.Descricao.ToUpper().Contains("Avaria".ToUpper()))
                {
                    AvariasPanel.Tag = item.Id;
                    AvariasComCulpaPrevLabel.Tag = item.Id;
                    AvariasComCulpaRealLabel.Tag = item.Id;
                    //AvariasComCulpaPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //AvariasComCulpaRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Acidente".ToUpper()))
                {
                    AcidentesPanel.Tag = item.Id;
                    AcidentesComCulpaPrevLabel.Tag = item.Id;
                    AcidentesComCulpaRealLabel.Tag = item.Id;
                    //AcidentesComCulpaPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //AcidentesComCulpaRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (Convert.ToInt32(_referencia) < 201905)
                {
                    PartidaPanel.Visible = false;
                    PontualidadePanel.Visible = false;
                    CumprimentoPanel.Visible = true;
                    CNHVencidasPanel.Visible = true;
                    CustoMultasOrgaoPanel.Visible = true;
                    ExamesVencidosPanel.Visible = true;
                    FinanceiroPanel.Height = 915;
                    ClientePanel.Top = 943;
                    CabecalhoClientePanel.Top = 922;

                    ProcessosInternosPanel.Height = 296;
                    CabecalhoProcessoInternoPanel.Top = 1505;
                    ProcessosInternosPanel.Top = 1528;

                    AprendizadoPanel.Height = 484;
                    CabecalhoAprendizadoPanel.Top = 1832;
                    AprendizadoPanel.Top = 1855;

                    if (item.Descricao.ToUpper().Contains("Cumprimento".ToUpper()) && 
                        item.Descricao.ToUpper().Contains("Partida - Pontualidade".ToUpper()))
                    {
                        CumprimentoPanel.Tag = item.Id;
                        CumprimentoPartidaPrevLabel.Tag = item.Id;
                        CumprimentoPartidaRealLabel.Tag = item.Id;
                        //CumprimentoPartidaPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //CumprimentoPartidaRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                }
                else
                {
                    CumprimentoPanel.Visible = false;
                    CNHVencidasPanel.Visible = false;
                    ExamesVencidosPanel.Visible = false;
                    CustoMultasOrgaoPanel.Visible = false;
                    FinanceiroPanel.Height = 864;
                    ClientePanel.Top = 909;
                    CabecalhoClientePanel.Top = 888;

                    ProcessosInternosPanel.Height = 359;
                    CabecalhoProcessoInternoPanel.Top = 1470;
                    ProcessosInternosPanel.Top = 1493;

                    AprendizadoPanel.Height = 364;
                    CabecalhoAprendizadoPanel.Top = 1855;
                    AprendizadoPanel.Top = 1878;

                    PartidaPanel.Top = CumprimentoPanel.Top;
                    PartidaPanel.Left = CumprimentoPanel.Left;

                    PartidaPanel.Visible = true;
                    PontualidadePanel.Visible = true;

                    if (!item.Descricao.ToUpper().Contains("Partida - Pontualidade".ToUpper()))
                    {
                        if (item.Descricao.ToUpper().Contains("Partida".ToUpper()))
                        {
                            PartidaPanel.Tag = item.Id;
                            PartidaPrevLabel.Tag = item.Id;
                            PartidaRealLabel.Tag = item.Id;
                            //PartidaPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                            //PartidaRealLabel.Enabled = item.RealizadoPermiteAlterar;
                        }
                        if (item.Descricao.ToUpper().Contains("Pontualidade".ToUpper()))
                        {
                            PontualidadePanel.Tag = item.Id;
                            PontualidadePrevLabel.Tag = item.Id;
                            PontualidadeRealLabel.Tag = item.Id;
                            //PontualidadePrevLabel.Enabled = item.PrevistoPermiteAlterar;
                            //PontualidadeRealLabel.Enabled = item.RealizadoPermiteAlterar;
                        }
                    }
                }

                if (item.Descricao.ToUpper().Contains("Carros".ToUpper()))
                {
                    CarrosRetidosPanel.Tag = item.Id;
                    CarrosRetidosPrevLabel.Tag = item.Id;
                    CarrosRetidosRealLabel.Tag = item.Id;
                    //CarrosRetidosPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //CarrosRetidosRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Emergenciais".ToUpper()) || item.Descricao.ToUpper().Contains("Emergênciais".ToUpper()))
                {
                    ComprasEmergenciaisPanel.Tag = item.Id;
                    ComprasEmergenciaisPrevLabel.Tag = item.Id;
                    ComprasEmergenciaisRealLabel.Tag = item.Id;
                    //ComprasEmergenciaisPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //ComprasEmergenciaisRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }
                #endregion

                #region Aprendizado e Crescimento
                if (item.Descricao.ToUpper().Contains("CNH".ToUpper()))
                {
                    CNHVencidasPanel.Tag = item.Id;
                    CNHVencidasPrevLabel.Tag = item.Id;
                    CNHVencidasRealLabel.Tag = item.Id;
                    //CNHVencidasPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //CNHVencidasRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Exames".ToUpper()))
                {
                    ExamesVencidosPanel.Tag = item.Id;
                    ExamesVencidosPrevLabel.Tag = item.Id;
                    ExamesVencidosRealLabel.Tag = item.Id;
                    //ExamesVencidosPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                    //ExamesVencidosRealLabel.Enabled = item.RealizadoPermiteAlterar;
                }

                if (item.Descricao.ToUpper().Contains("Absenteísmo".ToUpper()))
                {
                    if (item.Descricao.ToUpper().Contains("Op".ToUpper()))
                    {
                        AbsenteismoOpPanel.Tag = item.Id;
                        IndicesAbsenteismoOpPrevLabel.Tag = item.Id;
                        IndicesAbsenteismoOpRealLabel.Tag = item.Id;
                        //IndicesAbsenteismoOpPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //IndicesAbsenteismoOpRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Adm".ToUpper()))
                    {
                        AbsenteismoAdmPanel.Tag = item.Id;
                        IndicesAbsenteismoAdmPrevLabel.Tag = item.Id;
                        IndicesAbsenteismoAdmRealLabel.Tag = item.Id;
                        //IndicesAbsenteismoAdmPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //IndicesAbsenteismoAdmRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Man".ToUpper()))
                    {
                        AbsenteismoManPanel.Tag = item.Id;
                        IndicesAbsenteismoManPrevLabel.Tag = item.Id;
                        IndicesAbsenteismoManRealLabel.Tag = item.Id;
                        //IndicesAbsenteismoManPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //IndicesAbsenteismoManRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    {
                        AbsenteismoPanel.Tag = item.Id;
                        IndicesAbsenteismoPrevLabel.Tag = item.Id;
                        IndicesAbsenteismoRealLabel.Tag = item.Id;
                        //IndicesAbsenteismoPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //IndicesAbsenteismoRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                }

                if (item.Descricao.ToUpper().Contains("Turnover".ToUpper()))
                {
                    if (item.Descricao.ToUpper().Contains("Op".ToUpper()))
                    {
                        TurnoverOpPanel.Tag = item.Id;
                        IndiceTurnoverOpPrevLabel.Tag = item.Id;
                        IndiceTurnoverOpRealLabel.Tag = item.Id;
                        //IndiceTurnoverOpPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //IndiceTurnoverOpRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Adm".ToUpper()))
                    {
                        TurnoverAdmPanel.Tag = item.Id;
                        IndiceTurnoverAdmPrevLabel.Tag = item.Id;
                        IndiceTurnoverAdmRealLabel.Tag = item.Id;
                        //IndiceTurnoverAdmPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //IndiceTurnoverAdmRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Man".ToUpper()))
                    {
                        TurnoverManPanel.Tag = item.Id;
                        IndiceTurnoverManPrevLabel.Tag = item.Id;
                        IndiceTurnoverManRealLabel.Tag = item.Id;
                        //IndiceTurnoverManPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //IndiceTurnoverManRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                    else
                    {
                        TurnoverPanel.Tag = item.Id;
                        IndiceTurnoverPrevLabel.Tag = item.Id;
                        IndiceTurnoverRealLabel.Tag = item.Id;
                        //IndiceTurnoverPrevLabel.Enabled = item.PrevistoPermiteAlterar;
                        //IndiceTurnoverRealLabel.Enabled = item.RealizadoPermiteAlterar;
                    }
                }
                #endregion

                BuscarRealizadoButton.Cursor = Cursors.Default;
            }

            gravarButton.Text = (_listaValoresMetas.Count() != 0 ? "&Alterar" : "&Gravar");

            if (_listaValoresMetas.Count != 0)
            {
                foreach (var item in _listaValoresMetas)
                {
                    _dataCorteFinanceiro = item.DataCorteFinanceiro;
                    _dataCorteOperacional = item.DataCorteOperacional;
                    DiasUteisCurrencyTextBox.DecimalValue = item.DiasUteis;

                    #region Financeiro
                    if (item.Descricao.ToUpper().Equals("EBITDA Financeiro".ToUpper()))
                    {
                        Ebitda2FPrevLabel.Tag = item.IdMetas;
                        Ebitda2FRealLabel.Tag = item.IdMetas;
                        Ebitda2FPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        Ebitda2FRealLabel.Text = string.Format("{0:N}", item.Realizado);
                    }

                    if (item.Descricao.ToUpper().Contains("Receita".ToUpper()) && item.Descricao.ToUpper().Contains("Líquida".ToUpper()))
                    {
                        if (item.Realizado != 0 && _dataCorteFinanceiro == DateTime.MinValue)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Financeiro ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Financeiro";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                {
                                    item.DataCorteFinanceiro = Publicas._datasLembrete[0];
                                    new Notificacoes.Mensagem("Não esqueça de gravar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                }
                            }
                        }

                        ReceitaLiquidaPrevLabel.Tag = item.IdMetas;
                        ReceitaLiquidaRealLabel.Tag = item.IdMetas;
                        ReceitaLiquidaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        ReceitaLiquidaRealLabel.Text = string.Format("{0:N}", item.Realizado);
                    }

                    if (item.Descricao.ToUpper().Equals("Custo Manutenção Frota".ToUpper()))
                    {
                            CustoManutencaoFrotaPrevLabel.Tag = item.IdMetas;
                            CustoManutencaoFrotaRealLabel.Tag = item.IdMetas;
                            CustoManutencaoFrotaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoManutencaoFrotaRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CustoManutencaoFrotaPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + CustoManutencaoFrotaPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(CustoManutencaoFrotaPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CustoManutencaoFrotaRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + CustoManutencaoFrotaRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(CustoManutencaoFrotaRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Outros".ToUpper()) && item.Descricao.ToUpper().Contains("Custos".ToUpper()))
                    {
                        OutrosCustosPrevLabel.Tag = item.IdMetas;
                        OutrosCustosRealLabel.Tag = item.IdMetas;
                        OutrosCustosPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        OutrosCustosRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            OutrosCustosPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + OutrosCustosPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(OutrosCustosPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            OutrosCustosRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + OutrosCustosRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(OutrosCustosRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Bruta".ToUpper()) && item.Descricao.ToUpper().Contains("Receita".ToUpper()) && item.Descricao.ToUpper().Contains("Total".ToUpper()))
                    {
                        ReceitaBruta1PrevLabel.Tag = item.IdMetas;
                        ReceitaBruta1RealLabel.Tag = item.IdMetas;
                        ReceitaBruta1PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        ReceitaBruta1RealLabel.Text = string.Format("{0:N}", item.Realizado);
                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ReceitaBruta2PrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + ReceitaBruta2PrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(ReceitaBruta2PrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ReceitaBruta2RealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + ReceitaBruta2RealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(ReceitaBruta2RealLabel, _tool);
                        }
                    }
                    else
                    if (item.Descricao.ToUpper().Contains("Bruta".ToUpper()) && item.Descricao.ToUpper().Contains("Receita".ToUpper()))
                    {
                        ReceitaBruta2PrevLabel.Tag = item.IdMetas;
                        ReceitaBruta2RealLabel.Tag = item.IdMetas;

                        ReceitaBruta2PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        ReceitaBruta3PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        ReceitaBruta4PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        ReceitaBruta2RealLabel.Text = string.Format("{0:N}", item.Realizado);
                        ReceitaBruta3RealLabel.Text = string.Format("{0:N}", item.Realizado);
                        ReceitaBruta4RealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ReceitaBruta2PrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + ReceitaBruta2PrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(ReceitaBruta2PrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ReceitaBruta2RealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + ReceitaBruta2RealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(ReceitaBruta2RealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Receita Subsídio".ToUpper()))
                    {
                        ReceitaSubsidioPrevLabel.Tag = item.IdMetas;
                        ReceitaSubsidioRealLabel.Tag = item.IdMetas;
                        ReceitaSubsidioPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        ReceitaSubsidioRealLabel.Text = string.Format("{0:N}", item.Realizado);
                        ReceitaSubsidio2PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        ReceitaSubsidio2RealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ReceitaSubsidioPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + ReceitaSubsidioPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(ReceitaSubsidioPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ReceitaSubsidioRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + ReceitaSubsidioRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(ReceitaSubsidioRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Folha".ToUpper()))
                    {
                        if (item.Descricao.ToUpper().Contains("Op".ToUpper()) && item.Descricao.ToUpper().Contains("Custo".ToUpper()))
                        {
                            CustoFolhaOp1PrevLabel.Tag = item.IdMetas;
                            CustoFolhaOp1RealLabel.Tag = item.IdMetas;
                            CustoFolhaOp1PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoFolhaOp2PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoFolhaOp1RealLabel.Text = string.Format("{0:N}", item.Realizado);
                            CustoFolhaOp2RealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoFolhaOp1PrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + CustoFolhaOp1PrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(CustoFolhaOp1PrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoFolhaOp1RealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + CustoFolhaOp1RealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(CustoFolhaOp1RealLabel, _tool);
                            }
                        }
                        else
                        if (item.Descricao.ToUpper().Contains("Adm".ToUpper()) && item.Descricao.ToUpper().Contains("Custo".ToUpper()))
                        {
                            CustoFolhaAdm1PrevLabel.Tag = item.IdMetas;
                            CustoFolhaAdm1RealLabel.Tag = item.IdMetas;
                            CustoFolhaAdm1PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoFolhaAdm2PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoFolhaAdm1RealLabel.Text = string.Format("{0:N}", item.Realizado);
                            CustoFolhaAdm2RealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoFolhaAdm1PrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + CustoFolhaAdm1PrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(CustoFolhaAdm1PrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoFolhaAdm1RealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + CustoFolhaAdm1RealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(CustoFolhaAdm1RealLabel, _tool);
                            }
                        }
                        else
                        if (item.Descricao.ToUpper().Contains("Man".ToUpper()) && item.Descricao.ToUpper().Contains("Custo".ToUpper()))
                        {
                            CustoFolhaMan1PrevLabel.Tag = item.IdMetas;
                            CustoFolhaMan1RealLabel.Tag = item.IdMetas;
                            CustoFolhaMan1PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoFolhaMan2PrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoFolhaMan1RealLabel.Text = string.Format("{0:N}", item.Realizado);
                            CustoFolhaMan2RealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoFolhaMan1PrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + CustoFolhaMan1PrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(CustoFolhaMan1PrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoFolhaMan1RealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + CustoFolhaMan1RealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(CustoFolhaMan1RealLabel, _tool);
                            }
                        }
                        //else
                        //if (item.Descricao.ToUpper().Contains("Man".ToUpper()) && item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
                        //{
                        //    EficienciaFolhaManPrevLabel.Tag = item.IdMetas;
                        //    EficienciaFolhaManRealLabel.Tag = item.IdMetas;
                        //    EficienciaFolhaManPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        //    EficienciaFolhaManRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        //    if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        //    {
                        //        EficienciaFolhaManPrevLabel.PositiveColor = Color.OrangeRed;
                        //        ToolTipInfo _tool = new ToolTipInfo();
                        //        _tool.Footer.Font = label41.Font;
                        //        _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                        //                            "Previsto Atual ".PadRight(20) + EficienciaFolhaManPrevLabel.Text + Environment.NewLine +
                        //                            "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                        //        superToolTip1.SetToolTip(EficienciaFolhaManPrevLabel, _tool);
                        //    }

                        //    if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        //    {
                        //        EficienciaFolhaManRealLabel.PositiveColor = Color.OrangeRed;
                        //        ToolTipInfo _tool = new ToolTipInfo();
                        //        _tool.Footer.Font = label41.Font;
                        //        _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                        //                            "Real Atual ".PadRight(20) + EficienciaFolhaManRealLabel.Text + Environment.NewLine +
                        //                            "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                        //        superToolTip1.SetToolTip(EficienciaFolhaManRealLabel, _tool);
                        //    }
                        //}
                        //else
                        //if (item.Descricao.ToUpper().Contains("Op".ToUpper()) && item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
                        //{
                        //    EficienciaFolhaOpPrevLabel.Tag = item.IdMetas;
                        //    EficienciaFolhaOpRealLabel.Tag = item.IdMetas;
                        //    EficienciaFolhaOpPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        //    EficienciaFolhaOpRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        //    if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        //    {
                        //        EficienciaFolhaOpPrevLabel.PositiveColor = Color.OrangeRed;
                        //        ToolTipInfo _tool = new ToolTipInfo();
                        //        _tool.Footer.Font = label41.Font;
                        //        _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                        //                            "Previsto Atual ".PadRight(20) + EficienciaFolhaOpPrevLabel.Text + Environment.NewLine +
                        //                            "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                        //        superToolTip1.SetToolTip(EficienciaFolhaOpPrevLabel, _tool);
                        //    }

                        //    if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        //    {
                        //        EficienciaFolhaOpRealLabel.PositiveColor = Color.OrangeRed;
                        //        ToolTipInfo _tool = new ToolTipInfo();
                        //        _tool.Footer.Font = label41.Font;
                        //        _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                        //                            "Real Atual ".PadRight(20) + EficienciaFolhaOpRealLabel.Text + Environment.NewLine +
                        //                            "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                        //        superToolTip1.SetToolTip(EficienciaFolhaOpRealLabel, _tool);
                        //    }
                        //}
                        //else
                        //if (item.Descricao.ToUpper().Contains("Adm".ToUpper()) && item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
                        //{
                        //    EficienciaFolhaAdmPrevLabel.Tag = item.IdMetas;
                        //    EficienciaFolhaAdmRealLabel.Tag = item.IdMetas;
                        //    EficienciaFolhaAdmPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        //    EficienciaFolhaAdmRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        //    if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        //    {
                        //        EficienciaFolhaAdmPrevLabel.PositiveColor = Color.OrangeRed;
                        //        ToolTipInfo _tool = new ToolTipInfo();
                        //        _tool.Footer.Font = label41.Font;
                        //        _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                        //                            "Previsto Atual ".PadRight(20) + EficienciaFolhaAdmPrevLabel.Text + Environment.NewLine +
                        //                            "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                        //        superToolTip1.SetToolTip(EficienciaFolhaAdmPrevLabel, _tool);
                        //    }

                        //    if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        //    {
                        //        EficienciaFolhaAdmRealLabel.PositiveColor = Color.OrangeRed;
                        //        ToolTipInfo _tool = new ToolTipInfo();
                        //        _tool.Footer.Font = label41.Font;
                        //        _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                        //                            "Real Atual ".PadRight(20) + EficienciaFolhaAdmRealLabel.Text + Environment.NewLine +
                        //                            "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                        //        superToolTip1.SetToolTip(EficienciaFolhaAdmRealLabel, _tool);
                        //    }
                        //}
                        else
                        if (item.Descricao.ToUpper().Contains("Custo".ToUpper()))
                        {
                            CustoFolhaTotalPrevLabel.Tag = item.IdMetas;
                            CustoFolhaTotalRealLabel.Tag = item.IdMetas;
                            CustoFolhaTotalPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoFolhaTotalRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoFolhaTotalPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + CustoFolhaTotalPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(CustoFolhaTotalPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoFolhaTotalRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + CustoFolhaTotalRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(CustoFolhaTotalRealLabel, _tool);
                            }
                        }
                        else
                        if (item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
                        {
                            EficienciaFolhaTotalPrevLabel.Tag = item.IdMetas;
                            EficienciaFolhaTotalRealLabel.Tag = item.IdMetas;
                            EficienciaFolhaTotalPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            EficienciaFolhaTotalRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                EficienciaFolhaTotalPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + EficienciaFolhaTotalPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(EficienciaFolhaTotalPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                EficienciaFolhaTotalRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + EficienciaFolhaTotalRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(EficienciaFolhaTotalRealLabel, _tool);
                            }
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Eficiência de Man".ToUpper()))
                    {
                        EficienciaManutencaoFrotaPrevLabel.Tag = item.IdMetas;
                        EficienciaManutencaoFrotaRealLabel.Tag = item.IdMetas;
                        EficienciaManutencaoFrotaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        EficienciaManutencaoFrotaRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            EficienciaManutencaoFrotaRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + EficienciaManutencaoFrotaRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(EficienciaManutencaoFrotaRealLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            EficienciaManutencaoFrotaRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + EficienciaManutencaoFrotaRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(EficienciaManutencaoFrotaRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Km".ToUpper()) && item.Descricao.ToUpper().Contains("Rodado".ToUpper()))
                    {
                        KmRodadoPrevLabel.Tag = item.IdMetas;
                        KmRodadoRealLabel.Tag = item.IdMetas;
                        KmRodadoPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        KmRodadoRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            KmRodadoPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + KmRodadoPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(KmRodadoPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            KmRodadoRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + KmRodadoRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(KmRodadoRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Pneus".ToUpper()))
                    {
                        CustoPneuPrevLabel.Tag = item.IdMetas;
                        CustoPneuRealLabel.Tag = item.IdMetas;
                        CustoPneuPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        CustoPneuRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CustoPneuPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + CustoPneuPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(CustoPneuPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CustoPneuRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + CustoPneuRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(CustoPneuRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Peças".ToUpper()))
                    {
                        CustoPecasPrevLabel.Tag = item.IdMetas;
                        CustoPecasRealLabel.Tag = item.IdMetas;
                        CustoPecasPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        CustoPecasRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CustoPecasPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + CustoPecasPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(CustoPecasPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CustoPecasRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + CustoPecasRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(CustoPecasRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Custo Hora".ToUpper()) && item.Descricao.ToUpper().Contains("Extra".ToUpper()))
                    {
                        if (item.Descricao.ToUpper().Contains("+".ToUpper()))
                        {
                            CustoHorasExtrasSomaPrevLabel.Tag = item.IdMetas;
                            CustoHorasExtrasSomaRealLabel.Tag = item.IdMetas;
                            CustoHorasExtrasSomaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoHorasExtrasSomaRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoHorasExtrasSomaPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + CustoHorasExtrasSomaPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(CustoHorasExtrasSomaPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoHorasExtrasSomaRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + CustoHorasExtrasSomaRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(CustoHorasExtrasSomaRealLabel, _tool);
                            }
                        }
                        else
                        if (item.Descricao.ToUpper().Contains("Op".ToUpper()))
                        {
                            CustoHorasExtrasOpPrevLabel.Tag = item.IdMetas;
                            CustoHorasExtrasOpRealLabel.Tag = item.IdMetas;
                            CustoHorasExtrasOpPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoHorasExtrasOpRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoHorasExtrasOpPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + CustoHorasExtrasOpPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(CustoHorasExtrasOpPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoHorasExtrasOpRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + CustoHorasExtrasOpRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(CustoHorasExtrasOpRealLabel, _tool);
                            }
                        }
                        else
                        if (item.Descricao.ToUpper().Contains("Man".ToUpper()))
                        {
                            CustoHorasExtrasManPrevLabel.Tag = item.IdMetas;
                            CustoHorasExtrasManRealLabel.Tag = item.IdMetas;
                            CustoHorasExtrasManPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoHorasExtrasManRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoHorasExtrasManPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + CustoHorasExtrasManPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(CustoHorasExtrasManPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoHorasExtrasManRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + CustoHorasExtrasManRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(CustoHorasExtrasManRealLabel, _tool);
                            }
                        }
                        else
                        if (item.Descricao.ToUpper().Contains("Adm".ToUpper()))
                        {
                            CustoHorasExtrasAdmPrevLabel.Tag = item.IdMetas;
                            CustoHorasExtrasAdmRealLabel.Tag = item.IdMetas;
                            CustoHorasExtrasAdmPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoHorasExtrasAdmRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoHorasExtrasAdmPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + CustoHorasExtrasAdmPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(CustoHorasExtrasAdmPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoHorasExtrasAdmRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + CustoHorasExtrasAdmRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(CustoHorasExtrasAdmRealLabel, _tool);
                            }
                        }
                        else
                        
                        {
                            CustoHorasExtrasPrevLabel.Tag = item.IdMetas;
                            CustoHorasExtrasRealLabel.Tag = item.IdMetas;
                            CustoHorasExtrasPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CustoHorasExtrasRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoHorasExtrasPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + CustoHorasExtrasPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(CustoHorasExtrasPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CustoHorasExtrasRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + CustoHorasExtrasRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(CustoHorasExtrasRealLabel, _tool);
                            }
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Multa".ToUpper()) && item.Descricao.ToUpper().Contains("Gestor".ToUpper()))
                    {
                        CustoMultasOrgaoGestorPrevLabel.Tag = item.IdMetas;
                        CustoMultasOrgaoGestorRealLabel.Tag = item.IdMetas;
                        CustoMultasOrgaoGestorPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        CustoMultasOrgaoGestorRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CustoMultasOrgaoGestorPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + CustoMultasOrgaoGestorPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(CustoMultasOrgaoGestorPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CustoMultasOrgaoGestorRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + CustoMultasOrgaoGestorRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(CustoMultasOrgaoGestorRealLabel, _tool);
                        }

                        if (!gravarButton.Text.Contains("Gravar") && _dataCorteOperacional != DateTime.MinValue &&
                            item.Previsto == 0 && item.Realizado == 0)
                           CustoMultasPictureBox.ImageLocation = _arquivoVerde;
                        //if (item.Previsto == 0 && item.Realizado == 0 && (item.IdEmpresa == 6 || item.IdEmpresa == 7))// cisne e aruja conforme Erika Scucuglia
                    }

                    if (item.Descricao.ToUpper().Contains("Deduções".ToUpper()))
                    {
                        DeducoesReceitaPrevLabel.Tag = item.IdMetas;
                        DeducoesReceitaRealLabel.Tag = item.IdMetas;
                        DeducoesReceitaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        DeducoesReceitaRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            DeducoesReceitaPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + DeducoesReceitaPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(DeducoesReceitaPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            DeducoesReceitaRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + DeducoesReceitaRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(DeducoesReceitaRealLabel, _tool);
                        }
                    }

                    #endregion

                    #region Cliente
                    
                    if (item.Descricao.ToUpper().Contains("Índice de Limpeza de frota".ToUpper()))
                    {
                        IndiceLimpezaFrotaPrevLabel.Tag = item.IdMetas;
                        IndiceLimpezaFrotaRealLabel.Tag = item.IdMetas;
                        IndiceLimpezaFrotaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        IndiceLimpezaFrotaRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            IndiceLimpezaFrotaPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + IndiceLimpezaFrotaPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(IndiceLimpezaFrotaPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            IndiceLimpezaFrotaRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + IndiceLimpezaFrotaRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(IndiceLimpezaFrotaRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Índice KM/MKBF".ToUpper()))
                    {

                        if (item.Realizado != 0 && _dataCorteOperacional == DateTime.MinValue) //Realizado M.K.B.F.
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                {
                                    item.DataCorteOperacional = Publicas._datasLembrete[0];
                                    new Notificacoes.Mensagem("Não esqueça de gravar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                }
                            }
                        }
                        
                        MKBFPrevLabel.Tag = item.IdMetas;
                        MKBFRealLabel.Tag = item.IdMetas;
                        MKBFPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        MKBFRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N0}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + MKBFPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(MKBFPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N0}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + MKBFRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(MKBFRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("MKBF Rodoviário".ToUpper()))
                    {

                        if (item.Realizado != 0 && _dataCorteOperacional == DateTime.MinValue)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                {
                                    item.DataCorteOperacional = Publicas._datasLembrete[0];
                                    new Notificacoes.Mensagem("Não esqueça de gravar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                }
                            }
                        }

                        MKBFRodoviarioPrevLabel.Tag = item.IdMetas;
                        MKBFRodoviarioRealLabel.Tag = item.IdMetas;
                        MKBFRodoviarioPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        MKBFRodoviarioRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFRodoviarioPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N0}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + MKBFRodoviarioPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(MKBFRodoviarioPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFRodoviarioRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N0}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + MKBFRodoviarioRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(MKBFRodoviarioRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("MKBF Urbano".ToUpper()))
                    {

                        if (item.Realizado != 0 && _dataCorteOperacional == DateTime.MinValue)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                {
                                    item.DataCorteOperacional = Publicas._datasLembrete[0];
                                    new Notificacoes.Mensagem("Não esqueça de gravar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                }
                            }
                        }

                        MKBFUrbanoPrevLabel.Tag = item.IdMetas;
                        MKBFUrbanoRealLabel.Tag = item.IdMetas;
                        MKBFUrbanoPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        MKBFUrbanoRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFUrbanoPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N0}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + MKBFUrbanoPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(MKBFUrbanoPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFUrbanoRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N0}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + MKBFUrbanoRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(MKBFUrbanoRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("MKBF Suburbano".ToUpper()))
                    {

                        if (item.Realizado != 0 && _dataCorteOperacional == DateTime.MinValue)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                {
                                    item.DataCorteOperacional = Publicas._datasLembrete[0];
                                    new Notificacoes.Mensagem("Não esqueça de gravar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                }
                            }
                        }

                        MKBFSubUrbanoPrevLabel.Tag = item.IdMetas;
                        MKBFSubUrbanoRealLabel.Tag = item.IdMetas;
                        MKBFSubUrbanoPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        MKBFSubUrbanoRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFSubUrbanoPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N0}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + MKBFSubUrbanoPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(MKBFSubUrbanoPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFSubUrbanoRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N0}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + MKBFSubUrbanoRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(MKBFSubUrbanoRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("MKBF Intermunicipal".ToUpper()))
                    {

                        if (item.Realizado != 0 && _dataCorteOperacional == DateTime.MinValue)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                {
                                    item.DataCorteOperacional = Publicas._datasLembrete[0];
                                    new Notificacoes.Mensagem("Não esqueça de gravar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                }
                            }
                        }

                        MKBFIntermunicipalPrevLabel.Tag = item.IdMetas;
                        MKBFIntermunicipalRealLabel.Tag = item.IdMetas;
                        MKBFIntermunicipalPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        MKBFIntermunicipalRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFIntermunicipalPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N0}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + MKBFIntermunicipalPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(MKBFIntermunicipalPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFIntermunicipalRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N0}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + MKBFIntermunicipalRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(MKBFIntermunicipalRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("MKBF Municipal".ToUpper()))
                    {

                        if (item.Realizado != 0 && _dataCorteOperacional == DateTime.MinValue)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                {
                                    item.DataCorteOperacional = Publicas._datasLembrete[0];
                                    new Notificacoes.Mensagem("Não esqueça de gravar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                }
                            }
                        }

                        MKBFMunicipalPrevLabel.Tag = item.IdMetas;
                        MKBFMunicipalRealLabel.Tag = item.IdMetas;
                        MKBFMunicipalPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        MKBFMunicipalRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFMunicipalPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N0}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + MKBFMunicipalPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(MKBFMunicipalPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFMunicipalRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N0}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + MKBFMunicipalRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(MKBFMunicipalRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("MKBF Escolar".ToUpper()))
                    {

                        if (item.Realizado != 0 && _dataCorteOperacional == DateTime.MinValue)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                {
                                    item.DataCorteOperacional = Publicas._datasLembrete[0];
                                    new Notificacoes.Mensagem("Não esqueça de gravar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                }
                            }
                        }

                        MKBFEscolarPrevLabel.Tag = item.IdMetas;
                        MKBFEscolarRealLabel.Tag = item.IdMetas;
                        MKBFEscolarPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        MKBFEscolarRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFEscolarPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N0}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + MKBFEscolarPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(MKBFEscolarPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFEscolarRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N0}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + MKBFEscolarRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(MKBFEscolarRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("MKBF Fretamento".ToUpper()))
                    {

                        if (item.Realizado != 0 && _dataCorteOperacional == DateTime.MinValue)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                {
                                    item.DataCorteOperacional = Publicas._datasLembrete[0];
                                    new Notificacoes.Mensagem("Não esqueça de gravar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                                }
                            }
                        }

                        MKBFFretamentoPrevLabel.Tag = item.IdMetas;
                        MKBFFretamentoRealLabel.Tag = item.IdMetas;
                        MKBFFretamentoPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        MKBFFretamentoRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFFretamentoPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N0}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + MKBFFretamentoPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(MKBFFretamentoPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            MKBFFretamentoRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N0}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + MKBFFretamentoRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(MKBFFretamentoRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Reclamação".ToUpper()))
                    {
                        ReclamacaoPrevLabel.Tag = item.IdMetas;
                        ReclamacaoRealLabel.Tag = item.IdMetas;
                        ReclamacaoPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        ReclamacaoRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ReclamacaoPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + ReclamacaoPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(ReclamacaoPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ReclamacaoRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + ReclamacaoRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(ReclamacaoRealLabel, _tool);
                        }
                    }
                    #endregion

                    #region Processos internos
                    if (item.Descricao.ToUpper().Contains("Avaria".ToUpper()))
                    {
                        AvariasComCulpaPrevLabel.Tag = item.IdMetas;
                        AvariasComCulpaRealLabel.Tag = item.IdMetas;
                        AvariasComCulpaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        AvariasComCulpaRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            AvariasComCulpaPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + AvariasComCulpaPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(AvariasComCulpaPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            AvariasComCulpaRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + AvariasComCulpaRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(AvariasComCulpaRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Acidente".ToUpper()))
                    {
                        AcidentesComCulpaPrevLabel.Tag = item.IdMetas;
                        AcidentesComCulpaRealLabel.Tag = item.IdMetas;
                        AcidentesComCulpaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        AcidentesComCulpaRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            AcidentesComCulpaPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + AcidentesComCulpaPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(AcidentesComCulpaPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            AcidentesComCulpaRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + AcidentesComCulpaRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(AcidentesComCulpaRealLabel, _tool);
                        }
                    }

                    if (Convert.ToInt32(_referencia) < 201905)
                    {
                        if (item.Descricao.ToUpper().Contains("Cumprimento".ToUpper()) &&
                            item.Descricao.ToUpper().Contains("Partida - Pontualidade".ToUpper()))
                        {
                            CumprimentoPanel.Tag = item.IdMetas;
                            CumprimentoPartidaPrevLabel.Tag = item.IdMetas;
                            CumprimentoPartidaRealLabel.Tag = item.IdMetas;
                            CumprimentoPartidaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            CumprimentoPartidaRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CumprimentoPartidaPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + CumprimentoPartidaPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(CumprimentoPartidaPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                CumprimentoPartidaRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + CumprimentoPartidaRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(CumprimentoPartidaRealLabel, _tool);
                            }
                        }
                    }
                    else
                    {
                        if (!item.Descricao.ToUpper().Contains("Partida - Pontualidade".ToUpper()))
                        {
                            if (item.Descricao.ToUpper().Contains("Partida".ToUpper()))
                            {
                                PartidaPanel.Tag = item.IdMetas;
                                PartidaPrevLabel.Tag = item.IdMetas;
                                PartidaRealLabel.Tag = item.IdMetas;
                                PartidaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                                PartidaRealLabel.Text = string.Format("{0:N}", item.Realizado);

                                if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                                {
                                    PartidaPrevLabel.PositiveColor = Color.OrangeRed;
                                    ToolTipInfo _tool = new ToolTipInfo();
                                    _tool.Footer.Font = label41.Font;
                                    _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                        "Previsto Atual ".PadRight(20) + PartidaPrevLabel.Text + Environment.NewLine +
                                                        "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                    superToolTip1.SetToolTip(PartidaPrevLabel, _tool);
                                }

                                if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                                {
                                    PartidaRealLabel.PositiveColor = Color.OrangeRed;
                                    ToolTipInfo _tool = new ToolTipInfo();
                                    _tool.Footer.Font = label41.Font;
                                    _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                        "Real Atual ".PadRight(20) + PartidaRealLabel.Text + Environment.NewLine +
                                                        "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                    superToolTip1.SetToolTip(PartidaRealLabel, _tool);
                                }
                            }

                            if (item.Descricao.ToUpper().Contains("Pontualidade".ToUpper()))
                            {
                                PontualidadePanel.Tag = item.IdMetas;
                                PontualidadePrevLabel.Tag = item.IdMetas;
                                PontualidadeRealLabel.Tag = item.IdMetas;
                                PontualidadePrevLabel.Text = string.Format("{0:N}", item.Previsto);
                                PontualidadeRealLabel.Text = string.Format("{0:N}", item.Realizado);

                                if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                                {
                                    PartidaPrevLabel.PositiveColor = Color.OrangeRed;
                                    ToolTipInfo _tool = new ToolTipInfo();
                                    _tool.Footer.Font = label41.Font;
                                    _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                        "Previsto Atual ".PadRight(20) + PontualidadePrevLabel.Text + Environment.NewLine +
                                                        "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                    superToolTip1.SetToolTip(PontualidadePrevLabel, _tool);
                                }

                                if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                                {
                                    PontualidadeRealLabel.PositiveColor = Color.OrangeRed;
                                    ToolTipInfo _tool = new ToolTipInfo();
                                    _tool.Footer.Font = label41.Font;
                                    _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                        "Real Atual ".PadRight(20) + PontualidadeRealLabel.Text + Environment.NewLine +
                                                        "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                    superToolTip1.SetToolTip(PontualidadeRealLabel, _tool);
                                }
                            }
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Carros".ToUpper()))
                    {
                        CarrosRetidosPrevLabel.Tag = item.IdMetas;
                        CarrosRetidosRealLabel.Tag = item.IdMetas;
                        CarrosRetidosPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        CarrosRetidosRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CarrosRetidosPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + item.PrevistoOriginal.ToString() + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + CarrosRetidosPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(CarrosRetidosPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CarrosRetidosRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + item.RealizadoOriginal.ToString() + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + CarrosRetidosRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(CarrosRetidosRealLabel, _tool);
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Emergenciais".ToUpper()) || item.Descricao.ToUpper().Contains("Compra".ToUpper()) || item.Descricao.ToUpper().Contains("Emergênciais".ToUpper()))
                    {
                        ComprasEmergenciaisPrevLabel.Tag = item.IdMetas;
                        ComprasEmergenciaisRealLabel.Tag = item.IdMetas;
                        ComprasEmergenciaisPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        ComprasEmergenciaisRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ComprasEmergenciaisPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + ComprasEmergenciaisPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(ComprasEmergenciaisPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ComprasEmergenciaisRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + ComprasEmergenciaisRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(ComprasEmergenciaisRealLabel, _tool);
                        }
                    }
                    #endregion

                    #region Aprendizado e Crescimento
                    if (item.Descricao.ToUpper().Contains("CNH".ToUpper()))
                    {
                        CNHVencidasPrevLabel.Tag = item.IdMetas;
                        CNHVencidasRealLabel.Tag = item.IdMetas;
                        CNHVencidasPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        CNHVencidasRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CNHVencidasPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + item.PrevistoOriginal.ToString() + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + CNHVencidasPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(CNHVencidasPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            CNHVencidasRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + item.RealizadoOriginal.ToString() + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + CNHVencidasRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(CNHVencidasRealLabel, _tool);
                        }

                        if (!gravarButton.Text.Contains("Gravar") && _dataCorteOperacional != DateTime.MinValue &&
                            item.Previsto == 0 && item.Realizado == 0)
                            CHNVencidasPictureBox.ImageLocation = _arquivoVerde;
                    }

                    if (item.Descricao.ToUpper().Contains("Exames".ToUpper()))
                    {
                        ExamesVencidosPrevLabel.Tag = item.IdMetas;
                        ExamesVencidosRealLabel.Tag = item.IdMetas;
                        ExamesVencidosPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                        ExamesVencidosRealLabel.Text = string.Format("{0:N}", item.Realizado);

                        if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ExamesVencidosPrevLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + item.PrevistoOriginal.ToString() + Environment.NewLine +
                                                "Previsto Atual ".PadRight(20) + ExamesVencidosPrevLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                            superToolTip1.SetToolTip(ExamesVencidosPrevLabel, _tool);
                        }

                        if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                        {
                            ExamesVencidosRealLabel.PositiveColor = Color.OrangeRed;
                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Font = label41.Font;
                            _tool.Footer.Text = "Real Anterior ".PadRight(20) + item.RealizadoOriginal.ToString() + Environment.NewLine +
                                                "Real Atual ".PadRight(20) + ExamesVencidosRealLabel.Text + Environment.NewLine +
                                                "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                            superToolTip1.SetToolTip(ExamesVencidosRealLabel, _tool);
                        }

                        if (!gravarButton.Text.Contains("Gravar") && _dataCorteOperacional != DateTime.MinValue &&
                            item.Previsto == 0 && item.Realizado == 0)
                            ExamesVencidosPictureBox.ImageLocation = _arquivoVerde;
                    }

                    if (item.Descricao.ToUpper().Contains("Absenteísmo".ToUpper()))
                    {
                        if (item.Descricao.ToUpper().Contains("Op".ToUpper()))
                        {
                            IndicesAbsenteismoOpPrevLabel.Tag = item.IdMetas;
                            IndicesAbsenteismoOpRealLabel.Tag = item.IdMetas;
                            IndicesAbsenteismoOpPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            IndicesAbsenteismoOpRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndicesAbsenteismoOpPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + IndicesAbsenteismoOpPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(IndicesAbsenteismoOpPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndicesAbsenteismoOpRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + IndicesAbsenteismoOpRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(IndicesAbsenteismoOpRealLabel, _tool);
                            }
                        }
                        else
                        if (item.Descricao.ToUpper().Contains("Adm".ToUpper()))
                        {
                            IndicesAbsenteismoAdmPrevLabel.Tag = item.IdMetas;
                            IndicesAbsenteismoAdmRealLabel.Tag = item.IdMetas;
                            IndicesAbsenteismoAdmPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            IndicesAbsenteismoAdmRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndicesAbsenteismoAdmPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + IndicesAbsenteismoAdmPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(IndicesAbsenteismoAdmPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndicesAbsenteismoAdmRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + IndicesAbsenteismoAdmRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(IndicesAbsenteismoAdmRealLabel, _tool);
                            }
                        }
                        else
                        if (item.Descricao.ToUpper().Contains("Man".ToUpper()))
                        {
                            IndicesAbsenteismoManPrevLabel.Tag = item.IdMetas;
                            IndicesAbsenteismoManRealLabel.Tag = item.IdMetas;
                            IndicesAbsenteismoManPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            IndicesAbsenteismoManRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndicesAbsenteismoManPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + IndicesAbsenteismoManPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(IndicesAbsenteismoManPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndicesAbsenteismoManRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + IndicesAbsenteismoManRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(IndicesAbsenteismoManRealLabel, _tool);
                            }
                        }
                        else
                        {
                            IndicesAbsenteismoPrevLabel.Tag = item.IdMetas;
                            IndicesAbsenteismoRealLabel.Tag = item.IdMetas;
                            IndicesAbsenteismoPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            IndicesAbsenteismoRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndicesAbsenteismoPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + IndicesAbsenteismoPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(IndicesAbsenteismoPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndicesAbsenteismoRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + IndicesAbsenteismoRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(IndicesAbsenteismoRealLabel, _tool);
                            }
                        }
                    }

                    if (item.Descricao.ToUpper().Contains("Turnover".ToUpper()))
                    {
                        if (item.Descricao.ToUpper().Contains("Op".ToUpper()))
                        {
                            IndiceTurnoverOpPrevLabel.Tag = item.IdMetas;
                            IndiceTurnoverOpRealLabel.Tag = item.IdMetas;
                            IndiceTurnoverOpPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            IndiceTurnoverOpRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndiceTurnoverOpPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + IndiceTurnoverOpPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(IndiceTurnoverOpPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndiceTurnoverOpRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + IndiceTurnoverOpRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(IndiceTurnoverOpRealLabel, _tool);
                            }
                        }
                        else
                        if (item.Descricao.ToUpper().Contains("Adm".ToUpper()))
                        {
                            IndiceTurnoverAdmPrevLabel.Tag = item.IdMetas;
                            IndiceTurnoverAdmRealLabel.Tag = item.IdMetas;
                            IndiceTurnoverAdmPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            IndiceTurnoverAdmRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndiceTurnoverAdmPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + IndiceTurnoverAdmPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(IndiceTurnoverAdmPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndiceTurnoverAdmRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + IndiceTurnoverAdmRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(IndiceTurnoverAdmRealLabel, _tool);
                            }
                        }
                        else
                        if (item.Descricao.ToUpper().Contains("Man".ToUpper()))
                        {
                            IndiceTurnoverManPrevLabel.Tag = item.IdMetas;
                            IndiceTurnoverManRealLabel.Tag = item.IdMetas;
                            IndiceTurnoverManPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            IndiceTurnoverManRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndiceTurnoverManPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + IndiceTurnoverManPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(IndiceTurnoverManPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndiceTurnoverManRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + IndiceTurnoverManRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(IndiceTurnoverManRealLabel, _tool);
                            }
                        }
                        else
                        {
                            IndiceTurnoverPrevLabel.Tag = item.IdMetas;
                            IndiceTurnoverRealLabel.Tag = item.IdMetas;
                            IndiceTurnoverPrevLabel.Text = string.Format("{0:N}", item.Previsto);
                            IndiceTurnoverRealLabel.Text = string.Format("{0:N}", item.Realizado);

                            if (item.Previsto != item.PrevistoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndiceTurnoverPrevLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + string.Format("{0:N}", item.PrevistoOriginal) + Environment.NewLine +
                                                    "Previsto Atual ".PadRight(20) + IndiceTurnoverPrevLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoPrevisto;
                                superToolTip1.SetToolTip(IndiceTurnoverPrevLabel, _tool);
                            }

                            if (item.Realizado != item.RealizadoOriginal && Publicas._usuario.PermiteAlterarBSC)
                            {
                                IndiceTurnoverRealLabel.PositiveColor = Color.OrangeRed;
                                ToolTipInfo _tool = new ToolTipInfo();
                                _tool.Footer.Font = label41.Font;
                                _tool.Footer.Text = "Real Anterior ".PadRight(20) + string.Format("{0:N}", item.RealizadoOriginal) + Environment.NewLine +
                                                    "Real Atual ".PadRight(20) + IndiceTurnoverRealLabel.Text + Environment.NewLine +
                                                    "Motivo ".PadRight(20) + item.MotivoEdicaoReal;
                                superToolTip1.SetToolTip(IndiceTurnoverRealLabel, _tool);
                            }
                        }
                    }

                    #endregion
                }
            }

            try
            {
                _dataCorteFinanceiro = _listaValoresMetas.Max(m => m.DataCorteFinanceiro);
                _dataCorteOperacional = _listaValoresMetas.Max(m => m.DataCorteOperacional);

                DataCorteLabel.Text = (_dataCorteOperacional == DateTime.MinValue ? "" : "Data Corte Operacional " + _dataCorteOperacional.ToShortDateString()) 
                    + Environment.NewLine 
                    + (_dataCorteFinanceiro == DateTime.MinValue ? "" : "Data Corte Financeiro " + _dataCorteFinanceiro.ToShortDateString());
            }
            catch { }

            ReceitaLiquidaPrevLabel_DecimalValueChanged(sender, e);
            ReceitaLiquidaRealLabel_DecimalValueChanged(sender, e);
            CustoFolhaAdm1PrevLabel_DecimalValueChanged(sender, e);
            CustoFolhaAdm1RealLabel_DecimalValueChanged(sender, e);
            CustoHorasExtrasOpPrevLabel_DecimalValueChanged(sender, e);
            CustoHorasExtrasOpRealLabel_DecimalValueChanged(sender, e);
            CustoPecasRealLabel_DecimalValueChanged(sender, e);
            CustoPecasPrevLabel_DecimalValueChanged(sender, e);

            // Para excluir não pode ter usado no contrato de metas/Definição das metas. 
            excluirButton.Enabled = _listaValoresMetas.Count() != 0 && _listaValoresMetas.Where(w => w.AplicouNoContratoDeMetas).Count() == 0;
            BuscarPrevistoButton.Enabled = _listaValoresMetas.Count() == 0 || _listaValoresMetas.Where(w => w.AplicouNoContratoDeMetas).Count() == 0;
            BuscarRealizadoButton.Enabled = _listaValoresMetas.Count() == 0 || _listaValoresMetas.Where(w => w.AplicouNoContratoDeMetas).Count() == 0;
            gravarButton.Enabled = true;
            ContratoDeMetasButton.Enabled = !Publicas._usuario.PermiteAlterarBSC;
            referenciaMaskedEditBox.Cursor = Cursors.Default;

            mensagemSistemaLabel.Text = "";
            this.Refresh();


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

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                referenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                textBox1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void EbitdaPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EbitdaRealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void ReceitaLiquidaPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReceitaLiquidaRealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EbitdaRealLabel.Focus();
            }
        }

        private void EbitdaRevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReceitaLiquidaPrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EbitdaPrevLabel.Focus();
            }
        }

        private void ReceitaLiquidaRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReceitaBruta1PrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaLiquidaPrevLabel.Focus();
            }
        }

        private void ReceitaBruta1PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReceitaBruta1RealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaLiquidaRealLabel.Focus();
            }
        }

        private void ReceitaBruta1RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReceitaBruta2PrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaBruta1PrevLabel.Focus();
            }
        }

        private void ReceitaBruta2PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    ReceitaBruta2RealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaBruta1RealLabel.Focus();
            }
        }

        private void ReceitaBruta2RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                {
                    if (ReceitaSubsidioPrevLabel.Enabled)
                        ReceitaSubsidioPrevLabel.Focus();
                    else
                    {
                        if (DeducoesReceitaPrevLabel.Enabled)
                            DeducoesReceitaPrevLabel.Focus();
                        else
                        {
                            if (CustoFolhaOp1PrevLabel.Enabled)
                                CustoFolhaOp1PrevLabel.Focus();
                        }
                    }
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaBruta2PrevLabel.Focus();
            }
        }

        private void ReceitaSubsidioPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    ReceitaSubsidioRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaBruta2RealLabel.Focus();
            }
        }

        private void ReceitaSubsidioRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                {
                    if (DeducoesReceitaPrevLabel.Enabled)
                        DeducoesReceitaPrevLabel.Focus();
                    else
                    {
                        if (CustoFolhaOp1PrevLabel.Enabled)
                            CustoFolhaOp1PrevLabel.Focus();
                    }
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaSubsidioPrevLabel.Focus();
            }
        }

        private void CustoFolhaTotalPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CustoFolhaTotalRealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DeducoesReceitaRealLabel.Focus();
            }
        }

        private void DeducoesReceitaPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    DeducoesReceitaRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (ReceitaSubsidioRealLabel.Enabled)
                    ReceitaSubsidioRealLabel.Focus();
                else
                    ReceitaBruta2RealLabel.Focus();
            }
        }

        private void DeducoesReceitaRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                {
                    if (CustoFolhaOp1PrevLabel.Enabled)
                        CustoFolhaOp1PrevLabel.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DeducoesReceitaPrevLabel.Focus();
            }
        }

        private void CustoFolhaTotalRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CustoFolhaAdm1PrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaTotalPrevLabel.Focus();
            }
        }

        private void CustoFolhaAdm1PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CustoFolhaAdm1RealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaTotalRealLabel.Focus();
            }
        }

        private void CustoFolhaAdm1RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CustoManutencaoFrotaPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaAdm1PrevLabel.Focus();
            }
        }

        private void CustoFolhaMan1PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CustoFolhaMan1RealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaOp1RealLabel.Focus();
            }
        }

        private void CustoFolhaMan1RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CustoFolhaAdm1PrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaMan1PrevLabel.Focus();
            }
        }

        private void CustoFolhaOp1PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CustoFolhaOp1RealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DeducoesReceitaRealLabel.Focus();
            }
        }

        private void CustoFolhaOp1RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CustoFolhaMan1PrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaOp1PrevLabel.Focus();
            }
        }

        private void CustoManutencaoFrotaPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CustoManutencaoFrotaRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaAdm1RealLabel.Focus();
            }
        }

        private void CustoManutencaoFrotaRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    OutrosCustosPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoManutencaoFrotaPrevLabel.Focus();
            }
        }

        private void OutrosCustosPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    OutrosCustosRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoManutencaoFrotaRealLabel.Focus();
            }
        }

        private void OutrosCustosRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CustoPecasPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OutrosCustosPrevLabel.Focus();
            }
        }

        private void ReceitaBruta3PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReceitaBruta3RealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OutrosCustosRealLabel.Focus();
            }
        }

        private void ReceitaBruta3RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReceitaBruta4PrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaBruta3PrevLabel.Focus();
            }
        }

        private void ReceitaBruta4PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReceitaBruta4RealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaBruta3RealLabel.Focus();
            }
        }

        private void ReceitaBruta4RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReceitaSubsidio2PrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaBruta4PrevLabel.Focus();
            }
        }

        private void ReceitaSubsidio2PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReceitaSubsidio2RealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaBruta4RealLabel.Focus();
            }
        }

        private void ReceitaSubsidio2RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EficienciaFolhaTotalPrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaSubsidio2PrevLabel.Focus();
            }
        }

        private void EficienciaFolhaTotalPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EficienciaFolhaTotalRealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReceitaSubsidio2RealLabel.Focus();
            }
        }

        private void EficienciaFolhaTotalRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EficienciaFolhaOpPrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaFolhaTotalPrevLabel.Focus();
            }
        }

        private void EficienciaFolhaOpPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    EficienciaFolhaOpRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaFolhaTotalRealLabel.Focus();
            }
        }

        private void EficienciaFolhaOpRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    OutrosCustosRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaFolhaOpPrevLabel.Focus();
            }
        }
        
        private void CustoFolhaOp2PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CustoFolhaOp2RealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaFolhaOpRealLabel.Focus();
            }
        }

        private void CustoFolhaOp2RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EficienciaFolhaManPrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaOp2PrevLabel.Focus();
            }
        }

        private void EficienciaFolhaManPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    EficienciaFolhaManRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaFolhaOpRealLabel.Focus();
            }
        }
        
        private void EficienciaFolhaManRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    EficienciaFolhaAdmPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaFolhaManPrevLabel.Focus();
            }
        }

        private void CustoFolhaMan2PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CustoFolhaMan2RealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaFolhaManRealLabel.Focus();
            }
        }

        private void CustoFolhaMan2RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EficienciaFolhaAdmPrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaMan2PrevLabel.Focus();
            }
        }
        private void EficienciaFolhaAdmPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    EficienciaFolhaAdmRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaFolhaManRealLabel.Focus();
            }
        }

        private void EficienciaFolhaAdmRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CustoPecasPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaFolhaAdmPrevLabel.Focus();
            }
        }

        private void CustoFolhaAdm2PrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CustoFolhaAdm2RealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaFolhaAdmRealLabel.Focus();
            }
        }

        private void CustoFolhaAdm2RealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EficienciaManutencaoFrotaPrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaAdm2PrevLabel.Focus();
            }
        }

        private void EficienciaManutencaoFrotaPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EficienciaManutencaoFrotaRealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoFolhaAdm2RealLabel.Focus();
            }
        }

        private void EficienciaManutencaoFrotaRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CustoPecasPrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EficienciaManutencaoFrotaPrevLabel.Focus();
            }
        }

        private void CustoPecasPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CustoPecasRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (EficienciaFolhaAdmRealLabel.Enabled)
                    EficienciaFolhaAdmRealLabel.Focus();
                else
                    OutrosCustosRealLabel.Focus();
            }
        }

        private void CustoPecasRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CustoPneuPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoPecasPrevLabel.Focus();
            }
        }

        private void CustoPneuPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CustoPneuRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoPecasRealLabel.Focus();
            }
        }

        private void CustoPneuRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    KmRodadoPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoPneuPrevLabel.Focus();
            }
        }

        private void KmRodadoPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    KmRodadoRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoPneuRealLabel.Focus();
            }
        }

        private void KmRodadoRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CustoHorasExtrasOpPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                KmRodadoPrevLabel.Focus();
            }
        }

        private void CustoHorasExtrasPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CustoHorasExtrasRealLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                KmRodadoRealLabel.Focus(); 
            }
        }

        private void CustoHorasExtrasRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CustoHorasExtrasOpPrevLabel.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoHorasExtrasPrevLabel.Focus();
            }
        }

        private void CustoHorasExtrasOpPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CustoHorasExtrasOpRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                KmRodadoRealLabel.Focus();
            }
        }

        private void CustoHorasExtrasOpRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CustoHorasExtrasManPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoHorasExtrasOpPrevLabel.Focus();
            }
        }

        private void CustoHorasExtrasManPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CustoHorasExtrasManRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoHorasExtrasOpRealLabel.Focus();
            }
        }

        private void CustoHorasExtrasManRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CustoHorasExtrasAdmPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoHorasExtrasManPrevLabel.Focus();
            }
        }

        private void CustoHorasExtrasAdmPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CustoHorasExtrasAdmRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoHorasExtrasManRealLabel.Focus();
            }
        }

        private void CustoHorasExtrasAdmRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CustoMultasOrgaoGestorPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoHorasExtrasAdmPrevLabel.Focus();
            }
        }

        private void CustoMultasOrgaoGestorPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CustoMultasOrgaoGestorRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoHorasExtrasAdmRealLabel.Focus();
            }
        }

        private void CustoMultasOrgaoGestorRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    IndiceLimpezaFrotaPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoMultasOrgaoGestorPrevLabel.Focus();
            }
        }

        private void IndiceLimpezaFrotaPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    IndiceLimpezaFrotaRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoMultasOrgaoGestorRealLabel.Focus();
            }
        }

        private void IndiceLimpezaFrotaRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    MKBFPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndiceLimpezaFrotaPrevLabel.Focus();
            }
        }

        private void MKBFPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    MKBFRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndiceLimpezaFrotaRealLabel.Focus();
            }
        }

        private void MKBFRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void ReclamacaoPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    ReclamacaoRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                MKBFFretamentoRealLabel.Focus();
            }
        }

        private void ReclamacaoRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    AvariasComCulpaPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReclamacaoPrevLabel.Focus();
            }
        }

        private void AvariasComCulpaPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    AvariasComCulpaRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReclamacaoRealLabel.Focus();
            }
        }

        private void AvariasComCulpaRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    AcidentesComCulpaPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AvariasComCulpaPrevLabel.Focus();
            }
        }

        private void AcidentesComCulpaPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    AcidentesComCulpaRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AvariasComCulpaRealLabel.Focus();
            }
        }

        private void AcidentesComCulpaRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                {
                    if (CumprimentoPanel.Visible)
                        CumprimentoPartidaPrevLabel.Focus();
                    else
                        PartidaPrevLabel.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AcidentesComCulpaPrevLabel.Focus();
            }
        }

        private void CumprimentoPartidaPRevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CumprimentoPartidaRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AcidentesComCulpaRealLabel.Focus();
            }
        }

        private void CumprimentoPartidaRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CarrosRetidosPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CumprimentoPartidaPrevLabel.Focus();
            }
        }

        private void CarrosRetidosPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CarrosRetidosRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (CumprimentoPanel.Visible)
                    CumprimentoPartidaRealLabel.Focus();
                else
                    PartidaRealLabel.Focus();
            }
        }

        private void CarrosRetidosRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    ComprasEmergenciaisPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CarrosRetidosPrevLabel.Focus();
            }
        }

        private void ComprasEmergenciaisPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    ComprasEmergenciaisRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CarrosRetidosRealLabel.Focus();
            }
        }

        private void ComprasEmergenciaisRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                {
                    if (PontualidadePanel.Visible)
                        PontualidadePrevLabel.Focus();
                    else
                        IndicesAbsenteismoPrevLabel.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ComprasEmergenciaisPrevLabel.Focus();
            }
        }

        private void IndicesAbsenteismoPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    IndicesAbsenteismoRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (PontualidadePanel.Visible)
                    PontualidadeRealLabel.Focus();
                else
                    ComprasEmergenciaisRealLabel.Focus();
            }
        }

        private void IndicesAbsenteismoRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    IndicesAbsenteismoOpPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicesAbsenteismoPrevLabel.Focus();
            }
        }

        private void IndicesAbsenteismoOpPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    IndicesAbsenteismoOpRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicesAbsenteismoRealLabel.Focus();
            }
        }

        private void IndicesAbsenteismoOpRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    IndicesAbsenteismoManPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicesAbsenteismoOpPrevLabel.Focus();
            }
        }

        private void IndicesAbsenteismoManPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    IndicesAbsenteismoManRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicesAbsenteismoOpRealLabel.Focus();
            }
        }

        private void IndicesAbsenteismoManRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    IndicesAbsenteismoAdmPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicesAbsenteismoManPrevLabel.Focus();
            }
        }

        private void IndicesAbsenteismoAdmPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    IndicesAbsenteismoAdmRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicesAbsenteismoManRealLabel.Focus();
            }
        }

        private void IndicesAbsenteismoAdmRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    IndiceTurnoverPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicesAbsenteismoAdmPrevLabel.Focus();
            }
        }

        private void IndiceTurnoverPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    IndiceTurnoverRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicesAbsenteismoAdmRealLabel.Focus();
            }
        }

        private void IndiceTurnoverRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    IndiceTurnoverOpPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndiceTurnoverPrevLabel.Focus();
            }
        }

        private void IndiceTurnoverOpPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    IndiceTurnoverOpRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndiceTurnoverRealLabel.Focus();
            }
        }

        private void IndiceTurnoverOpRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    IndiceTurnoverManPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndiceTurnoverOpPrevLabel.Focus();
            }
        }

        private void IndiceTurnoverManPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    IndiceTurnoverManRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndiceTurnoverOpRealLabel.Focus();
            }
        }

        private void IndiceTurnoverManRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    IndiceTurnoverAdmPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndiceTurnoverManPrevLabel.Focus();
            }
        }

        private void IndiceTurnoverAdmPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    IndiceTurnoverAdmRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndiceTurnoverManRealLabel.Focus();
            }
        }

        private void IndiceTurnoverAdmRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CNHVencidasPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndiceTurnoverAdmPrevLabel.Focus();
            }
        }

        private void CNHVencidasPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    CNHVencidasRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndiceTurnoverAdmRealLabel.Focus();
            }
        }

        private void CNHVencidasRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    ExamesVencidosPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CNHVencidasPrevLabel.Focus();
            }
        }

        private void ExamesVencidosPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoPrevistoPanel.Visible)
                    MotivoPrevistoTextBox.Focus();
                else
                    ExamesVencidosRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CNHVencidasRealLabel.Focus();
            }
        }

        private void ExamesVencidosRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ExamesVencidosPrevLabel.Focus();
            }
        }

        private void EbitdaPrevLabel_Enter(object sender, EventArgs e)
        {
            try
            {
                ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
            }
            catch { }
        }

        private void EbitdaPrevLabel_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _componenteFocado = ((CurrencyTextBox)sender);
            _valor = 0;
            bool _achou = false;
            PrevistoAntesCurrencyTextBox.DecimalValue = 0;
            PrevistoAtualCurrencyTextBox.DecimalValue = 0;
            RealAntesCurrencyTextBox.DecimalValue = 0;
            RealAtualCurrencyTextBox.DecimalValue = 0;
            MotivoPrevistoTextBox.Text = string.Empty;
            MotivoRealTextBox.Text = string.Empty;

            #region Receita Bruta
            if (((CurrencyTextBox)sender).Name == ReceitaBruta2PrevLabel.Name)
            {                
                PrevistoAtualCurrencyTextBox.DecimalValue = ReceitaBruta2PrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(ReceitaBruta2PrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(ReceitaBruta2PrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = ReceitaBruta2Panel.Left;
                    MotivoPrevistoPanel.Top = ReceitaSubsidioPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == ReceitaBruta2RealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = ReceitaBruta2RealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(ReceitaBruta2PrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(ReceitaBruta2PrevLabel.Tag)))
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = ReceitaBruta2Panel.Left;
                    MotivoRealPanel.Top = ReceitaSubsidioPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Subsidio
            if (((CurrencyTextBox)sender).Name == ReceitaSubsidioPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = ReceitaSubsidioPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(ReceitaSubsidioPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(ReceitaSubsidioPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = ReceitaBruta2Panel.Left;
                    MotivoPrevistoPanel.Top = DeducoesReceitaPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == ReceitaSubsidioRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = ReceitaSubsidioRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(ReceitaSubsidioPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(ReceitaSubsidioPrevLabel.Tag)))
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Left = ReceitaBruta2Panel.Left;
                    MotivoRealPanel.Top = DeducoesReceitaPanel.Top;
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Deduções sobre receita
            if (((CurrencyTextBox)sender).Name == DeducoesReceitaPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = DeducoesReceitaPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(DeducoesReceitaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(DeducoesReceitaPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = DeducoesReceitaPanel.Left;
                    MotivoPrevistoPanel.Top = CustoFolhaOperacaoPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == DeducoesReceitaRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = DeducoesReceitaRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(DeducoesReceitaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(DeducoesReceitaPrevLabel.Tag)))
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = DeducoesReceitaPanel.Left;
                    MotivoRealPanel.Top = CustoFolhaOperacaoPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Custo Folha Operacao
            if (((CurrencyTextBox)sender).Name == CustoFolhaOp1PrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CustoFolhaOp1PrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoFolhaOp1PrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoFolhaOp1PrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CustoFolhaOperacaoPanel.Left;
                    MotivoPrevistoPanel.Top = CustoFolhaManutencaoPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CustoFolhaOp1RealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CustoFolhaOp1RealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoFolhaOp1PrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoFolhaOp1PrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CustoFolhaOperacaoPanel.Left;
                    MotivoRealPanel.Top = CustoFolhaManutencaoPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Custo Folha Manutencao
            if (((CurrencyTextBox)sender).Name == CustoFolhaMan1PrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CustoFolhaMan1PrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoFolhaMan1PrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoFolhaMan1PrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.Left = CustoFolhaOperacaoPanel.Left;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Top = CustoFolhaAdministracaoPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CustoFolhaMan1RealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CustoFolhaMan1RealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoFolhaMan1PrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoFolhaMan1PrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CustoFolhaOperacaoPanel.Left;
                    MotivoRealPanel.Top = CustoFolhaAdministracaoPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Custo Folha Administracao
            if (((CurrencyTextBox)sender).Name == CustoFolhaAdm1PrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CustoFolhaAdm1PrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoFolhaAdm1PrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoFolhaAdm1PrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CustoFolhaOperacaoPanel.Left;
                    MotivoPrevistoPanel.Top = CustoManutencaoFrotaPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CustoFolhaAdm1RealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CustoFolhaAdm1RealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoFolhaAdm1PrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoFolhaAdm1PrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CustoFolhaOperacaoPanel.Left;
                    MotivoRealPanel.Top = CustoManutencaoFrotaPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Custo Manutencao Frota
            if (((CurrencyTextBox)sender).Name == CustoManutencaoFrotaPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CustoManutencaoFrotaPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoManutencaoFrotaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoManutencaoFrotaPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CustoManutencaoFrotaPanel.Left;
                    MotivoPrevistoPanel.Top = OutrosCustosPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CustoManutencaoFrotaRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CustoManutencaoFrotaRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoManutencaoFrotaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoManutencaoFrotaPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CustoManutencaoFrotaPanel.Left;
                    MotivoRealPanel.Top = OutrosCustosPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Outros Custo 
            if (((CurrencyTextBox)sender).Name == OutrosCustosPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = OutrosCustosPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(OutrosCustosPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(OutrosCustosPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = OutrosCustosPanel.Left;
                    MotivoPrevistoPanel.Top = ReceitaBruta4Panel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == OutrosCustosRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = OutrosCustosRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(OutrosCustosPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(OutrosCustosPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = OutrosCustosPanel.Left;
                    MotivoRealPanel.Top = ReceitaBruta4Panel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Eficiencia Folha OP
            if (((CurrencyTextBox)sender).Name == EficienciaFolhaOpPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = EficienciaFolhaOpPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(EficienciaFolhaOpPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(EficienciaFolhaOpPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = EficienciaFolhaOpPanel.Left;
                    MotivoPrevistoPanel.Top = EficienciaFolhaManPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == EficienciaFolhaOpRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = EficienciaFolhaOpRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(EficienciaFolhaOpPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(EficienciaFolhaOpPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = EficienciaFolhaOpPanel.Left;
                    MotivoRealPanel.Top = EficienciaFolhaManPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Eficiencia Folha Man
            if (((CurrencyTextBox)sender).Name == EficienciaFolhaManPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = EficienciaFolhaManPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(EficienciaFolhaManPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(EficienciaFolhaManPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = EficienciaFolhaManPanel.Left;
                    MotivoPrevistoPanel.Top = EficienciaFolhaAdmPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == EficienciaFolhaManRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = EficienciaFolhaManRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(EficienciaFolhaManPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(EficienciaFolhaManPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = EficienciaFolhaManPanel.Left;
                    MotivoRealPanel.Top = EficienciaFolhaAdmPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Eficiencia Folha Adm
            if (((CurrencyTextBox)sender).Name == EficienciaFolhaAdmPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = EficienciaFolhaAdmPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(EficienciaFolhaAdmPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(EficienciaFolhaAdmPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoPrevistoPanel.Top = CustosPecasPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == EficienciaFolhaAdmRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = EficienciaFolhaAdmRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(EficienciaFolhaAdmPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(EficienciaFolhaAdmPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoRealPanel.Top = CustosPecasPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Custos Peças
            if (((CurrencyTextBox)sender).Name == CustoPecasPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CustoPecasPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoPecasPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoPecasPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoPrevistoPanel.Top = CustosPneusPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CustoPecasRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CustoPecasRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoPecasPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoPecasPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoRealPanel.Top = CustosPneusPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Custos Pneus
            if (((CurrencyTextBox)sender).Name == CustoPneuPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CustoPneuPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoPneuPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoPneuPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoPrevistoPanel.Top = KmRodadoPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CustoPneuRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CustoPneuRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoPneuPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoPneuPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoRealPanel.Top = KmRodadoPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Km Rodado
            if (((CurrencyTextBox)sender).Name == KmRodadoPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = KmRodadoPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(KmRodadoPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(KmRodadoPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoPrevistoPanel.Top = CustoHorasExtraOpPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == KmRodadoRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = KmRodadoRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(KmRodadoPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(KmRodadoPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoRealPanel.Top = CustoHorasExtraOpPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Custo Hora Extra Op
            if (((CurrencyTextBox)sender).Name == CustoHorasExtrasOpPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CustoHorasExtrasOpPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoHorasExtrasOpPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoHorasExtrasOpPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoPrevistoPanel.Top = CustoHorasExtraManPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CustoHorasExtrasOpRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CustoHorasExtrasOpRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoHorasExtrasOpPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoHorasExtrasOpPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoRealPanel.Top = CustoHorasExtraManPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Custo Hora Extra Man
            if (((CurrencyTextBox)sender).Name == CustoHorasExtrasManPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CustoHorasExtrasManPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoHorasExtrasManPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoHorasExtrasManPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoPrevistoPanel.Top = CustoHorasExtraAdmPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CustoHorasExtrasManRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CustoHorasExtrasManRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoHorasExtrasAdmPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoHorasExtrasManPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoRealPanel.Top = CustoHorasExtraAdmPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Custo Hora Extra Adm
            if (((CurrencyTextBox)sender).Name == CustoHorasExtrasAdmPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CustoHorasExtrasAdmPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoHorasExtrasAdmPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoHorasExtrasAdmPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoPrevistoPanel.Top = CustoMultasOrgaoPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CustoHorasExtrasAdmRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CustoHorasExtrasAdmRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoHorasExtrasAdmPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoHorasExtrasAdmPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = EficienciaFolhaAdmPanel.Left;
                    MotivoRealPanel.Top = CustoMultasOrgaoPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Custo multa Orgão gestor
            if (((CurrencyTextBox)sender).Name == CustoMultasOrgaoGestorPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CustoMultasOrgaoGestorPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoMultasOrgaoGestorPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoMultasOrgaoGestorPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CustoMultasOrgaoPanel.Left;
                    MotivoPrevistoPanel.Top = CustoMultasOrgaoPanel.Top + CustoMultasOrgaoPanel.Height + 3;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CustoMultasOrgaoGestorRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CustoMultasOrgaoGestorRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CustoMultasOrgaoGestorPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CustoMultasOrgaoGestorPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    FinanceiroPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CustoMultasOrgaoPanel.Left;
                    MotivoRealPanel.Top = CustoMultasOrgaoPanel.Top + CustoMultasOrgaoPanel.Height +3;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Limpeza de frota
            if (((CurrencyTextBox)sender).Name == IndiceLimpezaFrotaPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = IndiceLimpezaFrotaPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(IndiceLimpezaFrotaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndiceLimpezaFrotaPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoPrevistoPanel.Parent = ClientePanel.Parent;
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = LimpezaFrotaPanel.Left + LimpezaFrotaPanel.Width;
                    MotivoPrevistoPanel.Top = CabecalhoClientePanel.Top;
                    MotivoPrevistoTextBox.Focus();
                    ClientePanel.AutoScrollPosition = new Point(0); 
                }
            }

            if (((CurrencyTextBox)sender).Name == IndiceLimpezaFrotaRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = IndiceLimpezaFrotaRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(IndiceLimpezaFrotaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndiceLimpezaFrotaPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoRealPanel.Parent = ClientePanel.Parent;
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = LimpezaFrotaPanel.Left + LimpezaFrotaPanel.Width;
                    MotivoRealPanel.Top = CabecalhoClientePanel.Top;
                    MotivoRealTextBox.Focus();
                    ClientePanel.AutoScrollPosition = new Point(0); 
                }
            }
            #endregion

            #region MKBF
            if (((CurrencyTextBox)sender).Name == MKBFPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = MKBFPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(MKBFPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(MKBFPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoPrevistoPanel.Parent = ClientePanel.Parent;
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = LimpezaFrotaPanel.Left + LimpezaFrotaPanel.Width;
                    MotivoPrevistoPanel.Top = CabecalhoClientePanel.Top;
                    MotivoPrevistoTextBox.Focus();
                    ClientePanel.AutoScrollPosition = new Point(59);
                }
            }

            if (((CurrencyTextBox)sender).Name == MKBFRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = MKBFRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(MKBFPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(MKBFPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoRealPanel.Parent = ClientePanel.Parent;
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = LimpezaFrotaPanel.Left + LimpezaFrotaPanel.Width;
                    MotivoRealPanel.Top = CabecalhoClientePanel.Top;
                    MotivoRealTextBox.Focus();
                    ClientePanel.AutoScrollPosition = new Point(59);
                }
            }
            #endregion

            #region Reclamação Passageiros
            if (((CurrencyTextBox)sender).Name == ReclamacaoPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = ReclamacaoPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(ReclamacaoPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(ReclamacaoPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoPrevistoPanel.Parent = ClientePanel.Parent;
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = LimpezaFrotaPanel.Left + LimpezaFrotaPanel.Width;
                    MotivoPrevistoPanel.Top = CabecalhoClientePanel.Top;
                    MotivoPrevistoTextBox.Focus();
                    ClientePanel.AutoScrollPosition = new Point(118);
                }
            }

            if (((CurrencyTextBox)sender).Name == ReclamacaoRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = ReclamacaoRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(ReclamacaoPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(ReclamacaoPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoRealPanel.Parent = ClientePanel.Parent;
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = LimpezaFrotaPanel.Left + LimpezaFrotaPanel.Width;
                    MotivoRealPanel.Top = CabecalhoClientePanel.Top;
                    MotivoRealTextBox.Focus();
                    ClientePanel.AutoScrollPosition = new Point(0,-118);
                }
            }
            #endregion

            #region Avarias
            if (((CurrencyTextBox)sender).Name == AvariasComCulpaPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = AvariasComCulpaPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(AvariasComCulpaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(AvariasComCulpaPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoPrevistoPanel.Parent = ProcessosInternosPanel.Parent;
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CustoMultasOrgaoPanel.Left + CustoMultasOrgaoPanel.Width;
                    MotivoPrevistoPanel.Top = CabecalhoProcessoInternoPanel.Top + CabecalhoProcessoInternoPanel.Height;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == AvariasComCulpaRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = AvariasComCulpaRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(AvariasComCulpaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(AvariasComCulpaPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoRealPanel.Parent = ProcessosInternosPanel.Parent;
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CustoMultasOrgaoPanel.Left + CustoMultasOrgaoPanel.Width;
                    MotivoRealPanel.Top = CabecalhoProcessoInternoPanel.Top + CabecalhoProcessoInternoPanel.Height;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Acidentes
            if (((CurrencyTextBox)sender).Name == AcidentesComCulpaPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = AcidentesComCulpaPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(AcidentesComCulpaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(AcidentesComCulpaPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoPrevistoPanel.Parent = ProcessosInternosPanel.Parent;
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CustoMultasOrgaoPanel.Left + CustoMultasOrgaoPanel.Width;
                    MotivoPrevistoPanel.Top = CabecalhoProcessoInternoPanel.Top + CabecalhoProcessoInternoPanel.Height;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == AcidentesComCulpaRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = AcidentesComCulpaRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(AcidentesComCulpaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(AcidentesComCulpaPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoRealPanel.Parent = ProcessosInternosPanel.Parent;
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CustoMultasOrgaoPanel.Left + CustoMultasOrgaoPanel.Width;
                    MotivoRealPanel.Top = CabecalhoProcessoInternoPanel.Top + CabecalhoProcessoInternoPanel.Height;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Cumprimento de Partidas
            if (((CurrencyTextBox)sender).Name == CumprimentoPartidaPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CumprimentoPartidaPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CumprimentoPartidaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CumprimentoPartidaPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoPrevistoPanel.Parent = ProcessosInternosPanel.Parent;
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CustoMultasOrgaoPanel.Left + CustoMultasOrgaoPanel.Width;
                    MotivoPrevistoPanel.Top = CabecalhoProcessoInternoPanel.Top + CabecalhoProcessoInternoPanel.Height;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CumprimentoPartidaRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CumprimentoPartidaRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CumprimentoPartidaPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CumprimentoPartidaPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoRealPanel.Parent = ProcessosInternosPanel.Parent;
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CustoMultasOrgaoPanel.Left + CustoMultasOrgaoPanel.Width;
                    MotivoRealPanel.Top = CabecalhoProcessoInternoPanel.Top + CabecalhoProcessoInternoPanel.Height;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Carros Retidos
            if (((CurrencyTextBox)sender).Name == CarrosRetidosPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CarrosRetidosPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CarrosRetidosPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CarrosRetidosPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoPrevistoPanel.Parent = ProcessosInternosPanel.Parent;
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CustoMultasOrgaoPanel.Left + CustoMultasOrgaoPanel.Width;
                    MotivoPrevistoPanel.Top = CabecalhoProcessoInternoPanel.Top + CabecalhoProcessoInternoPanel.Height;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CarrosRetidosRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CarrosRetidosRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CarrosRetidosPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CarrosRetidosPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoRealPanel.Parent = ProcessosInternosPanel.Parent;
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CustoMultasOrgaoPanel.Left + CustoMultasOrgaoPanel.Width;
                    MotivoRealPanel.Top = CabecalhoProcessoInternoPanel.Top + CabecalhoProcessoInternoPanel.Height;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Compras Emergenciais
            if (((CurrencyTextBox)sender).Name == ComprasEmergenciaisPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = ComprasEmergenciaisPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(ComprasEmergenciaisPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(ComprasEmergenciaisPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoPrevistoPanel.Parent = ProcessosInternosPanel.Parent;
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CustoMultasOrgaoPanel.Left + CustoMultasOrgaoPanel.Width;
                    MotivoPrevistoPanel.Top = CabecalhoProcessoInternoPanel.Top + CabecalhoProcessoInternoPanel.Height;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == ComprasEmergenciaisRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = ComprasEmergenciaisRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(ComprasEmergenciaisPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(ComprasEmergenciaisPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    MotivoRealPanel.Parent = ProcessosInternosPanel.Parent;
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CustoMultasOrgaoPanel.Left + CustoMultasOrgaoPanel.Width;
                    MotivoRealPanel.Top = CabecalhoProcessoInternoPanel.Top + CabecalhoProcessoInternoPanel.Height;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Absenteismo Op
            if (((CurrencyTextBox)sender).Name == IndicesAbsenteismoOpPrevLabel.Name)
            {
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(IndicesAbsenteismoOpPrevLabel.Tag);
                PrevistoAtualCurrencyTextBox.DecimalValue = IndicesAbsenteismoOpPrevLabel.DecimalValue;
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndicesAbsenteismoOpPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;                    
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = AbsenteismoOpPanel.Left;
                    MotivoPrevistoPanel.Top = AbsenteismoManPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == IndicesAbsenteismoOpRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = IndicesAbsenteismoOpRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(IndicesAbsenteismoOpPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndicesAbsenteismoOpPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;                    
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = AbsenteismoOpPanel.Left;
                    MotivoRealPanel.Top = AbsenteismoManPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Absenteismo Man
            if (((CurrencyTextBox)sender).Name == IndicesAbsenteismoManPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = IndicesAbsenteismoManPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(IndicesAbsenteismoManPrevLabel.Tag);

                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndicesAbsenteismoManPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = AbsenteismoOpPanel.Left;
                    MotivoPrevistoPanel.Top = AbsenteismoAdmPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == IndicesAbsenteismoManRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = IndicesAbsenteismoManRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(IndicesAbsenteismoManPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndicesAbsenteismoManPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;                    
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = AbsenteismoOpPanel.Left;
                    MotivoRealPanel.Top = AbsenteismoAdmPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Absenteismo Adm
            if (((CurrencyTextBox)sender).Name == IndicesAbsenteismoAdmPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = IndicesAbsenteismoAdmPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(IndicesAbsenteismoAdmPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndicesAbsenteismoAdmPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = AbsenteismoOpPanel.Left;
                    MotivoPrevistoPanel.Top = TurnoverOpPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == IndicesAbsenteismoAdmRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = IndicesAbsenteismoAdmRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(IndicesAbsenteismoAdmPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndicesAbsenteismoAdmPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = AbsenteismoOpPanel.Left;
                    MotivoRealPanel.Top = TurnoverOpPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Absenteismo 
            if (((CurrencyTextBox)sender).Name == IndicesAbsenteismoPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = IndicesAbsenteismoPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(IndicesAbsenteismoPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndicesAbsenteismoPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = AbsenteismoPanel.Left;
                    MotivoPrevistoPanel.Top = AbsenteismoAdmPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == IndicesAbsenteismoRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = IndicesAbsenteismoRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(IndicesAbsenteismoPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndicesAbsenteismoPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = AbsenteismoPanel.Left;
                    MotivoRealPanel.Top = AbsenteismoAdmPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Turnover 
            if (((CurrencyTextBox)sender).Name == IndiceTurnoverPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = IndiceTurnoverPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(IndiceTurnoverPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndiceTurnoverPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = AbsenteismoPanel.Left;
                    MotivoPrevistoPanel.Top = TurnoverAdmPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == IndiceTurnoverRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = IndiceTurnoverRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(IndiceTurnoverPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndiceTurnoverPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = AbsenteismoPanel.Left;
                    MotivoRealPanel.Top = TurnoverAdmPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Turnover op
            if (((CurrencyTextBox)sender).Name == IndiceTurnoverOpPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = IndiceTurnoverOpPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(IndiceTurnoverOpPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndiceTurnoverOpPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = TurnoverOpPanel.Left;
                    MotivoPrevistoPanel.Top = TurnoverManPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == IndiceTurnoverOpRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = IndiceTurnoverOpRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(IndiceTurnoverOpPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndiceTurnoverOpPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = TurnoverOpPanel.Left;
                    MotivoRealPanel.Top = TurnoverManPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Turnover Man
            if (((CurrencyTextBox)sender).Name == IndiceTurnoverManPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = IndiceTurnoverManPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(IndiceTurnoverManPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndiceTurnoverManPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = TurnoverOpPanel.Left;
                    MotivoPrevistoPanel.Top = TurnoverAdmPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == IndiceTurnoverManRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = IndiceTurnoverManRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(IndiceTurnoverManPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndiceTurnoverManPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = TurnoverOpPanel.Left;
                    MotivoRealPanel.Top = TurnoverAdmPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Turnover Adm
            if (((CurrencyTextBox)sender).Name == IndiceTurnoverAdmPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = IndiceTurnoverAdmPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(IndiceTurnoverAdmPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndiceTurnoverAdmPrevLabel.Tag)))
                {
                    _achou = item.Existe;
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (_achou && 
                    PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = TurnoverOpPanel.Left;
                    MotivoPrevistoPanel.Top = CNHVencidasPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == IndiceTurnoverAdmRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = IndiceTurnoverAdmRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(IndiceTurnoverAdmPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(IndiceTurnoverAdmPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = TurnoverOpPanel.Left;
                    MotivoRealPanel.Top = CNHVencidasPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region CNH Vencidas
            if (((CurrencyTextBox)sender).Name == CNHVencidasPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = CNHVencidasPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(CNHVencidasPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CNHVencidasPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CNHVencidasPanel.Left;
                    MotivoPrevistoPanel.Top = ExamesVencidosPanel.Top;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == CNHVencidasRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = CNHVencidasRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(CNHVencidasPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(CNHVencidasPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CNHVencidasPanel.Left;
                    MotivoRealPanel.Top = ExamesVencidosPanel.Top;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion

            #region Exames Vencidos
            if (((CurrencyTextBox)sender).Name == ExamesVencidosPrevLabel.Name)
            {
                PrevistoAtualCurrencyTextBox.DecimalValue = ExamesVencidosPrevLabel.DecimalValue;
                PrevistoAntesCurrencyTextBox.Tag = Convert.ToInt32(ExamesVencidosPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(ExamesVencidosPrevLabel.Tag)))
                {
                    _valor = item.Previsto;
                    PrevistoAntesCurrencyTextBox.DecimalValue = item.PrevistoOriginal;
                    MotivoPrevistoTextBox.Text = item.MotivoEdicaoPrevisto;
                }

                if (PrevistoAntesCurrencyTextBox.DecimalValue != 0 && PrevistoAntesCurrencyTextBox.DecimalValue != PrevistoAtualCurrencyTextBox.DecimalValue && _valor != PrevistoAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoPrevistoPanel);
                    MotivoPrevistoPanel.Visible = true;
                    MotivoPrevistoPanel.BringToFront();
                    MotivoPrevistoPanel.Left = CNHVencidasPanel.Left;
                    MotivoPrevistoPanel.Top = ExamesVencidosPanel.Top + ExamesVencidosPanel.Height + 3;
                    MotivoPrevistoTextBox.Focus();
                }
            }

            if (((CurrencyTextBox)sender).Name == ExamesVencidosRealLabel.Name)
            {
                RealAtualCurrencyTextBox.DecimalValue = ExamesVencidosRealLabel.DecimalValue;
                RealAntesCurrencyTextBox.Tag = Convert.ToInt32(ExamesVencidosPrevLabel.Tag);
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(ExamesVencidosPrevLabel.Tag)))// o Id está sempre o previsto
                {
                    _valor = item.Realizado;
                    RealAntesCurrencyTextBox.DecimalValue = item.RealizadoOriginal;
                    MotivoRealTextBox.Text = item.MotivoEdicaoReal;
                }

                if (RealAntesCurrencyTextBox.DecimalValue != 0 && RealAntesCurrencyTextBox.DecimalValue != RealAtualCurrencyTextBox.DecimalValue && _valor != RealAtualCurrencyTextBox.DecimalValue)
                {
                    AprendizadoPanel.Controls.Add(MotivoRealPanel);
                    MotivoRealPanel.Visible = true;
                    MotivoRealPanel.BringToFront();
                    MotivoRealPanel.Left = CNHVencidasPanel.Left;
                    MotivoRealPanel.Top = ExamesVencidosPanel.Top + ExamesVencidosPanel.Height + 3;
                    MotivoRealTextBox.Focus();
                }
            }
            #endregion
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ExamesVencidosRealLabel.Focus();
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

        private void limparButton_Click(object sender, EventArgs e)
        {
            _buscouRealizadoFinanceiro = false;
            _buscouRealizadoOperacional = false;
            _dataCorteOperacional = DateTime.MinValue;
            _dataCorteFinanceiro = DateTime.MinValue;

            if (_listaValoresMetas.Count() != 0)
                _listaValoresMetas.Clear();

            if (_listaValoresMetasMesAnterior != null)
                _listaValoresMetasMesAnterior.Clear();

            new MetasBO().Excluir(_bscEmEdicao);

            foreach (Control item in AprendizadoPanel.Controls)
            {
                if (item is Panel)
                {
                    foreach (Control itemP in item.Controls)
                    {
                        if (itemP is CurrencyTextBox)
                        {
                            ((CurrencyTextBox)itemP).DecimalValue = 0;
                            ((CurrencyTextBox)itemP).Tag = null;
                            ((CurrencyTextBox)itemP).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                            ((CurrencyTextBox)itemP).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);

                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Text = "";
                            superToolTip1.SetToolTip((CurrencyTextBox)itemP, _tool);
                        }

                        if (itemP is PictureBox)
                            ((PictureBox)itemP).ImageLocation = _arquivoCinza;
                    }
                }
            }

            foreach (Control item in ClientePanel.Controls)
            {
                if (item is Panel)
                {
                    foreach (Control itemP in item.Controls)
                    {
                        if (itemP is CurrencyTextBox)
                        {
                            ((CurrencyTextBox)itemP).DecimalValue = 0;
                            ((CurrencyTextBox)itemP).Tag = null;
                            ((CurrencyTextBox)itemP).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                            ((CurrencyTextBox)itemP).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);

                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Text = "";
                            superToolTip1.SetToolTip((CurrencyTextBox)itemP, _tool);
                        }
                        if (itemP is PictureBox)
                            ((PictureBox)itemP).ImageLocation = _arquivoCinza;

                    }
                }
            }

            foreach (Control item in ProcessosInternosPanel.Controls)
            {
                if (item is Panel)
                {
                    foreach (Control itemP in item.Controls)
                    {
                        if (itemP is CurrencyTextBox)
                        {
                            ((CurrencyTextBox)itemP).DecimalValue = 0;
                            ((CurrencyTextBox)itemP).Tag = null;
                            ((CurrencyTextBox)itemP).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                            ((CurrencyTextBox)itemP).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);

                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Text = "";
                            superToolTip1.SetToolTip((CurrencyTextBox)itemP, _tool);
                        }
                        if (itemP is PictureBox)
                            ((PictureBox)itemP).ImageLocation = _arquivoCinza;

                    }
                }
            }

            foreach (Control item in FinanceiroPanel.Controls)
            {
                if (item is Panel)
                {
                    foreach (Control itemP in item.Controls)
                    {
                        if (itemP is CurrencyTextBox)
                        {
                            ((CurrencyTextBox)itemP).DecimalValue = 0;
                            ((CurrencyTextBox)itemP).Tag = null;
                            ((CurrencyTextBox)itemP).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                            ((CurrencyTextBox)itemP).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);

                            ToolTipInfo _tool = new ToolTipInfo();
                            _tool.Footer.Text = "";
                            superToolTip1.SetToolTip((CurrencyTextBox)itemP, _tool);
                        }
                        if (itemP is PictureBox)
                            ((PictureBox)itemP).ImageLocation = _arquivoCinza;

                    }
                }
            }

            grupospanel.AutoScrollPosition = new Point(0);
            referenciaMaskedEditBox.Text = string.Empty;
            DiasUteisCurrencyTextBox.DecimalValue = 0;

            EbitdaRealLabel.DecimalValue = 0;
            Ebitda2RealLabel.DecimalValue = 0;
            EbitdaPrevLabel.DecimalValue = 0;
            Ebitda2PrevLabel.DecimalValue = 0;

            //EbitdaFRealLabel.DecimalValue = 0;
            //Ebitda2FRealLabel.DecimalValue = 0;
            //EbitdaFPrevLabel.DecimalValue = 0;
            //Ebitda2FPrevLabel.DecimalValue = 0;

            DataCorteLabel.Text = "";
            gravarButton.Text = "&Gravar";
            referenciaMaskedEditBox.Focus();
            excluirButton.Enabled = false;
            gravarButton.Enabled = false;
            ExamesVencidosPictureBox.ImageLocation = _arquivoCinza;
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void referenciaMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void ReceitaBruta2PrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            ReceitaBruta1PrevLabel.DecimalValue = ReceitaBruta2PrevLabel.DecimalValue + ReceitaSubsidioPrevLabel.DecimalValue;
            ReceitaBruta3PrevLabel.DecimalValue = ReceitaBruta2PrevLabel.DecimalValue + ReceitaSubsidioPrevLabel.DecimalValue;
            ReceitaBruta4PrevLabel.DecimalValue = ReceitaBruta2PrevLabel.DecimalValue;
            ReceitaSubsidio2PrevLabel.DecimalValue = ReceitaSubsidioPrevLabel.DecimalValue;
            ReceitaBruta2RealLabel_DecimalValueChanged(sender, e);

        }

        private void ReceitaBruta2RealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            ReceitaBruta1RealLabel.DecimalValue = ReceitaBruta2RealLabel.DecimalValue + ReceitaSubsidioRealLabel.DecimalValue;
            ReceitaBruta3RealLabel.DecimalValue = ReceitaBruta2RealLabel.DecimalValue + ReceitaSubsidioRealLabel.DecimalValue;
            ReceitaBruta4RealLabel.DecimalValue = ReceitaBruta2RealLabel.DecimalValue;
            ReceitaSubsidio2RealLabel.DecimalValue = ReceitaSubsidioRealLabel.DecimalValue;
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                ReceitaBruta1PictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(ReceitaBruta1PrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (ReceitaBruta1PrevLabel.DecimalValue != 0)
                        valor = (ReceitaBruta1RealLabel.DecimalValue / ReceitaBruta1PrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (ReceitaBruta1RealLabel.DecimalValue != 0)
                            valor = (ReceitaBruta1PrevLabel.DecimalValue / ReceitaBruta1RealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (ReceitaBruta1PrevLabel.DecimalValue == ReceitaBruta1RealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - ReceitaBruta1RealLabel.DecimalValue);
                    }
                }

                //if (ReceitaBruta1PrevLabel.DecimalValue != 0)
                 //   valor = (ReceitaBruta1RealLabel.DecimalValue / ReceitaBruta1PrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (ReceitaBruta1PrevLabel.DecimalValue == 0 && ReceitaBruta1RealLabel.DecimalValue == 0)
                    || (ReceitaBruta1PrevLabel.DecimalValue > 0 && ReceitaBruta1RealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    ReceitaBruta1PictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    ReceitaBruta1PictureBox.ImageLocation = _arquivoAmarelo;
                else
                    ReceitaBruta1PictureBox.ImageLocation = _arquivoVermelho;
                
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                ReceitaBruta2PictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(ReceitaBruta2PrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (ReceitaBruta2PrevLabel.DecimalValue != 0)
                        valor = (ReceitaBruta2RealLabel.DecimalValue / ReceitaBruta2PrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (ReceitaBruta2RealLabel.DecimalValue != 0)
                            valor = (ReceitaBruta2PrevLabel.DecimalValue / ReceitaBruta2RealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (ReceitaBruta2PrevLabel.DecimalValue == ReceitaBruta2RealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - ReceitaBruta2RealLabel.DecimalValue);
                    }
                }

                //if (ReceitaBruta2PrevLabel.DecimalValue != 0)
                //   valor = (ReceitaBruta2RealLabel.DecimalValue / ReceitaBruta2PrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (ReceitaBruta2PrevLabel.DecimalValue == 0 && ReceitaBruta2RealLabel.DecimalValue == 0)
                    || (ReceitaBruta2PrevLabel.DecimalValue > 0 && ReceitaBruta2RealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    ReceitaBruta2PictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    ReceitaBruta2PictureBox.ImageLocation = _arquivoAmarelo;
                else
                    ReceitaBruta2PictureBox.ImageLocation = _arquivoVermelho;
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                ReceitaSubsidio1PictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(ReceitaSubsidioPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (ReceitaSubsidioPrevLabel.DecimalValue != 0)
                        valor = (ReceitaSubsidioRealLabel.DecimalValue / ReceitaSubsidioPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (ReceitaSubsidioRealLabel.DecimalValue != 0)
                            valor = (ReceitaSubsidioPrevLabel.DecimalValue / ReceitaSubsidioRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (ReceitaSubsidioPrevLabel.DecimalValue == ReceitaSubsidioRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - ReceitaSubsidioRealLabel.DecimalValue);
                    }
                }
                // if (ReceitaSubsidioPrevLabel.DecimalValue != 0)
                //  valor = (ReceitaSubsidioRealLabel.DecimalValue / ReceitaSubsidioPrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (ReceitaSubsidioPrevLabel.DecimalValue == 0 && ReceitaSubsidioRealLabel.DecimalValue == 0)
                   || (ReceitaSubsidioPrevLabel.DecimalValue > 0 && ReceitaSubsidioRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    ReceitaSubsidio1PictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    ReceitaSubsidio1PictureBox.ImageLocation = _arquivoAmarelo;
                else
                    ReceitaSubsidio1PictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void ReceitaBruta1PrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            ReceitaLiquidaPrevLabel.DecimalValue = ReceitaBruta1PrevLabel.DecimalValue - DeducoesReceitaPrevLabel.DecimalValue;
            ReceitaBruta1RealLabel_DecimalValueChanged(sender, e);
        }

        private void ReceitaBruta1RealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            ReceitaLiquidaRealLabel.DecimalValue = ReceitaBruta1RealLabel.DecimalValue - DeducoesReceitaRealLabel.DecimalValue;

            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                DeducoesReceitaPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(DeducoesReceitaPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (DeducoesReceitaPrevLabel.DecimalValue != 0)
                        valor = (DeducoesReceitaRealLabel.DecimalValue / DeducoesReceitaPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (DeducoesReceitaRealLabel.DecimalValue != 0)
                            valor = (DeducoesReceitaPrevLabel.DecimalValue / DeducoesReceitaRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (DeducoesReceitaPrevLabel.DecimalValue == DeducoesReceitaRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - DeducoesReceitaRealLabel.DecimalValue);
                    }
                }

                //if (DeducoesReceitaRealLabel.DecimalValue != 0)
                //  valor = (DeducoesReceitaPrevLabel.DecimalValue / DeducoesReceitaRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (DeducoesReceitaPrevLabel.DecimalValue == 0 && DeducoesReceitaRealLabel.DecimalValue == 0)
                    || (DeducoesReceitaPrevLabel.DecimalValue > 0 && DeducoesReceitaRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    DeducoesReceitaPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    DeducoesReceitaPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    DeducoesReceitaPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void CustoFolhaAdm1PrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            CustoFolhaTotalPrevLabel.DecimalValue = CustoFolhaAdm1PrevLabel.DecimalValue + CustoFolhaMan1PrevLabel.DecimalValue + CustoFolhaOp1PrevLabel.DecimalValue;
            CustoFolhaOp2PrevLabel.DecimalValue = CustoFolhaOp1PrevLabel.DecimalValue;
            CustoFolhaMan2PrevLabel.DecimalValue = CustoFolhaMan1PrevLabel.DecimalValue;
            CustoFolhaAdm2PrevLabel.DecimalValue = CustoFolhaAdm1RealLabel.DecimalValue;

            EficienciaFolhaAdmPrevLabel.DecimalValue = CustoFolhaAdm1PrevLabel.DecimalValue / 1000;
            EficienciaFolhaManPrevLabel.DecimalValue = CustoFolhaMan1PrevLabel.DecimalValue / 1000;
            EficienciaFolhaOpPrevLabel.DecimalValue = CustoFolhaOp1PrevLabel.DecimalValue / 1000;

            CustoFolhaAdm1RealLabel_DecimalValueChanged(sender, e);
        }

        private void CustoFolhaAdm1RealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            CustoFolhaTotalRealLabel.DecimalValue = CustoFolhaAdm1RealLabel.DecimalValue + CustoFolhaMan1RealLabel.DecimalValue + CustoFolhaOp1RealLabel.DecimalValue;
            CustoFolhaOp2RealLabel.DecimalValue = CustoFolhaOp1RealLabel.DecimalValue;
            CustoFolhaMan2RealLabel.DecimalValue = CustoFolhaMan1RealLabel.DecimalValue;
            CustoFolhaAdm2RealLabel.DecimalValue = CustoFolhaAdm1RealLabel.DecimalValue;

            EficienciaFolhaAdmRealLabel.DecimalValue = CustoFolhaAdm1RealLabel.DecimalValue / 1000;
            EficienciaFolhaManRealLabel.DecimalValue = CustoFolhaMan1RealLabel.DecimalValue / 1000;
            EficienciaFolhaOpRealLabel.DecimalValue = CustoFolhaOp1RealLabel.DecimalValue / 1000;

            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoFolhaPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoFolhaTotalPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoFolhaTotalPrevLabel.DecimalValue != 0)
                        valor = (CustoFolhaTotalRealLabel.DecimalValue / CustoFolhaTotalPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoFolhaTotalRealLabel.DecimalValue != 0)
                            valor = (CustoFolhaTotalPrevLabel.DecimalValue / CustoFolhaTotalRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoFolhaTotalPrevLabel.DecimalValue == CustoFolhaTotalRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoFolhaTotalRealLabel.DecimalValue);
                    }
                }
                //if (CustoFolhaTotalRealLabel.DecimalValue != 0)
                //  valor = (CustoFolhaTotalPrevLabel.DecimalValue / CustoFolhaTotalRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CustoFolhaTotalPrevLabel.DecimalValue == 0 && CustoFolhaTotalRealLabel.DecimalValue == 0)
                    || (CustoFolhaTotalPrevLabel.DecimalValue > 0 && CustoFolhaTotalRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoFolhaPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    CustoFolhaPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoFolhaPictureBox.ImageLocation = _arquivoVermelho;
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoFolhaAdm1PictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoFolhaAdm1PrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoFolhaAdm1PrevLabel.DecimalValue != 0)
                        valor = (CustoFolhaAdm1RealLabel.DecimalValue / CustoFolhaAdm1PrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoFolhaAdm1RealLabel.DecimalValue != 0)
                            valor = (CustoFolhaAdm1PrevLabel.DecimalValue / CustoFolhaAdm1RealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoFolhaAdm1PrevLabel.DecimalValue == CustoFolhaAdm1RealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoFolhaAdm1RealLabel.DecimalValue);
                    }
                }

                //if (CustoFolhaAdm1RealLabel.DecimalValue != 0)
                //  valor = (CustoFolhaAdm1PrevLabel.DecimalValue / CustoFolhaAdm1RealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CustoFolhaAdm1PrevLabel.DecimalValue == 0 && CustoFolhaAdm1RealLabel.DecimalValue == 0)
                    || (CustoFolhaAdm1PrevLabel.DecimalValue > 0 && CustoFolhaAdm1RealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoFolhaAdm1PictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    CustoFolhaAdm1PictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoFolhaAdm1PictureBox.ImageLocation = _arquivoVermelho;
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoFolhaOp1PictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoFolhaOp1PrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoFolhaOp1PrevLabel.DecimalValue != 0)
                        valor = (CustoFolhaOp1RealLabel.DecimalValue / CustoFolhaOp1PrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoFolhaOp1RealLabel.DecimalValue != 0)
                            valor = (CustoFolhaOp1PrevLabel.DecimalValue / CustoFolhaOp1RealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoFolhaOp1PrevLabel.DecimalValue == CustoFolhaOp1RealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoFolhaOp1RealLabel.DecimalValue);
                    }
                }

                // if (CustoFolhaOp1RealLabel.DecimalValue != 0)
                //    valor = (CustoFolhaOp1PrevLabel.DecimalValue / CustoFolhaOp1RealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CustoFolhaOp1PrevLabel.DecimalValue == 0 && CustoFolhaOp1RealLabel.DecimalValue == 0)
                    || (CustoFolhaOp1PrevLabel.DecimalValue > 0 && CustoFolhaOp1RealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoFolhaOp1PictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    CustoFolhaOp1PictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoFolhaOp1PictureBox.ImageLocation = _arquivoVermelho;
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoFolhaMan1PictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoFolhaMan1PrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoFolhaMan1PrevLabel.DecimalValue != 0)
                        valor = (CustoFolhaMan1RealLabel.DecimalValue / CustoFolhaMan1PrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoFolhaMan1RealLabel.DecimalValue != 0)
                            valor = (CustoFolhaMan1PrevLabel.DecimalValue / CustoFolhaMan1RealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoFolhaMan1PrevLabel.DecimalValue == CustoFolhaMan1RealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoFolhaMan1RealLabel.DecimalValue);
                    }
                }
                // if (CustoFolhaMan1RealLabel.DecimalValue != 0)
                //   valor = (CustoFolhaMan1PrevLabel.DecimalValue / CustoFolhaMan1RealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CustoFolhaMan1PrevLabel.DecimalValue == 0 && CustoFolhaMan1RealLabel.DecimalValue == 0)
                    || (CustoFolhaMan1PrevLabel.DecimalValue > 0 && CustoFolhaMan1RealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoFolhaMan1PictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    CustoFolhaMan1PictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoFolhaMan1PictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void ReceitaLiquidaPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal custos = (CustoFolhaTotalPrevLabel.DecimalValue + CustoManutencaoFrotaPrevLabel.DecimalValue + OutrosCustosPrevLabel.DecimalValue);
            decimal perc = 0;
            EbitdaPrevLabel.DecimalValue = 0;

            if (ReceitaBruta2PrevLabel.DecimalValue != 0)
            {
                perc = (((ReceitaLiquidaPrevLabel.DecimalValue - ReceitaSubsidioPrevLabel.DecimalValue) - custos) / ReceitaBruta2PrevLabel.DecimalValue) * 100;
                EbitdaPrevLabel.DecimalValue = perc;
            }
            EbitdaPrevLabel.Refresh();

            Ebitda2PrevLabel.DecimalValue = ((ReceitaLiquidaPrevLabel.DecimalValue - ReceitaSubsidioPrevLabel.DecimalValue ) - custos);

            EficienciaFolhaTotalPrevLabel.DecimalValue = CustoFolhaTotalPrevLabel.DecimalValue / 1000;

           // vira do DRE
            //if (ReceitaBruta1PrevLabel.DecimalValue != 0)
            //{
            //    perc = ((ReceitaLiquidaPrevLabel.DecimalValue - custos) / ReceitaBruta1PrevLabel.DecimalValue) * 100;
            //    EbitdaFPrevLabel.DecimalValue = perc;
            //}
            //EbitdaFPrevLabel.Refresh();

            //Ebitda2FPrevLabel.DecimalValue = ((ReceitaLiquidaPrevLabel.DecimalValue) - custos);

            //if (ReceitaBruta1PrevLabel.DecimalValue != 0)
            //{
            //    perc = ((ReceitaLiquidaPrevLabel.DecimalValue - custos) / ReceitaBruta1PrevLabel.DecimalValue) * 100;
            //    EbitdaPrevLabel.DecimalValue = perc;
            //}
            //EbitdaPrevLabel.Refresh();

            //Ebitda2PrevLabel.DecimalValue = (ReceitaLiquidaPrevLabel.DecimalValue - custos);

            //EficienciaFolhaTotalPrevLabel.DecimalValue = CustoFolhaTotalPrevLabel.DecimalValue / 1000;

            //if (ReceitaBruta2PrevLabel.DecimalValue != 0)
            //{
            //    perc = (((ReceitaLiquidaPrevLabel.DecimalValue - ReceitaSubsidioPrevLabel.DecimalValue) - custos) / ReceitaBruta2PrevLabel.DecimalValue) * 100;
            //    EbitdaFPrevLabel.DecimalValue = perc;
            //}
            //EbitdaFPrevLabel.Refresh();

            //Ebitda2FPrevLabel.DecimalValue = ((ReceitaLiquidaPrevLabel.DecimalValue - ReceitaSubsidioPrevLabel.DecimalValue) - custos);

            ReceitaLiquidaRealLabel_DecimalValueChanged(sender, e);

        }

        private void ReceitaLiquidaRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            EbitdaRealLabel.DecimalValue = 0;
            //decimal perc = 0;
            decimal custos = (CustoFolhaTotalRealLabel.DecimalValue + CustoManutencaoFrotaRealLabel.DecimalValue + OutrosCustosRealLabel.DecimalValue);

            if (ReceitaBruta2RealLabel.DecimalValue != 0)
                EbitdaRealLabel.DecimalValue = (((ReceitaLiquidaRealLabel.DecimalValue - ReceitaSubsidioRealLabel.DecimalValue )-
                                                custos) / ReceitaBruta2RealLabel.DecimalValue) * 100;

            Ebitda2RealLabel.DecimalValue = ((ReceitaLiquidaRealLabel.DecimalValue - ReceitaSubsidioRealLabel.DecimalValue) - custos);

            EficienciaFolhaTotalRealLabel.DecimalValue = CustoFolhaTotalRealLabel.DecimalValue / 1000;

            //if (ReceitaBruta1RealLabel.DecimalValue != 0)
            //{
            //    perc = (((ReceitaLiquidaRealLabel.DecimalValue) - custos) / ReceitaBruta1RealLabel.DecimalValue) * 100;
            //    EbitdaFRealLabel.DecimalValue = perc;
            //}
            //EbitdaFRealLabel.Refresh();

            //Ebitda2FRealLabel.DecimalValue = ((ReceitaLiquidaRealLabel.DecimalValue) - custos);

            //if (ReceitaBruta1RealLabel.DecimalValue != 0)
            //    EbitdaRealLabel.DecimalValue = ((ReceitaLiquidaRealLabel.DecimalValue -
            //                                    custos) / ReceitaBruta1RealLabel.DecimalValue) * 100;

            //Ebitda2RealLabel.DecimalValue = (ReceitaLiquidaRealLabel.DecimalValue -
            //                                (CustoFolhaTotalRealLabel.DecimalValue + CustoManutencaoFrotaRealLabel.DecimalValue + OutrosCustosRealLabel.DecimalValue));

            //EficienciaFolhaTotalRealLabel.DecimalValue = CustoFolhaTotalRealLabel.DecimalValue / 1000;

            //if (ReceitaBruta1RealLabel.DecimalValue != 0)
            //{
            //    perc = (((ReceitaLiquidaRealLabel.DecimalValue - ReceitaSubsidioRealLabel.DecimalValue) - custos) / ReceitaBruta1RealLabel.DecimalValue) * 100;
            //    EbitdaFRealLabel.DecimalValue = perc;
            //}
            //EbitdaFRealLabel.Refresh();

            //Ebitda2FRealLabel.DecimalValue = ((ReceitaLiquidaRealLabel.DecimalValue - ReceitaSubsidioRealLabel.DecimalValue) - custos);


            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                ReceitaLiquidaPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(ReceitaLiquidaPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (ReceitaLiquidaPrevLabel.DecimalValue != 0)
                        valor = (ReceitaLiquidaRealLabel.DecimalValue / ReceitaLiquidaPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (ReceitaLiquidaRealLabel.DecimalValue != 0)
                            valor = (ReceitaLiquidaPrevLabel.DecimalValue / ReceitaLiquidaRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (ReceitaLiquidaPrevLabel.DecimalValue == ReceitaLiquidaRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - ReceitaLiquidaPrevLabel.DecimalValue);
                    }
                }

                //if (ReceitaLiquidaPrevLabel.DecimalValue != 0)
                //  valor = (ReceitaLiquidaRealLabel.DecimalValue / ReceitaLiquidaPrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (ReceitaLiquidaPrevLabel.DecimalValue == 0 && ReceitaLiquidaRealLabel.DecimalValue == 0)
                    || (ReceitaLiquidaPrevLabel.DecimalValue > 0 && ReceitaLiquidaRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    ReceitaLiquidaPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    ReceitaLiquidaPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    ReceitaLiquidaPictureBox.ImageLocation = _arquivoVermelho;
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                EbitdaFPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(EbitdaFPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (EbitdaFPrevLabel.DecimalValue != 0)
                        valor = (EbitdaFRealLabel.DecimalValue / EbitdaFPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (EbitdaFRealLabel.DecimalValue != 0)
                            valor = (EbitdaFPrevLabel.DecimalValue / EbitdaFRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (EbitdaFPrevLabel.DecimalValue == EbitdaFRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - EbitdaFRealLabel.DecimalValue);
                    }
                }

                //if (EbitdaFPrevLabel.DecimalValue != 0)
                //  valor = (EbitdaFRealLabel.DecimalValue / EbitdaFPrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (EbitdaFPrevLabel.DecimalValue == 0 && EbitdaFRealLabel.DecimalValue == 0)
                    || (EbitdaFPrevLabel.DecimalValue > 0 && EbitdaFRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    EbitdaFPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    EbitdaFPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    EbitdaFPictureBox.ImageLocation = _arquivoVermelho;
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                EbitdaPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(EbitdaPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (EbitdaPrevLabel.DecimalValue != 0)
                        valor = (EbitdaRealLabel.DecimalValue / EbitdaPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (EbitdaRealLabel.DecimalValue != 0)
                            valor = (EbitdaPrevLabel.DecimalValue / EbitdaRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (EbitdaPrevLabel.DecimalValue == EbitdaRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - EbitdaRealLabel.DecimalValue);
                    }
                }

                //if (EbitdaPrevLabel.DecimalValue != 0)
                //  valor = (EbitdaRealLabel.DecimalValue / EbitdaPrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (EbitdaPrevLabel.DecimalValue == 0 && EbitdaRealLabel.DecimalValue == 0)
                    || (EbitdaPrevLabel.DecimalValue > 0 && EbitdaRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    EbitdaPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    EbitdaPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    EbitdaPictureBox.ImageLocation = _arquivoVermelho;
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoManutencaoFrotaPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoManutencaoFrotaPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoManutencaoFrotaPrevLabel.DecimalValue != 0)
                        valor = (CustoManutencaoFrotaRealLabel.DecimalValue / CustoManutencaoFrotaPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoManutencaoFrotaRealLabel.DecimalValue != 0)
                            valor = (CustoManutencaoFrotaPrevLabel.DecimalValue / CustoManutencaoFrotaRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoManutencaoFrotaPrevLabel.DecimalValue == CustoManutencaoFrotaRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoManutencaoFrotaRealLabel.DecimalValue);
                    }
                }

                //if (CustoManutencaoFrotaRealLabel.DecimalValue != 0)
                //  valor = (CustoManutencaoFrotaPrevLabel.DecimalValue / CustoManutencaoFrotaRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CustoManutencaoFrotaPrevLabel.DecimalValue == 0 && CustoManutencaoFrotaRealLabel.DecimalValue == 0)
                    || (CustoManutencaoFrotaPrevLabel.DecimalValue > 0 && CustoManutencaoFrotaRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoManutencaoFrotaPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    CustoManutencaoFrotaPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoManutencaoFrotaPictureBox.ImageLocation = _arquivoVermelho;                
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                OutrosCustosPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(OutrosCustosPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (OutrosCustosPrevLabel.DecimalValue != 0)
                        valor = (OutrosCustosRealLabel.DecimalValue / OutrosCustosPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (OutrosCustosRealLabel.DecimalValue != 0)
                            valor = (OutrosCustosPrevLabel.DecimalValue / OutrosCustosRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (OutrosCustosPrevLabel.DecimalValue == OutrosCustosRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - OutrosCustosRealLabel.DecimalValue);
                    }
                }

                //if (OutrosCustosRealLabel.DecimalValue != 0)
                //  valor = (OutrosCustosPrevLabel.DecimalValue / OutrosCustosRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (OutrosCustosPrevLabel.DecimalValue == 0 && OutrosCustosRealLabel.DecimalValue == 0)
                    || (OutrosCustosPrevLabel.DecimalValue > 0 && OutrosCustosRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    OutrosCustosPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    OutrosCustosPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    OutrosCustosPictureBox.ImageLocation = _arquivoVermelho;                
            }
        }

        private void CustoPecasPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            if (KmRodadoPrevLabel.DecimalValue != 0)
            {
                if (Convert.ToInt32(_referencia) < 201905)
                    EficienciaManutencaoFrotaPrevLabel.DecimalValue = (CustoPecasPrevLabel.DecimalValue + CustoPneuPrevLabel.DecimalValue) / KmRodadoPrevLabel.DecimalValue;
                else
                    EficienciaManutencaoFrotaPrevLabel.DecimalValue = CustoPecasPrevLabel.DecimalValue / KmRodadoPrevLabel.DecimalValue;
            }
        }

        private void CustoPecasRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (KmRodadoRealLabel.DecimalValue != 0)
            {
                if (Convert.ToInt32(_referencia) < 201905)
                    EficienciaManutencaoFrotaRealLabel.DecimalValue = (CustoPecasRealLabel.DecimalValue + CustoPneuRealLabel.DecimalValue) / KmRodadoRealLabel.DecimalValue;
                else
                    EficienciaManutencaoFrotaRealLabel.DecimalValue = CustoPecasRealLabel.DecimalValue / KmRodadoRealLabel.DecimalValue;
                
            }
            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoPecasPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoPecasPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoPecasPrevLabel.DecimalValue != 0)
                        valor = (CustoPecasRealLabel.DecimalValue / CustoPecasPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoPecasRealLabel.DecimalValue != 0)
                            valor = (CustoPecasPrevLabel.DecimalValue / CustoPecasRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoPecasPrevLabel.DecimalValue == CustoPecasRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoPecasRealLabel.DecimalValue);
                    }
                }
                //if (CustoPecasRealLabel.DecimalValue != 0)
                //  valor = (CustoPecasPrevLabel.DecimalValue / CustoPecasRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CustoPecasPrevLabel.DecimalValue == 0 && CustoPecasRealLabel.DecimalValue == 0)
                    || (CustoPecasPrevLabel.DecimalValue > 0 && CustoPecasRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoPecasPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    CustoPecasPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoPecasPictureBox.ImageLocation = _arquivoVermelho;
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoPneusPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoPneuPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoPneuPrevLabel.DecimalValue != 0)
                        valor = (CustoPneuRealLabel.DecimalValue / CustoPneuPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoPneuRealLabel.DecimalValue != 0)
                            valor = (CustoPneuPrevLabel.DecimalValue / CustoPneuRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoPneuPrevLabel.DecimalValue == CustoPneuRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoPneuRealLabel.DecimalValue);
                    }
                }

                //if (CustoPneuRealLabel.DecimalValue != 0)
                //  valor = (CustoPneuPrevLabel.DecimalValue / CustoPneuRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CustoPneuPrevLabel.DecimalValue == 0 && CustoPneuRealLabel.DecimalValue == 0)
                    || (CustoPneuPrevLabel.DecimalValue > 0 && CustoPneuRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoPneusPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    CustoPneusPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoPneusPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void CustoHorasExtrasOpPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            CustoHorasExtrasPrevLabel.DecimalValue = CustoHorasExtrasOpPrevLabel.DecimalValue + CustoHorasExtrasManPrevLabel.DecimalValue + CustoHorasExtrasAdmPrevLabel.DecimalValue;
            CustoHorasExtrasOpRealLabel_DecimalValueChanged(sender, e);

            CustoHorasExtrasSomaPrevLabel.DecimalValue = CustoHorasExtrasOpPrevLabel.DecimalValue + CustoHorasExtrasManPrevLabel.DecimalValue;
        }

        private void CustoHorasExtrasOpRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            CustoHorasExtrasRealLabel.DecimalValue = CustoHorasExtrasOpRealLabel.DecimalValue + CustoHorasExtrasManRealLabel.DecimalValue + CustoHorasExtrasAdmRealLabel.DecimalValue;
            CustoHorasExtrasSomaRealLabel.DecimalValue = CustoHorasExtrasOpRealLabel.DecimalValue + CustoHorasExtrasManRealLabel.DecimalValue;

            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoHEOpPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoPneuPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoHorasExtrasOpPrevLabel.DecimalValue != 0)
                        valor = (CustoHorasExtrasOpRealLabel.DecimalValue / CustoHorasExtrasOpPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoHorasExtrasOpRealLabel.DecimalValue != 0)
                            valor = (CustoHorasExtrasOpPrevLabel.DecimalValue / CustoHorasExtrasOpRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoHorasExtrasOpPrevLabel.DecimalValue == CustoHorasExtrasOpRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoHorasExtrasOpRealLabel.DecimalValue);
                    }
                }

                //if (CustoHorasExtrasOpRealLabel.DecimalValue != 0)
                //  valor = (CustoHorasExtrasOpPrevLabel.DecimalValue / CustoHorasExtrasOpRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CustoHorasExtrasOpPrevLabel.DecimalValue == 0 && CustoHorasExtrasOpRealLabel.DecimalValue == 0)
                    || (CustoHorasExtrasOpPrevLabel.DecimalValue > 0 && CustoHorasExtrasOpRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoHEOpPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    CustoHEOpPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoHEOpPictureBox.ImageLocation = _arquivoVermelho;
            }
        

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoHEManPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoHorasExtrasManPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoHorasExtrasOpPrevLabel.DecimalValue != 0)
                        valor = (CustoHorasExtrasManRealLabel.DecimalValue / CustoHorasExtrasManPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoHorasExtrasManRealLabel.DecimalValue != 0)
                            valor = (CustoHorasExtrasManPrevLabel.DecimalValue / CustoHorasExtrasManRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoHorasExtrasManPrevLabel.DecimalValue == CustoHorasExtrasManRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoHorasExtrasManRealLabel.DecimalValue);
                    }
                }
                //if (CustoHorasExtrasManRealLabel.DecimalValue != 0)
                //valor = (CustoHorasExtrasManPrevLabel.DecimalValue / CustoHorasExtrasManRealLabel.DecimalValue) * 100;
                if (valor >= 100 || (CustoHorasExtrasManPrevLabel.DecimalValue == 0 && CustoHorasExtrasManRealLabel.DecimalValue == 0)
                    || (CustoHorasExtrasManPrevLabel.DecimalValue > 0 && CustoHorasExtrasManRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoHEManPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    CustoHEManPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoHEManPictureBox.ImageLocation = _arquivoVermelho;
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoHEAdmPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoHorasExtrasAdmPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoHorasExtrasAdmPrevLabel.DecimalValue != 0)
                        valor = (CustoHorasExtrasAdmRealLabel.DecimalValue / CustoHorasExtrasAdmPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoHorasExtrasAdmRealLabel.DecimalValue != 0)
                            valor = (CustoHorasExtrasAdmPrevLabel.DecimalValue / CustoHorasExtrasAdmRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoHorasExtrasAdmPrevLabel.DecimalValue == CustoHorasExtrasAdmRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoHorasExtrasAdmRealLabel.DecimalValue);
                    }
                }

                if (valor >= 100 || (CustoHorasExtrasAdmPrevLabel.DecimalValue == 0 && CustoHorasExtrasAdmRealLabel.DecimalValue == 0)
                    || (CustoHorasExtrasAdmPrevLabel.DecimalValue > 0 && CustoHorasExtrasAdmRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoHEAdmPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    CustoHEAdmPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoHEAdmPictureBox.ImageLocation = _arquivoVermelho;
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                CustoHEPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoHorasExtrasPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoHorasExtrasPrevLabel.DecimalValue != 0)
                        valor = (CustoHorasExtrasRealLabel.DecimalValue / CustoHorasExtrasPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoHorasExtrasRealLabel.DecimalValue != 0)
                            valor = (CustoHorasExtrasPrevLabel.DecimalValue / CustoHorasExtrasRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoHorasExtrasPrevLabel.DecimalValue == CustoHorasExtrasRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoHorasExtrasRealLabel.DecimalValue);
                    }
                }

                if (valor >= 100 || (CustoHorasExtrasPrevLabel.DecimalValue == 0 && CustoHorasExtrasRealLabel.DecimalValue == 0)
                    || (CustoHorasExtrasPrevLabel.DecimalValue > 0 && CustoHorasExtrasRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoHEPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    CustoHEPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoHEPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void BuscarRealizadoButton_Click(object sender, EventArgs e)
        {
            _buscouRealizadoFinanceiro = true;
            _dataCorteFinanceiro = DateTime.Now;
            BuscarRealizadoButton.Cursor = Cursors.WaitCursor;
            mensagemSistemaLabel.Text = "Aguarde, Buscando valores realizados...";
            this.Refresh();

            DateTime inicio = Convert.ToDateTime("01/" + referenciaMaskedEditBox.Text);
            DateTime fim = Convert.ToDateTime("01/" + referenciaMaskedEditBox.Text).AddMonths(1).AddDays(-1);

            BuscarRealizadoButton.Cursor = Cursors.WaitCursor;
            decimal _valorReceitaBruta = new ValoresDasMetasNoGlobusBO().ReceitaBruta(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(ReceitaBruta2PrevLabel.Tag), _empresa.IdEmpresa);
            ReceitaBruta2RealLabel.DecimalValue = _valorReceitaBruta / 1000;
            ToolTipInfo _tool = new ToolTipInfo();
            _tool.Body.Text = "Valor DRE dividido por 1000";
            superToolTip1.SetToolTip(ReceitaBruta1RealLabel, _tool);

            decimal _valorSubsidio = new ValoresDasMetasNoGlobusBO().ReceitaSubsidio(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(ReceitaSubsidioPrevLabel.Tag), _empresa.IdEmpresa);
            ReceitaSubsidioRealLabel.DecimalValue = _valorSubsidio / 1000;

            decimal _valorDeducoes = new ValoresDasMetasNoGlobusBO().DeducoesSobreReceita(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(DeducoesReceitaPrevLabel.Tag), _empresa.IdEmpresa);
            DeducoesReceitaRealLabel.DecimalValue = _valorDeducoes / 1000;

            decimal _valorFolhaAdm = new ValoresDasMetasNoGlobusBO().FolhaAdministracao(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(CustoFolhaAdm1PrevLabel.Tag), _empresa.IdEmpresa);
            CustoFolhaAdm1RealLabel.DecimalValue = _valorFolhaAdm / 1000;

            decimal _valorFolhaOp = new ValoresDasMetasNoGlobusBO().FolhaOperacao(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(CustoFolhaOp1PrevLabel.Tag), _empresa.IdEmpresa);
            CustoFolhaOp1RealLabel.DecimalValue = _valorFolhaOp / 1000;

            decimal _valorFolhaMan = new ValoresDasMetasNoGlobusBO().FolhaManutencao(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(CustoFolhaMan1PrevLabel.Tag), _empresa.IdEmpresa);
            CustoFolhaMan1RealLabel.DecimalValue = _valorFolhaMan / 1000;

            decimal _valorManutencaoFrota = new ValoresDasMetasNoGlobusBO().ManutencaoFrota(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(CustoManutencaoFrotaPrevLabel.Tag), _empresa.IdEmpresa);
            CustoManutencaoFrotaRealLabel.DecimalValue = _valorManutencaoFrota / 1000;

            decimal _valorOutrosCustos = new ValoresDasMetasNoGlobusBO().OutrosCustosDespesas(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(OutrosCustosPrevLabel.Tag), _empresa.IdEmpresa);
            OutrosCustosRealLabel.DecimalValue = _valorOutrosCustos / 1000;

            decimal _valorPecas = new ValoresDasMetasNoGlobusBO().Pecas(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(CustoPecasPrevLabel.Tag), _empresa.IdEmpresa);
            CustoPecasRealLabel.DecimalValue = _valorPecas / 1000;

            decimal _valorPneus = new ValoresDasMetasNoGlobusBO().Pneus(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(CustoPneuPrevLabel.Tag), _empresa.IdEmpresa);
            CustoPneuRealLabel.DecimalValue = _valorPneus / 1000; 

            decimal _valorKm = new ValoresDasMetasNoGlobusBO().KmRodado(_empresa.CodigoEmpresaGlobus, inicio, fim);
            KmRodadoRealLabel.DecimalValue = _valorKm / 1000;

            decimal _valorHEAdm = new ValoresDasMetasNoGlobusBO().HorasExtrasAdministracao(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(CustoHorasExtrasAdmPrevLabel.Tag), _empresa.IdEmpresa);
            CustoHorasExtrasAdmRealLabel.DecimalValue = _valorHEAdm / 1000;

            decimal _valorHEMan = new ValoresDasMetasNoGlobusBO().HorasExtrasManutencao(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(CustoHorasExtrasManPrevLabel.Tag), _empresa.IdEmpresa);
            CustoHorasExtrasManRealLabel.DecimalValue = _valorHEMan / 1000;

            decimal _valorHEOp = new ValoresDasMetasNoGlobusBO().HorasExtrasOperacao(_empresa.CodigoEmpresaGlobus, _referencia, Convert.ToInt32(CustoHorasExtrasOpPrevLabel.Tag), _empresa.IdEmpresa);
            CustoHorasExtrasOpRealLabel.DecimalValue = _valorHEOp / 1000;

            //CustoMultasOrgaoGestorRealLabel.DecimalValue = new ValoresDasMetasNoGlobusBO().ConsultarRadar(_empresa.CodigoEmpresaGlobus, inicio, fim, "Multas Orgão Gestor");

            BuscarRealizadoButton.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";
            this.Refresh();
        }

        private void EficienciaFolhaOpRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                EficienciaFolhaOpPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(EficienciaFolhaOpPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (EficienciaFolhaOpPrevLabel.DecimalValue != 0)
                        valor = (EficienciaFolhaOpRealLabel.DecimalValue / EficienciaFolhaOpPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (EficienciaFolhaOpRealLabel.DecimalValue != 0)
                            valor = (EficienciaFolhaOpPrevLabel.DecimalValue / EficienciaFolhaOpRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (EficienciaFolhaOpPrevLabel.DecimalValue == EficienciaFolhaOpRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - EficienciaFolhaOpRealLabel.DecimalValue);
                    }
                }

                //if (EficienciaFolhaOpRealLabel.DecimalValue != 0)
                //  valor = (EficienciaFolhaOpPrevLabel.DecimalValue / EficienciaFolhaOpRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (EficienciaFolhaOpPrevLabel.DecimalValue == 0 && EficienciaFolhaOpRealLabel.DecimalValue == 0)
                    || (EficienciaFolhaOpPrevLabel.DecimalValue > 0 && EficienciaFolhaOpRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    EficienciaFolhaOpPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    EficienciaFolhaOpPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    EficienciaFolhaOpPictureBox.ImageLocation = _arquivoVermelho;
                
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                EficienciaFolhaManPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(EficienciaFolhaManPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (EficienciaFolhaManPrevLabel.DecimalValue != 0)
                        valor = (EficienciaFolhaManRealLabel.DecimalValue / EficienciaFolhaManPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (EficienciaFolhaManRealLabel.DecimalValue != 0)
                            valor = (EficienciaFolhaManPrevLabel.DecimalValue / EficienciaFolhaManRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (EficienciaFolhaManPrevLabel.DecimalValue == EficienciaFolhaManRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - EficienciaFolhaManRealLabel.DecimalValue);
                    }
                }
                //if (EficienciaFolhaManRealLabel.DecimalValue != 0)
                //  valor = (EficienciaFolhaManPrevLabel.DecimalValue / EficienciaFolhaManRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (EficienciaFolhaManPrevLabel.DecimalValue == 0 && EficienciaFolhaManRealLabel.DecimalValue == 0)
                    || (EficienciaFolhaManPrevLabel.DecimalValue > 0 && EficienciaFolhaManRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    EficienciaFolhaManPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    EficienciaFolhaManPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    EficienciaFolhaManPictureBox.ImageLocation = _arquivoVermelho;
                
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                EficienciaFolhaAdmPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(EficienciaFolhaAdmPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (EficienciaFolhaAdmPrevLabel.DecimalValue != 0)
                        valor = (EficienciaFolhaAdmRealLabel.DecimalValue / EficienciaFolhaAdmPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (EficienciaFolhaAdmRealLabel.DecimalValue != 0)
                            valor = (EficienciaFolhaAdmPrevLabel.DecimalValue / EficienciaFolhaAdmRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (EficienciaFolhaAdmPrevLabel.DecimalValue == EficienciaFolhaAdmRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - EficienciaFolhaAdmRealLabel.DecimalValue);
                    }
                }
                //if (EficienciaFolhaAdmRealLabel.DecimalValue != 0)
                //  valor = (EficienciaFolhaAdmPrevLabel.DecimalValue / EficienciaFolhaAdmRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (EficienciaFolhaAdmPrevLabel.DecimalValue == 0 && EficienciaFolhaAdmRealLabel.DecimalValue == 0)
                    || (EficienciaFolhaAdmPrevLabel.DecimalValue > 0 && EficienciaFolhaAdmRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    EficienciaFolhaAdmPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    EficienciaFolhaAdmPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    EficienciaFolhaAdmPictureBox.ImageLocation = _arquivoVermelho;                
            }

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                EficienciaFolhaPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(EficienciaFolhaTotalPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (EficienciaFolhaTotalPrevLabel.DecimalValue != 0)
                        valor = (EficienciaFolhaTotalRealLabel.DecimalValue / EficienciaFolhaTotalPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (EficienciaFolhaTotalRealLabel.DecimalValue != 0)
                            valor = (EficienciaFolhaTotalPrevLabel.DecimalValue / EficienciaFolhaTotalRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (EficienciaFolhaTotalPrevLabel.DecimalValue == EficienciaFolhaTotalRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - EficienciaFolhaTotalRealLabel.DecimalValue);
                    }
                }

                //if (EficienciaFolhaTotalRealLabel.DecimalValue != 0)
                //  valor = (EficienciaFolhaTotalPrevLabel.DecimalValue / EficienciaFolhaTotalRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (EficienciaFolhaTotalPrevLabel.DecimalValue == 0 && EficienciaFolhaTotalRealLabel.DecimalValue == 0)
                    || (EficienciaFolhaTotalPrevLabel.DecimalValue > 0 && EficienciaFolhaTotalRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    EficienciaFolhaPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    EficienciaFolhaPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    EficienciaFolhaPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void EficienciaManutencaoFrotaRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteFinanceiro == DateTime.MinValue)
                EficienciaManutencaoFrotaPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(EficienciaManutencaoFrotaPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (EficienciaManutencaoFrotaPrevLabel.DecimalValue != 0)
                        valor = (EficienciaManutencaoFrotaRealLabel.DecimalValue / EficienciaManutencaoFrotaPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (EficienciaManutencaoFrotaRealLabel.DecimalValue != 0)
                            valor = (EficienciaManutencaoFrotaPrevLabel.DecimalValue / EficienciaManutencaoFrotaRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (EficienciaManutencaoFrotaPrevLabel.DecimalValue == EficienciaManutencaoFrotaRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - EficienciaManutencaoFrotaRealLabel.DecimalValue);
                    }
                }

                //if (EficienciaManutencaoFrotaRealLabel.DecimalValue != 0)
                //  valor = (EficienciaManutencaoFrotaPrevLabel.DecimalValue / EficienciaManutencaoFrotaRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (EficienciaManutencaoFrotaPrevLabel.DecimalValue == 0 && EficienciaManutencaoFrotaRealLabel.DecimalValue == 0)
                    || (EficienciaManutencaoFrotaPrevLabel.DecimalValue > 0 && EficienciaManutencaoFrotaRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    EficienciaManutencaoFrotaPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    EficienciaManutencaoFrotaPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    EficienciaManutencaoFrotaPictureBox.ImageLocation = _arquivoVermelho;                
            }
        }

        private void CustoMultasOrgaoGestorRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                CustoMultasPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CustoMultasOrgaoGestorPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CustoMultasOrgaoGestorPrevLabel.DecimalValue != 0)
                        valor = (CustoMultasOrgaoGestorRealLabel.DecimalValue / CustoMultasOrgaoGestorPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CustoMultasOrgaoGestorRealLabel.DecimalValue != 0)
                            valor = (CustoMultasOrgaoGestorPrevLabel.DecimalValue / CustoMultasOrgaoGestorRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CustoMultasOrgaoGestorPrevLabel.DecimalValue == CustoMultasOrgaoGestorRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CustoMultasOrgaoGestorRealLabel.DecimalValue);
                    }
                }
                //if (CustoMultasOrgaoGestorRealLabel.DecimalValue != 0)
                //  valor = (CustoMultasOrgaoGestorPrevLabel.DecimalValue / CustoMultasOrgaoGestorRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CustoMultasOrgaoGestorPrevLabel.DecimalValue == 0 && CustoMultasOrgaoGestorRealLabel.DecimalValue == 0)
                    || (CustoMultasOrgaoGestorPrevLabel.DecimalValue > 0 && CustoMultasOrgaoGestorRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CustoMultasPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    CustoMultasPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CustoMultasPictureBox.ImageLocation = _arquivoVermelho;                
            }
        }

        private void IndiceLimpezaFrotaRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                LimpezaFrotaPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(IndiceLimpezaFrotaPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (IndiceLimpezaFrotaPrevLabel.DecimalValue != 0)
                        valor = (IndiceLimpezaFrotaRealLabel.DecimalValue / IndiceLimpezaFrotaPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (IndiceLimpezaFrotaRealLabel.DecimalValue != 0)
                            valor = (IndiceLimpezaFrotaPrevLabel.DecimalValue / IndiceLimpezaFrotaRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (IndiceLimpezaFrotaPrevLabel.DecimalValue == IndiceLimpezaFrotaRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - IndiceLimpezaFrotaRealLabel.DecimalValue);
                    }
                }

                //if (IndiceLimpezaFrotaPrevLabel.DecimalValue != 0)
                //  valor = (IndiceLimpezaFrotaRealLabel.DecimalValue / IndiceLimpezaFrotaPrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (IndiceLimpezaFrotaPrevLabel.DecimalValue == 0 && IndiceLimpezaFrotaRealLabel.DecimalValue == 0)
                    || (IndiceLimpezaFrotaPrevLabel.DecimalValue > 0 && IndiceLimpezaFrotaRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    LimpezaFrotaPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    LimpezaFrotaPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    LimpezaFrotaPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void MKBFRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                MKBFPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(MKBFPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (MKBFPrevLabel.DecimalValue != 0)
                        valor = (MKBFRealLabel.DecimalValue / MKBFPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (MKBFRealLabel.DecimalValue != 0)
                            valor = (MKBFPrevLabel.DecimalValue / MKBFRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (MKBFPrevLabel.DecimalValue == MKBFRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - MKBFRealLabel.DecimalValue);
                    }
                }

                //if (MKBFPrevLabel.DecimalValue != 0)
                //  valor = (MKBFRealLabel.DecimalValue / MKBFPrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (MKBFPrevLabel.DecimalValue == 0 && MKBFRealLabel.DecimalValue == 0)
                    || (MKBFPrevLabel.DecimalValue > 0 && MKBFRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    MKBFPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    MKBFPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    MKBFPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void ReclamacaoRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                ReclamacaoPassageirosPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(ReclamacaoPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (ReclamacaoPrevLabel.DecimalValue != 0)
                        valor = (ReclamacaoRealLabel.DecimalValue / ReclamacaoPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (ReclamacaoRealLabel.DecimalValue != 0)
                            valor = (ReclamacaoPrevLabel.DecimalValue / ReclamacaoRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (ReclamacaoPrevLabel.DecimalValue == ReclamacaoRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - ReclamacaoRealLabel.DecimalValue);
                    }
                }

                //if (ReclamacaoPrevLabel.DecimalValue != 0)
                //  valor = (ReclamacaoRealLabel.DecimalValue / ReclamacaoPrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (ReclamacaoPrevLabel.DecimalValue == 0 && ReclamacaoRealLabel.DecimalValue == 0)
                    || (ReclamacaoPrevLabel.DecimalValue > 0 && ReclamacaoRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    ReclamacaoPassageirosPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    ReclamacaoPassageirosPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    ReclamacaoPassageirosPictureBox.ImageLocation = _arquivoVermelho;                
            }
        }

        private void AvariasComCulpaRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                AvariasPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(AvariasComCulpaPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (AvariasComCulpaPrevLabel.DecimalValue != 0)
                        valor = (AvariasComCulpaRealLabel.DecimalValue / AvariasComCulpaPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (AvariasComCulpaRealLabel.DecimalValue != 0)
                            valor = (AvariasComCulpaPrevLabel.DecimalValue / AvariasComCulpaRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (AvariasComCulpaPrevLabel.DecimalValue == AvariasComCulpaRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - AvariasComCulpaRealLabel.DecimalValue);
                    }
                }

                //if (AvariasComCulpaPrevLabel.DecimalValue != 0)
                //  valor = (AvariasComCulpaRealLabel.DecimalValue / AvariasComCulpaPrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (AvariasComCulpaPrevLabel.DecimalValue == 0 && AvariasComCulpaRealLabel.DecimalValue == 0)
                    || (AvariasComCulpaPrevLabel.DecimalValue > 0 && AvariasComCulpaRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    AvariasPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    AvariasPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    AvariasPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void AcidentesComCulpaRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                AcidentesPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(AcidentesComCulpaPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (AcidentesComCulpaPrevLabel.DecimalValue != 0)
                        valor = (AcidentesComCulpaRealLabel.DecimalValue / AcidentesComCulpaPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (AcidentesComCulpaRealLabel.DecimalValue != 0)
                            valor = (AcidentesComCulpaPrevLabel.DecimalValue / AcidentesComCulpaRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (AcidentesComCulpaPrevLabel.DecimalValue == AcidentesComCulpaRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - AcidentesComCulpaRealLabel.DecimalValue);
                    }
                }

                //if (AcidentesComCulpaPrevLabel.DecimalValue != 0)
                //  valor = (AcidentesComCulpaRealLabel.DecimalValue / AcidentesComCulpaPrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (AcidentesComCulpaPrevLabel.DecimalValue == 0 && AcidentesComCulpaRealLabel.DecimalValue == 0)
                    || (AcidentesComCulpaPrevLabel.DecimalValue > 0 && AcidentesComCulpaRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    AcidentesPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    AcidentesPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    AcidentesPictureBox.ImageLocation = _arquivoVermelho;                
            }
        }

        private void CumprimentoPartidaRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {

            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                CumprimentoPartidaPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CumprimentoPartidaPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CumprimentoPartidaPrevLabel.DecimalValue != 0)
                        valor = (CumprimentoPartidaRealLabel.DecimalValue / CumprimentoPartidaPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CumprimentoPartidaRealLabel.DecimalValue != 0)
                            valor = (CumprimentoPartidaPrevLabel.DecimalValue / CumprimentoPartidaRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CumprimentoPartidaPrevLabel.DecimalValue == CumprimentoPartidaRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CumprimentoPartidaRealLabel.DecimalValue);
                    }
                }

                //if (CumprimentoPartidaPrevLabel.DecimalValue != 0)
                //  valor = (CumprimentoPartidaRealLabel.DecimalValue / CumprimentoPartidaPrevLabel.DecimalValue) * 100;

                if (valor >= 100 || (CumprimentoPartidaPrevLabel.DecimalValue == 0 && CumprimentoPartidaRealLabel.DecimalValue == 0)
                    || (CumprimentoPartidaPrevLabel.DecimalValue > 0 && CumprimentoPartidaRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CumprimentoPartidaPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    CumprimentoPartidaPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CumprimentoPartidaPictureBox.ImageLocation = _arquivoVermelho;                
            }
        }

        private void CarrosRetidosRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                CarrosRetidosPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CarrosRetidosPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CumprimentoPartidaPrevLabel.DecimalValue != 0)
                        valor = (CarrosRetidosRealLabel.DecimalValue / CarrosRetidosPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CarrosRetidosRealLabel.DecimalValue != 0)
                            valor = (CarrosRetidosPrevLabel.DecimalValue / CarrosRetidosRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CarrosRetidosPrevLabel.DecimalValue == CarrosRetidosRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CarrosRetidosRealLabel.DecimalValue);
                    }
                }

               // if (CarrosRetidosRealLabel.DecimalValue != 0)
                 //   valor = (CarrosRetidosPrevLabel.DecimalValue / CarrosRetidosRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CarrosRetidosPrevLabel.DecimalValue == 0 && CarrosRetidosRealLabel.DecimalValue == 0)
                    || (CarrosRetidosPrevLabel.DecimalValue > 0 && CarrosRetidosRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    CarrosRetidosPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    CarrosRetidosPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CarrosRetidosPictureBox.ImageLocation = _arquivoVermelho;                
            }
        }

        private void ComprasEmergenciaisRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                ComprasEmergenciaisPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(ComprasEmergenciaisPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (ComprasEmergenciaisPrevLabel.DecimalValue != 0)
                        valor = (ComprasEmergenciaisRealLabel.DecimalValue / ComprasEmergenciaisPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (ComprasEmergenciaisRealLabel.DecimalValue != 0)
                            valor = (ComprasEmergenciaisPrevLabel.DecimalValue / ComprasEmergenciaisRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (ComprasEmergenciaisPrevLabel.DecimalValue == ComprasEmergenciaisRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - ComprasEmergenciaisRealLabel.DecimalValue);
                    }
                }

                //if (ComprasEmergenciaisRealLabel.DecimalValue != 0)
                  //  valor = (ComprasEmergenciaisPrevLabel.DecimalValue / ComprasEmergenciaisRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (ComprasEmergenciaisPrevLabel.DecimalValue == 0 && ComprasEmergenciaisRealLabel.DecimalValue == 0)
                    || (ComprasEmergenciaisPrevLabel.DecimalValue > 0 && ComprasEmergenciaisRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    ComprasEmergenciaisPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    ComprasEmergenciaisPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    ComprasEmergenciaisPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void IndicesAbsenteismoRealLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                AbsenteismoAdmPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(IndicesAbsenteismoAdmPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (IndicesAbsenteismoAdmPrevLabel.DecimalValue != 0)
                        valor = (IndicesAbsenteismoAdmRealLabel.DecimalValue / IndicesAbsenteismoAdmPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (IndicesAbsenteismoAdmRealLabel.DecimalValue != 0)
                            valor = (IndicesAbsenteismoAdmPrevLabel.DecimalValue / IndicesAbsenteismoAdmRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (IndicesAbsenteismoAdmPrevLabel.DecimalValue == IndicesAbsenteismoAdmRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - IndicesAbsenteismoAdmRealLabel.DecimalValue);
                    }
                }

                //if (IndicesAbsenteismoAdmRealLabel.DecimalValue != 0)
                  //  valor = (IndicesAbsenteismoAdmPrevLabel.DecimalValue / IndicesAbsenteismoAdmRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (IndicesAbsenteismoAdmPrevLabel.DecimalValue == 0 && IndicesAbsenteismoAdmRealLabel.DecimalValue == 0)
                    || (IndicesAbsenteismoAdmPrevLabel.DecimalValue > 0 && IndicesAbsenteismoAdmRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    AbsenteismoAdmPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    AbsenteismoAdmPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    AbsenteismoAdmPictureBox.ImageLocation = _arquivoVermelho;
            }

            valor = 0;
            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                AbsenteismoOpPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(IndicesAbsenteismoOpPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (IndicesAbsenteismoOpPrevLabel.DecimalValue != 0)
                        valor = (IndicesAbsenteismoOpRealLabel.DecimalValue / IndicesAbsenteismoOpPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (IndicesAbsenteismoOpRealLabel.DecimalValue != 0)
                            valor = (IndicesAbsenteismoOpPrevLabel.DecimalValue / IndicesAbsenteismoOpRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (IndicesAbsenteismoOpPrevLabel.DecimalValue == IndicesAbsenteismoOpRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - IndicesAbsenteismoOpRealLabel.DecimalValue);
                    }
                }
                //if (IndicesAbsenteismoOpRealLabel.DecimalValue != 0)
                  //  valor = (IndicesAbsenteismoOpPrevLabel.DecimalValue / IndicesAbsenteismoOpRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (IndicesAbsenteismoOpPrevLabel.DecimalValue == 0 && IndicesAbsenteismoOpRealLabel.DecimalValue == 0)
                    || (IndicesAbsenteismoOpPrevLabel.DecimalValue > 0 && IndicesAbsenteismoOpRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    AbsenteismoOpPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    AbsenteismoOpPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    AbsenteismoOpPictureBox.ImageLocation = _arquivoVermelho;                
            }

            valor = 0;
            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                AbsenteismoManPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(IndicesAbsenteismoManPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (IndicesAbsenteismoManPrevLabel.DecimalValue != 0)
                        valor = (IndicesAbsenteismoManRealLabel.DecimalValue / IndicesAbsenteismoManPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (IndicesAbsenteismoManRealLabel.DecimalValue != 0)
                            valor = (IndicesAbsenteismoManPrevLabel.DecimalValue / IndicesAbsenteismoManRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (IndicesAbsenteismoManPrevLabel.DecimalValue == IndicesAbsenteismoManRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - IndicesAbsenteismoManRealLabel.DecimalValue);
                    }
                }

                //if (IndicesAbsenteismoManRealLabel.DecimalValue != 0)
                  //  valor = (IndicesAbsenteismoManPrevLabel.DecimalValue / IndicesAbsenteismoManRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (IndicesAbsenteismoManPrevLabel.DecimalValue == 0 && IndicesAbsenteismoManRealLabel.DecimalValue == 0)
                    || (IndicesAbsenteismoManPrevLabel.DecimalValue > 0 && IndicesAbsenteismoManRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    AbsenteismoManPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    AbsenteismoManPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    AbsenteismoManPictureBox.ImageLocation = _arquivoVermelho;
            }

            valor = 0;
            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                AbsenteismoPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(IndicesAbsenteismoPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (IndicesAbsenteismoPrevLabel.DecimalValue != 0)
                        valor = (IndicesAbsenteismoRealLabel.DecimalValue / IndicesAbsenteismoPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (IndicesAbsenteismoRealLabel.DecimalValue != 0)
                            valor = (IndicesAbsenteismoPrevLabel.DecimalValue / IndicesAbsenteismoRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (IndicesAbsenteismoPrevLabel.DecimalValue == IndicesAbsenteismoRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - IndicesAbsenteismoRealLabel.DecimalValue);
                    }
                }

                //if (IndicesAbsenteismoRealLabel.DecimalValue != 0)
                  //  valor = (IndicesAbsenteismoPrevLabel.DecimalValue / IndicesAbsenteismoRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (IndicesAbsenteismoPrevLabel.DecimalValue == 0 && IndicesAbsenteismoRealLabel.DecimalValue == 0)
                    || (IndicesAbsenteismoPrevLabel.DecimalValue > 0 && IndicesAbsenteismoRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    AbsenteismoPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    AbsenteismoPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    AbsenteismoPictureBox.ImageLocation = _arquivoVermelho;
            }

        }

        private void IndiceTurnoverPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                TurnoverAdmPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(IndiceTurnoverAdmPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (IndiceTurnoverAdmPrevLabel.DecimalValue != 0)
                        valor = (IndiceTurnoverAdmRealLabel.DecimalValue / IndiceTurnoverAdmPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (IndiceTurnoverAdmRealLabel.DecimalValue != 0)
                            valor = (IndiceTurnoverAdmPrevLabel.DecimalValue / IndiceTurnoverAdmRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (IndiceTurnoverAdmPrevLabel.DecimalValue == IndiceTurnoverAdmRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - IndiceTurnoverAdmRealLabel.DecimalValue);
                    }
                }

                //if (IndiceTurnoverAdmRealLabel.DecimalValue != 0)
                  //  valor = (IndiceTurnoverAdmPrevLabel.DecimalValue / IndiceTurnoverAdmRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (IndiceTurnoverAdmPrevLabel.DecimalValue == 0 && IndiceTurnoverAdmRealLabel.DecimalValue == 0) 
                    || (IndiceTurnoverAdmPrevLabel.DecimalValue > 0 && IndiceTurnoverAdmRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    TurnoverAdmPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    TurnoverAdmPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    TurnoverAdmPictureBox.ImageLocation = _arquivoVermelho;
            }

            valor = 0;
            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                TurnoverOpPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(IndiceTurnoverOpPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (IndiceTurnoverOpPrevLabel.DecimalValue != 0)
                        valor = (IndiceTurnoverAdmRealLabel.DecimalValue / IndiceTurnoverOpPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (IndiceTurnoverOpRealLabel.DecimalValue != 0)
                            valor = (IndiceTurnoverOpPrevLabel.DecimalValue / IndiceTurnoverOpRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (IndiceTurnoverOpPrevLabel.DecimalValue == IndiceTurnoverOpRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - IndiceTurnoverOpRealLabel.DecimalValue);
                    }
                }

                //if (IndiceTurnoverOpRealLabel.DecimalValue != 0)
                  ///  valor = (IndiceTurnoverOpPrevLabel.DecimalValue / IndiceTurnoverOpRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (IndiceTurnoverOpPrevLabel.DecimalValue == 0 && IndiceTurnoverOpRealLabel.DecimalValue == 0)
                    || (IndiceTurnoverOpPrevLabel.DecimalValue > 0 && IndiceTurnoverOpRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    TurnoverOpPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    TurnoverOpPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    TurnoverOpPictureBox.ImageLocation = _arquivoVermelho;
            }

            valor = 0;
            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                TurnoverManPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(IndiceTurnoverManPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (IndiceTurnoverManPrevLabel.DecimalValue != 0)
                        valor = (IndiceTurnoverManRealLabel.DecimalValue / IndiceTurnoverManPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (IndiceTurnoverManRealLabel.DecimalValue != 0)
                            valor = (IndiceTurnoverManPrevLabel.DecimalValue / IndiceTurnoverManRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (IndiceTurnoverManPrevLabel.DecimalValue == IndiceTurnoverManRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - IndiceTurnoverManRealLabel.DecimalValue);
                    }
                }
                //if (IndiceTurnoverManRealLabel.DecimalValue != 0)
                  //  valor = (IndiceTurnoverManPrevLabel.DecimalValue / IndiceTurnoverManRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (IndiceTurnoverManPrevLabel.DecimalValue == 0 && IndiceTurnoverManRealLabel.DecimalValue == 0)
                    || (IndiceTurnoverManPrevLabel.DecimalValue > 0 && IndiceTurnoverManRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    TurnoverManPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    TurnoverManPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    TurnoverManPictureBox.ImageLocation = _arquivoVermelho;
            }

            valor = 0;
            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                TurnoverPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(IndiceTurnoverPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (IndiceTurnoverPrevLabel.DecimalValue != 0)
                        valor = (IndiceTurnoverRealLabel.DecimalValue / IndiceTurnoverPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (IndiceTurnoverRealLabel.DecimalValue != 0)
                            valor = (IndiceTurnoverPrevLabel.DecimalValue / IndiceTurnoverRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (IndiceTurnoverPrevLabel.DecimalValue == IndiceTurnoverRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - IndiceTurnoverRealLabel.DecimalValue);
                    }
                }
                //if (IndiceTurnoverRealLabel.DecimalValue != 0)
                  //  valor = (IndiceTurnoverPrevLabel.DecimalValue / IndiceTurnoverRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (IndiceTurnoverPrevLabel.DecimalValue == 0 && IndiceTurnoverRealLabel.DecimalValue == 0)
                    || (IndiceTurnoverPrevLabel.DecimalValue > 0 && IndiceTurnoverRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    TurnoverPictureBox.ImageLocation = _arquivoVerde;
                else
                    if (valor >= 98 && valor < 100)
                    TurnoverPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    TurnoverPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void CNHVencidasPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                CHNVencidasPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(CNHVencidasPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (CNHVencidasPrevLabel.DecimalValue != 0)
                        valor = (CNHVencidasRealLabel.DecimalValue / CNHVencidasPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (CNHVencidasRealLabel.DecimalValue != 0)
                            valor = (CNHVencidasPrevLabel.DecimalValue / CNHVencidasRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (CNHVencidasPrevLabel.DecimalValue == CNHVencidasRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - CNHVencidasRealLabel.DecimalValue);
                    }
                }

                //if (CNHVencidasRealLabel.DecimalValue != 0)
                  //  valor = (CNHVencidasPrevLabel.DecimalValue / CNHVencidasRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (CNHVencidasPrevLabel.DecimalValue == CNHVencidasRealLabel.DecimalValue))
                    CHNVencidasPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    CHNVencidasPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    CHNVencidasPictureBox.ImageLocation = _arquivoVermelho;
            }


            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                ExamesVencidosPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(ExamesVencidosPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (ExamesVencidosPrevLabel.DecimalValue != 0)
                        valor = (ExamesVencidosRealLabel.DecimalValue / ExamesVencidosPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (ExamesVencidosRealLabel.DecimalValue != 0)
                            valor = (ExamesVencidosPrevLabel.DecimalValue / ExamesVencidosRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (ExamesVencidosPrevLabel.DecimalValue == ExamesVencidosRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - ExamesVencidosRealLabel.DecimalValue);
                    }
                }
                //if (ExamesVencidosRealLabel.DecimalValue != 0)
                  //  valor = (ExamesVencidosPrevLabel.DecimalValue / ExamesVencidosRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (ExamesVencidosPrevLabel.DecimalValue == ExamesVencidosRealLabel.DecimalValue))
                    ExamesVencidosPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    ExamesVencidosPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    ExamesVencidosPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            bool _encontrou = false;
            if (String.IsNullOrEmpty(MotivoPrevistoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o motivo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                MotivoPrevistoTextBox.Focus();
                return;
            }

            foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(PrevistoAntesCurrencyTextBox.Tag)))
            {
                _encontrou = true;
                item.MotivoEdicaoPrevisto = MotivoPrevistoTextBox.Text;
                item.Previsto = PrevistoAtualCurrencyTextBox.DecimalValue;
                item.PrevistoOriginal = PrevistoAntesCurrencyTextBox.DecimalValue;
            }
            if (!_encontrou)
            {
                ValoresDasMetas _val = new ValoresDasMetas();
                _val.IdMetas = Convert.ToInt32(PrevistoAntesCurrencyTextBox.Tag);
                _val.Previsto = PrevistoAtualCurrencyTextBox.DecimalValue;
                _val.PrevistoOriginal = PrevistoAntesCurrencyTextBox.DecimalValue;
                _val.MotivoEdicaoPrevisto = MotivoPrevistoTextBox.Text;
                _val.IdEmpresa = _empresa.IdEmpresa;

                _listaValoresMetas.Add(_val);
            }

            MotivoPrevistoPanel.Visible = false;
            _componenteFocado.Focus();

            ((CurrencyTextBox)_componenteFocado).PositiveColor = Color.DarkRed;
            ToolTipInfo _tool = new ToolTipInfo();
            _tool.Footer.Font = label41.Font;
            _tool.Footer.Text = "Previsto Anterior ".PadRight(20) + PrevistoAntesCurrencyTextBox.Text + Environment.NewLine +
                                "Previsto Atual ".PadRight(20) + PrevistoAtualCurrencyTextBox.Text + Environment.NewLine +
                                "Motivo ".PadRight(20) + MotivoPrevistoTextBox.Text;
            superToolTip1.SetToolTip(_componenteFocado, _tool);
        }

        private void MotivoPrevistoTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(MotivoPrevistoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o motivo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                MotivoPrevistoTextBox.Focus();
                return;
            }
        }

        private void buttonAdv2_Click(object sender, EventArgs e)// Confirmar Motivo Realizado
        {
            bool _encontrou = false;

            if (String.IsNullOrEmpty(MotivoRealTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o motivo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                MotivoRealTextBox.Focus();
                return;
            }

            foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(RealAntesCurrencyTextBox.Tag)))
            {
                _encontrou = true;
                item.MotivoEdicaoReal = MotivoRealTextBox.Text;
                item.Realizado = RealAtualCurrencyTextBox.DecimalValue;
                item.RealizadoOriginal = RealAntesCurrencyTextBox.DecimalValue;
            }
            if (!_encontrou)
            {
                ValoresDasMetas _val = new ValoresDasMetas();
                _val.IdMetas = Convert.ToInt32(RealAntesCurrencyTextBox.Tag);
                _val.Previsto = RealAtualCurrencyTextBox.DecimalValue;
                _val.PrevistoOriginal = RealAntesCurrencyTextBox.DecimalValue;
                _val.MotivoEdicaoReal = MotivoRealPanel.Text;
                _val.IdEmpresa = _empresa.IdEmpresa;

                _listaValoresMetas.Add(_val);
            }

            MotivoRealPanel.Visible = false;
            _componenteFocado.Focus();

            ((CurrencyTextBox)_componenteFocado).PositiveColor = Color.DarkRed;
            ToolTipInfo _tool = new ToolTipInfo();
            _tool.Footer.Font = label41.Font;
            _tool.Footer.Text = "Real Anterior ".PadRight(20) + RealAntesCurrencyTextBox.Text + Environment.NewLine +
                                "Real Atual ".PadRight(20) + RealAtualCurrencyTextBox.Text + Environment.NewLine +
                                "Motivo ".PadRight(20) + MotivoRealTextBox.Text;
            superToolTip1.SetToolTip(_componenteFocado, _tool);

        }

        private void ClientePanel_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void BuscarPrevistoButton_Click(object sender, EventArgs e)
        {// operacional
            _buscouRealizadoOperacional = true;
            _dataCorteOperacional = DateTime.Now;
            BuscarPrevistoButton.Cursor = Cursors.WaitCursor;
            mensagemSistemaLabel.Text = "Aguarde, Buscando valores realizados...";
            this.Refresh();

            DateTime inicio = Convert.ToDateTime("01/" + referenciaMaskedEditBox.Text);
            DateTime fim = Convert.ToDateTime("01/" + referenciaMaskedEditBox.Text).AddMonths(1).AddDays(-1);
            ToolTipInfo _tool = new ToolTipInfo();

            BuscarPrevistoButton.Cursor = Cursors.WaitCursor;
            // buscar o MKBF por modal
            #region Rodoviario

            decimal _valorKm = new ValoresDasMetasNoGlobusBO().KmRodadoModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFRodoviarioPrevLabel.Tag));
            decimal _valorMKBF = new ValoresDasMetasNoGlobusBO().MKBFModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFRodoviarioPrevLabel.Tag));

            MKBFRodoviarioRealLabel.DecimalValue = (_valorMKBF == 0 ? 0 : _valorKm / _valorMKBF);
            _tool = new ToolTipInfo();
            _tool.Body.Text = "KM Rodado / Total de RA+SOS" + Environment.NewLine + _valorKm.ToString() + " / " + _valorMKBF.ToString();
            superToolTip1.SetToolTip(MKBFRodoviarioRealLabel, _tool);
            #endregion

            #region Urbano
            _valorKm = new ValoresDasMetasNoGlobusBO().KmRodadoModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFUrbanoPrevLabel.Tag));
            _valorMKBF = new ValoresDasMetasNoGlobusBO().MKBFModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFUrbanoPrevLabel.Tag));

            MKBFUrbanoRealLabel.DecimalValue = (_valorMKBF == 0 ? 0 : _valorKm / _valorMKBF);
            _tool = new ToolTipInfo();
            _tool.Body.Text = "KM Rodado / Total de RA+SOS" + Environment.NewLine + _valorKm.ToString() + " / " + _valorMKBF.ToString();
            superToolTip1.SetToolTip(MKBFUrbanoRealLabel, _tool);
            #endregion

            #region SubUrbano
            _valorKm = new ValoresDasMetasNoGlobusBO().KmRodadoModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFSubUrbanoPrevLabel.Tag));
            _valorMKBF = new ValoresDasMetasNoGlobusBO().MKBFModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFSubUrbanoPrevLabel.Tag));

            MKBFSubUrbanoRealLabel.DecimalValue = (_valorMKBF == 0 ? 0 : _valorKm / _valorMKBF);
            _tool = new ToolTipInfo();
            _tool.Body.Text = "KM Rodado / Total de RA+SOS" + Environment.NewLine + _valorKm.ToString() + " / " + _valorMKBF.ToString();
            superToolTip1.SetToolTip(MKBFSubUrbanoRealLabel, _tool);
            #endregion

            #region Municipal
            _valorKm = new ValoresDasMetasNoGlobusBO().KmRodadoModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFMunicipalPrevLabel.Tag));
            _valorMKBF = new ValoresDasMetasNoGlobusBO().MKBFModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFMunicipalPrevLabel.Tag));

            MKBFMunicipalRealLabel.DecimalValue = (_valorMKBF == 0 ? 0 : _valorKm / _valorMKBF);
            _tool = new ToolTipInfo();
            _tool.Body.Text = "KM Rodado / Total de RA+SOS" + Environment.NewLine + _valorKm.ToString() + " / " + _valorMKBF.ToString();
            superToolTip1.SetToolTip(MKBFMunicipalRealLabel, _tool);
            #endregion

            #region InterMunicipal
            _valorKm = new ValoresDasMetasNoGlobusBO().KmRodadoModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFIntermunicipalPrevLabel.Tag));
            _valorMKBF = new ValoresDasMetasNoGlobusBO().MKBFModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFIntermunicipalPrevLabel.Tag));

            MKBFIntermunicipalRealLabel.DecimalValue = (_valorMKBF == 0 ? 0 : _valorKm / _valorMKBF);
            _tool = new ToolTipInfo();
            _tool.Body.Text = "KM Rodado / Total de RA+SOS" + Environment.NewLine + _valorKm.ToString() + " / " + _valorMKBF.ToString();
            superToolTip1.SetToolTip(MKBFIntermunicipalRealLabel, _tool);
            #endregion

            #region Escolar
            _valorKm = new ValoresDasMetasNoGlobusBO().KmRodadoModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFEscolarPrevLabel.Tag));
            _valorMKBF = new ValoresDasMetasNoGlobusBO().MKBFModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFEscolarPrevLabel.Tag));

            MKBFEscolarRealLabel.DecimalValue = (_valorMKBF == 0 ? 0 : _valorKm / _valorMKBF);
            _tool = new ToolTipInfo();
            _tool.Body.Text = "KM Rodado / Total de RA+SOS" + Environment.NewLine + _valorKm.ToString() + " / " + _valorMKBF.ToString();
            superToolTip1.SetToolTip(MKBFEscolarRealLabel, _tool);
            #endregion

            #region Fretamento
            _valorKm = new ValoresDasMetasNoGlobusBO().KmRodadoModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFFretamentoPrevLabel.Tag));
            _valorMKBF = new ValoresDasMetasNoGlobusBO().MKBFModal(_empresa.CodigoEmpresaGlobus, inicio, fim, Convert.ToInt32(MKBFFretamentoPrevLabel.Tag));

            MKBFFretamentoRealLabel.DecimalValue = (_valorMKBF == 0 ? 0 : _valorKm / _valorMKBF);
            _tool = new ToolTipInfo();
            _tool.Body.Text = "KM Rodado / Total de RA+SOS" + Environment.NewLine + _valorKm.ToString() + " / " + _valorMKBF.ToString();
            superToolTip1.SetToolTip(MKBFFretamentoRealLabel, _tool);
            #endregion

            _valorKm = new ValoresDasMetasNoGlobusBO().KmRodado(_empresa.CodigoEmpresaGlobus, inicio, fim);
            KmRodadoRealLabel.DecimalValue = _valorKm / 1000;

            _valorMKBF = new ValoresDasMetasNoGlobusBO().MKBF(_empresa.CodigoEmpresaGlobus, inicio, fim);

            MKBFRealLabel.DecimalValue = (_valorMKBF == 0 ? 0 : _valorKm / _valorMKBF);
            _tool = new ToolTipInfo();
            _tool.Body.Text = "KM Rodado / Total de RA+SOS" + Environment.NewLine + _valorKm.ToString() + " / " + _valorMKBF.ToString();
            superToolTip1.SetToolTip(MKBFRealLabel, _tool);

            IndiceLimpezaFrotaRealLabel.DecimalValue = new ValoresDasMetasNoGlobusBO().ConsultarRadar(_empresa.CodigoEmpresaGlobus, inicio, fim, "Limpeza de Frota", "P");
            ReclamacaoRealLabel.DecimalValue = new ValoresDasMetasNoGlobusBO().ConsultarRadar(_empresa.CodigoEmpresaGlobus, inicio, fim, "Reclamação de Passageiro");
            ComprasEmergenciaisRealLabel.DecimalValue = new ValoresDasMetasNoGlobusBO().ConsultarRadar(_empresa.CodigoEmpresaGlobus, inicio, fim, "Compras Emergenciais", "P");

            CNHVencidasRealLabel.DecimalValue = new ValoresDasMetasNoGlobusBO().CNHVencidas(_empresa.CodigoEmpresaGlobus, inicio, fim);

            decimal _valorAvarias = new ValoresDasMetasNoGlobusBO().Avarias(_empresa.CodigoEmpresaGlobus, inicio, fim);
            _tool = new ToolTipInfo();
            // a partir de janeiro 2020 nao considera mais o KM
            //_tool.Body.Text = "Total Ocorrências / KM Rodado" + Environment.NewLine + _valorAvarias.ToString() + " / " + _valorKm.ToString();
            superToolTip1.SetToolTip(AvariasComCulpaRealLabel, _tool);
            if (_valorAvarias != 0)
                AvariasComCulpaRealLabel.DecimalValue = _valorAvarias;
            //AvariasComCulpaRealLabel.DecimalValue = _valorKm / _valorAvarias;

            decimal _valorAcidentes = new ValoresDasMetasNoGlobusBO().Acidentes(_empresa.CodigoEmpresaGlobus, inicio, fim);
            _tool = new ToolTipInfo();
            // a partir de janeiro 2020 nao considera mais o KM
            //_tool.Body.Text = "Total Ocorrências / KM Rodado" + Environment.NewLine + _valorAcidentes.ToString() + " / " + _valorKm.ToString();
            superToolTip1.SetToolTip(AcidentesComCulpaRealLabel, _tool);
            if (_valorAcidentes != 0)
                AcidentesComCulpaRealLabel.DecimalValue = _valorAcidentes;
            //AcidentesComCulpaRealLabel.DecimalValue = _valorKm / _valorAcidentes;

            decimal _valorFuncionariosAtivos = new ValoresDasMetasNoGlobusBO().FuncionariosAtivos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Absenteísmo Empresa");
            decimal _valorAbsenteismo = new ValoresDasMetasNoGlobusBO().Absenteismo(_empresa.CodigoEmpresaGlobus, inicio, fim, "Absenteísmo Empresa");
            _tool = new ToolTipInfo();
            _tool.Body.Text = "(Total Ocorrências / 30) / Funcionários Ativos" + Environment.NewLine + "(" + _valorAbsenteismo.ToString() + " / 30) / " + _valorFuncionariosAtivos.ToString();
            superToolTip1.SetToolTip(IndicesAbsenteismoRealLabel, _tool);
            if (_valorFuncionariosAtivos != 0)
                IndicesAbsenteismoRealLabel.DecimalValue = ((_valorAbsenteismo / 30) / _valorFuncionariosAtivos) * 100;

            _valorFuncionariosAtivos = new ValoresDasMetasNoGlobusBO().FuncionariosAtivos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Absenteísmo Operação");
            _valorAbsenteismo = new ValoresDasMetasNoGlobusBO().Absenteismo(_empresa.CodigoEmpresaGlobus, inicio, fim, "Absenteísmo Operação");
            _tool = new ToolTipInfo();
            _tool.Body.Text = "(Total Ocorrências / 30) / Funcionários Ativos" + Environment.NewLine + "(" + _valorAbsenteismo.ToString() + " / 30) / " + _valorFuncionariosAtivos.ToString();
            superToolTip1.SetToolTip(IndicesAbsenteismoOpRealLabel, _tool);
            if (_valorFuncionariosAtivos != 0)
                IndicesAbsenteismoOpRealLabel.DecimalValue = ((_valorAbsenteismo / 30) / _valorFuncionariosAtivos) * 100;

            _valorFuncionariosAtivos = new ValoresDasMetasNoGlobusBO().FuncionariosAtivos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Absenteísmo Manutenção");
            _valorAbsenteismo = new ValoresDasMetasNoGlobusBO().Absenteismo(_empresa.CodigoEmpresaGlobus, inicio, fim, "Absenteísmo Manutenção");
            _tool = new ToolTipInfo();
            _tool.Body.Text = "(Total Ocorrências / 30) / Funcionários Ativos" + Environment.NewLine + "(" + _valorAbsenteismo.ToString() + " / 30) / " + _valorFuncionariosAtivos.ToString();
            superToolTip1.SetToolTip(IndicesAbsenteismoManRealLabel, _tool);
            if (_valorFuncionariosAtivos != 0)
                IndicesAbsenteismoManRealLabel.DecimalValue = ((_valorAbsenteismo / 30) / _valorFuncionariosAtivos) * 100;

            _valorFuncionariosAtivos = new ValoresDasMetasNoGlobusBO().FuncionariosAtivos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Absenteísmo Administração");
            _valorAbsenteismo = new ValoresDasMetasNoGlobusBO().Absenteismo(_empresa.CodigoEmpresaGlobus, inicio, fim, "Absenteísmo Administração");
            _tool = new ToolTipInfo();
            _tool.Body.Text = "(Total Ocorrências / 30) / Funcionários Ativos" + Environment.NewLine + "(" + _valorAbsenteismo.ToString() + " / 30) / " + _valorFuncionariosAtivos.ToString();
            superToolTip1.SetToolTip(IndicesAbsenteismoAdmRealLabel, _tool);
            if (_valorFuncionariosAtivos != 0)
                IndicesAbsenteismoAdmRealLabel.DecimalValue = ((_valorAbsenteismo / 30) / _valorFuncionariosAtivos) * 100;

            _valorFuncionariosAtivos = new ValoresDasMetasNoGlobusBO().FuncionariosAtivos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Empresa", true);
            decimal _valorAdmitidos = new ValoresDasMetasNoGlobusBO().FuncionariosAdmitidos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Empresa");
            decimal _valorDemitidos = new ValoresDasMetasNoGlobusBO().FuncionariosDemitidos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Empresa");
            _tool = new ToolTipInfo();
            _tool.Body.Text = "(Total Admitidos + Total Demitidos / 2) / Funcionários Ativos" + Environment.NewLine + "(" + _valorAdmitidos + " + " + _valorDemitidos.ToString() + " / 2) / " + _valorFuncionariosAtivos.ToString();
            superToolTip1.SetToolTip(IndiceTurnoverRealLabel, _tool);
            if (_valorFuncionariosAtivos != 0)
                IndiceTurnoverRealLabel.DecimalValue = (((_valorDemitidos + _valorAdmitidos) / 2) / _valorFuncionariosAtivos) * 100;

            _valorFuncionariosAtivos = new ValoresDasMetasNoGlobusBO().FuncionariosAtivos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Operação", true);
            _valorAdmitidos = new ValoresDasMetasNoGlobusBO().FuncionariosAdmitidos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Operação");
            _valorDemitidos = new ValoresDasMetasNoGlobusBO().FuncionariosDemitidos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Operação");
            _tool = new ToolTipInfo();
            _tool.Body.Text = "(Total Admitidos + Total Demitidos / 2) / Funcionários Ativos" + Environment.NewLine + "(" + _valorAdmitidos + " + " + _valorDemitidos.ToString() + " / 2) / " + _valorFuncionariosAtivos.ToString();
            superToolTip1.SetToolTip(IndiceTurnoverOpRealLabel, _tool);
            if (_valorFuncionariosAtivos != 0)
                IndiceTurnoverOpRealLabel.DecimalValue = (((_valorDemitidos + _valorAdmitidos) / 2) / _valorFuncionariosAtivos) * 100;

            _valorFuncionariosAtivos = new ValoresDasMetasNoGlobusBO().FuncionariosAtivos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Manutenção", true);
            _valorAdmitidos = new ValoresDasMetasNoGlobusBO().FuncionariosAdmitidos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Manutenção");
            _valorDemitidos = new ValoresDasMetasNoGlobusBO().FuncionariosDemitidos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Manutenção");
            _tool = new ToolTipInfo();
            _tool.Body.Text = "(Total Admitidos + Total Demitidos / 2) / Funcionários Ativos" + Environment.NewLine + "(" + _valorAdmitidos + " + " + _valorDemitidos.ToString() + " / 2) / " + _valorFuncionariosAtivos.ToString();
            superToolTip1.SetToolTip(IndiceTurnoverManRealLabel, _tool);
            if (_valorFuncionariosAtivos != 0)
                IndiceTurnoverManRealLabel.DecimalValue = (((_valorDemitidos + _valorAdmitidos) / 2) / _valorFuncionariosAtivos) * 100;

            _valorFuncionariosAtivos = new ValoresDasMetasNoGlobusBO().FuncionariosAtivos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Administração", true);
            _valorAdmitidos = new ValoresDasMetasNoGlobusBO().FuncionariosAdmitidos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Administração");
            _valorDemitidos = new ValoresDasMetasNoGlobusBO().FuncionariosDemitidos(_empresa.CodigoEmpresaGlobus, inicio, fim, "Turnover Administração");
            _tool = new ToolTipInfo();
            _tool.Body.Text = "(Total Admitidos + Total Demitidos / 2) / Funcionários Ativos" + Environment.NewLine + "(" + _valorAdmitidos + " + " + _valorDemitidos.ToString() + " / 2) / " + _valorFuncionariosAtivos.ToString();
            superToolTip1.SetToolTip(IndiceTurnoverAdmRealLabel, _tool);
            if (_valorFuncionariosAtivos != 0)
                IndiceTurnoverAdmRealLabel.DecimalValue = (((_valorDemitidos + _valorAdmitidos) / 2) / _valorFuncionariosAtivos) * 100;

            BuscarPrevistoButton.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";
            this.Refresh();


            #region old - buscava o previsto - calculo automatico
            //bool _achou = false;
            //string _ultimoMes;
            //decimal _mediaMes1 = 0;
            //decimal _mediaMes2 = 0;
            //decimal _mediaMes3 = 0;
            //decimal _mediaMes4 = 0;
            //decimal _mediaMes5 = 0;

            //decimal _valorMes1 = 0;
            //decimal _valorMes2 = 0;
            //decimal _valorMes3 = 0;
            //decimal _valorMes4 = 0;
            //decimal _valorMes5 = 0;

            //decimal _media = 0;

            //if (_listaValoresMetas == null)
            //    _listaValoresMetas = new List<ValoresDasMetas>();

            //foreach (var item in _listaMetas)
            //{
            //    ValoresDasMetas _val = new ValoresDasMetas();
            //    _achou = false;

            //    foreach (var val in _listaValoresMetas.Where(w => w.IdMetas == item.Id))
            //    {
            //        _achou = true;
            //        _val = val;
            //        _listaValoresMetas.Remove(_val);
            //        break;
            //    }

            //    if (!_achou)
            //    {
            //        _val.IdMetas = item.Id;
            //        _val.IdEmpresa = _empresa.IdEmpresa;
            //        _val.Referencia = _referencia;
            //        _val.DiasUteis = (int)DiasUteisCurrencyTextBox.DecimalValue;
            //        _val.Descricao = item.Descricao;
            //    }

            //    #region Calculo por Mes Anterior
            //    if (item.PrevistoCalculaPor == "MA")
            //    {
            //        _ultimoMes = _listaValoresMetasMesAnterior.Where(w => w.Referencia != _referencia)
            //                                                  .Max(m => m.Referencia);

            //        foreach (var prev in _listaValoresMetasMesAnterior.Where(w => w.Referencia == _ultimoMes && w.IdMetas == item.Id))
            //        {
            //            if (item.UsarColunaPrevistoParaCalculo)
            //                _val.Previsto = prev.Previsto;
            //            else
            //                _val.Previsto = prev.Realizado;
            //        }
            //    }
            //    #endregion

            //    #region calculo por média
            //    if (item.PrevistoCalculaPor == "MD")
            //    {
            //        int qtd = 0;
            //        int dias = 0;

            //        _mediaMes1 = 0;
            //        _mediaMes2 = 0;
            //        _mediaMes3 = 0;
            //        _mediaMes4 = 0;
            //        _mediaMes5 = 0;

            //        _valorMes1 = 0;
            //        _valorMes2 = 0;
            //        _valorMes3 = 0;
            //        _valorMes4 = 0;
            //        _valorMes5 = 0;

            //        _media = 0;

            //        if (item.PrevistoQdtMeses <= 5)
            //        {
            //            _mesAux = _mes;
            //            while (qtd < item.PrevistoQdtMeses)
            //            {
            //                if (qtd == 0)
            //                    _mesAux = _mesAux - 2;
            //                else
            //                    _mesAux = _mesAux - 1;

            //                if (_mesAux < 1 || _mesAux > 12)
            //                {
            //                    _mesAux = 12 + _mesAux;
            //                    _referenciaInicio = (_ano - 1).ToString() + _mesAux.ToString("00");
            //                }
            //                else
            //                {
            //                    _referenciaInicio = _ano.ToString() + _mesAux.ToString("00");
            //                }

            //                foreach (var prev in _listaValoresMetasMesAnterior.Where(w => w.Referencia == _referenciaInicio && w.IdMetas == item.Id))
            //                {
            //                    if (item.PrevistoAplicaDiasUteis)
            //                        dias = prev.DiasUteis;
            //                    else
            //                        dias = Convert.ToInt32(Convert.ToDateTime("01/" + (_mesAux + 1).ToString("00") + "/" + _ano.ToString()).Subtract(Convert.ToDateTime("01/" + _mesAux.ToString("00") + "/" + _ano.ToString())).TotalDays);

            //                    switch (qtd)
            //                    {
            //                        case 0:
            //                            if (item.UsarColunaPrevistoParaCalculo)
            //                                _valorMes1 = prev.Previsto;
            //                            else
            //                                _valorMes1 = prev.Realizado;

            //                            _mediaMes1 = _valorMes1 / dias;
            //                            break;
            //                        case 1:
            //                            if (item.UsarColunaPrevistoParaCalculo)
            //                                _valorMes2 = prev.Previsto;
            //                            else
            //                                _valorMes2 = prev.Realizado;

            //                            _mediaMes2 = _valorMes2 / dias;
            //                            break;
            //                        case 2:
            //                            if (item.UsarColunaPrevistoParaCalculo)
            //                                _valorMes3 = prev.Previsto;
            //                            else
            //                                _valorMes3 = prev.Realizado;

            //                            _mediaMes3 = _valorMes3 / dias;
            //                            break;
            //                        case 3:
            //                            if (item.UsarColunaPrevistoParaCalculo)
            //                                _valorMes4 = prev.Previsto;
            //                            else
            //                                _valorMes4 = prev.Realizado;

            //                            _mediaMes4 = _valorMes4 / dias;
            //                            break;
            //                        case 4:
            //                            if (item.UsarColunaPrevistoParaCalculo)
            //                                _valorMes5 = prev.Previsto;
            //                            else
            //                                _valorMes5 = prev.Realizado;

            //                            _mediaMes5 = _valorMes5 / dias;
            //                            break;
            //                    }

            //                }

            //                qtd++;
            //            }

            //            if (item.PrevistoQdtMeses > 0)
            //            {
            //                _media = (_mediaMes1 + _mediaMes2 + _mediaMes3 + _mediaMes4 + _mediaMes5) / item.PrevistoQdtMeses;
            //                _media = _media * DiasUteisCurrencyTextBox.DecimalValue;

            //                _val.Previsto = _media;
            //            }
            //        }
            //    }
            //    #endregion

            //    _listaValoresMetas.Add(_val);
            //}

            //if (_listaValoresMetas.Count != 0)
            //{
            //    foreach (var item in _listaValoresMetas)
            //    {

            //        #region Financeiro
            //        if (item.Descricao.ToUpper().Contains("Ebitda".ToUpper()))
            //        {
            //            EbitdaPrevLabel.Tag = item.IdMetas;
            //            //EbitdaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Receita".ToUpper()) && item.Descricao.ToUpper().Contains("Líquida".ToUpper()))
            //        {
            //            ReceitaLiquidaPrevLabel.Tag = item.IdMetas;
            //            ReceitaLiquidaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Manutenção".ToUpper()) && item.Descricao.ToUpper().Contains("Frota".ToUpper()))
            //        {
            //            CustoManutencaoFrotaPrevLabel.Tag = item.IdMetas;
            //            CustoManutencaoFrotaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Outros".ToUpper()) && item.Descricao.ToUpper().Contains("Custos".ToUpper()))
            //        {
            //            OutrosCustosPrevLabel.Tag = item.IdMetas;
            //            OutrosCustosPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Bruta".ToUpper()) && item.Descricao.ToUpper().Contains("Receita".ToUpper()) && !item.Descricao.ToUpper().Contains("Total".ToUpper()))
            //        {
            //            ReceitaBruta2PrevLabel.Tag = item.IdMetas;
            //            ReceitaBruta2PrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            ReceitaBruta4PrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Subsídio".ToUpper()))
            //        {
            //            ReceitaSubsidioPrevLabel.Tag = item.IdMetas;
            //            ReceitaSubsidioPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            ReceitaSubsidio2PrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Folha".ToUpper()))
            //        {
            //            if (item.Descricao.ToUpper().Contains("Op".ToUpper()) && item.Descricao.ToUpper().Contains("Custo".ToUpper()))
            //            {
            //                CustoFolhaOp1PrevLabel.Tag = item.IdMetas;
            //                CustoFolhaOp1PrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //                CustoFolhaOp2PrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Adm".ToUpper()) && item.Descricao.ToUpper().Contains("Custo".ToUpper()))
            //            {
            //                CustoFolhaAdm1PrevLabel.Tag = item.IdMetas;
            //                CustoFolhaAdm1PrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //                CustoFolhaAdm2PrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Man".ToUpper()) && item.Descricao.ToUpper().Contains("Custo".ToUpper()))
            //            {
            //                CustoFolhaMan1PrevLabel.Tag = item.IdMetas;
            //                CustoFolhaMan1PrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //                CustoFolhaMan2PrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Man".ToUpper()) && item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
            //            {
            //                EficienciaFolhaManPrevLabel.Tag = item.IdMetas;
            //                EficienciaFolhaManPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Op".ToUpper()) && item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
            //            {
            //                EficienciaFolhaOpPrevLabel.Tag = item.IdMetas;
            //                EficienciaFolhaOpPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Adm".ToUpper()) && item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
            //            {
            //                EficienciaFolhaAdmPrevLabel.Tag = item.IdMetas;
            //                EficienciaFolhaAdmPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Custo".ToUpper()))
            //            {
            //                CustoFolhaTotalPrevLabel.Tag = item.IdMetas;
            //                CustoFolhaTotalPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Eficiência".ToUpper()))
            //            {
            //                EficienciaFolhaTotalPrevLabel.Tag = item.IdMetas;
            //                EficienciaFolhaTotalPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //        }

            //        if (item.Descricao.ToUpper().Contains("Eficiência de Man".ToUpper()))
            //        {
            //            EficienciaManutencaoFrotaPrevLabel.Tag = item.IdMetas;
            //            EficienciaManutencaoFrotaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Km".ToUpper()) && item.Descricao.ToUpper().Contains("Rodado".ToUpper()))
            //        {
            //            KmRodadoPrevLabel.Tag = item.IdMetas;
            //            KmRodadoPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Pneus".ToUpper()))
            //        {
            //            CustoPneuPrevLabel.Tag = item.IdMetas;
            //            CustoPneuPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Peças".ToUpper()))
            //        {
            //            CustoPecasPrevLabel.Tag = item.IdMetas;
            //            CustoPecasPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Hora".ToUpper()) && item.Descricao.ToUpper().Contains("Extra".ToUpper()))
            //        {
            //            if (item.Descricao.ToUpper().Contains("+".ToUpper()))
            //            {
            //                CustoHorasExtrasSomaPrevLabel.Tag = item.IdMetas;
            //                CustoHorasExtrasSomaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Op".ToUpper()))
            //            {
            //                CustoHorasExtrasOpPrevLabel.Tag = item.IdMetas;
            //                CustoHorasExtrasOpPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Man".ToUpper()))
            //            {
            //                CustoHorasExtrasManPrevLabel.Tag = item.IdMetas;
            //                CustoHorasExtrasManPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Adm".ToUpper()))
            //            {
            //                CustoHorasExtrasAdmPrevLabel.Tag = item.IdMetas;
            //                CustoHorasExtrasAdmPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            {
            //                CustoHorasExtrasPrevLabel.Tag = item.IdMetas;
            //                CustoHorasExtrasPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //        }

            //        if (item.Descricao.ToUpper().Contains("Multa".ToUpper()) && item.Descricao.ToUpper().Contains("Gestor".ToUpper()))
            //        {
            //            CustoMultasOrgaoGestorPrevLabel.Tag = item.IdMetas;
            //            CustoMultasOrgaoGestorPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Deduções".ToUpper()))
            //        {
            //            DeducoesReceitaPrevLabel.Tag = item.IdMetas;
            //            DeducoesReceitaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        #endregion

            //        #region Cliente
            //        if (item.Descricao.ToUpper().Contains("Limpeza".ToUpper()))
            //        {
            //            IndiceLimpezaFrotaPrevLabel.Tag = item.IdMetas;
            //            IndiceLimpezaFrotaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }
            //        if (item.Descricao.ToUpper().Contains("MKBF".ToUpper()))
            //        {
            //            MKBFPrevLabel.Tag = item.IdMetas;
            //            MKBFPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }
            //        if (item.Descricao.ToUpper().Contains("Reclamação".ToUpper()))
            //        {
            //            ReclamacaoPrevLabel.Tag = item.IdMetas;
            //            ReclamacaoPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }
            //        #endregion

            //        #region Processos internos
            //        if (item.Descricao.ToUpper().Contains("Avaria".ToUpper()))
            //        {
            //            AvariasComCulpaPrevLabel.Tag = item.IdMetas;
            //            AvariasComCulpaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Acidente".ToUpper()))
            //        {
            //            AcidentesComCulpaPrevLabel.Tag = item.IdMetas;
            //            AcidentesComCulpaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Cumprimento".ToUpper()))
            //        {
            //            CumprimentoPartidaPrevLabel.Tag = item.IdMetas;
            //            CumprimentoPartidaPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Carros".ToUpper()))
            //        {
            //            CarrosRetidosPrevLabel.Tag = item.IdMetas;
            //            CarrosRetidosPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Emergenciais".ToUpper()) || item.Descricao.ToUpper().Contains("Compra".ToUpper()) || item.Descricao.ToUpper().Contains("Emergênciais".ToUpper()))
            //        {
            //            ComprasEmergenciaisPrevLabel.Tag = item.IdMetas;
            //            ComprasEmergenciaisPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }
            //        #endregion

            //        #region Aprendizado e Crescimento
            //        if (item.Descricao.ToUpper().Contains("CNH".ToUpper()))
            //        {
            //            CNHVencidasPrevLabel.Tag = item.IdMetas;
            //            CNHVencidasPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Exames".ToUpper()))
            //        {
            //            ExamesVencidosPrevLabel.Tag = item.IdMetas;
            //            ExamesVencidosPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //        }

            //        if (item.Descricao.ToUpper().Contains("Absenteísmo".ToUpper()))
            //        {
            //            if (item.Descricao.ToUpper().Contains("Op".ToUpper()))
            //            {
            //                IndicesAbsenteismoOpPrevLabel.Tag = item.IdMetas;
            //                IndicesAbsenteismoOpPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Adm".ToUpper()))
            //            {
            //                IndicesAbsenteismoAdmPrevLabel.Tag = item.IdMetas;
            //                IndicesAbsenteismoAdmPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Man".ToUpper()))
            //            {
            //                IndicesAbsenteismoManPrevLabel.Tag = item.IdMetas;
            //                IndicesAbsenteismoManPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            {
            //                IndicesAbsenteismoPrevLabel.Tag = item.IdMetas;
            //                IndicesAbsenteismoPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //        }

            //        if (item.Descricao.ToUpper().Contains("Turnover".ToUpper()))
            //        {
            //            if (item.Descricao.ToUpper().Contains("Op".ToUpper()))
            //            {
            //                IndiceTurnoverOpPrevLabel.Tag = item.IdMetas;
            //                IndiceTurnoverOpPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Adm".ToUpper()))
            //            {
            //                IndiceTurnoverAdmPrevLabel.Tag = item.IdMetas;
            //                IndiceTurnoverAdmPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            if (item.Descricao.ToUpper().Contains("Man".ToUpper()))
            //            {
            //                IndiceTurnoverManPrevLabel.Tag = item.IdMetas;
            //                IndiceTurnoverManPrevLabel.Text = string.Format("{0:N}", item.Previsto);
            //            }
            //            else
            //            {
            //                IndiceTurnoverPrevLabel.Tag = item.IdMetas;
            //                IndiceTurnoverPrevLabel.Text = string.Format("{0:N}", item.Previsto);                            
            //            }
            //        }

            //        #endregion
            //    }
            //}
            //ReceitaLiquidaPrevLabel_DecimalValueChanged(sender, e);
            #endregion

        }

        private void DiasUteisCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (ReceitaBruta2PrevLabel.Enabled)
                    ReceitaBruta2PrevLabel.Focus();
                else
                    FocoCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void CumprimentoPanel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = ((Panel)sender).ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(Pens.DarkGray, rect);
        }

        private void label41_Click(object sender, EventArgs e)
        {
            //new Cadastros.Metas().ShowDialog();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            gravarButton.Cursor = Cursors.WaitCursor;
            mensagemSistemaLabel.Text = "Aguarde, Gravando...";
            this.Refresh();
            int idMetas = 0;
            bool _achou = false;
            decimal _previsto = 0;
            decimal _real = 0;
            ValoresDasMetas _val = new ValoresDasMetas();
            bool _infDataCorteFim = false;
            bool _infDataCorteOp = false;

            foreach (Control item in AprendizadoPanel.Controls)
            {
                if (item is Panel)
                {
                    idMetas = 0;
                    _achou = false;
                    _previsto = 0;
                    _real = 0;

                    foreach (Control itemP in item.Controls)
                    {
                        if (itemP is CurrencyTextBox && itemP.Enabled)
                        {
                            if (((CurrencyTextBox)itemP).Name.Contains("Prev") && !((CurrencyTextBox)itemP).Name.StartsWith("Prev"))
                            {
                                idMetas = Convert.ToInt32(((CurrencyTextBox)itemP).Tag);
                                _previsto = ((CurrencyTextBox)itemP).DecimalValue;
                            }
                            if (((CurrencyTextBox)itemP).Name.Contains("Real") && !((CurrencyTextBox)itemP).Name.StartsWith("Real"))
                                _real = ((CurrencyTextBox)itemP).DecimalValue;
                        }
                    }

                    foreach (var metas in _listaValoresMetas.Where(w => w.IdMetas == idMetas))
                    {
                        _achou = true;

                        if (!metas.Existe)
                        {
                            metas.PrevistoOriginal = _previsto;
                            metas.RealizadoOriginal = _real;
                        }
                        else
                        {
                            if (metas.PrevistoOriginal == 0)
                                metas.PrevistoOriginal = metas.Previsto;
                            if (metas.RealizadoOriginal == 0)
                                metas.RealizadoOriginal = metas.Realizado;
                        }

                        if (_real != 0 && _dataCorteOperacional == DateTime.MinValue && !_infDataCorteOp)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                                _infDataCorteOp = true;
                            else
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                    _dataCorteOperacional = Publicas._datasLembrete[0];
                            }
                        }

                        metas.DataCorteOperacional = _dataCorteOperacional;
                        metas.Previsto = _previsto;
                        metas.Realizado = _real;
                        break;
                    }

                    if (!_achou && idMetas != 0)
                    {
                        _val = new ValoresDasMetas();
                        _val.IdMetas = idMetas;
                        _val.IdEmpresa = _empresa.IdEmpresa;
                        _val.Referencia = _referencia;
                        _val.DiasUteis = (int)DiasUteisCurrencyTextBox.DecimalValue;

                        foreach (var metas in _listaMetas.Where(w => w.Id == idMetas))
                        {
                            _val.Descricao = metas.Descricao;
                        }

                        _val.Previsto = _previsto;
                        _val.Realizado = _real;
                        _val.PrevistoOriginal = _previsto;
                        _val.RealizadoOriginal = _real;

                        _listaValoresMetas.Add(_val);
                    }
                }
            }
        
            foreach (Control item in ClientePanel.Controls)
            {
                if (item is Panel)
                {
                    idMetas = 0;
                    _achou = false;
                    _previsto = 0;
                    _real = 0;

                    foreach (Control itemP in item.Controls)
                    {
                        if (itemP is CurrencyTextBox && itemP.Enabled)
                        {
                            if (((CurrencyTextBox)itemP).Name.Contains("Prev") && !((CurrencyTextBox)itemP).Name.StartsWith("Prev"))
                            {
                                idMetas = Convert.ToInt32(((CurrencyTextBox)itemP).Tag);
                                _previsto = ((CurrencyTextBox)itemP).DecimalValue;
                            }
                            if (((CurrencyTextBox)itemP).Name.Contains("Real") && !((CurrencyTextBox)itemP).Name.StartsWith("Real"))
                                _real = ((CurrencyTextBox)itemP).DecimalValue;
                        }
                    }

                    foreach (var metas in _listaValoresMetas.Where(w => w.IdMetas == idMetas))
                    {
                        _achou = true;

                        if (!metas.Existe)
                        {
                            metas.PrevistoOriginal = _previsto;
                            metas.RealizadoOriginal = _real;
                        }
                        else
                        {
                            if (metas.PrevistoOriginal == 0)
                                metas.PrevistoOriginal = metas.Previsto;
                            if (metas.RealizadoOriginal == 0)
                                metas.RealizadoOriginal = metas.Realizado;
                        }

                        if (_real != 0 && _dataCorteOperacional == DateTime.MinValue && !_infDataCorteOp)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                                _infDataCorteOp = true;
                            else
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                    _dataCorteOperacional = Publicas._datasLembrete[0];
                            }
                        }

                        metas.DataCorteOperacional = _dataCorteOperacional;
                        metas.Previsto = _previsto;
                        metas.Realizado = _real;
                        break;
                    }

                    if (!_achou && idMetas != 0)
                    {
                        _val = new ValoresDasMetas();
                        _val.IdMetas = idMetas;
                        _val.IdEmpresa = _empresa.IdEmpresa;
                        _val.Referencia = _referencia;
                        _val.DiasUteis = (int)DiasUteisCurrencyTextBox.DecimalValue;

                        foreach (var metas in _listaMetas.Where(w => w.Id == idMetas))
                        {
                            _val.Descricao = metas.Descricao;
                        }

                        _val.Previsto = _previsto;
                        _val.Realizado = _real;
                        _val.PrevistoOriginal = _previsto;
                        _val.RealizadoOriginal = _real;

                        _listaValoresMetas.Add(_val);
                    }
                }
            }

            foreach (Control item in ProcessosInternosPanel.Controls)
            {
                if (item is Panel)
                {
                    idMetas = 0;
                    _achou = false;
                    _previsto = 0;
                    _real = 0;

                    foreach (Control itemP in item.Controls)
                    {
                        if (itemP is CurrencyTextBox && itemP.Enabled)
                        {
                            if (((CurrencyTextBox)itemP).Name.Contains("Prev") && !((CurrencyTextBox)itemP).Name.StartsWith("Prev"))
                            {
                                idMetas = Convert.ToInt32(((CurrencyTextBox)itemP).Tag);
                                _previsto = ((CurrencyTextBox)itemP).DecimalValue;
                            }
                            if (((CurrencyTextBox)itemP).Name.Contains("Real") && !((CurrencyTextBox)itemP).Name.StartsWith("Real"))
                                _real = ((CurrencyTextBox)itemP).DecimalValue;
                        }
                    }

                    foreach (var metas in _listaValoresMetas.Where(w => w.IdMetas == idMetas))
                    {
                        _achou = true;

                        if (!metas.Existe)
                        {
                            metas.PrevistoOriginal = _previsto;
                            metas.RealizadoOriginal = _real;
                        }
                        else
                        {
                            if (metas.PrevistoOriginal == 0)
                                metas.PrevistoOriginal = metas.Previsto;
                            if (metas.RealizadoOriginal == 0)
                                metas.RealizadoOriginal = metas.Realizado;
                        }

                        if (_real != 0 && _dataCorteOperacional == DateTime.MinValue && !_infDataCorteOp)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Operacional ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                                _infDataCorteOp = true;
                            else
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Operacional";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                    _dataCorteOperacional = Publicas._datasLembrete[0];
                            }
                        }

                        metas.DataCorteOperacional = _dataCorteOperacional;
                        metas.Previsto = _previsto;
                        metas.Realizado = _real;
                        break;
                    }

                    if (!_achou && idMetas != 0)
                    {
                        _val = new ValoresDasMetas();
                        _val.IdMetas = idMetas;
                        _val.IdEmpresa = _empresa.IdEmpresa;
                        _val.Referencia = _referencia;
                        _val.DiasUteis = (int)DiasUteisCurrencyTextBox.DecimalValue;

                        foreach (var metas in _listaMetas.Where(w => w.Id == idMetas))
                        {
                            _val.Descricao = metas.Descricao;
                        }

                        _val.Previsto = _previsto;
                        _val.Realizado = _real;
                        _val.PrevistoOriginal = _previsto;
                        _val.RealizadoOriginal = _real;

                        _listaValoresMetas.Add(_val);
                    }
                }
            }

            foreach (Control item in FinanceiroPanel.Controls)
            {
                if (item is Panel)
                {
                    idMetas = 0;
                    _achou = false;
                    _previsto = 0;
                    _real = 0;

                    foreach (Control itemP in item.Controls)
                    {
                        if (itemP is CurrencyTextBox)
                        {
                            if (Convert.ToInt32(((CurrencyTextBox)itemP).Tag) != 0)
                            {
                                if (((CurrencyTextBox)itemP).Name.Contains("Prev") && !((CurrencyTextBox)itemP).Name.StartsWith("Prev"))
                                {
                                    idMetas = Convert.ToInt32(((CurrencyTextBox)itemP).Tag);
                                    _previsto = ((CurrencyTextBox)itemP).DecimalValue;
                                }
                                if (((CurrencyTextBox)itemP).Name.Contains("Real") && !((CurrencyTextBox)itemP).Name.StartsWith("Real"))
                                    _real = ((CurrencyTextBox)itemP).DecimalValue;
                            }
                        }
                    }

                    foreach (var metas in _listaValoresMetas.Where(w => w.IdMetas == idMetas))
                    {
                        _achou = true;

                        if (!metas.Existe)
                        {
                            metas.PrevistoOriginal = _previsto;
                            metas.RealizadoOriginal = _real;
                        }
                        else
                        {
                            if (metas.PrevistoOriginal == 0)
                                metas.PrevistoOriginal = metas.Previsto;
                            if (metas.RealizadoOriginal == 0)
                                metas.RealizadoOriginal = metas.Realizado;
                        }

                        metas.Previsto = _previsto;
                        metas.Realizado = _real;
                        if (_real != 0 && _dataCorteFinanceiro == DateTime.MinValue && !_infDataCorteFim)
                        {
                            if (new Notificacoes.Mensagem("Deseja informar a Data de Corte do Resultado Financeiro ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                                _infDataCorteFim = true;
                            else
                            {
                                GestaoEstrategica.DataDeCorteResultadoBSC _tela = new DataDeCorteResultadoBSC();
                                _tela.DataDeCorteLabel.Text = "Escolha a data de Corte do Resultado Financeiro";
                                _tela.ShowDialog();

                                if (Publicas._datasLembrete != null)
                                    _dataCorteFinanceiro = Publicas._datasLembrete[0];
                            }
                        }
                        metas.DataCorteFinanceiro = _dataCorteFinanceiro;
                        break;
                    }

                    if (!_achou && idMetas != 0)
                    {
                        _val = new ValoresDasMetas();

                        _val.IdMetas = idMetas;
                        _val.IdEmpresa = _empresa.IdEmpresa;
                        _val.Referencia = _referencia;
                        _val.DiasUteis = (int)DiasUteisCurrencyTextBox.DecimalValue;

                        foreach (var metas in _listaMetas.Where(w => w.Id == idMetas))
                        {
                            _val.Descricao = metas.Descricao;
                        }

                        _val.Previsto = _previsto;
                        _val.Realizado = _real;
                        _val.PrevistoOriginal = _previsto;
                        _val.RealizadoOriginal = _real;

                        _listaValoresMetas.Add(_val);
                    }
                    
                }
            }

            _listaValoresMetas.ForEach(u => {
                u.DiasCorridos = (int)DiasCorridosCurrencyTextBox.DecimalValue;
                u.DiasUteis = (int)DiasUteisCurrencyTextBox.DecimalValue;
                u.DataCorteFinanceiro = _dataCorteFinanceiro;
                u.DataCorteOperacional = _dataCorteOperacional;
            });

            if (!new ValoresDasMetasNoGlobusBO().Gravar(_listaValoresMetas, _listaItensAvaliacao, referenciaMaskedEditBox.ClipText))
            {
                gravarButton.Cursor = Cursors.Default;
                mensagemSistemaLabel.Text = "";
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            // Não pode ter valor de realizado
            bool _gravouParaoAno = false;

            if (referenciaMaskedEditBox.ClipText.Substring(0, 2) == "01" && _listaValoresMetas.Sum(s => s.Realizado) == 0 && _listaValoresMetas.Sum(s => s.Previsto) != 0)
            {
                _listaValoresMetasMesAnterior = new MetasBO().Listar(false, _empresa.IdEmpresa, 
                    referenciaMaskedEditBox.ClipText.Substring(2, 4) + "02", referenciaMaskedEditBox.ClipText.Substring(2, 4) + "12",0);

                if (_listaValoresMetasMesAnterior.Count() == 0)
                {
                    if (new Notificacoes.Mensagem("Deseja Replicar o Previsto para o ano inteiro?." , Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                    {
                        _gravouParaoAno = true;
                        int _mes = 2;
                        int _ano = Convert.ToInt32(referenciaMaskedEditBox.ClipText.Substring(2, 4));
                        while (_mes <= 12)
                        {
                            if (_mes == 12)
                            {
                                _diasCorridos = Convert.ToInt32(Convert.ToDateTime("01/01/" + (_ano + 1).ToString()).Subtract(Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString())).TotalDays);
                                //_diasUteis = new FeriadoBO().DiasUteis(_empresa.IdEmpresa, Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString()), Convert.ToDateTime("01/01/" + (_ano + 1).ToString()).AddDays(-1));
                            }
                            else
                            {
                                _diasCorridos = Convert.ToInt32(Convert.ToDateTime("01/" + (_mes + 1).ToString("00") + "/" + _ano.ToString()).Subtract(Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString())).TotalDays);
                                //_diasUteis = new FeriadoBO().DiasUteis(_empresa.IdEmpresa, Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString()), Convert.ToDateTime("01/" + (_mes + 1).ToString("00") + "/" + _ano.ToString()).AddDays(-1));
                            }

                            DiasCorridosCurrencyTextBox.DecimalValue = _diasCorridos;
                            //DiasUteisCurrencyTextBox.DecimalValue = _diasUteis;

                            _listaValoresMetas.ForEach(u =>
                            {
                                u.Existe = false;
                                u.DiasCorridos = (int)DiasCorridosCurrencyTextBox.DecimalValue;
                                u.DiasUteis = (int)DiasUteisCurrencyTextBox.DecimalValue;
                                u.Referencia = _ano + _mes.ToString("00");
                            });

                            if (!new ValoresDasMetasNoGlobusBO().Gravar(_listaValoresMetas, _listaItensAvaliacao, _ano + _mes.ToString("00")))
                            {
                                gravarButton.Cursor = Cursors.Default;
                                mensagemSistemaLabel.Text = "";
                                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                            _mes++;
                        }
                    }
                }
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou valores das metas da empresa " + empresaComboBoxAdv.Text + " periodo " + referenciaMaskedEditBox.Text +
                (_buscouRealizadoFinanceiro ? " Gravou os realizados financeiro buscados do Globus" : "") +
                (_buscouRealizadoOperacional ? " Gravou os realizados operacionais buscados do Globus" : "") +
                (_gravouParaoAno ? " Gravou o previsto para todos os meses do ano" : "");

            foreach (var itemL in _listaValoresMetasLog.OrderBy(o => o.IdMetas))
            {
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == itemL.IdMetas))
                {
                    if (itemL.Previsto != item.Previsto || itemL.Realizado != item.Realizado)
                        _log.Descricao = _log.Descricao + " [ " + itemL.Metas;

                    if (itemL.Previsto != item.Previsto)
                        _log.Descricao = _log.Descricao +  " Previsto de " + itemL.Previsto.ToString() + " para " + item.Previsto.ToString() ;

                    if (itemL.Realizado != item.Realizado)
                        _log.Descricao = _log.Descricao + " Realizado de " + itemL.Realizado.ToString() + " para " + item.Realizado.ToString();

                    if (itemL.Previsto != item.Previsto || itemL.Realizado != item.Realizado)
                        _log.Descricao = _log.Descricao + " ]";
                }
            }

            _log.Tela = "Indicadores de Desempenho";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            gravarButton.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";
            limparButton_Click(sender, e);
            this.Refresh();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new ValoresDasMetasNoGlobusBO().Excluir(_listaValoresMetas))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu valores das metas da empresa " + empresaComboBoxAdv.Text + " periodo " + referenciaMaskedEditBox.Text;
            _log.Tela = "Indicadores de Desempenho";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void ContratoDeMetasButton_Click(object sender, EventArgs e)
        {
            Avaliacao_de_desempenho.DefinicaoDeMetas _tela = new Avaliacao_de_desempenho.DefinicaoDeMetas();
            _tela.tituloLabel.Text = "Contrato de Metas";

            _tela.empresaComboBoxAdv.Enabled = false;
            _tela.usuarioTextBox.Enabled = false;
            _tela.referenciaMaskedEditBox.Text = this.referenciaMaskedEditBox.ClipText;
            _tela._empresa = this._empresa;
            _tela.ShowInTaskbar = true;
            _tela.Show();

        }

        private void verMesesAnterioresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_listaValoresMetasMesAnterior == null)
                return;

            int idMeta = 0;
            int top = 0;
            int left = 0;
            int largura = 0;
            
            Label labelChamador = contextMenuStrip1.SourceControl as Label;

            panelChamador = labelChamador.Parent as Panel;

            idMeta = Convert.ToInt16(panelChamador.Tag);
            top = panelChamador.Top;
            left = panelChamador.Left;
            largura = panelChamador.Width;
            parente = panelChamador.Parent;
            _alturaOriginal = parente.Height;
            _topOriginalCabProcessos = CabecalhoProcessoInternoPanel.Top;
            _topOriginalProcessos = ProcessosInternosPanel.Top;
            _topOriginalCabAprendizado = CabecalhoAprendizadoPanel.Top;
            _topOriginalAprendizado = AprendizadoPanel.Top;

            gridGroupingControl1.DataSource = _listaValoresMetasMesAnterior.Where(w => w.IdMetas == idMeta).OrderBy(o => o.Referencia).ToList();
            MesesAnterioresLabel.Text = labelChamador.Text;
            if (panelChamador.Name.Contains("Subsidio") || panelChamador.Name.Contains("ReceitaBruta2"))
                MesesAnteriorsPanel.Left = left - 5 - MesesAnteriorsPanel.Width;
            else
                MesesAnteriorsPanel.Left = left + largura + 5;

            if (panelChamador.Name.Contains("Multa") || panelChamador.Name.Contains("Pecas") || panelChamador.Name.Contains("Pneu") ||
                panelChamador.Name.Contains("Km") || panelChamador.Name.Contains("Extra"))
                MesesAnteriorsPanel.Top = (top + panelChamador.Height) - MesesAnteriorsPanel.Height;
            else
            if (panelChamador.Name.Contains("Limpeza") || panelChamador.Name.Contains("Mkbf") || panelChamador.Name.Contains("Reclamacao"))
            {
                parente = parente.Parent;
                MesesAnteriorsPanel.Top = CabecalhoClientePanel.Top;
                
                this.Refresh();
            }
            else
            if (panelChamador.Name.Contains("Avarias") || panelChamador.Name.Contains("Acidente") || panelChamador.Name.Contains("Cumprimento") ||
                panelChamador.Name.Contains("Retidos") || panelChamador.Name.Contains("Compras") || 
                panelChamador.Name.Contains("Partida") || panelChamador.Name.Contains("Pontualidade"))
            {
                parente = parente.Parent;
                MesesAnteriorsPanel.Top = CabecalhoProcessoInternoPanel.Top;
            }
            if (panelChamador.Name.Contains("Exames") || panelChamador.Name.Contains("CNH"))
                MesesAnteriorsPanel.Top = (top + panelChamador.Height) - MesesAnteriorsPanel.Height;
            else
            if (panelChamador.Name.Contains("Turnover") )
                MesesAnteriorsPanel.Top = AbsenteismoPanel.Top;
            else
                MesesAnteriorsPanel.Top = top;

            MesesAnteriorsPanel.Parent = parente;
            MesesAnteriorsPanel.BringToFront();
            MesesAnteriorsPanel.Visible = true;
        }

        private void tituloMinimizarLabel_Click(object sender, EventArgs e)
        {
            MesesAnteriorsPanel.Visible = false;
        }

        private void tituloMinimizarLabel_MouseHover(object sender, EventArgs e)
        {
            tituloMinimizarLabel.Text = "T";
        }

        private void tituloMinimizarLabel_MouseLeave(object sender, EventArgs e)
        {
            tituloMinimizarLabel.Text = "S";
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            if (panelChamador.Name.Contains("EficienciaFolha"))
                e.Style.Format = "N3";
            else
            if (panelChamador.Name.Contains("EficienciaMan"))
                e.Style.Format = "N4";
            else
            if (panelChamador.Name.Contains("Retidos") || panelChamador.Name.Contains("MKBF") ||
                panelChamador.Name.Contains("CNH") || panelChamador.Name.Contains("Exames") || panelChamador.Name.Contains("Reclamação"))
            {
                e.Style.Format = "N0";
            }
            else
                e.Style.Format = "n2";
        }

        private void gráficoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_listaValoresMetasMesAnterior == null)
                return;

            int idMeta = 0;
            Label labelChamador = contextMenuStrip1.SourceControl as Label;

            panelChamador = labelChamador.Parent as Panel;
            idMeta = Convert.ToInt32(panelChamador.Tag);

            GestaoEstrategica.Graficos _Tela = new Graficos();
            _Tela._indicador = idMeta;
            _Tela._empresaGrafico = _empresa;
            _Tela._ano = Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4));
            _Tela.Size = new Size(this.Width, this.Height);
            _Tela.ShowDialog();
        }

        private void FinanceiroPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GestaoDeMetas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_bscEmEdicao != null)
                new MetasBO().Excluir(_bscEmEdicao);
        }

        private void PartidaPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    PartidaRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AcidentesComCulpaRealLabel.Focus();
            }
        }

        private void PartidaRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    CarrosRetidosPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PartidaPrevLabel.Focus();
            }
        }

        private void PontualidadeRealLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    IndicesAbsenteismoPrevLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PontualidadePrevLabel.Focus();
            }
        }

        private void PontualidadePrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MotivoRealPanel.Visible)
                    MotivoRealTextBox.Focus();
                else
                    PontualidadeRealLabel.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ComprasEmergenciaisRealLabel.Focus();
            }
        }

        private void PartidaPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                PartidaPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(PartidaPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (PartidaPrevLabel.DecimalValue != 0)
                        valor = (PartidaRealLabel.DecimalValue / PartidaPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (PartidaRealLabel.DecimalValue != 0)
                            valor = (PartidaPrevLabel.DecimalValue / PartidaRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (PartidaPrevLabel.DecimalValue == PartidaRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - PartidaRealLabel.DecimalValue);
                    }
                }

                //if (PartidaRealLabel.DecimalValue != 0)
                //   valor = (PartidaPrevLabel.DecimalValue / PartidaRealLabel.DecimalValue) * 100;

                if (valor >= 99 || (PartidaPrevLabel.DecimalValue == 0 && PartidaRealLabel.DecimalValue == 0)
                    || (PartidaPrevLabel.DecimalValue > 0 && PartidaRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    PartidaPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 99)
                    PartidaPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    PartidaPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void PontualidadePrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                PontualidadePictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(PontualidadePrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (PontualidadePrevLabel.DecimalValue != 0)
                        valor = (PontualidadeRealLabel.DecimalValue / PontualidadePrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (PontualidadeRealLabel.DecimalValue != 0)
                            valor = (PontualidadePrevLabel.DecimalValue / PontualidadeRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (PontualidadePrevLabel.DecimalValue == PontualidadeRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - PontualidadeRealLabel.DecimalValue);
                    }
                }
                // if (PontualidadeRealLabel.DecimalValue != 0)
                //   valor = (PontualidadePrevLabel.DecimalValue / PontualidadeRealLabel.DecimalValue) * 100;

                if (valor >= 100 || (PontualidadePrevLabel.DecimalValue == 0 && PontualidadeRealLabel.DecimalValue == 0)
                    || (PontualidadePrevLabel.DecimalValue > 0 && PontualidadeRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    PontualidadePictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    PontualidadePictureBox.ImageLocation = _arquivoAmarelo;
                else
                    PontualidadePictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void ClientePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void MKBFRodoviarioPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void MKBFRodoviarioPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                MKBFRodoviarioPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(MKBFRodoviarioPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (MKBFRodoviarioPrevLabel.DecimalValue != 0)
                        valor = (MKBFRodoviarioRealLabel.DecimalValue / MKBFRodoviarioPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (MKBFRodoviarioRealLabel.DecimalValue != 0)
                            valor = (MKBFRodoviarioPrevLabel.DecimalValue / MKBFRodoviarioRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (MKBFRodoviarioPrevLabel.DecimalValue == MKBFRodoviarioRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - MKBFRodoviarioRealLabel.DecimalValue);
                    }
                }

                if (valor >= 100 || (MKBFRodoviarioPrevLabel.DecimalValue == 0 && MKBFRodoviarioRealLabel.DecimalValue == 0)
                    || (MKBFRodoviarioPrevLabel.DecimalValue > 0 && MKBFRodoviarioRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    MKBFRodoviarioPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    MKBFRodoviarioPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    MKBFRodoviarioPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void MKBFUrbanoPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                MKBFUrbanoPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(MKBFUrbanoPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (MKBFUrbanoPrevLabel.DecimalValue != 0)
                        valor = (MKBFUrbanoRealLabel.DecimalValue / MKBFUrbanoPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (MKBFUrbanoRealLabel.DecimalValue != 0)
                            valor = (MKBFUrbanoPrevLabel.DecimalValue / MKBFUrbanoRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (MKBFUrbanoPrevLabel.DecimalValue == MKBFUrbanoRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - MKBFUrbanoRealLabel.DecimalValue);
                    }
                }

                if (valor >= 100 || (MKBFUrbanoPrevLabel.DecimalValue == 0 && MKBFUrbanoRealLabel.DecimalValue == 0)
                    || (MKBFUrbanoPrevLabel.DecimalValue > 0 && MKBFUrbanoRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    MKBFUrbanoPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    MKBFUrbanoPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    MKBFUrbanoPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void MKBFSubUrbanoPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                MKBFSuburbanoPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(MKBFSubUrbanoPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (MKBFSubUrbanoPrevLabel.DecimalValue != 0)
                        valor = (MKBFSubUrbanoRealLabel.DecimalValue / MKBFSubUrbanoPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (MKBFSubUrbanoRealLabel.DecimalValue != 0)
                            valor = (MKBFSubUrbanoPrevLabel.DecimalValue / MKBFSubUrbanoRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (MKBFSubUrbanoPrevLabel.DecimalValue == MKBFSubUrbanoRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - MKBFSubUrbanoRealLabel.DecimalValue);
                    }
                }

                if (valor >= 100 || (MKBFSubUrbanoPrevLabel.DecimalValue == 0 && MKBFSubUrbanoRealLabel.DecimalValue == 0)
                    || (MKBFSubUrbanoPrevLabel.DecimalValue > 0 && MKBFSubUrbanoRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    MKBFSuburbanoPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    MKBFSuburbanoPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    MKBFSuburbanoPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void MKBFMunicipalPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                MKBFMunicipalPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(MKBFMunicipalPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (MKBFMunicipalPrevLabel.DecimalValue != 0)
                        valor = (MKBFMunicipalRealLabel.DecimalValue / MKBFMunicipalPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (MKBFMunicipalRealLabel.DecimalValue != 0)
                            valor = (MKBFMunicipalPrevLabel.DecimalValue / MKBFMunicipalRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (MKBFMunicipalPrevLabel.DecimalValue == MKBFMunicipalRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - MKBFMunicipalRealLabel.DecimalValue);
                    }
                }

                if (valor >= 100 || (MKBFMunicipalPrevLabel.DecimalValue == 0 && MKBFMunicipalRealLabel.DecimalValue == 0)
                    || (MKBFMunicipalPrevLabel.DecimalValue > 0 && MKBFMunicipalRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    MKBFMunicipalPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    MKBFMunicipalPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    MKBFMunicipalPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void MKBFIntermunicipalPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                MKBFIntermunicipalPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(MKBFIntermunicipalPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (MKBFIntermunicipalPrevLabel.DecimalValue != 0)
                        valor = (MKBFIntermunicipalRealLabel.DecimalValue / MKBFIntermunicipalPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (MKBFIntermunicipalRealLabel.DecimalValue != 0)
                            valor = (MKBFIntermunicipalPrevLabel.DecimalValue / MKBFIntermunicipalRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (MKBFIntermunicipalPrevLabel.DecimalValue == MKBFIntermunicipalRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - MKBFIntermunicipalRealLabel.DecimalValue);
                    }
                }

                if (valor >= 100 || (MKBFIntermunicipalPrevLabel.DecimalValue == 0 && MKBFIntermunicipalRealLabel.DecimalValue == 0)
                    || (MKBFIntermunicipalPrevLabel.DecimalValue > 0 && MKBFIntermunicipalRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    MKBFIntermunicipalPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    MKBFIntermunicipalPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    MKBFIntermunicipalPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void MKBFEscolarPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                MKBFEscolarPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(MKBFEscolarPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (MKBFEscolarPrevLabel.DecimalValue != 0)
                        valor = (MKBFEscolarRealLabel.DecimalValue / MKBFEscolarPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (MKBFEscolarRealLabel.DecimalValue != 0)
                            valor = (MKBFEscolarPrevLabel.DecimalValue / MKBFEscolarRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (MKBFEscolarPrevLabel.DecimalValue == MKBFEscolarRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - MKBFEscolarRealLabel.DecimalValue);
                    }
                }

                if (valor >= 100 || (MKBFEscolarPrevLabel.DecimalValue == 0 && MKBFEscolarRealLabel.DecimalValue == 0)
                    || (MKBFEscolarPrevLabel.DecimalValue > 0 && MKBFEscolarRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    MKBFEscolarPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    MKBFEscolarPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    MKBFEscolarPictureBox.ImageLocation = _arquivoVermelho;
            }
        }

        private void MKBFFretamentoPrevLabel_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal valor = 0;

            if (gravarButton.Text.Contains("Gravar") || _dataCorteOperacional == DateTime.MinValue)
                MKBFFretamentoPictureBox.ImageLocation = _arquivoCinza;
            else
            {
                Classes.Metas _metas = new MetasBO().Consultar(Convert.ToInt32(MKBFFretamentoPrevLabel.Tag));

                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                {
                    if (MKBFFretamentoPrevLabel.DecimalValue != 0)
                        valor = (MKBFFretamentoRealLabel.DecimalValue / MKBFFretamentoPrevLabel.DecimalValue) * 100;
                }
                else
                {
                    if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                    {
                        if (MKBFFretamentoRealLabel.DecimalValue != 0)
                            valor = (MKBFFretamentoPrevLabel.DecimalValue / MKBFFretamentoRealLabel.DecimalValue) * 100;
                    }
                    else
                    {
                        if (MKBFFretamentoPrevLabel.DecimalValue == MKBFFretamentoRealLabel.DecimalValue)
                            valor = 100;
                        else
                            valor = (100 - MKBFFretamentoRealLabel.DecimalValue);
                    }
                }

                if (valor >= 100 || (MKBFFretamentoPrevLabel.DecimalValue == 0 && MKBFFretamentoRealLabel.DecimalValue == 0)
                    || (MKBFFretamentoPrevLabel.DecimalValue > 0 && MKBFFretamentoRealLabel.DecimalValue == 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                    MKBFFretamentoPictureBox.ImageLocation = _arquivoVerde;
                else
                if (valor >= 98 && valor < 100)
                    MKBFFretamentoPictureBox.ImageLocation = _arquivoAmarelo;
                else
                    MKBFFretamentoPictureBox.ImageLocation = _arquivoVermelho;
            }
        }
    }
}
