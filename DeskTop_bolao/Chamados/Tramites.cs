using Classes;
using Negocio;
using Suportte.Notificacoes;
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
            }
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
        Usuario _usuario;
        Chamado _chamado;
        Tela _tela;
        string[] arquivo;

        int _idModulo = 0;
        int _idCategoria = 0;
        int _idTela = 0;
        int _idEmpresa = 0;
        int _posicaoChat = 6;
        int _heigthMinimo = 85;
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
            StyledRenderer styleRenderer1 = new StyledRenderer();
            concluirToggleButton.Renderer = styleRenderer1;

            // Trazer as empresas que o usuário esta autorizado.
            _listaEmpresas = new EmpresaBO().Listar();
            empresaComboBox.DataSource = _listaEmpresas.Where(w => w.Ativo)
                                                       .OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBox.DisplayMember = "CodigoeNome";

            // Trazer as categorias que o usuário está autorizado
            _listaCategorias = new CategoriaBO().Listar();
            categoriaComboBox.DataSource = _listaCategorias.Where(w => w.Ativo)
                                                           .OrderBy(o => o.Descricao).ToList();
            categoriaComboBox.DisplayMember = "Descricao";

            tipoAdequacaoComboBox.Items.AddRange(new object[] { "SIM", "PSE" });
            tipoAdequacaoComboBox.SelectedIndex = 0;

            statusComboBox.Items.AddRange(new object[] { "Em Andamento", "Pendente solicitante", "Aguardando adequação", "Cancelado"});
            statusComboBox.SelectedIndex = 0;

            _parametro = new ParametrosBO().Consultar();

            // trazer o Tipo De chamados.
            tipoChamadoComboBox.Items.AddRange(new object[] { "Erro", "Dúvidas", "Implementação", "Acesso" });
            tipoChamadoComboBox.SelectedIndex = 1;

            // trazer a prioridades
            prioridadeComboBox.Items.AddRange(new object[] { "Crítico", "Alta", "Media", "Baixa" });
            prioridadeComboBox.SelectedIndex = 2;

            // trazer a origem
            origemComboBox.Items.AddRange(new object[] { "Chamado", "E-mail", "Telefone" });
            origemComboBox.SelectedIndex = 0;
            origemComboBox.Enabled = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente ||
            Publicas._usuario.Tipo == Publicas.TipoUsuario.Todos;

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

            #region dados solicitante
            _usuario = new UsuarioBO().ConsultarPorId(_chamado.IdUsuario);
            nomeUsuarioTextBox.Text = _usuario.Nome;
            ipMaquinaTextBox.Text = _usuario.IpMaquina;
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


            if (_chamado.Status == Publicas.StatusChamado.Finalizado)
            {
                statusComboBox.Items.Add("Finalizado");
                statusComboBox.SelectedIndex = 4;
            }
            else
            statusComboBox.SelectedIndex = (_chamado.Status == Publicas.StatusChamado.EmAndamento ? 0 :
                (_chamado.Status == Publicas.StatusChamado.Pendente ? 1 :
                (_chamado.Status == Publicas.StatusChamado.Adequacao ? 2 :
                (_chamado.Status == Publicas.StatusChamado.Novo ? 0 :
                3))));

            prioridadeComboBox.SelectedIndex = (_chamado.Prioridade == Publicas.Prioridades.Critico ? 0 :
                (_chamado.Prioridade == Publicas.Prioridades.Alta ? 1 :
                (_chamado.Prioridade == Publicas.Prioridades.Media ? 2 : 3)));

            origemComboBox.SelectedIndex = (_chamado.Origens == Publicas.Origem.OnLine ? 0 :
                (_chamado.Origens == Publicas.Origem.Email ? 1 :
                (_chamado.Origens == Publicas.Origem.Chat ? 3 :
                2)));

            tipoChamadoComboBox.SelectedIndex = (_chamado.Tipo == Publicas.TipoChamado.Erro ? 0 :
                (_chamado.Tipo == Publicas.TipoChamado.Duvida ? 1 :
                (_chamado.Tipo == Publicas.TipoChamado.Acesso ? 3 :
                2)));

            if (! string.IsNullOrEmpty(_chamado.Adequacao))
            {
                tipoAdequacaoComboBox.SelectedIndex = (_chamado.Adequacao.Contains("SIM") ? 0 : 1);
                numeroAdequacaoTextBox.Text = _chamado.Adequacao.Replace("SIM ", "").Replace("PSE ", "");
            }

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

            _historicos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, false);

            _listaAnexos = new ChamadoBO().ListarAnexos(_chamado.IdChamado);

            foreach (var item in _listaAnexos)
                listBox2.Items.Add(item.NomeArquivo);

            foreach (var item in _historicos)
            {
                CriaPanelHistoricos(item.Descricao, item.Data, (item.Tipo == "S" || item.IdUsuario == _chamado.IdUsuario ? item.Nome : ""), (item.Tipo == "S" ? "" : item.Nome), false, item.IdHistorico);
            }

            statusComboBox.Visible = true;
            statusComboBox.Enabled = (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente || 
                                      Publicas._usuario.Tipo == Publicas.TipoUsuario.Todos) &&
                                      _chamado.Status != Publicas.StatusChamado.Finalizado;

            if (_chamado.IdUsuario == Publicas._idUsuario)
            {
                if (statusComboBox.SelectedIndex != 2 && statusComboBox.SelectedIndex != 4) // Aguardando adequação e finalizado não pode mudar o status 
                    statusComboBox.SelectedIndex = 0;
                statusComboBox.Enabled = false;
            }
                        
            tipoChamadoComboBox.Enabled = _chamado.Status != Publicas.StatusChamado.Finalizado;
            prioridadeComboBox.Enabled = _chamado.Status != Publicas.StatusChamado.Finalizado;
            origemComboBox.Enabled = _chamado.Status != Publicas.StatusChamado.Finalizado;
            categoriaComboBox.Enabled = _chamado.Status != Publicas.StatusChamado.Finalizado;
            modulosComboBox.Enabled = _chamado.Status != Publicas.StatusChamado.Finalizado;
            telasComboBox.Enabled = _chamado.Status != Publicas.StatusChamado.Finalizado;
            concluirLabel.Visible = _chamado.Status != Publicas.StatusChamado.Finalizado;
            concluirToggleButton.Visible = _chamado.Status != Publicas.StatusChamado.Finalizado;
            
            avaliacaoRatingControl.Visible = _chamado.Status == Publicas.StatusChamado.Finalizado;
            gravarButton.Visible = _chamado.Status != Publicas.StatusChamado.Finalizado;

            reabrirButton.Visible = _chamado.Status == Publicas.StatusChamado.Finalizado; // ver prazo
            reabrirButton.Enabled = _chamado.DataRetorno.AddDays(Publicas._prazoReabrir).Date >= DateTime.Now.Date;
            prazoLabel.Visible = !reabrirButton.Enabled && reabrirButton.Visible;

            avaliacaoRatingControl.ToolTipSettings.Body.Text = _chamado.DescricaoAvaliacao;
            avaliacaoRatingControl.Value = (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Ruim ? 1 :
                (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Bom ? 2 :
                (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Regular ? 3 :
                (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.MuitoBom ? 4 :
                (_chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Excelente ? 5 : 0)))));

            if (_chamado.Status == Publicas.StatusChamado.Finalizado &&
                _chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.SemAvaliacao &&
                _chamado.IdUsuario == Publicas._idUsuario)
            {
                Publicas._chamado = _chamado;
                new Chamados.Avaliacao().ShowDialog();
            }

        }

        private void categoriaComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
                return;
            }

            _idCategoria = 0;

            foreach (var item in _listaCategorias.Where(w => w.Descricao == categoriaComboBox.Text))
            {
                _idCategoria = item.IdCategoria;
            }

            modulosComboBox.DataSource = _listaModulos.Where(w => w.IdCategoria == _idCategoria &&
                                                                  w. Ativo)
                                                      .OrderBy(o => o.Nome).ToList();
            modulosComboBox.DisplayMember = "Nome";

            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void categoriaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
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
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                prioridadeComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoChamadoComboBox.Focus();
            }
        }

        private void CriaPanelHistoricos(string mensagem, DateTime data, string nomeSolicitante, string nomeResponsavel, bool lida, int idHistorico)
        {
            int QtdAnexos = 0;

            Panel panel = new Panel();
            panel.Size = new Size(685, _heigthMinimo);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Name = "Historico" + _posicaoChat;
            
            Panel panel2 = new Panel();
            panel2.Size = new Size(21, 20);

            Panel panel3 = new Panel();
            panel3.Size = new Size(21, 20);
            panel3.Dock = DockStyle.Bottom;
            panel3.ContextMenuStrip = anexosContextMenuStrip;
            panel3.Name = "panelAnexos_" + idHistorico.ToString();

            Label lbl = new Label();
            lbl.Size = new Size(359, 20);
            lbl.Dock = DockStyle.Left;
            lbl.Font = new Font("Century Gothic", 8);
            lbl.ForeColor = Publicas._bordaEntrada;
            lbl.Text = "" ;
            
            TextBoxExt lblAnexo = new TextBoxExt();
            lblAnexo.Dock = DockStyle.Left;
            lblAnexo.Style = TextBoxExt.theme.Metro;
            lblAnexo.ThemesEnabled = false;
            lblAnexo.BorderStyle = BorderStyle.None;
            lblAnexo.Size = new Size(126, 16);
            lblAnexo.Font = new Font("Century Gothic", 8, FontStyle.Underline);
            lblAnexo.ForeColor = Publicas._bordaEntrada;
            lblAnexo.TextAlign = HorizontalAlignment.Left;
            lblAnexo.NearImage = clipAnexoPictureBox.Image;
            lblAnexo.TabStop = false;
            lblAnexo.ReadOnly = true;
            QtdAnexos = _listaAnexos.Where(w => w.IdHistorico == idHistorico).Count();
            lblAnexo.Text = " Anexos: " + QtdAnexos.ToString();
            lblAnexo.Name = "labelAnexos_" + idHistorico.ToString();
            lblAnexo.ContextMenuStrip = anexosContextMenuStrip;
            lblAnexo.Click += new System.EventHandler(lblAnexo_Click);
            lblAnexo.MouseLeave += new System.EventHandler(lblAnexo_MouseLeave);
            lblAnexo.MouseHover += new System.EventHandler(lblAnexo_MouseHover);

            panel3.Visible = QtdAnexos != 0;
            panel3.Controls.Add(lblAnexo);

            TextBoxExt dataTxt = new TextBoxExt();
            dataTxt.Dock = DockStyle.Right;
            dataTxt.Style = TextBoxExt.theme.Metro;
            dataTxt.ThemesEnabled = false;
            dataTxt.BorderStyle = BorderStyle.None;
            dataTxt.Size = new Size(126, 16);
            dataTxt.Font = new Font("Century Gothic", 8);
            dataTxt.ForeColor = Publicas._bordaEntrada;
            dataTxt.TextAlign = HorizontalAlignment.Right;
            dataTxt.NearImage = Properties.Resources.Calendario_Claro;
            dataTxt.TabStop = false;
            dataTxt.ReadOnly = true;

            RichTextBox text = new RichTextBox();
            text.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            text.ForeColor = Color.Black;
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
                    
            if (nomeSolicitante != null && !string.IsNullOrEmpty(nomeSolicitante.Trim()))
            {
                lbl.Text = nomeSolicitante;
                dataTxt.Text = data.ToShortDateString() + " " + data.ToShortTimeString() + " ";
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                panel.Location = new Point(30, _posicaoChat);
                historicoPanel.Controls.Add(panel);
            }
            else
            {
                lbl.Text = nomeResponsavel;
                dataTxt.Text = data.ToShortDateString() + " " + data.ToShortTimeString() + " ";
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                panel.Location = new Point(10, _posicaoChat);
                historicoPanel.Controls.Add(panel);
            }

            dataTxt.ForeColor = Publicas._bordaEntrada;
            text.BringToFront();
            
            _posicaoChat = _posicaoChat + (panel.Height != _heigthMinimo ? panel.Height : _heigthMinimo) + 4;

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
            foreach (var item in _listaTelas.Where(w => w.NomeCompleto == telasComboBox.Text))
                _idTela = item.IdTela;

            _chamado.IdCategoria = _idCategoria;
            _chamado.IdTela = _idTela;
            _chamado.Assunto = assuntoTextBox.Text;
            _chamado.Descricao = descricaoTextBox.Text.Trim();

            _chamado.Tipo = (tipoChamadoComboBox.SelectedIndex == 0 ? Publicas.TipoChamado.Erro :
                            (tipoChamadoComboBox.SelectedIndex == 1 ? Publicas.TipoChamado.Duvida : Publicas.TipoChamado.Implementacao));

            if (concluirToggleButton.ToggleState == ToggleButtonState.Active)
                _chamado.Status = Publicas.StatusChamado.Finalizado;
            else
            {
                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante && _chamado.Status == Publicas.StatusChamado.Novo)
                    _chamado.Status = Publicas.StatusChamado.Novo;
                else
                    _chamado.Status = (statusComboBox.SelectedIndex == 0 ? Publicas.StatusChamado.EmAndamento :
                                  (statusComboBox.SelectedIndex == 1 ? Publicas.StatusChamado.Pendente :
                                  (statusComboBox.SelectedIndex == 2 ? Publicas.StatusChamado.Adequacao : Publicas.StatusChamado.Cancelado)));
            }

            if (adequacaoLabel.Visible && string.IsNullOrEmpty(numeroAdequacaoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o número da adequação.", Publicas.TipoMensagem.Alerta).ShowDialog();
                numeroAdequacaoTextBox.Focus();
                return;
            }

            if (adequacaoLabel.Visible)
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
                Status = _chamado.Status
            }, _listaAnexosGravar, Publicas._sla))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            LimpaHistoricos(historicoPanel);

            _historicos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, false);
            _listaAnexos = new ChamadoBO().ListarAnexos(_chamado.IdChamado);

            foreach (var item in _listaAnexos)
                listBox2.Items.Add(item.NomeArquivo);

            foreach (var item in _historicos)
            {
                CriaPanelHistoricos(item.Descricao, item.Data, (item.Tipo == "S" || item.IdUsuario == _chamado.IdUsuario ? item.Nome : ""), (item.Tipo == "S" ? "" : item.Nome), false, item.IdHistorico);
            }

            EnviarEmail();

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (_chamado.Status == Publicas.StatusChamado.Finalizado ? 
                "Concluiu o chamado " : 
                "Incluiu um novo tramite ao chamado ") + numeroTextBoxExt.Text;
            _log.Tela = "Tramites - Chamado";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            assuntoTextBox.Text = string.Empty;
            descricaoTextBox.Text = string.Empty;
            listBox1.Items.Clear();

        }

        private void EnviarEmail()
        {
            string[] _dadosEmail = new string[50];

            _ultimoHistoricos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, true);
            string _emailDestino = Publicas._usuario.Email + "; " ;

            foreach (var item in _ultimoHistoricos)
            {

                _usuario = new UsuarioBO().ConsultarPorId(item.IdUsuario);

                _emailDestino = _emailDestino + _usuario.Email + "; ";
                break;
            }

            // Se nao encontrou o atendente nos tramites irá buscar todos os atendentes para a categoria
            if (_emailDestino == "")
            {
                _listaUsuarios = new UsuarioBO().ConsultaAtendentesParaACategoria(_idCategoria);

                foreach (var item in _listaUsuarios)
                {
                    _emailDestino = _emailDestino + item.Email + "; ";
                }
            }

            _emailDestino = _emailDestino.Substring(0, _emailDestino.Length - 2);

            _dadosEmail[0] = (_chamado.Status == Publicas.StatusChamado.Finalizado ? "Conclusão" : 
                             (_chamado.Status == Publicas.StatusChamado.Reaberto ? "Reabertura" : "Alteração")) 
                            + " do Chamado " + _chamado.Numero;

            _dadosEmail[2] = _usuario.Nome;

            _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            _dadosEmail[3] = empresaComboBox.Text.Substring(10, empresaComboBox.Text.Length - 10);
            _dadosEmail[4] = _chamado.Numero;

            _dadosEmail[5] = (_chamado.Status == Publicas.StatusChamado.Finalizado ? "Finalizado" :
                             (_chamado.Status == Publicas.StatusChamado.Reaberto ? "Reabertura" : "Alterado"));
            _dadosEmail[6] = assuntoTextBox.Text;
            _dadosEmail[7] = categoriaComboBox.Text;
            _dadosEmail[8] = modulosComboBox.Text;
            _dadosEmail[9] = telasComboBox.Text;
            _dadosEmail[10] = tipoChamadoComboBox.Text;
            _dadosEmail[11] = prioridadeComboBox.Text;
            _dadosEmail[12] = descricaoTextBox.Text.Trim();
            _dadosEmail[13] = anexoLabel.Text;
            _dadosEmail[14] = _parametro.PrazoRetorno.ToString();

            if (!Publicas.EnviarEmailChamado(_dadosEmail, true, false, false, _emailDestino, _dadosEmail[0]))
            {
                new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            new Notificacoes.Mensagem("Processo finalizado.", Publicas.TipoMensagem.Sucesso).ShowDialog();

        }

        private void statusComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
                return;
            }

            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            adequacaoLabel.Visible = statusComboBox.SelectedIndex == 2;
            numeroAdequacaoTextBox.Visible = adequacaoLabel.Visible;
            tipoAdequacaoComboBox.Visible = adequacaoLabel.Visible;
        }

        private void statusComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (tipoAdequacaoComboBox.Visible)
                    tipoAdequacaoComboBox.Focus();
                else
                    descricaoTextBox.Focus();
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
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
                return;
            }

            _idModulo = 0;

            foreach (var item in _listaModulos.Where(w => w.Nome == modulosComboBox.Text))
            {
                _idModulo = item.IdModulo;
            }

            telasComboBox.DataSource = _listaTelas.Where(w => w.IdModulo == _idModulo)
                                                      .OrderBy(o => o.NomeCompleto).ToList();
            telasComboBox.DisplayMember = "NomeCompleto";

            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void numeroAdequacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
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
        }

        private void descricaoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void assuntoTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
                return;
            }

            if (string.IsNullOrEmpty(assuntoTextBox.Text))
            {
                assuntoTextBox.Focus();
                return;
            }

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void descricaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
                return;
            }

            if (string.IsNullOrEmpty(descricaoTextBox.Text))
            {
                descricaoTextBox.Focus();
                return;
            }

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void descricaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                concluirToggleButton.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (assuntoTextBox.Enabled)
                    assuntoTextBox.Focus();
                else
                {
                    if (numeroAdequacaoTextBox.Enabled)
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

                        System.Diagnostics.Process.Start(Publicas._caminhoAnexosChamado + listBox2.Items[id].ToString());
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

                    System.Diagnostics.Process.Start(Publicas._caminhoAnexosChamado + listBox2.Items[id].ToString());
                }
            }
        }

        public bool SalvaArquivos(Byte[] anexo, string nomeArquivo)
        {
            try
            {
                FileStream stream = new FileStream(Publicas._caminhoAnexosChamado + nomeArquivo, FileMode.Create);
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
                    listBox1.Items.AddRange(arquivos);
                    anexoLabel.Text = listBox1.Items.Count.ToString();
                    
                    foreach (var item in arquivos)
                    {
                        CriaPanelsArquivo(item);
                    }
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
                Status = _chamado.Status
            }, _listaAnexosGravar, Publicas._sla))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            LimpaHistoricos(historicoPanel);

            _historicos = new ChamadoBO().ListarHistoricos(_chamado.IdChamado, false);
            _listaAnexos = new ChamadoBO().ListarAnexos(_chamado.IdChamado);

            foreach (var item in _listaAnexos)
                listBox2.Items.Add(item.NomeArquivo);

            foreach (var item in _historicos)
            {
                CriaPanelHistoricos(item.Descricao, item.Data, (item.Tipo == "S" || item.IdUsuario == _chamado.IdUsuario ? item.Nome : ""), (item.Tipo == "S" ? "" : item.Nome), false, item.IdHistorico);
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
    }
}
