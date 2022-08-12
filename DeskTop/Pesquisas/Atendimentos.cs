using Classes;
using DynamicFilter;
using Negocio;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.Pesquisas
{
    public partial class Atendimentos : Form
    {
        public Atendimentos()
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
        
        string value;

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

        private void Atendimentos_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
            // alterado para o showin
            Publicas._codigoRetornoPesquisa = string.Empty;
        }

        private void gridGroupingControl_TableControlCellClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            try
            {
                value = e.TableControl.Table.CurrentRecord.GetRecord().Info;
            }
            catch { }
        }

        private void gridGroupingControl_TableControlCurrentCellKeyUp(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlKeyEventArgs e)
        {
            try
            {
                value = e.TableControl.Table.CurrentRecord.GetRecord().Info;
            }
            catch { }
        }

        private void confirmarButton_Click(object sender, EventArgs e)
        {
            int posIniId = 0;
            int posFimId = 0;

            try
            {
                posIniId = value.IndexOf("Codigo =") + 9;
                posFimId = value.IndexOf(", IdUsuario");
                Publicas._codigoRetornoPesquisa = value.Substring(posIniId, posFimId - posIniId).Trim();
                Close();
            }            
            catch { }
        }

        private void Atendimentos_Shown(object sender, EventArgs e)
        {
            mensagemSistemaLabel.Text = "Pesquisando atedimentos...";
            this.UseWaitCursor = true;
            this.Refresh();

            List<Atendimento> _lista;

            if (Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Atendimento)
                _lista = new AtendimentoBO().Listar(Publicas._usuario.IdEmpresa,
                                                    Publicas.TelaPesquisaSAC.Atendimento);
            else
            {
                if (Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Finaliza)
                    _lista = new AtendimentoBO().Listar(Publicas._usuario.IdEmpresa,
                                                        Publicas.TelaPesquisaSAC.Finaliza);
                else
                {
                    if (Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Responde)
                        _lista = new AtendimentoBO().Listar(Publicas._usuario.IdEmpresa,
                                                            Publicas.TelaPesquisaSAC.Responde,
                                                            Publicas._usuario.IdDepartamento);
                    else
                    {
                        if (Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Retorno)
                            _lista = new AtendimentoBO().Listar(Publicas._usuario.IdEmpresa,
                                                            Publicas.TelaPesquisaSAC.Retorno);
                        else
                           _lista = new AtendimentoBO().Listar(Publicas._usuario.IdEmpresa,
                                                            Publicas.TelaPesquisaSAC.Satisfacao);
                    }
                }
            }

            gridGroupingControl.DataSource = _lista.OrderBy(o => o.DataAbertura).ToList();

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.gridGroupingControl);

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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
                this.gridGroupingControl.SetMetroStyle(metroColor);
                this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            mensagemSistemaLabel.Text = "";
            this.UseWaitCursor = false;
            this.Refresh();

            gridGroupingControl.Focus();
        }


        private void confirmarButton_Enter(object sender, EventArgs e)
        {
            confirmarButton.BackColor = Publicas._botaoFocado;
            confirmarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void confirmarButton_Validating(object sender, CancelEventArgs e)
        {
            confirmarButton.ForeColor = Publicas._fonteBotao;
            confirmarButton.BackColor = Publicas._botao;
        }

        private void confirmarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gridGroupingControl.Focus();
            }
        }

        private void gridGroupingControl_TableControlCurrentCellKeyDown(object sender, GridTableControlKeyEventArgs e)
        {
            if (e.Inner.KeyCode == Keys.Enter || e.Inner.KeyCode == Keys.Return)
                confirmarButton.Focus();
            Publicas._escTeclado = false;
            if (e.Inner.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

    }
}
