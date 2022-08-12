namespace Suportte
{
    partial class Chat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chat));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.mensagemPanel = new System.Windows.Forms.Panel();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.gravarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.historicoPanel = new System.Windows.Forms.Panel();
            this.grupoChatPanel = new System.Windows.Forms.Panel();
            this.tituloHistoricoPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.chatTimer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.tituloMensagemPanel = new System.Windows.Forms.Panel();
            this.dataHoraChatRecebidoLabel = new System.Windows.Forms.Label();
            this.mensagemRichTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.mensagemPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            this.historicoPanel.SuspendLayout();
            this.tituloHistoricoPanel.SuspendLayout();
            this.tituloMensagemPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mensagemRichTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.panel1.Controls.Add(this.tituloLabel);
            this.panel1.Controls.Add(this.powerPictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 40);
            this.panel1.TabIndex = 3;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.tituloLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.tituloLabel.Location = new System.Drawing.Point(12, 10);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(53, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Chat";
            this.tituloLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(481, 5);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(32, 32);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // mensagemPanel
            // 
            this.mensagemPanel.Controls.Add(this.mensagemRichTextBox);
            this.mensagemPanel.Controls.Add(this.dataHoraChatRecebidoLabel);
            this.mensagemPanel.Controls.Add(this.pictureBox16);
            this.mensagemPanel.Controls.Add(this.gravarButton);
            this.mensagemPanel.Controls.Add(this.tituloMensagemPanel);
            this.mensagemPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mensagemPanel.Location = new System.Drawing.Point(0, 565);
            this.mensagemPanel.Name = "mensagemPanel";
            this.mensagemPanel.Size = new System.Drawing.Size(516, 210);
            this.mensagemPanel.TabIndex = 5;
            // 
            // pictureBox16
            // 
            this.pictureBox16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox16.Image = global::Suportte.Properties.Resources.linhaDivisoriaVerde1;
            this.pictureBox16.Location = new System.Drawing.Point(0, 206);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(516, 4);
            this.pictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox16.TabIndex = 20;
            this.pictureBox16.TabStop = false;
            // 
            // gravarButton
            // 
            this.gravarButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.gravarButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.gravarButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.gravarButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.gravarButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.gravarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gravarButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.gravarButton.ForeColor = System.Drawing.Color.Black;
            this.gravarButton.IsBackStageButton = false;
            this.gravarButton.Location = new System.Drawing.Point(392, 164);
            this.gravarButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.gravarButton.Name = "gravarButton";
            this.gravarButton.OverrideFormManagedColor = true;
            this.gravarButton.Size = new System.Drawing.Size(103, 34);
            this.gravarButton.TabIndex = 17;
            this.gravarButton.Text = "&Enviar";
            this.gravarButton.Click += new System.EventHandler(this.gravarButton_Click);
            // 
            // historicoPanel
            // 
            this.historicoPanel.Controls.Add(this.grupoChatPanel);
            this.historicoPanel.Controls.Add(this.tituloHistoricoPanel);
            this.historicoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historicoPanel.Location = new System.Drawing.Point(0, 40);
            this.historicoPanel.Name = "historicoPanel";
            this.historicoPanel.Size = new System.Drawing.Size(516, 525);
            this.historicoPanel.TabIndex = 6;
            // 
            // grupoChatPanel
            // 
            this.grupoChatPanel.AutoScroll = true;
            this.grupoChatPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grupoChatPanel.Location = new System.Drawing.Point(0, 25);
            this.grupoChatPanel.Name = "grupoChatPanel";
            this.grupoChatPanel.Size = new System.Drawing.Size(516, 500);
            this.grupoChatPanel.TabIndex = 18;
            this.grupoChatPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.grupoChatPanel_Scroll);
            // 
            // tituloHistoricoPanel
            // 
            this.tituloHistoricoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.tituloHistoricoPanel.Controls.Add(this.label3);
            this.tituloHistoricoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloHistoricoPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloHistoricoPanel.Name = "tituloHistoricoPanel";
            this.tituloHistoricoPanel.Size = new System.Drawing.Size(516, 25);
            this.tituloHistoricoPanel.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.label3.ForeColor = System.Drawing.Color.GreenYellow;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(516, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Históricos";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chatTimer
            // 
            this.chatTimer.Interval = 5000;
            this.chatTimer.Tick += new System.EventHandler(this.chatTimer_Tick);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.label4.ForeColor = System.Drawing.Color.GreenYellow;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(516, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Mensagem";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tituloMensagemPanel
            // 
            this.tituloMensagemPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.tituloMensagemPanel.Controls.Add(this.label4);
            this.tituloMensagemPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloMensagemPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloMensagemPanel.Name = "tituloMensagemPanel";
            this.tituloMensagemPanel.Size = new System.Drawing.Size(516, 25);
            this.tituloMensagemPanel.TabIndex = 16;
            // 
            // dataHoraChatRecebidoLabel
            // 
            this.dataHoraChatRecebidoLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataHoraChatRecebidoLabel.Font = new System.Drawing.Font("Century Gothic", 10.25F, System.Drawing.FontStyle.Bold);
            this.dataHoraChatRecebidoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.dataHoraChatRecebidoLabel.Location = new System.Drawing.Point(0, 25);
            this.dataHoraChatRecebidoLabel.Name = "dataHoraChatRecebidoLabel";
            this.dataHoraChatRecebidoLabel.Size = new System.Drawing.Size(516, 16);
            this.dataHoraChatRecebidoLabel.TabIndex = 21;
            this.dataHoraChatRecebidoLabel.Text = "Usuários";
            this.dataHoraChatRecebidoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dataHoraChatRecebidoLabel.Visible = false;
            // 
            // mensagemRichTextBox
            // 
            this.mensagemRichTextBox.AcceptsReturn = true;
            this.mensagemRichTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.mensagemRichTextBox.BeforeTouchSize = new System.Drawing.Size(509, 129);
            this.mensagemRichTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.mensagemRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mensagemRichTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.mensagemRichTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.mensagemRichTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.mensagemRichTextBox.Location = new System.Drawing.Point(4, 29);
            this.mensagemRichTextBox.MaxLength = 2000;
            this.mensagemRichTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.mensagemRichTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.mensagemRichTextBox.Multiline = true;
            this.mensagemRichTextBox.Name = "mensagemRichTextBox";
            this.mensagemRichTextBox.Size = new System.Drawing.Size(509, 129);
            this.mensagemRichTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.mensagemRichTextBox.TabIndex = 22;
            this.mensagemRichTextBox.UseBorderColorOnFocus = true;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 775);
            this.ControlBox = false;
            this.Controls.Add(this.historicoPanel);
            this.Controls.Add(this.mensagemPanel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Chat";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.Chat_Load);
            this.Shown += new System.EventHandler(this.Chat_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.mensagemPanel.ResumeLayout(false);
            this.mensagemPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            this.historicoPanel.ResumeLayout(false);
            this.tituloHistoricoPanel.ResumeLayout(false);
            this.tituloMensagemPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mensagemRichTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel mensagemPanel;
        private System.Windows.Forms.Panel historicoPanel;
        private System.Windows.Forms.Panel tituloHistoricoPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer chatTimer;
        private Syncfusion.Windows.Forms.ButtonAdv gravarButton;
        private System.Windows.Forms.Panel grupoChatPanel;
        private System.Windows.Forms.PictureBox pictureBox16;
        private System.Windows.Forms.Label dataHoraChatRecebidoLabel;
        private System.Windows.Forms.Panel tituloMensagemPanel;
        private System.Windows.Forms.Label label4;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt mensagemRichTextBox;
    }
}