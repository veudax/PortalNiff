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

namespace Suportte.Chamados
{
    public partial class Abertura : Form
    {
        public Abertura()
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

        List<EmpresaDoUsuario> _listaEmpresas;
        List<CategoriaDoUsuario> _listaCategorias;
        List<ModuloDoUsuario> _listaModulos;
        List<Tela> _listaTelas;
        List<Usuario> _listaUsuarios;
        Usuario _usuario;
        Parametro _parametro;
        string[] arquivo;
        int _topArquivos = 8;
        int _qtd = 0;

        int _idModulo = 0;
        int _idCategoria = 0;
        int _idTela = 0;
        int _idEmpresa = 0;

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Abertura_Shown(object sender, EventArgs e)
        {
            
            // trazer o Tipo De chamados.
            tipoChamadoComboBox.Items.AddRange(new object[] { "Erro", "Dúvidas", "Implementação", "Acesso" });
            tipoChamadoComboBox.SelectedIndex = 1;

            // trazer a prioridades
            prioridadeComboBox.Items.AddRange(new object[] { "Crítico", "Alta", "Media", "Baixa" });
            prioridadeComboBox.SelectedIndex = 2;

            // trazer a origem
            origemComboBox.Items.AddRange(new object[] { "Chamado", "E-mail", "Telefone", "Chat" });
            origemComboBox.SelectedIndex = 0;
            origemComboBox.Enabled = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente ||
            Publicas._usuario.Tipo == Publicas.TipoUsuario.Todos;

            dataAberturaDateTimePicker.Value = DateTime.Now;

            nomeUsuarioTextBox.Enabled = (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente ||
                                          Publicas._usuario.Tipo == Publicas.TipoUsuario.Todos);

            _parametro = new ParametrosBO().Consultar();

            if (Publicas._usuario.Tipo != Publicas.TipoUsuario.Socilitante)
                nomeUsuarioTextBox.Focus();
            else
            {
                nomeUsuarioTextBox.Text = Publicas._usuario.UsuarioAcesso;
                nomeUsuarioTextBox_Validating(sender, new CancelEventArgs());
                tipoChamadoComboBox.Focus();
            }
        }

        private void categoriaComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _idCategoria = 0;

