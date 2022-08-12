using Classes;
using Syncfusion.Windows.Forms;
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
    public partial class Mensagem : Form
    {
        
        public Mensagem(string mensagem, Publicas.TipoMensagem tipoMensagem)
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

            mensagemTextBox.Text = mensagem;
            tituloLabel.Text = Publicas.GetDescription(tipoMensagem, "");

            if (tipoMensagem == Publicas.TipoMensagem.Confirmacao)
            {
                okButton.Visible = false;
                simButton.Visible = true;
                naoButton.Visible = true;
                imagemPictureBox.Image = Properties.Resources.Confirmacao;
                simButton.Enabled = true;
                naoButton.Enabled = true;
                simButton.Location = new Point(169, 16);
                naoButton.Location = new Point(387, 16);
                naoButton.Focus();
            }
            else
            {
                okButton.Visible = true;
                simButton.Visible = false;
                naoButton.Visible = false;

                if (tipoMensagem == Publicas.TipoMensagem.Alerta)
                    imagemPictureBox.Image = Properties.Resources.Aviso;

                if (tipoMensagem == Publicas.TipoMensagem.Erro)
                    imagemPictureBox.Image = Properties.Resources.Erro;

                if (tipoMensagem == Publicas.TipoMensagem.Informacao ||
                    tipoMensagem == Publicas.TipoMensagem.Sucesso)
                    imagemPictureBox.Image = Properties.Resources.Informacao;

                okButton.Enabled = true;
                okButton.Location = new Point(278, 16);
                okButton.Focus();
            }
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

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void simButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Publicas._clicouSIM = true;
            Close();
        }

        private void naoButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Publicas._clicouSIM = false;
            Close();
        }

        private void simButton_Enter(object sender, EventArgs e)
        {
            simButton.BackColor = Publicas._botaoFocado;
            simButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void simButton_Validating(object sender, CancelEventArgs e)
        {
            simButton.BackColor = Publicas._botao;
            simButton.ForeColor = Publicas._fonteBotao;
        }

        private void naoButton_Enter(object sender, EventArgs e)
        {
            naoButton.BackColor = Publicas._botaoFocado;
            naoButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void naoButton_Validating(object sender, CancelEventArgs e)
        {
            naoButton.BackColor = Publicas._botao;
            naoButton.ForeColor = Publicas._fonteBotao;
        }

        private void okButton_Enter(object sender, EventArgs e)
        {
            okButton.BackColor = Publicas._botaoFocado;
            okButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void okButton_Validating(object sender, CancelEventArgs e)
        {
            okButton.BackColor = Publicas._botao;
            okButton.ForeColor = Publicas._fonteBotao;
        }
    }
}
