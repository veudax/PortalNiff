using Classes;
using Suportte.Cadastros;
using DynamicFilter;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Suportte.Notificacoes;
using GridScheduleSample;
using Syncfusion.Windows.Forms.Schedule;
using System.Globalization;
using Syncfusion.Windows.Forms.Chart;
//using Site.wsLogin;

namespace Suportte
{
    public partial class Principal : Form
    {
        int _leftSubMenus = 204;
        int _leftSubMenus2 = 406;
        int _topNotificacaoMedia = 876;
        int _topNotificacaoPequena = 898;
        int _tamanhoDosMenusInvisiveisAntesDoCadastro = 0;
        int _tamanhoDosMenusInvisiveisAntesDoSAC = 0;
        int _tamanhoSAC = 0;
        int _tamanhoOriginalSac = 0;
        int _rowIndexComunicado = 0;
        int contadorTempoEncerrarSistema = 0;
        int contadorTempoSenha = -1;
        int _quantidadeGraficos = 0;
        int idSuperior;

        string _dataCompilacao;
        string _versaoAplicativo;
        string tipoAvaliacao;

        bool _comunicadosEmPesquisa = false;
        bool _atualizarComunicado = false;
        bool _chamadosEmPesquisa = false;
        bool _notificacaoEmPesquisa = false;
        bool _notificacaoChamadoEmPesquisa = false;
        bool _andamentoAvaliacaoEmPesquisa = false;
        bool _mudouVisualizacaoAndamento = false;
        bool _notificacaoBolaoEmPesquisa = false;
        bool _parouAtualizacao = false;
        //bool _sacEmPesquisa = false;

        Publicas.StatusComunicado _statusComunicadoSelecionado;

        List<Classes.Chat> _listaChat;
        Empresa _empresa;
        Parametro _parametros;
        Colaboradores _colaboradores;
        NotificacaoDoSistema _notificacao;
        Colaboradores supervisor;
        List<UsuarioLogado> _listaUsuariosLogagos;
        List<Atendimento> _listaAtendimentos;
        List<Usuario> _listaAniversariantes;
        List<Chamado> _listaChamados;
        List<Comunicado> _listaComunicados;
        List<NotificacaoComunicado> _listaComunicadosNotificacao;
        List<NotificacaoChamado> _listaChamadosNotificacao;
        List<Classes.BancoDeHoras> _bancoDeHoras;
        List<Classes.AutoAvaliacao> _listaAvaliacoes;
        List<Classes.AutoAvaliacao> _listaAvaliacoesNotas;
        List<Classes.BolaoJogos> _listaJogosEncerrados;
        List<Classes.BolaoJogos> _listaJogosDos3Dias;

        Classes.Cargos _cargo;

        public Principal()
        {
            InitializeComponent();
            Publicas.mensagemDeErro = "";
            tituloSistemaLabel.Text = Publicas._nomeDoSistema;

            #region temas
            if (DateTime.Now.Date >= Publicas._dataInicioTeste &&
                DateTime.Now.Date <= Publicas._dataFimTeste)
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
                
                Publicas._alterouSkin = true;
                Publicas.AplicarSkin(this);
            }
            else
                if (DateTime.Now.Date >= Publicas._dataInicioPeriodoNatal &&
                DateTime.Now.Date <= Publicas._dataFimAnoNovo)
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
                FundoPictureBox.ImageLocation = @"Imagens\Fundo_Natal_Vermelho.png";

