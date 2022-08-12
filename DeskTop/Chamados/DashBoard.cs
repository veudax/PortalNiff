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
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();

            dataInicioDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataFinalDateTimePicker.BackColor = nomeUsuarioTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Parametro _parametros;

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

        private void DashBoard_Shown(object sender, EventArgs e)
        {
            _parametros = new ParametrosBO().Consultar();

            dataInicioDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01).AddMonths(-_parametros.MesesConsultaDashboardChamados);
            dataFinalDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

        }

        private void consultarButton_Click(object sender, EventArgs e)
        {

        }
    }
}
