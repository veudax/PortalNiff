using Classes;
using Negocio;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
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

namespace Suportte.Biblioteca
{
    public partial class Acompanhamento : Form
    {
        public Acompanhamento()
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

        List<Classes.EmprestimoLivros> _emprestimos;
        List<Classes.ReservaLivros> _reservas;
        List<Classes.ResenhaLivros> _resenha;

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

        private void Acompanhamento_Shown(object sender, EventArgs e)
        {
            _emprestimos = new EmprestimoLivrosBO().Listar();
            _reservas = new ReservaLivrosBO().Listar();

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.gridGroupingControl);
            filter.WireGrid(this.gridGroupingControl2);

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
            this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;

            this.gridGroupingControl.SetMetroStyle(metroColor);

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            gridGroupingControl.DataSource = _emprestimos.OrderBy(o => o.DataAcompanhamento).ToList();

            gridGroupingControl2.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl2.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl2.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl2.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl2.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl2.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl2.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl2.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl2.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            this.gridGroupingControl2.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.gridGroupingControl2.TableOptions.SelectionTextColor = Color.WhiteSmoke;

            this.gridGroupingControl2.SetMetroStyle(metroColor);

            this.gridGroupingControl2.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl2.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            gridGroupingControl2.DataSource = _reservas.OrderBy(o => o.DataSolicitado).ToList();

            gridGroupingControl.Focus();

            _resenha = new ResenhaLivrosBO().ListarPontuacao();

            PerguntasGrid.DataSource = _resenha;
            PerguntasGrid.SortIconPlacement = SortIconPlacement.Left;
            PerguntasGrid.TopLevelGroupOptions.ShowFilterBar = false;
            PerguntasGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            PerguntasGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            PerguntasGrid.TableControl.CellToolTip.Active = true;
            PerguntasGrid.RecordNavigationBar.Label = "Livros";

            for (int i = 0; i < PerguntasGrid.TableDescriptor.Columns.Count; i++)
            {
                PerguntasGrid.TableDescriptor.Columns[i].ReadOnly = false;
                PerguntasGrid.TableDescriptor.Columns[i].AllowSort = true;
            }

            this.PerguntasGrid.SetMetroStyle(metroColor);

            this.PerguntasGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.PerguntasGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.PerguntasGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.PerguntasGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;


        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridGroupingControl_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            Record dr;
            try
            { 
                if (e.TableCellIdentity.RowIndex != -1)
                {
                    GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    //if (rec != null)
                    {
                        dr = rec.GetRecord() as Record;
                        if (dr != null && (DateTime)dr["DataAcompanhamento"] <= DateTime.Now.Date)
                            e.Style.TextColor = Color.Red;
                    }
                }
            }
            catch { }
        }

        private void devolverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Biblioteca.Devolucao _tela = new Biblioteca.Devolucao();

            try
            { 
                int i = gridGroupingControl.Table.CurrentRecord.GetRecord().GetRowIndex();
                GridRecordRow _registro = gridGroupingControl.Table.DisplayElements[i] as GridRecordRow;
                
                if (_registro != null)
                {
                    Record dr = _registro.GetRecord() as Record;

                    if (dr != null)
                    {
                        _tela.codigoTextBox.Text = dr["IdLivro"].ToString();
                        _tela.ColaboradorTextBox.Text = dr["IdColaborador"].ToString();
                    }
                }

            }
            catch { }
            
            _tela.ShowDialog();

            Acompanhamento_Shown(sender, e);
        }

        private void renovarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Biblioteca.Emprestimos _tela = new Biblioteca.Emprestimos();

            try
            { 
                int i = gridGroupingControl.Table.CurrentRecord.GetRecord().GetRowIndex();
                GridRecordRow _registro = gridGroupingControl.Table.DisplayElements[i] as GridRecordRow;

                if (_registro != null)
                {
                    Record dr = _registro.GetRecord() as Record;

                    if (dr != null)
                    {
                        _tela.codigoTextBox.Text = dr["IdLivro"].ToString();
                        _tela.ColaboradorTextBox.Text = dr["IdColaborador"].ToString();
                    }
                }

            }
            catch { }

            _tela.ShowDialog();
            Acompanhamento_Shown(sender, e);
        }
    }
}
