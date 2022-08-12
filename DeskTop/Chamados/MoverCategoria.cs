using Classes;
using Negocio;
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

namespace Suportte.Chamados
{
    public partial class MoverCategoria : Form
    {
        public MoverCategoria()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Categoria> _listaCategorias;
        List<Modulo> _listaModulos;
        List<Tela> _listaTelas;
        List<Usuario> _listaUsuarios;
        List<Empresa> _listaEmpresas;

        Usuario _usuario;
        Chamado _chamado;
        Tela _tela;

        int _idCategoria;
        int _idModulo;
        int _idTela;

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

        private void MoverCategoria_Shown(object sender, EventArgs e)
        {
            // Trazer as empresas que o usuário esta autorizado.
            _listaEmpresas = new EmpresaBO().Listar();
            empresaComboBox.DataSource = _listaEmpresas.Where(w => w.Ativo)
                                                       .OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBox.DisplayMember = "CodigoeNome";

            EmpresaNovaComboBox.DataSource = _listaEmpresas.Where(w => w.Ativo)
                                           .OrderBy(o => o.CodigoeNome).ToList();
            EmpresaNovaComboBox.DisplayMember = "CodigoeNome";

            // Trazer as categorias que o usuário está autorizado
            _listaCategorias = new CategoriaBO().Listar();
            categoriaComboBox.DataSource = _listaCategorias.Where(w => w.Ativo)
                                                           .OrderBy(o => o.Descricao).ToList();
            categoriaComboBox.DisplayMember = "Descricao";
            NovaCategoriaComboBox.DataSource = _listaCategorias.Where(w => w.Ativo)
                                                           .OrderBy(o => o.Descricao).ToList();
            NovaCategoriaComboBox.DisplayMember = "Descricao";

            // trazer o Tipo De chamados.
            tipoChamadoComboBox.Items.AddRange(new object[] { "Erro", "Dúvidas", "Implementação", "Acesso", "Ajustes", "Projeto" });
            tipoChamadoComboBox.SelectedIndex = 1;
            // trazer o Tipo De chamados.
            TipoChamadoNovoComboBox.Items.AddRange(new object[] { "Erro", "Dúvidas", "Implementação", "Acesso", "Ajustes", "Projeto" });
            TipoChamadoNovoComboBox.SelectedIndex = 1;

            // carregar dados
            _chamado = new ChamadoBO().Consulta(Publicas._idChamado);
            _listaModulos = new ModuloBO().Listar(_chamado.IdCategoria);

            modulosComboBox.DataSource = _listaModulos.Where(w => w.Ativo)
                                                      .OrderBy(o => o.Nome).ToList();
            modulosComboBox.DisplayMember = "Nome";
            NovoModuloComboBox.DataSource = _listaModulos.Where(w => w.Ativo)
                                                      .OrderBy(o => o.Nome).ToList();
            NovoModuloComboBox.DisplayMember = "Nome";

            _tela = new TelaBO().Consultar(_chamado.IdTela);
            
            foreach (var item in _listaEmpresas.Where(w => w.IdEmpresa == _chamado.IdEmpresa))
            {
                for (int i = 0; i < empresaComboBox.Items.Count; i++)
                {
                    empresaComboBox.SelectedIndex = i;
                    if (empresaComboBox.Text == item.CodigoeNome)
                        break;
                }
            }
            EmpresaNovaComboBox.SelectedIndex = empresaComboBox.SelectedIndex;

            if (_chamado.IdTela != 0)
            {
                _listaTelas = new TelaBO().Listar(_tela.IdModulo);
                telasComboBox.DataSource = _listaTelas.Where(w => w.Ativo)
                                                      .OrderBy(o => o.NomeCompleto).ToList();
                telasComboBox.DisplayMember = "NomeCompleto";
                NovaTelaComboBox.DataSource = _listaTelas.Where(w => w.Ativo)
                                                      .OrderBy(o => o.NomeCompleto).ToList();
                NovaTelaComboBox.DisplayMember = "NomeCompleto";
            }

            #region dados solicitante
            _usuario = new UsuarioBO().ConsultarPorId(_chamado.IdUsuario);
            nomeUsuarioTextBox.Text = _usuario.Nome;
            nomeMaquinaTextBox.Text = _usuario.NomeMaquina;
            telefoneTextBox.Text = _usuario.Telefone.ToString();
            ramalTextBox.Text = _usuario.Ramal.ToString();
            #endregion

            dataAberturaDateTimePicker.Value = _chamado.Data;
            assuntoTextBox.Text = _chamado.Assunto;

            numeroTextBoxExt.Text = _chamado.Numero;

            tipoChamadoComboBox.SelectedIndex = (_chamado.Tipo == Publicas.TipoChamado.Erro ? 0 :
                (_chamado.Tipo == Publicas.TipoChamado.Duvida ? 1 :
                (_chamado.Tipo == Publicas.TipoChamado.Implementacao ? 2 :
                (_chamado.Tipo == Publicas.TipoChamado.Acesso ? 3 :
                (_chamado.Tipo == Publicas.TipoChamado.Ajustes ? 4 : 5)))));

            TipoChamadoNovoComboBox.SelectedIndex = (_chamado.Tipo == Publicas.TipoChamado.Erro ? 0 :
                (_chamado.Tipo == Publicas.TipoChamado.Duvida ? 1 :
                (_chamado.Tipo == Publicas.TipoChamado.Implementacao ? 2 :
                (_chamado.Tipo == Publicas.TipoChamado.Acesso ? 3 :
                (_chamado.Tipo == Publicas.TipoChamado.Ajustes ? 4 : 5)))));

            foreach (var item in _listaCategorias.Where(w => w.IdCategoria == _chamado.IdCategoria))
            {
                for (int i = 0; i < categoriaComboBox.Items.Count; i++)
                {
                    categoriaComboBox.SelectedIndex = i;
                    if (categoriaComboBox.Text == item.Descricao)
                        break;
                }
            }

            if (_chamado.IdTela == 0)
            {
                telasComboBox.Text = "";
                modulosComboBox.Text = "";
            }
            else
            {
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
            }
            
            NovaCategoriaComboBox.Text = "";
            NovoModuloComboBox.Text = "";
            NovaTelaComboBox.Text = "";

            avaliacaoRatingControl.Visible = _chamado.Status == Publicas.StatusChamado.Finalizado;
            gravarButton.Visible = _chamado.Status != Publicas.StatusChamado.Finalizado;
        }

