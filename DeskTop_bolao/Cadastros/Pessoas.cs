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
    public partial class Pessoas : Form
    {
        public Pessoas()
        {
            InitializeComponent();

            #region Ajustes campos
            salarioCurrencyTextBox.BackGroundColor = usuarioTextBox.BackColor;
            valorCurrencyTextBox.BackGroundColor = usuarioTextBox.BackColor;
            duracaoCurrencyTextBox.BackGroundColor = usuarioTextBox.BackColor;
            duracaoCurrencyTextBox.BackGroundColor = usuarioTextBox.BackColor;

            cursoGridGroupingControl.BackColor = usuarioTextBox.BackColor;
            competenciasGridGroupingControl.BackColor = usuarioTextBox.BackColor;
            tecnicasGridGroupingControl.BackColor = usuarioTextBox.BackColor;

            dataNascimentoDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataNascimentoDateTimePicker.BackColor = usuarioTextBox.BackColor;

            dataAdmissaoDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataAdmissaoDateTimePicker.BackColor = usuarioTextBox.BackColor;

            dataDesligamentoDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataDesligamentoDateTimePicker.BackColor = usuarioTextBox.BackColor;

            inicioDateTimePicker.BorderColor = Publicas._bordaSaida;
            inicioDateTimePicker.BackColor = usuarioTextBox.BackColor;

            fimDateTimePicker.BorderColor = Publicas._bordaSaida;
            fimDateTimePicker.BackColor = usuarioTextBox.BackColor;
            #endregion

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.CompetenciasDoColaborador> _listaComportamentais;
        List<Classes.CompetenciasDoColaborador> _listaTecnicas;
        List<Classes.Cursos> _listaCursos;
        List<Classes.HistoricosDoColaborador> _listaHistorico;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _listaEmpresasAvaliados;

        Classes.Empresa _empresa;
        Classes.Empresa _empresaDoSuperior;
        Classes.FuncionariosGlobus _funcionariosGlobus;
        Classes.FuncionariosGlobus _superiorFuncionariosGlobus;
        Classes.Colaboradores _colaboradores;
        Classes.Colaboradores _supervisorColaboradores;
        Classes.Departamento _departamento;
        Classes.Cargos _cargos;
        Classes.Escolaridade _escolaridade;
        Classes.Usuario _usuario;

        GridDynamicFilter filter = new GridDynamicFilter();
        GridMetroColors metroColor = new GridMetroColors();

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

        private void Pessoas_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            empresaDoSuperiorComboBox.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaDoSuperiorComboBox.DisplayMember = "CodigoeNome";
            empresaDoSuperiorComboBox.Focus();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.cursoGridGroupingControl);

            cursoGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            cursoGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            cursoGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            cursoGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            cursoGridGroupingControl.RecordNavigationBar.Label = "Cursos";
            cursoGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < cursoGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                cursoGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                cursoGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                cursoGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                cursoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                cursoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                cursoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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

            this.cursoGridGroupingControl.SetMetroStyle(metroColor);

            this.cursoGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.cursoGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.cursoGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.cursoGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            filter.WireGrid(this.empresasGridGroupingControl);
            empresasGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            empresasGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            empresasGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            empresasGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            empresasGridGroupingControl.RecordNavigationBar.Label = "Cursos";
            empresasGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < empresasGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0)
                    empresasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                empresasGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                empresasGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                empresasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                empresasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                empresasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }
            
            this.empresasGridGroupingControl.SetMetroStyle(metroColor);

            this.empresasGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.empresasGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.empresasGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.empresasGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            filter.WireGrid(this.historicoGridGroupingControl);

            historicoGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            historicoGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            historicoGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            historicoGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            historicoGridGroupingControl.RecordNavigationBar.Label = "Cursos";
            historicoGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < historicoGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                historicoGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                historicoGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                historicoGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                historicoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                historicoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                historicoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }
                        
            this.historicoGridGroupingControl.SetMetroStyle(metroColor);

            this.historicoGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.historicoGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.historicoGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.historicoGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            empresaComboBoxAdv.Focus();
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                usuarioTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void usuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                setorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void dataNascimentoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataAdmissaoDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                usuarioTextBox.Focus();
            }
        }

        private void dataAdmissaoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataDesligamentoDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataNascimentoDateTimePicker.Focus();
            }
        }

        private void dataDesligamentoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                salarioCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataAdmissaoDateTimePicker.Focus();
            }
        }

        private void salarioCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                setorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataDesligamentoDateTimePicker.Focus();
            }
        }

        private void setorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                escolaridadeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                usuarioTextBox.Focus();
            }
        }

        private void cargoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                empresaDoSuperiorComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                escolaridadeTextBox.Focus();
            }
        }

        private void supervisorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                tabControlAdv1.SelectedTab = EmpresasTabPage;
                cursoGridGroupingControl.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaDoSuperiorComboBox.Focus();
            }
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                tabControlAdv1.SelectedTab = EmpresasTabPage;
                cursoGridGroupingControl.Focus();
            }
        }

        private void cursoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                valorCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cursoTextBox.Focus();
            }
        }

        private void valorCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                duracaoCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cursoTextBox.Focus();
            }
        }

        private void duracaoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inicioDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                valorCurrencyTextBox.Focus();
            }
        }

        private void inicioDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                fimDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                duracaoCurrencyTextBox.Focus();
            }
        }

        private void fimDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                obrigatorioCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                inicioDateTimePicker.Focus();
            }
        }

        private void obrigatorioCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                opniaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                obrigatorioCheckBox.Focus();
            }
        }

        private void opniaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cursoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                obrigatorioCheckBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cursoGridGroupingControl.Focus();
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

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            usuarioTextBox.BorderColor = Publicas._bordaEntrada;

            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void dataNascimentoDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void valorCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void cpfMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
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

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            try
            {
                foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
                {
                    _empresa = item;
                }                
            }
            catch { }

            // Coloca a mesma empresa do colaborador para o superior, podendo alterar
            empresaDoSuperiorComboBox.Text = empresaComboBoxAdv.Text;
        }

        private void usuarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            usuarioTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaUsuarioButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.Funcionarios().ShowDialog();

                usuarioTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(usuarioTextBox.Text) || usuarioTextBox.Text == "0")
                {
                    usuarioTextBox.Text = string.Empty;
                    usuarioTextBox.Focus();
                    return;
                }
            }

            usuarioTextBox.Text = usuarioTextBox.Text.PadLeft(6, '0');
            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(usuarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, usuarioTextBox.Text, true);

            _listaEmpresas = new EmpresaBO().Listar(true);

            if (!_funcionariosGlobus.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            if (_funcionariosGlobus.DataDesligamento != DateTime.MinValue && !_funcionariosGlobus.Ativo)
            {
                new Notificacoes.Mensagem("Colaborador desligado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            _usuario = new UsuarioBO().ConsultaUsuarioPorCodigoFuncionarioGlobus(_funcionariosGlobus.Id);
            
            nomeTextBox.Text = _funcionariosGlobus.Nome;
            dataAdmissaoDateTimePicker.Value = _funcionariosGlobus.DataAdmissao;
            dataNascimentoDateTimePicker.Value = _funcionariosGlobus.DataNascimento;

            dataDesligamentoDateTimePicker.Visible = (_funcionariosGlobus.DataDesligamento != DateTime.MinValue);
            dataDesligamentoLabel.Visible = dataDesligamentoDateTimePicker.Visible;

            dataDesligamentoDateTimePicker.Value = _funcionariosGlobus.DataDesligamento;
            cpfMaskedEditBox.Text = _funcionariosGlobus.CPF;
            salarioCurrencyTextBox.DecimalValue = _funcionariosGlobus.Salario;

            acessaAvaliacaoDesempenhoCheckBox.Checked = _usuario.AcessaAvaliacaoDesempenho;
            acessoColaboradorCheckBox.Checked = _usuario.AcessoDeColaborador;
            acessoControladoriaCheckBox.Checked = _usuario.AcessoDeControladoria;
            acessoGestorCheckBox.Checked = _usuario.AcessoDeGestor;
            acessoRHCheckBox.Checked = _usuario.AcessoDeRH;

            if (_colaboradores.Existe)
            {
                // buscar as empresas

                _listaEmpresasAvaliados = new ColaboradoresBO().Listar(_colaboradores.Id);

                if (_listaEmpresasAvaliados == null || _listaEmpresasAvaliados.Count() == 0)
                {
                    if (_listaEmpresasAvaliados == null)
                        _listaEmpresasAvaliados = new List<EmpresaQueOColaboradorEhAvaliado>();

                    foreach (var item in _listaEmpresas.Where(w => w.AvaliaColaboradores))
                    {
                        EmpresaQueOColaboradorEhAvaliado _fator = new EmpresaQueOColaboradorEhAvaliado();

                        _fator.IdEmpresa = item.IdEmpresa;
                        _fator.Empresa = item.NomeAbreviado;

                        _listaEmpresasAvaliados.Add(_fator);
                    }
                }
                
                setorTextBox.Text = _colaboradores.IdDepartamento.ToString();
                if (_colaboradores.IdDepartamento != 0)
                {
                    setorTextBox_Validating(sender, e);
                }

                cargoTextBox.Text = _colaboradores.IdCargo.ToString();
                participaAvaliacaoCheckBox.Checked = _colaboradores.ParticipaDaAvaliacao;

                if (_colaboradores.IdCargo != 0)
                {
                    cargoTextBox_Validating(sender, e);
                }

                if (_colaboradores.IdSupervisor != 0)
                {
                    _supervisorColaboradores = new ColaboradoresBO().Consultar(_colaboradores.IdSupervisor);

                    foreach (var item in _listaEmpresas.Where(w => w.IdEmpresa == _supervisorColaboradores.IdEmpresa))
                    {
                        empresaDoSuperiorComboBox.Text = item.CodigoeNome;
                    }
                    nomeSupervisorTextBox.Text = _supervisorColaboradores.Nome;
                    supervisorTextBox.Text = _supervisorColaboradores.Codigo;
                }

                salarioCurrencyTextBox.DecimalValue = _colaboradores.Salario;
                
                #region Cursos
                _listaCursos = new CursosBO().Listar(_colaboradores.Id);
                cursoGridGroupingControl.DataSource = _listaCursos;
                #endregion

                #region Historico 
                _listaHistorico = new HistoricosDoColaboradorBO().Listar(_colaboradores.Id);
                historicoGridGroupingControl.DataSource = _listaHistorico.OrderByDescending(o => o.Data).ToList();
                #endregion

                #region  Escolaridade
                escolaridadeTextBox.Text = _colaboradores.IdEscolaridade.ToString();
                escolaridadeTextBox_Validating(sender, e);
                #endregion
            }
            else
            {
                if (_listaEmpresasAvaliados == null)
                    _listaEmpresasAvaliados = new List<EmpresaQueOColaboradorEhAvaliado>();

                foreach (var item in _listaEmpresas.Where(w => w.AvaliaColaboradores))
                {
                    EmpresaQueOColaboradorEhAvaliado _fator = new EmpresaQueOColaboradorEhAvaliado();

                    _fator.IdEmpresa = item.IdEmpresa;
                    _fator.Empresa = item.NomeAbreviado;

                    _listaEmpresasAvaliados.Add(_fator);
                }
            }

            empresasGridGroupingControl.DataSource = _listaEmpresasAvaliados;
            excluirButton.Enabled = _colaboradores.Existe;
            gravarButton.Enabled = true;
            
        }

        private void salarioCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void setorTextBox_Validating(object sender, CancelEventArgs e)
        {
            setorTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaDepartamentoButton.Enabled = false;
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

            _departamento = new DepartamentoBO().Consultar(Convert.ToInt32(setorTextBox.Text));

            if (_departamento == null || !_departamento.Existe)
            {
                new Notificacoes.Mensagem("Departamento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                setorTextBox.Focus();
                return;
            }

            if (!_departamento.Ativo)
            {
                new Notificacoes.Mensagem("Departamento não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                setorTextBox.Focus();
                return;
            }

            setorTextBox.Text = _departamento.Id.ToString();
            descricaoDeptoTextBox.Text = _departamento.Descricao;
        }

        private void cargoTextBox_Validating(object sender, CancelEventArgs e)
        {
            cargoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaCargoButton.Enabled = false;
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

            #region Carrega dados das competencias para o cargo
            _listaComportamentais = new CompetenciasDoColaboradorBO().Listar(_cargos.Id, _colaboradores.Id, "C");
            _listaTecnicas = new CompetenciasDoColaboradorBO().Listar(_cargos.Id, _colaboradores.Id, "T");

            #region grid Competencias

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.competenciasGridGroupingControl);
            filter.WireGrid(this.tecnicasGridGroupingControl);

            competenciasGridGroupingControl.DataSource = _listaComportamentais;
            competenciasGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            competenciasGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            competenciasGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            competenciasGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            competenciasGridGroupingControl.RecordNavigationBar.Label = "Comportamentais";
            competenciasGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < competenciasGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                competenciasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;

                if (i != 0)
                    competenciasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                competenciasGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                competenciasGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                competenciasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                competenciasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                competenciasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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

            this.competenciasGridGroupingControl.SetMetroStyle(metroColor);

            this.competenciasGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.competenciasGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.competenciasGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.competenciasGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            tecnicasGridGroupingControl.DataSource = _listaTecnicas;
            tecnicasGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            tecnicasGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            tecnicasGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            tecnicasGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            tecnicasGridGroupingControl.RecordNavigationBar.Label = "Técnicas";
            tecnicasGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < tecnicasGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                tecnicasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                if (i != 0)
                    tecnicasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                tecnicasGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                tecnicasGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                tecnicasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                tecnicasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                tecnicasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            this.tecnicasGridGroupingControl.SetMetroStyle(metroColor);

            this.tecnicasGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.tecnicasGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.tecnicasGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.tecnicasGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            #endregion
            #endregion
        }

        private void supervisorTextBox_Validating(object sender, CancelEventArgs e)
        {
            supervisorTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaSuperiorButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                tabControlAdv1.SelectedTab = EmpresasTabPage;
                cursoGridGroupingControl.Focus();
                return;
            }

            if (String.IsNullOrEmpty(supervisorTextBox.Text))
            {
                Publicas._codigoEmpresaGlobus = _empresaDoSuperior.CodigoEmpresaGlobus;
                new Pesquisas.Funcionarios().ShowDialog();

                supervisorTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (supervisorTextBox.Text == "" || supervisorTextBox.Text == "0")
                {
                    supervisorTextBox.Text = string.Empty;
                    supervisorTextBox.Focus();
                    return;
                }
            }

            supervisorTextBox.Text = supervisorTextBox.Text.PadLeft(6, '0');

            _superiorFuncionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(supervisorTextBox.Text, _empresaDoSuperior.CodigoEmpresaGlobus);

            _supervisorColaboradores = new ColaboradoresBO().Consultar(_empresaDoSuperior.CodigoEmpresaGlobus, supervisorTextBox.Text, true);

            if (!_superiorFuncionariosGlobus.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                supervisorTextBox.Focus();
                return;
            }

            if (_superiorFuncionariosGlobus.DataDesligamento != DateTime.MinValue && !_funcionariosGlobus.Ativo)
            {
                new Notificacoes.Mensagem("Colaborador desligado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                supervisorTextBox.Focus();
                return;
            }

            if (!_supervisorColaboradores.Existe)
            {
                new Notificacoes.Mensagem("Supervisor não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                supervisorTextBox.Focus();
                return;
            }
            nomeSupervisorTextBox.Text = _superiorFuncionariosGlobus.Nome;
        }

        private void cursoTextBox_Validating(object sender, CancelEventArgs e)
        {
            cursoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            cursoGridGroupingControl.DataSource = new List<Classes.Cursos>();

            if (string.IsNullOrEmpty(cursoTextBox.Text.Trim()))
            {
                cursoGridGroupingControl.DataSource = _listaCursos;
                cursoGridGroupingControl.Visible = true;
                cursoGridGroupingControl.Focus();
                cursosPanel.Visible = false;
                return;
            }

            if (_listaCursos == null)
                _listaCursos = new List<Cursos>();

            foreach (var item in _listaCursos.Where(w => w.Descricao.ToUpper() == cursoTextBox.Text.ToUpper()))
            {
                valorCurrencyTextBox.DecimalValue = item.Valor;
                duracaoCurrencyTextBox.DecimalValue = item.Duracao;
                inicioDateTimePicker.Value = item.Inicio;
                fimDateTimePicker.Value = item.Fim;
                obrigatorioCheckBox.Checked = item.Obrigatorio;
                opniaoTextBox.Text = item.Opniao;
            }
        }        

        private void opniaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            opniaoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (_listaCursos == null)
                _listaCursos = new List<Cursos>();

            bool existe = false;
            foreach (var item in _listaCursos.Where(w => w.Descricao.ToUpper() == cursoTextBox.Text.ToUpper()))
            {
                existe = true;
                item.Valor = valorCurrencyTextBox.DecimalValue;
                item.Duracao = (int)duracaoCurrencyTextBox.DecimalValue;
                item.Inicio = inicioDateTimePicker.Value;
                item.Fim = fimDateTimePicker.Value;
                item.Obrigatorio = obrigatorioCheckBox.Checked;
                item.Opniao = opniaoTextBox.Text;
            }

            if (!existe)
            {
                Classes.Cursos _curso = new Cursos();

                _curso.Descricao = cursoTextBox.Text;
                _curso.Valor = valorCurrencyTextBox.DecimalValue;
                _curso.Duracao = (int)duracaoCurrencyTextBox.DecimalValue;
                _curso.Inicio = inicioDateTimePicker.Value;
                _curso.Fim = fimDateTimePicker.Value;
                _curso.Obrigatorio = obrigatorioCheckBox.Checked;
                _curso.Opniao = opniaoTextBox.Text;
                _listaCursos.Add(_curso);
            }

            valorCurrencyTextBox.DecimalValue = 0;
            duracaoCurrencyTextBox.DecimalValue = 0;
            inicioDateTimePicker.Value = DateTime.MinValue;
            fimDateTimePicker.Value = DateTime.MinValue;
            obrigatorioCheckBox.Checked = false;
            opniaoTextBox.Text = string.Empty;
            cursoTextBox.Text = string.Empty;
            cursoTextBox.Focus();
        }

        private void inicioDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            inicioDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (inicioDateTimePicker.Value == DateTime.MinValue)
            {
                new Notificacoes.Mensagem("Data inicio do curso deve ser informada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                inicioDateTimePicker.Focus();
                return;
            }
        }

        private void fimDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            fimDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (fimDateTimePicker.Value != DateTime.MinValue && inicioDateTimePicker.Value <= DateTime.MinValue)
            {
                new Notificacoes.Mensagem("Data inicio do curso deve ser menor que a data final.", Publicas.TipoMensagem.Alerta).ShowDialog();
                fimDateTimePicker.Focus();
                return;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        { // incluir
            cursoGridGroupingControl.Visible = false;
            cursosPanel.Visible = true;
            cursoTextBox.Focus();
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            usuarioTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            cargoTextBox.Text = string.Empty;
            setorTextBox.Text = string.Empty;
            supervisorTextBox.Text = string.Empty;
            descricaoCargoTextBox.Text = string.Empty;
            descricaoDeptoTextBox.Text = string.Empty;
            nomeSupervisorTextBox.Text = string.Empty;
            cpfMaskedEditBox.Text = string.Empty;
            cursoTextBox.Text = string.Empty;
            opniaoTextBox.Text = string.Empty;
            dataAdmissaoDateTimePicker.Value = DateTime.MinValue;
            dataNascimentoDateTimePicker.Value = DateTime.MinValue;
            dataDesligamentoDateTimePicker.Value = DateTime.MinValue;
            inicioDateTimePicker.Value = DateTime.MinValue;
            fimDateTimePicker.Value = DateTime.MinValue;
            salarioCurrencyTextBox.DecimalValue = 0;
            valorCurrencyTextBox.DecimalValue = 0;
            duracaoCurrencyTextBox.DecimalValue = 0;
            escolaridadeTextBox.Text = string.Empty;
            descricaoEscolaridadeTextBox.Text = string.Empty;
            empresaComboBoxAdv.Focus();
            
            if (_listaHistorico != null)
                _listaCursos.Clear();

            if (_listaTecnicas != null)
                _listaTecnicas.Clear();

            if (_listaComportamentais != null)
                _listaComportamentais.Clear();

            if (_listaHistorico != null)
                _listaHistorico.Clear();

            if (_listaEmpresasAvaliados != null)
                _listaEmpresasAvaliados.Clear();

            cursoGridGroupingControl.DataSource = new List<Classes.Cursos>();
            competenciasGridGroupingControl.DataSource = new List<Classes.CompetenciasDoColaborador>();
            tecnicasGridGroupingControl.DataSource = new List<Classes.CompetenciasDoColaborador>();
            historicoGridGroupingControl.DataSource = new List<Classes.HistoricosDoColaborador>();
            empresasGridGroupingControl.DataSource = new List<EmpresaQueOColaboradorEhAvaliado>();

            cursoGridGroupingControl.Visible = true;
            cursosPanel.Visible = false;
            tabControlAdv1.SelectedTab = EmpresasTabPage;
            pesquisaSuperiorButton.Enabled = false;
            pesquisaEscolaridadeButton.Enabled = false;
            pesquisaDepartamentoButton.Enabled = false;
            pesquisaCargoButton.Enabled = false;
            pesquisaUsuarioButton.Enabled = false;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(setorTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o departamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                setorTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cargoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o cargo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                cargoTextBox.Focus();
                return;
            }

            if (_colaboradores == null)
                _colaboradores = new Colaboradores();

            if (_listaCursos == null)
                _listaCursos = new List<Cursos>();

            if (_supervisorColaboradores == null)
                _supervisorColaboradores = new Colaboradores();

            HistoricosDoColaborador _historico = null;

            try
            {
                if (_colaboradores.Existe &&
                    (_colaboradores.IdCargo != _cargos.Id || _colaboradores.IdDepartamento != _departamento.Id ||
                     _colaboradores.IdSupervisor != _supervisorColaboradores.Id || //_colaboradores.Salario != salarioCurrencyTextBox.DecimalValue ||
                     _historico.IdEscolaridade != _escolaridade.Id))
                {
                    _historico = new HistoricosDoColaborador();

                    if (_colaboradores.IdCargo != _cargos.Id)
                        _historico.IdCargo = _cargos.Id;

                    if (_colaboradores.IdDepartamento != _departamento.Id)
                        _historico.IdDepartamento = _departamento.Id;

                    if (!string.IsNullOrEmpty(supervisorTextBox.Text))
                    {
                        if (_colaboradores.IdSupervisor != _supervisorColaboradores.Id)
                            _historico.IdSuperior = _supervisorColaboradores.Id;
                    }

                    if (_colaboradores.Salario != salarioCurrencyTextBox.DecimalValue)
                        _historico.Salario = salarioCurrencyTextBox.DecimalValue;

                    if (_historico.IdEscolaridade != _escolaridade.Id)
                        _historico.IdEscolaridade = _escolaridade.Id;
                }
                else
                {
                    if (!_colaboradores.Existe)
                    {
                        _historico = new HistoricosDoColaborador();

                        _historico.IdCargo = _cargos.Id;
                        _historico.IdDepartamento = _departamento.Id;

                        if (!string.IsNullOrEmpty(supervisorTextBox.Text.Trim()))
                            _historico.IdSuperior = _supervisorColaboradores.Id;

                        _historico.Salario = salarioCurrencyTextBox.DecimalValue;
                        _historico.IdEscolaridade = _escolaridade.Id;
                    }
                }
            }
            catch { }

            _colaboradores.CodigoInternoFuncionarioGlobus = _funcionariosGlobus.Id;
            _colaboradores.IdCargo = _cargos.Id;
            _colaboradores.IdDepartamento = _departamento.Id;
            _colaboradores.IdEmpresa = _empresa.IdEmpresa;
            _colaboradores.Nome = nomeTextBox.Text;
            _colaboradores.ParticipaDaAvaliacao = participaAvaliacaoCheckBox.Checked;

            if (!string.IsNullOrEmpty(supervisorTextBox.Text.Trim()))
                _colaboradores.IdSupervisor = _supervisorColaboradores.Id;

            _colaboradores.DataAdmissao = dataAdmissaoDateTimePicker.Value;
            _colaboradores.DataNascimento = dataNascimentoDateTimePicker.Value;

            if (dataDesligamentoDateTimePicker.Value != DateTime.MinValue)
                _colaboradores.DataDesligamento = dataDesligamentoDateTimePicker.Value;

            _colaboradores.IdEscolaridade = _escolaridade.Id;
            _colaboradores.Salario = salarioCurrencyTextBox.DecimalValue;

            List<Classes.CompetenciasDoColaborador> _listaCompentencias = new List<CompetenciasDoColaborador>();

            if (_listaComportamentais.Where(w => w.Marcado).Count() != 0)
                _listaCompentencias.AddRange(_listaComportamentais.Where(w => w.Marcado));

            if (_listaTecnicas.Where(w => w.Marcado).Count() != 0)
                _listaCompentencias.AddRange(_listaTecnicas.Where(w => w.Marcado));

            //if (!new ColaboradoresBO().Gravar(_colaboradores, _listaCursos, _historico, _listaCompentencias, _listaEmpresasAvaliados))
            //{
            //    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
            //    return;
            //}

            if (_usuario != null)
            {
                _usuario.AcessaAvaliacaoDesempenho = acessaAvaliacaoDesempenhoCheckBox.Checked;
                _usuario.AcessoDeColaborador = acessoColaboradorCheckBox.Checked;
                _usuario.AcessoDeControladoria = acessoControladoriaCheckBox.Checked;
                _usuario.AcessoDeGestor = acessoGestorCheckBox.Checked;
                _usuario.AcessoDeRH = acessoRHCheckBox.Checked;
                _usuario.IdDepartamento = _colaboradores.IdDepartamento;
                _usuario.IdCargo = _colaboradores.IdCargo;
                _usuario.IdEmpresa = _colaboradores.IdEmpresa;

                new UsuarioBO().Gravar(_usuario);
            }

            limparButton_Click(sender, e);
        }

        private void cargoLabel_Click(object sender, EventArgs e)
        {
            new Cadastros.Cargos().ShowDialog();
        }

        private void departamentoLabel_Click(object sender, EventArgs e)
        {
            new Cadastros.Departamentos().ShowDialog();
        }

        private void departamentoLabel_MouseHover(object sender, EventArgs e)
        {
            departamentoLabel.Cursor = Cursors.Hand;
        }

        private void departamentoLabel_MouseLeave(object sender, EventArgs e)
        {
            departamentoLabel.Cursor = Cursors.Default;
        }

        private void cargoLabel_MouseHover(object sender, EventArgs e)
        {
            cargoLabel.Cursor = Cursors.Hand;
        }

        private void cargoLabel_MouseLeave(object sender, EventArgs e)
        {
            cargoLabel.Cursor = Cursors.Default;
        }

        private void escolaridadeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cargoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                setorTextBox.Focus();
            }
        }

        private void escolaridadeTextBox_Validating(object sender, CancelEventArgs e)
        {
            escolaridadeTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaEscolaridadeButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }
            
            if (String.IsNullOrEmpty(escolaridadeTextBox.Text))
            {
                new Pesquisas.Escolaridade().ShowDialog();

                escolaridadeTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (escolaridadeTextBox.Text == "" || escolaridadeTextBox.Text == "0")
                {
                    escolaridadeTextBox.Text = string.Empty;
                    escolaridadeTextBox.Focus();
                    return;
                }
            }

            _escolaridade = new EscolaridadeBO().Consultar(Convert.ToInt32( escolaridadeTextBox.Text));

            if (!_escolaridade.Existe)
            {
                new Notificacoes.Mensagem("Escalaridade não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                escolaridadeTextBox.Focus();
                return;
            }

            if (!_escolaridade.Ativo)
            {
                new Notificacoes.Mensagem("Escolaridade não está ativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                escolaridadeTextBox.Focus();
                return;
            }
                        
            descricaoEscolaridadeTextBox.Text = _escolaridade.Descricao;
        }

        private void escolaridadeLabel_MouseHover(object sender, EventArgs e)
        {
            escolaridadeLabel.Cursor = Cursors.Hand;
        }

        private void escolaridadeLabel_MouseLeave(object sender, EventArgs e)
        {
            escolaridadeLabel.Cursor = Cursors.Default;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new ColaboradoresBO().Excluir(_colaboradores))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void setorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void usuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void setorTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaDepartamentoButton.Enabled = string.IsNullOrEmpty(setorTextBox.Text.Trim());
        }

        private void escolaridadeTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaEscolaridadeButton.Enabled = string.IsNullOrEmpty(escolaridadeTextBox.Text.Trim());
        }

        private void cargoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCargoButton.Enabled = string.IsNullOrEmpty(cargoTextBox.Text.Trim());
        }

        private void supervisorTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaSuperiorButton.Enabled = string.IsNullOrEmpty(supervisorTextBox.Text.Trim());
        }

        private void pesquisaUsuarioButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuarioTextBox.Text))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._idSuperior = 0;
                new Pesquisas.Funcionarios().ShowDialog();

                usuarioTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(usuarioTextBox.Text) || usuarioTextBox.Text == "0")
                {
                    usuarioTextBox.Text = string.Empty;
                    usuarioTextBox.Focus();
                    return;
                }

                usuarioTextBox_Validating(sender, new CancelEventArgs());
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

        private void pesquisaEscolaridadeButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(escolaridadeTextBox.Text))
            {
                new Pesquisas.Escolaridade().ShowDialog();

                escolaridadeTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (escolaridadeTextBox.Text == "" || escolaridadeTextBox.Text == "0")
                {
                    escolaridadeTextBox.Text = string.Empty;
                    escolaridadeTextBox.Focus();
                    return;
                }

                escolaridadeTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaSuperiorButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(supervisorTextBox.Text))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.Funcionarios().ShowDialog();

                supervisorTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (supervisorTextBox.Text == "" || supervisorTextBox.Text == "0")
                {
                    supervisorTextBox.Text = string.Empty;
                    supervisorTextBox.Focus();
                    return;
                }

                supervisorTextBox_Validating(sender, new CancelEventArgs());
            }
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

        private void empresaDoSuperiorComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                supervisorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cargoTextBox.Focus();
            }
        }

        private void empresaDoSuperiorComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            try
            {
                foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaDoSuperiorComboBox.Text))
                {
                    _empresaDoSuperior = item;
                }
            }
            catch { }
        }

        private void pesquisaSuperiorButton_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(supervisorTextBox.Text))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._idSuperior = 0;
                new Pesquisas.Funcionarios().ShowDialog();

                supervisorTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(supervisorTextBox.Text) || supervisorTextBox.Text == "0")
                {
                    supervisorTextBox.Text = string.Empty;
                    supervisorTextBox.Focus();
                    return;
                }

                supervisorTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void setorTextBox_Enter(object sender, EventArgs e)
        {
            setorTextBox.BorderColor = Publicas._bordaEntrada;

            pesquisaDepartamentoButton.Enabled = string.IsNullOrEmpty(setorTextBox.Text.Trim());
        }

        private void cargoTextBox_Enter(object sender, EventArgs e)
        {
            cargoTextBox.BorderColor = Publicas._bordaEntrada;

            pesquisaCargoButton.Enabled = string.IsNullOrEmpty(cargoTextBox.Text.Trim());
        }

        private void escolaridadeTextBox_Enter(object sender, EventArgs e)
        {
            escolaridadeTextBox.BorderColor = Publicas._bordaEntrada;

            pesquisaEscolaridadeButton.Enabled = string.IsNullOrEmpty(escolaridadeTextBox.Text.Trim());
        }

        private void supervisorTextBox_Enter(object sender, EventArgs e)
        {
            supervisorTextBox.BorderColor = Publicas._bordaEntrada;

            pesquisaSuperiorButton.Enabled = string.IsNullOrEmpty(supervisorTextBox.Text.Trim());
        }

        private void acessoRHCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            visualizaRadarCompletoCheckBox.Enabled = acessoGestorCheckBox.Checked;

            if (acessoColaboradorCheckBox.Checked)
                visualizaRadarCompletoCheckBox.Checked = false;

            if (acessoRHCheckBox.Checked)
                visualizaRadarCompletoCheckBox.Checked = true;
        }
    }
}
