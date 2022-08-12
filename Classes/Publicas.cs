using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Xml;

namespace Classes
{
    public static class Publicas
    {
        #region Enumeradores
        public enum CryptProvider
        {
            Rijndael,
            RC2,
            DES,
            TripleDES
        }

        public enum StatusUsuario
        {
            OnLine = 1,
            Ausente = 2,
            OffLine = 3,
            Ocupado = 4
        }

        public enum TipoUsuario
        {
            Socilitante = 1,
            Atendente = 2,
            BI = 3,
            Todos = 4
        }

        public enum TipoUsuarioSAC
        {
            Administrador = 1,
            Atendente = 2,
            UsuarioComum = 3,
            Finalizador = 4,
            Mediador = 5
        }

        public enum StatusChamado
        {
            [Description("Novo")]
            Novo = 'N',
            [Description("Aguardando solicitante")]
            Pendente = 'P',
            [Description("Aguardando analista")]
            EmAndamento = 'E',
            [Description("Aguardando Terceiros")]
            Adequacao = 'A',
            [Description("Cancelado")]
            Cancelado = 'C',
            [Description("Reaberto")]
            Reaberto = 'R',
            [Description("Finalizado")]
            Finalizado = 'F',
            [Description("Em Desenvolvimento")]
            EmDesenvolvimento = 'D',
            [Description("Aguardando Autorização")]
            AguardandoAutorizacao = 'U',
            [Description("Aguardando Cronograma")]
            AguardandoCronograma = 'G',
            [Description("Aguardando Conserto")]
            AguardandoConserto = 'S'
        }

        public enum Origem
        {
            [Description("Chamado")]
            OnLine = 'O',
            [Description("Telefone")]
            Telefone = 'T',
            [Description("E-mail")]
            Email = 'E',
            [Description("Chat")]
            Chat = 'C'
        }

        public enum Prioridades
        {
            [Description("Crítico")]
            Critico = 'C',
            [Description("Alta")]
            Alta = 'A',
            [Description("Media")]
            Media = 'M',
            [Description("Baixa")]
            Baixa = 'B'
        }

        public enum TipoAdequacao
        {
            SIM = 1,
            PSE = 2
        }

        public enum TipoChamado
        {
            [Description("Dúvidas")]
            Duvida = 1,
            [Description("Erro")]
            Erro = 2,
            [Description("Implementação")]
            Implementacao = 3,
            [Description("Acesso")]
            Acesso = 4,
            [Description("Ajustes")]
            Ajustes = 5,
            [Description("Projeto")]
            Projeto = 6,
            [Description("Solicitação")]
            Solicitacao = 7
        }

        public enum TipoDeTela
        {
            Cadastro = 1,
            [Description("Relatório")]
            Relatorio = 2,
            [Description("Movimentação")]
            Movimentacao = 3,
            [Description("Integraçao")]
            Integracao = 4,
            [Description("Geração de arquivo")]
            GeracaoDeArquivo = 5
        }

        public enum TipoCalculoChamado
        {
            Ano = 0,
            AnoMes = 1,
            Sequencial = 2
        }

        public enum AcessoUsuario
        {
            SAC = 1,
            Chamado = 2,
            SAC_BI = 3
        }

        public enum TipoDeCobranca
        {
            [Description("Com custo")]
            ComCusto = 1,
            [Description("Sem custo")]
            SemCusto = 2,
            [Description("Com custo e desconto")]
            ComCustoDesconto = 3
        }

        public enum TipoAgenda
        {
            [Description("Reserva de Sala de Reunião")]
            SalaDeReuniao = 1,
            [Description("Solicitação de Carro")]
            Carro = 2,
            [Description("Visita")]
            Visita = 3,
            [Description("Treinamento Interno")]
            TreinamentoInterno = 4,
            [Description("Treinamento Externo")]
            TreinamentoExterno = 5,
            [Description("Particular")]
            Particular = 6,
            [Description("Férias")]
            Ferias = 7,
            [Description("Atestado Médico")]
            AtestadoMedico = 8,
            [Description("Todos")]
            Todos = 9,

        }

        public enum TipoTreinamento
        {
            Interno = 1,
            Externo = 2
        }

        public enum TipoUsuarioCancela
        {
            Solicitante = 1,
            Atendente = 2,
            Administrador = 3
        }

        public enum StatusAgenda
        {
            [Description("Ativo")]
            Ativo = 1,
            [Description("Cancelado")]
            Cancelado = 2,
            [Description("Reservado")]
            Reservado = 3,
            [Description("Solicitação de Carro")]
            SolicitacaoCarro = 4,
            [Description("Finalizado")]
            Finalizado = 5
        }

        public enum TipoDespesa
        {
            Adiantamento = 1,
            Despesa = 2
        }

        public enum ValidacaoUsuario
        {
            UsuarioNaoCadastrado = 0,
            UsuarioInativo = 1,
            SenhaInvalida = 2,
            UsuarioOk = 3,
            ErroConsulta = 4,
            ProblemaAoConectar = 5
        }

        public enum TipoMensagem
        {
            [Description("Atenção")]
            Alerta = 1,
            [Description("Sucesso")]
            Sucesso = 2,
            [Description("Erro")]
            Erro = 3,
            [Description("Confirmação")]
            Confirmacao = 4,
            [Description("Informação")]
            Informacao = 5
        }

        public enum StatusAtendimento
        {
            Ativo = 1,
            Cancelado = 2,
            Respondido = 3,
            Finalizado = 4
        }

        public enum SituacaoAtendimento
        {
            [Description("Manter com o atendente")]
            ManterComAtendente = 1,
            [Description("Enviado ao colaborador")]
            EnviadoAoColaborador = 2,
            [Description("Enviado ao finalizador")]
            EnviadoAoFinalizador = 3,
            [Description("Finalizado")]
            Finalizado = 4,
            [Description("Cancelado")]
            Cancelado = 5,
            [Description("Aguardando retorno ao cliente")]
            AguardandoRetornoAoCliente = 6,
            [Description("Aguardando satisfação do cliente")]
            AguardandoSatisfacao = 7,
            [Description("Satisfação do cliente")]
            Satisfacao = 8,
        }

        public enum OpcaoDeRetornoAtendimento
        {
            Telefone = 1,
            Fax = 2,
            Email = 3,
            Nenhum = 4
        }

        public enum OrigemAtendimento
        {
            [Description("Pessoalmente")]
            Pessoalmente = 1,
            [Description("Internet")]
            Internet = 2,
            [Description("Telefone")]
            Telefone = 3,
            [Description("E-mail")]
            Email = 4,
            [Description("Orgão gestor")]
            OrgaoGestor = 5,
            [Description("Jornal e rádio")]
            JornalRadio = 6

        }

        public enum TipoDeSatisfacaoAtendimento
        {
            [Description("1 - Ruim")]
            Ruim = 0,
            [Description("2 - Regular")]
            Regular = 1,
            [Description("3 - Bom")]
            Bom = 2,
            [Description("4 - Muito Bom")]
            MuitoBom = 3,
            [Description("5 - Excelente")]
            Excelente = 4,
            SemAvaliacao = 5
        }

        public enum TipoCalculoCodigoSAC
        {
            Ano = 0,
            EmpresaAno = 1,
            AnoMes = 2,
            EmpresaAnoMes = 3,
            Sequencial = 4
        }

        public enum TelaPesquisaSAC
        {
            Atendimento = 0,
            Retorno = 1,
            Responde = 2,
            Finaliza = 3,
            Grid = 4,
            Satisfacao = 5,
            ChamadoAbertura = 6,
            ChamadoTramites = 7,
            ChamadoConcluido = 8
        }

        public enum RadarBI
        {
            [Description("Frota Patrimonial")]
            FrotaPatrimonial = 0,
            [Description("Frota Operacional")]
            FrotaOperacional = 1,
            [Description("Subsídio Débito")]
            SubsidioDebito = 2,
            [Description("Subsídio Crédito")]
            SubsidioCredito = 3,
            [Description("Limpeza de Frota")]
            LimpezaDeFrota = 4,
            [Description("Compras Emergenciais")]
            ComprasEmergenciais = 5,
            [Description("Multas Orgão Gestor")]
            MultaOrgaoGestor = 6,
            [Description("Reclamação de Passageiro")]
            ReclamaCaoPassageiro = 7,
            [Description("Dias uteis")]
            DiasUteis = 8,
            [Description("Ponte de Feriado")]
            PontesFeriados = 9,
            [Description("Cumprimento de Partida")]
            CumprimentoDePartida = 10,
            [Description("Carros Retidos")]
            CarrosRetidos = 11,
            [Description("Exames Vencidos")]
            ExamesVencidos = 12
        }

        public enum OrdemRadarBI
        {
            [Description("1")]
            FrotaPatrimonial = 1,
            [Description("2")]
            FrotaOperacional = 2,
            [Description("3")]
            LimpezaDeFrota = 3,
            [Description("4")]
            ComprasEmergenciais = 4,
            [Description("5")]
            MultaOrgaoGestor = 5,
            [Description("17")]
            SubsidioDebito = 6,
            [Description("18")]
            SubsidioCredito = 7,
            [Description("6")]
            ReclamaCaoPassageiro = 8,
            [Description("0")]
            DiasUteis = 9,
            [Description("0")]
            PontesFeriados = 10,
            [Description("0")]
            CumprimentoDePartida = 11,
            [Description("0")]
            CarrosRetidos = 12,
            [Description("5")]
            ExamesVencidos = 13
        }

