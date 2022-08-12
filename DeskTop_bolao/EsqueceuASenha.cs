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

namespace Suportte
{
    public partial class EsqueceuASenha : Form
    {
        public EsqueceuASenha()
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

        int contadorTempoSenha = -1;

        private void cpfMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cpfMaskedEditBox.Focus();
            }
        }

        private void cpfMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            verSenhaPictureBox_Click(sender, new EventArgs());
        }

        private void verSenhaPictureBox_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cpfMaskedEditBox.ClipText.Trim()))
            {
                new Notificacoes.Mensagem("Informe o seu CPF.", Publicas.TipoMensagem.Alerta).ShowDialog();
                cpfMaskedEditBox.Focus();
                return;
            }

            contadorTempoSenha = 0;

            if (cpfMaskedEditBox.ClipText != Publicas._usuario.CPF.ToString())
            {
                Log _log = new Log();
                _log.IdUsuario = Publicas._usuario.Id;
                _log.Descricao = "Informou o CPF inválido.";
                _log.Tela = "Principal - Login - Esqueceu senha";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }

                new Notificacoes.Mensagem("CPF inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                cpfMaskedEditBox.Focus();
                return;
            }

            textosenhalabel6.Visible = true;
            senhaLabel.Text = Publicas._usuario.Senha;
            ContadorTimer.Start();
        }

        private void ContadorTimer_Tick(object sender, EventArgs e)
        {
            if (contadorTempoSenha != -1)
            {
                contSenhalabel.Text = (5 - contadorTempoSenha).ToString();

                contadorTempoSenha++;

                if (contadorTempoSenha > 6)
                {
                    senhaLabel.Visible = false;
                    Close();
                }
            }
        }
    }
}
