using Classes;
using Suportte.Notificacoes;
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

namespace Suportte.SAC
{
    public partial class Motivo : Form
    {
        public Motivo()
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

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Publicas._motivoCancelamentoDevolucao = string.Empty;
            Publicas._cancelouMotivo = true;
            Close();
        }

        private void descricaoAtendimentoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            Publicas._motivoCancelamentoDevolucao = string.Empty;
            Publicas._cancelouMotivo = true;
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(motivoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Motivo deve ser informado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                motivoTextBox.Focus();
                return;
            }

            Publicas._motivoCancelamentoDevolucao = motivoTextBox.Text;
            Publicas._cancelouMotivo = false;
            Close();
        }

        private void motivoTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                ((TextBoxExt)sender).BorderColor = Color.Navy;
            }
            catch { }
        }

        private void motivoTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ((TextBoxExt)sender).BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            }
            catch { }
        }

        private void motivoTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