                Publicas._alterouSkin = true;
                Publicas.AplicarSkin(this);
            }
            else
            {
                if (DateTime.Now.Date >= Publicas._dataInicioPeriodoHalloween &&
                    DateTime.Now.Date <= Publicas._dataFimPeriodoHalloween)
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
                    FundoPictureBox.ImageLocation = @"Imagens\Fundo_Halloween.jpg";
                    Publicas._alterouSkin = true;
                    Publicas.AplicarSkin(this);
                }
                else
                {
                    if ((DateTime.Now.Date >= Publicas._dataInicioCopa &&
                         DateTime.Now.Date <= Publicas._dataFimCopa))
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

                        FundoPictureBox.ImageLocation = @"Imagens\Fundo_Copa.jpg";

                        Publicas._alterouSkin = true;
                        Publicas.AplicarSkin(this);
                    }
                    else
                    {
                        if ((DateTime.Now.Date >= Publicas._dataInicioCarnaval &&
                             DateTime.Now.Date <= Publicas._dataFimCarnaval))
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

                            FundoPictureBox.ImageLocation = @"Imagens\Fundo_Carnaval.jpg";

                            Publicas._alterouSkin = true;
                            Publicas.AplicarSkin(this);
                        }
                        else
                        {
                            if ((DateTime.Now.Date >= Publicas._dataInicioPascoa &&
                                 DateTime.Now.Date <= Publicas._dataFimPascoa))
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
                                Publicas._panelTitulo = Publicas.padraoNIFFClaro_BordaEntrada;

                                Publicas._alterouSkin = false;
                            }
                        }
                    }
                }
            }
            #endregion

            menuUsuarioPanel.Size = new Size(menuUsuarioPanel.Width, 58);
            _tamanhoOriginalSac = menuSACPanel.Height;
        }
        

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

        private void MenuPictureBox_MouseHover(object sender, EventArgs e)
        {
            if (Publicas._imagemMenu == "menu5")
            {
                Publicas._imagemMenu = "menu4";
                MenuPictureBox.Image = Properties.Resources.Menu4;
            }
            else
            {
                Publicas._imagemMenu = "menu5";
                MenuPictureBox.Image = Properties.Resources.Menu5;
            }
        }

        private void MenuPictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (Publicas._imagemMenu == "menu5")
            {
                Publicas._imagemMenu = "menu4";
                MenuPictureBox.Image = Properties.Resources.Menu4;
            }
            else
            {
                Publicas._imagemMenu = "menu5";
                MenuPictureBox.Image = Properties.Resources.Menu5;
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
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
        
        private void cadastrosMenuPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuAvulsoPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;
            menuNineBoxPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;

            if (!menuCadastroPanel.Visible)
            {
                menuCadastroPanel.Left = _leftSubMenus;
                menuCadastroPanel.Top = (menuPanel.Width != 206 ? 82 : 107);
                menuCadastroPanel.BringToFront();
                menuCadastroPanel.Visible = true;
            }

            tituloMenuCadastroPanel.Visible = (menuPanel.Width != 206);

            cadastroLabel.Font = new Font(cadastroLabel.Font, FontStyle.Bold);
            cadastrosMenuPanel.BackColor = Color.Silver;
        }

        private void MenuPictureBox_Click(object sender, EventArgs e)
        {
            menuPanel.Visible = !menuPanel.Visible;
        }

        private void chatMenuPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuCadastroPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;

            if (menuPanel.Width != 206)
            {
                menuAvulsoPanel.Left = 51;
                menuAvulsoPanel.BringToFront();

                if (((Control)sender).Name.Contains("chat"))
                {
                    menuAvulsoPanel.Top = 203;
                    menuAvusloLabel.Text = "Chat";
                    menuAvulsoPanel.Visible = true;
                }
                if (((Control)sender).Name.Contains("despesas"))
                {
                    menuAvulsoPanel.Top = 155;
                    menuAvusloLabel.Text = "Despesas";
                    menuAvulsoPanel.Visible = true;
                }
                if (((Control)sender).Name.Contains("agenda"))
                {
                    menuAvulsoPanel.Top = 107;
                    menuAvusloLabel.Text = "Agenda";
                    menuAvulsoPanel.Visible = true;
                }
                if (((Control)sender).Name.Contains("abrirChamado"))
                {
                    menuAvulsoPanel.Top = 59;
                    menuAvusloLabel.Text = "Abrir chamado";
                    menuAvulsoPanel.Visible = true;
                }
                if (((Control)sender).Name.Contains("diversos"))
                {
                    menuAvulsoPanel.Top = 206;
                    menuAvusloLabel.Text = "Diversos";
                    menuAvulsoPanel.Visible = true;
                }
                
            }
            else
            {
                cadastroLabel.Font = new Font(cadastroLabel.Font, cadastroLabel.Font.Style & ~FontStyle.Bold);
                diversosLabel.Font = new Font(diversosLabel.Font, diversosLabel.Font.Style & ~FontStyle.Bold);
                sacLabel.Font = new Font(sacLabel.Font, sacLabel.Font.Style & ~FontStyle.Bold);
                juridicoLabel.Font = new Font(juridicoLabel.Font, juridicoLabel.Font.Style & ~FontStyle.Bold);
                avaliacaoDesempenhoLabel.Font = new Font(avaliacaoDesempenhoLabel.Font, avaliacaoDesempenhoLabel.Font.Style & ~FontStyle.Bold);

                if (((Control)sender).Name.Contains("chat"))
                {
                    chatLabel.Font = new Font(chatLabel.Font, FontStyle.Bold);
                    despesaLabel.Font = new Font(chatLabel.Font, chatLabel.Font.Style & ~FontStyle.Bold);
                    agendaLabel.Font = new Font(agendaLabel.Font, agendaLabel.Font.Style & ~FontStyle.Bold);
                    abrirChamadoLabel.Font = new Font(abrirChamadoLabel.Font, abrirChamadoLabel.Font.Style & ~FontStyle.Bold);
                }
                if (((Control)sender).Name.Contains("despesas"))
                {
                    chatLabel.Font = new Font(chatLabel.Font, chatLabel.Font.Style & ~FontStyle.Bold);
                    agendaLabel.Font = new Font(agendaLabel.Font, agendaLabel.Font.Style & ~FontStyle.Bold);
                    abrirChamadoLabel.Font = new Font(abrirChamadoLabel.Font, abrirChamadoLabel.Font.Style & ~FontStyle.Bold);
                    despesaLabel.Font = new Font(despesaLabel.Font, FontStyle.Bold);
                }
                if (((Control)sender).Name.Contains("agenda"))
                {
                    agendaLabel.Font = new Font(agendaLabel.Font, FontStyle.Bold);

                    chatLabel.Font = new Font(chatLabel.Font, chatLabel.Font.Style & ~FontStyle.Bold);
                    despesaLabel.Font = new Font(agendaLabel.Font, agendaLabel.Font.Style & ~FontStyle.Bold);
                    abrirChamadoLabel.Font = new Font(abrirChamadoLabel.Font, abrirChamadoLabel.Font.Style & ~FontStyle.Bold);
                }
                if (((Control)sender).Name.Contains("abrirChamado"))
                {
                    abrirChamadoLabel.Font = new Font(abrirChamadoLabel.Font, FontStyle.Bold);
                    chatLabel.Font = new Font(chatLabel.Font, chatLabel.Font.Style & ~FontStyle.Bold);
                    agendaLabel.Font = new Font(agendaLabel.Font, agendaLabel.Font.Style & ~FontStyle.Bold);
                    despesaLabel.Font = new Font(abrirChamadoLabel.Font, abrirChamadoLabel.Font.Style & ~FontStyle.Bold);
                }
            }
        }

        private void limpezaMenuPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            // alterado o nome de limpeza para diversos
            menuAvulsoPanel.Visible = false;
            menuCadastroPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;
            menuNineBoxPanel.Visible = false;

            if (!menuLimpezaPanel.Visible)
            {
                menuLimpezaPanel.Left = _leftSubMenus;
                menuLimpezaPanel.Top = (menuPanel.Width != 206 ? 130 : 155);
                menuLimpezaPanel.BringToFront();
                menuLimpezaPanel.Visible = true;
            }

            tituloMenuLimpezaPanel.Visible = (menuPanel.Width != 206);

            diversosLabel.Font = new Font(diversosLabel.Font, FontStyle.Bold);
            diversosMenuPanel.BackColor = Color.Silver;
        }

        private void usuariosMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCadastro;
            new Usuarios().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void sistemasMenuPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuLimpezaPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuCorridasPanel.Visible = false;

            if (!menuSistemasPanel.Visible)
            {
                menuSistemasPanel.Left = _leftSubMenus2;
                menuSistemasPanel.Top = 299;
                menuSistemasPanel.BringToFront();
                menuSistemasPanel.Visible = true;
            }

            sistemasLabel.Font = new Font(sistemasLabel.Font, FontStyle.Bold);
            sistemasMenuPanel.BackColor = Color.Silver;
        }

        private void abrirMenuUsuarioPictureBox_Click(object sender, EventArgs e)
        {
            escondeAbreChatPictureBox.Image = Properties.Resources.SetaUpBranca;
            chatPanel.Location = new Point(chatPanel.Location.X, panel2.Location.Y - 25);
            chatPanel.Size = new Size(chatPanel.Width, 25);
            menuUsuarioPanel.BringToFront();
            menuUsuarioPanel.Visible = true;
            menuUsuarioTimer.Start();
        }

        private void Principal_Shown(object sender, EventArgs e)
        {

            _notificacao = new NotificacaoDoSistemaBO().Consultar();
            if (_notificacao.Existe)
            {
                if (_notificacao.DataDaAcao <= DateTime.Now && _notificacao.DataFimDaAcao >= DateTime.Now)
                {
                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Abriu o sistema durante a atualização";
                    _log.Tela = "Principal - Login";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                    Close();
                    return;
                }
            }

            menuPanel.Width = 54; //reduzido 
            _leftSubMenus = 53; //reduzido 
            _leftSubMenus2 = 258; //reduzido
            recolherMenuPictureBox.Left = 12;

            recolherMenuPictureBox_Click(sender, e);

            loginPanel.Left = 104;
            loginPanel.Top = 368;

            #region Ajusta Posição login
            loginPanel.Location = new Point(loginPanel.Left, (Screen.PrimaryScreen.WorkingArea.Size.Height - tituloPanel.Height - panel2.Height - loginPanel.Height) / 2);
            #endregion 

            dashBoardTabControl.Location = new Point(menuPanel.Width + 5, tituloPanel.Height + 5);
            dashBoardTabControl.Size = new Size(this.Size.Width - (menuPanel.Width + 15), this.Size.Height - (tituloPanel.Height + panel2.Height + 35));

            int tamanho = (int)(colaboradoresAvaliacaoPanel.Width / 3);
            emAndamentoPanel.Size = new Size(tamanho, emAndamentoPanel.Height);
            naoIniciadasPanel.Size = new Size(tamanho, emAndamentoPanel.Height);
            finalizadasPanel.Size = new Size(tamanho, emAndamentoPanel.Height);
            _topNotificacaoPequena = panel2.Location.Y - 25;
            _topNotificacaoMedia = panel2.Location.Y - 25;

            //MessageBox.Show();
            
            _versaoAplicativo = Application.ProductVersion;
            _dataCompilacao = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();
            usuarioLoginTextBox.Focus();
        }

        private void esqueceuSenhaLabel_MouseHover(object sender, EventArgs e)
        {
            esqueceuSenhaLabel.ForeColor = Color.DarkGreen;
        }

        private void esqueceuSenhaLabel_MouseLeave(object sender, EventArgs e)
        {
            esqueceuSenhaLabel.ForeColor = label27.ForeColor;
        }

        private void primeiroAcessoLabel_MouseHover(object sender, EventArgs e)
        {
            primeiroAcessoLabel.ForeColor = Color.DarkGreen;
        }

        private void primeiroAcessoLabel_MouseLeave(object sender, EventArgs e)
        {
            primeiroAcessoLabel.ForeColor = Color.Black;
        }

        private void acessarLoginButton_Click(object sender, EventArgs e)
        {
            Publicas.ValidacaoUsuario retorno;

            // Object[] retornoValidaUsuario;
            acessarLoginButton.BackColor = Publicas.padraoNIFFClaro_Botao;
            acessarLoginButton.ForeColor = Publicas.padraoNIFFClaro_FonteBotao;

            #region Validação do usuário 
            if (string.IsNullOrEmpty(usuarioLoginTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o usuário!", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioLoginTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(senhaLoginTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a senha!", Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaLoginTextBox.Focus();
                return;
            }

            // não encontrou a string de conexão e não usou a classe publica. criou uma nova sessao da classe publica, com isso as variaveis ficaram vazias. verificar porque.
            //retorno = (Publicas.ValidacaoUsuario)new LoginWS().ValidarUsuario(usuarioLoginTextBox.Text, senhaLoginTextBox.Text);

            retorno = new LoginBO().ValidarUsuario(usuarioLoginTextBox.Text, senhaLoginTextBox.Text, Publicas._conexaoString);

            //retorno = (Publicas.ValidacaoUsuario)((Object[])retornoValidaUsuario)[0];
            //Publicas._usuario = (Usuario)((Object[])retornoValidaUsuario)[1];
            //Publicas._idUsuario = (int)((Object[])retornoValidaUsuario)[2];

            if (retorno == Publicas.ValidacaoUsuario.ProblemaAoConectar)
            {
                new Notificacoes.Mensagem(Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                senhaLoginTextBox.Text = string.Empty;
                usuarioLoginTextBox.Focus();
                return;
            }
            if (retorno == Publicas.ValidacaoUsuario.ErroConsulta)
            {
                new Notificacoes.Mensagem("Problemas durante a consulta." +
                        Environment.NewLine +
                        Publicas.mensagemDeErro, Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaLoginTextBox.Text = string.Empty;
                usuarioLoginTextBox.Focus();
                return;
            }
            if (retorno == Publicas.ValidacaoUsuario.UsuarioNaoCadastrado)
            {
                new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaLoginTextBox.Text = string.Empty;
                usuarioLoginTextBox.Focus();
                return;
            }

            if (retorno == Publicas.ValidacaoUsuario.UsuarioInativo)
            {
                new Notificacoes.Mensagem("Usuário inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaLoginTextBox.Text = string.Empty;
                senhaLoginTextBox.Focus();
                return;
            }

            if (retorno == Publicas.ValidacaoUsuario.SenhaInvalida)
            {
                new Notificacoes.Mensagem("Senha inválida.", Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaLoginTextBox.Text = string.Empty;
                senhaLoginTextBox.Focus();
                return;
            }

            loginPanel.Visible = false;
            Refresh();

            if (Publicas._usuario.CPF != 0)
            {
                if (senhaLoginTextBox.Text == Publicas._usuario.CPF.ToString().Substring(0, 6))
                {
                    new Notificacoes.Mensagem("Identificamos que está usando a senha padrão." +
                        Environment.NewLine + "Por gentileza troque sua senha.", Publicas.TipoMensagem.Informacao).ShowDialog();
                    new TrocaDeSenha().ShowDialog();
                }
            }

            new LoginBO().AlterarStatusUsuario(Publicas._idUsuario, Publicas.StatusUsuario.OnLine, Publicas._conexaoString);

            if (Publicas._usuario.AcessaChat)
                onLineLabel.Font = new Font(onLineLabel.Font, FontStyle.Bold);

            usuarioLogadoLabel.Text = "Olá," + Environment.NewLine + Publicas._usuariologado;
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

            #region Parametros gerais
            Publicas._prazoReabrir = 1;

            _parametros = new ParametrosBO().Consultar();
            if (_parametros.Existe)
                Publicas._prazoReabrir = _parametros.PrazoReabertura;
            #endregion

            #region habilita menu principal
            agendaMenuPanel.Enabled = Publicas._usuario.AcessaAgenda;
            agendaLabel.Enabled = Publicas._usuario.AcessaAgenda;

            despesasMenuPanel.Enabled = Publicas._usuario.AcessaAgenda;
            despesaLabel.Enabled = Publicas._usuario.AcessaAgenda;

            chatMenuPanel.Enabled = Publicas._usuario.AcessaChat;
            chatLabel.Enabled = Publicas._usuario.AcessaChat;

            cadastrosMenuPanel.Enabled = Publicas._usuario.Administrador;
            cadastroLabel.Enabled = Publicas._usuario.Administrador;

            diversosMenuPanel.Enabled = Publicas._usuario.AcessaBI;
            diversosLabel.Enabled = Publicas._usuario.AcessaBI;
            radarBIPanel.Visible = Publicas._usuario.AcessaBI;

            integrarNotasPanel.Visible = (Publicas._usuario.IdDepartamento == 22 || Publicas._usuario.Administrador);
            //ajustePedidoPanel.Visible = Publicas._usuario.Administrador;

            menuLimpezaPanel.Height = 318 - (descontoBeneficioPanel.Height + ajustesNFePanel.Height + ajustePedidoPanel.Height + graficosChamadosPanel.Height + 
                ((Publicas._usuario.IdDepartamento == 22 || Publicas._usuario.Administrador) ? 0 : integrarNotasPanel.Height)) ;

            // em desenvolvimento
            //ajustesNFePanel.Enabled = Publicas._usuario.Administrador;
            //descontoBeneficioPanel.Enabled = Publicas._usuario.AcessaDescontoBeneficio;

            sacPanel.Enabled = Publicas._usuario.AcessaSac;
            sacLabel.Enabled = Publicas._usuario.AcessaSac;

            juridicoLabel.Enabled = Publicas._usuario.AcessaJuridico;
            juridicoPanel.Enabled = Publicas._usuario.AcessaJuridico;
            cadastrosComunicadoPanel.Enabled = Publicas._usuario.AcessaCadastroJuridico;
            
            avaliacaoDesempenhoMenuPanel.Visible = Publicas._usuario.AcessaAvaliacaoDesempenho;
            avaliacaoDesempenhoLabel.Enabled = Publicas._usuario.AcessaAvaliacaoDesempenho;
            //cadastroAvaliacaoPanel.Visible = Publicas._usuario.AcessoDeRH;
            RecursosHumanosPanel.Visible = Publicas._usuario.AcessoDeRH;
            controladoriaPanel.Visible = Publicas._usuario.AcessoDeControladoria;

            colaboradoresPanel.Visible = Publicas._usuario.AcessoDeColaborador;
            GestorPanel.Visible = Publicas._usuario.AcessoDeGestor;

            BolaoCopaPanel.Visible = Publicas._usuario.AdministraBolaoCopa || Publicas._usuario.Administrador ||
            ((DateTime.Now.Date >= Publicas._dataInicioCopa.AddDays(-14) && DateTime.Now.Date <= Publicas._dataFimCopa.AddDays(3))) &&
            Publicas._usuario.ParticipaBolaoCopa && (Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.UsuarioAcesso == "ESILVA")
            ;

            SelecoesPanel.Visible = Publicas._usuario.AdministraBolaoCopa;
            JogosPanel.Visible = Publicas._usuario.AdministraBolaoCopa;
            ResultadoJogosPanel.Visible = Publicas._usuario.AdministraBolaoCopa;
            ValorArrecadadoPanel.Visible = Publicas._usuario.AdministraBolaoCopa;

            MenuBolaoCopaMundo.Size = new Size(MenuBolaoCopaMundo.Width, 179);

            if (!Publicas._usuario.AdministraBolaoCopa)
                MenuBolaoCopaMundo.Size = new Size(MenuBolaoCopaMundo.Width, 80);

            bibliotecaPanel.Enabled = ( Publicas._usuario.IdDepartamento == 12 || Publicas._usuario.Administrador) &&
                                      (Publicas._usuario.IdEmpresa == 1); //RH NIFF

            menuAvaliacaoDesempenhoPanel.Height = 224 - (
                (Publicas._usuario.AcessoDeRH ? 0 : RecursosHumanosPanel.Height) +                
                (Publicas._usuario.AcessoDeColaborador ? 0 : colaboradoresPanel.Height) +
                (Publicas._usuario.AcessoDeControladoria ? 0 : controladoriaPanel.Height) +
                (Publicas._usuario.AcessoDeGestor ? 0 : GestorPanel.Height));

            abrirChamadoMenuPanel.Enabled = false;
            abrirChamadoLabel.Enabled = false;

            MenuPictureBox.Visible = true;
            trocaSenhaPanel.Visible = true;
            editarPerfilPanel.Visible = true;
            trocarUsuarioPanel.Visible = true;

            agruparButton.Visible = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente;
            agruparChamadosToolStripMenuItem.Visible = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente;
            moverChamadosToolStripMenuItem.Visible = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente;
            #endregion

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

            #region acessos Menu SAC
            _tamanhoDosMenusInvisiveisAntesDoCadastro = (!Publicas._usuario.AcessaAgenda ? agendaMenuPanel.Height : 0) +
                                         (!Publicas._usuario.AcessaAgenda ? despesasMenuPanel.Height : 0) +
                                         (!Publicas._usuario.AcessaChat ? chatMenuPanel.Height : 0);

            _tamanhoDosMenusInvisiveisAntesDoSAC = _tamanhoDosMenusInvisiveisAntesDoCadastro +
                (!Publicas._usuario.Administrador ? cadastrosMenuPanel.Height : 0) + 
                (!Publicas._usuario.AcessaBI ? diversosMenuPanel.Height : 0);
            
            //habilitar os menus do sac conforme usuario

            atendimentoMenuPanel.Visible = (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Atendente) ||
                                           (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador);

            retornarLigacoesMenuPanel.Visible = (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Atendente) ||
                                           (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador);

            responderMenuPanel.Visible = (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.UsuarioComum) ||
                                           (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador);

            finalizarMenuPanel.Visible = (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Finalizador) ||
                                           (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador);

            //relatoriosMenuPanel.Visible = (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Atendente) ||
            //                              (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Finalizador) ||
            //                              (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador);

            atendimentoToolStripMenuItem.Visible = (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Atendente) ||
                          (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador);

            retornarToolStripMenuItem.Visible = (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Atendente) ||
                                           (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador);

            responderToolStripMenuItem.Visible = (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.UsuarioComum) ||
                                           (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador);

            finalizarToolStripMenuItem.Visible = (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Finalizador) ||
                                           (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador);

            _tamanhoSAC = ((Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Atendente) ||
                           (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador) ? 0 : atendimentoMenuPanel.Height + retornarLigacoesMenuPanel.Height) +
                            ((Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.UsuarioComum) ||
                            (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador) ? 0 : responderMenuPanel.Height) +
                           ((Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Finalizador) ||
                            (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador) ? 0 : finalizarMenuPanel.Height) + 
                           ((Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Atendente) ||
                            (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Finalizador) ||
                            (Publicas._usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador) ? 0 : relatoriosMenuPanel.Height) ;

            ChamadosTabPage.TabVisible = false;
            AtendimentoSACTabPage.TabVisible = Publicas._usuario.AcessaSac;
            andamentoAvaliacaoTabPage.TabVisible = (Publicas._usuario.AcessoDeRH || (Publicas._usuario.AcessoDeGestor && supervisor.Existe)) && Publicas._usuario.AcessaAvaliacaoDesempenho;
            
            if (andamentoAvaliacaoTabPage.TabVisible)
            {
                int tamanho = (int)(colaboradoresAvaliacaoPanel.Width / 3);
                emAndamentoPanel.Size = new Size(tamanho, emAndamentoPanel.Height);
                naoIniciadasPanel.Size = new Size(tamanho, emAndamentoPanel.Height);
                finalizadasPanel.Size = new Size(tamanho, emAndamentoPanel.Height);

                List<Classes.Prazos> _prazos = new PrazosBO().Listar(true, true);

                referenciasComboBox.Items.Clear();
                tipoAvaliacaoComboBox.Items.Clear();

                tipoAvaliacaoComboBox.Items.AddRange(new object[] { "Auto Avaliação", "Feedback do Gestor", "Metas numéricas", "Avaliação do Gestor", "Avaliação do RH", "Feedback do Avaliado", "Plano de Desenvolvimento individual" });

                foreach (var item in _prazos)
                {
                    referenciasComboBox.Items.Add(item.Referencia);
                }

                referenciasComboBox.SelectedIndex = 0;
                tipoAvaliacaoComboBox.SelectedIndex = 0;
            }

            sacTimer.Enabled = Publicas._usuario.AcessaSac;

            GridDynamicFilter filter = new GridDynamicFilter();
            GridMetroColors metroColor = new GridMetroColors();
            if (Publicas._usuario.AcessaSac)
            {
                sacTimer_Tick(sender, e);
                                
                filter.ApplyFilterOnlyOnCellLostFocus = true;
                filter.WireGrid(this.sacGridGroupingControl);

                sacGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
                sacGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
                sacGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
                sacGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
                sacGridGroupingControl.RecordNavigationBar.Label = "Atendimentos";
                sacGridGroupingControl.TableControl.CellToolTip.Active = true;

                for (int i = 0; i < sacGridGroupingControl.TableDescriptor.Columns.Count; i++)
                {
                    sacGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                    sacGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                    sacGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                    sacGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                    sacGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                    sacGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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
                
                this.sacGridGroupingControl.SetMetroStyle(metroColor);

                this.sacGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

                this.sacGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.sacGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
                this.sacGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            }

            menuSACPanel.Size =  new Size(menuSACPanel.Width, menuSACPanel.Height - _tamanhoSAC - relatoriosMenuPanel.Height);
            menuSACPanel.Refresh();
            #endregion

            #region Chamados
            chamadoTimer_Tick(sender, e);
            chamadoTimer.Start();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.chamadosGridGroupingControl);

            chamadosGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            chamadosGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            chamadosGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            chamadosGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            chamadosGridGroupingControl.RecordNavigationBar.Label = "Chamados";
            chamadosGridGroupingControl.TableControl.CellToolTip.Active = true;
            chamadosGridGroupingControl.TableDescriptor.AllowEdit = false;
            for (int i = 0; i < chamadosGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                chamadosGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                chamadosGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                chamadosGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                chamadosGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                chamadosGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                chamadosGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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

            this.chamadosGridGroupingControl.SetMetroStyle(metroColor);

            this.chamadosGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.chamadosGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.chamadosGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.chamadosGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            #endregion

            #region Comunicados
            juridicoTabPageAdv.TabVisible = Publicas._usuario != null && Publicas._usuario.AcessaJuridico;
            anosComunicadosComboBox_SelectedIndexChanged(sender, e);
            comunicadoTimer_Tick(sender, e);
            comunicadoTimer.Start();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.comunicadoGroupingControl);

            comunicadoGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            comunicadoGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            comunicadoGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            comunicadoGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            comunicadoGroupingControl.RecordNavigationBar.Label = "Comunicados";
            comunicadoGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < comunicadoGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                comunicadoGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                comunicadoGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                comunicadoGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                comunicadoGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                comunicadoGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                comunicadoGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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

            this.comunicadoGroupingControl.SetMetroStyle(metroColor);

            this.comunicadoGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.comunicadoGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.comunicadoGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.comunicadoGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            anosComunicadosComboBox.Items.Clear();
            List<int> datas = new ComunicadoBO().Datas();

            foreach (var item in datas)
            {
                anosComunicadosComboBox.Items.Add(item);
            }

            anosComunicadosComboBox.Items.Add("Todos");
            anosComunicadosComboBox.SelectedIndex = 0;
            #endregion

            #region Aniversariantes
            try
            {
                int topAniversario = _topNotificacaoPequena - (aniversarioPanel.Height + 5);
                int qtd = 1;

                if (Publicas._usuario.AniversariantesApenasDaEmpresa)
                    _listaAniversariantes = new UsuarioBO().ListarAniversariantesDaSemana(Publicas._usuario.IdEmpresa,7).OrderBy(o => o.DataNascimento).ToList();
                else
                    _listaAniversariantes = new UsuarioBO().ListarAniversariantesDaSemana(0,7).OrderBy(o => o.DataNascimento).ToList();

                Publicas._fecharAniversarios = true;

                foreach (var item in _listaAniversariantes)
                {
                    if (item.Id == Publicas._idUsuario && item.DataNascimento == DateTime.Now.Date)
                    {
                        Publicas._aniversario = new AniversarioBO().Consulta();

                        if (!Publicas._aniversario.Existe ||
                            (Publicas._aniversario.Existe && Publicas._aniversario.MostrarMensagem))
                        {
                            Publicas._fecharAniversarios = false;
                            Aniversario _tela = new Aniversario();
                            _tela.TopMost = true;
                            _tela.Show();
                        }
                    }
                    else
                    {
                        if (qtd > 6)
                            break;

                        if (!aniversarioPanel.Visible)
                        {
                            aniversarioPanel.Location = new Point(aniversarioPanel.Left, topAniversario);
                            tituloDataLabel.Text = "Aniversariante";
                            dataLabel.Text = item.DataNascimento.ToShortDateString();
                            nomeLabel.Text = item.Nome;
                            empresaLabel.Text = item.Empresa;
                            aniversarioPanel.BringToFront();
                            aniversarioPanel.Visible = true;
                        }
                        else
                        {
                            Panel panel = new Panel();
                            panel.Name = "aniversarioPanel" + qtd.ToString();

                            Panel panelTitulo = new Panel();
                            Label labelTitulo = new Label();
                            Label labelData = new Label();
                            Label labelNome = new Label();
                            Label labelEmpresa = new Label();
                            PictureBox imagem = new PictureBox();
                            PictureBox imagemBalao = new PictureBox();
                            imagem.Click += new System.EventHandler(this.powerPictureBox_Click_1);

                            imagem.Size = powerPictureBox.Size;
                            imagem.Image = powerPictureBox.Image;
                            imagem.SizeMode = PictureBoxSizeMode.StretchImage;

                            imagemBalao.Size = balaoPictureBox.Size;
                            imagemBalao.Image = balaoPictureBox.Image;
                            imagemBalao.SizeMode = balaoPictureBox.SizeMode;
                            imagemBalao.Location = balaoPictureBox.Location;

                            panel.Size = aniversarioPanel.Size;
                            panel.Location = new Point(aniversarioPanel.Left, topAniversario);
                            panelTitulo.Size = tituloAniversariantesPanel.Size;
                            panelTitulo.BackColor = tituloAniversariantesPanel.BackColor;

                            panel.Controls.Add(panelTitulo);
                            panel.Controls.Add(imagemBalao);

                            panelTitulo.Dock = DockStyle.Top;
                            panelTitulo.Controls.Add(labelTitulo);
                            panelTitulo.Controls.Add(imagem);
                            imagem.Dock = DockStyle.Right;
                            labelTitulo.Dock = DockStyle.Fill;
                            labelTitulo.ForeColor = tituloDataLabel.ForeColor;
                            labelTitulo.Font = tituloDataLabel.Font;
                            labelTitulo.TextAlign = ContentAlignment.MiddleLeft;

                            labelData.Location = dataLabel.Location;
                            labelData.AutoSize = false;
                            labelData.Size = dataLabel.Size;
                            labelData.ForeColor = dataLabel.ForeColor;
                            labelData.Font = dataLabel.Font;

                            labelData.TextAlign = ContentAlignment.MiddleRight;
                            labelNome.Location = nomeLabel.Location;
                            labelNome.AutoSize = false;
                            labelNome.ForeColor = nomeLabel.ForeColor;
                            labelNome.Font = nomeLabel.Font;
                            labelNome.Size = nomeLabel.Size;
                            labelNome.TextAlign = ContentAlignment.MiddleLeft;

                            labelEmpresa.Location = empresaLabel.Location;
                            labelEmpresa.AutoSize = false;
                            labelEmpresa.Size = empresaLabel.Size;
                            labelEmpresa.TextAlign = ContentAlignment.MiddleLeft;
                            labelEmpresa.ForeColor = empresaLabel.ForeColor;
                            labelEmpresa.Font = empresaLabel.Font;

                            panel.Controls.Add(labelData);
                            panel.Controls.Add(labelNome);
                            panel.Controls.Add(labelEmpresa);

                            labelTitulo.Text = "Aniversariante";
                            labelData.Text = item.DataNascimento.ToShortDateString();
                            labelNome.Text = item.Nome;
                            labelEmpresa.Text = item.Empresa;

                            panel.Visible = true;
                            this.Controls.Add(panel);

                            panel.BringToFront();
                        }
                        topAniversario = topAniversario - (aniversarioPanel.Height + 5);
                        Refresh();
                        qtd++;
                    }
                }
                aniversariosTimer.Start();
            }
            catch { }
            #endregion

            #region Log
            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Usuário efetuou o login";
            _log.Tela = "Principal - Login";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            #endregion

            notificacaoTimer.Start();
            notificacaoChamadoTimer.Start();

            #region Feriado
            Feriado _feriado = new FeriadoBO().Consultar(Publicas._usuario.IdEmpresa);

            if (_feriado.Existe)
            {
                int topAniversario = _topNotificacaoPequena - (aniversarioPanel.Height + 5);

                DataFeriadoLabel.Text = _feriado.Data.ToShortDateString();
                nomeFeriadoLabel.Text = _feriado.Descricao;
                feriadoPanel.Location = new Point(aniversarioPanel.Left, topAniversario);
                feriadoPanel.BringToFront();
                feriadoPanel.Visible = true;
            }
            feriadoTimer.Start();
            #endregion

            #region grids avaliação
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


            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.emAndamentoGridGroupingControl);

            emAndamentoGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            emAndamentoGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            emAndamentoGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            emAndamentoGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            emAndamentoGridGroupingControl.RecordNavigationBar.Label = "Colaboradores";
            emAndamentoGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < emAndamentoGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                emAndamentoGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                emAndamentoGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                emAndamentoGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                emAndamentoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                emAndamentoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                emAndamentoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            this.emAndamentoGridGroupingControl.SetMetroStyle(metroColor);

            this.emAndamentoGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.emAndamentoGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.emAndamentoGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.emAndamentoGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            filter.WireGrid(this.naoIniciadoGridGroupingControl);

            naoIniciadoGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            naoIniciadoGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            naoIniciadoGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            naoIniciadoGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            naoIniciadoGridGroupingControl.RecordNavigationBar.Label = "Colaboradores";
            naoIniciadoGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < naoIniciadoGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                naoIniciadoGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                naoIniciadoGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                naoIniciadoGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                naoIniciadoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                naoIniciadoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                naoIniciadoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            this.naoIniciadoGridGroupingControl.SetMetroStyle(metroColor);

            this.naoIniciadoGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.naoIniciadoGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.naoIniciadoGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.naoIniciadoGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            filter.WireGrid(this.finalizadosGridGroupingControl);

            finalizadosGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            finalizadosGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            finalizadosGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            finalizadosGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            finalizadosGridGroupingControl.RecordNavigationBar.Label = "Colaboradores";
            finalizadosGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < finalizadosGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                finalizadosGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                finalizadosGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                finalizadosGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                finalizadosGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                finalizadosGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                finalizadosGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            this.finalizadosGridGroupingControl.SetMetroStyle(metroColor);

            this.finalizadosGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.finalizadosGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.finalizadosGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.finalizadosGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

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

            this.rankingGridGroupingControl.SetMetroStyle(metroColor);

            this.rankingGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.rankingGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.rankingGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.rankingGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            #endregion

            #region GridBanco de horas
            bancoHorasGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            bancoHorasGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            bancoHorasGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            bancoHorasGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            bancoHorasGridGroupingControl.RecordNavigationBar.Label = "Atendimentos";
            bancoHorasGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < bancoHorasGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                bancoHorasGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                bancoHorasGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                bancoHorasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                bancoHorasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                bancoHorasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                bancoHorasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            this.bancoHorasGridGroupingControl.SetMetroStyle(metroColor);

            this.bancoHorasGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.bancoHorasGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.bancoHorasGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.bancoHorasGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.bancoHorasGridGroupingControl.Table.DefaultRecordRowHeight = 35;
            #endregion

            if (Publicas._usuario.Administrador)
            {
                new UsuarioBO().IncluirUsuariosCriadoNoGlobus();
                new UsuarioBO().DesativaUsuarios();
            }
            
            menuUsuarioPanel.Size = new Size(menuUsuarioPanel.Width, 200);

            menuPanel.Visible = true;
            this.Refresh();

        }
         
        private void usuarioLoginTextBox_Enter(object sender, EventArgs e)
        {
            usuarioLoginTextBox.BorderColor = Publicas._bordaEntrada;
        }

        private void senhaLoginTextBox_Enter(object sender, EventArgs e)
        {
            senhaLoginTextBox.BorderColor = Publicas._bordaEntrada;
        }

        private void usuarioLoginTextBox_Validating(object sender, CancelEventArgs e)
        {
            usuarioLoginTextBox.BorderColor = Publicas._bordaSaida;
        }

        private void senhaLoginTextBox_Validating(object sender, CancelEventArgs e)
        {
            senhaLoginTextBox.BorderColor = Publicas._bordaSaida;
        }

        private void usuarioLoginTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                senhaLoginTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void senhaLoginTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                acessarLoginButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                usuarioLoginTextBox.Focus();
            }
        }
        
        private void empresasMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCadastro;
            new Empresas().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void categoriaPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaChamado;
            new Categorias().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void menuUsuarioPanel_MouseLeave(object sender, EventArgs e)
        {
            menuUsuarioPanel.Visible = !menuUsuarioPanel.Visible;
        }

        private void menuUsuarioTimer_Tick(object sender, EventArgs e)
        {
            MenuBolaoCopaMundo.Visible = false;
            menuUsuarioPanel.Visible = false;
            menuUsuarioTimer.Stop();
        }

        private void localizacaoMenuPanel_MouseHover(object sender, EventArgs e)
        {
            if (menuSistemasPanel.Visible)
                menuSistemasPanel.Visible = false;
        }

        private void recolherMenuPictureBox_Click(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();

            // primeiro reduz o menu, para depois posicionar os lefts.
            menuAvulsoPanel.Visible = false;
            menuCadastroPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuNineBoxPanel.Visible = false;

            menuPanel.Width = (menuPanel.Width == 206 ? 54 : 206);
            _leftSubMenus = (menuPanel.Width == 206 ? 204 : 53);
            _leftSubMenus2 = (menuPanel.Width == 206 ? 406 : 258);
            recolherMenuPictureBox.Left = (menuPanel.Width == 206 ? 82 : 12);
            recolherMenuPictureBox.ImageLocation = (menuPanel.Width == 206 ? @"Imagens\reduzir.png" : @"Imagens\Expandir.png");

            dashBoardTabControl.Location = new Point(menuPanel.Width + 5, tituloPanel.Height + 5);
            dashBoardTabControl.Size = new Size(this.Size.Width - (menuPanel.Width + 15), this.Size.Height - (tituloPanel.Height + panel2.Height + 35));

            int tamanho = (int)(colaboradoresAvaliacaoPanel.Width / 3);
            emAndamentoPanel.Size = new Size(tamanho, emAndamentoPanel.Height);
            naoIniciadasPanel.Size = new Size(tamanho, emAndamentoPanel.Height);
            finalizadasPanel.Size = new Size(tamanho, emAndamentoPanel.Height);
            
        }

        private void SelecaoMenuCorSemSerSelecionada()
        {
            #region Deixa os Panels sem seleção
            #region outros
            abrirChamadoMenuPanel.BackColor = Color.WhiteSmoke;
            abrirChamadoLabel.Font = new Font(abrirChamadoLabel.Font, abrirChamadoLabel.Font.Style & ~FontStyle.Bold);
            cadastrosMenuPanel.BackColor = Color.WhiteSmoke;
            cadastroLabel.Font = new Font(cadastroLabel.Font, cadastroLabel.Font.Style & ~FontStyle.Bold);
            diversosMenuPanel.BackColor = Color.WhiteSmoke;
            diversosLabel.Font = new Font(diversosLabel.Font, diversosLabel.Font.Style & ~FontStyle.Bold);
            sacPanel.BackColor = Color.WhiteSmoke;
            sacLabel.Font = new Font(sacLabel.Font, sacLabel.Font.Style & ~FontStyle.Bold);
            juridicoPanel.BackColor = Color.WhiteSmoke;
            juridicoLabel.Font = new Font(juridicoLabel.Font, juridicoLabel.Font.Style & ~FontStyle.Bold);
            avaliacaoDesempenhoMenuPanel.BackColor = Color.WhiteSmoke;
            avaliacaoDesempenhoLabel.Font = new Font(avaliacaoDesempenhoLabel.Font, avaliacaoDesempenhoLabel.Font.Style & ~FontStyle.Bold);
            agendaMenuPanel.BackColor = Color.WhiteSmoke;
            agendaLabel.Font = new Font(agendaLabel.Font, agendaLabel.Font.Style & ~FontStyle.Bold);
            despesasMenuPanel.BackColor = Color.WhiteSmoke;
            despesaLabel.Font = new Font(despesaLabel.Font, despesaLabel.Font.Style & ~FontStyle.Bold);
            chatMenuPanel.BackColor = Color.WhiteSmoke;
            chatLabel.Font = new Font(chatLabel.Font, chatLabel.Font.Style & ~FontStyle.Bold);
            usuariosMenuPanel.BackColor = Color.WhiteSmoke;
            usuarioLabel.Font = new Font(usuarioLabel.Font, usuarioLabel.Font.Style & ~FontStyle.Bold);
            parametrosMenuPanel.BackColor = Color.WhiteSmoke;
            parametrosLabel.Font = new Font(parametrosLabel.Font, parametrosLabel.Font.Style & ~FontStyle.Bold);
            empresasMenuPanel.BackColor = Color.WhiteSmoke;
            empresasLabel.Font = new Font(empresasLabel.Font, empresasLabel.Font.Style & ~FontStyle.Bold);
            salaReuniaoMenuPanel.BackColor = Color.WhiteSmoke;
            salaReuniaoLabel.Font = new Font(salaReuniaoLabel.Font, salaReuniaoLabel.Font.Style & ~FontStyle.Bold);
            sistemasMenuPanel.BackColor = Color.WhiteSmoke;
            sistemasLabel.Font = new Font(sistemasLabel.Font, sistemasLabel.Font.Style & ~FontStyle.Bold);
            tipoAtendimentoMenuPanel.BackColor = Color.WhiteSmoke;
            tipoAtendimentoLabel.Font = new Font(tipoAtendimentoLabel.Font, tipoAtendimentoLabel.Font.Style & ~FontStyle.Bold);
            tipoEmtuMenuPanel.BackColor = Color.WhiteSmoke;
            tipoEMTULabel.Font = new Font(tipoEMTULabel.Font, tipoEMTULabel.Font.Style & ~FontStyle.Bold);
            corridasPanel.BackColor = Color.WhiteSmoke;
            corridasLabel.Font = new Font(corridasLabel.Font, corridasLabel.Font.Style & ~FontStyle.Bold);
            notificacaoPanel.BackColor = Color.WhiteSmoke;
            notificacaoLabel.Font = new Font(notificacaoLabel.Font, notificacaoLabel.Font.Style & ~FontStyle.Bold);
            periodoBancoHorasPanel.BackColor = Color.WhiteSmoke;
            periodoBancoHorasLabel.Font = new Font(periodoBancoHorasLabel.Font, periodoBancoHorasLabel.Font.Style & ~FontStyle.Bold);
            cadastrosComunicadoPanel.BackColor = Color.WhiteSmoke;
            cadastrosComunicadoLabel.Font = new Font(cadastrosComunicadoLabel.Font, cadastrosComunicadoLabel.Font.Style & ~FontStyle.Bold);
            novoComunicadoPanel.BackColor = Color.WhiteSmoke;
            novoComunicadoLabel.Font = new Font(novoComunicadoLabel.Font, novoComunicadoLabel.Font.Style & ~FontStyle.Bold);
            cadastroCorridasPanel.BackColor = Color.WhiteSmoke;
            cadastroCorridasLabel.Font = new Font(cadastroCorridasLabel.Font, cadastroCorridasLabel.Font.Style & ~FontStyle.Bold);
            participantesPanel.BackColor = Color.WhiteSmoke;
            participantesLlabel.Font = new Font(participantesLlabel.Font, participantesLlabel.Font.Style & ~FontStyle.Bold);
            resultadosPanel.BackColor = Color.WhiteSmoke;
            resultadosLlabel.Font = new Font(resultadosLlabel.Font, resultadosLlabel.Font.Style & ~FontStyle.Bold);
            definicaoMetasGestorPanel.BackColor = Color.WhiteSmoke;
            definicaoMetasGestorLabel.Font = new Font(definicaoMetasGestorLabel.Font, definicaoMetasGestorLabel.Font.Style & ~FontStyle.Bold);
            metasPanel.BackColor = Color.WhiteSmoke;
            metasLabel.Font = new Font(metasLabel.Font, metasLabel.Font.Style & ~FontStyle.Bold);
            atendimentoMenuPanel.BackColor = Color.WhiteSmoke;
            atendimentoLabel.Font = new Font(atendimentoLabel.Font, atendimentoLabel.Font.Style & ~FontStyle.Bold);
            retornarLigacoesMenuPanel.BackColor = Color.WhiteSmoke;
            retornarLigacoesLabel.Font = new Font(retornarLigacoesLabel.Font, retornarLigacoesLabel.Font.Style & ~FontStyle.Bold);
            responderMenuPanel.BackColor = Color.WhiteSmoke;
            responderLabel.Font = new Font(responderLabel.Font, responderLabel.Font.Style & ~FontStyle.Bold);
            finalizarMenuPanel.BackColor = Color.WhiteSmoke;
            finalizarLabel.Font = new Font(finalizarLabel.Font, finalizarLabel.Font.Style & ~FontStyle.Bold);
            relatoriosMenuPanel.BackColor = Color.WhiteSmoke;
            relatoriosLabel.Font = new Font(relatoriosLabel.Font, relatoriosLabel.Font.Style & ~FontStyle.Bold);
            satisfacaoPanel.BackColor = Color.WhiteSmoke;
            satisfacaoLabel.Font = new Font(satisfacaoLabel.Font, satisfacaoLabel.Font.Style & ~FontStyle.Bold);
            descontoBeneficioPanel.BackColor = Color.WhiteSmoke;
            descontoBeneficioLabel.Font = new Font(descontoBeneficioLabel.Font, descontoBeneficioLabel.Font.Style & ~FontStyle.Bold);
            radarBIPanel.BackColor = Color.WhiteSmoke;
            radarBILabel.Font = new Font(radarBILabel.Font, radarBILabel.Font.Style & ~FontStyle.Bold);
            ajustesNFePanel.BackColor = Color.WhiteSmoke;
            ajustesNFeLabel4.Font = new Font(ajustesNFeLabel4.Font, ajustesNFeLabel4.Font.Style & ~FontStyle.Bold);
            graficosChamadosPanel.BackColor = Color.WhiteSmoke;
            graficosChamadosLabel.Font = new Font(graficosChamadosLabel.Font, graficosChamadosLabel.Font.Style & ~FontStyle.Bold);
            integrarNotasPanel.BackColor = Color.WhiteSmoke;
            integrarNotasLabel.Font = new Font(integrarNotasLabel.Font, integrarNotasLabel.Font.Style & ~FontStyle.Bold);
            controladoriaPanel.BackColor = Color.WhiteSmoke;
            controladoriaLabel.Font = new Font(controladoriaLabel.Font, controladoriaLabel.Font.Style & ~FontStyle.Bold);
            RecursosHumanosPanel.BackColor = Color.WhiteSmoke;
            RecursosHumanosLabel.Font = new Font(RecursosHumanosLabel.Font, RecursosHumanosLabel.Font.Style & ~FontStyle.Bold);
            colaboradoresPanel.BackColor = Color.WhiteSmoke;
            colaboradoresLabel.Font = new Font(colaboradoresLabel.Font, colaboradoresLabel.Font.Style & ~FontStyle.Bold);
            GestorPanel.BackColor = Color.WhiteSmoke;
            gestorLabel.Font = new Font(gestorLabel.Font, gestorLabel.Font.Style & ~FontStyle.Bold);
            pessoasPanel.BackColor = Color.WhiteSmoke;
            pessoasLabel.Font = new Font(pessoasLabel.Font, pessoasLabel.Font.Style & ~FontStyle.Bold);
            prazosPanel.BackColor = Color.WhiteSmoke;
            prazosLabel.Font = new Font(prazosLabel.Font, prazosLabel.Font.Style & ~FontStyle.Bold);
            cargosPanel.BackColor = Color.WhiteSmoke;
            cargosLabel.Font = new Font(cargosLabel.Font, cargosLabel.Font.Style & ~FontStyle.Bold);
            escolaridadePanel.BackColor = Color.WhiteSmoke;
            escolaridadeLabel.Font = new Font(escolaridadeLabel.Font, escolaridadeLabel.Font.Style & ~FontStyle.Bold);
            competenciaPanel.BackColor = Color.WhiteSmoke;
            competenciaLabel.Font = new Font(competenciaLabel.Font, competenciaLabel.Font.Style & ~FontStyle.Bold);
            departamentoMenuPanel.BackColor = Color.WhiteSmoke;
            departamentoLabel.Font = new Font(departamentoLabel.Font, departamentoLabel.Font.Style & ~FontStyle.Bold);
            pontuacaoMenuPanel.BackColor = Color.WhiteSmoke;
            pontuacaoMenuLabel.Font = new Font(pontuacaoMenuLabel.Font, pontuacaoMenuLabel.Font.Style & ~FontStyle.Bold);
            cadastroAvaliacaoPanel.BackColor = Color.WhiteSmoke;
            cadastroAvaliacaoLabel.Font = new Font(cadastroAvaliacaoLabel.Font, cadastroAvaliacaoLabel.Font.Style & ~FontStyle.Bold);
            metasDeResultadoRHPanel.BackColor = Color.WhiteSmoke;
            metasDeResultadoRHLabel.Font = new Font(metasDeResultadoRHLabel.Font, metasDeResultadoRHLabel.Font.Style & ~FontStyle.Bold);
            metasDeCrescimentoRHPanel.BackColor = Color.WhiteSmoke;
            metasDeCrescimentoRHLabel.Font = new Font(metasDeCrescimentoRHLabel.Font, metasDeCrescimentoRHLabel.Font.Style & ~FontStyle.Bold);
            feedbackRHPanel.BackColor = Color.WhiteSmoke;
            feedbackRHLabel.Font = new Font(feedbackRHLabel.Font, feedbackRHLabel.Font.Style & ~FontStyle.Bold);
            radarRHPanel.BackColor = Color.WhiteSmoke;
            radarRHLabel.Font = new Font(radarRHLabel.Font, radarRHLabel.Font.Style & ~FontStyle.Bold);
            notasRHPanel.BackColor = Color.WhiteSmoke;
            notasRHLabel.Font = new Font(notasRHLabel.Font, notasRHLabel.Font.Style & ~FontStyle.Bold);
            metasDeResultadoColaboradorPanel.BackColor = Color.WhiteSmoke;
            metasDeResultadoColaboradorLabel.Font = new Font(metasDeResultadoColaboradorLabel.Font, metasDeResultadoColaboradorLabel.Font.Style & ~FontStyle.Bold);
            autoAvaliacaoPanel.BackColor = Color.WhiteSmoke;
            autoAvaliacaoLabel.Font = new Font(autoAvaliacaoLabel.Font, autoAvaliacaoLabel.Font.Style & ~FontStyle.Bold);
            feedbakcColaboradorPanel.BackColor = Color.WhiteSmoke;
            feedbackColaboradorLabel.Font = new Font(feedbackColaboradorLabel.Font, feedbackColaboradorLabel.Font.Style & ~FontStyle.Bold);
            radarColaboradorPanel.BackColor = Color.WhiteSmoke;
            radarColaboradorLabel.Font = new Font(radarColaboradorLabel.Font, radarColaboradorLabel.Font.Style & ~FontStyle.Bold);
            metasDeResultadoGestorPanel.BackColor = Color.WhiteSmoke;
            metasDeResultadoGestorLabel.Font = new Font(metasDeResultadoGestorLabel.Font, metasDeResultadoGestorLabel.Font.Style & ~FontStyle.Bold);
            metasDeCrescimentoGestorPanel.BackColor = Color.WhiteSmoke;
            metasDeCrescimentoGestorLabel.Font = new Font(metasDeCrescimentoGestorLabel.Font, metasDeCrescimentoGestorLabel.Font.Style & ~FontStyle.Bold);
            feedbakcGestorPanel.BackColor = Color.WhiteSmoke;
            feedbakcGestorLabel.Font = new Font(feedbakcGestorLabel.Font, feedbakcGestorLabel.Font.Style & ~FontStyle.Bold);
            radarGestorPanel.BackColor = Color.WhiteSmoke;
            radarGestorLabel.Font = new Font(radarGestorLabel.Font, radarGestorLabel.Font.Style & ~FontStyle.Bold);
            categoriaPanel.BackColor = Color.WhiteSmoke;
            categoriaLabel.Font = new Font(categoriaLabel.Font, categoriaLabel.Font.Style & ~FontStyle.Bold);
            modulosPanel.BackColor = Color.WhiteSmoke;
            modulosLabel.Font = new Font(modulosLabel.Font, modulosLabel.Font.Style & ~FontStyle.Bold);
            telasPanel.BackColor = Color.WhiteSmoke;
            telasLabel.Font = new Font(telasLabel.Font, telasLabel.Font.Style & ~FontStyle.Bold);
            trocaSenhaPanel.BackColor = Color.WhiteSmoke;
            trocaSenhaLabel.Font = new Font(trocaSenhaLabel.Font, trocaSenhaLabel.Font.Style & ~FontStyle.Bold);
            editarPerfilPanel.BackColor = Color.WhiteSmoke;
            editarPerfilLabel.Font = new Font(editarPerfilLabel.Font, editarPerfilLabel.Font.Style & ~FontStyle.Bold);
            sairPanel.BackColor = Color.WhiteSmoke;
            sairLabel.Font = new Font(sairLabel.Font, sairLabel.Font.Style & ~FontStyle.Bold);
            trocarUsuarioPanel.BackColor = Color.WhiteSmoke;
            trocarUsuarioLabel.Font = new Font(trocarUsuarioLabel.Font, trocarUsuarioLabel.Font.Style & ~FontStyle.Bold);
            varaPanel.BackColor = Color.WhiteSmoke;
            varaLabel.Font = new Font(varaLabel.Font, varaLabel.Font.Style & ~FontStyle.Bold);
            tipoPagamentoPanel.BackColor = Color.WhiteSmoke;
            tipoPagamentoLabel.Font = new Font(tipoPagamentoLabel.Font, tipoPagamentoLabel.Font.Style & ~FontStyle.Bold);

            cargosNineBoxLabel.Font = new Font(cargosNineBoxLabel.Font, cargosNineBoxLabel.Font.Style & ~FontStyle.Bold);
            colaboradoresNineBoxlabel.Font = new Font(colaboradoresNineBoxlabel.Font, colaboradoresNineBoxlabel.Font.Style & ~FontStyle.Bold);
            cargosNineBoxPanel.BackColor = Color.WhiteSmoke;
            colaboradoresNineBoxPanel.BackColor = Color.WhiteSmoke;

            pontuacaoNineBoxLabel.Font = new Font(gestorLabel.Font, pontuacaoNineBoxLabel.Font.Style & ~FontStyle.Bold);
            pontuacaoNineBoxPanel.BackColor = Color.WhiteSmoke;
            #endregion

            #region Biblioteca

            cadastroLivrosLabel.Font = new Font(cadastroLivrosLabel.Font, cadastroLivrosLabel.Font.Style & ~FontStyle.Bold);
            cadastroLivrosPanel.BackColor = Color.WhiteSmoke;

            EmprestimoLivrosLabel.Font = new Font(EmprestimoLivrosLabel.Font, EmprestimoLivrosLabel.Font.Style & ~FontStyle.Bold);
            EmprestimoLivrosPanel.BackColor = Color.WhiteSmoke;

            ReservaLivrosLabel.Font = new Font(ReservaLivrosLabel.Font, ReservaLivrosLabel.Font.Style & ~FontStyle.Bold);
            ReservaLivrosPanel.BackColor = Color.WhiteSmoke;

            DevolucaoLivrosLabel.Font = new Font(DevolucaoLivrosLabel.Font, DevolucaoLivrosLabel.Font.Style & ~FontStyle.Bold);
            DevolucaoLivrosPanel.BackColor = Color.WhiteSmoke;

            PerguntasLivrosLabel.Font = new Font(PerguntasLivrosLabel.Font, PerguntasLivrosLabel.Font.Style & ~FontStyle.Bold);
            PerguntasLivrosPanel.BackColor = Color.WhiteSmoke;
            
            bibliotecaLabel.Font = new Font(bibliotecaLabel.Font, bibliotecaLabel.Font.Style & ~FontStyle.Bold);
            bibliotecaPanel.BackColor = Color.WhiteSmoke;

            categoriaLivrosLabel.Font = new Font(categoriaLivrosLabel.Font, categoriaLivrosLabel.Font.Style & ~FontStyle.Bold);
            categoriaLivrosPanel.BackColor = Color.WhiteSmoke;

            LivrosLabel.Font = new Font(LivrosLabel.Font, LivrosLabel.Font.Style & ~FontStyle.Bold);
            LivrosPanel.BackColor = Color.WhiteSmoke;

            PontuacaoLivrosLabel.Font = new Font(PontuacaoLivrosLabel.Font, PontuacaoLivrosLabel.Font.Style & ~FontStyle.Bold);
            PontuacaoLivrosPanel.BackColor = Color.WhiteSmoke;
            
            #endregion

            #region bolão
            SelecoesPanel.BackColor = Color.WhiteSmoke;
            SelecoesLabel.Font = new Font(SelecoesLabel.Font, SelecoesLabel.Font.Style & ~FontStyle.Bold);
            JogosPanel.BackColor = Color.WhiteSmoke;
            JogosLabel.Font = new Font(JogosLabel.Font, JogosLabel.Font.Style & ~FontStyle.Bold);
            ResultadoJogosPanel.BackColor = Color.WhiteSmoke;
            ResultadoJogosLabel.Font = new Font(ResultadoJogosLabel.Font, ResultadoJogosLabel.Font.Style & ~FontStyle.Bold);
            PalpiteFinalPanel.BackColor = Color.WhiteSmoke;
            PalpiteFinalLabel.Font = new Font(PalpiteFinalLabel.Font, PalpiteFinalLabel.Font.Style & ~FontStyle.Bold);
            PalpitePlacarPanel.BackColor = Color.WhiteSmoke;
            PalpitePlacarLabel.Font = new Font(PalpiteFinalLabel.Font, PalpitePlacarLabel.Font.Style & ~FontStyle.Bold);
            RankingBolaoLabel.Font = new Font(RankingBolaoLabel.Font, RankingBolaoLabel.Font.Style & ~FontStyle.Bold);
            RankingBolaoPanel.BackColor = Color.WhiteSmoke;
            ValorArrecadadoLabel.Font = new Font(ValorArrecadadoLabel.Font, ValorArrecadadoLabel.Font.Style & ~FontStyle.Bold);
            ValorArrecadadoPanel.BackColor = Color.WhiteSmoke;
            #endregion

            #endregion
        }

        private void usuariosMenuPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            BolaoCopaPanel.Visible = false;

            #region subMenu Cadastros

            if (((Control)sender).Name.Contains("usuario"))
            {
                menuNineBoxPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                usuarioLabel.Font = new Font(usuarioLabel.Font, FontStyle.Bold);
                usuariosMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("parametro"))
            {
                menuNineBoxPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                parametrosLabel.Font = new Font(parametrosLabel.Font, FontStyle.Bold);
                parametrosMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("empresa"))
            {
                menuNineBoxPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                empresasLabel.Font = new Font(empresasLabel.Font,FontStyle.Bold);
                empresasMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("salaReuniao"))
            {
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                salaReuniaoLabel.Font = new Font(salaReuniaoLabel.Font, FontStyle.Bold);
                salaReuniaoMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("tipoAtendimento"))
            {
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                tipoAtendimentoLabel.Font = new Font(tipoAtendimentoLabel.Font, FontStyle.Bold);
                tipoAtendimentoMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("EMTU"))
            {
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                tipoEMTULabel.Font = new Font(tipoEMTULabel.Font, FontStyle.Bold);
                tipoEmtuMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("notificacao"))
            {
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                notificacaoPanel.BackColor = Color.Silver;
                notificacaoLabel.Font = new Font(notificacaoLabel.Font, FontStyle.Bold);
                return;
            }

            if (((Control)sender).Name.Contains("periodoBancoHoras"))
            {
                menuSACPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                periodoBancoHorasLabel.Font = new Font(periodoBancoHorasLabel.Font, FontStyle.Bold);
                periodoBancoHorasPanel.BackColor = Color.Silver;
                return;
            }

            #region subMenu Sistemas

            if (((Control)sender).Name.Contains("categoria"))
            {
                menuSACPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                categoriaLabel.Font = new Font(categoriaLabel.Font, FontStyle.Bold);
                categoriaPanel.BackColor = Color.Silver;
                modulosLabel.Font = new Font(modulosLabel.Font, modulosLabel.Font.Style & ~FontStyle.Bold);
                telasLabel.Font = new Font(telasLabel.Font, telasLabel.Font.Style & ~FontStyle.Bold);
                return;
            }

            if (((Control)sender).Name.Contains("modulos"))
            {
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                categoriaLabel.Font = new Font(categoriaLabel.Font, categoriaLabel.Font.Style & ~FontStyle.Bold);
                modulosLabel.Font = new Font(modulosLabel.Font, FontStyle.Bold);
                modulosPanel.BackColor = Color.Silver;
                telasLabel.Font = new Font(telasLabel.Font, telasLabel.Font.Style & ~FontStyle.Bold);
                return;
            }

            if (((Control)sender).Name.Contains("telas"))
            {
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                categoriaLabel.Font = new Font(categoriaLabel.Font, categoriaLabel.Font.Style & ~FontStyle.Bold);
                modulosLabel.Font = new Font(modulosLabel.Font, modulosLabel.Font.Style & ~FontStyle.Bold);
                telasLabel.Font = new Font(telasLabel.Font, FontStyle.Bold);
                telasPanel.BackColor = Color.Silver;
                return;
            }
            #endregion

            #region subMenu Corridas

            if (((Control)sender).Name.Contains("cadastroCorridas"))
            {
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                cadastroCorridasLabel.Font = new Font(cadastroCorridasLabel.Font, FontStyle.Bold);
                cadastroCorridasPanel.BackColor = Color.Silver;
                participantesLlabel.Font = new Font(participantesLlabel.Font, participantesLlabel.Font.Style & ~FontStyle.Bold);
                resultadosLlabel.Font = new Font(resultadosLlabel.Font, resultadosLlabel.Font.Style & ~FontStyle.Bold);
                return;
            }

            if (((Control)sender).Name.Contains("participantes"))
            {
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                menuNineBoxPanel.Visible = false;
                cadastroCorridasLabel.Font = new Font(cadastroCorridasLabel.Font, cadastroCorridasLabel.Font.Style & ~FontStyle.Bold);
                participantesPanel.BackColor = Color.Silver;
                participantesLlabel.Font = new Font(participantesLlabel.Font, participantesLlabel.Font.Style);
                resultadosLlabel.Font = new Font(resultadosLlabel.Font, resultadosLlabel.Font.Style & ~FontStyle.Bold);

                return;
            }

            if (((Control)sender).Name.Contains("resultados"))
            {
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                cadastroCorridasLabel.Font = new Font(cadastroCorridasLabel.Font, cadastroCorridasLabel.Font.Style & ~FontStyle.Bold);
                participantesLlabel.Font = new Font(participantesLlabel.Font, participantesLlabel.Font.Style & ~FontStyle.Bold);
                resultadosLlabel.Font = new Font(resultadosLlabel.Font, FontStyle.Bold);
                resultadosPanel.BackColor = Color.Silver;
                return;
            }
            #endregion
            #endregion

            #region subMenu Diversos
            if (((Control)sender).Name.Contains("desconto"))
            {
                menuSistemasPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                descontoBeneficioLabel.Font = new Font(descontoBeneficioLabel.Font, FontStyle.Bold);
                descontoBeneficioPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("radarBI"))
            {
                menuSistemasPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                radarBILabel.Font = new Font(radarBILabel.Font, FontStyle.Bold);
                radarBIPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("ajustesNFe"))
            {
                menuSistemasPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                ajustesNFeLabel4.Font = new Font(ajustesNFeLabel4.Font, FontStyle.Bold);
                ajustesNFePanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("ajustePedido"))
            {
                menuSistemasPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                ajustePedidoLabel.Font = new Font(ajustePedidoLabel.Font, FontStyle.Bold);
                ajustePedidoPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("integrarNotas"))
            {
                menuSistemasPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                integrarNotasLabel.Font = new Font(integrarNotasLabel.Font, FontStyle.Bold);
                integrarNotasPanel.BackColor = Color.Silver;
                return;
            }
            #endregion

            #region subMenu usuario
            if (((Control)sender).Name.Contains("trocaSenha"))
            {
                menuNineBoxPanel.Visible = false;
                menuCadastroPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                trocaSenhaLabel.Font = new Font(trocaSenhaLabel.Font, FontStyle.Bold);
                trocaSenhaPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("editarPerfil"))
            {
                menuCadastroPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                editarPerfilLabel.Font = new Font(editarPerfilLabel.Font, FontStyle.Bold);
                editarPerfilPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("sair"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                sairLabel.Font = new Font(sairLabel.Font, FontStyle.Bold);
                sairPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("trocarUsuario"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                trocarUsuarioLabel.Font = new Font(trocarUsuarioLabel.Font, FontStyle.Bold);
                trocarUsuarioPanel.BackColor = Color.Silver;
                return;
            }
            #endregion

            #region subMenu SAC
            if (((Control)sender).Name.Contains("atendimento"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                atendimentoLabel.Font = new Font(atendimentoLabel.Font, FontStyle.Bold);
                atendimentoMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("retornar"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                retornarLigacoesLabel.Font = new Font(retornarLigacoesLabel.Font, FontStyle.Bold);
                retornarLigacoesMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("responder"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                responderLabel.Font = new Font(responderLabel.Font, FontStyle.Bold);
                responderMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("finalizar"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                finalizarLabel.Font = new Font(finalizarLabel.Font, FontStyle.Bold);
                finalizarMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("satisfacao"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                satisfacaoLabel.Font = new Font(satisfacaoLabel.Font, FontStyle.Bold);
                satisfacaoPanel.BackColor = Color.Silver;
                return;
            }
            #endregion

            #region subMenu Avaliação desempenho - Recursos Humanos
            if (((Control)sender).Name.Contains("metasDeResultadoRH"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                metasDeResultadoRHLabel.Font = new Font(metasDeResultadoRHLabel.Font, FontStyle.Bold);
                metasDeResultadoRHPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("metasDeCrescimentoRH"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                metasDeCrescimentoRHLabel.Font = new Font(metasDeCrescimentoRHLabel.Font, FontStyle.Bold);
                metasDeCrescimentoRHPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("feedbackRH"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                feedbackRHLabel.Font = new Font(feedbackRHLabel.Font, FontStyle.Bold);
                feedbackRHPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("radarRH"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                menuNineBoxPanel.Visible = false;
                radarRHLabel.Font = new Font(radarRHLabel.Font, FontStyle.Bold);
                radarRHPanel.BackColor = Color.Silver;
                return;
            }
            
            if (((Control)sender).Name.Contains("notasRH"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                notasRHLabel.Font = new Font(notasRHLabel.Font, FontStyle.Bold);
                notasRHPanel.BackColor = Color.Silver;
                return;
            }

            #endregion

            #region subMenu Avaliação desempenho - Colaborador
            if (((Control)sender).Name.Contains("metasDeResultadoColaborador"))
            {
                menuCadastroPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                metasDeResultadoColaboradorLabel.Font = new Font(metasDeResultadoColaboradorLabel.Font, FontStyle.Bold);
                metasDeResultadoColaboradorPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("feedbackColaborador"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                feedbakcColaboradorPanel.BackColor = Color.Silver;
                feedbackColaboradorLabel.Font = new Font(feedbackColaboradorLabel.Font, FontStyle.Bold);
                return;
            }

            if (((Control)sender).Name.Contains("autoAvaliacao"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                autoAvaliacaoLabel.Font = new Font(autoAvaliacaoLabel.Font, FontStyle.Bold);
                autoAvaliacaoPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("radarColaborador"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                radarColaboradorLabel.Font = new Font(radarColaboradorLabel.Font, FontStyle.Bold);
                radarColaboradorPanel.BackColor = Color.Silver;
                return;
            }
            #endregion

            #region subMenu Avaliação desempenho - Gestor
            if (((Control)sender).Name.Contains("metasDeResultadoGestor"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                metasDeResultadoGestorLabel.Font = new Font(metasDeResultadoGestorLabel.Font, FontStyle.Bold);
                metasDeResultadoGestorPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("metasDeCrescimentoGestor"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                metasDeCrescimentoGestorLabel.Font = new Font(metasDeCrescimentoGestorLabel.Font, FontStyle.Bold);
                metasDeCrescimentoGestorPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("feedbakcGestor"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                feedbakcGestorLabel.Font = new Font(feedbakcGestorLabel.Font, FontStyle.Bold);
                feedbakcGestorPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("radarGestor"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;

                menuNineBoxPanel.Visible = false;
                radarGestorLabel.Font = new Font(radarGestorLabel.Font, FontStyle.Bold);
                radarGestorPanel.BackColor = Color.Silver;
                return;
            }

            #endregion

            #region subMenu Avaliação desempenho - Cadastros
            #region NineBox
            if (((Control)sender).Name.Contains("cargosNine"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                cargosNineBoxLabel.Font = new Font(cargosNineBoxLabel.Font, FontStyle.Bold);
                cargosNineBoxPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("colaboradoresNine"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                colaboradoresNineBoxlabel.Font = new Font(colaboradoresNineBoxlabel.Font, FontStyle.Bold);
                colaboradoresNineBoxPanel.BackColor = Color.Silver;
                return;
            }
            #endregion
            if (((Control)sender).Name.Contains("pessoas"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                pessoasLabel.Font = new Font(pessoasLabel.Font, FontStyle.Bold);
                pessoasPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("cargos"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                cargosLabel.Font = new Font(cargosLabel.Font, FontStyle.Bold);
                cargosPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("prazos"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                prazosLabel.Font = new Font(prazosLabel.Font, FontStyle.Bold);
                prazosPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("escolaridade"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                escolaridadeLabel.Font = new Font(escolaridadeLabel.Font, FontStyle.Bold);
                escolaridadePanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("competencia"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                competenciaLabel.Font = new Font(competenciaLabel.Font, FontStyle.Bold);
                competenciaPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("departamento"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                departamentoLabel.Font = new Font(departamentoLabel.Font, FontStyle.Bold);
                departamentoMenuPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("pontuacao"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                pontuacaoMenuLabel.Font = new Font(pontuacaoMenuLabel.Font, FontStyle.Bold);
                pontuacaoMenuPanel.BackColor = Color.Silver;
                return;
            }

            
            #endregion

            #region subMenu Avaliação desempennho - Controladoria

            if (((Control)sender).Name.Contains("metas"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;

                metasLabel.Font = new Font(metasLabel.Font, FontStyle.Bold);
                metasPanel.BackColor = Color.Silver;
                return;
            }
            
            if (((Control)sender).Name.Contains("definicaoMetasGestor"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;

                definicaoMetasGestorLabel.Font = new Font(definicaoMetasGestorLabel.Font, FontStyle.Bold);
                definicaoMetasGestorPanel.BackColor = Color.Silver;
                return;
            }
            #endregion

            #region subMenu Juridico
            if (((Control)sender).Name.Contains("tipoPagamento"))
            {
                menuSACPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                tipoPagamentoLabel.Font = new Font(tipoPagamentoLabel.Font, FontStyle.Bold);
                tipoPagamentoPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("vara"))
            {
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                varaLabel.Font = new Font(varaLabel.Font, FontStyle.Bold);
                varaPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("novoComunicado"))
            {
                menuCadastroPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuNineBoxPanel.Visible = false;
                menuLimpezaPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                cadastrosComunicadoLabel.Font = new Font(cadastrosComunicadoLabel.Font, cadastrosComunicadoLabel.Font.Style & ~FontStyle.Bold);
                novoComunicadoLabel.Font = new Font(novoComunicadoLabel.Font, FontStyle.Bold);
                novoComunicadoPanel.BackColor = Color.Silver;
                return;
            }
            #endregion


        }

        private void modulosPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaChamado;
            new Modulos().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void chatMenuPanel_Click(object sender, EventArgs e)
        {
            //EscondeMenus();

            //sacTimer.Start();
            //chatPanel.BringToFront();
            //usuariosChatTimer.Start();
            //usuariosChatTimer_Tick(sender, e);
            //chatPanel.Location = new Point(chatPanel.Location.X, (tituloPanel.Top + tituloPanel.Height) + menuUsuarioPanel.Height);
            //chatPanel.Size = new Size(chatPanel.Width, (panel2.Location.Y - (tituloPanel.Top + tituloPanel.Height) - menuUsuarioPanel.Height));
            //escondeAbreChatPictureBox.Image = Properties.Resources.SetaDownBranca;
            //chatPanel.Visible = true;

            //new Chat().ShowDialog();
        }

        private void chatTimer_Tick(object sender, EventArgs e)
        {
            _listaChat = new ChatBO().BuscarChatNaoLida(Publicas._idUsuario);

            if (_listaChat.Count() != 0)
            {
                foreach (var item in _listaChat)
                {
                    Notificacoes.NotificacaoChat _notificacao = new Notificacoes.NotificacaoChat();
                    _notificacao.mensagemRichTextBox.Text = item.Mensagem;
                    _notificacao.tituloQtdLabel.Text = _listaChat.Count().ToString();
                    _notificacao.enviadaLabel.Text = "Enviada por " + item.NomeRecebida;
                    _notificacao.tituloQtdLabel.Tag = item.IdUsuarioOrigem;
                    _notificacao.Show();
                    chatTimer.Stop();
                    break;
                }               
            }
        }

        private void sacPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuAvulsoPanel.Visible = false;
            menuCadastroPanel.Visible = false;           
            menuSistemasPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuNineBoxPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;

            if (!menuSACPanel.Visible)
            {
                menuSACPanel.Left = _leftSubMenus;
                menuSACPanel.Top = (menuPanel.Width != 206 ? 178 : 203);
                menuSACPanel.BringToFront();
                menuSACPanel.Visible = true;
            }

            tituloMenuSACPanel.Visible = (menuPanel.Width != 206);

            sacLabel.Font = new Font(sacLabel.Font, FontStyle.Bold);
            sacPanel.BackColor = Color.Silver;
        }

        private void departamentoMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Departamentos().ShowDialog();
            AtivaTimer(sender, e);            
        }

        private void tipoAtendimentoMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new TiposDeAtendimento().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void tipoEmtuMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new EMTU().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void dataHoraTimer_Tick(object sender, EventArgs e)
        {

            dataHoraLabel.Text = "| Maquina " + Environment.MachineName + "     | Versão de " + _dataCompilacao + " |       " +
                DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            mensagemLabel.Text = Publicas._mensagemSistema;


            if (senhaPanel.Visible && senhaLabel.Visible && contadorTempoSenha != -1)
            {
                contSenhalabel.Text = (5 - contadorTempoSenha).ToString();

                contadorTempoSenha++;

                if (contadorTempoSenha > 6)
                {
                    senhaLabel.Visible = false;
                    cpfMaskedEditBox.Text = string.Empty;
                    senhaLabel.Text = string.Empty;
                    senhaPanel.Visible = false;
                    usuarioLoginTextBox.Focus();
                }
            }

            _notificacao = new NotificacaoDoSistemaBO().Consultar();

            if (_notificacao.DataDaAcao <= DateTime.Now && _notificacao.DataFimDaAcao >= DateTime.Now)
            {
                if (contadorTempoEncerrarSistema == 0)
                {
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

        private void atendimentoMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            ParaTimer();
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new SAC.Atendimentos().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void usuarioLogadoPanel_MouseHover(object sender, EventArgs e)
        {
            usuarioLogadoPanel.Cursor = Cursors.Hand;
        }

        private void usuarioLogadoPanel_MouseLeave(object sender, EventArgs e)
        {
            usuarioLogadoPanel.Cursor = Cursors.Default;
        }

        private void trocaSenhaPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema;
            new Cadastros.TrocaDeSenha().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void trocarUsuarioPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();
            menuUsuarioPanel.Size = new Size(menuUsuarioPanel.Width, 58);

            MenuPictureBox.Visible = false;
            menuUsuarioPanel.Visible = false;
            _tamanhoSAC = 0;

            new LoginBO().AlterarStatusUsuario(Publicas._idUsuario, Publicas.StatusUsuario.OffLine, Publicas._conexaoString);

            Publicas._idUsuario = 0;
            Publicas._usuario = null;
            Publicas._mensagemSistema = string.Empty;
            this.Cursor = Cursors.Default;

            
            usuarioLogadoLabel.Text = "Olá," + Environment.NewLine + "Efetue seu login";
            usuarioLoginTextBox.Text = string.Empty;
            senhaLoginTextBox.Text = string.Empty;
            fotoUsuarioPictureBox.Image = Properties.Resources.UserLogin;
            fotoUsuarioPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

            trocaSenhaPanel.Visible = false;
            editarPerfilPanel.Visible = false;
            trocarUsuarioPanel.Visible = false;
            chamadosFinalizadosPanel.Visible = false;
            menuSACPanel.Size = new Size(menuSACPanel.Width, _tamanhoOriginalSac);
            dashBoardTabControl.Visible = false;

            fecharNotificacaoComunicadoTimer_Tick(sender, e);
            fecharNotificacaoChamadoTimer_Tick(sender, e);
            aniversariosTimer_Tick(sender, e);
            feriadoPanel.Visible = false;

            Control[] controle;
            string nome;

            for (int i = 1; i < 7; i++)
            {
                nome = "corridaPanel" + (i == 1 ? "" : i.ToString());

                controle = this.Controls.Find(nome, true);

                if (i == 1)
                    controle[0].Visible = false; // o primeiro não é criado em tempo de execução por isso fica invisivel
                else
                {
                    try
                    {
                        controle[0].Dispose();
                    }
                    catch { }
                }
            }

            loginPanel.BringToFront();
            loginPanel.Visible = true;
            usuarioLoginTextBox.Focus();
        }

        private void EscondeMenus()
        {
            aniversariosTimer_Tick(this, new EventArgs());

            BancoHorasPanel.Visible = false;
            fecharNotificacaoChamadoTimer_Tick(this, new EventArgs());
            fecharNotificacaoComunicadoTimer_Tick(this, new EventArgs());

            ParaTimer();
            chamadoTimer.Stop();
            

            Publicas._mensagemSistema = (Publicas._alterouSkin ? "Aplicando tema, aguarde." : string.Empty);
            dataHoraTimer_Tick(this, new EventArgs());

            chamadosFinalizadosPanel.Visible = false;
            menuUsuarioPanel.Visible = false;
            dashBoardTabControl.Visible = false;
            menuPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuCadastroPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuAvulsoPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;
            menuNineBoxPanel.Visible = false;
            Refresh();
        }

        private void relatoriosLabel_MouseHover(object sender, EventArgs e)
        {
            // abrir menu 
            menuCadastroPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuLimpezaPanel.Visible = false;

            atendimentoLabel.Font = new Font(atendimentoLabel.Font, atendimentoLabel.Font.Style & ~FontStyle.Bold);
            retornarLigacoesLabel.Font = new Font(retornarLigacoesLabel.Font, retornarLigacoesLabel.Font.Style & ~FontStyle.Bold);
            responderLabel.Font = new Font(responderLabel.Font, responderLabel.Font.Style & ~FontStyle.Bold);
            finalizarLabel.Font = new Font(finalizarLabel.Font, finalizarLabel.Font.Style & ~FontStyle.Bold);
            relatoriosLabel.Font = new Font(finalizarLabel.Font, FontStyle.Bold);
            satisfacaoLabel.Font = new Font(satisfacaoLabel.Font, satisfacaoLabel.Font.Style & ~FontStyle.Bold);
        }

        private void editarPerfilPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();
            tituloSistemaLabel.Text = Publicas._nomeDoSistema;
            new Cadastros.EditarPerfil().ShowDialog();

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
                        Publicas._usuario.ParticipaBolaoCopa && (Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.UsuarioAcesso == "ESILVA");

            SelecoesPanel.Visible = Publicas._usuario.AdministraBolaoCopa;
            JogosPanel.Visible = Publicas._usuario.AdministraBolaoCopa;
            ResultadoJogosPanel.Visible = Publicas._usuario.AdministraBolaoCopa;

            MenuBolaoCopaMundo.Size = new Size(MenuBolaoCopaMundo.Width, 179);

            if (!Publicas._usuario.AdministraBolaoCopa)
                MenuBolaoCopaMundo.Size = new Size(MenuBolaoCopaMundo.Width, 80);

            AtivaTimer(sender, e);
        }

        private void retornarLigacoesMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new SAC.Retornar().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void responderMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new SAC.Responder().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void finalizarMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            new SAC.Finalizar().ShowDialog();
            AtivaTimer(sender, e);
        }
        
        private void padraoNiffEscuroLabel_Click(object sender, EventArgs e)
        {
            //Publicas._fonte = Publicas.padraoNIFFEscuro_FonteClara;
            //Publicas._fundo = Publicas.padraoNiffEscuro_Fundo;
            //Publicas._bordaSaida = Publicas.padraoNIFFescuro_BordaSaida;
            //Publicas._bordaEntrada = Publicas.padraoNIFFEscuro_BordaEntrada;

            Publicas._fonte = Publicas.padraoNatal_FonteEscura;
            Publicas._fundo = Publicas.padraoNatal_Fundo;
            Publicas._bordaSaida = Publicas.padraoNatal_BordaSaida;
            Publicas._bordaEntrada = Publicas.padraoNatal_BordaEntrada;
            Publicas._botaoFocado = Publicas.padraoNatal_BotaoFocado;
            Publicas._botao = Publicas.padraoNatalo_Botao;
            Publicas._fonteBotao = Publicas.padraoNatal_FonteBotao;
            Publicas._fonteBotaoFocado = Publicas.padraoNatal_FonteBotaoFocado;
            Publicas._tabPageAtiva = Publicas.padraoNatal_BordaEntrada;

            menuSkinPanel.Visible = false;
            Publicas._alterouSkin = true;

            foreach (Control componenteDaTela in this.Controls)
            {
                //Componentes que não mudam de cor para o padrão NIFF
                if (componenteDaTela.Name != "panel1" && componenteDaTela.Name != "panel2" &&
                    componenteDaTela.Name != "MenuPictureBox" && componenteDaTela.Name != "skinPictureBox" &&
                    componenteDaTela.Name != "skinLabel" && componenteDaTela.Name != "fotoUsuarioPictureBox" &&
                    componenteDaTela.Name != "olaLabel" && componenteDaTela.Name != "usuarioLogadoLabel" &&
                    componenteDaTela.Name != "skinSetaPictureBox" && componenteDaTela.Name != "abrirMenuUsuarioPictureBox" &&
                    componenteDaTela.Name != "dataHoraLabel" && componenteDaTela.Name != "padraoNiffClaroPanel" &&
                    componenteDaTela.Name != "padraoNiffEscuroPanel" && componenteDaTela.Name != "menuSkinPanel")
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            this.Refresh();
        }

        private void padraoNiffClaroLabel_Click(object sender, EventArgs e)
        {
            Publicas._fonte = Publicas.padraoNIFFClaro_FonteEscura;
            Publicas._fundo = Publicas.padraoNiffClaro_Fundo;
            Publicas._bordaSaida = Publicas.padraoNIFFClaro_BordaSaida;
            Publicas._bordaEntrada = Publicas.padraoNIFFClaro_BordaEntrada;
            Publicas._tabPageAtiva = Publicas.padraoNIFFClaro_BordaEntrada;

            Publicas._alterouSkin = false;
            menuSkinPanel.Visible = false;
            foreach (Control componenteDaTela in this.Controls)
            {
                //Componentes que não mudam de cor para o padrão NIFF
                if (componenteDaTela.Name != "panel1" && componenteDaTela.Name != "panel2" &&
                    componenteDaTela.Name != "MenuPictureBox" && componenteDaTela.Name != "skinPictureBox" &&
                    componenteDaTela.Name != "skinLabel" && componenteDaTela.Name != "fotoUsuarioPictureBox" &&
                    componenteDaTela.Name != "olaLabel" && componenteDaTela.Name != "usuarioLogadoLabel" &&
                    componenteDaTela.Name != "skinSetaPictureBox" && componenteDaTela.Name != "abrirMenuUsuarioPictureBox" &&
                    componenteDaTela.Name != "dataHoraLabel" && componenteDaTela.Name != "padraoNiffClaroPanel" &&
                    componenteDaTela.Name != "padraoNiffEscuroPanel" && componenteDaTela.Name != "menuSkinPanel")
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }

        }

        private void skinPictureBox_MouseHover(object sender, EventArgs e)
        {
            skinPanel.Cursor = Cursors.Hand;
        }

        private void skinPanel_MouseLeave(object sender, EventArgs e)
        {
            skinPanel.Cursor = Cursors.Default;
        }

        private void skinPanel_Click(object sender, EventArgs e)
        {
            menuSkinPanel.Visible = true;
        }

        private void usuariosChatTimer_Tick(object sender, EventArgs e)
        {
            mensagemLabel.Text = "Atualizando lista de usuários do chat ...";
            mensagemLabel.Cursor = Cursors.WaitCursor;
            this.Refresh();
            _listaUsuariosLogagos = new ChatBO().ConsultarUsuariosLogados().OrderBy(o => o.Empresa)
                                                                           .OrderBy(o => o.Status)
                                                                           .OrderBy(o => o.Nome).ToList();
            usuarioGridGroupingControl.DataSource = _listaUsuariosLogagos;
            //usuarioGridGroupingControl.Table.ExpandAllGroups();
            mensagemLabel.Cursor = Cursors.Default;
            mensagemLabel.Text = "";
            this.Refresh();
        }

        private void escondeAbreChatPictureBox_Click(object sender, EventArgs e)
        {
            chatPanel.BringToFront();
            if (chatPanel.Height != 25)
            {
                chatPanel.Size = new Size(chatPanel.Width, 25);
                escondeAbreChatPictureBox.Image = Properties.Resources.SetaUpBranca;
                chatPanel.Location = new Point(chatPanel.Location.X, panel2.Location.Y - 25);
            }
            else
            {
                chatPanel.Location = new Point(chatPanel.Location.X, (tituloPanel.Top + tituloPanel.Height + menuUsuarioPanel.Height));
                chatPanel.Size = new Size(chatPanel.Width, (panel2.Location.Y - (tituloPanel.Top + tituloPanel.Height) - menuUsuarioPanel.Height));
                escondeAbreChatPictureBox.Image = Properties.Resources.SetaDownBranca;

            }
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Publicas.stringConexao = Publicas._conexaoString;
            
            #region Agenda
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;

            SimpleScheduleDataProvider data = new SimpleScheduleDataProvider();

            data.MasterList = new SimpleScheduleAppointmentList(); 
            data.FileName = "default.schedule";
            //this.agendaScheduleControl.ScheduleType = ScheduleViewType.Month;
            //this.agendaScheduleControl.DataSource = data;
            //this.agendaScheduleControl.Culture = CultureInfo.CreateSpecificCulture("pt-BR");

            //this.agendaScheduleControl.Calendar.DateValue = DateTime.Now;

//            this.scheduleControl1.ScheduleAppointmentClick += new ScheduleAppointmentClickEventHandler(scheduleControl1_ScheduleAppointmentClick);

            //this.agendaScheduleControl.Appearance.VisualStyle = GridVisualStyles.Metro;
            //this.agendaScheduleControl.NavigationPanelFillWithCalendar = true;
            ScheduleGrid.DisplayStrings[4] = "Duplo clique para adicionar um novo evento";
            #endregion

            #region Ajusta menu principal para não sobrepor a barra de ferramentas
            this.Left = 0;
            this.Width = 0;
            this.Top = 0;
            //obter o tamanho real de work do usuario
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            #endregion

            Publicas._idSuperior = 0;

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.usuarioGridGroupingControl);

            usuarioGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            usuarioGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            usuarioGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            usuarioGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            usuarioGridGroupingControl.RecordNavigationBar.Label = "Usuários";
            for (int i = 0; i < usuarioGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                usuarioGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                usuarioGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                usuarioGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                usuarioGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                usuarioGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                usuarioGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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

            this.usuarioGridGroupingControl.SetMetroStyle(metroColor);

            this.usuarioGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.usuarioGridGroupingControl.TableOptions.SelectionBackColor = Color.FromArgb(37, 38, 91);
            this.usuarioGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.usuarioGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            chatPanel.Visible = false;
            escondeAbreChatPictureBox.Image = Properties.Resources.SetaUpBranca;
            chatPanel.Location = new Point(chatPanel.Location.X, panel2.Location.Y - 25);
            chatPanel.Size = new Size(chatPanel.Width, 25);

            statusComboBox.Items.AddRange(new object[] { "Não Finalizados", "Finalizados", "Cancelados", "Todos" });
            statusComboBox.SelectedIndex = 0;

            statusComunidadosComboBoxAdv.Items.AddRange(new object[] { "Novo", "Aprovados", "Recusados", "Finalizados", "Cancelados", "Todos" });
            statusComunidadosComboBoxAdv.SelectedIndex = 0;

        }

        private void usuarioGridGroupingControl_TableControlCellClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
           
        }

        private void usuarioGridGroupingControl_TableControlCellDoubleClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                string value = e.TableControl.Table.CurrentRecord.GetRecord().Info;

                int posIniId = 0;
                int posFimId = 0;

                posIniId = value.IndexOf("Id =") + 4;
                posFimId = value.IndexOf(", UsuarioAcesso");

                int idUsuarioDestino = Convert.ToInt32(value.Substring(posIniId, posFimId - posIniId).Trim());

                posIniId = value.IndexOf("Nome =") + 7;
                posFimId = value.IndexOf(", Ativo");

                string nome = value.Substring(posIniId, posFimId - posIniId).Trim();

                Chat _chatTela = new Chat();
                _chatTela.Text = nome;

                _chatTela.idUsuarioDestino = idUsuarioDestino;
                _chatTela.BringToFront();
                _chatTela.Show();
            }
            catch
            { }
        }

        private void MudaStatusChat(Publicas.StatusUsuario status)
        {
            new LoginBO().AlterarStatusUsuario(Publicas._idUsuario, status, Publicas._conexaoString);
            menuStatusChatPpanel.Visible = false;
        }

        private void ocupadoPanel_Click(object sender, EventArgs e)
        {
            MudaStatusChat(Publicas.StatusUsuario.Ocupado);
            ocupadoLabel.Font = new Font(ocupadoLabel.Font, FontStyle.Bold);
            ausenteLabel.Font = new Font(ausenteLabel.Font, ausenteLabel.Font.Style & ~FontStyle.Bold);
            offLineLabel.Font = new Font(offLineLabel.Font, offLineLabel.Font.Style & ~FontStyle.Bold);
            onLineLabel.Font = new Font(onLineLabel.Font, onLineLabel.Font.Style & ~FontStyle.Bold);
        }

        private void onLinePanel_Click(object sender, EventArgs e)
        {
            MudaStatusChat(Publicas.StatusUsuario.OnLine);
            onLineLabel.Font = new Font(onLineLabel.Font, FontStyle.Bold);
            ausenteLabel.Font = new Font(ausenteLabel.Font, ausenteLabel.Font.Style & ~FontStyle.Bold);
            offLineLabel.Font = new Font(offLineLabel.Font, offLineLabel.Font.Style & ~FontStyle.Bold);
            ocupadoLabel.Font = new Font(ocupadoLabel.Font, ocupadoLabel.Font.Style & ~FontStyle.Bold);
        }

        private void ausentePanel_Click(object sender, EventArgs e)
        {
            MudaStatusChat(Publicas.StatusUsuario.Ausente);
            ausenteLabel.Font = new Font(ausenteLabel.Font, FontStyle.Bold);
            onLineLabel.Font = new Font(onLineLabel.Font, onLineLabel.Font.Style & ~FontStyle.Bold);
            offLineLabel.Font = new Font(offLineLabel.Font, offLineLabel.Font.Style & ~FontStyle.Bold);
            ocupadoLabel.Font = new Font(ocupadoLabel.Font, ocupadoLabel.Font.Style & ~FontStyle.Bold);
        }

        private void offLineLabel_Click(object sender, EventArgs e)
        {
            MudaStatusChat(Publicas.StatusUsuario.OffLine);
            offLineLabel.Font = new Font(offLineLabel.Font, FontStyle.Bold);
            onLineLabel.Font = new Font(onLineLabel.Font, onLineLabel.Font.Style & ~FontStyle.Bold);
            ausenteLabel.Font = new Font(ausenteLabel.Font, ausenteLabel.Font.Style & ~FontStyle.Bold);
            ocupadoLabel.Font = new Font(ocupadoLabel.Font, ocupadoLabel.Font.Style & ~FontStyle.Bold);
        }

        private void menuChatPictureBox_MouseHover(object sender, EventArgs e)
        {
            menuChatPictureBox.Cursor = Cursors.Hand;
        }

        private void menuChatPictureBox_MouseLeave(object sender, EventArgs e)
        {
            menuChatPictureBox.Cursor = Cursors.Default;
        }

        private void menuChatPictureBox_Click(object sender, EventArgs e)
        {
            menuStatusChatPpanel.Visible = !menuStatusChatPpanel.Visible;
        }

        private void atualizarChatPanel_Click(object sender, EventArgs e)
        {
            menuStatusChatPpanel.Visible = false;
            usuariosChatTimer_Tick(sender, e);
        }

        private void sacGridGroupingControl_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            { // buscar da empresa do usuario
                if (e.TableCellIdentity.Column.MappingName == "SLA")
                {
                    if (Convert.ToDateTime(e.Style.Text) >= DateTime.Now.AddMinutes(720))
                    {
                        e.Style.BackColor = Color.LightGreen;
                        e.Style.CellTipText = "Dentro do prazo";
                    }
                    else
                    if (Convert.ToDateTime(e.Style.Text) >= DateTime.Now)
                    {
                        e.Style.BackColor = Color.Khaki;
                        e.Style.CellTipText = "Prazo finalizando";
                    }
                    else
                    {
                        e.Style.BackColor = Color.Tomato;
                        e.Style.TextColor = Color.White;
                        e.Style.CellTipText = "Passou do prazo.";
                    }
                }
            }
            catch { }
        }

        private void sacTimer_Tick(object sender, EventArgs e)
        {
            //if (!dashBoardTabControl.Visible)
            //dashBoardTabControl.Visible = true;

            if (!Publicas._usuario.AcessaSac)
            {
                sacTimer.Stop();
                return;
            }

            textBoxExt1.Focus();
            mensagemLabel.Text = "Atualizando lista dos atendimentos ...";
            mensagemLabel.Cursor = Cursors.WaitCursor;
            this.Refresh();
            try
            {
                _listaAtendimentos = new AtendimentoBO().Listar(Publicas._usuario.IdEmpresa,
                                                                Publicas.TelaPesquisaSAC.Grid, 0);


                sacGridGroupingControl.DataSource = _listaAtendimentos.OrderBy(o => o.Codigo).OrderBy(o => o.DataAbertura).ToList();
            }
            catch
            {
                sacGridGroupingControl.DataSource = new List<Atendimento>();
            }

            try
            {
                Record[] registro = new Record[1];
                registro[1] = this.sacGridGroupingControl.Table.Records[1];

                sacGridGroupingControl.Table.SelectedRecords.AddRange(registro);
            }
            catch { }

            dashBoardTabControl.SelectedTab = AtendimentoSACTabPage;
            AtendimentoSACTabPage.TabVisible = _listaAtendimentos.Count() != 0;

            //aki
            dashBoardTabControl.Visible = ChamadosTabPage.TabVisible || AtendimentoSACTabPage.TabVisible || juridicoTabPageAdv.TabVisible || andamentoAvaliacaoTabPage.TabVisible;
            sacGridGroupingControl.Focus();
            mensagemLabel.Cursor = Cursors.Default;
            mensagemLabel.Text = "";

            if (!chamadoTimer.Enabled)
                chamadoTimer.Start();
        }

        private void sacGridGroupingControl_TableControlCellMouseDown(object sender, GridTableControlCellMouseEventArgs e)
        {
            

        }

        private void sacGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            
            string value = "";

            try
            {
                value = e.TableControl.Table.CurrentRecord.GetRecord().Info;

                int posIniId = 0;
                int posFimId = 0;

                try
                {
                    posIniId = value.IndexOf("Codigo =") + 9;
                    posFimId = value.IndexOf(", IdUsuario");
                    Publicas._codigoRetornoPesquisa = value.Substring(posIniId, posFimId - posIniId).Trim();
                }
                catch { }

                foreach (Atendimento item in _listaAtendimentos.Where(w => w.Codigo == Publicas._codigoRetornoPesquisa))
                {
                    atendimentoToolStripMenuItem.Enabled = item.Situacao == Publicas.SituacaoAtendimento.ManterComAtendente &&
                        (item.Status == Publicas.StatusAtendimento.Ativo || item.Status == Publicas.StatusAtendimento.Respondido);

                    retornarToolStripMenuItem.Enabled = item.Status == Publicas.StatusAtendimento.Finalizado &&
                                    (item.Situacao == Publicas.SituacaoAtendimento.AguardandoRetornoAoCliente ||
                                     item.Situacao == Publicas.SituacaoAtendimento.Finalizado ||
                                     item.Situacao == Publicas.SituacaoAtendimento.EnviadoAoFinalizador) &&
                                    (item.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Telefone ||
                                    item.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Fax);

                    responderToolStripMenuItem.Enabled = item.Situacao == Publicas.SituacaoAtendimento.EnviadoAoColaborador;
                    finalizarToolStripMenuItem.Enabled = item.Situacao == Publicas.SituacaoAtendimento.EnviadoAoFinalizador;

                    satisfaçãoDoClienteToolStripMenuItem.Enabled = (item.Status == Publicas.StatusAtendimento.Finalizado &&
                                                                    item.AguardaSatisfacaoCliente &&
                                                                    item.Retornou);
                }
            }
            catch { }
        }

        private void sacGridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            
            string value = "";

            try
            {
                value = e.TableControl.Table.CurrentRecord.GetRecord().Info;

                int posIniId = 0;
                int posFimId = 0;

                try
                {
                    posIniId = value.IndexOf("Codigo =") + 9;
                    posFimId = value.IndexOf(", IdUsuario");
                    Publicas._codigoRetornoPesquisa = value.Substring(posIniId, posFimId - posIniId).Trim();
                }
                catch { }

                foreach (Atendimento item in _listaAtendimentos.Where(w => w.Codigo == Publicas._codigoRetornoPesquisa))
                {
                    atendimentoToolStripMenuItem.Enabled = item.Situacao == Publicas.SituacaoAtendimento.ManterComAtendente && 
                        (item.Status == Publicas.StatusAtendimento.Ativo || item.Status == Publicas.StatusAtendimento.Respondido);

                    retornarToolStripMenuItem.Enabled = item.Status == Publicas.StatusAtendimento.Finalizado &&
                                    (item.Situacao == Publicas.SituacaoAtendimento.AguardandoRetornoAoCliente ||
                                     item.Situacao == Publicas.SituacaoAtendimento.Finalizado ||
                                     item.Situacao == Publicas.SituacaoAtendimento.EnviadoAoFinalizador) &&
                                    (item.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Telefone ||
                                    item.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Fax);

                    responderToolStripMenuItem.Enabled = item.Situacao == Publicas.SituacaoAtendimento.EnviadoAoColaborador;
                    finalizarToolStripMenuItem.Enabled = item.Situacao == Publicas.SituacaoAtendimento.EnviadoAoFinalizador;

                    satisfaçãoDoClienteToolStripMenuItem.Enabled = (item.Status == Publicas.StatusAtendimento.Finalizado &&
                                                                    item.AguardaSatisfacaoCliente &&
                                                                    item.Retornou);
                }
            }
            catch { }

        }

        private void responderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EscondeMenus();
                        
            SAC.Responder _tela = new SAC.Responder();
            _tela.codigoTextBox.Text = Publicas._codigoRetornoPesquisa;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void atendimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            SAC.Atendimentos _tela = new SAC.Atendimentos();
            _tela.codigoTextBox.Text = Publicas._codigoRetornoPesquisa;
            
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void finalizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            SAC.Finalizar _tela = new SAC.Finalizar();
            _tela.codigoTextBox.Text = Publicas._codigoRetornoPesquisa;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void descontoBeneficioPanel_Click(object sender, EventArgs e)
        {
            //EscondeMenus();

            //Diversos.ValoresADescontar _tela = new Diversos.ValoresADescontar();
            //_tela.ShowDialog();
            //AtivaTimer(sender, e);
        }

        private void radarBIPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBI;
            Diversos.RadarBI _tela = new Diversos.RadarBI();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void ajustesNFePanel_Click(object sender, EventArgs e)
        {
            //EscondeMenus();

            //Diversos.AjustesNfeEPedidos _tela = new Diversos.AjustesNfeEPedidos();
            //_tela.tituloLabel.Text = "Ajuste NF-e";
            //_tela.pedidoTabPageAdv.TabVisible = false;
            //_tela.NFtabPageAdv.TabVisible = true;
            //_tela.ShowDialog();
            //AtivaTimer(sender, e);
        }

        private void ajustePedidoPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            Diversos.AjustesNfeEPedidos _tela = new Diversos.AjustesNfeEPedidos();
            _tela.tituloLabel.Text = "Ajuste Pedidos";
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void retornarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            SAC.Retornar _tela = new SAC.Retornar();
            _tela.codigoTextBox.Text = Publicas._codigoRetornoPesquisa;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void satisfaçãoDoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            SAC.Satisfacao _tela = new SAC.Satisfacao();
            _tela.codigoTextBox.Text = Publicas._codigoRetornoPesquisa;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void satisfacaoPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaSAC;
            SAC.Satisfacao _tela = new SAC.Satisfacao();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void acessarLoginButton_Enter(object sender, EventArgs e)
        {
            acessarLoginButton.BackColor = Publicas._botaoFocado;
            acessarLoginButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void acessarLoginButton_Validating(object sender, CancelEventArgs e)
        {
            acessarLoginButton.BackColor = Publicas._botao;
            acessarLoginButton.ForeColor = Publicas._fonteBotao;
        }

        private void menuSACPanel_MouseLeave(object sender, EventArgs e)
        {
            menuSACPanel.Visible = !menuSACPanel.Visible;
        }

        private void AtendimentoSACTabPage_MouseEnter(object sender, EventArgs e)
        {
            // primeiro reduz o menu, para depois posicionar os lefts.
            menuAvulsoPanel.Visible = false;
            menuCadastroPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuNineBoxPanel.Visible = false;

        }

        private void sacGridGroupingControl_TableControlCellMouseHoverEnter(object sender, GridTableControlCellMouseEventArgs e)
        {
            menuAvulsoPanel.Visible = false;
            menuCadastroPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuSACPanel.Visible = false;
        }

        private void salaReuniaoMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCadastro;
            Cadastros.SalaDeReuniao _tela = new Cadastros.SalaDeReuniao();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void agendaMenuPanel_Click(object sender, EventArgs e)
        {
            //EscondeMenus();

            //Cadastros.Agenda _tela = new Cadastros.Agenda();
            //_tela.ShowDialog();
            //AtivaTimer(sender, e);
        }

        private void powerPictureBox_Click_1(object sender, EventArgs e)
        {
            ((Panel)((Panel)((PictureBox)sender).Parent).Parent).Visible = false;
        }

        private void aniversariosTimer_Tick(object sender, EventArgs e)
        {
            Control[] controle;
            string nome; 

            aniversariosTimer.Stop();
            if (Publicas._fecharAniversarios)
            {
                for (int i = 1; i < 7; i++)
                {
                    nome = "aniversarioPanel" + (i == 1 ? "" : i.ToString());

                    controle = this.Controls.Find(nome, true);

                    if (i == 1)
                        controle[0].Visible = false; // o primeiro não é criado em tempo de execução por isso fica invisivel
                    else
                    {
                        try
                        {
                            controle[0].Dispose();
                        }
                        catch { }
                    }
                }


                #region Corridas

                if (Publicas._usuario != null && !Publicas._usuario.NaoNotificaCorridas)
                {
                    List<Classes.Corridas> _listaCorrida = new CorridasBO().Listar(true, Publicas._usuario.Id);

                    Classes.ParticipanteCorrida _participante = new ParticipanteCorridaBO().Consultar(Publicas._idUsuario, 0);

                    try
                    {
                        int topAniversario = _topNotificacaoMedia - (comunicadoPanel.Height + 5);

                        if (_participante.Classificacao != 0 && !_participante.Visualizado)
                        {
                            resultadoPanel.Location = new Point(aniversarioPanel.Left, topAniversario);
                            tituloResultadoLabel.Text = "Seu resultado da corrida do dia " + _participante.DataCorrida.ToShortDateString();
                            classificacaoLabel.Text = "Classificação " + _participante.Classificacao.ToString();
                            classificacaoGeralLabel.Text = "Classificação geral " + _participante.ClassificacaoGeral.ToString();
                            tempoBrutoLabel.Text = "Tempo Bruto " + _participante.TempoBrutoFormatado;
                            tempoLiquidoLabel.Text = "Tempo Liquido " + _participante.TempoLiquidoFormatado;
                            ritmoLabel.Text = "Ritmo " + _participante.RitmoFormatado;
                            resultadoPanel.BringToFront();
                            resultadoPanel.Visible = true;
                            new ParticipanteCorridaBO().ResultadoVisualizado(_participante.Id);
                        }

                        topAniversario = _topNotificacaoPequena - (aniversarioPanel.Height + 5);
                        int qtd = 1;

                        foreach (var item in _listaCorrida.Where(w => w.Data >= DateTime.Now.Date))
                        {
                            // verifica se corrida foi exibida para o usuário
                            NotificacaoCorridas _notificacao = new NotificacaoCorridasBO().Consultar(item.Id);

                            if (qtd > 6 || item.IdUsuario != 0 || _notificacao.Existe)
                                break;

                            new NotificacaoCorridasBO().Gravar(item.Id);

                            if (!corridaPanel.Visible)
                            {
                                corridaPanel.Location = new Point(aniversarioPanel.Left, topAniversario);
                                dataCorridaLabel.Text = item.Data.ToShortDateString();
                                nomeCorridaLabel.Text = item.Nome;
                                nomeCorridaLabel.Tag = item.Id;
                                localCorridaLabel.Text = item.Local;
                                corridaPanel.Visible = true;
                            }
                            else
                            {
                                Panel panel = new Panel();
                                panel.Name = "corridaPanel" + qtd.ToString();

                                Panel panelTitulo = new Panel();
                                Label labelTitulo = new Label();
                                Label labelData = new Label();
                                Label labelNome = new Label();
                                Label labelLocal = new Label();
                                PictureBox imagem = new PictureBox();
                                PictureBox imagemBalao = new PictureBox();
                                imagem.Click += new System.EventHandler(this.powerPictureBox_Click_1);
                                labelNome.Click += new System.EventHandler(this.nomeCorridaLabel_Click);
                                labelNome.MouseLeave += new System.EventHandler(this.nomeCorridaLabel_MouseLeave);
                                labelNome.MouseHover += new System.EventHandler(this.nomeCorridaLabel_MouseHover);

                                imagem.Size = powerPictureBox.Size;
                                imagem.Image = powerPictureBox.Image;
                                imagem.SizeMode = PictureBoxSizeMode.StretchImage;

                                imagemBalao.Size = corridaPictureBox.Size;
                                imagemBalao.Image = corridaPictureBox.Image;
                                imagemBalao.SizeMode = corridaPictureBox.SizeMode;
                                imagemBalao.Location = corridaPictureBox.Location;

                                panel.Size = corridaPanel.Size;
                                panel.Location = new Point(corridaPanel.Left, topAniversario);
                                panelTitulo.Size = tituloCorridaPanel.Size;
                                panelTitulo.BackColor = tituloCorridaPanel.BackColor;

                                panel.Controls.Add(panelTitulo);
                                panel.Controls.Add(imagemBalao);

                                panelTitulo.Dock = DockStyle.Top;
                                panelTitulo.Controls.Add(labelTitulo);
                                panelTitulo.Controls.Add(imagem);
                                imagem.Dock = DockStyle.Right;
                                labelTitulo.Dock = DockStyle.Fill;
                                labelTitulo.ForeColor = tituloCorridaLabel.ForeColor;
                                labelTitulo.Font = tituloCorridaLabel.Font;
                                labelTitulo.TextAlign = ContentAlignment.MiddleLeft;
                                labelTitulo.Text = tituloCorridaLabel.Text;

                                labelData.Location = dataCorridaLabel.Location;
                                labelData.AutoSize = false;
                                labelData.Size = dataCorridaLabel.Size;
                                labelData.ForeColor = dataCorridaLabel.ForeColor;
                                labelData.Font = dataCorridaLabel.Font;

                                labelData.TextAlign = ContentAlignment.MiddleRight;
                                labelNome.Location = nomeCorridaLabel.Location;
                                labelNome.AutoSize = false;
                                labelNome.ForeColor = nomeCorridaLabel.ForeColor;
                                labelNome.Font = nomeCorridaLabel.Font;
                                labelNome.Size = nomeCorridaLabel.Size;
                                labelNome.TextAlign = ContentAlignment.MiddleLeft;

                                labelLocal.Location = localCorridaLabel.Location;
                                labelLocal.AutoSize = false;
                                labelLocal.Size = localCorridaLabel.Size;
                                labelLocal.TextAlign = ContentAlignment.MiddleLeft;
                                labelLocal.ForeColor = localCorridaLabel.ForeColor;
                                labelLocal.Font = localCorridaLabel.Font;

                                panel.Controls.Add(labelData);
                                panel.Controls.Add(labelNome);
                                panel.Controls.Add(labelLocal);

                                labelData.Text = item.Data.ToShortDateString();
                                labelNome.Text = item.Nome;
                                labelNome.Tag = item.Id;
                                labelLocal.Text = item.Local;

                                panel.Visible = true;
                                this.Controls.Add(panel);

                                panel.BringToFront();
                            }
                            topAniversario = topAniversario - (comunicadoPanel.Height + 5);
                            Refresh();
                            qtd++;

                        }
                    }
                    catch { }
                }

                #endregion

                #region Banco de horas

                try
                {
                    if (Publicas._usuario.IdEmpresa == 1) // Apenas para NIFF
                    {
                        List<PeriodoBancoHorasColaborador> _periodo = new PeriodoBancoHorasColaboradorBO().Listar(_colaboradores.Id);
                        DateTime _inicio;
                        DateTime _fim;

                        int _idSupervisor = 0;

                        if (supervisor.Existe)
                            _idSupervisor = _colaboradores.Id;

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
                            
                            BancoHorasPanel.BringToFront();

                            if (_bancoDeHoras.Count == 1)
                            {
                                int topAniversario = _topNotificacaoPequena - (aniversarioPanel.Height + 5);
                                bancoHorasGridGroupingControl.Visible = false;
                                bancoHorasGridGroupingControl.Visible = false;
                                foreach (var hora in _bancoDeHoras)
                                {

                                    {
                                        BancoHorasPanel.Size = new Size(331, 119);
                                        BancoHorasPanel.Location = new Point(aniversarioPanel.Left, topAniversario);

                                        PeriodoBancoDadosLabel.Text = _inicio.ToShortDateString() + " a " + _fim.ToShortDateString();
                                        liquidoBancoDados.Text = hora.TotalLiquido + " " + hora.Tipo;
                                        apuradoBancoHorasLabel.Text = "Apurado até a data " + hora.Data.ToShortDateString();
                                        BancoHorasPanel.Visible = hora.TotalLiquido != "00:00";
                                    }
                                }
                            }
                            else
                            {
                                apuradoBancoHorasLabel.Visible = false;
                                int topAniversario = _topNotificacaoMedia - (BancoHorasPanel.Height + 5);
                                BancoHorasPanel.Size = new Size(331, 307);
                                BancoHorasPanel.Location = new Point(aniversarioPanel.Left, topAniversario - 193);
                                bancoHorasGridGroupingControl.BringToFront();
                                bancoHorasGridGroupingControl.DataSource = _bancoDeHoras;
                                
                                bancoHorasGridGroupingControl.Visible = true;
                                BancoHorasPanel.Visible = true;
                            }
                        }
                    }
                }
                catch { }
                #endregion

                notificacaoBolaoTimer.Start();
            }
        }

        private void agendaScheduleControl_ShowingAppointmentForm(object sender, ShowingAppointFormEventArgs e)
        {
            e.Cancel = true;

            new Cadastros.Agenda().ShowDialog();
        }

//        his.scheduleControl1.SwitchViewStyle = false; 
 
//this.scheduleControl1.Calendar.CalenderGrid.MouseDown += CalenderGrid_MouseDown; 
//this.scheduleControl1.ScheduleContextMenuClick += scheduleControl1_ScheduleContextMenuClick; 
 
//void CalenderGrid_MouseDown(object sender, MouseEventArgs e)
//        {
//            this.scheduleControl1.SwitchViewStyle = false;
//        }
//        void scheduleControl1_ScheduleContextMenuClick(object sender, ScheduleContextMenuClickEventArgs e)
//        {
//            this.scheduleControl1.SwitchViewStyle = true;
//        }


        private void agendaScheduleControl_SetupContextMenu(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            //agendaScheduleControl.GetScheduleHost().ContextMenuStrip = contextMenuStrip1;
        }

        private void SetCheckValues(ToolStripMenuItem item)
        {
            //item.Checked = true;
            //ContextMenuStrip contextMenuStrip = item.Owner as ContextMenuStrip;

            //foreach (ToolStripMenuItem menuItem in contextMenuStrip.Items)
            //{
            //    if (item != menuItem)
            //        menuItem.Checked = false;
            //}
        }

        private void abrirChamadoMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaChamado;
            Chamados.Abertura _tela = new Chamados.Abertura();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void telasPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaChamado;
            Cadastros.Telas _tela = new Cadastros.Telas();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void parametrosMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaChamado;
            Cadastros.Parametros _tela = new Cadastros.Parametros();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void chamadoTimer_Tick(object sender, EventArgs e)
        { 
            //if (!dashBoardTabControl.Visible)
                //dashBoardTabControl.Visible = true;

            chamadoTimer.Stop();

            if (!_chamadosEmPesquisa)
                chamadosBackgroundWorker.RunWorkerAsync();
        }

        private void chamadosGridGroupingControl_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            int minutos = 1440; //total de minutos no dia

            minutos = _parametros.PrazoRetorno * minutos;
            Record dr;
            try
            { // buscar da empresa do usuario
                if (e.TableCellIdentity.RowIndex != -1)
                {
                    GridRecordRow rec = this.chamadosGridGroupingControl.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    //if (rec != null)
                    {
                        dr = rec.GetRecord() as Record;
                        if (dr != null && dr["DescricaoPrioridade"].Equals("Crítico"))
                        {
                            e.Style.TextColor = Color.Red;
                            e.Style.CellTipText = "Crítico";
                        }
                        if (dr != null && dr["DescricaoStatus"].Equals("Finalizado") && _parametros.ExigeAvaliacao)
                        {
                            e.Style.TextColor = Color.Green;
                            if (Publicas._idUsuario == (int)dr["idUsuario"])
                                e.Style.CellTipText = "Defina sua satisfação ao atendimento desse chamado";
                            else
                                e.Style.CellTipText = "Aguardando avaliação do solicitante.";
                        }
                    }
                    //}

                    if (e.TableCellIdentity.Column.MappingName == "SLA")
                    {
                        //if (Convert.ToDateTime(e.Style.Text) <= DateTime.Now.AddMinutes(minutos) &&
                        //    Convert.ToDateTime(e.Style.Text) >= DateTime.Now.AddMinutes((int)minutos / 2))
                        if (dr != null && dr["TipoPrazos"].Equals("N"))
                        {
                            e.Style.BackColor = Color.LightGreen;
                            e.Style.CellTipText = "Dentro do prazo";
                        }
                        else
                        //if (Convert.ToDateTime(e.Style.Text) <= DateTime.Now.AddMinutes((int)minutos / 2) &&
                        //    Convert.ToDateTime(e.Style.Text) >= DateTime.Now)
                        if (dr != null && dr["TipoPrazos"].Equals("C"))
                        {
                            e.Style.BackColor = Color.Khaki;
                            e.Style.CellTipText = "Prazo finalizando";
                        }
                        else
                        {
                            e.Style.BackColor = Color.Tomato;
                            e.Style.TextColor = Color.White;
                            e.Style.CellTipText = "Passou do prazo.";
                        }
                    }
                }
            }
            catch { }
        }

        private void chamadosGridGroupingControl_TableControlCellDoubleClick(object sender, GridTableControlCellClickEventArgs e)
        {
            // abrir a tela de tramites/historicos do chamado
            GridRecordRow rec = this.chamadosGridGroupingControl.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idChamado = (int)dr["IdChamado"];
                    Publicas._sla = (DateTime)dr["SLA"];
                    EscondeMenus();

                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaChamado;
                    new Chamados.Tramites().ShowDialog();
                    AtivaTimer(sender, e);
                }
                    
            }
        }

        private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_listaChamados == null)
                return;

            switch (statusComboBox.SelectedIndex)
            {
                case 0: // não Finalizados
                    chamadosGridGroupingControl.DataSource = _listaChamados.Where(w => w.Status != Publicas.StatusChamado.Finalizado && w.Status != Publicas.StatusChamado.Cancelado)
                                                                   .OrderBy(o => o.Data)
                                                                   .OrderBy(o => o.Ordem).ToList();
                    break;
                case 1: // Finalizados
                    chamadosGridGroupingControl.DataSource = _listaChamados.Where(w => w.Status == Publicas.StatusChamado.Finalizado)
                                                                   .OrderBy(o => o.Data)
                                                                   .OrderBy(o => o.Ordem).ToList();
                    break;
                case 2: // Cancelados
                    chamadosGridGroupingControl.DataSource = _listaChamados.Where(w => w.Status == Publicas.StatusChamado.Cancelado)
                                                                   .OrderBy(o => o.Data)
                                                                   .OrderBy(o => o.Ordem).ToList();
                    break;
                case 3: // Todos
                    chamadosGridGroupingControl.DataSource = _listaChamados.OrderBy(o => o.Data)
                                                                   .OrderBy(o => o.Ordem).ToList();
                    break;
            }
            
        }

        private void numerosChamadosLabel_Click(object sender, EventArgs e)
        {
            chamadosFinalizadosPanel.Visible = false;
            Publicas._idChamado = Convert.ToInt32(((Label)sender).Tag);
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaChamado;
            new Chamados.Tramites().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            chamadosFinalizadosPanel.Visible = false;
        }

        private void ParaTimer()
        {
            sacTimer.Stop();
            chamadoTimer.Stop();
            comunicadoTimer.Stop();
            notificacaoTimer.Stop();
            notificacaoChamadoTimer.Stop();            
        }

        private void AtivaTimer(object sender, EventArgs e)
        {
            tituloSistemaLabel.Text = Publicas._nomeDoSistema;
            sacTimer.Start();

            sacTimer_Tick(sender, e);
            chamadoTimer.Start();
            chamadoTimer_Tick(sender, e);
            comunicadoTimer.Start();
            comunicadoTimer_Tick(sender, e);
            notificacaoTimer.Stop();
            notificacaoTimer_Tick(sender, e);
            notificacaoChamadoTimer.Stop();
            notificacaoChamadoTimer_Tick(sender, e);
        }

        private void numerosChamadosLabel_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Hand;
        }

        private void numerosChamadosLabel_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Default;
        }

        private void juridicoPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuAvulsoPanel.Visible = false;
            menuCadastroPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuNineBoxPanel.Visible = false;

            if (!menuJuridicoPanel.Visible)
            {
                menuJuridicoPanel.Left = _leftSubMenus;
                menuJuridicoPanel.Top = (menuPanel.Width != 206 ? 226 : 251);
                menuJuridicoPanel.BringToFront();
                menuJuridicoPanel.Visible = true;
            }

            tituloJuridicoPanel.Visible = (menuPanel.Width != 206);

            juridicoLabel.Font = new Font(juridicoLabel.Font, FontStyle.Bold);
            juridicoPanel.BackColor = Color.Silver;
        }

        private void recolherMenuPictureBox_MouseHover(object sender, EventArgs e)
        {
            recolherMenuPictureBox.Cursor = Cursors.Hand;
        }

        private void recolherMenuPictureBox_MouseLeave(object sender, EventArgs e)
        {
            recolherMenuPictureBox.Cursor = Cursors.Default;
        }

        private void tipoPagamentoPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
            Cadastros.TiposDePagamento _tela = new Cadastros.TiposDePagamento();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void varaPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
            Cadastros.Vara _tela = new Cadastros.Vara();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void novoComunicadoPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
            Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Novo;
            Juridico.Comunicado _tela = new Juridico.Comunicado();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
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
                    mensagemComunicadoLabel.Text = "Pesquisando comunicados, aguarde... ";
                }
            }
            catch { }
        }

        private void statusComunidadosComboBoxAdv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Publicas._usuario != null)
                anosComunicadosComboBox_SelectedIndexChanged(sender, e);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //comunicadoBackgroundWorker

            if (Publicas._usuario.AcessaJuridico)
            {
                _comunicadosEmPesquisa = true;

                try
                {
                    if (anosComunicadosComboBox.Text.Contains("Todos") && statusComunidadosComboBoxAdv.Text.Contains("Todos"))
                        _listaComunicados = new ComunicadoBO().Listar();
                    else
                        _listaComunicados = new ComunicadoBO().Listar(0, Convert.ToInt32(anosComunicadosComboBox.Text), _statusComunicadoSelecionado);
                }
                catch
                {
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Publicas._usuario.AcessaJuridico)
            {
                //comunicadoBackgroundWorker
                textBoxExt1.Focus();
                mensagemLabel.Text = "Atualizando lista dos comunicados ...";
                mensagemLabel.Cursor = Cursors.WaitCursor;
                this.Refresh();

                //statusComunidadosComboBoxAdv_SelectedIndexChanged(sender, e);
                comunicadoGroupingControl.DataSource = _listaComunicados;

                mensagemLabel.Cursor = Cursors.Default;
                mensagemLabel.Text = "";

                _comunicadosEmPesquisa = false;

                if (_atualizarComunicado)
                {
                    if (!_comunicadosEmPesquisa)
                    {
                        comunicadoBackgroundWorker.RunWorkerAsync();
                        mensagemComunicadoLabel.Text = "Pesquisando comunicados, aguarde... ";
                    }
                }

                _atualizarComunicado = false;
                mensagemComunicadoLabel.Text = string.Empty;
                comunicadoTimer.Start();
            }

            try
            {
                juridicoTabPageAdv.TabVisible = Publicas._usuario.AcessaJuridico;
                //_listaComunicados.Count() != 0;
            }
            catch { }

            //aki
            dashBoardTabControl.Visible = ChamadosTabPage.TabVisible || AtendimentoSACTabPage.TabVisible || juridicoTabPageAdv.TabVisible || andamentoAvaliacaoTabPage.TabVisible ;
        }

        private void chamadosBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _chamadosEmPesquisa = true;

            try
            {
                if (Publicas._usuario.Tipo != Publicas.TipoUsuario.Atendente)
                    _listaChamados = new ChamadoBO().Listar(Publicas._usuario.IdDepartamento);
                else
                    _listaChamados = new ChamadoBO().Listar();
            }
            catch
            {
            }
        }

        private void chamadosBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            textBoxExt1.Focus();
            mensagemLabel.Text = "Atualizando lista dos chamados ...";
            mensagemLabel.Cursor = Cursors.WaitCursor;
            this.Refresh();

            statusComboBox_SelectedIndexChanged(sender, e);

            chamadosGridGroupingControl.Focus();
            mensagemLabel.Cursor = Cursors.Default;
            mensagemLabel.Text = "";

            ChamadosTabPage.TabVisible = _listaChamados.Count != 0;
            _chamadosEmPesquisa = false;

            #region Verifica chamados finalizados sem avaliação
            int n = 1;
            numerosChamados2Label.Visible = false;
            numerosChamados3Label.Visible = false;
            numerosChamados4Label.Visible = false;
            numerosChamados5Label.Visible = false;
            numerosChamados6Label.Visible = false;

            if (_parametros.ExigeAvaliacao && _listaChamados != null)
            {
                foreach (var item in _listaChamados.Where(w => w.Status == Publicas.StatusChamado.Finalizado &&
                                                          w.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.SemAvaliacao))
                {
                    switch (n)
                    {
                        case 1:
                            numerosChamadosLabel.Text = item.Numero;
                            numerosChamadosLabel.Tag = item.IdChamado;
                            break;
                        case 2:
                            numerosChamados2Label.Text = item.Numero;
                            numerosChamados2Label.Tag = item.IdChamado;
                            numerosChamados2Label.Visible = true;
                            break;
                        case 3:
                            numerosChamados3Label.Text = item.Numero;
                            numerosChamados3Label.Tag = item.IdChamado;
                            numerosChamados3Label.Visible = true;
                            break;
                        case 4:
                            numerosChamados4Label.Text = item.Numero;
                            numerosChamados4Label.Tag = item.IdChamado;
                            numerosChamados4Label.Visible = true;
                            break;
                        case 5:
                            numerosChamados5Label.Text = item.Numero;
                            numerosChamados5Label.Tag = item.IdChamado;
                            numerosChamados5Label.Visible = true;
                            break;
                        case 6:
                            numerosChamados6Label.Text = item.Numero;
                            numerosChamados6Label.Tag = item.IdChamado;
                            numerosChamados6Label.Visible = true;
                            break;
                    }
                    n++;
                }

                chamadosFinalizadosPanel.BringToFront();
                chamadosFinalizadosPanel.Left = (this.Width / 2) - chamadosFinalizadosPanel.Width / 2;
                chamadosFinalizadosPanel.Top = (this.Height / 2) - chamadosFinalizadosPanel.Height / 2;
                chamadosFinalizadosPanel.Visible = _listaChamados.Where(w => w.Status == Publicas.StatusChamado.Finalizado &&
                                                                             w.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.SemAvaliacao &&
                                                                             w.IdUsuario == Publicas._idUsuario).Count() != 0;
            }
            #endregion

            //aki
            dashBoardTabControl.Visible = ChamadosTabPage.TabVisible || AtendimentoSACTabPage.TabVisible || juridicoTabPageAdv.TabVisible || andamentoAvaliacaoTabPage.TabVisible;
        }

        private void anosComunicadosComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Publicas._anoSelecionadoComunicado = DateTime.Now.Year;
            _statusComunicadoSelecionado = Publicas.StatusComunicado.Todos;

            try
            {
                if (!anosComunicadosComboBox.Text.Contains("Todos"))
                    Publicas._anoSelecionadoComunicado = Convert.ToInt32(anosComunicadosComboBox.Text);
            }
            catch { }

            switch (statusComunidadosComboBoxAdv.SelectedIndex)
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
                    mensagemComunicadoLabel.Text = "Pesquisando comunicados, aguarde... ";
                }
                else
                    _atualizarComunicado = true;
            }
            catch { _atualizarComunicado = true; }
           
        }

        private void comunicadoGroupingControl_TableControlCellDoubleClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.comunicadoGroupingControl.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    EscondeMenus();

                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Alterado;
                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    new Juridico.Comunicado().ShowDialog();
                    AtivaTimer(sender, e);

                    Publicas._idComunicado = 0;
                }

            }
        }

        private void aprovarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.comunicadoGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    EscondeMenus();

                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Aprovado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();
                    _tela.tituloLabel.Text = "Aprovar Comunicado";
                    _tela.gravarButton.Visible = false;
                    _tela.statusButton.Text = "&Aprovar";
                    _tela.statusButton.Location = new Point(_tela.gravarButton.Left, _tela.gravarButton.Top);
                    _tela.statusButton.Visible = true;
                    _tela.ShowDialog();
                    AtivaTimer(sender, e);

                    Publicas._idComunicado = 0;
                }
            }
        }

        private void comunicadoGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                _rowIndexComunicado = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.comunicadoGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

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

        private void comunicadoGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                _rowIndexComunicado = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.comunicadoGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

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

        private void reprovarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.comunicadoGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    EscondeMenus();

                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Reprovado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();
                    _tela.tituloLabel.Text = "Reprovar Comunicado";
                    _tela.gravarButton.Visible = false;
                    _tela.statusButton.Text = "&Reprovar";
                    _tela.statusButton.Location = new Point(_tela.gravarButton.Left, _tela.gravarButton.Top);
                    _tela.statusButton.Visible = true;
                    _tela.ShowDialog();
                    AtivaTimer(sender, e);

                    Publicas._idComunicado = 0;
                }
            }
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.comunicadoGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    EscondeMenus();

                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Cancelado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();
                    _tela.tituloLabel.Text = "Cancelar Comunicado";
                    _tela.gravarButton.Visible = false;
                    _tela.statusButton.Text = "&Cancelar";
                    _tela.statusButton.Location = new Point(_tela.gravarButton.Left, _tela.gravarButton.Top);
                    _tela.statusButton.Visible = true;
                    _tela.ShowDialog();
                    AtivaTimer(sender, e);

                    Publicas._idComunicado = 0;
                }
            }
        }

        private void finalizarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.comunicadoGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    EscondeMenus();

                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Finalizado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();
                    _tela.tituloLabel.Text = "Finalizar Comunicado";
                    _tela.gravarButton.Visible = false;
                    _tela.statusButton.Text = "&Finalizar";
                    _tela.statusButton.Location = new Point(_tela.gravarButton.Left, _tela.gravarButton.Top);
                    _tela.statusButton.Visible = true;
                    _tela.ShowDialog();
                    AtivaTimer(sender, e);

                    Publicas._idComunicado = 0;
                }
            }
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.comunicadoGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    EscondeMenus();

                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Alterado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();

                    _tela.tituloLabel.Text = (alterarToolStripMenuItem.Text.Contains("Consultar") ? "Consultar" : "Alterar") + " Comunicado";
                    _tela.statusButton.Text = "Gravar";
                    _tela.ShowDialog();

                    AtivaTimer(sender, e);

                    Publicas._idComunicado = 0;
                }

            }
        }

        private void reenviarEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.comunicadoGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Publicas._idComunicado = (int)dr["Id"];
                    EscondeMenus();

                    tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaJuridico;
                    Publicas._chamadoPeloMenuDeComunicado = Publicas.StatusComunicado.Alterado;
                    Juridico.Comunicado _tela = new Juridico.Comunicado();
                    _tela.tituloLabel.Text = "Reeviar e-mail sobre o Comunicado";
                    _tela.gravarButton.Visible = false;
                    _tela.statusButton.Text = "&Enviar Email";
                    _tela.statusButton.Location = new Point(_tela.gravarButton.Left, _tela.gravarButton.Top);
                    _tela.statusButton.Visible = true;
                    _tela.ShowDialog();
                    AtivaTimer(sender, e);

                    Publicas._idComunicado = 0;
                }
            }
        }

        private void esqueceuSenhaLabel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuarioLoginTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe seu usuário.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioLoginTextBox.Focus();
                return;
            }

            Publicas._usuario = new UsuarioBO().Consultar(usuarioLoginTextBox.Text);

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Esqueceu a senha.";
            _log.Tela = "Principal - Login";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            senhaPanel.Location = new Point(loginPanel.Left, loginPanel.Top + loginPanel.Height);
            senhaPanel.Visible = true;
            cpfMaskedEditBox.Focus();

        }

        private void verSenhaPictureBox_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cpfMaskedEditBox.ClipText.Trim()))
            {
                new Notificacoes.Mensagem("Informe o seu CPF.", Publicas.TipoMensagem.Alerta).ShowDialog();
                cpfMaskedEditBox.Focus();
                return;
            }

            contadorTempoSenha = 0;

            if (cpfMaskedEditBox.ClipText != Publicas._usuario.CPF.ToString())
            {
                Log _log = new Log();
                _log.IdUsuario = Publicas._usuario.Id;
                _log.Descricao = "Informou o CPF inválido.";
                _log.Tela = "Principal - Login - Esqueceu senha";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }

                new Notificacoes.Mensagem("CPF inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaPanel.Visible = false;
                usuarioLoginTextBox.Focus();
                return;
            }

            textosenhalabel6.Visible = true;
            senhaLabel.Text = Publicas._usuario.Senha;
        }

        private void senhaTimer_Tick(object sender, EventArgs e)
        {
            cpfMaskedEditBox.Text = string.Empty;
            senhaLabel.Text = string.Empty;
            senhaTimer.Stop();
            senhaPanel.Visible = false;
            usuarioLoginTextBox.Focus();
        }

        private void verSenhaPictureBox_MouseHover(object sender, EventArgs e)
        {
            verSenhaPictureBox.Cursor = Cursors.Hand;
        }

        private void verSenhaPictureBox_MouseLeave(object sender, EventArgs e)
        {
            verSenhaPictureBox.Cursor = Cursors.Default;
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

        private void notificacaoComunicadoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                int topComunicado = _topNotificacaoMedia - (comunicadoPanel.Height + 5);
                int qtd = 1;

                Publicas._fecharComunicados = true;

                foreach (var item in _listaComunicadosNotificacao)
                {
                    
                    if (qtd > 6)
                        break;

                    if (!comunicadoPanel.Visible)
                    {
                        comunicadoPanel.Location = new Point(aniversarioPanel.Left, topComunicado);
                        tituloComunicadoLabel.Text = " Comunicado " + item.Status;
                        processoComunicadoLabel.Text = item.Processo;
                        varaComunicadoLabel.Text = item.Vara;
                        empresaComunicadoLabel.Text = item.Empresa;
                        solicitanteLabel.Text = (item.Status == Publicas.StatusComunicado.Novo ? item.Solicitante :
                            (item.Status == Publicas.StatusComunicado.Alterado ? item.UsuarioAlterador :
                            (item.Status == Publicas.StatusComunicado.Aprovado ? item.UsuarioAprovador :
                            (item.Status == Publicas.StatusComunicado.Cancelado ? item.UsuarioCancelador :
                            (item.Status == Publicas.StatusComunicado.Finalizado ? item.UsuarioFinaliza : item.UsuarioReprovador)))));
                        
                        comunicadoPanel.BringToFront();
                        comunicadoPanel.Visible = true;
                    }
                    else
                    {
                        Panel panel = new Panel();
                        panel.Name = "comunicadoPanel" + qtd.ToString();

                        Panel panelTitulo = new Panel();
                        Label labelTitulo = new Label();
                        Label labelData = new Label();
                        Label labelNome = new Label();
                        Label labelEmpresa = new Label();
                        Label labelSolicitante = new Label();
                        PictureBox imagem = new PictureBox();
                        PictureBox imagemBalao = new PictureBox();
                        imagem.Click += new System.EventHandler(this.powerPictureBox_Click_1);

                        imagem.Size = powerPictureBox.Size;
                        imagem.Image = powerPictureBox.Image;
                        imagem.SizeMode = PictureBoxSizeMode.StretchImage;

                        imagemBalao.Size = juridicoPicture.Size;
                        imagemBalao.Image = juridicoPicture.Image;
                        imagemBalao.SizeMode = juridicoPicture.SizeMode;
                        imagemBalao.Location = juridicoPicture.Location;

                        panel.Size = comunicadoPanel.Size;
                        panel.Location = new Point(comunicadoPanel.Left, topComunicado);
                        panelTitulo.Size = tituloComunicadoPanel.Size;
                        panelTitulo.BackColor = tituloComunicadoPanel.BackColor;

                        panel.Controls.Add(panelTitulo);
                        panel.Controls.Add(imagemBalao);

                        panelTitulo.Dock = DockStyle.Top;
                        panelTitulo.Controls.Add(labelTitulo);
                        panelTitulo.Controls.Add(imagem);
                        imagem.Dock = DockStyle.Right;
                        labelTitulo.Dock = DockStyle.Fill;
                        labelTitulo.ForeColor = tituloDataLabel.ForeColor;
                        labelTitulo.Font = tituloDataLabel.Font;
                        labelTitulo.TextAlign = ContentAlignment.MiddleLeft;

                        labelData.Location = processoComunicadoLabel.Location;
                        labelData.AutoSize = false;
                        labelData.TextAlign = ContentAlignment.MiddleLeft;
                        labelData.Size = processoComunicadoLabel.Size;
                        labelData.ForeColor = processoComunicadoLabel.ForeColor;
                        labelData.Font = processoComunicadoLabel.Font;

                        labelNome.Location = varaComunicadoLabel.Location;
                        labelNome.AutoSize = false;
                        labelNome.ForeColor = varaComunicadoLabel.ForeColor;
                        labelNome.Font = varaComunicadoLabel.Font;
                        labelNome.Size = varaComunicadoLabel.Size;
                        labelNome.TextAlign = ContentAlignment.MiddleLeft;

                        labelEmpresa.Location = empresaComunicadoLabel.Location;
                        labelEmpresa.AutoSize = false;
                        labelEmpresa.Size = empresaComunicadoLabel.Size;
                        labelEmpresa.TextAlign = ContentAlignment.MiddleLeft;
                        labelEmpresa.ForeColor = empresaComunicadoLabel.ForeColor;
                        labelEmpresa.Font = empresaComunicadoLabel.Font;

                        labelSolicitante.Location = solicitanteLabel.Location;
                        labelSolicitante.AutoSize = false;
                        labelSolicitante.Size = solicitanteLabel.Size;
                        labelSolicitante.TextAlign = ContentAlignment.MiddleLeft;
                        labelSolicitante.ForeColor = solicitanteLabel.ForeColor;
                        labelSolicitante.Font = solicitanteLabel.Font;

                        panel.Controls.Add(labelData);
                        panel.Controls.Add(labelNome);
                        panel.Controls.Add(labelEmpresa);
                        panel.Controls.Add(labelSolicitante);

                        labelTitulo.Text = " Comunicado " + item.Status;
                        labelData.Text = item.Processo;
                        labelNome.Text = item.Vara;
                        labelEmpresa.Text = item.Empresa;
                        labelSolicitante.Text = (item.Status == Publicas.StatusComunicado.Novo ? item.Solicitante :
                            (item.Status == Publicas.StatusComunicado.Alterado ? item.UsuarioAlterador :
                            (item.Status == Publicas.StatusComunicado.Aprovado ? item.UsuarioAprovador :
                            (item.Status == Publicas.StatusComunicado.Cancelado ? item.UsuarioCancelador :
                            (item.Status == Publicas.StatusComunicado.Finalizado ? item.UsuarioFinaliza : item.UsuarioReprovador)))));

                        panel.Visible = true;
                        this.Controls.Add(panel);

                        panel.BringToFront();
                    }

                    topComunicado = topComunicado - (chamadoPanel.Height + 5);
                    new ComunicadoBO().GravarNotificacao(item);
                    Refresh();
                    qtd++;
                    
                }
                fecharNotificacaoComunicadoTimer.Start();
                

                _notificacaoEmPesquisa = false;
            }
            catch { }
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

        private void fecharNotificacaoComunicadoTimer_Tick(object sender, EventArgs e)
        {
            Control[] controle;
            string nome;

            fecharNotificacaoComunicadoTimer.Stop();
            if (Publicas._fecharComunicados)
            {
                for (int i = 1; i < 7; i++)
                {
                    nome = "comunicadoPanel" + (i == 1 ? "" : i.ToString());

                    controle = this.Controls.Find(nome, true);

                    if (i == 1)
                        controle[0].Visible = false; // o primeiro não é criado em tempo de execução por isso fica invisivel
                    else
                    {
                        try
                        {
                            controle[0].Dispose();
                        }
                        catch { }
                    }
                }
            }

            notificacaoTimer.Start();
        }

        private void feriadoTimer_Tick(object sender, EventArgs e)
        {
            feriadoPanel.Visible = false;
            feriadoTimer.Stop();
        }

        private void acessarLoginButton_Leave(object sender, EventArgs e)
        {
            acessarLoginButton.BackColor = Publicas._botao;
            acessarLoginButton.ForeColor = Publicas._fonteBotao;
        }

        private void avaliacaoDesempenhoMenuPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuAvulsoPanel.Visible = false;
            menuCadastroPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuNineBoxPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;

            if (!menuAvaliacaoDesempenhoPanel.Visible)
            {
                menuAvaliacaoDesempenhoPanel.Left = _leftSubMenus;
                menuAvaliacaoDesempenhoPanel.Top = (menuPanel.Width != 206 ? 274 : 299);
                menuAvaliacaoDesempenhoPanel.BringToFront();
                menuAvaliacaoDesempenhoPanel.Visible = true;
            }

            tituloAvaliacaoDesempenhoPanel.Visible = (menuPanel.Width != 206);

            avaliacaoDesempenhoLabel.Font = new Font(avaliacaoDesempenhoLabel.Font, FontStyle.Bold);
            avaliacaoDesempenhoMenuPanel.BackColor = Color.Silver;
        }

        private void cadastroAvaliacaoPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuCadastroPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;

            if (!menuCadastroAvaliacaoPanel.Visible)
            {
                menuCadastroAvaliacaoPanel.Left = _leftSubMenus2 + 205;
                menuCadastroAvaliacaoPanel.Top = menuRHAvaliacaoPanel.Top; // 347;
                menuCadastroAvaliacaoPanel.BringToFront();
                menuCadastroAvaliacaoPanel.Visible = true;
            }

            cadastroAvaliacaoLabel.Font = new Font(cadastroAvaliacaoLabel.Font, FontStyle.Bold);
            cadastroAvaliacaoPanel.BackColor = Color.Silver;
        }

        private void comunicadoGroupingControl_MouseEnter(object sender, EventArgs e)
        {
            menuAvulsoPanel.Visible = false;
            menuCadastroPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuNineBoxPanel.Visible = false;
        }

        private void chamadosGridGroupingControl_MouseEnter(object sender, EventArgs e)
        {
            menuAvulsoPanel.Visible = false;
            menuCadastroPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuNineBoxPanel.Visible = false;
        }

        private void cadastrosComunicadoPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuCorridasPanel.Visible = false;
            menuNineBoxPanel.Visible = false;
            if (!menuCadastroJuridicoPanel.Visible)
            {
                menuCadastroJuridicoPanel.Left = _leftSubMenus2;
                menuCadastroJuridicoPanel.Top = 251;
                menuCadastroJuridicoPanel.BringToFront();
                menuCadastroJuridicoPanel.Visible = true;
            }

            cadastrosComunicadoLabel.Font = new Font(cadastrosComunicadoLabel.Font, FontStyle.Bold);
            cadastrosComunicadoPanel.BackColor = Color.Silver;
            novoComunicadoLabel.Font = new Font(novoComunicadoLabel.Font, novoComunicadoLabel.Font.Style & ~FontStyle.Bold);
        }

        private void notificacaoChamadoBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _notificacaoEmPesquisa = true;

            try
            {
                _listaChamadosNotificacao = new NotificacaoChamadoBO().Listar();
            }
            catch
            {
            }
        }

        private void notificacaoChamadoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                int topComunicado = _topNotificacaoMedia - (chamadoPanel.Height + 5);
                int qtd = 1;

                Publicas._fecharChamados = true;

                foreach (var item in _listaChamadosNotificacao)
                {

                    if (qtd > 6)
                        break;

                    if (!chamadoPanel.Visible)
                    {
                        chamadoPanel.Location = new Point(aniversarioPanel.Left, topComunicado);
                        tituloChamadoLabel.Text = " Chamado " + Publicas.GetDescription(item.Status, "");
                        numeroChamadoLabel.Text = item.Numero;
                        numeroChamadoLabel.Tag = item.IdChamado;
                        assuntoChamadoLabel.Text = item.Adequacao;
                        empresaChamadoLabel.Text = item.Empresa;
                        solicitanteChamadoLabel.Text = item.NomeAnalista;
                        
                        chamadoPanel.BringToFront();
                        chamadoPanel.Visible = true;
                    }
                    else
                    {
                        Panel panel = new Panel();
                        panel.Name = "chamadoPanel" + qtd.ToString();

                        Panel panelTitulo = new Panel();
                        Label labelTitulo = new Label();
                        Label labelData = new Label();
                        Label labelNome = new Label();
                        Label labelEmpresa = new Label();
                        Label labelSolicitante = new Label();
                        PictureBox imagem = new PictureBox();
                        PictureBox imagemBalao = new PictureBox();
                        imagem.Click += new System.EventHandler(this.powerPictureBox_Click_1);

                        imagem.Size = powerPictureBox.Size;
                        imagem.Image = powerPictureBox.Image;
                        imagem.SizeMode = PictureBoxSizeMode.StretchImage;

                        imagemBalao.Size = chamadoPictureBox.Size;
                        imagemBalao.Image = chamadoPictureBox.Image;
                        imagemBalao.SizeMode = chamadoPictureBox.SizeMode;
                        imagemBalao.Location = chamadoPictureBox.Location;

                        panel.Size = chamadoPanel.Size;
                        panel.Location = new Point(chamadoPanel.Left, topComunicado);
                        panelTitulo.Size = tituloChamadoPanel.Size;
                        panelTitulo.BackColor = tituloChamadoPanel.BackColor;

                        panel.Controls.Add(panelTitulo);
                        panel.Controls.Add(imagemBalao);

                        panelTitulo.Dock = DockStyle.Top;
                        panelTitulo.Controls.Add(labelTitulo);
                        panelTitulo.Controls.Add(imagem);
                        imagem.Dock = DockStyle.Right;
                        labelTitulo.Dock = DockStyle.Fill;
                        labelTitulo.ForeColor = tituloDataLabel.ForeColor;
                        labelTitulo.Font = tituloDataLabel.Font;
                        labelTitulo.TextAlign = ContentAlignment.MiddleLeft;

                        labelData.Location = numeroChamadoLabel.Location;
                        labelData.AutoSize = false;
                        labelData.TextAlign = ContentAlignment.MiddleLeft;
                        labelData.Size = numeroChamadoLabel.Size;
                        labelData.ForeColor = numeroChamadoLabel.ForeColor;
                        labelData.Font = numeroChamadoLabel.Font;
                        labelData.Click += new System.EventHandler(this.numeroChamadoLabel_Click);
                        labelData.MouseLeave += new System.EventHandler(this.numeroChamadoLabel_MouseLeave);
                        labelData.MouseHover += new System.EventHandler(this.numeroChamadoLabel_MouseHover);
                        
                        labelNome.Location = assuntoChamadoLabel.Location;
                        labelNome.AutoSize = false;
                        labelNome.ForeColor = assuntoChamadoLabel.ForeColor;
                        labelNome.Font = assuntoChamadoLabel.Font;
                        labelNome.Size = assuntoChamadoLabel.Size;
                        labelNome.TextAlign = ContentAlignment.MiddleLeft;

                        labelEmpresa.Location = empresaChamadoLabel.Location;
                        labelEmpresa.AutoSize = false;
                        labelEmpresa.Size = empresaChamadoLabel.Size;
                        labelEmpresa.TextAlign = ContentAlignment.MiddleLeft;
                        labelEmpresa.ForeColor = empresaChamadoLabel.ForeColor;
                        labelEmpresa.Font = empresaChamadoLabel.Font;

                        labelSolicitante.Location = solicitanteChamadoLabel.Location;
                        labelSolicitante.AutoSize = false;
                        labelSolicitante.Size = solicitanteChamadoLabel.Size;
                        labelSolicitante.TextAlign = ContentAlignment.MiddleLeft;
                        labelSolicitante.ForeColor = solicitanteChamadoLabel.ForeColor;
                        labelSolicitante.Font = solicitanteChamadoLabel.Font;

                        panel.Controls.Add(labelData);
                        panel.Controls.Add(labelNome);
                        panel.Controls.Add(labelEmpresa);
                        panel.Controls.Add(labelSolicitante);

                        labelTitulo.Text = " Chamado " + Publicas.GetDescription(item.Status, "");
                        labelData.Text = item.Numero;
                        labelData.Tag = item.IdChamado;
                        labelNome.Text = item.Assunto;
                        labelEmpresa.Text = item.Empresa;
                        labelSolicitante.Text = item.NomeAnalista;

                        panel.Visible = true;
                        this.Controls.Add(panel);

                        panel.BringToFront();

                    }

                    if (Publicas._idUsuario != item.IdUsuario && Publicas._idUsuario != item.IdUsuarioAnterior)
                        new NotificacaoChamadoBO().Gravar(item);

                    topComunicado = topComunicado - (chamadoPanel.Height + 5);

                    Refresh();
                    qtd++;

                }
                fecharNotificacaoChamadoTimer.Start();

                _notificacaoEmPesquisa = false;
            }
            catch { }
        }

        private void numeroChamadoLabel_Click(object sender, EventArgs e)
        {
            chamadosFinalizadosPanel.Visible = false;
            Publicas._idChamado = Convert.ToInt32(((Label)sender).Tag);
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaChamado;
            NotificacaoChamado _notificacao = new NotificacaoChamadoBO().Consultar(Publicas._idChamado );
            new NotificacaoChamadoBO().Gravar(_notificacao);

            new Chamados.Tramites().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void numeroChamadoLabel_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Hand;
        }

        private void numeroChamadoLabel_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Default;
        }

        private void notificacaoChamadoTimer_Tick(object sender, EventArgs e)
        {
            if (Publicas._usuario == null)
                return;

            notificacaoChamadoTimer.Stop();

            try
            {
                if (!_notificacaoChamadoEmPesquisa)
                {
                    notificacaoChamadoBackgroundWorker.RunWorkerAsync();
                }
            }
            catch { }
        }

        private void fecharNotificacaoChamadoTimer_Tick(object sender, EventArgs e)
        {
            Control[] controle;
            string nome;

            fecharNotificacaoChamadoTimer.Stop();
            if (Publicas._fecharComunicados)
            {
                for (int i = 1; i < 7; i++)
                {
                    nome = "chamadoPanel" + (i == 1 ? "" : i.ToString());

                    controle = this.Controls.Find(nome, true);

                    if (i == 1)
                        controle[0].Visible = false; // o primeiro não é criado em tempo de execução por isso fica invisivel
                    else
                    {
                        try
                        {
                            controle[0].Dispose();
                        }
                        catch { }
                    }
                }
            }

            notificacaoChamadoTimer.Start();
        }

        private void agruparChamadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chamadosGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            chamadosGridGroupingControl.TableDescriptor.AllowEdit = true;
            chamadosGridGroupingControl.TableDescriptor.VisibleColumns.Add("Agrupar");
            chamadosGridGroupingControl.TableDescriptor.Columns["Agrupar"].ReadOnly = false;
            chamadosGridGroupingControl.TableDescriptor.VisibleColumns.Move(14, 1);
            agruparButton.Enabled = true;
        }

        private void agruparButton_Click(object sender, EventArgs e)
        {
            new ChamadoBO().Agrupar(_listaChamados);

            chamadosGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            chamadosGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Agrupar");
            chamadosGridGroupingControl.TableDescriptor.AllowEdit = false;
            agruparButton.Enabled = false;
        }

        private void escolaridadePanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Escolaridade().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void prazosPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Prazos().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void competenciaPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Competencia().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void corridasPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuLimpezaPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;
            menuBibliotecaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;
            menuSistemasPanel.Visible = false;

            if (!menuCorridasPanel.Visible)
            {
                menuCorridasPanel.Left = _leftSubMenus2;
                menuCorridasPanel.Top = 443;
                menuCorridasPanel.BringToFront();
                menuCorridasPanel.Visible = true;
            }

            corridasLabel.Font = new Font(corridasLabel.Font, FontStyle.Bold);
            corridasPanel.BackColor = Color.Silver;
        }

        private void cadastroCorridasPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCorridas;
            new Cadastros.Corridas().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void participantesPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCorridas;
            new Cadastros.Participantes().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void pictureBox71_Click(object sender, EventArgs e)
        {
            ((Panel)((Panel)((PictureBox)sender).Parent).Parent).Visible = false;
        }

        private void nomeCorridaLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCorridas;
            Cadastros.Participantes _tela = new Cadastros.Participantes();
            _tela.codigoTextBox.Text = Convert.ToString(nomeCorridaLabel.Tag);

            ((Panel)((Label)sender).Parent).Visible = false;

            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void nomeCorridaLabel_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Hand;
        }

        private void nomeCorridaLabel_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Default;
        }

        private void resultadosPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCorridas;
            new Cadastros.ResultadoDasCorridas().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void cargosPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Cargos().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void pessoasPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Pessoas().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void metasPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            new Cadastros.Metas().ShowDialog();
            AtivaTimer(sender, e);
        }

        private void autoAvaliacaoPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.AvaliacaoComportamental _tela = new Avaliacao_de_desempenho.AvaliacaoComportamental();
            _tela.tituloLabel.Text = "Auto Avaliação";
            _tela.empresaComboBoxAdv.Enabled = false;
            _tela.usuarioTextBox.Enabled = false;
            _tela.tipoAvaliacao = Publicas.TipoPrazos.AutoAvaliacao;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void metasCrescimentoPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.AvaliacaoComportamental _tela = new Avaliacao_de_desempenho.AvaliacaoComportamental();
            _tela.tituloLabel.Text = "Avaliação comportamental da equipe - pelo Gestor";
            _tela.empresaComboBoxAdv.Enabled = true;
            _tela.usuarioTextBox.Enabled = true;

            _tela.tipoAvaliacao = Publicas.TipoPrazos.AvaliacaoDoGestor;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void RecursosHumanosPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;

            if (!menuRHAvaliacaoPanel.Visible)
            {
                menuRHAvaliacaoPanel.Left = _leftSubMenus2;
                menuRHAvaliacaoPanel.Top = (Publicas._usuario.AcessoDeControladoria ? 347 : 299);
                menuRHAvaliacaoPanel.BringToFront();
                menuRHAvaliacaoPanel.Visible = true;
            }

            RecursosHumanosLabel.Font = new Font(RecursosHumanosLabel.Font, FontStyle.Bold);
            RecursosHumanosPanel.BackColor = Color.Silver;            
        }

        private void colaboradoresPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuNineBoxPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;

            if (!menuColaboradorAvaliacaoPanel.Visible)
            {
                menuColaboradorAvaliacaoPanel.Left = _leftSubMenus2;
                menuColaboradorAvaliacaoPanel.Top = (Publicas._usuario.AcessoDeControladoria && Publicas._usuario.AcessoDeRH ? 395 : 
                                                    (Publicas._usuario.AcessoDeControladoria || Publicas._usuario.AcessoDeRH ? 347 : 299)) ;
                menuColaboradorAvaliacaoPanel.BringToFront();
                menuColaboradorAvaliacaoPanel.Visible = true;
            }

            colaboradoresLabel.Font = new Font(colaboradoresLabel.Font, FontStyle.Bold);
            colaboradoresPanel.BackColor = Color.Silver;
        }

        private void menuGestorPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;
            menuNineBoxPanel.Visible = false;

            if (!menuGestorPanel.Visible)
            {
                menuGestorPanel.Left = _leftSubMenus2;
                menuGestorPanel.Top = (Publicas._usuario.AcessoDeControladoria && Publicas._usuario.AcessoDeRH && Publicas._usuario.AcessoDeColaborador ? 443 :
                                      ((Publicas._usuario.AcessoDeControladoria && Publicas._usuario.AcessoDeRH) ||
                                       (Publicas._usuario.AcessoDeColaborador && Publicas._usuario.AcessoDeRH) ||
                                       (Publicas._usuario.AcessoDeColaborador && Publicas._usuario.AcessoDeControladoria) ? 394 :
                                       (!Publicas._usuario.AcessoDeControladoria && !Publicas._usuario.AcessoDeRH && !Publicas._usuario.AcessoDeColaborador ? 299 : 347)));
                menuGestorPanel.BringToFront();
                menuGestorPanel.Visible = true;
            }

            gestorLabel.Font = new Font(gestorLabel.Font, FontStyle.Bold);
            GestorPanel.BackColor = Color.Silver;
        }

        private void metasDeCrescimentoRHPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.AvaliacaoComportamental _tela = new Avaliacao_de_desempenho.AvaliacaoComportamental();
            _tela.tituloLabel.Text = "Avaliação comportamental da equipe - pelo Recursos Humanos";
            _tela.empresaComboBoxAdv.Enabled = true;
            _tela.usuarioTextBox.Enabled = true;

            _tela.tipoAvaliacao = Publicas.TipoPrazos.AvaliacaoRH;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void feedbakcColaboradorPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Feedback _tela = new Avaliacao_de_desempenho.Feedback();
            _tela.Size = new Size(718, 569);
            _tela.empresaComboBoxAdv.Enabled = false;
            _tela.usuarioTextBox.Enabled = false;

            _tela.tipoAvaliacao = Publicas.TipoPrazos.FeedbackAvaliado;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void feedbakcGestorPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Feedback _tela = new Avaliacao_de_desempenho.Feedback();
            _tela.Size = new Size(718, 383);
            _tela.empresaComboBoxAdv.Enabled = true;
            _tela.usuarioTextBox.Enabled = true;

            _tela.tipoAvaliacao = Publicas.TipoPrazos.FeedbackGestor;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void feedbackRHPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Feedback _tela = new Avaliacao_de_desempenho.Feedback();
            _tela.Size = new Size(718, 569);
            _tela.empresaComboBoxAdv.Enabled = true;
            _tela.usuarioTextBox.Enabled = true;

            _tela.tipoAvaliacao = Publicas.TipoPrazos.RHConsultaFeedback;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void definicaoMetasGestorPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.DefinicaoDeMetas _tela = new Avaliacao_de_desempenho.DefinicaoDeMetas();

            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void controladoriaPictureBox_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuNineBoxPanel.Visible = false;

            if (!menuControladoriaAvaliacaoPanel.Visible)
            {
                menuControladoriaAvaliacaoPanel.Left = _leftSubMenus2;
                menuControladoriaAvaliacaoPanel.Top = 299;
                menuControladoriaAvaliacaoPanel.BringToFront();
                menuControladoriaAvaliacaoPanel.Visible = true;
            }

            controladoriaLabel.Font = new Font(controladoriaLabel.Font, FontStyle.Bold);
            controladoriaPanel.BackColor = Color.Silver;
        }

        private void integrarNotasPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaGlobus;
            Contabilidade.IntegraNotaServico _tela = new Contabilidade.IntegraNotaServico();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void metasDeResultadoRHPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.DefinicaoDeMetas _tela = new Avaliacao_de_desempenho.DefinicaoDeMetas();
            _tela.tituloLabel.Text = "Metas Numéricas - RH";
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void metasDeResultadoColaboradorPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.DefinicaoDeMetas _tela = new Avaliacao_de_desempenho.DefinicaoDeMetas();
            _tela.tituloLabel.Text = "Metas Numéricas";

            _tela.empresaComboBoxAdv.Enabled = false;
            _tela.usuarioTextBox.Enabled = false;

            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void metasDeResultadoGestorPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.DefinicaoDeMetas _tela = new Avaliacao_de_desempenho.DefinicaoDeMetas();
            _tela.tituloLabel.Text = "Metas Numéricas - Gestor";
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void pontuacaoMenuPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Cadastros.Pontuacao _tela = new Cadastros.Pontuacao();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void radarRHPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            Publicas._telaRadarChamadaPeloMenu = "RH";
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Radar _tela = new Avaliacao_de_desempenho.Radar();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void radarGestorPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            Publicas._telaRadarChamadaPeloMenu = "Gestor";
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Radar _tela = new Avaliacao_de_desempenho.Radar();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void notificacaoPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCadastro;
            Notificacoes.Cadastro _tela = new Notificacoes.Cadastro();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void radarColaboradorPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            Publicas._telaRadarChamadaPeloMenu = "Colaborador";
            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Radar _tela = new Avaliacao_de_desempenho.Radar();
            _tela.empresaComboBoxAdv.Enabled = false;
            _tela.usuarioTextBox.Enabled = false;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void periodoBancoHorasPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaCadastro;
            Cadastros.PeriodoBancoHoras _tela = new Cadastros.PeriodoBancoHoras();
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void closeBancoHorasPictureBox_Click(object sender, EventArgs e)
        {
            BancoHorasPanel.Visible = false;
        }

        private void cpfMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            verSenhaPictureBox_Click(sender, new EventArgs());
        }

        private void cpfMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                usuarioLoginTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cpfMaskedEditBox.Focus();
            }
        }

        private void tabPageAdv1_TabIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void tabPageAdv1_Enter(object sender, EventArgs e)
        {
            int tamanho = (int)(colaboradoresAvaliacaoPanel.Width / 3);
            emAndamentoPanel.Size = new Size(tamanho, emAndamentoPanel.Height);
            naoIniciadasPanel.Size = new Size(tamanho, emAndamentoPanel.Height);
            finalizadasPanel.Size = new Size(tamanho, emAndamentoPanel.Height);

            List<Classes.Prazos> _prazos = new PrazosBO().Listar(true, true);

            referenciasComboBox.Items.Clear();
            tipoAvaliacaoComboBox.Items.Clear();

            tipoAvaliacaoComboBox.Items.AddRange(new object[] { "Auto Avaliação", "Feedback do Gestor", "Metas numéricas", "Avaliação do Gestor", "Avaliação do RH", "Feedback do Avaliado", "Plano de Desenvolvimento individual" });

            foreach (var item in _prazos)
            {
                referenciasComboBox.Items.Add(item.Referencia);
            }

            referenciasComboBox.SelectedIndex = 0;
            tipoAvaliacaoComboBox.SelectedIndex = 0;
        }

        private void tipoAvaliacaoComboBox_SelectedIndexChanging(object sender, SelectedIndexChangingArgs e)
        {

        }

        private void tipoAvaliacaoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            finalizadosGridGroupingControl.DataSource = new List<AutoAvaliacao>();
            emAndamentoGridGroupingControl.DataSource = new List<AutoAvaliacao>();
            naoIniciadoGridGroupingControl.DataSource = new List<AutoAvaliacao>();

            try
            {
                idSuperior = 0;

                if (Publicas._usuario.AcessoDeGestor && !Publicas._usuario.AcessoDeRH)
                    idSuperior = _colaboradores.Id;

                tipoAvaliacao = (tipoAvaliacaoComboBox.SelectedIndex == 0 ? "AA" : // AutoAvaliacao
                                      (tipoAvaliacaoComboBox.SelectedIndex == 1 ? "FG" : // Feedback Gestor
                                      (tipoAvaliacaoComboBox.SelectedIndex == 2 ? "MN" : // Metas Numericas
                                      (tipoAvaliacaoComboBox.SelectedIndex == 3 ? "AG" : // Avaliação Gestos
                                      (tipoAvaliacaoComboBox.SelectedIndex == 4 ? "AR" : // Avaliação RH
                                      (tipoAvaliacaoComboBox.SelectedIndex == 5 ? "FA" : // FeedBack Avaliado
                                      "PD" // Plano de desenvolvimento
                                      ))))));

                andamentoAvaliacaoBackgroundWorker.RunWorkerAsync();
            }
            catch { }

        }

        private void ParentTable_PrepareViewStyleInfo(object sender, GridPrepareViewStyleInfoEventArgs e)
        {
            if (e.ColIndex == 2 && e.RowIndex == 2) // Sets the font, color, alignment and text to the dropped column
                e.Style.Text = "";
        }
        
        private void referenciasComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipoAvaliacaoComboBox_SelectedIndexChanged(sender, e);
        }

        private void naoIniciadoGridGroupingControl_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            if (e.TableCellIdentity.TableCellType == GridTableCellType.GroupCaptionCell && e.TableCellIdentity.GroupedColumn != null)
            {
                string x = e.Style.Text;
            }
        }

        private void andamentoAvaliacaoBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!_andamentoAvaliacaoEmPesquisa)
            {
                andamentoAvaliacaoTimer.Stop();

                _andamentoAvaliacaoEmPesquisa = true;
                
                try
                {
                    _listaAvaliacoesNotas = new AutoAvaliacaoBO().ListarNotas(tipoAvaliacao, idSuperior);

                    _listaAvaliacoes = new AutoAvaliacaoBO().Listar(Convert.ToInt32(referenciasComboBox.Text.Replace("/", "")), _cargo.TipoDoCargo, tipoAvaliacao, idSuperior);
                }
                catch
                {
                    _andamentoAvaliacaoEmPesquisa = false;
                }
            }
        }

        private void andamentoAvaliacaoBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!_mudouVisualizacaoAndamento)
                andamentoButton_Click(sender, e);

            try
            {
                rankingGridGroupingControl.DataSource = new List<AutoAvaliacao>();

                #region Popula os grids
                finalizadosGridGroupingControl.DataSource = _listaAvaliacoes.Where(w => w.Status == "F").ToList();
                emAndamentoGridGroupingControl.DataSource = _listaAvaliacoes.Where(w => w.Status == "I").ToList();
                naoIniciadoGridGroupingControl.DataSource = _listaAvaliacoes.Where(w => w.Status == "N").ToList();
                #endregion

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
                chartControl1.Series.Add(_serie1);
                chartControl1.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                #region Grafico EOVG Lavras

                ChartSeries _serieLavras = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

                todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 3).Count();
                finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 3).Count();
                iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 3).Count();
                naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 3).Count();

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
                chartControl10.Series.Add(_serieLavras);
                chartControl10.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                #region Grafico ABC

                ChartSeries _serieABC = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

                todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 4).Count();
                finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 4).Count();
                iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 4).Count();
                naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 4).Count();

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
                chartControl2.Series.Add(_serieABC);
                chartControl2.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                #region Grafico Rapido

                ChartSeries _serieRapido = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

                todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 5).Count();
                finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 5).Count();
                iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 5).Count();
                naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 5).Count();

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
                chartControl3.Series.Add(_serieRapido);
                chartControl3.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                #region Grafico Cisne

                ChartSeries _serieCisne = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

                todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 6).Count();
                finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 6).Count();
                iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 6).Count();
                naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 6).Count();

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
                chartControl4.Series.Add(_serieCisne);
                chartControl4.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                #region Grafico NIFF

                ChartSeries _serieNiff = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

                todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 1).Count();
                finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 1).Count();
                iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 1).Count();
                naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 1).Count();

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
                chartControl5.Series.Add(_serieNiff);
                chartControl5.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                #region Grafico Aruja

                ChartSeries _serieAruja = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

                todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 7).Count();
                finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 7).Count();
                iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 7).Count();
                naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 7).Count();

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
                chartControl6.Series.Add(_serieAruja);
                chartControl6.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                #region Grafico campibus

                ChartSeries _serieCampibus = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

                todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 8).Count();
                finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 8).Count();
                iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 8).Count();
                naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 8).Count();

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
                chartControl7.Series.Add(_serieCampibus);
                chartControl7.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                #region Grafico Ribe

                ChartSeries _serieRibe = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

                todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 9).Count();
                finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 9).Count();
                iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 9).Count();
                naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 9).Count();

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
                chartControl8.Series.Add(_serieRibe);
                chartControl8.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                #region Grafico VUG Dutra

                ChartSeries _serieVugDutra = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

                todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 10).Count();
                finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 10).Count();
                iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 10).Count();
                naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 10).Count();

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
                chartControl9.Series.Add(_serieVugDutra);
                chartControl9.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                #region Grafico VUG Bebedouro

                ChartSeries _serieVugBebedouro = new ChartSeries("Não Iniciadas", ChartSeriesType.Pie);

                todos = _listaAvaliacoes.Where(w => w.IdEmpresa == 11).Count();
                finalizados = _listaAvaliacoes.Where(w => w.Status == "F" && w.IdEmpresa == 11).Count();
                iniciados = _listaAvaliacoes.Where(w => w.Status == "I" && w.IdEmpresa == 11).Count();
                naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N" && w.IdEmpresa == 11).Count();

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
                chartControl11.Series.Add(_serieVugBebedouro);
                chartControl11.BackColor = tipoAvaliacaoComboBox.BackColor;
                #endregion

                naoIniciadoGridGroupingControl.ShowGroupDropArea = true;
                naoIniciadoGridGroupingControl.GridGroupDropArea.BackColor = Publicas._bordaEntrada;
                naoIniciadoGridGroupingControl.GridGroupDropArea.PrepareViewStyleInfo += new GridPrepareViewStyleInfoEventHandler(ParentTable_PrepareViewStyleInfo);
                emAndamentoGridGroupingControl.ShowGroupDropArea = true;
                emAndamentoGridGroupingControl.GridGroupDropArea.BackColor = Publicas._bordaEntrada;
                emAndamentoGridGroupingControl.GridGroupDropArea.PrepareViewStyleInfo += new GridPrepareViewStyleInfoEventHandler(ParentTable_PrepareViewStyleInfo);
                finalizadosGridGroupingControl.ShowGroupDropArea = true;
                finalizadosGridGroupingControl.GridGroupDropArea.BackColor = Publicas._bordaEntrada;
                finalizadosGridGroupingControl.GridGroupDropArea.PrepareViewStyleInfo += new GridPrepareViewStyleInfoEventHandler(ParentTable_PrepareViewStyleInfo);

                chartControl1.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl2.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl3.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl4.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl5.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl6.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl7.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl8.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl9.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl10.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl11.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);

                chartControl1.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl2.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl3.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl4.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl5.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl6.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl7.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl8.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl9.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl10.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                chartControl11.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                #endregion

                rankingGridGroupingControl.DataSource = _listaAvaliacoesNotas.Where(w => w.ReferenciaFormatada == referenciasComboBox.Text).ToList();

                MontaGraficosNotas();


                int tamanho = (int)(colaboradoresAvaliacaoPanel.Width / 8);
                graficoEOVGDutraPanel.Size = new Size(tamanho, graficoEOVGDutraPanel.Height);
                graficoEOVGLavrasPanel.Size = new Size(tamanho, graficoEOVGLavrasPanel.Height);
                graficoABCPanel.Size = new Size(tamanho, graficoABCPanel.Height);
                graficoRapidoPanel.Size = new Size(tamanho, graficoRapidoPanel.Height);
                graficoCisnePanel.Size = new Size(tamanho, graficoCisnePanel.Height);
                graficoNiffPanel.Size = new Size(tamanho, graficoNiffPanel.Height);
                graficoArujaPanel.Size = new Size(tamanho, graficoArujaPanel.Height);
                graficoCampibusPanel.Size = new Size(tamanho, graficoCampibusPanel.Height);

                graficosNotasPanel.Refresh();
            }
            catch { }

            _andamentoAvaliacaoEmPesquisa = false;
            mensagemAndamentoAvaliacaoLabel.Text = "";
            andamentoAvaliacaoTimer.Start();
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

            graficosNotasPanel.Controls.Clear();

            ChartSeries _serieNotas;
            ChartControl _graficoNotas;
            int top = 5;
            int left = 3;
            int i = 1;


            foreach (var item in _listaAvaliacoesNotas.GroupBy(g => new { g.IdColaborador, g.Colaborador, g.Empresa, g.IdEmpresa })
                                                      .Select(s => new { IdColaborador = s.Key.IdColaborador,
                                                                         Colaborador = s.Key.Colaborador, Empresa = s.Key.Empresa,
                                                                         IdEmpresa = s.Key.IdEmpresa })
                                                      .OrderBy(o => o.Colaborador))
            {
                if (idEmpresa != 0 && idEmpresa != item.IdEmpresa)
                    continue;


                _quantidadeGraficos++;

                _graficoNotas = new ChartControl();
                _graficoNotas.Name = "grafico" + _quantidadeGraficos.ToString();
                _graficoNotas.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
                _graficoNotas.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(tipoAvaliacaoComboBox.BackColor);
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

                _graficoNotas.Title.Font = tipoAvaliacaoComboBox.Font;
                _graficoNotas.Series.Add(_serieNotas);

                _graficoNotas.Series[0].Style.Callout.Enable = true;
                _graficoNotas.Series[0].Style.Callout.DisplayTextAndFormat = "{2}";
                _graficoNotas.Series[0].Style.Callout.Position = LabelPosition.Top;

                graficosNotasPanel.Controls.Add(_graficoNotas);
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

        private void andamentoAvaliacaoTimer_Tick(object sender, EventArgs e)
        {
            if (Publicas._usuario == null || (!Publicas._usuario.AcessaAvaliacaoDesempenho || (!Publicas._usuario.AcessoDeRH && !Publicas._usuario.AcessoDeGestor)))
            {
                andamentoAvaliacaoTimer.Stop();
                return;
            }

            andamentoAvaliacaoTimer.Stop();

            try
            {
                idSuperior = 0;

                if (Publicas._usuario.AcessoDeGestor && !Publicas._usuario.AcessoDeRH)
                    idSuperior = _colaboradores.Id;

                tipoAvaliacao = (tipoAvaliacaoComboBox.SelectedIndex == 0 ? "AA" : // AutoAvaliacao
                                      (tipoAvaliacaoComboBox.SelectedIndex == 1 ? "FG" : // Feedback Gestor
                                      (tipoAvaliacaoComboBox.SelectedIndex == 2 ? "MN" : // Metas Numericas
                                      (tipoAvaliacaoComboBox.SelectedIndex == 3 ? "AG" : // Avaliação Gestos
                                      (tipoAvaliacaoComboBox.SelectedIndex == 4 ? "AR" : // Avaliação RH
                                      (tipoAvaliacaoComboBox.SelectedIndex == 5 ? "FA" : // FeedBack Avaliado
                                      "PD" // Plano de desenvolvimento
                                      ))))));

                if (!_andamentoAvaliacaoEmPesquisa)
                {
                    andamentoAvaliacaoBackgroundWorker.RunWorkerAsync();
                    mensagemAndamentoAvaliacaoLabel.Text = "Pesquisando andamento das avaliações, aguarde... ";
                }
            }
            catch { }
        }

        private void andamentoButton_Click(object sender, EventArgs e)
        {
            avaliacaoEmpresasPanel.BringToFront();
            avaliacaoEmpresasPanel.Dock = DockStyle.Fill;
            avaliacaoEmpresasPanel.Visible = true;
            avaliacoesNotasPanel.Visible = false;
            rankingPanel.Visible = false;
            andamentoButton.BackColor = Publicas._botao;
            notasButton.BackColor = tipoAvaliacaoComboBox.BackColor;
            rankingButton.BackColor = tipoAvaliacaoComboBox.BackColor;

            _mudouVisualizacaoAndamento = true;
        }

        private void notasButton_Click(object sender, EventArgs e)
        {
            avaliacoesNotasPanel.BringToFront();
            avaliacoesNotasPanel.Dock = DockStyle.Fill;
            avaliacoesNotasPanel.Visible = true;
            avaliacaoEmpresasPanel.Visible = false;
            rankingPanel.Visible = false;
            andamentoButton.BackColor = tipoAvaliacaoComboBox.BackColor;
            notasButton.BackColor = Publicas._botao;
            rankingButton.BackColor = tipoAvaliacaoComboBox.BackColor;

            tipoAvaliacaoComboBox_SelectedIndexChanged(sender, e);

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

        private void rankingButton_Click(object sender, EventArgs e)
        {
            rankingPanel.BringToFront();
            rankingPanel.Dock = DockStyle.Fill;
            avaliacaoEmpresasPanel.Visible = false;
            avaliacoesNotasPanel.Visible = false;
            rankingPanel.Visible = true;
            rankingButton.BackColor = Publicas._botao;
            notasButton.BackColor = tipoAvaliacaoComboBox.BackColor;
            andamentoButton.BackColor = tipoAvaliacaoComboBox.BackColor;

            _mudouVisualizacaoAndamento = true;
        }

        private void bancoHorasGridGroupingControl_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            Record dr;
            try
            { // buscar da empresa do usuario
                if (e.TableCellIdentity.RowIndex != -1)
                {
                    GridRecordRow rec = this.bancoHorasGridGroupingControl.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    //if (rec != null)
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

        private void pontuacaoNineBoxPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;

            if (!menuNineBoxPanel.Visible)
            {
                menuNineBoxPanel.Left = _leftSubMenus2 + 410;
                menuNineBoxPanel.Top = 635;
                menuNineBoxPanel.BringToFront();
                menuNineBoxPanel.Visible = true;
            }

            pontuacaoNineBoxLabel.Font = new Font(gestorLabel.Font, FontStyle.Bold);
            pontuacaoNineBoxPanel.BackColor = Color.Silver;
        }

        private void chamadosGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {

        }

        private void dashBoardTabControl_Enter(object sender, EventArgs e)
        {
            menuNineBoxPanel.Visible = false;
        }

        private void chamadosGridGroupingControl_Enter(object sender, EventArgs e)
        {
            menuNineBoxPanel.Visible = false;
        }

        private void cargosNineBoxPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.EixoYNineBox _tela = new Avaliacao_de_desempenho.EixoYNineBox();

            _tela.cargosPanel.Visible = true;
            _tela.colaboradorPanel.Visible = false;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void colaboradoresNineBoxPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.EixoYNineBox _tela = new Avaliacao_de_desempenho.EixoYNineBox();

            _tela.cargosPanel.Visible = false;
            _tela.colaboradorPanel.Visible = true;
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void dataHoraLabel_Click(object sender, EventArgs e)
        {
           
        }

        private void bibliotecaPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuAvulsoPanel.Visible = false;
            menuCadastroPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuLimpezaPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuJuridicoPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuNineBoxPanel.Visible = false;
            menuAvaliacaoDesempenhoPanel.Visible = false;

            if (!menuBibliotecaPanel.Visible)
            {
                menuBibliotecaPanel.Left = _leftSubMenus;
                menuBibliotecaPanel.Top = (menuPanel.Width != 206 ? 322 : 347);
                menuBibliotecaPanel.BringToFront();
                menuBibliotecaPanel.Visible = true;
            }

            tituloBibliotecaPanel.Visible = (menuPanel.Width != 206);

            bibliotecaLabel.Font = new Font(avaliacaoDesempenhoLabel.Font, FontStyle.Bold);
            bibliotecaPanel.BackColor = Color.Silver;
        }

        private void cadastroLivrosPictureBox_MouseHover(object sender, EventArgs e)
        {

        }

        private void categoriaLivrosPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Categorias _tela = new Biblioteca.Categorias();

            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void notasRHPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaAvaliacao;
            Avaliacao_de_desempenho.Notas _tela = new Avaliacao_de_desempenho.Notas();

            //_tela.cargosPanel.Visible = false;
            //_tela.colaboradorPanel.Visible = true;
            _tela.Size = new Size(this.Width-5,  this.Height - (tituloPanel.Height + panel2.Height +2));
            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void pararAtualizacaoPictureBox_Click(object sender, EventArgs e)
        {
            _parouAtualizacao = !_parouAtualizacao;

            if (!_parouAtualizacao)
                AtivaTimer(sender, e);
            else
            {
                ParaTimer();
                chamadoTimer.Stop();
            }
        }

        private void pararAtualizacaoPictureBox_MouseHover(object sender, EventArgs e)
        {
            pararAtualizacaoPictureBox.Cursor = Cursors.Hand;
        }

        private void pararAtualizacaoPictureBox_MouseLeave(object sender, EventArgs e)
        {
            pararAtualizacaoPictureBox.Cursor = Cursors.Default;
        }

        private void SelecoesLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBolaoCopa;
            BolaoCopadoMundo.Selecoes _tela = new BolaoCopadoMundo.Selecoes();

            MenuBolaoCopaMundo.Visible = false;
            _tela.ShowDialog();
            Refresh();
            AtivaTimer(sender, e);
        }

        private void BolaoCopaPanel_MouseHover(object sender, EventArgs e)
        {
            BolaoCopaPanel.Cursor = Cursors.Hand;
        }

        private void BolaoCopaPanel_MouseLeave(object sender, EventArgs e)
        {
            BolaoCopaPanel.Cursor = Cursors.Default;
        }

        private void BolaoCopaPanel_Click(object sender, EventArgs e)
        {
            //escondeAbreChatPictureBox.Image = Properties.Resources.SetaUpBranca;
            chatPanel.Location = new Point(chatPanel.Location.X, panel2.Location.Y - 25);
            chatPanel.Size = new Size(chatPanel.Width, 25);
            MenuBolaoCopaMundo.Location = new Point(BolaoCopaPanel.Left, BolaoCopaPanel.Height);
            MenuBolaoCopaMundo.BringToFront();
            MenuBolaoCopaMundo.Visible = true;
            menuUsuarioTimer.Start();
        }

        private void JogosLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBolaoCopa;
            BolaoCopadoMundo.Jogos _tela = new BolaoCopadoMundo.Jogos();

            MenuBolaoCopaMundo.Visible = false;
            _tela.ShowDialog();
            Refresh();
            AtivaTimer(sender, e);
        }

        private void PapiteFinalLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBolaoCopa;
            BolaoCopadoMundo.PalpiteFinal _tela = new BolaoCopadoMundo.PalpiteFinal();

            _tela.ShowDialog();
            MenuBolaoCopaMundo.Visible = false;
            Refresh();
            AtivaTimer(sender, e);
        }

        private void PapitePlacarLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBolaoCopa;
            BolaoCopadoMundo.PalpiteJogos _tela = new BolaoCopadoMundo.PalpiteJogos();

            MenuBolaoCopaMundo.Visible = false;
            _tela.ShowDialog();
            Refresh();
            AtivaTimer(sender, e);
        }

        private void ResultadosLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBolaoCopa;
            BolaoCopadoMundo.ResultadosJogos _tela = new BolaoCopadoMundo.ResultadosJogos();

            MenuBolaoCopaMundo.Visible = false;
            _tela.ShowDialog();
            Refresh();
            AtivaTimer(sender, e);
        }

        private void SelecoesLabel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();

            if (((Control)sender).Name.Contains("Selecoes"))
            {
                menuNineBoxPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                SelecoesLabel.Font = new Font(SelecoesLabel.Font, FontStyle.Bold);
                SelecoesPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("Resultado"))
            {
                menuNineBoxPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                ResultadoJogosLabel.Font = new Font(ResultadoJogosLabel.Font, FontStyle.Bold);
                ResultadoJogosPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("Jogos"))
            {
                menuNineBoxPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                JogosLabel.Font = new Font(JogosLabel.Font, FontStyle.Bold);
                JogosPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("PalpitePlacar"))
            {
                menuNineBoxPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                PalpitePlacarLabel.Font = new Font(PalpitePlacarLabel.Font, FontStyle.Bold);
                PalpitePlacarPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("PalpiteFinal"))
            {
                menuNineBoxPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                PalpiteFinalLabel.Font = new Font(PalpiteFinalLabel.Font, FontStyle.Bold);
                PalpiteFinalPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("RankingBolao"))
            {
                menuNineBoxPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                RankingBolaoLabel.Font = new Font(RankingBolaoLabel.Font, FontStyle.Bold);
                RankingBolaoPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("ValorArrecadado"))
            {
                menuNineBoxPanel.Visible = false;
                menuSACPanel.Visible = false;
                menuSistemasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuCadastroJuridicoPanel.Visible = false;
                menuCorridasPanel.Visible = false;
                menuCadastroAvaliacaoPanel.Visible = false;
                menuRHAvaliacaoPanel.Visible = false;
                menuColaboradorAvaliacaoPanel.Visible = false;
                menuGestorPanel.Visible = false;
                menuControladoriaAvaliacaoPanel.Visible = false;

                ValorArrecadadoLabel.Font = new Font(ValorArrecadadoLabel.Font, FontStyle.Bold);
                ValorArrecadadoPanel.BackColor = Color.Silver;
                return;
            }
        }

        private void BolaoCopaBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!_notificacaoBolaoEmPesquisa)
            {
                _notificacaoBolaoEmPesquisa = true;
                _listaJogosEncerrados = new BolaoJogosBO().Listar(DateTime.Now.Year, true);
                _listaJogosDos3Dias = new BolaoJogosBO().ListarJogosSemPalpites(DateTime.Now.Year);
            }            
        }

        private void BolaoCopaBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                int topbolao = _topNotificacaoPequena - (aniversarioPanel.Height + 5);

                if (BancoHorasPanel.Visible)
                    topbolao = BancoHorasPanel.Top - (aniversarioPanel.Height + 5);

                int qtd = 1;
                int qtdDias = 1;                

                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                    qtdDias = 3;

                Publicas._fecharNotificacaoBolao = true;

                // Jogos encerrados 
                foreach (var item in _listaJogosEncerrados.Where(w => w.Data.Date >= DateTime.Now.Date.AddDays(-qtdDias))
                                                          .OrderBy(o => o.Data))
                {

                    if (qtd > 5)
                        break;

                    if (!NotificacaoCopaPanel.Visible)
                    {
                        NotificacaoCopaPanel.Location = new Point(aniversarioPanel.Left, topbolao);
                        tituloNotificacaoCopaLabel.Text = "Partida encerrada ";
                        DataHoraPartida.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString();
                        Time1Label.Text = item.Nome1 + "  " + item.Placar1;
                        Time2Label.Text = item.Placar2 + "  " + item.Nome2;

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            Bandeira2PictureBox.Refresh();
                        }
                        catch { }

                        NotificacaoCopaPanel.BringToFront();
                        NotificacaoCopaPanel.Visible = true;
                    }
                    else
                    {
                        Panel panel = new Panel();
                        panel.Name = "NotificacaoCopaPanel" + qtd.ToString();

                        Panel panelTitulo = new Panel();
                        Label labelTitulo = new Label();
                        Label labelData = new Label();
                        Label labelTime1 = new Label();
                        Label labelTime2 = new Label();
                        Label labelx = new Label();
                        PictureBox imagem = new PictureBox();
                        PictureBox bandeira1 = new PictureBox();
                        PictureBox bandeira2 = new PictureBox();

                        imagem.Click += new System.EventHandler(this.powerPictureBox_Click_1);

                        imagem.Size = powerPictureBox.Size;
                        imagem.Image = powerPictureBox.Image;
                        imagem.SizeMode = PictureBoxSizeMode.StretchImage;

                        bandeira1.Size = Bandeira1PictureBox.Size;
                        bandeira1.Image = Bandeira1PictureBox.Image;
                        bandeira1.SizeMode = Bandeira1PictureBox.SizeMode;
                        bandeira1.Location = Bandeira1PictureBox.Location;

                        bandeira2.Size = Bandeira2PictureBox.Size;
                        bandeira2.Image = Bandeira2PictureBox.Image;
                        bandeira2.SizeMode = Bandeira2PictureBox.SizeMode;
                        bandeira2.Location = Bandeira2PictureBox.Location;

                        panel.Size = NotificacaoCopaPanel.Size;
                        panel.Location = new Point(aniversarioPanel.Left, topbolao);
                        panelTitulo.Size = tituloNotificacaoCopaPanel.Size;
                        panelTitulo.BackColor = tituloNotificacaoCopaLabel.BackColor;

                        panel.Controls.Add(panelTitulo);
                        panel.Controls.Add(bandeira1);
                        panel.Controls.Add(bandeira2);

                        panelTitulo.Dock = DockStyle.Top;
                        panelTitulo.Controls.Add(labelTitulo);
                        panelTitulo.Controls.Add(imagem);
                        imagem.Dock = DockStyle.Right;

                        labelTitulo.Dock = DockStyle.Fill;
                        labelTitulo.ForeColor = tituloDataLabel.ForeColor;
                        labelTitulo.Font = tituloDataLabel.Font;
                        labelTitulo.TextAlign = ContentAlignment.MiddleLeft;

                        labelData.Location = DataHoraPartida.Location;
                        labelData.AutoSize = false;
                        labelData.TextAlign = DataHoraPartida.TextAlign;
                        labelData.Size = DataHoraPartida.Size;
                        labelData.ForeColor = DataHoraPartida.ForeColor;
                        labelData.Font = DataHoraPartida.Font;

                        labelTime1.Location = Time1Label.Location;
                        labelTime1.AutoSize = false;
                        labelTime1.ForeColor = Time1Label.ForeColor;
                        labelTime1.Font = Time1Label.Font;
                        labelTime1.Size = Time1Label.Size;
                        labelTime1.TextAlign = Time1Label.TextAlign;

                        labelTime2.Location = Time2Label.Location;
                        labelTime2.AutoSize = false;
                        labelTime2.Size = Time2Label.Size;
                        labelTime2.TextAlign = Time2Label.TextAlign;
                        labelTime2.ForeColor = Time2Label.ForeColor;
                        labelTime2.Font = Time2Label.Font;

                        labelx.Location = xLabel.Location;
                        labelx.AutoSize = false;
                        labelx.Size = xLabel.Size;
                        labelx.TextAlign = xLabel.TextAlign;
                        labelx.ForeColor = xLabel.ForeColor;
                        labelx.Font = xLabel.Font;
                        labelx.Text = "X";

                        panel.Controls.Add(labelData);
                        panel.Controls.Add(labelTime1);
                        panel.Controls.Add(labelTime2);
                        panel.Controls.Add(labelx);

                        labelTitulo.Text = tituloNotificacaoCopaLabel.Text;
                        labelData.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString();
                        labelTime1.Text = item.Nome1 + "  " + item.Placar1;
                        labelTime2.Text = item.Placar2 + "  " + item.Nome2; 

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                bandeira1.Image = new Bitmap(mStream);
                            }

                            bandeira1.SizeMode = PictureBoxSizeMode.StretchImage;
                            bandeira1.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            Bandeira2PictureBox.Refresh();
                        }
                        catch { }

                        panel.Visible = true;
                        this.Controls.Add(panel);

                        panel.BringToFront();
                    }

                    topbolao = topbolao - (aniversarioPanel.Height + 5);

                    BolaoNotificacao _not = new BolaoNotificacao();
                    _not.IdJogo = item.Id;
                    _not.IdColaborador = Publicas._idColaborador;

                    new BolaoNotificacaoBO().Gravar(_not);
                    Refresh();
                    qtd++;

                }


                // Jogos sem Palpites
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                    qtdDias = 3;

                //foreach (var item in _listaJogosDos3Dias.Where(w => w.Data >= _data && w.Data.Date <= _data.Date.AddDays(qtdDias))
                foreach (var item in _listaJogosDos3Dias.Where(w => w.Data.Date >= DateTime.Now && w.Data.Date <= DateTime.Now.Date.AddDays(qtdDias))
                                                         .OrderBy(o => o.Data))
                {

                    if (qtd > 6)
                        break;

                    if (!NotificacaoCopaPanel.Visible)
                    {
                        NotificacaoCopaPanel.Location = new Point(aniversarioPanel.Left, topbolao);
                        tituloNotificacaoCopaLabel.Text = "Você não definiu o placar do jogo ";
                        DataHoraPartida.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString();
                        Time1Label.Text = item.Nome1 + "  " + item.Placar1;
                        Time2Label.Text = item.Placar2 + "  " + item.Nome2;

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            Bandeira2PictureBox.Refresh();
                        }
                        catch { }

                        NotificacaoCopaPanel.BringToFront();
                        NotificacaoCopaPanel.Visible = true;
                    }
                    else
                    {
                        Panel panel = new Panel();
                        panel.Name = "NotificacaoCopaPanel" + qtd.ToString();

                        Panel panelTitulo = new Panel();
                        Label labelTitulo = new Label();
                        Label labelData = new Label();
                        Label labelTime1 = new Label();
                        Label labelTime2 = new Label();
                        Label labelx = new Label();
                        PictureBox imagem = new PictureBox();
                        PictureBox bandeira1 = new PictureBox();
                        PictureBox bandeira2 = new PictureBox();

                        imagem.Click += new System.EventHandler(this.powerPictureBox_Click_1);

                        imagem.Size = powerPictureBox.Size;
                        imagem.Image = powerPictureBox.Image;
                        imagem.SizeMode = PictureBoxSizeMode.StretchImage;

                        bandeira1.Size = Bandeira1PictureBox.Size;
                        bandeira1.Image = Bandeira1PictureBox.Image;
                        bandeira1.SizeMode = Bandeira1PictureBox.SizeMode;
                        bandeira1.Location = Bandeira1PictureBox.Location;

                        bandeira2.Size = Bandeira2PictureBox.Size;
                        bandeira2.Image = Bandeira2PictureBox.Image;
                        bandeira2.SizeMode = Bandeira2PictureBox.SizeMode;
                        bandeira2.Location = Bandeira2PictureBox.Location;

                        panel.Size = NotificacaoCopaPanel.Size;
                        panel.Location = new Point(aniversarioPanel.Left, topbolao);
                        panelTitulo.Size = tituloNotificacaoCopaPanel.Size;
                        panelTitulo.BackColor = tituloNotificacaoCopaLabel.BackColor;

                        panel.Controls.Add(panelTitulo);
                        panel.Controls.Add(bandeira1);
                        panel.Controls.Add(bandeira2);

                        panelTitulo.Dock = DockStyle.Top;
                        panelTitulo.Controls.Add(labelTitulo);
                        panelTitulo.Controls.Add(imagem);
                        imagem.Dock = DockStyle.Right;

                        labelTitulo.Dock = DockStyle.Fill;
                        labelTitulo.ForeColor = tituloDataLabel.ForeColor;
                        labelTitulo.Font = tituloDataLabel.Font;
                        labelTitulo.TextAlign = ContentAlignment.MiddleLeft;

                        labelData.Location = DataHoraPartida.Location;
                        labelData.AutoSize = false;
                        labelData.TextAlign = DataHoraPartida.TextAlign;
                        labelData.Size = DataHoraPartida.Size;
                        labelData.ForeColor = DataHoraPartida.ForeColor;
                        labelData.Font = DataHoraPartida.Font;

                        labelTime1.Location = Time1Label.Location;
                        labelTime1.AutoSize = false;
                        labelTime1.ForeColor = Time1Label.ForeColor;
                        labelTime1.Font = Time1Label.Font;
                        labelTime1.Size = Time1Label.Size;
                        labelTime1.TextAlign = Time1Label.TextAlign;

                        labelTime2.Location = Time2Label.Location;
                        labelTime2.AutoSize = false;
                        labelTime2.Size = Time2Label.Size;
                        labelTime2.TextAlign = Time2Label.TextAlign;
                        labelTime2.ForeColor = Time2Label.ForeColor;
                        labelTime2.Font = Time2Label.Font;

                        labelx.Location = xLabel.Location;
                        labelx.AutoSize = false;
                        labelx.Size = xLabel.Size;
                        labelx.TextAlign = xLabel.TextAlign;
                        labelx.ForeColor = xLabel.ForeColor;
                        labelx.Font = xLabel.Font;
                        labelx.Text = "X";

                        panel.Controls.Add(labelData);
                        panel.Controls.Add(labelTime1);
                        panel.Controls.Add(labelTime2);
                        panel.Controls.Add(labelx);

                        labelTitulo.Text = tituloNotificacaoCopaLabel.Text;
                        labelData.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString();
                        labelTime1.Text = item.Nome1 + "  " + item.Placar1;
                        labelTime2.Text = item.Placar2 + "  " + item.Nome2;

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                bandeira1.Image = new Bitmap(mStream);
                            }

                            bandeira1.SizeMode = PictureBoxSizeMode.StretchImage;
                            bandeira1.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            Bandeira2PictureBox.Refresh();
                        }
                        catch { }

                        panel.Visible = true;
                        this.Controls.Add(panel);

                        panel.BringToFront();
                    }

                    topbolao = topbolao - (aniversarioPanel.Height + 5);

                    BolaoNotificacao _not = new BolaoNotificacao();
                    _not.IdJogo = item.Id;
                    _not.IdColaborador = Publicas._idColaborador;

                    new BolaoNotificacaoBO().Gravar(_not);
                    Refresh();
                    qtd++;

                }


                fecharNotificacaoBolaoTimer.Start();

                _notificacaoBolaoEmPesquisa = false;
            }
            catch { }
        }

        private void fecharNotificacaoBolaoTimer_Tick(object sender, EventArgs e)
        {
            Control[] controle;
            string nome;

            fecharNotificacaoComunicadoTimer.Stop();
            if (Publicas._fecharNotificacaoBolao)
            {
                for (int i = 1; i < 7; i++)
                {
                    nome = "NotificacaoCopaPanel" + (i == 1 ? "" : i.ToString());

                    controle = this.Controls.Find(nome, true);

                    if (i == 1)
                        controle[0].Visible = false; // o primeiro não é criado em tempo de execução por isso fica invisivel
                    else
                    {
                        try
                        {
                            controle[0].Dispose();
                        }
                        catch { }
                    }
                }
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

        private void RankingLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBolaoCopa;
            BolaoCopadoMundo.Ranking _tela = new BolaoCopadoMundo.Ranking();

            MenuBolaoCopaMundo.Visible = false;
            _tela.ShowDialog();
            Refresh();
            AtivaTimer(sender, e);
        }

        private void ValorArrecadadoLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBolaoCopa;
            BolaoCopadoMundo.ValorArrecadado _tela = new BolaoCopadoMundo.ValorArrecadado();

            MenuBolaoCopaMundo.Visible = false;
            _tela.ShowDialog();
            Refresh();
            AtivaTimer(sender, e);
        }

        private void EmprestimoLivrosPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();

            menuNineBoxPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;
            BolaoCopaPanel.Visible = false;
            cadastroBibliotecaPanel.Visible = false;

            if (((Control)sender).Name.Contains("EmprestimoLivros"))
            {
                EmprestimoLivrosLabel.Font = new Font(EmprestimoLivrosLabel.Font, FontStyle.Bold);
                EmprestimoLivrosPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("ReservaLivros"))
            {
                ReservaLivrosLabel.Font = new Font(ReservaLivrosLabel.Font, FontStyle.Bold);
                ReservaLivrosPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("DevolucaoLivros"))
            {
                DevolucaoLivrosLabel.Font = new Font(DevolucaoLivrosLabel.Font, FontStyle.Bold);
                DevolucaoLivrosPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("PerguntasLivros"))
            {
                PerguntasLivrosLabel.Font = new Font(PerguntasLivrosLabel.Font, FontStyle.Bold);
                PerguntasLivrosPanel.BackColor = Color.Silver;
                return;
            }
        }

        private void cadastroLivrosPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();
            menuNineBoxPanel.Visible = false;
            menuSACPanel.Visible = false;
            menuSistemasPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuCadastroJuridicoPanel.Visible = false;
            menuCorridasPanel.Visible = false;
            menuCadastroAvaliacaoPanel.Visible = false;
            menuRHAvaliacaoPanel.Visible = false;
            menuColaboradorAvaliacaoPanel.Visible = false;
            menuGestorPanel.Visible = false;
            menuControladoriaAvaliacaoPanel.Visible = false;
            BolaoCopaPanel.Visible = false;

            if (!cadastroBibliotecaPanel.Visible)
            {
                cadastroBibliotecaPanel.Left = _leftSubMenus2;
                cadastroBibliotecaPanel.Top = menuBibliotecaPanel.Top;
                cadastroBibliotecaPanel.BringToFront();
                cadastroBibliotecaPanel.Visible = true;
            }

            cadastroLivrosLabel.Font = new Font(cadastroLivrosLabel.Font, FontStyle.Bold);
            cadastroLivrosPanel.BackColor = Color.Silver;
        }

        private void PontuacaoLivrosPanel_MouseHover(object sender, EventArgs e)
        {
            SelecaoMenuCorSemSerSelecionada();

            if (((Control)sender).Name.Contains("categoriaLivros"))
            {
                categoriaLivrosLabel.Font = new Font(categoriaLivrosLabel.Font, FontStyle.Bold);
                categoriaLivrosPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("PontuacaoLivros"))
            {
                PontuacaoLivrosLabel.Font = new Font(PontuacaoLivrosLabel.Font, FontStyle.Bold);
                PontuacaoLivrosPanel.BackColor = Color.Silver;
                return;
            }

            if (((Control)sender).Name.Contains("Livros"))
            {
                LivrosLabel.Font = new Font(LivrosLabel.Font, FontStyle.Bold);
                LivrosPanel.BackColor = Color.Silver;
                return;
            }
        }

        private void LivrosPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Livros _tela = new Biblioteca.Livros();

            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void EmprestimoLivrosPanel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Emprestimos _tela = new Biblioteca.Emprestimos();

            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void DevolucaoLivrosLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Devolucao _tela = new Biblioteca.Devolucao();

            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void ReservaLivrosLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Reserva _tela = new Biblioteca.Reserva();

            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void AcompanhamentoLivrosLabel_Click(object sender, EventArgs e)
        {
            EscondeMenus();

            tituloSistemaLabel.Text = Publicas._nomeDoSistema + " - " + Publicas._nomeSubSistemaBiblioteca;
            Biblioteca.Acompanhamento _tela = new Biblioteca.Acompanhamento();

            _tela.ShowDialog();
            AtivaTimer(sender, e);
        }

        private void FundoPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void dashBoardTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxExt2_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusComunidadosComboBoxAdv_Click(object sender, EventArgs e)
        {

        }
    }
}
