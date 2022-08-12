namespace Suportte
{
    partial class EsqueceuASenha
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EsqueceuASenha));
            this.senhaPanel = new System.Windows.Forms.Panel();
            this.contSenhalabel = new System.Windows.Forms.Label();
            this.verSenhaPictureBox = new System.Windows.Forms.PictureBox();
            this.senhaLabel = new System.Windows.Forms.Label();
            this.textosenhalabel6 = new System.Windows.Forms.Label();
            this.cpflabel = new System.Windows.Forms.Label();
            this.cpfMaskedEditBox = new Syncfusion.Windows.Forms.Tools.MaskedEditBox();
            this.ContadorTimer = new System.Windows.Forms.Timer(this.components);
            this.senhaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.verSenhaPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpfMaskedEditBox)).BeginInit();
            this.SuspendLayout();
            // 
            // senhaPanel
            // 
            this.senhaPanel.Controls.Add(this.contSenhalabel);
            this.senhaPanel.Controls.Add(this.verSenhaPictureBox);
            this.senhaPanel.Controls.Add(this.senhaLabel);
            this.senhaPanel.Controls.Add(this.textosenhalabel6);
            this.senhaPanel.Controls.Add(this.cpflabel);
            this.senhaPanel.Controls.Add(this.cpfMaskedEditBox);
            this.senhaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.senhaPanel.Location = new System.Drawing.Point(0, 0);
            this.senhaPanel.Name = "senhaPanel";
            this.senhaPanel.Size = new System.Drawing.Size(407, 88);
            this.senhaPanel.TabIndex = 0;
            // 
            // contSenhalabel
            // 
            this.contSenhalabel.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contSenhalabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.contSenhalabel.Location = new System.Drawing.Point(337, 59);
            this.contSenhalabel.Name = "contSenhalabel";
            this.contSenhalabel.Size = new System.Drawing.Size(62, 19);
            this.contSenhalabel.TabIndex = 4;
            this.contSenhalabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // verSenhaPictureBox
            // 
            this.verSenhaPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("verSenhaPictureBox.Image")));
            this.verSenhaPictureBox.Location = new System.Drawing.Point(146, 30);
            this.verSenhaPictureBox.Name = "verSenhaPictureBox";
            this.verSenhaPictureBox.Size = new System.Drawing.Size(23, 23);
            this.verSenhaPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.verSenhaPictureBox.TabIndex = 34;
            this.verSenhaPictureBox.TabStop = false;
            this.verSenhaPictureBox.Click += new System.EventHandler(this.verSenhaPictureBox_Click);
            // 
            // senhaLabel
            // 
            this.senhaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.senhaLabel.Location = new System.Drawing.Point(179, 32);
            this.senhaLabel.Name = "senhaLabel";
            this.senhaLabel.Size = new System.Drawing.Size(225, 23);
            this.senhaLabel.TabIndex = 3;
            this.senhaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textosenhalabel6
            // 
            this.textosenhalabel6.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textosenhalabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.textosenhalabel6.Location = new System.Drawing.Point(188, 8);
            this.textosenhalabel6.Name = "textosenhalabel6";
            this.textosenhalabel6.Size = new System.Drawing.Size(211, 16);
            this.textosenhalabel6.TabIndex = 2;
            this.textosenhalabel6.Text = "Sua senha é";
            this.textosenhalabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textosenhalabel6.Visible = false;
            // 
            // cpflabel
            // 
            this.cpflabel.AutoSize = true;
            this.cpflabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpflabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.cpflabel.Location = new System.Drawing.Point(31, 11);
            this.cpflabel.Name = "cpflabel";
            this.cpflabel.Size = new System.Drawing.Size(92, 15);
            this.cpflabel.TabIndex = 1;
            this.cpflabel.Text = "Informe seu CPF";
            // 
            // cpfMaskedEditBox
            // 
            this.cpfMaskedEditBox.BackColor = System.Drawing.SystemColors.Control;
            this.cpfMaskedEditBox.BeforeTouchSize = new System.Drawing.Size(133, 23);
            this.cpfMaskedEditBox.BorderColor = System.Drawing.Color.DimGray;
            this.cpfMaskedEditBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpfMaskedEditBox.DecimalSeparator = '.';
            this.cpfMaskedEditBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.cpfMaskedEditBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.cpfMaskedEditBox.Location = new System.Drawing.Point(10, 30);
            this.cpfMaskedEditBox.Mask = "999.999.999-99";
            this.cpfMaskedEditBox.MaxLength = 14;
            this.cpfMaskedEditBox.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.cpfMaskedEditBox.Metrocolor = System.Drawing.Color.DimGray;
            this.cpfMaskedEditBox.Name = "cpfMaskedEditBox";
            this.cpfMaskedEditBox.Size = new System.Drawing.Size(133, 23);
            this.cpfMaskedEditBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.cpfMaskedEditBox.TabIndex = 0;
            this.cpfMaskedEditBox.ThemesEnabled = false;
            this.cpfMaskedEditBox.UseBorderColorOnFocus = true;
            this.cpfMaskedEditBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cpfMaskedEditBox_KeyDown);
            this.cpfMaskedEditBox.Validating += new System.ComponentModel.CancelEventHandler(this.cpfMaskedEditBox_Validating);
            // 
            // ContadorTimer
            // 
            this.ContadorTimer.Enabled = true;
            this.ContadorTimer.Interval = 1000;
            this.ContadorTimer.Tick += new System.EventHandler(this.ContadorTimer_Tick);
            // 
            // EsqueceuASenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 88);
            this.ControlBox = false;
            this.Controls.Add(this.senhaPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EsqueceuASenha";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "EsqueceuASenha";
            this.senhaPanel.ResumeLayout(false);
            this.senhaPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.verSenhaPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpfMaskedEditBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel senhaPanel;
        private System.Windows.Forms.Label contSenhalabel;
        private System.Windows.Forms.PictureBox verSenhaPictureBox;
        private System.Windows.Forms.Label senhaLabel;
        private System.Windows.Forms.Label textosenhalabel6;
        private System.Windows.Forms.Label cpflabel;
        private Syncfusion.Windows.Forms.Tools.MaskedEditBox cpfMaskedEditBox;
        private System.Windows.Forms.Timer ContadorTimer;
    }
}