        public enum TipoRadarBI
        {
            [Description("Planejamento")]
            FrotaPatrimonial = 1,
            [Description("Planejamento")]
            FrotaOperacional = 2,
            [Description("Financeiro")]
            SubsidioDebito = 3,
            [Description("Financeiro")]
            SubsidioCredito = 4,
            [Description("Manutenção")]
            LimpezaDeFrota = 5,
            [Description("Manutenção")]
            ComprasEmergenciais = 6,
            [Description("Operacional")]
            MultaOrgaoGestor = 7,
            [Description("Operacional")]
            ReclamaCaoPassageiro = 8,
            [Description("Planejamento")]
            DiasUteis = 9,
            [Description("Planejamento")]
            PontesFeriados = 10,
            [Description("Planejamento")]
            CumprimentoDePartida = 11,
            [Description("Planejamento")]
            CarrosRetidos = 12,
            [Description("Planejamento")]
            ExamesVencidos = 13
        }

        public enum TipoPedidoGlobus
        {
            [Description("Produto")]
            Produto = 0,
            [Description("Serviço")]
            Servico = 1
        }


        public enum TipoPedido
        {
            [Description("Mensal")]
            Mensal = 1,
            [Description("Emergencial")]
            Emergencial = 2
        }

        public enum TipoNfeGlobus
        {
            [Description("Entrada")]
            Entrada = 0,
            [Description("Saída")]
            Saida = 1
        }

        public enum StatusComunicado
        {
            [Description("Novo")]
            Novo = 'N',
            [Description("Reprovado")]
            Reprovado = 'R',
            [Description("Aprovado")]
            Aprovado = 'A',
            [Description("Finalizado")]
            Finalizado = 'F',
            [Description("Cancelado")]
            Cancelado = 'C',
            [Description("Alterado")]
            Alterado = 'L',
            [Description("Todos")]
            Todos = 'T'
        }

        public enum TipoEmailComunicado
        {
            [Description("Jurídico")]
            Juridico = 'J',
            [Description("Financeiro")]
            Financeiro = 'F',
            [Description("Diretoria")]
            Diretoria = 'D'
        }

        public enum TipoPessoa
        {
            [Description("Jurídica")]
            Juridica = 'J',
            [Description("Fisíca")]
            Fisica = 'F'
        }

        public enum TipoVencimento
        {
            [Description("Original")]
            Original = 'O',
            [Description("Antecipada")]
            Antecipada = 'A',
            [Description("Postergada")]
            Postergada = 'P',
            [Description("Importado")]
            Importado = 'I'
        }

        public enum TipoPrazos
        {
            [Description("Auto Avaliação")]
            AutoAvaliacao = 0,
            [Description("Feedback do Gestor")]
            FeedbackGestor = 1,
            [Description("Metas Numéricas")]
            MetasNumericas = 2,
            SemSelecao = 3,
            [Description("Avaliação do Gestor")]
            AvaliacaoDoGestor = 4,
            [Description("Avaliação do RH")]
            AvaliacaoRH = 5,
            [Description("Feedback do Avaliado")]
            FeedbackAvaliado = 6,
            [Description("RH Consulta Feedback")]
            RHConsultaFeedback = 7,
            [Description("Plano de desenvolvimento")]
            PlanoDeDesenvolvimento = 8,
            [Description("Média RH e Gestor")]
            Media = 9
        }

        public enum TipoCompetencias
        {
            [Description("Técnica")]
            Tecnica = 0,
            [Description("Comportamental")]
            Comportamental = 1,
            SemSelecao = 2
        }

        public enum TipoDeMetas
        {
            [Description("Crescimento")]
            Crescimento = 0,
            [Description("Resultado")]
            Resultado = 1
        }

        public enum Perspectivas
        {
            [Description("Financeira")]
            Financeira = 0,
            [Description("Processos")]
            Processos = 1,
            [Description("Aprendizagem e crescimento")]
            Aprendizagem = 2,
            [Description("Cliente")]
            Cliente = 3
        }

        public enum RegraFormulaMetas
        {
            [Description("Maior Melhor")]
            MaiorMelhor = 0,
            [Description("Menor Melhor")]
            MenorMelhor = 1,
            [Description("Igual")]
            Igual = 2,
        }

        public enum CamposCabecalhoValidarArquivei // Não mudar sem aplicar no banco de dados
        {
            [Description("CNPJ Destinatário")]
            CNPJDestinatario,
            [Description("IE Destinatário")]
            IEDestinatario,
            [Description("Endereço Destinatário")]
            EnderecoDestinatario,
            [Description("Bairro Destinatário")]
            BairroDestinatario,
            [Description("CEP Destinatário")]
            CEPDestinatario,
            [Description("Razão Social Destinatário")]
            RazaoSocialDestinatario,
            [Description("CNPJ Emitende")]
            CNPJEmitente,
            [Description("IE Emitente")]
            IEEmitente,
            [Description("Razão Social Emitente")]
            RazaoSocialEmitente,
            [Description("Chave de Acesso")]
            ChaveDeAcesso,
            [Description("Data de Emissão")]
            DataEmissao,
            [Description("Número NF")]
            NumeroNF,
            [Description("Modelo NF")]
            ModeloNF,
            [Description("Série")]
            Serie,
            [Description("Natureza da Operaçao")]
            NaturezaOperacao,
            [Description("Valor Total NF")]
            ValorTotalNF,
            [Description("Valor Total Produto")]
            ValorProduto,
            [Description("Base ICMS")]
            BaseICMS,
            [Description("Isentas + Outras = Valor Contabil")]
            IsentasOutrasIgualContabil,
            [Description("Dados Adicionais")]
            DadosAdicionais,
            [Description("Municipio Origem")]
            MunicipioOrigem,
            [Description("Munucipio Destino")]
            MunicipioDestino


        }

        public enum CamposCabecalhoOrdenacaoArquivei // Não mudar sem aplicar no banco de dados
        {
            [Description("1")]
            ChaveDeAcesso,
            [Description("2")]
            CNPJEmitente,
            [Description("3")]
            IEEmitente,
            [Description("4")]
            RazaoSocialEmitente,
            [Description("5")]
            NumeroNF,
            [Description("6")]
            Serie,
            [Description("7")]
            DataEmissao,
            [Description("8")]
            BaseICMS,
            [Description("9")]
            ValorTotalNF,
            [Description("10")]
            ValorProduto,
            [Description("11")]
            ModeloNF,
            [Description("12")]
            NaturezaOperacao,
            [Description("13")]
            DadosAdicionais,
            [Description("14")]
            CNPJDestinatario,
            [Description("15")]
            IEDestinatario,
            [Description("16")]
            RazaoSocialDestinatario,
            [Description("17")]
            EnderecoDestinatario,
            [Description("18")]
            BairroDestinatario,
            [Description("19")]
            CEPDestinatario,
            [Description("20")]
            IsentasOutrasIgualContabil,
        }

        public enum CamposItensValidarArquivei // Não mudar sem aplicar no banco de dados
        {
            [Description("Valor ICMS")]
            ValorICMS,
            [Description("Aliquota ICMS")]
            AliquotaICMS,
            [Description("Valor ICMS ST")]
            ValorICMSST,
            [Description("Valor IPI")]
            ValorIPI,
            [Description("Desconto")]
            Desconto,
            [Description("Seguro")]
            Seguro,
            [Description("Outras Despesas")]
            OutrasDespesas,
            [Description("Valor Frete")]
            ValorFrete,
            [Description("CCe")]
            CCe,
            [Description("CST")]
            CST,
            [Description("CST ICMS")]
            CSTICMS,
            [Description("CFOP")]
            CFOP,
            [Description("Valor Total")]
            ValorTotal,
            [Description("Operação Fiscal")]
            OperacaoFiscal

        }

        public enum CamposItensOrdenacaoArquivei // Não mudar sem aplicar no banco de dados
        {
            [Description("1")]
            ValorICMS,
            [Description("2")]
            AliquotaICMS,
            [Description("3")]
            ValorICMSST,
            [Description("4")]
            ValorIPI,
            [Description("5")]
            Desconto,
            [Description("6")]
            Seguro,
            [Description("7")]
            OutrasDespesas,
            [Description("8")]
            ValorFrete,
            [Description("9")]
            CCe,
            [Description("10")]
            CST,
            [Description("11")]
            CSTICMS,
            [Description("12")]
            CFOP,
            [Description("13")]
            ValorTotal,
            [Description("14")]
            OperacaoFiscal
        }
        #endregion
        
        #region Atributos

        private static GridMetroColors metroColor = new GridMetroColors();

        public static int _idUsuario;
        public static int _idUsuarioNovo;
        public static int _idRetornoPesquisa;
        public static int _idChamado;
        public static int _idTemporizador;
        public static int _idComunicado;
        public static int _prazoReabrir;
        public static int _anoSelecionadoComunicado;
        public static int _idSuperior;
        public static int _idColaborador;
        public static int _telefone;
        public static int _idEmpresa;
        public static int _idTorneio;
        public static int _heigthTelaInicial;
        public static int _heigthTituloTelaInicial;
        public static int _heigthBarraTelaInicial;
        public static int _widthTelaInicial;
        public static int _idLivro;
        public static int _idInteiroAuxiliar;
        public static int _prazoLembrete;

        public static decimal _codigoPesquisa;

