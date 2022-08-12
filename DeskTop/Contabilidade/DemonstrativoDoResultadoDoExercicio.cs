using Classes;
using Negocio;
using Syncfusion.Windows.Forms;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace Suportte.Contabilidade
{
    public partial class DemonstrativoDoResultadoDoExercicio : Form
    {
        public DemonstrativoDoResultadoDoExercicio()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);

                    if (Publicas._TemaBlack)
                    {
                     /*   gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                        gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                        gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                        gridGroupingControl1.BackColor = Publicas._panelTitulo;*/

                    }
                }
                //referenciaMaskedEditBox.ThemeName = "";

                //referenciaMaskedEditBox.Metrocolor = Publicas._bordaSaida;
                referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;
                referenciaMaskedEditBox.UseBorderColorOnFocus = false;

                referenciaMaskedEditBox.BackColor = empresaComboBoxAdv.BackColor;
                referenciaMaskedEditBox.ForeColor = empresaComboBoxAdv.ForeColor;

                DissidioCurrencyText.DecimalValue = 0;
                DissidioCurrencyText.Tag = null;
                DissidioCurrencyText.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                DissidioCurrencyText.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                DissidioCurrencyText.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                DissidioCurrencyText.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                DissidioCurrencyText.BackGroundColor = Publicas._fundo;

                if (Publicas._TemaBlack)
                {

                    label5.ForeColor = Color.Black;
                    label20.ForeColor = Color.Black;
                    label24.ForeColor = Color.Black;
                    label21.ForeColor = Color.Black;
                    label22.ForeColor = Color.Black;
                    label31.ForeColor = Color.Black;
                    label98.ForeColor = Color.Black;
                    label202.ForeColor = Color.Black;
                    label90.ForeColor = Color.Black;
                    label97.ForeColor = Color.Black;
                    label133.ForeColor = Color.Black;
                    label175.ForeColor = Color.Black;
                    label155.ForeColor = Color.Black;
                    label171.ForeColor = Color.Black;
                    label185.ForeColor = Color.Black;
                    label201.ForeColor = Color.Black;
                    label236.ForeColor = Color.Black;
                    label295.ForeColor = Color.Black;
                    label294.ForeColor = Color.Black;


                    label353.ForeColor = Color.Black;
                    label327.ForeColor = Color.Black;
                    label334.ForeColor = Color.Black;
                    label326.ForeColor = Color.Black;
                    label400.ForeColor = Color.Black;
                    label380.ForeColor = Color.Black;
                    label393.ForeColor = Color.Black;
                    label376.ForeColor = Color.Black;
                    label413.ForeColor = Color.Black;

                    //    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    //    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    //    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    //    gridGroupingControl1.BackColor = Publicas._panelTitulo;
                }
            }

            foreach (Control item in DadosPanel.Controls)
            {
                if (item is Panel)
                {
                    if (((Panel)item).Name != "DadosPanel" && ((Panel)item).Name != "ReceitaLiquidaPanel")
                        ((Panel)item).Paint += new System.Windows.Forms.PaintEventHandler(this.ReceitaLiquidaPanel_Paint);

                    foreach (Control itemP in item.Controls)
                    {

                        if (itemP is PictureBox)
                        {
                            ((PictureBox)itemP).BackColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkOliveGreen);
                            ((PictureBox)itemP).Width = 2;

                        }

                        if (itemP is CurrencyTextBox)
                        {
                            if ((((CurrencyTextBox)itemP).ReadOnly))
                            {
                                ((CurrencyTextBox)itemP).ReadOnly = false;
                                ((CurrencyTextBox)itemP).Enabled = false;
                            }

                            if (!Publicas._usuario.Desenvolvedor)
                            {
                                if (((CurrencyTextBox)itemP).Name.Contains("Real"))
                                    ((CurrencyTextBox)itemP).Enabled = !Publicas._usuario.ApenasConsultaDRE;
                                else
                                if (((CurrencyTextBox)itemP).Name.Contains("Prev"))
                                {
                                    if (Publicas._usuario.ApenasConsultaDRE && Publicas._usuario.ApenasEditarPrevistoDRE)
                                        ((CurrencyTextBox)itemP).Enabled = true;
                                    else
                                        ((CurrencyTextBox)itemP).Enabled = !Publicas._usuario.ApenasConsultaDRE;
                                }
                            }

                            ((CurrencyTextBox)itemP).DecimalValue = 0;
                            ((CurrencyTextBox)itemP).Tag = null;
                            ((CurrencyTextBox)itemP).PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                            ((CurrencyTextBox)itemP).ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                            ((CurrencyTextBox)itemP).BackGroundColor = Publicas._fundo;
                            ((CurrencyTextBox)itemP).MinValue = (decimal)-999999999.99;
                            ((CurrencyTextBox)itemP).MaxValue = (decimal)999999999.99;

                            if (((CurrencyTextBox)itemP).Name != "ReceitaLiquidaPanel")
                            {
                                ((CurrencyTextBox)itemP).Enter += new System.EventHandler(this.ReceitaLiquidaPrevLabel_Enter);
                                ((CurrencyTextBox)itemP).Validating += new System.ComponentModel.CancelEventHandler(this.ReceitaLiquidaPrevLabel_Validating);
                                ((CurrencyTextBox)itemP).KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReceitaLiquidaPrevLabel_KeyDown);
                            }
                        }
                    }
                }

            }

            Publicas._mensagemSistema = string.Empty;
        }

        string _referencia;
        int _mes;
        int _ano;
        int _diasCorridos;
        //int _diasUteis;
        int idMetasGrava = 0;
        string arquivoImportado = "";

        DateTime _dataInicio;
        DateTime _dataFim;

        Classes.Empresa _empresa;
        Classes.Metas _metas;
        Classes.DRE _dre;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        List<Classes.Metas> _listaMetas;
        List<Classes.ValoresDasMetas> _listaValoresMetas;
        List<Classes.ValoresDasMetas> _listaValoresMetasLog;
        Classes.ValoresDasMetas _valoresMetas;
        Classes.BSCEmEdicao _bscEmEdicao;

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

        private void ReceitaLiquidaPanel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = ((Panel)sender).ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle((Publicas._TemaBlack ? Pens.Black : Pens.DarkOliveGreen), rect);
        }

        private void DemonstrativoDoResultadoDoExercicio_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);
            #region coloca os botões centralizados
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { gravarButton, limparButton, excluirButton, AtualizarButton };
            _botoes = Publicas.CentralizarBotoes(_botoes, this.Width, limparButton.Left - (gravarButton.Left + gravarButton.Width));

            for (int i = 0; i < _botoes.Count(); i++)
            {
                if (i == 0)
                    gravarButton.Left = _botoes[i].Left;
                if (i == 1)
                    limparButton.Left = _botoes[i].Left;
                if (i == 2)
                    excluirButton.Left = _botoes[i].Left;
                if (i == 3)
                    AtualizarButton.Left = _botoes[i].Left;
            }
            #endregion

            #region Empresas autorizadas para o colaborador
            _listaEmpresas = new EmpresaBO().Listar(false);

            _empresaDoColaborador = new ColaboradoresBO().Listar(Publicas._idColaborador);

            if (!Publicas._usuario.ApenasConsultaDRE || Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.IdEmpresa == 19)
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
            #endregion

            #region Popula Id nos campos
            _listaMetas = new MetasBO().Listar(true);

            foreach (var item in _listaMetas.Where(w => w.ExibeNoDRE))
            {

                if (item.Descricao.ToUpper().StartsWith("Qtde Passageiros de Gratuidade".ToUpper()))
                {
                    QtdePassaseirosGratuidadePanel.Tag = item.Id;
                    QtdePassaseirosGratuidadePrevLabel.Tag = item.Id;
                    QtdePassaseirosGratuidadeRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("Qtde Passageiros Pagantes".ToUpper()))
                {
                    QtdePassaseirosPagantesPanel.Tag = item.Id;
                    QtdePassaseirosPagantesPrevLabel.Tag = item.Id;
                    QtdePassaseirosPagantesRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("Qtde Passageiros Integrações Sem valor".ToUpper()))
                {
                    QtdePassaseirosIntegracoesSValorPanel.Tag = item.Id;
                    QtdePassaseirosIntegracoesSValorPrevLabel.Tag = item.Id;
                    QtdePassaseirosIntegracoesSValorRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("KM Rodado".ToUpper()))
                {
                    KmRodadoPanel.Tag = item.Id;
                    KmRodadoPrevLabel.Tag = item.Id;
                    KmRodadoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("% Ebitida".ToUpper()))
                {
                    PercEbitda.Tag = item.Id;
                    PercEbitdaPrevLabel.Tag = item.Id;
                    PercEbitdaRealLabel.Tag = item.Id;
                }
                if (item.Descricao.ToUpper().Contains("Desoneração".ToUpper()))
                {
                    DesoneracaoPanel.Tag = item.Id;
                    DesoneracaoPrevLabel.Tag = item.Id;
                    DesoneracaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Equipamentos de Proteção".ToUpper()))
                {
                    EquipamentosDeProtecaoPanel.Tag = item.Id;
                    EquipamentosDeProtecaoPrevLabel.Tag = item.Id;
                    EquipamentosDeProtecaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Manutenção de Software e Programas".ToUpper()))
                {
                    ManutencaoSoftwarePanel.Tag = item.Id;
                    ManutencaoSoftwarePrevLabel.Tag = item.Id;
                    ManutencaoSoftwareRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Despesas com Informática".ToUpper()))
                {
                    ServicosTIPanel.Tag = item.Id;
                    ServicosTIPrevLabel.Tag = item.Id;
                    ServicosTIRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Despesas com Informática".ToUpper()))
                {
                    ServicosTIPanel.Tag = item.Id;
                    ServicosTIPrevLabel.Tag = item.Id;
                    ServicosTIRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Festas e Eventos, Copa e Cozinha".ToUpper()))
                {
                    FestasPanel.Tag = item.Id;
                    FestasPrevLabel.Tag = item.Id;
                    FestasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Outras Despesas Indedutiveis".ToUpper()))
                {
                    OutrasDespesasIndedutiveisPanel.Tag = item.Id;
                    OutrasDespesasIndedutiveisPrevLabel.Tag = item.Id;
                    OutrasDespesasIndedutiveisRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Taxas e Impostos".ToUpper()))
                {
                    TaxasImpostosPanel.Tag = item.Id;
                    TaxasImpostosPrevLabel.Tag = item.Id;
                    TaxasImpostosRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Contribuições Sindicais".ToUpper()))
                {
                    ContribuicoesSindicaisPanel.Tag = item.Id;
                    ContribuicoesSindicaisPrevLabel.Tag = item.Id;
                    ContribuicoesSindicaisRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Indenizações e Recuperações".ToUpper()))
                {
                    IndenizacoesPanel.Tag = item.Id;
                    IndenizacoesPrevLabel.Tag = item.Id;
                    IndenizacoesRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Multas de Transitos Totalizador".ToUpper()))
                {
                    MultasDeTransitoPanel.Tag = item.Id;
                    MultasDeTransitoPrevLabel.Tag = item.Id;
                    MultasDeTransitoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Multa de Transito Frota de Apoio".ToUpper()))
                {
                    MultasDeTransitoApoioPanel.Tag = item.Id;
                    MultasDeTransitoApoioPrevLabel.Tag = item.Id;
                    MultasDeTransitoApoioRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Multas de Transito Frota Operacional".ToUpper()))
                {
                    MultasDeTransitoFrotaOpPanel.Tag = item.Id;
                    MultasDeTransitoFrotaOpPrevLabel.Tag = item.Id;
                    MultasDeTransitoFrotaOpRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Diárias, lanches, Pedágios e Outros".ToUpper()))
                {
                    DespesasDiariasLanchesPanel.Tag = item.Id;
                    DespesasDiariasLanchesPrevLabel.Tag = item.Id;
                    DespesasDiariasLanchesRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Estruturas Externas".ToUpper()))
                {
                    DespesasEstruturasExternasPanel.Tag = item.Id;
                    DespesasEstruturasExternasPrevLabel.Tag = item.Id;
                    DespesasEstruturasExternasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Locação de Bens Móveis".ToUpper()))
                {
                    LocacaoBensMoveisPanel.Tag = item.Id;
                    LocacaoBensMoveisPrevLabel.Tag = item.Id;
                    LocacaoBensMoveisRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Manutenção Software e Programas Operação".ToUpper()))
                {
                    SoftwareProgramasPanel.Tag = item.Id;
                    SoftwareProgramasPrevLabel.Tag = item.Id;
                    SoftwareProgramasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Litros Consumidos".ToUpper()))
                {
                    LitrosConsumidosPanel.Tag = item.Id;
                    LitrosConsumidosPrevLabel.Tag = item.Id;
                    LitrosConsumidosRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Ebitda".ToUpper()))
                {
                    EbitdaFPanel.Tag = item.Id;
                    EbitdaPrevLabel.Tag = item.Id;
                    EbitdaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Receita".ToUpper()) && item.Descricao.ToUpper().Contains("Líquida".ToUpper()))
                {
                    ReceitaLiquidaPanel.Tag = item.Id;
                    ReceitaLiquidaPrevLabel.Tag = item.Id;
                    ReceitaLiquidaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Deduções".ToUpper()) && item.Descricao.ToUpper().Contains("Receita".ToUpper()))
                {
                    DeducoesReceitaPanel.Tag = item.Id;
                    DeducoesReceitaPrevLabel.Tag = item.Id;
                    DeducoesReceitaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Custo Manutenção".ToUpper()) && item.Descricao.ToUpper().Contains("Frota".ToUpper()))
                {
                    CustoManutencaoFrotaPanel.Tag = item.Id;
                    CustoManutencaoFrotaPrevLabel.Tag = item.Id;
                    CustoManutencaoFrotaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Bruta".ToUpper()) && item.Descricao.ToUpper().Contains("Receita".ToUpper()) && item.Descricao.ToUpper().Contains("Total".ToUpper()))
                {
                    ReceitaBrutaTotalPanel.Tag = item.Id;
                    ReceitaBrutaTotalPrevLabel.Tag = item.Id;
                    ReceitaBrutaTotalRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Bruta".ToUpper()) && item.Descricao.ToUpper().Contains("Receita".ToUpper()))
                {
                    ReceitaBrutaPanel.Tag = item.Id;
                    ReceitaBrutaPrevLabel.Tag = item.Id;
                    ReceitaBrutaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Receitas".ToUpper()))
                {
                    ReceitasPanel.Tag = item.Id;
                    ReceitasPrevLabel.Tag = item.Id;
                    ReceitasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Outras Receitas".ToUpper()))
                {
                    OutrasReceitasPanel.Tag = item.Id;
                    OutrasReceitasPrevLabel.Tag = item.Id;
                    OutrasReceitasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Subsídio".ToUpper()))
                {
                    ReceitaSubsidioPanel.Tag = item.Id;
                    ReceitaSubsidioPrevLabel.Tag = item.Id;
                    ReceitaSubsidioRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Impostos".ToUpper()) && item.Descricao.ToUpper().Contains("Receita".ToUpper()))
                {
                    ImpostoSobreReceitaPanel.Tag = item.Id;
                    ImpostosPrevLabel.Tag = item.Id;
                    ImpostosRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Consórcios".ToUpper()) && item.Descricao.ToUpper().Contains("Consórcio".ToUpper()))
                {
                    OrgaoGestorConsorciosPanel.Tag = item.Id;
                    OrgaoGestorConsorcioPrevLabel.Tag = item.Id;
                    OrgaoGestorConsorcioRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("EMTU".ToUpper()) && item.Descricao.ToUpper().Contains("Internorte".ToUpper()))
                {
                    EMTCPanel.Tag = item.Id;
                    EMTCPrevLabel.Tag = item.Id;
                    EMTCRealRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Transurc".ToUpper()))
                {
                    TransurcPanel.Tag = item.Id;
                    TransurcPrevLabel.Tag = item.Id;
                    TransurcRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Prourbano".ToUpper()))
                {
                    TranserpPanel.Tag = item.Id;
                    TranserpPrevLabel.Tag = item.Id;
                    TranserpRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("PDF".ToUpper()))
                {
                    PdfPanel.Tag = item.Id;
                    PdfPrevLabel.Tag = item.Id;
                    PdfRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Folha".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    FolhaOperacaoPanel.Tag = item.Id;
                    FolhaOpPrevLabel.Tag = item.Id;
                    FolhaOpRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Folha".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    FolhaManutencaoPanel.Tag = item.Id;
                    FolhaManutencaoPrevLabel.Tag = item.Id;
                    FolhaManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Folha".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    CustoFolhaAdministracaoPanel.Tag = item.Id;
                    CustoFolhaAdmPrevLabel.Tag = item.Id;
                    CustoFolhaAdmRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("Custo Fixo".ToUpper()) && item.Descricao.ToUpper().Contains("Indireto".ToUpper()))
                {
                    CustoFixoIndiretoPanel.Tag = item.Id;
                    CustoFixoIndiretoPrevLabel.Tag = item.Id;
                    CustoFixoIndiretoRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().StartsWith("Custo Fixo".ToUpper()) && item.Descricao.ToUpper().Contains("Direto".ToUpper()))
                {
                    CustosFixosDiretoPanel.Tag = item.Id;
                    CustosFixosDiretoPrevLabel.Tag = item.Id;
                    CustosFixosDiretoRealLabel.Tag = item.Id;
                }


                if (item.Descricao.ToUpper().Contains("Serviços Prestados".ToUpper()))
                {
                    CustosTotalServicosPanel.Tag = item.Id;
                    CustosTotalServicosPrevLabel.Tag = item.Id;
                    CustosTotalServicosRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Margem".ToUpper()) && item.Descricao.ToUpper().Contains("Contribuição".ToUpper()))
                {
                    MargemContribuicaoPanel.Tag = item.Id;
                    MargemContribuicaoPrevLabel.Tag = item.Id;
                    MargemContribuicaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Salários".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    SalarioOperacionalPanel.Tag = item.Id;
                    SalarioOperacionalPrevLabel.Tag = item.Id;
                    SalarioOperacionalRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Salários".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    SalarioManutencaoPanel.Tag = item.Id;
                    SalarioManutencaoPrevLabel.Tag = item.Id;
                    SalarioManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Salários".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    SalarioAdministracaoPanel.Tag = item.Id;
                    SalarioAdministracaoPrevLabel.Tag = item.Id;
                    SalarioAdministracaoRealLabel.Tag = item.Id;
                }


                if (item.Descricao.ToUpper().StartsWith("1/3 Férias".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    Ferias13OperacionalPanel.Tag = item.Id;
                    Ferias13OperacionalPrevLabel.Tag = item.Id;
                    Ferias13OperacionalRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("1/3 Férias".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    Ferias13ManutencaoPanel.Tag = item.Id;
                    Ferias13ManutencaoPrevLabel.Tag = item.Id;
                    Ferias13ManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("1/3 Férias".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    Ferias13AdministracaoPanel.Tag = item.Id;
                    Ferias13AdministracaoPrevLabel.Tag = item.Id;
                    Ferias13AdministracaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("Férias".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    FeriasOperacionalPanel.Tag = item.Id;
                    FeriasOperacionalPrevLabel.Tag = item.Id;
                    FeriasOperacionalRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("Férias".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    FeriasManutencaoPanel.Tag = item.Id;
                    FeriasManutencaoPrevLabel.Tag = item.Id;
                    FeriasManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("Férias".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    FeriasAdministracaoPanel.Tag = item.Id;
                    FeriasAdministracaoPrevLabel.Tag = item.Id;
                    FeriasAdministracaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Custo Hora Extra".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    HorasExtrasOperacionaisPanel.Tag = item.Id;
                    HorasExtrasOperacionaisPrevLabel.Tag = item.Id;
                    HorasExtrasOperacionaisRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Custo Hora Extra".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    HorasExtrasManutencaoPanel.Tag = item.Id;
                    HorasExtrasManutencaoPrevLabel.Tag = item.Id;
                    HorasExtrasManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Custo Hora Extra".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    HorasExtrasAdministracaoPanel.Tag = item.Id;
                    HorasExtrasAdministracaoPrevLabel.Tag = item.Id;
                    HorasExtrasAdministracaoRealLabel.Tag = item.Id;
                }


                if (item.Descricao.ToUpper().Contains("Minutos Horas Extras".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    HorasExtrasOpMinutosPanel.Tag = item.Id;
                    HorasExtrasOpMinutosPrevLabel.Tag = item.Id;
                    HorasExtrasOpMinutosRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Minutos Horas Extras".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    HorasExtrasManMinutosPanel.Tag = item.Id;
                    HorasExtrasManMinutosPrevLabel.Tag = item.Id;
                    HorasExtrasManMinutosRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Minutos Horas Extras".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    HorasExtrasAdmMinutosPanel.Tag = item.Id;
                    HorasExtrasAdmMinutosPrevLabel.Tag = item.Id;
                    HorasExtrasAdmMinutosRealLabel.Tag = item.Id;
                }


                if (item.Descricao.ToUpper().Contains("Encargos".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    EncargosOperacionalPanel.Tag = item.Id;
                    EncargosOperacionalPrevLabel.Tag = item.Id;
                    EncargosOperacionalRealPrev.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Encargos".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    EncargosManutencaoPanel.Tag = item.Id;
                    EncargosManutencaoPrevLabel.Tag = item.Id;
                    EncargosManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Encargos".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    EncargosAdministracaoPanel.Tag = item.Id;
                    EncargosAdministracaoPrevLabel.Tag = item.Id;
                    EncargosAdministracaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Plr".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    PlrOperacaoPanel.Tag = item.Id;
                    PlrOperacaoPrevLabel.Tag = item.Id;
                    PlrOperacaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Plr".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    PLRManutencaopanel.Tag = item.Id;
                    PLRManutencaoPrevLabel.Tag = item.Id;
                    PLRManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Plr".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    PLRAdministracaoPanel.Tag = item.Id;
                    PLRAdministracaoPrevLabel.Tag = item.Id;
                    PLRAdministracaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Comissões".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    ComissaoOperacionalPanel.Tag = item.Id;
                    ComissaoOperacionalPrevLabel.Tag = item.Id;
                    ComissaoOperacionalRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Comissões".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    ComisssaoManutencaoPanel.Tag = item.Id;
                    ComisssaoManutencaoPrevLabel.Tag = item.Id;
                    ComisssaoManutencaoRealPrev.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Comissões".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    ComissoesAdministracaoPanel.Tag = item.Id;
                    ComissoesAdministracaoPrevLabel.Tag = item.Id;
                    ComissoesAdministracaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Prêmio".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    PremioOperacionalPanel.Tag = item.Id;
                    PremioOperacionalPrevLabel.Tag = item.Id;
                    PremioOperacionalRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Prêmio".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    PremioManutencaoPanel.Tag = item.Id;
                    PremioManutencaoPrevLabel.Tag = item.Id;
                    PremioManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Prêmio".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    PremioAdministracaoPanel.Tag = item.Id;
                    PremioAdministracaoPrevLabel.Tag = item.Id;
                    PremioAdministracaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Outras Remunerações".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    OutrasRemuneracoesOperacionalPanel.Tag = item.Id;
                    OutrasRemuneracoesOperacionalPrevLabel.Tag = item.Id;
                    OutrasRemuneracoesOperacionalRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Outras Remunerações".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    OutrasRemuneracoesManutencaoPanel.Tag = item.Id;
                    OutrasRemuneracoesManutencaoPrevLabel.Tag = item.Id;
                    OutrasRemuneracoesManutencaoRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Outras Remunerações".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    OutrasRemuneracoesAdministracaoPanel.Tag = item.Id;
                    OutrasRemuneracoesAdministracaoPrevLabel.Tag = item.Id;
                    OutrasRemuneracoesAdministracaoRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Equals("Outros pagamentos".ToUpper()))
                {
                    OutrosAdmPanel.Tag = item.Id;
                    OutrosAdmPrevLabel.Tag = item.Id;
                    OutrosAdmRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Pro-labore".ToUpper()))
                {
                    ProLaborePanel.Tag = item.Id;
                    ProLaborePrevLabel.Tag = item.Id;
                    ProLaboreRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Benefícios".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    BeneficiosOperacionalPanel.Tag = item.Id;
                    BeneficiosOperacionalPrevLabel.Tag = item.Id;
                    BeneficiosOperacionalRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Benefícios".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    BeneficiosManutencaoPanel.Tag = item.Id;
                    BeneficiosManutencaoPrevLabel.Tag = item.Id;
                    BeneficiosManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Benefícios".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    BeneficiosAdministracaoPanel.Tag = item.Id;
                    BeneficiosAdministracaoPrevLabel.Tag = item.Id;
                    BeneficiosAdministracaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Demissões".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    DemissoesOperacionalPanel.Tag = item.Id;
                    DemissoesOperacionalPrevLabel.Tag = item.Id;
                    DemissoesOperacionalRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Demissões".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    DemissoesManutencaoPanel.Tag = item.Id;
                    DemissoesManutencaoPrevLabel.Tag = item.Id;
                    DemissoesManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Demissões".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    DemissoesAdministracaoPanel.Tag = item.Id;
                    DemissoesAdministracaoPrevLabel.Tag = item.Id;
                    DemissoesAdministracaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Provisões".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    ProvisaoOperacionalPanel.Tag = item.Id;
                    ProvisaoOperacionalPrevLabel.Tag = item.Id;
                    ProvisaoOperacionalRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Provisões".ToUpper()) && item.Descricao.ToUpper().Contains("Manutenção".ToUpper()))
                {
                    ProvisaoManutencaoPanel.Tag = item.Id;
                    ProvisaoManutencaoPrevLabel.Tag = item.Id;
                    ProvisaoManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Provisões".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    ProvisaoAdministracaoPanel.Tag = item.Id;
                    ProvisaoAdministracaoPrevLabel.Tag = item.Id;
                    ProvisaoAdministracaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Outras Despesas Operacionais do Custo Fixo Indireto".ToUpper()))
                {
                    OutrasDespesasOperacionaisPanel.Tag = item.Id;
                    OutrasDespesasOperacionaisPrevLabel.Tag = item.Id;
                    OutrasDespesasOperacionaisRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Equals("Outros pagamentos".ToUpper()))
                {
                    OutrosAdmPanel.Tag = item.Id;
                    OutrosAdmPrevLabel.Tag = item.Id;
                    OutrosAdmRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Outras Remunerações".ToUpper()) && item.Descricao.ToUpper().Contains("Operação".ToUpper()))
                {
                    OutrasRemuneracoesOperacionalPanel.Tag = item.Id;
                    OutrasRemuneracoesOperacionalPrevLabel.Tag = item.Id;
                    OutrasRemuneracoesOperacionalRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Outras Despesas".ToUpper()) && item.Descricao.ToUpper().Contains("Pessoal".ToUpper()) && item.Descricao.ToUpper().Contains("Indireto".ToUpper()))
                {
                    OutrasDespPessoalManutencaoPanel.Tag = item.Id;
                    OutrasDespPessoalManutencaoPrevLabel.Tag = item.Id;
                    OutrasDespPessoalManutencaoRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Outras Despesas".ToUpper()) && item.Descricao.ToUpper().Contains("Direto".ToUpper()))
                {
                    OutrasDespPessoalFixoDiretoPanel.Tag = item.Id;
                    OutrasDespPessoalFixoDiretoPrevLabel.Tag = item.Id;
                    OutrasDespPessoalFixoDiretoRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Outras Despesas".ToUpper()) && item.Descricao.ToUpper().Contains("Pessoal".ToUpper()))
                {
                    OutrasDespPessoalAdmPanel.Tag = item.Id;
                    OutrasDespPessoalAdmPrevLabel.Tag = item.Id;
                    OutrasDespPessoalAdmRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Outras Remunerações".ToUpper()) && item.Descricao.ToUpper().Contains("Administração".ToUpper()))
                {
                    OutrasRemuneracoesAdministracaoPanel.Tag = item.Id;
                    OutrasRemuneracoesAdministracaoPrevLabel.Tag = item.Id;
                    OutrasRemuneracoesAdministracaoRealLabel.Tag = item.Id;
                }
                else
                if (item.Descricao.ToUpper().Contains("Outras Despesas na Frota".ToUpper()))
                {
                    OutrasDespesasFrotaPanel.Tag = item.Id;
                    OutrasDespesasFrotaPrevLabel.Tag = item.Id;
                    OutrasDespesasFrotaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Pro-labore".ToUpper()))
                {
                    ProLaborePanel.Tag = item.Id;
                    ProLaborePrevLabel.Tag = item.Id;
                    ProLaborePrevLabel.Tag = item.Id;
                    ProLaborePrevLabel.Tag = item.Id;
                    ProLaboreRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Total Gestão".ToUpper()) && item.Descricao.ToUpper().Contains("Frota".ToUpper()))
                {
                    TotalGestaoPanel.Tag = item.Id;
                    TotalGestaoPrevLabel.Tag = item.Id;
                    TotalGestaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Despesas".ToUpper()) && item.Descricao.ToUpper().Contains("Licenciamento".ToUpper()) && item.Descricao.ToUpper().Contains("Gestão".ToUpper()))
                {
                    DespesasLicenciamentoGestaoPanel.Tag = item.Id;
                    DespesasLicenciamentoGestaoPrevLabel.Tag = item.Id;
                    DespesasLicenciamentoGestaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Despesas com a".ToUpper()) && item.Descricao.ToUpper().Contains("Frota".ToUpper()))
                {
                    DespesasComFrotaPanel.Tag = item.Id;
                    DespesasComFrotaPrevLabel.Tag = item.Id;
                    DespesasComFrotaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Seguros".ToUpper()) && item.Descricao.ToUpper().Contains("Frota".ToUpper()))
                {
                    SegurosVeiculoGestaoPanel.Tag = item.Id;
                    SegurosVeiculoGestaoPrevLabel.Tag = item.Id;
                    SegurosVeiculoGestaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("Frota de Apoio Totalizador".ToUpper()))
                {
                    FrotaDeApoioPanel.Tag = item.Id;
                    FrotaDeApoioPrevLabel.Tag = item.Id;
                    FrotaDeApoioRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Manutenção da Frota".ToUpper()) && item.Descricao.ToUpper().Contains("Apoio".ToUpper()))
                {
                    CustosManutencaoPanel.Tag = item.Id;
                    CustosManutencaoPrevLabel.Tag = item.Id;
                    CustosManutencaoRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Licenciamento".ToUpper()) && item.Descricao.ToUpper().Contains("Apoio".ToUpper()))
                {
                    LicenciamentoFrotaApoioPanel.Tag = item.Id;
                    LicenciamentoFrotaApoioPrevLabel.Tag = item.Id;
                    LicenciamentoFrotaApoioRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Equipamentos e Ferramentas".ToUpper()))
                {
                    DespesasEquipFerramentasPanel.Tag = item.Id;
                    DespesasEquipFerramentasPrevLabel.Tag = item.Id;
                    DespesasEquipFerramentasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Ferramentas Grupo".ToUpper()))
                {
                    DespesasFerramentasPanel.Tag = item.Id;
                    DespesasFerramentasPrevLabel.Tag = item.Id;
                    DespesasFerramentasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Maquinas".ToUpper()) && item.Descricao.ToUpper().Contains("Pequeno".ToUpper()))
                {
                    MaquinasPanel.Tag = item.Id;
                    MaquinasPrevLabel.Tag = item.Id;
                    MaquinasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Desp".ToUpper()) && item.Descricao.ToUpper().Contains("Manut".ToUpper()) && item.Descricao.ToUpper().Contains("Equipamentos".ToUpper()))
                {
                    DespesasManutencaoEquipPanel.Tag = item.Id;
                    DespesasManutencaoEquipPrevLabel.Tag = item.Id;
                    DespesasManutencaoEquipRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Desp".ToUpper()) && item.Descricao.ToUpper().Contains("Manut".ToUpper()) && item.Descricao.ToUpper().Contains("Ferramenta".ToUpper()))
                {
                    DespesasManutencaoFerramentasPanel.Tag = item.Id;
                    DespesasManutencaoFerramentasPrevLabel.Tag = item.Id;
                    DespesasManutencaoFerramentasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Peças Novas e Recondicionadas".ToUpper()))
                {
                    CustosPecasPanel.Tag = item.Id;
                    CustoPecasPrevLabel.Tag = item.Id;
                    CustoPecasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Peças Novas".ToUpper()) && item.Descricao.ToUpper().Contains("Acessórios".ToUpper()))
                {
                    PecasNovasPanel.Tag = item.Id;
                    PecasNovasPrevLabel.Tag = item.Id;
                    PecasNovasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Peças Recondicionadas".ToUpper()) && item.Descricao.ToUpper().Contains("TErceiros".ToUpper()))
                {
                    PecasRecondicionadasPanel.Tag = item.Id;
                    PecasRecondicionadasPrevLabel.Tag = item.Id;
                    PecasRecondicionadasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Custo de Pneus Novos e Recapados".ToUpper()))
                {
                    CustosPneusPanel.Tag = item.Id;
                    CustoPneusPrevLabel.Tag = item.Id;
                    CustoPneusRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Pneus Novos".ToUpper()))
                {
                    PneusNovosPanel.Tag = item.Id;
                    PneusNovosPrevLabel.Tag = item.Id;
                    PneusNovosRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Pneus Recapados".ToUpper()))
                {
                    PneusRecapadosPanel.Tag = item.Id;
                    PneusRecapadosPrevLabel.Tag = item.Id;
                    PneusRecapadosRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Óleo Diesel".ToUpper()))
                {
                    OleoDieselPanel.Tag = item.Id;
                    OleoDieselPrevLabel.Tag = item.Id;
                    OleoDieselRealLabel.Tag = item.Id;
                }
                if (item.Descricao.ToUpper().Equals("Credito ICMS sobre Óleo Diesel".ToUpper()))
                {
                    CreditoICMSPanel.Tag = item.Id;
                    CreditoICMSPrevLabel.Tag = item.Id;
                    CreditoICMSRealLabel.Tag = item.Id;
                }
                if (item.Descricao.ToUpper().Equals("Lubrificantes".ToUpper()))
                {
                    LubrificantesPanel.Tag = item.Id;
                    LubrificantesPrevLabel.Tag = item.Id;
                    LubrificantesRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Total Óleo Diesel e Lubrificantes".ToUpper()))
                {
                    CustoOleoDieselPanel.Tag = item.Id;
                    CustoOleoDieselPrevLabel.Tag = item.Id;
                    CustoOleoDieselRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Limpeza".ToUpper()) && item.Descricao.ToUpper().Contains("Frota".ToUpper()))
                {
                    MaterialLimpezaFrotaPanel.Tag = item.Id;
                    MaterialLimpezaFrotaPrevLabel.Tag = item.Id;
                    MaterialLimpezaFrotaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().StartsWith("Total Despesas".ToUpper()))
                {
                    TotalDespesasPanel.Tag = item.Id;
                    TotalDespesasPrevLabel.Tag = item.Id;
                    TotalDespesasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Processos".ToUpper()) && item.Descricao.ToUpper().Contains("Jurídicos".ToUpper()))
                {
                    ProcessoJuridicosPanel.Tag = item.Id;
                    ProcessoJuridicosPrevLabel.Tag = item.Id;
                    ProcessoJuridicosRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Despesas".ToUpper()) && item.Descricao.ToUpper().Contains("Administrativas".ToUpper()))
                {
                    DespesasAdministrativasPanel.Tag = item.Id;
                    DespesasAdministrativasPrevLabel.Tag = item.Id;
                    DespesasAdministrativasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Limpeza".ToUpper()) && item.Descricao.ToUpper().Contains("Predial".ToUpper()))
                {
                    LimpezaPredialPanel.Tag = item.Id;
                    LimpezaPredialPrevLabel.Tag = item.Id;
                    LimpezaPredialRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Manutenção".ToUpper()) && item.Descricao.ToUpper().Contains("PRedial".ToUpper()))
                {
                    ManutencaoPredialPanel.Tag = item.Id;
                    ManutencaoPredialPrevLabel.Tag = item.Id;
                    ManutencaoPredialRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Despesa".ToUpper()) && item.Descricao.ToUpper().Contains("Segurança".ToUpper()))
                {
                    DespesasSegurancaPanel.Tag = item.Id;
                    DespesasSegurancaPrevLabel.Tag = item.Id;
                    DespesasSegurancaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Telefone".ToUpper()) && item.Descricao.ToUpper().Contains("Eletrica".ToUpper()))
                {
                    TelefonePanel.Tag = item.Id;
                    TelefonePrevLabel.Tag = item.Id;
                    TelefoneRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Despesas com TI".ToUpper()))
                {
                    DespesasComTIPanel.Tag = item.Id;
                    DespesasComTIPrevLabel.Tag = item.Id;
                    DespesasComTIRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Manutenção".ToUpper()) && item.Descricao.ToUpper().Contains("Informática".ToUpper()))
                {
                    ManutencaoEquipInformaticaPanel.Tag = item.Id;
                    ManutencaoEquipInformaticaPrevLabel.Tag = item.Id;
                    ManutencaoEquipInformaticaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Material".ToUpper()) && item.Descricao.ToUpper().Contains("Impressos".ToUpper()))
                {
                    MaterialEscritorioPanel.Tag = item.Id;
                    MaterialEscritorioPrevLabel.Tag = item.Id;
                    MaterialEscritorioRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Despesas com Medicina do Trabalho".ToUpper()))
                {
                    DespesasMedicasPanel.Tag = item.Id;
                    DespesasMedicasPrevLabel.Tag = item.Id;
                    DespesasMedicasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Manutenção".ToUpper()) && item.Descricao.ToUpper().Contains("Bens".ToUpper()))
                {
                    ManutencaoDeBensPanel.Tag = item.Id;
                    ManutencaoDeBensPrevLabel.Tag = item.Id;
                    ManutencaoDeBensRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Viagens".ToUpper()) && item.Descricao.ToUpper().Contains("Pedagios".ToUpper()))
                {
                    DespesasViagensPanel.Tag = item.Id;
                    DespesasViagensPrevLabel.Tag = item.Id;
                    DespesasViagensRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Serviços".ToUpper()) && item.Descricao.ToUpper().Contains("Diversos".ToUpper()))
                {
                    DespesasServicosDiversosPanel.Tag = item.Id;
                    DespesasServicosDiversosPrevLabel.Tag = item.Id;
                    DespesasServicosDiversosRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Controle".ToUpper()) && item.Descricao.ToUpper().Contains("Ambiental".ToUpper()))
                {
                    ControleAmbientalPanel.Tag = item.Id;
                    ControleAmbientalPrevLabel.Tag = item.Id;
                    ControleAmbientalRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Frete".ToUpper()) && item.Descricao.ToUpper().Contains("Malote".ToUpper()))
                {
                    FretesCarretosMalotesPanel.Tag = item.Id;
                    FretesCarretosMalotesPrevLabel.Tag = item.Id;
                    FretesCarretosMalotesRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Equals("Asessorias Contabil, Fiscal e Empresarial".ToUpper()))
                {
                    AcessoriasPanel.Tag = item.Id;
                    AcessoriasPrevLabel.Tag = item.Id;
                    AcessoriasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Assinaturas".ToUpper()) && item.Descricao.ToUpper().Contains("Propagandas".ToUpper()))
                {
                    AlugueiseBrindesPanel.Tag = item.Id;
                    AlugueiseBrindesPrevLabel.Tag = item.Id;
                    AlugueiseBrindesRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Taxas, Impostos e Contribuições".ToUpper()))
                {
                    TaxasImpostosContribuicoesPanel.Tag = item.Id;
                    TaxasImpostosContribuicoesPrevLabel.Tag = item.Id;
                    TaxasImpostosContribuicoesRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Holding".ToUpper()))
                {
                    DespesaHoldingPanel.Tag = item.Id;
                    DespesaHoldingPrevLabel.Tag = item.Id;
                    DespesaHoldingRealPrev.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Despesas".ToUpper()) && item.Descricao.ToUpper().Contains("Venda".ToUpper()))
                {
                    DespesasComVendaPanel.Tag = item.Id;
                    DespesasComVendaPrevLabel.Tag = item.Id;
                    DespesasComVendaRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("recuperação".ToUpper()) && item.Descricao.ToUpper().Contains("Despesas".ToUpper()))
                {
                    RecuperacaoDespesasPanel.Tag = item.Id;
                    RecuperacaoDespesasPrevLabel.Tag = item.Id;
                    RecuperacaoDespesasRealLabel.Tag = item.Id;
                }

                if (item.Descricao.ToUpper().Contains("Multas".ToUpper()) && item.Descricao.ToUpper().Contains("Fiscais".ToUpper()))
                {
                    MultasDeInfracaoFiscalPanel.Tag = item.Id;
                    MultasDeInfracaoFiscalPrevLabel.Tag = item.Id;
                    MultasDeInfracaoFiscalRealLabel.Tag = item.Id;
                }

            }
            #endregion
        }

        private void ReceitaLiquidaPrevLabel_Enter(object sender, EventArgs e)
        {
            try
            {
                ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
            }
            catch { }
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            referenciaMaskedEditBox.ThemeStyle.BorderColor = Publicas._bordaEntrada;
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
            //pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void ReceitaLiquidaPrevLabel_Validating(object sender, CancelEventArgs e)
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

            Decimal _valor = ((CurrencyTextBox)sender).DecimalValue;
            bool igual = false;
            bool encontrou = false;
            int _idMeta = Convert.ToInt32(((CurrencyTextBox)sender).Tag);

            if (((CurrencyTextBox)sender).Name.Contains("PrevL"))
            {
                foreach (var itemM in _listaValoresMetas.Where(w => w.IdMetas == _idMeta))
                {
                    encontrou = true;

                    igual = _valor == itemM.Previsto;
                    itemM.Previsto = _valor;
                }

                if (!encontrou)
                {
                    _valoresMetas = new ValoresDasMetas();
                    _valoresMetas.IdMetas = _idMeta;
                    _valoresMetas.IdEmpresa = _empresa.IdEmpresa;
                    _valoresMetas.Referencia = _referencia;
                    _valoresMetas.Previsto = _valor;

                    _listaValoresMetas.Add(_valoresMetas);
                }

                if (!igual && !AtualizarNoFinalCheckBox.Checked)
                    CalculaTotaisDosGruposPrevisto();
            }
            else
            {
                foreach (var itemM in _listaValoresMetas.Where(w => w.IdMetas == _idMeta))
                {
                    encontrou = true;
                    igual = _valor == itemM.Realizado;
                    itemM.Realizado = _valor;
                }

                if (!encontrou)
                {
                    _valoresMetas = new ValoresDasMetas();
                    _valoresMetas.IdMetas = _idMeta;
                    _valoresMetas.IdEmpresa = _empresa.IdEmpresa;
                    _valoresMetas.Referencia = _referencia;
                    _valoresMetas.Realizado = _valor;

                    _listaValoresMetas.Add(_valoresMetas);
                }

                if (!igual && !AtualizarNoFinalCheckBox.Checked)
                    CalculaTotaisDosGruposRealizado();
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
                new Notificacoes.Mensagem("Selecione a Empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            arquivoImportado = "";
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            referenciaMaskedEditBox.ThemeStyle.BorderColor = Publicas._bordaSaida;
            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                //pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                referenciaMaskedEditBox.Text = string.Empty;
                referenciaMaskedEditBox.Focus();
                return;
            }

            try
            {
                if (referenciaMaskedEditBox.ClipText.Trim().Length != 6)
                {
                    new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }
                _dataInicio = new DateTime(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(2, 4)), Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(0, 2)), 1);
                _dataFim = _dataInicio.AddMonths(1).AddDays(-1);
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

            if (_bscEmEdicao.Existe && _bscEmEdicao.IdUsuario != Publicas._usuario.Id && (!Publicas._usuario.ApenasConsultaDRE || Publicas._usuario.ApenasEditarPrevistoDRE))
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
                _bscEmEdicao.Tela = "Contabilidade - DRE";

                if (!Publicas._usuario.ApenasConsultaDRE || Publicas._usuario.ApenasEditarPrevistoDRE)
                {
                    new MetasBO().Gravar(_bscEmEdicao);

                    _bscEmEdicao = new MetasBO().Consultar(_empresa.IdEmpresa, referenciaMaskedEditBox.ClipText);
                }
            }

            referenciaMaskedEditBox.Cursor = Cursors.WaitCursor;
            mensagemSistemaLabel.Text = "Aguarde, Pesquisando...";
            this.Refresh();

            _referencia = referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2);
            _dre = new ValoresDasMetasNoGlobusBO().ConsultarDRE(_empresa.IdEmpresa, _referencia);

            idMetasGrava = 0;

            PeriodoEncerradoCheckBox.Checked = false;


            if (_listaValoresMetas == null)
                _listaValoresMetas = new List<ValoresDasMetas>();

            _listaValoresMetas = new MetasBO().ListarDRE(false, _empresa.IdEmpresa, _referencia);
            _listaValoresMetasLog = new MetasBO().ListarDRE(false, _empresa.IdEmpresa, _referencia);

            //if (Convert.ToInt32(_referencia) < 201906 && _listaValoresMetas.Count() != 0 && (_empresa.IdEmpresa != 1 || _empresa.IdEmpresa != 9 || _empresa.IdEmpresa != 11) &&
            //    (!Publicas._usuario.Desenvolvedor && Publicas._usuario.UsuarioAcesso != "AVSOUZA"))
            //    idMetasGrava = 45;

            if (_dre.Existe)
            {
                PeriodoEncerradoCheckBox.Checked = _dre.Fechado;
                DissidioCurrencyText.DecimalValue = _dre.Dissidio;

                DataCorteLabel.Text = (_dre.DataFechamento == DateTime.MinValue ? "" : "Data Corte Financeiro " + _dre.DataFechamento.ToShortDateString());

            }

            foreach (var item in _listaValoresMetas.OrderBy(o => o.IdMetas))
            {
                foreach (Control itemC in DadosPanel.Controls)
                {
                    if (itemC is Panel)
                    {
                        foreach (Control itemP in itemC.Controls)
                        {
                            if (itemP is CurrencyTextBox)
                            {
                                if (Convert.ToInt32(((CurrencyTextBox)itemP).Tag) == item.IdMetas)
                                {
                                    if (((CurrencyTextBox)itemP).Name.Contains("Real"))
                                        ((CurrencyTextBox)itemP).DecimalValue = item.Realizado;
                                    else
                                        ((CurrencyTextBox)itemP).DecimalValue = item.Previsto;
                                }
                            }
                        }
                    }
                }
            }

            CalculaTotaisDosGruposRealizado();
            CalculaTotaisDosGruposPrevisto();

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
                //_diasUteis = new FeriadoBO().DiasUteis(_empresa.IdEmpresa, Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString()), Convert.ToDateTime("01/" + (_mes + 1).ToString("00") + "/" + _ano.ToString()).AddDays(-1));
            }

            gravarButton.Text = (_dre.Existe ? "&Alterar" : "&Gravar");

            if ((Publicas._usuario.ApenasConsultaDRE && Publicas._usuario.ApenasEditarPrevistoDRE && !_dre.Fechado) || Publicas._usuario.Desenvolvedor)
                gravarButton.Enabled = true;
            else
            {
                if (Publicas._usuario.ApenasConsultaDRE)
                    gravarButton.Enabled = false;
                else
                    gravarButton.Enabled = !_dre.Fechado || (_dre.Fechado && Publicas._usuario.PermiteReabrirDRE);
            }

            excluirButton.Enabled = _dre.Existe && (!_dre.Fechado || (_dre.Fechado && Publicas._usuario.PermiteReabrirDRE));
            ImpotarButton.Enabled = _listaValoresMetas.Where(w => w.Previsto == 0 && w.IdMetas == 145).Count() > 0 ||
                _listaValoresMetas.Where(w => w.IdMetas == 145).Count() == 0;

            BuscarRealizadoButton.Enabled = !PeriodoEncerradoCheckBox.Checked || Publicas._usuario.Desenvolvedor;
            AtualizarButton.Enabled = AtualizarNoFinalCheckBox.Checked;

            referenciaMaskedEditBox.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";
            this.Refresh();
        }

        private void BuscarRealizadoButton_Click(object sender, EventArgs e)
        {
            int _idMeta = 0;
            bool encontrou = false;
            decimal _valor = 0;

            BuscarRealizadoButton.Cursor = Cursors.WaitCursor;
            mensagemSistemaLabel.Text = "Aguarde, Pesquisando...";
            this.Refresh();

            #region Busca movimento na contabilidade
            foreach (Control item in DadosPanel.Controls)
            {
                if (item is Panel)
                {
                    foreach (Control itemP in item.Controls)
                    {
                        _valor = 0;
                        if (itemP is CurrencyTextBox)
                        {
                            _idMeta = Convert.ToInt32(((CurrencyTextBox)itemP).Tag);

                            _metas = new MetasBO().Consultar(_idMeta);

                            if (!_metas.GrupoTotalizador)
                            {
                                if (((CurrencyTextBox)itemP).Name.Contains("Real"))
                                {
                                    if (((CurrencyTextBox)itemP).Name.ToUpper().StartsWith("KMRODADO"))
                                    {
                                        _valor = new ValoresDasMetasNoGlobusBO().KmRodado(_empresa.CodigoEmpresaGlobus, _dataInicio, _dataFim);

                                        ((CurrencyTextBox)itemP).DecimalValue = Math.Round(_valor / 1000, 0);
                                    }
                                    else
                                    if (((CurrencyTextBox)itemP).Name.ToUpper().StartsWith("LITROSCONSUMIDOS"))
                                    {
                                        _valor = new ValoresDasMetasNoGlobusBO().LitrosConsumidos(_empresa.CodigoEmpresaGlobus
                                                                                                 , _dataInicio
                                                                                                 , _dataFim);

                                        ((CurrencyTextBox)itemP).DecimalValue = Math.Round(_valor, 0);
                                    }
                                    else
                                    {
                                        if (((CurrencyTextBox)itemP).Name.ToUpper().StartsWith("QTDEPASSASEIROS"))
                                        {
                                            if (((CurrencyTextBox)itemP).Name.ToUpper().Contains("GRATUIDADE"))
                                                _valor = new ValoresDasMetasNoGlobusBO().Gratuidade(_empresa.CodigoEmpresaGlobus
                                                                                                 , _dataInicio
                                                                                                 , _dataFim);

                                            if (((CurrencyTextBox)itemP).Name.ToUpper().Contains("PAGANTES"))
                                                _valor = new ValoresDasMetasNoGlobusBO().Pagantes(_empresa.CodigoEmpresaGlobus
                                                                                                 , _dataInicio
                                                                                                 , _dataFim);

                                            if (((CurrencyTextBox)itemP).Name.ToUpper().Contains("INTEGRACOESSVALOR"))
                                                _valor = new ValoresDasMetasNoGlobusBO().IntegracoesSemValor(_empresa.CodigoEmpresaGlobus
                                                                                                 , _dataInicio
                                                                                                 , _dataFim);

                                            ((CurrencyTextBox)itemP).DecimalValue = Math.Round(_valor, 0);
                                        }
                                        else
                                        {
                                            if (((CurrencyTextBox)itemP).Name.ToUpper().StartsWith("RECEITA") || ((CurrencyTextBox)itemP).Name.ToUpper().StartsWith("OUTRASRECEITAS"))
                                                _valor = new ValoresDasMetasNoGlobusBO().Receitas(_empresa.CodigoEmpresaGlobus
                                                                                                  , _referencia
                                                                                                  , _idMeta
                                                                                                  , _empresa.IdEmpresa);
                                            else
                                                _valor = new ValoresDasMetasNoGlobusBO().Despesas(_empresa.CodigoEmpresaGlobus
                                                                                                  , _referencia
                                                                                                  , _idMeta
                                                                                                  , _empresa.IdEmpresa);

                                            ((CurrencyTextBox)itemP).DecimalValue = Math.Round(_valor / 1000, 2);
                                        }
                                    }
                                    encontrou = false;

                                    foreach (var itemM in _listaValoresMetas.Where(w => w.IdMetas == _idMeta))
                                    {
                                        encontrou = true;
                                        if (((CurrencyTextBox)itemP).Name.ToUpper().StartsWith("LITROSCONSUMIDOS") ||
                                            ((CurrencyTextBox)itemP).Name.ToUpper().StartsWith("QTDEPASSASEIROS"))
                                            itemM.Realizado = Math.Round(_valor, 0);
                                        else
                                            itemM.Realizado = Math.Round(_valor / 1000, 2);
                                    }

                                    if (!encontrou)
                                    {
                                        _valoresMetas = new ValoresDasMetas();
                                        _valoresMetas.IdMetas = _idMeta;
                                        _valoresMetas.IdEmpresa = _empresa.IdEmpresa;
                                        _valoresMetas.Referencia = _referencia;

                                        if (((CurrencyTextBox)itemP).Name.ToUpper().StartsWith("LITROSCONSUMIDOS") ||
                                            (((CurrencyTextBox)itemP).Name.ToUpper().StartsWith("QTDEPASSASEIROS")))
                                            _valoresMetas.Realizado = Math.Round(_valor, 0);
                                        else
                                            _valoresMetas.Realizado = Math.Round(_valor / 1000, 2);

                                        _listaValoresMetas.Add(_valoresMetas);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            CalculaTotaisDosGruposRealizado();

            //gridGroupingControl1.DataSource = _listaValoresMetas;

            BuscarRealizadoButton.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";
            this.Refresh();

        }

        private void CalculaTotaisDosGruposRealizado()
        {
            int _idMeta = 0;
            bool encontrou = false;
            decimal _valor = 0;
            bool first = true;
            string formula = "";
            string[] id;

            #region Calcula Totais dos grupos
            // repete 5 vezes.
            for (int i = 0; i < 6; i++)
            {
                foreach (Control item in DadosPanel.Controls)
                {
                    if (item is Panel)
                    {
                        foreach (Control itemP in item.Controls)
                        {
                            _valor = 0;
                            if (itemP is CurrencyTextBox)
                            {
                                if (((CurrencyTextBox)itemP).Name.Contains("Real"))
                                {
                                    _idMeta = Convert.ToInt32(((CurrencyTextBox)itemP).Tag);

                                    if (_idMeta <= idMetasGrava)
                                        continue;

                                    _metas = new MetasBO().Consultar(_idMeta);
                                    /**/
                                    if (_idMeta == 145)
                                    {
                                        Console.WriteLine("idMeta = 145");
                                    }
                                    /**/
                                    if (_metas.Existe && _metas.GrupoTotalizador && _metas.NivelCalculo == i)
                                    {

                                        formula = _metas.FormulaTotalizador;

                                        if (formula.Contains("+"))
                                            id = formula.Split('+');
                                        else
                                            id = formula.Split('-');

                                        first = true;
                                        foreach (var itemA in id)
                                        {
                                            if (string.IsNullOrEmpty(itemA))
                                                continue;

                                            foreach (var itemV in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(Publicas.OnlyNumbers(itemA.Trim()))))
                                            {

                                                if (first)
                                                    _valor = Math.Round(itemV.Realizado, 2);
                                                else
                                                if (formula.Contains("+"))
                                                    _valor = _valor + Math.Round(itemV.Realizado, 2);
                                                else
                                                    _valor = _valor - Math.Round(itemV.Realizado, 2);

                                                first = false;
                                            }
                                        }

                                        ((CurrencyTextBox)itemP).DecimalValue = _valor;
                                        encontrou = false;

                                        foreach (var itemM in _listaValoresMetas.Where(w => w.IdMetas == _idMeta))
                                        {
                                            encontrou = true;
                                            itemM.Realizado = _valor;
                                        }

                                        if (!encontrou)
                                        {
                                            _valoresMetas = new ValoresDasMetas();
                                            _valoresMetas.IdMetas = _idMeta;
                                            _valoresMetas.IdEmpresa = _empresa.IdEmpresa;
                                            _valoresMetas.Referencia = _referencia;
                                            _valoresMetas.Realizado = _valor;

                                            _listaValoresMetas.Add(_valoresMetas);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void CalculaTotaisDosGruposPrevisto()
        {
            int _idMeta = 0;
            bool encontrou = false;
            decimal _valor = 0;
            bool first = true;
            string formula = "";
            string[] id;

            #region Calcula Totais dos grupos
            // repete 5 vezes.
            for (int i = 0; i < 6; i++)
            {
                foreach (Control item in DadosPanel.Controls)
                {
                    if (item is Panel)
                    {
                        foreach (Control itemP in item.Controls)
                        {
                            _valor = 0;
                            if (itemP is CurrencyTextBox)
                            {
                                if (((CurrencyTextBox)itemP).Name.Contains("Prev"))
                                {
                                    _idMeta = Convert.ToInt32(((CurrencyTextBox)itemP).Tag);

                                    if (_idMeta <= idMetasGrava)
                                        continue;

                                    _metas = new MetasBO().Consultar(_idMeta);

                                    if (_metas.Existe && _metas.GrupoTotalizador && _metas.NivelCalculo == i)
                                    {

                                        formula = _metas.FormulaTotalizador;

                                        if (formula.Contains("+"))
                                            id = formula.Split('+');
                                        else
                                            id = formula.Split('-');

                                        first = true;
                                        foreach (var itemA in id)
                                        {
                                            foreach (var itemV in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(Publicas.OnlyNumbers(itemA.Trim()))))
                                            {

                                                if (first)
                                                    _valor = Math.Round(itemV.Previsto, 2);
                                                else
                                                if (formula.Contains("+"))
                                                    _valor = _valor + Math.Round(itemV.Previsto, 2);
                                                else
                                                    _valor = _valor - Math.Round(itemV.Previsto, 2);

                                                first = false;
                                            }
                                        }

                                        ((CurrencyTextBox)itemP).DecimalValue = _valor;
                                        encontrou = false;

                                        foreach (var itemM in _listaValoresMetas.Where(w => w.IdMetas == _idMeta))
                                        {
                                            encontrou = true;
                                            itemM.Previsto = _valor;
                                        }

                                        if (!encontrou)
                                        {
                                            _valoresMetas = new ValoresDasMetas();
                                            _valoresMetas.IdMetas = _idMeta;
                                            _valoresMetas.IdEmpresa = _empresa.IdEmpresa;
                                            _valoresMetas.Referencia = _referencia;
                                            _valoresMetas.Previsto = _valor;

                                            _listaValoresMetas.Add(_valoresMetas);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            if (_bscEmEdicao != null)
                new MetasBO().Excluir(_bscEmEdicao);
            Close();
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
                PeriodoEncerradoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void PeriodoEncerradoCheckBox_KeyDown(object sender, KeyEventArgs e)
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

        private void ReceitaLiquidaPrevLabel_KeyDown(object sender, KeyEventArgs e)
        {
            Control ctl;
            ctl = ((Control)sender).Parent;

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            gravarButton.Cursor = Cursors.WaitCursor;
            mensagemSistemaLabel.Text = "Aguarde, Gravando...";
            this.Refresh();

            CalculaTotaisDosGruposRealizado();
            CalculaTotaisDosGruposPrevisto();

            if (PeriodoEncerradoCheckBox.Checked && _dre.DataFechamento == DateTime.MinValue)
            {
                if (new Notificacoes.Mensagem("Confirma o fechamento do período ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    gravarButton.Cursor = Cursors.Default;
                    mensagemSistemaLabel.Text = "";
                    this.Refresh();
                    PeriodoEncerradoCheckBox.Focus();
                    return;
                }
            }

            if (_dre == null)
                _dre = new DRE();

            _dre.IdEmpresa = _empresa.IdEmpresa;
            _dre.Fechado = PeriodoEncerradoCheckBox.Checked;
            _dre.Referencia = _referencia;

            if (_dre.Fechado && _dre.DataFechamento == DateTime.MinValue)
            {
                _listaValoresMetas.ForEach(u => u.DataCorteFinanceiro = DateTime.Now);
                _dre.DataFechamento = DateTime.Now;
            }

            if (!new ValoresDasMetasNoGlobusBO().Gravar(_dre, _listaValoresMetas.Where(w => w.IdMetas > idMetasGrava).ToList()))
            {
                gravarButton.Cursor = Cursors.Default;
                mensagemSistemaLabel.Text = "";
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou valores das metas da empresa " + empresaComboBoxAdv.Text + " periodo " + referenciaMaskedEditBox.Text;

            foreach (var itemL in _listaValoresMetasLog.OrderBy(o => o.IdMetas))
            {
                foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == itemL.IdMetas))
                {
                    if (itemL.Previsto != item.Previsto || itemL.Realizado != item.Realizado)
                        _log.Descricao = _log.Descricao + " [ " + itemL.Metas;

                    if (itemL.Previsto != item.Previsto)
                        _log.Descricao = _log.Descricao + " Previsto de " + itemL.Previsto.ToString() + " para " + item.Previsto.ToString();

                    if (itemL.Realizado != item.Realizado)
                        _log.Descricao = _log.Descricao + " Realizado de " + itemL.Realizado.ToString() + " para " + item.Realizado.ToString();

                    if (itemL.Previsto != item.Previsto || itemL.Realizado != item.Realizado)
                        _log.Descricao = _log.Descricao + " ]";
                }
            }

            if (arquivoImportado != "")
                _log.Descricao = _log.Descricao + " importado pelo arquivo " + arquivoImportado;

            _log.Tela = "Contabilidade - DRE";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
            gravarButton.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";
            this.Refresh();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            if (_listaValoresMetas.Count() != 0)
                _listaValoresMetas.Clear();

            foreach (Control item in DadosPanel.Controls)
            {
                if (item is Panel)
                {
                    foreach (Control itemP in item.Controls)
                    {
                        if (itemP is CurrencyTextBox)
                            ((CurrencyTextBox)itemP).DecimalValue = 0;
                    }
                }
            }

            new MetasBO().Excluir(_bscEmEdicao);
            DadosPanel.AutoScrollPosition = new Point(0);
            referenciaMaskedEditBox.Text = string.Empty;

            EbitdaRealLabel.DecimalValue = 0;
            EbitdaPrevLabel.DecimalValue = 0;

            DataCorteLabel.Text = "";
            gravarButton.Text = "&Gravar";
            referenciaMaskedEditBox.Focus();
            excluirButton.Enabled = false;
            gravarButton.Enabled = false;
            ImpotarButton.Enabled = false;
        }

        private void AtualizarButton_Click(object sender, EventArgs e)
        {
            AtualizarButton.Cursor = Cursors.WaitCursor;
            mensagemSistemaLabel.Text = "Aguarde, Atualizando valores...";
            this.Refresh();

            CalculaTotaisDosGruposRealizado();
            CalculaTotaisDosGruposPrevisto();

            AtualizarButton.Cursor = Cursors.Default;
            AtualizarButton.Cursor = Cursors.Default;
            AtualizarButton.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";
            this.Refresh();
        }

        private void AtualizarNoFinalCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            AtualizarButton.Enabled = AtualizarNoFinalCheckBox.Checked;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new ValoresDasMetasNoGlobusBO().Excluir(_dre.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluir DRE da empresa " + empresaComboBoxAdv.Text + " periodo " + referenciaMaskedEditBox.Text;

            _log.Tela = "Contabilidade - DRE";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void DemonstrativoDoResultadoDoExercicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_bscEmEdicao != null)
                new MetasBO().Excluir(_bscEmEdicao);
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        { //importar
            if (new Notificacoes.Mensagem("Confirma a importação ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                new Notificacoes.Mensagem("Nenhum arquivo selecionado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            mensagemSistemaLabel.Text = "Importando arquivo, aguarde...";
            Refresh();

            string[] arquivos = openFileDialog1.FileNames;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            Excel.Range range = null;

            string str = "";
            int rCnt;
            int cCnt;
            int rw = 0;
            int cl = 0;
            int colunaDaReferencia = 0;
            string codigo = "";
            decimal valor = 0;

            arquivoImportado = "";
            foreach (var itemA in arquivos)
            {
                
                arquivoImportado = itemA;

                xlApp = new Excel.Application();

                try
                {
                    xlApp.DisplayAlerts = false;
                    xlWorkBook = xlApp.Workbooks.Open(itemA, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 1);

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    range = xlWorkSheet.UsedRange;
                    rw = range.Rows.Count;
                    cl = range.Columns.Count;
                    // linha um é o cabeçalho
                    str = (Convert.ToString((range.Cells[1, 1] as Excel.Range).Value2));

                    for (cCnt = 1; cCnt <= cl; cCnt++)
                    { // localiza a coluna que irá importar pela referencia. 
                        try
                        {
                            str = Convert.ToString((range.Cells[2, cCnt] as Excel.Range).Value);
                            if (str.Substring(3,7) != referenciaMaskedEditBox.Text)
                                continue;

                            colunaDaReferencia = cCnt;
                            break;
                        }
                        catch { }
                    }

                    if (colunaDaReferencia == 0)
                    {
                        new Notificacoes.Mensagem("Referência não encontrada no arquivo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                        mensagemSistemaLabel.Text = "";
                        Refresh();
                        return;
                    }

                    for (rCnt = 3; rCnt <= rw; rCnt++)
                    { // le as linhas
                        try
                        {
                            str = Convert.ToString((range.Cells[rCnt, colunaDaReferencia] as Excel.Range).Value2);
                            valor = Math.Round(Convert.ToDecimal(str.Trim().Replace(".", "").Replace(" ", "")), 2);

                            try
                            {
                                codigo = Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2);
                            }
                            catch
                            {
                                codigo = Convert.ToString((range.Cells[rCnt, 2] as Excel.Range).Value2);
                            }

                            if (_listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(codigo)).Count() == 0 && codigo != null)
                            {
                                    _valoresMetas = new ValoresDasMetas();
                                    _valoresMetas.IdMetas = Convert.ToInt32(codigo);
                                    _valoresMetas.IdEmpresa = _empresa.IdEmpresa;
                                    _valoresMetas.Referencia = _referencia;
                                    _valoresMetas.Previsto = valor;

                                    _listaValoresMetas.Add(_valoresMetas);
                            }
                            else
                            foreach (var item in _listaValoresMetas.Where(w => w.IdMetas == Convert.ToInt32(codigo)))
                            {
                                item.Previsto = valor;
                            } 
                        }
                        catch { }
                    }
                }
                catch
                {
                    new Notificacoes.Mensagem("Problemas na importação.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    mensagemSistemaLabel.Text = "";
                    Refresh();
                    return;
                }
                break;
            }


            CalculaTotaisDosGruposPrevisto();
           // gridGroupingControl1.DataSource = _listaValoresMetas;

            gravarButton_Click(sender, e);

            mensagemSistemaLabel.Text = "";
            Refresh();

        }

        private void PeriodoEncerradoCheckBox_Validating(object sender, CancelEventArgs e)
        {

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }

        private void AtualizarNoFinalCheckBox_Validating(object sender, CancelEventArgs e)
        {

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }

        private void DissidioCurrencyText_Validating(object sender, CancelEventArgs e)
        {

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }
    }
}
