using Classes;
using Negocio;
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
    public partial class Temporizador : Form
    {
        public Temporizador()
        {
            InitializeComponent();

            dataDateTimePicker.Value = DateTime.Now;
            dataDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataDateTimePicker.BackColor = empresaComboBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

            }
            Publicas._mensagemSistema = string.Empty;
        }

        public int _idTemporizadoChamado = 0;
        List<Categoria> _listaCategorias;
        List<Modulo> _listaModulos;
        List<Tela> _listaTelas;
        List<Empresa> _listaEmpresas;

        Usuario _usuario;
        Chamado _chamado;
        Tela _tela;
        
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

        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                //PatText.Focus();
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

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Temporizador_Shown(object sender, EventArgs e)
        {
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
            
            // trazer o Tipo De chamados.
            tipoChamadoComboBox.Items.AddRange(new object[] { "Erro", "Dúvidas", "Implementação", "Acesso", "Ajustes", "Projeto" });
            tipoChamadoComboBox.SelectedIndex = 1;
            
            // carregar dados
            _chamado = new ChamadoBO().Consulta(Publicas._idChamado);
            _listaModulos = new ModuloBO().Listar(_chamado.IdCategoria);

            modulosComboBox.DataSource = _listaModulos.Where(w => w.Ativo)
                                                      .OrderBy(o => o.Nome).ToList();
            modulosComboBox.DisplayMember = "Nome";
           
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
           
            if (_chamado.IdTela != 0)
            {
                _listaTelas = new TelaBO().Listar(_tela.IdModulo);
                telasComboBox.DataSource = _listaTelas.Where(w => w.Ativo)
                                                      .OrderBy(o => o.NomeCompleto).ToList();
                telasComboBox.DisplayMember = "NomeCompleto";
                
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
            
            avaliacaoRatingControl.Visible = _chamado.Status == Publicas.StatusChamado.Finalizado;
            gravarButton.Visible = _chamado.Status != Publicas.StatusChamado.Finalizado;
        }

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void EntradaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
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
    }
}