        private void NovaCategoriaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NovoModuloComboBox.ReadOnly = NovaCategoriaComboBox.SelectedIndex == -1;
        }

        private void NovaCategoriaComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _idCategoria = 0;

            if (!string.IsNullOrEmpty(NovaCategoriaComboBox.Text))
            {
                foreach (var item in _listaCategorias.Where(w => w.Descricao == NovaCategoriaComboBox.Text))
                {
                    _idCategoria = item.IdCategoria;
                }

                _listaModulos.Clear();
                _listaModulos.Add(new Modulo());
                _listaModulos.AddRange(new ModuloBO().Listar(_idCategoria, true));
                NovaTelaComboBox.DataSource = new List<Tela>();

                NovoModuloComboBox.DataSource = _listaModulos.OrderBy(o => o.Nome).ToList();
                NovoModuloComboBox.DisplayMember = "Nome";

                gravarButton.Enabled = true;
            }

        }

        private void NovaCategoriaComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void NovaCategoriaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                NovoModuloComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                NovaCategoriaComboBox.Focus();
            }
        }

        private void NovoModuloComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                NovaTelaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                NovaCategoriaComboBox.Focus();
            }
        }

        private void NovaTelaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                NovoModuloComboBox.Focus();
            }
        }

        private void NovoModuloComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NovaTelaComboBox.ReadOnly = NovoModuloComboBox.SelectedIndex == -1;
        }

        private void NovoModuloComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _idModulo = 0;

            if (!string.IsNullOrEmpty(NovoModuloComboBox.Text))
            {
                foreach (var item in _listaModulos.Where(w => w.Nome == NovoModuloComboBox.Text))
                {
                    _idModulo = item.IdModulo;
                }

                if (_listaTelas != null)
                    _listaTelas.Clear();
                else
                    _listaTelas = new List<Tela>();

                _listaTelas.Add(new Tela());
                _listaTelas.AddRange(new TelaBO().Listar(_idModulo, true));

                NovaTelaComboBox.DataSource = _listaTelas.OrderBy(o => o.Nome).ToList();
                NovaTelaComboBox.DisplayMember = "NomeCompleto";
            }
        }

        private void NovaTelaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void NovaTelaComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (!string.IsNullOrEmpty(NovaTelaComboBox.Text))
            {
                foreach (var item in _listaTelas.Where(w => w.NomeCompleto == NovaTelaComboBox.Text))
                {
                    _idTela = item.IdTela;
                }
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            int _idEmpresa = 0;
            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == EmpresaNovaComboBox.Text))
            {
                _idEmpresa = item.IdEmpresa;
            }

            if (_chamado.IdCategoria != _idCategoria)
            {
                _chamado.Status = Publicas.StatusChamado.Novo;
                _chamado.TrocouCategoria = true;
            }

            _chamado.IdCategoria = _idCategoria;

            if (_chamado.IdEmpresa != _idEmpresa)
                _chamado.IdEmpresa = _idEmpresa;

            if (_idTela != 0)
                _chamado.IdTela = _idTela;

            if (tipoChamadoComboBox.Text != TipoChamadoNovoComboBox.Text)
                _chamado.Tipo = (TipoChamadoNovoComboBox.SelectedIndex == 0 ? Publicas.TipoChamado.Erro :
                                (TipoChamadoNovoComboBox.SelectedIndex == 1 ? Publicas.TipoChamado.Duvida :
                                (TipoChamadoNovoComboBox.SelectedIndex == 2 ? Publicas.TipoChamado.Implementacao :
                                (TipoChamadoNovoComboBox.SelectedIndex == 3 ? Publicas.TipoChamado.Acesso :
                                (TipoChamadoNovoComboBox.SelectedIndex == 4 ? Publicas.TipoChamado.Ajustes : Publicas.TipoChamado.Projeto)))));

            if (!new ChamadoBO().Gravar(_chamado))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new ChamadoBO().GravarHistorico(new HistoricoDoChamado()
            {
                IdChamado = _chamado.IdChamado,
                Descricao = ((categoriaComboBox.Text != NovaCategoriaComboBox.Text ?
                             "Alterou categoria de " + categoriaComboBox.Text + " para " + NovaCategoriaComboBox.Text : "") +
                             (modulosComboBox.Text != NovoModuloComboBox.Text ?
                             " Alterou o módulo de " + modulosComboBox.Text + " para " + NovoModuloComboBox.Text : "") +
                             (telasComboBox.Text != NovaTelaComboBox.Text ?
                             " Alterou o local de " + telasComboBox.Text + " para " + NovaTelaComboBox.Text : "")).Trim(),
                IdUsuario = Publicas._usuario.Id,
                Status = _chamado.Status,
                Privado = true,
                Usuario = "A"
        }, new List<AnexoDoHistorico>(), Publicas._sla))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (categoriaComboBox.Text != NovaCategoriaComboBox.Text)
            {

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

                _dadosEmail[0] = "Chamado " + _chamado.Numero + " alterado para sua categoria";
                _dadosEmail[2] = Publicas._usuario.Nome.Split(' ')[0];

                _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                _dadosEmail[4] = _chamado.Numero;

                _dadosEmail[5] = "";
                _dadosEmail[6] = assuntoTextBox.Text;
                _dadosEmail[7] = categoriaComboBox.Text;
                _dadosEmail[8] = modulosComboBox.Text;
                _dadosEmail[9] = telasComboBox.Text;
                _dadosEmail[10] = tipoChamadoComboBox.Text;
                _dadosEmail[16] = Publicas._usuario.Nome.Split(' ')[0];


                if (!Publicas.EnviarEmailChamado(_dadosEmail, false, true, false, _emailDestino, _dadosEmail[0], true))
                {
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                new Notificacoes.Mensagem("Processo finalizado." + Environment.NewLine +
                    "Chamado nº " + _chamado.Numero + " movido para " + NovaCategoriaComboBox.Text, Publicas.TipoMensagem.Sucesso).ShowDialog();
                #endregion

                Log _log = new Log();
                _log.IdUsuario = Publicas._usuario.Id;
                _log.Descricao = ((categoriaComboBox.Text != NovaCategoriaComboBox.Text ?
                             " Alterou categoria de " + categoriaComboBox.Text + " para " + NovaCategoriaComboBox.Text : "") +
                             (modulosComboBox.Text != NovoModuloComboBox.Text ?
                             " Alterou o módulo de " + modulosComboBox.Text + " para " + NovoModuloComboBox.Text : "") +
                             (telasComboBox.Text != NovaTelaComboBox.Text ?
                             " Alterou o local de " + telasComboBox.Text + " para " + NovaTelaComboBox.Text : "")).Trim();
                _log.Tela = "Alteração de categoria";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }
            }
            Close();
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
    }
}
