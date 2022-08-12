using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;
using Negocio;
using Syncfusion.Grouping;
using System.Text.RegularExpressions;

namespace Suportte
{
    public partial class Chat : Form
    {
        public Chat()
        {
            InitializeComponent();
        }

        List<Classes.Chat> _listaChat;
        List<Classes.Chat> _listaExibidos = new List<Classes.Chat>();

        public int idUsuarioDestino = 0;
        int _posicaoChat = 6;
        int _heigthMinimo = 66;
        int _heigthMinimoCalculo = 22;

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

        private void Chat_Shown(object sender, EventArgs e)
        {
            //_listaUsuariosLogagos = new ChatBO().ConsultarUsuariosLogados();
            //usuarioGridGroupingControl.DataSource = _listaUsuariosLogagos;
            //usuarioGridGroupingControl.Table.ExpandAllGroups();

            //if (idUsuarioDestino != 0)
                CarregaChat();

            tituloLabel.Text = this.Text;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void usuariosGridDataBoundGrid_CellClick(object sender, Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs e)
        {
            

        }

        private void usuarioGridGroupingControl_TableControlCellClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            _listaExibidos.Clear();
            grupoChatPanel.Controls.Clear();
            _heigthMinimo = 66;
            _heigthMinimoCalculo = 22;
            _posicaoChat = 6;
           
            string value = e.TableControl.Table.CurrentRecord.GetRecord().Info;

            int posIniId = 0;
            int posFimId = 0;

            posIniId = value.IndexOf("Id =") + 4;
            posFimId = value.IndexOf(", Tipo");

            idUsuarioDestino = Convert.ToInt32(value.Substring(posIniId, posFimId - posIniId).Trim());

            CarregaChat();
            chatTimer.Start();
        }

        private void chatTimer_Tick(object sender, EventArgs e)
        {
            if (idUsuarioDestino != 0)
            {
                if (_posicaoChat > 505)
                    _posicaoChat = 505;
                CarregaChat(false);

                grupoChatPanel.VerticalScroll.Value = grupoChatPanel.VerticalScroll.Maximum;

            }
        }

        //private void panel_Paint(object sender, PaintEventArgs e)
        //{
        //    ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, 5, ButtonBorderStyle.Solid, Color.Black, 5, ButtonBorderStyle.Solid, Color.Black, 5, ButtonBorderStyle.Solid, Color.Black, 5, ButtonBorderStyle.Solid);
        //}

        private void CriaPanelChat(bool enviado, DateTime data, string mensagem, bool lida, string enviada, string recebida)
        {
            Panel panel = new Panel();
            panel.Size = new Size(459, _heigthMinimo);

            Panel panel2 = new Panel();
            panel2.Size = new Size(21, 80);
            Label lbl  = new Label();
            lbl.Size = new Size(459, 20);
            lbl.Dock = DockStyle.Top;
            lbl.Font = new Font("Century Gothic", 8, FontStyle.Bold);
            lbl.ForeColor = dataHoraChatRecebidoLabel.ForeColor;
            lbl.Text = "Data " + data.ToShortDateString() + " " + data.ToShortTimeString();

            Label lblLida = new Label();
            lblLida.Size = new Size(459, 15);
            lblLida.Dock = DockStyle.Bottom;
            lblLida.Font = new Font("Century Gothic", 6, FontStyle.Bold);
            lblLida.TextAlign = ContentAlignment.TopRight;
            lblLida.ForeColor = dataHoraChatRecebidoLabel.ForeColor;
            lblLida.Text = (lida ? "Lida" : "");

            RichTextBox text = new RichTextBox();
            text.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            text.ForeColor = dataHoraChatRecebidoLabel.ForeColor;
            text.Dock = DockStyle.Fill;
            text.BorderStyle = BorderStyle.None;
            text.ReadOnly = true;
            text.Margin = new Padding(10, 10, 0, 0);
            text.ScrollBars = RichTextBoxScrollBars.None;

            text.Text = mensagem;

            //text.AppendText(mensagem.Replace("\n", ""));

           decimal qtdlinhas = Math.Round(Convert.ToDecimal(text.TextLength / 62));

            if (text.Lines.Count() > 2 || qtdlinhas> 2)
            {
                qtdlinhas = Math.Round(Convert.ToDecimal(text.TextLength / 62));

                if (qtdlinhas >= text.Lines.Count())
                    panel.ClientSize = new Size(459, text.ClientRectangle.Height + 45);
                else
                    panel.Size = new Size(459, (_heigthMinimoCalculo * text.Lines.Count()));
            }

            panel.Controls.Add(lbl);
            panel.Controls.Add(lblLida);
            panel.Controls.Add(panel2);
            panel.Controls.Add(text) ;
            panel2.Dock = DockStyle.Left;

            if (enviado)
            {
                lbl.Text = "Você enviou para " + enviada + " - " + lbl.Text;
                text.BackColor = Color.FromArgb(226, 250, 183);
                lbl.TextAlign = ContentAlignment.TopRight;
                panel.BackColor = Color.FromArgb(226, 250, 183);
                panel.Location = new Point(30, _posicaoChat);
                grupoChatPanel.Controls.Add(panel);
            }
            else
            {
                lbl.Text = "Você recebeu de " + enviada + " - " + lbl.Text;
                text.BackColor = Color.FromArgb(219, 219, 240);
                lbl.TextAlign = ContentAlignment.TopLeft;
                panel.BackColor = Color.FromArgb(219, 219, 240);
                panel.Location = new Point(10, _posicaoChat);
                grupoChatPanel.Controls.Add(panel);
            }
            text.BringToFront();
            _posicaoChat = _posicaoChat + (panel.Height != _heigthMinimo ? panel.Height : _heigthMinimo) + 4;

        }

        private void CarregaChat(bool todas = true)
        {
            _listaChat = new ChatBO().ConsultarHistorico(Publicas._idUsuario, idUsuarioDestino, todas);

            foreach (Classes.Chat item in _listaChat.OrderBy(o => o.DataHora))
            {
                if (_listaExibidos.Count() == 0 || _listaExibidos.Where(w => w.IdChat == item.IdChat).Count() == 0)
                {
                    CriaPanelChat(item.Enviada, item.DataHora, item.Mensagem, item.Lida, item.NomeEnviada, item.NomeRecebida);

                    if (item.IdUsuarioDestino == Publicas._idUsuario)
                        new ChatBO().GravarChatComoLido(item.IdChat);

                    _listaExibidos.Add(item);
                }
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            Classes.Chat _chat = new Classes.Chat();

            _chat.IdUsuarioDestino = idUsuarioDestino;
            _chat.IdUsuarioOrigem = Publicas._idUsuario;
            _chat.Mensagem = mensagemRichTextBox.Text;

            new ChatBO().EnviarChat(_chat);
            mensagemRichTextBox.Text = "";

        }

        private void grupoChatPanel_Scroll(object sender, ScrollEventArgs e)
        {
            if (grupoChatPanel.VerticalScroll.Value != grupoChatPanel.VerticalScroll.Maximum)
                chatTimer.Stop();
            else
                chatTimer.Start();

        }

        private void Chat_Load(object sender, EventArgs e)
        {
            //if (Publicas._alterouSkin)
            //{
            //    foreach (Control componenteDaTela in this.Controls)
            //    {
            //        Publicas.AplicarSkin(componenteDaTela);
            //    }
            //}

        }
    }
}
