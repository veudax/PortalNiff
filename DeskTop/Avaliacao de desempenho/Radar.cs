using Classes;
using DynamicFilter;
using Negocio;
using Suportte.Notificacoes;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.CellGrid;
using Syncfusion.Windows.Forms.Chart;
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

namespace Suportte.Avaliacao_de_desempenho
{
    public partial class Radar : Form
    {
        public Radar()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    gridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Colaboradores _colaboradores;
        Classes.Colaboradores _colaboradoresSuperior;
        List<Classes.AutoAvaliacao> _avaliacoes;
        List<Classes.AutoAvaliacao> _listaReferencias;
        Classes.FuncionariosGlobus _funcionariosGlobus;
        Classes.Cargos _cargos;

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        int _rowIndex = 0;

        public Publicas.TipoPrazos tipoAvaliacao;

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

        private void Radar_Shown(object sender, EventArgs e)
        {
            chartControl1.Visible = false;
            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
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

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl.SetMetroStyle(metroColor);
                this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl.Table.DefaultRecordRowHeight = 45;

            _listaEmpresas = new EmpresaBO().Listar(false);
            _empresaDoColaborador = new ColaboradoresBO().Listar(Publicas._idColaborador);
            if (Publicas._usuario.PermiteAlterarBSC || Publicas._telaRadarChamadaPeloMenu == "RH" || Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.IdEmpresa == 19)
                empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            else
            {
                _listaEmpresasAutorizadas = new List<Empresa>();
                foreach (var item in _empresaDoColaborador.Where(w => w.Inicio != DateTime.MinValue.Date &&
                                                                      (w.Fim == DateTime.MinValue.Date || w.Fim <= DateTime.Now.Date)))
                {
                    _listaEmpresasAutorizadas.AddRange(_listaEmpresas.Where(w => w.IdEmpresa == item.IdEmpresa));
                }

                empresaComboBoxAdv.DataSource = _listaEmpresasAutorizadas.OrderBy(o => o.CodigoeNome).ToList();
            }
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
            {
                empresaComboBoxAdv.SelectedIndex = i;
                if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                {
                    break;
                }
            }
            _colaboradoresSuperior = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas._usuario.RegistroFuncionario, false);

            if (!usuarioTextBox.Enabled)
            {
                usuarioTextBox.Text = Publicas._usuario.RegistroFuncionario;

                _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, usuarioTextBox.Text, false);
                nomeTextBox.Text = _colaboradores.Nome;

                _cargos = new CargosBO().Consultar(_colaboradores.IdCargo);
                descricaoCargoTextBox.Text = _cargos.Descricao;

                usuarioTextBox_Validating(sender, new CancelEventArgs());

                usuarioTextBox.ReadOnly = true;
                usuarioTextBox.Enabled = true;
            }

