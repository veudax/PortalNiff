using Classes;
using Negocio;
using Suportte.Notificacoes;
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
    public partial class Avaliacao : Form
    {
        public Avaliacao()
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

        private void gravarButton_Click(object sender, EventArgs e)
        {

            Publicas._chamado.Avaliacao = (avaliacaoRatingControl.Value == 1 ? Publicas.TipoDeSatisfacaoAtendimento.Ruim :
                                           (avaliacaoRatingControl.Value == 2 ? Publicas.TipoDeSatisfacaoAtendimento.Bom :
                                           (avaliacaoRatingControl.Value == 3 ? Publicas.TipoDeSatisfacaoAtendimento.Regular :
                                           (avaliacaoRatingControl.Value == 4 ? Publicas.TipoDeSatisfacaoAtendimento.MuitoBom : Publicas.TipoDeSatisfacaoAtendimento.Excelente))));

            Publicas._chamado.DescricaoAvaliacao = descricaoTextBox.Text;
            Publicas._chamado.ProblemaResolvido = problemaResolvidoComboBox.SelectedIndex == 0;
            Publicas._chamado.DentroDoPrazo = dentroDoPrazoBoxAdv1.SelectedIndex == 0;

            if (!new ChamadoBO().Gravar(Publicas._chamado))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Close();
        }

        private void avaliacaoRatingControl_ValueChanged(object sender, Syncfusion.Windows.Forms.Tools.RatingValueChangedEventArgs args)
        {
            gravarButton.Enabled = avaliacaoRatingControl.Value > 0;
        }

        private void Avaliacao_Shown(object sender, EventArgs e)
        {
            dentroDoPrazoBoxAdv1.Items.AddRange(new object[] { "SIM", "NÃO" });
            problemaResolvidoComboBox.Items.AddRange(new object[] { "SIM", "NÃO" });
            dentroDoPrazoBoxAdv1.SelectedIndex = 0;
            problemaResolvidoComboBox.SelectedIndex = 0;

        }
    }
}
