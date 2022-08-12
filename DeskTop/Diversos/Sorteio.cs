using Classes;
using Negocio;
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

namespace Suportte.Diversos
{
    public partial class Sorteio : Form
    {
        public Sorteio()
        {
            InitializeComponent();

            dataDateTimePicker.Value = DateTime.Now;
            dataDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataDateTimePicker.BackColor = QuantidadeGrupoTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Sorteio> _listaSorteio;
        List<Classes.Colaboradores> _listaColaboradores;
        List<Classes.FuncionariosGlobus> _listaFuncionarios;
        List<Classes.ParticipantesSorteio> _listaParticipantes;

        int[] arr;
        int reg = 1;
        int i = 0;
        int grupo = 0;

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

        private void Sorteio_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);


            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
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

            dataDateTimePicker.Value = DateTime.Now.Date;
            CriarGruposCheckBox_CheckStateChanged(sender, e);
            SepararSexoCheckBox_CheckStateChanged(sender, e);

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
            MulherGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            MulherGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(MulherGrid);
            filter.WireGrid(HomensGrid);
            filter.WireGrid(SorteioGrid);

            MulherGrid.SortIconPlacement = SortIconPlacement.Left;
            MulherGrid.TopLevelGroupOptions.ShowFilterBar = true;
            MulherGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            MulherGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            MulherGrid.RecordNavigationBar.Label = "Colaboradores";

            for (int i = 0; i < MulherGrid.TableDescriptor.Columns.Count; i++)
            {
                MulherGrid.TableDescriptor.Columns[i].AllowFilter = true;
                MulherGrid.TableDescriptor.Columns[i].ReadOnly = false;
                MulherGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                MulherGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                MulherGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }           

            MulherGrid.SetMetroStyle(metroColor);
            MulherGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            MulherGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            HomensGrid.SortIconPlacement = SortIconPlacement.Left;
            HomensGrid.TopLevelGroupOptions.ShowFilterBar = true;
            HomensGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            HomensGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            HomensGrid.RecordNavigationBar.Label = "Colaboradores";

            for (int i = 0; i < HomensGrid.TableDescriptor.Columns.Count; i++)
            {
                HomensGrid.TableDescriptor.Columns[i].AllowFilter = true;
                HomensGrid.TableDescriptor.Columns[i].ReadOnly = false;
                HomensGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                HomensGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                HomensGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            HomensGrid.SetMetroStyle(metroColor);
            HomensGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            HomensGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            SorteioGrid.SortIconPlacement = SortIconPlacement.Left;
            SorteioGrid.TopLevelGroupOptions.ShowFilterBar = true;
            SorteioGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            SorteioGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            SorteioGrid.RecordNavigationBar.Label = "Colaboradores";

            for (int i = 0; i < SorteioGrid.TableDescriptor.Columns.Count; i++)
            {
                SorteioGrid.TableDescriptor.Columns[i].AllowFilter = true;
                SorteioGrid.TableDescriptor.Columns[i].ReadOnly = false;
                SorteioGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                SorteioGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                SorteioGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            SorteioGrid.SetMetroStyle(metroColor);
            SorteioGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            SorteioGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            empresaComboBoxAdv.FlatBorderColor = Publicas._bordaSaida;

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

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SepararSexoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void SepararSexoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SomenteConfirmadosCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void SomenteConfirmadosCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CriarGruposCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SepararSexoCheckBox.Focus();
            }
        }