            empresaComboBoxAdv.Enabled = true;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                usuarioTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void usuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }
        }

        private void usuarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            usuarioTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaUsuarioButton.Enabled = false;
                usuarioTextBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._idSuperior = (!tituloLabel.Text.Contains("Gestor") ? 0 : _colaboradoresSuperior.Id);
                Publicas._participaAvaliacao = true;

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

            if (usuarioTextBox.Text == Publicas._usuario.RegistroFuncionario && Publicas._telaRadarChamadaPeloMenu != "Colaborador")
            {
                new Notificacoes.Mensagem("Registro do funcionário deve ser diferente do seu registro.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(usuarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, usuarioTextBox.Text, false);

            //if (!_funcionariosGlobus.Existe)
            //{
            //    new Notificacoes.Mensagem("Colaborador não cadastrado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    usuarioTextBox.Focus();
            //    return;
            //}

            //if (_funcionariosGlobus.DataDesligamento != DateTime.MinValue && !_funcionariosGlobus.Ativo)
            //{
            //    new Notificacoes.Mensagem("Colaborador desligado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    usuarioTextBox.Focus();
            //    return;
            //}

            if (_colaboradores.IdSupervisor != _colaboradoresSuperior.Id && Publicas._telaRadarChamadaPeloMenu == "Gestor" &&
                (Publicas._usuario.UsuarioAcesso != "JFERNANDES" && Publicas._usuario.UsuarioAcesso != "MDMUNOZ" && Publicas._usuario.UsuarioAcesso != "TIFELICIO"))
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado na sua equipe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _funcionariosGlobus.Nome;

            _cargos = new CargosBO().Consultar(_colaboradores.IdCargo);
            descricaoCargoTextBox.Text = _cargos.Descricao;

            _avaliacoes = new AutoAvaliacaoBO().Listar(_colaboradores.Id, (Publicas._telaRadarChamadaPeloMenu == "Colaborador" || 
                                                                           (Publicas._telaRadarChamadaPeloMenu == "Gestor" && !Publicas._usuario.VisualizaRadarCompleto) ? "Media" : "Todos"));

            if (_listaReferencias == null)
                _listaReferencias = new List<AutoAvaliacao>();
            else
                _listaReferencias.Clear();

            foreach (var item in _avaliacoes.OrderBy(o => o.Ano).OrderBy(o => o.MesReferencia))
            {
                AutoAvaliacao _ano = new AutoAvaliacao();
                _ano.Ano = item.Ano;
                _ano.MesReferencia = item.MesReferencia;
                _ano.ReferenciaFormatada = item.ReferenciaFormatada;

                if (_listaReferencias.Where(w => w.MesReferencia == _ano.MesReferencia).Count() == 0)
                    _listaReferencias.Add(_ano);
            }

            gridGroupingControl.DataSource = _listaReferencias;

            chartControl1.Series.Clear();

            foreach (var mes in _listaReferencias)
            {
                MontaGrafico(mes.MesReferencia);
                
                chartControl1.Visible = true;
                break;
            }
            
        }

        private void gridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                chartControl1.Series.Clear();

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;

                    if (dr != null)
                    {
                        MontaGrafico((int)dr["MesReferencia"]);
                    }
                }
            }
            catch { }            
        }

        private void MontaGrafico(int referencia)
        {
            string tipo = "";

            ChartSeries serieRH = null;

            foreach (var item in _avaliacoes.Where(w => w.MesReferencia == referencia)
                                            .OrderBy(o => o.Tipo))
            {

                if (tipo == "")
                {
                    tipo = Publicas.GetDescription(item.Tipo, "");
                    serieRH = new ChartSeries(Publicas.GetDescription(item.Tipo, ""), ChartSeriesType.Radar);
                }

                if (tipo != Publicas.GetDescription(item.Tipo, ""))
                {
                    chartControl1.Series.Add(serieRH);
                    tipo = Publicas.GetDescription(item.Tipo, "");
                    serieRH = new ChartSeries(Publicas.GetDescription(item.Tipo, ""), ChartSeriesType.Radar);
                }

                serieRH.Points.Add(item.Comentario, (double)item.TotalAvaliacao);
            }

            chartControl1.Series.Add(serieRH);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            chartControl1.Series.Clear();
            chartControl1.Visible = false;
            _listaReferencias.Clear();
            _avaliacoes.Clear();
            gridGroupingControl.DataSource = new List<AutoAvaliacao>();
            usuarioTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            descricaoCargoTextBox.Text = string.Empty;
            pesquisaUsuarioButton.Enabled = false;
            usuarioTextBox.Focus();
        }

        private void gridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                chartControl1.Series.Clear();

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;

                    if (dr != null)
                    {
                        MontaGrafico((int)dr["MesReferencia"]);
                    }
                }
            }
            catch { }
        }

        private void pesquisaUsuarioButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._idSuperior = (!tituloLabel.Text.Contains("Gestor") ? 0 : _colaboradoresSuperior.Id);
                Publicas._participaAvaliacao = true;

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

        private void usuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void Radar_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }
    }
}
