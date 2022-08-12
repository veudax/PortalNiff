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

namespace Suportte.Notificacoes
{
    public partial class Aniversario : Form
    {
        public Aniversario()
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

        string[] mensagens; 

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
            if (!Publicas._aniversario.Existe)
                Publicas._aniversario = new Aniversarios();

            Publicas._aniversario.Id = Publicas._idUsuario;
            Publicas._aniversario.MostrarMensagem = ativoCheckBox.Checked;
            Publicas._aniversario.Mensagem = mensagemLabel.Text;

            if (!new AniversarioBO().Gravar(Publicas._aniversario))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Publicas._fecharAniversarios = true;
            Close();
        }

        private void Aniversario_Load(object sender, EventArgs e)
        {

            ativoCheckBox.Checked = true;

            if (Publicas._aniversario.Existe)
                mensagemLabel.Text = Publicas._aniversario.Mensagem;
            else
            {
                mensagens = new string[] { "", mensagemLabel.Text, label1.Text, label2.Text };

                Random _aleatorio = new Random();

                int texto = _aleatorio.Next(1, 3);

                mensagemLabel.Text = mensagens[texto];
            }

            this.TopMost = false;
        }
    }
}
