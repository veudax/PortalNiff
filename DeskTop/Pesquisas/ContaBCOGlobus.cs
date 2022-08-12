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
    public partial class ContaBCOGlobus : Form
    {
        public ContaBCOGlobus()
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

        private void confirmarButton_Click(object sender, EventArgs e)
        {
            try
            {
                value = gridGroupingControl.Table.CurrentRecord.GetRecord().Info;
            }
            catch { }
            int posIniId = 0;
            int posFimId = 0;

            try
            {
                posIniId = value.IndexOf("Conta =") + 7;
                posFimId = value.IndexOf(", Nome");
                Publicas._codigoRetornoPesquisa = value.Substring(posIniId, posFimId - posIniId).Trim();
            }
            catch { }
            finally
            {
                Close();
            }
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

        private void ContaBCOGlobus_Shown(object sender, EventArgs e)
        {
            List<Classes.Financeiro.ContaGlobus> _lista = new FinanceiroBO().ListarContasGlobus(Publicas._codigoEmpresaGlobus, Publicas._idRetornoPesquisa, Publicas._idInteiroAuxiliar);
            Publicas._codigoRetornoPesquisa = string.Empty;
            Publicas._idRetornoPesquisa = 0;
            gridGroupingControl.DataSource = _lista.OrderBy(o => o.Nome).ToList();

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

            gridGroupingControl.Focus();
        }

        private void ContaBCOGlobus_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }
    }
}