            if (!string.IsNullOrEmpty(categoriaComboBox.Text))
            {
                foreach (var item in _listaCategorias.Where(w => w.Descricao == categoriaComboBox.Text))
                {
                    _idCategoria = item.IdCategoria;
                }

                // Trazer os módulos que o usuário está autorizado para a categoria selecionada
                _listaModulos = new UsuarioBO().ConsultaModulosAutorizadasDoUsuario(_usuario.Id);
                modulosComboBox.DataSource = _listaModulos.Where(w => w.IdCategoria == _idCategoria &&
                                                                      w.ModuloAutoriza)
                                                          .OrderBy(o => o.Nome).ToList();
                modulosComboBox.DisplayMember = "Nome";
            }

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
                if (origemComboBox.Enabled)
                    origemComboBox.Focus();
                else
                    prioridadeComboBox.Focus();
            }
        }

        private void categoriaComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void modulosComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _idModulo = 0;

            if (!string.IsNullOrEmpty(modulosComboBox.Text))
            {
                foreach (var item in _listaModulos.Where(w => w.Nome == modulosComboBox.Text))
                {
                    _idModulo = item.IdModulo;
                }
                _listaTelas = new TelaBO().Listar();

                telasComboBox.DataSource = _listaTelas.Where(w => w.IdModulo == _idModulo)
                                                          .OrderBy(o => o.Nome).ToList();
                telasComboBox.DisplayMember = "NomeCompleto";
            }

            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void telasComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void tipoChamadoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void nomeTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(nomeUsuarioTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Usuário solicitante não informado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeUsuarioTextBox.Focus();
                return;
            }

            if (String.IsNullOrEmpty(emailTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Usuário solicitante sem e-mail informado." + 
                    Environment.NewLine + 
                    "Não é possível enviar e-mail da abertura desse chamado." +
                    Environment.NewLine + 
                    "Por gentileza efetue a alteração do perfil, incluindo o e-mail.", Publicas.TipoMensagem.Alerta).ShowDialog();

                if (_usuario.Tipo == Publicas.TipoUsuario.Socilitante)
                    new Cadastros.EditarPerfil().ShowDialog();
                else
                    new Cadastros.Usuarios().ShowDialog();

                _usuario = new UsuarioBO().Consultar(_usuario.UsuarioAcesso);
                emailTextBox.Text = _usuario.Email;
            }            

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBox.Text))
            {
                _idEmpresa = item.IdEmpresa;
            }

            foreach (var item in _listaTelas.Where(w => w.NomeCompleto == telasComboBox.Text))
            {
                _idTela = item.IdTela;
            }

            Chamado _chamado = new Chamado();
            _chamado.Assunto = assuntoTextBox.Text;
            _chamado.Descricao = descricaoTextBox.Text.Trim();
            _chamado.IdCategoria = _idCategoria;
            _chamado.IdTela = _idTela;
            _chamado.IdEmpresa = _idEmpresa;

            _chamado.IdUsuario = _usuario.Id;
            _chamado.Origens = (origemComboBox.SelectedIndex == 0 ? Publicas.Origem.OnLine :
                               (origemComboBox.SelectedIndex == 1 ? Publicas.Origem.Email : Publicas.Origem.Telefone));
            _chamado.Status = Publicas.StatusChamado.Novo;
            _chamado.Prioridade = (prioridadeComboBox.SelectedIndex == 0 ? Publicas.Prioridades.Critico :
                                  (prioridadeComboBox.SelectedIndex == 1 ? Publicas.Prioridades.Alta :
                                  (prioridadeComboBox.SelectedIndex == 2 ? Publicas.Prioridades.Media : Publicas.Prioridades.Baixa)));
            _chamado.Tipo = (tipoChamadoComboBox.SelectedIndex == 0 ? Publicas.TipoChamado.Erro :
                            (tipoChamadoComboBox.SelectedIndex == 1 ? Publicas.TipoChamado.Duvida : Publicas.TipoChamado.Implementacao));

            _chamado.Numero = new ChamadoBO().ProximoCodigo(Publicas.TipoCalculoChamado.AnoMes, "-");

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
                IdChamado = Publicas._idChamado,
                Descricao = _chamado.Descricao,
                IdUsuario = _usuario.Id,
                Status = _chamado.Status
            }, _listaAnexosGravar, Publicas._sla))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            #region EnviaEmail
            string[] _dadosEmail = new string[50];

            //Localizar atendentes para o chamado. 
            string _emailDestino = "";
            _listaUsuarios = new UsuarioBO().ConsultaAtendentesParaACategoria(_idCategoria);
            
            foreach (var item in _listaUsuarios)
            {
                _emailDestino = _emailDestino + item.Email + "; ";
            }
            _emailDestino = _emailDestino.Substring(0, _emailDestino.Length - 2);

            _dadosEmail[0] = "Abertura de chamado " + _chamado.Numero;
            _dadosEmail[2] = Publicas._usuario.Nome;

            _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            _dadosEmail[3] = empresaComboBox.Text.Substring(10, empresaComboBox.Text.Length-10);
            _dadosEmail[4] = _chamado.Numero;

            _dadosEmail[5] = "";
            _dadosEmail[6] = assuntoTextBox.Text;
            _dadosEmail[7] = categoriaComboBox.Text;
            _dadosEmail[8] = modulosComboBox.Text;
            _dadosEmail[9] = telasComboBox.Text;
            _dadosEmail[10] = tipoChamadoComboBox.Text;
            _dadosEmail[11] = prioridadeComboBox.Text;
            _dadosEmail[12] = descricaoTextBox.Text.Trim();
            _dadosEmail[13] = anexoLabel.Text;
            _dadosEmail[14] = _parametro.PrazoRetorno.ToString();
            
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

            new Notificacoes.Mensagem("Processo finalizado.", Publicas.TipoMensagem.Sucesso).ShowDialog();
            #endregion

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Abriu o chamado " + _chamado.Numero;
            _log.Tela = "Abertura - Chamado";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
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
                empresaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                modulosComboBox.Focus();
            }
        }

        private void empresaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                assuntoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                telasComboBox.Focus();
            }
        }

        private void assuntoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                descricaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBox.Focus();
            }
        }

        private void descricaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (gravarButton.Enabled)
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                assuntoTextBox.Focus();
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

        private void nomeUsuarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (nomeUsuarioTextBox.Text.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                nomeUsuarioTextBox.Text = Publicas._usuarioAcesso;

                if (nomeUsuarioTextBox.Text.Trim() == "")
                {
                    nomeUsuarioTextBox.Focus();
                    return;
                }
            }

            _usuario = new UsuarioBO().Consultar(nomeUsuarioTextBox.Text);

            if (!_usuario.Existe)
            {
                new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeUsuarioTextBox.Focus();
                return;
            }

            if (!_usuario.Ativo)
            {
                new Notificacoes.Mensagem("Usuário inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeUsuarioTextBox.Focus();
                return;
            }

            // Trazer as empresas que o usuário esta autorizado.
            _listaEmpresas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(_usuario.Id);
            empresaComboBox.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBox.DisplayMember = "CodigoeNome";

            // Trazer as categorias que o usuário está autorizado
            _listaCategorias = new UsuarioBO().ConsultaCategoriasAutorizadasDoUsuario(_usuario.Id);
            categoriaComboBox.DataSource = _listaCategorias.Where(w => w.CategoriaAutoriza)
                                                           .OrderBy(o => o.Descricao).ToList();
            categoriaComboBox.DisplayMember = "Descricao";

            nomeTextBox.Text = _usuario.Nome;
            ipMaquinaTextBox.Text = _usuario.IpMaquina;
            nomeMaquinaTextBox.Text = _usuario.NomeMaquina;
            departamentoTextBox.Text = _usuario.Departamento;
            telefoneTextBox.Text = _usuario.Telefone.ToString();
            ramalTextBox.Text = _usuario.Ramal.ToString();
            emailTextBox.Text = _usuario.Email;
        }

        private void nomeUsuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoChamadoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
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
                if (nomeUsuarioTextBox.Enabled)
                    nomeUsuarioTextBox.Focus();
                else
                    ((Control)sender).Focus();
            }
        }

        private void prioridadeComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (origemComboBox.Enabled)
                    origemComboBox.Focus();
                else
                    categoriaComboBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoChamadoComboBox.Focus();
            }
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

        private void limparButton_Click(object sender, EventArgs e)
        {
            nomeUsuarioTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            telefoneTextBox.Text = string.Empty;
            ipMaquinaTextBox.Text = string.Empty;
            ramalTextBox.Text = string.Empty;
            nomeMaquinaTextBox.Text = string.Empty;
            departamentoTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            tipoChamadoComboBox.SelectedIndex = 0;
            prioridadeComboBox.SelectedIndex = 2;
            origemComboBox.SelectedIndex = 0;
            categoriaComboBox.SelectedIndex = 0;
            modulosComboBox.SelectedIndex = -1;
            empresaComboBox.SelectedIndex = -1;
            assuntoTextBox.Text = string.Empty;
            descricaoTextBox.Text = string.Empty;
            anexoLabel.Text = "0";
            modulosComboBox.Enabled = false;
            empresaComboBox.Enabled = false;
            telasComboBox.Enabled = false;

            listBox1.Items.Clear();

        }

        private void anexoPanel_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Title = "Selecione o arquivo a ser anexado.";
            anexoImagensPanel.Visible = false;
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

        private void anexoPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
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
                    tipo = caminho + @"\Imagens\pgn.png";
                    break;
                default:
                    tipo = caminho + @"\Imagens\File.png";
                    break;
            }

            if (_qtd == 0)
            {
                arquivoPanel.Location = new Point(arquivoPanel.Left, _topArquivos = 8);
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
                panel.Location = new Point(arquivoPanel.Left, _topArquivos);

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

            _topArquivos = _topArquivos + (arquivoPanel.Height + 5);

            Refresh();
            _qtd++;
        }


        private void anexoPanel_DragDrop(object sender, DragEventArgs e)
        {
            arquivo = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            anexoImagensPanel.Visible = false;
            for (i = 0; i < arquivo.Length; i++)
            {
                listBox1.Items.Add(arquivo[i]);
                CriaPanelsArquivo(arquivo[i]);
            }

            anexoLabel.Text = listBox1.Items.Count.ToString();
        }

        private void anexoPanel_MouseHover(object sender, EventArgs e)
        {
            anexoPanel.BackColor = Color.Gainsboro;
        }

        private void anexoPanel_MouseLeave(object sender, EventArgs e)
        {
            anexoPanel.BackColor = principalPanel.BackColor;
        }

        private void categoriaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            modulosComboBox.Enabled = categoriaComboBox.SelectedIndex != -1;
        }

        private void modulosComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            telasComboBox.Enabled = modulosComboBox.SelectedIndex != -1;
        }

        private void telasComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            empresaComboBox.Enabled = telasComboBox.SelectedIndex != -1;
        }

        private void empresaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            assuntoTextBox.Enabled = empresaComboBox.SelectedIndex != -1;
            descricaoTextBox.Enabled = assuntoTextBox.Enabled;
        }

        private void anexoImagensPanel_Click(object sender, EventArgs e)
        {
            
        }

        private void verPictureBox_Click(object sender, EventArgs e)
        {
            anexoImagensPanel.Visible = !anexoImagensPanel.Visible;
        }

        private void removerLabel_Click(object sender, EventArgs e)
        {
            _qtd = 0;
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

            _topArquivos = 8;
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
    }
}
