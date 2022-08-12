using Classes;
using Negocio;
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

                if (Publicas._TemaBlack)
                    this.BackColor = Publicas._fundo;
            }
            Publicas._mensagemSistema = string.Empty;
        }

        public bool GravouAvaliacao = false;
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
            if (avaliacaoRatingControl.Value == 0)
            {
                new Notificacoes.Mensagem("Nenhuma estrelas selecionada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            if (avaliacaoRatingControl.Value <= 3 && string.IsNullOrEmpty(descricaoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Por gentileza descreva o motivo da avaliação tão baixa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                descricaoTextBox.Focus();
                return;
            }

            if (avaliacaoRatingControl.Value <= 3 && !string.IsNullOrEmpty(descricaoTextBox.Text.Trim()) && descricaoTextBox.Text.Trim().Length <= 20)
            {
                new Notificacoes.Mensagem("Por gentileza seu comentário está muito pequeno." +
                    Environment.NewLine + "Precisamos de mais detalhes para melhorar o atendimento." +
                    Environment.NewLine +
                    Environment.NewLine + "Seja coerente, não repita palavras e pontuação apenas para não apresentar está mensagem. " +
                    Environment.NewLine + "Está é uma ferramenta de trabalho.", Publicas.TipoMensagem.Alerta).ShowDialog();
                descricaoTextBox.Focus();
                return;
            }

            if (problemaResolvidoComboBox.SelectedIndex == 0 || dentroDoPrazoBoxAdv1.SelectedIndex == 0 || CortezComboBox.SelectedIndex == 0)
            {
                new Notificacoes.Mensagem("Nenhum resposta atribuida as perguntas.", Publicas.TipoMensagem.Alerta).ShowDialog();
                if (problemaResolvidoComboBox.SelectedIndex == 0)
                {
                    problemaResolvidoComboBox.DroppedDown = true;
                    problemaResolvidoComboBox.Focus();
                    return;
                }
                if (dentroDoPrazoBoxAdv1.SelectedIndex == 0)
                {
                    dentroDoPrazoBoxAdv1.DroppedDown = true;
                    dentroDoPrazoBoxAdv1.Focus();
                    return;
                }
                if (CortezComboBox.SelectedIndex == 0)
                {
                    CortezComboBox.DroppedDown = true;
                    CortezComboBox.Focus();
                    return;
                }
            }

            if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante ||
                Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && Publicas._chamado.IdUsuario == Publicas._usuario.Id)
            {
                Publicas._chamado.Avaliacao = (avaliacaoRatingControl.Value == 1 ? Publicas.TipoDeSatisfacaoAtendimento.Ruim :
                                               (avaliacaoRatingControl.Value == 2 ? Publicas.TipoDeSatisfacaoAtendimento.Regular:
                                               (avaliacaoRatingControl.Value == 3 ? Publicas.TipoDeSatisfacaoAtendimento.Bom :
                                               (avaliacaoRatingControl.Value == 4 ? Publicas.TipoDeSatisfacaoAtendimento.MuitoBom : Publicas.TipoDeSatisfacaoAtendimento.Excelente))));

                Publicas._chamado.DescricaoAvaliacao = descricaoTextBox.Text;
                Publicas._chamado.ProblemaResolvido = problemaResolvidoComboBox.SelectedIndex == 2;
                Publicas._chamado.DentroDoPrazo = dentroDoPrazoBoxAdv1.SelectedIndex == 2;
                Publicas._chamado.AtendenteFoiCortez = CortezComboBox.SelectedIndex == 2;

                if (!new ChamadoBO().GravarAvaliacaoAtendente(Publicas._chamado))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }
            else
            {
                Publicas._chamado.AvaliacaoSolicitante = (avaliacaoRatingControl.Value == 1 ? Publicas.TipoDeSatisfacaoAtendimento.Ruim :
                                               (avaliacaoRatingControl.Value == 2 ? Publicas.TipoDeSatisfacaoAtendimento.Regular :
                                               (avaliacaoRatingControl.Value == 3 ? Publicas.TipoDeSatisfacaoAtendimento.Bom :
                                               (avaliacaoRatingControl.Value == 4 ? Publicas.TipoDeSatisfacaoAtendimento.MuitoBom : Publicas.TipoDeSatisfacaoAtendimento.Excelente))));

                Publicas._chamado.DescricaoAvaliacaoSolic = descricaoTextBox.Text;
                Publicas._chamado.SolicitanteAbriuCorreto = problemaResolvidoComboBox.SelectedIndex == 2;
                Publicas._chamado.SolicitanteDentroPrazo = dentroDoPrazoBoxAdv1.SelectedIndex == 2;
                Publicas._chamado.SolicitanteFoiCortez = CortezComboBox.SelectedIndex == 2;

                if (!new ChamadoBO().GravarAvaliacaoSolicitante(Publicas._chamado))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }            

            GravouAvaliacao = true;
            Close();
        }

        private void avaliacaoRatingControl_ValueChanged(object sender, Syncfusion.Windows.Forms.Tools.RatingValueChangedEventArgs args)
        {
            gravarButton.Enabled = avaliacaoRatingControl.Value > 0;
        }

        private void Avaliacao_Shown(object sender, EventArgs e)
        {
            if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante ||
                Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && Publicas._chamado.IdUsuario == Publicas._usuario.Id)
            {
                label1.Text = "Problema foi resolvido?";
                label2.Text = "Seu chamado foi atendido em tempo hábil?";
                label3.Text = "De modo geral, como você avalia a qualidade do atendimento?";
                label4.Text = "Atendente foi cortês?";
            }
            else
            {
                label1.Text = "Chamado aberto corretamente?";
                label2.Text = "Solicitante retornou em tempo hábil?";
                label3.Text = "De modo geral, como você avalia a qualidade dessa solicitação?";
                label4.Text = "Solicitante foi cortês?";
            }

            dentroDoPrazoBoxAdv1.Items.AddRange(new object[] { " ",  "NÃO", "SIM" });
            problemaResolvidoComboBox.Items.AddRange(new object[] { " ", "NÃO", "SIM" });
            CortezComboBox.Items.AddRange(new object[] { " ", "NÃO", "SIM" });
            dentroDoPrazoBoxAdv1.SelectedIndex = 0;
            problemaResolvidoComboBox.SelectedIndex = 0;
            CortezComboBox.SelectedIndex = 0;

            if (Publicas._chamado.TrocouCategoria &&
                Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && Publicas._chamado.IdUsuario != Publicas._usuario.Id)
            {
                ToolTipInfo _tool = new ToolTipInfo();
                _tool.Body.Text = " Houve troca de categoria por isso" + Environment.NewLine + "que o chamado não foi aberto corretamente";
                _tool.Footer.Text = "";
                superToolTip.SetToolTip(label1, _tool);
                problemaResolvidoComboBox.SelectedIndex = 1; // Não
                problemaResolvidoComboBox.Enabled = false;
            }
        }

        private void problemaResolvidoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }
    }
}
