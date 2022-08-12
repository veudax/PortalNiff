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

namespace Suportte.GestaoEstrategica
{
    public partial class CopiaContasContabeisDaMeta : Form
    {
        public CopiaContasContabeisDaMeta()
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
                    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl1.BackColor = Publicas._panelTitulo;

                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        public Classes.Metas _metas;
        public Classes.Empresa _empresa;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;
        public Classes.RateioBeneficios.PlanoContabil _plano;
        public List<Classes.MetasContasContabeis> _listaMetas;

        private void CopiaContasContabeisDaMeta_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);
            _listaEmpresasAutorizadas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._idUsuario);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.gridGroupingControl1);

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl1.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 22;
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Metas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _metas = new MetasBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (!_metas.Existe)
            {
                new Notificacoes.Mensagem("Meta não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _metas.Descricao;

            _listaMetas = new MetasBO().ListarContasMetas(_metas.Id);

            gridGroupingControl1.DataSource = new List<MetasContasContabeis>();
            gridGroupingControl1.DataSource = _listaMetas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.Plano == _plano.NumeroPlano).ToList();
            gravarButton.Enabled = true;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaMetas.Where(w => w.Marcado).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma conta contábil selecionada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }
            Close();
        }

        private void marcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listaMetas.ForEach(u => u.Marcado = true);
            gridGroupingControl1.DataSource = new List<MetasContasContabeis>();
            gridGroupingControl1.DataSource = _listaMetas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.Plano == _plano.NumeroPlano).ToList();
        }

        private void desmarcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listaMetas.ForEach(u => u.Marcado = false);
            gridGroupingControl1.DataSource = new List<MetasContasContabeis>();
            gridGroupingControl1.DataSource = _listaMetas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.Plano == _plano.NumeroPlano).ToList();
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
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

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
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

            if (_listaEmpresasAutorizadas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.EmpresaAutoriza).Count() == 0)
            {
                new Notificacoes.Mensagem("Usuário não autorizado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }
        }

        private void PlanoTextBox_Validating(object sender, CancelEventArgs e)
        {
            PlanoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (PlanoTextBox.Text.Trim() == "")
            {
                new Pesquisas.PlanoContabil().ShowDialog();

                PlanoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (PlanoTextBox.Text.Trim() == "" || PlanoTextBox.Text == "0")
                {
                    PlanoTextBox.Text = string.Empty;
                    PlanoTextBox.Focus();
                    return;
                }
            }

            _plano = new RateioBeneficioBO().Consultar(Convert.ToInt32(PlanoTextBox.Text));

            if (!_plano.Existe)
            {
                new Notificacoes.Mensagem("Plano contábil não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                PlanoTextBox.Focus();
                return;
            }

            NomePlanoTextBox.Text = _plano.Nome;

            _listaMetas = new MetasBO().ListarContasMetas(_empresa.IdEmpresa, _plano.NumeroPlano, _metas.Id);

            gridGroupingControl1.DataSource = new List<MetasContasContabeis>();
            gridGroupingControl1.DataSource = _listaMetas.Where(w => w.IdEmpresa == _empresa.IdEmpresa 
                                                                  && w.Plano == _plano.NumeroPlano 
                                                                  && w.IdMetas == _metas.Id).ToList();
            gravarButton.Enabled = true;
        }
    }
}
