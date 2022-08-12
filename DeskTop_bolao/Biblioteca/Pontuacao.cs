using Classes;
using Negocio;
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
    public partial class Pontuacao : Form
    {
        public Pontuacao()
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

        Classes.Colaboradores _colaborador;
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

        private void Pontuacao_Shown(object sender, EventArgs e)
        {
            _resenha = new ResenhaLivrosBO().ListarPontuacao(Publicas._idColaborador);

            _colaborador = new ColaboradoresBO().ConsultaColaborador(Convert.ToInt32(ColaboradorTextBox.Text.Trim()));
            NomeColaboradorTextBox.Text = _colaborador.Nome;

            GridMetroColors metroColor = new GridMetroColors();

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

            this.PerguntasGrid.SetMetroStyle(metroColor);

            this.PerguntasGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.PerguntasGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.PerguntasGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.PerguntasGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            //this.PerguntasGrid.Table.DefaultRecordRowHeight = 45;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
