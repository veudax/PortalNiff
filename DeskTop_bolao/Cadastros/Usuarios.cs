using Classes;
using Negocio;
using Suportte.Notificacoes;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
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

namespace Suportte.Cadastros
{
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();

            dataNascimentoDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataNascimentoDateTimePicker.BackColor = usuarioTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<EmpresaDoUsuario> _listaEmpresas;
        List<Empresa> _empresas;
        List<CategoriaDoUsuario> _listaCategorias;
        List<ModuloDoUsuario> _listaModulos;
        Usuario _usuario;
        Usuario _usuarioLog;
        Departamento _departamento;
        FuncionariosGlobus _funcionariosGlobus;
        Classes.Cargos _cargos;

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

        private void Usuarios_Load(object sender, EventArgs e)
        {
           
            tipoComboBox.Items.AddRange(new object[] { "Solicitante", "Atendente", "Ambos" });
            TipoSACComboBox.Items.AddRange(new object[] {"Atendente", "Usuário comum", "Finalizador", "Administrador"});

            tipoComboBox.SelectedIndex = 0;
            TipoSACComboBox.SelectedIndex = 0;

            _empresas = new EmpresaBO().Listar();

            grupoChatcomboBoxAdv.DataSource = _empresas;
            grupoChatcomboBoxAdv.DisplayMember = "Nome";
        }
        
        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {

            //VisualStyles and themes
            GridMetroColors theme = new GridMetroColors();
            //changes the header text color on mouse hovering.
            theme.HeaderTextColor.HoverTextColor = Color.GhostWhite;
            theme.CheckBoxColor.BorderColor = Publicas._bordaEntrada;
            theme.CheckBoxColor.CheckColor = Publicas._bordaEntrada;

            theme.HeaderBottomBorderColor = Publicas._bordaEntrada;
            theme.ComboboxColor.PressedBackColor = Publicas._bordaEntrada;
            theme.PushButtonColor.PushedBackColor = Publicas._bordaEntrada;

            //Applys the customized theme to the gridControl metrostyle.
            this.empresasGridDataBoundGrid.SetMetroStyle(theme);
            this.categoriaGridDataBoundGrid.SetMetroStyle(theme);
            this.modulosGridDataBoundGrid.SetMetroStyle(theme);
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void usuarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (usuarioTextBox.Text.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                usuarioTextBox.Text = Publicas._usuarioAcesso;

                if (usuarioTextBox.Text.Trim() == "")
                {
                    usuarioTextBox.Focus();
                    return;
                }
            }

            _usuario = new UsuarioBO().Consultar(usuarioTextBox.Text);

            if (_usuario.Existe)
            {
                _usuarioLog = new UsuarioBO().Consultar(usuarioTextBox.Text);

                nomeTextBox.Text = _usuario.Nome;
                tipoComboBox.SelectedIndex = (_usuario.Tipo == Publicas.TipoUsuario.Socilitante ? 0
                                             : (_usuario.Tipo == Publicas.TipoUsuario.Atendente ? 1
                                             : 2));
                senhaTextBox.Text = _usuario.Senha;
                dataNascimentoDateTimePicker.Value = _usuario.DataNascimento;

                if (_usuario.Telefone != 0 )
                    telefoneTextBox.Text = _usuario.Telefone.ToString();

                if (_usuario.Ramal != 0)
                    ramalTextBox.Text = _usuario.Ramal.ToString();

                descricaoCargoTextBox.Text = _usuario.Cargo;

                if (_usuario.IdCargo != 0)
                {
                    cargoTextBox.Text = _usuario.IdCargo.ToString();
                    cargoTextBox_Validating(sender, new CancelEventArgs());
                }

                ipMaquinaTextBox.Text = _usuario.IpMaquina;
                nomeMaquinaTextBox.Text = _usuario.NomeMaquina;
                emailTextBox.Text = _usuario.Email;
                ativoCheckBox.Checked = _usuario.Ativo;
                administradorCheckBox.Checked = _usuario.Administrador;
                acessaAgendaCheckBox.Checked = _usuario.AcessaAgenda;
                acessaBICheckBox.Checked = _usuario.AcessaBI;
                acessaChatCheckBox.Checked = _usuario.AcessaChat;
                acessaSACCheckBox.Checked = _usuario.AcessaSac;
                excluiHistoricoCheckBox.Checked = _usuario.PermiteExcluirChat;
                incluifotosCheckBox.Checked = _usuario.PermiteIncluirExcluirFoto;
                acessaBeneficiosCheckBox.Checked = _usuario.AcessaDescontoBeneficio;
                acessaJuridicoCheckBox.Checked = _usuario.AcessaJuridico;

                acessaCadastrosJuridicosCheckBox.Checked = _usuario.AcessaCadastroJuridico;
                permiteAprovarCheckBox.Checked = _usuario.PermiteAprovarComunicado;
                permiteCancelarCheckBox.Checked = _usuario.PermiteCancelarComunicado;
                permiteReprovarCheckBox.Checked = _usuario.PermiteReprovarComunicado;
                permiteAlterarCheckBox.Checked = _usuario.PermiteAlterarComunicado;
                permiteFinalizarCheckBox.Checked = _usuario.PermiteFinalizarComunicado;
                acessaDashBoardChamadosCheckBoxAdv.Checked = _usuario.AcessaDashBoardChamados;
                acessaAvaliacaoDesempenhoCheckBox.Checked = _usuario.AcessaAvaliacaoDesempenho;
                acessoRHCheckBox.Checked = _usuario.AcessoDeRH;
                acessoGestorCheckBox.Checked = _usuario.AcessoDeGestor;
                acessoColaboradorCheckBox.Checked = _usuario.AcessoDeColaborador;
                acessoControladoriaCheckBox.Checked = _usuario.AcessoDeControladoria;
                visualizaRadarCompletoCheckBox.Checked = _usuario.VisualizaRadarCompleto;
                visualizaBancoHorasDeptoCheckBox.Checked = _usuario.VisualizaBancoHorasDoDepartamento;
                ParticipaBolaoCopaCheckBox.Checked = _usuario.ParticipaBolaoCopa;
                AdministraBolaoCheckBox.Checked = _usuario.AdministraBolaoCopa;
                AdministraBibliotecaCheckBox.Checked = _usuario.AdministraBiblioteca;
                AdministraCorridasCheckBox.Checked = _usuario.AdministraCorridas;

                TipoSACComboBox.SelectedIndex = (_usuario.TipoSac == Publicas.TipoUsuarioSAC.Atendente ? 0
                                                : (_usuario.TipoSac == Publicas.TipoUsuarioSAC.UsuarioComum ? 1 
                                                : (_usuario.TipoSac == Publicas.TipoUsuarioSAC.Finalizador ? 2 : 3)));

                codigoFuncionarioTextBox.Text = _usuario.RegistroFuncionario;

                if (_usuario.CPF != 0)
                    cpfMaskedEditBox.Text = _usuario.CPF.ToString();

                if (_usuario.IdDepartamento != 0)
                {
                    setorTextBox.Text = _usuario.IdDepartamento.ToString();

                    _departamento = new DepartamentoBO().Consultar(_usuario.IdDepartamento);
                    descricaoDeptoTextBox.Text = _departamento.Descricao;
                }

                emailPowerBITextBox.Text = _usuario.EmailAcessoPowerBi;
                emailDepartamentoTextBox.Text = _usuario.EmailDepartamento;

                grupoChatcomboBoxAdv.Text = "";
                foreach (var item in _empresas.Where(w => w.IdEmpresa == _usuario.IdEmpresa))
                {
                    grupoChatcomboBoxAdv.SelectedText = item.Nome;
                }
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _usuario.Existe;

            #region Empresas Autorizadas
            _listaEmpresas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(_usuario.Id);

            if (!string.IsNullOrEmpty(Publicas.mensagemDeErro))
            {
                new Notificacoes.Mensagem("Problemas durante a consulta." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                buttonAdv1_Click(sender, new EventArgs()); // limpar

                return;
            }
            empresasGridDataBoundGrid.DataSource = _listaEmpresas;
            empresasGridDataBoundGrid.AllowProportionalColumnSizing = false;
            empresasGridDataBoundGrid.SortIconPlacement = SortIconPlacement.Left;
            empresasGridDataBoundGrid.TopLevelGroupOptions.ShowFilterBar = false;
            empresasGridDataBoundGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            empresasGridDataBoundGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            empresasGridDataBoundGrid.RecordNavigationBar.Label = "Empresas";

            for (int i = 0; i < empresasGridDataBoundGrid.TableDescriptor.Columns.Count; i++)
            {
                empresasGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = false;
                
                if (i > 0)
                {
                    empresasGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = true;
                    empresasGridDataBoundGrid.TableDescriptor.Columns[i].Appearance.AnyRecordFieldCell.Enabled = false;
                }

                empresasGridDataBoundGrid.TableDescriptor.Columns[i].AllowFilter = false;
                empresasGridDataBoundGrid.TableDescriptor.Columns[i].AllowSort = true;
                empresasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                empresasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                empresasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            // para permitir editar dados.
            this.empresasGridDataBoundGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.empresasGridDataBoundGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.empresasGridDataBoundGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.empresasGridDataBoundGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.empresasGridDataBoundGrid.TableOptions.RecordRowHeight = 18;
            this.empresasGridDataBoundGrid.TableOptions.RecordPreviewRowHeight = 18;
            this.empresasGridDataBoundGrid.TableOptions.SummaryRowHeight = 18;

            empresasGridDataBoundGrid.Refresh();
            #endregion

            #region Categorias Autorizadas

            _listaCategorias = new UsuarioBO().ConsultaCategoriasAutorizadasDoUsuario(_usuario.Id);
            if (!string.IsNullOrEmpty(Publicas.mensagemDeErro))
            {
                new Notificacoes.Mensagem("Problemas durante a consulta." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                buttonAdv1_Click(sender, new EventArgs()); // limpar

                return;
            }

            categoriaGridDataBoundGrid.DataSource = _listaCategorias;
            categoriaGridDataBoundGrid.AllowProportionalColumnSizing = false;
            categoriaGridDataBoundGrid.Binder.InternalColumns[0].Width = 15;
            categoriaGridDataBoundGrid.Binder.InternalColumns[0].HeaderText = " ";
            categoriaGridDataBoundGrid.Binder.InternalColumns[1].Width = 120;
            categoriaGridDataBoundGrid.Refresh();
            #endregion

            #region Modulos Autorizados
            _listaModulos = new UsuarioBO().ConsultaModulosAutorizadasDoUsuario(_usuario.Id);
            if (!string.IsNullOrEmpty(Publicas.mensagemDeErro))
            {
                new Notificacoes.Mensagem("Problemas durante a consulta." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                buttonAdv1_Click(sender, new EventArgs()); // limpar

                return;
            }

            modulosGridDataBoundGrid.DataSource = _listaModulos;
            modulosGridDataBoundGrid.AllowProportionalColumnSizing = false;
            modulosGridDataBoundGrid.Binder.InternalColumns[0].Width = 15;
            modulosGridDataBoundGrid.Binder.InternalColumns[0].HeaderText = " ";
            modulosGridDataBoundGrid.Binder.InternalColumns[1].Width = 120;
            modulosGridDataBoundGrid.Binder.InternalColumns[2].Width = 80;
            modulosGridDataBoundGrid.Refresh();
            #endregion

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._usuarioAcesso != "")
                nomeTextBox.Focus();
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            usuarioTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            tipoComboBox.SelectedIndex = -1;
            TipoSACComboBox.SelectedIndex = -1;
            senhaTextBox.Text = string.Empty; ;
            dataNascimentoDateTimePicker.Value = DateTime.Now;
            telefoneTextBox.Text = string.Empty ;
            ramalTextBox.Text = string.Empty; 
            cargoTextBox.Text = string.Empty; 
            ipMaquinaTextBox.Text = string.Empty;
            nomeMaquinaTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            ativoCheckBox.Checked = true;
            administradorCheckBox.Checked = false;
            acessaAgendaCheckBox.Checked = false;
            acessaBICheckBox.Checked = false;
            acessaChatCheckBox.Checked = false;
            excluiHistoricoCheckBox.Checked = false;
            incluifotosCheckBox.Checked = false;
            acessaBeneficiosCheckBox.Checked = false;
            usuarioTextBox.Focus();
            empresasGridDataBoundGrid.DataSource = new List<EmpresaDoUsuario>();
            categoriaGridDataBoundGrid.DataSource = new List<Categoria>();
            modulosGridDataBoundGrid.DataSource = new List<Modulo>();
            grupoChatcomboBoxAdv.Text = "";
            codigoFuncionarioTextBox.Text = string.Empty;
            cpfMaskedEditBox.Text = string.Empty;
            emailPowerBITextBox.Text = string.Empty;
            codigoFuncionarioTextBox.Text = string.Empty;
            setorTextBox.Text = string.Empty;
            descricaoDeptoTextBox.Text = string.Empty;
            emailDepartamentoTextBox.Text = string.Empty;
            acessaJuridicoCheckBox.Checked = false;
            acessaCadastrosJuridicosCheckBox.Checked = false;
            permiteReprovarCheckBox.Checked = false;
            permiteCancelarCheckBox.Checked = false;
            permiteAprovarCheckBox.Checked = false;
            permiteAlterarCheckBox.Checked = false;
            permiteFinalizarCheckBox.Checked = false;
            acessaDashBoardChamadosCheckBoxAdv.Checked = false;
            acessaAvaliacaoDesempenhoCheckBox.Checked = false;
            acessoRHCheckBox.Checked = false;
            acessoGestorCheckBox.Checked = false;
            acessoColaboradorCheckBox.Checked = false;
            acessoControladoriaCheckBox.Checked = false;
            visualizaRadarCompletoCheckBox.Checked = false;
            visualizaBancoHorasDeptoCheckBox.Checked = false;
            ParticipaBolaoCopaCheckBox.Checked = false;
            AdministraBolaoCheckBox.Checked = false;
            AdministraBibliotecaCheckBox.Checked = false;
            AdministraCorridasCheckBox.Checked = false;

            acessaAvaliacaoDesempenhoCheckBox.Checked = false;
            acessoColaboradorCheckBox.Checked = false;
            acessoControladoriaCheckBox.Checked = false;
            acessoGestorCheckBox.Checked = false;
            acessoRHCheckBox.Checked = false;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void usuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                usuarioTextBox.Focus();
            }
        }

        private void tipoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void dataNascimentoDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void tipoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                senhaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void senhaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cpfMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoComboBox.Focus();
            }
        }

        private void dataNascimentoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                telefoneTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cpfMaskedEditBox.Focus();
            }
        }

        private void telefoneTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ramalTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataNascimentoDateTimePicker.Focus();
            }
        }

        private void ramalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cargoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                telefoneTextBox.Focus();
            }
        }

        private void cargoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                grupoChatcomboBoxAdv.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
               ramalTextBox.Focus();
            }
        }

        private void ipMaquinaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoFuncionarioTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                setorTextBox.Focus();
            }
        }

        private void nomeMaquinaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                emailDepartamentoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                emailTextBox.Focus();
            }
        }

        private void emailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeMaquinaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoFuncionarioTextBox.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                administradorCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                emailDepartamentoTextBox.Focus();
            }
        }

        private void administradorCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                acessaAgendaCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void acessaAgendaCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                acessaChatCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                administradorCheckBox.Focus();
            }
        }

        private void acessaChatCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                acessaBICheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessaAgendaCheckBox.Focus();
            }
        }

        private void acessaBICheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                acessaSACCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessaChatCheckBox.Focus();
            }
        }

        private void excluiHistoricoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                incluifotosCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessaJuridicoCheckBox.Focus();
            }
        }

        private void incluifotosCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                acessaDashBoardChamadosCheckBoxAdv.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                excluiHistoricoCheckBox.Focus();
            }
        }

        private void categoriaMultiSelectionComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                modulosGridDataBoundGrid.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                incluifotosCheckBox.Focus();
            }
        }

        private void empresasMultiSelectionComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                modulosGridDataBoundGrid.Focus();
            }
        }

        private void modulosMultiSelectionComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                empresasGridDataBoundGrid.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                categoriaGridDataBoundGrid.Focus();
            }
        }

        private void tipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void dataNascimentoDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void emailPowerBITextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (avaliacaoDesempenhoPanel.Enabled)
                    acessoRHCheckBox.Focus();
                else
                {
                    diversosTabControl.SelectedTab = autorizacoesTabPage;
                    empresasGridDataBoundGrid.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            { 
                Publicas._escTeclado = true;
                if (JuridicoPanel.Enabled)
                    permiteCancelarCheckBox.Focus();
                else
                {
                    if (SACPanel.Enabled)
                        TipoSACComboBox.Focus();
                    else
                        acessaAvaliacaoDesempenhoCheckBox.Focus();
                }
            }
        }

        private void categoriaGridDataBoundGrid_CellClick(object sender, GridCellClickEventArgs e)
        {
            if (_listaModulos != null && _listaModulos.Count() != 0)
            {
                int col = this.categoriaGridDataBoundGrid.CurrentCell.ColIndex;
                int linha = this.categoriaGridDataBoundGrid.CurrentCell.RowIndex;

                object value = this.categoriaGridDataBoundGrid.Model[linha, 2].CellValue;

                int _idCategoria = 0;
                foreach (var item in _listaCategorias.Where(w => w.Descricao == value.ToString()))
                {
                    _idCategoria = item.IdCategoria;
                }

                modulosGridDataBoundGrid.DataSource = null;
                modulosGridDataBoundGrid.DataSource = _listaModulos.Where(w => w.IdCategoria == _idCategoria).ToList();

            }
        }

        private void categoriaGridDataBoundGrid_QueryScrollCellInView(object sender, GridQueryScrollCellInViewEventArgs e)
        {
            if (_listaModulos != null && _listaModulos.Count() != 0)
            {
                int col = this.categoriaGridDataBoundGrid.CurrentCell.ColIndex;
                int linha = this.categoriaGridDataBoundGrid.CurrentCell.RowIndex;

                object value = this.categoriaGridDataBoundGrid.Model[linha, 2].CellValue;

                int _idCategoria = 0;
                foreach (var item in _listaCategorias.Where(w => w.Descricao == value.ToString()))
                {
                    _idCategoria = item.IdCategoria;
                }

                modulosGridDataBoundGrid.DataSource = null;
                modulosGridDataBoundGrid.DataSource = _listaModulos.Where(w => w.IdCategoria == _idCategoria).ToList();

            }
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new UsuarioBO().Excluir(_usuario.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new UsuarioBO().ExcluirEmpresasAutorizadas(_usuario.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new UsuarioBO().ExcluirCategoriasAutorizadas(_usuario.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new UsuarioBO().ExcluirModulosAutorizadas(_usuario.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu o usuário " + _usuario.UsuarioAcesso;
            _log.Tela = "Cadastro de Usuários";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            buttonAdv1_Click(sender, e);// limpar
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuarioTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o login de acesso do usuário.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o nome do usuário.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }
            
            if (senhaTextBox.Text.Trim().Length < 6)
            {
                new Notificacoes.Mensagem("Senha deve ter no mínimo 6 caracteres.", Publicas.TipoMensagem.Alerta).ShowDialog();
                senhaTextBox.Focus();
                return;
            }

            _usuario.Nome = nomeTextBox.Text;
            _usuario.Ativo = ativoCheckBox.Checked;
            _usuario.IpMaquina = ipMaquinaTextBox.Text;
            _usuario.NomeMaquina = nomeMaquinaTextBox.Text;

            _usuario.Ramal =  0;
            if (!string.IsNullOrEmpty(ramalTextBox.Text.Trim()))
                _usuario.Ramal = Convert.ToInt32(ramalTextBox.Text);

            _usuario.Telefone = 0;
            if (!string.IsNullOrEmpty(telefoneTextBox.ClipText.Trim()))
                _usuario.Telefone = Convert.ToInt32(telefoneTextBox.ClipText);

            _usuario.UsuarioAcesso = usuarioTextBox.Text;
            _usuario.Senha = senhaTextBox.Text;
            _usuario.Setor = setorTextBox.Text;
            _usuario.Cargo = descricaoCargoTextBox.Text;

            if (!string.IsNullOrEmpty(cargoTextBox.Text))
                _usuario.IdCargo = Convert.ToInt32(cargoTextBox.Text);

            _usuario.Email = emailTextBox.Text;
            _usuario.EmailAcessoPowerBi = emailPowerBITextBox.Text;
            _usuario.EmailDepartamento = emailDepartamentoTextBox.Text;
            
            _usuario.DataNascimento = dataNascimentoDateTimePicker.Value;
            _usuario.Tipo = (tipoComboBox.SelectedIndex == 0 ? Publicas.TipoUsuario.Socilitante :
                            (tipoComboBox.SelectedIndex == 1 ? Publicas.TipoUsuario.Atendente : Publicas.TipoUsuario.Todos));

            _usuario.TipoSac = (TipoSACComboBox.SelectedIndex == 0 ? Publicas.TipoUsuarioSAC.Atendente :
                                (TipoSACComboBox.SelectedIndex == 1 ? Publicas.TipoUsuarioSAC.UsuarioComum :
                                (TipoSACComboBox.SelectedIndex == 2 ? Publicas.TipoUsuarioSAC.Finalizador :
                                Publicas.TipoUsuarioSAC.Administrador)));

            _usuario.Administrador = administradorCheckBox.Checked;
            _usuario.AcessaAgenda = acessaAgendaCheckBox.Checked;
            _usuario.AcessaBI = acessaBICheckBox.Checked;
            _usuario.AcessaChat = acessaChatCheckBox.Checked;
            _usuario.AcessaSac = acessaSACCheckBox.Checked;
            _usuario.PermiteExcluirChat = excluiHistoricoCheckBox.Checked;
            _usuario.PermiteIncluirExcluirFoto = incluifotosCheckBox.Checked;
            _usuario.AcessaDescontoBeneficio = acessaBeneficiosCheckBox.Checked;
            _usuario.AcessaJuridico = acessaJuridicoCheckBox.Checked;
            _usuario.AcessaCadastroJuridico = acessaCadastrosJuridicosCheckBox.Checked;
            _usuario.PermiteAprovarComunicado = permiteAprovarCheckBox.Checked;
            _usuario.PermiteCancelarComunicado = permiteCancelarCheckBox.Checked;
            _usuario.PermiteReprovarComunicado = permiteReprovarCheckBox.Checked;
            _usuario.PermiteAlterarComunicado = permiteAlterarCheckBox.Checked;
            _usuario.PermiteFinalizarComunicado = permiteFinalizarCheckBox.Checked;
            _usuario.AcessaDashBoardChamados = acessaDashBoardChamadosCheckBoxAdv.Checked;
            _usuario.AcessaAvaliacaoDesempenho = acessaAvaliacaoDesempenhoCheckBox.Checked;
            _usuario.AcessoDeRH = acessoRHCheckBox.Checked;
            _usuario.AcessoDeGestor = acessoGestorCheckBox.Checked;
            _usuario.AcessoDeColaborador = acessoColaboradorCheckBox.Checked;
            _usuario.AcessoDeControladoria = acessoControladoriaCheckBox.Checked;
            _usuario.VisualizaRadarCompleto = visualizaRadarCompletoCheckBox.Checked;
            _usuario.VisualizaBancoHorasDoDepartamento = visualizaBancoHorasDeptoCheckBox.Checked;
            _usuario.ParticipaBolaoCopa = ParticipaBolaoCopaCheckBox.Checked;
            _usuario.AdministraBolaoCopa = AdministraBolaoCheckBox.Checked;
            _usuario.AdministraBiblioteca = AdministraBibliotecaCheckBox.Checked;
            _usuario.AdministraCorridas = AdministraCorridasCheckBox.Checked;

            if (!string.IsNullOrEmpty(setorTextBox.Text.Trim()))
                _usuario.IdDepartamento = Convert.ToInt32(setorTextBox.Text);
            
            _usuario.CPF = 0;
            
            if (!string.IsNullOrEmpty(cpfMaskedEditBox.ClipText.Trim()))
                _usuario.CPF = Convert.ToDecimal(cpfMaskedEditBox.ClipText);

            try
            {
                foreach (var item in _empresas.Where(w => w.Nome == grupoChatcomboBoxAdv.Text))
                {
                    _usuario.IdEmpresa = item.IdEmpresa;
                } 
                
            }
            catch { }

            if (_usuario.IdEmpresa != 0)
            {
                Empresa _empresa = new EmpresaBO().Consultar(_usuario.IdEmpresa);

                if (!string.IsNullOrEmpty(codigoFuncionarioTextBox.Text.Trim()))
                {
                    _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(codigoFuncionarioTextBox.Text.PadLeft(6, '0'), _empresa.CodigoEmpresaGlobus);
                    _usuario.CodigoInternoFuncionarioGlobus = _funcionariosGlobus.Id;
                }
            }


            if (!new UsuarioBO().Gravar(_usuario))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new UsuarioBO().GravarEmpresasAutorizadas(_listaEmpresas.Where(w => w.EmpresaAutoriza).ToList(), Publicas._idUsuarioNovo))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new UsuarioBO().GravarCategoriasAutorizadas(_listaCategorias.Where(w => w.CategoriaAutoriza).ToList(), Publicas._idUsuarioNovo))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new UsuarioBO().GravarModulosAutorizadas(_listaModulos.Where(w => w.ModuloAutoriza).ToList(), Publicas._idUsuarioNovo))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;

            _log.Descricao = (_usuario.Existe ? "Alterou " : "Incluiu ") + "o usuário " + _usuario.UsuarioAcesso +
                (!_usuario.Existe ? "" :
                (_usuario.AcessaAgenda == _usuarioLog.AcessaAgenda ? "" : " [AcessaAgenda] de " + _usuarioLog.AcessaAgenda.ToString() + " para " + _usuario.AcessaAgenda.ToString() + "") +
                (_usuario.AcessaBI == _usuarioLog.AcessaBI ? "" : " [AcessaBI] de " + _usuarioLog.AcessaBI.ToString() + " para " + _usuario.AcessaBI.ToString() + "") +
                (_usuario.AcessaChat == _usuarioLog.AcessaChat ? "" : " [AcessaChat] de " + _usuarioLog.AcessaChat.ToString() + " para " + _usuario.AcessaChat.ToString() + "") +
                (_usuario.AcessaDescontoBeneficio == _usuarioLog.AcessaDescontoBeneficio ? "" : " [AcessaDescontoBeneficio] de " + _usuarioLog.AcessaDescontoBeneficio.ToString() + " para " + _usuario.AcessaDescontoBeneficio.ToString() + "") +
                (_usuario.AcessaJuridico == _usuarioLog.AcessaJuridico ? "" : " [AcessaJuridico] de " + _usuarioLog.AcessaJuridico.ToString() + " para " + _usuario.AcessaJuridico.ToString() + "") +
                (_usuario.AcessaSac == _usuarioLog.AcessaSac ? "" : " [AcessaSac] de " + _usuarioLog.AcessaSac.ToString() + " para " + _usuario.AcessaSac.ToString() + "") +
                (_usuario.Administrador == _usuarioLog.Administrador ? "" : " [Administrador] de " + _usuarioLog.Administrador.ToString() + " para " + _usuario.Administrador.ToString() + "") +
                (_usuario.Ativo == _usuarioLog.Ativo ? "" : " [Ativo] de " + _usuarioLog.Ativo.ToString() + " para " + _usuario.Ativo.ToString() + "") +
                (_usuario.Cargo == _usuarioLog.Cargo ? "" : " [Cargo] de " + _usuarioLog.Cargo + " para " + _usuario.Cargo + "") +
                (_usuario.CodigoInternoFuncionarioGlobus == _usuarioLog.CodigoInternoFuncionarioGlobus ? "" : " [CodigoInternoFuncionarioGlobus] de " + _usuarioLog.CodigoInternoFuncionarioGlobus.ToString() + " para " + _usuario.CodigoInternoFuncionarioGlobus.ToString() + "") +
                (_usuario.CPF == _usuarioLog.CPF ? "" : " [CPF] de " + _usuarioLog.CPF + " para " + _usuario.CPF + "") +
                (_usuario.DataNascimento == _usuarioLog.DataNascimento ? "" : " [DataNascimento] de " + _usuarioLog.DataNascimento.ToString() + " para " + _usuario.DataNascimento.ToString() + "") +
                (_usuario.Email == _usuarioLog.Email ? "" : " [Email] de " + _usuarioLog.Email + " para " + _usuario.Email + "") +
                (_usuario.EmailAcessoPowerBi == _usuarioLog.EmailAcessoPowerBi ? "" : " [EmailAcessoPowerBi] de " + _usuarioLog.EmailAcessoPowerBi.ToString() + " para " + _usuario.EmailAcessoPowerBi.ToString() + "") +
                (_usuario.IdDepartamento == _usuarioLog.IdDepartamento ? "" : " [IdDepartamento] de " + _usuarioLog.IdDepartamento.ToString() + " para " + _usuario.IdDepartamento.ToString() + "") +
                (_usuario.IdEmpresa == _usuarioLog.IdEmpresa ? "" : " [IdEmpresa] de " + _usuarioLog.IdEmpresa.ToString() + " para " + _usuario.IdEmpresa.ToString() + "") +
                (_usuario.IpMaquina == _usuarioLog.IpMaquina ? "" : " [IpMaquina] de " + _usuarioLog.IpMaquina + " para " + _usuario.IpMaquina + "") +
                (_usuario.Nome == _usuarioLog.Nome ? "" : " [Nome] de " + _usuarioLog.Nome + " para " + _usuario.Nome + "") +
                (_usuario.NomeMaquina == _usuarioLog.NomeMaquina ? "" : " [NomeMaquina] de " + _usuarioLog.NomeMaquina + " para " + _usuario.NomeMaquina + "") +
                (_usuario.PermiteExcluirChat == _usuarioLog.PermiteExcluirChat ? "" : "[PermiteExcluirChat] de " + _usuarioLog.PermiteExcluirChat.ToString() + " para " + _usuario.PermiteExcluirChat.ToString() + "") +
                (_usuario.PermiteIncluirExcluirFoto == _usuarioLog.PermiteIncluirExcluirFoto ? "" : "[PermiteIncluirExcluirFoto] de " + _usuarioLog.PermiteIncluirExcluirFoto.ToString() + " para " + _usuario.PermiteIncluirExcluirFoto.ToString() + "") +
                (_usuario.Ramal == _usuarioLog.Ramal ? "" : " [Ramal] de " + _usuarioLog.Ramal.ToString() + " para " + _usuario.Ramal.ToString() + "") +
                (_usuario.Senha == _usuarioLog.Senha ? "" : " [Senha]") +
                (_usuario.Telefone == _usuarioLog.Telefone ? "" : " [Telefone] de " + _usuarioLog.Telefone.ToString() + " para " + _usuario.Telefone.ToString() + "") +
                (_usuario.Tipo == _usuarioLog.Tipo ? "" : " [Tipo] de " + _usuarioLog.Tipo + " para " + _usuario.Tipo + "") +
                (_usuario.TipoSac == _usuarioLog.TipoSac ? "" : " [TipoSac] de " + _usuarioLog.TipoSac + " para " + _usuario.TipoSac + "")
            );
            _log.Tela = "Cadastro de Usuários";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            buttonAdv1_Click(sender, e);// limpar
        }

        private void setorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ipMaquinaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                grupoChatcomboBoxAdv.Focus();
            }
        }

        private void grupoChatcomboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                setorTextBox.Focus();
                
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cargoTextBox.Focus();
            }

        }

        private void acessaSACCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                acessaBeneficiosCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessaBICheckBox.Focus();
            }
        }

        private void TipoSACComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (JuridicoPanel.Enabled)
                    acessaCadastrosJuridicosCheckBox.Focus();
                else
                {
                    if (emailPowerBITextBox.Enabled)
                        emailPowerBITextBox.Focus();
                    else
                    {
                        diversosTabControl.SelectedTab = autorizacoesTabPage;
                        empresasGridDataBoundGrid.Focus();
                    }
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessaAvaliacaoDesempenhoCheckBox.Focus();
            }
        }

        private void codigoFuncionarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                emailTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ipMaquinaTextBox.Focus();
            }
        }

        private void setorTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (String.IsNullOrEmpty(setorTextBox.Text))
            {
                new Pesquisas.Departamentos().ShowDialog();

                setorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (setorTextBox.Text == "" || setorTextBox.Text == "0")
                {
                    setorTextBox.Text = string.Empty;
                    setorTextBox.Focus();
                    return;
                }

            }

            _departamento = new DepartamentoBO().Consultar(Convert.ToInt32( setorTextBox.Text));
                
            if (_departamento == null || !_departamento.Existe)
            {
                new Notificacoes.Mensagem("Departamento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                setorTextBox.Focus();
                return;
            }

            if (!_departamento.Ativo)
            {
                new Notificacoes.Mensagem("Departamento Inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                setorTextBox.Focus();
                return;
            }

            setorTextBox.Text = _departamento.Id.ToString();
            descricaoDeptoTextBox.Text = _departamento.Descricao;            

            if (Publicas._idRetornoPesquisa != 0)
                ipMaquinaTextBox.Focus();
        }

        private void departamentoLabel_Click(object sender, EventArgs e)
        {
            // abrir cadastro de departamento
            new Departamentos().ShowDialog();
        }

        private void cpfMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void cpfMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void cpfMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataNascimentoDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                senhaTextBox.Focus();
            }
        }

        private void emailDepartamentoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                diversosTabControl.SelectedTab = opcoesTabPage;
                ativoCheckBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeMaquinaTextBox.Focus();
            }

        }

        private void acessaBeneficiosCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                acessaJuridicoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessaSACCheckBox.Focus();
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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                emailDepartamentoTextBox.Focus();
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

        private void departamentoLabel_MouseHover(object sender, EventArgs e)
        {
            departamentoLabel.Cursor = Cursors.Hand;
        }

        private void departamentoLabel_MouseLeave(object sender, EventArgs e)
        {
            departamentoLabel.Cursor = Cursors.Default;
        }

        private void acessaJuridicoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                excluiHistoricoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessaBeneficiosCheckBox.Focus();
            }
        }

        private void usuarioTextBox_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void setorTextBox_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void usuarioTextBox_MouseHover(object sender, EventArgs e)
        {

        }

        private void usuarioTextBox_MouseLeave(object sender, EventArgs e)
        {
            usuarioTextBox.Cursor = Cursors.Default;
        }

        private void setorTextBox_MouseHover(object sender, EventArgs e)
        {

        }

        private void setorTextBox_MouseLeave(object sender, EventArgs e)
        {
            setorTextBox.Cursor = Cursors.Default;
        }

        private void acessaCadastrosJuridicosCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                permiteAprovarCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (SACPanel.Enabled)
                    TipoSACComboBox.Focus();
                else
                    acessaAvaliacaoDesempenhoCheckBox.Focus();
            }
        }

        private void permiteAprovarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                permiteReprovarCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessaCadastrosJuridicosCheckBox.Focus();
            }
        }

        private void permiteReprovarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                permiteAlterarCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                permiteAprovarCheckBox.Focus();
            }
        }

        private void permiteCancelarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (emailPowerBITextBox.Enabled)
                    emailPowerBITextBox.Focus();
                else
                {
                    diversosTabControl.SelectedTab = autorizacoesTabPage;
                    empresasGridDataBoundGrid.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                permiteFinalizarCheckBox.Focus();
            }
        }

        private void acessaBICheckBox_CheckedChanged(object sender, EventArgs e)
        {
            emailPowerBITextBox.Enabled = acessaBICheckBox.Checked;
        }

        private void acessaSACCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SACPanel.Enabled = acessaSACCheckBox.Checked;
        }

        private void acessaJuridicoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            JuridicoPanel.Enabled = acessaJuridicoCheckBox.Checked;
        }

        private void permiteAlterarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                permiteFinalizarCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                permiteReprovarCheckBox.Focus();
            }
        }

        private void permiteFinalizarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                permiteCancelarCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                permiteAlterarCheckBox.Focus();
            }
        }

        private void acessaDashBoardChamadosCheckBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                acessaAvaliacaoDesempenhoCheckBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                incluifotosCheckBox.Focus();
            }
        }

        private void avaliacaoDesempenhoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (SACPanel.Enabled)
                    TipoSACComboBox.Focus();
                else
                {
                    if (JuridicoPanel.Enabled)
                        acessaCadastrosJuridicosCheckBox.Focus();
                    else
                        if (emailPowerBITextBox.Enabled)
                        emailPowerBITextBox.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessaDashBoardChamadosCheckBoxAdv.Focus();
            }
        }

        private void acessoRHCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                acessoGestorCheckBox.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (emailPowerBITextBox.Enabled)
                    emailPowerBITextBox.Focus();
                else
                {
                    if (JuridicoPanel.Enabled)
                        permiteCancelarCheckBox.Focus();
                    else
                    {
                        if (SACPanel.Enabled)
                            TipoSACComboBox.Focus();
                        else
                            acessaAvaliacaoDesempenhoCheckBox.Focus();
                    }
                }
            }
        }

        private void acessoColaboradorCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                acessoControladoriaCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessoGestorCheckBox.Focus();
            }
        }

        private void acessoGestorCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)            
                acessoColaboradorCheckBox.Focus();
            
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessoRHCheckBox.Focus();
            }
        }

        private void acessaAvaliacaoDesempenhoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            avaliacaoDesempenhoPanel.Enabled = acessaAvaliacaoDesempenhoCheckBox.Checked;
        }

        private void usuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void cargoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cargoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (String.IsNullOrEmpty(cargoTextBox.Text))
            {
                new Pesquisas.Cargos().ShowDialog();

                cargoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (cargoTextBox.Text == "" || cargoTextBox.Text == "0")
                {
                    cargoTextBox.Text = string.Empty;
                    cargoTextBox.Focus();
                    return;
                }
            }

            _cargos = new CargosBO().Consultar(Convert.ToInt32(cargoTextBox.Text));

            if (_cargos == null || !_cargos.Existe)
            {
                new Notificacoes.Mensagem("Cargo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                cargoTextBox.Focus();
                return;
            }

            if (!_cargos.Ativo)
            {
                new Notificacoes.Mensagem("Cargo não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                cargoTextBox.Focus();
                return;
            }

            cargoTextBox.Text = _cargos.Id.ToString();
            descricaoCargoTextBox.Text = _cargos.Descricao;
        }

        private void cargoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCargoButton.Enabled = string.IsNullOrEmpty(cargoTextBox.Text.Trim());
        }

        private void setorTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaDepartamentoButton.Enabled = string.IsNullOrEmpty(setorTextBox.Text.Trim());
        }

        private void setorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            new Cadastros.Cargos().ShowDialog();
        }

        private void cargoLabel_MouseHover(object sender, EventArgs e)
        {
            cargoLabel.Cursor = Cursors.Hand;
        }

        private void cargoLabel_MouseLeave(object sender, EventArgs e)
        {
            cargoLabel.Cursor = Cursors.Default;
        }

        private void pesquisaCargoButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cargoTextBox.Text))
            {
                new Pesquisas.Cargos().ShowDialog();

                cargoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (cargoTextBox.Text == "" || cargoTextBox.Text == "0")
                {
                    cargoTextBox.Text = string.Empty;
                    cargoTextBox.Focus();
                    return;
                }

                cargoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaDepartamentoButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(setorTextBox.Text))
            {
                new Pesquisas.Departamentos().ShowDialog();

                setorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (setorTextBox.Text == "" || setorTextBox.Text == "0")
                {
                    setorTextBox.Text = string.Empty;
                    setorTextBox.Focus();
                    return;
                }
                setorTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void acessoControladoriaCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                diversosTabControl.SelectedTab = autorizacoesTabPage;
                empresasGridDataBoundGrid.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                acessoColaboradorCheckBox.Focus();
            }
        }

        private void acessoGestorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            visualizaRadarCompletoCheckBox.Enabled = acessoGestorCheckBox.Checked;

            if (acessoColaboradorCheckBox.Checked)
                visualizaRadarCompletoCheckBox.Checked = false;

            if (acessoRHCheckBox.Checked)
                visualizaRadarCompletoCheckBox.Checked = true;
        }

        private void acessaChatCheckBox_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void excluiHistoricoCheckBox_CheckStateChanged(object sender, EventArgs e)
        {

        }
    }
    
}
