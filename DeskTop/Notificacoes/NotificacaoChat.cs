using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.Notificacoes
{
    public partial class NotificacaoChat : Form
    {
        public NotificacaoChat()
        {
            InitializeComponent();
        }

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

        private void NotificacaoChat_Load(object sender, EventArgs e)
        {
            this.Top = 780;//-1 * (this.Height);
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width -40;

            showTimer.Start();
        }

        int interval = 0;
        private void showTimer_Tick(object sender, EventArgs e)
        {
            if (this.Top < 700)
            {
                this.Top -= interval; // drop the alert
                interval -= 2; // double the speed
            }
            else
            {
                showTimer.Stop();
            }
        }

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.2; //reduce opacity to zero
            }
            else
            {
                this.Close(); //then close
            }
        }

        private void timeOutTimer_Tick(object sender, EventArgs e)
        {
            closeTimer.Start();
        }

        private void enviadaLabel_DoubleClick(object sender, EventArgs e)
        {
            Chat _chat = new Chat();
            _chat.idUsuarioDestino = Convert.ToInt32(tituloQtdLabel.Tag);
            this.Close();
            _chat.ShowDialog();
        }
    }
}
