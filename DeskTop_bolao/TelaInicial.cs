using Classes;
using Negocio;
using Suportte.Cadastros;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Chart;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Suportte
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();

            DateTime _dataTemas = //new DateTime(2018, 06, 14);
            DateTime.Now.Date;

            #region temas
            if (_dataTemas >= Publicas._dataInicioTeste &&
                _dataTemas <= Publicas._dataFimTeste)
            {
                Publicas._fonte = Publicas.padraoNatal_FonteEscura;
                Publicas._fundo = Publicas.padraoNatal_Fundo;
                Publicas._bordaSaida = Publicas.padraoNatal_BordaSaida;
                Publicas._bordaEntrada = Publicas.padraoNatal_BordaEntrada;
                Publicas._botaoFocado = Publicas.padraoNatal_BotaoFocado;
                Publicas._botao = Publicas.padraoNatalo_Botao;
                Publicas._fonteBotao = Publicas.padraoNatal_FonteBotao;
                Publicas._fonteBotaoFocado = Publicas.padraoNatal_FonteBotaoFocado;
                Publicas._tabPageAtiva = Publicas.padraoNatal_BordaEntrada;
                Publicas._panelTitulo = Publicas.padraoNatal_PanelTitulo;
                Publicas._panelTituloFoco = Publicas.padraoNatal_PanelTituloFoco;

                Publicas._alterouSkin = true;
                Publicas.AplicarSkin(this);
            }
            else
                if (_dataTemas >= Publicas._dataInicioPeriodoNatal &&
                _dataTemas <= Publicas._dataFimAnoNovo)
            {
                Publicas._fonte = Publicas.padraoNatal_FonteEscura;
                Publicas._fundo = Publicas.padraoNatal_Fundo;
                Publicas._bordaSaida = Publicas.padraoNatal_BordaSaida;
                Publicas._bordaEntrada = Publicas.padraoNatal_BordaEntrada;
                Publicas._botaoFocado = Publicas.padraoNatal_BotaoFocado;
                Publicas._botao = Publicas.padraoNatalo_Botao;
                Publicas._fonteBotao = Publicas.padraoNatal_FonteBotao;
                Publicas._fonteBotaoFocado = Publicas.padraoNatal_FonteBotaoFocado;
                Publicas._tabPageAtiva = Publicas.padraoNatal_BordaEntrada;
                Publicas._panelTitulo = Publicas.padraoNatal_PanelTitulo;
                Publicas._panelTituloFoco = Publicas.padraoNatal_PanelTituloFoco;
                FundoPictureBox.ImageLocation = @"Imagens\Fundo_Natal_Vermelho.png";

                Publicas._alterouSkin = true;
                Publicas.AplicarSkin(this);
            }
            else
            {
                if (_dataTemas >= Publicas._dataInicioPeriodoHalloween &&
                    _dataTemas <= Publicas._dataFimPeriodoHalloween)
                {
                    Publicas._fonte = Publicas.padraoHalloween_FonteEscura;
                    Publicas._fundo = Publicas.padraoHalloween_Fundo;
                    Publicas._bordaSaida = Publicas.padraoHalloween_BordaSaida;
                    Publicas._bordaEntrada = Publicas.padraoHalloween_BordaEntrada;
                    Publicas._botaoFocado = Publicas.padraoHalloween_BotaoFocado;
                    Publicas._botao = Publicas.padraoHalloween_Botao;
                    Publicas._fonteBotao = Publicas.padraoHalloween_FonteBotao;
                    Publicas._fonteBotaoFocado = Publicas.padraoHalloween_FonteBotaoFocado;
                    Publicas._tabPageAtiva = Publicas.padraoHalloween_BordaEntrada;
                    Publicas._panelTitulo = Publicas.padraoHalloween_PanelTitulo;
                    Publicas._panelTituloFoco = Publicas.padraoHallowen_PanelTituloFoco;
                    FundoPictureBox.ImageLocation = @"Imagens\Fundo_Halloween.jpg";
                    Publicas._alterouSkin = true;
                    Publicas.AplicarSkin(this);
                }
                else
                {
                    if ((_dataTemas >= Publicas._dataInicioCopa &&
                         _dataTemas <= Publicas._dataFimCopa))
                    {
                        Publicas._fonte = Publicas.padraoCopa_FonteEscura;
                        Publicas._fundo = Publicas.padraoCopa_Fundo;
                        Publicas._bordaSaida = Publicas.padraoCopa_BordaSaida;
                        Publicas._bordaEntrada = Publicas.padraoCopa_BordaEntrada;
                        Publicas._botaoFocado = Publicas.padraoCopa_BotaoFocado;
                        Publicas._botao = Publicas.padraoCopa_Botao;
                        Publicas._fonteBotao = Publicas.padraoCopa_FonteBotao;
                        Publicas._fonteBotaoFocado = Publicas.padraoCopa_FonteBotaoFocado;
                        Publicas._tabPageAtiva = Publicas.padraoCopa_BordaEntrada;
                        Publicas._panelTitulo = Publicas.padraoCopa_PanelTitulo;
                        Publicas._panelTituloFoco = Publicas.padraoCopa_PanelTituloFoco;
                        ///FundoPictureBox.ImageLocation = @"Imagens\Fundo_Copa.png";

                        Publicas._alterouSkin = true;
                        Publicas.AplicarSkin(this);
                    }
                    else
                    {
                        if ((_dataTemas >= Publicas._dataInicioCarnaval &&
                             _dataTemas <= Publicas._dataFimCarnaval))
                        {
                            Publicas._fonte = Publicas.padraoCarnaval_FonteEscura;
                            Publicas._fundo = Publicas.padraoCarnaval_Fundo;
                            Publicas._bordaSaida = Publicas.padraoCarnaval_BordaSaida;
                            Publicas._bordaEntrada = Publicas.padraoCarnaval_BordaEntrada;
                            Publicas._botaoFocado = Publicas.padraoCarnaval_BotaoFocado;
                            Publicas._botao = Publicas.padraoCarnaval_Botao;
                            Publicas._fonteBotao = Publicas.padraoCarnaval_FonteBotao;
                            Publicas._fonteBotaoFocado = Publicas.padraoCarnaval_FonteBotaoFocado;
                            Publicas._tabPageAtiva = Publicas.padraoCarnaval_BordaEntrada;
                            Publicas._panelTitulo = Publicas.padraoCarnaval_PanelTitulo;
                            Publicas._panelTituloFoco = Publicas.padraoCarnaval_PanelTituloFoco;
                            FundoPictureBox.ImageLocation = @"Imagens\Fundo_Carnaval.jpg";

                            Publicas._alterouSkin = true;
                            Publicas.AplicarSkin(this);
                        }
                        else
                        {
                            if ((_dataTemas >= Publicas._dataInicioPascoa &&
                                 _dataTemas <= Publicas._dataFimPascoa))
                            {
                                Publicas._fonte = Publicas.padraoPascoa_FonteEscura;
                                Publicas._fundo = Publicas.padraoPascoa_Fundo;
                                Publicas._bordaSaida = Publicas.padraoPascoa_BordaSaida;
                                Publicas._bordaEntrada = Publicas.padraoPascoa_BordaEntrada;
                                Publicas._botaoFocado = Publicas.padraoPascoa_BotaoFocado;
                                Publicas._botao = Publicas.padraoPascoa_Botao;
                                Publicas._fonteBotao = Publicas.padraoPascoa_FonteBotao;
                                Publicas._fonteBotaoFocado = Publicas.padraoPascoa_FonteBotaoFocado;
                                Publicas._tabPageAtiva = Publicas.padraoPascoa_BordaEntrada;
                                Publicas._panelTitulo = Publicas.padraoPascoa_PanelTitulo;
                                Publicas._panelTituloFoco = Publicas.padraoPascoa_PanelTituloFoco;
                                FundoPictureBox.ImageLocation = @"Imagens\Fundo_Pascoa.jpg";

                                Publicas._alterouSkin = true;
                                Publicas.AplicarSkin(this);
                            }
                            else
                            {
                                Publicas._fonte = Publicas.padraoNIFFClaro_FonteEscura;
                                Publicas._fundo = Publicas.padraoNiffClaro_Fundo;
                                Publicas._bordaSaida = Publicas.padraoNIFFClaro_BordaSaida;
                                Publicas._bordaEntrada = Publicas.padraoNIFFClaro_BordaEntrada;
                                Publicas._tabPageAtiva = Publicas.padraoNIFFClaro_BordaEntrada;
                                Publicas._botaoFocado = Publicas.padraoNIFFClaro_BotaoFocado;
                                Publicas._botao = Publicas.padraoNIFFClaro_Botao;
                                Publicas._fonteBotao = Publicas.padraoNIFFClaro_FonteBotao;
                                Publicas._fonteBotaoFocado = Publicas.padraoNIFFClaro_FonteBotaoFocado;
                                Publicas._panelTitulo = Publicas.padraoNIFFClaro_PanelTitulo;
                                Publicas._panelTituloFoco = Publicas.padraoNIFFClaro_PanelTituloFoco;
                                Publicas._alterouSkin = false;
                            }
                        }
                    }
                }
            }
            #endregion
        }

        #region Atributos
        private class ArquivosEmpresaArquivei
        {
            public int IdEmpresa { get; set; }
            public string NomeArquivo { get; set; }
            public string Acao { get; set; }
        }

        string _dataCompilacao;
        string _versaoAplicativo;
        string tipoAvaliacao;

        int idSuperior;
        int _quantidadeGraficos = 0;
        int _widthMenuPadrao = 235;
        int _heidthMenuPadrao = 48;
        int _heidthMenuSistema = 52;
        int _widthLabelMenu = 185;
        int _widthLabelMenuSistema = 140;
        int _rowIndexComunicado = 0;
        int _quantidadeNotificacoesNaoLidas = 0;
        int contadorTempoEncerrarSistema = 0;
        int quantidadeBiblioteca = 0;

        bool _comunicadosEmPesquisa = false;
        bool _notificacaoEmPesquisa = false;
        bool _atualizarComunicado = false;
        bool _notificacaoBolaoEmPesquisa = false;
        bool _fixaDashBoard = false;
        bool _andamentoAvaliacaoEmPesquisa = false;
        bool _mudouVisualizacaoAndamento = false;
        bool _arquiveiEmPesquisa = false;
        bool _bibliotecaEmPesquisa = false;

        Publicas.StatusComunicado _statusComunicadoSelecionado;

        Classes.Empresa _empresa;
        Classes.Colaboradores _colaboradores;
        Classes.Colaboradores supervisor;
        Classes.Cargos _cargo;
        Classes.Feriado _feriado;
        Classes.ParticipanteCorrida _participante;
        Classes.NotificacaoDoSistema _notificacao;

        List<Classes.Comunicado> _listaComunicados = new List<Comunicado>();
        List<Classes.NotificacaoComunicado> _listaComunicadosNotificacao = new List<NotificacaoComunicado>();
        List<Classes.Usuario> _listaAniversariantes;
        List<Classes.BancoDeHoras> _bancoDeHoras = new List<BancoDeHoras>();
        List<Classes.Corridas> _listaCorrida = new List<Classes.Corridas>();
        List<Classes.BolaoJogos> _listaJogosEncerrados = new List<BolaoJogos>();
        List<Classes.BolaoJogos> _listaJogosDos3Dias = new List<BolaoJogos>();
        List<Classes.AutoAvaliacao> _listaAvaliacoes = new List<AutoAvaliacao>();
        List<Classes.AutoAvaliacao> _listaAvaliacoesNotas = new List<AutoAvaliacao>();
        List<Classes.ParametrosArquivei> _listaParametrosArquivei = new List<ParametrosArquivei>();
        List<Classes.Arquivei> _listaArquivei = new List<Arquivei>();
        List<Classes.ItensArquivei> _listaItensArquivei = new List<ItensArquivei>();
        List<Classes.ItensParametrosArquivei> _listaItensParametros = new List<ItensParametrosArquivei>();
        List<Classes.EmprestimoLivros> _listaEmprestimos = new List<EmprestimoLivros>();
        List<Classes.ReservaLivros> _listaReservas = new List<ReservaLivros>();
        List<Classes.ResenhaLivros> _listaResenhas = new List<ResenhaLivros>();
        List<Classes.ResenhaLivros> _listaResenhasNaoCadastradas = new List<ResenhaLivros>();
        List<PartidasDoTorneio> _listaTorneios = new List<PartidasDoTorneio>();
        List<ArquivosEmpresaArquivei> _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();

        GridDynamicFilter filter = new GridDynamicFilter();
        GridMetroColors metroColor = new GridMetroColors();
        GridColumnDescriptor colunaGrid = new GridColumnDescriptor();

        #region Componentes de criação de menu
        Panel MenuUsuarioPanel;
        Panel MenuSistemaPanel;

        #region menu usuario
        Panel TrocaSenhaPanel;
        PictureBox DivisoriaTrocaSenhaImagem;
        Label TrocaSenhaLabel;

        Panel EditaPerfilPanel;
        PictureBox DivisoriaEditaPerfilImagem;
        Label EditaPerfilLabel;

        Panel SairPanel;
        PictureBox DivisoriaSairImagem;
        Label SairLabel;

        Panel TrocaUsuarioPanel;
        PictureBox DivisoriaTrocaUsuarioImagem;
        Label TrocaUsuarioLabel;
        #endregion

        #region MenuSistema
        Panel RecursosHumanosPanel;
        PictureBox DivisoriaRecursosHumanosImagem;
        PictureBox Divisoria2RecursosHumanosImagem;
        Label RecursosHumanosLabel;
        Label RecursosHumanosSetaLabel;

        Panel JuridicoPanel;
        PictureBox DivisoriaJuridicoImagem;
        PictureBox Divisoria2JuridicoImagem;
        Label JuridicoLabel;
        Label JuridicoSetaLabel;

        Panel ContabilidadePanel;
        PictureBox DivisoriaContabilidadeImagem;
        PictureBox Divisoria2ContabilidadeImagem;
        Label ContabilidadeLabel;
        Label ContabilidadeSetaLabel;

        Panel ControladoriaPanel;
        PictureBox DivisoriaControladoriaImagem;
        PictureBox Divisoria2ControladoriaImagem;
        Label ControladoriaLabel;
        Label ControladoriaSetaLabel;

        Panel AtendimentoPanel;
        PictureBox DivisoriaAtendimentoImagem;
        PictureBox Divisoria2AtendimentoImagem;
        Label AtendimentoLabel;
        Label AtendimentoSetaLabel;

        Panel TIPanel;
        PictureBox DivisoriaTIImagem;
        PictureBox Divisoria2TIImagem;
        Label TILabel;
        Label TISetaLabel;
        #endregion

        #region Menu RecursosHumanos
        Panel SubMenuRecursosHumanos;
        Panel SubMenuAvaliacaoDesempenho;
        Panel SubMenuADPlanejamento;
        Panel SubMenuADColaborador;
        Panel SubMenuADGestor;
        Panel SubMenuADRecursosHumanos;
        Panel SubMenuCadastrosRH;
        Panel SubMenuNineBox;

        Panel AvaliacaoDesempenhoPanel;
        PictureBox DivisoriaAvaliacaoDesempenhoImagem;
        PictureBox Divisoria2AvaliacaoDesempenhoImagem;
        Label AvaliacaoDesempenhoLabel;
        Label AvaliacaoDesempenhoSetaLabel;

        Panel BibliotecaPanel;
        PictureBox DivisoriaBibliotecaImagem;
        PictureBox Divisoria2BibliotecaImagem;
        Label BibliotecaLabel;
        Label BibliotecaSetaLabel;

        Panel CorridasPanel;
        PictureBox DivisoriaCorridasImagem;
        PictureBox Divisoria2CorridasImagem;
        Label CorridasLabel;
        Label CorridasSetaLabel;

        Panel PeriodoBancoHorasPanel;
        PictureBox DivisoriaPeriodoBancoHorasImagem;
        Label PeriodoBancoHorasLabel;

        #region SubMenu Avaliação Desempenho
        Panel AD_PlanejamentoPanel;
        PictureBox DivisoriaAD_PlanejamentoImagem;
        PictureBox Divisoria2AD_PlanejamentoImagem;
        Label AD_PlanejamentoLabel;
        Label AD_PlanejamentoSetaLabel;

        Panel AD_RecursosHumanosPanel;
        PictureBox DivisoriaAD_RecursosHumanosImagem;
        PictureBox Divisoria2AD_RecursosHumanosImagem;
        Label AD_RecursosHumanosLabel;
        Label AD_RecursosHumanosSetaLabel;

        Panel AD_ColaboradorPanel;
        PictureBox DivisoriaAD_ColaboradorImagem;
        PictureBox Divisoria2AD_ColaboradorImagem;
        Label AD_ColaboradorLabel;
        Label AD_ColaboradorSetaLabel;

        Panel AD_GestorPanel;
        PictureBox DivisoriaAD_GestorImagem;
        PictureBox Divisoria2AD_GestorImagem;
        Label AD_GestorLabel;
        Label AD_GestorSetaLabel;

        #region SubMenu Planejamento

        Panel MetasPanel;
        PictureBox DivisoriaMetasImagem;
        Label MetasLabel;

        Panel DefinicaoMetasPanel;
        PictureBox DivisoriaDefinicaoMetasImagem;
        Label DefinicaoMetasLabel;

        #endregion

        #region SubMenu Colaborador/Gestor

        Panel RadarPanel;
        PictureBox DivisoriaRadarImagem;
        Label RadarLabel;

        Panel FeedbackPanel;
        PictureBox DivisoriaFeedbackImagem;
        Label FeedbackLabel;

        Panel AutoAvaliacaoPanel;
        PictureBox DivisoriaAutoAvaliacaoImagem;
        Label AutoAvaliacaoLabel;

        Panel MetasNumericasPanel;
        PictureBox DivisoriaMetasNumericasImagem;
        Label MetasNumericasLabel;
        #endregion

        #region SubMenu RecursosHumanos

        Panel CadastrosRHPanel;
        PictureBox DivisoriaCadastrosRHImagem;
        PictureBox Divisoria2CadastrosRHImagem;
        Label CadastrosRHLabel;
        Label CadastrosRHSetaLabel;

        Panel NotasPanel;
        PictureBox DivisoriaNotasImagem;
        Label NotasLabel;

        #region CadastrosRH
        Panel NineBoxPanel;
        PictureBox DivisoriaNineBoxImagem;
        PictureBox Divisoria2NineBoxImagem;
        Label NineBoxLabel;
        Label NineBoxSetaLabel;

        Panel ColaboradoresPanel;
        PictureBox DivisoriaColaboradoresImagem;
        Label ColaboradoresLabel;

        Panel PrazosPanel;
        PictureBox DivisoriaPrazosImagem;
        Label PrazosLabel;

        Panel CargosPanel;
        PictureBox DivisoriaCargosImagem;
        Label CargosLabel;

        Panel EscolaridadePanel;
        PictureBox DivisoriaEscolaridadeImagem;
        Label EscolaridadeLabel;

        Panel CompetenciasPanel;
        PictureBox DivisoriaCompetenciasImagem;
        Label CompetenciasLabel;

        Panel DepartamentoPanel;
        PictureBox DivisoriaDepartamentoImagem;
        Label DepartamentoLabel;

        Panel PontuacaoPanel;
        PictureBox DivisoriaPontuacaoImagem;
        Label PontuacaoLabel;

        #region NineBox
        Panel CargosNineBoxPanel;
        PictureBox DivisoriaCargosNineBoxImagem;
        Label CargosNineBoxLabel;

        Panel ColaboradoresNineBoxPanel;
        PictureBox DivisoriaColaboradoresNineBoxImagem;
        Label ColaboradoresNineBoxLabel;

        #endregion

        #endregion

        #endregion

        #endregion

        #endregion

        #region SubMenu Biblioteca
        Panel SubMenuBiblioteca;
        Panel SubMenuCadastroBiblioteca;

        Panel CadastroBibliotecaPanel;
        PictureBox DivisoriaCadastroBibliotecaImagem;
        PictureBox Divisoria2CadastroBibliotecaImagem;
        Label CadastroBibliotecaLabel;
        Label CadastroBibliotecaSetaLabel;

        Panel EmprestimoPanel;
        PictureBox DivisoriaEmprestimoImagem;
        Label EmprestimoLabel;

        Panel ReservaPanel;
        PictureBox DivisoriaReservaImagem;
        Label ReservaLabel;

        Panel DevolucaoPanel;
        PictureBox DivisoriaDevolucaoImagem;
        Label DevolucaoLabel;

        Panel PerguntasPanel;
        PictureBox DivisoriaPerguntasImagem;
        Label PerguntasLabel;

        Panel RespostasPanel;
        PictureBox DivisoriaRespostasImagem;
        Label RespostasLabel;

        Panel PontuacaoLivrosPanel;
        PictureBox DivisoriaPontuacaoLivrosImagem;
        Label PontuacaoLivrosLabel;

        Panel AcompanhamentoPanel;
        PictureBox DivisoriaAcompanhamentoImagem;
        Label AcompanhamentoLabel;

        #region Cadastros
        Panel CategoriasPanel;
        PictureBox DivisoriaCategoriasImagem;
        Label CategoriasLabel;

        Panel LivrosPanel;
        PictureBox DivisoriaLivrosImagem;
        Label LivrosLabel;
        #endregion

        #endregion

        #region SubMenu Corridas
        Panel SubMenuCorridas;

        Panel CadastroCorridasPanel;
        PictureBox DivisoriaCadastroCorridasImagem;
        Label CadastroCorridasLabel;

        Panel ParticipantesPanel;
        PictureBox DivisoriaParticipantesImagem;
        Label ParticipantesLabel;

        Panel ResultadoPanel;
        PictureBox DivisoriaResultadoImagem;
        Label ResultadoLabel;
        #endregion

        #region Menu Juridico
        Panel SubMenuJuridico;
        Panel SubMenuCadastroJuridico;

        Panel CadastroJuridicoPanel;
        PictureBox DivisoriaCadastroJuridicoImagem;
        PictureBox Divisoria2CadastroJuridicoImagem;
        Label CadastroJuridicoLabel;
        Label CadastroJuridicoSetaLabel;

        Panel AbrirComunicadoPanel;
        PictureBox DivisoriaAbrirComunicadoImagem;
        Label AbrirComunicadoLabel;

        Panel TiposPagamentoPanel;
        PictureBox DivisoriaTiposPagamentoImagem;
        Label TiposPagamentoLabel;

        Panel VaraPanel;
        PictureBox DivisoriaVaraImagem;
        Label VaraLabel;

        #endregion

        #region Menu Contabilidade
        Panel SubMenuContabilidade;
        Panel SubMenuCTBArquivei;

        Panel IntegraNFSPanel;
        PictureBox DivisoriaIntegraNFSImagem;
        Label IntegraNFSLabel;

        Panel ArquiveiPanel;
        PictureBox DivisoriaArquiveiImagem;
        PictureBox Divisoria2ArquiveiImagem;
        Label ArquiveiLabel;
        Label ArquiveiSetaLabel;

        Panel ParametroArquiveiPanel;
        PictureBox DivisoriaParametroArquiveiImagem;
        Label ParametroArquiveiLabel;

        Panel CFOPArquiveiPanel;
        PictureBox DivisoriaCFOPArquiveiImagem;
        Label CFOPArquiveiLabel;


        Panel ValidacaoArquiveiPanel;
        PictureBox DivisoriaValidacaoArquiveiImagem;
        Label ValidacaoArquiveiLabel;

        #endregion

        #region Menu Atendimento
        Panel SubMenuAtendimento;

        Panel SACPanel;
        PictureBox DivisoriaSACImagem;
        PictureBox Divisoria2SACImagem;
        Label SACLabel;
        Label SACSetaLabel;

        Panel ChamadosPanel;
        PictureBox DivisoriaChamadosImagem;
        PictureBox Divisoria2ChamadosImagem;
        Label ChamadosLabel;
        Label ChamadosSetaLabel;

        #region SubMenu SAC
        Panel SubMenuSACPanel;
        Panel SubMenuCadastroSACPanel;

        Panel CadastroSACPanel;
        PictureBox DivisoriaCadastroSACImagem;
        PictureBox Divisoria2CadastroSACImagem;
        Label CadastroSACLabel;
        Label CadastroSACSetaLabel;

        Panel AtendimentoSACPanel;
        PictureBox DivisoriaAtendimentoSACImagem;
        Label AtendimentoSACLabel;

        Panel RetornarLigacoesPanel;
        PictureBox DivisoriaRetornarLigacoesImagem;
        Label RetornarLigacoesLabel;

        Panel ResponderAtendimentoPanel;
        PictureBox DivisoriaResponderAtendimentoImagem;
        Label ResponderAtendimentoLabel;

        Panel FinalizarAtendimentoPanel;
        PictureBox DivisoriaFinalizarAtendimentoImagem;
        Label FinalizarAtendimentoLabel;

        Panel SatisfacaoPanel;
        PictureBox DivisoriaSatisfacaoImagem;
        Label SatisfacaoLabel;

        #region CadastroSAC
        Panel TiposAtendimentoPanel;
        PictureBox DivisoriaTiposAtendimentoImagem;
        Label TiposAtendimentoLabel;

        Panel TiposAtendimentoEMTUPanel;
        PictureBox DivisoriaTiposAtendimentoEMTUImagem;
        Label TiposAtendimentoEMTULabel;
        #endregion
        #endregion

        #region SubMenu Chamados
        Panel SubMenuChamadosPanel;

        Panel AbrirChamadoPanel;
        PictureBox DivisoriaAbrirChamadoImagem;
        Label AbrirChamadoLabel;
        #endregion

        #endregion

        #region Menu Controladoria
        Panel SubMenuControladoria;

        Panel RadarBIPanel;
        PictureBox DivisoriaRadarBIImagem;
        Label RadarBILabel;
        #endregion

        #region Menu TI
        Panel MenuTIPanel;
        Panel SubMenuCadastroTIPanel;
        Panel SubMenuSistemasTIPanel;

        Panel CadastroTIPanel;
        PictureBox DivisoriaCadastroTIImagem;
        PictureBox Divisoria2CadastroTIImagem;
        Label CadastroTILabel;
        Label CadastroTISetaLabel;

        Panel SistemasTIPanel;
        PictureBox DivisoriaSistemasTIImagem;
        PictureBox Divisoria2SistemasTIImagem;
        Label SistemasTILabel;
        Label SistemasTISetaLabel;

        Panel UsuariosPanel;
        PictureBox DivisoriaUsuariosImagem;
        Label UsuariosLabel;

        Panel ParametrosPanel;
        PictureBox DivisoriaParametrosImagem;
        Label ParametrosLabel;

        Panel EmpresasPanel;
        PictureBox DivisoriaEmpresasImagem;
        Label EmpresasLabel;

        Panel SalaReuniaoPanel;
        PictureBox DivisoriaSalaReuniaoImagem;
        Label SalaReuniaoLabel;

        Panel NotificacoesPanel;
        PictureBox DivisoriaNotificacoesImagem;
        Label NotificacoesLabel;

        #region Sistemas
        Panel CategoriasTIPanel;
        PictureBox DivisoriaCategoriasTIImagem;
        Label CategoriasTILabel;

        Panel ModulosTIPanel;
        PictureBox DivisoriaModulosTIImagem;
        Label ModulosTILabel;

        Panel TelasTIPanel;
        PictureBox DivisoriaTelasTIImagem;
        Label TelasTILabel;

        #endregion

        #endregion
        #endregion

        #region Componentes de criação do DashBoard
        Panel ComunicadoDashBoardPanel;
        Panel FiltrosComunicadosDashBoardPanel;
        Label ListaComunicadosDashBoardLabel;
        Label ImagemListaLabel;
        Label AnoComunicadosDashBoardLabel;
        Label ImagemAnoLabel;
        Label MensagemComunicadosDashBoardLabel;
        ComboBoxAdv StatusComunicadosDashBoardComboBox;
        ComboBoxAdv AnoComunicadosDashBoardComboBox;
        GridGroupingControl ComunidadosDashBoardGrid;

        Panel ChamadosDashBoardPanel;
        Panel SacDashBoardPanel;


        ChartControl GraficoEmpresaChart;
        GridGroupingControl AvaliacaoRankingGrid;

        #endregion

        #region Componentes Criação Notificações
        Panel ExibeNotificacaoPanel;
        Panel TituloNotificacaoPanel;
        Label TituloNotificacaoLabel;
        Label Texto1Label;
        Label Texto2Label;
        Label Texto3Label;
        Label Texto4Label;
        Label Texto5Label;
        Label Texto6Label;
        PictureBox Imagem1Picture;
        PictureBox Imagem2Picture;
        GridGroupingControl NotificacaoGrid;

        #region Componentes Criação Bolão
        Panel MenuBolaoPanel;
        Panel SelecaoPanel;
        Panel JogosPanel;
        Panel ResultadoBolaoPanel;
        Panel PalpitesFinalistasPanel;
        Panel PalpitesPlacarPanel;
        Panel RankingBolaoPanel;
        Panel ValorArrecadadoPanel;

        Label SelecaoLabel;
        Label JogosLabel;
        Label ResultadoBolaoLabel;
        Label PalpitesFinalistasLabel;
        Label PalpitesPlacarLabel;
        Label RankingBolaoLabel;
        Label ValorArrecadadoLabel;

        PictureBox DivisoriaSelecaoImagem;
        PictureBox DivisoriaJogosImagem;
        PictureBox DivisoriaResultadoBolaoImagem;
        PictureBox DivisoriaPalpitesFinalistasImagem;
        PictureBox DivisoriaPalpitesPlacarImagem;
        PictureBox DivisoriaRankingBolaoImagem;
        PictureBox DivisoriaValorArrecadadoImagem;
        #endregion


        #endregion

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

        #region Controles principais
        private void dataHoraTimer_Tick(object sender, EventArgs e)
        {

            dataHoraLabel.Text = "| Maquina " + Environment.MachineName + "     | Versão de " + _dataCompilacao + " |       " +
                DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            mensagemLabel.Text = Publicas._mensagemSistema;

            QuantidadeNotificacaoLabel.Text = _quantidadeNotificacoesNaoLidas.ToString();

            if (_quantidadeNotificacoesNaoLidas != 0)
            {
                if (QuantidadeNotificacaoLabel.ForeColor == Color.Red)
                    QuantidadeNotificacaoLabel.ForeColor = BotaoDashboardLabel.ForeColor;
                else
                    QuantidadeNotificacaoLabel.ForeColor = Color.Red;
            }
            else
                QuantidadeNotificacaoLabel.ForeColor = BotaoDashboardLabel.ForeColor;

            _notificacao = new NotificacaoDoSistemaBO().Consultar();

            if (_notificacao.DataDaAcao <= DateTime.Now && _notificacao.DataFimDaAcao >= DateTime.Now)
            {
                if (contadorTempoEncerrarSistema == 0)
                {
                    FecharDashBoard();
                    notificacaoSistemaPanel.Dock = DockStyle.Right;
                    notificacaoSistemaPanel.BringToFront();
                    notificacaoSistemaPanel.Visible = true;
                    dataNotificacaoLabel.Text = _notificacao.DataDoAviso.ToShortDateString();
                    textoLabel.Text = _notificacao.Motivo.Replace("#datainicio", _notificacao.DataDaAcao.Date.ToShortDateString())
                                                         .Replace("#horainicio", _notificacao.DataDaAcao.ToShortTimeString())
                                                         .Replace("#tempo", _notificacao.DataFimDaAcao.Subtract(_notificacao.DataDaAcao).TotalMinutes.ToString() + " minutos");
                }

                contagemLabel.Text = (60 - contadorTempoEncerrarSistema).ToString();

                contadorTempoEncerrarSistema++;

                if (contadorTempoEncerrarSistema >= 60)
                {

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Encerrou o sistema após notificar o usuário.";
                    _log.Tela = "Principal";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                    Close();
                }
                return;
            }

            if (!string.IsNullOrEmpty(Publicas._mensagemSistema))
                this.Cursor = Cursors.WaitCursor;
            else
            {
                if (this.Cursor != Cursors.Default)
                    this.Cursor = Cursors.Default;
            }
        }

        private void TelaInicial_Shown(object sender, EventArgs e)
        {
            _versaoAplicativo = Application.ProductVersion;
            _dataCompilacao = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();
            NomePadrao();
            DashBoardPanel.Width = 25;

            #region Chama tela de login
            TelaLogin loginPanel = new TelaLogin();

            loginPanel.Location = new Point(loginPanel.Left + 100, (Screen.PrimaryScreen.WorkingArea.Size.Height - tituloPanel.Height - panel2.Height - loginPanel.Height) / 2);

            loginPanel.ShowDialog();
            #endregion

            if (Publicas._encerrarSemLogar)
                Close();

            if (Publicas._usuario != null)
            {
                #region Verificação de senha padrão
                if (Publicas._usuario.CPF != 0)
                {
                    if (Publicas._senhaLogin == Publicas._usuario.CPF.ToString().Substring(0, 6))
                    {
                        new Notificacoes.Mensagem("Identificamos que está usando a senha padrão." +
                            Environment.NewLine + "Por gentileza troque sua senha.", Publicas.TipoMensagem.Informacao).ShowDialog();
                        new TrocaDeSenha().ShowDialog();
                    }
                }
                #endregion

                #region atualização de status do usuário
                new LoginBO().AlterarStatusUsuario(Publicas._idUsuario, Publicas.StatusUsuario.OnLine, Publicas._conexaoString);
                #endregion

                #region  Identificação do usuário na tela inicial
                usuarioLogadoLabel.Text = "Olá," + Environment.NewLine + Publicas._usuariologado;

                #region imagem/foto usuario

                try
                {
                    if (!Directory.Exists(Publicas._caminhoPortal))
                    {
                        Directory.CreateDirectory(Publicas._caminhoPortal);
                    }

                    FileStream FS;

                    string _imagem = Publicas._caminhoPortal + "imagePerfil" + Publicas._usuario.Id + ".jpg";

                    if (!File.Exists(_imagem))
                    {
                        FS = new FileStream(_imagem, FileMode.CreateNew);
                        FS.Write(Publicas._usuario.Foto, 0, Publicas._usuario.Foto.Length);
                        FS.Close();
                        FS = null;
                        fotoUsuarioPictureBox.Image = Image.FromFile(_imagem);
                    }

                    fotoUsuarioPictureBox.Image = Image.FromFile(_imagem);

                    fotoUsuarioPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    fotoUsuarioPictureBox.Refresh();
                }
                catch
                {
                    fotoUsuarioPictureBox.Image = Properties.Resources.UserLogin;
                    fotoUsuarioPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                }

                #endregion
                #endregion

                #region Consultas de empresa e colaboradores
                _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);
                _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas._usuario.RegistroFuncionario, true);

                try
                {
                    Publicas._idColaborador = _colaboradores.Id;
                    _cargo = new CargosBO().Consultar(_colaboradores.IdCargo);

                    supervisor = new ColaboradoresBO().ConsultaSeEhSupervisor(_colaboradores.Id);
                }
                catch { }
                #endregion

                //AcessoAoMenuPanel.Visible = true;

                //Imagem1Picture fundo campeonato videogame
                List<PartidasDoTorneio> _listaPartidas = new TorneioBO().ListarPartidas(Publicas._idColaborador);
                
                foreach (var item in _listaPartidas.Where(w => w.Data == DateTime.Now.Date))
                {
                    _listaTorneios = new TorneioBO().ListarPartidas(item.IdTorneio, item.Data, item.NomePartida);

                    if (_colaboradores.Sexo == "F" && item.IdTorneio == 3)
                        FundoPictureBox.ImageLocation = @"Imagens\FundoArmsMeninas.png";
                    else
                    {
                        if (_colaboradores.Sexo == "M" && item.IdTorneio == 3)
                            FundoPictureBox.ImageLocation = @"Imagens\FundoArmsMisango.png";
                        else
                            FundoPictureBox.ImageLocation = @"Imagens\FundoComMarioKart.png";
                    }
                }

                //BolaoCopaPanel.Visible = Publicas._usuario.AdministraBolaoCopa || Publicas._usuario.Administrador ||
                //    ((DateTime.Now.Date >= Publicas._dataInicioCopa.AddDays(-14) && DateTime.Now.Date <= Publicas._dataFimCopa.AddDays(3))) &&
                //    Publicas._usuario.ParticipaBolaoCopa && (Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.UsuarioAcesso == "ESILVA" || Publicas._usuario.UsuarioAcesso == "VBSANTOS");

                int qtdDias = 1;

                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                    qtdDias = 3;

                if (Publicas._usuario.AniversariantesApenasDaEmpresa)
                    _listaAniversariantes = new UsuarioBO().ListarAniversariantesDaSemana(Publicas._usuario.IdEmpresa, qtdDias).OrderBy(o => o.DataNascimento).ToList();
                else
                    _listaAniversariantes = new UsuarioBO().ListarAniversariantesDaSemana(0, qtdDias).OrderBy(o => o.DataNascimento).ToList();

                _feriado = new FeriadoBO().Consultar(Publicas._usuario.IdEmpresa);

                //if (Publicas._usuario != null && !Publicas._usuario.NaoNotificaCorridas)
                //{
                //    _listaCorrida = new CorridasBO().Listar(true, Publicas._usuario.Id);
                //    _participante = new ParticipanteCorridaBO().Consultar(Publicas._idUsuario, 0);
                //}

                if (_feriado.Existe)
                    _quantidadeNotificacoesNaoLidas = 1;

                _quantidadeNotificacoesNaoLidas = _quantidadeNotificacoesNaoLidas + 1 + _listaAniversariantes.Count() + _listaPartidas.Where(w => w.Data == DateTime.Now.Date.AddDays(32)).Count(); //+
                                                                                                                   //(_participante != null && !_participante.Visualizado ? _listaCorrida.Count() : 0);

                AvaliacaoDesempenhoTabPage.TabVisible = false;  //(Publicas._usuario.AcessoDeRH || (Publicas._usuario.AcessoDeGestor && supervisor.Existe)) && Publicas._usuario.AcessaAvaliacaoDesempenho;
                ComunicadosTabPage.TabVisible = false; //Publicas._usuario.AcessaJuridico;
                SACTabPage.TabVisible = false;// Publicas._usuario.AcessaSac;

                DashBoardPanel.BringToFront();
                DashBoardPanel.Visible = true;

                //EnquetePictureBox.Visible = Publicas._usuario.AdministraBolaoCopa || Publicas._usuario.Administrador ||
                //        ((DateTime.Now.Date >= Publicas._dataInicioCopa.AddDays(-14) && DateTime.Now.Date <= Publicas._dataFimCopa.AddDays(3))) &&
                //        (Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.UsuarioAcesso == "ESILVA" || Publicas._usuario.UsuarioAcesso == "VBSANTOS" || Publicas._usuario.UsuarioAcesso == "ADRIANA");

                //BolaoPictureBox.Visible = EnquetePictureBox.Visible;

                //notificacaoTimer.Start();
                notificacaoBolaoTimer.Start();

                //if (!_arquiveiEmPesquisa)
                //  ArquiveiBackgroundWorker.RunWorkerAsync();

                //if (!_bibliotecaEmPesquisa)
                    //BibliotecaBackgroundWorker.RunWorkerAsync();

                #region Cor Grids
                metroColor.GroupDropAreaColor.BackColor = Publicas._bordaEntrada;
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
                #endregion

                #region Cria grid para o avaliação de desempenho


                #region Campo para o filtro

                List<Classes.Prazos> _prazos = new PrazosBO().Listar(true, true);

                TipoAvaliacaoComboBox.Items.AddRange(new object[] { "Auto Avaliação", "Feedback do Gestor", "Metas numéricas", "Avaliação do Gestor", "Avaliação do RH", "Feedback do Avaliado", "Plano de Desenvolvimento individual" });

                foreach (var item in _prazos)
                {
                    ReferenciaAvaliacaoComboBox.Items.Add(item.Referencia);
                }

                ReferenciaAvaliacaoComboBox.SelectedIndex = 0;
                TipoAvaliacaoComboBox.SelectedIndex = 0;

                #endregion


                #endregion

                if (!new BolaoEnqueteBO().Consultar(Publicas._idColaborador) && Publicas._usuario.IdEmpresa == 1 && EnquetePictureBox.Visible) 
                    EnquetePictureBox_Click(sender, new EventArgs());
            }
        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {
            Publicas.stringConexao = Publicas._conexaoString;
            Publicas._idSuperior = 0;

            #region Ajusta menu principal para não sobrepor a barra de ferramentas
            this.Left = 0;
            this.Width = 0;
            this.Top = 0;

            //obter o tamanho real de work do usuario
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            #endregion
        }

        private void FecharDashBoard()
        {
            DashBoardPanel.Size = new Size(25, 0);
            BotaoDashboardLabel.Text = "7";
            DashBordTabControl.Visible = false;

            if (!_fixaDashBoard)
            {
                if (ComunicadoDashBoardPanel != null)
                {
                    ComunicadosTabPage.Controls.Remove(ComunicadoDashBoardPanel);
                    ComunicadoDashBoardPanel.Dispose();
                }
            }
        }

        
        private void NomePadrao()
        {
            tituloSistemaLabel.Text = "Torneios"; //Publicas._nomeDoSistema;

            if (_fixaDashBoard)
            {
                DashBoardPanel.Size = new Size(this.Width - 5, 0);
                BotaoDashboardLabel.Text = "8";
                DashBordTabControl.Visible = true;
            }

            Publicas._mensagemSistema = "";
            Refresh();
        }
        #endregion

        #region Menu Usuário
        private void usuarioLogadoLabel_MouseHover(object sender, EventArgs e)
        {
            usuarioLogadoPanel.Cursor = Cursors.Hand;
            usuarioLogadoPanel.BackColor = Publicas._panelTituloFoco;
        }

        private void usuarioLogadoLabel_Click(object sender, EventArgs e)
        {
            FechaMenuSistema();
            FechaSubMenuBolao();

            if (MenuUsuarioPanel != null)
            {
                FechaMenuUsuario();
                MenuUsuarioPanel = null;
                return;
            }

            #region Cria estrutura do menu do usuario

            // Menu de fundo (onde agrupa os demais itens)
            MenuUsuarioPanel = new Panel();
            MenuUsuarioPanel.Size = new Size(usuarioLogadoPanel.Width, _heidthMenuPadrao * (Publicas._usuario != null ? 4 : 2));
            MenuUsuarioPanel.Location = new Point(usuarioLogadoPanel.Left, usuarioLogadoPanel.Height);
            MenuUsuarioPanel.BackColor = Color.Silver;

            #region Trocar Usuário
            TrocaUsuarioPanel = new Panel();
            TrocaUsuarioPanel.Size = new Size(_widthMenuPadrao, _heidthMenuPadrao);
            TrocaUsuarioPanel.Dock = DockStyle.Top;
            TrocaUsuarioPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            TrocaUsuarioPanel.Name = "TrocaUsuarioPanel";

            DivisoriaTrocaUsuarioImagem = new PictureBox();
            DivisoriaTrocaUsuarioImagem.Size = new Size(0, 2);
            DivisoriaTrocaUsuarioImagem.Dock = DockStyle.Bottom;
            DivisoriaTrocaUsuarioImagem.BackColor = Color.Silver;

            //TrocaUsuarioImagem = new PictureBox();
            //TrocaUsuarioImagem.Location = new Point(_leftImagemMenuPadrao, _topImagemMenuPadrao);
            //TrocaUsuarioImagem.Size = new Size(32, 32);
            //TrocaUsuarioImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            //TrocaUsuarioImagem.Image = Properties.Resources.refresh1;
            //TrocaUsuarioImagem.Name = "TrocaUsuarioImagem";
            //TrocaUsuarioImagem.Click += new System.EventHandler(this.TrocarUsuarioPanel_Click);
            //TrocaUsuarioImagem.MouseHover += new EventHandler(this.TrocaSenhaPanel_MouseHover);

            TrocaUsuarioLabel = new Label();
            TrocaUsuarioLabel.AutoSize = false;
            TrocaUsuarioLabel.Size = new Size(_widthLabelMenu, 0);
            TrocaUsuarioLabel.Dock = DockStyle.Fill;
            TrocaUsuarioLabel.Text = "Trocar de Usuário";
            TrocaUsuarioLabel.Font = new Font(dataHoraLabel.Font.FontFamily, (float)12, dataHoraLabel.Font.Style);
            TrocaUsuarioLabel.ForeColor = Color.WhiteSmoke;
            TrocaUsuarioLabel.TextAlign = ContentAlignment.MiddleLeft;
            TrocaUsuarioLabel.Click += new System.EventHandler(this.TrocarUsuarioPanel_Click);
            TrocaUsuarioLabel.MouseHover += new EventHandler(this.TrocaSenhaPanel_MouseHover);
            TrocaUsuarioLabel.Name = "TrocaUsuarioLabel";

            TrocaUsuarioPanel.Controls.Add(TrocaUsuarioLabel);
            TrocaUsuarioPanel.Controls.Add(DivisoriaTrocaUsuarioImagem);

            MenuUsuarioPanel.Controls.Add(TrocaUsuarioPanel);
            #endregion

            #region Sair
            SairPanel = new Panel();
            SairPanel.Size = new Size(_widthMenuPadrao, _heidthMenuPadrao);
            SairPanel.Dock = DockStyle.Top;
            SairPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            SairPanel.Name = "SairPanel";

            DivisoriaSairImagem = new PictureBox();
            DivisoriaSairImagem.Size = new Size(0, 2);
            DivisoriaSairImagem.Dock = DockStyle.Bottom;
            DivisoriaSairImagem.BackColor = Color.Silver;

            //SairImagem = new PictureBox();
            //SairImagem.Location = new Point(_leftImagemMenuPadrao, _topImagemMenuPadrao);
            //SairImagem.Size = new Size(32, 32);
            //SairImagem.SizeMode = PictureBoxSizeMode.StretchImage;
            //SairImagem.Image = Properties.Resources.Power;
            //SairImagem.Name = "SairImagem";
            //SairImagem.Click += new System.EventHandler(this.SairPanel_Click);
            //SairImagem.MouseHover += new EventHandler(this.TrocaSenhaPanel_MouseHover);

            SairLabel = new Label();
            SairLabel.AutoSize = false;
            SairLabel.Size = new Size(_widthLabelMenu, 0);
            SairLabel.Dock = DockStyle.Fill;
            SairLabel.Text = "Sair";
            SairLabel.Font = new Font(dataHoraLabel.Font.FontFamily, (float)12, dataHoraLabel.Font.Style);
            SairLabel.ForeColor = Color.WhiteSmoke;
            SairLabel.TextAlign = ContentAlignment.MiddleLeft;
            SairLabel.Click += new System.EventHandler(this.SairPanel_Click);
            SairLabel.MouseHover += new EventHandler(this.TrocaSenhaPanel_MouseHover);
            SairLabel.Name = "SairLabel";

            SairPanel.Controls.Add(SairLabel);
            SairPanel.Controls.Add(DivisoriaSairImagem);

            MenuUsuarioPanel.Controls.Add(SairPanel);
            #endregion

            if (Publicas._usuario != null)
            {
                #region Editar perfil
                EditaPerfilPanel = new Panel();
                EditaPerfilPanel.Size = new Size(_widthMenuPadrao, _heidthMenuPadrao);
                EditaPerfilPanel.Dock = DockStyle.Top;
                EditaPerfilPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
                EditaPerfilPanel.Name = "EditaPerfilPanel";

                DivisoriaEditaPerfilImagem = new PictureBox();
                DivisoriaEditaPerfilImagem.Size = new Size(0, 2);
                DivisoriaEditaPerfilImagem.Dock = DockStyle.Bottom;
                DivisoriaEditaPerfilImagem.BackColor = Color.Silver;

                //EditaPerfilImagem = new PictureBox();
                //EditaPerfilImagem.Location = new Point(_leftImagemMenuPadrao, _topImagemMenuPadrao);
                //EditaPerfilImagem.Size = new Size(32, 32);
                //EditaPerfilImagem.SizeMode = PictureBoxSizeMode.StretchImage;
                //EditaPerfilImagem.Image = Properties.Resources.EditUser;
                //EditaPerfilImagem.Name = "EditaPerfilImagem";
                //EditaPerfilImagem.Click += new System.EventHandler(this.EditarPerfilPanel_Click);
                //EditaPerfilImagem.MouseHover += new EventHandler(this.TrocaSenhaPanel_MouseHover);

                EditaPerfilLabel = new Label();
                EditaPerfilLabel.AutoSize = false;
                EditaPerfilLabel.Size = new Size(_widthLabelMenu, 0);
                EditaPerfilLabel.Dock = DockStyle.Fill;
                EditaPerfilLabel.Text = "Editar Perfil";
                EditaPerfilLabel.Font = new Font(dataHoraLabel.Font.FontFamily, (float)12, dataHoraLabel.Font.Style);
                EditaPerfilLabel.ForeColor = Color.WhiteSmoke;
                EditaPerfilLabel.TextAlign = ContentAlignment.MiddleLeft;
                EditaPerfilLabel.Click += new System.EventHandler(this.EditarPerfilPanel_Click);
                EditaPerfilLabel.MouseHover += new EventHandler(this.TrocaSenhaPanel_MouseHover);
                EditaPerfilLabel.Name = "EditaPerfilLabel";

                #region Adiciona campos ao menu trocasenha e ao grupo principal
                EditaPerfilPanel.Controls.Add(EditaPerfilLabel);
                //EditaPerfilPanel.Controls.Add(EditaPerfilImagem);
                EditaPerfilPanel.Controls.Add(DivisoriaEditaPerfilImagem);
                //EditaPerfilImagem.SendToBack();

                MenuUsuarioPanel.Controls.Add(EditaPerfilPanel);
                #endregion
                #endregion

                #region Trocar Senha
                TrocaSenhaPanel = new Panel();
                TrocaSenhaPanel.Size = new Size(_widthMenuPadrao, _heidthMenuPadrao);
                TrocaSenhaPanel.Dock = DockStyle.Top;
                TrocaSenhaPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
                TrocaSenhaPanel.Name = "TrocaSenhaPanel";

                DivisoriaTrocaSenhaImagem = new PictureBox();
                DivisoriaTrocaSenhaImagem.Size = new Size(0, 2);
                DivisoriaTrocaSenhaImagem.Dock = DockStyle.Bottom;
                DivisoriaTrocaSenhaImagem.BackColor = Color.Silver;

                //TrocaSenhaImagem = new PictureBox();
                //TrocaSenhaImagem.Location = new Point(_leftImagemMenuPadrao, _topImagemMenuPadrao);
                //TrocaSenhaImagem.Size = new Size(32, 32);
                //TrocaSenhaImagem.SizeMode = PictureBoxSizeMode.StretchImage;
                //TrocaSenhaImagem.Image = Properties.Resources.TrocaSenha1;
                //TrocaSenhaImagem.Name = "TrocaSenhaImagem";
                //TrocaSenhaImagem.Click += new System.EventHandler(this.TrocaSenhaPanel_Click);
                //TrocaSenhaImagem.MouseHover += new EventHandler(this.TrocaSenhaPanel_MouseHover);

                TrocaSenhaLabel = new Label();
                TrocaSenhaLabel.AutoSize = false;
                TrocaSenhaLabel.Size = new Size(_widthLabelMenu, 0);
                TrocaSenhaLabel.Dock = DockStyle.Fill;
                TrocaSenhaLabel.Text = "Trocar senha";
                TrocaSenhaLabel.Font = new Font(dataHoraLabel.Font.FontFamily, (float)12, dataHoraLabel.Font.Style);
                TrocaSenhaLabel.TextAlign = ContentAlignment.MiddleLeft;
                TrocaSenhaLabel.ForeColor = Color.WhiteSmoke;

                TrocaSenhaLabel.Click += new System.EventHandler(this.TrocaSenhaPanel_Click);
                TrocaSenhaLabel.MouseHover += new EventHandler(this.TrocaSenhaPanel_MouseHover);
                TrocaSenhaLabel.Name = "TrocaSenhaLabel";

                #region Adiciona campos ao menu trocasenha e ao grupo principal
                TrocaSenhaPanel.Controls.Add(TrocaSenhaLabel);
                //TrocaSenhaPanel.Controls.Add(TrocaSenhaImagem);
                TrocaSenhaPanel.Controls.Add(DivisoriaTrocaSenhaImagem);
                //TrocaSenhaImagem.SendToBack();

                MenuUsuarioPanel.Controls.Add(TrocaSenhaPanel);
                #endregion
                #endregion
            }

            this.Controls.Add(MenuUsuarioPanel);
            MenuUsuarioPanel.BringToFront();
            MenuUsuarioPanel.Visible = true;

            #endregion
        }

        private void TrocaSenhaPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresMenuUsuario();

            #region subMenu usuario
            if (((Control)sender).Name.Contains("TrocaSenha"))
            {
                TrocaSenhaLabel.Font = new Font(TrocaSenhaLabel.Font, FontStyle.Bold);
                TrocaSenhaLabel.ForeColor = Publicas._fonteBotaoFocado;
                TrocaSenhaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("EditaPerfil"))
            {
                EditaPerfilLabel.Font = new Font(EditaPerfilLabel.Font, FontStyle.Bold);
                EditaPerfilLabel.ForeColor = Publicas._fonteBotaoFocado;
                EditaPerfilLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Sair"))
            {
                SairLabel.Font = new Font(SairLabel.Font, FontStyle.Bold);
                SairLabel.ForeColor = Publicas._fonteBotaoFocado;
                SairLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("TrocaUsuario"))
            {
                TrocaUsuarioLabel.Font = new Font(TrocaUsuarioLabel.Font, FontStyle.Bold);
                TrocaUsuarioLabel.ForeColor = Publicas._fonteBotaoFocado;
                TrocaUsuarioLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            #endregion
        }

        private void FechaMenuUsuario()
        {
            FecharDashBoard();

            if (MenuUsuarioPanel != null)
            {
                MenuUsuarioPanel.Visible = false;
                this.Controls.Remove(MenuUsuarioPanel);
                MenuUsuarioPanel.Dispose();
                MenuUsuarioPanel = null;
            }
        }

        private void TrocaSenhaPanel_Click(object sender, EventArgs e)
        {
            MensagemSistema();
            tituloSistemaLabel.Text = "Bolão da copa do mundo 2018";// Publicas._nomeDoSistema;
            FechaMenuUsuario();
            new Cadastros.TrocaDeSenha().ShowDialog();
            NomePadrao();
        }

        private void EditarPerfilPanel_Click(object sender, EventArgs e)
        {
            MensagemSistema();
            tituloSistemaLabel.Text = "Bolão da copa do mundo 2018";//Publicas._nomeDoSistema;
            FechaMenuUsuario();

            new Cadastros.EditarPerfil().ShowDialog();
            NomePadrao();

            usuarioLogadoLabel.Text = "Olá," + Environment.NewLine + Publicas._usuariologado;

            try
            {
                //FileStream FS = new FileStream("image.jpg", FileMode.OpenOrCreate);
                //FS.Write(Publicas._usuario.Foto, 0, Publicas._usuario.Foto.Length);
                //FS.Close();
                //FS = null;

                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(Publicas._usuario.Foto, 0, Publicas._usuario.Foto.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    fotoUsuarioPictureBox.Image = new Bitmap(mStream);
                    mStream.Close();
                    mStream.Dispose();
                }

                fotoUsuarioPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                fotoUsuarioPictureBox.Refresh();
            }
            catch
            {
                fotoUsuarioPictureBox.Image = Properties.Resources.UserLogin;
                fotoUsuarioPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            }

            BolaoCopaPanel.Visible = Publicas._usuario.AdministraBolaoCopa || Publicas._usuario.Administrador ||
                        ((DateTime.Now.Date >= Publicas._dataInicioCopa.AddDays(-14) && DateTime.Now.Date <= Publicas._dataFimCopa.AddDays(3))) &&
                        Publicas._usuario.ParticipaBolaoCopa && (Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.UsuarioAcesso == "ESILVA" || Publicas._usuario.UsuarioAcesso == "VBSANTOS");

        }

        private void SairPanel_Click(object sender, EventArgs e)
        {
            if (Publicas._idUsuario != 0)
                new LoginBO().AlterarStatusUsuario(Publicas._idUsuario, Publicas.StatusUsuario.OffLine, Publicas._conexaoString);

            Log _log = new Log();

            _log.Descricao = "Encerrou o sistema sem efetuar login";

            if (Publicas._idUsuario != 0)
            {
                _log.IdUsuario = Publicas._usuario.Id;
                _log.Descricao = "Usuário efetuou o logoff";
            }

            _log.Tela = "Principal";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            Close();
        }

        private void TrocarUsuarioPanel_Click(object sender, EventArgs e)
        {
            _quantidadeNotificacoesNaoLidas = 0;

            DashBoardPanel.Visible = false;
            notificacaoBolaoTimer.Stop();
            notificacaoTimer.Stop();

            FechaMenuSistema();
            FechaMenuUsuario();

            new LoginBO().AlterarStatusUsuario(Publicas._idUsuario, Publicas.StatusUsuario.OffLine, Publicas._conexaoString);

            Publicas._idUsuario = 0;
            Publicas._usuario = null;
            Publicas._mensagemSistema = string.Empty;
            this.Cursor = Cursors.Default;

            usuarioLogadoLabel.Text = "Olá," + Environment.NewLine + "Efetue seu login";

            fotoUsuarioPictureBox.Image = Properties.Resources.UserLogin;
            fotoUsuarioPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            #region Chama tela de login
            NomePadrao();
            this.TelaInicial_Shown(sender, new EventArgs());
            #endregion

        }

        private void MudaSelecaoDeCoresMenuUsuario()
        {
            TrocaSenhaLabel.Font = new Font(TrocaSenhaLabel.Font, TrocaSenhaLabel.Font.Style & ~FontStyle.Bold);
            EditaPerfilLabel.Font = new Font(EditaPerfilLabel.Font, EditaPerfilLabel.Font.Style & ~FontStyle.Bold);
            SairLabel.Font = new Font(SairLabel.Font, SairLabel.Font.Style & ~FontStyle.Bold);
            TrocaUsuarioLabel.Font = new Font(TrocaUsuarioLabel.Font, TrocaUsuarioLabel.Font.Style & ~FontStyle.Bold);

            TrocaSenhaLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            EditaPerfilLabel.BackColor = TrocaSenhaPanel.BackColor;
            SairLabel.BackColor = TrocaSenhaPanel.BackColor;
            TrocaUsuarioLabel.BackColor = TrocaSenhaPanel.BackColor;

            TrocaSenhaLabel.ForeColor = Color.WhiteSmoke;
            EditaPerfilLabel.ForeColor = Color.WhiteSmoke;
            SairLabel.ForeColor = Color.WhiteSmoke;
            TrocaUsuarioLabel.ForeColor = Color.WhiteSmoke;
        }

        private void usuarioLogadoLabel_MouseLeave(object sender, EventArgs e)
        {
            usuarioLogadoLabel.Cursor = Cursors.Default;
            usuarioLogadoPanel.BackColor = Publicas._panelTitulo;
        }

        #endregion

        #region Criação da estrutura do menu e subMenus

        private void FechaMenuSistema()
        {
            MostraNotificacoesPanel.Visible = false;
            FecharDashBoard();
            FechaSubMenuRecursosHumanos();
            FechaSubMenuJuridico();
            FechaSubMenuContabilidade();
            FechaSubMenuControladoria();
            FechaSubMenuAtendimento();
            FechaSubMenuTI();

            if (MenuSistemaPanel != null)
            {
                MenuSistemaPanel.Visible = false;
                this.Controls.Remove(MenuSistemaPanel);
                MenuSistemaPanel.Dispose();
                MenuSistemaPanel = null;
            }
            Refresh();
        }

        private void MudaSelecaoDeCoresMenuSistema()
        {
            ControladoriaLabel.Font = new Font(ControladoriaLabel.Font, ControladoriaLabel.Font.Style & ~FontStyle.Bold);
            ContabilidadeLabel.Font = new Font(ContabilidadeLabel.Font, ContabilidadeLabel.Font.Style & ~FontStyle.Bold);
            RecursosHumanosLabel.Font = new Font(RecursosHumanosLabel.Font, RecursosHumanosLabel.Font.Style & ~FontStyle.Bold);
            AtendimentoLabel.Font = new Font(AtendimentoLabel.Font, AtendimentoLabel.Font.Style & ~FontStyle.Bold);
            JuridicoLabel.Font = new Font(JuridicoLabel.Font, JuridicoLabel.Font.Style & ~FontStyle.Bold);
            TILabel.Font = new Font(TILabel.Font, TILabel.Font.Style & ~FontStyle.Bold);

            ControladoriaLabel.ForeColor = Color.WhiteSmoke;
            ControladoriaSetaLabel.ForeColor = Color.WhiteSmoke;
            ContabilidadeLabel.ForeColor = Color.WhiteSmoke;
            ContabilidadeSetaLabel.ForeColor = Color.WhiteSmoke;
            RecursosHumanosLabel.ForeColor = Color.WhiteSmoke;
            RecursosHumanosSetaLabel.ForeColor = Color.WhiteSmoke;
            AtendimentoLabel.ForeColor = Color.WhiteSmoke;
            AtendimentoSetaLabel.ForeColor = Color.WhiteSmoke;
            JuridicoLabel.ForeColor = Color.WhiteSmoke;
            JuridicoSetaLabel.ForeColor = Color.WhiteSmoke;
            TILabel.ForeColor = Color.WhiteSmoke;
            TISetaLabel.ForeColor = Color.WhiteSmoke;

            RecursosHumanosSetaLabel.Text = "6";
            ControladoriaSetaLabel.Text = "6";
            JuridicoSetaLabel.Text = "6";
            AtendimentoSetaLabel.Text = "6";
            ContabilidadeSetaLabel.Text = "6";
            TISetaLabel.Text = "6";

            ControladoriaLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ContabilidadeLabel.BackColor = ControladoriaPanel.BackColor;
            RecursosHumanosLabel.BackColor = ControladoriaPanel.BackColor;
            AtendimentoLabel.BackColor = ControladoriaPanel.BackColor;
            JuridicoLabel.BackColor = ControladoriaPanel.BackColor;
            TILabel.BackColor = ControladoriaPanel.BackColor;

            ControladoriaSetaLabel.BackColor = ControladoriaPanel.BackColor;
            ContabilidadeSetaLabel.BackColor = ControladoriaPanel.BackColor;
            RecursosHumanosSetaLabel.BackColor = ControladoriaPanel.BackColor;
            AtendimentoSetaLabel.BackColor = ControladoriaPanel.BackColor;
            JuridicoSetaLabel.BackColor = ControladoriaPanel.BackColor;
            TISetaLabel.BackColor = ControladoriaPanel.BackColor;
        }

        private void AcessoAoMenuSeta_MouseHover(object sender, EventArgs e)
        {
            AcessoAoMenuPanel.Cursor = Cursors.Hand;
            AcessoAoMenuPanel.BackColor = Publicas._panelTituloFoco;
            AcessoAoMenuLabel.Font = new Font(AcessoAoMenuLabel.Font, FontStyle.Bold);
        }

        private void AcessoAoMenuSeta_MouseLeave(object sender, EventArgs e)
        {
            AcessoAoMenuPanel.BackColor = Publicas._panelTitulo;
            AcessoAoMenuLabel.Font = new Font(AcessoAoMenuLabel.Font, AcessoAoMenuLabel.Font.Style & ~FontStyle.Bold);
        }

        private void AcessoAoMenuSeta_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaSubMenuBolao();
            FechaSubMenuRecursosHumanos();
            FechaSubMenuJuridico();
            FechaSubMenuContabilidade();
            FechaSubMenuControladoria();
            FechaSubMenuAtendimento();
            FechaSubMenuTI();

            if (MenuSistemaPanel != null)
            {
                FechaMenuSistema();
                return;
            }

            if (Publicas._usuario == null)
                return;

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            MenuSistemaPanel = new Panel();
            MenuSistemaPanel.Size = new Size(AcessoAoMenuPanel.Width, _heidthMenuSistema * 6);
            MenuSistemaPanel.Location = new Point(AcessoAoMenuPanel.Left, AcessoAoMenuPanel.Height);
            MenuSistemaPanel.BackColor = Color.Silver;
            this.Controls.Add(MenuSistemaPanel);
            MenuSistemaPanel.BringToFront();
            MenuSistemaPanel.Visible = true;
            #endregion

            // Os ultimos SubMenus deve ser incluidos primeiros

            #region TI
            TIPanel = new Panel();
            TIPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            TIPanel.Dock = DockStyle.Top;
            TIPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            TIPanel.Name = "TIPanel";
            TIPanel.Enabled = Publicas._usuario.Administrador;

            DivisoriaTIImagem = new PictureBox();
            DivisoriaTIImagem.Size = new Size(0, 4);
            DivisoriaTIImagem.Dock = DockStyle.Bottom;
            DivisoriaTIImagem.BackColor = Color.Silver;

            Divisoria2TIImagem = new PictureBox();
            Divisoria2TIImagem.Size = new Size(1, 2);
            Divisoria2TIImagem.Dock = DockStyle.Right;
            Divisoria2TIImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            TISetaLabel = new Label();
            TISetaLabel.AutoSize = false;
            TISetaLabel.Size = new Size(20, 59);
            TISetaLabel.Dock = DockStyle.Right;
            TISetaLabel.Text = "6";
            TISetaLabel.Font = new Font("Webdings", (float)12);
            TISetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            TISetaLabel.MouseHover += new EventHandler(this.RecursosHumanosSetaLabel_MouseHover);
            TISetaLabel.Click += new System.EventHandler(this.TISetaLabel_Click);
            TISetaLabel.Name = "TISetaLabel";
            TISetaLabel.ForeColor = Color.WhiteSmoke;

            TILabel = new Label();
            TILabel.AutoSize = false;
            TILabel.Size = new Size(_widthLabelMenuSistema, 0);
            TILabel.Dock = DockStyle.Fill;
            TILabel.Text = "Tecnologia da Informação";
            TILabel.Font = CorFontepadraoLabel.Font;
            TILabel.ForeColor = Color.WhiteSmoke;
            TILabel.TextAlign = ContentAlignment.MiddleRight;
            TILabel.MouseHover += new EventHandler(this.RecursosHumanosPanel_MouseHover);
            TILabel.Name = "TILabel";

            TIPanel.Controls.Add(TILabel);
            TIPanel.Controls.Add(Divisoria2TIImagem);
            TIPanel.Controls.Add(TISetaLabel);
            TIPanel.Controls.Add(DivisoriaTIImagem);

            MenuSistemaPanel.Controls.Add(TIPanel);
            #endregion

            #region Controladoria
            ControladoriaPanel = new Panel();
            ControladoriaPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ControladoriaPanel.Dock = DockStyle.Top;
            ControladoriaPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ControladoriaPanel.Enabled = Publicas._usuario.AcessaBI;
            ControladoriaPanel.Name = "ControladoriaPanel";

            DivisoriaControladoriaImagem = new PictureBox();
            DivisoriaControladoriaImagem.Size = new Size(0, 4);
            DivisoriaControladoriaImagem.Dock = DockStyle.Bottom;
            DivisoriaControladoriaImagem.BackColor = Color.Silver;

            Divisoria2ControladoriaImagem = new PictureBox();
            Divisoria2ControladoriaImagem.Size = new Size(1, 2);
            Divisoria2ControladoriaImagem.Dock = DockStyle.Right;
            Divisoria2ControladoriaImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            ControladoriaSetaLabel = new Label();
            ControladoriaSetaLabel.AutoSize = false;
            ControladoriaSetaLabel.Size = new Size(20, 59);
            ControladoriaSetaLabel.Dock = DockStyle.Right;
            ControladoriaSetaLabel.Text = "6";
            ControladoriaSetaLabel.Font = new Font("Webdings", (float)12);
            ControladoriaSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            ControladoriaSetaLabel.MouseHover += new EventHandler(this.RecursosHumanosSetaLabel_MouseHover);
            ControladoriaSetaLabel.Click += new System.EventHandler(this.ControladoriaSetaLabel_Click);
            ControladoriaSetaLabel.Name = "ControladoriaSetaLabel";
            ControladoriaSetaLabel.ForeColor = Color.WhiteSmoke;

            ControladoriaLabel = new Label();
            ControladoriaLabel.AutoSize = false;
            ControladoriaLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ControladoriaLabel.Dock = DockStyle.Fill;
            ControladoriaLabel.Text = "Controladoria";
            ControladoriaLabel.Font = CorFontepadraoLabel.Font;
            ControladoriaLabel.ForeColor = Color.WhiteSmoke;
            ControladoriaLabel.TextAlign = ContentAlignment.MiddleRight;
            ControladoriaLabel.MouseHover += new EventHandler(this.RecursosHumanosPanel_MouseHover);
            ControladoriaLabel.Name = "ControladoriaLabel";

            ControladoriaPanel.Controls.Add(ControladoriaLabel);
            ControladoriaPanel.Controls.Add(Divisoria2ControladoriaImagem);
            ControladoriaPanel.Controls.Add(ControladoriaSetaLabel);
            ControladoriaPanel.Controls.Add(DivisoriaControladoriaImagem);

            MenuSistemaPanel.Controls.Add(ControladoriaPanel);
            #endregion

            #region Atendimento
            AtendimentoPanel = new Panel();
            AtendimentoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AtendimentoPanel.Dock = DockStyle.Top;
            AtendimentoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AtendimentoPanel.Name = "AtendimentoPanel";

            DivisoriaAtendimentoImagem = new PictureBox();
            DivisoriaAtendimentoImagem.Size = new Size(0, 4);
            DivisoriaAtendimentoImagem.Dock = DockStyle.Bottom;
            DivisoriaAtendimentoImagem.BackColor = Color.Silver;

            Divisoria2AtendimentoImagem = new PictureBox();
            Divisoria2AtendimentoImagem.Size = new Size(1, 2);
            Divisoria2AtendimentoImagem.Dock = DockStyle.Right;
            Divisoria2AtendimentoImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            AtendimentoSetaLabel = new Label();
            AtendimentoSetaLabel.AutoSize = false;
            AtendimentoSetaLabel.Size = new Size(20, 59);
            AtendimentoSetaLabel.Dock = DockStyle.Right;
            AtendimentoSetaLabel.Text = "6";
            AtendimentoSetaLabel.Font = new Font("Webdings", (float)12);
            AtendimentoSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            AtendimentoSetaLabel.Click += new System.EventHandler(this.AtendimentoSetaLabel_Click);
            AtendimentoSetaLabel.MouseHover += new EventHandler(this.RecursosHumanosSetaLabel_MouseHover);
            AtendimentoSetaLabel.Name = "AtendimentoSetaLabel";
            AtendimentoSetaLabel.ForeColor = Color.WhiteSmoke;

            AtendimentoLabel = new Label();
            AtendimentoLabel.AutoSize = false;
            AtendimentoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AtendimentoLabel.Dock = DockStyle.Fill;
            AtendimentoLabel.Text = "Atendimento";
            AtendimentoLabel.Font = CorFontepadraoLabel.Font;
            AtendimentoLabel.ForeColor = Color.WhiteSmoke;
            AtendimentoLabel.TextAlign = ContentAlignment.MiddleRight;
            AtendimentoLabel.MouseHover += new EventHandler(this.RecursosHumanosPanel_MouseHover);
            AtendimentoLabel.Name = "AtendimentoLabel";

            AtendimentoPanel.Controls.Add(AtendimentoLabel);
            //AtendimentoPanel.Controls.Add(AtendimentoImagem);
            AtendimentoPanel.Controls.Add(Divisoria2AtendimentoImagem);
            AtendimentoPanel.Controls.Add(AtendimentoSetaLabel);
            AtendimentoPanel.Controls.Add(DivisoriaAtendimentoImagem);

            MenuSistemaPanel.Controls.Add(AtendimentoPanel);
            #endregion

            #region Contabilidade
            ContabilidadePanel = new Panel();
            ContabilidadePanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ContabilidadePanel.Dock = DockStyle.Top;
            ContabilidadePanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ContabilidadePanel.Name = "ContabilidadePanel";
            ContabilidadePanel.Enabled = Publicas._usuario.Administrador ||
                Publicas._usuario.Departamento.ToUpper().Contains("Contabilidade".ToUpper()) ||
                Publicas._usuario.Departamento.ToUpper().Contains("Fiscal".ToUpper());

            DivisoriaContabilidadeImagem = new PictureBox();
            DivisoriaContabilidadeImagem.Size = new Size(0, 4);
            DivisoriaContabilidadeImagem.Dock = DockStyle.Bottom;
            DivisoriaContabilidadeImagem.BackColor = Color.Silver;

            Divisoria2ContabilidadeImagem = new PictureBox();
            Divisoria2ContabilidadeImagem.Size = new Size(1, 2);
            Divisoria2ContabilidadeImagem.Dock = DockStyle.Right;
            Divisoria2ContabilidadeImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            ContabilidadeSetaLabel = new Label();
            ContabilidadeSetaLabel.AutoSize = false;
            ContabilidadeSetaLabel.Size = new Size(20, 59);
            ContabilidadeSetaLabel.Dock = DockStyle.Right;
            ContabilidadeSetaLabel.Text = "6";
            ContabilidadeSetaLabel.Font = new Font("Webdings", (float)12);
            ContabilidadeSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            ContabilidadeSetaLabel.Click += new System.EventHandler(this.ContabilidadeSetaLabel_Click);
            ContabilidadeSetaLabel.MouseHover += new EventHandler(this.RecursosHumanosSetaLabel_MouseHover);
            ContabilidadeSetaLabel.Name = "ContabilidadeSetaLabel";
            ContabilidadeSetaLabel.ForeColor = Color.WhiteSmoke;

            ContabilidadeLabel = new Label();
            ContabilidadeLabel.AutoSize = false;
            ContabilidadeLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ContabilidadeLabel.Dock = DockStyle.Fill;
            ContabilidadeLabel.Text = "Contabilidade";
            ContabilidadeLabel.Font = CorFontepadraoLabel.Font;
            ContabilidadeLabel.ForeColor = Color.WhiteSmoke;
            ContabilidadeLabel.TextAlign = ContentAlignment.MiddleRight;
            ContabilidadeLabel.MouseHover += new EventHandler(this.RecursosHumanosPanel_MouseHover);
            ContabilidadeLabel.Name = "ContabilidadeLabel";

            ContabilidadePanel.Controls.Add(ContabilidadeLabel);
            //ContabilidadePanel.Controls.Add(ContabilidadeImagem);
            ContabilidadePanel.Controls.Add(Divisoria2ContabilidadeImagem);
            ContabilidadePanel.Controls.Add(ContabilidadeSetaLabel);
            ContabilidadePanel.Controls.Add(DivisoriaContabilidadeImagem);

            MenuSistemaPanel.Controls.Add(ContabilidadePanel);
            #endregion

            #region Juridico
            JuridicoPanel = new Panel();
            JuridicoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            JuridicoPanel.Dock = DockStyle.Top;
            JuridicoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            JuridicoPanel.Name = "JuridicoPanel";
            JuridicoPanel.Enabled = Publicas._usuario.AcessaJuridico;

            DivisoriaJuridicoImagem = new PictureBox();
            DivisoriaJuridicoImagem.Size = new Size(0, 4);
            DivisoriaJuridicoImagem.Dock = DockStyle.Bottom;
            DivisoriaJuridicoImagem.BackColor = Color.Silver;

            Divisoria2JuridicoImagem = new PictureBox();
            Divisoria2JuridicoImagem.Size = new Size(1, 2);
            Divisoria2JuridicoImagem.Dock = DockStyle.Right;
            Divisoria2JuridicoImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            JuridicoSetaLabel = new Label();
            JuridicoSetaLabel.AutoSize = false;
            JuridicoSetaLabel.Size = new Size(20, 59);
            JuridicoSetaLabel.Dock = DockStyle.Right;
            JuridicoSetaLabel.Text = "6";
            JuridicoSetaLabel.Font = new Font("Webdings", (float)12);
            JuridicoSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            JuridicoSetaLabel.Click += new System.EventHandler(this.JuridicoSetaLabel_Click);
            JuridicoSetaLabel.MouseHover += new EventHandler(this.RecursosHumanosSetaLabel_MouseHover);
            JuridicoSetaLabel.Name = "JuridicoSetaLabel";
            JuridicoSetaLabel.ForeColor = Color.WhiteSmoke;

            JuridicoLabel = new Label();
            JuridicoLabel.AutoSize = false;
            JuridicoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            JuridicoLabel.Dock = DockStyle.Fill;
            JuridicoLabel.Text = "Juridico";
            JuridicoLabel.Font = CorFontepadraoLabel.Font;
            JuridicoLabel.ForeColor = Color.WhiteSmoke;
            JuridicoLabel.TextAlign = ContentAlignment.MiddleRight;
            JuridicoLabel.MouseHover += new EventHandler(this.RecursosHumanosPanel_MouseHover);
            JuridicoLabel.Name = "JuridicoLabel";

            JuridicoPanel.Controls.Add(JuridicoLabel);
            //JuridicoPanel.Controls.Add(JuridicoImagem);
            JuridicoPanel.Controls.Add(Divisoria2JuridicoImagem);
            JuridicoPanel.Controls.Add(JuridicoSetaLabel);
            JuridicoPanel.Controls.Add(DivisoriaJuridicoImagem);

            MenuSistemaPanel.Controls.Add(JuridicoPanel);
            #endregion

            #region RecursosHumanos
            RecursosHumanosPanel = new Panel();
            RecursosHumanosPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            RecursosHumanosPanel.Dock = DockStyle.Top;
            RecursosHumanosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            RecursosHumanosPanel.Name = "RecursosHumanosPanel";

            DivisoriaRecursosHumanosImagem = new PictureBox();
            DivisoriaRecursosHumanosImagem.Size = new Size(0, 4);
            DivisoriaRecursosHumanosImagem.Dock = DockStyle.Bottom;
            DivisoriaRecursosHumanosImagem.BackColor = Color.Silver;

            Divisoria2RecursosHumanosImagem = new PictureBox();
            Divisoria2RecursosHumanosImagem.Size = new Size(1, 2);
            Divisoria2RecursosHumanosImagem.Dock = DockStyle.Right;
            Divisoria2RecursosHumanosImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            RecursosHumanosSetaLabel = new Label();
            RecursosHumanosSetaLabel.AutoSize = false;
            RecursosHumanosSetaLabel.Size = new Size(20, 59);
            RecursosHumanosSetaLabel.Dock = DockStyle.Right;
            RecursosHumanosSetaLabel.Text = "6";
            RecursosHumanosSetaLabel.Font = new Font("Webdings", (float)12);
            RecursosHumanosSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            RecursosHumanosSetaLabel.Click += new System.EventHandler(this.RecursosHumanosSetaLabel_Click);
            RecursosHumanosSetaLabel.MouseHover += new EventHandler(this.RecursosHumanosSetaLabel_MouseHover);
            RecursosHumanosSetaLabel.Name = "RecursosHumanosSetaLabel";
            RecursosHumanosSetaLabel.ForeColor = Color.WhiteSmoke;

            RecursosHumanosLabel = new Label();
            RecursosHumanosLabel.AutoSize = false;
            RecursosHumanosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            RecursosHumanosLabel.Dock = DockStyle.Fill;
            RecursosHumanosLabel.Text = "Recursos Humanos";
            RecursosHumanosLabel.Font = CorFontepadraoLabel.Font;
            RecursosHumanosLabel.ForeColor = Color.WhiteSmoke;
            RecursosHumanosLabel.TextAlign = ContentAlignment.MiddleRight;
            RecursosHumanosLabel.MouseHover += new EventHandler(this.RecursosHumanosPanel_MouseHover);
            RecursosHumanosLabel.Name = "RecursosHumanosLabel";

            RecursosHumanosPanel.Controls.Add(RecursosHumanosLabel);
            //RecursosHumanosPanel.Controls.Add(RecursosHumanosImagem);
            RecursosHumanosPanel.Controls.Add(Divisoria2RecursosHumanosImagem);
            RecursosHumanosPanel.Controls.Add(RecursosHumanosSetaLabel);
            RecursosHumanosPanel.Controls.Add(DivisoriaRecursosHumanosImagem);

            MenuSistemaPanel.Controls.Add(RecursosHumanosPanel);
            #endregion
        }

        #region SubMenu Recursos Humanos

        private void RecursosHumanosPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresMenuSistema();
            FechaMenuUsuario();
            FechaSubMenuRecursosHumanos();
            FechaSubMenuJuridico();
            FechaSubMenuContabilidade();
            FechaSubMenuControladoria();
            FechaSubMenuAtendimento();
            FechaSubMenuTI();

            if (((Control)sender).Name.Contains("RecursosHumanos"))
            {
                RecursosHumanosLabel.Font = new Font(RecursosHumanosLabel.Font, FontStyle.Bold);
                RecursosHumanosLabel.ForeColor = Publicas._fonteBotaoFocado;
                RecursosHumanosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Controladoria"))
            {
                ControladoriaLabel.Font = new Font(ControladoriaLabel.Font, FontStyle.Bold);
                ControladoriaLabel.ForeColor = Publicas._fonteBotaoFocado;
                ControladoriaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Contabilidade"))
            {
                ContabilidadeLabel.Font = new Font(ContabilidadeLabel.Font, FontStyle.Bold);
                ContabilidadeLabel.ForeColor = Publicas._fonteBotaoFocado;
                ContabilidadeLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Juridico"))
            {
                JuridicoLabel.Font = new Font(JuridicoLabel.Font, FontStyle.Bold);
                JuridicoLabel.ForeColor = Publicas._fonteBotaoFocado;
                JuridicoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Atendimento"))
            {
                AtendimentoLabel.Font = new Font(AtendimentoLabel.Font, FontStyle.Bold);
                AtendimentoLabel.ForeColor = Publicas._fonteBotaoFocado;
                AtendimentoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("TI"))
            {
                TILabel.Font = new Font(TILabel.Font, FontStyle.Bold);
                TILabel.ForeColor = Publicas._fonteBotaoFocado;
                TILabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void RecursosHumanosSetaLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresMenuSistema();
            FechaMenuUsuario();
            FechaSubMenuRecursosHumanos();
            FechaSubMenuJuridico();
            FechaSubMenuContabilidade();
            FechaSubMenuControladoria();
            FechaSubMenuAtendimento();
            FechaSubMenuTI();

            if (((Control)sender).Name.Contains("RecursosHumanos"))
            {
                RecursosHumanosSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                RecursosHumanosSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Controladoria"))
            {
                ControladoriaSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                ControladoriaSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Contabilidade"))
            {
                ContabilidadeSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                ContabilidadeSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Juridico"))
            {
                JuridicoSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                JuridicoSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Atendimento"))
            {
                AtendimentoSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                AtendimentoSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("TI"))
            {
                TISetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                TISetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void RecursosHumanosSetaLabel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu do RecursosHumanos
            RecursosHumanosSetaLabel.Text = "3";
            RecursosHumanosSetaLabel.ForeColor = Publicas._bordaEntrada;
            RecursosHumanosLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuRecursosHumanos != null)
            {
                FechaSubMenuRecursosHumanos();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuRecursosHumanos = new Panel();
            SubMenuRecursosHumanos.Size = new Size(145, _heidthMenuSistema * 4);
            SubMenuRecursosHumanos.Location = new Point(MenuSistemaPanel.Width + 2, AcessoAoMenuPanel.Height);
            SubMenuRecursosHumanos.BackColor = Color.Silver;
            this.Controls.Add(SubMenuRecursosHumanos);
            SubMenuRecursosHumanos.BringToFront();
            SubMenuRecursosHumanos.Visible = true;
            #endregion

            #region PeriodoBancoHoras
            PeriodoBancoHorasPanel = new Panel();
            PeriodoBancoHorasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            PeriodoBancoHorasPanel.Dock = DockStyle.Top;
            PeriodoBancoHorasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            PeriodoBancoHorasPanel.Name = "PeriodoBancoHorasPanel";
            PeriodoBancoHorasPanel.Enabled = Publicas._usuario.Administrador || (Publicas._usuario.Departamento.Contains("RH") || Publicas._usuario.Departamento.ToUpper().Contains("Recursos Humanos".ToUpper()));

            DivisoriaPeriodoBancoHorasImagem = new PictureBox();
            DivisoriaPeriodoBancoHorasImagem.Size = new Size(0, 4);
            DivisoriaPeriodoBancoHorasImagem.Dock = DockStyle.Bottom;
            DivisoriaPeriodoBancoHorasImagem.BackColor = Color.Silver;

            PeriodoBancoHorasLabel = new Label();
            PeriodoBancoHorasLabel.AutoSize = false;
            PeriodoBancoHorasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            PeriodoBancoHorasLabel.Dock = DockStyle.Fill;
            PeriodoBancoHorasLabel.Text = "Período" + Environment.NewLine + "Banco de horas";
            PeriodoBancoHorasLabel.Font = CorFontepadraoLabel.Font;
            PeriodoBancoHorasLabel.ForeColor = Color.WhiteSmoke;
            PeriodoBancoHorasLabel.TextAlign = ContentAlignment.MiddleRight;
            PeriodoBancoHorasLabel.MouseHover += new EventHandler(this.CorridasPanel_MouseHover);
            PeriodoBancoHorasLabel.Click += new System.EventHandler(PeriodoBancoHorasPanel_Click);
            PeriodoBancoHorasLabel.Name = "PeriodoBancoHorasLabel";

            PeriodoBancoHorasPanel.Controls.Add(PeriodoBancoHorasLabel);
            //PeriodoBancoHorasPanel.Controls.Add(PeriodoBancoHorasImagem);
            //PeriodoBancoHorasPanel.Controls.Add(PeriodoBancoHorasSetaLabel);
            PeriodoBancoHorasPanel.Controls.Add(DivisoriaPeriodoBancoHorasImagem);

            SubMenuRecursosHumanos.Controls.Add(PeriodoBancoHorasPanel);
            #endregion

            #region Corridas 
            CorridasPanel = new Panel();
            CorridasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CorridasPanel.Dock = DockStyle.Top;
            CorridasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CorridasPanel.Name = "CorridasPanel";

            DivisoriaCorridasImagem = new PictureBox();
            DivisoriaCorridasImagem.Size = new Size(0, 4);
            DivisoriaCorridasImagem.Dock = DockStyle.Bottom;
            DivisoriaCorridasImagem.BackColor = Color.Silver;

            Divisoria2CorridasImagem = new PictureBox();
            Divisoria2CorridasImagem.Size = new Size(1, 2);
            Divisoria2CorridasImagem.Dock = DockStyle.Right;
            Divisoria2CorridasImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            CorridasSetaLabel = new Label();
            CorridasSetaLabel.AutoSize = false;
            CorridasSetaLabel.Size = new Size(20, 59);
            CorridasSetaLabel.Dock = DockStyle.Right;
            CorridasSetaLabel.Text = "6";
            CorridasSetaLabel.Font = new Font("Webdings", (float)12);
            CorridasSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            CorridasSetaLabel.Click += new System.EventHandler(this.CorridasSetaLabel_Click);
            CorridasSetaLabel.MouseHover += new EventHandler(this.CorridasSetaLabel_MouseHover);
            CorridasSetaLabel.Name = "CorridasSetaLabel";
            CorridasSetaLabel.ForeColor = Color.WhiteSmoke;

            CorridasLabel = new Label();
            CorridasLabel.AutoSize = false;
            CorridasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CorridasLabel.Dock = DockStyle.Fill;
            CorridasLabel.Text = "Corridas";
            CorridasLabel.Font = CorFontepadraoLabel.Font;
            CorridasLabel.ForeColor = Color.WhiteSmoke;
            CorridasLabel.TextAlign = ContentAlignment.MiddleRight;
            CorridasLabel.MouseHover += new EventHandler(this.CorridasPanel_MouseHover);
            CorridasLabel.Name = "CorridasLabel";

            CorridasPanel.Controls.Add(CorridasLabel);
            //CorridasPanel.Controls.Add(CorridasImagem);
            CorridasPanel.Controls.Add(Divisoria2CorridasImagem);
            CorridasPanel.Controls.Add(CorridasSetaLabel);
            CorridasPanel.Controls.Add(DivisoriaCorridasImagem);

            SubMenuRecursosHumanos.Controls.Add(CorridasPanel);
            #endregion

            #region Biblioteca 
            BibliotecaPanel = new Panel();
            BibliotecaPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            BibliotecaPanel.Dock = DockStyle.Top;
            BibliotecaPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            BibliotecaPanel.Name = "BibliotecaPanel";

            DivisoriaBibliotecaImagem = new PictureBox();
            DivisoriaBibliotecaImagem.Size = new Size(0, 4);
            DivisoriaBibliotecaImagem.Dock = DockStyle.Bottom;
            DivisoriaBibliotecaImagem.BackColor = Color.Silver;

            Divisoria2BibliotecaImagem = new PictureBox();
            Divisoria2BibliotecaImagem.Size = new Size(1, 2);
            Divisoria2BibliotecaImagem.Dock = DockStyle.Right;
            Divisoria2BibliotecaImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            BibliotecaSetaLabel = new Label();
            BibliotecaSetaLabel.AutoSize = false;
            BibliotecaSetaLabel.Size = new Size(20, 59);
            BibliotecaSetaLabel.Dock = DockStyle.Right;
            BibliotecaSetaLabel.Text = "6";
            BibliotecaSetaLabel.Font = new Font("Webdings", (float)12);
            BibliotecaSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            BibliotecaSetaLabel.Click += new System.EventHandler(this.BibliotecaSetaLabel_Click);
            BibliotecaSetaLabel.MouseHover += new EventHandler(this.CorridasSetaLabel_MouseHover);
            BibliotecaSetaLabel.Name = "BibliotecaSetaLabel";
            BibliotecaSetaLabel.ForeColor = Color.WhiteSmoke;

            BibliotecaLabel = new Label();
            BibliotecaLabel.AutoSize = false;
            BibliotecaLabel.Size = new Size(_widthLabelMenuSistema, 0);
            BibliotecaLabel.Dock = DockStyle.Fill;
            BibliotecaLabel.Text = "Biblioteca";
            BibliotecaLabel.Font = CorFontepadraoLabel.Font;
            BibliotecaLabel.ForeColor = Color.WhiteSmoke;
            BibliotecaLabel.TextAlign = ContentAlignment.MiddleRight;
            BibliotecaLabel.MouseHover += new EventHandler(this.CorridasPanel_MouseHover);
            BibliotecaLabel.Name = "BibliotecaLabel";

            BibliotecaPanel.Controls.Add(BibliotecaLabel);
            //BibliotecaPanel.Controls.Add(BibliotecaImagem);
            BibliotecaPanel.Controls.Add(Divisoria2BibliotecaImagem);
            BibliotecaPanel.Controls.Add(BibliotecaSetaLabel);
            BibliotecaPanel.Controls.Add(DivisoriaBibliotecaImagem);

            SubMenuRecursosHumanos.Controls.Add(BibliotecaPanel);
            #endregion

            #region AvaliacaoDesempenho 
            AvaliacaoDesempenhoPanel = new Panel();
            AvaliacaoDesempenhoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AvaliacaoDesempenhoPanel.Dock = DockStyle.Top;
            AvaliacaoDesempenhoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AvaliacaoDesempenhoPanel.Name = "AvaliacaoDesempenhoPanel";
            AvaliacaoDesempenhoPanel.Enabled = Publicas._usuario.AcessaAvaliacaoDesempenho;

            DivisoriaAvaliacaoDesempenhoImagem = new PictureBox();
            DivisoriaAvaliacaoDesempenhoImagem.Size = new Size(0, 4);
            DivisoriaAvaliacaoDesempenhoImagem.Dock = DockStyle.Bottom;
            DivisoriaAvaliacaoDesempenhoImagem.BackColor = Color.Silver;

            Divisoria2AvaliacaoDesempenhoImagem = new PictureBox();
            Divisoria2AvaliacaoDesempenhoImagem.Size = new Size(1, 2);
            Divisoria2AvaliacaoDesempenhoImagem.Dock = DockStyle.Right;
            Divisoria2AvaliacaoDesempenhoImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            AvaliacaoDesempenhoSetaLabel = new Label();
            AvaliacaoDesempenhoSetaLabel.AutoSize = false;
            AvaliacaoDesempenhoSetaLabel.Size = new Size(20, 59);
            AvaliacaoDesempenhoSetaLabel.Dock = DockStyle.Right;
            AvaliacaoDesempenhoSetaLabel.Text = "6";
            AvaliacaoDesempenhoSetaLabel.Font = new Font("Webdings", (float)12);
            AvaliacaoDesempenhoSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            AvaliacaoDesempenhoSetaLabel.Click += new System.EventHandler(this.AvaliacaoDesempenhoSetaLabel_Click);
            AvaliacaoDesempenhoSetaLabel.MouseHover += new EventHandler(this.CorridasSetaLabel_MouseHover);
            AvaliacaoDesempenhoSetaLabel.Name = "AvaliacaoDesempenhoSetaLabel";
            AvaliacaoDesempenhoSetaLabel.ForeColor = Color.WhiteSmoke;

            AvaliacaoDesempenhoLabel = new Label();
            AvaliacaoDesempenhoLabel.AutoSize = false;
            AvaliacaoDesempenhoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AvaliacaoDesempenhoLabel.Dock = DockStyle.Fill;
            AvaliacaoDesempenhoLabel.Text = "Avaliação de" + Environment.NewLine + "Desempenho";
            AvaliacaoDesempenhoLabel.Font = CorFontepadraoLabel.Font;
            AvaliacaoDesempenhoLabel.ForeColor = Color.WhiteSmoke;
            AvaliacaoDesempenhoLabel.TextAlign = ContentAlignment.MiddleRight;
            AvaliacaoDesempenhoLabel.MouseHover += new EventHandler(this.CorridasPanel_MouseHover);
            AvaliacaoDesempenhoLabel.Name = "AvaliacaoDesempenhoLabel";

            AvaliacaoDesempenhoPanel.Controls.Add(AvaliacaoDesempenhoLabel);
            //AvaliacaoDesempenhoPanel.Controls.Add(AvaliacaoDesempenhoImagem);
            AvaliacaoDesempenhoPanel.Controls.Add(Divisoria2AvaliacaoDesempenhoImagem);
            AvaliacaoDesempenhoPanel.Controls.Add(AvaliacaoDesempenhoSetaLabel);
            AvaliacaoDesempenhoPanel.Controls.Add(DivisoriaAvaliacaoDesempenhoImagem);

            SubMenuRecursosHumanos.Controls.Add(AvaliacaoDesempenhoPanel);
            #endregion
        }

        private void CorridasPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuRecursosHumanos();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();

            if (((Control)sender).Name.Contains("Corridas"))
            {
                CorridasLabel.Font = new Font(CorridasLabel.Font, FontStyle.Bold);
                CorridasLabel.ForeColor = Publicas._fonteBotaoFocado;
                CorridasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AvaliacaoDesempenho"))
            {
                AvaliacaoDesempenhoLabel.Font = new Font(AvaliacaoDesempenhoLabel.Font, FontStyle.Bold);
                AvaliacaoDesempenhoLabel.ForeColor = Publicas._fonteBotaoFocado;
                AvaliacaoDesempenhoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Biblioteca"))
            {
                BibliotecaLabel.Font = new Font(BibliotecaLabel.Font, FontStyle.Bold);
                BibliotecaLabel.ForeColor = Publicas._fonteBotaoFocado;
                BibliotecaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("PeriodoBancoHoras"))
            {
                PeriodoBancoHorasLabel.Font = new Font(PeriodoBancoHorasLabel.Font, FontStyle.Bold);
                PeriodoBancoHorasLabel.ForeColor = Publicas._fonteBotaoFocado;
                PeriodoBancoHorasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void CorridasSetaLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuRecursosHumanos();
            FechaMenuUsuario();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();

            if (((Control)sender).Name.Contains("Biblioteca"))
            {
                BibliotecaSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                BibliotecaSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Corridas"))
            {
                CorridasSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                CorridasSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AvaliacaoDesempenho"))
            {
                AvaliacaoDesempenhoSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                AvaliacaoDesempenhoSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void MudaSelecaoDeCoresSubMenuRecursosHumanos()
        {
            PeriodoBancoHorasLabel.Font = new Font(ControladoriaLabel.Font, ControladoriaLabel.Font.Style & ~FontStyle.Bold);
            AvaliacaoDesempenhoLabel.Font = new Font(ContabilidadeLabel.Font, ContabilidadeLabel.Font.Style & ~FontStyle.Bold);
            CorridasLabel.Font = new Font(RecursosHumanosLabel.Font, RecursosHumanosLabel.Font.Style & ~FontStyle.Bold);
            BibliotecaLabel.Font = new Font(AtendimentoLabel.Font, AtendimentoLabel.Font.Style & ~FontStyle.Bold);

            PeriodoBancoHorasLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AvaliacaoDesempenhoLabel.BackColor = ControladoriaPanel.BackColor;
            CorridasLabel.BackColor = ControladoriaPanel.BackColor;
            BibliotecaLabel.BackColor = ControladoriaPanel.BackColor;

            AvaliacaoDesempenhoSetaLabel.ForeColor = Color.WhiteSmoke;
            CorridasSetaLabel.ForeColor = Color.WhiteSmoke;
            BibliotecaSetaLabel.ForeColor = Color.WhiteSmoke;
            PeriodoBancoHorasLabel.ForeColor = Color.WhiteSmoke;
            AvaliacaoDesempenhoLabel.ForeColor = Color.WhiteSmoke;
            CorridasLabel.ForeColor = Color.WhiteSmoke;
            BibliotecaLabel.ForeColor = Color.WhiteSmoke;
            AvaliacaoDesempenhoSetaLabel.BackColor = ControladoriaPanel.BackColor;
            CorridasSetaLabel.BackColor = ControladoriaPanel.BackColor;
            BibliotecaSetaLabel.BackColor = ControladoriaPanel.BackColor;

            AvaliacaoDesempenhoSetaLabel.Text = "6";
            CorridasSetaLabel.Text = "6";
            BibliotecaSetaLabel.Text = "6";

        }

        private void MensagemSistema()
        {
            if (Publicas._alterouSkin)
            {
                Publicas._mensagemSistema = (Publicas._alterouSkin ? "Aplicando tema, aguarde." : string.Empty);
                dataHoraTimer_Tick(this, new EventArgs());
                Refresh();
            }
        }
        private void PeriodoBancoHorasPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();

            MensagemSistema();
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCadastro;
            Cadastros.PeriodoBancoHoras _tela = new Cadastros.PeriodoBancoHoras();
            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void FechaSubMenuRecursosHumanos()
        {
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();

            if (SubMenuRecursosHumanos != null)
            {
                SubMenuRecursosHumanos.Visible = false;
                this.Controls.Remove(SubMenuRecursosHumanos);
                SubMenuRecursosHumanos.Dispose();
                SubMenuRecursosHumanos = null;
            }
        }

        #region SubMenu Avaliação Desempenho

        private void AvaliacaoDesempenhoSetaLabel_Click(object sender, EventArgs e)
        {
            FechaSubMenuBiblioteca();

            AvaliacaoDesempenhoSetaLabel.Text = "3";
            AvaliacaoDesempenhoSetaLabel.ForeColor = Publicas._bordaEntrada;
            AvaliacaoDesempenhoLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuAvaliacaoDesempenho != null)
            {
                FechaSubMenuAvaliacaoDesempenho();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuAvaliacaoDesempenho = new Panel();
            SubMenuAvaliacaoDesempenho.Size = new Size(145, _heidthMenuSistema * 4);
            SubMenuAvaliacaoDesempenho.Location = new Point(MenuSistemaPanel.Width + SubMenuRecursosHumanos.Width + 4, AcessoAoMenuPanel.Height);
            SubMenuAvaliacaoDesempenho.BackColor = Color.Silver;
            this.Controls.Add(SubMenuAvaliacaoDesempenho);
            SubMenuAvaliacaoDesempenho.BringToFront();
            SubMenuAvaliacaoDesempenho.Visible = true;
            #endregion

            #region AD_Gestor 
            AD_GestorPanel = new Panel();
            AD_GestorPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AD_GestorPanel.Dock = DockStyle.Top;
            AD_GestorPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AD_GestorPanel.Name = "AD_GestorPanel";
            AD_GestorPanel.Enabled = Publicas._usuario.AcessoDeGestor;

            DivisoriaAD_GestorImagem = new PictureBox();
            DivisoriaAD_GestorImagem.Size = new Size(0, 4);
            DivisoriaAD_GestorImagem.Dock = DockStyle.Bottom;
            DivisoriaAD_GestorImagem.BackColor = Color.Silver;

            Divisoria2AD_GestorImagem = new PictureBox();
            Divisoria2AD_GestorImagem.Size = new Size(1, 2);
            Divisoria2AD_GestorImagem.Dock = DockStyle.Right;
            Divisoria2AD_GestorImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            AD_GestorSetaLabel = new Label();
            AD_GestorSetaLabel.AutoSize = false;
            AD_GestorSetaLabel.Size = new Size(20, 59);
            AD_GestorSetaLabel.Dock = DockStyle.Right;
            AD_GestorSetaLabel.Text = "6";
            AD_GestorSetaLabel.Font = new Font("Webdings", (float)12);
            AD_GestorSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            AD_GestorSetaLabel.Click += new System.EventHandler(this.AD_GestorPanel_Click);
            AD_GestorSetaLabel.MouseHover += new EventHandler(this.AD_GestorSetaLabel_MouseHover);
            AD_GestorSetaLabel.Name = "AD_GestorSetaLabel";
            AD_GestorSetaLabel.ForeColor = Color.WhiteSmoke;

            AD_GestorLabel = new Label();
            AD_GestorLabel.AutoSize = false;
            AD_GestorLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AD_GestorLabel.Dock = DockStyle.Fill;
            AD_GestorLabel.Text = "Gestor";
            AD_GestorLabel.Font = CorFontepadraoLabel.Font;
            AD_GestorLabel.ForeColor = Color.WhiteSmoke;
            AD_GestorLabel.TextAlign = ContentAlignment.MiddleRight;
            AD_GestorLabel.MouseHover += new EventHandler(this.AD_GestorPanel_MouseHover);
            AD_GestorLabel.Name = "AD_GestorLabel";

            AD_GestorPanel.Controls.Add(AD_GestorLabel);
            //AD_GestorPanel.Controls.Add(AD_GestorImagem);
            AD_GestorPanel.Controls.Add(Divisoria2AD_GestorImagem);
            AD_GestorPanel.Controls.Add(AD_GestorSetaLabel);
            AD_GestorPanel.Controls.Add(DivisoriaAD_GestorImagem);

            SubMenuAvaliacaoDesempenho.Controls.Add(AD_GestorPanel);
            #endregion

            #region AD_Colaborador 
            AD_ColaboradorPanel = new Panel();
            AD_ColaboradorPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AD_ColaboradorPanel.Dock = DockStyle.Top;
            AD_ColaboradorPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AD_ColaboradorPanel.Name = "AD_ColaboradorPanel";
            AD_ColaboradorPanel.Enabled = Publicas._usuario.AcessoDeColaborador;

            DivisoriaAD_ColaboradorImagem = new PictureBox();
            DivisoriaAD_ColaboradorImagem.Size = new Size(0, 4);
            DivisoriaAD_ColaboradorImagem.Dock = DockStyle.Bottom;
            DivisoriaAD_ColaboradorImagem.BackColor = Color.Silver;

            Divisoria2AD_ColaboradorImagem = new PictureBox();
            Divisoria2AD_ColaboradorImagem.Size = new Size(1, 2);
            Divisoria2AD_ColaboradorImagem.Dock = DockStyle.Right;
            Divisoria2AD_ColaboradorImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            AD_ColaboradorSetaLabel = new Label();
            AD_ColaboradorSetaLabel.AutoSize = false;
            AD_ColaboradorSetaLabel.Size = new Size(20, 59);
            AD_ColaboradorSetaLabel.Dock = DockStyle.Right;
            AD_ColaboradorSetaLabel.Text = "6";
            AD_ColaboradorSetaLabel.Font = new Font("Webdings", (float)12);
            AD_ColaboradorSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            AD_ColaboradorSetaLabel.Click += new System.EventHandler(this.AD_ColaboradorPanel_Click);
            AD_ColaboradorSetaLabel.MouseHover += new EventHandler(this.AD_GestorSetaLabel_MouseHover);
            AD_ColaboradorSetaLabel.Name = "AD_ColaboradorSetaLabel";
            AD_ColaboradorSetaLabel.ForeColor = Color.WhiteSmoke;

            AD_ColaboradorLabel = new Label();
            AD_ColaboradorLabel.AutoSize = false;
            AD_ColaboradorLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AD_ColaboradorLabel.Dock = DockStyle.Fill;
            AD_ColaboradorLabel.Text = "Colaborador";
            AD_ColaboradorLabel.Font = CorFontepadraoLabel.Font;
            AD_ColaboradorLabel.ForeColor = Color.WhiteSmoke;
            AD_ColaboradorLabel.TextAlign = ContentAlignment.MiddleRight;
            AD_ColaboradorLabel.MouseHover += new EventHandler(this.AD_GestorPanel_MouseHover);
            AD_ColaboradorLabel.Name = "AD_ColaboradorLabel";

            AD_ColaboradorPanel.Controls.Add(AD_ColaboradorLabel);
            //AD_ColaboradorPanel.Controls.Add(AD_ColaboradorImagem);
            AD_ColaboradorPanel.Controls.Add(Divisoria2AD_ColaboradorImagem);
            AD_ColaboradorPanel.Controls.Add(AD_ColaboradorSetaLabel);
            AD_ColaboradorPanel.Controls.Add(DivisoriaAD_ColaboradorImagem);

            SubMenuAvaliacaoDesempenho.Controls.Add(AD_ColaboradorPanel);
            #endregion

            #region AD_RecursosHumanos 
            AD_RecursosHumanosPanel = new Panel();
            AD_RecursosHumanosPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AD_RecursosHumanosPanel.Dock = DockStyle.Top;
            AD_RecursosHumanosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AD_RecursosHumanosPanel.Name = "AD_RecursosHumanosPanel";
            AD_RecursosHumanosPanel.Enabled = Publicas._usuario.AcessoDeRH;

            DivisoriaAD_RecursosHumanosImagem = new PictureBox();
            DivisoriaAD_RecursosHumanosImagem.Size = new Size(0, 4);
            DivisoriaAD_RecursosHumanosImagem.Dock = DockStyle.Bottom;
            DivisoriaAD_RecursosHumanosImagem.BackColor = Color.Silver;

            Divisoria2AD_RecursosHumanosImagem = new PictureBox();
            Divisoria2AD_RecursosHumanosImagem.Size = new Size(1, 2);
            Divisoria2AD_RecursosHumanosImagem.Dock = DockStyle.Right;
            Divisoria2AD_RecursosHumanosImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            AD_RecursosHumanosSetaLabel = new Label();
            AD_RecursosHumanosSetaLabel.AutoSize = false;
            AD_RecursosHumanosSetaLabel.Size = new Size(20, 59);
            AD_RecursosHumanosSetaLabel.Dock = DockStyle.Right;
            AD_RecursosHumanosSetaLabel.Text = "6";
            AD_RecursosHumanosSetaLabel.Font = new Font("Webdings", (float)12);
            AD_RecursosHumanosSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            AD_RecursosHumanosSetaLabel.Click += new System.EventHandler(this.AD_RecursosHumanosPanel_Click);
            AD_RecursosHumanosSetaLabel.MouseHover += new EventHandler(this.AD_GestorSetaLabel_MouseHover);
            AD_RecursosHumanosSetaLabel.Name = "AD_RecursosHumanosSetaLabel";
            AD_RecursosHumanosSetaLabel.ForeColor = Color.WhiteSmoke;

            AD_RecursosHumanosLabel = new Label();
            AD_RecursosHumanosLabel.AutoSize = false;
            AD_RecursosHumanosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AD_RecursosHumanosLabel.Dock = DockStyle.Fill;
            AD_RecursosHumanosLabel.Text = "Recursos Humanos";
            AD_RecursosHumanosLabel.Font = CorFontepadraoLabel.Font;
            AD_RecursosHumanosLabel.ForeColor = Color.WhiteSmoke;
            AD_RecursosHumanosLabel.TextAlign = ContentAlignment.MiddleRight;
            AD_RecursosHumanosLabel.MouseHover += new EventHandler(this.AD_GestorPanel_MouseHover);
            AD_RecursosHumanosLabel.Name = "AD_RecursosHumanosLabel";

            AD_RecursosHumanosPanel.Controls.Add(AD_RecursosHumanosLabel);
            //AD_RecursosHumanosPanel.Controls.Add(AD_RecursosHumanosImagem);
            AD_RecursosHumanosPanel.Controls.Add(Divisoria2AD_RecursosHumanosImagem);
            AD_RecursosHumanosPanel.Controls.Add(AD_RecursosHumanosSetaLabel);
            AD_RecursosHumanosPanel.Controls.Add(DivisoriaAD_RecursosHumanosImagem);

            SubMenuAvaliacaoDesempenho.Controls.Add(AD_RecursosHumanosPanel);
            #endregion

            #region AD_Planejamento 
            AD_PlanejamentoPanel = new Panel();
            AD_PlanejamentoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AD_PlanejamentoPanel.Dock = DockStyle.Top;
            AD_PlanejamentoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AD_PlanejamentoPanel.Name = "AD_PlanejamentoPanel";
            AD_PlanejamentoPanel.Enabled = Publicas._usuario.AcessoDeControladoria;

            DivisoriaAD_PlanejamentoImagem = new PictureBox();
            DivisoriaAD_PlanejamentoImagem.Size = new Size(0, 4);
            DivisoriaAD_PlanejamentoImagem.Dock = DockStyle.Bottom;
            DivisoriaAD_PlanejamentoImagem.BackColor = Color.Silver;

            Divisoria2AD_PlanejamentoImagem = new PictureBox();
            Divisoria2AD_PlanejamentoImagem.Size = new Size(1, 2);
            Divisoria2AD_PlanejamentoImagem.Dock = DockStyle.Right;
            Divisoria2AD_PlanejamentoImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128); 

            AD_PlanejamentoSetaLabel = new Label();
            AD_PlanejamentoSetaLabel.AutoSize = false;
            AD_PlanejamentoSetaLabel.Size = new Size(20, 59);
            AD_PlanejamentoSetaLabel.Dock = DockStyle.Right;
            AD_PlanejamentoSetaLabel.Text = "6";
            AD_PlanejamentoSetaLabel.Font = new Font("Webdings", (float)12);
            AD_PlanejamentoSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            AD_PlanejamentoSetaLabel.Click += new System.EventHandler(this.AD_PlanejamentoPanel_Click);
            AD_PlanejamentoSetaLabel.MouseHover += new EventHandler(this.AD_GestorSetaLabel_MouseHover);
            AD_PlanejamentoSetaLabel.Name = "AD_PlanejamentoSetaLabel";
            AD_PlanejamentoSetaLabel.ForeColor = Color.WhiteSmoke;

            AD_PlanejamentoLabel = new Label();
            AD_PlanejamentoLabel.AutoSize = false;
            AD_PlanejamentoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AD_PlanejamentoLabel.Dock = DockStyle.Fill;
            AD_PlanejamentoLabel.Text = "Planejamento";
            AD_PlanejamentoLabel.Font = CorFontepadraoLabel.Font;
            AD_PlanejamentoLabel.ForeColor = Color.WhiteSmoke;
            AD_PlanejamentoLabel.TextAlign = ContentAlignment.MiddleRight;
            AD_PlanejamentoLabel.MouseHover += new EventHandler(this.AD_GestorPanel_MouseHover);
            AD_PlanejamentoLabel.Name = "AD_PlanejamentoLabel";

            AD_PlanejamentoPanel.Controls.Add(AD_PlanejamentoLabel);
            //AD_ControladoriaPanel.Controls.Add(AD_ControladoriaImagem);
            AD_PlanejamentoPanel.Controls.Add(Divisoria2AD_PlanejamentoImagem);
            AD_PlanejamentoPanel.Controls.Add(AD_PlanejamentoSetaLabel);
            AD_PlanejamentoPanel.Controls.Add(DivisoriaAD_PlanejamentoImagem);

            SubMenuAvaliacaoDesempenho.Controls.Add(AD_PlanejamentoPanel);
            #endregion
        }

        private void MudaSelecaoDeCoresSubAvaliacaoDesempenho()
        {
            AD_GestorLabel.Font = new Font(AD_GestorLabel.Font, AD_GestorLabel.Font.Style & ~FontStyle.Bold);
            AD_PlanejamentoLabel.Font = new Font(AD_PlanejamentoLabel.Font, AD_PlanejamentoLabel.Font.Style & ~FontStyle.Bold);
            AD_ColaboradorLabel.Font = new Font(AD_ColaboradorLabel.Font, AD_ColaboradorLabel.Font.Style & ~FontStyle.Bold);
            AD_RecursosHumanosLabel.Font = new Font(AD_RecursosHumanosLabel.Font, AD_RecursosHumanosLabel.Font.Style & ~FontStyle.Bold);

            AD_GestorLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AD_PlanejamentoLabel.BackColor = AD_GestorLabel.BackColor;
            AD_ColaboradorLabel.BackColor = AD_GestorLabel.BackColor;
            AD_RecursosHumanosLabel.BackColor = AD_GestorLabel.BackColor;

            AD_GestorSetaLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AD_PlanejamentoSetaLabel.BackColor = AD_GestorLabel.BackColor;
            AD_ColaboradorSetaLabel.BackColor = AD_GestorLabel.BackColor;
            AD_RecursosHumanosSetaLabel.BackColor = AD_GestorLabel.BackColor;

            AD_GestorLabel.ForeColor = Color.WhiteSmoke;
            AD_GestorSetaLabel.ForeColor = Color.WhiteSmoke;
            AD_PlanejamentoLabel.ForeColor = Color.WhiteSmoke;
            AD_PlanejamentoSetaLabel.ForeColor = Color.WhiteSmoke;
            AD_ColaboradorLabel.ForeColor = Color.WhiteSmoke;
            AD_ColaboradorSetaLabel.ForeColor = Color.WhiteSmoke;
            AD_RecursosHumanosLabel.ForeColor = Color.WhiteSmoke;
            AD_RecursosHumanosSetaLabel.ForeColor = Color.WhiteSmoke;

            AD_RecursosHumanosSetaLabel.Text = "6";
            AD_ColaboradorSetaLabel.Text = "6";
            AD_PlanejamentoSetaLabel.Text = "6";
            AD_GestorSetaLabel.Text = "6";

        }

        private void AD_GestorPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubAvaliacaoDesempenho();
            FechaMenuUsuario();
            FechaSubMenuADPlanejamento();
            FechaSubMenuADColaborador();
            FechaSubMenuADRecursosHumanos();
            FechaSubMenuADGestor();

            if (((Control)sender).Name.Contains("AD_Gestor"))
            {
                AD_GestorLabel.Font = new Font(AD_GestorLabel.Font, FontStyle.Bold);
                AD_GestorLabel.ForeColor = Publicas._fonteBotaoFocado;
                AD_GestorLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AD_Colaborador"))
            {
                AD_ColaboradorLabel.Font = new Font(AD_ColaboradorLabel.Font, FontStyle.Bold);
                AD_ColaboradorLabel.ForeColor = Publicas._fonteBotaoFocado;
                AD_ColaboradorLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AD_Planejamento"))
            {
                AD_PlanejamentoLabel.Font = new Font(AD_PlanejamentoLabel.Font, FontStyle.Bold);
                AD_PlanejamentoLabel.ForeColor = Publicas._fonteBotaoFocado;
                AD_PlanejamentoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AD_RecursosHumanos"))
            {
                AD_RecursosHumanosLabel.Font = new Font(AD_RecursosHumanosLabel.Font, FontStyle.Bold);
                AD_RecursosHumanosLabel.ForeColor = Publicas._fonteBotaoFocado;
                AD_RecursosHumanosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void AD_GestorSetaLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubAvaliacaoDesempenho();
            FechaMenuUsuario();
            FechaSubMenuADPlanejamento();
            FechaSubMenuADColaborador();
            FechaSubMenuADRecursosHumanos();
            FechaSubMenuADGestor();

            if (((Control)sender).Name.Contains("AD_Gestor"))
            {
                AD_GestorSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                AD_GestorSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AD_Colaborador"))
            {
                AD_ColaboradorSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                AD_ColaboradorSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AD_Planejamento"))
            {
                AD_PlanejamentoSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                AD_PlanejamentoSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AD_RecursosHumanos"))
            {
                AD_RecursosHumanosSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                AD_RecursosHumanosSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void FechaSubMenuAvaliacaoDesempenho()
        {
            FechaSubMenuADRecursosHumanos();
            FechaSubMenuADGestor();
            FechaSubMenuADPlanejamento();
            FechaSubMenuADColaborador();

            if (SubMenuAvaliacaoDesempenho != null)
            {
                SubMenuAvaliacaoDesempenho.Visible = false;
                this.Controls.Remove(SubMenuAvaliacaoDesempenho);
                SubMenuAvaliacaoDesempenho.Dispose();
                SubMenuAvaliacaoDesempenho = null;
            }
        }

        #region SubMenu Planejamento
        private void AD_PlanejamentoPanel_Click(object sender, EventArgs e)
        {
            AD_PlanejamentoSetaLabel.Text = "3";
            AD_PlanejamentoSetaLabel.ForeColor = Publicas._bordaEntrada;
            AD_PlanejamentoLabel.ForeColor = Publicas._bordaEntrada;

            FechaSubMenuADGestor();
            FechaSubMenuADColaborador();
            FechaSubMenuADRecursosHumanos();

            if (SubMenuADPlanejamento != null)
            {
                FechaSubMenuADPlanejamento();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuADPlanejamento = new Panel();
            SubMenuADPlanejamento.Size = new Size(145, _heidthMenuSistema * 3);
            SubMenuADPlanejamento.Location = new Point(MenuSistemaPanel.Width + SubMenuRecursosHumanos.Width + SubMenuAvaliacaoDesempenho.Width + 6, AcessoAoMenuPanel.Height);
            SubMenuADPlanejamento.BackColor = Color.Silver;
            this.Controls.Add(SubMenuADPlanejamento);
            #endregion

            #region DefinicaoDefinicaoMetas 
            DefinicaoMetasPanel = new Panel();
            DefinicaoMetasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            DefinicaoMetasPanel.Dock = DockStyle.Top;
            DefinicaoMetasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            DefinicaoMetasPanel.Name = "DefinicaoMetasPanel";

            DivisoriaDefinicaoMetasImagem = new PictureBox();
            DivisoriaDefinicaoMetasImagem.Size = new Size(0, 4);
            DivisoriaDefinicaoMetasImagem.Dock = DockStyle.Bottom;
            DivisoriaDefinicaoMetasImagem.BackColor = Color.Silver;

            DefinicaoMetasLabel = new Label();
            DefinicaoMetasLabel.AutoSize = false;
            DefinicaoMetasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            DefinicaoMetasLabel.Dock = DockStyle.Fill;
            DefinicaoMetasLabel.Text = "Definição das metas";
            DefinicaoMetasLabel.Font = CorFontepadraoLabel.Font;
            DefinicaoMetasLabel.ForeColor = Color.WhiteSmoke;
            DefinicaoMetasLabel.TextAlign = ContentAlignment.MiddleRight;
            DefinicaoMetasLabel.MouseHover += new EventHandler(this.DefinicaoMetasLabel_MouseHover);
            DefinicaoMetasLabel.Click += new System.EventHandler(DefinicaoMetasPanel_Click);
            DefinicaoMetasLabel.Name = "DefinicaoMetasLabel";

            DefinicaoMetasPanel.Controls.Add(DefinicaoMetasLabel);
            DefinicaoMetasPanel.Controls.Add(DivisoriaDefinicaoMetasImagem);

            SubMenuADPlanejamento.Controls.Add(DefinicaoMetasPanel);
            #endregion

            #region Cargos
            CargosPanel = new Panel();
            CargosPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CargosPanel.Dock = DockStyle.Top;
            CargosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CargosPanel.Name = "CargosPanel";

            DivisoriaCargosImagem = new PictureBox();
            DivisoriaCargosImagem.Size = new Size(0, 4);
            DivisoriaCargosImagem.Dock = DockStyle.Bottom;
            DivisoriaCargosImagem.BackColor = Color.Silver;

            CargosLabel = new Label();
            CargosLabel.AutoSize = false;
            CargosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CargosLabel.Dock = DockStyle.Fill;
            CargosLabel.Text = "Cargos";
            CargosLabel.Font = CorFontepadraoLabel.Font;
            CargosLabel.ForeColor = Color.WhiteSmoke;
            CargosLabel.TextAlign = ContentAlignment.MiddleRight;
            CargosLabel.MouseHover += new EventHandler(this.DefinicaoMetasLabel_MouseHover);
            CargosLabel.Click += new System.EventHandler(CargosPanel_Click);
            CargosLabel.Name = "CargosLabel";

            CargosPanel.Controls.Add(CargosLabel);
            CargosPanel.Controls.Add(DivisoriaCargosImagem);

            SubMenuADPlanejamento.Controls.Add(CargosPanel);
            #endregion

            #region Metas
            MetasPanel = new Panel();
            MetasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            MetasPanel.Dock = DockStyle.Top;
            MetasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            MetasPanel.Name = "MetasPanel";

            DivisoriaMetasImagem = new PictureBox();
            DivisoriaMetasImagem.Size = new Size(0, 4);
            DivisoriaMetasImagem.Dock = DockStyle.Bottom;
            DivisoriaMetasImagem.BackColor = Color.Silver;

            MetasLabel = new Label();
            MetasLabel.AutoSize = false;
            MetasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            MetasLabel.Dock = DockStyle.Fill;
            MetasLabel.Text = "Metas";
            MetasLabel.Font = CorFontepadraoLabel.Font;
            MetasLabel.ForeColor = Color.WhiteSmoke;
            MetasLabel.TextAlign = ContentAlignment.MiddleRight;
            MetasLabel.MouseHover += new EventHandler(this.DefinicaoMetasLabel_MouseHover);
            MetasLabel.Click += new System.EventHandler(MetasPanel_Click);
            MetasLabel.Name = "MetasLabel";

            MetasPanel.Controls.Add(MetasLabel);
            MetasPanel.Controls.Add(DivisoriaMetasImagem);

            SubMenuADPlanejamento.Controls.Add(MetasPanel);
            #endregion

            SubMenuADPlanejamento.BringToFront();
            SubMenuADPlanejamento.Visible = true;

        }

        private void MudaSelecaoDeCoresSubADControladoria()
        {
            MetasLabel.Font = new Font(MetasLabel.Font, MetasLabel.Font.Style & ~FontStyle.Bold);
            DefinicaoMetasLabel.Font = new Font(DefinicaoMetasLabel.Font, DefinicaoMetasLabel.Font.Style & ~FontStyle.Bold);
            CargosLabel.Font = new Font(DefinicaoMetasLabel.Font, DefinicaoMetasLabel.Font.Style & ~FontStyle.Bold);

            DefinicaoMetasLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            MetasLabel.BackColor = DefinicaoMetasLabel.BackColor;
            CargosLabel.BackColor = DefinicaoMetasLabel.BackColor;

            DefinicaoMetasLabel.ForeColor = Color.WhiteSmoke;
            MetasLabel.ForeColor = Color.WhiteSmoke;
            CargosLabel.ForeColor = Color.WhiteSmoke;
        }

        private void DefinicaoMetasLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubADControladoria();
            FechaMenuUsuario();

            if (((Control)sender).Name.Contains("Definicao"))
            {
                DefinicaoMetasLabel.Font = new Font(DefinicaoMetasLabel.Font, FontStyle.Bold);
                DefinicaoMetasLabel.ForeColor = Publicas._fonteBotaoFocado;
                DefinicaoMetasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Metas"))
            {
                MetasLabel.Font = new Font(MetasLabel.Font, FontStyle.Bold);
                MetasLabel.ForeColor = Publicas._fonteBotaoFocado;
                MetasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Cargos"))
            {
                CargosLabel.Font = new Font(CargosLabel.Font, FontStyle.Bold);
                CargosLabel.ForeColor = Publicas._fonteBotaoFocado;
                CargosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void MetasPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            
            MensagemSistema();
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Metas().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void DefinicaoMetasPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.DefinicaoDeMetas _tela = new Avaliacao_de_desempenho.DefinicaoDeMetas();

            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void FechaSubMenuADPlanejamento()
        {
            if (SubMenuADPlanejamento != null)
            {
                SubMenuADPlanejamento.Visible = false;
                this.Controls.Remove(SubMenuADPlanejamento);
                SubMenuADPlanejamento.Dispose();
                SubMenuADPlanejamento = null;
            }
        }
        #endregion

        #region SubMenu Colaborador
        private void AD_ColaboradorPanel_Click(object sender, EventArgs e)
        {
            AD_ColaboradorSetaLabel.Text = "3";
            AD_ColaboradorSetaLabel.ForeColor = Publicas._bordaEntrada;
            AD_ColaboradorLabel.ForeColor = Publicas._bordaEntrada;

            FechaSubMenuADPlanejamento();
            FechaSubMenuADGestor();
            FechaSubMenuADRecursosHumanos();

            if (SubMenuADColaborador != null)
            {
                FechaSubMenuADColaborador();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuADColaborador = new Panel();
            SubMenuADColaborador.Size = new Size(145, _heidthMenuSistema * 4);
            SubMenuADColaborador.Location = new Point(MenuSistemaPanel.Width + SubMenuRecursosHumanos.Width + SubMenuAvaliacaoDesempenho.Width + 6, AcessoAoMenuPanel.Height);
            SubMenuADColaborador.BackColor = Color.Silver;
            this.Controls.Add(SubMenuADColaborador);
            SubMenuADColaborador.BringToFront();
            SubMenuADColaborador.Visible = true;
            #endregion

            #region Radar
            RadarPanel = new Panel();
            RadarPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            RadarPanel.Dock = DockStyle.Top;
            RadarPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            RadarPanel.Name = "RadarPanel";

            DivisoriaRadarImagem = new PictureBox();
            DivisoriaRadarImagem.Size = new Size(0, 4);
            DivisoriaRadarImagem.Dock = DockStyle.Bottom;
            DivisoriaRadarImagem.BackColor = Color.Silver;

            RadarLabel = new Label();
            RadarLabel.AutoSize = false;
            RadarLabel.Size = new Size(_widthLabelMenuSistema, 0);
            RadarLabel.Dock = DockStyle.Fill;
            RadarLabel.Text = "Radar";
            RadarLabel.Font = CorFontepadraoLabel.Font;
            RadarLabel.ForeColor = Color.WhiteSmoke;
            RadarLabel.TextAlign = ContentAlignment.MiddleRight;
            RadarLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            RadarLabel.Click += new System.EventHandler(RadarColaboradorPanel_Click);
            RadarLabel.Name = "RadarLabel";

            RadarPanel.Controls.Add(RadarLabel);
            RadarPanel.Controls.Add(DivisoriaRadarImagem);

            SubMenuADColaborador.Controls.Add(RadarPanel);
            #endregion

            #region Feedback
            FeedbackPanel = new Panel();
            FeedbackPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            FeedbackPanel.Dock = DockStyle.Top;
            FeedbackPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            FeedbackPanel.Name = "FeedbackPanel";

            DivisoriaFeedbackImagem = new PictureBox();
            DivisoriaFeedbackImagem.Size = new Size(0, 4);
            DivisoriaFeedbackImagem.Dock = DockStyle.Bottom;
            DivisoriaFeedbackImagem.BackColor = Color.Silver;

            FeedbackLabel = new Label();
            FeedbackLabel.AutoSize = false;
            FeedbackLabel.Size = new Size(_widthLabelMenuSistema, 0);
            FeedbackLabel.Dock = DockStyle.Fill;
            FeedbackLabel.Text = "Feedback";
            FeedbackLabel.Font = CorFontepadraoLabel.Font;
            FeedbackLabel.ForeColor = Color.WhiteSmoke;
            FeedbackLabel.TextAlign = ContentAlignment.MiddleRight;
            FeedbackLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            FeedbackLabel.Click += new System.EventHandler(FeedbackColaboradorPanel_Click);
            FeedbackLabel.Name = "FeedbackLabel";

            FeedbackPanel.Controls.Add(FeedbackLabel);
            FeedbackPanel.Controls.Add(DivisoriaFeedbackImagem);

            SubMenuADColaborador.Controls.Add(FeedbackPanel);
            #endregion            

            #region AutoAvaliacao
            AutoAvaliacaoPanel = new Panel();
            AutoAvaliacaoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AutoAvaliacaoPanel.Dock = DockStyle.Top;
            AutoAvaliacaoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AutoAvaliacaoPanel.Name = "AutoAvaliacaoPanel";

            DivisoriaAutoAvaliacaoImagem = new PictureBox();
            DivisoriaAutoAvaliacaoImagem.Size = new Size(0, 4);
            DivisoriaAutoAvaliacaoImagem.Dock = DockStyle.Bottom;
            DivisoriaAutoAvaliacaoImagem.BackColor = Color.Silver;

            AutoAvaliacaoLabel = new Label();
            AutoAvaliacaoLabel.AutoSize = false;
            AutoAvaliacaoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AutoAvaliacaoLabel.Dock = DockStyle.Fill;
            AutoAvaliacaoLabel.Text = "Auto Avaliação";
            AutoAvaliacaoLabel.Font = CorFontepadraoLabel.Font;
            AutoAvaliacaoLabel.ForeColor = Color.WhiteSmoke;
            AutoAvaliacaoLabel.TextAlign = ContentAlignment.MiddleRight;
            AutoAvaliacaoLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            AutoAvaliacaoLabel.Click += new System.EventHandler(AutoAvaliacaoPanel_Click);
            AutoAvaliacaoLabel.Name = "AutoAvaliacaoLabel";

            AutoAvaliacaoPanel.Controls.Add(AutoAvaliacaoLabel);
            AutoAvaliacaoPanel.Controls.Add(DivisoriaAutoAvaliacaoImagem);

            SubMenuADColaborador.Controls.Add(AutoAvaliacaoPanel);
            #endregion            

            #region MetasNumericas
            MetasNumericasPanel = new Panel();
            MetasNumericasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            MetasNumericasPanel.Dock = DockStyle.Top;
            MetasNumericasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            MetasNumericasPanel.Name = "MetasNumericasPanel";

            DivisoriaMetasNumericasImagem = new PictureBox();
            DivisoriaMetasNumericasImagem.Size = new Size(0, 4);
            DivisoriaMetasNumericasImagem.Dock = DockStyle.Bottom;
            DivisoriaMetasNumericasImagem.BackColor = Color.Silver;

            MetasNumericasLabel = new Label();
            MetasNumericasLabel.AutoSize = false;
            MetasNumericasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            MetasNumericasLabel.Dock = DockStyle.Fill;
            MetasNumericasLabel.Text = "Metas Numéricas";
            MetasNumericasLabel.Font = CorFontepadraoLabel.Font;
            MetasNumericasLabel.ForeColor = Color.WhiteSmoke;
            MetasNumericasLabel.TextAlign = ContentAlignment.MiddleRight;
            MetasNumericasLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            MetasNumericasLabel.Click += new System.EventHandler(MetasNumericasColaboradorPanel_Click);
            MetasNumericasLabel.Name = "MetasNumericasLabel";

            MetasNumericasPanel.Controls.Add(MetasNumericasLabel);
            MetasNumericasPanel.Controls.Add(DivisoriaMetasNumericasImagem);

            SubMenuADColaborador.Controls.Add(MetasNumericasPanel);
            #endregion            
        }

        private void MudaSelecaoDeCoresSubADColaborador()
        {
            MetasNumericasLabel.Font = new Font(MetasNumericasLabel.Font, MetasNumericasLabel.Font.Style & ~FontStyle.Bold);
            AutoAvaliacaoLabel.Font = new Font(AutoAvaliacaoLabel.Font, AutoAvaliacaoLabel.Font.Style & ~FontStyle.Bold);
            FeedbackLabel.Font = new Font(FeedbackLabel.Font, FeedbackLabel.Font.Style & ~FontStyle.Bold);
            RadarLabel.Font = new Font(RadarLabel.Font, RadarLabel.Font.Style & ~FontStyle.Bold);

            MetasNumericasLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AutoAvaliacaoLabel.BackColor = ControladoriaPanel.BackColor;
            FeedbackLabel.BackColor = ControladoriaPanel.BackColor;
            RadarLabel.BackColor = ControladoriaPanel.BackColor;

            MetasNumericasLabel.ForeColor = Color.WhiteSmoke;
            AutoAvaliacaoLabel.ForeColor = Color.WhiteSmoke;
            FeedbackLabel.ForeColor = Color.WhiteSmoke;
            RadarLabel.ForeColor = Color.WhiteSmoke;

            if (CadastrosRHLabel != null)
            {
                CadastrosRHLabel.Font = new Font(CadastrosRHLabel.Font, CadastrosRHLabel.Font.Style & ~FontStyle.Bold);
                CadastrosRHLabel.BackColor = MetasNumericasLabel.BackColor;
                CadastrosRHLabel.ForeColor = Color.WhiteSmoke;
                CadastrosRHSetaLabel.Font = new Font(CadastrosRHSetaLabel.Font, CadastrosRHSetaLabel.Font.Style & ~FontStyle.Bold);
                CadastrosRHSetaLabel.BackColor = MetasNumericasLabel.BackColor;
                CadastrosRHLabel.ForeColor = Color.WhiteSmoke;
                CadastrosRHSetaLabel.Text = "6";
            }

            if (NotasLabel != null)
            {
                NotasLabel.Font = new Font(NotasLabel.Font, NotasLabel.Font.Style & ~FontStyle.Bold);
                NotasLabel.BackColor = ControladoriaPanel.BackColor;
                NotasLabel.ForeColor = Color.WhiteSmoke;
            }
        }

        private void MetasNumericasLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubADColaborador();
            FechaMenuUsuario();
            FechaSubMenuCadastrosRH();

            if (((Control)sender).Name.Contains("MetasNumericas"))
            {
                MetasNumericasLabel.Font = new Font(MetasNumericasLabel.Font, FontStyle.Bold);
                MetasNumericasLabel.ForeColor = Publicas._fonteBotaoFocado;
                MetasNumericasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AutoAvaliacao"))
            {
                AutoAvaliacaoLabel.Font = new Font(AutoAvaliacaoLabel.Font, FontStyle.Bold);
                AutoAvaliacaoLabel.ForeColor = Publicas._fonteBotaoFocado;
                AutoAvaliacaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Feedback"))
            {
                FeedbackLabel.Font = new Font(FeedbackLabel.Font, FontStyle.Bold);
                FeedbackLabel.ForeColor = Publicas._fonteBotaoFocado;
                FeedbackLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Radar"))
            {
                RadarLabel.Font = new Font(RadarLabel.Font, FontStyle.Bold);
                RadarLabel.ForeColor = Publicas._fonteBotaoFocado;
                RadarLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Notas"))
            {
                NotasLabel.Font = new Font(NotasLabel.Font, FontStyle.Bold);
                NotasLabel.ForeColor = Publicas._fonteBotaoFocado;
                NotasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("CadastrosRH"))
            {
                CadastrosRHLabel.Font = new Font(CadastrosRHLabel.Font, FontStyle.Bold);
                CadastrosRHLabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastrosRHLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void MetasNumericasColaboradorPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.DefinicaoDeMetas _tela = new Avaliacao_de_desempenho.DefinicaoDeMetas();
            _tela.tituloLabel.Text = "Metas Numéricas";

            _tela.empresaComboBoxAdv.Enabled = false;
            _tela.usuarioTextBox.Enabled = false;

            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void AutoAvaliacaoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.AvaliacaoComportamental _tela = new Avaliacao_de_desempenho.AvaliacaoComportamental();
            _tela.tituloLabel.Text = "Auto Avaliação";
            _tela.empresaComboBoxAdv.Enabled = false;
            _tela.usuarioTextBox.Enabled = false;
            _tela.tipoAvaliacao = Publicas.TipoPrazos.AutoAvaliacao;
            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void FeedbackColaboradorPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Feedback _tela = new Avaliacao_de_desempenho.Feedback();
            _tela.Size = new Size(718, 569);
            _tela.empresaComboBoxAdv.Enabled = false;
            _tela.usuarioTextBox.Enabled = false;

            _tela.tipoAvaliacao = Publicas.TipoPrazos.FeedbackAvaliado;
            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void RadarColaboradorPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            Publicas._telaRadarChamadaPeloMenu = "Colaborador";
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Radar _tela = new Avaliacao_de_desempenho.Radar();
            _tela.empresaComboBoxAdv.Enabled = false;
            _tela.usuarioTextBox.Enabled = false;
            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void FechaSubMenuADColaborador()
        {
            if (SubMenuADColaborador != null)
            {
                SubMenuADColaborador.Visible = false;
                this.Controls.Remove(SubMenuADColaborador);
                SubMenuADColaborador.Dispose();
                SubMenuADColaborador = null;
            }
        }
        #endregion

        #region SubMenu Gestor
        private void AD_GestorPanel_Click(object sender, EventArgs e)
        {
            AD_GestorSetaLabel.Text = "3";
            AD_GestorSetaLabel.ForeColor = Publicas._bordaEntrada;
            AD_GestorLabel.ForeColor = Publicas._bordaEntrada;

            FechaSubMenuADColaborador();
            FechaSubMenuADPlanejamento();
            FechaSubMenuADRecursosHumanos();

            if (SubMenuADGestor != null)
            {
                FechaSubMenuADGestor();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuADGestor = new Panel();
            SubMenuADGestor.Size = new Size(145, _heidthMenuSistema * 4);
            SubMenuADGestor.Location = new Point(MenuSistemaPanel.Width + SubMenuRecursosHumanos.Width + SubMenuAvaliacaoDesempenho.Width + 6, AcessoAoMenuPanel.Height);
            SubMenuADGestor.BackColor = Color.Silver;
            this.Controls.Add(SubMenuADGestor);
            SubMenuADGestor.BringToFront();
            SubMenuADGestor.Visible = true;
            #endregion

            #region Radar
            RadarPanel = new Panel();
            RadarPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            RadarPanel.Dock = DockStyle.Top;
            RadarPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            RadarPanel.Name = "RadarPanel";

            DivisoriaRadarImagem = new PictureBox();
            DivisoriaRadarImagem.Size = new Size(0, 4);
            DivisoriaRadarImagem.Dock = DockStyle.Bottom;
            DivisoriaRadarImagem.BackColor = Color.Silver;

            RadarLabel = new Label();
            RadarLabel.AutoSize = false;
            RadarLabel.Size = new Size(_widthLabelMenuSistema, 0);
            RadarLabel.Dock = DockStyle.Fill;
            RadarLabel.Text = "Radar";
            RadarLabel.Font = CorFontepadraoLabel.Font;
            RadarLabel.ForeColor = Color.WhiteSmoke;
            RadarLabel.TextAlign = ContentAlignment.MiddleRight;
            RadarLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            RadarLabel.Click += new System.EventHandler(RadarGestorPanel_Click);
            RadarLabel.Name = "RadarLabel";

            RadarPanel.Controls.Add(RadarLabel);
            RadarPanel.Controls.Add(DivisoriaRadarImagem);

            SubMenuADGestor.Controls.Add(RadarPanel);
            #endregion

            #region Feedback
            FeedbackPanel = new Panel();
            FeedbackPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            FeedbackPanel.Dock = DockStyle.Top;
            FeedbackPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            FeedbackPanel.Name = "FeedbackPanel";

            DivisoriaFeedbackImagem = new PictureBox();
            DivisoriaFeedbackImagem.Size = new Size(0, 4);
            DivisoriaFeedbackImagem.Dock = DockStyle.Bottom;
            DivisoriaFeedbackImagem.BackColor = Color.Silver;

            FeedbackLabel = new Label();
            FeedbackLabel.AutoSize = false;
            FeedbackLabel.Size = new Size(_widthLabelMenuSistema, 0);
            FeedbackLabel.Dock = DockStyle.Fill;
            FeedbackLabel.Text = "Feedback";
            FeedbackLabel.Font = CorFontepadraoLabel.Font;
            FeedbackLabel.ForeColor = Color.WhiteSmoke;
            FeedbackLabel.TextAlign = ContentAlignment.MiddleRight;
            FeedbackLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            FeedbackLabel.Click += new System.EventHandler(FeedbackGestorPanel_Click);
            FeedbackLabel.Name = "FeedbackLabel";

            FeedbackPanel.Controls.Add(FeedbackLabel);
            FeedbackPanel.Controls.Add(DivisoriaFeedbackImagem);

            SubMenuADGestor.Controls.Add(FeedbackPanel);
            #endregion            

            #region AutoAvaliacao
            AutoAvaliacaoPanel = new Panel();
            AutoAvaliacaoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AutoAvaliacaoPanel.Dock = DockStyle.Top;
            AutoAvaliacaoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AutoAvaliacaoPanel.Name = "AutoAvaliacaoPanel";

            DivisoriaAutoAvaliacaoImagem = new PictureBox();
            DivisoriaAutoAvaliacaoImagem.Size = new Size(0, 4);
            DivisoriaAutoAvaliacaoImagem.Dock = DockStyle.Bottom;
            DivisoriaAutoAvaliacaoImagem.BackColor = Color.Silver;

            AutoAvaliacaoLabel = new Label();
            AutoAvaliacaoLabel.AutoSize = false;
            AutoAvaliacaoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AutoAvaliacaoLabel.Dock = DockStyle.Fill;
            AutoAvaliacaoLabel.Text = "Avaliação Qualitativa";
            AutoAvaliacaoLabel.Font = CorFontepadraoLabel.Font;
            AutoAvaliacaoLabel.ForeColor = Color.WhiteSmoke;
            AutoAvaliacaoLabel.TextAlign = ContentAlignment.MiddleRight;
            AutoAvaliacaoLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            AutoAvaliacaoLabel.Click += new System.EventHandler(AvaliacaoQualitativaPanel_Click);
            AutoAvaliacaoLabel.Name = "AutoAvaliacaoLabel";

            AutoAvaliacaoPanel.Controls.Add(AutoAvaliacaoLabel);
            AutoAvaliacaoPanel.Controls.Add(DivisoriaAutoAvaliacaoImagem);

            SubMenuADGestor.Controls.Add(AutoAvaliacaoPanel);
            #endregion            

            #region MetasNumericas
            MetasNumericasPanel = new Panel();
            MetasNumericasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            MetasNumericasPanel.Dock = DockStyle.Top;
            MetasNumericasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            MetasNumericasPanel.Name = "MetasNumericasPanel";

            DivisoriaMetasNumericasImagem = new PictureBox();
            DivisoriaMetasNumericasImagem.Size = new Size(0, 4);
            DivisoriaMetasNumericasImagem.Dock = DockStyle.Bottom;
            DivisoriaMetasNumericasImagem.BackColor = Color.Silver;

            MetasNumericasLabel = new Label();
            MetasNumericasLabel.AutoSize = false;
            MetasNumericasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            MetasNumericasLabel.Dock = DockStyle.Fill;
            MetasNumericasLabel.Text = "Metas Numéricas";
            MetasNumericasLabel.Font = CorFontepadraoLabel.Font;
            MetasNumericasLabel.ForeColor = Color.WhiteSmoke;
            MetasNumericasLabel.TextAlign = ContentAlignment.MiddleRight;
            MetasNumericasLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            MetasNumericasLabel.Click += new System.EventHandler(MetasNumericasGestorPanel_Click);
            MetasNumericasLabel.Name = "MetasNumericasLabel";

            MetasNumericasPanel.Controls.Add(MetasNumericasLabel);
            MetasNumericasPanel.Controls.Add(DivisoriaMetasNumericasImagem);

            SubMenuADGestor.Controls.Add(MetasNumericasPanel);
            #endregion            
        }
               
        private void MetasNumericasGestorPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.DefinicaoDeMetas _tela = new Avaliacao_de_desempenho.DefinicaoDeMetas();
            _tela.tituloLabel.Text = "Metas Numéricas - Gestor";
            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void AvaliacaoQualitativaPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.AvaliacaoComportamental _tela = new Avaliacao_de_desempenho.AvaliacaoComportamental();
            _tela.tituloLabel.Text = "Avaliação comportamental da equipe - pelo Gestor";
            _tela.empresaComboBoxAdv.Enabled = true;
            _tela.usuarioTextBox.Enabled = true;

            _tela.tipoAvaliacao = Publicas.TipoPrazos.AvaliacaoDoGestor;
            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void FeedbackGestorPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Feedback _tela = new Avaliacao_de_desempenho.Feedback();
            _tela.Size = new Size(718, 383);
            _tela.empresaComboBoxAdv.Enabled = true;
            _tela.usuarioTextBox.Enabled = true;

            _tela.tipoAvaliacao = Publicas.TipoPrazos.FeedbackGestor;
            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void RadarGestorPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            Publicas._telaRadarChamadaPeloMenu = "Gestor";
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Radar _tela = new Avaliacao_de_desempenho.Radar();
            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void FechaSubMenuADGestor()
        {
            if (SubMenuADGestor != null)
            {
                SubMenuADGestor.Visible = false;
                this.Controls.Remove(SubMenuADGestor);
                SubMenuADGestor.Dispose();
                SubMenuADGestor = null;
            }
        }
        #endregion

        #region SubMenu Recursos Humanos
        private void AD_RecursosHumanosPanel_Click(object sender, EventArgs e)
        {
            AD_RecursosHumanosSetaLabel.Text = "3";
            AD_RecursosHumanosSetaLabel.ForeColor = Publicas._bordaEntrada;
            AD_RecursosHumanosLabel.ForeColor = Publicas._bordaEntrada;

            FechaSubMenuADColaborador();
            FechaSubMenuADPlanejamento();
            FechaSubMenuADGestor();

            if (SubMenuADRecursosHumanos != null)
            {
                FechaSubMenuADRecursosHumanos();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuADRecursosHumanos = new Panel();
            SubMenuADRecursosHumanos.Size = new Size(145, _heidthMenuSistema * 6);
            SubMenuADRecursosHumanos.Location = new Point(MenuSistemaPanel.Width + SubMenuRecursosHumanos.Width + SubMenuAvaliacaoDesempenho.Width + 6, AcessoAoMenuPanel.Height);
            SubMenuADRecursosHumanos.BackColor = Color.Silver;
            this.Controls.Add(SubMenuADRecursosHumanos);
            SubMenuADRecursosHumanos.BringToFront();
            SubMenuADRecursosHumanos.Visible = true;
            #endregion

            #region Notas
            NotasPanel = new Panel();
            NotasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            NotasPanel.Dock = DockStyle.Top;
            NotasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            NotasPanel.Name = "NotasPanel";

            DivisoriaNotasImagem = new PictureBox();
            DivisoriaNotasImagem.Size = new Size(0, 4);
            DivisoriaNotasImagem.Dock = DockStyle.Bottom;
            DivisoriaNotasImagem.BackColor = Color.Silver;

            NotasLabel = new Label();
            NotasLabel.AutoSize = false;
            NotasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            NotasLabel.Dock = DockStyle.Fill;
            NotasLabel.Text = "Notas";
            NotasLabel.Font = CorFontepadraoLabel.Font;
            NotasLabel.ForeColor = Color.WhiteSmoke;
            NotasLabel.TextAlign = ContentAlignment.MiddleRight;
            NotasLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            NotasLabel.Click += new System.EventHandler(NotasPanel_Click);
            NotasLabel.Name = "NotasLabel";

            NotasPanel.Controls.Add(NotasLabel);
            NotasPanel.Controls.Add(DivisoriaNotasImagem);

            SubMenuADRecursosHumanos.Controls.Add(NotasPanel);
            #endregion

            #region Radar
            RadarPanel = new Panel();
            RadarPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            RadarPanel.Dock = DockStyle.Top;
            RadarPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            RadarPanel.Name = "RadarPanel";

            DivisoriaRadarImagem = new PictureBox();
            DivisoriaRadarImagem.Size = new Size(0, 4);
            DivisoriaRadarImagem.Dock = DockStyle.Bottom;
            DivisoriaRadarImagem.BackColor = Color.Silver;

            RadarLabel = new Label();
            RadarLabel.AutoSize = false;
            RadarLabel.Size = new Size(_widthLabelMenuSistema, 0);
            RadarLabel.Dock = DockStyle.Fill;
            RadarLabel.Text = "Radar";
            RadarLabel.Font = CorFontepadraoLabel.Font;
            RadarLabel.ForeColor = Color.WhiteSmoke;
            RadarLabel.TextAlign = ContentAlignment.MiddleRight;
            RadarLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            RadarLabel.Click += new System.EventHandler(RadarRecursosHumanosPanel_Click);
            RadarLabel.Name = "RadarLabel";

            RadarPanel.Controls.Add(RadarLabel);
            RadarPanel.Controls.Add(DivisoriaRadarImagem);

            SubMenuADRecursosHumanos.Controls.Add(RadarPanel);
            #endregion

            #region Feedback
            FeedbackPanel = new Panel();
            FeedbackPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            FeedbackPanel.Dock = DockStyle.Top;
            FeedbackPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            FeedbackPanel.Name = "FeedbackPanel";

            DivisoriaFeedbackImagem = new PictureBox();
            DivisoriaFeedbackImagem.Size = new Size(0, 4);
            DivisoriaFeedbackImagem.Dock = DockStyle.Bottom;
            DivisoriaFeedbackImagem.BackColor = Color.Silver;

            FeedbackLabel = new Label();
            FeedbackLabel.AutoSize = false;
            FeedbackLabel.Size = new Size(_widthLabelMenuSistema, 0);
            FeedbackLabel.Dock = DockStyle.Fill;
            FeedbackLabel.Text = "Feedback";
            FeedbackLabel.Font = CorFontepadraoLabel.Font;
            FeedbackLabel.ForeColor = Color.WhiteSmoke;
            FeedbackLabel.TextAlign = ContentAlignment.MiddleRight;
            FeedbackLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            FeedbackLabel.Click += new System.EventHandler(FeedbackRecursosHumanosPanel_Click);
            FeedbackLabel.Name = "FeedbackLabel";

            FeedbackPanel.Controls.Add(FeedbackLabel);
            FeedbackPanel.Controls.Add(DivisoriaFeedbackImagem);

            SubMenuADRecursosHumanos.Controls.Add(FeedbackPanel);
            #endregion            

            #region AutoAvaliacao
            AutoAvaliacaoPanel = new Panel();
            AutoAvaliacaoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AutoAvaliacaoPanel.Dock = DockStyle.Top;
            AutoAvaliacaoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AutoAvaliacaoPanel.Name = "AutoAvaliacaoPanel";

            DivisoriaAutoAvaliacaoImagem = new PictureBox();
            DivisoriaAutoAvaliacaoImagem.Size = new Size(0, 4);
            DivisoriaAutoAvaliacaoImagem.Dock = DockStyle.Bottom;
            DivisoriaAutoAvaliacaoImagem.BackColor = Color.Silver;

            AutoAvaliacaoLabel = new Label();
            AutoAvaliacaoLabel.AutoSize = false;
            AutoAvaliacaoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AutoAvaliacaoLabel.Dock = DockStyle.Fill;
            AutoAvaliacaoLabel.Text = "Avaliação Qualitativa";
            AutoAvaliacaoLabel.Font = CorFontepadraoLabel.Font;
            AutoAvaliacaoLabel.ForeColor = Color.WhiteSmoke;
            AutoAvaliacaoLabel.TextAlign = ContentAlignment.MiddleRight;
            AutoAvaliacaoLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            AutoAvaliacaoLabel.Click += new System.EventHandler(AvaliacaoQualitativaRecursosHumanosPanel_Click);
            AutoAvaliacaoLabel.Name = "AutoAvaliacaoLabel";

            AutoAvaliacaoPanel.Controls.Add(AutoAvaliacaoLabel);
            AutoAvaliacaoPanel.Controls.Add(DivisoriaAutoAvaliacaoImagem);

            SubMenuADRecursosHumanos.Controls.Add(AutoAvaliacaoPanel);
            #endregion            

            #region MetasNumericas
            MetasNumericasPanel = new Panel();
            MetasNumericasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            MetasNumericasPanel.Dock = DockStyle.Top;
            MetasNumericasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            MetasNumericasPanel.Name = "MetasNumericasPanel";

            DivisoriaMetasNumericasImagem = new PictureBox();
            DivisoriaMetasNumericasImagem.Size = new Size(0, 4);
            DivisoriaMetasNumericasImagem.Dock = DockStyle.Bottom;
            DivisoriaMetasNumericasImagem.BackColor = Color.Silver;

            MetasNumericasLabel = new Label();
            MetasNumericasLabel.AutoSize = false;
            MetasNumericasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            MetasNumericasLabel.Dock = DockStyle.Fill;
            MetasNumericasLabel.Text = "Metas Numéricas";
            MetasNumericasLabel.Font = CorFontepadraoLabel.Font;
            MetasNumericasLabel.ForeColor = Color.WhiteSmoke;
            MetasNumericasLabel.TextAlign = ContentAlignment.MiddleRight;
            MetasNumericasLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            MetasNumericasLabel.Click += new System.EventHandler(MetasNumericasRecursosHumanosPanel_Click);
            MetasNumericasLabel.Name = "MetasNumericasLabel";

            MetasNumericasPanel.Controls.Add(MetasNumericasLabel);
            MetasNumericasPanel.Controls.Add(DivisoriaMetasNumericasImagem);

            SubMenuADRecursosHumanos.Controls.Add(MetasNumericasPanel);
            #endregion            

            #region Cadastros 
            CadastrosRHPanel = new Panel();
            CadastrosRHPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CadastrosRHPanel.Dock = DockStyle.Top;
            CadastrosRHPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CadastrosRHPanel.Name = "CadastrosRHPanel";

            DivisoriaCadastrosRHImagem = new PictureBox();
            DivisoriaCadastrosRHImagem.Size = new Size(0, 4);
            DivisoriaCadastrosRHImagem.Dock = DockStyle.Bottom;
            DivisoriaCadastrosRHImagem.BackColor = Color.Silver;

            Divisoria2CadastrosRHImagem = new PictureBox();
            Divisoria2CadastrosRHImagem.Size = new Size(1, 2);
            Divisoria2CadastrosRHImagem.Dock = DockStyle.Right;
            Divisoria2CadastrosRHImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            CadastrosRHSetaLabel = new Label();
            CadastrosRHSetaLabel.AutoSize = false;
            CadastrosRHSetaLabel.Size = new Size(20, 59);
            CadastrosRHSetaLabel.Dock = DockStyle.Right;
            CadastrosRHSetaLabel.Text = "6";
            CadastrosRHSetaLabel.Font = new Font("Webdings", (float)12);
            CadastrosRHSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            CadastrosRHSetaLabel.Click += new System.EventHandler(this.CadastrosRHSetaLabel_Click);
            CadastrosRHSetaLabel.MouseHover += new EventHandler(this.CadastrosRHSetaLabel_MouseHover);
            CadastrosRHSetaLabel.Name = "CadastrosRHSetaLabel";
            CadastrosRHSetaLabel.ForeColor = Color.WhiteSmoke;

            CadastrosRHLabel = new Label();
            CadastrosRHLabel.AutoSize = false;
            CadastrosRHLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CadastrosRHLabel.Dock = DockStyle.Fill;
            CadastrosRHLabel.Text = "Cadastros";
            CadastrosRHLabel.Font = CorFontepadraoLabel.Font;
            CadastrosRHLabel.ForeColor = Color.WhiteSmoke;
            CadastrosRHLabel.TextAlign = ContentAlignment.MiddleRight;
            CadastrosRHLabel.MouseHover += new EventHandler(this.MetasNumericasLabel_MouseHover);
            CadastrosRHLabel.Name = "CadastrosRHLabel";

            CadastrosRHPanel.Controls.Add(CadastrosRHLabel);
            //CadastrosRHPanel.Controls.Add(CadastrosRHImagem);
            CadastrosRHPanel.Controls.Add(Divisoria2CadastrosRHImagem);
            CadastrosRHPanel.Controls.Add(CadastrosRHSetaLabel);
            CadastrosRHPanel.Controls.Add(DivisoriaCadastrosRHImagem);

            SubMenuADRecursosHumanos.Controls.Add(CadastrosRHPanel);
            #endregion
        }
        
        private void CadastrosRHSetaLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubADColaborador();
            FechaMenuUsuario();

            if (((Control)sender).Name.Contains("CadastrosRH"))
            {
                CadastrosRHSetaLabel.Font = new Font(CadastrosRHSetaLabel.Font, FontStyle.Bold);
                CadastrosRHSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastrosRHSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void MetasNumericasRecursosHumanosPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.DefinicaoDeMetas _tela = new Avaliacao_de_desempenho.DefinicaoDeMetas();
            _tela.tituloLabel.Text = "Metas Numéricas - RH";
            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void AvaliacaoQualitativaRecursosHumanosPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.AvaliacaoComportamental _tela = new Avaliacao_de_desempenho.AvaliacaoComportamental();
            _tela.tituloLabel.Text = "Avaliação comportamental da equipe - pelo Recursos Humanos";
            _tela.empresaComboBoxAdv.Enabled = true;
            _tela.usuarioTextBox.Enabled = true;

            _tela.tipoAvaliacao = Publicas.TipoPrazos.AvaliacaoRH;
            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void FeedbackRecursosHumanosPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Feedback _tela = new Avaliacao_de_desempenho.Feedback();
            _tela.Size = new Size(718, 569);
            _tela.empresaComboBoxAdv.Enabled = true;
            _tela.usuarioTextBox.Enabled = true;

            _tela.tipoAvaliacao = Publicas.TipoPrazos.RHConsultaFeedback;
            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void RadarRecursosHumanosPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            Publicas._telaRadarChamadaPeloMenu = "RH";
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Radar _tela = new Avaliacao_de_desempenho.Radar();
            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void NotasPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Notas _tela = new Avaliacao_de_desempenho.Notas();

            _tela.Size = new Size(this.Width - 5, this.Height - (tituloPanel.Height + panel2.Height + 2));
            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void FechaSubMenuADRecursosHumanos()
        {
            FechaSubMenuCadastrosRH();
            if (SubMenuADRecursosHumanos != null)
            {
                SubMenuADRecursosHumanos.Visible = false;
                this.Controls.Remove(SubMenuADRecursosHumanos);
                SubMenuADRecursosHumanos.Dispose();
                SubMenuADRecursosHumanos = null;
            }
        }

        #region SubMenu Cadastros
        private void CadastrosRHSetaLabel_Click(object sender, EventArgs e)
        {
            CadastrosRHSetaLabel.Text = "3";
            CadastrosRHSetaLabel.ForeColor = Publicas._bordaEntrada;
            CadastrosRHLabel.ForeColor = Publicas._bordaEntrada;

            FechaSubMenuADColaborador();
            FechaSubMenuADPlanejamento();
            FechaSubMenuADGestor();

            if (SubMenuCadastrosRH != null)
            {
                FechaSubMenuCadastrosRH();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuCadastrosRH = new Panel();
            SubMenuCadastrosRH.Size = new Size(145, _heidthMenuSistema * 8);
            SubMenuCadastrosRH.Location = new Point(MenuSistemaPanel.Width + SubMenuRecursosHumanos.Width + 
                                SubMenuAvaliacaoDesempenho.Width + SubMenuRecursosHumanos.Width + 8, AcessoAoMenuPanel.Height);
            SubMenuCadastrosRH.BackColor = Color.Silver;
            this.Controls.Add(SubMenuCadastrosRH);
            SubMenuCadastrosRH.BringToFront();
            SubMenuCadastrosRH.Visible = true;
            #endregion

            #region Pontuacao
            PontuacaoPanel = new Panel();
            PontuacaoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            PontuacaoPanel.Dock = DockStyle.Top;
            PontuacaoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            PontuacaoPanel.Name = "PontuacaoPanel";

            DivisoriaPontuacaoImagem = new PictureBox();
            DivisoriaPontuacaoImagem.Size = new Size(0, 4);
            DivisoriaPontuacaoImagem.Dock = DockStyle.Bottom;
            DivisoriaPontuacaoImagem.BackColor = Color.Silver;

            PontuacaoLabel = new Label();
            PontuacaoLabel.AutoSize = false;
            PontuacaoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            PontuacaoLabel.Dock = DockStyle.Fill;
            PontuacaoLabel.Text = "Pontuação";
            PontuacaoLabel.Font = CorFontepadraoLabel.Font;
            PontuacaoLabel.ForeColor = Color.WhiteSmoke;
            PontuacaoLabel.TextAlign = ContentAlignment.MiddleRight;
            PontuacaoLabel.MouseHover += new EventHandler(this.NineBoxLabel_MouseHover);
            PontuacaoLabel.Click += new System.EventHandler(PontuacaoPanel_Click);
            PontuacaoLabel.Name = "PontuacaoLabel";

            PontuacaoPanel.Controls.Add(PontuacaoLabel);
            PontuacaoPanel.Controls.Add(DivisoriaPontuacaoImagem);

            SubMenuCadastrosRH.Controls.Add(PontuacaoPanel);
            #endregion

            #region Departamento
            DepartamentoPanel = new Panel();
            DepartamentoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            DepartamentoPanel.Dock = DockStyle.Top;
            DepartamentoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            DepartamentoPanel.Name = "DepartamentoPanel";

            DivisoriaDepartamentoImagem = new PictureBox();
            DivisoriaDepartamentoImagem.Size = new Size(0, 4);
            DivisoriaDepartamentoImagem.Dock = DockStyle.Bottom;
            DivisoriaDepartamentoImagem.BackColor = Color.Silver;

            DepartamentoLabel = new Label();
            DepartamentoLabel.AutoSize = false;
            DepartamentoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            DepartamentoLabel.Dock = DockStyle.Fill;
            DepartamentoLabel.Text = "Departamento";
            DepartamentoLabel.Font = CorFontepadraoLabel.Font;
            DepartamentoLabel.ForeColor = Color.WhiteSmoke;
            DepartamentoLabel.TextAlign = ContentAlignment.MiddleRight;
            DepartamentoLabel.MouseHover += new EventHandler(this.NineBoxLabel_MouseHover);
            DepartamentoLabel.Click += new System.EventHandler(DepartamentoPanel_Click);
            DepartamentoLabel.Name = "DepartamentoLabel";

            DepartamentoPanel.Controls.Add(DepartamentoLabel);
            DepartamentoPanel.Controls.Add(DivisoriaDepartamentoImagem);

            SubMenuCadastrosRH.Controls.Add(DepartamentoPanel);
            #endregion

            #region Competencias
            CompetenciasPanel = new Panel();
            CompetenciasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CompetenciasPanel.Dock = DockStyle.Top;
            CompetenciasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CompetenciasPanel.Name = "CompetenciasPanel";

            DivisoriaCompetenciasImagem = new PictureBox();
            DivisoriaCompetenciasImagem.Size = new Size(0, 4);
            DivisoriaCompetenciasImagem.Dock = DockStyle.Bottom;
            DivisoriaCompetenciasImagem.BackColor = Color.Silver;

            CompetenciasLabel = new Label();
            CompetenciasLabel.AutoSize = false;
            CompetenciasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CompetenciasLabel.Dock = DockStyle.Fill;
            CompetenciasLabel.Text = "Competências";
            CompetenciasLabel.Font = CorFontepadraoLabel.Font;
            CompetenciasLabel.ForeColor = Color.WhiteSmoke;
            CompetenciasLabel.TextAlign = ContentAlignment.MiddleRight;
            CompetenciasLabel.MouseHover += new EventHandler(this.NineBoxLabel_MouseHover);
            CompetenciasLabel.Click += new System.EventHandler(CompetenciasPanel_Click);
            CompetenciasLabel.Name = "CompetenciasLabel";

            CompetenciasPanel.Controls.Add(CompetenciasLabel);
            CompetenciasPanel.Controls.Add(DivisoriaCompetenciasImagem);

            SubMenuCadastrosRH.Controls.Add(CompetenciasPanel);
            #endregion

            #region Escolaridade
            EscolaridadePanel = new Panel();
            EscolaridadePanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            EscolaridadePanel.Dock = DockStyle.Top;
            EscolaridadePanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            EscolaridadePanel.Name = "EscolaridadePanel";

            DivisoriaEscolaridadeImagem = new PictureBox();
            DivisoriaEscolaridadeImagem.Size = new Size(0, 4);
            DivisoriaEscolaridadeImagem.Dock = DockStyle.Bottom;
            DivisoriaEscolaridadeImagem.BackColor = Color.Silver;

            EscolaridadeLabel = new Label();
            EscolaridadeLabel.AutoSize = false;
            EscolaridadeLabel.Size = new Size(_widthLabelMenuSistema, 0);
            EscolaridadeLabel.Dock = DockStyle.Fill;
            EscolaridadeLabel.Text = "Escolaridade";
            EscolaridadeLabel.Font = CorFontepadraoLabel.Font;
            EscolaridadeLabel.ForeColor = Color.WhiteSmoke;
            EscolaridadeLabel.TextAlign = ContentAlignment.MiddleRight;
            EscolaridadeLabel.MouseHover += new EventHandler(this.NineBoxLabel_MouseHover);
            EscolaridadeLabel.Click += new System.EventHandler(EscolaridadePanel_Click);
            EscolaridadeLabel.Name = "EscolaridadeLabel";

            EscolaridadePanel.Controls.Add(EscolaridadeLabel);
            EscolaridadePanel.Controls.Add(DivisoriaEscolaridadeImagem);

            SubMenuCadastrosRH.Controls.Add(EscolaridadePanel);
            #endregion

            #region Cargos
            CargosPanel = new Panel();
            CargosPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CargosPanel.Dock = DockStyle.Top;
            CargosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CargosPanel.Name = "CargosPanel";

            DivisoriaCargosImagem = new PictureBox();
            DivisoriaCargosImagem.Size = new Size(0, 4);
            DivisoriaCargosImagem.Dock = DockStyle.Bottom;
            DivisoriaCargosImagem.BackColor = Color.Silver;

            CargosLabel = new Label();
            CargosLabel.AutoSize = false;
            CargosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CargosLabel.Dock = DockStyle.Fill;
            CargosLabel.Text = "Cargos";
            CargosLabel.Font = CorFontepadraoLabel.Font;
            CargosLabel.ForeColor = Color.WhiteSmoke;
            CargosLabel.TextAlign = ContentAlignment.MiddleRight;
            CargosLabel.MouseHover += new EventHandler(this.NineBoxLabel_MouseHover);
            CargosLabel.Click += new System.EventHandler(CargosPanel_Click);
            CargosLabel.Name = "CargosLabel";

            CargosPanel.Controls.Add(CargosLabel);
            CargosPanel.Controls.Add(DivisoriaCargosImagem);

            SubMenuCadastrosRH.Controls.Add(CargosPanel);
            #endregion

            #region Prazos
            PrazosPanel = new Panel();
            PrazosPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            PrazosPanel.Dock = DockStyle.Top;
            PrazosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            PrazosPanel.Name = "PrazosPanel";

            DivisoriaPrazosImagem = new PictureBox();
            DivisoriaPrazosImagem.Size = new Size(0, 4);
            DivisoriaPrazosImagem.Dock = DockStyle.Bottom;
            DivisoriaPrazosImagem.BackColor = Color.Silver;

            PrazosLabel = new Label();
            PrazosLabel.AutoSize = false;
            PrazosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            PrazosLabel.Dock = DockStyle.Fill;
            PrazosLabel.Text = "Prazos";
            PrazosLabel.Font = CorFontepadraoLabel.Font;
            PrazosLabel.ForeColor = Color.WhiteSmoke;
            PrazosLabel.TextAlign = ContentAlignment.MiddleRight;
            PrazosLabel.MouseHover += new EventHandler(this.NineBoxLabel_MouseHover);
            PrazosLabel.Click += new System.EventHandler(PrazosPanel_Click);
            PrazosLabel.Name = "PrazosLabel";

            PrazosPanel.Controls.Add(PrazosLabel);
            PrazosPanel.Controls.Add(DivisoriaPrazosImagem);

            SubMenuCadastrosRH.Controls.Add(PrazosPanel);
            #endregion

            #region Colaboradores
            ColaboradoresPanel = new Panel();
            ColaboradoresPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ColaboradoresPanel.Dock = DockStyle.Top;
            ColaboradoresPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ColaboradoresPanel.Name = "ColaboradoresPanel";

            DivisoriaColaboradoresImagem = new PictureBox();
            DivisoriaColaboradoresImagem.Size = new Size(0, 4);
            DivisoriaColaboradoresImagem.Dock = DockStyle.Bottom;
            DivisoriaColaboradoresImagem.BackColor = Color.Silver;

            ColaboradoresLabel = new Label();
            ColaboradoresLabel.AutoSize = false;
            ColaboradoresLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ColaboradoresLabel.Dock = DockStyle.Fill;
            ColaboradoresLabel.Text = "Colaboradores";
            ColaboradoresLabel.Font = CorFontepadraoLabel.Font;
            ColaboradoresLabel.ForeColor = Color.WhiteSmoke;
            ColaboradoresLabel.TextAlign = ContentAlignment.MiddleRight;
            ColaboradoresLabel.MouseHover += new EventHandler(this.NineBoxLabel_MouseHover);
            ColaboradoresLabel.Click += new System.EventHandler(ColaboradoresPanel_Click);
            ColaboradoresLabel.Name = "ColaboradoresLabel";

            ColaboradoresPanel.Controls.Add(ColaboradoresLabel);
            ColaboradoresPanel.Controls.Add(DivisoriaColaboradoresImagem);

            SubMenuCadastrosRH.Controls.Add(ColaboradoresPanel);
            #endregion
             
            #region NineBox 
            NineBoxPanel = new Panel(); 
            NineBoxPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            NineBoxPanel.Dock = DockStyle.Top;
            NineBoxPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            NineBoxPanel.Name = "NineBoxPanel";

            DivisoriaNineBoxImagem = new PictureBox();
            DivisoriaNineBoxImagem.Size = new Size(0, 4);
            DivisoriaNineBoxImagem.Dock = DockStyle.Bottom;
            DivisoriaNineBoxImagem.BackColor = Color.Silver;

            Divisoria2NineBoxImagem = new PictureBox();
            Divisoria2NineBoxImagem.Size = new Size(1, 2);
            Divisoria2NineBoxImagem.Dock = DockStyle.Right;
            Divisoria2NineBoxImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            NineBoxSetaLabel = new Label();
            NineBoxSetaLabel.AutoSize = false;
            NineBoxSetaLabel.Size = new Size(20, 59);
            NineBoxSetaLabel.Dock = DockStyle.Right;
            NineBoxSetaLabel.Text = "6";
            NineBoxSetaLabel.Font = new Font("Webdings", (float)12);
            NineBoxSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            NineBoxSetaLabel.Click += new System.EventHandler(this.NineBoxSetaLabel_Click);
            NineBoxSetaLabel.MouseHover += new EventHandler(this.NineBoxSetaLabel_MouseHover);
            NineBoxSetaLabel.Name = "NineBoxSetaLabel";
            NineBoxSetaLabel.ForeColor = Color.WhiteSmoke;

            NineBoxLabel = new Label();
            NineBoxLabel.AutoSize = false;
            NineBoxLabel.Size = new Size(_widthLabelMenuSistema, 0);
            NineBoxLabel.Dock = DockStyle.Fill;
            NineBoxLabel.Text = "9 box";
            NineBoxLabel.Font = CorFontepadraoLabel.Font;
            NineBoxLabel.ForeColor = Color.WhiteSmoke;
            NineBoxLabel.TextAlign = ContentAlignment.MiddleRight;
            NineBoxLabel.MouseHover += new EventHandler(this.NineBoxLabel_MouseHover);
            NineBoxLabel.Name = "NineBoxLabel";

            NineBoxPanel.Controls.Add(NineBoxLabel);
            //NineBoxPanel.Controls.Add(NineBoxImagem);
            NineBoxPanel.Controls.Add(Divisoria2NineBoxImagem);
            NineBoxPanel.Controls.Add(NineBoxSetaLabel);
            NineBoxPanel.Controls.Add(DivisoriaNineBoxImagem);

            SubMenuCadastrosRH.Controls.Add(NineBoxPanel);
            #endregion
        }

        private void MudaSelecaoDeCoresSubCadastrosRH()
        {
            ColaboradoresLabel.Font = new Font(ColaboradoresLabel.Font, ColaboradoresLabel.Font.Style & ~FontStyle.Bold);
            PrazosLabel.Font = new Font(PrazosLabel.Font, PrazosLabel.Font.Style & ~FontStyle.Bold);
            CargosLabel.Font = new Font(CargosLabel.Font, CargosLabel.Font.Style & ~FontStyle.Bold);
            EscolaridadeLabel.Font = new Font(EscolaridadeLabel.Font, EscolaridadeLabel.Font.Style & ~FontStyle.Bold);
            CompetenciasLabel.Font = new Font(CompetenciasLabel.Font, CompetenciasLabel.Font.Style & ~FontStyle.Bold);
            DepartamentoLabel.Font = new Font(DepartamentoLabel.Font, DepartamentoLabel.Font.Style & ~FontStyle.Bold);
            PontuacaoLabel.Font = new Font(PontuacaoLabel.Font, PontuacaoLabel.Font.Style & ~FontStyle.Bold);
            NineBoxLabel.Font = new Font(NineBoxLabel.Font, NineBoxLabel.Font.Style & ~FontStyle.Bold);
            NineBoxSetaLabel.Font = new Font(NineBoxSetaLabel.Font, NineBoxSetaLabel.Font.Style & ~FontStyle.Bold);

            ColaboradoresLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            PrazosLabel.BackColor = ColaboradoresLabel.BackColor;
            CargosLabel.BackColor = ColaboradoresLabel.BackColor;
            EscolaridadeLabel.BackColor = ColaboradoresLabel.BackColor;
            CompetenciasLabel.BackColor = ColaboradoresLabel.BackColor;
            DepartamentoLabel.BackColor = ColaboradoresLabel.BackColor;
            PontuacaoLabel.BackColor = ColaboradoresLabel.BackColor;
            NineBoxLabel.BackColor = ColaboradoresLabel.BackColor;
            NineBoxSetaLabel.BackColor = ColaboradoresLabel.BackColor;

            ColaboradoresLabel.ForeColor = Color.WhiteSmoke;
            PrazosLabel.ForeColor = Color.WhiteSmoke;
            CargosLabel.ForeColor = Color.WhiteSmoke;
            EscolaridadeLabel.ForeColor = Color.WhiteSmoke;
            CompetenciasLabel.ForeColor = Color.WhiteSmoke;
            DepartamentoLabel.ForeColor = Color.WhiteSmoke;
            PontuacaoLabel.ForeColor = Color.WhiteSmoke;
            NineBoxLabel.ForeColor = Color.WhiteSmoke;
            NineBoxSetaLabel.ForeColor = Color.WhiteSmoke;

            NineBoxSetaLabel.Text = "6";
        }

        private void NineBoxSetaLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubCadastrosRH();
            FechaMenuUsuario();
            FechaSubMenuNineBox();  

            if (((Control)sender).Name.Contains("NineBox"))
            {
                NineBoxSetaLabel.Font = new Font(NineBoxSetaLabel.Font, FontStyle.Bold);
                NineBoxSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                NineBoxSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void NineBoxLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubCadastrosRH();
            FechaMenuUsuario();
            FechaSubMenuNineBox();   

            if (((Control)sender).Name.Contains("Colaboradores"))
            {
                ColaboradoresLabel.Font = new Font(ColaboradoresLabel.Font, FontStyle.Bold);
                ColaboradoresLabel.ForeColor = Publicas._fonteBotaoFocado;
                ColaboradoresLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Prazos"))
            {
                PrazosLabel.Font = new Font(PrazosLabel.Font, FontStyle.Bold);
                PrazosLabel.ForeColor = Publicas._fonteBotaoFocado;
                PrazosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Cargos"))
            {
                CargosLabel.Font = new Font(CargosLabel.Font, FontStyle.Bold);
                CargosLabel.ForeColor = Publicas._fonteBotaoFocado;
                CargosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Escolaridade"))
            {
                EscolaridadeLabel.Font = new Font(EscolaridadeLabel.Font, FontStyle.Bold);
                EscolaridadeLabel.ForeColor = Publicas._fonteBotaoFocado;
                EscolaridadeLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Competencias"))
            {
                CompetenciasLabel.Font = new Font(CompetenciasLabel.Font, FontStyle.Bold);
                CompetenciasLabel.ForeColor = Publicas._fonteBotaoFocado;
                CompetenciasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Departamento"))
            {
                DepartamentoLabel.Font = new Font(DepartamentoLabel.Font, FontStyle.Bold);
                DepartamentoLabel.ForeColor = Publicas._fonteBotaoFocado;
                DepartamentoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Pontuacao"))
            {
                PontuacaoLabel.Font = new Font(PontuacaoLabel.Font, FontStyle.Bold);
                PontuacaoLabel.ForeColor = Publicas._fonteBotaoFocado;
                PontuacaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("NineBox"))
            {
                NineBoxLabel.Font = new Font(NineBoxLabel.Font, FontStyle.Bold);
                NineBoxLabel.ForeColor = Publicas._fonteBotaoFocado;
                NineBoxLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void FechaSubMenuCadastrosRH()
        {
            FechaSubMenuNineBox();
            if (SubMenuCadastrosRH != null)
            {
                SubMenuCadastrosRH.Visible = false;
                this.Controls.Remove(SubMenuCadastrosRH);
                SubMenuCadastrosRH.Dispose();
                SubMenuCadastrosRH = null;
            }
        }

        private void ColaboradoresPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Pessoas().ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void PrazosPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Prazos().ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void CargosPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Cargos().ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void EscolaridadePanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Escolaridade().ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void CompetenciasPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Competencia().ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void DepartamentoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Departamentos().ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void PontuacaoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Pontuacao().ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        #region SubMenu NineBox
        private void NineBoxSetaLabel_Click(object sender, EventArgs e)
        {
            NineBoxSetaLabel.Text = "3";
            NineBoxSetaLabel.ForeColor = Publicas._bordaEntrada;
            NineBoxLabel.ForeColor = Publicas._bordaEntrada;

            FechaSubMenuADColaborador();
            FechaSubMenuADPlanejamento();
            FechaSubMenuADGestor();

            if (SubMenuNineBox != null)
            {
                FechaSubMenuNineBox();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuNineBox = new Panel();
            SubMenuNineBox.Size = new Size(145, _heidthMenuSistema * 2);
            SubMenuNineBox.Location = new Point(MenuSistemaPanel.Width + SubMenuRecursosHumanos.Width +
                                SubMenuAvaliacaoDesempenho.Width + SubMenuRecursosHumanos.Width +
                                SubMenuCadastrosRH .Width + 10, AcessoAoMenuPanel.Height);
            SubMenuNineBox.BackColor = Color.Silver;
            this.Controls.Add(SubMenuNineBox);
            SubMenuNineBox.BringToFront();
            SubMenuNineBox.Visible = true;
            #endregion

            #region ColaboradoresNineBox
            ColaboradoresNineBoxPanel = new Panel();
            ColaboradoresNineBoxPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ColaboradoresNineBoxPanel.Dock = DockStyle.Top;
            ColaboradoresNineBoxPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ColaboradoresNineBoxPanel.Name = "ColaboradoresNineBoxPanel";

            DivisoriaColaboradoresNineBoxImagem = new PictureBox();
            DivisoriaColaboradoresNineBoxImagem.Size = new Size(0, 4);
            DivisoriaColaboradoresNineBoxImagem.Dock = DockStyle.Bottom;
            DivisoriaColaboradoresNineBoxImagem.BackColor = Color.Silver;

            ColaboradoresNineBoxLabel = new Label();
            ColaboradoresNineBoxLabel.AutoSize = false;
            ColaboradoresNineBoxLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ColaboradoresNineBoxLabel.Dock = DockStyle.Fill;
            ColaboradoresNineBoxLabel.Text = "Colaboradores";
            ColaboradoresNineBoxLabel.Font = CorFontepadraoLabel.Font;
            ColaboradoresNineBoxLabel.ForeColor = Color.WhiteSmoke;
            ColaboradoresNineBoxLabel.TextAlign = ContentAlignment.MiddleRight;
            ColaboradoresNineBoxLabel.MouseHover += new EventHandler(this.CargosNineBoxLabel_MouseHover);
            ColaboradoresNineBoxLabel.Click += new System.EventHandler(ColaboradoresNineBoxPanel_Click);
            ColaboradoresNineBoxLabel.Name = "ColaboradoresNineBoxLabel";

            ColaboradoresNineBoxPanel.Controls.Add(ColaboradoresNineBoxLabel);
            ColaboradoresNineBoxPanel.Controls.Add(DivisoriaColaboradoresNineBoxImagem);

            SubMenuNineBox.Controls.Add(ColaboradoresNineBoxPanel);
            #endregion

            #region CargosNineBox
            CargosNineBoxPanel = new Panel();
            CargosNineBoxPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CargosNineBoxPanel.Dock = DockStyle.Top;
            CargosNineBoxPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CargosNineBoxPanel.Name = "CargosNineBoxPanel";

            DivisoriaCargosNineBoxImagem = new PictureBox();
            DivisoriaCargosNineBoxImagem.Size = new Size(0, 4);
            DivisoriaCargosNineBoxImagem.Dock = DockStyle.Bottom;
            DivisoriaCargosNineBoxImagem.BackColor = Color.Silver;

            CargosNineBoxLabel = new Label();
            CargosNineBoxLabel.AutoSize = false;
            CargosNineBoxLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CargosNineBoxLabel.Dock = DockStyle.Fill;
            CargosNineBoxLabel.Text = "Cargos";
            CargosNineBoxLabel.Font = CorFontepadraoLabel.Font;
            CargosNineBoxLabel.ForeColor = Color.WhiteSmoke;
            CargosNineBoxLabel.TextAlign = ContentAlignment.MiddleRight;
            CargosNineBoxLabel.MouseHover += new EventHandler(this.CargosNineBoxLabel_MouseHover);
            CargosNineBoxLabel.Click += new System.EventHandler(CargosNineBoxPanel_Click);
            CargosNineBoxLabel.Name = "CargosNineBoxLabel";

            CargosNineBoxPanel.Controls.Add(CargosNineBoxLabel);
            CargosNineBoxPanel.Controls.Add(DivisoriaCargosNineBoxImagem);

            SubMenuNineBox.Controls.Add(CargosNineBoxPanel);
            #endregion
        }

        private void FechaSubMenuNineBox()
        {
            if (SubMenuNineBox != null)
            {
                SubMenuNineBox.Visible = false;
                this.Controls.Remove(SubMenuNineBox);
                SubMenuNineBox.Dispose();
                SubMenuNineBox = null;
            }
        }

        private void MudaSelecaoDeCoresSubNineBox()
        {
            ColaboradoresNineBoxLabel.Font = new Font(ColaboradoresNineBoxLabel.Font, ColaboradoresNineBoxLabel.Font.Style & ~FontStyle.Bold);
            CargosNineBoxLabel.Font = new Font(CargosNineBoxLabel.Font, CargosNineBoxLabel.Font.Style & ~FontStyle.Bold);

            ColaboradoresNineBoxLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CargosNineBoxLabel.BackColor = ColaboradoresLabel.BackColor;

            ColaboradoresNineBoxLabel.ForeColor = Color.WhiteSmoke;
            CargosNineBoxLabel.ForeColor = Color.WhiteSmoke;            
        }

        private void CargosNineBoxLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubNineBox();
            FechaMenuUsuario();

            if (((Control)sender).Name.Contains("Cargos"))
            {
                CargosNineBoxLabel.Font = new Font(CargosNineBoxLabel.Font, FontStyle.Bold);
                CargosNineBoxLabel.ForeColor = Publicas._fonteBotaoFocado;
                CargosNineBoxLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Colaboradores"))
            {
                ColaboradoresNineBoxLabel.Font = new Font(ColaboradoresNineBoxLabel.Font, FontStyle.Bold);
                ColaboradoresNineBoxLabel.ForeColor = Publicas._fonteBotaoFocado;
                ColaboradoresNineBoxLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void CargosNineBoxPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.EixoYNineBox _tela = new Avaliacao_de_desempenho.EixoYNineBox();

            _tela.cargosPanel.Visible = true;
            _tela.colaboradorPanel.Visible = false;
            _tela.ShowDialog();
            NomePadrao();
        }

        private void ColaboradoresNineBoxPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.EixoYNineBox _tela = new Avaliacao_de_desempenho.EixoYNineBox();

            _tela.cargosPanel.Visible = false;
            _tela.colaboradorPanel.Visible = true;
            _tela.ShowDialog();
            NomePadrao();
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region SubMenu Biblioteca
        private void BibliotecaSetaLabel_Click(object sender, EventArgs e)
        {
            BibliotecaSetaLabel.Text = "3";
            BibliotecaSetaLabel.ForeColor = Publicas._bordaEntrada;
            BibliotecaLabel.ForeColor = Publicas._bordaEntrada;            

            FechaSubMenuAvaliacaoDesempenho();

            if (SubMenuBiblioteca != null)
            {
                FechaSubMenuBiblioteca();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuBiblioteca = new Panel();
            SubMenuBiblioteca.Size = new Size(145, _heidthMenuSistema * 8);
            SubMenuBiblioteca.Location = new Point(MenuSistemaPanel.Width + SubMenuRecursosHumanos.Width + 4, AcessoAoMenuPanel.Height);
            SubMenuBiblioteca.BackColor = Color.Silver;
            this.Controls.Add(SubMenuBiblioteca);
            SubMenuBiblioteca.BringToFront();
            SubMenuBiblioteca.Visible = true;
            #endregion

            // Os ultimos SubMenus deve ser incluidos primeiros
            #region Acompanhamento
            AcompanhamentoPanel = new Panel();
            AcompanhamentoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AcompanhamentoPanel.Dock = DockStyle.Top;
            AcompanhamentoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AcompanhamentoPanel.Enabled = Publicas._usuario.AdministraBiblioteca;

            AcompanhamentoPanel.Name = "AcompanhamentoPanel"; 

            DivisoriaAcompanhamentoImagem = new PictureBox();
            DivisoriaAcompanhamentoImagem.Size = new Size(0, 4);
            DivisoriaAcompanhamentoImagem.Dock = DockStyle.Bottom;
            DivisoriaAcompanhamentoImagem.BackColor = Color.Silver;

            AcompanhamentoLabel = new Label();
            AcompanhamentoLabel.AutoSize = false;
            AcompanhamentoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AcompanhamentoLabel.Dock = DockStyle.Fill;
            AcompanhamentoLabel.Text = "Acompanhamento";
            AcompanhamentoLabel.Font = CorFontepadraoLabel.Font;
            AcompanhamentoLabel.ForeColor = Color.WhiteSmoke;
            AcompanhamentoLabel.TextAlign = ContentAlignment.MiddleRight;
            AcompanhamentoLabel.Click += new System.EventHandler(this.AcompanhamentoPanel_Click);
            AcompanhamentoLabel.MouseHover += new EventHandler(this.CadastroBibliotecaLabel_MouseHover);
            AcompanhamentoLabel.Name = "AcompanhamentoLabel";

            AcompanhamentoPanel.Controls.Add(AcompanhamentoLabel);
            AcompanhamentoPanel.Controls.Add(DivisoriaAcompanhamentoImagem);

            SubMenuBiblioteca.Controls.Add(AcompanhamentoPanel);
            #endregion

            #region PontuacaoLivros
            PontuacaoLivrosPanel = new Panel();
            PontuacaoLivrosPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            PontuacaoLivrosPanel.Dock = DockStyle.Top;
            PontuacaoLivrosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            PontuacaoLivrosPanel.Name = "PontuacaoLivrosPanel";

            DivisoriaPontuacaoLivrosImagem = new PictureBox();
            DivisoriaPontuacaoLivrosImagem.Size = new Size(0, 4);
            DivisoriaPontuacaoLivrosImagem.Dock = DockStyle.Bottom;
            DivisoriaPontuacaoLivrosImagem.BackColor = Color.Silver;

            PontuacaoLivrosLabel = new Label();
            PontuacaoLivrosLabel.AutoSize = false;
            PontuacaoLivrosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            PontuacaoLivrosLabel.Dock = DockStyle.Fill;
            PontuacaoLivrosLabel.Text = "Pontuação";
            PontuacaoLivrosLabel.Font = CorFontepadraoLabel.Font;
            PontuacaoLivrosLabel.ForeColor = Color.WhiteSmoke;
            PontuacaoLivrosLabel.TextAlign = ContentAlignment.MiddleRight;
            PontuacaoLivrosLabel.Click += new System.EventHandler(this.PontuacaoLivrosPanel_Click);
            PontuacaoLivrosLabel.MouseHover += new EventHandler(this.CadastroBibliotecaLabel_MouseHover);
            PontuacaoLivrosLabel.Name = "PontuacaoLivrosLabel";

            PontuacaoLivrosPanel.Controls.Add(PontuacaoLivrosLabel);
            PontuacaoLivrosPanel.Controls.Add(DivisoriaPontuacaoLivrosImagem);

            SubMenuBiblioteca.Controls.Add(PontuacaoLivrosPanel);
            #endregion

            #region Respostas
            RespostasPanel = new Panel();
            RespostasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            RespostasPanel.Dock = DockStyle.Top;
            RespostasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            RespostasPanel.Name = "RespostasPanel";
            //RespostasPanel.Enabled = false;

            DivisoriaRespostasImagem = new PictureBox();
            DivisoriaRespostasImagem.Size = new Size(0, 4);
            DivisoriaRespostasImagem.Dock = DockStyle.Bottom;
            DivisoriaRespostasImagem.BackColor = Color.Silver;

            RespostasLabel = new Label();
            RespostasLabel.AutoSize = false;
            RespostasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            RespostasLabel.Dock = DockStyle.Fill;
            RespostasLabel.Text = "Respostas";
            RespostasLabel.Font = CorFontepadraoLabel.Font;
            RespostasLabel.ForeColor = Color.WhiteSmoke;
            RespostasLabel.TextAlign = ContentAlignment.MiddleRight;
            RespostasLabel.Click += new System.EventHandler(this.RespostasPanel_Click);
            RespostasLabel.MouseHover += new EventHandler(this.CadastroBibliotecaLabel_MouseHover);
            RespostasLabel.Name = "RespostasLabel";

            RespostasPanel.Controls.Add(RespostasLabel);
            //RespostasPanel.Controls.Add(RespostasImagem);
            RespostasPanel.Controls.Add(DivisoriaRespostasImagem);

            SubMenuBiblioteca.Controls.Add(RespostasPanel);
            #endregion

            #region Perguntas
            PerguntasPanel = new Panel();
            PerguntasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            PerguntasPanel.Dock = DockStyle.Top;
            PerguntasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            PerguntasPanel.Name = "PerguntasPanel";
            //PerguntasPanel.Enabled = false;

            DivisoriaPerguntasImagem = new PictureBox();
            DivisoriaPerguntasImagem.Size = new Size(0, 4);
            DivisoriaPerguntasImagem.Dock = DockStyle.Bottom;
            DivisoriaPerguntasImagem.BackColor = Color.Silver;

            PerguntasLabel = new Label();
            PerguntasLabel.AutoSize = false;
            PerguntasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            PerguntasLabel.Dock = DockStyle.Fill;
            PerguntasLabel.Text = "Resenha e Perguntas";
            PerguntasLabel.Font = CorFontepadraoLabel.Font;
            PerguntasLabel.ForeColor = Color.WhiteSmoke;
            PerguntasLabel.TextAlign = ContentAlignment.MiddleRight;
            PerguntasLabel.Click += new System.EventHandler(this.PerguntasPanel_Click);
            PerguntasLabel.MouseHover += new EventHandler(this.CadastroBibliotecaLabel_MouseHover);
            PerguntasLabel.Name = "PerguntasLabel";

            PerguntasPanel.Controls.Add(PerguntasLabel);
            //PerguntasPanel.Controls.Add(PerguntasImagem);
            PerguntasPanel.Controls.Add(DivisoriaPerguntasImagem);

            SubMenuBiblioteca.Controls.Add(PerguntasPanel);
            #endregion

            #region Devolucao
            DevolucaoPanel = new Panel();
            DevolucaoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            DevolucaoPanel.Dock = DockStyle.Top;
            DevolucaoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            DevolucaoPanel.Name = "DevolucaoPanel";
            DevolucaoPanel.Enabled = Publicas._usuario.AdministraBiblioteca;

            DivisoriaDevolucaoImagem = new PictureBox();
            DivisoriaDevolucaoImagem.Size = new Size(0, 4);
            DivisoriaDevolucaoImagem.Dock = DockStyle.Bottom;
            DivisoriaDevolucaoImagem.BackColor = Color.Silver;

            DevolucaoLabel = new Label();
            DevolucaoLabel.AutoSize = false;
            DevolucaoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            DevolucaoLabel.Dock = DockStyle.Fill;
            DevolucaoLabel.Text = "Devolução";
            DevolucaoLabel.Font = CorFontepadraoLabel.Font;
            DevolucaoLabel.ForeColor = Color.WhiteSmoke;
            DevolucaoLabel.TextAlign = ContentAlignment.MiddleRight;
            DevolucaoLabel.Click += new System.EventHandler(this.DevolucaoPanel_Click);
            DevolucaoLabel.MouseHover += new EventHandler(this.CadastroBibliotecaLabel_MouseHover);
            DevolucaoLabel.Name = "DevolucaoLabel";

            DevolucaoPanel.Controls.Add(DevolucaoLabel);
            //DevolucaoPanel.Controls.Add(DevolucaoImagem);
            DevolucaoPanel.Controls.Add(DivisoriaDevolucaoImagem);

            SubMenuBiblioteca.Controls.Add(DevolucaoPanel);
            #endregion

            #region Reserva
            ReservaPanel = new Panel();
            ReservaPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ReservaPanel.Dock = DockStyle.Top;
            ReservaPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ReservaPanel.Name = "ReservaPanel";

            DivisoriaReservaImagem = new PictureBox();
            DivisoriaReservaImagem.Size = new Size(0, 4);
            DivisoriaReservaImagem.Dock = DockStyle.Bottom;
            DivisoriaReservaImagem.BackColor = Color.Silver;

            ReservaLabel = new Label();
            ReservaLabel.AutoSize = false;
            ReservaLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ReservaLabel.Dock = DockStyle.Fill;
            ReservaLabel.Text = "Reserva";
            ReservaLabel.Font = CorFontepadraoLabel.Font;
            ReservaLabel.ForeColor = Color.WhiteSmoke;
            ReservaLabel.TextAlign = ContentAlignment.MiddleRight;
            ReservaLabel.Click += new System.EventHandler(this.ReservaPanel_Click);
            ReservaLabel.MouseHover += new EventHandler(this.CadastroBibliotecaLabel_MouseHover);
            ReservaLabel.Name = "ReservaLabel";

            ReservaPanel.Controls.Add(ReservaLabel);
            //ReservaPanel.Controls.Add(ReservaImagem);
            ReservaPanel.Controls.Add(DivisoriaReservaImagem);

            SubMenuBiblioteca.Controls.Add(ReservaPanel);
            #endregion

            #region Emprestimo
            EmprestimoPanel = new Panel();
            EmprestimoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            EmprestimoPanel.Dock = DockStyle.Top;
            EmprestimoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            EmprestimoPanel.Name = "EmprestimoPanel";
            EmprestimoPanel.Enabled = Publicas._usuario.AdministraBiblioteca;

            DivisoriaEmprestimoImagem = new PictureBox();
            DivisoriaEmprestimoImagem.Size = new Size(0, 4);
            DivisoriaEmprestimoImagem.Dock = DockStyle.Bottom;
            DivisoriaEmprestimoImagem.BackColor = Color.Silver;

            EmprestimoLabel = new Label();
            EmprestimoLabel.AutoSize = false;
            EmprestimoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            EmprestimoLabel.Dock = DockStyle.Fill;
            EmprestimoLabel.Text = "Emprestimo";
            EmprestimoLabel.Font = CorFontepadraoLabel.Font;
            EmprestimoLabel.ForeColor = Color.WhiteSmoke;
            EmprestimoLabel.TextAlign = ContentAlignment.MiddleRight;
            EmprestimoLabel.Click += new System.EventHandler(this.EmprestimoPanel_Click);
            EmprestimoLabel.MouseHover += new EventHandler(this.CadastroBibliotecaLabel_MouseHover);
            EmprestimoLabel.Name = "EmprestimoLabel";

            EmprestimoPanel.Controls.Add(EmprestimoLabel);
            EmprestimoPanel.Controls.Add(DivisoriaEmprestimoImagem);

            SubMenuBiblioteca.Controls.Add(EmprestimoPanel);
            #endregion

            #region CadastroBiblioteca
            CadastroBibliotecaPanel = new Panel();
            CadastroBibliotecaPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CadastroBibliotecaPanel.Dock = DockStyle.Top;
            CadastroBibliotecaPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CadastroBibliotecaPanel.Name = "CadastroBibliotecaPanel";
            CadastroBibliotecaPanel.Enabled = Publicas._usuario.AdministraBiblioteca;

            DivisoriaCadastroBibliotecaImagem = new PictureBox();
            DivisoriaCadastroBibliotecaImagem.Size = new Size(0, 4);
            DivisoriaCadastroBibliotecaImagem.Dock = DockStyle.Bottom;
            DivisoriaCadastroBibliotecaImagem.BackColor = Color.Silver;

            Divisoria2CadastroBibliotecaImagem = new PictureBox();
            Divisoria2CadastroBibliotecaImagem.Size = new Size(1, 2);
            Divisoria2CadastroBibliotecaImagem.Dock = DockStyle.Right;
            Divisoria2CadastroBibliotecaImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            CadastroBibliotecaSetaLabel = new Label();
            CadastroBibliotecaSetaLabel.AutoSize = false;
            CadastroBibliotecaSetaLabel.Size = new Size(20, 59);
            CadastroBibliotecaSetaLabel.Dock = DockStyle.Right;
            CadastroBibliotecaSetaLabel.Text = "6";
            CadastroBibliotecaSetaLabel.Font = new Font("Webdings", (float)12);
            CadastroBibliotecaSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            CadastroBibliotecaSetaLabel.Click += new System.EventHandler(this.CadastroBibliotecaSeta_Click);
            CadastroBibliotecaSetaLabel.MouseHover += new EventHandler(this.CadastroBibliotecaSetaLabel_MouseHover);
            CadastroBibliotecaSetaLabel.Name = "CadastroBibliotecaSetaLabel";
            CadastroBibliotecaSetaLabel.ForeColor = Color.WhiteSmoke;

            CadastroBibliotecaLabel = new Label();
            CadastroBibliotecaLabel.AutoSize = false;
            CadastroBibliotecaLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CadastroBibliotecaLabel.Dock = DockStyle.Fill;
            CadastroBibliotecaLabel.Text = "Cadastros";
            CadastroBibliotecaLabel.Font = CorFontepadraoLabel.Font;
            CadastroBibliotecaLabel.ForeColor = Color.WhiteSmoke;
            CadastroBibliotecaLabel.TextAlign = ContentAlignment.MiddleRight;
            CadastroBibliotecaLabel.MouseHover += new EventHandler(this.CadastroBibliotecaLabel_MouseHover);
            CadastroBibliotecaLabel.Name = "CadastroBibliotecaLabel";

            CadastroBibliotecaPanel.Controls.Add(CadastroBibliotecaLabel);
            //CadastroBibliotecaPanel.Controls.Add(CadastroBibliotecaImagem);
            CadastroBibliotecaPanel.Controls.Add(Divisoria2CadastroBibliotecaImagem);
            CadastroBibliotecaPanel.Controls.Add(CadastroBibliotecaSetaLabel);
            CadastroBibliotecaPanel.Controls.Add(DivisoriaCadastroBibliotecaImagem);

            SubMenuBiblioteca.Controls.Add(CadastroBibliotecaPanel);
            #endregion
        }

        private void FechaSubMenuBiblioteca()
        {
            FechaSubMenuCadastroBiblioteca();
            if (SubMenuBiblioteca != null)
            {
                SubMenuBiblioteca.Visible = false;
                this.Controls.Remove(SubMenuBiblioteca);
                SubMenuBiblioteca.Dispose();
                SubMenuBiblioteca = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuBiblioteca()
        {
            CadastroBibliotecaLabel.Font = new Font(CadastroBibliotecaLabel.Font, CadastroBibliotecaLabel.Font.Style & ~FontStyle.Bold);
            EmprestimoLabel.Font = new Font(EmprestimoLabel.Font, EmprestimoLabel.Font.Style & ~FontStyle.Bold);
            ReservaLabel.Font = new Font(ReservaLabel.Font, ReservaLabel.Font.Style & ~FontStyle.Bold);
            DevolucaoLabel.Font = new Font(DevolucaoLabel.Font, DevolucaoLabel.Font.Style & ~FontStyle.Bold);
            PerguntasLabel.Font = new Font(PerguntasLabel.Font, PerguntasLabel.Font.Style & ~FontStyle.Bold);
            AcompanhamentoLabel.Font = new Font(AcompanhamentoLabel.Font, AcompanhamentoLabel.Font.Style & ~FontStyle.Bold);
            RespostasLabel.Font = new Font(RespostasLabel.Font, RespostasLabel.Font.Style & ~FontStyle.Bold);
            PontuacaoLivrosLabel.Font = new Font(PontuacaoLivrosLabel.Font, PontuacaoLivrosLabel.Font.Style & ~FontStyle.Bold);

            CadastroBibliotecaLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            EmprestimoLabel.BackColor = CadastroBibliotecaLabel.BackColor;
            ReservaLabel.BackColor = CadastroBibliotecaLabel.BackColor;
            DevolucaoLabel.BackColor = CadastroBibliotecaLabel.BackColor;
            PerguntasLabel.BackColor = CadastroBibliotecaLabel.BackColor;
            AcompanhamentoLabel.BackColor = CadastroBibliotecaLabel.BackColor;
            RespostasLabel.BackColor = CadastroBibliotecaLabel.BackColor;
            PontuacaoLivrosLabel.BackColor = CadastroBibliotecaLabel.BackColor;

            CadastroBibliotecaSetaLabel.ForeColor = Color.WhiteSmoke;

            CadastroBibliotecaLabel.ForeColor = Color.WhiteSmoke;
            EmprestimoLabel.ForeColor = Color.WhiteSmoke;
            ReservaLabel.ForeColor = Color.WhiteSmoke;
            DevolucaoLabel.ForeColor = Color.WhiteSmoke;
            PerguntasLabel.ForeColor = Color.WhiteSmoke;
            AcompanhamentoLabel.ForeColor = Color.WhiteSmoke;
            RespostasLabel.ForeColor = Color.WhiteSmoke;
            PontuacaoLivrosLabel.ForeColor = Color.WhiteSmoke;

            CadastroBibliotecaSetaLabel.Text = "6";

        }

        private void CadastroBibliotecaLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuBiblioteca();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuCadastroBiblioteca();

            if (((Control)sender).Name.Contains("Acompanhamento"))
            {
                AcompanhamentoLabel.Font = new Font(AcompanhamentoLabel.Font, FontStyle.Bold);
                AcompanhamentoLabel.ForeColor = Publicas._fonteBotaoFocado;
                AcompanhamentoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("CadastroBiblioteca"))
            {
                CadastroBibliotecaLabel.Font = new Font(CadastroBibliotecaLabel.Font, FontStyle.Bold);
                CadastroBibliotecaLabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastroBibliotecaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Emprestimo"))
            {
                EmprestimoLabel.Font = new Font(EmprestimoLabel.Font, FontStyle.Bold);
                EmprestimoLabel.ForeColor = Publicas._fonteBotaoFocado;
                EmprestimoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Reserva"))
            {
                ReservaLabel.Font = new Font(ReservaLabel.Font, FontStyle.Bold);
                ReservaLabel.ForeColor = Publicas._fonteBotaoFocado;
                ReservaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Devolucao"))
            {
                DevolucaoLabel.Font = new Font(DevolucaoLabel.Font, FontStyle.Bold);
                DevolucaoLabel.ForeColor = Publicas._fonteBotaoFocado;
                DevolucaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Perguntas"))
            {
                PerguntasLabel.Font = new Font(PerguntasLabel.Font, FontStyle.Bold);
                PerguntasLabel.ForeColor = Publicas._fonteBotaoFocado;
                PerguntasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Respostas"))
            {
                RespostasLabel.Font = new Font(RespostasLabel.Font, FontStyle.Bold);
                RespostasLabel.ForeColor = Publicas._fonteBotaoFocado;
                RespostasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Pontuacao"))
            {
                PontuacaoLivrosLabel.Font = new Font(PontuacaoLivrosLabel.Font, FontStyle.Bold);
                PontuacaoLivrosLabel.ForeColor = Publicas._fonteBotaoFocado;
                PontuacaoLivrosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void CadastroBibliotecaSetaLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuBiblioteca();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuCadastroBiblioteca();

            if (((Control)sender).Name.Contains("CadastroBiblioteca"))
            {
                CadastroBibliotecaSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastroBibliotecaSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void AcompanhamentoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Acompanhamento _tela = new Biblioteca.Acompanhamento();

            _tela.ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void PontuacaoLivrosPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Pontuacao _tela = new Biblioteca.Pontuacao();
            _tela.ColaboradorTextBox.Text = Publicas._idColaborador.ToString();

            _tela.ShowDialog();
            NomePadrao();
        }

        private void RespostasPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Respostas _tela = new Biblioteca.Respostas();

            _tela.ShowDialog();
            NomePadrao();
        }

        private void PerguntasPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Perguntas _tela = new Biblioteca.Perguntas();

            _tela.ShowDialog();
            NomePadrao();
        }

        private void DevolucaoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Devolucao _tela = new Biblioteca.Devolucao();

            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void ReservaPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Reserva _tela = new Biblioteca.Reserva();

            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void EmprestimoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Emprestimos _tela = new Biblioteca.Emprestimos();

            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        #region SubMenu Cadastro Biblioteca
        private void CadastroBibliotecaSeta_Click(object sender, EventArgs e)
        {
            CadastroBibliotecaSetaLabel.Text = "3";
            CadastroBibliotecaSetaLabel.ForeColor = Publicas._bordaEntrada;
            CadastroBibliotecaLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuCadastroBiblioteca != null)
            {
                FechaSubMenuCadastroBiblioteca();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuCadastroBiblioteca = new Panel();
            SubMenuCadastroBiblioteca.Size = new Size(145, _heidthMenuSistema * 2);
            SubMenuCadastroBiblioteca.Location = new Point(MenuSistemaPanel.Width + SubMenuRecursosHumanos.Width + SubMenuBiblioteca.Width + 6, AcessoAoMenuPanel.Height);
            SubMenuCadastroBiblioteca.BackColor = Color.Silver;
            this.Controls.Add(SubMenuCadastroBiblioteca);
            SubMenuCadastroBiblioteca.BringToFront();
            SubMenuCadastroBiblioteca.Visible = true;
            #endregion

            #region Livros
            LivrosPanel = new Panel();
            LivrosPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            LivrosPanel.Dock = DockStyle.Top;
            LivrosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            LivrosPanel.Name = "LivrosPanel";

            DivisoriaLivrosImagem = new PictureBox();
            DivisoriaLivrosImagem.Size = new Size(0, 4);
            DivisoriaLivrosImagem.Dock = DockStyle.Bottom;
            DivisoriaLivrosImagem.BackColor = Color.Silver;

            LivrosLabel = new Label();
            LivrosLabel.AutoSize = false;
            LivrosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            LivrosLabel.Dock = DockStyle.Fill;
            LivrosLabel.Text = "Livros";
            LivrosLabel.Font = CorFontepadraoLabel.Font;
            LivrosLabel.ForeColor = Color.WhiteSmoke;
            LivrosLabel.TextAlign = ContentAlignment.MiddleRight;
            LivrosLabel.Click += new System.EventHandler(this.LivrosPanel_Click);
            LivrosLabel.MouseHover += new EventHandler(this.CategoriasLabel_MouseHover);
            LivrosLabel.Name = "LivrosLabel";

            LivrosPanel.Controls.Add(LivrosLabel);
            LivrosPanel.Controls.Add(DivisoriaLivrosImagem);

            SubMenuCadastroBiblioteca.Controls.Add(LivrosPanel);
            #endregion

            #region Categorias
            CategoriasPanel = new Panel();
            CategoriasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CategoriasPanel.Dock = DockStyle.Top;
            CategoriasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            CategoriasPanel.Name = "CategoriasPanel";

            DivisoriaCategoriasImagem = new PictureBox();
            DivisoriaCategoriasImagem.Size = new Size(0, 4);
            DivisoriaCategoriasImagem.Dock = DockStyle.Bottom;
            DivisoriaCategoriasImagem.BackColor = Color.Silver;

            CategoriasLabel = new Label();
            CategoriasLabel.AutoSize = false;
            CategoriasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CategoriasLabel.Dock = DockStyle.Fill;
            CategoriasLabel.Text = "Categorias";
            CategoriasLabel.Font = CorFontepadraoLabel.Font;
            CategoriasLabel.ForeColor = Color.WhiteSmoke;
            CategoriasLabel.TextAlign = ContentAlignment.MiddleRight;
            CategoriasLabel.Click += new System.EventHandler(this.CategoriasPanel_Click);
            CategoriasLabel.MouseHover += new EventHandler(this.CategoriasLabel_MouseHover);
            CategoriasLabel.Name = "CategoriasLabel";

            CategoriasPanel.Controls.Add(CategoriasLabel);
            CategoriasPanel.Controls.Add(DivisoriaCategoriasImagem);

            SubMenuCadastroBiblioteca.Controls.Add(CategoriasPanel);
            #endregion
        }

        private void FechaSubMenuCadastroBiblioteca()
        {
            if (SubMenuCadastroBiblioteca != null)
            {
                SubMenuCadastroBiblioteca.Visible = false;
                this.Controls.Remove(SubMenuCadastroBiblioteca);
                SubMenuCadastroBiblioteca.Dispose();
                SubMenuCadastroBiblioteca = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuCadastroBiblioteca()
        {
            CategoriasLabel.Font = new Font(CategoriasLabel.Font, CategoriasLabel.Font.Style & ~FontStyle.Bold);
            LivrosLabel.Font = new Font(LivrosLabel.Font, LivrosLabel.Font.Style & ~FontStyle.Bold);

            CategoriasLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            LivrosLabel.BackColor = CategoriasLabel.BackColor;

            CategoriasLabel.ForeColor = Color.WhiteSmoke;
            LivrosLabel.ForeColor = Color.WhiteSmoke;
        }

        private void CategoriasLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuCadastroBiblioteca();
            FechaSubMenuAvaliacaoDesempenho();

            if (((Control)sender).Name.Contains("Categorias"))
            {
                CategoriasLabel.Font = new Font(CategoriasLabel.Font, FontStyle.Bold);
                CategoriasLabel.ForeColor = Publicas._fonteBotaoFocado;
                CategoriasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Livros"))
            {
                LivrosLabel.Font = new Font(LivrosLabel.Font, FontStyle.Bold);
                LivrosLabel.ForeColor = Publicas._fonteBotaoFocado;
                LivrosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

        }

        private void CategoriasPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Categorias _tela = new Biblioteca.Categorias();

            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void LivrosPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Livros _tela = new Biblioteca.Livros();

            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void PontuacaoBibliotecaPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            //
            //AtivaTimer(sender, e);
        }
        #endregion

        #endregion

        #region SubMenu Corridas
        private void CorridasSetaLabel_Click(object sender, EventArgs e)
        {
            CorridasSetaLabel.Text = "3";
            CorridasSetaLabel.ForeColor = Publicas._bordaEntrada;
            CorridasLabel.ForeColor = Publicas._bordaEntrada;

            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();

            if (SubMenuCorridas != null)
            {
                FechaSubMenuCorridas();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuCorridas = new Panel();
            SubMenuCorridas.Size = new Size(145, _heidthMenuSistema * 3);
            SubMenuCorridas.Location = new Point(MenuSistemaPanel.Width + SubMenuRecursosHumanos.Width + 4, AcessoAoMenuPanel.Height);
            SubMenuCorridas.BackColor = Color.Silver;
            this.Controls.Add(SubMenuCorridas);
            SubMenuCorridas.BringToFront();
            SubMenuCorridas.Visible = true;
            #endregion

            // Os ultimos SubMenus deve ser incluidos primeiros
            #region Resultado
            ResultadoPanel = new Panel();
            ResultadoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ResultadoPanel.Dock = DockStyle.Top;
            ResultadoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            ResultadoPanel.Name = "ResultadoPanel";

            DivisoriaResultadoImagem = new PictureBox();
            DivisoriaResultadoImagem.Size = new Size(0, 4);
            DivisoriaResultadoImagem.Dock = DockStyle.Bottom;
            DivisoriaResultadoImagem.BackColor = Color.Silver;

            ResultadoLabel = new Label();
            ResultadoLabel.AutoSize = false;
            ResultadoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ResultadoLabel.Dock = DockStyle.Fill;
            ResultadoLabel.Text = "Resultados";
            ResultadoLabel.Font = CorFontepadraoLabel.Font;
            ResultadoLabel.ForeColor = Color.WhiteSmoke;
            ResultadoLabel.TextAlign = ContentAlignment.MiddleRight;
            ResultadoLabel.Click += new System.EventHandler(this.ResultadoPanel_Click);
            ResultadoLabel.MouseHover += new EventHandler(this.CadastroCorridasLabel_MouseHover);
            ResultadoLabel.Name = "ResultadoLabel";

            ResultadoPanel.Controls.Add(ResultadoLabel);
            ResultadoPanel.Controls.Add(DivisoriaResultadoImagem);

            SubMenuCorridas.Controls.Add(ResultadoPanel);
            #endregion

            #region Participantes
            ParticipantesPanel = new Panel();
            ParticipantesPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ParticipantesPanel.Dock = DockStyle.Top;
            ParticipantesPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ParticipantesPanel.Name = "ParticipantesPanel";
            ParticipantesPanel.Enabled = Publicas._usuario.AdministraCorridas;

            DivisoriaParticipantesImagem = new PictureBox();
            DivisoriaParticipantesImagem.Size = new Size(0, 4);
            DivisoriaParticipantesImagem.Dock = DockStyle.Bottom;
            DivisoriaParticipantesImagem.BackColor = Color.Silver;

            ParticipantesLabel = new Label();
            ParticipantesLabel.AutoSize = false;
            ParticipantesLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ParticipantesLabel.Dock = DockStyle.Fill;
            ParticipantesLabel.Text = "Participantes";
            ParticipantesLabel.Font = CorFontepadraoLabel.Font;
            ParticipantesLabel.ForeColor = Color.WhiteSmoke;
            ParticipantesLabel.TextAlign = ContentAlignment.MiddleRight;
            ParticipantesLabel.Click += new System.EventHandler(this.ParticipantesPanel_Click);
            ParticipantesLabel.MouseHover += new EventHandler(this.CadastroCorridasLabel_MouseHover);
            ParticipantesLabel.Name = "ParticipantesLabel";

            ParticipantesPanel.Controls.Add(ParticipantesLabel);
            //ParticipantesPanel.Controls.Add(ParticipantesImagem);
            ParticipantesPanel.Controls.Add(DivisoriaParticipantesImagem);

            SubMenuCorridas.Controls.Add(ParticipantesPanel);
            #endregion

            #region Cadastro
            CadastroCorridasPanel = new Panel();
            CadastroCorridasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CadastroCorridasPanel.Dock = DockStyle.Top;
            CadastroCorridasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CadastroCorridasPanel.Name = "CadastroCorridasPanel";
            CadastroCorridasPanel.Enabled = Publicas._usuario.AdministraCorridas;

            DivisoriaCadastroCorridasImagem = new PictureBox();
            DivisoriaCadastroCorridasImagem.Size = new Size(0, 4);
            DivisoriaCadastroCorridasImagem.Dock = DockStyle.Bottom;
            DivisoriaCadastroCorridasImagem.BackColor = Color.Silver;

            CadastroCorridasLabel = new Label();
            CadastroCorridasLabel.AutoSize = false;
            CadastroCorridasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CadastroCorridasLabel.Dock = DockStyle.Fill;
            CadastroCorridasLabel.Text = "Cadastro";
            CadastroCorridasLabel.Font = CorFontepadraoLabel.Font;
            CadastroCorridasLabel.ForeColor = Color.WhiteSmoke;
            CadastroCorridasLabel.TextAlign = ContentAlignment.MiddleRight;
            CadastroCorridasLabel.Click += new System.EventHandler(this.CadastroCorridasPanel_Click);
            CadastroCorridasLabel.MouseHover += new EventHandler(this.CadastroCorridasLabel_MouseHover);
            CadastroCorridasLabel.Name = "CadastroCorridasLabel";

            CadastroCorridasPanel.Controls.Add(CadastroCorridasLabel);
            //CadastroCorridasPanel.Controls.Add(CadastroCorridasImagem);
            CadastroCorridasPanel.Controls.Add(DivisoriaCadastroCorridasImagem);

            SubMenuCorridas.Controls.Add(CadastroCorridasPanel);
            #endregion
        }

        private void FechaSubMenuCorridas()
        {
            if (SubMenuCorridas != null)
            {
                SubMenuCorridas.Visible = false;
                this.Controls.Remove(SubMenuCorridas);
                SubMenuCorridas.Dispose();
                SubMenuCorridas = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuCorridas()
        {
            CadastroCorridasLabel.Font = new Font(CadastroCorridasLabel.Font, CadastroCorridasLabel.Font.Style & ~FontStyle.Bold);
            ParticipantesLabel.Font = new Font(ParticipantesLabel.Font, ParticipantesLabel.Font.Style & ~FontStyle.Bold);
            ResultadoLabel.Font = new Font(ResultadoLabel.Font, ResultadoLabel.Font.Style & ~FontStyle.Bold);

            CadastroCorridasLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ParticipantesLabel.BackColor = CadastroCorridasLabel.BackColor;
            ResultadoLabel.BackColor = CadastroCorridasLabel.BackColor;

            CadastroCorridasLabel.ForeColor = Color.WhiteSmoke;
            ParticipantesLabel.ForeColor = Color.WhiteSmoke;
            ResultadoLabel.ForeColor = Color.WhiteSmoke;
        }

        private void CadastroCorridasLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuCorridas();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();

            if (((Control)sender).Name.Contains("Cadastro"))
            {
                CadastroCorridasLabel.Font = new Font(CadastroCorridasLabel.Font, FontStyle.Bold);
                CadastroCorridasLabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastroCorridasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Participantes"))
            {
                ParticipantesLabel.Font = new Font(ParticipantesLabel.Font, FontStyle.Bold);
                ParticipantesLabel.ForeColor = Publicas._fonteBotaoFocado;
                ParticipantesLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Resultado"))
            {
                ResultadoLabel.Font = new Font(ResultadoLabel.Font, FontStyle.Bold);
                ResultadoLabel.ForeColor = Publicas._fonteBotaoFocado;
                ResultadoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Reserva"))
            {
                ReservaLabel.Font = new Font(ReservaLabel.Font, FontStyle.Bold);
                ReservaLabel.ForeColor = Publicas._fonteBotaoFocado;
                ReservaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Devolucao"))
            {
                DevolucaoLabel.Font = new Font(DevolucaoLabel.Font, FontStyle.Bold);
                DevolucaoLabel.ForeColor = Publicas._fonteBotaoFocado;
                DevolucaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Perguntas"))
            {
                PerguntasLabel.Font = new Font(PerguntasLabel.Font, FontStyle.Bold);
                PerguntasLabel.ForeColor = Publicas._fonteBotaoFocado;
                PerguntasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void CadastroCorridasPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCorridas;
            new Cadastros.Corridas().ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void ParticipantesPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCorridas;
            new Cadastros.Participantes().ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }

        private void ResultadoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCorridas;
            new Cadastros.ResultadoDasCorridas().ShowDialog();
            NomePadrao();

            //AtivaTimer(sender, e);
        }
        #endregion

        #endregion

        #region SubMenu Juridico
        private void JuridicoSetaLabel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu do RecursosHumanos
            JuridicoSetaLabel.Text = "3";
            JuridicoSetaLabel.ForeColor = Publicas._bordaEntrada;
            JuridicoLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuJuridico != null)
            {
                FechaSubMenuJuridico();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuJuridico = new Panel();
            SubMenuJuridico.Size = new Size(145, _heidthMenuSistema * 2);
            SubMenuJuridico.Location = new Point(MenuSistemaPanel.Width + 2, AcessoAoMenuPanel.Height);
            SubMenuJuridico.BackColor = Color.Silver;
            this.Controls.Add(SubMenuJuridico);
            SubMenuJuridico.BringToFront();
            SubMenuJuridico.Visible = true;
            #endregion

            #region AbrirComunicado
            AbrirComunicadoPanel = new Panel();
            AbrirComunicadoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AbrirComunicadoPanel.Dock = DockStyle.Top;
            AbrirComunicadoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AbrirComunicadoPanel.Name = "AbrirComunicadoPanel";

            DivisoriaAbrirComunicadoImagem = new PictureBox();
            DivisoriaAbrirComunicadoImagem.Size = new Size(0, 4);
            DivisoriaAbrirComunicadoImagem.Dock = DockStyle.Bottom;
            DivisoriaAbrirComunicadoImagem.BackColor = Color.Silver;

            AbrirComunicadoLabel = new Label();
            AbrirComunicadoLabel.AutoSize = false;
            AbrirComunicadoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AbrirComunicadoLabel.Dock = DockStyle.Fill;
            AbrirComunicadoLabel.Text = "Abrir Comunicado";
            AbrirComunicadoLabel.Font = CorFontepadraoLabel.Font;
            AbrirComunicadoLabel.ForeColor = Color.WhiteSmoke;
            AbrirComunicadoLabel.TextAlign = ContentAlignment.MiddleRight;
            AbrirComunicadoLabel.MouseHover += new EventHandler(this.CadastroJuridicoSetaPanel_MouseHover);
            AbrirComunicadoLabel.Click += new System.EventHandler(this.AbrirComunicadoPanel_Click);
            AbrirComunicadoLabel.Name = "AbrirComunicadoLabel";

            AbrirComunicadoPanel.Controls.Add(AbrirComunicadoLabel);
            AbrirComunicadoPanel.Controls.Add(DivisoriaAbrirComunicadoImagem);

            SubMenuJuridico.Controls.Add(AbrirComunicadoPanel);
            #endregion

            #region CadastroJuridico 
            CadastroJuridicoPanel = new Panel();
            CadastroJuridicoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CadastroJuridicoPanel.Dock = DockStyle.Top;
            CadastroJuridicoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CadastroJuridicoPanel.Name = "CadastroJuridicoPanel";
            CadastroJuridicoPanel.Enabled = Publicas._usuario.AcessaCadastroJuridico;

            DivisoriaCadastroJuridicoImagem = new PictureBox();
            DivisoriaCadastroJuridicoImagem.Size = new Size(0, 4);
            DivisoriaCadastroJuridicoImagem.Dock = DockStyle.Bottom;
            DivisoriaCadastroJuridicoImagem.BackColor = Color.Silver;

            Divisoria2CadastroJuridicoImagem = new PictureBox();
            Divisoria2CadastroJuridicoImagem.Size = new Size(1, 2);
            Divisoria2CadastroJuridicoImagem.Dock = DockStyle.Right;
            Divisoria2CadastroJuridicoImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128); 

            CadastroJuridicoSetaLabel = new Label();
            CadastroJuridicoSetaLabel.AutoSize = false;
            CadastroJuridicoSetaLabel.Size = new Size(20, 59);
            CadastroJuridicoSetaLabel.Dock = DockStyle.Right;
            CadastroJuridicoSetaLabel.Text = "6";
            CadastroJuridicoSetaLabel.Font = new Font("Webdings", (float)12);
            CadastroJuridicoSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            CadastroJuridicoSetaLabel.Click += new System.EventHandler(this.CadastroJuridicoSetaLabel_Click);
            CadastroJuridicoSetaLabel.MouseHover += new EventHandler(this.CadastroJuridicoSetaPanel_MouseHover);
            CadastroJuridicoSetaLabel.Name = "CadastroJuridicoSetaLabel";
            CadastroJuridicoSetaLabel.ForeColor = Color.WhiteSmoke;

            CadastroJuridicoLabel = new Label();
            CadastroJuridicoLabel.AutoSize = false;
            CadastroJuridicoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CadastroJuridicoLabel.Dock = DockStyle.Fill; 
            CadastroJuridicoLabel.Text = "Cadastros";
            CadastroJuridicoLabel.Font = CorFontepadraoLabel.Font;
            CadastroJuridicoLabel.ForeColor = Color.WhiteSmoke;
            CadastroJuridicoLabel.TextAlign = ContentAlignment.MiddleRight;
            CadastroJuridicoLabel.MouseHover += new EventHandler(this.CadastroJuridicoPanel_MouseHover);
            CadastroJuridicoLabel.Name = "CadastroJuridicoLabel";

            CadastroJuridicoPanel.Controls.Add(CadastroJuridicoLabel);
            //CadastroJuridicoPanel.Controls.Add(CadastroJuridicoImagem);
            CadastroJuridicoPanel.Controls.Add(Divisoria2CadastroJuridicoImagem);
            CadastroJuridicoPanel.Controls.Add(CadastroJuridicoSetaLabel);
            CadastroJuridicoPanel.Controls.Add(DivisoriaCadastroJuridicoImagem);

            SubMenuJuridico.Controls.Add(CadastroJuridicoPanel);
            #endregion
        } 

        private void FechaSubMenuJuridico()
        {
            FechaSubMenuCadastroJuridico();            

            if (SubMenuJuridico != null)
            {
                SubMenuJuridico.Visible = false;
                this.Controls.Remove(SubMenuJuridico);
                SubMenuJuridico.Dispose();
                SubMenuJuridico = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuJuridico()
        {
            CadastroJuridicoLabel.Font = new Font(CadastroJuridicoLabel.Font, CadastroJuridicoLabel.Font.Style & ~FontStyle.Bold);
            AbrirComunicadoLabel.Font = new Font(AbrirComunicadoLabel.Font, AbrirComunicadoLabel.Font.Style & ~FontStyle.Bold);

            CadastroJuridicoLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AbrirComunicadoLabel.BackColor = CadastroJuridicoLabel.BackColor;
            CadastroJuridicoSetaLabel.BackColor = CadastroJuridicoLabel.BackColor;
            
            CadastroJuridicoLabel.ForeColor = Color.WhiteSmoke;
            CadastroJuridicoSetaLabel.ForeColor = Color.WhiteSmoke;
            AbrirComunicadoLabel.ForeColor = Color.WhiteSmoke;

            CadastroJuridicoSetaLabel.Text = "6";            
        }

        private void CadastroJuridicoPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuJuridico();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();

            if (((Control)sender).Name.Contains("Cadastro"))
            {
                CadastroJuridicoLabel.Font = new Font(CadastroJuridicoLabel.Font, FontStyle.Bold);
                CadastroJuridicoLabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastroJuridicoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AbrirComunicado"))
            {
                AbrirComunicadoLabel.Font = new Font(AbrirComunicadoLabel.Font, FontStyle.Bold);
                AbrirComunicadoLabel.ForeColor = Publicas._fonteBotaoFocado;
                AbrirComunicadoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void CadastroJuridicoSetaPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuJuridico();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();

            if (((Control)sender).Name.Contains("Cadastro"))
            {
                CadastroJuridicoSetaLabel.Font = new Font(CadastroJuridicoSetaLabel.Font, FontStyle.Bold);
                CadastroJuridicoSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastroJuridicoSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("AbrirComunicado"))
            {
                AbrirComunicadoLabel.Font = new Font(AbrirComunicadoLabel.Font, FontStyle.Bold);
                AbrirComunicadoLabel.ForeColor = Publicas._fonteBotaoFocado;
                AbrirComunicadoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void AbrirComunicadoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
            Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Novo;
            Juridico.Comunicado _tela = new Juridico.Comunicado();
            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        #region Cadastros
        private void CadastroJuridicoSetaLabel_Click(object sender, EventArgs e)
        {            
            CadastroJuridicoSetaLabel.Text = "3";
            CadastroJuridicoSetaLabel.ForeColor = Publicas._bordaEntrada;
            CadastroJuridicoLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuCadastroJuridico != null)
            {
                FechaSubMenuCadastroJuridico();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuCadastroJuridico = new Panel();
            SubMenuCadastroJuridico.Size = new Size(145, _heidthMenuSistema * 2);
            SubMenuCadastroJuridico.Location = new Point(MenuSistemaPanel.Width + SubMenuJuridico.Width + 4, AcessoAoMenuPanel.Height);
            SubMenuCadastroJuridico.BackColor = Color.Silver;
            this.Controls.Add(SubMenuCadastroJuridico);
            SubMenuCadastroJuridico.BringToFront();
            SubMenuCadastroJuridico.Visible = true;
            #endregion

            #region TiposPagamento
            TiposPagamentoPanel = new Panel();
            TiposPagamentoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            TiposPagamentoPanel.Dock = DockStyle.Top;
            TiposPagamentoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            TiposPagamentoPanel.Name = "TiposPagamentoPanel";

            DivisoriaTiposPagamentoImagem = new PictureBox();
            DivisoriaTiposPagamentoImagem.Size = new Size(0, 4);
            DivisoriaTiposPagamentoImagem.Dock = DockStyle.Bottom;
            DivisoriaTiposPagamentoImagem.BackColor = Color.Silver;

            TiposPagamentoLabel = new Label();
            TiposPagamentoLabel.AutoSize = false;
            TiposPagamentoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            TiposPagamentoLabel.Dock = DockStyle.Fill;
            TiposPagamentoLabel.Text = "Tipos de Pagamento";
            TiposPagamentoLabel.Font = CorFontepadraoLabel.Font;
            TiposPagamentoLabel.ForeColor = Color.WhiteSmoke;
            TiposPagamentoLabel.TextAlign = ContentAlignment.MiddleRight;
            TiposPagamentoLabel.MouseHover += new EventHandler(this.TiposPagamentoPanel_MouseHover);
            TiposPagamentoLabel.Click += new System.EventHandler(this.TiposPagamentoPanel_Click);
            TiposPagamentoLabel.Name = "TiposPagamentoLabel";

            TiposPagamentoPanel.Controls.Add(TiposPagamentoLabel);
            TiposPagamentoPanel.Controls.Add(DivisoriaTiposPagamentoImagem);

            SubMenuCadastroJuridico.Controls.Add(TiposPagamentoPanel);
            #endregion

            #region Vara
            VaraPanel = new Panel();
            VaraPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            VaraPanel.Dock = DockStyle.Top;
            VaraPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            VaraPanel.Name = "VaraPanel";

            DivisoriaVaraImagem = new PictureBox();
            DivisoriaVaraImagem.Size = new Size(0, 4);
            DivisoriaVaraImagem.Dock = DockStyle.Bottom;
            DivisoriaVaraImagem.BackColor = Color.Silver; 

            VaraLabel = new Label();
            VaraLabel.AutoSize = false;
            VaraLabel.Size = new Size(_widthLabelMenuSistema, 0);
            VaraLabel.Dock = DockStyle.Fill;
            VaraLabel.Text = "Vara";
            VaraLabel.Font = CorFontepadraoLabel.Font;
            VaraLabel.ForeColor = Color.WhiteSmoke;
            VaraLabel.TextAlign = ContentAlignment.MiddleRight;
            VaraLabel.MouseHover += new EventHandler(this.TiposPagamentoPanel_MouseHover);
            VaraLabel.Click += new System.EventHandler(this.VaraPanel_Click);
            VaraLabel.Name = "VaraLabel";

            VaraPanel.Controls.Add(VaraLabel);
            VaraPanel.Controls.Add(DivisoriaVaraImagem);

            SubMenuCadastroJuridico.Controls.Add(VaraPanel);
            #endregion
        }

        private void FechaSubMenuCadastroJuridico()
        {
            if (SubMenuCadastroJuridico != null)
            {
                SubMenuCadastroJuridico.Visible = false;
                this.Controls.Remove(SubMenuCadastroJuridico);
                SubMenuCadastroJuridico.Dispose();
                SubMenuCadastroJuridico = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuCadastroJuridico()
        {
            TiposPagamentoLabel.Font = new Font(TiposPagamentoLabel.Font, TiposPagamentoLabel.Font.Style & ~FontStyle.Bold);
            VaraLabel.Font = new Font(VaraLabel.Font, VaraLabel.Font.Style & ~FontStyle.Bold);

            TiposPagamentoLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            VaraLabel.BackColor = TiposPagamentoLabel.BackColor;

            TiposPagamentoLabel.ForeColor = Color.WhiteSmoke;
            VaraLabel.ForeColor = Color.WhiteSmoke;
        }

        private void TiposPagamentoPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuCadastroJuridico();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();

            if (((Control)sender).Name.Contains("TiposPagamento"))
            {
                TiposPagamentoLabel.Font = new Font(TiposPagamentoLabel.Font, FontStyle.Bold);
                TiposPagamentoLabel.ForeColor = Publicas._fonteBotaoFocado;
                TiposPagamentoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Vara"))
            {
                VaraLabel.Font = new Font(VaraLabel.Font, FontStyle.Bold);
                VaraLabel.ForeColor = Publicas._fonteBotaoFocado;
                VaraLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void TiposPagamentoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
            Cadastros.TiposDePagamento _tela = new Cadastros.TiposDePagamento();
            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void VaraPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
            Cadastros.Vara _tela = new Cadastros.Vara();
            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }
        #endregion

        #endregion

        #region SubMenu Contabilidade
        private void ContabilidadeSetaLabel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu 
            ContabilidadeSetaLabel.Text = "3";
            ContabilidadeSetaLabel.ForeColor = Publicas._bordaEntrada;
            ContabilidadeLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuContabilidade != null)
            {
                FechaSubMenuContabilidade();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuContabilidade = new Panel();
            SubMenuContabilidade.Size = new Size(145, _heidthMenuSistema * 2);
            SubMenuContabilidade.Location = new Point(MenuSistemaPanel.Width + 2, AcessoAoMenuPanel.Height);
            SubMenuContabilidade.BackColor = Color.Silver;
            this.Controls.Add(SubMenuContabilidade);
            SubMenuContabilidade.BringToFront();
            SubMenuContabilidade.Visible = true;
            #endregion

            #region IntegraNFS
            IntegraNFSPanel = new Panel();
            IntegraNFSPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            IntegraNFSPanel.Dock = DockStyle.Top;
            IntegraNFSPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            IntegraNFSPanel.Name = "IntegraNFSPanel";

            DivisoriaIntegraNFSImagem = new PictureBox();
            DivisoriaIntegraNFSImagem.Size = new Size(0, 4);
            DivisoriaIntegraNFSImagem.Dock = DockStyle.Bottom;
            DivisoriaIntegraNFSImagem.BackColor = Color.Silver;

            IntegraNFSLabel = new Label();
            IntegraNFSLabel.AutoSize = false;
            IntegraNFSLabel.Size = new Size(_widthLabelMenuSistema, 0);
            IntegraNFSLabel.Dock = DockStyle.Fill;
            IntegraNFSLabel.Text = "Integra NFS Globus" + Environment.NewLine + "EST x ESF ISS";
            IntegraNFSLabel.Font = CorFontepadraoLabel.Font;
            IntegraNFSLabel.ForeColor = Color.WhiteSmoke;
            IntegraNFSLabel.TextAlign = ContentAlignment.MiddleRight;
            IntegraNFSLabel.MouseHover += new EventHandler(this.ContabilidadePanel_MouseHover);
            IntegraNFSLabel.Click += new System.EventHandler(this.IntegraNFSLabel_Click);
            IntegraNFSLabel.Name = "IntegraNFSLabel";

            IntegraNFSPanel.Controls.Add(IntegraNFSLabel);
            IntegraNFSPanel.Controls.Add(DivisoriaIntegraNFSImagem);

            SubMenuContabilidade.Controls.Add(IntegraNFSPanel);
            #endregion

            #region Arquivei
            ArquiveiPanel = new Panel();
            ArquiveiPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ArquiveiPanel.Dock = DockStyle.Top;
            ArquiveiPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ArquiveiPanel.Name = "ArquiveiPanel";
            //ArquiveiPanel.Enabled = false;

            DivisoriaArquiveiImagem = new PictureBox();
            DivisoriaArquiveiImagem.Size = new Size(0, 4);
            DivisoriaArquiveiImagem.Dock = DockStyle.Bottom;
            DivisoriaArquiveiImagem.BackColor = Color.Silver;

            Divisoria2ArquiveiImagem = new PictureBox();
            Divisoria2ArquiveiImagem.Size = new Size(1, 2);
            Divisoria2ArquiveiImagem.Dock = DockStyle.Right;
            Divisoria2ArquiveiImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            ArquiveiSetaLabel = new Label();
            ArquiveiSetaLabel.AutoSize = false;
            ArquiveiSetaLabel.Size = new Size(20, 59);
            ArquiveiSetaLabel.Dock = DockStyle.Right;
            ArquiveiSetaLabel.Text = "6";
            ArquiveiSetaLabel.Font = new Font("Webdings", (float)12);
            ArquiveiSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            ArquiveiSetaLabel.MouseHover += new EventHandler(this.ArquiveiSetaLabel_MouseHover);
            ArquiveiSetaLabel.Click += new System.EventHandler(this.ArquiveiSetaPanel_Click);
            ArquiveiSetaLabel.Name = "ArquiveiSetaLabel";
            ArquiveiSetaLabel.ForeColor = Color.WhiteSmoke;

            ArquiveiLabel = new Label();
            ArquiveiLabel.AutoSize = false;
            ArquiveiLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ArquiveiLabel.Dock = DockStyle.Fill;
            ArquiveiLabel.Text = "Arquivei";
            ArquiveiLabel.Font = CorFontepadraoLabel.Font;
            ArquiveiLabel.ForeColor = Color.WhiteSmoke;
            ArquiveiLabel.TextAlign = ContentAlignment.MiddleRight;
            ArquiveiLabel.MouseHover += new EventHandler(this.ContabilidadePanel_MouseHover);
            ArquiveiLabel.Name = "ArquiveiLabel";

            ArquiveiPanel.Controls.Add(ArquiveiLabel);
            ArquiveiPanel.Controls.Add(Divisoria2ArquiveiImagem);
            ArquiveiPanel.Controls.Add(ArquiveiSetaLabel);
            ArquiveiPanel.Controls.Add(DivisoriaArquiveiImagem);
            SubMenuContabilidade.Controls.Add(ArquiveiPanel);
            #endregion
        }

        private void MudaSelecaoDeCoresSubMenuContabilidade()
        {
            ArquiveiLabel.Font = new Font(ArquiveiLabel.Font, ArquiveiLabel.Font.Style & ~FontStyle.Bold);
            IntegraNFSLabel.Font = new Font(IntegraNFSLabel.Font, IntegraNFSLabel.Font.Style & ~FontStyle.Bold);
            ArquiveiSetaLabel.Font = new Font(ArquiveiSetaLabel.Font, ArquiveiSetaLabel.Font.Style & ~FontStyle.Bold);

            ArquiveiLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            IntegraNFSLabel.BackColor = ArquiveiLabel.BackColor;
            ArquiveiSetaLabel.BackColor = ArquiveiLabel.BackColor;

            ArquiveiLabel.ForeColor = Color.WhiteSmoke;
            IntegraNFSLabel.ForeColor = Color.WhiteSmoke;
            ArquiveiSetaLabel.ForeColor = Color.WhiteSmoke;

            ArquiveiSetaLabel.Text = "6";
        }
        
        private void ContabilidadePanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuContabilidade();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();

            if (((Control)sender).Name.Contains("Arquivei"))
            {
                ArquiveiLabel.Font = new Font(ArquiveiLabel.Font, FontStyle.Bold);
                ArquiveiLabel.ForeColor = Publicas._fonteBotaoFocado;
                ArquiveiLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("IntegraNFS"))
            {
                IntegraNFSLabel.Font = new Font(IntegraNFSLabel.Font, FontStyle.Bold);
                IntegraNFSLabel.ForeColor = Publicas._fonteBotaoFocado;
                IntegraNFSLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void ArquiveiSetaLabel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuContabilidade();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();

            if (((Control)sender).Name.Contains("Arquivei"))
            {
                ArquiveiSetaLabel.Font = new Font(ArquiveiSetaLabel.Font, FontStyle.Bold);
                ArquiveiSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                ArquiveiSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void FechaSubMenuContabilidade()
        {
            FechaSubMenuCTBArquivei();

            if (SubMenuContabilidade != null)
            {
                SubMenuContabilidade.Visible = false;
                this.Controls.Remove(SubMenuContabilidade);
                SubMenuContabilidade.Dispose();
                SubMenuContabilidade = null;
            }
        }

        private void IntegraNFSLabel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaContabil;
            Contabilidade.IntegraNotaServico _tela = new Contabilidade.IntegraNotaServico();
            _tela.Size = new Size(this.Width - 5, this.Height - (tituloPanel.Height + panel2.Height + 2));

            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void FechaSubMenuCTBArquivei()
        {
            if (SubMenuCTBArquivei != null)
            {
                SubMenuCTBArquivei.Visible = false;
                this.Controls.Remove(SubMenuCTBArquivei);
                SubMenuCTBArquivei.Dispose();
                SubMenuCTBArquivei = null;
            }
        }

        private void ArquiveiSetaPanel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu 
            ArquiveiSetaLabel.Text = "3";
            ArquiveiSetaLabel.ForeColor = Publicas._bordaEntrada;
            ArquiveiLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuCTBArquivei != null)
            {
                FechaSubMenuCTBArquivei();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuCTBArquivei = new Panel();
            SubMenuCTBArquivei.Size = new Size(145, _heidthMenuSistema * 3);
            SubMenuCTBArquivei.Location = new Point(MenuSistemaPanel.Width + SubMenuContabilidade.Width + 4, AcessoAoMenuPanel.Height);
            SubMenuCTBArquivei.BackColor = Color.Silver;
            this.Controls.Add(SubMenuCTBArquivei);
            SubMenuCTBArquivei.BringToFront();
            SubMenuCTBArquivei.Visible = true;
            #endregion

            #region Validação
            ValidacaoArquiveiPanel = new Panel();
            ValidacaoArquiveiPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ValidacaoArquiveiPanel.Dock = DockStyle.Top;
            ValidacaoArquiveiPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ValidacaoArquiveiPanel.Name = "ValidacaoArquiveiPanel";

            DivisoriaValidacaoArquiveiImagem = new PictureBox();
            DivisoriaValidacaoArquiveiImagem.Size = new Size(0, 4);
            DivisoriaValidacaoArquiveiImagem.Dock = DockStyle.Bottom;
            DivisoriaValidacaoArquiveiImagem.BackColor = Color.Silver;

            ValidacaoArquiveiLabel = new Label();
            ValidacaoArquiveiLabel.AutoSize = false;
            ValidacaoArquiveiLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ValidacaoArquiveiLabel.Dock = DockStyle.Fill;
            ValidacaoArquiveiLabel.Text = "Validação Arquivei x Globus";
            ValidacaoArquiveiLabel.Font = CorFontepadraoLabel.Font;
            ValidacaoArquiveiLabel.ForeColor = Color.WhiteSmoke;
            ValidacaoArquiveiLabel.TextAlign = ContentAlignment.MiddleRight;
            ValidacaoArquiveiLabel.MouseHover += new EventHandler(this.ParametroArquiveiPanel_MouseHover);
            ValidacaoArquiveiLabel.Click += new System.EventHandler(this.ValidacaoArquiveiLabel_Click);
            ValidacaoArquiveiLabel.Name = "ValidacaoArquiveiLabel";

            ValidacaoArquiveiPanel.Controls.Add(ValidacaoArquiveiLabel);
            ValidacaoArquiveiPanel.Controls.Add(DivisoriaValidacaoArquiveiImagem);

            SubMenuCTBArquivei.Controls.Add(ValidacaoArquiveiPanel);
            #endregion

            #region CFOP
            CFOPArquiveiPanel = new Panel();
            CFOPArquiveiPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CFOPArquiveiPanel.Dock = DockStyle.Top;
            CFOPArquiveiPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CFOPArquiveiPanel.Name = "CFOPArquiveiPanel";

            DivisoriaCFOPArquiveiImagem = new PictureBox();
            DivisoriaCFOPArquiveiImagem.Size = new Size(0, 4);
            DivisoriaCFOPArquiveiImagem.Dock = DockStyle.Bottom;
            DivisoriaCFOPArquiveiImagem.BackColor = Color.Silver;

            CFOPArquiveiLabel = new Label();
            CFOPArquiveiLabel.AutoSize = false;
            CFOPArquiveiLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CFOPArquiveiLabel.Dock = DockStyle.Fill;
            CFOPArquiveiLabel.Text = "CFOP e CST";
            CFOPArquiveiLabel.Font = CorFontepadraoLabel.Font;
            CFOPArquiveiLabel.ForeColor = Color.WhiteSmoke;
            CFOPArquiveiLabel.TextAlign = ContentAlignment.MiddleRight;
            CFOPArquiveiLabel.MouseHover += new EventHandler(this.ParametroArquiveiPanel_MouseHover);
            CFOPArquiveiLabel.Click += new System.EventHandler(this.CFOPArquiveiLabel_Click);
            CFOPArquiveiLabel.Name = "CFOPArquiveiLabel";

            CFOPArquiveiPanel.Controls.Add(CFOPArquiveiLabel);
            CFOPArquiveiPanel.Controls.Add(DivisoriaCFOPArquiveiImagem);

            SubMenuCTBArquivei.Controls.Add(CFOPArquiveiPanel);
            #endregion

            #region Parametros
            ParametroArquiveiPanel = new Panel();
            ParametroArquiveiPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ParametroArquiveiPanel.Dock = DockStyle.Top;
            ParametroArquiveiPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ParametroArquiveiPanel.Name = "ParametroArquiveiPanel";

            DivisoriaParametroArquiveiImagem = new PictureBox();
            DivisoriaParametroArquiveiImagem.Size = new Size(0, 4);
            DivisoriaParametroArquiveiImagem.Dock = DockStyle.Bottom;
            DivisoriaParametroArquiveiImagem.BackColor = Color.Silver;

            ParametroArquiveiLabel = new Label();
            ParametroArquiveiLabel.AutoSize = false;
            ParametroArquiveiLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ParametroArquiveiLabel.Dock = DockStyle.Fill;
            ParametroArquiveiLabel.Text = "Parâmetros";
            ParametroArquiveiLabel.Font = CorFontepadraoLabel.Font;
            ParametroArquiveiLabel.ForeColor = Color.WhiteSmoke;
            ParametroArquiveiLabel.TextAlign = ContentAlignment.MiddleRight;
            ParametroArquiveiLabel.MouseHover += new EventHandler(this.ParametroArquiveiPanel_MouseHover);
            ParametroArquiveiLabel.Click += new System.EventHandler(this.ParametroArquiveiLabel_Click);
            ParametroArquiveiLabel.Name = "ParametroArquiveiLabel";

            ParametroArquiveiPanel.Controls.Add(ParametroArquiveiLabel);
            ParametroArquiveiPanel.Controls.Add(DivisoriaParametroArquiveiImagem);

            SubMenuCTBArquivei.Controls.Add(ParametroArquiveiPanel);
            #endregion
        }

        private void MudaSelecaoDeCoresSubMenuCTBArquivei()
        {
            ValidacaoArquiveiLabel.Font = new Font(ValidacaoArquiveiLabel.Font, ValidacaoArquiveiLabel.Font.Style & ~FontStyle.Bold);
            ParametroArquiveiLabel.Font = new Font(ParametroArquiveiLabel.Font, ParametroArquiveiLabel.Font.Style & ~FontStyle.Bold);
            CFOPArquiveiLabel.Font = new Font(CFOPArquiveiLabel.Font, CFOPArquiveiLabel.Font.Style & ~FontStyle.Bold);

            ValidacaoArquiveiLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ParametroArquiveiLabel.BackColor = ValidacaoArquiveiLabel.BackColor;
            CFOPArquiveiLabel.BackColor = ValidacaoArquiveiLabel.BackColor;

            ValidacaoArquiveiLabel.ForeColor = Color.WhiteSmoke;
            ParametroArquiveiLabel.ForeColor = Color.WhiteSmoke;
            CFOPArquiveiLabel.ForeColor = Color.WhiteSmoke;
        }

        private void ParametroArquiveiPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuCTBArquivei();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();

            if (((Control)sender).Name.Contains("Validacao"))
            {
                ValidacaoArquiveiLabel.Font = new Font(ValidacaoArquiveiLabel.Font, FontStyle.Bold);
                ValidacaoArquiveiLabel.ForeColor = Publicas._fonteBotaoFocado;
                ValidacaoArquiveiLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Parametro"))
            {
                ParametroArquiveiLabel.Font = new Font(ParametroArquiveiLabel.Font, FontStyle.Bold);
                ParametroArquiveiLabel.ForeColor = Publicas._fonteBotaoFocado;
                ParametroArquiveiLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("CFOP"))
            {
                CFOPArquiveiLabel.Font = new Font(CFOPArquiveiLabel.Font, FontStyle.Bold);
                CFOPArquiveiLabel.ForeColor = Publicas._fonteBotaoFocado;
                CFOPArquiveiLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void CFOPArquiveiLabel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaContabil;
            Contabilidade.CFOPeCST _tela = new Contabilidade.CFOPeCST();
            _tela.ShowDialog();
            NomePadrao();
        }

        private void ParametroArquiveiLabel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaContabil;
            Contabilidade.ParametrosArquivei _tela = new Contabilidade.ParametrosArquivei();
            _tela.ShowDialog();
            NomePadrao();            
        }

        private void ValidacaoArquiveiLabel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaContabil;
            Contabilidade.ValidacaoArquivei _tela = new Contabilidade.ValidacaoArquivei();
            _tela.Size = new Size(this.Width - 5, this.Height - (tituloPanel.Height + panel2.Height + 2));

            _tela.ShowDialog();
            NomePadrao();
        }

        #endregion

        #region Controladoria
        private void ControladoriaSetaLabel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu 
            ControladoriaSetaLabel.Text = "3";
            ControladoriaSetaLabel.ForeColor = Publicas._bordaEntrada;
            ControladoriaLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuControladoria != null)
            {
                FechaSubMenuControladoria();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuControladoria = new Panel();
            SubMenuControladoria.Size = new Size(145, _heidthMenuSistema);
            SubMenuControladoria.Location = new Point(MenuSistemaPanel.Width + 2, AcessoAoMenuPanel.Height);
            SubMenuControladoria.BackColor = Color.Silver;
            this.Controls.Add(SubMenuControladoria);
            SubMenuControladoria.BringToFront();
            SubMenuControladoria.Visible = true;
            #endregion

            #region RadarBI
            RadarBIPanel = new Panel();
            RadarBIPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            RadarBIPanel.Dock = DockStyle.Top;
            RadarBIPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            RadarBIPanel.Name = "RadarBIPanel";

            DivisoriaRadarBIImagem = new PictureBox();
            DivisoriaRadarBIImagem.Size = new Size(0, 4);
            DivisoriaRadarBIImagem.Dock = DockStyle.Bottom;
            DivisoriaRadarBIImagem.BackColor = Color.Silver;

            RadarBILabel = new Label();
            RadarBILabel.AutoSize = false;
            RadarBILabel.Size = new Size(_widthLabelMenuSistema, 0);
            RadarBILabel.Dock = DockStyle.Fill;
            RadarBILabel.Text = "Radar PowerBI";
            RadarBILabel.Font = CorFontepadraoLabel.Font;
            RadarBILabel.ForeColor = Color.WhiteSmoke;
            RadarBILabel.TextAlign = ContentAlignment.MiddleRight;
            RadarBILabel.MouseHover += new EventHandler(this.RadarPanel_MouseHover);
            RadarBILabel.Click += new System.EventHandler(this.RadarBIPanel_Click);
            RadarBILabel.Name = "RadarBILabel";

            RadarBIPanel.Controls.Add(RadarBILabel);
            RadarBIPanel.Controls.Add(DivisoriaRadarBIImagem);

            SubMenuControladoria.Controls.Add(RadarBIPanel);
            #endregion

        }

        private void MudaSelecaoDeCoresSubMenuControladoria()
        {
            RadarBILabel.Font = new Font(RadarBILabel.Font, RadarBILabel.Font.Style & ~FontStyle.Bold);
            RadarBILabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            RadarBILabel.ForeColor = Color.WhiteSmoke;
        }

        private void RadarPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuControladoria();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuContabilidade();

            if (((Control)sender).Name.Contains("RadarBI"))
            {
                RadarBILabel.Font = new Font(RadarBILabel.Font, FontStyle.Bold);
                RadarBILabel.ForeColor = Publicas._fonteBotaoFocado;
                RadarBILabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void FechaSubMenuControladoria()
        {
            if (SubMenuControladoria != null)
            {
                SubMenuControladoria.Visible = false;
                this.Controls.Remove(SubMenuControladoria);
                SubMenuControladoria.Dispose();
                SubMenuControladoria = null;
            }
        }

        private void RadarBIPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBI;
            Diversos.RadarBI _tela = new Diversos.RadarBI();
            _tela.ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }
        #endregion

        #region Atendimento

        private void AtendimentoSetaLabel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu do RecursosHumanos
            AtendimentoSetaLabel.Text = "3";
            AtendimentoSetaLabel.ForeColor = Publicas._bordaEntrada;
            AtendimentoLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuAtendimento != null)
            {
                FechaSubMenuAtendimento();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuAtendimento = new Panel();
            SubMenuAtendimento.Size = new Size(145, _heidthMenuSistema * 2);
            SubMenuAtendimento.Location = new Point(MenuSistemaPanel.Width + 2, AcessoAoMenuPanel.Height);
            SubMenuAtendimento.BackColor = Color.Silver;
            this.Controls.Add(SubMenuAtendimento);
            SubMenuAtendimento.BringToFront();
            SubMenuAtendimento.Visible = true;
            #endregion

            #region SAC 
            SACPanel = new Panel();
            SACPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            SACPanel.Dock = DockStyle.Top;
            SACPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            SACPanel.Name = "SACPanel";
            SACPanel.Enabled = Publicas._usuario.AcessaSac;

            DivisoriaSACImagem = new PictureBox();
            DivisoriaSACImagem.Size = new Size(0, 4);
            DivisoriaSACImagem.Dock = DockStyle.Bottom;
            DivisoriaSACImagem.BackColor = Color.Silver;

            Divisoria2SACImagem = new PictureBox();
            Divisoria2SACImagem.Size = new Size(1, 2);
            Divisoria2SACImagem.Dock = DockStyle.Right;
            Divisoria2SACImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            SACSetaLabel = new Label();
            SACSetaLabel.AutoSize = false;
            SACSetaLabel.Size = new Size(20, 59);
            SACSetaLabel.Dock = DockStyle.Right;
            SACSetaLabel.Text = "6";
            SACSetaLabel.Font = new Font("Webdings", (float)12);
            SACSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            SACSetaLabel.Click += new System.EventHandler(this.SACSetaLabel_Click);
            SACSetaLabel.MouseHover += new EventHandler(this.SACPanelSetaPanel_MouseHover);
            SACSetaLabel.Name = "SACSetaLabel";
            SACSetaLabel.ForeColor = Color.WhiteSmoke;

            SACLabel = new Label();
            SACLabel.AutoSize = false;
            SACLabel.Size = new Size(_widthLabelMenuSistema, 0);
            SACLabel.Dock = DockStyle.Fill;
            SACLabel.Text = "SAC";
            SACLabel.Font = CorFontepadraoLabel.Font;
            SACLabel.ForeColor = Color.WhiteSmoke;
            SACLabel.TextAlign = ContentAlignment.MiddleRight;
            SACLabel.MouseHover += new EventHandler(this.SACPanel_MouseHover);
            SACLabel.Name = "SACLabel";

            SACPanel.Controls.Add(SACLabel);
            //SACPanel.Controls.Add(SACImagem);
            SACPanel.Controls.Add(Divisoria2SACImagem);
            SACPanel.Controls.Add(SACSetaLabel);
            SACPanel.Controls.Add(DivisoriaSACImagem);

            SubMenuAtendimento.Controls.Add(SACPanel);
            #endregion

            #region Chamados 
            ChamadosPanel = new Panel();
            ChamadosPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ChamadosPanel.Dock = DockStyle.Top;
            ChamadosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ChamadosPanel.Name = "ChamadosPanel";

            DivisoriaChamadosImagem = new PictureBox();
            DivisoriaChamadosImagem.Size = new Size(0, 4);
            DivisoriaChamadosImagem.Dock = DockStyle.Bottom;
            DivisoriaChamadosImagem.BackColor = Color.Silver;

            Divisoria2ChamadosImagem = new PictureBox();
            Divisoria2ChamadosImagem.Size = new Size(1, 2);
            Divisoria2ChamadosImagem.Dock = DockStyle.Right;
            Divisoria2ChamadosImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            ChamadosSetaLabel = new Label();
            ChamadosSetaLabel.AutoSize = false;
            ChamadosSetaLabel.Size = new Size(20, 59);
            ChamadosSetaLabel.Dock = DockStyle.Right;
            ChamadosSetaLabel.Text = "6";
            ChamadosSetaLabel.Font = new Font("Webdings", (float)12);
            ChamadosSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            ChamadosSetaLabel.Click += new System.EventHandler(this.ChamadoSetaLabel_Click);
            ChamadosSetaLabel.MouseHover += new EventHandler(this.SACPanelSetaPanel_MouseHover);
            ChamadosSetaLabel.Name = "ChamadosSetaLabel";
            ChamadosSetaLabel.ForeColor = Color.WhiteSmoke;

            ChamadosLabel = new Label();
            ChamadosLabel.AutoSize = false;
            ChamadosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ChamadosLabel.Dock = DockStyle.Fill;
            ChamadosLabel.Text = "Chamados";
            ChamadosLabel.Font = CorFontepadraoLabel.Font;
            ChamadosLabel.ForeColor = Color.WhiteSmoke;
            ChamadosLabel.TextAlign = ContentAlignment.MiddleRight;
            ChamadosLabel.MouseHover += new EventHandler(this.SACPanel_MouseHover);
            ChamadosLabel.Name = "ChamadosLabel";

            ChamadosPanel.Controls.Add(ChamadosLabel);
            //ChamadosPanel.Controls.Add(ChamadosImagem);
            ChamadosPanel.Controls.Add(Divisoria2ChamadosImagem);
            ChamadosPanel.Controls.Add(ChamadosSetaLabel);
            ChamadosPanel.Controls.Add(DivisoriaChamadosImagem);

            SubMenuAtendimento.Controls.Add(ChamadosPanel);
            #endregion
        }

        private void FechaSubMenuAtendimento()
        {
            FechaSubMenuSAC();
            FechaSubMenuChamadosPanel();

            if (SubMenuAtendimento != null)
            {
                SubMenuAtendimento.Visible = false;
                this.Controls.Remove(SubMenuAtendimento);
                SubMenuAtendimento.Dispose();
                SubMenuAtendimento = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuAtendimento()
        {
            SACLabel.Font = new Font(SACLabel.Font, SACLabel.Font.Style & ~FontStyle.Bold);
            ChamadosLabel.Font = new Font(ChamadosLabel.Font, ChamadosLabel.Font.Style & ~FontStyle.Bold);
            SACSetaLabel.Font = new Font(SACSetaLabel.Font, SACSetaLabel.Font.Style & ~FontStyle.Bold);
            ChamadosSetaLabel.Font = new Font(ChamadosSetaLabel.Font, ChamadosSetaLabel.Font.Style & ~FontStyle.Bold);

            SACLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ChamadosLabel.BackColor = SACLabel.BackColor;
            SACSetaLabel.BackColor = SACLabel.BackColor;
            ChamadosSetaLabel.BackColor = SACLabel.BackColor;

            SACLabel.ForeColor = Color.WhiteSmoke;
            ChamadosLabel.ForeColor = Color.WhiteSmoke;
            SACSetaLabel.ForeColor = Color.WhiteSmoke;
            ChamadosSetaLabel.ForeColor = Color.WhiteSmoke;

            SACSetaLabel.Text = "6";
            ChamadosSetaLabel.Text = "6";
        }

        private void SACPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuAtendimento();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();
            FechaSubMenuChamadosPanel();
            FechaSubMenuSAC();

            if (((Control)sender).Name.Contains("SAC"))
            {
                SACLabel.Font = new Font(SACLabel.Font, FontStyle.Bold);
                SACLabel.ForeColor = Publicas._fonteBotaoFocado;
                SACLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Chamado"))
            {
                ChamadosLabel.Font = new Font(ChamadosLabel.Font, FontStyle.Bold);
                ChamadosLabel.ForeColor = Publicas._fonteBotaoFocado;
                ChamadosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void SACPanelSetaPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuAtendimento();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();
            FechaSubMenuChamadosPanel();
            FechaSubMenuSAC();

            if (((Control)sender).Name.Contains("SAC"))
            {
                SACSetaLabel.Font = new Font(SACSetaLabel.Font, FontStyle.Bold);
                SACSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                SACSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Chamado"))
            {
                ChamadosSetaLabel.Font = new Font(ChamadosSetaLabel.Font, FontStyle.Bold);
                ChamadosSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                ChamadosSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        #region SAC
        private void SACSetaLabel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu 
            SACSetaLabel.Text = "3";
            SACSetaLabel.ForeColor = Publicas._bordaEntrada;
            SACLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuSACPanel != null)
            {
                FechaSubMenuAtendimento();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuSACPanel = new Panel();
            SubMenuSACPanel.Size = new Size(145, _heidthMenuSistema * 6);
            SubMenuSACPanel.Location = new Point(MenuSistemaPanel.Width + SubMenuAtendimento.Width + 4, AcessoAoMenuPanel.Height);
            SubMenuSACPanel.BackColor = Color.Silver;
            this.Controls.Add(SubMenuSACPanel);
            SubMenuSACPanel.BringToFront();
            SubMenuSACPanel.Visible = true;
            #endregion

            #region Satisfacao
            SatisfacaoPanel = new Panel();
            SatisfacaoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            SatisfacaoPanel.Dock = DockStyle.Top;
            SatisfacaoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            SatisfacaoPanel.Name = "SatisfacaoPanel";

            DivisoriaSatisfacaoImagem = new PictureBox();
            DivisoriaSatisfacaoImagem.Size = new Size(0, 4);
            DivisoriaSatisfacaoImagem.Dock = DockStyle.Bottom;
            DivisoriaSatisfacaoImagem.BackColor = Color.Silver;

            SatisfacaoLabel = new Label();
            SatisfacaoLabel.AutoSize = false;
            SatisfacaoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            SatisfacaoLabel.Dock = DockStyle.Fill;
            SatisfacaoLabel.Text = "Satisfação";
            SatisfacaoLabel.Font = CorFontepadraoLabel.Font;
            SatisfacaoLabel.ForeColor = Color.WhiteSmoke;
            SatisfacaoLabel.TextAlign = ContentAlignment.MiddleRight;
            SatisfacaoLabel.MouseHover += new EventHandler(this.CadastroSACPanel_MouseHover);
            SatisfacaoLabel.Click += new System.EventHandler(this.SatisfacaoPanel_Click);
            SatisfacaoLabel.Name = "SatisfacaoLabel";

            SatisfacaoPanel.Controls.Add(SatisfacaoLabel);
            SatisfacaoPanel.Controls.Add(DivisoriaSatisfacaoImagem);

            SubMenuSACPanel.Controls.Add(SatisfacaoPanel);
            #endregion

            #region FinalizarAtendimento
            FinalizarAtendimentoPanel = new Panel();
            FinalizarAtendimentoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            FinalizarAtendimentoPanel.Dock = DockStyle.Top;
            FinalizarAtendimentoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            FinalizarAtendimentoPanel.Name = "FinalizarAtendimentoPanel";

            DivisoriaFinalizarAtendimentoImagem = new PictureBox();
            DivisoriaFinalizarAtendimentoImagem.Size = new Size(0, 4);
            DivisoriaFinalizarAtendimentoImagem.Dock = DockStyle.Bottom;
            DivisoriaFinalizarAtendimentoImagem.BackColor = Color.Silver;

            FinalizarAtendimentoLabel = new Label();
            FinalizarAtendimentoLabel.AutoSize = false;
            FinalizarAtendimentoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            FinalizarAtendimentoLabel.Dock = DockStyle.Fill;
            FinalizarAtendimentoLabel.Text = "Finalizar Atendimento";
            FinalizarAtendimentoLabel.Font = CorFontepadraoLabel.Font;
            FinalizarAtendimentoLabel.ForeColor = Color.WhiteSmoke;
            FinalizarAtendimentoLabel.TextAlign = ContentAlignment.MiddleRight;
            FinalizarAtendimentoLabel.MouseHover += new EventHandler(this.CadastroSACPanel_MouseHover);
            FinalizarAtendimentoLabel.Click += new System.EventHandler(this.FinalizarAtendimentoPanel_Click);
            FinalizarAtendimentoLabel.Name = "FinalizarAtendimentoLabel";

            FinalizarAtendimentoPanel.Controls.Add(FinalizarAtendimentoLabel);
            FinalizarAtendimentoPanel.Controls.Add(DivisoriaFinalizarAtendimentoImagem);

            SubMenuSACPanel.Controls.Add(FinalizarAtendimentoPanel);
            #endregion

            #region ResponderAtendimento
            ResponderAtendimentoPanel = new Panel();
            ResponderAtendimentoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ResponderAtendimentoPanel.Dock = DockStyle.Top;
            ResponderAtendimentoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ResponderAtendimentoPanel.Name = "ResponderAtendimentoPanel";

            DivisoriaResponderAtendimentoImagem = new PictureBox();
            DivisoriaResponderAtendimentoImagem.Size = new Size(0, 4);
            DivisoriaResponderAtendimentoImagem.Dock = DockStyle.Bottom;
            DivisoriaResponderAtendimentoImagem.BackColor = Color.Silver;

            ResponderAtendimentoLabel = new Label();
            ResponderAtendimentoLabel.AutoSize = false;
            ResponderAtendimentoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ResponderAtendimentoLabel.Dock = DockStyle.Fill;
            ResponderAtendimentoLabel.Text = "Responder Atendimento";
            ResponderAtendimentoLabel.Font = CorFontepadraoLabel.Font;
            ResponderAtendimentoLabel.ForeColor = Color.WhiteSmoke;
            ResponderAtendimentoLabel.TextAlign = ContentAlignment.MiddleRight;
            ResponderAtendimentoLabel.MouseHover += new EventHandler(this.CadastroSACPanel_MouseHover);
            ResponderAtendimentoLabel.Click += new System.EventHandler(this.ResponderAtendimentoPanel_Click);
            ResponderAtendimentoLabel.Name = "ResponderAtendimentoLabel";

            ResponderAtendimentoPanel.Controls.Add(ResponderAtendimentoLabel);
            ResponderAtendimentoPanel.Controls.Add(DivisoriaResponderAtendimentoImagem);

            SubMenuSACPanel.Controls.Add(ResponderAtendimentoPanel);
            #endregion

            #region RetornarLigacoes
            RetornarLigacoesPanel = new Panel();
            RetornarLigacoesPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            RetornarLigacoesPanel.Dock = DockStyle.Top;
            RetornarLigacoesPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            RetornarLigacoesPanel.Name = "RetornarLigacoesPanel";

            DivisoriaRetornarLigacoesImagem = new PictureBox();
            DivisoriaRetornarLigacoesImagem.Size = new Size(0, 4);
            DivisoriaRetornarLigacoesImagem.Dock = DockStyle.Bottom;
            DivisoriaRetornarLigacoesImagem.BackColor = Color.Silver;

            RetornarLigacoesLabel = new Label();
            RetornarLigacoesLabel.AutoSize = false;
            RetornarLigacoesLabel.Size = new Size(_widthLabelMenuSistema, 0);
            RetornarLigacoesLabel.Dock = DockStyle.Fill;
            RetornarLigacoesLabel.Text = "Retornar Ligações";
            RetornarLigacoesLabel.Font = CorFontepadraoLabel.Font;
            RetornarLigacoesLabel.ForeColor = Color.WhiteSmoke;
            RetornarLigacoesLabel.TextAlign = ContentAlignment.MiddleRight;
            RetornarLigacoesLabel.MouseHover += new EventHandler(this.CadastroSACPanel_MouseHover);
            RetornarLigacoesLabel.Click += new System.EventHandler(this.RetornarLigacoesPanel_Click);
            RetornarLigacoesLabel.Name = "RetornarLigacoesLabel";

            RetornarLigacoesPanel.Controls.Add(RetornarLigacoesLabel);
            RetornarLigacoesPanel.Controls.Add(DivisoriaRetornarLigacoesImagem);

            SubMenuSACPanel.Controls.Add(RetornarLigacoesPanel);
            #endregion

            #region AtendimentoSAC
            AtendimentoSACPanel = new Panel();
            AtendimentoSACPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AtendimentoSACPanel.Dock = DockStyle.Top;
            AtendimentoSACPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AtendimentoSACPanel.Name = "AtendimentoSACPanel";

            DivisoriaAtendimentoSACImagem = new PictureBox();
            DivisoriaAtendimentoSACImagem.Size = new Size(0, 4);
            DivisoriaAtendimentoSACImagem.Dock = DockStyle.Bottom;
            DivisoriaAtendimentoSACImagem.BackColor = Color.Silver;

            AtendimentoSACLabel = new Label();
            AtendimentoSACLabel.AutoSize = false;
            AtendimentoSACLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AtendimentoSACLabel.Dock = DockStyle.Fill;
            AtendimentoSACLabel.Text = "Registrar Atendimento";
            AtendimentoSACLabel.Font = CorFontepadraoLabel.Font;
            AtendimentoSACLabel.ForeColor = Color.WhiteSmoke;
            AtendimentoSACLabel.TextAlign = ContentAlignment.MiddleRight;
            AtendimentoSACLabel.MouseHover += new EventHandler(this.CadastroSACPanel_MouseHover);
            AtendimentoSACLabel.Click += new System.EventHandler(this.AtendimentoSACPanel_Click);
            AtendimentoSACLabel.Name = "AtendimentoSACLabel";

            AtendimentoSACPanel.Controls.Add(AtendimentoSACLabel);
            AtendimentoSACPanel.Controls.Add(DivisoriaAtendimentoSACImagem);

            SubMenuSACPanel.Controls.Add(AtendimentoSACPanel);
            #endregion

            #region CadastroSAC 
            CadastroSACPanel = new Panel();
            CadastroSACPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CadastroSACPanel.Dock = DockStyle.Top;
            CadastroSACPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CadastroSACPanel.Name = "CadastroSACPanel";

            DivisoriaCadastroSACImagem = new PictureBox();
            DivisoriaCadastroSACImagem.Size = new Size(0, 4);
            DivisoriaCadastroSACImagem.Dock = DockStyle.Bottom;
            DivisoriaCadastroSACImagem.BackColor = Color.Silver;

            Divisoria2CadastroSACImagem = new PictureBox();
            Divisoria2CadastroSACImagem.Size = new Size(1, 2);
            Divisoria2CadastroSACImagem.Dock = DockStyle.Right;
            Divisoria2CadastroSACImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            CadastroSACSetaLabel = new Label();
            CadastroSACSetaLabel.AutoSize = false;
            CadastroSACSetaLabel.Size = new Size(20, 59);
            CadastroSACSetaLabel.Dock = DockStyle.Right;
            CadastroSACSetaLabel.Text = "6";
            CadastroSACSetaLabel.Font = new Font("Webdings", (float)12);
            CadastroSACSetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            CadastroSACSetaLabel.Click += new System.EventHandler(this.CadastroSACSetaLabel_Click);
            CadastroSACSetaLabel.MouseHover += new EventHandler(this.CadastroSACSetaPanel_MouseHover);
            CadastroSACSetaLabel.Name = "CadastroSACSetaLabel";
            CadastroSACSetaLabel.ForeColor = Color.WhiteSmoke;

            CadastroSACLabel = new Label();
            CadastroSACLabel.AutoSize = false;
            CadastroSACLabel.Size = new Size(_widthLabelMenuSistema, 0);
            CadastroSACLabel.Dock = DockStyle.Fill;
            CadastroSACLabel.Text = "Cadastros";
            CadastroSACLabel.Font = CorFontepadraoLabel.Font;
            CadastroSACLabel.ForeColor = Color.WhiteSmoke;
            CadastroSACLabel.TextAlign = ContentAlignment.MiddleRight;
            CadastroSACLabel.MouseHover += new EventHandler(this.CadastroSACPanel_MouseHover);
            CadastroSACLabel.Name = "CadastroSACLabel";

            CadastroSACPanel.Controls.Add(CadastroSACLabel);
            //CadastroSACPanel.Controls.Add(CadastroSACImagem);
            CadastroSACPanel.Controls.Add(Divisoria2CadastroSACImagem);
            CadastroSACPanel.Controls.Add(CadastroSACSetaLabel);
            CadastroSACPanel.Controls.Add(DivisoriaCadastroSACImagem);

            SubMenuSACPanel.Controls.Add(CadastroSACPanel);
            #endregion
        }

        private void FechaSubMenuSAC()
        {
            FechaSubMenuCadastroSAC();

            if (SubMenuSACPanel != null)
            {
                SubMenuSACPanel.Visible = false;
                this.Controls.Remove(SubMenuSACPanel);
                SubMenuSACPanel.Dispose();
                SubMenuSACPanel = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuSAC()
        {
            CadastroSACLabel.Font = new Font(CadastroSACLabel.Font, CadastroSACLabel.Font.Style & ~FontStyle.Bold);
            CadastroSACSetaLabel.Font = new Font(CadastroSACSetaLabel.Font, CadastroSACSetaLabel.Font.Style & ~FontStyle.Bold);
            AtendimentoSACLabel.Font = new Font(AtendimentoSACLabel.Font, AtendimentoSACLabel.Font.Style & ~FontStyle.Bold);
            RetornarLigacoesLabel.Font = new Font(RetornarLigacoesLabel.Font, RetornarLigacoesLabel.Font.Style & ~FontStyle.Bold);
            ResponderAtendimentoLabel.Font = new Font(ResponderAtendimentoLabel.Font, ResponderAtendimentoLabel.Font.Style & ~FontStyle.Bold);
            FinalizarAtendimentoLabel.Font = new Font(FinalizarAtendimentoLabel.Font, FinalizarAtendimentoLabel.Font.Style & ~FontStyle.Bold);
            SatisfacaoLabel.Font = new Font(SatisfacaoLabel.Font, SatisfacaoLabel.Font.Style & ~FontStyle.Bold);

            CadastroSACLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CadastroSACSetaLabel.BackColor = CadastroSACLabel.BackColor;
            AtendimentoSACLabel.BackColor = CadastroSACLabel.BackColor;
            RetornarLigacoesLabel.BackColor = CadastroSACLabel.BackColor;
            ResponderAtendimentoLabel.BackColor = CadastroSACLabel.BackColor;
            FinalizarAtendimentoLabel.BackColor = CadastroSACLabel.BackColor;
            SatisfacaoLabel.BackColor = CadastroSACLabel.BackColor;

            CadastroSACLabel.ForeColor = Color.WhiteSmoke;
            CadastroSACSetaLabel.ForeColor = Color.WhiteSmoke;
            AtendimentoSACLabel.ForeColor = Color.WhiteSmoke;
            RetornarLigacoesLabel.ForeColor = Color.WhiteSmoke;
            ResponderAtendimentoLabel.ForeColor = Color.WhiteSmoke;
            FinalizarAtendimentoLabel.ForeColor = Color.WhiteSmoke;
            SatisfacaoLabel.ForeColor = Color.WhiteSmoke;

            CadastroSACSetaLabel.Text = "6";
        }

        private void CadastroSACPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuSAC();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();
            FechaSubMenuCadastroSAC();

            if (((Control)sender).Name.Contains("Cadastro"))
            {
                CadastroSACLabel.Font = new Font(CadastroSACLabel.Font, FontStyle.Bold);
                CadastroSACLabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastroSACLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Retornar"))
            {
                RetornarLigacoesLabel.Font = new Font(RetornarLigacoesLabel.Font, FontStyle.Bold);
                RetornarLigacoesLabel.ForeColor = Publicas._fonteBotaoFocado;
                RetornarLigacoesLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Responder"))
            {
                ResponderAtendimentoLabel.Font = new Font(ResponderAtendimentoLabel.Font, FontStyle.Bold);
                ResponderAtendimentoLabel.ForeColor = Publicas._fonteBotaoFocado;
                ResponderAtendimentoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Finalizar"))
            {
                FinalizarAtendimentoLabel.Font = new Font(FinalizarAtendimentoLabel.Font, FontStyle.Bold);
                FinalizarAtendimentoLabel.ForeColor = Publicas._fonteBotaoFocado;
                FinalizarAtendimentoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Atendimento"))
            {
                AtendimentoSACLabel.Font = new Font(AtendimentoSACLabel.Font, FontStyle.Bold);
                AtendimentoSACLabel.ForeColor = Publicas._fonteBotaoFocado;
                AtendimentoSACLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Satisfacao"))
            {
                SatisfacaoLabel.Font = new Font(SatisfacaoLabel.Font, FontStyle.Bold);
                SatisfacaoLabel.ForeColor = Publicas._fonteBotaoFocado;
                SatisfacaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void CadastroSACSetaPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuSAC();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();
            FechaSubMenuCadastroSAC();

            if (((Control)sender).Name.Contains("Cadastro"))
            {
                CadastroSACSetaLabel.Font = new Font(CadastroSACSetaLabel.Font, FontStyle.Bold);
                CadastroSACSetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastroSACSetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }            
        }

        private void AtendimentoSACPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new SAC.Atendimentos().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void RetornarLigacoesPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new SAC.Retornar().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void ResponderAtendimentoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new SAC.Responder().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void FinalizarAtendimentoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new SAC.Finalizar().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void SatisfacaoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new SAC.Satisfacao().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        #region cadastro
        private void CadastroSACSetaLabel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu 
            SACSetaLabel.Text = "3";
            SACSetaLabel.ForeColor = Publicas._bordaEntrada;
            SACLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuCadastroSACPanel != null)
            {
                FechaSubMenuAtendimento();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuCadastroSACPanel = new Panel();
            SubMenuCadastroSACPanel.Size = new Size(145, _heidthMenuSistema* 2);
            SubMenuCadastroSACPanel.Location = new Point(MenuSistemaPanel.Width + SubMenuAtendimento.Width + SubMenuSACPanel.Width + 6, AcessoAoMenuPanel.Height);
            SubMenuCadastroSACPanel.BackColor = Color.Silver;
            this.Controls.Add(SubMenuCadastroSACPanel);
            SubMenuCadastroSACPanel.BringToFront();
            SubMenuCadastroSACPanel.Visible = true;
            #endregion

            #region TiposAtendimento
            TiposAtendimentoPanel = new Panel();
            TiposAtendimentoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            TiposAtendimentoPanel.Dock = DockStyle.Top;
            TiposAtendimentoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            TiposAtendimentoPanel.Name = "TiposAtendimentoPanel";

            DivisoriaTiposAtendimentoImagem = new PictureBox();
            DivisoriaTiposAtendimentoImagem.Size = new Size(0, 4);
            DivisoriaTiposAtendimentoImagem.Dock = DockStyle.Bottom;
            DivisoriaTiposAtendimentoImagem.BackColor = Color.Silver;

            TiposAtendimentoLabel = new Label();
            TiposAtendimentoLabel.AutoSize = false;
            TiposAtendimentoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            TiposAtendimentoLabel.Dock = DockStyle.Fill;
            TiposAtendimentoLabel.Text = "Tipos de Atendimento";
            TiposAtendimentoLabel.Font = CorFontepadraoLabel.Font;
            TiposAtendimentoLabel.ForeColor = Color.WhiteSmoke;
            TiposAtendimentoLabel.TextAlign = ContentAlignment.MiddleRight;
            TiposAtendimentoLabel.MouseHover += new EventHandler(this.TiposAtendimentoPanel_MouseHover);
            TiposAtendimentoLabel.Click += new System.EventHandler(this.TiposAtendimentoPanel_Click);
            TiposAtendimentoLabel.Name = "TiposAtendimentoLabel";

            TiposAtendimentoPanel.Controls.Add(TiposAtendimentoLabel);
            TiposAtendimentoPanel.Controls.Add(DivisoriaTiposAtendimentoImagem);

            SubMenuCadastroSACPanel.Controls.Add(TiposAtendimentoPanel);
            #endregion

            #region TiposAtendimentoEMTU
            TiposAtendimentoEMTUPanel = new Panel();
            TiposAtendimentoEMTUPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            TiposAtendimentoEMTUPanel.Dock = DockStyle.Top;
            TiposAtendimentoEMTUPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            TiposAtendimentoEMTUPanel.Name = "TiposAtendimentoEMTUPanel";

            DivisoriaTiposAtendimentoEMTUImagem = new PictureBox();
            DivisoriaTiposAtendimentoEMTUImagem.Size = new Size(0, 4);
            DivisoriaTiposAtendimentoEMTUImagem.Dock = DockStyle.Bottom;
            DivisoriaTiposAtendimentoEMTUImagem.BackColor = Color.Silver;

            TiposAtendimentoEMTULabel = new Label();
            TiposAtendimentoEMTULabel.AutoSize = false;
            TiposAtendimentoEMTULabel.Size = new Size(_widthLabelMenuSistema, 0);
            TiposAtendimentoEMTULabel.Dock = DockStyle.Fill;
            TiposAtendimentoEMTULabel.Text = "Tipos de Atendimento EMTU";
            TiposAtendimentoEMTULabel.Font = CorFontepadraoLabel.Font;
            TiposAtendimentoEMTULabel.ForeColor = Color.WhiteSmoke;
            TiposAtendimentoEMTULabel.TextAlign = ContentAlignment.MiddleRight;
            TiposAtendimentoEMTULabel.MouseHover += new EventHandler(this.TiposAtendimentoPanel_MouseHover);
            TiposAtendimentoEMTULabel.Click += new System.EventHandler(this.TiposAtendimentoEMTUPanel_Click);
            TiposAtendimentoEMTULabel.Name = "TiposAtendimentoEMTULabel";

            TiposAtendimentoEMTUPanel.Controls.Add(TiposAtendimentoEMTULabel);
            TiposAtendimentoEMTUPanel.Controls.Add(DivisoriaTiposAtendimentoEMTUImagem);

            SubMenuCadastroSACPanel.Controls.Add(TiposAtendimentoEMTUPanel);
            #endregion
        }

        private void FechaSubMenuCadastroSAC()
        {
            if (SubMenuCadastroSACPanel != null)
            {
                SubMenuCadastroSACPanel.Visible = false;
                this.Controls.Remove(SubMenuCadastroSACPanel);
                SubMenuCadastroSACPanel.Dispose();
                SubMenuCadastroSACPanel = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuCadastroSAC()
        {
            TiposAtendimentoEMTULabel.Font = new Font(TiposAtendimentoEMTULabel.Font, TiposAtendimentoEMTULabel.Font.Style & ~FontStyle.Bold);
            TiposAtendimentoLabel.Font = new Font(TiposAtendimentoLabel.Font, TiposAtendimentoLabel.Font.Style & ~FontStyle.Bold);

            TiposAtendimentoEMTULabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            TiposAtendimentoLabel.BackColor = CadastroSACLabel.BackColor;

            TiposAtendimentoEMTULabel.ForeColor = Color.WhiteSmoke;
            TiposAtendimentoLabel.ForeColor = Color.WhiteSmoke;            
        }

        private void TiposAtendimentoPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuCadastroSAC();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();

            if (((Control)sender).Name.Contains("EMTU"))
            {
                TiposAtendimentoEMTULabel.Font = new Font(TiposAtendimentoEMTULabel.Font, FontStyle.Bold);
                TiposAtendimentoEMTULabel.ForeColor = Publicas._fonteBotaoFocado;
                TiposAtendimentoEMTULabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Tipos"))
            {
                TiposAtendimentoLabel.Font = new Font(TiposAtendimentoLabel.Font, FontStyle.Bold);
                TiposAtendimentoLabel.ForeColor = Publicas._fonteBotaoFocado;
                TiposAtendimentoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void TiposAtendimentoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new TiposDeAtendimento().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void TiposAtendimentoEMTUPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new EMTU().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }
        #endregion

        #endregion

        #region Chamados
        private void ChamadoSetaLabel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu 
            ChamadosSetaLabel.Text = "3";
            ChamadosSetaLabel.ForeColor = Publicas._bordaEntrada;
            ChamadosLabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuChamadosPanel != null)
            {
                FechaSubMenuChamadosPanel();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuChamadosPanel = new Panel();
            SubMenuChamadosPanel.Size = new Size(145, _heidthMenuSistema);
            SubMenuChamadosPanel.Location = new Point(MenuSistemaPanel.Width + SubMenuAtendimento.Width + 4, AcessoAoMenuPanel.Height);
            SubMenuChamadosPanel.BackColor = Color.Silver;
            this.Controls.Add(SubMenuChamadosPanel);
            SubMenuChamadosPanel.BringToFront();
            SubMenuChamadosPanel.Visible = true;
            #endregion

            #region AbrirChamado
            AbrirChamadoPanel = new Panel();
            AbrirChamadoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            AbrirChamadoPanel.Dock = DockStyle.Top;
            AbrirChamadoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            AbrirChamadoPanel.Name = "AbrirChamadoPanel";
            AbrirChamadoPanel.Enabled = false;

            DivisoriaAbrirChamadoImagem = new PictureBox();
            DivisoriaAbrirChamadoImagem.Size = new Size(0, 4);
            DivisoriaAbrirChamadoImagem.Dock = DockStyle.Bottom;
            DivisoriaAbrirChamadoImagem.BackColor = Color.Silver;

            AbrirChamadoLabel = new Label();
            AbrirChamadoLabel.AutoSize = false;
            AbrirChamadoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            AbrirChamadoLabel.Dock = DockStyle.Fill;
            AbrirChamadoLabel.Text = "Abrir Chamado";
            AbrirChamadoLabel.Font = CorFontepadraoLabel.Font;
            AbrirChamadoLabel.ForeColor = Color.WhiteSmoke;
            AbrirChamadoLabel.TextAlign = ContentAlignment.MiddleRight;
            AbrirChamadoLabel.MouseHover += new EventHandler(this.AbrirChamadoPanel_MouseHover);
            //AbrirChamadoLabel.Click += new System.EventHandler(this.AbrirChamadoPanel_Click);
            AbrirChamadoLabel.Name = "AbrirChamadoLabel";

            AbrirChamadoPanel.Controls.Add(AbrirChamadoLabel);
            AbrirChamadoPanel.Controls.Add(DivisoriaAbrirChamadoImagem);

            SubMenuChamadosPanel.Controls.Add(AbrirChamadoPanel);
            #endregion
        }

        private void FechaSubMenuChamadosPanel()
        {
            if (SubMenuChamadosPanel != null)
            {
                SubMenuChamadosPanel.Visible = false;
                this.Controls.Remove(SubMenuChamadosPanel);
                SubMenuChamadosPanel.Dispose();
                SubMenuChamadosPanel = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuChamadosPanel()
        {
            AbrirChamadoLabel.Font = new Font(AbrirChamadoLabel.Font, AbrirChamadoLabel.Font.Style & ~FontStyle.Bold);

            AbrirChamadoLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            AbrirChamadoLabel.ForeColor = Color.WhiteSmoke;
        }

        private void AbrirChamadoPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuChamadosPanel();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();
            FechaSubMenuSAC();

            if (((Control)sender).Name.Contains("Abrir"))
            {
                AbrirChamadoLabel.Font = new Font(AbrirChamadoLabel.Font, FontStyle.Bold);
                AbrirChamadoLabel.ForeColor = Publicas._fonteBotaoFocado;
                AbrirChamadoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        #endregion

        #endregion

        #region TI
        private void TISetaLabel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu do RecursosHumanos
            TISetaLabel.Text = "3";
            TISetaLabel.ForeColor = Publicas._bordaEntrada;
            TILabel.ForeColor = Publicas._bordaEntrada;

            if (MenuTIPanel != null)
            {
                FechaSubMenuTI();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            MenuTIPanel = new Panel();
            MenuTIPanel.Size = new Size(145, _heidthMenuSistema);
            MenuTIPanel.Location = new Point(MenuSistemaPanel.Width + 2, AcessoAoMenuPanel.Height);
            MenuTIPanel.BackColor = Color.Silver;
            this.Controls.Add(MenuTIPanel);
            MenuTIPanel.BringToFront();
            MenuTIPanel.Visible = true;
            #endregion

            #region CadastroTI 
            CadastroTIPanel = new Panel();
            CadastroTIPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CadastroTIPanel.Dock = DockStyle.Top;
            CadastroTIPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CadastroTIPanel.Name = "CadastroTIPanel";

            DivisoriaCadastroTIImagem = new PictureBox();
            DivisoriaCadastroTIImagem.Size = new Size(0, 4);
            DivisoriaCadastroTIImagem.Dock = DockStyle.Bottom;
            DivisoriaCadastroTIImagem.BackColor = Color.Silver;

            Divisoria2CadastroTIImagem = new PictureBox();
            Divisoria2CadastroTIImagem.Size = new Size(1, 2);
            Divisoria2CadastroTIImagem.Dock = DockStyle.Right;
            Divisoria2CadastroTIImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            CadastroTISetaLabel = new Label();
            CadastroTISetaLabel.AutoSize = false;
            CadastroTISetaLabel.Size = new Size(20, 59);
            CadastroTISetaLabel.Dock = DockStyle.Right;
            CadastroTISetaLabel.Text = "6";
            CadastroTISetaLabel.Font = new Font("Webdings", (float)12);
            CadastroTISetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            CadastroTISetaLabel.Click += new System.EventHandler(this.CadastroTISetaLabel_Click);
            CadastroTISetaLabel.MouseHover += new EventHandler(this.CadastrosTISetaPanel_MouseHover);
            CadastroTISetaLabel.Name = "CadastroTISetaLabel";
            CadastroTISetaLabel.ForeColor = Color.WhiteSmoke;

            CadastroTILabel = new Label();
            CadastroTILabel.AutoSize = false;
            CadastroTILabel.Size = new Size(_widthLabelMenuSistema, 0);
            CadastroTILabel.Dock = DockStyle.Fill;
            CadastroTILabel.Text = "Cadastros";
            CadastroTILabel.Font = CorFontepadraoLabel.Font;
            CadastroTILabel.ForeColor = Color.WhiteSmoke;
            CadastroTILabel.TextAlign = ContentAlignment.MiddleRight;
            CadastroTILabel.MouseHover += new EventHandler(this.CadastrosTIPanel_MouseHover);
            CadastroTILabel.Name = "CadastroTILabel";

            CadastroTIPanel.Controls.Add(CadastroTILabel);
            //CadastroTIPanel.Controls.Add(CadastroTIImagem);
            CadastroTIPanel.Controls.Add(Divisoria2CadastroTIImagem);
            CadastroTIPanel.Controls.Add(CadastroTISetaLabel);
            CadastroTIPanel.Controls.Add(DivisoriaCadastroTIImagem);

            MenuTIPanel.Controls.Add(CadastroTIPanel);
            #endregion            
        }

        private void FechaSubMenuTI()
        {
            FechaSubMenuCadastroTIPanel();

            if (MenuTIPanel != null)
            {
                MenuTIPanel.Visible = false;
                this.Controls.Remove(MenuTIPanel);
                MenuTIPanel.Dispose();
                MenuTIPanel = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuTI()
        {
            CadastroTILabel.Font = new Font(CadastroTILabel.Font, CadastroTILabel.Font.Style & ~FontStyle.Bold);
            CadastroTISetaLabel.Font = new Font(CadastroTISetaLabel.Font, CadastroTISetaLabel.Font.Style & ~FontStyle.Bold);

            CadastroTILabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CadastroTISetaLabel.BackColor = CadastroTILabel.BackColor;

            CadastroTILabel.ForeColor = Color.WhiteSmoke;
            CadastroTISetaLabel.ForeColor = Color.WhiteSmoke;

            CadastroTISetaLabel.Text = "6";
        }

        private void CadastrosTIPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuTI();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();
            FechaSubMenuChamadosPanel();
            FechaSubMenuSAC();
            FechaSubMenuCadastroTIPanel();

            if (((Control)sender).Name.Contains("Cadastro"))
            {
                CadastroTILabel.Font = new Font(CadastroTILabel.Font, FontStyle.Bold);
                CadastroTILabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastroTILabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void CadastrosTISetaPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuTI();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();
            FechaSubMenuChamadosPanel();
            FechaSubMenuSAC();
            FechaSubMenuCadastroTIPanel();

            if (((Control)sender).Name.Contains("Cadastro"))
            {
                CadastroTISetaLabel.Font = new Font(CadastroTISetaLabel.Font, FontStyle.Bold);
                CadastroTISetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                CadastroTISetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        #region Cadastro

        private void CadastroTISetaLabel_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu do RecursosHumanos
            CadastroTISetaLabel.Text = "3";
            CadastroTISetaLabel.ForeColor = Publicas._bordaEntrada;
            CadastroTILabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuCadastroTIPanel != null)
            {
                FechaSubMenuCadastroTIPanel();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuCadastroTIPanel = new Panel();
            SubMenuCadastroTIPanel.Size = new Size(145, _heidthMenuSistema * 6);
            SubMenuCadastroTIPanel.Location = new Point(MenuSistemaPanel.Width + MenuTIPanel.Width + 4, AcessoAoMenuPanel.Height);
            SubMenuCadastroTIPanel.BackColor = Color.Silver;
            this.Controls.Add(SubMenuCadastroTIPanel);
            SubMenuCadastroTIPanel.BringToFront();
            SubMenuCadastroTIPanel.Visible = true;
            #endregion

            #region Notificacoes
            NotificacoesPanel = new Panel();
            NotificacoesPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            NotificacoesPanel.Dock = DockStyle.Top;
            NotificacoesPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            NotificacoesPanel.Name = "NotificacoesPanel";

            DivisoriaNotificacoesImagem = new PictureBox();
            DivisoriaNotificacoesImagem.Size = new Size(0, 4);
            DivisoriaNotificacoesImagem.Dock = DockStyle.Bottom;
            DivisoriaNotificacoesImagem.BackColor = Color.Silver;

            NotificacoesLabel = new Label();
            NotificacoesLabel.AutoSize = false;
            NotificacoesLabel.Size = new Size(_widthLabelMenuSistema, 0);
            NotificacoesLabel.Dock = DockStyle.Fill;
            NotificacoesLabel.Text = "Notificações";
            NotificacoesLabel.Font = CorFontepadraoLabel.Font;
            NotificacoesLabel.ForeColor = Color.WhiteSmoke;
            NotificacoesLabel.TextAlign = ContentAlignment.MiddleRight;
            NotificacoesLabel.MouseHover += new EventHandler(this.SistemasTIPanel_MouseHover);
            NotificacoesLabel.Click += new System.EventHandler(this.NotificacoesPanel_Click);
            NotificacoesLabel.Name = "NotificacoesLabel";

            NotificacoesPanel.Controls.Add(NotificacoesLabel);
            NotificacoesPanel.Controls.Add(DivisoriaNotificacoesImagem);

            SubMenuCadastroTIPanel.Controls.Add(NotificacoesPanel);
            #endregion

            #region SalaReuniao
            SalaReuniaoPanel = new Panel();
            SalaReuniaoPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            SalaReuniaoPanel.Dock = DockStyle.Top;
            SalaReuniaoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            SalaReuniaoPanel.Name = "SalaReuniaoPanel";

            DivisoriaSalaReuniaoImagem = new PictureBox();
            DivisoriaSalaReuniaoImagem.Size = new Size(0, 4);
            DivisoriaSalaReuniaoImagem.Dock = DockStyle.Bottom;
            DivisoriaSalaReuniaoImagem.BackColor = Color.Silver;

            SalaReuniaoLabel = new Label();
            SalaReuniaoLabel.AutoSize = false;
            SalaReuniaoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            SalaReuniaoLabel.Dock = DockStyle.Fill;
            SalaReuniaoLabel.Text = "Sala de Reunião";
            SalaReuniaoLabel.Font = CorFontepadraoLabel.Font;
            SalaReuniaoLabel.ForeColor = Color.WhiteSmoke;
            SalaReuniaoLabel.TextAlign = ContentAlignment.MiddleRight;
            SalaReuniaoLabel.MouseHover += new EventHandler(this.SistemasTIPanel_MouseHover);
            SalaReuniaoLabel.Click += new System.EventHandler(this.SalaReuniaoPanel_Click);
            SalaReuniaoLabel.Name = "SalaReuniaoLabel";

            SalaReuniaoPanel.Controls.Add(SalaReuniaoLabel);
            SalaReuniaoPanel.Controls.Add(DivisoriaSalaReuniaoImagem);

            SubMenuCadastroTIPanel.Controls.Add(SalaReuniaoPanel);
            #endregion

            #region Empresas
            EmpresasPanel = new Panel();
            EmpresasPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            EmpresasPanel.Dock = DockStyle.Top;
            EmpresasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            EmpresasPanel.Name = "EmpresasPanel";

            DivisoriaEmpresasImagem = new PictureBox();
            DivisoriaEmpresasImagem.Size = new Size(0, 4);
            DivisoriaEmpresasImagem.Dock = DockStyle.Bottom;
            DivisoriaEmpresasImagem.BackColor = Color.Silver;

            EmpresasLabel = new Label();
            EmpresasLabel.AutoSize = false;
            EmpresasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            EmpresasLabel.Dock = DockStyle.Fill;
            EmpresasLabel.Text = "Empresas";
            EmpresasLabel.Font = CorFontepadraoLabel.Font;
            EmpresasLabel.ForeColor = Color.WhiteSmoke;
            EmpresasLabel.TextAlign = ContentAlignment.MiddleRight;
            EmpresasLabel.MouseHover += new EventHandler(this.SistemasTIPanel_MouseHover);
            EmpresasLabel.Click += new System.EventHandler(this.EmpresasPanel_Click);
            EmpresasLabel.Name = "EmpresasLabel";

            EmpresasPanel.Controls.Add(EmpresasLabel);
            EmpresasPanel.Controls.Add(DivisoriaEmpresasImagem);

            SubMenuCadastroTIPanel.Controls.Add(EmpresasPanel);
            #endregion

            #region Parametros
            ParametrosPanel = new Panel();
            ParametrosPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ParametrosPanel.Dock = DockStyle.Top;
            ParametrosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ParametrosPanel.Name = "ParametrosPanel";

            DivisoriaParametrosImagem = new PictureBox();
            DivisoriaParametrosImagem.Size = new Size(0, 4);
            DivisoriaParametrosImagem.Dock = DockStyle.Bottom;
            DivisoriaParametrosImagem.BackColor = Color.Silver;

            ParametrosLabel = new Label();
            ParametrosLabel.AutoSize = false;
            ParametrosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ParametrosLabel.Dock = DockStyle.Fill;
            ParametrosLabel.Text = "Parâmetros";
            ParametrosLabel.Font = CorFontepadraoLabel.Font;
            ParametrosLabel.ForeColor = Color.WhiteSmoke;
            ParametrosLabel.TextAlign = ContentAlignment.MiddleRight;
            ParametrosLabel.MouseHover += new EventHandler(this.SistemasTIPanel_MouseHover);
            ParametrosLabel.Click += new System.EventHandler(this.ParametrosPanel_Click);
            ParametrosLabel.Name = "ParametrosLabel";

            ParametrosPanel.Controls.Add(ParametrosLabel);
            ParametrosPanel.Controls.Add(DivisoriaParametrosImagem);

            SubMenuCadastroTIPanel.Controls.Add(ParametrosPanel);
            #endregion

            #region Usuarios
            UsuariosPanel = new Panel();
            UsuariosPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            UsuariosPanel.Dock = DockStyle.Top;
            UsuariosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            UsuariosPanel.Name = "UsuariosPanel";

            DivisoriaUsuariosImagem = new PictureBox();
            DivisoriaUsuariosImagem.Size = new Size(0, 4);
            DivisoriaUsuariosImagem.Dock = DockStyle.Bottom;
            DivisoriaUsuariosImagem.BackColor = Color.Silver;

            UsuariosLabel = new Label();
            UsuariosLabel.AutoSize = false;
            UsuariosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            UsuariosLabel.Dock = DockStyle.Fill;
            UsuariosLabel.Text = "Usuários";
            UsuariosLabel.Font = CorFontepadraoLabel.Font;
            UsuariosLabel.ForeColor = Color.WhiteSmoke;
            UsuariosLabel.TextAlign = ContentAlignment.MiddleRight;
            UsuariosLabel.MouseHover += new EventHandler(this.SistemasTIPanel_MouseHover);
            UsuariosLabel.Click += new System.EventHandler(this.UsuariosPanel_Click);
            UsuariosLabel.Name = "UsuariosLabel";

            UsuariosPanel.Controls.Add(UsuariosLabel);
            UsuariosPanel.Controls.Add(DivisoriaUsuariosImagem);

            SubMenuCadastroTIPanel.Controls.Add(UsuariosPanel);
            #endregion

            #region SistemasTI 
            SistemasTIPanel = new Panel();
            SistemasTIPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            SistemasTIPanel.Dock = DockStyle.Top;
            SistemasTIPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            SistemasTIPanel.Name = "SistemasTIPanel";

            DivisoriaSistemasTIImagem = new PictureBox();
            DivisoriaSistemasTIImagem.Size = new Size(0, 4);
            DivisoriaSistemasTIImagem.Dock = DockStyle.Bottom;
            DivisoriaSistemasTIImagem.BackColor = Color.Silver;

            Divisoria2SistemasTIImagem = new PictureBox();
            Divisoria2SistemasTIImagem.Size = new Size(1, 2);
            Divisoria2SistemasTIImagem.Dock = DockStyle.Right;
            Divisoria2SistemasTIImagem.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

            SistemasTISetaLabel = new Label();
            SistemasTISetaLabel.AutoSize = false;
            SistemasTISetaLabel.Size = new Size(20, 59);
            SistemasTISetaLabel.Dock = DockStyle.Right;
            SistemasTISetaLabel.Text = "6";
            SistemasTISetaLabel.Font = new Font("Webdings", (float)12);
            SistemasTISetaLabel.TextAlign = ContentAlignment.MiddleCenter;
            SistemasTISetaLabel.Click += new System.EventHandler(this.SistemasTISeta_Click);
            SistemasTISetaLabel.MouseHover += new EventHandler(this.SistemasTISetaPanel_MouseHover);
            SistemasTISetaLabel.Name = "SistemasTISetaLabel";
            SistemasTISetaLabel.ForeColor = Color.WhiteSmoke;

            SistemasTILabel = new Label();
            SistemasTILabel.AutoSize = false;
            SistemasTILabel.Size = new Size(_widthLabelMenuSistema, 0);
            SistemasTILabel.Dock = DockStyle.Fill;
            SistemasTILabel.Text = "Sistemas";
            SistemasTILabel.Font = CorFontepadraoLabel.Font;
            SistemasTILabel.ForeColor = Color.WhiteSmoke;
            SistemasTILabel.TextAlign = ContentAlignment.MiddleRight;
            SistemasTILabel.MouseHover += new EventHandler(this.SistemasTIPanel_MouseHover);
            SistemasTILabel.Name = "SistemasTILabel";

            SistemasTIPanel.Controls.Add(SistemasTILabel);
            //SistemasTIPanel.Controls.Add(SistemasTIImagem);
            SistemasTIPanel.Controls.Add(Divisoria2SistemasTIImagem);
            SistemasTIPanel.Controls.Add(SistemasTISetaLabel);
            SistemasTIPanel.Controls.Add(DivisoriaSistemasTIImagem);

            SubMenuCadastroTIPanel.Controls.Add(SistemasTIPanel);
            #endregion            
        }

        private void FechaSubMenuCadastroTIPanel()
        {
            FechaSubMenuSistemasTIPanel();

            if (SubMenuCadastroTIPanel != null)
            {
                SubMenuCadastroTIPanel.Visible = false;
                this.Controls.Remove(SubMenuCadastroTIPanel);
                SubMenuCadastroTIPanel.Dispose();
                SubMenuCadastroTIPanel = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuCadastroTI()
        {
            UsuariosLabel.Font = new Font(UsuariosLabel.Font, UsuariosLabel.Font.Style & ~FontStyle.Bold);
            EmpresasLabel.Font = new Font(EmpresasLabel.Font, EmpresasLabel.Font.Style & ~FontStyle.Bold);
            ParametrosLabel.Font = new Font(ParametrosLabel.Font, ParametrosLabel.Font.Style & ~FontStyle.Bold);
            SalaReuniaoLabel.Font = new Font(SalaReuniaoLabel.Font, SalaReuniaoLabel.Font.Style & ~FontStyle.Bold);
            NotificacoesLabel.Font = new Font(NotificacoesLabel.Font, NotificacoesLabel.Font.Style & ~FontStyle.Bold);
            SistemasTILabel.Font = new Font(SistemasTILabel.Font, SistemasTILabel.Font.Style & ~FontStyle.Bold);
            SistemasTISetaLabel.Font = new Font(SistemasTISetaLabel.Font, SistemasTISetaLabel.Font.Style & ~FontStyle.Bold);

            UsuariosLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            EmpresasLabel.BackColor = UsuariosLabel.BackColor;
            ParametrosLabel.BackColor = UsuariosLabel.BackColor;
            SalaReuniaoLabel.BackColor = UsuariosLabel.BackColor;
            NotificacoesLabel.BackColor = UsuariosLabel.BackColor;
            SistemasTILabel.BackColor = UsuariosLabel.BackColor;
            SistemasTISetaLabel.BackColor = UsuariosLabel.BackColor;

            UsuariosLabel.ForeColor = Color.WhiteSmoke;
            EmpresasLabel.ForeColor = Color.WhiteSmoke;
            ParametrosLabel.ForeColor = Color.WhiteSmoke;
            SalaReuniaoLabel.ForeColor = Color.WhiteSmoke;
            NotificacoesLabel.ForeColor = Color.WhiteSmoke;
            SistemasTILabel.ForeColor = Color.WhiteSmoke;
            SistemasTISetaLabel.ForeColor = Color.WhiteSmoke;

            SistemasTISetaLabel.Text = "6";
        }

        private void SistemasTIPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuCadastroTI();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();
            FechaSubMenuChamadosPanel();
            FechaSubMenuSAC();
            FechaSubMenuSistemasTIPanel();

            if (((Control)sender).Name.Contains("Usuario"))
            {
                UsuariosLabel.Font = new Font(UsuariosLabel.Font, FontStyle.Bold);
                UsuariosLabel.ForeColor = Publicas._fonteBotaoFocado;
                UsuariosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Empresas"))
            {
                EmpresasLabel.Font = new Font(EmpresasLabel.Font, FontStyle.Bold);
                EmpresasLabel.ForeColor = Publicas._fonteBotaoFocado;
                EmpresasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Parametros"))
            {
                ParametrosLabel.Font = new Font(ParametrosLabel.Font, FontStyle.Bold);
                ParametrosLabel.ForeColor = Publicas._fonteBotaoFocado;
                ParametrosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("SalaReuniao"))
            {
                SalaReuniaoLabel.Font = new Font(SalaReuniaoLabel.Font, FontStyle.Bold);
                SalaReuniaoLabel.ForeColor = Publicas._fonteBotaoFocado;
                SalaReuniaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Sistemas"))
            {
                SistemasTILabel.Font = new Font(SistemasTILabel.Font, FontStyle.Bold);
                SistemasTILabel.ForeColor = Publicas._fonteBotaoFocado;
                SistemasTILabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Notificacoes"))
            {
                NotificacoesLabel.Font = new Font(NotificacoesLabel.Font, FontStyle.Bold);
                NotificacoesLabel.ForeColor = Publicas._fonteBotaoFocado;
                NotificacoesLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void SistemasTISetaPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuCadastroTI();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();
            FechaSubMenuChamadosPanel();
            FechaSubMenuSAC();
            FechaSubMenuSistemasTIPanel();

            if (((Control)sender).Name.Contains("Usuario"))
            {
                UsuariosLabel.Font = new Font(UsuariosLabel.Font, FontStyle.Bold);
                UsuariosLabel.ForeColor = Publicas._fonteBotaoFocado;
                UsuariosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Empresas"))
            {
                EmpresasLabel.Font = new Font(EmpresasLabel.Font, FontStyle.Bold);
                EmpresasLabel.ForeColor = Publicas._fonteBotaoFocado;
                EmpresasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Parametros"))
            {
                ParametrosLabel.Font = new Font(ParametrosLabel.Font, FontStyle.Bold);
                ParametrosLabel.ForeColor = Publicas._fonteBotaoFocado;
                ParametrosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("SalaReuniao"))
            {
                SalaReuniaoLabel.Font = new Font(SalaReuniaoLabel.Font, FontStyle.Bold);
                SalaReuniaoLabel.ForeColor = Publicas._fonteBotaoFocado;
                SalaReuniaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Sistemas"))
            {
                SistemasTISetaLabel.Font = new Font(SistemasTISetaLabel.Font, FontStyle.Bold);
                SistemasTISetaLabel.ForeColor = Publicas._fonteBotaoFocado;
                SistemasTISetaLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Notificacoes"))
            {
                NotificacoesLabel.Font = new Font(NotificacoesLabel.Font, FontStyle.Bold);
                NotificacoesLabel.ForeColor = Publicas._fonteBotaoFocado;
                NotificacoesLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void UsuariosPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaTI;
            new Usuarios().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void ParametrosPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaTI;
            new Cadastros.Parametros().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void EmpresasPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaTI;
            new Cadastros.Empresas().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void SalaReuniaoPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaTI;
            new Cadastros.SalaDeReuniao().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void NotificacoesPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaTI;
            new Notificacoes.Cadastro().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        #region Sistemas
        private void SistemasTISeta_Click(object sender, EventArgs e)
        {
            // abrir o SubMenu do RecursosHumanos
            SistemasTISetaLabel.Text = "3";
            SistemasTISetaLabel.ForeColor = Publicas._bordaEntrada;
            SistemasTILabel.ForeColor = Publicas._bordaEntrada;

            if (SubMenuSistemasTIPanel != null)
            {
                FechaSubMenuCadastroTIPanel();
                return;
            }

            #region Cria estrutura 

            // Menu de fundo (onde agrupa os demais itens)
            SubMenuSistemasTIPanel = new Panel();
            SubMenuSistemasTIPanel.Size = new Size(145, _heidthMenuSistema * 3);
            SubMenuSistemasTIPanel.Location = new Point(MenuSistemaPanel.Width + MenuTIPanel.Width + SubMenuCadastroTIPanel.Width + 6, AcessoAoMenuPanel.Height);
            SubMenuSistemasTIPanel.BackColor = Color.Silver;
            this.Controls.Add(SubMenuSistemasTIPanel);
            SubMenuSistemasTIPanel.BringToFront();
            SubMenuSistemasTIPanel.Visible = true;
            #endregion

            #region CategoriasTI
            CategoriasTIPanel = new Panel();
            CategoriasTIPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            CategoriasTIPanel.Dock = DockStyle.Top;
            CategoriasTIPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            CategoriasTIPanel.Name = "CategoriasTIPanel";

            DivisoriaCategoriasTIImagem = new PictureBox();
            DivisoriaCategoriasTIImagem.Size = new Size(0, 4);
            DivisoriaCategoriasTIImagem.Dock = DockStyle.Bottom;
            DivisoriaCategoriasTIImagem.BackColor = Color.Silver;

            CategoriasTILabel = new Label();
            CategoriasTILabel.AutoSize = false;
            CategoriasTILabel.Size = new Size(_widthLabelMenuSistema, 0);
            CategoriasTILabel.Dock = DockStyle.Fill;
            CategoriasTILabel.Text = "Categorias";
            CategoriasTILabel.Font = CorFontepadraoLabel.Font;
            CategoriasTILabel.ForeColor = Color.WhiteSmoke;
            CategoriasTILabel.TextAlign = ContentAlignment.MiddleRight;
            CategoriasTILabel.MouseHover += new EventHandler(this.CategoriasTIPanel_MouseHover);
            CategoriasTILabel.Click += new System.EventHandler(this.CategoriasTIPanel_Click);
            CategoriasTILabel.Name = "CategoriasTILabel";

            CategoriasTIPanel.Controls.Add(CategoriasTILabel);
            CategoriasTIPanel.Controls.Add(DivisoriaCategoriasTIImagem);

            SubMenuSistemasTIPanel.Controls.Add(CategoriasTIPanel);
            #endregion

            #region ModulosTI
            ModulosTIPanel = new Panel();
            ModulosTIPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            ModulosTIPanel.Dock = DockStyle.Top;
            ModulosTIPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ModulosTIPanel.Name = "ModulosTIPanel";

            DivisoriaModulosTIImagem = new PictureBox();
            DivisoriaModulosTIImagem.Size = new Size(0, 4);
            DivisoriaModulosTIImagem.Dock = DockStyle.Bottom;
            DivisoriaModulosTIImagem.BackColor = Color.Silver;

            ModulosTILabel = new Label();
            ModulosTILabel.AutoSize = false;
            ModulosTILabel.Size = new Size(_widthLabelMenuSistema, 0);
            ModulosTILabel.Dock = DockStyle.Fill;
            ModulosTILabel.Text = "Módulos";
            ModulosTILabel.Font = CorFontepadraoLabel.Font;
            ModulosTILabel.ForeColor = Color.WhiteSmoke;
            ModulosTILabel.TextAlign = ContentAlignment.MiddleRight;
            ModulosTILabel.MouseHover += new EventHandler(this.CategoriasTIPanel_MouseHover);
            ModulosTILabel.Click += new System.EventHandler(this.ModulosTIPanel_Click);
            ModulosTILabel.Name = "ModulosTILabel";

            ModulosTIPanel.Controls.Add(ModulosTILabel);
            ModulosTIPanel.Controls.Add(DivisoriaModulosTIImagem);

            SubMenuSistemasTIPanel.Controls.Add(ModulosTIPanel);
            #endregion

            #region TelasTI
            TelasTIPanel = new Panel();
            TelasTIPanel.Size = new Size(_widthMenuPadrao, _heidthMenuSistema);
            TelasTIPanel.Dock = DockStyle.Top;
            TelasTIPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            TelasTIPanel.Name = "TelasTIPanel";

            DivisoriaTelasTIImagem = new PictureBox();
            DivisoriaTelasTIImagem.Size = new Size(0, 4);
            DivisoriaTelasTIImagem.Dock = DockStyle.Bottom;
            DivisoriaTelasTIImagem.BackColor = Color.Silver;

            TelasTILabel = new Label();
            TelasTILabel.AutoSize = false;
            TelasTILabel.Size = new Size(_widthLabelMenuSistema, 0);
            TelasTILabel.Dock = DockStyle.Fill;
            TelasTILabel.Text = "Telas";
            TelasTILabel.Font = CorFontepadraoLabel.Font;
            TelasTILabel.ForeColor = Color.WhiteSmoke;
            TelasTILabel.TextAlign = ContentAlignment.MiddleRight;
            TelasTILabel.MouseHover += new EventHandler(this.CategoriasTIPanel_MouseHover);
            TelasTILabel.Click += new System.EventHandler(this.TelaTIPanel_Click);
            TelasTILabel.Name = "TelasTILabel";

            TelasTIPanel.Controls.Add(TelasTILabel);
            TelasTIPanel.Controls.Add(DivisoriaTelasTIImagem);

            SubMenuSistemasTIPanel.Controls.Add(TelasTIPanel);
            #endregion
        }

        private void FechaSubMenuSistemasTIPanel()
        {

            if (SubMenuSistemasTIPanel != null)
            {
                SubMenuSistemasTIPanel.Visible = false;
                this.Controls.Remove(SubMenuSistemasTIPanel);
                SubMenuSistemasTIPanel.Dispose();
                SubMenuSistemasTIPanel = null;
            }
        }

        private void MudaSelecaoDeCoresSubMenuSistemasTI()
        {
            CategoriasTILabel.Font = new Font(CategoriasTILabel.Font, CategoriasTILabel.Font.Style & ~FontStyle.Bold);
            ModulosTILabel.Font = new Font(ModulosTILabel.Font, ModulosTILabel.Font.Style & ~FontStyle.Bold);
            TelasTILabel.Font = new Font(TelasTILabel.Font, TelasTILabel.Font.Style & ~FontStyle.Bold);
            
            CategoriasTILabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            ModulosTILabel.BackColor = CategoriasTILabel.BackColor;
            TelasTILabel.BackColor = CategoriasTILabel.BackColor;
            
            CategoriasTILabel.ForeColor = Color.WhiteSmoke;
            ModulosTILabel.ForeColor = Color.WhiteSmoke;
            TelasTILabel.ForeColor = Color.WhiteSmoke;            
        }

        private void CategoriasTIPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuSistemasTI();
            FechaSubMenuAvaliacaoDesempenho();
            FechaSubMenuBiblioteca();
            FechaSubMenuCorridas();
            FechaSubMenuCadastroJuridico();
            FechaSubMenuControladoria();
            FechaSubMenuChamadosPanel();
            FechaSubMenuSAC();

            if (((Control)sender).Name.Contains("Categorias"))
            {
                CategoriasTILabel.Font = new Font(CategoriasTILabel.Font, FontStyle.Bold);
                CategoriasTILabel.ForeColor = Publicas._fonteBotaoFocado;
                CategoriasTILabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Modulos"))
            {
                ModulosTILabel.Font = new Font(ModulosTILabel.Font, FontStyle.Bold);
                ModulosTILabel.ForeColor = Publicas._fonteBotaoFocado;
                ModulosTILabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Telas"))
            {
                TelasTILabel.Font = new Font(TelasTILabel.Font, FontStyle.Bold);
                TelasTILabel.ForeColor = Publicas._fonteBotaoFocado;
                TelasTILabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void CategoriasTIPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaTI;
            new Categorias().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void ModulosTIPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaTI;
            new Modulos().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        private void TelaTIPanel_Click(object sender, EventArgs e)
        {
            FechaMenuUsuario();
            FechaMenuSistema();
            MensagemSistema();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaTI;
            new Cadastros.Telas().ShowDialog();
            NomePadrao();
            //AtivaTimer(sender, e);
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region DashBoard
        private void BotaoDashboardLabel_MouseHover(object sender, EventArgs e)
        {
            BotaoDashboardLabel.Cursor = Cursors.Hand;
            BotaoDashboardLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
        }

        private void BotaoDashboardLabel_MouseLeave(object sender, EventArgs e)
        {
            BotaoDashboardLabel.Cursor = Cursors.Hand;
            BotaoDashboardLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
        }

        private void BotaoDashboardLabel_Click(object sender, EventArgs e)
        {
            filter = new GridDynamicFilter();
            metroColor = new GridMetroColors();
            colunaGrid = new GridColumnDescriptor();

            FechaSubMenuRecursosHumanos();
            FechaSubMenuJuridico();
            FechaSubMenuContabilidade();
            FechaSubMenuControladoria();
            FechaSubMenuAtendimento();
            FechaSubMenuTI();

            if (MenuSistemaPanel != null)
            {
                MenuSistemaPanel.Visible = false;
                this.Controls.Remove(MenuSistemaPanel);
                MenuSistemaPanel.Dispose();
                MenuSistemaPanel = null;
            }

            if (MenuUsuarioPanel != null)
            {
                MenuUsuarioPanel.Visible = false;
                this.Controls.Remove(MenuUsuarioPanel);
                MenuUsuarioPanel.Dispose();
                MenuUsuarioPanel = null;
            }

            MostraNotificacoesPanel.Visible = false;
                        
            if (DashBoardPanel.Width == 25)
            {

                DashBoardPanel.Size = new Size(this.Width - 5, 0);
                BotaoDashboardLabel.Text = "8";
                DashBordTabControl.Visible = true;

                #region Cria grid para o comunicado

                ComunicadoDashBoardPanel = new Panel();
                ComunicadoDashBoardPanel.Dock = DockStyle.Fill ;

                #region Campo para o filtro de comunicados
                FiltrosComunicadosDashBoardPanel = new Panel();
                FiltrosComunicadosDashBoardPanel.Size = new Size(0, 42);
                FiltrosComunicadosDashBoardPanel.Dock = DockStyle.Top;

                ListaComunicadosDashBoardLabel = new Label();
                ListaComunicadosDashBoardLabel.Font = new Font(dataHoraLabel.Font.FontFamily, (float)12);
                ListaComunicadosDashBoardLabel.ForeColor = Color.DarkBlue;
                ListaComunicadosDashBoardLabel.AutoSize = true;
                ListaComunicadosDashBoardLabel.TextAlign = ContentAlignment.MiddleRight;
                ListaComunicadosDashBoardLabel.Text = "Informe o Status";
                ListaComunicadosDashBoardLabel.Location = new Point(49, 9);                

                AnoComunicadosDashBoardLabel = new Label();
                AnoComunicadosDashBoardLabel.Font = new Font(dataHoraLabel.Font.FontFamily, (float)12);
                AnoComunicadosDashBoardLabel.ForeColor = Color.DarkBlue;
                AnoComunicadosDashBoardLabel.AutoSize = true;
                AnoComunicadosDashBoardLabel.TextAlign = ContentAlignment.MiddleRight;
                AnoComunicadosDashBoardLabel.Text = "Ano";
                AnoComunicadosDashBoardLabel.Location = new Point(430, 9);

                ImagemListaLabel = new Label();
                ImagemListaLabel.Font = new Font("Wingdings 3", (float)12);
                ImagemListaLabel.ForeColor = Color.DarkBlue;
                ImagemListaLabel.AutoSize = false;
                ImagemListaLabel.TextAlign = ContentAlignment.MiddleLeft;
                ImagemListaLabel.Text = "_";
                ImagemListaLabel.Location = new Point(200, 9);

                ImagemAnoLabel = new Label();
                ImagemAnoLabel.Font = new Font("Wingdings 3", (float)12);
                ImagemAnoLabel.ForeColor = Color.DarkBlue;
                ImagemAnoLabel.AutoSize = false;
                ImagemAnoLabel.TextAlign = ContentAlignment.MiddleLeft;
                ImagemAnoLabel.Text = "_";
                ImagemAnoLabel.Location = new Point(480, 9);

                MensagemComunicadosDashBoardLabel = new Label();
                MensagemComunicadosDashBoardLabel.Font = new Font(dataHoraLabel.Font.FontFamily, (float)12, FontStyle.Bold);
                MensagemComunicadosDashBoardLabel.ForeColor = Color.DarkOliveGreen;
                MensagemComunicadosDashBoardLabel.Dock = DockStyle.Right;
                MensagemComunicadosDashBoardLabel.AutoSize = false;
                MensagemComunicadosDashBoardLabel.TextAlign = ContentAlignment.MiddleRight;
                MensagemComunicadosDashBoardLabel.Size = new Size(233, 0);

                StatusComunicadosDashBoardComboBox = new ComboBoxAdv();
                StatusComunicadosDashBoardComboBox.FlatBorderColor = Color.DarkBlue;
                StatusComunicadosDashBoardComboBox.Font = new Font(dataHoraLabel.Font.FontFamily, (float)9.75);
                StatusComunicadosDashBoardComboBox.Size = new Size(188, 25);
                StatusComunicadosDashBoardComboBox.Location = new Point(228, 7);
                StatusComunicadosDashBoardComboBox.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
                StatusComunicadosDashBoardComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                StatusComunicadosDashBoardComboBox.SelectedIndexChanged += new System.EventHandler(StatusComunicadosDashBoardComboBox_SelectedIndexChanged);

                AnoComunicadosDashBoardComboBox = new ComboBoxAdv();
                AnoComunicadosDashBoardComboBox.FlatBorderColor = Color.DarkBlue;
                AnoComunicadosDashBoardComboBox.Font = new Font(dataHoraLabel.Font.FontFamily, (float)9.75);
                AnoComunicadosDashBoardComboBox.Size = new Size(81, 25);
                AnoComunicadosDashBoardComboBox.Location = new Point(503, 7);
                AnoComunicadosDashBoardComboBox.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
                AnoComunicadosDashBoardComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                AnoComunicadosDashBoardComboBox.SelectedIndexChanged += new System.EventHandler(this.AnoComunicadosDashBoardComboBox_SelectedIndexChanged);

                FiltrosComunicadosDashBoardPanel.Controls.Add(MensagemComunicadosDashBoardLabel);
                FiltrosComunicadosDashBoardPanel.Controls.Add(AnoComunicadosDashBoardComboBox);
                FiltrosComunicadosDashBoardPanel.Controls.Add(ImagemAnoLabel);
                FiltrosComunicadosDashBoardPanel.Controls.Add(AnoComunicadosDashBoardLabel);
                FiltrosComunicadosDashBoardPanel.Controls.Add(StatusComunicadosDashBoardComboBox);
                FiltrosComunicadosDashBoardPanel.Controls.Add(ImagemListaLabel);
                FiltrosComunicadosDashBoardPanel.Controls.Add(ListaComunicadosDashBoardLabel);

                #endregion

                #region cria o grid

                ComunidadosDashBoardGrid = new GridGroupingControl();
                filter.WireGrid(ComunidadosDashBoardGrid);

                ComunidadosDashBoardGrid.TableControlCellClick += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventHandler(ComunidadosDashBoardGrid_TableControlCellClick);
                ComunidadosDashBoardGrid.TableControlCurrentCellKeyUp += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlKeyEventHandler(ComunidadosDashBoardGrid_TableControlCurrentCellKeyUp);
                ComunidadosDashBoardGrid.TableControlCellDoubleClick += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventHandler(ComunidadosDashBoardGrid_TableControlCellDoubleClick);
                ComunidadosDashBoardGrid.ContextMenuStrip = comunicadosContextMenuStrip;

                ComunidadosDashBoardGrid.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
                ComunidadosDashBoardGrid.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.SetCurrent;
                ComunidadosDashBoardGrid.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
                ComunidadosDashBoardGrid.Font = new Font(dataHoraLabel.Font.FontFamily, (float)9);
                ComunidadosDashBoardGrid.ShowNavigationBar = true;
                ComunidadosDashBoardGrid.Dock = DockStyle.Fill;
                ComunidadosDashBoardGrid.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
                ComunidadosDashBoardGrid.ShowRowHeaders = false;
                ComunidadosDashBoardGrid.TopLevelGroupOptions.ShowCaption = false;
                ComunidadosDashBoardGrid.TableDescriptor.AllowEdit = false;
                ComunidadosDashBoardGrid.TableDescriptor.AllowNew = false;
                ComunidadosDashBoardGrid.TableDescriptor.AllowRemove = false;

                ComunidadosDashBoardGrid.SortIconPlacement = SortIconPlacement.Left;
                ComunidadosDashBoardGrid.TopLevelGroupOptions.ShowFilterBar = true;
                ComunidadosDashBoardGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
                ComunidadosDashBoardGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
                ComunidadosDashBoardGrid.RecordNavigationBar.Label = "Comunicados";
                ComunidadosDashBoardGrid.TableControl.CellToolTip.Active = true;
                ComunidadosDashBoardGrid.SetMetroStyle(metroColor);

                ComunidadosDashBoardGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
                ComunidadosDashBoardGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                ComunidadosDashBoardGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
                ComunidadosDashBoardGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

                #region Cria as colunas

                colunaGrid = new GridColumnDescriptor("Processo", "Processo", "Processo");
                colunaGrid.AllowFilter = true;
                colunaGrid.AllowSort = true;
                colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;

                colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = true;
                colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                colunaGrid.Appearance.AnyRecordFieldCell.TextColor = Color.DimGray;
                colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(dataHoraLabel.Font.FontFamily, (float)9, FontStyle.Bold));
                colunaGrid.Appearance.ColumnHeaderCell.AutoSize = true;
                colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                ComunidadosDashBoardGrid.TableDescriptor.Columns.Add(colunaGrid);

                colunaGrid = new GridColumnDescriptor("Status", "Status", "Status");
                colunaGrid.AllowFilter = true;
                colunaGrid.AllowSort = true;
                colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;
                colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = true;
                colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                colunaGrid.Appearance.ColumnHeaderCell.AutoSize = true;
                colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.ColumnHeaderCell.ShowButtons = GridShowButtons.Hide;
                ComunidadosDashBoardGrid.TableDescriptor.Columns.Add(colunaGrid);

                colunaGrid = new GridColumnDescriptor("Abertura", "Abertura", "Data de Abertura");
                colunaGrid.AllowFilter = true;
                colunaGrid.AllowSort = true;
                colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;
                colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = true;
                colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                colunaGrid.Appearance.AnyRecordFieldCell.Format = "g";
                //colunaGrid.Appearance.AnyRecordFieldCell.CellValueType = new GridPropertyTypeDefaultStyleCollectionEnumerator.DateTime;

                colunaGrid.Appearance.ColumnHeaderCell.AutoSize = true;
                colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.ColumnHeaderCell.ShowButtons = GridShowButtons.Hide;
                ComunidadosDashBoardGrid.TableDescriptor.Columns.Add(colunaGrid);

                colunaGrid = new GridColumnDescriptor("Solicitante", "Solicitante", "Solicitante");
                colunaGrid.AllowFilter = true;
                colunaGrid.AllowSort = true;
                colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;
                colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = true;
                colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                colunaGrid.Appearance.ColumnHeaderCell.AutoSize = true;
                colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.ColumnHeaderCell.ShowButtons = GridShowButtons.Hide;
                ComunidadosDashBoardGrid.TableDescriptor.Columns.Add(colunaGrid);

                colunaGrid = new GridColumnDescriptor("Empresa", "Empresa", "Empresa");
                colunaGrid.AllowFilter = true;
                colunaGrid.AllowSort = true;
                colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;
                colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = true;
                colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                colunaGrid.Appearance.ColumnHeaderCell.AutoSize = true;
                colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.ColumnHeaderCell.ShowButtons = GridShowButtons.Hide;
                ComunidadosDashBoardGrid.TableDescriptor.Columns.Add(colunaGrid);

                colunaGrid = new GridColumnDescriptor("Tipo", "Tipo", "Tipo");
                colunaGrid.AllowFilter = true;
                colunaGrid.AllowSort = true;
                colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;
                colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = true;
                colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                colunaGrid.Appearance.ColumnHeaderCell.AutoSize = true;
                colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.ColumnHeaderCell.ShowButtons = GridShowButtons.Hide;
                ComunidadosDashBoardGrid.TableDescriptor.Columns.Add(colunaGrid);

                colunaGrid = new GridColumnDescriptor("Autor", "Autor", "Autor");
                colunaGrid.AllowFilter = true;
                colunaGrid.AllowSort = true;
                colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;
                colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = true;
                colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                colunaGrid.Appearance.ColumnHeaderCell.AutoSize = true;
                colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                colunaGrid.Appearance.ColumnHeaderCell.ShowButtons = GridShowButtons.Hide;
                ComunidadosDashBoardGrid.TableDescriptor.Columns.Add(colunaGrid);
                ComunidadosDashBoardGrid.TableDescriptor.FrozenColumn = "Processo";
                ComunidadosDashBoardGrid.TableDescriptor.SortedColumns.Add("Abertura", ListSortDirection.Descending);

                #endregion

                #endregion

                ComunicadoDashBoardPanel.Controls.Add(ComunidadosDashBoardGrid);
                ComunicadoDashBoardPanel.Controls.Add(FiltrosComunicadosDashBoardPanel);

                ComunicadosTabPage.Controls.Add(ComunicadoDashBoardPanel);
                ComunidadosDashBoardGrid.BringToFront();
                FiltrosComunicadosDashBoardPanel.SendToBack();

                StatusComunicadosDashBoardComboBox.Items.AddRange(new object[] { "Novo", "Aprovados", "Recusados", "Finalizados", "Cancelados", "Todos" });
                StatusComunicadosDashBoardComboBox.SelectedIndex = 0;

                List<int> datas = new ComunicadoBO().Datas();

                foreach (var item in datas)
                {
                    AnoComunicadosDashBoardComboBox.Items.Add(item);
                }

                AnoComunicadosDashBoardComboBox.Items.Add("Todos");
                AnoComunicadosDashBoardComboBox.SelectedIndex = 0;

                #endregion

                #region Cria grid para o chamado

                #endregion

                #region Avaliação
                PraparaAvaliacao();
                #endregion

            }
            else
            {
                DashBoardPanel.Size = new Size(25, 0);
                BotaoDashboardLabel.Text = "7";
                DashBordTabControl.Visible = false;

                if (ComunicadoDashBoardPanel != null)
                {
                    ComunicadosTabPage.Controls.Remove(ComunicadoDashBoardPanel);
                    ComunicadoDashBoardPanel.Dispose();
                }
            }
        }
        #region Comunicado

        private void StatusComunicadosDashBoardComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Publicas._usuario != null)
                AnoComunicadosDashBoardComboBox_SelectedIndexChanged(sender, e);
        }
        
        private void AnoComunicadosDashBoardComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Publicas._anoSelecionadoComunicado = DateTime.Now.Year;
            _statusComunicadoSelecionado = Publicas.StatusComunicado.Todos;

            try
            {
                if (!AnoComunicadosDashBoardComboBox.Text.Contains("Todos"))
                    Publicas._anoSelecionadoComunicado = Convert.ToInt32(AnoComunicadosDashBoardComboBox.Text);
            }
            catch { }

            switch (StatusComunicadosDashBoardComboBox.SelectedIndex)
            {
                case 0:
                    _statusComunicadoSelecionado = Publicas.StatusComunicado.Novo;
                    break;
                case 1:
                    _statusComunicadoSelecionado = Publicas.StatusComunicado.Aprovado;
                    break;
                case 2:
                    _statusComunicadoSelecionado = Publicas.StatusComunicado.Reprovado;
                    break;
                case 3:
                    _statusComunicadoSelecionado = Publicas.StatusComunicado.Finalizado;
                    break;
                case 4:
                    _statusComunicadoSelecionado = Publicas.StatusComunicado.Cancelado;
                    break;
            }

            _atualizarComunicado = false;
            try
            {
                if (!_comunicadosEmPesquisa)
                {
                    comunicadoBackgroundWorker.RunWorkerAsync();
                    MensagemComunicadosDashBoardLabel.Text = "Pesquisando comunicados, aguarde... ";
                }
                else
                    _atualizarComunicado = true;
            }
            catch { _atualizarComunicado = true; }

        }

        private void comunicadoBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Publicas._usuario.AcessaJuridico)
            {
                _comunicadosEmPesquisa = true;

                try
                {
                    if (AnoComunicadosDashBoardComboBox.Text.Contains("Todos") && StatusComunicadosDashBoardComboBox.Text.Contains("Todos"))
                        _listaComunicados = new ComunicadoBO().Listar();
                    else
                        _listaComunicados = new ComunicadoBO().Listar(0, Convert.ToInt32(AnoComunicadosDashBoardComboBox.Text), _statusComunicadoSelecionado);
                }
                catch
                {
                }
            }
        }

        private void comunicadoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Publicas._usuario.AcessaJuridico)
            {
                //comunicadoBackgroundWorker
                mensagemLabel.Text = "Atualizando lista dos comunicados ...";
                mensagemLabel.Cursor = Cursors.WaitCursor;
                this.Refresh();

                //statusComunidadosComboBoxAdv_SelectedIndexChanged(sender, e);
                ComunidadosDashBoardGrid.DataSource = _listaComunicados;

                mensagemLabel.Cursor = Cursors.Default;
                mensagemLabel.Text = "";

                _comunicadosEmPesquisa = false;

                if (_atualizarComunicado)
                {
                    if (!_comunicadosEmPesquisa)
                    {
                        comunicadoBackgroundWorker.RunWorkerAsync();
                        MensagemComunicadosDashBoardLabel.Text = "Pesquisando comunicados, aguarde... ";
                    }
                }

                _atualizarComunicado = false;
                MensagemComunicadosDashBoardLabel.Text = string.Empty;
                Refresh();
                comunicadoTimer.Start();
            }

            try
            {
                ComunicadosTabPage.TabVisible = Publicas._usuario.AcessaJuridico;
            }
            catch { }
        }

        private void comunicadoTimer_Tick(object sender, EventArgs e)
        {
            if (Publicas._usuario == null || !Publicas._usuario.AcessaJuridico)
                return;

            comunicadoTimer.Stop();

            try
            {
                if (!_comunicadosEmPesquisa)
                {
                    comunicadoBackgroundWorker.RunWorkerAsync();
                    MensagemComunicadosDashBoardLabel.Text = "Pesquisando comunicados, aguarde... ";
                }
            }
            catch { }
        }

        private void ComunidadosDashBoardGrid_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                _rowIndexComunicado = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.ComunidadosDashBoardGrid.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        if ((Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Alterado ||
                            (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Novo)
                        {
                            alterarToolStripMenuItem.Text = "Alterar";
                            alterarToolStripMenuItem.Enabled = Publicas._usuario.PermiteAlterarComunicado;
                        }
                        else
                        {
                            alterarToolStripMenuItem.Text = "Consultar";
                            alterarToolStripMenuItem.Enabled = true;
                        }

                        aprovarToolStripMenuItem.Enabled = ((Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Alterado ||
                                                            (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Novo) &&
                                                            Publicas._usuario.PermiteAprovarComunicado;

                        cancelarToolStripMenuItem.Enabled = ((Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Alterado ||
                                                             (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Novo ||
                                                             (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Aprovado) &&
                                                             Publicas._usuario.PermiteCancelarComunicado;

                        finalizarToolStripMenuItem1.Enabled = (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Aprovado &&
                                                                Publicas._usuario.PermiteFinalizarComunicado;

                        reprovarToolStripMenuItem.Enabled = ((Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Alterado ||
                                                             (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Novo) &&
                                                             Publicas._usuario.PermiteReprovarComunicado;
                    }
                }
            }
            catch { }
        }

        private void ComunidadosDashBoardGrid_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                _rowIndexComunicado = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = ComunidadosDashBoardGrid.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        if ((Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Alterado ||
                            (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Novo)
                        {
                            alterarToolStripMenuItem.Text = "Alterar";
                            alterarToolStripMenuItem.Enabled = Publicas._usuario.PermiteAlterarComunicado;
                        }
                        else
                        {
                            alterarToolStripMenuItem.Text = "Consultar";
                            alterarToolStripMenuItem.Enabled = true;
                        }

                        aprovarToolStripMenuItem.Enabled = ((Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Alterado ||
                                                            (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Novo) &&
                                                            Publicas._usuario.PermiteAprovarComunicado;

                        cancelarToolStripMenuItem.Enabled = ((Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Alterado ||
                                                             (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Novo ||
                                                             (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Aprovado) &&
                                                             Publicas._usuario.PermiteCancelarComunicado;

                        finalizarToolStripMenuItem1.Enabled = (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Aprovado &&
                                                                Publicas._usuario.PermiteFinalizarComunicado;

                        reprovarToolStripMenuItem.Enabled = ((Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Alterado ||
                                                             (Publicas.StatusComunicado)dr["Status"] == Publicas.StatusComunicado.Novo) &&
                                                             Publicas._usuario.PermiteReprovarComunicado;
                    }
                }
            }
            catch { }
        }

        private void ComunidadosDashBoardGrid_TableControlCellDoubleClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.ComunidadosDashBoardGrid.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    FecharDashBoard();
                    MensagemSistema();
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Alterado;
                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    new Juridico.Comunicado().ShowDialog();
                    NomePadrao();
                    Publicas._idComunicado = 0;
                }

            }
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = ComunidadosDashBoardGrid.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    FecharDashBoard();
                    MensagemSistema();
                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Alterado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();

                    _tela.tituloLabel.Text = (alterarToolStripMenuItem.Text.Contains("Consultar") ? "Consultar" : "Alterar") + " Comunicado";
                    _tela.statusButton.Text = "Gravar";
                    _tela.ShowDialog();

                    NomePadrao();

                    Publicas._idComunicado = 0;
                }

            }
        }

        private void reenviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = ComunidadosDashBoardGrid.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    FecharDashBoard();
                    MensagemSistema();
                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Alterado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();
                    _tela.tituloLabel.Text = "Reeviar e-mail sobre o Comunicado";
                    _tela.gravarButton.Visible = false;
                    _tela.statusButton.Text = "&Enviar Email";
                    _tela.statusButton.Location = new Point(_tela.gravarButton.Left, _tela.gravarButton.Top);
                    _tela.statusButton.Visible = true;
                    _tela.ShowDialog();

                    NomePadrao();
                    Publicas._idComunicado = 0;
                }
            }
        }

        private void reprovarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = ComunidadosDashBoardGrid.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    FecharDashBoard();
                    MensagemSistema();
                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Reprovado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();
                    _tela.tituloLabel.Text = "Reprovar Comunicado";
                    _tela.gravarButton.Visible = false;
                    _tela.statusButton.Text = "&Reprovar";
                    _tela.statusButton.Location = new Point(_tela.gravarButton.Left, _tela.gravarButton.Top);
                    _tela.statusButton.Visible = true;
                    _tela.ShowDialog();
                    NomePadrao();

                    Publicas._idComunicado = 0;
                }
            }
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = ComunidadosDashBoardGrid.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    FecharDashBoard();
                    MensagemSistema();
                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Cancelado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();
                    _tela.tituloLabel.Text = "Cancelar Comunicado";
                    _tela.gravarButton.Visible = false;
                    _tela.statusButton.Text = "&Cancelar";
                    _tela.statusButton.Location = new Point(_tela.gravarButton.Left, _tela.gravarButton.Top);
                    _tela.statusButton.Visible = true;
                    _tela.ShowDialog();
                    NomePadrao();

                    Publicas._idComunicado = 0;
                }
            }
        }

        private void finalizarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = ComunidadosDashBoardGrid.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    FecharDashBoard();
                    MensagemSistema();
                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Finalizado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();
                    _tela.tituloLabel.Text = "Finalizar Comunicado";
                    _tela.gravarButton.Visible = false;
                    _tela.statusButton.Text = "&Finalizar";
                    _tela.statusButton.Location = new Point(_tela.gravarButton.Left, _tela.gravarButton.Top);
                    _tela.statusButton.Visible = true;
                    _tela.ShowDialog();
                    NomePadrao();

                    Publicas._idComunicado = 0;
                }
            }
        }

        private void aprovarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = ComunidadosDashBoardGrid.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    FecharDashBoard();
                    MensagemSistema();
                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Aprovado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();
                    _tela.tituloLabel.Text = "Aprovar Comunicado";
                    _tela.gravarButton.Visible = false;
                    _tela.statusButton.Text = "&Aprovar";
                    _tela.statusButton.Location = new Point(_tela.gravarButton.Left, _tela.gravarButton.Top);
                    _tela.statusButton.Visible = true;
                    _tela.ShowDialog();
                    NomePadrao();

                    Publicas._idComunicado = 0;
                }
            }
        }

        private void FixarDashBoardPictureBox_Click(object sender, EventArgs e)
        {
            _fixaDashBoard = false;
            FixarDashBoardPictureBox.Visible = false;
            LiberarDashBoardPictureBox.Visible = true;
        }

        private void LiberarDashBoardPictureBox_Click(object sender, EventArgs e)
        {
            _fixaDashBoard = true;

            FixarDashBoardPictureBox.Visible = true;
            LiberarDashBoardPictureBox.Visible = false;
        }

        private void FixarDashBoardPictureBox_MouseHover(object sender, EventArgs e)
        {
            FixarDashBoardPictureBox.Cursor = Cursors.Hand;
            FixarDashBoardPictureBox.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
        }

        private void FixarDashBoardPictureBox_MouseLeave(object sender, EventArgs e)
        {
            FixarDashBoardPictureBox.Cursor = Cursors.Default;
            FixarDashBoardPictureBox.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
        }

        private void LiberarDashBoardPictureBox_MouseHover(object sender, EventArgs e)
        {
            LiberarDashBoardPictureBox.Cursor = Cursors.Hand;
            LiberarDashBoardPictureBox.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
        }

        private void LiberarDashBoardPictureBox_MouseLeave(object sender, EventArgs e)
        {
            LiberarDashBoardPictureBox.Cursor = Cursors.Default;
            LiberarDashBoardPictureBox.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
        }
        #endregion

        #region Avaliacao
        private void PraparaAvaliacao()
        {
            int qtdGraficosVisiveis = 0;
            DashBordTabControl.Refresh();
            int tamanho = (int)((DashBoardPanel.Width - 65) / 3);
            EmAndamentoPanel.Size = new Size(tamanho, EmAndamentoPanel.Height);
            NaoIniciadoPanel.Size = new Size(tamanho, EmAndamentoPanel.Height);
            FinalizadosPanel.Size = new Size(tamanho, EmAndamentoPanel.Height);

            #region Grid
            filter.WireGrid(this.AvaliacaoEmAndamentoGrid);

            AvaliacaoEmAndamentoGrid.SortIconPlacement = SortIconPlacement.Left;
            AvaliacaoEmAndamentoGrid.TopLevelGroupOptions.ShowFilterBar = true;
            AvaliacaoEmAndamentoGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            AvaliacaoEmAndamentoGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            AvaliacaoEmAndamentoGrid.RecordNavigationBar.Label = "Colaboradores";
            AvaliacaoEmAndamentoGrid.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < AvaliacaoEmAndamentoGrid.TableDescriptor.Columns.Count; i++)
            {
                AvaliacaoEmAndamentoGrid.TableDescriptor.Columns[i].AllowFilter = true;
                AvaliacaoEmAndamentoGrid.TableDescriptor.Columns[i].AllowSort = true;
                AvaliacaoEmAndamentoGrid.TableDescriptor.Columns[i].ReadOnly = false;
                AvaliacaoEmAndamentoGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                AvaliacaoEmAndamentoGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                AvaliacaoEmAndamentoGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            AvaliacaoEmAndamentoGrid.SetMetroStyle(metroColor);
            AvaliacaoEmAndamentoGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            AvaliacaoEmAndamentoGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            AvaliacaoEmAndamentoGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            AvaliacaoEmAndamentoGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            filter.WireGrid(this.AvaliacaoNaoIniciadaGrid);

            AvaliacaoNaoIniciadaGrid.SortIconPlacement = SortIconPlacement.Left;
            AvaliacaoNaoIniciadaGrid.TopLevelGroupOptions.ShowFilterBar = true;
            AvaliacaoNaoIniciadaGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            AvaliacaoNaoIniciadaGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            AvaliacaoNaoIniciadaGrid.RecordNavigationBar.Label = "Colaboradores";
            AvaliacaoNaoIniciadaGrid.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < AvaliacaoNaoIniciadaGrid.TableDescriptor.Columns.Count; i++)
            {
                AvaliacaoNaoIniciadaGrid.TableDescriptor.Columns[i].AllowFilter = true;
                AvaliacaoNaoIniciadaGrid.TableDescriptor.Columns[i].AllowSort = true;
                AvaliacaoNaoIniciadaGrid.TableDescriptor.Columns[i].ReadOnly = false;
                AvaliacaoNaoIniciadaGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                AvaliacaoNaoIniciadaGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                AvaliacaoNaoIniciadaGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            AvaliacaoNaoIniciadaGrid.SetMetroStyle(metroColor);
            AvaliacaoNaoIniciadaGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            AvaliacaoNaoIniciadaGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            AvaliacaoNaoIniciadaGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            AvaliacaoNaoIniciadaGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            filter.WireGrid(this.AvaliacaoFinalizadasGrid);

            AvaliacaoFinalizadasGrid.SortIconPlacement = SortIconPlacement.Left;
            AvaliacaoFinalizadasGrid.TopLevelGroupOptions.ShowFilterBar = true;
            AvaliacaoFinalizadasGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            AvaliacaoFinalizadasGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            AvaliacaoFinalizadasGrid.RecordNavigationBar.Label = "Colaboradores";
            AvaliacaoFinalizadasGrid.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < AvaliacaoFinalizadasGrid.TableDescriptor.Columns.Count; i++)
            {
                AvaliacaoFinalizadasGrid.TableDescriptor.Columns[i].AllowFilter = true;
                AvaliacaoFinalizadasGrid.TableDescriptor.Columns[i].AllowSort = true;
                AvaliacaoFinalizadasGrid.TableDescriptor.Columns[i].ReadOnly = false;
                AvaliacaoFinalizadasGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                AvaliacaoFinalizadasGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                AvaliacaoFinalizadasGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            AvaliacaoFinalizadasGrid.SetMetroStyle(metroColor);
            AvaliacaoFinalizadasGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            AvaliacaoFinalizadasGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            AvaliacaoFinalizadasGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            AvaliacaoFinalizadasGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            filter.WireGrid(this.rankingGridGroupingControl);

            rankingGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            rankingGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            rankingGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            rankingGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            rankingGridGroupingControl.RecordNavigationBar.Label = "Colaboradores";
            rankingGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < rankingGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                rankingGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                rankingGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                rankingGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                rankingGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                rankingGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                rankingGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            rankingGridGroupingControl.SetMetroStyle(metroColor);
            rankingGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            rankingGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            rankingGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            rankingGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            #endregion

            if (!_mudouVisualizacaoAndamento)
                andamentoButton_Click(this, new EventArgs());

            rankingGridGroupingControl.DataSource = _listaAvaliacoesNotas.Where(w => w.ReferenciaFormatada == ReferenciaAvaliacaoComboBox.Text).ToList();

            #region prepara os graficos pizza

            double finalizados = 0;
            double iniciados = 0;
            double naoIniciados = 0;
            double todos = 0;

            chartControl1.Series.Clear();
            chartControl2.Series.Clear();
            chartControl3.Series.Clear();
            chartControl4.Series.Clear();
            chartControl5.Series.Clear();
            chartControl6.Series.Clear();
            chartControl7.Series.Clear();
            chartControl8.Series.Clear();
            chartControl9.Series.Clear();
            chartControl10.Series.Clear();
            chartControl11.Series.Clear();

            #region Grafico EOVG Dutra

            ChartSeries _serie1 = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 2).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 2).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 2).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 2).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serie1.Points.Add(0, naoIniciados);
            _serie1.Points.Add(1, iniciados);
            _serie1.Points.Add(2, finalizados);
            _serie1.Points.Add(3, todos);
            _serie1.EnableStyles = true;
            _serie1.PointsToolTipFormat = "{2}";

            try
            {
                _serie1.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serie1.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serie1.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serie1.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoEOVGDutraPanel.Visible = todos != 0;
            chartControl1.Series.Add(_serie1);
            chartControl1.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            #region Grafico EOVG Lavras

            ChartSeries _serieLavras = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 3).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 3).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 3).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 3).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serieLavras.Points.Add(0, naoIniciados);
            _serieLavras.Points.Add(1, iniciados);
            _serieLavras.Points.Add(2, finalizados);
            _serieLavras.Points.Add(3, todos);
            _serieLavras.EnableStyles = true;
            _serieLavras.PointsToolTipFormat = "{2}";

            try
            {
                _serieLavras.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieLavras.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieLavras.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieLavras.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoEOVGLavrasPanel.Visible = todos != 0;
            chartControl10.Series.Add(_serieLavras);
            chartControl10.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            #region Grafico ABC

            ChartSeries _serieABC = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 4).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 4).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 4).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 4).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serieABC.Points.Add(0, naoIniciados);
            _serieABC.Points.Add(1, iniciados);
            _serieABC.Points.Add(2, finalizados);
            _serieABC.Points.Add(3, todos);
            _serieABC.EnableStyles = true;
            _serieABC.PointsToolTipFormat = "{2}";

            try
            {
                _serieABC.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieABC.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieABC.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieABC.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoABCPanel.Visible = todos != 0;
            chartControl2.Series.Add(_serieABC);
            chartControl2.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            #region Grafico Rapido

            ChartSeries _serieRapido = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 5).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 5).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 5).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 5).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serieRapido.Points.Add(0, naoIniciados);
            _serieRapido.Points.Add(1, iniciados);
            _serieRapido.Points.Add(2, finalizados);
            _serieRapido.Points.Add(3, todos);
            _serieRapido.EnableStyles = true;
            _serieRapido.PointsToolTipFormat = "{2}";

            try
            {
                _serieRapido.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieRapido.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieRapido.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieRapido.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoRapidoPanel.Visible = todos != 0;
            chartControl3.Series.Add(_serieRapido);
            chartControl3.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            #region Grafico Cisne

            ChartSeries _serieCisne = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 6).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 6).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 6).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 6).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serieCisne.Points.Add(0, naoIniciados);
            _serieCisne.Points.Add(1, iniciados);
            _serieCisne.Points.Add(2, finalizados);
            _serieCisne.Points.Add(3, todos);
            _serieCisne.EnableStyles = true;
            _serieCisne.PointsToolTipFormat = "{2}";

            try
            {
                _serieCisne.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieCisne.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieCisne.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieCisne.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoCisnePanel.Visible = todos != 0;
            chartControl4.Series.Add(_serieCisne);
            chartControl4.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            #region Grafico NIFF

            ChartSeries _serieNiff = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 1).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 1).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 1).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 1).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serieNiff.Points.Add(0, naoIniciados);
            _serieNiff.Points.Add(1, iniciados);
            _serieNiff.Points.Add(2, finalizados);
            _serieNiff.Points.Add(3, todos);
            _serieNiff.EnableStyles = true;
            _serieNiff.PointsToolTipFormat = "{2}";

            try
            {
                _serieNiff.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieNiff.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieNiff.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieNiff.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoNiffPanel.Visible = todos != 0;
            chartControl5.Series.Add(_serieNiff);
            chartControl5.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            #region Grafico Aruja

            ChartSeries _serieAruja = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 7).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 7).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 7).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 7).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serieAruja.Points.Add(0, naoIniciados);
            _serieAruja.Points.Add(1, iniciados);
            _serieAruja.Points.Add(2, finalizados);
            _serieAruja.Points.Add(3, todos);
            _serieAruja.EnableStyles = true;
            _serieAruja.PointsToolTipFormat = "{2}";

            try
            {
                _serieAruja.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieAruja.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieAruja.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieAruja.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoArujaPanel.Visible = todos != 0;
            chartControl6.Series.Add(_serieAruja);
            chartControl6.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            #region Grafico campibus

            ChartSeries _serieCampibus = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 8).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 8).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 8).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 8).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serieCampibus.Points.Add(0, naoIniciados);
            _serieCampibus.Points.Add(1, iniciados);
            _serieCampibus.Points.Add(2, finalizados);
            _serieCampibus.Points.Add(3, todos);
            _serieCampibus.EnableStyles = true;
            _serieCampibus.PointsToolTipFormat = "{2}";

            try
            {
                _serieCampibus.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieCampibus.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieCampibus.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieCampibus.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoCampibusPanel.Visible = todos != 0;
            chartControl7.Series.Add(_serieCampibus);
            chartControl7.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            #region Grafico Ribe

            ChartSeries _serieRibe = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 9).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 9).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 9).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 9).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serieRibe.Points.Add(0, naoIniciados);
            _serieRibe.Points.Add(1, iniciados);
            _serieRibe.Points.Add(2, finalizados);
            _serieRibe.Points.Add(3, todos);
            _serieRibe.EnableStyles = true;
            _serieRibe.PointsToolTipFormat = "{2}";

            try
            {
                _serieRibe.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieRibe.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieRibe.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieRibe.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoRibePanel.Visible = todos != 0;
            chartControl8.Series.Add(_serieRibe);
            chartControl8.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            #region Grafico VUG Dutra

            ChartSeries _serieVugDutra = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 10).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 10).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 10).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 10).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serieVugDutra.Points.Add(0, naoIniciados);
            _serieVugDutra.Points.Add(1, iniciados);
            _serieVugDutra.Points.Add(2, finalizados);
            _serieVugDutra.Points.Add(3, todos);
            _serieVugDutra.EnableStyles = true;
            _serieVugDutra.PointsToolTipFormat = "{2}";

            try
            {
                _serieVugDutra.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieVugDutra.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieVugDutra.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieVugDutra.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoVugDutraPanel.Visible = todos != 0;
            chartControl9.Series.Add(_serieVugDutra);
            chartControl9.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            #region Grafico VUG Bebedouro

            ChartSeries _serieVugBebedouro = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

            todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 11).Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 11).Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 11).Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 11).Count();
            qtdGraficosVisiveis = qtdGraficosVisiveis + (todos != 0 ? 1 : 0);

            _serieVugBebedouro.Points.Add(0, naoIniciados);
            _serieVugBebedouro.Points.Add(1, iniciados);
            _serieVugBebedouro.Points.Add(2, finalizados);
            _serieVugBebedouro.Points.Add(3, todos);
            _serieVugBebedouro.EnableStyles = true;
            _serieVugBebedouro.PointsToolTipFormat = "{2}";

            try
            {
                _serieVugBebedouro.Styles[0].ToolTipFormat = "Não Iniciadas = " + naoIniciados.ToString() + " - " + Math.Round((naoIniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieVugBebedouro.Styles[1].ToolTipFormat = "Iniciadas = " + iniciados.ToString() + " - " + Math.Round((iniciados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieVugBebedouro.Styles[2].ToolTipFormat = "Finalizadas = " + finalizados.ToString() + " - " + Math.Round((finalizados / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            try
            {
                _serieVugBebedouro.Styles[3].ToolTipFormat = "Total colaboradores = " + todos.ToString() + " - " + Math.Round((todos / todos) * 100, 2) + "%";
            }
            catch
            {
            }

            graficoVugBebedouroPanel.Visible = todos != 0;
            chartControl11.Series.Add(_serieVugBebedouro);
            chartControl11.BackColor = TipoAvaliacaoComboBox.BackColor;
            #endregion

            AvaliacaoNaoIniciadaGrid.ShowGroupDropArea = true;
            AvaliacaoNaoIniciadaGrid.GridGroupDropArea.BackColor = Publicas._bordaEntrada;
            AvaliacaoNaoIniciadaGrid.GridGroupDropArea.PrepareViewStyleInfo += new GridPrepareViewStyleInfoEventHandler(ParentTable_PrepareViewStyleInfo);
            AvaliacaoEmAndamentoGrid.ShowGroupDropArea = true;
            AvaliacaoEmAndamentoGrid.GridGroupDropArea.BackColor = Publicas._bordaEntrada;
            AvaliacaoEmAndamentoGrid.GridGroupDropArea.PrepareViewStyleInfo += new GridPrepareViewStyleInfoEventHandler(ParentTable_PrepareViewStyleInfo);
            AvaliacaoFinalizadasGrid.ShowGroupDropArea = true;
            AvaliacaoFinalizadasGrid.GridGroupDropArea.BackColor = Publicas._bordaEntrada;
            AvaliacaoFinalizadasGrid.GridGroupDropArea.PrepareViewStyleInfo += new GridPrepareViewStyleInfoEventHandler(ParentTable_PrepareViewStyleInfo);

            chartControl1.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl2.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl3.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl4.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl5.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl6.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl7.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl8.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl9.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl10.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl11.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);

            chartControl1.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl2.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl3.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl4.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl5.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl6.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl7.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl8.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl9.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl10.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
            chartControl11.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);

            MontaGraficosNotas();

            tamanho = (int)((DashBoardPanel.Width - 65) / qtdGraficosVisiveis);
            graficoEOVGDutraPanel.Size = new Size(tamanho, graficoEOVGDutraPanel.Height);
            graficoEOVGLavrasPanel.Size = new Size(tamanho, graficoEOVGLavrasPanel.Height);
            graficoABCPanel.Size = new Size(tamanho, graficoABCPanel.Height);
            graficoRapidoPanel.Size = new Size(tamanho, graficoRapidoPanel.Height);
            graficoCisnePanel.Size = new Size(tamanho, graficoCisnePanel.Height);
            graficoNiffPanel.Size = new Size(tamanho, graficoNiffPanel.Height);
            graficoArujaPanel.Size = new Size(tamanho, graficoArujaPanel.Height);
            graficoCampibusPanel.Size = new Size(tamanho, graficoCampibusPanel.Height);
            graficoVugDutraPanel.Size = new Size(tamanho, graficoCampibusPanel.Height);
            graficoVugBebedouroPanel.Size = new Size(tamanho, graficoCampibusPanel.Height);

            //graficosNotasPanel.Refresh();
            #endregion
        }

        private void andamentoAvaliacaoBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!_andamentoAvaliacaoEmPesquisa)
            {
                _andamentoAvaliacaoEmPesquisa = true;

                try
                {
                    _listaAvaliacoesNotas = new AutoAvaliacaoBO().ListarNotas(tipoAvaliacao, idSuperior);

                    _listaAvaliacoes = new AutoAvaliacaoBO().Listar(Convert.ToInt32(ReferenciaAvaliacaoComboBox.Text.Replace("/", "")), _cargo.TipoDoCargo, tipoAvaliacao, idSuperior);
                }
                catch
                {
                    _andamentoAvaliacaoEmPesquisa = false;
                }
            }
        }

        private void andamentoAvaliacaoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _andamentoAvaliacaoEmPesquisa = false;
            MensagemAvaliacaoLabel.Text = "";

            AvaliacaoFinalizadasGrid.DataSource = _listaAvaliacoes.Where(w => w.Status == "F").ToList();
            AvaliacaoEmAndamentoGrid.DataSource = _listaAvaliacoes.Where(w => w.Status == "I").ToList();
            AvaliacaoNaoIniciadaGrid.DataSource = _listaAvaliacoes.Where(w => w.Status == "N").ToList();

            PraparaAvaliacao();
        }

        private void TipoAvaliacaoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TipoAvaliacaoComboBox == null)
                return;
            try
            {
                idSuperior = 0;

                if (Publicas._usuario.AcessoDeGestor && !Publicas._usuario.AcessoDeRH)
                    idSuperior = _colaboradores.Id;

                tipoAvaliacao = (TipoAvaliacaoComboBox.SelectedIndex == 0 ? "AA" : // AutoAvaliacao
                                      (TipoAvaliacaoComboBox.SelectedIndex == 1 ? "FG" : // Feedback Gestor
                                      (TipoAvaliacaoComboBox.SelectedIndex == 2 ? "MN" : // Metas Numericas
                                      (TipoAvaliacaoComboBox.SelectedIndex == 3 ? "AG" : // Avaliação Gestos
                                      (TipoAvaliacaoComboBox.SelectedIndex == 4 ? "AR" : // Avaliação RH
                                      (TipoAvaliacaoComboBox.SelectedIndex == 5 ? "FA" : // FeedBack Avaliado
                                      "PD" // Plano de desenvolvimento
                                      ))))));

                andamentoAvaliacaoBackgroundWorker.RunWorkerAsync();
            }
            catch { }
        }

        private void ReferenciaAvaliacaoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoAvaliacaoComboBox_SelectedIndexChanged(sender, e);
        }

        private void andamentoButton_Click(object sender, EventArgs e)
        {
            AndamentoAvaliacoesPanel.BringToFront();
            AndamentoAvaliacoesPanel.Dock = DockStyle.Fill;
            AndamentoAvaliacoesPanel.Visible = true;
            NotasAvaliacaoPanel.Visible = false;
            RankingAvaliacaoPanel.Visible = false;
            andamentoButton.BackColor = Publicas._botao;
            andamentoButton.ForeColor = Publicas._fonteBotao;
            notasButton.BackColor = TipoAvaliacaoComboBox.BackColor;
            notasButton.ForeColor = Publicas._fonteBotaoFocado;
            rankingButton.BackColor = TipoAvaliacaoComboBox.BackColor;
            rankingButton.ForeColor = Publicas._fonteBotaoFocado;
            _mudouVisualizacaoAndamento = true;
        }

        private void rankingButton_Click(object sender, EventArgs e)
        {
            RankingAvaliacaoPanel.BringToFront();
            RankingAvaliacaoPanel.Dock = DockStyle.Fill;
            NotasAvaliacaoPanel.Visible = false;
            NotasAvaliacaoPanel.Visible = false;
            RankingAvaliacaoPanel.Visible = true;
            rankingButton.BackColor = Publicas._botao;
            rankingButton.ForeColor = Publicas._fonteBotao;
            notasButton.BackColor = TipoAvaliacaoComboBox.BackColor;
            notasButton.ForeColor = Publicas._fonteBotaoFocado;
            andamentoButton.BackColor = TipoAvaliacaoComboBox.BackColor;
            andamentoButton.ForeColor = Publicas._fonteBotaoFocado;
            _mudouVisualizacaoAndamento = true;
        }

        private void notasButton_Click(object sender, EventArgs e)
        {
            NotasAvaliacaoPanel.BringToFront();
            NotasAvaliacaoPanel.Dock = DockStyle.Fill;
            NotasAvaliacaoPanel.Visible = true;
            AndamentoAvaliacoesPanel.Visible = false;
            RankingAvaliacaoPanel.Visible = false;
            andamentoButton.BackColor = TipoAvaliacaoComboBox.BackColor;
            andamentoButton.ForeColor = Publicas._fonteBotaoFocado;
            notasButton.BackColor = Publicas._botao;
            notasButton.ForeColor = Publicas._fonteBotao;
            rankingButton.BackColor = TipoAvaliacaoComboBox.BackColor;
            rankingButton.ForeColor = Publicas._fonteBotaoFocado;
            _mudouVisualizacaoAndamento = true;
        }

        private void chartControl1_Click(object sender, EventArgs e)
        {
            //eovg Dutra
            MontaGraficosNotas(2);
        }

        private void chartControl10_Click(object sender, EventArgs e)
        {
            // Eovg Lavras
            MontaGraficosNotas(3);
        }

        private void chartControl2_Click(object sender, EventArgs e)
        {
            //ABC
            MontaGraficosNotas(4);
        }

        private void chartControl3_Click(object sender, EventArgs e)
        {
            // Rapido
            MontaGraficosNotas(5);
        }

        private void chartControl4_Click(object sender, EventArgs e)
        {
            // Cisne
            MontaGraficosNotas(6);
        }

        private void chartControl5_Click(object sender, EventArgs e)
        {
            //NIFF
            MontaGraficosNotas(1);
        }

        private void chartControl6_Click(object sender, EventArgs e)
        {
            //Aruja
            MontaGraficosNotas(7);
        }

        private void chartControl7_Click(object sender, EventArgs e)
        {
            //Campibus
            MontaGraficosNotas(8);
        }

        private void MontaGraficosNotas(int idEmpresa = 0)
        {
            #region prepara grafico notas

            if (_quantidadeGraficos != 0)
            {
                Control[] controle;
                string nome;

                for (int j = 1; j <= _quantidadeGraficos; j++)
                {
                    nome = "grafico" + j.ToString();

                    controle = this.Controls.Find(nome, true);

                    try
                    {
                        controle[0].Dispose();
                    }
                    catch { }
                }

                _quantidadeGraficos = 0;
            }

            NotasAvaliacaoPanel.Controls.Clear();

            ChartSeries _serieNotas;
            ChartControl _graficoNotas;
            int top = 5;
            int left = 3;
            int i = 1;


            foreach (var item in _listaAvaliacoesNotas.GroupBy(g => new { g.IdColaborador, g.Colaborador, g.Empresa, g.IdEmpresa })
                                                      .Select(s => new {
                                                          IdColaborador = s.Key.IdColaborador,
                                                          Colaborador = s.Key.Colaborador,
                                                          Empresa = s.Key.Empresa,
                                                          IdEmpresa = s.Key.IdEmpresa
                                                      })
                                                      .OrderBy(o => o.Colaborador))
            {
                if (idEmpresa != 0 && idEmpresa != item.IdEmpresa)
                    continue;


                _quantidadeGraficos++;

                _graficoNotas = new ChartControl();
                _graficoNotas.Name = "grafico" + _quantidadeGraficos.ToString();
                _graficoNotas.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
                _graficoNotas.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(TipoAvaliacaoComboBox.BackColor);
                _graficoNotas.Size = new Size(419, 188);
                _graficoNotas.Location = new Point(left, top);
                _graficoNotas.PrimaryXAxis.ValueType = ChartValueType.Category;
                _graficoNotas.Skins = Syncfusion.Windows.Forms.Chart.Skins.Metro;
                _graficoNotas.Palette = ChartColorPalette.Metro;
                _graficoNotas.Series3D = false;
                _graficoNotas.ShowLegend = false;
                _graficoNotas.Legend.Position = ChartDock.Bottom;
                _graficoNotas.LegendsPlacement = ChartPlacement.Outside;

                _serieNotas = new ChartSeries("", ChartSeriesType.Line);

                foreach (var itemn in _listaAvaliacoesNotas.Where(w => w.IdColaborador == item.IdColaborador))
                {
                    _graficoNotas.Text = itemn.Colaborador;
                    _serieNotas.Points.Add(itemn.ReferenciaFormatada, (double)itemn.TotalAvaliacao);
                }

                _graficoNotas.Title.Font = TipoAvaliacaoComboBox.Font;
                _graficoNotas.Series.Add(_serieNotas);

                _graficoNotas.Series[0].Style.Callout.Enable = true;
                _graficoNotas.Series[0].Style.Callout.DisplayTextAndFormat = "{2}";
                _graficoNotas.Series[0].Style.Callout.Position = LabelPosition.Top;

                NotasAvaliacaoPanel.Controls.Add(_graficoNotas);
                top = top + _graficoNotas.Height + 10;
                i++;

                if (i == 3)
                {
                    i = 1;
                    left = left + _graficoNotas.Width + 10;
                    top = 5;
                }
            }

            #endregion
        }

        private void AvaliacaoNaoIniciadaGrid_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            if (e.TableCellIdentity.TableCellType == GridTableCellType.GroupCaptionCell && e.TableCellIdentity.GroupedColumn != null)
            {
                string x = e.Style.Text;
            }
        }

        private void ParentTable_PrepareViewStyleInfo(object sender, GridPrepareViewStyleInfoEventArgs e)
        {
            if (e.ColIndex == 2 && e.RowIndex == 2) // Sets the font, color, alignment and text to the dropped column
                e.Style.Text = "";
        }

        private void chartControl9_Click(object sender, EventArgs e)
        {
            //Vug Dutra
            MontaGraficosNotas(10);
        }
        #endregion

        #endregion

        #region Notificação
        private void QuantidadeNotificacaoLabel_Click(object sender, EventArgs e)
        {
            FechaSubMenuBolao();

            // Mostrar as notificações
            if (MostraNotificacoesPanel.Visible)
            {
                MostraNotificacoesPanel.Visible = false;
                LimpaComponentesNotificacao();
                return;
            }

            DashBoardPanel.Size = new Size(25, 0);
            BotaoDashboardLabel.Text = "7";

            try
            {
                int topComunicado = 0;

                Publicas._fecharComunicados = true;
                if (_feriado.Existe)
                {
                    ExibeNotificacaoPanel = new Panel();
                    ExibeNotificacaoPanel.Size = new Size(331, 98);
                    ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                    TituloNotificacaoPanel = new Panel();
                    TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                    TituloNotificacaoPanel.Dock = DockStyle.Top;
                    TituloNotificacaoPanel.Size = new Size(0, 25);

                    TituloNotificacaoLabel = new Label();
                    TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                    TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                    TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                    TituloNotificacaoLabel.Dock = DockStyle.Fill;
                    TituloNotificacaoLabel.Text = "Amanhã é Feriado";

                    TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                    Texto1Label = new Label();
                    Texto1Label.Text = _feriado.Data.ToShortDateString();
                    Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                    Texto1Label.AutoSize = false;
                    Texto1Label.Size = new Size(324, 19);
                    Texto1Label.Location = new Point(4, 28);
                    Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                    Texto1Label.ForeColor = Publicas._fonte;
                    Texto1Label.SendToBack();

                    Texto2Label = new Label();
                    Texto2Label.Text = _feriado.Descricao;
                    Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                    Texto2Label.AutoSize = false;
                    Texto2Label.Size = new Size(324, 19);
                    Texto2Label.Location = new Point(4, 50);
                    Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                    Texto2Label.ForeColor = Publicas._fonte;
                    Texto2Label.SendToBack();

                    Texto3Label = new Label();
                    Texto3Label.Text = "Desejamos um ótimo descanso";
                    Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                    Texto3Label.AutoSize = false;
                    Texto3Label.Size = new Size(324, 19);
                    Texto3Label.Location = new Point(4, 72);
                    Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                    Texto3Label.ForeColor = Publicas._fonte;
                    Texto3Label.SendToBack();

                    ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                    ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                    ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                    ExibeNotificacaoPanel.Controls.Add(Texto3Label);

                    TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                    topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                }

                #region Banco de Horas
                if (Publicas._usuario.IdEmpresa == 1)
                {
                    DateTime _inicio = DateTime.MinValue;
                    DateTime _fim = DateTime.MinValue;
                    int _idSupervisor = 0;

                    if (supervisor.Existe)
                        _idSupervisor = _colaboradores.Id;

                    _bancoDeHoras = new List<BancoDeHoras>();

                    #region Pesquisa horario
                    if (Publicas._usuario.VisualizaBancoHorasDoDepartamento)
                    {
                        //List<DepartamentosGerenciadosPeloColaborador> _listaColaboradores = new DepartamentoBO().ListarColaboradores(_colaboradores.Id, _colaboradores.IdEmpresa);
                        List<DepartamentosGerenciadosPeloColaborador> _listaColaboradores = new DepartamentoBO().ListarColaboradores(_colaboradores.Id, _colaboradores.IdEmpresa);

                        foreach (var item2 in _listaColaboradores.OrderBy(o => o.IdColaborador))
                        {
                            List<PeriodoBancoHorasColaborador> _periodo = new PeriodoBancoHorasColaboradorBO().Listar(item2.IdColaborador);

                            List<Classes.BancoDeHoras> _bancoDeHorasDepartamento = new List<BancoDeHoras>();

                            foreach (var item in _periodo.Where(w => DateTime.Now.Date >= w.Inicio && DateTime.Now.Date <= w.Fim))
                            {
                                _inicio = item.Inicio;
                                _fim = item.Fim;


                                _bancoDeHorasDepartamento = new BancoDeHorasBO().Listar(item2.IdColaborador, _idSupervisor, _inicio, _fim, _colaboradores.IdDepartamento);

                                if (_bancoDeHorasDepartamento.Count() == 0)
                                {
                                    _inicio = item.Inicio.AddMonths(-2);
                                    _fim = item.Inicio.AddDays(-1);

                                    _bancoDeHorasDepartamento = new BancoDeHorasBO().Listar(item2.IdColaborador, _colaboradores.Id, _inicio, _fim, _colaboradores.IdDepartamento);
                                }
                            }

                            _bancoDeHoras.AddRange(_bancoDeHorasDepartamento);
                        }
                    }
                    else
                    {
                        List<PeriodoBancoHorasColaborador> _periodo = new PeriodoBancoHorasColaboradorBO().Listar(_colaboradores.Id);

                        foreach (var item in _periodo.Where(w => DateTime.Now.Date >= w.Inicio && DateTime.Now.Date <= w.Fim))
                        {
                            _inicio = item.Inicio;
                            _fim = item.Fim;

                            _bancoDeHoras = new BancoDeHorasBO().Listar(_colaboradores.Id, _idSupervisor, _inicio, _fim, _colaboradores.IdDepartamento);

                            if (_bancoDeHoras.Count() == 0)
                            {
                                _inicio = item.Inicio.AddMonths(-2);
                                _fim = item.Inicio.AddDays(-1);

                                _bancoDeHoras = new BancoDeHorasBO().Listar(_colaboradores.Id, _colaboradores.Id, _inicio, _fim, _colaboradores.IdDepartamento);
                            }
                        }
                    }
                    #endregion

                    #region Popula tela
                    ExibeNotificacaoPanel = new Panel();
                    if (_bancoDeHoras.Count <= 1)
                        ExibeNotificacaoPanel.Size = new Size(331, 95);
                    else
                        ExibeNotificacaoPanel.Size = new Size(331, 350);

                    ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                    TituloNotificacaoPanel = new Panel();
                    TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                    TituloNotificacaoPanel.Dock = DockStyle.Top;
                    TituloNotificacaoPanel.Size = new Size(0, 25);

                    TituloNotificacaoLabel = new Label();
                    TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                    TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                    TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                    TituloNotificacaoLabel.Dock = DockStyle.Fill;
                    TituloNotificacaoLabel.Text = "Banco de Horas";

                    TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                    if (NotificacaoGrid != null)
                        NotificacaoGrid = null;

                    if (_bancoDeHoras.Count == 1)
                    {
                        foreach (var hora in _bancoDeHoras)
                        {
                            Texto1Label = new Label();
                            Texto1Label.Text = _inicio.ToShortDateString() + " a " + _fim.ToShortDateString();
                            Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                            Texto1Label.AutoSize = false;
                            Texto1Label.Size = new Size(324, 19);
                            Texto1Label.Location = new Point(4, 28);
                            Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                            Texto1Label.ForeColor = Publicas._fonte;
                            Texto1Label.SendToBack();

                            Texto2Label = new Label();
                            Texto2Label.Text = hora.TotalLiquido + " " + hora.Tipo;
                            Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                            Texto2Label.AutoSize = false;
                            Texto2Label.Size = new Size(324, 19);
                            Texto2Label.Location = new Point(4, 50);
                            Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                            Texto2Label.ForeColor = Publicas._fonte;
                            Texto2Label.SendToBack();

                            Texto3Label = new Label();
                            Texto3Label.Text = "Relógio importado até a data " + hora.Data.ToShortDateString() + "." + Environment.NewLine +
                            "O horário que vale é o da frequência, pois nele é tratado o saldo de horários. Peça ao DP a sua frequência para o período acima.";
                            Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                            Texto3Label.AutoSize = false;
                            Texto3Label.Size = new Size(324, 19);
                            Texto3Label.Location = new Point(4, 72);
                            Texto3Label.Font = new Font("Century Gothic", (float)9, FontStyle.Regular);
                            Texto3Label.ForeColor = Publicas._fonte;
                            Texto3Label.SendToBack();

                            ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                            ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                            ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                            ExibeNotificacaoPanel.Controls.Add(Texto3Label);

                            TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                            topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                        }
                    }
                    else // grid
                    {
                        GridDynamicFilter filter = new GridDynamicFilter();
                        GridMetroColors metroColor = new GridMetroColors();
                        GridColumnDescriptor colunaGrid = new GridColumnDescriptor();

                        metroColor.GroupDropAreaColor.BackColor = Publicas._bordaEntrada;

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

                        Texto1Label = new Label();
                        Texto1Label.Text = "Relógio importado até a data " + _bancoDeHoras.Max(M => M.Data).ToShortDateString() + "." + Environment.NewLine +
                            "O horário que vale é o da frequência, pois nele é tratado o saldo de horários. Peça ao DP a frequência do colaborador conforme o período acima.";
                        Texto1Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(324, 69);
                        Texto1Label.Dock = DockStyle.Bottom;
                        //Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)9, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;
                        Texto1Label.SendToBack();


                        NotificacaoGrid = new GridGroupingControl();
                        NotificacaoGrid.QueryCellStyleInfo += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableCellStyleInfoEventHandler(this.NotificacaoGrid_QueryCellStyleInfo);
                        filter.WireGrid(NotificacaoGrid);

                        NotificacaoGrid.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
                        NotificacaoGrid.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.SetCurrent;
                        NotificacaoGrid.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
                        NotificacaoGrid.Font = new Font(dataHoraLabel.Font.FontFamily, (float)9);
                        NotificacaoGrid.Dock = DockStyle.Fill;
                        NotificacaoGrid.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
                        NotificacaoGrid.ShowRowHeaders = true;
                        NotificacaoGrid.TopLevelGroupOptions.ShowCaption = false;
                        NotificacaoGrid.TableDescriptor.AllowEdit = false;
                        NotificacaoGrid.TableDescriptor.AllowNew = false;
                        NotificacaoGrid.TableDescriptor.AllowRemove = false;

                        NotificacaoGrid.SortIconPlacement = SortIconPlacement.Left;
                        NotificacaoGrid.TopLevelGroupOptions.ShowFilterBar = true;
                        NotificacaoGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
                        NotificacaoGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
                        NotificacaoGrid.TableControl.CellToolTip.Active = true;
                        NotificacaoGrid.SetMetroStyle(metroColor);

                        NotificacaoGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
                        NotificacaoGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                        NotificacaoGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
                        NotificacaoGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

                        colunaGrid = new GridColumnDescriptor("NomeColaborador", "NomeColaborador", "Nome");
                        colunaGrid.AllowFilter = true;
                        colunaGrid.AllowSort = true;
                        colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                        colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                        colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;
                        colunaGrid.Width = 139;

                        colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = false;
                        colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        colunaGrid.Appearance.AnyRecordFieldCell.TextColor = Color.DimGray;
                        colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        colunaGrid.Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(dataHoraLabel.Font.FontFamily, (float)8, FontStyle.Bold));
                        colunaGrid.Appearance.ColumnHeaderCell.AutoSize = false;
                        colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        NotificacaoGrid.TableDescriptor.Columns.Add(colunaGrid);

                        colunaGrid = new GridColumnDescriptor("TotalLiquido", "TotalLiquido", "Horas");
                        colunaGrid.AllowFilter = true;
                        colunaGrid.AllowSort = true;
                        colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                        colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                        colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;

                        colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = true;
                        colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                        colunaGrid.Appearance.AnyRecordFieldCell.TextColor = Color.DimGray;
                        colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        colunaGrid.Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(dataHoraLabel.Font.FontFamily, (float)8, FontStyle.Bold));
                        colunaGrid.Appearance.ColumnHeaderCell.AutoSize = true;
                        colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                        colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        NotificacaoGrid.TableDescriptor.Columns.Add(colunaGrid);

                        colunaGrid = new GridColumnDescriptor("Tipo", "Tipo", "Tipo");
                        colunaGrid.AllowFilter = true;
                        colunaGrid.AllowSort = true;
                        colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                        colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                        colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;

                        colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = true;
                        colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        colunaGrid.Appearance.AnyRecordFieldCell.TextColor = Color.DimGray;
                        colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        colunaGrid.Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(dataHoraLabel.Font.FontFamily, (float)8, FontStyle.Bold));
                        colunaGrid.Appearance.ColumnHeaderCell.AutoSize = true;
                        colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        NotificacaoGrid.TableDescriptor.Columns.Add(colunaGrid);

                        colunaGrid = new GridColumnDescriptor("Periodo", "Periodo", "Período");
                        colunaGrid.AllowFilter = true;
                        colunaGrid.AllowSort = true;
                        colunaGrid.FilterRowOptions.AllowCustomFilter = false;
                        colunaGrid.FilterRowOptions.AllowEmptyFilter = false;
                        colunaGrid.FilterRowOptions.FilterMode = FilterMode.Value;

                        colunaGrid.Appearance.AnyRecordFieldCell.AutoSize = true;
                        colunaGrid.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        colunaGrid.Appearance.AnyRecordFieldCell.TextColor = Color.DimGray;
                        colunaGrid.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        colunaGrid.Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(dataHoraLabel.Font.FontFamily, (float)8, FontStyle.Regular));
                        colunaGrid.Appearance.ColumnHeaderCell.AutoSize = true;
                        colunaGrid.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        colunaGrid.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        NotificacaoGrid.TableDescriptor.Columns.Add(colunaGrid);

                        NotificacaoGrid.Table.DefaultRecordRowHeight = 35;

                        NotificacaoGrid.DataSource = _bancoDeHoras.OrderBy(o => o.NomeColaborador).ToList();

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(NotificacaoGrid);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);

                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                    }
                    #endregion                    
                }
                #endregion

                #region Torneios

                if (_listaTorneios.Count() != 0)
                {
                    ExibeNotificacaoPanel = new Panel();
                    ExibeNotificacaoPanel.Size = new Size(331, (_listaTorneios.Count() == 4 ? 138 : (_listaTorneios.Count() == 3 ? 118 : 98)));
                    ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                    TituloNotificacaoPanel = new Panel();
                    TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                    TituloNotificacaoPanel.Dock = DockStyle.Top;
                    TituloNotificacaoPanel.Size = new Size(0, 25);

                    TituloNotificacaoLabel = new Label();
                    TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                    TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                    TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                    TituloNotificacaoLabel.Dock = DockStyle.Fill;
                    TituloNotificacaoLabel.Text = "Sua competição de hoje";

                    TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);
                    ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);

                    int i = 1;
                    foreach (var item in _listaTorneios)
                    {
                        if (i == 1)
                        {
                            Texto1Label = new Label();
                            Texto1Label.Text = item.Torneio;
                            Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                            Texto1Label.AutoSize = false;
                            Texto1Label.Size = new Size(324, 19);
                            Texto1Label.Location = new Point(4, 28);
                            Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                            Texto1Label.ForeColor = Publicas._fonte;
                            Texto1Label.SendToBack();

                            Texto2Label = new Label();
                            Texto2Label.Text = item.Nome;
                            Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                            Texto2Label.AutoSize = false;
                            Texto2Label.Size = new Size(324, 19);
                            Texto2Label.Location = new Point(4, 50);
                            Texto2Label.Font = new Font("Century Gothic", (float)10.25, (item.IdColaborador == Publicas._idColaborador ? FontStyle.Bold : FontStyle.Regular));
                            Texto2Label.ForeColor = Publicas._fonte;
                            Texto2Label.SendToBack();

                            ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                            ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        }
                        if (i == 2)
                        {
                            Texto3Label = new Label();
                            Texto3Label.Text = item.Nome;
                            Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                            Texto3Label.AutoSize = false;
                            Texto3Label.Size = new Size(324, 19);
                            Texto3Label.Location = new Point(4, 72);
                            Texto3Label.Font = new Font("Century Gothic", (float)10.25, (item.IdColaborador == Publicas._idColaborador ? FontStyle.Bold : FontStyle.Regular));
                            Texto3Label.ForeColor = Publicas._fonte;
                            Texto3Label.SendToBack();
                            ExibeNotificacaoPanel.Controls.Add(Texto3Label);
                        }

                        if (i == 3)
                        {
                            Texto4Label = new Label();
                            Texto4Label.Text = item.Nome;
                            Texto4Label.TextAlign = ContentAlignment.MiddleLeft;
                            Texto4Label.AutoSize = false;
                            Texto4Label.Size = new Size(324, 19);
                            Texto4Label.Location = new Point(4, 92);
                            Texto4Label.Font = new Font("Century Gothic", (float)10.25, (item.IdColaborador == Publicas._idColaborador ? FontStyle.Bold : FontStyle.Regular));
                            Texto4Label.ForeColor = Publicas._fonte;
                            Texto4Label.SendToBack();
                            ExibeNotificacaoPanel.Controls.Add(Texto4Label);
                        }

                        if (i == 4)
                        {
                            Texto5Label = new Label();
                            Texto5Label.Text = item.Nome;
                            Texto5Label.TextAlign = ContentAlignment.MiddleLeft;
                            Texto5Label.AutoSize = false;
                            Texto5Label.Size = new Size(324, 19);
                            Texto5Label.Location = new Point(4, 112);
                            Texto5Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                            Texto5Label.ForeColor = Publicas._fonte;
                            Texto5Label.SendToBack();
                            ExibeNotificacaoPanel.Controls.Add(Texto5Label);
                        }
                        i++;
                    }
                    TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                    topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                }
                #endregion

                #region Aniversários
                if (MostrarAnivervariosCheckBox.Checked)
                {
                    foreach (var item in _listaAniversariantes)
                    {
                        ExibeNotificacaoPanel = new Panel();
                        ExibeNotificacaoPanel.Size = new Size(331, 98);
                        ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                        TituloNotificacaoPanel = new Panel();
                        TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                        TituloNotificacaoPanel.Dock = DockStyle.Top;
                        TituloNotificacaoPanel.Size = new Size(0, 25);

                        TituloNotificacaoLabel = new Label();
                        TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                        TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                        TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                        TituloNotificacaoLabel.Dock = DockStyle.Fill;
                        TituloNotificacaoLabel.Text = "Aniversariante";

                        TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                        Texto1Label = new Label();
                        Texto1Label.Text = item.DataNascimento.ToShortDateString();
                        Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(324, 19);
                        Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;
                        Texto1Label.SendToBack();

                        Texto2Label = new Label();
                        Texto2Label.Text = item.Nome;
                        Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto2Label.AutoSize = false;
                        Texto2Label.Size = new Size(324, 19);
                        Texto2Label.Location = new Point(4, 50);
                        Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto2Label.ForeColor = Publicas._fonte;
                        Texto2Label.SendToBack();

                        Texto3Label = new Label();
                        Texto3Label.Text = item.Empresa;
                        Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto3Label.AutoSize = false;
                        Texto3Label.Size = new Size(324, 19);
                        Texto3Label.Location = new Point(4, 72);
                        Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto3Label.ForeColor = Publicas._fonte;
                        Texto3Label.SendToBack();

                        Imagem1Picture = new PictureBox();
                        Imagem1Picture.Size = new Size(50, 44);
                        Imagem1Picture.ImageLocation = @"Imagens\BalaoNiver.png";
                        Imagem1Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                        Imagem1Picture.Location = new Point(276, 50);

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto3Label);
                        ExibeNotificacaoPanel.Controls.Add(Imagem1Picture);
                        Imagem1Picture.BringToFront();

                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                        Refresh();
                    }
                }
                #endregion

                #region Bolão
                DateTime _dataTeste = new DateTime(2018, 06, 14);
                int qtdDias = 1;

                if (_listaJogosEncerrados != null &&_listaJogosEncerrados.Count != 0)
                {
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                        qtdDias = 3;

                    foreach (var item in _listaJogosEncerrados.Where(w => w.Data.Date >= DateTime.Now.Date.AddDays(-qtdDias))
                                                          .OrderBy(o => o.Data))
                    {
                        ExibeNotificacaoPanel = new Panel();
                        ExibeNotificacaoPanel.Size = new Size(331, 98);
                        ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                        TituloNotificacaoPanel = new Panel();
                        TituloNotificacaoPanel.BackColor = Publicas._panelTitulo; 
                        TituloNotificacaoPanel.Dock = DockStyle.Top;
                        TituloNotificacaoPanel.Size = new Size(0, 25);

                        TituloNotificacaoLabel = new Label();
                        TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                        TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                        TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                        TituloNotificacaoLabel.Dock = DockStyle.Fill;
                        TituloNotificacaoLabel.Text = "Bolão da Copa - Partida encerrada";

                        TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                        Texto1Label = new Label();
                        Texto1Label.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString();
                        Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(324, 19);
                        Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;
                        Texto1Label.SendToBack();

                        Texto2Label = new Label();
                        Texto2Label.Text = item.Nome1;
                        Texto2Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto2Label.AutoSize = false;
                        Texto2Label.Size = new Size(88, 38);
                        Texto2Label.Location = new Point(51, 55);
                        Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto2Label.ForeColor = Publicas._fonte;
                        Texto2Label.SendToBack();

                        Texto3Label = new Label();
                        Texto3Label.Text = item.Placar1.ToString();
                        Texto3Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto3Label.AutoSize = false;
                        Texto3Label.Size = new Size(22, 38);
                        Texto3Label.Location = new Point(140, 55);
                        Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto3Label.ForeColor = Publicas._fonte;
                        Texto3Label.SendToBack();

                        Texto4Label = new Label();
                        Texto4Label.Text = "x";
                        Texto4Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto4Label.AutoSize = false;
                        Texto4Label.Size = new Size(17, 38);
                        Texto4Label.Location = new Point(158, 55);
                        Texto4Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto4Label.ForeColor = Publicas._fonte;
                        Texto4Label.SendToBack();

                        Texto5Label = new Label();
                        Texto5Label.Text = item.Placar2.ToString();
                        Texto5Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto5Label.AutoSize = false;
                        Texto5Label.Size = new Size(22, 38);
                        Texto5Label.Location = new Point(172, 55);
                        Texto5Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto5Label.ForeColor = Publicas._fonte;
                        Texto5Label.SendToBack();

                        Texto6Label = new Label();
                        Texto6Label.Text = item.Nome2;
                        Texto6Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto6Label.AutoSize = false;
                        Texto6Label.Size = new Size(88, 38);
                        Texto6Label.Location = new Point(195, 55);
                        Texto6Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto6Label.ForeColor = Publicas._fonte;
                        Texto6Label.SendToBack();

                        Imagem1Picture = new PictureBox();
                        Imagem1Picture.Size = new Size(41, 23);
                        Imagem1Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                        Imagem1Picture.Location = new Point(8, 59);

                        Imagem2Picture = new PictureBox();
                        Imagem2Picture.Size = new Size(41, 23);
                        Imagem2Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                        Imagem2Picture.Location = new Point(284, 59);


                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                Imagem1Picture.Image = new Bitmap(mStream);
                            }

                            Imagem1Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                            Imagem1Picture.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                Imagem2Picture.Image = new Bitmap(mStream);
                            }

                            Imagem2Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                            Imagem2Picture.Refresh();
                        }
                        catch { }

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto3Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto4Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto5Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto6Label);
                        ExibeNotificacaoPanel.Controls.Add(Imagem1Picture);
                        ExibeNotificacaoPanel.Controls.Add(Imagem2Picture);

                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);

                        BolaoNotificacao _not = new BolaoNotificacao();
                        _not.IdJogo = item.Id;
                        _not.IdColaborador = Publicas._idColaborador;

                        new BolaoNotificacaoBO().Gravar(_not);
                    }
                }

                if (_listaJogosDos3Dias != null && _listaJogosDos3Dias.Count != 0)
                {
                    qtdDias = 1;

                    if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                        qtdDias = 3;

                    foreach (var item in _listaJogosDos3Dias.Where(w => w.Data.Date >= DateTime.Now && w.Data.Date <= DateTime.Now.Date.AddDays(qtdDias))
                                                        .OrderBy(o => o.Data))
                    {
                        ExibeNotificacaoPanel = new Panel();
                        ExibeNotificacaoPanel.Size = new Size(331, 98);
                        ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                        TituloNotificacaoPanel = new Panel();
                        TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                        TituloNotificacaoPanel.Dock = DockStyle.Top;
                        TituloNotificacaoPanel.Size = new Size(0, 25);

                        TituloNotificacaoLabel = new Label();
                        TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                        TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                        TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                        TituloNotificacaoLabel.Dock = DockStyle.Fill;
                        TituloNotificacaoLabel.Text = "Bolão - Partidas sem seus palpites";

                        TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                        Texto1Label = new Label();
                        Texto1Label.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString();
                        Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(324, 19);
                        Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;
                        Texto1Label.SendToBack();

                        Texto2Label = new Label();
                        Texto2Label.Text = item.Nome1;
                        Texto2Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto2Label.AutoSize = false;
                        Texto2Label.Size = new Size(88, 38);
                        Texto2Label.Location = new Point(51, 55);
                        Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto2Label.ForeColor = Publicas._fonte;
                        Texto2Label.SendToBack();

                        Texto3Label = new Label();
                        Texto3Label.Text = item.Placar1.ToString();
                        Texto3Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto3Label.AutoSize = false;
                        Texto3Label.Size = new Size(22, 38);
                        Texto3Label.Location = new Point(140, 55);
                        Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto3Label.ForeColor = Publicas._fonte;
                        Texto3Label.SendToBack();

                        Texto4Label = new Label();
                        Texto4Label.Text = "x";
                        Texto4Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto4Label.AutoSize = false;
                        Texto4Label.Size = new Size(17, 38);
                        Texto4Label.Location = new Point(158, 55);
                        Texto4Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto4Label.ForeColor = Publicas._fonte;
                        Texto4Label.SendToBack();

                        Texto5Label = new Label();
                        Texto5Label.Text = item.Placar2.ToString();
                        Texto5Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto5Label.AutoSize = false;
                        Texto5Label.Size = new Size(22, 38);
                        Texto5Label.Location = new Point(172, 55);
                        Texto5Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto5Label.ForeColor = Publicas._fonte;
                        Texto5Label.SendToBack();

                        Texto6Label = new Label();
                        Texto6Label.Text = item.Nome2;
                        Texto6Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto6Label.AutoSize = false;
                        Texto6Label.Size = new Size(88, 38);
                        Texto6Label.Location = new Point(195, 55);
                        Texto6Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto6Label.ForeColor = Publicas._fonte;
                        Texto6Label.SendToBack();

                        Imagem1Picture = new PictureBox();
                        Imagem1Picture.Size = new Size(41, 23);
                        Imagem1Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                        Imagem1Picture.Location = new Point(8, 59);

                        Imagem2Picture = new PictureBox();
                        Imagem2Picture.Size = new Size(41, 23);
                        Imagem2Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                        Imagem2Picture.Location = new Point(284, 59);

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                Imagem1Picture.Image = new Bitmap(mStream);
                            }

                            Imagem1Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                            Imagem1Picture.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                Imagem2Picture.Image = new Bitmap(mStream);
                            }

                            Imagem2Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                            Imagem2Picture.Refresh();
                        }
                        catch { }

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto3Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto4Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto5Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto6Label);
                        ExibeNotificacaoPanel.Controls.Add(Imagem1Picture);
                        ExibeNotificacaoPanel.Controls.Add(Imagem2Picture);

                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);

                        BolaoNotificacao _not = new BolaoNotificacao();
                        _not.IdJogo = item.Id;
                        _not.IdColaborador = Publicas._idColaborador;

                        new BolaoNotificacaoBO().Gravar(_not);
                    }
                }
                #endregion

                #region Corridas
                if (Publicas._usuario != null && !Publicas._usuario.NaoNotificaCorridas)
                {
                    if (_participante.Classificacao != 0 && !_participante.Visualizado)
                    {
                        ExibeNotificacaoPanel = new Panel();
                        ExibeNotificacaoPanel.Size = new Size(331, 120);
                        ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                        TituloNotificacaoPanel = new Panel();
                        TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                        TituloNotificacaoPanel.Dock = DockStyle.Top;
                        TituloNotificacaoPanel.Size = new Size(0, 25);

                        TituloNotificacaoLabel = new Label();
                        TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                        TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                        TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                        TituloNotificacaoLabel.Dock = DockStyle.Fill;
                        TituloNotificacaoLabel.Text = "Seu resultado na corrida do dia" + _participante.DataCorrida.ToShortDateString();

                        TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                        Texto1Label = new Label();
                        Texto1Label.Text = "Classificação " + _participante.Classificacao.ToString();
                        Texto1Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(138, 19);
                        Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;
                        Texto1Label.SendToBack();

                        Texto2Label = new Label();
                        Texto2Label.Text = "Classificação geral " + _participante.ClassificacaoGeral.ToString();
                        Texto2Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto2Label.AutoSize = false;
                        Texto2Label.Size = new Size(179, 19);
                        Texto2Label.Location = new Point(147, 28);
                        Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto2Label.ForeColor = Publicas._fonte;
                        Texto2Label.SendToBack();

                        Texto3Label = new Label();
                        Texto3Label.Text = "Tempo Bruto " + _participante.TempoBrutoFormatado;
                        Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto3Label.AutoSize = false;
                        Texto3Label.Size = new Size(177, 19);
                        Texto3Label.Location = new Point(4, 53);
                        Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto3Label.ForeColor = Publicas._fonte;
                        Texto3Label.SendToBack();

                        Texto4Label = new Label();
                        Texto4Label.Text = "Tempo Liquido " + _participante.TempoLiquidoFormatado;
                        Texto4Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto4Label.AutoSize = false;
                        Texto4Label.Size = new Size(177, 19);
                        Texto4Label.Location = new Point(4, 75);
                        Texto4Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto4Label.ForeColor = Publicas._fonte;
                        Texto4Label.SendToBack();

                        Texto5Label = new Label();
                        Texto5Label.Text = "Ritmo " + _participante.RitmoFormatado;
                        Texto5Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto5Label.AutoSize = false;
                        Texto5Label.Size = new Size(177, 19);
                        Texto5Label.Location = new Point(4, 97);
                        Texto5Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto5Label.ForeColor = Publicas._fonte;
                        Texto5Label.SendToBack();

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto3Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto4Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto5Label);

                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);

                        new ParticipanteCorridaBO().ResultadoVisualizado(_participante.Id);
                        Refresh();
                    }

                    foreach (var item in _listaCorrida.Where(w => w.Data >= DateTime.Now.Date))
                    {
                        ExibeNotificacaoPanel = new Panel();
                        ExibeNotificacaoPanel.Size = new Size(331, 98);
                        ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                        TituloNotificacaoPanel = new Panel();
                        TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                        TituloNotificacaoPanel.Dock = DockStyle.Top;
                        TituloNotificacaoPanel.Size = new Size(0, 25);

                        TituloNotificacaoLabel = new Label();
                        TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                        TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                        TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                        TituloNotificacaoLabel.Dock = DockStyle.Fill;
                        TituloNotificacaoLabel.Text = "Inscreva-se para a corrida";

                        TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                        Texto1Label = new Label();
                        Texto1Label.Text = item.Data.ToShortDateString();
                        Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(138, 19);
                        Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;
                        Texto1Label.SendToBack();

                        Texto2Label = new Label();
                        Texto2Label.Text = item.Nome; 
                        Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto2Label.AutoSize = false;
                        Texto2Label.Size = new Size(179, 19);
                        Texto2Label.Location = new Point(147, 28);
                        Texto2Label.Tag = item.Id;
                        Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto2Label.ForeColor = Publicas._fonte;
                        Texto2Label.SendToBack();

                        Texto3Label = new Label();
                        Texto3Label.Text = item.Local;
                        Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto3Label.AutoSize = false;
                        Texto3Label.Size = new Size(177, 19);
                        Texto3Label.Location = new Point(4, 53);
                        Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto3Label.ForeColor = Publicas._fonte;
                        Texto3Label.SendToBack();

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto3Label);
                       
                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);

                        Refresh();
                    }
                }
                #endregion

                #region Comunicados
                if (_listaComunicadosNotificacao != null)
                {
                    foreach (var item in _listaComunicadosNotificacao)
                    {
                        ExibeNotificacaoPanel = new Panel();
                        ExibeNotificacaoPanel.Size = new Size(331, 120);
                        ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                        TituloNotificacaoPanel = new Panel();
                        TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                        TituloNotificacaoPanel.Dock = DockStyle.Top;
                        TituloNotificacaoPanel.Size = new Size(0, 25);

                        TituloNotificacaoLabel = new Label();
                        TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                        TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                        TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                        TituloNotificacaoLabel.Dock = DockStyle.Fill;
                        TituloNotificacaoLabel.Text = " Comunicado " + item.Status;

                        TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                        Texto1Label = new Label();
                        Texto1Label.Text = "Processo nº " + item.Processo;
                        Texto1Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(324, 19);
                        Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;

                        Texto2Label = new Label();
                        Texto2Label.Text = "Vara " + item.Vara;
                        Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto2Label.AutoSize = false;
                        Texto2Label.Size = new Size(324, 19);
                        Texto2Label.Location = new Point(4, 50);
                        Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto2Label.ForeColor = Publicas._fonte;

                        Texto3Label = new Label();
                        Texto3Label.Text = item.Empresa;
                        Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto3Label.AutoSize = false;
                        Texto3Label.Size = new Size(324, 19);
                        Texto3Label.Location = new Point(4, 72);
                        Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto3Label.ForeColor = Publicas._fonte;

                        Texto4Label = new Label();

                        Texto4Label.Text = (item.Status == Publicas.StatusComunicado.Novo ? "Aberto por " :
                                (item.Status == Publicas.StatusComunicado.Alterado ? "Alterado por " :
                                (item.Status == Publicas.StatusComunicado.Aprovado ? "Aprovador por " :
                                (item.Status == Publicas.StatusComunicado.Cancelado ? "Cancelado por " :
                                (item.Status == Publicas.StatusComunicado.Finalizado ? "Finalizado por" : "Reprovado por "))))) +
                                (item.Status == Publicas.StatusComunicado.Novo ? item.Solicitante :
                                (item.Status == Publicas.StatusComunicado.Alterado ? item.UsuarioAlterador :
                                (item.Status == Publicas.StatusComunicado.Aprovado ? item.UsuarioAprovador :
                                (item.Status == Publicas.StatusComunicado.Cancelado ? item.UsuarioCancelador :
                                (item.Status == Publicas.StatusComunicado.Finalizado ? item.UsuarioFinaliza : item.UsuarioReprovador)))));

                        Texto4Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto4Label.AutoSize = false;
                        Texto4Label.Size = new Size(324, 19);
                        Texto4Label.Location = new Point(4, 95);
                        Texto4Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto4Label.ForeColor = Publicas._fonte;

                        Imagem1Picture = new PictureBox();
                        Imagem1Picture.Size = new Size(50, 44);
                        Imagem1Picture.ImageLocation = @"Imagens\Juridico.png";
                        Imagem1Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                        Imagem1Picture.Location = new Point(276, 50);

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto3Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto4Label);
                        ExibeNotificacaoPanel.Controls.Add(Imagem1Picture);
                        Imagem1Picture.BringToFront();

                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);

                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                        new ComunicadoBO().GravarNotificacao(item);
                        Refresh();
                    }
                }
                #endregion

                #region Arquivei
                string nomeArquivo = "";
                if (_listaArquivei != null)
                {
                    foreach (var item in _listaArquivei.GroupBy(g => new { g.NomeArquivo, g.DataImportado, g.NomeEmpresa })
                                                       .Select(s => new { DataImportado = s.Key.DataImportado, NomeArquivo = s.Key.NomeArquivo, NomeEmpresa = s.Key.NomeEmpresa }))
                    {
                        if (nomeArquivo == "" || nomeArquivo != item.NomeArquivo)
                        {
                            nomeArquivo = item.NomeArquivo;
                            ExibeNotificacaoPanel = new Panel();
                            ExibeNotificacaoPanel.Size = new Size(331, 98);
                            ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                            TituloNotificacaoPanel = new Panel();
                            TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                            TituloNotificacaoPanel.Dock = DockStyle.Top;
                            TituloNotificacaoPanel.Size = new Size(0, 25);

                            TituloNotificacaoLabel = new Label();
                            TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                            TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                            TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                            TituloNotificacaoLabel.Dock = DockStyle.Fill;
                            TituloNotificacaoLabel.Text = "Arquivei";

                            TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                            Texto1Label = new Label();
                            Texto1Label.Text = item.DataImportado.ToShortDateString();
                            Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                            Texto1Label.AutoSize = false;
                            Texto1Label.Size = new Size(324, 19);
                            Texto1Label.Location = new Point(4, 28);
                            Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                            Texto1Label.ForeColor = Publicas._fonte;
                            Texto1Label.SendToBack();

                            Texto2Label = new Label();
                            Texto2Label.Text = "Arquivo importado: " + Path.GetFileName(item.NomeArquivo);
                            Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                            Texto2Label.AutoSize = false;
                            Texto2Label.Size = new Size(324, 19);
                            Texto2Label.Location = new Point(4, 50);
                            Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                            Texto2Label.ForeColor = Publicas._fonte;
                            Texto2Label.SendToBack();

                            Texto3Label = new Label();
                            Texto3Label.Text = "Empresa: " + item.NomeEmpresa;
                            Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                            Texto3Label.AutoSize = false;
                            Texto3Label.Size = new Size(324, 19);
                            Texto3Label.Location = new Point(4, 72);
                            Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                            Texto3Label.ForeColor = Publicas._fonte;
                            Texto3Label.SendToBack();

                            ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                            ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                            ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                            ExibeNotificacaoPanel.Controls.Add(Texto3Label);

                            TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                            topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                        }
                    }
                }
                #endregion

                #region Biblioteca
                if (_listaEmprestimos != null)
                {
                    foreach (var item in _listaEmprestimos.OrderBy(o => o.DataAcompanhamento))
                    {
                        ExibeNotificacaoPanel = new Panel();
                        ExibeNotificacaoPanel.Size = new Size(331, 118);
                        ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                        TituloNotificacaoPanel = new Panel();
                        TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                        TituloNotificacaoPanel.Dock = DockStyle.Top;
                        TituloNotificacaoPanel.Size = new Size(0, 25);

                        TituloNotificacaoLabel = new Label();
                        TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                        TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                        TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                        TituloNotificacaoLabel.Dock = DockStyle.Fill;
                        TituloNotificacaoLabel.Text = "Biblioteca - Devolver até";

                        TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                        Texto1Label = new Label();
                        Texto1Label.Text = item.DataAcompanhamento.ToShortDateString();
                        Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(324, 19);
                        Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;
                        Texto1Label.SendToBack();

                        Texto2Label = new Label();
                        Texto2Label.Text = "Livro: " + item.NomeLivro;
                        Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto2Label.AutoSize = false;
                        Texto2Label.Size = new Size(324, 19);
                        Texto2Label.Location = new Point(4, 50);
                        Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto2Label.ForeColor = Publicas._fonte;
                        Texto2Label.SendToBack();

                        Texto3Label = new Label();
                        Texto3Label.Text = item.NomeColaborador;
                        Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto3Label.AutoSize = false;
                        Texto3Label.Size = new Size(324, 19);
                        Texto3Label.Location = new Point(4, 72);
                        Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto3Label.ForeColor = Publicas._fonte;
                        Texto3Label.SendToBack();

                        Texto4Label = new Label();
                        Texto4Label.Text = "Emprestado em " + item.Data.ToShortDateString();
                        Texto4Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto4Label.AutoSize = false;
                        Texto4Label.Size = new Size(324, 19);
                        Texto4Label.Location = new Point(4, 92);
                        Texto4Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto4Label.ForeColor = Publicas._fonte;
                        Texto4Label.SendToBack();

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto3Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto4Label);

                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                    }
                }

                if (_listaReservas != null)
                {
                    foreach (var item in _listaReservas.OrderBy(o => o.DataSolicitado))
                    {
                        ExibeNotificacaoPanel = new Panel();
                        ExibeNotificacaoPanel.Size = new Size(331, 98);
                        ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                        TituloNotificacaoPanel = new Panel();
                        TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                        TituloNotificacaoPanel.Dock = DockStyle.Top;
                        TituloNotificacaoPanel.Size = new Size(0, 25);

                        TituloNotificacaoLabel = new Label();
                        TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                        TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                        TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                        TituloNotificacaoLabel.Dock = DockStyle.Fill;
                        TituloNotificacaoLabel.Text = "Biblioteca - Reservas em aberto";

                        TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                        Texto1Label = new Label();
                        Texto1Label.Text = item.DataSolicitado.ToShortDateString();
                        Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(324, 19);
                        Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;
                        Texto1Label.SendToBack();

                        Texto2Label = new Label();
                        Texto2Label.Text = "Livro: " + item.NomeLivro;
                        Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto2Label.AutoSize = false;
                        Texto2Label.Size = new Size(324, 19);
                        Texto2Label.Location = new Point(4, 50);
                        Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto2Label.ForeColor = Publicas._fonte;
                        Texto2Label.SendToBack();

                        Texto3Label = new Label();
                        Texto3Label.Text = "Solicitante " + item.NomeColaborador;
                        Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto3Label.AutoSize = false;
                        Texto3Label.Size = new Size(324, 19);
                        Texto3Label.Location = new Point(4, 72);
                        Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto3Label.ForeColor = Publicas._fonte;
                        Texto3Label.SendToBack();

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto3Label);

                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                    }
                }

                if (_listaResenhas != null)
                {
                    foreach (var item in _listaResenhas.OrderBy(o => o.Data))
                    {
                        ExibeNotificacaoPanel = new Panel();
                        ExibeNotificacaoPanel.Size = new Size(331, 98);
                        ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                        TituloNotificacaoPanel = new Panel();
                        TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                        TituloNotificacaoPanel.Dock = DockStyle.Top;
                        TituloNotificacaoPanel.Size = new Size(0, 25);

                        TituloNotificacaoLabel = new Label();
                        TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                        TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                        TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                        TituloNotificacaoLabel.Dock = DockStyle.Fill;

                        if (item.Ativo)
                            TituloNotificacaoLabel.Text = "Biblioteca - Resenha liberada";
                        else
                            TituloNotificacaoLabel.Text = "Biblioteca - Resenha cadastrada não liberada";

                        TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                        Texto1Label = new Label();

                        if (item.Ativo)
                            Texto1Label.Text = item.DataLiberacao.ToShortDateString();
                        else
                            Texto1Label.Text = item.Data.ToShortDateString();

                        Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(324, 19);
                        Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;
                        Texto1Label.SendToBack();

                        Texto2Label = new Label();
                        Texto2Label.Text = "Livro: " + item.NomeLivro;
                        Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto2Label.AutoSize = false;
                        Texto2Label.Size = new Size(324, 19);
                        Texto2Label.Location = new Point(4, 50);
                        Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto2Label.ForeColor = Publicas._fonte;
                        Texto2Label.SendToBack();

                        Texto3Label = new Label();
                        if (!item.Ativo)
                            Texto3Label.Text = "Colaborador " + item.NomeColaborador;
                        else
                            Texto3Label.Text = item.Pontuacao.ToString() + " Pontos pela participação";
                        Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto3Label.AutoSize = false;
                        Texto3Label.Size = new Size(324, 19);
                        Texto3Label.Location = new Point(4, 72);
                        Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto3Label.ForeColor = Publicas._fonte;
                        Texto3Label.SendToBack();

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto3Label);

                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                    }
                } 

                if (_listaResenhasNaoCadastradas != null)
                {
                    foreach (var item in _listaResenhasNaoCadastradas.OrderBy(o => o.Data))
                    {
                        ExibeNotificacaoPanel = new Panel();
                        ExibeNotificacaoPanel.Size = new Size(331, 98);
                        ExibeNotificacaoPanel.Location = new Point(0, topComunicado);

                        TituloNotificacaoPanel = new Panel();
                        TituloNotificacaoPanel.BackColor = Publicas._panelTitulo;
                        TituloNotificacaoPanel.Dock = DockStyle.Top;
                        TituloNotificacaoPanel.Size = new Size(0, 25);

                        TituloNotificacaoLabel = new Label();
                        TituloNotificacaoLabel.Font = new Font(usuarioLogadoLabel.Font, usuarioLogadoLabel.Font.Style);
                        TituloNotificacaoLabel.ForeColor = Publicas._fundo;
                        TituloNotificacaoLabel.TextAlign = ContentAlignment.MiddleLeft;
                        TituloNotificacaoLabel.Dock = DockStyle.Fill;
                        TituloNotificacaoLabel.Text = "Biblioteca - Livro sem resenha";
                        TituloNotificacaoPanel.Controls.Add(TituloNotificacaoLabel);

                        Texto1Label = new Label();
                        Texto1Label.Text = item.Data.ToShortDateString();
                        Texto1Label.TextAlign = ContentAlignment.MiddleRight;
                        Texto1Label.AutoSize = false;
                        Texto1Label.Size = new Size(324, 19);
                        Texto1Label.Location = new Point(4, 28);
                        Texto1Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto1Label.ForeColor = Publicas._fonte;
                        Texto1Label.SendToBack();

                        Texto2Label = new Label();
                        Texto2Label.Text = "Livro: " + item.NomeLivro;
                        Texto2Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto2Label.AutoSize = false;
                        Texto2Label.Size = new Size(324, 19);
                        Texto2Label.Location = new Point(4, 50);
                        Texto2Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Bold);
                        Texto2Label.ForeColor = Publicas._fonte;
                        Texto2Label.SendToBack();

                        Texto3Label = new Label();
                        Texto3Label.Text = "Colaborador " + item.NomeColaborador;
                        Texto3Label.TextAlign = ContentAlignment.MiddleLeft;
                        Texto3Label.AutoSize = false;
                        Texto3Label.Size = new Size(324, 19);
                        Texto3Label.Location = new Point(4, 72);
                        Texto3Label.Font = new Font("Century Gothic", (float)10.25, FontStyle.Regular);
                        Texto3Label.ForeColor = Publicas._fonte;
                        Texto3Label.SendToBack();

                        ExibeNotificacaoPanel.Controls.Add(TituloNotificacaoPanel);
                        ExibeNotificacaoPanel.Controls.Add(Texto1Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto2Label);
                        ExibeNotificacaoPanel.Controls.Add(Texto3Label);

                        TextosNotificacaoPanel.Controls.Add(ExibeNotificacaoPanel);
                        topComunicado = topComunicado + (ExibeNotificacaoPanel.Height + 2);
                    }
                }
                #endregion

                
                _notificacaoEmPesquisa = false;
                _notificacaoBolaoEmPesquisa = false;

            }
            catch {            }
            finally
            {
                MostraNotificacoesPanel.BringToFront();
                MostraNotificacoesPanel.Visible = true;

                _quantidadeNotificacoesNaoLidas = 0;
                QuantidadeNotificacaoLabel.Text = "";
            }
        }

        private void LimpaComponentesNotificacao()
        {
            foreach (Control controle in TextosNotificacaoPanel.Controls)
            {
                try
                {
                    controle.Dispose();
                }
                catch { }
            }

            if (TextosNotificacaoPanel.Controls.Count != 0)
            {
                LimpaComponentesNotificacao();
                return;
            }
        }

        private void NotificacaoGrid_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            Record dr;
            try
            { // buscar da empresa do usuario
                if (e.TableCellIdentity.RowIndex != -1)
                {
                    GridRecordRow rec = this.NotificacaoGrid.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        dr = rec.GetRecord() as Record;
                        if (dr != null && dr["Tipo"].ToString().Contains("Extras"))
                        {
                            e.Style.TextColor = Color.Blue;
                        }
                        if (dr != null && dr["Tipo"].ToString().Contains("Incompletas"))
                        {
                            e.Style.TextColor = Color.Red;
                        }
                    }
                }
            }
            catch { }
        }

        private void notificacaoComunicadoBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _notificacaoEmPesquisa = true;

            try
            {
                _listaComunicadosNotificacao = new ComunicadoBO().ListarNotificacoes(DateTime.Now.Year);
            }
            catch
            {
            }
        }

        private void notificacaoTimer_Tick(object sender, EventArgs e)
        {
            if (Publicas._usuario == null || !Publicas._usuario.AcessaJuridico)
                return;

            notificacaoTimer.Stop();

            try
            {
                if (!_notificacaoEmPesquisa)
                {
                    notificacaoComunicadoBackgroundWorker.RunWorkerAsync();
                }
            }
            catch { }
        }

        private void notificacaoComunicadoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _quantidadeNotificacoesNaoLidas = _quantidadeNotificacoesNaoLidas + _listaComunicadosNotificacao.Count();
            notificacaoTimer.Start();
        }

        private void QuantidadeNotificacaoLabel_MouseHover(object sender, EventArgs e)
        {
            QuantidadeNotificacaoLabel.Cursor = Cursors.Hand;
            QuantidadeNotificacaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);

        }

        private void QuantidadeNotificacaoLabel_MouseLeave(object sender, EventArgs e)
        {   
            QuantidadeNotificacaoLabel.Cursor = Cursors.Default;
            QuantidadeNotificacaoLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
        }

        private void LimparNotificacoesButton_Click(object sender, EventArgs e)
        {
            LimpaComponentesNotificacao();

            try
            {
                _listaArquivei.Clear();
            }
            catch { }
            try
            {
                _listaAniversariantes.Clear();
            }
            catch { }
            try
            {
                _listaComunicadosNotificacao.Clear();
            }
            catch { }
            try
            {
                _listaCorrida.Clear();
            }
            catch { }
            _listaJogosDos3Dias.Clear();
            try
            {
                _listaJogosEncerrados.Clear();
            }
            catch { }
            try
            {
                _listaResenhas.Clear();
            }
            catch { }
            try
            {
                _listaResenhasNaoCadastradas.Clear();
            }
            catch { }
            try
            {
                _listaEmprestimos.Clear();
            }
            catch { }
            try
            {
                _listaResenhas.Clear();
            }
            catch { }

            MostraNotificacoesPanel.Visible = false;
        }
        #endregion

        #region Bolão
        private void BolaoCopaPanel_Click(object sender, EventArgs e)
        {
            FechaMenuSistema();
            FechaMenuUsuario();

            if (MenuBolaoPanel != null)
            {
                FechaSubMenuBolao();
                return;
            }

            if (Publicas._usuario == null)
                return;

            MenuBolaoPanel = new Panel();
            MenuBolaoPanel.Size = new Size(BolaoCopaPanel.Width, 25 * (Publicas._usuario.AdministraBolaoCopa ? 7 : 3));
            MenuBolaoPanel.Location = new Point(BolaoCopaPanel.Left, AcessoAoMenuPanel.Height);
            MenuBolaoPanel.BackColor = Color.Silver;
            this.Controls.Add(MenuBolaoPanel);
            MenuBolaoPanel.BringToFront();
            MenuBolaoPanel.Visible = true;

            #region Seleção
            SelecaoPanel = new Panel();
            SelecaoPanel.Font = CorFontepadraoLabel.Font;
            SelecaoPanel.Size = new Size(0,25);
            SelecaoPanel.Dock = DockStyle.Top;
            SelecaoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            SelecaoLabel = new Label();
            SelecaoLabel.AutoSize = false;
            SelecaoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            SelecaoLabel.Dock = DockStyle.Fill;
            SelecaoLabel.Text = "Seleções";
            SelecaoLabel.Font = CorFontepadraoLabel.Font;
            SelecaoLabel.ForeColor = Color.WhiteSmoke;
            SelecaoLabel.TextAlign = ContentAlignment.MiddleLeft;
            SelecaoLabel.MouseHover += new EventHandler(this.SelecaoPanel_MouseHover);
            SelecaoLabel.Click += new System.EventHandler(this.SelecaoLabel_Click);
            SelecaoLabel.Name = "SelecaoLabel";

            DivisoriaSelecaoImagem = new PictureBox();
            DivisoriaSelecaoImagem.Size = new Size(0, 2);
            DivisoriaSelecaoImagem.Dock = DockStyle.Bottom;
            DivisoriaSelecaoImagem.BackColor = Color.Silver;

            SelecaoPanel.Controls.Add(SelecaoLabel);
            SelecaoPanel.Controls.Add(DivisoriaSelecaoImagem);

            MenuBolaoPanel.Controls.Add(SelecaoPanel);
            #endregion

            #region Jogos
            JogosPanel = new Panel();
            JogosPanel.Font = CorFontepadraoLabel.Font;
            JogosPanel.Size = new Size(0, 25);
            JogosPanel.Dock = DockStyle.Top;
            JogosPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            JogosLabel = new Label();
            JogosLabel.AutoSize = false;
            JogosLabel.Size = new Size(_widthLabelMenuSistema, 0);
            JogosLabel.Dock = DockStyle.Fill;
            JogosLabel.Text = "Jogos";
            JogosLabel.Font = CorFontepadraoLabel.Font;
            JogosLabel.ForeColor = Color.WhiteSmoke;
            JogosLabel.TextAlign = ContentAlignment.MiddleLeft;
            JogosLabel.MouseHover += new EventHandler(this.SelecaoPanel_MouseHover);
            JogosLabel.Click += new System.EventHandler(this.JogosLabel_Click);
            JogosLabel.Name = "JogosLabel";

            DivisoriaJogosImagem = new PictureBox();
            DivisoriaJogosImagem.Size = new Size(0, 2);
            DivisoriaJogosImagem.Dock = DockStyle.Bottom;
            DivisoriaJogosImagem.BackColor = Color.Silver;

            JogosPanel.Controls.Add(JogosLabel);
            JogosPanel.Controls.Add(DivisoriaJogosImagem);

            MenuBolaoPanel.Controls.Add(JogosPanel);
            #endregion

            #region Resultado
            ResultadoBolaoPanel = new Panel();
            ResultadoBolaoPanel.Font = CorFontepadraoLabel.Font;
            ResultadoBolaoPanel.Size = new Size(0, 25);
            ResultadoBolaoPanel.Dock = DockStyle.Top;
            ResultadoBolaoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            ResultadoBolaoLabel = new Label();
            ResultadoBolaoLabel.AutoSize = false;
            ResultadoBolaoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ResultadoBolaoLabel.Dock = DockStyle.Fill;
            ResultadoBolaoLabel.Text = "Resultados";
            ResultadoBolaoLabel.Font = CorFontepadraoLabel.Font;
            ResultadoBolaoLabel.ForeColor = Color.WhiteSmoke;
            ResultadoBolaoLabel.TextAlign = ContentAlignment.MiddleLeft;
            ResultadoBolaoLabel.MouseHover += new EventHandler(this.SelecaoPanel_MouseHover);
            ResultadoBolaoLabel.Click += new System.EventHandler(this.ResultadoBolaoLabel_Click);
            ResultadoBolaoLabel.Name = "ResultadosLabel";

            DivisoriaResultadoBolaoImagem = new PictureBox();
            DivisoriaResultadoBolaoImagem.Size = new Size(0, 2);
            DivisoriaResultadoBolaoImagem.Dock = DockStyle.Bottom;
            DivisoriaResultadoBolaoImagem.BackColor = Color.Silver;

            ResultadoBolaoPanel.Controls.Add(ResultadoBolaoLabel);
            ResultadoBolaoPanel.Controls.Add(DivisoriaResultadoBolaoImagem);

            MenuBolaoPanel.Controls.Add(ResultadoBolaoPanel);
            #endregion

            #region Palpites Finalistas
            PalpitesFinalistasPanel = new Panel();
            PalpitesFinalistasPanel.Font = CorFontepadraoLabel.Font;
            PalpitesFinalistasPanel.Size = new Size(0, 25);
            PalpitesFinalistasPanel.Dock = DockStyle.Top;
            PalpitesFinalistasPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            PalpitesFinalistasLabel = new Label();
            PalpitesFinalistasLabel.AutoSize = false;
            PalpitesFinalistasLabel.Size = new Size(_widthLabelMenuSistema, 0);
            PalpitesFinalistasLabel.Dock = DockStyle.Fill;
            PalpitesFinalistasLabel.Text = "Palpites de Finalistas";
            PalpitesFinalistasLabel.Font = CorFontepadraoLabel.Font;
            PalpitesFinalistasLabel.ForeColor = Color.WhiteSmoke;
            PalpitesFinalistasLabel.TextAlign = ContentAlignment.MiddleLeft;
            PalpitesFinalistasLabel.MouseHover += new EventHandler(this.SelecaoPanel_MouseHover);
            PalpitesFinalistasLabel.Click += new System.EventHandler(this.PalpitesFinalistasLabel_Click);
            PalpitesFinalistasLabel.Name = "PalpitesFinalistasLabel";

            DivisoriaPalpitesFinalistasImagem = new PictureBox();
            DivisoriaPalpitesFinalistasImagem.Size = new Size(0, 2);
            DivisoriaPalpitesFinalistasImagem.Dock = DockStyle.Bottom;
            DivisoriaPalpitesFinalistasImagem.BackColor = Color.Silver;

            PalpitesFinalistasPanel.Controls.Add(PalpitesFinalistasLabel);
            PalpitesFinalistasPanel.Controls.Add(DivisoriaPalpitesFinalistasImagem);

            MenuBolaoPanel.Controls.Add(PalpitesFinalistasPanel);
            #endregion

            #region Palpites de placar
            PalpitesPlacarPanel = new Panel();
            PalpitesPlacarPanel.Font = CorFontepadraoLabel.Font;
            PalpitesPlacarPanel.Size = new Size(0, 25);
            PalpitesPlacarPanel.Dock = DockStyle.Top;
            PalpitesPlacarPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            PalpitesPlacarLabel = new Label();
            PalpitesPlacarLabel.AutoSize = false;
            PalpitesPlacarLabel.Size = new Size(_widthLabelMenuSistema, 0);
            PalpitesPlacarLabel.Dock = DockStyle.Fill;
            PalpitesPlacarLabel.Text = "Palpites de Placar";
            PalpitesPlacarLabel.Font = CorFontepadraoLabel.Font;
            PalpitesPlacarLabel.ForeColor = Color.WhiteSmoke;
            PalpitesPlacarLabel.TextAlign = ContentAlignment.MiddleLeft;
            PalpitesPlacarLabel.MouseHover += new EventHandler(this.SelecaoPanel_MouseHover);
            PalpitesPlacarLabel.Click += new System.EventHandler(this.PalpitesPlacarLabel_Click);
            PalpitesPlacarLabel.Name = "PalpitesPlacarLabel";

            DivisoriaPalpitesPlacarImagem = new PictureBox();
            DivisoriaPalpitesPlacarImagem.Size = new Size(0, 2);
            DivisoriaPalpitesPlacarImagem.Dock = DockStyle.Bottom;
            DivisoriaPalpitesPlacarImagem.BackColor = Color.Silver;

            PalpitesPlacarPanel.Controls.Add(PalpitesPlacarLabel);
            PalpitesPlacarPanel.Controls.Add(DivisoriaPalpitesPlacarImagem);

            MenuBolaoPanel.Controls.Add(PalpitesPlacarPanel);
            #endregion

            #region Ranking
            RankingBolaoPanel = new Panel();
            RankingBolaoPanel.Font = CorFontepadraoLabel.Font;
            RankingBolaoPanel.Size = new Size(0, 25);
            RankingBolaoPanel.Dock = DockStyle.Top;
            RankingBolaoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            RankingBolaoLabel = new Label();
            RankingBolaoLabel.AutoSize = false;
            RankingBolaoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            RankingBolaoLabel.Dock = DockStyle.Fill;
            RankingBolaoLabel.Text = "Ranking";
            RankingBolaoLabel.Font = CorFontepadraoLabel.Font;
            RankingBolaoLabel.ForeColor = Color.WhiteSmoke;
            RankingBolaoLabel.TextAlign = ContentAlignment.MiddleLeft;
            RankingBolaoLabel.MouseHover += new EventHandler(this.SelecaoPanel_MouseHover);
            RankingBolaoLabel.Click += new System.EventHandler(this.RankingLabel_Click);
            RankingBolaoLabel.Name = "RankingLabel";

            DivisoriaRankingBolaoImagem = new PictureBox();
            DivisoriaRankingBolaoImagem.Size = new Size(0, 2);
            DivisoriaRankingBolaoImagem.Dock = DockStyle.Bottom;
            DivisoriaRankingBolaoImagem.BackColor = Color.Silver;

            RankingBolaoPanel.Controls.Add(RankingBolaoLabel);
            RankingBolaoPanel.Controls.Add(DivisoriaRankingBolaoImagem);

            MenuBolaoPanel.Controls.Add(RankingBolaoPanel);
            #endregion

            #region Valor Arrecadado
            ValorArrecadadoPanel = new Panel();
            ValorArrecadadoPanel.Font = CorFontepadraoLabel.Font;
            ValorArrecadadoPanel.Size = new Size(0, 25);
            ValorArrecadadoPanel.Dock = DockStyle.Top;
            ValorArrecadadoPanel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);

            ValorArrecadadoLabel = new Label();
            ValorArrecadadoLabel.AutoSize = false;
            ValorArrecadadoLabel.Size = new Size(_widthLabelMenuSistema, 0);
            ValorArrecadadoLabel.Dock = DockStyle.Fill;
            ValorArrecadadoLabel.Text = "Valor Arrecadado";
            ValorArrecadadoLabel.Font = CorFontepadraoLabel.Font;
            ValorArrecadadoLabel.ForeColor = Color.WhiteSmoke;
            ValorArrecadadoLabel.TextAlign = ContentAlignment.MiddleLeft;
            ValorArrecadadoLabel.MouseHover += new EventHandler(this.SelecaoPanel_MouseHover);
            ValorArrecadadoLabel.Click += new System.EventHandler(this.ValorArrecadadoLabel_Click);
            ValorArrecadadoLabel.Name = "ValorArrecadadoLabel";

            DivisoriaValorArrecadadoImagem = new PictureBox();
            DivisoriaValorArrecadadoImagem.Size = new Size(0, 2);
            DivisoriaValorArrecadadoImagem.Dock = DockStyle.Bottom;
            DivisoriaValorArrecadadoImagem.BackColor = Color.Silver;

            ValorArrecadadoPanel.Controls.Add(ValorArrecadadoLabel); 
            ValorArrecadadoPanel.Controls.Add(DivisoriaValorArrecadadoImagem);

            MenuBolaoPanel.Controls.Add(ValorArrecadadoPanel);
            #endregion

            SelecaoPanel.Visible = Publicas._usuario.AdministraBolaoCopa;
            JogosPanel.Visible = Publicas._usuario.AdministraBolaoCopa;
            ResultadoBolaoPanel.Visible = Publicas._usuario.AdministraBolaoCopa;
            ValorArrecadadoPanel.Visible = Publicas._usuario.AdministraBolaoCopa;
        }

        private void SelecaoPanel_MouseHover(object sender, EventArgs e)
        {
            MudaSelecaoDeCoresSubMenuBolao();
            FechaMenuUsuario();
            FechaMenuSistema();

            if (((Control)sender).Name.Contains("Selecao"))
            {
                SelecaoLabel.Font = new Font(SelecaoLabel.Font, FontStyle.Bold);
                SelecaoLabel.ForeColor = Publicas._fonteBotaoFocado;
                SelecaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Jogos"))
            {
                JogosLabel.Font = new Font(JogosLabel.Font, FontStyle.Bold);
                JogosLabel.ForeColor = Publicas._fonteBotaoFocado;
                JogosLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Finalistas"))
            {
                PalpitesFinalistasLabel.Font = new Font(PalpitesFinalistasLabel.Font, FontStyle.Bold);
                PalpitesFinalistasLabel.ForeColor = Publicas._fonteBotaoFocado;
                PalpitesFinalistasLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Placar"))
            {
                PalpitesPlacarLabel.Font = new Font(PalpitesPlacarLabel.Font, FontStyle.Bold);
                PalpitesPlacarLabel.ForeColor = Publicas._fonteBotaoFocado;
                PalpitesPlacarLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Ranking"))
            {
                RankingBolaoLabel.Font = new Font(RankingBolaoLabel.Font, FontStyle.Bold);
                RankingBolaoLabel.ForeColor = Publicas._fonteBotaoFocado;
                RankingBolaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Valor"))
            {
                ValorArrecadadoLabel.Font = new Font(ValorArrecadadoLabel.Font, FontStyle.Bold);
                ValorArrecadadoLabel.ForeColor = Publicas._fonteBotaoFocado;
                ValorArrecadadoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }

            if (((Control)sender).Name.Contains("Resultado"))
            {
                ResultadoBolaoLabel.Font = new Font(ResultadoBolaoLabel.Font, FontStyle.Bold);
                ResultadoBolaoLabel.ForeColor = Publicas._fonteBotaoFocado;
                ResultadoBolaoLabel.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
                return;
            }
        }

        private void MudaSelecaoDeCoresSubMenuBolao()
        {
            SelecaoLabel.Font = new Font(SelecaoLabel.Font, SelecaoLabel.Font.Style & ~FontStyle.Bold);
            JogosLabel.Font = new Font(JogosLabel.Font, JogosLabel.Font.Style & ~FontStyle.Bold);
            PalpitesFinalistasLabel.Font = new Font(PalpitesFinalistasLabel.Font, PalpitesFinalistasLabel.Font.Style & ~FontStyle.Bold);
            PalpitesPlacarLabel.Font = new Font(PalpitesPlacarLabel.Font, PalpitesPlacarLabel.Font.Style & ~FontStyle.Bold);
            RankingBolaoLabel.Font = new Font(RankingBolaoLabel.Font, RankingBolaoLabel.Font.Style & ~FontStyle.Bold);
            ValorArrecadadoLabel.Font = new Font(ValorArrecadadoLabel.Font, ValorArrecadadoLabel.Font.Style & ~FontStyle.Bold);
            ResultadoBolaoLabel.Font = new Font(ResultadoBolaoLabel.Font, ResultadoBolaoLabel.Font.Style & ~FontStyle.Bold);

            SelecaoLabel.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
            JogosLabel.BackColor = SelecaoLabel.BackColor;
            PalpitesFinalistasLabel.BackColor = SelecaoLabel.BackColor;
            PalpitesPlacarLabel.BackColor = SelecaoLabel.BackColor;
            RankingBolaoLabel.BackColor = SelecaoLabel.BackColor;
            ValorArrecadadoLabel.BackColor = SelecaoLabel.BackColor;
            ResultadoBolaoLabel.BackColor = SelecaoLabel.BackColor;

            SelecaoLabel.ForeColor = Color.WhiteSmoke;
            JogosLabel.ForeColor = Color.WhiteSmoke;
            PalpitesFinalistasLabel.ForeColor = Color.WhiteSmoke;
            PalpitesPlacarLabel.ForeColor = Color.WhiteSmoke;
            RankingBolaoLabel.ForeColor = Color.WhiteSmoke;
            ValorArrecadadoLabel.ForeColor = Color.WhiteSmoke;
            ResultadoBolaoLabel.ForeColor = Color.WhiteSmoke;
        }

        private void FechaSubMenuBolao()
        {
            if (MenuBolaoPanel != null)
            {
                MenuBolaoPanel.Visible = false;
                this.Controls.Remove(MenuBolaoPanel);
                MenuBolaoPanel.Dispose();
                MenuBolaoPanel = null;
            }

            Refresh();
        }

        private void SelecaoLabel_Click(object sender, EventArgs e)
        {
            FechaSubMenuBolao();
            MensagemSistema();
            tituloSistemaLabel.Text = "Bolão da copa do mundo 2018";
            BolaoCopadoMundo.Selecoes _tela = new BolaoCopadoMundo.Selecoes();

            _tela.ShowDialog();
            NomePadrao();
        }

        private void JogosLabel_Click(object sender, EventArgs e)
        {
            FechaSubMenuBolao();
            MensagemSistema();
            tituloSistemaLabel.Text = "Bolão da copa do mundo 2018";
            BolaoCopadoMundo.Jogos _tela = new BolaoCopadoMundo.Jogos();

            _tela.ShowDialog();
            NomePadrao();
        }

        private void PalpitesFinalistasLabel_Click(object sender, EventArgs e)
        {
            FechaSubMenuBolao();
            MensagemSistema();
            tituloSistemaLabel.Text = "Bolão da copa do mundo 2018";
            BolaoCopadoMundo.PalpiteFinal _tela = new BolaoCopadoMundo.PalpiteFinal();

            _tela.ShowDialog();
            NomePadrao();
        }

        private void PalpitesPlacarLabel_Click(object sender, EventArgs e)
        {
            FechaSubMenuBolao();
            MensagemSistema();
            tituloSistemaLabel.Text = "Bolão da copa do mundo 2018";
            BolaoCopadoMundo.PalpiteJogos _tela = new BolaoCopadoMundo.PalpiteJogos();

            _tela.ShowDialog();
            NomePadrao();
        }

        private void RankingLabel_Click(object sender, EventArgs e)
        {
            FechaSubMenuBolao();
            MensagemSistema();
            tituloSistemaLabel.Text = "Bolão da copa do mundo 2018";
            BolaoCopadoMundo.Ranking _tela = new BolaoCopadoMundo.Ranking();
            _tela.Size = new Size(_tela.Width, this.Height - (tituloPanel.Height + panel2.Height + 2));

            _tela.ShowDialog();
            NomePadrao();
        }

        private void ValorArrecadadoLabel_Click(object sender, EventArgs e)
        {
            FechaSubMenuBolao();
            MensagemSistema();
            tituloSistemaLabel.Text = "Bolão da copa do mundo 2018";
            BolaoCopadoMundo.ValorArrecadado _tela = new BolaoCopadoMundo.ValorArrecadado();

            _tela.ShowDialog();
            NomePadrao();
        }

        private void ResultadoBolaoLabel_Click(object sender, EventArgs e)
        {
            FechaSubMenuBolao();
            MensagemSistema();
            tituloSistemaLabel.Text = "Bolão da copa do mundo 2018";
            BolaoCopadoMundo.ResultadosJogos _tela = new BolaoCopadoMundo.ResultadosJogos();

            _tela.ShowDialog();
            NomePadrao();
        }

        private void BolaoCopaPanel_MouseHover(object sender, EventArgs e)
        {
            BolaoCopaPanel.Cursor = Cursors.Hand;
            BolaoCopaPanel.BackColor = Publicas._panelTituloFoco;
        }

        private void BolaoCopaPanel_MouseLeave(object sender, EventArgs e)
        {
            BolaoCopaPanel.Cursor = Cursors.Default;
            BolaoCopaPanel.BackColor = Publicas._panelTitulo;
        }

        private void BolaoCopaBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!_notificacaoBolaoEmPesquisa)
            {
                _notificacaoBolaoEmPesquisa = true;
                Publicas._jogosNaoFinalizados = false;
                _listaJogosEncerrados = new BolaoJogosBO().Listar(DateTime.Now.Year, true);
                _listaJogosDos3Dias = new BolaoJogosBO().ListarJogosSemPalpites(DateTime.Now.Year);
            }
        }

        private void BolaoCopaBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((DateTime.Now.Date >= Publicas._dataInicioCopa.AddDays(-14) && DateTime.Now.Date <= Publicas._dataFimCopa.AddDays(3)))
            {
                int qtdDias = 1;

                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                    qtdDias = 3;

                _quantidadeNotificacoesNaoLidas = _quantidadeNotificacoesNaoLidas + _listaJogosEncerrados.Where(w => w.Data.Date >= DateTime.Now.Date.AddDays(-qtdDias)).Count();

                qtdDias = 1;

                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                    qtdDias = 3;

                _quantidadeNotificacoesNaoLidas = _quantidadeNotificacoesNaoLidas + _listaJogosDos3Dias.Where(w => w.Data.Date >= DateTime.Now && w.Data.Date <= DateTime.Now.Date.AddDays(qtdDias)).Count();
            }
            notificacaoBolaoTimer.Start();
        }

        private void notificacaoBolaoTimer_Tick(object sender, EventArgs e)
        {
            notificacaoBolaoTimer.Stop();

            if (Publicas._usuario == null || !Publicas._usuario.ParticipaBolaoCopa)
                return;

            try
            {
                if (!_notificacaoBolaoEmPesquisa)
                {
                    BolaoCopaBackgroundWorker.RunWorkerAsync();
                }
            }
            catch { }
        }


        #endregion

        private void BolaoCopaPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ArquiveiBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _listaParametrosArquivei = new ParametrosArquiveiBO().Listar();
            _listaItensParametros = new ItensParametrosArquiveiBO().Listar(0);
            _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
            DirectoryInfo dirInfo;

            foreach (var item in _listaParametrosArquivei)
            {
                dirInfo = new DirectoryInfo(item.Diretorio);

                if (dirInfo.Exists)
                {
                    FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                    foreach (FileSystemInfo file in files)
                    {
                        if (!file.FullName.Contains("Processado") && !file.FullName.Contains("~$"))
                            _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei() { IdEmpresa = item.IdEmpresa, NomeArquivo = file.FullName, Acao = item.AcaoComArquivo });
                    }
                }
            }

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            Excel.Range range;
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();

            _listaItensArquivei = new List<ItensArquivei>();
            _listaArquivei = new List<Arquivei>();

            string str;
            int rCnt;
            int cCnt;
            int rw = 0;
            int cl = 0;

            string cnpjdestinatario = "";
            string iedestinatario = "";
            string enderecodestinatario = "";
            string bairrodestinatario = "";
            string cepdestinatario = "";
            string razaosocialdestinatario = "";
            string cnpjemitente = "";
            string ieemitente = "";
            string razaosocialemitente = "";
            DateTime dataemissao = DateTime.MinValue;
            int numeronf = 0;
            string modelonf = "";
            string serie = "";
            string naturezaoperacao = "";
            decimal valortotalnf = 0;
            decimal valorproduto = 0;
            decimal baseicms = 0;
            string dadosadicionais = "";
            string numeroenddestinatario = "";
            string tipo = "";
            string status = "";
            string operacao = "";


            _arquiveiEmPesquisa = true;

            #region Leitura/Gravação arquivo excel.
            int idArquivo = 1;

            foreach (var item in _arquivosPorEmpresa)
            {
                string chaveAcesso = "";
                
                xlApp = new Excel.Application();

                xlWorkBook = xlApp.Workbooks.Open(item.NomeArquivo, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;

                // linha um é o cabeçalho
                for (rCnt = 2; rCnt <= rw; rCnt++)
                {
                    _arquivei = new Arquivei();
                    _arquivei.IdEmpresa = item.IdEmpresa;
                    _arquivei.NomeArquivo = item.NomeArquivo;

                    _itens = new ItensArquivei();

                    for (cCnt = 1; cCnt <= cl; cCnt++)
                    {
                        try
                        {
                            str = Convert.ToString((range.Cells[rCnt, cCnt] as Excel.Range).Value2);
                            _itens.IdArquivei = idArquivo;

                            switch (cCnt)
                            {
                                case 1:
                                    if (str != chaveAcesso &&  chaveAcesso != "") // grava o anterior
                                    {
                                        _arquivei.Id = idArquivo;
                                        _arquivei.RazaoSocialDestinatario = razaosocialdestinatario;
                                        _arquivei.RazaoSocialEmitente = razaosocialemitente;
                                        _arquivei.CNPJDestinatario = cnpjdestinatario;
                                        //_arquivei.CNPJEemitente = cnpjemitente;
                                        _arquivei.IEDestinatario = iedestinatario;
                                        _arquivei.IEEmitente = ieemitente;
                                        _arquivei.EnderecoDestinatario = enderecodestinatario;
                                        _arquivei.NumeroEndDestinatario = numeroenddestinatario;
                                        _arquivei.BairroDestinatario = bairrodestinatario;
                                        //_arquivei.CEPDdestinatario = cepdestinatario;
                                        _arquivei.ChaveDeAcesso = chaveAcesso;
                                        _arquivei.DataEmissao = dataemissao;
                                        _arquivei.NumeroNF = numeronf;
                                        _arquivei.ModeloNF = modelonf;
                                        _arquivei.Serie = serie;
                                        _arquivei.Tipo = tipo;
                                        _arquivei.Status = status;
                                        _arquivei.Operacao = operacao;
                                        _arquivei.NaturezaOperacao = naturezaoperacao;
                                        _arquivei.ValorProduto = valorproduto;
                                        _arquivei.ValorTotalNF = valortotalnf;
                                        _arquivei.BaseICMS = baseicms;
                                        _arquivei.DadosAdicionais = dadosadicionais;
                                        _itens.IdArquivei = idArquivo;

                                        _listaArquivei.Add(_arquivei);
                                        idArquivo++;

                                        _arquivei = new Arquivei();
                                    }
                                    chaveAcesso = str;
                                    break;
                                case 2:
                                    razaosocialdestinatario = str;
                                    break;
                                case 3:
                                    cnpjdestinatario = str;
                                    break;
                                case 4:
                                    iedestinatario = str;
                                    break;
                                case 5:
                                    enderecodestinatario = str;
                                    break;
                                case 6:
                                    numeroenddestinatario = str;
                                    break;
                                case 7:
                                    bairrodestinatario = str;
                                    break;
                                case 8:
                                    cepdestinatario = str;
                                    break;
                                case 9:
                                    razaosocialemitente = str;
                                    break;
                                case 10:
                                    cnpjemitente = str;
                                    break;
                                case 11:
                                    ieemitente = str;
                                    break;
                                case 12:
                                    try
                                    {
                                        dataemissao = DateTime.FromOADate(double.Parse(str));
                                    }
                                    catch { }                                    
                                    break;
                                case 13:
                                    try
                                    { 
                                    numeronf = Convert.ToInt32(str);
                                    }
                                    catch {}                                    
                                    break;
                                case 14:
                                    modelonf = str;
                                    break;
                                case 15:
                                    serie = str;
                                    break;
                                case 16:
                                    tipo = str;
                                    break;
                                case 17:
                                    status = str;
                                    break;
                                case 18:
                                    naturezaoperacao = str;
                                    break;
                                case 19:
                                    operacao = str;
                                    break;
                                case 20:
                                    try
                                    {
                                        valortotalnf = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 21:
                                    try
                                    {
                                        valorproduto = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 22:
                                    try
                                    {
                                        baseicms = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 23:
                                    try
                                    {
                                        _itens.ValorICMS = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 24:
                                    try
                                    {
                                        _itens.AliquotaICMS = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 25:
                                    try
                                    {
                                        _itens.ValorICMSSub = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 26:
                                    try
                                    {
                                        _itens.ValorIPI = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 27:
                                    try
                                    {
                                        _itens.Desconto = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 28:
                                    try
                                    {
                                        _itens.Seguro = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 29:
                                    try
                                    {
                                        _itens.OutrasDespesas = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 30:
                                    try
                                    {
                                        _itens.ValorFrete = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                                case 31:
                                    _itens.CCe = str;
                                    break;
                                case 32:
                                    dadosadicionais = str;
                                    break;
                                case 33:
                                    _itens.CST = str;
                                    break;
                                case 34:
                                    _itens.CSTICMS = str;
                                    break;
                                case 35:
                                    try
                                    {
                                        _itens.CFOP = Convert.ToInt32(str);
                                    }
                                    catch { }
                                    break;
                                case 36:
                                    try
                                    {
                                        _itens.ValorTotal = Convert.ToDecimal(str);
                                    }
                                    catch { }
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            //new Notificacoes.Mensagem(ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
                        }
                    }
                    _listaItensArquivei.Add(_itens);
                }

                if (_listaArquivei.Where(w => w.ChaveDeAcesso == chaveAcesso).Count() == 0)
                {
                    _arquivei.IdEmpresa = item.IdEmpresa;
                    _arquivei.NomeArquivo = item.NomeArquivo;
                    _arquivei.Id = idArquivo;
                    _arquivei.RazaoSocialDestinatario = razaosocialdestinatario;
                    _arquivei.RazaoSocialEmitente = razaosocialemitente;
                    _arquivei.CNPJDestinatario = cnpjdestinatario;
                    //_arquivei.CNPJEemitente = cnpjemitente;
                    _arquivei.IEDestinatario = iedestinatario;
                    _arquivei.IEEmitente = ieemitente;
                    _arquivei.EnderecoDestinatario = enderecodestinatario;
                    _arquivei.NumeroEndDestinatario = numeroenddestinatario;
                    _arquivei.BairroDestinatario = bairrodestinatario;
                    //_arquivei.CEPDdestinatario = cepdestinatario;
                    _arquivei.ChaveDeAcesso = chaveAcesso;
                    _arquivei.DataEmissao = dataemissao;
                    _arquivei.NumeroNF = numeronf;
                    _arquivei.ModeloNF = modelonf;
                    _arquivei.Serie = serie;
                    _arquivei.Tipo = tipo;
                    _arquivei.Status = status;
                    _arquivei.Operacao = operacao;
                    _arquivei.NaturezaOperacao = naturezaoperacao;
                    _arquivei.ValorProduto = valorproduto;
                    _arquivei.ValorTotalNF = valortotalnf;
                    _arquivei.BaseICMS = baseicms;
                    _arquivei.DadosAdicionais = dadosadicionais;
                    _itens.IdArquivei = idArquivo;
                    _listaArquivei.Add(_arquivei);

                    // para começar a leitura do novo arquivo se existir
                    idArquivo++;
                    chaveAcesso = "";
                }

                #region Ação a ser tomada com o arquivo Arquivei
                if (item.Acao == "E")
                {
                    try
                    {
                        File.Delete(item.NomeArquivo);

                        Log _log = new Log();
                        _log.IdUsuario = Publicas._usuario.Id;
                        _log.Descricao = "Excluiu o arquivo '" + item.NomeArquivo + "', após processamento do Arquivei, conforme parametrizado";
                        _log.Tela = "Principal";

                        try
                        {
                            new LogBO().Gravar(_log);
                        }
                        catch { }
                    }
                    catch (IOException ex)
                    {
                        Log _log = new Log();
                        _log.IdUsuario = Publicas._usuario.Id;
                        _log.Descricao = "Não foi possível Excluiu o arquivo '" + item.NomeArquivo + "', após processamento do Arquivei, conforme parametrizado. tentaremos renomear. [ erro apresentado: " +
                            ex.Message + " ]";
                        _log.Tela = "Principal";

                        try
                        {
                            new LogBO().Gravar(_log);
                        }
                        catch { }

                        try
                        {
                            File.Move(item.NomeArquivo, item.NomeArquivo.Replace("xlsx", "xlsxProcessado"));
                        }
                        catch 
                        {

                            _log = new Log();
                            _log.IdUsuario = Publicas._usuario.Id;
                            _log.Descricao = "Não foi possível Remonear o arquivo '" + item.NomeArquivo + "', após processamento do Arquivei, conforme parametrizado.";
                            _log.Tela = "Principal";

                            try
                            {
                                new LogBO().Gravar(_log);
                            }
                            catch { }
                        }
                    }
                }
                else
                {
                    try
                    {
                        File.Move(item.NomeArquivo, item.NomeArquivo.Replace("xlsx", "xlsxProcessado"));
                    }
                    catch
                    {

                        Log _log = new Log();
                        _log.IdUsuario = Publicas._usuario.Id;
                        _log.Descricao = "Não foi possível Remonear o arquivo '" + item.NomeArquivo + "', após processamento do Arquivei, conforme parametrizado.";
                        _log.Tela = "Principal";

                        try
                        {
                            new LogBO().Gravar(_log);
                        }
                        catch { }
                    }
                }
                #endregion
            }

            if (_listaArquivei.Count != 0)
                new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);
            #endregion

        }

        private void ArquiveiBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _arquiveiEmPesquisa = false;
            //_quantidadeNotificacoesNaoLidas = _quantidadeNotificacoesNaoLidas +  new ArquiveiBO().Importados().Count();
        }

        private void BibliotecaBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _bibliotecaEmPesquisa = true;

            if (Publicas._usuario.IdEmpresa != 1) // apenas para NIFF
               return;

            if (Publicas._usuario.Administrador ||
                Publicas._usuario.Departamento.Contains("RH") || Publicas._usuario.Departamento.ToUpper().Contains("Recursos Humanos".ToUpper()))
            {
                _listaEmprestimos = new EmprestimoLivrosBO().Listar(true);
                _listaResenhas = new ResenhaLivrosBO().Listar(false, 0);
                _listaResenhasNaoCadastradas = new ResenhaLivrosBO().ListarLivroSemResenha(0);
                _listaReservas = new ReservaLivrosBO().Listar();
            }
            else
            {
                _listaEmprestimos = new EmprestimoLivrosBO().Listar(true, Publicas._idColaborador);
                _listaResenhas = new ResenhaLivrosBO().Listar(true, Publicas._idColaborador);
                _listaResenhasNaoCadastradas = new ResenhaLivrosBO().ListarLivroSemResenha(Publicas._idColaborador);
            }            
        }

        private void BibliotecaBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _bibliotecaEmPesquisa = false;

            if (Publicas._usuario.IdEmpresa != 1) // apenas para NIFF
                return;

            if (quantidadeBiblioteca != _listaEmprestimos.Count + _listaReservas.Count + _listaResenhas.Count + _listaResenhasNaoCadastradas.Count())
            {
                quantidadeBiblioteca = _listaEmprestimos.Count + _listaReservas.Count + _listaResenhas.Count + _listaResenhasNaoCadastradas.Count();
                _quantidadeNotificacoesNaoLidas = quantidadeBiblioteca;
            }

            NotificacaoBibliotecaTimer.Start();
        }

        private void NotificaoBibliotecaTimer_Tick(object sender, EventArgs e)
        {
            NotificacaoBibliotecaTimer.Stop();

            if (Publicas._usuario == null || Publicas._usuario.IdEmpresa != 1)
                return;

            try
            {
                if (!_bibliotecaEmPesquisa)
                {
                    BibliotecaBackgroundWorker.RunWorkerAsync();
                }
            }
            catch { }
        }

        private void BolaoPictureBox_Click(object sender, EventArgs e)
        {
            List<BolaoPalpitesDosColaboradores> _listaApostas = new List<BolaoPalpitesDosColaboradores>();
            List<BolaoPalpiteFinalDoColaborador> _listaPalpiteFinal = new List<BolaoPalpiteFinalDoColaborador>();
            ApostasPanel.Size = new Size(661, 0);
            ApostasPanel.BringToFront();
            ApostasPanel.Dock = DockStyle.Right;
            filter.WireGrid(ComunidadosDashBoardGrid);

            if (!ApostasPanel.Visible)
            {
                ApostasBolaoGrid.SortIconPlacement = SortIconPlacement.Left;
                ApostasBolaoGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
                ApostasBolaoGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
                ApostasBolaoGrid.TableControl.CellToolTip.Active = true;
                ApostasBolaoGrid.TopLevelGroupOptions.ShowFilterBar = true;
                ApostasBolaoGrid.SetMetroStyle(metroColor);

                // para permitir editar dados.
                ApostasBolaoGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
                ApostasBolaoGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                ApostasBolaoGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
                ApostasBolaoGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

                _listaApostas = new BolaoPalpitesDosColaboradoresBO().AcompanharJogos(DateTime.Now.Year);

                ApostasBolaoGrid.DataSource = _listaApostas;

                FinalistasGrid.SortIconPlacement = SortIconPlacement.Left;
                FinalistasGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
                FinalistasGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
                FinalistasGrid.TableControl.CellToolTip.Active = true;
                FinalistasGrid.TopLevelGroupOptions.ShowFilterBar = true;
                FinalistasGrid.SetMetroStyle(metroColor);

                // para permitir editar dados.
                FinalistasGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
                FinalistasGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                FinalistasGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
                FinalistasGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

                if (DateTime.Now.Date > new DateTime(2018, 06, 29))
                    _listaPalpiteFinal = new BolaoPalpiteFinalDoColaboradorBO().Listar(DateTime.Now.Year);

                FinalistasGrid.DataSource = _listaPalpiteFinal;
                FinalistasTabPage.TabVisible = DateTime.Now.Date > new DateTime(2018, 06, 29);
            }

            ApostasPanel.Visible = !ApostasPanel.Visible && (_listaApostas.Count() > 0 || _listaPalpiteFinal.Count() > 0);
        }

        private void tituloMinimizarLabel_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void tituloMinimizarLabel_MouseHover(object sender, EventArgs e)
        {
            tituloMinimizarLabel.Cursor = Cursors.Hand;
            tituloMinimizarLabel.BackColor = Publicas._panelTituloFoco;
        }

        private void tituloMinimizarLabel_MouseLeave(object sender, EventArgs e)
        {
            tituloMinimizarLabel.Cursor = Cursors.Default;
            tituloMinimizarLabel.BackColor = Publicas._panelTitulo;
        }

        private void BolaoPictureBox_MouseHover(object sender, EventArgs e)
        {
            BolaoPictureBox.Cursor = Cursors.Hand;
            BolaoPictureBox.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
        }

        private void BolaoPictureBox_MouseLeave(object sender, EventArgs e)
        {
            BolaoPictureBox.Cursor = Cursors.Default;
            BolaoPictureBox.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
        }

        private void EnquetePictureBox_Click(object sender, EventArgs e)
        {
            FechaSubMenuBolao();
            ApostasPanel.Visible = false;

            EnquetePanel.Dock = DockStyle.Right;
            EnquetePanel.Size = new Size(434, 0);
            DashBoardPanel.Size = new Size(25, 0);
            BotaoDashboardLabel.Text = "7";

            EnquetePanel.BringToFront();
            EnquetePanel.Visible = !EnquetePanel.Visible;
            buttonAdv1.Visible = !new BolaoEnqueteBO().Consultar(Publicas._idColaborador);

            comboBoxAdv1.Enabled = Publicas._usuario.ParticipaBolaoCopa;
            checkBoxAdv1.Enabled = Publicas._usuario.ParticipaBolaoCopa;
            checkBoxAdv2.Enabled = Publicas._usuario.ParticipaBolaoCopa;
            checkBoxAdv3.Enabled = Publicas._usuario.ParticipaBolaoCopa;
            checkBoxAdv4.Enabled = Publicas._usuario.ParticipaBolaoCopa;

            if (!buttonAdv1.Visible)
            {
                List<BolaoEnquete> _lista = new BolaoEnqueteBO().Listar(Publicas._idColaborador);

                foreach (var item in _lista.OrderBy(o => o.IdPergunta))
                {
                    if (item.IdPergunta.Equals(1))
                        comboBoxAdv1.Text = (item.Opcao.Equals("S") ? "Sim" : "Não");

                    if (item.IdPergunta.Equals(3))
                        checkBoxAdv1.Checked = item.Opcao.Equals("S");

                    if (item.IdPergunta.Equals(4))
                        checkBoxAdv2.Checked = item.Opcao.Equals("S");

                    if (item.IdPergunta.Equals(5))
                        checkBoxAdv3.Checked = item.Opcao.Equals("S");

                    if (item.IdPergunta.Equals(6))
                        checkBoxAdv4.Checked = item.Opcao.Equals("S");

                    if (item.IdPergunta.Equals(7))
                    {
                        comboBoxAdv2.Text = (item.Opcao.Equals("S") ? "Sim" : "Não");
                        comentarioTextBox.Text = item.Sugestao;
                    }
                }

                #region Mostrar Grafico
                int[] _qtd = new BolaoEnqueteBO().QuantidadeGostouBolao();
                progressBarAdv1.Maximum = _qtd[2];
                progressBarAdv1.Value = _qtd[0];
                progressBarAdv1.Visible = true;

                _qtd = new BolaoEnqueteBO().QuantidadeMudarDivisaoPremiacao();
                progressBarAdv2.Maximum = _qtd[2];
                progressBarAdv2.Value = _qtd[0];
                progressBarAdv2.Visible = true;

                _qtd = new BolaoEnqueteBO().QuantidadeMudarPontuacao();
                progressBarAdv3.Maximum = _qtd[2];
                progressBarAdv3.Value = _qtd[0];
                progressBarAdv3.Visible = true;

                _qtd = new BolaoEnqueteBO().QuantidadeMudarArtilheiro();
                progressBarAdv4.Maximum = _qtd[2];
                progressBarAdv4.Value = _qtd[0];
                progressBarAdv4.Visible = true;

                _qtd = new BolaoEnqueteBO().QuantidadeMudarPlacarInverso();
                progressBarAdv5.Maximum = _qtd[2];
                progressBarAdv5.Value = _qtd[0];
                progressBarAdv5.Visible = true;

                _qtd = new BolaoEnqueteBO().QuantidadeParticipantes();
                progressBarAdv6.Maximum = _qtd[2];
                progressBarAdv6.Value = _qtd[0];
                progressBarAdv6.Visible = true;

                #endregion
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.URL = @"Imagens\Video_MarioKart.mp4";
        }

        private void label22_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.URL = @"Imagens\Video_ARMS.mp4";
        }

        private void label23_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.URL = @"Imagens\Video_Pinball_mesa1.wmv";
        }

        private void label24_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.URL = @"Imagens\Video_Pinball_mesa2.wmv";
        }

        private void label25_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.URL = @"Imagens\Video_Pinball_mesa3.wmv";
        }

        private void label26_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.URL = @"Imagens\Video_Pinball_mesa4.wmv";
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            #region Gravar
            BolaoEnquete _enquete = new BolaoEnquete();
            if (Publicas._usuario.ParticipaBolaoCopa)
            {
                _enquete.IdColaborador = Publicas._idColaborador;
                _enquete.IdPergunta = Convert.ToInt32(comboBoxAdv1.Tag);
                _enquete.Opcao = comboBoxAdv1.Text.ToUpper().Substring(0, 1);

                new BolaoEnqueteBO().Grava(_enquete);

                _enquete = new BolaoEnquete();

                _enquete.IdColaborador = Publicas._idColaborador;
                _enquete.IdPergunta = Convert.ToInt32(checkBoxAdv1.Tag);
                _enquete.Opcao = (checkBoxAdv1.Checked ? "S" : "N");

                new BolaoEnqueteBO().Grava(_enquete);

                _enquete = new BolaoEnquete();

                _enquete.IdColaborador = Publicas._idColaborador;
                _enquete.IdPergunta = Convert.ToInt32(checkBoxAdv2.Tag);
                _enquete.Opcao = (checkBoxAdv2.Checked ? "S" : "N");

                new BolaoEnqueteBO().Grava(_enquete);

                _enquete = new BolaoEnquete();

                _enquete.IdColaborador = Publicas._idColaborador;
                _enquete.IdPergunta = Convert.ToInt32(checkBoxAdv3.Tag);
                _enquete.Opcao = (checkBoxAdv3.Checked ? "S" : "N");

                new BolaoEnqueteBO().Grava(_enquete);

                _enquete = new BolaoEnquete();

                _enquete.IdColaborador = Publicas._idColaborador;
                _enquete.IdPergunta = Convert.ToInt32(checkBoxAdv4.Tag);
                _enquete.Opcao = (checkBoxAdv4.Checked ? "S" : "N");

                new BolaoEnqueteBO().Grava(_enquete);
            }

            _enquete = new BolaoEnquete();

            _enquete.IdColaborador = Publicas._idColaborador;
            _enquete.IdPergunta = Convert.ToInt32(comboBoxAdv2.Tag);
            _enquete.Opcao = comboBoxAdv2.Text.ToUpper().Substring(0, 1);
            _enquete.Sugestao = comentarioTextBox.Text;

            new BolaoEnqueteBO().Grava(_enquete);
            #endregion

            #region Mostrar Grafico
            int[] _qtd = new BolaoEnqueteBO().QuantidadeGostouBolao();
            progressBarAdv1.Maximum = _qtd[2];
            progressBarAdv1.Value = _qtd[0];
            progressBarAdv1.Visible = true;

            _qtd = new BolaoEnqueteBO().QuantidadeMudarDivisaoPremiacao();
            progressBarAdv2.Maximum = _qtd[2];
            progressBarAdv2.Value = _qtd[0];
            progressBarAdv2.Visible = true;

            _qtd = new BolaoEnqueteBO().QuantidadeMudarPontuacao();
            progressBarAdv3.Maximum = _qtd[2];
            progressBarAdv3.Value = _qtd[0];
            progressBarAdv3.Visible = true;

            _qtd = new BolaoEnqueteBO().QuantidadeMudarArtilheiro();
            progressBarAdv4.Maximum = _qtd[2];
            progressBarAdv4.Value = _qtd[0];
            progressBarAdv4.Visible = true;

            _qtd = new BolaoEnqueteBO().QuantidadeMudarPlacarInverso();
            progressBarAdv5.Maximum = _qtd[2];
            progressBarAdv5.Value = _qtd[0];
            progressBarAdv5.Visible = true;

            _qtd = new BolaoEnqueteBO().QuantidadeParticipantes();
            progressBarAdv6.Maximum = _qtd[2];
            progressBarAdv6.Value = _qtd[0];
            progressBarAdv6.Visible = true;

            #endregion
            buttonAdv1.Visible = false;
        }

        private void label21_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).Font = new Font(((Label)sender).Font, FontStyle.Underline);
            ((Label)sender).Cursor = Cursors.Hand;
        }

        private void label21_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Font = new Font(((Label)sender).Font, ((Label)sender).Font.Style & ~FontStyle.Underline);
            ((Label)sender).Cursor = Cursors.Default;
        }

        private void TorneioPictureBox_Click(object sender, EventArgs e)
        {
            ApostasPanel.Visible = false;

            CadastrarPartidasPanel.Visible = Publicas._usuario.AdministraBolaoCopa;

            List<Classes.Participantes> _listaApostas = new List<Classes.Participantes>();

            TorneioPanel.Size = new Size(461, 0);
            TorneioPanel.BringToFront();
            TorneioPanel.Dock = DockStyle.Right;
            //filter.WireGrid(ApostasBolaoGrid);

            if (!TorneioPanel.Visible)
            {
                TorneioGrid.SortIconPlacement = SortIconPlacement.Left;
                TorneioGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
                TorneioGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
                TorneioGrid.TableControl.CellToolTip.Active = true;

                TorneioGrid.SetMetroStyle(metroColor);

                // para permitir editar dados.
                TorneioGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
                TorneioGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                TorneioGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
                TorneioGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

                _listaApostas = new TorneioBO().ListarClassificacao();

                TorneioGrid.DataSource = _listaApostas;
                TorneioGrid.TopLevelGroupOptions.ShowFilterBar = false;

                for (int i = 0; i < TorneioGrid.TableDescriptor.Relations.Count; i++)
                {

                    TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.RecordRowHeight = 50;
                    TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.ShowRowHeader = false;

                    for (int j = 0; j < TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns.Count; j++)
                    {
                        TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].AllowFilter = false;
                        TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                        TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                        TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(dataHoraLabel.Font.FontFamily, (float)8, FontStyle.Regular));

                        if (TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName != "Torneio" &&
                            !TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.StartsWith("Round") &&
                            TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName != "Total" &&
                            TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName != "Data")

                            TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                        else
                        {
                            TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                            TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                            if (TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.StartsWith("Torneio"))
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Width = 150;

                            if (TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.StartsWith("Round"))
                            {
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Replace("Round", "");
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Width = 40;
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            }
                            if (TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.StartsWith("Total"))
                            {
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            }
                            if (TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.StartsWith("Data"))
                            {
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.FrozenColumn = "Data";
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(dataHoraLabel.Font.FontFamily, (float)8, FontStyle.Bold));
                                try
                                {
                                    TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.SortedColumns.Add("Data");
                                }
                                catch { }
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                                TorneioGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Width = 70;
                            }
                        }
                    }
                }
            }

            TorneioPanel.Visible = !TorneioPanel.Visible;
        }

        private void buttonAdv2_Click(object sender, EventArgs e)
        {
            TorneioPanel.Visible = false;
            new BolaoCopadoMundo.Campeonato().ShowDialog();
        }

        private void TorneioPictureBox_MouseHover(object sender, EventArgs e)
        {
            TorneioPictureBox.Cursor = Cursors.Hand;
            TorneioPictureBox.BackColor = System.Drawing.Color.FromArgb(115, 117, 128);
        }

        private void TorneioPictureBox_MouseLeave(object sender, EventArgs e)
        {
            TorneioPictureBox.Cursor = Cursors.Default;
            TorneioPictureBox.BackColor = System.Drawing.Color.FromArgb(128, 131, 143);
        }
    }

}
