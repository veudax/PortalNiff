namespace Suportte.Notificacoes
{
    partial class NotificacaoChat
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
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.tituloQtdLabel = new System.Windows.Forms.Label();
            this.Titulolabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.camposPanel = new System.Windows.Forms.Panel();
            this.mensagemRichTextBox = new System.Windows.Forms.RichTextBox();
            this.enviadaLabel = new System.Windows.Forms.Label();
            this.showTimer = new System.Windows.Forms.Timer(this.components);
            this.timeOutTimer = new System.Windows.Forms.Timer(this.components);
            this.closeTimer = new System.Windows.Forms.Timer(this.components);
            this.tituloPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.camposPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tituloPanel
            // 
            this.tituloPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.tituloPanel.Controls.Add(this.tituloQtdLabel);
            this.tituloPanel.Controls.Add(this.Titulolabel);
            this.tituloPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloPanel.Name = "tituloPanel";
            this.tituloPanel.Size = new System.Drawing.Size(327, 28);
            this.tituloPanel.TabIndex = 3;
            this.tituloPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // tituloQtdLabel
            // 
            this.tituloQtdLabel.AutoSize = true;
            this.tituloQtdLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.tituloQtdLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F, System.Drawing.FontStyle.Bold);
            this.tituloQtdLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.tituloQtdLabel.Location = new System.Drawing.Point(308, 0);
            this.tituloQtdLabel.Name = "tituloQtdLabel";
            this.tituloQtdLabel.Size = new System.Drawing.Size(19, 19);
            this.tituloQtdLabel.TabIndex = 14;
            this.tituloQtdLabel.Text = "0";
            // 
            // Titulolabel
            // 
            this.Titulolabel.AutoSize = true;
            this.Titulolabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.Titulolabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.Titulolabel.Location = new System.Drawing.Point(12, 4);
            this.Titulolabel.Name = "Titulolabel";
            this.Titulolabel.Size = new System.Drawing.Size(236, 21);
            this.Titulolabel.TabIndex = 13;
            this.Titulolabel.Text = "Nova mensagem recebida";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.camposPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(327, 86);
            this.panel2.TabIndex = 4;
            // 
            // camposPanel
            // 
            this.camposPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.camposPanel.Controls.Add(this.mensagemRichTextBox);
            this.camposPanel.Controls.Add(this.enviadaLabel);
            this.camposPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camposPanel.Location = new System.Drawing.Point(0, 0);
            this.camposPanel.Name = "camposPanel";
            this.camposPanel.Size = new System.Drawing.Size(327, 86);
            this.camposPanel.TabIndex = 21;
            // 
            // mensagemRichTextBox
            // 
            this.mensagemRichTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(250)))), ((int)(((byte)(183)))));
            this.mensagemRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mensagemRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mensagemRichTextBox.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensagemRichTextBox.Location = new System.Drawing.Point(0, 21);
            this.mensagemRichTextBox.Name = "mensagemRichTextBox";
            this.mensagemRichTextBox.ReadOnly = true;
            this.mensagemRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.mensagemRichTextBox.Size = new System.Drawing.Size(325, 63);
            this.mensagemRichTextBox.TabIndex = 23;
            this.mensagemRichTextBox.TabStop = false;
            this.mensagemRichTextBox.Text = "";
            // 
            // enviadaLabel
            // 
            this.enviadaLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.enviadaLabel.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.enviadaLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.enviadaLabel.Location = new System.Drawing.Point(0, 0);
            this.enviadaLabel.Name = "enviadaLabel";
            this.enviadaLabel.Size = new System.Drawing.Size(325, 21);
            this.enviadaLabel.TabIndex = 22;
            this.enviadaLabel.Text = "Enviada por: ";
            this.enviadaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.enviadaLabel.DoubleClick += new System.EventHandler(this.enviadaLabel_DoubleClick);
            // 
            // showTimer
            // 
            this.showTimer.Tick += new System.EventHandler(this.showTimer_Tick);
            // 
            // timeOutTimer
            // 
            this.timeOutTimer.Interval = 5000;
            this.timeOutTimer.Tick += new System.EventHandler(this.timeOutTimer_Tick);
            // 
            // closeTimer
            // 
            this.closeTimer.Tick += new System.EventHandler(this.closeTimer_Tick);
            // 
            // NotificacaoChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 114);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tituloPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotificacaoChat";
            this.Text = "NotificacaoChat";
            this.Load += new System.EventHandler(this.NotificacaoChat_Load);
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.camposPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        private System.Windows.Forms.Label Titulolabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer showTimer;
        private System.Windows.Forms.Timer timeOutTimer;
        private System.Windows.Forms.Timer closeTimer;
        private System.Windows.Forms.Panel camposPanel;
        public System.Windows.Forms.RichTextBox mensagemRichTextBox;
        public System.Windows.Forms.Label tituloQtdLabel;
        public System.Windows.Forms.Label enviadaLabel;
    }
}