        private void CriarGruposCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SomenteAtivosCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SomenteConfirmadosCheckBox.Focus();
            }
        }

        private void SomenteAtivosCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (QuantidadeGrupoTextBox.Enabled)
                    QuantidadeGrupoTextBox.Focus();
                else
                    QuantidadeTextBox.Focus();

            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CriarGruposCheckBox.Focus();
            }
        }

        private void QuantidadeGrupoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CarregarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SomenteAtivosCheckBox.Focus();
            }
        }

        private void QuantidadeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CarregarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (QuantidadeGrupoTextBox.Enabled)
                    QuantidadeGrupoTextBox.Focus();
                else
                    SomenteAtivosCheckBox.Focus();
            }
        }

        private void CarregarButton_Click(object sender, EventArgs e)
        {
            if (CriarGruposCheckBox.Checked && string.IsNullOrEmpty(QuantidadeGrupoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a quantidade de grupos.", Publicas.TipoMensagem.Alerta).ShowDialog();
                QuantidadeGrupoTextBox.Focus();
                return;
            }

            if (!CriarGruposCheckBox.Checked && string.IsNullOrEmpty(QuantidadeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a quantidade de colaboradores a ser sortiado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                QuantidadeTextBox.Focus();
                return;
            }

            _listaParticipantes = new List<ParticipantesSorteio>();

            if (SomenteConfirmadosCheckBox.Checked)
            {
                // buscar os colaboradores
                _listaColaboradores = new ColaboradoresBO().ListarColaboradoresParticipantes(_empresa.IdEmpresa, 7, true);

                foreach (var item in _listaColaboradores.OrderBy(o => o.Nome))
                {
                    ParticipantesSorteio _part = new ParticipantesSorteio();
                    _part.Id = item.Id;
                    _part.Nome = item.Nome;
                    _part.Sexo = item.Sexo;
                    _part.Sorteado = false;

                    _listaParticipantes.Add(_part);
                }                
            }
            else
            {
                // buscar os funcionários 
                _listaFuncionarios = new FuncionariosGlobusBO().Listar(_empresa.CodigoEmpresaGlobus, SomenteAtivosCheckBox.Checked);

                foreach (var item in _listaFuncionarios.OrderBy(o => o.Nome))
                {
                    ParticipantesSorteio _part = new ParticipantesSorteio();
                    _part.Id = item.Id;
                    _part.Nome = item.Nome;
                    _part.Sexo = item.Sexo;
                    _part.Sorteado = false;

                    _listaParticipantes.Add(_part);
                }
            }
            if (!SepararSexoCheckBox.Checked)
                HomensGrid.DataSource = _listaParticipantes;
            else
            {
                MulherGrid.DataSource = _listaParticipantes.Where(w => w.Sexo == "F").ToList();
                HomensGrid.DataSource = _listaParticipantes.Where(w => w.Sexo == "M").ToList();
            }

            gravarButton.Enabled = _listaParticipantes.Count() != 0;
        }

        private void CriarGruposCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            QuantidadeGrupoTextBox.Enabled = CriarGruposCheckBox.Checked;
            QuantidadeTextBox.Enabled = !CriarGruposCheckBox.Checked;
        }

        private void SepararSexoCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            FemininoPanel.Visible = SepararSexoCheckBox.Checked;
            pictureBox2.Visible = SepararSexoCheckBox.Checked;
            MasculinoPanel.Dock = (SepararSexoCheckBox.Checked ? DockStyle.Right : DockStyle.Fill);
            TituloMasculinoLabel.Text = (SepararSexoCheckBox.Checked ? "Homens" : "Participantes");
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            _listaSorteio = new List<Classes.Sorteio>();
            arr = new int[_listaParticipantes.Count()];
            Publicas.RetornaPosicaoAleatoria(_listaParticipantes.Count() , 1, arr);
            i = 1;
            grupo = 0;

            if (CriarGruposCheckBox.Checked)
            {
                #region cria grupos
                if (SepararSexoCheckBox.Checked)
                {
                    arr = new int[_listaParticipantes.Where(w => w.Sexo == "F").Count()];
                    Publicas.RetornaPosicaoAleatoria(_listaParticipantes.Where(w => w.Sexo == "F").Count() , 1, arr);

                    SortearPeloSexo("F", Convert.ToInt32(QuantidadeGrupoTextBox.Text));
                    reg = 1;

                    arr = new int[_listaParticipantes.Where(w => w.Sexo == "M").Count()];
                    Publicas.RetornaPosicaoAleatoria(_listaParticipantes.Where(w => w.Sexo == "M").Count(), 1, arr);

                    SortearPeloSexo("M", Convert.ToInt32(QuantidadeGrupoTextBox.Text));                    
                }
                else
                {
                    int x = 0;
                    
                    while (_listaParticipantes.Where(w => !w.Sorteado).Count() != 0)
                    {
                        while (i <= Convert.ToInt32(QuantidadeGrupoTextBox.Text) && _listaParticipantes.Where(w => !w.Sorteado).Count() != 0)
                        {
                            int cont = 1;
                            int pos = arr[x];

                            foreach (var item in _listaParticipantes)
                            {
                                Classes.Sorteio _sorteio = new Classes.Sorteio();

                                if (cont != pos)
                                {
                                    cont++;
                                    continue;
                                }

                                _sorteio.IdGrupo = grupo + 1;
                                _sorteio.IdColaborador = item.Id;
                                _sorteio.Nome = item.Nome;
                                _sorteio.PosicaoSorteio = reg;
                                _listaSorteio.Add(_sorteio);
                                break;
                            }
                            reg++;
                            i++;
                            x++;
                            if (x >= arr.Length)
                                break;
                        }
                        grupo++;
                        i = 1;
                        reg = 1;
                        if (x >= arr.Length)
                            break;
                    }
                }
                #endregion
            }
            else
            {
                #region sem grupos

                reg = 1;
                if (SepararSexoCheckBox.Checked)
                {
                    arr = new int[_listaParticipantes.Where(w => w.Sexo == "F").Count()];
                    Publicas.RetornaPosicaoAleatoria(_listaParticipantes.Where(w => w.Sexo == "F").Count(), 1, arr);

                    SortearPeloSexo("F", Convert.ToInt32(QuantidadeTextBox.Text));
                    reg = 1;
                    i = 1;

                    arr = new int[_listaParticipantes.Where(w => w.Sexo == "M").Count()];
                    Publicas.RetornaPosicaoAleatoria(_listaParticipantes.Where(w => w.Sexo == "M").Count(), 1, arr);

                    SortearPeloSexo("M", Convert.ToInt32(QuantidadeTextBox.Text));
                }
                else
                {
                    while (i <= Convert.ToInt32(QuantidadeTextBox.Text))
                    {
                        int cont = 1;
                        int pos = arr[i];

                        foreach (var item in _listaParticipantes)
                        {
                            Classes.Sorteio _sorteio = new Classes.Sorteio();

                            if (cont != pos)
                            {
                                cont++;
                                continue;
                            }

                            _sorteio.IdGrupo = 1;
                            _sorteio.IdColaborador = item.Id;
                            _sorteio.Nome = item.Nome;
                            _sorteio.PosicaoSorteio = reg;
                            _listaSorteio.Add(_sorteio);
                            break;
                        }
                        reg++;
                        i++;

                        if (i >= arr.Length)
                            break;
                    }
                }
                #endregion
            }

            SorteioGrid.DataSource = _listaSorteio;
            tabControlAdv1.SelectedTab = SorteioTabPage;
        }

        private void SortearPeloSexo(string Sexo, int Quantidade)
        {
            int x = 0;
            while (_listaParticipantes.Where(w => !w.Sorteado && w.Sexo == Sexo).Count() != 0)
            {
                while (i <= Quantidade && _listaParticipantes.Where(w => !w.Sorteado && w.Sexo == Sexo).Count() != 0)
                {
                    int cont = 1;
                    
                    int pos = arr[x];

                    foreach (var item in _listaParticipantes.Where(w => w.Sexo == Sexo))
                    {

                        Classes.Sorteio _sorteio = new Classes.Sorteio();

                        if (cont != pos)
                        {
                            cont++;
                            continue;
                        }

                        _sorteio.IdGrupo = grupo + 1;
                        _sorteio.IdColaborador = item.Id;
                        _sorteio.Nome = item.Nome;
                        _sorteio.PosicaoSorteio = reg;
                        _listaSorteio.Add(_sorteio);
                        break;
                    }
                    reg++;
                    i++;
                    x++;
                    if (x >= arr.Length)
                        break;
                }

                if (!CriarGruposCheckBox.Checked)
                    break;

                grupo++;
                i = 1;
                reg = 1;

                if (x >= arr.Length)
                    break;
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CarregarButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                 HomensGrid.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (QuantidadeTextBox.Enabled)
                    QuantidadeTextBox.Focus();
                else
                    QuantidadeGrupoTextBox.Focus();
            }
        }

        private void QuantidadeGrupoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void QuantidadeGrupoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void CarregarButton_Enter(object sender, EventArgs e)
        {
            CarregarButton.BackColor = Publicas._botaoFocado;
            CarregarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void CarregarButton_Validating(object sender, CancelEventArgs e)
        {
            CarregarButton.BackColor = Publicas._botao;
            CarregarButton.ForeColor = Publicas._fonteBotao;
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                HomensGrid.Focus();
            }
        }
    }

    
}
