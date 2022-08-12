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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Suportte.Chamados
{
    public partial class Tramites : Form
    {
        public Tramites()
        {
            InitializeComponent();

            dataAberturaDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataAberturaDateTimePicker.BackColor = nomeUsuarioTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    dataAberturaDateTimePicker.Style = VisualStyle.Office2016Black;
                    concluirToggleButton.VisualStyle = ToggleButtonStyle.Office2016Black;

                    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl1.BackColor = Publicas._panelTitulo;
                }
            }
            TituloTempoEstimadoPanel.BackColor = historicoPanel.BackColor;
            Publicas._mensagemSistema = string.Empty;
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

        List<Empresa> _listaEmpresas;
        List<Categoria> _listaCategorias;
        List<Modulo> _listaModulos;
        List<Tela> _listaTelas;
        Parametro _parametro;
        List<Usuario> _listaUsuarios;
        List<HistoricoDoChamado> _historicos;
        List<HistoricoDoChamado> _ultimoHistoricos;
        List<HistoricoDoChamado> _listaExibidos = new List<HistoricoDoChamado>();
        List<AnexoDoHistorico> _listaAnexos;
        List<Classes.TempoExecucao> _listaTemposExecucao; 
        Usuario _usuario;
        Usuario _usuarioAutorizador;
        Usuario _usuarioConvidado;
        Chamado _chamado;
        Tela _tela;
        string[] arquivo;
        PictureBox foto = new PictureBox();

        int _idModulo = 0;
        int _idCategoria = 0;
        int _idTela = 0;
        int _idEmpresa = 0;
        int _posicaoChat = 6;
        int _heigthMinimo = 105;
        int _topArquivos = 33;
        int _leftArquivos = 6;
        int _qtd = 0;
        int _qtdleft = 0;


        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Tramites_Shown(object sender, EventArgs e)
        {
            focotextBox1.Focus();
            gridGroupingControl1.DataSource = new List<Classes.ControleDeHoras>();
            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl1.TableControl.CellToolTip.Active = true;

            if (!Publicas._TemaBlack)
            {
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

                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
                this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 20;

            StyledRenderer styleRenderer1 = new StyledRenderer();
           // concluirToggleButton.Renderer = styleRenderer1;
           // AutorizadoToggleButton.Renderer = styleRenderer1;
           // AguardarToggleButton.Renderer = styleRenderer1;

            concluirToggleButton.ActiveState.Text = "SIM";
            concluirToggleButton.InactiveState.Text = "NÃO";

            AutorizadoToggleButton.ActiveState.Text = "SIM";
            AutorizadoToggleButton.InactiveState.Text = "NÃO";

            AguardarToggleButton.ActiveState.Text = "SIM";
            AguardarToggleButton.InactiveState.Text = "NÃO";

            // Trazer as empresas que o usuário esta autorizado.
            _listaEmpresas = new EmpresaBO().Listar();
            empresaComboBox.DataSource = _listaEmpresas.Where(w => w.Ativo)
                                                       .OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBox.DisplayMember = "CodigoeNome";

            empresaComboBox.Enabled = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente;
            SolicitarAutorizacaoCheckBox.Visible = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente;

            // Trazer as categorias que o usuário está autorizado
            _listaCategorias = new CategoriaBO().Listar();
            categoriaComboBox.DataSource = _listaCategorias.Where(w => w.Ativo)
                                                           .OrderBy(o => o.Descricao).ToList();
            categoriaComboBox.DisplayMember = "Descricao";

            tipoAdequacaoComboBox.Items.AddRange(new object[] { "SIM", "PSE", "Pedido", "Ticket", "Outros" });
            tipoAdequacaoComboBox.SelectedIndex = 0;

            statusComboBox.Items.AddRange(new object[] { "Em Andamento", "Aguardando Solicitante", "Aguardando retorno de terceiro",
                "Cancelado", "Reaberto", "Em Desenvolvimento","Finalizado", "Aguardando Autorização", "Aguardando Cronograma", "Aguardando Conserto" });
            statusComboBox.SelectedIndex = 0;

            _parametro = new ParametrosBO().Consultar();

            // trazer o Tipo De chamados.
            tipoChamadoComboBox.Items.AddRange(new object[] { "Erro", "Dúvidas", "Implementação", "Acesso", "Ajustes","Projeto","Solicitação" });
            tipoChamadoComboBox.SelectedIndex = 1;

            // trazer a prioridades
            prioridadeComboBox.Items.AddRange(new object[] { "Crítico", "Alta", "Media", "Baixa" });
            prioridadeComboBox.SelectedIndex = 2;

            // trazer a origem
            origemComboBox.Items.AddRange(new object[] { "Chamado", "E-mail", "Telefone" });
            origemComboBox.SelectedIndex = 0;
            origemComboBox.ReadOnly = Publicas._usuario.Tipo != Publicas.TipoUsuario.Atendente;

            // carregar dados
            _chamado = new ChamadoBO().Consulta(Publicas._idChamado);
            _listaModulos = new ModuloBO().Listar(_chamado.IdCategoria);

            modulosComboBox.DataSource = _listaModulos.Where(w => w.Ativo)
                                                      .OrderBy(o => o.Nome).ToList();
            modulosComboBox.DisplayMember = "Nome";

            _tela = new TelaBO().Consultar(_chamado.IdTela);

            _listaTelas = new TelaBO().Listar(_tela.IdModulo);
            telasComboBox.DataSource = _listaTelas.Where(w => w.Ativo)
                                                  .OrderBy(o => o.NomeCompleto).ToList();
            telasComboBox.DisplayMember = "NomeCompleto";

            PrivadoCheckBox.Visible = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente;
            SolicitarAcompanhamentoCheckBox.Visible = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && _chamado.IdUsuarioAcompanhamento == 0;

            #region dados solicitante
            _usuario = new UsuarioBO().ConsultarPorId(_chamado.IdUsuario);
            nomeUsuarioTextBox.Text = _usuario.Nome;
            nomeMaquinaTextBox.Text = _usuario.NomeMaquina;
            departamentoTextBox.Text = _usuario.Departamento;
            telefoneTextBox.Text = _usuario.Telefone.ToString();
            ramalTextBox.Text = _usuario.Ramal.ToString();
            emailTextBox.Text = _usuario.Email;
            #endregion

            dataAberturaDateTimePicker.Value = _chamado.Data;
            assuntoTextBox.Text = _chamado.Assunto;
            _idCategoria = _chamado.IdCategoria;
            _idEmpresa = _chamado.IdEmpresa;
            numeroTextBoxExt.Text = _chamado.Numero;
            assuntoTextBox.Enabled = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente;

            _listaTemposExecucao = new ChamadoBO().Temporizador(_chamado.IdChamado);

            TituloTempoEstimadoPanel.Visible = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && Publicas._usuario.Id != _chamado.IdUsuario;
            EntradaMaskedEditBox.Enabled = _chamado.Status == Publicas.StatusChamado.Novo;
            DateTime _dataEstimada = DateTime.MinValue.AddMinutes(_chamado.MinutosEstimados);
            EntradaMaskedEditBox.Text = (((_dataEstimada.Day-1) * 24) + _dataEstimada.Hour).ToString("00") + ":" + _dataEstimada.Minute.ToString("00");

            try
            {
                int total = _listaTemposExecucao.Sum(s => s.Minutos);

                if (DateTime.MinValue.AddMinutes(total).Day == 1)
                    HorasUtilizadaslabel.Text = "Horas Utilizadas " + DateTime.MinValue.AddMinutes(total).ToShortTimeString();
                else
                {
                    HorasUtilizadaslabel.Text = "Horas Utilizadas " + ((24 * ((DateTime.MinValue.AddMinutes(total)).Day - 1)) + DateTime.MinValue.AddMinutes(total).Hour).ToString() + ":" + DateTime.MinValue.AddMinutes(total).Minute;
                }
            }
            catch { }


            if (_chamado.PrazoDesenvolvimento != 0)
                PrazoEntregaTextBox.Text = _chamado.PrazoDesenvolvimento.ToString();
            
            if (_chamado.Status == Publicas.StatusChamado.Finalizado)
                statusComboBox.SelectedIndex = 6;
            else
            statusComboBox.SelectedIndex = (_chamado.Status == Publicas.StatusChamado.EmAndamento || _chamado.Status == Publicas.StatusChamado.Novo ? 0 :
                (_chamado.Status == Publicas.StatusChamado.Pendente ? 1 :
                (_chamado.Status == Publicas.StatusChamado.Adequacao ? 2 :
                (_chamado.Status == Publicas.StatusChamado.Cancelado ? 3 :
                (_chamado.Status == Publicas.StatusChamado.Reaberto ? 4 :
                (_chamado.Status == Publicas.StatusChamado.EmDesenvolvimento ? 5 :
                (_chamado.Status == Publicas.StatusChamado.Finalizado ? 6 :
                (_chamado.Status == Publicas.StatusChamado.AguardandoAutorizacao ? 7 :
                (_chamado.Status == Publicas.StatusChamado.AguardandoCronograma ? 8 :
                9)))))))));

            prioridadeComboBox.SelectedIndex = (_chamado.Prioridade == Publicas.Prioridades.Critico ? 0 :
                (_chamado.Prioridade == Publicas.Prioridades.Alta ? 1 :
                (_chamado.Prioridade == Publicas.Prioridades.Media ? 2 : 3)));

            origemComboBox.SelectedIndex = (_chamado.Origens == Publicas.Origem.OnLine ? 0 :
                (_chamado.Origens == Publicas.Origem.Email ? 1 :
                (_chamado.Origens == Publicas.Origem.Chat ? 3 :
                2)));

            tipoChamadoComboBox.SelectedIndex = (_chamado.Tipo == Publicas.TipoChamado.Erro ? 0 :
                (_chamado.Tipo == Publicas.TipoChamado.Duvida ? 1 :
                (_chamado.Tipo == Publicas.TipoChamado.Implementacao ? 2 : 
                (_chamado.Tipo == Publicas.TipoChamado.Acesso ? 3 :
                (_chamado.Tipo == Publicas.TipoChamado.Ajustes ? 4 :
                (_chamado.Tipo == Publicas.TipoChamado.Projeto ? 5 :
                6 ))))));

            if (!string.IsNullOrEmpty(_chamado.Adequacao))
            {
                tipoAdequacaoComboBox.SelectedIndex = (_chamado.Adequacao.Contains("SIM") ? 0 :
                    (_chamado.Adequacao.Contains("PSE") ? 1 :
                    (_chamado.Adequacao.Contains("Pedido") ? 2 :
                    (_chamado.Adequacao.Contains("Ticket") ? 3 : 4 ))));
                numeroAdequacaoTextBox.Text = _chamado.Adequacao.Replace("SIM ", "").Replace("PSE ", "").Replace("Pedido ","").Replace("Ticket ","").Replace("Outros ","");

                if (_chamado.PrazoDesenvolvimento != 0 )
                    PrazoEntregaTextBox.Text = _chamado.PrazoDesenvolvimento.ToString();
            }

            tipoAdequacaoComboBox.Visible = !string.IsNullOrEmpty(numeroAdequacaoTextBox.Text.Trim());
            numeroAdequacaoTextBox.Visible = !string.IsNullOrEmpty(numeroAdequacaoTextBox.Text.Trim());
            adequacaoLabel.Visible = (statusComboBox.SelectedIndex == 2 || !string.IsNullOrEmpty(PrazoEntregaTextBox.Text.Trim())) && statusComboBox.SelectedIndex != 5;

            foreach (var item in _listaEmpresas.Where(w => w.IdEmpresa == _chamado.IdEmpresa))
            {
                for (int i = 0; i < empresaComboBox.Items.Count; i++)
                {
                    empresaComboBox.SelectedIndex = i;
                    if (empresaComboBox.Text == item.CodigoeNome)
                        break;
                }
            }

            foreach (var item in _listaCategorias.Where(w => w.IdCategoria == _chamado.IdCategoria))
            {
                for (int i = 0; i < categoriaComboBox.Items.Count; i++)
                {
                    categoriaComboBox.SelectedIndex = i;
                    if (categoriaComboBox.Text == item.Descricao)
                        break;
                }
            }

            foreach (var item in _listaModulos.Where(w => w.IdModulo == _tela.IdModulo))
            {
                for (int i = 0; i < modulosComboBox.Items.Count; i++)
                {
                    modulosComboBox.SelectedIndex = i;
                    if (modulosComboBox.Text == item.Nome)
                        break;
                }
            }

            foreach (var item in _listaTelas.Where(w => w.IdTela == _chamado.IdTela))
            {
                for (int i = 0; i < telasComboBox.Items.Count; i++)
                {
                    telasComboBox.SelectedIndex = i;
                    if (telasComboBox.Text == item.NomeCompleto)
                        break;
                }
            }

            if (_chamado.PrazoDesenvolvimento != 0)
                PrazoEntregaTextBox.Text = _chamado.PrazoDesenvolvimento.ToString();
            _historicos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, false, false);
            TotalHistoricosLabel.Text = _historicos.Count() + " Tramites";
            TotalHistoricosLabel.Visible = _historicos.Count() >= 10;

            _listaAnexos = new ChamadoBO().ListarAnexos(_chamado.IdChamado);

            foreach (var item in _listaAnexos)
                listBox2.Items.Add(item.NomeArquivo);

            foreach (var item in _historicos)
            {
                if ((Publicas._usuario.Tipo != Publicas.TipoUsuario.Atendente && !item.Privado) ||
                    (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente))
                    CriaPanelHistoricos(item.Descricao, item.Data, (item.Tipo == "S" || item.IdUsuario == _chamado.IdUsuario ? item.Nome : ""), (item.Tipo == "S" ? "" : item.Nome), false, item.IdHistorico, item.Privado);
            }

            statusComboBox.Visible = true;
            statusComboBox.ReadOnly = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente &&
                                      (_chamado.Status == Publicas.StatusChamado.Finalizado || _chamado.Status == Publicas.StatusChamado.AguardandoAutorizacao);

            if (_chamado.IdUsuario == Publicas._idUsuario)
            { // Quem abriu não pode mudar o status.
                //if (statusComboBox.SelectedIndex != 2 && statusComboBox.SelectedIndex != 6 && ) // Aguardando adequação e finalizado não pode mudar o status 
                //    statusComboBox.SelectedIndex = 0;
                statusComboBox.ReadOnly = true;
            }

            Publicas._escTeclado = true;
            tipoChamadoComboBox.ReadOnly = _chamado.Status == Publicas.StatusChamado.Finalizado;
            prioridadeComboBox.ReadOnly = _chamado.Status == Publicas.StatusChamado.Finalizado;
            origemComboBox.ReadOnly = _chamado.Status == Publicas.StatusChamado.Finalizado;
            categoriaComboBox.ReadOnly = _chamado.Status == Publicas.StatusChamado.Finalizado;
            modulosComboBox.ReadOnly = _chamado.Status == Publicas.StatusChamado.Finalizado;
            telasComboBox.ReadOnly = _chamado.Status == Publicas.StatusChamado.Finalizado;
            concluirLabel.Visible = _chamado.Status != Publicas.StatusChamado.Finalizado;
            concluirToggleButton.Visible = _chamado.Status != Publicas.StatusChamado.Finalizado;
            
            avaliacaoRatingControl.Visible = _chamado.Status == Publicas.StatusChamado.Finalizado;
            gravarButton.Visible = _chamado.Status != Publicas.StatusChamado.Finalizado;

            reabrirButton.Visible = _chamado.Status == Publicas.StatusChamado.Finalizado &&
                _chamado.DataRetorno.AddDays(Publicas._prazoReabrir).Date >= DateTime.Now.Date; // ver prazo

            //reabrirButton.Enabled = _chamado.DataRetorno.AddDays(Publicas._prazoReabrir).Date >= DateTime.Now.Date;
            CriarNovoChamadoButton.Visible = _chamado.Status == Publicas.StatusChamado.Finalizado &&
                _chamado.DataRetorno.AddDays(Publicas._prazoReabrir).Date < DateTime.Now.Date; // ver prazo
            CriarNovoChamadoButton.Enabled = CriarNovoChamadoButton.Visible;

            prazoLabel.Visible = CriarNovoChamadoButton.Visible;

            avaliacaoRatingControl.ToolTipSettings.Body.Text = _chamado.DescricaoAvaliacao;
            avaliacaoRatingControl.Value = (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Ruim ? 1 :
                (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Regular ? 2 :
                (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Bom ? 3 :
                (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.MuitoBom ? 4 :
                (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Excelente ? 5 : 0)))));

            if ((Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante && _chamado.IdUsuario == Publicas._usuario.Id) ||
                (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && _chamado.IdUsuario == Publicas._usuario.Id))
            {
                if (_chamado.Status == Publicas.StatusChamado.Finalizado &&
                   (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.SemAvaliacao ||
                    (!_chamado.Reavaliado && _chamado.Reavaliar)) &&
                    _chamado.IdUsuario == Publicas._idUsuario)
                {
                    Publicas._chamado = _chamado;
                    Chamados.Avaliacao _tela = new Chamados.Avaliacao();
                    _tela.ShowDialog();

                    if (_tela.GravouAvaliacao)
                        Close();
                }
            }
            else
            {
                if (_chamado.Status == Publicas.StatusChamado.Finalizado &&
                _chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.SemAvaliacao &&
                _historicos.Where(w => w.IdUsuario == Publicas._idUsuario).Count() != 0)
                {
                    Publicas._chamado = _chamado;
                    Chamados.Avaliacao _tela = new Chamados.Avaliacao();
                    _tela.ShowDialog();

                    if (_tela.GravouAvaliacao)
                        Close();
                }
            }

            if (_chamado.ApenasAtendendeQueEncerra && Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante)
            {
                concluirToggleButton.Visible = false;
                concluirLabel.Visible = false;
            }

            if (_chamado.IdUsuarioAutorizacao == Publicas._usuario.Id)
            {
                if ((!_chamado.Autorizado && !_chamado.AguardarAutorizado && string.IsNullOrEmpty(_chamado.MotivoNegacaoDaAutorizacao)) ||
                    (!_chamado.Autorizado && _chamado.AguardarAutorizado ))
                {
                    new Notificacoes.Mensagem("Este chamado aguarda sua autorização.", Publicas.TipoMensagem.Alerta).ShowDialog();

                    _usuarioAutorizador = new UsuarioBO().ConsultarPorId(_chamado.IdUsuarioAutorizacao);
                    UsuarioAutorizacaoTextBox.Text = _usuarioAutorizador.UsuarioAcesso;
                    nomeTextBox.Text = _usuarioAutorizador.Nome;

                    AutorizacaoPanel.Location = new Point(163, 186);
                    AutorizacaoPanel.Visible = true;
                    AutorizacaoPanel.Size = new Size(708, 256);
                    AutorizarButton.Location = new Point(613, 219);
                    AutorizarButton.Text = "&Gravar";
                    UsuarioAutorizacaoTextBox.Enabled = false;
                    AutorizadoToggleButton.Focus();
                    AutorizarButton.Enabled = true;
                }
            }
            Publicas._escTeclado = false;
        }

        private void categoriaComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _idCategoria = 0;

            foreach (var item in _listaCategorias.Where(w => w.Descricao == categoriaComboBox.Text))
            {
                _idCategoria = item.IdCategoria;
            }

            _listaModulos = new ModuloBO().Listar(_idCategoria);
            modulosComboBox.DataSource = _listaModulos.Where(w => w. Ativo)
                                                      .OrderBy(o => o.Nome).ToList();
            modulosComboBox.DisplayMember = "Nome";

        }

        private void categoriaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                modulosComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                origemComboBox.Focus();
            }
        }

        private void categoriaComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void origemComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                categoriaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                prioridadeComboBox.Focus();
            }
        }

        private void prioridadeComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                origemComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoChamadoComboBox.Focus();
            }
        }

        private void tipoChamadoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                prioridadeComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoChamadoComboBox.Focus();
            }
        }

        private void CriaPanelHistoricos(string mensagem, DateTime data, string nomeSolicitante, string nomeResponsavel, bool lida, int idHistorico, bool privado)
        {
            int QtdAnexos = 0;
            bool Agrupado = false;
            bool Associado = false;
            string numero = "";
            bool Lido = false;
            int idUsuario = 0;

            foreach (var item in _historicos.Where(w => w.IdHistorico == idHistorico))
            {
                Agrupado = item.Agrupado;
                Associado = item.Associado;
                numero = item.NumeroChamado;
                idUsuario = item.IdUsuario;

                NotificacaoChamado _not = new NotificacaoChamadoBO().Consultar(Publicas._usuario.Id, item.IdHistorico);
                if (!_not.Existe)
                {
                    new NotificacaoChamadoBO().Gravar(new NotificacaoChamado()
                    {
                        IdChamado = _chamado.IdChamado,
                        IdHistorico = item.IdHistorico,
                        IdUsuario = Publicas._usuario.Id
                    });
                    //item.Lido = true;
                }
                Lido = item.Lido;
            }

            Panel panel = new Panel();
            panel.Size = new Size(685, _heigthMinimo);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Name = "Historico" + _posicaoChat;
            if (Publicas._TemaBlack)
                panel.BackColor = Publicas._fundo;
            
            Panel panel2 = new Panel();
            panel2.Size = new Size(21, 20);

            Panel panel3 = new Panel();
            panel3.Size = new Size(21, 20);
            panel3.Dock = DockStyle.Bottom;
            panel3.ContextMenuStrip = anexosContextMenuStrip;
            panel3.Name = "panelAnexos_" + idHistorico.ToString();
            if (Publicas._TemaBlack)
                panel.BackColor = Publicas._panelTitulo;

            Label lbl = new Label();
            lbl.Size = new Size(459, 20);
            lbl.Dock = DockStyle.Left;
            lbl.Font = new Font("Century Gothic", 8, FontStyle.Bold);
            lbl.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : Publicas._bordaEntrada);
            lbl.Text = "" ;
            lbl.MouseLeave += new System.EventHandler(lbl_MouseLeave);
            lbl.MouseHover += new System.EventHandler(lbl_MouseHover);


            TextBoxExt lblAnexo = new TextBoxExt();
            lblAnexo.Dock = DockStyle.Left;
            if (Publicas._TemaBlack)
            {
                lblAnexo.Style = TextBoxExt.theme.Office2016Black;
                lblAnexo.BackColor = Publicas._panelTitulo;
            }
            else
            {
                lblAnexo.Style = TextBoxExt.theme.Metro;
            }
            lblAnexo.NearImage = clipAnexoPictureBox.Image;
            lblAnexo.ThemesEnabled = true;
            lblAnexo.BorderStyle = BorderStyle.None;
            lblAnexo.Size = new Size(126, 16);
            lblAnexo.Font = new Font("Century Gothic", 8, FontStyle.Underline);
            lblAnexo.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : Publicas._bordaEntrada); 
            lblAnexo.TextAlign = HorizontalAlignment.Left;
            lblAnexo.TabStop = false;
            lblAnexo.ReadOnly = true;
            QtdAnexos = _listaAnexos.Where(w => w.IdHistorico == idHistorico).Count();
            lblAnexo.Text = " Anexos: " + QtdAnexos.ToString();
            lblAnexo.Name = "labelAnexos_" + idHistorico.ToString();
            lblAnexo.ContextMenuStrip = anexosContextMenuStrip;
            lblAnexo.Click += new System.EventHandler(lblAnexo_Click);
            lblAnexo.MouseLeave += new System.EventHandler(lblAnexo_MouseLeave);
            lblAnexo.MouseHover += new System.EventHandler(lblAnexo_MouseHover);

            Label lblprivado = new Label();
            lblprivado.Size = new Size(359, 20);
            lblprivado.Dock = DockStyle.Right;
            lblprivado.Font = new Font("Century Gothic", 8, FontStyle.Bold);
            lblprivado.ForeColor = (Publicas._TemaBlack ? Color.OrangeRed : Color.Maroon);
            lblprivado.Text = (privado ? "Tramite privado" : "");
            lblprivado.TextAlign = ContentAlignment.MiddleRight;

            Label lblLido = new Label();
            lblLido.Size = new Size(30, 20);
            lblLido.Dock = DockStyle.Right;
            lblLido.Font = new Font("Wingdings 2", 10, FontStyle.Bold);
            lblLido.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : Publicas._bordaEntrada);
            lblLido.Text = (Lido ? "P" : "");
            lblprivado.TextAlign = ContentAlignment.MiddleRight;

            panel3.Visible = QtdAnexos != 0 || privado || Lido;
            panel3.Controls.Add(lblAnexo);
            if (privado)
            panel3.Controls.Add(lblprivado);
            if (Lido)
            panel3.Controls.Add(lblLido);

            Label lblEditar = new Label();
            lblEditar.Size = new Size(30, 20);
            lblEditar.Dock = DockStyle.Right;
            lblEditar.Font = new Font("Wingdings 2", 10, FontStyle.Bold);
            lblEditar.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : Publicas._bordaEntrada);
            lblEditar.Text = "!";
            lblEditar.Tag = idHistorico;
            lblEditar.Click += new System.EventHandler(lblEditar_Click);
            lblEditar.MouseLeave += new System.EventHandler(lblEditar_MouseLeave);
            lblEditar.MouseHover += new System.EventHandler(lblEditar_MouseHover);

            ToolTipInfo _tool = new ToolTipInfo();
            _tool.Footer.Font = descricaoTextBox.Font;
            _tool.Footer.Text = "Editar";
            superToolTip.SetToolTip(lblEditar, _tool);

            Label dataTxt = new Label();
            dataTxt.Dock = DockStyle.Right;
            if (Publicas._TemaBlack)
                dataTxt.BackColor = Publicas._panelTitulo;

            dataTxt.Size = new Size(126, 16);
            dataTxt.Font = new Font("Century Gothic", 8);
            dataTxt.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : Publicas._bordaEntrada);
            dataTxt.TextAlign = ContentAlignment.MiddleRight;

            RichTextBox text = new RichTextBox();
            text.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            if (Publicas._TemaBlack)
                text.BackColor = Publicas._fundo;
            text.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.Black);
            text.Dock = DockStyle.Fill;
            text.BorderStyle = BorderStyle.None;
            text.ReadOnly = true;
            text.Margin = new Padding(10, 10, 0, 0);
            text.ScrollBars = RichTextBoxScrollBars.None;
            text.Multiline = true;
            text.Size = new Size(685, _heigthMinimo-20);
            text.ScrollBars = RichTextBoxScrollBars.Vertical;
            
            text.Text = mensagem;

            panel2.Controls.Add(lbl);
            panel2.Controls.Add(dataTxt);

            if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                panel2.Controls.Add(lblEditar);

            panel.Controls.Add(panel2);
            panel.Controls.Add(panel3);
            panel.Controls.Add(text);

            decimal qtdlinhas = Arredontamento(text.TextLength, 80);
            panel2.Dock = DockStyle.Top;

            qtdlinhas = (qtdlinhas < text.Lines.Count() ? qtdlinhas + text.Lines.Count() : qtdlinhas);

            if (qtdlinhas < 5)
                panel.Size = new Size(685, _heigthMinimo);
            else
            {
                if (qtdlinhas < 10)
                    panel.Size = new Size(685, (int)qtdlinhas * 22);
                else
                {
                    if (qtdlinhas < 15)
                        panel.Size = new Size(685, (int)qtdlinhas * 22);
                    else
                        panel.Size = new Size(685, 350);
                }
            }

            string[] usuarios;
            lbl.Tag = idUsuario;

            if (nomeSolicitante != null && !string.IsNullOrEmpty(nomeSolicitante.Trim()))
            {
                usuarios = nomeSolicitante.Split(' ');
                lbl.Text = usuarios[0] + " " + usuarios[usuarios.Length-1] + 
                    (Associado || Agrupado ? "                 [ Tramite do chamado -> " + numero + " ]": "");
                dataTxt.Text = data.ToShortDateString() + " " + data.ToShortTimeString() + " ";
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                panel.Location = new Point(30, _posicaoChat);
                historicoPanel.Controls.Add(panel);
            }
            else
            {
                usuarios = nomeResponsavel.Split(' ');
                lbl.Text = usuarios[0] + " " + usuarios[usuarios.Length - 1] + 
                    (Associado || Agrupado ? "                 [ Tramite do chamado -> " + numero + " ]": "");
                dataTxt.Text = data.ToShortDateString() + " " + data.ToShortTimeString() + " ";
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                panel.Location = new Point(10, _posicaoChat);
                historicoPanel.Controls.Add(panel);
            }

            text.BringToFront();
            
            _posicaoChat = _posicaoChat + (panel.Height != _heigthMinimo ? panel.Height : _heigthMinimo) + 4;

        }

        private void lbl_MouseHover(object sender, EventArgs e)
        {
            Classes.Usuario _usuarioFoto = new UsuarioBO().ConsultarPorId(Convert.ToInt32(((Label)sender).Tag));

            if (_usuarioFoto.Foto != null)
            {
                
                foto.Location = new Point((((Label)sender).Parent).Left + 15, (((Label)sender).Parent).Top + 20);

                foto.Parent = (Panel)((Panel)((Label)sender).Parent).Parent;
                foto.Size = new Size(70,75);

                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(_usuarioFoto.Foto, 0, _usuarioFoto.Foto.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    foto.Image = new Bitmap(mStream);
                }

                foto.SizeMode = PictureBoxSizeMode.StretchImage;

                foto.BringToFront();
                foto.Visible = true;
                foto.Refresh();
            }
        }

        private void lbl_MouseLeave(object sender, EventArgs e)
        {
            foto.Visible = false;
            foto.Parent = null;
        }

        private void lblEditar_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Hand;
        }

        private void lblEditar_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Default;
        }

        private void lblEditar_Click(object sender, EventArgs e)
        {
            Chamados.Editar _tela = new Editar();

            _tela.tipoAdequacaoComboBox.Items.AddRange(new object[] { "SIM", "PSE", "Pedido", "Ticket", "Outros" });
            _tela.tipoAdequacaoComboBox.Visible = false;
            _tela.numeroAdequacaoTextBox.Visible = false;
            _tela.PrazoEntregaTextBox.Visible = false;
            _tela.adequacaoLabel.Visible = false;
            _tela.label1.Visible = false;

            _tela.numeroTextBoxExt.Text = this.numeroTextBoxExt.Text;
            _tela._idHistorico = Convert.ToInt32(((Label)sender).Tag);
            _tela._idChamado = _chamado.IdChamado;

            foreach (var item in _historicos.Where(w => w.IdHistorico == _tela._idHistorico))
            {
                if (item.Adequacao != null || item.Adequacao != " ")
                {
                    _tela.tipoAdequacaoComboBox.SelectedIndex = this.tipoAdequacaoComboBox.SelectedIndex;
                    _tela.numeroAdequacaoTextBox.Text = this.numeroAdequacaoTextBox.Text;
                    _tela.PrazoEntregaTextBox.Text = this.PrazoEntregaTextBox.Text;
                    _tela.tipoAdequacaoComboBox.Visible = true;
                    _tela.numeroAdequacaoTextBox.Visible = true;
                    _tela.PrazoEntregaTextBox.Visible = true;
                    _tela.adequacaoLabel.Visible = true;
                    _tela.label1.Visible = true;
                }
            }

            _tela.ShowDialog();

            LimpaHistoricos(historicoPanel);

            _historicos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, false, false);
            _listaAnexos = new ChamadoBO().ListarAnexos(_chamado.IdChamado);

            foreach (var item in _listaAnexos)
                listBox2.Items.Add(item.NomeArquivo);

            foreach (var item in _historicos)
            {
                if ((Publicas._usuario.Tipo != Publicas.TipoUsuario.Atendente && !item.Privado) ||
                    (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente))
                    CriaPanelHistoricos(item.Descricao, item.Data, (item.Tipo == "S" || item.IdUsuario == _chamado.IdUsuario ? item.Nome : ""), (item.Tipo == "S" ? "" : item.Nome), false, item.IdHistorico, item.Privado);
            }
        }

        private void lblAnexo_Click(object sender, EventArgs e)
        {
            Point p = MousePosition;
            int id = Convert.ToInt32(((TextBoxExt)sender).Name.Replace("labelAnexos_", ""));
            
            if (anexosContextMenuStrip.Items.Count != 2)
            {
                for (int j = anexosContextMenuStrip.Items.Count-1; j > 1 ; j--)
                {
                    anexosContextMenuStrip.Items.RemoveAt(j);
                }
            }

            anexosContextMenuStrip.Items.Add("-");
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                foreach (var item in _listaAnexos.Where(w => w.IdHistorico == id))
                {
                    if (item.NomeArquivo == listBox2.Items[i].ToString())
                    {
                        ToolStripMenuItem menu = new ToolStripMenuItem();
                        menu.Text = listBox2.Items[i].ToString();
                        menu.Click += new System.EventHandler(abrirAnexo_Click);
                        anexosContextMenuStrip.Items.Add(menu);
                    }
                }
            }
            
            anexosContextMenuStrip.Show(p.X, p.Y);                
        }

        private void lblAnexo_MouseHover(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).Cursor = Cursors.Hand;
        }

        private void lblAnexo_MouseLeave(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).Cursor = Cursors.Default;
        }

        public int Arredontamento(decimal valor, int divisor)
        {
            int l = (int)Math.Truncate(valor/divisor);
            decimal d = (valor/divisor) - l;

            d = Math.Round(d * 100);

            if (d > 50)
                l = l + 1;

            return l;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            try
            {
                assuntoTextBox.Text = assuntoTextBox.Text.Replace("'", "");
            }
            catch { }
             
            if (statusComboBox.SelectedIndex == 9 && _idCategoria != 2)
            {
                new Notificacoes.Mensagem("Status 'Aguardando Conserto' só pode ser utilizado na categoria 'Infraestrutura'." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                statusComboBox.Focus();
                return;
            }
             
            if (statusComboBox.SelectedIndex == 8 && _idCategoria != 9 && _idCategoria != 10 && _idCategoria != 11)
            {
                new Notificacoes.Mensagem("Status 'Aguardando Cronograma' só pode ser utilizado nas categorias:" +
                    Environment.NewLine +
                    "'Power BI', 'Support' e 'Desenvolvimento Web'." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                statusComboBox.Focus();
                return;
            }

            if (PrazoEntregaTextBox.Visible && (PrazoEntregaTextBox.Text.Trim() == "" || Convert.ToInt32(PrazoEntregaTextBox.Text.Trim()) == 0) &&
                Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && 
                (statusComboBox.SelectedIndex == 2 || statusComboBox.SelectedIndex == 5 || statusComboBox.SelectedIndex == 8 || statusComboBox.SelectedIndex == 9))
            {
                // Desenvolvimento, Cronograma, Conserto e Adequação
                new Notificacoes.Mensagem("Informe o prazo para a entrega.", Publicas.TipoMensagem.Alerta).ShowDialog();
                PrazoEntregaTextBox.Focus();
                return;
            }

            try
            {
                if (!PrivadoCheckBox.Checked && _listaTemposExecucao != null && _listaTemposExecucao.Count() > 0)
                {
                    // Pausa se existir sem data final e for do mesmo usuário.
                    foreach (var item in _listaTemposExecucao.Where(w => w.Id == Publicas._idTemporizador && w.DataFim == DateTime.MinValue && w.IdUsuario == Publicas._usuario.Id))
                    {
                        new ChamadoBO().PausarTemporizador(Publicas._idTemporizador, item.DataInicio);
                    }
                }
            }
            catch { }

            try
            {
                _chamado.MinutosEstimados = (int)Convert.ToInt32(EntradaMaskedEditBox.Text.Substring(0, 2).Trim()) * 60 + Convert.ToInt32(EntradaMaskedEditBox.Text.Substring(3, 2).Trim());
            }
            catch { }

            if ((statusComboBox.SelectedIndex == 0 || statusComboBox.SelectedIndex == 4) && 
                Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && 
                !PrivadoCheckBox.Checked && 
                concluirToggleButton.ToggleState != ToggleButtonState.Active)
            {
                if (new Notificacoes.Mensagem("Deseja manter o chamado com você (em andamento) ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    statusComboBox.SelectedIndex = 1;//
                else
                    statusComboBox.SelectedIndex = 0;
            }

            if (statusComboBox.SelectedIndex == 2 && Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && !PrivadoCheckBox.Checked && concluirToggleButton.ToggleState != ToggleButtonState.Active)
            {
                if (new Notificacoes.Mensagem("Deseja manter Aguardando Terceiros ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    statusComboBox.SelectedIndex = 1;// aguardando Solicitante
                else
                    statusComboBox.SelectedIndex = 2; // aguardando terceiros
            }

            if (concluirToggleButton.ToggleState == ToggleButtonState.Active && Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
            {
                if (new Notificacoes.Mensagem("Deseja criar um lembrete para esse chamado ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                {
                    new Chamados.Lembrete().ShowDialog();
                    if (!Publicas._temLembrete)
                    {
                        new Notificacoes.Mensagem("Processo cancelado." + Environment.NewLine + Environment.NewLine +
                            "Não foi informado o Prazo para o Lembrete.", Publicas.TipoMensagem.Alerta).ShowDialog();
                        return;
                    }
                    else
                    {
                        _chamado.DiasDoLembrete = Publicas._prazoLembrete;
                        _chamado.MotivoLembrete = Publicas._motivoLembrete;

                        if (Publicas._datasLembrete.Count() > 0)
                        {
                            List<Classes.Lembrete> _lembrete = new List<Classes.Lembrete>();

                            foreach (var item in Publicas._datasLembrete)
                            {
                                _lembrete.Add(new Classes.Lembrete()
                                {
                                    IdChamado = _chamado.IdChamado,
                                    Data = item.Date
                                });
                            }

                            new ChamadoBO().GravarLembrete(_lembrete);
                        }
                    }
                }
            }

            int _idEmpresa = 0;
            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBox.Text))
            {
                _idEmpresa = item.IdEmpresa;
            }

            foreach (var item in _listaTelas.Where(w => w.NomeCompleto == telasComboBox.Text))
                _idTela = item.IdTela;

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (_chamado.Status == Publicas.StatusChamado.Finalizado ?
                "Concluiu o chamado " :
                "Incluiu um novo tramite" + (PrivadoCheckBox.Checked ? " Privado" : "") + " ao chamado ") + numeroTextBoxExt.Text +
                (_chamado.Assunto != assuntoTextBox.Text ? " Alterou o assunto de " + _chamado.Assunto + " para " + assuntoTextBox.Text : "");

            _log.Tela = "Tramites - Chamado";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            _chamado.IdTela = _idTela;
            _chamado.Assunto = assuntoTextBox.Text;
            _chamado.Descricao = (descricaoTextBox.Text == descricaoTextBox.Text.ToUpper() ?
                                             CultureInfo.CurrentCulture.TextInfo.ToTitleCase(descricaoTextBox.Text.Trim().ToLower()) :
                                             descricaoTextBox.Text.Trim());
            _chamado.IdEmpresa = _idEmpresa;

            _chamado.Tipo = (tipoChamadoComboBox.SelectedIndex == 0 ? Publicas.TipoChamado.Erro :
                            (tipoChamadoComboBox.SelectedIndex == 1 ? Publicas.TipoChamado.Duvida :
                            (tipoChamadoComboBox.SelectedIndex == 2 ? Publicas.TipoChamado.Implementacao :
                            (tipoChamadoComboBox.SelectedIndex == 3 ? Publicas.TipoChamado.Acesso :
                            (tipoChamadoComboBox.SelectedIndex == 4 ? Publicas.TipoChamado.Ajustes :
                            (tipoChamadoComboBox.SelectedIndex == 5 ? Publicas.TipoChamado.Projeto :
                            Publicas.TipoChamado.Solicitacao ))))));

            if (_usuarioConvidado != null)
                _chamado.IdUsuarioAcompanhamento = _usuarioConvidado.Id;

            if (!PrivadoCheckBox.Checked)
            {
                if (concluirToggleButton.ToggleState == ToggleButtonState.Active && statusComboBox.SelectedIndex != 3)// diferente de cancelado 
                {
                    // verificar se tem atendente
                    bool encontrouAtendente = false;
                    _ultimoHistoricos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, false, true);

                    foreach (var item in _ultimoHistoricos)
                    {
                        _usuario = new UsuarioBO().ConsultarPorId(item.IdUsuario);

                        if (_usuario.Tipo == Publicas.TipoUsuario.Atendente)
                        {
                            encontrouAtendente = true;
                            break;
                        }
                    }

                    // Se concluiu o chamado sem atendente, irá cancelar o chamado. 
                    if (!encontrouAtendente && Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante)
                        _chamado.Status = Publicas.StatusChamado.Cancelado;
                    else
                        _chamado.Status = Publicas.StatusChamado.Finalizado;
                }
                else
                {
                    if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante && _chamado.Status == Publicas.StatusChamado.Novo)
                        _chamado.Status = Publicas.StatusChamado.Novo;
                    else
                    {
                        if (statusComboBox.SelectedIndex == 1 && Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante)
                            _chamado.Status = Publicas.StatusChamado.EmAndamento;
                        else
                            _chamado.Status = (statusComboBox.SelectedIndex == 0 ? Publicas.StatusChamado.EmAndamento :
                                              (statusComboBox.SelectedIndex == 1 ? Publicas.StatusChamado.Pendente :
                                              (statusComboBox.SelectedIndex == 2 ? Publicas.StatusChamado.Adequacao :
                                              (statusComboBox.SelectedIndex == 3 ? Publicas.StatusChamado.Cancelado :
                                              (statusComboBox.SelectedIndex == 4 ? Publicas.StatusChamado.Reaberto :
                                              (statusComboBox.SelectedIndex == 5 ? Publicas.StatusChamado.EmDesenvolvimento :
                                              (statusComboBox.SelectedIndex == 7 ? Publicas.StatusChamado.AguardandoAutorizacao :
                                              (statusComboBox.SelectedIndex == 8 ? Publicas.StatusChamado.AguardandoCronograma :
                                              (statusComboBox.SelectedIndex == 9 ? Publicas.StatusChamado.AguardandoConserto :
                                              Publicas.StatusChamado.Finalizado)))))))));
                    }
                }

                if (_chamado.IdCategoria != _idCategoria) // mudou a categoria volta o status para novo, para chamar a atenção dos atendentes
                    _chamado.Status = Publicas.StatusChamado.Novo;
            }

            _chamado.IdCategoria = _idCategoria;

            if (PrazoEntregaTextBox.Visible && !string.IsNullOrEmpty(PrazoEntregaTextBox.Text.Trim()))
                _chamado.PrazoDesenvolvimento = Convert.ToInt32(PrazoEntregaTextBox.Text);

            if (numeroAdequacaoTextBox.Visible && string.IsNullOrEmpty(numeroAdequacaoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o número da adequação.", Publicas.TipoMensagem.Alerta).ShowDialog();
                numeroAdequacaoTextBox.Focus();
                return;
            }

            

            if (numeroAdequacaoTextBox.Visible)
                _chamado.Adequacao = tipoAdequacaoComboBox.Text + " " + numeroAdequacaoTextBox.Text;

            List<AnexoDoHistorico> _listaAnexosGravar = new List<AnexoDoHistorico>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                AnexoDoHistorico _anexos = new AnexoDoHistorico();

                StreamReader oStreamReader = new StreamReader(listBox1.Items[i].ToString());

                byte[] buffer = new byte[oStreamReader.BaseStream.Length];

                oStreamReader.BaseStream.Read(buffer, 0, buffer.Length);

                oStreamReader.Close();
                oStreamReader.Dispose();

                _anexos.Anexo = buffer;
                _anexos.NomeArquivo = Path.GetFileName(listBox1.Items[i].ToString());
                _listaAnexosGravar.Add(_anexos);
            }
             
            //if (_chamado.Numero != "201901-0139")
            {
                
                int idAutorizador = 0;

                //pega o ultimo autorizador 
                foreach (var item in _historicos.Where(w => w.Status == Publicas.StatusChamado.AguardandoAutorizacao
                                                         && w.IdUsuarioAutorizacao != 0)
                                                .OrderByDescending(o => o.IdHistorico))
                {
                    if (item.RespondeuAutorizacao)
                        idAutorizador = item.IdUsuarioAutorizacao;
                    break;
                }

                if (!new ChamadoBO().Gravar(_chamado))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                if (!new ChamadoBO().GravarHistorico(new HistoricoDoChamado()
                {
                    IdChamado = _chamado.IdChamado,
                    Descricao = _chamado.Descricao + Environment.NewLine + Environment.NewLine + Publicas._usuario.AssinaturaChamado,
                    IdUsuario = Publicas._idUsuario,
                    Status = _chamado.Status,
                    Privado = PrivadoCheckBox.Checked,
                    Prazo = _chamado.PrazoDesenvolvimento,
                    Adequacao = _chamado.Adequacao,
                    IdUsuarioAutorizacao = (_chamado.Status == Publicas.StatusChamado.AguardandoAutorizacao ? idAutorizador : 0),
                    Usuario = ((Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && _chamado.IdUsuario == Publicas._usuario.Id) ||
                               Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante ? "S" : "A")
                }, _listaAnexosGravar, Publicas._sla))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }
            
            LimpaHistoricos(historicoPanel);

            _historicos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, false, false);
            _listaAnexos = new ChamadoBO().ListarAnexos(_chamado.IdChamado);

            foreach (var item in _listaAnexos)
                listBox2.Items.Add(item.NomeArquivo);

            foreach (var item in _historicos)
            {
                if ((Publicas._usuario.Tipo != Publicas.TipoUsuario.Atendente && !item.Privado) ||
                    (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente))
                CriaPanelHistoricos(item.Descricao, item.Data, (item.Tipo == "S" || item.IdUsuario == _chamado.IdUsuario ? item.Nome : ""), (item.Tipo == "S" ? "" : item.Nome), false, item.IdHistorico, item.Privado);
            }

            //if (!PrivadoCheckBox.Checked && numeroTextBoxExt.Text != "201901-0025")
            if (!PrivadoCheckBox.Checked)
                EnviarEmail();                       

            descricaoTextBox.Text = string.Empty;
            listBox1.Items.Clear();
        }

        private void EnviarEmail()
        {
            string[] _dadosEmail = new string[50];
            string emailCopia = "";
            _ultimoHistoricos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, true, true);
            string _emailDestino = "";
            string[] nome = Publicas._usuario.Nome.Split(' ');

            _emailDestino = Publicas._usuario.Email + "; ";

            // Se nao encontrou o atendente nos tramites irá buscar todos os atendentes para a categoria
            if (_emailDestino == "")
            {
                _listaUsuarios = new UsuarioBO().ConsultaAtendentesParaACategoria(_idCategoria);

                foreach (var item in _listaUsuarios)
                {
                    if (item.IdEmpresa == _usuario.IdEmpresa || item.IdEmpresa == 19 || item.UsuarioAcesso == "ELSILVA")
                    {
                        if (!_emailDestino.Contains(item.Email))
                            _emailDestino = _emailDestino + item.Email + "; ";
                    }
                }
            }

            if (_emailDestino != "")
            {
                if (!_emailDestino.Contains(Publicas._usuario.Email))
                    _emailDestino = _emailDestino + Publicas._usuario.Email + "; ";
            }

            if (!emailCopia.Contains(_usuario.EmailDepartamento))
                emailCopia = emailCopia + _usuario.EmailDepartamento + "; ";

            foreach (var item in _historicos.Where(w => !w.Privado))
            {
                // só pode enviar email para usuários que não Autorizam
                if (_historicos.Where(w => w.IdUsuarioAutorizacao == item.IdUsuario).Count() == 0)
                {
                    _usuario = new UsuarioBO().ConsultarPorId(item.IdUsuario);

                    if (!_emailDestino.Contains(_usuario.Email))
                        _emailDestino = _emailDestino + _usuario.Email + "; ";
                }
            }

            if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
            {
                // Quando atendente envia o tramite, busca o ultimo solicitante
                foreach (var item in _ultimoHistoricos)
                {
                    _usuario = new UsuarioBO().ConsultarPorId(item.IdUsuario);

                    if (_usuario.Tipo == Publicas.TipoUsuario.Socilitante)
                    {
                        _dadosEmail[2] = _usuario.Nome.Split(' ')[0];
                        break;
                    }                    
                }
            }
            else
            {
                foreach (var item in _ultimoHistoricos.Where(w => !w.Privado))
                {
                    // Quando solicitante envia o tramite, busca o ultimo atendente
                    _usuario = new UsuarioBO().ConsultarPorId(item.IdUsuario);

                    if (_usuario.Tipo == Publicas.TipoUsuario.Atendente)
                    {
                        _dadosEmail[2] = _usuario.Nome.Split(' ')[0];
                        break;
                    }
                }
            }

            _emailDestino = _emailDestino.Substring(0, _emailDestino.Length - 2);

            _dadosEmail[0] = (_chamado.Status == Publicas.StatusChamado.Finalizado ? "Conclusão" : 
                             (_chamado.Status == Publicas.StatusChamado.Reaberto ? "Reabertura" : "Alteração")) 
                            + " do Chamado " + _chamado.Numero;

            _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            _dadosEmail[3] = empresaComboBox.Text.Substring(10, empresaComboBox.Text.Length - 10);
            _dadosEmail[4] = _chamado.Numero;

            _dadosEmail[5] = (_chamado.Status == Publicas.StatusChamado.Finalizado ? "Finalizado" :
                             (_chamado.Status == Publicas.StatusChamado.Reaberto ? "Reaberto" : "Respondido"));
            _dadosEmail[6] = assuntoTextBox.Text;
            _dadosEmail[7] = categoriaComboBox.Text;
            _dadosEmail[8] = modulosComboBox.Text;
            _dadosEmail[9] = telasComboBox.Text;
            _dadosEmail[10] = tipoChamadoComboBox.Text;
            _dadosEmail[11] = prioridadeComboBox.Text;
            _dadosEmail[12] = (descricaoTextBox.Text == descricaoTextBox.Text.ToUpper() ?
                                             CultureInfo.CurrentCulture.TextInfo.ToTitleCase(descricaoTextBox.Text.Trim().ToLower()) :
                                             descricaoTextBox.Text.Trim()) +
                (_chamado.Status == Publicas.StatusChamado.Finalizado ? "</br></br> Por gentileza, avalie o meu atendimento. </br> A sua avaliação é muito importante. </br> ;)" : "");
            _dadosEmail[13] = anexoLabel.Text;
            _dadosEmail[14] = _parametro.PrazoRetorno.ToString();

            if (Publicas._usuario.AssinaturaChamado == "")
            {
                _dadosEmail[16] = "<p style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 13px'/> Atenciosamente,</br> " +
                               nome[0] + " " + nome[nome.Length - 1] +
                               "</font></p>";
            }
            else
            {
                _dadosEmail[16] = "<p style = 'color: #4169e1; font-family: arial, sans-serif; font-size: 13px'/>" + 
                    Publicas._usuario.AssinaturaChamado + "</font></p>";
            }

            //if (_chamado.Numero != "201901-0139")
            {
                if (!Publicas.EnviarEmailChamado(_dadosEmail, true, false, false, _emailDestino, _dadosEmail[0], false, emailCopia))
                {
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                if (_usuarioConvidado != null)
                {
                    _dadosEmail[12] = " Você foi convidado para acompanhar/interagir neste chamado. </br></br>" + descricaoTextBox.Text.Trim();

                    if (!Publicas.EnviarEmailChamado(_dadosEmail, true, false, false, _usuarioConvidado.Email, _dadosEmail[0], false, ""))
                    {
                        new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }
                }
            }

            new Notificacoes.Mensagem("Processo finalizado.", Publicas.TipoMensagem.Sucesso).ShowDialog();

            if (_chamado.Status == Publicas.StatusChamado.Finalizado)
            {
                if (new Notificacoes.Mensagem("Deseja avaliar agora ?" , Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                {
                    Publicas._chamado = _chamado;
                    Chamados.Avaliacao _tela = new Chamados.Avaliacao();
                    _tela.ShowDialog();

                    if (_tela.GravouAvaliacao)
                        Close();
                }
            }
        }

        private void AutorizarEmail()
        {
            string[] _dadosEmail = new string[50];
            string emailCopia = "";
            _ultimoHistoricos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, false, true);
            string _emailDestino = "";
            string[] nome = Publicas._usuario.Nome.Split(' ');
            string[] nomeSol = Publicas._usuario.Nome.Split(' ');

            Classes.Usuario _usuarioSol = new UsuarioBO().ConsultarPorId(_chamado.IdUsuario);
            try
            {
                nomeSol = _usuarioSol.Nome.Split(' ');
            }
            catch { }

            // Se nao encontrou o atendente nos tramites irá buscar todos os atendentes para a categoria
            if (_emailDestino == "")
            {
                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && Publicas._usuario.Id != _usuarioAutorizador.Id)
                {
                    _emailDestino = Publicas._usuario.Email + "; " + _usuarioAutorizador.Email + "; ";
                    _dadosEmail[2] = _usuarioAutorizador.Nome.Split(' ')[0];
                }
                else
                {
                    _listaUsuarios = new UsuarioBO().ConsultaAtendentesParaACategoria(_idCategoria);

                    _emailDestino = _emailDestino + Publicas._usuario.Email + "; ";

                    foreach (var item in _listaUsuarios)
                    {
                        if (item.IdEmpresa == _usuario.IdEmpresa || item.IdEmpresa == 19 || item.UsuarioAcesso == "ELSILVA")
                        {
                            if (!_emailDestino.Contains(item.Email))
                                _emailDestino = _emailDestino + item.Email + "; ";
                        }
                    }
                }
            }

            _emailDestino = _emailDestino.Substring(0, _emailDestino.Length - 2);

            _dadosEmail[0] = "Solicitação de Autorização do Chamado " + _chamado.Numero;

            _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            _dadosEmail[3] = empresaComboBox.Text.Substring(10, empresaComboBox.Text.Length - 10);
            _dadosEmail[4] = _chamado.Numero;

            _dadosEmail[5] = "Respondido";
            _dadosEmail[6] = assuntoTextBox.Text;
            _dadosEmail[7] = categoriaComboBox.Text;
            _dadosEmail[8] = modulosComboBox.Text;
            _dadosEmail[9] = telasComboBox.Text;
            _dadosEmail[10] = tipoChamadoComboBox.Text;
            _dadosEmail[11] = prioridadeComboBox.Text;

            if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && Publicas._usuario.Id != _usuarioAutorizador.Id)
            {
                _dadosEmail[12] = "Solicito sua autorização para atender este chamado." + Environment.NewLine + Environment.NewLine;

                foreach (var item in _ultimoHistoricos.OrderBy(o => o.Data))
                {
                    _dadosEmail[12] = _dadosEmail[12] + item.Descricao;
                    break;
                }
            }
            else
            {
                foreach (var item in _ultimoHistoricos.OrderByDescending(o => o.Data))
                {
                    _dadosEmail[12] = _dadosEmail[12] + item.Descricao;
                    break;
                }
            }

            _dadosEmail[12] = _dadosEmail[12] +  "</br> Solicitado por " + nomeSol[0] + " " + nomeSol[nomeSol.Length - 1];

            _dadosEmail[13] = anexoLabel.Text;
            _dadosEmail[14] = _parametro.PrazoRetorno.ToString();

            if (Publicas._usuario.AssinaturaChamado == "")
            {
                _dadosEmail[16] = "<p style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 13px'/> Atenciosamente,</br> " +
                               nome[0] + " " + nome[nome.Length - 1] +
                               "</font></p>";
            }
            else
            {
                _dadosEmail[16] = "<p style = 'color: #4169e1; font-family: arial, sans-serif; font-size: 13px'/>" +
                    Publicas._usuario.AssinaturaChamado + "</font></p>";
            }

            if (!Publicas.EnviarEmailChamado(_dadosEmail, true, false, false, _emailDestino, _dadosEmail[0], false, emailCopia))
            {
                new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            new Notificacoes.Mensagem("Processo finalizado.", Publicas.TipoMensagem.Sucesso).ShowDialog();
            AutorizacaoPanel.Visible = false;
            Close();
        }

        private void statusComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }

        private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool ApenasMostraPrazo = statusComboBox.SelectedIndex == 5 || statusComboBox.SelectedIndex == 8 || statusComboBox.SelectedIndex == 9;

            concluirToggleButton.Enabled = statusComboBox.SelectedIndex != 3;

            if (ApenasMostraPrazo)
            { 
                PrazoEntradaLabel.Left = adequacaoLabel.Left;
                PrazoEntregaTextBox.Left = tipoAdequacaoComboBox.Left;
            }
            else
            {
                PrazoEntradaLabel.Left = 229;
                PrazoEntregaTextBox.Left = 229;
            }

            PrazoEntradaLabel.Visible = statusComboBox.SelectedIndex == 2 || ApenasMostraPrazo || !string.IsNullOrEmpty(PrazoEntregaTextBox.Text.Trim());
            PrazoEntregaTextBox.Visible = statusComboBox.SelectedIndex == 2|| ApenasMostraPrazo || !string.IsNullOrEmpty(PrazoEntregaTextBox.Text.Trim());

            adequacaoLabel.Visible = statusComboBox.SelectedIndex == 2 || !string.IsNullOrEmpty(numeroAdequacaoTextBox.Text.Trim());
            numeroAdequacaoTextBox.Visible = statusComboBox.SelectedIndex == 2 || !string.IsNullOrEmpty(numeroAdequacaoTextBox.Text.Trim()) ;
            tipoAdequacaoComboBox.Visible = statusComboBox.SelectedIndex == 2 || !string.IsNullOrEmpty(numeroAdequacaoTextBox.Text.Trim());
        }

        private void statusComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (tipoAdequacaoComboBox.Visible)
                    tipoAdequacaoComboBox.Focus();
                else
                    descricaoTextBox.Focus();
                return;
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                telasComboBox.Focus();
            }
        }

        private void modulosComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                telasComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                categoriaComboBox.Focus();
            }
        }

        private void telasComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (statusComboBox.Visible)
                    statusComboBox.Focus();
                else
                    descricaoTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                modulosComboBox.Focus();
            }
        }

        private void tipoAdequacaoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                numeroAdequacaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (statusComboBox.Visible)
                    statusComboBox.Focus();
                else
                    telasComboBox.Focus();
            }
        }

        private void modulosComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _idModulo = 0;

            foreach (var item in _listaModulos.Where(w => w.Nome == modulosComboBox.Text))
            {
                _idModulo = item.IdModulo;
            }

            _listaTelas = new TelaBO().Listar(_idModulo);
            telasComboBox.DataSource = _listaTelas.OrderBy(o => o.NomeCompleto).ToList();
            telasComboBox.DisplayMember = "NomeCompleto";

        }

        private void numeroAdequacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                descricaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoAdequacaoComboBox.Focus();
            }
        }

        private void descricaoTextBox_TextChanged(object sender, EventArgs e)
        {
            gravarButton.Enabled = !string.IsNullOrEmpty(descricaoTextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(assuntoTextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(empresaComboBox.Text.Trim()) &&
                !string.IsNullOrEmpty(telasComboBox.Text.Trim()) &&
                !string.IsNullOrEmpty(categoriaComboBox.Text.Trim());

            reabrirButton.Enabled = !string.IsNullOrEmpty(descricaoTextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(assuntoTextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(empresaComboBox.Text.Trim()) &&
                !string.IsNullOrEmpty(telasComboBox.Text.Trim()) &&
                !string.IsNullOrEmpty(categoriaComboBox.Text.Trim()) &&
                reabrirButton.Visible;
        }

        private void descricaoTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                descricaoTextBox.BorderColor = Publicas._bordaEntrada;
            }
            catch
            { }
        }

        private void assuntoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(assuntoTextBox.Text))
            {
                assuntoTextBox.Focus();
                return;
            }
        }

        private void descricaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            descricaoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(descricaoTextBox.Text))
            {
                descricaoTextBox.Focus();
                return;
            }
        }

        private void descricaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            //  concluirToggleButton.Focus();
            Tramites_KeyDown(sender, e);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (assuntoTextBox.Enabled)
                    assuntoTextBox.Focus();
                else
                {
                    if (numeroAdequacaoTextBox.Visible)
                        numeroAdequacaoTextBox.Focus();
                    else
                    {
                        if (statusComboBox.Enabled)
                            statusComboBox.Focus();
                        else
                            telasComboBox.Focus();
                    }
                }
            }
        }
       
        private void LimpaHistoricos(Control controle)
        {
            foreach (Control item in controle.Controls)
            {
                if (item.Name.StartsWith("Historico"))
                {
                    item.Dispose();
                    LimpaHistoricos(controle);
                }
            }

            _listaExibidos.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            _posicaoChat = 6;
        }

        private void anexoPanel_MouseHover(object sender, EventArgs e)
        {
            anexoPanel.BackColor = Color.Gainsboro;
        }

        private void anexoPanel_MouseLeave(object sender, EventArgs e)
        {
            anexoPanel.BackColor = principalPanel.BackColor;
        }

        private void anexoPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void anexoPanel_DragDrop(object sender, DragEventArgs e)
        {
            anexoImagensPanel.Visible = false;
            arquivo = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < arquivo.Length; i++)
            {
                if (arquivo[i].ToString().Length > 100)
                {
                    new Notificacoes.Mensagem("Nome do arquivo muito grande. Renomeio o arquivo!", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }

                if (!arquivo[i].ToString().ToLower().Contains(".pdf") && !arquivo[i].ToString().ToLower().Contains(".txt") &&
                            !arquivo[i].ToString().ToLower().Contains(".jpg") && !arquivo[i].ToString().ToLower().Contains(".jpeg") &&
                            !arquivo[i].ToString().ToLower().Contains(".xls") && !arquivo[i].ToString().ToLower().Contains(".rem") &&
                            !arquivo[i].ToString().ToLower().Contains(".ret") && !arquivo[i].ToString().ToLower().Contains(".png") &&
                            !arquivo[i].ToString().ToLower().Contains(".xml"))
                {
                    new Notificacoes.Mensagem("Extensão do arquivo inválido." + Environment.NewLine +
                        arquivo[i].ToString() +
                        Environment.NewLine + Environment.NewLine +
                        "Extensões permitidas: pdf, png, jpeg, txt, xls E xml"
                        , Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
                listBox1.Items.Add(arquivo[i]);
                CriaPanelsArquivo(arquivo[i]);
            }

            anexoLabel.Text = listBox1.Items.Count.ToString();
        }

        private void abrirTodosArquivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = 0; 

            if (!Directory.Exists(Publicas._caminhoAnexosChamado))
            {// cria o diretorio
                Directory.CreateDirectory(Publicas._caminhoAnexosChamado);
            }

            for (int i = 3; i < anexosContextMenuStrip.Items.Count; i++)
            {
                foreach (var item in _listaAnexos.Where(w => w.NomeArquivo == anexosContextMenuStrip.Items[i].Text))
                {
                    id = listBox2.FindString(item.NomeArquivo);
                    if (id != -1)
                    {
                        if (!SalvaArquivos(item.Anexo, listBox2.Items[id].ToString()))
                            return;

                        System.Diagnostics.Process.Start(Publicas._caminhoAnexosChamado + _chamado.Numero + "_" + listBox2.Items[id].ToString());
                    }
                }
            }            
        }

        private void abrirAnexo_Click(object sender, EventArgs e)
        {
            int id = 0;

            if (!Directory.Exists(Publicas._caminhoAnexosChamado))
            {// cria o diretorio
                Directory.CreateDirectory(Publicas._caminhoAnexosChamado);
            }

            foreach (var item in _listaAnexos.Where(w => w.NomeArquivo == ((ToolStripMenuItem)sender).Text))
            {
                id = listBox2.FindString(item.NomeArquivo);
                if (id != -1)
                {
                    if (!SalvaArquivos(item.Anexo, listBox2.Items[id].ToString()))
                        return;

                    System.Diagnostics.Process.Start(Publicas._caminhoAnexosChamado + _chamado.Numero + "_" + listBox2.Items[id].ToString());
                }
            }
        }

        public bool SalvaArquivos(Byte[] anexo, string nomeArquivo)
        {
            try
            {
                FileStream stream = new FileStream(Publicas._caminhoAnexosChamado + _chamado.Numero + "_" + nomeArquivo, FileMode.Create);
                stream.Write(anexo, 0, anexo.Length);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                new Notificacoes.Mensagem("Problemas ao salvar o(s) anexo(s)." + Environment.NewLine + ex.Message, Publicas.TipoMensagem.Erro);
                return false;
            }            
        }

        private void salvarTodosArquivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = 0; 

            if (!Directory.Exists(Publicas._caminhoAnexosChamado))
            {// cria o diretorio
                Directory.CreateDirectory(Publicas._caminhoAnexosChamado);
            }

            foreach (var item in _listaAnexos)
            {
                id = listBox2.FindString(item.NomeArquivo);
                if (id != -1)
                {
                    if (!SalvaArquivos(item.Anexo, listBox2.Items[id].ToString()))
                        return;
                }
            }

            if (new Notificacoes.Mensagem("Arquivos salvos no caminho abaixo." + Environment.NewLine + 
                "Mostrar a pasta ? " + Environment.NewLine + 
                Publicas._caminhoAnexosChamado, Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
            {
                Publicas.AbrirGerenciadorDeArquivos(Publicas._caminhoAnexosChamado);
            }
        }

        private void anexoPanel_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            anexoImagensPanel.Visible = false;
            openFileDialog1.Title = "Selecione o arquivo a ser anexado.";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] arquivos = openFileDialog1.FileNames;

                if (arquivos.Count() != 0)
                {
                   
                    foreach (var item in arquivos)
                    {
                        if (item.ToString().Length > 100)
                        {
                            new Notificacoes.Mensagem("Nome do arquivo muito grande. Renomeio o arquivo!", Publicas.TipoMensagem.Alerta).ShowDialog();
                            return;
                        }

                        if (!item.ToString().ToLower().Contains(".pdf") && !item.ToString().ToLower().Contains(".txt") && 
                            !item.ToString().ToLower().Contains(".jpg") && !item.ToString().ToLower().Contains(".jpeg") && 
                            !item.ToString().ToLower().Contains(".xls") && !item.ToString().ToLower().Contains(".rem") &&
                            !item.ToString().ToLower().Contains(".ret") && !item.ToString().ToLower().Contains(".png") &&
                            !item.ToString().ToLower().Contains(".xml"))
                        {
                            new Notificacoes.Mensagem("Extensão do arquivo inválido." + Environment.NewLine + 
                                item.ToString() + 
                                Environment.NewLine + Environment.NewLine +
                                "Extensões permitidas: pdf, png, jpeg, txt, xls e xml"
                                , Publicas.TipoMensagem.Alerta).ShowDialog();
                            return;
                        }
                        listBox1.Items.Add(item);
                        CriaPanelsArquivo(item);
                    }

                    anexoLabel.Text = listBox1.Items.Count.ToString();
                }
            }
        }

        private void reabrirButton_Click(object sender, EventArgs e)
        {
            _chamado.Status = Publicas.StatusChamado.Reaberto;
            _chamado.Descricao = descricaoTextBox.Text;

            List<AnexoDoHistorico> _listaAnexosGravar = new List<AnexoDoHistorico>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                AnexoDoHistorico _anexos = new AnexoDoHistorico();

                StreamReader oStreamReader = new StreamReader(listBox1.Items[i].ToString());

                byte[] buffer = new byte[oStreamReader.BaseStream.Length];

                oStreamReader.BaseStream.Read(buffer, 0, buffer.Length);

                oStreamReader.Close();
                oStreamReader.Dispose();

                _anexos.Anexo = buffer;
                _anexos.NomeArquivo = Path.GetFileName(listBox1.Items[i].ToString());
                _listaAnexosGravar.Add(_anexos);
            }

            if (!new ChamadoBO().Gravar(_chamado))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new ChamadoBO().GravarHistorico(new HistoricoDoChamado()
            {
                IdChamado = _chamado.IdChamado,
                Descricao = _chamado.Descricao,
                IdUsuario = Publicas._idUsuario,
                Status = _chamado.Status,
                Usuario = ((Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && _chamado.IdUsuario == Publicas._usuario.Id) ||
                               Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante ? "S" : "A")
            }, _listaAnexosGravar, Publicas._sla))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            LimpaHistoricos(historicoPanel);

            _historicos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, false, false);
            _listaAnexos = new ChamadoBO().ListarAnexos(_chamado.IdChamado);

            foreach (var item in _listaAnexos)
                listBox2.Items.Add(item.NomeArquivo);

            foreach (var item in _historicos)
            {
                if ((Publicas._usuario.Tipo != Publicas.TipoUsuario.Atendente && !item.Privado) ||
                    (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente))
                    CriaPanelHistoricos(item.Descricao, item.Data, (item.Tipo == "S" || item.IdUsuario == _chamado.IdUsuario ? item.Nome : ""), 
                    (item.Tipo == "S" ? "" : item.Nome), false, item.IdHistorico, item.Privado);
            }

            EnviarEmail();

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            
            _log.Descricao = "Reabriu o chamado " + numeroTextBoxExt.Text;
            _log.Tela = "Tramites - Chamado";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            new Notificacoes.Mensagem("Chamado Reaberto.", Publicas.TipoMensagem.Sucesso).ShowDialog();
        }

        private void CriaPanelsArquivo(string arquivo)
        {
            string caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string tipo = "";

            switch (Path.GetExtension(arquivo).ToUpper())
            {
                case ".TXT":
                    tipo = caminho + @"\Imagens\txt.png";
                    break;
                case ".RAR":
                    tipo = caminho + @"\Imagens\rar.png";
                    break;
                case ".EXE":
                    tipo = caminho + @"\Imagens\exe.png";
                    break;
                case ".XLSX":
                    tipo = caminho + @"\Imagens\xlsx.png";
                    break;
                case ".PPT":
                    tipo = caminho + @"\Imagens\ppt.png";
                    break;
                case ".JPG":
                    tipo = caminho + @"\Imagens\jpg.png";
                    break;
                case ".SQL":
                    tipo = caminho + @"\Imagens\sql.png";
                    break;
                case ".ZIP":
                    tipo = caminho + @"\Imagens\zip.png";
                    break;
                case ".HTML":
                    tipo = caminho + @"\Imagens\html.png";
                    break;
                case ".XLS":
                    tipo = caminho + @"\Imagens\xls.png";
                    break;
                case ".XML":
                    tipo = caminho + @"\Imagens\xml.png";
                    break;
                case ".DOC":
                    tipo = caminho + @"\Imagens\doc.png";
                    break;
                case ".PDF":
                    tipo = caminho + @"\Imagens\pdf.png";
                    break;
                case ".PNG":
                    tipo = caminho + @"\Imagens\png.png";
                    break;
                default:
                    tipo = caminho + @"\Imagens\File.png";
                    break;
            }

            if (_qtd == 0)
            {
                arquivoPanel.Location = new Point(arquivoPanel.Left, _topArquivos);
                nomeArquivoLabel.Text = Path.GetFileNameWithoutExtension(arquivo);
                removerLabel.Tag = nomeArquivoLabel.Text;
                imagemArquivoPictureBox.ImageLocation = tipo;
                arquivoPanel.Visible = true;
            }
            else
            {
                Panel panel = new Panel();
                panel.Name = "arquivoPanel" + _qtd.ToString();

                Label labelNomeArquivo = new Label();
                Label labelRemover = new Label();
                PictureBox imagem = new PictureBox();

                panel.Controls.Add(imagem);
                panel.Controls.Add(labelNomeArquivo);
                panel.Controls.Add(labelRemover);

                imagem.Size = imagemArquivoPictureBox.Size;
                imagem.Location = imagemArquivoPictureBox.Location;
                imagem.Image = imagemArquivoPictureBox.Image;
                imagem.SizeMode = PictureBoxSizeMode.StretchImage;
                imagem.ImageLocation = tipo;

                panel.Size = arquivoPanel.Size;
                panel.Location = new Point(_leftArquivos, _topArquivos);

                labelNomeArquivo.ForeColor = nomeArquivoLabel.ForeColor;
                labelNomeArquivo.Font = nomeArquivoLabel.Font;
                labelNomeArquivo.TextAlign = ContentAlignment.MiddleCenter;
                labelNomeArquivo.Text = Path.GetFileNameWithoutExtension(arquivo);
                labelNomeArquivo.AutoSize = false;
                labelNomeArquivo.Site = nomeArquivoLabel.Site;
                labelNomeArquivo.Location = nomeArquivoLabel.Location;

                labelRemover.TextAlign = ContentAlignment.MiddleCenter;
                labelRemover.AutoSize = false;
                labelRemover.Size = removerLabel.Size;
                labelRemover.ForeColor = removerLabel.ForeColor;
                labelRemover.Font = removerLabel.Font;
                labelRemover.Location = removerLabel.Location;
                labelRemover.Text = removerLabel.Text;
                labelRemover.Tag = labelNomeArquivo.Text;

                labelRemover.Click += new System.EventHandler(this.removerLabel_Click);
                labelRemover.MouseLeave += new System.EventHandler(this.removerLabel_MouseLeave);
                labelRemover.MouseHover += new System.EventHandler(this.removerLabel_MouseHover);

                panel.Visible = true;
                anexoImagensPanel.Controls.Add(panel);

                panel.BringToFront();

            }
            _qtdleft++;

            if (_qtdleft > 4)
            {
                _topArquivos = _topArquivos + (arquivoPanel.Height + 5);
                _leftArquivos = 6;
                _qtdleft = 0;
            }
            else
                _leftArquivos = _leftArquivos + (arquivoPanel.Width + 5);

            Refresh();
            _qtd++;
        }

        private void removerLabel_Click(object sender, EventArgs e)
        {
            _qtd = 0;
            _qtdleft = 0;
            Control[] controle;
            string nome = "";
            string arquivo = ((Label)sender).Tag.ToString();

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                nome = "arquivoPanel" + (i == 0 ? "" : i.ToString());

                controle = this.Controls.Find(nome, true);

                if (i == 0)
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

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString().Contains(arquivo))
                {
                    nome = listBox1.Items[i].ToString();
                    break;
                }
            }
            listBox1.Items.Remove(nome);

            _topArquivos = 33;
            _leftArquivos = 6;
            anexoImagensPanel.Visible = false;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                CriaPanelsArquivo(listBox1.Items[i].ToString());
            }

            anexoImagensPanel.Visible = true;
            anexoLabel.Text = listBox1.Items.Count.ToString();
        }

        private void removerLabel_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Hand;
        }

        private void removerLabel_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Default;
        }

        private void verPictureBox_Click(object sender, EventArgs e)
        {
            anexoImagensPanel.Visible = !anexoImagensPanel.Visible;
            anexoImagensPanel.Location = new Point(323, 184);
        }

        private void fechaAnexosPictureBox_Click(object sender, EventArgs e)
        {
            anexoImagensPanel.Visible = false;
        }

        private void CriarNovoChamadoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(descricaoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe a descrição para o novo chamado.", Publicas.TipoMensagem.Erro).ShowDialog();
                descricaoTextBox.Focus();
                return;
            }

            CriarNovoChamadoButton.Enabled = false;


            if (new Notificacoes.Mensagem("Deseja associar esse chamado a um novo ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;
            
            foreach (var item in _listaTelas.Where(w => w.NomeCompleto == telasComboBox.Text))
                _idTela = item.IdTela;

            Chamado _chamadoNovo = new Chamado();
            _chamadoNovo.Assunto = assuntoTextBox.Text;
            _chamadoNovo.Descricao = (descricaoTextBox.Text == descricaoTextBox.Text.ToUpper() ?
                                             CultureInfo.CurrentCulture.TextInfo.ToTitleCase(descricaoTextBox.Text.Trim().ToLower()) :
                                             descricaoTextBox.Text.Trim());

            _chamadoNovo.IdCategoria = _chamado.IdCategoria;
            _chamadoNovo.IdUsuario = Publicas._idUsuario;


            if (_chamado.IdTela == 0 && telasComboBox.Text != "")
                _chamadoNovo.IdTela = _idTela;
            else
                if (_chamado.IdTela != 0)
                _chamadoNovo.IdTela = _chamado.IdTela;

            _chamadoNovo.IdEmpresa = _idEmpresa;
            _chamadoNovo.IdChamadoAssociado = _chamado.IdChamado;
            _chamado.IdEmpresaSolicitante = _usuario.IdEmpresa;
                        
            _chamadoNovo.Origens = (origemComboBox.SelectedIndex == 0 ? Publicas.Origem.OnLine :
                               (origemComboBox.SelectedIndex == 1 ? Publicas.Origem.Email : Publicas.Origem.Telefone));
            _chamadoNovo.Status = Publicas.StatusChamado.Novo;
            _chamadoNovo.Prioridade = (prioridadeComboBox.SelectedIndex == 0 ? Publicas.Prioridades.Critico :
                                  (prioridadeComboBox.SelectedIndex == 1 ? Publicas.Prioridades.Alta :
                                  (prioridadeComboBox.SelectedIndex == 2 ? Publicas.Prioridades.Media : Publicas.Prioridades.Baixa)));
            _chamadoNovo.Tipo = (tipoChamadoComboBox.SelectedIndex == 0 ? Publicas.TipoChamado.Erro :
                            (tipoChamadoComboBox.SelectedIndex == 1 ? Publicas.TipoChamado.Duvida :
                            (tipoChamadoComboBox.SelectedIndex == 2 ? Publicas.TipoChamado.Implementacao :
                            (tipoChamadoComboBox.SelectedIndex == 3 ? Publicas.TipoChamado.Acesso :
                            (tipoChamadoComboBox.SelectedIndex == 4 ? Publicas.TipoChamado.Ajustes :
                            (tipoChamadoComboBox.SelectedIndex == 5 ? Publicas.TipoChamado.Projeto :
                            Publicas.TipoChamado.Solicitacao ))))));

            _chamadoNovo.Numero = new ChamadoBO().ProximoCodigo(Publicas.TipoCalculoChamado.AnoMes, "-");

            if (!new ChamadoBO().Gravar(_chamadoNovo))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            DateTime _data = _historicos.Min(m => m.Data);

            //string descricao = "";


            List<AnexoDoHistorico> _listaAnexosGravar = new List<AnexoDoHistorico>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                AnexoDoHistorico _anexos = new AnexoDoHistorico();

                StreamReader oStreamReader = new StreamReader(listBox1.Items[i].ToString());

                byte[] buffer = new byte[oStreamReader.BaseStream.Length];

                oStreamReader.BaseStream.Read(buffer, 0, buffer.Length);

                oStreamReader.Close();
                oStreamReader.Dispose();

                _anexos.Anexo = buffer;
                _anexos.NomeArquivo = Path.GetFileName(listBox1.Items[i].ToString());
                _listaAnexosGravar.Add(_anexos);
            }

            if (!new ChamadoBO().GravarHistorico(new HistoricoDoChamado()
            {
                IdChamado = Publicas._idChamado,
                Descricao = _chamadoNovo.Descricao,
                IdUsuario = _usuario.Id,
                Status = _chamadoNovo.Status,
                Usuario = "S"
            }, _listaAnexosGravar, Publicas._sla))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }
            
            new Notificacoes.Mensagem("Processo finalizado." + Environment.NewLine +
            "Aberto chamado nº " + _chamadoNovo.Numero + ".", Publicas.TipoMensagem.Sucesso).ShowDialog();
            
            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Abriu o chamado " + _chamadoNovo.Numero;
            _log.Tela = "Tramites - Chamado";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            
            #region EnviaEmail
            string[] _dadosEmail = new string[50];

            //Localizar atendentes para o chamado. 
            string _emailDestino = "";
            _listaUsuarios = new UsuarioBO().ConsultaAtendentesParaACategoria(_idCategoria);
            string[] nome = Publicas._usuario.Nome.Split(' ');

            foreach (var item in _listaUsuarios)
            {
                if (item.IdEmpresa == _usuario.IdEmpresa || item.IdEmpresa == 19 || item.UsuarioAcesso == "ELSILVA")
                {
                    if (!_emailDestino.Contains(item.Email))
                        _emailDestino = _emailDestino + item.Email + "; ";
                }
            }
            _emailDestino = _emailDestino.Substring(0, _emailDestino.Length - 2);

            _dadosEmail[0] = "Abertura de chamado " + _chamadoNovo.Numero;
            _dadosEmail[2] = Publicas._usuario.Nome.Split(' ')[0];

            _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            _dadosEmail[3] = empresaComboBox.Text.Substring(10, empresaComboBox.Text.Length - 10);
            _dadosEmail[4] = _chamadoNovo.Numero;

            _dadosEmail[5] = "";
            _dadosEmail[6] = assuntoTextBox.Text;
            _dadosEmail[7] = categoriaComboBox.Text;
            _dadosEmail[8] = modulosComboBox.Text;
            _dadosEmail[9] = telasComboBox.Text;
            _dadosEmail[10] = tipoChamadoComboBox.Text;
            _dadosEmail[11] = prioridadeComboBox.Text;
            _dadosEmail[12] = _chamadoNovo.Descricao;
            _dadosEmail[13] = anexoLabel.Text;
            _dadosEmail[14] = _parametro.PrazoRetorno.ToString();
            
            if (Publicas._usuario.AssinaturaChamado == "")
            {
                _dadosEmail[16] = "<p style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 13px'/> Atenciosamente,</br> " +
                               nome[0] + " " + nome[nome.Length - 1] +
                               "</font></p>";
            }
            else
            {
                _dadosEmail[16] = "<p style = 'color: #4169e1; font-family: arial, sans-serif; font-size: 13px'/>" +
                    Publicas._usuario.AssinaturaChamado + "</font></p>";
            }

            if (!Publicas.EnviarEmailChamado(_dadosEmail, false, true, false, _emailDestino,
                                                _dadosEmail[0]))
            {
                new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!Publicas.EnviarEmailChamado(_dadosEmail, false, false, false, Publicas._usuario.Email,
                                                _dadosEmail[0]))
            {
                new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            #endregion

            Close();
        }

        private void categoriaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void modulosComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_listaTelas = new TelaBO().Listar(_idModulo);
            //telasComboBox.DataSource = _listaTelas.Where(w => w.Ativo)
            //                                      .OrderBy(o => o.NomeCompleto).ToList();
            //telasComboBox.DisplayMember = "NomeCompleto";
        }

        private void TelaLabel_Click(object sender, EventArgs e)
        {
            if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                new Cadastros.Telas().ShowDialog();
        }

        private void ModuloLabel_Click(object sender, EventArgs e)
        {
            if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                new Cadastros.Modulos().ShowDialog();
        }

        private void CategoriaLabel_Click(object sender, EventArgs e)
        {
            if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                new Cadastros.Categorias().ShowDialog();
        }

        private void SolicitarAutorizacaoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SolicitarAutorizacaoCheckBox.Checked)
            {
                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                {
                    AutorizacaoPanel.Size = new Size(708, 85);
                    AutorizarButton.Location = new Point(613,44);
                    AutorizarButton.Text = "Solicitar";
                    UsuarioAutorizacaoTextBox.Enabled = true;
                    UsuarioAutorizacaoTextBox.Focus();
                }
                AutorizacaoPanel.Location = new Point(163, 186);
                AutorizacaoPanel.Visible = true;
            }
        }

        private void UsuarioAutorizacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                    AutorizarButton.Focus();
                else
                    AutorizadoToggleButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                UsuarioAutorizacaoTextBox.Focus();
            }
        }

        private void AutorizadoToggleButton_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                AguardarToggleButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                UsuarioAutorizacaoTextBox.Focus();
            }
        }

        private void AguardarToggleButton_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                MotivoAutorizacaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AutorizadoToggleButton.Focus();
            }
        }

        private void MotivoAutorizacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Tramites_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                AutorizarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AguardarToggleButton.Focus();
            }
        }

        private void UsuarioAutorizacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                UsuarioAutorizacaoTextBox.BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (UsuarioAutorizacaoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                UsuarioAutorizacaoTextBox.Text = Publicas._usuarioAcesso;

                if (UsuarioAutorizacaoTextBox.Text.Trim() == "")
                {
                    UsuarioAutorizacaoTextBox.Focus();
                    return;
                }
            }

            _usuarioAutorizador = new UsuarioBO().Consultar(UsuarioAutorizacaoTextBox.Text);

            if (!_usuarioAutorizador.Existe)
            {
                new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                UsuarioAutorizacaoTextBox.Focus();
                return;
            }

            if (!_usuarioAutorizador.Ativo)
            {
                new Notificacoes.Mensagem("Usuário inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                UsuarioAutorizacaoTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _usuarioAutorizador.Nome;
            AutorizarButton.Enabled = true;
        }

        private void AutorizadoToggleButton_ToggleStateChanged(object sender, ToggleStateChangedEventArgs e)
        {
            AguardarToggleButton.Visible = AutorizadoToggleButton.ToggleState != ToggleButtonState.Active;
            label2.Visible = AutorizadoToggleButton.ToggleState != ToggleButtonState.Active;
        }

        private void AutorizarButton_Click(object sender, EventArgs e)
        {
            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Tela = "Tramites - Chamado";

            if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && Publicas._usuario.Id != _usuarioAutorizador.Id)
            {
                _log.Descricao = "Solicitou Autorização para o chamado " + numeroTextBoxExt.Text;

                _chamado.Status = Publicas.StatusChamado.AguardandoAutorizacao;
                if (!new ChamadoBO().Gravar(_chamado))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
                if (!new ChamadoBO().GravarHistorico(new HistoricoDoChamado()
                {
                    IdChamado = _chamado.IdChamado,
                    Descricao = "Solicito sua autorização para atender este chamado."
                      + Environment.NewLine
                      + Environment.NewLine
                      + Publicas._usuario.AssinaturaChamado,
                    IdUsuario = Publicas._idUsuario,
                    Status = Publicas.StatusChamado.AguardandoAutorizacao,
                    IdUsuarioAutorizacao = _usuarioAutorizador.Id,
                    Usuario = "A"
                }, new List<Classes.AnexoDoHistorico>(), Publicas._sla))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }
            else
            {

                if (!NaoAutorizadoRadioButton.Checked && !AutorizadoRadioButton.Checked)
                {
                    new Notificacoes.Mensagem("Selecione uma opção 'Autorizado' ou 'Não Autorizado'.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    AutorizadoRadioButton.Focus();
                    return;
                }

                if (NaoAutorizadoRadioButton.Checked && !AguardarRadioButton.Checked && !NaoAguardarRadioButton.Checked)
                {
                    new Notificacoes.Mensagem("Selecione uma opção 'Aguardar' ou 'Não Aguardar'.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    AguardarRadioButton.Focus();
                    return;
                }

                //if (AutorizadoToggleButton.ToggleState == ToggleButtonState.Inactive && String.IsNullOrEmpty(MotivoAutorizacaoTextBox.Text.Trim()))
                if (NaoAutorizadoRadioButton.Checked && String.IsNullOrEmpty(MotivoAutorizacaoTextBox.Text.Trim()))
                {
                    new Notificacoes.Mensagem("Informe o motivo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    MotivoAutorizacaoTextBox.Focus();
                    return;
                }

                _log.Descricao = (NaoAutorizadoRadioButton.Checked ? "Não " : "") +
                    "Autorizou a solicitação do chamado " + numeroTextBoxExt.Text;

                if (NaoAutorizadoRadioButton.Checked && AguardarRadioButton.Checked)
                    _chamado.Status = Publicas.StatusChamado.AguardandoAutorizacao;
                else
                    _chamado.Status = Publicas.StatusChamado.EmAndamento;

                if (!new ChamadoBO().Gravar(_chamado))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                if (!new ChamadoBO().GravarHistorico(new HistoricoDoChamado()
                {
                    IdChamado = _chamado.IdChamado,
                    Descricao = (AutorizadoRadioButton.Checked ? "Autorizado!" : "Não Autorizado!")
                      + Environment.NewLine
                      + Environment.NewLine
                      + MotivoAutorizacaoTextBox.Text
                      + Environment.NewLine
                      + Environment.NewLine
                      + Publicas._usuario.AssinaturaChamado,
                    IdUsuario = Publicas._idUsuario,
                    Status = _chamado.Status,
                    IdUsuarioAutorizacao = _usuarioAutorizador.Id,
                    Autorizado = AutorizadoRadioButton.Checked,
                    AguardarAutorizado = AguardarRadioButton.Checked,
                    RespondeuAutorizacao = _chamado.Status == Publicas.StatusChamado.EmAndamento,
                    Usuario = "S"

                }, new List<Classes.AnexoDoHistorico>(), Publicas._sla))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

            }

            AutorizarEmail();

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
        }

        private void SolicitarAcompanhamentoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SolicitarAcompanhamentoCheckBox.Checked)
            {
                UsuarioConvidadoTextBox.Enabled = true;
                UsuarioConvidadoTextBox.Focus();
                UsuarioConvidadoTextBox.Visible = true;
            }
            UsuarioConvidadoPanel.Visible = SolicitarAcompanhamentoCheckBox.Checked;
        }

        private void UsuarioConvidadoTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                UsuarioConvidadoTextBox.BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                SolicitarAcompanhamentoCheckBox.Checked = UsuarioConvidadoTextBox.Text.Trim() != "";
                return;
            }

            if (UsuarioConvidadoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                UsuarioConvidadoTextBox.Text = Publicas._usuarioAcesso;

                if (UsuarioConvidadoTextBox.Text.Trim() == "")
                {
                    UsuarioConvidadoTextBox.Focus();
                    return;
                }
            }

            _usuarioConvidado = new UsuarioBO().Consultar(UsuarioConvidadoTextBox.Text);

            if (!_usuarioConvidado.Existe)
            {
                new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                UsuarioConvidadoTextBox.Focus();
                return;
            }

            if (!_usuarioConvidado.Ativo)
            {
                new Notificacoes.Mensagem("Usuário inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                UsuarioConvidadoTextBox.Focus();
                return;
            }

            if (_usuarioConvidado.IdDepartamento == _usuario.IdDepartamento && _usuario.IdEmpresa == _usuarioConvidado.IdEmpresa && _usuario.IdDepartamento != 26)
            {
                new Notificacoes.Mensagem("Usuário não pode ser do mesmo departamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                UsuarioConvidadoTextBox.Focus();
                return;
            }

            if (_chamado.Status == Publicas.StatusChamado.Finalizado)
            {
                _chamado.IdUsuarioAcompanhamento = _usuarioConvidado.Id;

                if (!new ChamadoBO().Gravar(_chamado))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                string[] _dadosEmail = new string[50];
                
                _ultimoHistoricos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, true, true);
                string _emailDestino = "";
                string[] nome = Publicas._usuario.Nome.Split(' ');

                _emailDestino = _usuarioConvidado.Email + "; ";
                                
                _dadosEmail[2] = nome[0];
                
                _emailDestino = _emailDestino.Substring(0, _emailDestino.Length - 2);

                _dadosEmail[0] = (_chamado.Status == Publicas.StatusChamado.Finalizado ? "Conclusão" :
                                 (_chamado.Status == Publicas.StatusChamado.Reaberto ? "Reabertura" : "Alteração"))
                                + " do Chamado " + _chamado.Numero;

                _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                _dadosEmail[3] = empresaComboBox.Text.Substring(10, empresaComboBox.Text.Length - 10);
                _dadosEmail[4] = _chamado.Numero;

                _dadosEmail[5] = (_chamado.Status == Publicas.StatusChamado.Finalizado ? "Finalizado" :
                                 (_chamado.Status == Publicas.StatusChamado.Reaberto ? "Reaberto" : "Respondido"));
                _dadosEmail[6] = assuntoTextBox.Text;
                _dadosEmail[7] = categoriaComboBox.Text;
                _dadosEmail[8] = modulosComboBox.Text;
                _dadosEmail[9] = telasComboBox.Text;
                _dadosEmail[10] = tipoChamadoComboBox.Text;
                _dadosEmail[11] = prioridadeComboBox.Text;
                _dadosEmail[13] = anexoLabel.Text;
                _dadosEmail[14] = _parametro.PrazoRetorno.ToString();

                if (Publicas._usuario.AssinaturaChamado == "")
                {
                    _dadosEmail[16] = "<p style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 13px'/> Atenciosamente,</br> " +
                                   nome[0] + " " + nome[nome.Length - 1] +
                                   "</font></p>";
                }
                else
                {
                    _dadosEmail[16] = "<p style = 'color: #4169e1; font-family: arial, sans-serif; font-size: 13px'/>" +
                        Publicas._usuario.AssinaturaChamado + "</font></p>";
                }

                if (_usuarioConvidado != null)
                {
                    _dadosEmail[12] = " Você foi convidado para acompanhar neste chamado. </br></br>" + "Por ele estar finalizado, filtre para mostrar os finalizados que ele aparecerá na sua lista";

                    if (!Publicas.EnviarEmailChamado(_dadosEmail, true, false, false, _usuarioConvidado.Email, _dadosEmail[0], false, ""))
                    {
                        new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }
                }

            }
        }

        private void Tramites_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Publicas.AbrirFerramentaDeCapitura();
        }

        private void UsuarioAutorizacaoTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                UsuarioAutorizacaoTextBox.BorderColor = Publicas._bordaEntrada;
            }
            catch
            { }

        }

        private void MotivoAutorizacaoTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                MotivoAutorizacaoTextBox.BorderColor = Publicas._bordaEntrada;
            }
            catch
            { }

        }

        private void UsuarioConvidadoTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                UsuarioConvidadoTextBox.BorderColor = Publicas._bordaEntrada;
            }
            catch
            { }

        }

        private void MotivoAutorizacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                MotivoAutorizacaoTextBox.BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void assuntoTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                assuntoTextBox.BorderColor = Publicas._bordaEntrada;
            }
            catch
            { }

        }

        private void AutorizacaoPanel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = ((Panel)sender).ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle((Publicas._TemaBlack ? Pens.Silver : Pens.Navy), rect);
        }

        private void NaoAutorizadoRadioButton_CheckChanged(object sender, EventArgs e)
        {
            AguardarPanel.Visible = NaoAutorizadoRadioButton.Checked;
            label2.Visible = NaoAutorizadoRadioButton.Checked;
        }

        private void UsuarioConvidadoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                SolicitarAcompanhamentoCheckBox.Focus();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = _listaTemposExecucao;
            ListaTemposExecutadosPanel.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ListaTemposExecutadosPanel.Visible = false;
        }
    }
}