        public static string _usuarioLogin = "";
        public static string _senhaLogin = "";
        public static string _codigoRetornoPesquisa;
        public static string stringConexao;
        public static string stringConexaoSigom;
        public static string stringConexaoProdata;
        public static string stringConexaoCigam;
        public static string mensagemDeErro;
        public static string _usuariologado;
        public static string _usuarioAcesso;
        public static string _motivoCancelamentoDevolucao;
        public static string _processoComunicado;
        public static string _codigoEmpresaGlobus;
        public static string _motivoLembrete;
        public static string _tipoDespesaReceita;
        public static string _novoContratoParcelamento;

        public static string _imagemMenu = "menu4";
        public static string _imagemPower = "power2";
        public static string _mensagemSistema = "";

        public static string _conexaoStringTeste = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.5.71)(PORT=1521)))" +
            "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = ORACLE))); User Id=Globus;" +
            "Password=o0wn33e3rnovo;";
 
        public static string _conexaoString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.5.2)(PORT=1521)))" +
            "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = ORACLE))); User Id=Globus;" +
            "Password=o0wn33e3rnovo;";// nffglb2013;";

        public static string _conexaoStringSigom = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.5.8)(PORT=1521)))" +
            "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = ORCL))); User Id=OpenDB;" +
            "Password=OpenDB;";

        public static string _conexaoStringProdataABC =
            "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.2.2)(PORT = 1521))))(CONNECT_DATA =(SID = MERC)));"+
            "User Id=mercury;Password=mercury;";

        public static string _conexaoStringCigamHomologacao =
            "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.5.30)(PORT = 1521))))(CONNECT_DATA =(SERVICE_NAME = desenv)));" +
            "User Id=Cigam;Password=Cigam;";

        public static string _conexaoStringCigamProducao =
            "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.5.2)(PORT = 1521))))(CONNECT_DATA =(SERVICE_NAME = ORACLE)));" +
            "User Id=Cigam;Password=Cigam;";


        public static string _caminhoAnexosChamado = @"c:\portalNiff\Anexos\";
        public static string _caminhoAnexosSAC = @"c:\portalNiff\SACAnexos\";
        public static string _caminhoAnexosRateioCTB = @"c:\portalNiff\Arquivos_Contabeis\";
        public static string _caminhoAnexosFinanceiro = @"c:\portalNiff\Arquivos_Financeiros\";
        public static string _caminhoPortal = @"c:\portalNiff\";
        public static string _nomeDoSistema = "Sistema Interno - Support Serviços";
        public static string _nomeSubSistemaJuridico = "Jurídico";
        public static string _nomeSubSistemaGlobus = "Globus";
        public static string _nomeSubSistemaCadastro = "Cadastro";
        public static string _nomeSubSistemaChamado = "Gerenciador de Chamados";
        public static string _nomeSubSistemaAvaliacao = "Avaliação de Desempenho";
        public static string _nomeSubSistemaSAC = "SAC";
        public static string _nomeSubSistemaBI = "BI";
        public static string _nomeSubSistemaCorridas = "Gerenciador de Corridas";
        public static string _nomeSubSistemaBiblioteca = "Biblioteca";
        public static string _nomeSubSistemaBolaoCopa = "Bolão copa do mundo";
        public static string _nomeSubSistemaContabil = "Contabilidade";
        public static string _nomeSubSistemaFiscal = "Escrituração Fiscal";
        public static string _nomeSubSistemaTI = "Tecnologia da Informação";
        public static string _nomeSubSistemaTorneios = "Gerenciador de Torneios";
        public static string _nomeSubSistemaProcessoSeletivo = "Processo Seletivo";
        public static string _nomeSubSistemaOperacional = "Operacional";
        public static string _nomeSubSistemaRecebedoria = "Recebedoria";
        public static string _nomeSubSistemaFinanceiro = "Financeiro";
        public static string _nomeSubSistemaDepartamentoPessoal = "Departamento Pessoal";
        public static string _nomeSubSistemaSuprimentos = "Suprimentos";
        public static string _caminhoPortalGlobus;
        public static string _telaRadarChamadaPeloMenu;
        public static string _notasAcessadoPeloMenu;

        public static bool _escTeclado;
        public static bool _setaParaBaixo;
        public static bool _alterouSkin = false;
        public static bool _cancelouMotivo = false;
        public static bool _devolveuAtendimento = false;
        public static bool _clicouSIM = false;
        public static bool _fecharAniversarios = false;
        public static bool _fecharComunicados = false;
        public static bool _fecharChamados = false;
        public static bool _porCargo = false;
        public static bool _fecharNotificacaoBolao = false;
        public static bool _jogosNaoFinalizados = true;
        public static bool _encerrarSemLogar;
        public static bool _participaAvaliacao = false;
        public static bool _chamouPelaTeladeLivros = false;
        public static bool _chamouPelaTelaResumoSigom = true;
        public static bool _TemaBlack = false;
        public static bool _temLembrete = false;
        public static bool _apenasAtivos = false;
        public static bool _consolidar = false;
        public static bool _cfopEntrada = false;
        public static bool _cfopSaida = false;

        public static Usuario _usuario;
        public static Aniversarios _aniversario;
        public static Chamado _chamado;
        public static Color _fundo;
        public static Color _fonte;
        public static Color _bordaEntrada;
        public static Color _bordaSaida;
        public static Color _botaoFocado;
        public static Color _botao;
        public static Color _fonteBotao;
        public static Color _fonteBotaoFocado;
        public static Color _tabPageAtiva;
        public static Color _panelTitulo;
        public static Color _panelTituloFoco;
        public static Color _fonteMenuSelecionado;

        public static DateTime _dataPesquisa;
        public static DateTime[] _datasLembrete;
        public static DateTime _dataInicioTeste = DateTime.MinValue; //new DateTime(DateTime.Now.Year, 01, 01);
        public static DateTime _dataFimTeste = DateTime.MinValue;//new DateTime(DateTime.Now.Year, 01, 01);

        public static DateTime _dataInicioPeriodoNatal = new DateTime(DateTime.Now.Year, 12, 23);
        public static DateTime _dataFimPeriodoNatal = new DateTime(DateTime.Now.Year, 12, 25);

        public static DateTime _dataInicioAnoNovo = new DateTime(DateTime.Now.Year, 01, 01);
        public static DateTime _dataFimAnoNovo = new DateTime(DateTime.Now.Year, 01, 02);

        public static DateTime _dataInicioPeriodoHalloween = new DateTime(DateTime.Now.Year, 10, 30);
        public static DateTime _dataFimPeriodoHalloween = new DateTime(DateTime.Now.Year, 10, 31);

        public static DateTime _dataInicioCrianca = new DateTime(DateTime.Now.Year, 10, 11);
        public static DateTime _dataFimCrianca = new DateTime(DateTime.Now.Year, 10, 12);

        //2021
        public static DateTime _dataInicioPascoa = new DateTime(DateTime.Now.Year, 04, 1);
        public static DateTime _dataFimPascoa = new DateTime(DateTime.Now.Year, 04, 4);

        public static DateTime _dataInicioCarnaval = new DateTime(DateTime.Now.Year, 02, 12);
        public static DateTime _dataFimCarnaval = new DateTime(DateTime.Now.Year, 02, 16);

        public static DateTime _dataInicioMaes = new DateTime(DateTime.Now.Year, 05, 7);
        public static DateTime _dataFimMaes = new DateTime(DateTime.Now.Year, 05, 09);

        public static DateTime _dataInicioPais = new DateTime(DateTime.Now.Year, 08, 15);
        public static DateTime _dataFimPais = new DateTime(DateTime.Now.Year, 08, 13);

        public static DateTime _dataInicioCopa = new DateTime(2018, 06, 14);
        public static DateTime _dataFimCopa = new DateTime(2018, 07, 15);

        public static DateTime _sla = DateTime.MinValue;
        public static TelaPesquisaSAC _telaQueChamouPesquisaDeAtendimento;
        public static StatusComunicado _chamadoPeloMenuDeComunicado;
        #endregion

        #region Cores Padrão NIFF Clara
        public static Color padraoNiffClaro_Fundo = Color.WhiteSmoke;
        public static Color padraoNIFFClaro_FonteEscura = System.Drawing.Color.FromArgb(3, 19, 53); // era 37, 38, 91
        public static Color padraoNIFFClaro_FonteClara = Color.WhiteSmoke;
        public static Color padraoNIFFClaro_BordaEntrada = System.Drawing.Color.FromArgb(3, 19, 53);
        public static Color padraoNIFFClaro_BordaSaida = Color.DimGray;
        public static Color padraoNIFFClaro_BotaoFocado = System.Drawing.Color.FromArgb(3, 19, 53);
        public static Color padraoNIFFClaro_Botao = System.Drawing.Color.FromArgb(3, 19, 53);
        public static Color padraoNIFFClaro_FonteBotao = Color.WhiteSmoke;
        public static Color padraoNIFFClaro_FonteBotaoFocado = Color.WhiteSmoke; //Color.GreenYellow;
        public static Color padraoNIFFClaroTitulo = padraoNatal_FonteEscura;
        public static Color padraoNIFFClaro_PanelTitulo = System.Drawing.Color.FromArgb(3, 19, 53);
        public static Color padraoNIFFClaro_PanelTituloFoco = System.Drawing.Color.FromArgb(2, 27, 68); // era 28, 28, 92
        #endregion

        #region Cores Fundo Escuro-Preto
        public static Color padraoEscuroPreto_Fundo = System.Drawing.Color.FromArgb(30, 30, 30);
        public static Color padraoEscuroPreto_FonteEscura = Color.WhiteSmoke;
        public static Color padraoEscuroPreto_FonteClara = Color.WhiteSmoke;
        public static Color padraoEscuroPreto_BordaEntrada = System.Drawing.Color.FromArgb(51, 51, 51);
        public static Color padraoEscuroPreto_BordaSaida = Color.Black;
        public static Color padraoEscuroPreto_BotaoFocado = System.Drawing.Color.FromArgb(51, 51, 51);
        public static Color padraoEscuroPreto_Botao = System.Drawing.Color.FromArgb(41, 41, 41);
        public static Color padraoEscuroPreto_FonteBotao = Color.WhiteSmoke;
        public static Color padraoEscuroPreto_FonteBotaoFocado = Color.Silver;
        public static Color padraoEscuroPretoTitulo = Color.WhiteSmoke;
        public static Color padraoEscuroPreto_PanelTitulo = System.Drawing.Color.FromArgb(41, 41, 41);
        public static Color padraoEscuroPreto_PanelTituloFoco = System.Drawing.Color.FromArgb(41, 41, 41);
        public static Color padraoEscuroPreto_FonteMenuSelecionado = System.Drawing.Color.FromArgb(100, 151, 178);//  Color.GreenYellow;
        #endregion

        #region Cores Natal
        public static Color padraoNatal_Fundo = Color.WhiteSmoke;
        public static Color padraoNatal_FonteEscura = Color.DarkRed;
        public static Color padraoNatal_FonteClara = Color.WhiteSmoke;
        public static Color padraoNatal_BordaEntrada = Color.DarkRed;
        public static Color padraoNatal_BordaSaida = Color.DarkGreen;
        public static Color padraoNatal_PanelTitulo = Color.DarkRed;
        public static Color padraoNatal_PanelTituloFoco = System.Drawing.Color.FromArgb(145, 30, 30);

        public static Color padraoNatal_BotaoFocado = Color.Gold;
        public static Color padraoNatalo_Botao = Color.Goldenrod;
        public static Color padraoNatal_FonteBotao = Color.WhiteSmoke;
        public static Color padraoNatal_FonteBotaoFocado = Color.DarkRed;

        #endregion

        #region Cores Ano Novo
        public static Color padraoAnoNovo_Fundo = Color.WhiteSmoke;
        public static Color padraoAnoNovo_FonteEscura = Color.Navy;
        public static Color padraoAnoNovo_FonteClara = Color.WhiteSmoke;
        public static Color padraoAnoNovo_BordaEntrada = Color.Gold;
        public static Color padraoAnoNovo_BordaSaida = Color.Navy;
        public static Color padraoAnoNovo_PanelTitulo = Color.MidnightBlue;

        public static Color padraoAnoNovo_BotaoFocado = Color.Goldenrod;
        public static Color padraoAnoNovoo_Botao = Color.Gold;
        public static Color padraoAnoNovo_FonteBotao = Color.MidnightBlue;
        public static Color padraoAnoNovo_FonteBotaoFocado = Color.MidnightBlue;

        #endregion        

        #region Cores Halloween
        public static Color padraoHalloween_Fundo = Color.WhiteSmoke;
        public static Color padraoHalloween_FonteEscura = Color.Black;
        public static Color padraoHalloween_FonteClara = Color.WhiteSmoke;
        public static Color padraoHalloween_BordaEntrada = Color.DarkOrange;
        public static Color padraoHalloween_BordaSaida = Color.Black;
        public static Color padraoHalloween_PanelTitulo = System.Drawing.Color.FromArgb(30, 30, 30);

        public static Color padraoHalloween_BotaoFocado = Color.Gray;
        public static Color padraoHalloween_Botao = Color.DarkGray;
        public static Color padraoHalloween_FonteBotao = Color.Black;
        public static Color padraoHalloween_FonteBotaoFocado = Color.DarkRed;
        public static Color padraoHallowen_PanelTituloFoco = System.Drawing.Color.FromArgb(30, 30, 30);
        #endregion

        #region Cores Pascoa
        public static Color padraoPascoa_Fundo = Color.WhiteSmoke;
        public static Color padraoPascoa_FonteEscura = Color.DarkGreen;
        public static Color padraoPascoa_FonteClara = Color.WhiteSmoke;
        public static Color padraoPascoa_BordaEntrada = Color.DarkOrange;
        public static Color padraoPascoa_BordaSaida = Color.DarkGreen;
        public static Color padraoPascoa_PanelTitulo = Color.DarkGreen;
        public static Color padraoPascoa_PanelTituloFoco = Color.Green;

        public static Color padraoPascoa_BotaoFocado = Color.OrangeRed;
        public static Color padraoPascoa_Botao = Color.Orange;
        public static Color padraoPascoa_FonteBotao = Color.WhiteSmoke;
        public static Color padraoPascoa_FonteBotaoFocado = Color.WhiteSmoke;

        #endregion

        #region Cores Carnaval
        public static Color padraoCarnaval_Fundo = Color.WhiteSmoke;
        public static Color padraoCarnaval_FonteEscura = Color.Purple;
        public static Color padraoCarnaval_FonteClara = Color.WhiteSmoke;
        public static Color padraoCarnaval_BordaEntrada = Color.MediumPurple;
        public static Color padraoCarnaval_BordaSaida = Color.Purple;
        public static Color padraoCarnaval_PanelTitulo = Color.Indigo;
        public static Color padraoCarnaval_PanelTituloFoco = Color.BlueViolet;

        public static Color padraoCarnaval_BotaoFocado = Color.Purple;
        public static Color padraoCarnaval_Botao = Color.DarkOrchid;
        public static Color padraoCarnaval_FonteBotao = Color.WhiteSmoke;
        public static Color padraoCarnaval_FonteBotaoFocado = Color.WhiteSmoke;

        #endregion

        #region Cores Copa Russia
        public static Color padraoCopa_Fundo = Color.WhiteSmoke;
        public static Color padraoCopa_FonteEscura = Color.DarkRed;
        public static Color padraoCopa_FonteClara = Color.WhiteSmoke;
        public static Color padraoCopa_BordaEntrada = System.Drawing.Color.FromArgb(25, 96, 167);
        public static Color padraoCopa_BordaSaida = Color.DarkRed;
        public static Color padraoCopa_PanelTitulo = System.Drawing.Color.FromArgb(25, 70, 167);

        public static Color padraoCopa_BotaoFocado = System.Drawing.Color.FromArgb(25, 70, 167);
        public static Color padraoCopa_Botao = System.Drawing.Color.FromArgb(25, 96, 167);
        public static Color padraoCopa_FonteBotao = Color.WhiteSmoke;
        public static Color padraoCopa_FonteBotaoFocado = Color.DarkRed;
        public static Color padraoCopa_PanelTituloFoco = System.Drawing.Color.FromArgb(25, 96, 167);
        #endregion

        #region Metodos
        public static void AplicarSkin(Control controle)
        {
            if (controle.Name == "panel1" || controle.Name == "powerPictureBox" ||
                    controle.Name == "MenuPictureBox" || controle.Name == "skinPictureBox" ||
                    controle.Name == "skinLabel" || controle.Name == "fotoUsuarioPictureBox" ||
                    controle.Name == "skinSetaPictureBox" || controle.Name == "abrirMenuUsuarioPictureBox" ||
                    controle.Name == "padraoNiffClaroPanel" ||
                    controle.Name == "padraoNiffEscuroPanel" || controle.Name == "menuSkinPanel" ||
                    controle.Name == "tituloMensagemPanel" || controle.Name == "TemaClaroPanel" || controle.Name == "TemaBlackPanel" ||
                    controle.Name == "AplicarTemaButton")
            {
                return;
            }

            if (controle is Panel)
            {
                if (controle.Name == "DashBoardPanel" || controle.Name == "TemaClaroPanel" || controle.Name == "TemaBlackPanel")
                    return;

                if (controle.Name == "tituloOpcoesPanel" ||
                    controle.Name == "tituloSacPanel" ||
                    controle.Name == "tituloPanel" ||
                    controle.Name == "tituloAutorizacaoPanel" ||
                    controle.Name.StartsWith("titulo") ||
                    controle.Name == "panel2" ||
                    controle.Name == "usuarioLogadoPanel" ||
                    controle.Name == "BolaoCopaPanel" ||
                    controle.Name == "AcessoAoMenuPanel" ||
                    controle.Name.ToUpper().Contains("TITULO")
                    )
                {
                    controle.BackColor = _panelTitulo;
                    controle.ForeColor = _fonte;
                }
                else
                {
                    controle.BackColor = _fundo;
                    controle.ForeColor = _fonte;
                }
            }

            if (controle is Label)
            {
                if (controle.Name == "tituloLabel" ||
                    controle.Name == "tituloOpcoesLabel" ||
                    controle.Name == "tituloSacLabel" ||
                    controle.Name == "tituloAutorizacaoLabel" ||
                    controle.Name == "olaLabel" ||
                    controle.Name == "usuarioLogadoLabel" ||
                    controle.Name == "mensagemLabel" ||
                    controle.Name == "dataHoraLabel" ||
                    controle.Name == "mensagemSistemaLabel" ||
                    controle.Name == "BolaoCopaLabel" ||
                    controle.Name == "AcessoAoMenuLabel" ||
                    controle.Name.StartsWith("titulo") ||
                    controle.Name.ToUpper().Contains("TITULO"))
                {
                    if (_TemaBlack)
                        controle.ForeColor = _fonte;
                    else
                        controle.ForeColor = _fundo;
                }
                else
                {
                    if (controle.Name != "prazoLabel")
                        controle.ForeColor = _fonte;
                    else
                    {
                        if (_TemaBlack)
                            controle.ForeColor = _fonte;
                        else
                            controle.ForeColor = _fundo;
                        controle.BackColor = _bordaSaida;
                    }
                }
            }

            if (controle is ButtonAdv)
            {
                if (!controle.Name.ToUpper().StartsWith("PESQUISA"))
                {
                    controle.BackColor = _botao;
                    controle.ForeColor = _fonteBotao;
                }
                else
                {
                    if (_TemaBlack)
                    {
                        controle.BackColor = _fundo;
                        controle.ForeColor = _fonteBotao;
                    }
                }
                ((ButtonAdv)controle).MetroColor = _botao;
                ((ButtonAdv)controle).FlatAppearance.BorderColor = _bordaSaida;
            }

            
            if (controle is ToggleButton)
            {
                //((ToggleButton)controle).
               /* ((ToggleButton)controle).ActiveState.BackColor = _bordaSaida;
                ((ToggleButton)controle).ActiveState.BorderColor = _bordaSaida;
                ((ToggleButton)controle).ActiveState.ForeColor = _fundo;
                ((ToggleButton)controle).ActiveState.HoverColor = _bordaEntrada;

                ((ToggleButton)controle).Slider.BackColor = _bordaSaida;
                ((ToggleButton)controle).Slider.BorderColor = _bordaSaida;
                ((ToggleButton)controle).Slider.ForeColor = _fundo;
                ((ToggleButton)controle).Slider.HoverColor = _bordaEntrada;

                ((ToggleButton)controle).InactiveState.BackColor = _bordaSaida;
                ((ToggleButton)controle).InactiveState.BorderColor = _bordaSaida;
                ((ToggleButton)controle).InactiveState.ForeColor = _fundo;
                ((ToggleButton)controle).InactiveState.HoverColor = _bordaEntrada;*/
            }

            try
            {
                ((ComboBoxAdv)controle).FlatBorderColor = _bordaSaida;
                if (_TemaBlack)
                {
                    ((ComboBoxAdv)controle).BackColor = _fundo;
                    ((ComboBoxAdv)controle).ForeColor = _fonte;
                }
            }
            catch { }


            try
            {
                if (!_TemaBlack)
                    ((DateTimePickerAdv)controle).BorderColor = _bordaSaida;

            }
            catch { }

            try
            {
                ((TextBoxExt)controle).UseBorderColorOnFocus = false;
                ((TextBoxExt)controle).BorderColor = _bordaSaida;
                if (_TemaBlack)
                {
                    ((TextBoxExt)controle).BackColor = _fundo;
                    ((TextBoxExt)controle).ForeColor = _fonteBotaoFocado;
                }
            }
            catch { }

            try
            {
                ((MaskedEditBox)controle).UseBorderColorOnFocus = false;
                ((MaskedEditBox)controle).BorderColor = _bordaSaida;
                ((MaskedEditBox)controle).FocusBorderColor = _bordaEntrada;
                ((MaskedEditBox)controle).Metrocolor = _bordaSaida;
                if (_TemaBlack)
                {
                    ((MaskedEditBox)controle).BackColor = _fundo;
                    ((MaskedEditBox)controle).ForeColor = _fonte;
                }
            }
            catch { }

            try
            {
                if (controle is CurrencyTextBox)
                {

                    ((CurrencyTextBox)controle).UseBorderColorOnFocus = false;
                    ((CurrencyTextBox)controle).ThemesEnabled = false;
                    ((CurrencyTextBox)controle).BorderColor = _bordaSaida;
                    ((CurrencyTextBox)controle).FocusBorderColor = _bordaEntrada;
                    ((CurrencyTextBox)controle).Metrocolor = _bordaSaida;

                    if (_TemaBlack)
                    {
                        ((CurrencyTextBox)controle).BackColor = _fundo;
                        ((CurrencyTextBox)controle).ForeColor = _fonte;
                    }
                }
            }
            catch { }

            try
            {
                ((Syncfusion.Windows.Forms.Tools.TabControlAdv)controle).TabPanelBackColor = _fundo;
                ((Syncfusion.Windows.Forms.Tools.TabControlAdv)controle).ActiveTabColor = _tabPageAtiva;
                ((Syncfusion.Windows.Forms.Tools.TabControlAdv)controle).ActiveTabForeColor = _fonteBotao;
                ((Syncfusion.Windows.Forms.Tools.TabControlAdv)controle).FixedSingleBorderColor = _tabPageAtiva;

                if (_TemaBlack)
                {
                    ((Syncfusion.Windows.Forms.Tools.TabControlAdv)controle).InactiveTabColor = _fundo;
                    ((Syncfusion.Windows.Forms.Tools.TabControlAdv)controle).InActiveTabForeColor = _fonteBotaoFocado;
                }

                for (int i = 0; i < (((Syncfusion.Windows.Forms.Tools.TabControlAdv)controle).TabPages.Count); i++)
                {
                    foreach (Control subControle in ((Syncfusion.Windows.Forms.Tools.TabControlAdv)controle).TabPages[i].Controls)
                        AplicarSkin(subControle);
                }
            }
            catch { }

            try
            {
                if (controle is GridGroupingControl)
                {
                    if (!_TemaBlack)
                    {
                        metroColor.HeaderBottomBorderColor = _bordaEntrada;
                        metroColor.HeaderColor.HoverColor = _bordaEntrada;
                        metroColor.HeaderColor.PressedColor = _bordaEntrada;
                        metroColor.GroupDropAreaColor.BackColor = Publicas._bordaEntrada;
                        metroColor.CheckBoxColor.BorderColor = _bordaEntrada;
                        metroColor.PushButtonColor.PushedBackColor = _bordaEntrada;
                        metroColor.PushButtonColor.HoverBackColor = _bordaEntrada;

                        metroColor.PushButtonColor.NormalBackColor = Color.WhiteSmoke;

                        metroColor.ComboboxColor.NormalBorderColor = _bordaEntrada;
                        metroColor.ComboboxColor.HoverBorderColor = _bordaEntrada;
                        metroColor.ComboboxColor.HoverBackColor = _bordaEntrada;
                        metroColor.ComboboxColor.PressedBackColor = _bordaEntrada;

                        metroColor.ComboboxColor.NormalBackColor = Color.WhiteSmoke;

                        ((GridGroupingControl)controle).TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                        ((GridGroupingControl)controle).TableOptions.SelectionTextColor = Color.WhiteSmoke;
                        ((GridGroupingControl)controle).SetMetroStyle(metroColor);
                    }
                }
            }
            catch { }

            foreach (Control subControle in controle.Controls)
                AplicarSkin(subControle);

        }

        public static bool ValidarEmail(string email)
        {

            Regex rg = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            return (rg.IsMatch(email));

        }

        #region E-mail SAC
        private static string PreparaCorpoDoEmail(string titulo, string empresa, string cliente,
                                                  string protocolo, string dataAbertura, string telefone,
                                                  string colaborador, string opcao, string textoOpcional, string textoPadrao = "")
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string arquivo = "";

            switch (_telaQueChamouPesquisaDeAtendimento)
            {
                case TelaPesquisaSAC.Atendimento:
                    if (cliente != "")
                        arquivo = @"\ModeloDeEmails\emailSAC.html";
                    else
                        arquivo = @"\ModeloDeEmails\emailSACAtendente.html";
                    break;
                case TelaPesquisaSAC.Retorno:
                    break;
                case TelaPesquisaSAC.Responde:
                    arquivo = @"\ModeloDeEmails\emailRetornoSAC.html";
                    break;
                case TelaPesquisaSAC.Finaliza:
                    if (_devolveuAtendimento)
                        arquivo = @"\ModeloDeEmails\emailDevolvidoSAC.html";
                    else
                    {
                        if (cliente == "")
                            arquivo = @"\ModeloDeEmails\emailFinalizadoSAC.html";
                        else
                            arquivo = @"\ModeloDeEmails\emailFinalizadoClienteSAC.html";
                    }
                    break;
            }

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{cliente}", cliente);
            body = body.Replace("{titulo}", titulo);
            body = body.Replace("{protocolo}", protocolo);
            body = body.Replace("{dataAbertura}", dataAbertura);
            body = body.Replace("{telefone}", telefone);
            body = body.Replace("{Empresa}", empresa);
            body = body.Replace("{Colaborador}", colaborador.Split(' ')[0]);
            body = body.Replace("{opcao}", opcao);
            body = body.Replace("{textoOpcional}", textoOpcional);
            body = body.Replace("{textoPadrao}", textoPadrao);
            return body;
        }

        public static bool EnviarEmail(string titulo, string empresa, string cliente, string protocolo, string dataAbertura, string telefone,
                                       string emailOrigem, string emailDestino, string senha, string assunto, string smtp, int porta,
                                       bool autentica, bool autenticaSSL, string emailDestinoOculto = "", string colaborador = "",
                                       string opcao = "", string textoOpcional = "", string textoPadrao ="")
        {
            if (emailOrigem == "")
                return true;

            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress(emailOrigem);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;
           // _mensagem.Bcc.Add("mdmunoz@supportse.com.br");// para acompanhar por um periodo o uso
            emailDestino = emailDestino + ";";

            if (emailDestinoOculto != "")
            {
                foreach (var address in emailDestinoOculto.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!_mensagem.Bcc.ToString().Contains(address))
                        _mensagem.Bcc.Add(address);
                }
            }
            else
                foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!_mensagem.To.ToString().Contains(address))
                        _mensagem.To.Add(address);
                }

            _mensagem.IsBodyHtml = true;

            _mensagem.Body = PreparaCorpoDoEmail(titulo, empresa, cliente, protocolo, dataAbertura, telefone, colaborador, opcao, textoOpcional, textoPadrao);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = assunto;
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = autentica;
            _smtp.EnableSsl = autenticaSSL;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.SendAsync(_mensagem, "envio");
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }

        #endregion

        #region E-mail Chamado
        private static string PreparaEmailChamado(string[] dados, bool tramite, bool atendente, bool agrupou, bool AlterouCategoria)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";
            if (AlterouCategoria)
                arquivo = @"\ModeloDeEmails\emailChamadoMoveu.html";
            else
            if (agrupou)
                arquivo = @"\ModeloDeEmails\emailChamadoAgrupou.html";
            else
            if (tramite)
                arquivo = @"\ModeloDeEmails\emailChamadoTramites.html";
            else
            {
                if (atendente)
                    arquivo = @"\ModeloDeEmails\emailChamadoAtendentes.html";
                else
                    arquivo = @"\ModeloDeEmails\emailChamado.html";
            }

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            body = body.Replace("{titulo}", dados[0]);
            body = body.Replace("{data}", dados[1]);
            body = body.Replace("{cliente}", dados[2]);
            body = body.Replace("{empresa}", dados[3]);
            body = body.Replace("{protocolo}", dados[4]);
            body = body.Replace("{status}", dados[5]);
            body = body.Replace("{assunto}", dados[6]);
            body = body.Replace("{sistema}", dados[7]);
            body = body.Replace("{modulo}", dados[8]);
            body = body.Replace("{local}", dados[9]);
            body = body.Replace("{tipoDoChamado}", dados[10]);
            body = body.Replace("{prioridade}", dados[11]);
            if (dados[12] != null)
                body = body.Replace("{textoOpcional}", dados[12].Replace("\r\n", "</br>"));
            else
                body = body.Replace("{textoOpcional}", dados[12]);

            body = body.Replace("{qtdAnexos}", dados[13]);
            body = body.Replace("{opcao}", dados[14]);
            body = body.Replace("{novochamado}", dados[15]);
            body = body.Replace("{Assinatura}", dados[16].Replace("\r\n", "</br>").Replace("\n", "</br>").Replace("'", "\""));

            return body;
        }

        public static bool EnviarEmailChamado(string[] dados, bool tramite, bool atendente, bool agrupou,
                               string emailDestino, string assunto, bool AlterouCategoria = false, string destinoCopia = "")
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress("chamados@supportse.com.br", "Gerenciador de Chamados", Encoding.UTF8);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.To.Add(address); 
            }

            if (destinoCopia != "")
            {
                foreach (var address in destinoCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (address.Trim() != "")
                        _mensagem.CC.Add(address);
                }
            }

            //if (!emailDestino.Trim().Contains("mdmunoz") && !destinoCopia.Trim().Contains("mdmunoz"))
              //  _mensagem.Bcc.Add("mdmunoz@supportse.com.br");

            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailChamado(dados, tramite, atendente, agrupou, AlterouCategoria);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = assunto;
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.SendAsync(_mensagem, "envio");
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }

        public static bool EnviarEmailSigom(string[] dados, string emailDestino, string assunto)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress("informatica@supportse.com.br", "informatica", Encoding.UTF8);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    if (address.Trim() != "")
                        _mensagem.To.Add(address);
                }
                catch { }
            }

            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailSigom(dados);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = assunto;
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.Send(_mensagem);
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }

        private static string PreparaEmailSigom(string[] dados)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";
            arquivo = @"\ModeloDeEmails\emailSigom.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            body = body.Replace("{diferencas}", dados[0]);
            body = body.Replace("{data}", dados[1]);
            body = body.Replace("{empresa}", dados[2]);
            body = body.Replace("{Sigom}", dados[3]);

            return body;
        }

        #endregion

        #region E-mail Arquivei

        public static bool EnviarEmailArquivei(string[] dados, string emailDestino, string assunto, bool nfe = true, bool nfse = false, bool emitidas = false)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress("informatica@supportse.com.br", "informatica", Encoding.UTF8);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.To.Add(address);
            }
            //_mensagem.Bcc.Add("mdmunoz@supportse.com.br");
            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailArquivei(dados, nfe, nfse);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = assunto;
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.Send(_mensagem);
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }

        private static string PreparaEmailArquivei(string[] dados, bool nfe = true, bool nfse = false, bool emitidas = false)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";

            if (nfe)
            {
                if (!emitidas)
                    arquivo = @"\ModeloDeEmails\emailArquivei.html";
                else
                    arquivo = @"\ModeloDeEmails\emailArquiveiEmitidas.html";
            }
            else
            {
                if (!nfe && !nfse)
                    arquivo = @"\ModeloDeEmails\emailArquiveiDacte.html";
                else
                    arquivo = @"\ModeloDeEmails\emailArquiveiNFSe.html";
            }
            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            body = body.Replace("{quantidade}", dados[0]);
            body = body.Replace("{data}", dados[1]);
            body = body.Replace("{empresa}", dados[2]);
            body = body.Replace("{NFNovas}", dados[3]);
            body = body.Replace("{quantidadeCadastrados}", dados[4]);
            //body = body.Replace("{quantidadeAntes}", dados[5]);
            body = body.Replace("{quantidadeNovos}", dados[6]);
            body = body.Replace("{quantidadeErro}", dados[7]);
            body = body.Replace("{arquivo}", dados[8]);
            body = body.Replace("{quantidadeCancelada}", dados[9]);
            body = body.Replace("{quantidadeCce}", dados[10]);

            body = body.Replace("{NFCanceladas}", dados[11]);
            body = body.Replace("{NFCarta}", dados[12]);
            body = body.Replace("{NFErros}", dados[13]);

            return body;
        }

        #endregion

        #region E-mail Notificação

        public static bool EnviarEmailNotificacao(string[] dados, string emailDestino, string assunto, bool atualizacaoConcluida)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress("informatica@supportse.com.br", "informatica", Encoding.UTF8);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.Bcc.Add(address);
            }
            
            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailNotificacao(dados, atualizacaoConcluida);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = assunto;
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.Send(_mensagem);
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }

        private static string PreparaEmailNotificacao(string[] dados, bool atualizacaoConcluida)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";

            if (!atualizacaoConcluida)
                arquivo = @"\ModeloDeEmails\emailNotificacao.html";
            else
                arquivo = @"\ModeloDeEmails\emailNotificacaoConcluida.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            body = body.Replace("{data}", dados[0]);
            body = body.Replace("{hora}", dados[1]);
            body = body.Replace("{horafim}", dados[2]);
            body = body.Replace("{versao}", Application.ProductVersion);

            return body;
        }

        #endregion

        #region E-mail Notas não lançadas

        public static bool EnviarEmailNotasNaoLancadas(string[] dados, string emailOrigem, string emailDestino, string emailCopia, string assunto, bool nfse = false)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress(emailOrigem, "", Encoding.UTF8);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.To.Add(address);
            }

            foreach (var address in emailCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.CC.Add(address);
            }

            //_mensagem.Bcc.Add("mdmunoz@supportse.com.br");
            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailNotasNaoLancadas(dados, nfse);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = assunto;
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.Send(_mensagem);
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }

        private static string PreparaEmailNotasNaoLancadas(string[] dados, bool nfse = false)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";

            if (!nfse)
                arquivo = @"\ModeloDeEmails\emailNotasNaoLancadas.html";
            else
                arquivo = @"\ModeloDeEmails\emailNotasServicoNaoLancadas.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            body = body.Replace("{empresa}", dados[0]);
            body = body.Replace("{notas}", dados[1]);

            return body;
        }

        #endregion

        #region E-mail ConferenciasCTB Notas

        public static bool EnviarEmailConferenciaCTB(string[] dados, string emailOrigem, string emailDestino, string emailCopia, string assunto)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress(emailOrigem, "", Encoding.UTF8);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.To.Add(address);
            }

            foreach (var address in emailCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.CC.Add(address);
            }

            //_mensagem.Bcc.Add("mdmunoz@supportse.com.br");
            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailConferenciaCTB(dados);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = assunto;
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.Send(_mensagem);
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }

        private static string PreparaEmailConferenciaCTB(string[] dados)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";
            arquivo = @"\ModeloDeEmails\emailConferenciaCTBNotas.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            //body = body.Replace("`", "'");
            dados[2] = dados[2].Replace("'", "\"");
            dados[2] = dados[2].Replace("`", "'");
            body = body.Replace("{empresa}", dados[0]);
            body = body.Replace("{referencia}", dados[1]);
            body = body.Replace("{notas}", dados[2]);

            return body;
        }

        #endregion

        #region E-mail parcelamento

        public static bool EnviarEmailParcelamento(string[] dados, string emailOrigem, string emailDestino, string emailCopia, string assunto)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress(emailOrigem, "", Encoding.UTF8);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.To.Add(address);
            }

            foreach (var address in emailCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.CC.Add(address);
            }

           // _mensagem.Bcc.Add("mdmunoz@supportse.com.br");
            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailParcelamento(dados);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = assunto;
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.Send(_mensagem);
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }

        private static string PreparaEmailParcelamento(string[] dados)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";
            arquivo = @"\ModeloDeEmails\emailParcelamento.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            body = body.Replace("{empresa}", dados[0]);
            body = body.Replace("{notas}", dados[1]);

            return body;
        }

        #endregion

        #region E-mail Teste
        private static string PreparaEmailTeste(string[] dados)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";

            arquivo = @"\ModeloDeEmails\emailTeste.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            body = body.Replace("{emailOrigem}", dados[0]);
            body = body.Replace("{emailDestino}", dados[1]);
            body = body.Replace("{data}", dados[2]);

            return body;
        }

        public static bool EnviarEmailTeste(string emailOrigem, string emailDestino)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress("informatica@supportse.com.br");

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            _mensagem.To.Add(emailDestino);
           // _mensagem.Bcc.Add("mdmunoz@supportse.com.br");

            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailTeste(new string[] { emailOrigem, emailDestino, DateTime.Now.ToString() });

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = "Teste de envio de e-mail";
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.SendAsync(_mensagem, "envio");
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }
        #endregion

        #region E-mail Comunicado
        private static string PreparaEmailComunicado(string[] dados, bool diretoria)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string arquivo = @"\ModeloDeEmails\emailAberturaComunicado.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("'", "\"");
            body = body.Replace("{titulo}", dados[0]);
            body = body.Replace("{data}", dados[1]);
            body = body.Replace("{solicitante}", dados[2]);
            body = body.Replace("{empresa}", dados[3]);
            body = body.Replace("{processo}", dados[4]);
            body = body.Replace("{novo processo}", dados[5]);
            body = body.Replace("{vara}", dados[6]);
            body = body.Replace("{autor}", dados[7]);
            body = body.Replace("{cpf}", dados[8]);
            body = body.Replace("{pis}", dados[9]);
            body = body.Replace("{custo}", dados[10]);
            body = body.Replace("{reembolso}", dados[11]);
            body = body.Replace("{seguro}", dados[12]);
            body = body.Replace("{tipo}", dados[13]);
            body = body.Replace("{referencia}", dados[14]);
            body = body.Replace("{resumo}", dados[15]);
            body = body.Replace("{total}", dados[16]);
            body = body.Replace("{Qtde parcelas}", dados[17]);
            body = body.Replace("{nota Fiscal}", dados[18]);
            body = body.Replace("{parcelas}", dados[19]);
            body = body.Replace("{observacoes}", dados[20]);
            body = body.Replace("{favorecido}", dados[21]);
            body = body.Replace("{cpf favorecido}", dados[22]);
            body = body.Replace("{banco}", dados[23]);
            body = body.Replace("{agencia}", dados[24]);
            body = body.Replace("{conta}", dados[25]);
            body = body.Replace("{tipo autor}", dados[26]);
            body = body.Replace("{tipo favorecido}", dados[27]);
            body = body.Replace("{valor reembolso}", dados[28]);
            body = body.Replace("{MotivoCancelamento}", dados[29]);

            return body;
        }

        public static bool EnviarEmailComunicado(string[] dados, bool diretoria,
                                       string emailOrigem, string emailDestino, string assunto, string smtp, int porta,
                                       bool autentica, bool autenticaSSL, string emailDestinoOculto = "")
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress(emailOrigem);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            //_mensagem.Bcc.Add("mdmunoz@supportse.com.br");

            if (emailDestinoOculto != "")
            {
                foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (address.Trim() != "")
                        _mensagem.Bcc.Add(address);
                }
            }
            else
                foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (address.Trim() != "")
                        _mensagem.To.Add(address);
                }

            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailComunicado(dados, diretoria);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = assunto;
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");
            _smtp.UseDefaultCredentials = autentica;
            _smtp.EnableSsl = autenticaSSL;

            try
            {
                _smtp.SendAsync(_mensagem, "envio");
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }
        #endregion

        #region E-mail Avaliação desempenho
        private static string PreparaEmailAvaliacao(string[] dados)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";

            arquivo = @"\ModeloDeEmails\emailAcompanhamentoDePrazos.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            body = body.Replace("{titulo}", dados[0]);
            body = body.Replace("{avaliacao}", dados[1]);
            body = body.Replace("{data}", dados[2]);
            body = body.Replace("{cliente}", dados[3]);
            body = body.Replace("{texto}", dados[4]);

            return body;
        }

        public static bool EnviarEmailAvaliacao(string[] dados, string emailDestino)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress("rh@supportse.com.br");

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            _mensagem.To.Add(emailDestino);

            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailAvaliacao(dados);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = "Acompanhamento de prazos";
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.SendAsync(_mensagem, "envio");
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }
        #endregion

        #region E-mail Biblioteca
        private static string PreparaEmailBiblioteca(string[] dados)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";

            arquivo = @"\ModeloDeEmails\emailBiblioteca.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            body = body.Replace("{livro}", dados[0]);
            body = body.Replace("{texto}", dados[1]);
            body = body.Replace("{texto2}", dados[2]);
            body = body.Replace("{qLivros}", dados[4]);
            body = body.Replace("{Fisico}", dados[5]);
            body = body.Replace("{qEbooks}", dados[6]);
            body = body.Replace("{ebook}", dados[7]);
            body = body.Replace("{pontos}", dados[8]);
            body = body.Replace("{texto3}", dados[9]);
            body = body.Replace("{perguntas}", dados[10]);
            return body;
        }

        public static bool EnviarEmailBiblioteca(string[] dados, string emailDestino, string emailCopia)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress("informatica@supportse.com.br");

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            emailDestino = emailDestino + ";";
            foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.To.Add(address);
            }

            emailCopia = emailCopia + ";";
            foreach (var address in emailCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.CC.Add(address);
            }

            //_mensagem.Bcc.Add("mdmunoz@supportse.com.br");

            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailBiblioteca(dados);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = "Biblioteca";
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.SendAsync(_mensagem, "envio");
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }
        #endregion

        #region Email Processo Seletivo
        private static string PreparaEmailProcessoSeletivo(string[] dados, bool agendamento)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";

            if (agendamento)
                arquivo = @"\ModeloDeEmails\emailProcessoSeletivo_Agendamento.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            body = body.Replace("{vaga}", dados[0]);
            body = body.Replace("{data}", dados[1]);
            body = body.Replace("{hora}", dados[2]);
            body = body.Replace("{empresa}", dados[3]);
            body = body.Replace("{saudacao}", dados[4]);
            body = body.Replace("{candidato}", dados[5]);
            body = body.Replace("{endereco}", dados[6]);
            body = body.Replace("{nome}", dados[7]);
            body = body.Replace("{confidencial}", dados[8]);
            body = body.Replace("{lembrete}", dados[9]);
            body = body.Replace("{complemento}", dados[10]);

            return body;
        }

        public static bool EnviarEmailProcessoSeletivo(string[] dados, string emailOrigem, string emailDestino, string emailCopia, bool agendamento)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress(emailOrigem);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            emailDestino = emailDestino + ";";
            foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.To.Add(address);
            }

            emailCopia = emailCopia + ";";
            foreach (var address in emailCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.CC.Add(address);
            }

            //_mensagem.Bcc.Add("mdmunoz@supportse.com.br");

            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailProcessoSeletivo(dados, agendamento);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            if (agendamento)
                _mensagem.Subject = "Processo Seletivo - Entrevista Marcada" + (dados[8] != "" ? " - Vaga Confidencial" : "");

            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.SendAsync(_mensagem, "envio");
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }
        #endregion

        #region E-mail Programação Ferias

        public static bool EnviarEmailProgramacaoFerias(string[] dados, string emailOrigem, string emailDestino, string emailCopia, string assunto)
        {
            Publicas.mensagemDeErro = "";
            SmtpClient _smtp = new SmtpClient("192.168.5.70", 25);
            MailAddress _emailOrigem = new MailAddress(emailOrigem, "", Encoding.UTF8);

            MailMessage _mensagem = new MailMessage();
            _mensagem.From = _emailOrigem;

            foreach (var address in emailDestino.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.To.Add(address);
            }

            foreach (var address in emailCopia.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Trim() != "")
                    _mensagem.CC.Add(address);
            }

            //_mensagem.Bcc.Add("mdmunoz@supportse.com.br");
            _mensagem.IsBodyHtml = true;
            _mensagem.Body = PreparaEmailProgramacaoFerias(dados);

            if (Publicas.mensagemDeErro != "")
                return false;

            _mensagem.BodyEncoding = System.Text.Encoding.UTF8;
            _mensagem.Subject = assunto;
            _mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
            _smtp.UseDefaultCredentials = true;
            _smtp.EnableSsl = false;
            _smtp.Credentials = new System.Net.NetworkCredential("corp\\suporte", "suporte@niff", "corp");

            try
            {
                _smtp.Send(_mensagem);
                return true;
            }
            catch (Exception ex)
            {
                mensagemDeErro = ex.Message;
                return false;
            }
        }

        private static string PreparaEmailProgramacaoFerias(string[] dados)
        {
            string body = string.Empty;
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());

            string arquivo = "";
            arquivo = @"\ModeloDeEmails\emailProgramacaoFerias.html";

            if (!File.Exists(caminho + arquivo))
            {
                Publicas.mensagemDeErro = "Arquivo de modelo do e-mail não encontrado";
                return "";
            }

            using (StreamReader reader = new StreamReader(caminho + arquivo))
            {
                body = reader.ReadToEnd();
            }

            //body = body.Replace("'", "\"");
            //body = body.Replace("`", "'");
            body = body.Replace("{nome}", dados[0]);
            body = body.Replace("{dataInicio}", dados[1]);
            body = body.Replace("{dataFim}", dados[2]);
            body = body.Replace("{texto}", dados[3]);
            body = body.Replace("{gestor}", dados[4]);
            body = body.Replace("{data}", dados[5]);
            body = body.Replace("{motivo}", dados[6]);

            return body;
        }

        #endregion

        public static string GetDescription(object enumValue, string defDesc)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return defDesc;
        }

        public static Log MontaLog(string descricao, string tela)
        {
            Log _log = new Log();
            _log.Descricao = descricao;
            _log.Tela = tela;
            _log.IdUsuario = Publicas._idUsuario;

            return _log;
        }

        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool ValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool ValidaPIS(string pis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            if (pis.Trim().Length != 11)
                return false;
            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            return pis.EndsWith(resto.ToString());
        }

        #region criptografia
        private static string _key = string.Empty;

        private static CryptProvider _cryptProvider;

        private static SymmetricAlgorithm _algorithm;

        public static string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private static void SetIV()
        {
            switch (_cryptProvider)
            {
                case CryptProvider.Rijndael: _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
                    break;
                default:
                    _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
                    break;
            }
        }


        public static void Criptografia()
        {
            _algorithm = new RijndaelManaged();
            _algorithm.Mode = CipherMode.CBC;
            _cryptProvider = CryptProvider.Rijndael;
        }

        public static void Criptografia(CryptProvider cryptProvider)
        {
            // Seleciona algoritmo simétrico
            switch (cryptProvider)
            {
                case CryptProvider.Rijndael:
                    _algorithm = new RijndaelManaged();
                    _cryptProvider = CryptProvider.Rijndael;
                    break;

                case CryptProvider.RC2:
                    _algorithm = new RC2CryptoServiceProvider();
                    _cryptProvider = CryptProvider.RC2;
                    break;

                case CryptProvider.DES:
                    _algorithm = new DESCryptoServiceProvider();
                    _cryptProvider = CryptProvider.DES;
                    break;

                case CryptProvider.TripleDES:
                    _algorithm = new TripleDESCryptoServiceProvider();
                    _cryptProvider = CryptProvider.TripleDES;
                    break;
            }

            _algorithm.Mode = CipherMode.CBC;
        }

        public static byte[] GetKey(CryptProvider cryptProvider)
        {
            Criptografia(cryptProvider);
            string salt = string.Empty;

            // Ajusta o tamanho da chave se necessário e retorna uma chave válida
            if (_algorithm.LegalKeySizes.Length > 0)
            {
                // Tamanho das chaves em bits

                int keySize = _key.Length * 8;
                int minSize = _algorithm.LegalKeySizes[0].MinSize;
                int maxSize = _algorithm.LegalKeySizes[0].MaxSize;
                int skipSize = _algorithm.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    // Busca o valor máximo da chave
                    _key = _key.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    // Seta um tamanho válido
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;

                    if (keySize < validSize)
                    {
                        // Preenche a chave com arterisco para corrigir o tamanho
                        _key = _key.PadRight(validSize / 8, '*');
                    }
                }
            }

            PasswordDeriveBytes key = new PasswordDeriveBytes(_key, ASCIIEncoding.ASCII.GetBytes(salt));
            return key.GetBytes(_key.Length);
        }

        public static string Encrypta(string texto, CryptProvider cryptProvider)
        {

            byte[] plainByte = Encoding.UTF8.GetBytes(texto);
            byte[] keyByte = GetKey(cryptProvider);


            // Seta a chave privada
            _algorithm.Key = keyByte;
            SetIV();

            // Interface de criptografia / Cria objeto de criptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateEncryptor();

            MemoryStream _memoryStream = new MemoryStream();
            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);

            // Grava os dados criptografados no MemoryStream
            _cryptoStream.Write(plainByte, 0, plainByte.Length);
            _cryptoStream.FlushFinalBlock();

            // Busca o tamanho dos bytes encriptados
            byte[] cryptoByte = _memoryStream.ToArray();

            // Converte para a base 64 string para uso posterior em um xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }

        public static string Decrypta(string textoCriptografado, CryptProvider cryptProvider)
        {
            // Converte a base 64 string em num array de bytes
            byte[] cryptoByte = Convert.FromBase64String(textoCriptografado);
            byte[] keyByte = GetKey(cryptProvider);

            // Seta a chave privada
            _algorithm.Key = keyByte;
            SetIV();

            // Interface de criptografia / Cria objeto de descriptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor();

            try
            {
                MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);

                // Busca resultado do CryptoStream
                StreamReader _streamReader = new StreamReader(_cryptoStream);
                return _streamReader.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region dados Radonico/aleatorio
        public class NumeroUsados
        {
            public int numero { get; set; }
        }

        public static int RetornaPosicaoAleatoria(int tamanho, int inicio, int[] numero)
        {
            int[] arr = numero;

            List<NumeroUsados> _numeros = new List<NumeroUsados>();

            Random rnd = new Random();
            int tmp = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                tmp = rnd.Next(inicio, tamanho + 1);

                while (_numeros.Where(w => w.numero == tmp).Count() != 0)
                {
                    tmp = rnd.Next(inicio, tamanho + 1);
                }

                arr[i] = tmp;

                NumeroUsados _num = new NumeroUsados();
                _num.numero = tmp;
                _numeros.Add(_num);
            }

            return tmp;
        }

        #endregion

        public static string DiaDaSemana(DayOfWeek _tipo)
        {
            string _texto = "Domingo";

            switch (_tipo)
            {
                case DayOfWeek.Monday:
                    _texto = "Segunda";
                    break;
                case DayOfWeek.Tuesday:
                    _texto = "Terça";
                    break;
                case DayOfWeek.Wednesday:
                    _texto = "Quarta";
                    break;
                case DayOfWeek.Thursday:
                    _texto = "Quinta";
                    break;
                case DayOfWeek.Friday:
                    _texto = "Sexta";
                    break;
                case DayOfWeek.Saturday:
                    _texto = "Sabado";
                    break;
            }
            return _texto;
        }

        public static string MesExtenso(string _tipo)
        {
            string _texto = "01";

            switch (_tipo)
            {
                case "01":
                    _texto = "Janeiro";
                    break;
                case "02":
                    _texto = "Fevereiro";
                    break;
                case "03":
                    _texto = "Março";
                    break;
                case "04":
                    _texto = "Abril";
                    break;
                case "05":
                    _texto = "Maio";
                    break;
                case "06":
                    _texto = "Junho";
                    break;
                case "07":
                    _texto = "Julho";
                    break;
                case "08":
                    _texto = "Agosto";
                    break;
                case "09":
                    _texto = "Setembro";
                    break;
                case "10":
                    _texto = "Outubro";
                    break;
                case "11":
                    _texto = "Novembro";
                    break;
                case "12":
                    _texto = "Dezembro";
                    break;
            }
            return _texto;
        }

        public static void AbrirGerenciadorDeArquivos(string path)
        {
            bool isfile = System.IO.File.Exists(path);
            string argument = "";
            if (isfile)
            {
                argument = @"/select, " + path;
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            else
            {
                bool isfolder = System.IO.Directory.Exists(path);
                if (isfolder)
                {
                    argument = @"/select, " + path;
                    System.Diagnostics.Process.Start("explorer.exe", argument);
                }
            }
        }

        public static void AbrirFerramentaDeCapitura()
        {
        
            try
            {
                System.Diagnostics.Process snippingToolProcess = new System.Diagnostics.Process();
                snippingToolProcess.EnableRaisingEvents = true;
                if (!Environment.Is64BitProcess)
                {
                    snippingToolProcess.StartInfo.FileName = "C:\\Windows\\sysnative\\SnippingTool.exe";
                    snippingToolProcess.Start();
                }
                else
                {
                    snippingToolProcess.StartInfo.FileName = "C:\\Windows\\system32\\SnippingTool.exe";
                    snippingToolProcess.Start();
                }
            }
            catch
            {
                //new Notificacoes.Mensagem("Nao foi possível abril a ferramenta de capitura de tela.", Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }
        }

        public static string OnlyNumbers(string toNormalize)
        {
            string resultString = string.Empty;
            Regex regexObj = new Regex(@"[^\d]");
            resultString = regexObj.Replace(toNormalize, "");
            return resultString;
        }

        public static List<ButtonAdv> CentralizarBotoes(List<ButtonAdv> botoes, int larguraTela, int distancia)
        {
            int largura = botoes[0].Width;
            int quantidade = botoes.Count();
            int espaco = (largura * quantidade) + (distancia * quantidade) + largura;
            int left = ((larguraTela - espaco) / 2) + 30;

            for (int i = 0; i < botoes.Count(); i++)
            {
                botoes[i].Left = left;
                left = left + largura + distancia;
            }

            return botoes;
        }

        public static string SerializaParaString<T>(this T valor)
        {
            XmlSerializer xml = new XmlSerializer(valor.GetType());
            StringWriter retorno = new StringWriter();
            xml.Serialize(retorno, valor);
            return retorno.ToString();
        }

        #endregion


    }
}
            