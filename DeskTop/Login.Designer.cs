namespace Suportte
{
    partial class TelaLogin
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
            Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo1 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaLogin));
            this.loginPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.senhaLoginTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.usuarioLoginTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.acessarLoginButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.pictureBox48 = new System.Windows.Forms.PictureBox();
            this.pictureBox47 = new System.Windows.Forms.PictureBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.esqueceuSenhaLabel = new System.Windows.Forms.Label();
            this.pictureBox41 = new System.Windows.Forms.PictureBox();
            this.superToolTip1 = new Syncfusion.Windows.Forms.Tools.SuperToolTip(this);
            this.loginPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.senhaLoginTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usuarioLoginTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox48)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox47)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox41)).BeginInit();
            this.SuspendLayout();
            // 
            // loginPanel
            // 
            this.loginPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.loginPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loginPanel.Controls.Add(this.label1);
            this.loginPanel.Controls.Add(this.senhaLoginTextBox);
            this.loginPanel.Controls.Add(this.usuarioLoginTextBox);
            this.loginPanel.Controls.Add(this.acessarLoginButton);
            this.loginPanel.Controls.Add(this.pictureBox48);
            this.loginPanel.Controls.Add(this.pictureBox47);
            this.loginPanel.Controls.Add(this.label27);
            this.loginPanel.Controls.Add(this.label26);
            this.loginPanel.Controls.Add(this.esqueceuSenhaLabel);
            this.loginPanel.Controls.Add(this.pictureBox41);
            this.loginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginPanel.Location = new System.Drawing.Point(0, 0);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(407, 318);
            this.loginPanel.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(389, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            toolTipInfo1.Body.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            toolTipInfo1.Body.Size = new System.Drawing.Size(20, 20);
            toolTipInfo1.Body.Text = "Encerra o sistema sem efeutar login";
            toolTipInfo1.Footer.Size = new System.Drawing.Size(20, 20);
            toolTipInfo1.Header.Size = new System.Drawing.Size(20, 20);
            this.superToolTip1.SetToolTip(this.label1, toolTipInfo1);
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            this.label1.MouseHover += new System.EventHandler(this.label1_MouseHover);
            // 
            // senhaLoginTextBox
            // 
            this.senhaLoginTextBox.AcceptsReturn = true;
            this.senhaLoginTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.senhaLoginTextBox.BeforeTouchSize = new System.Drawing.Size(276, 26);
            this.senhaLoginTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.senhaLoginTextBox.BorderSides = System.Windows.Forms.Border3DSide.Bottom;
            this.senhaLoginTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.senhaLoginTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.senhaLoginTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.senhaLoginTextBox.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.senhaLoginTextBox.ForeColor = System.Drawing.Color.Black;
            this.senhaLoginTextBox.Location = new System.Drawing.Point(68, 213);
            this.senhaLoginTextBox.MaxLength = 15;
            this.senhaLoginTextBox.Name = "senhaLoginTextBox";
            this.senhaLoginTextBox.NearImage = global::Suportte.Properties.Resources.Key;
            this.senhaLoginTextBox.PasswordChar = '●';
            this.senhaLoginTextBox.Size = new System.Drawing.Size(276, 26);
            this.senhaLoginTextBox.TabIndex = 3;
            this.senhaLoginTextBox.ThemeName = "Default";
            this.senhaLoginTextBox.UseBorderColorOnFocus = true;
            this.senhaLoginTextBox.UseSystemPasswordChar = true;
            this.senhaLoginTextBox.Enter += new System.EventHandler(this.senhaLoginTextBox_Enter);
            this.senhaLoginTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.senhaLoginTextBox_KeyDown);
            this.senhaLoginTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.senhaLoginTextBox_Validating);
            // 
            // usuarioLoginTextBox
            // 
            this.usuarioLoginTextBox.AcceptsReturn = true;
            this.usuarioLoginTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.usuarioLoginTextBox.BeforeTouchSize = new System.Drawing.Size(276, 26);
            this.usuarioLoginTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.usuarioLoginTextBox.BorderSides = System.Windows.Forms.Border3DSide.Bottom;
            this.usuarioLoginTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.usuarioLoginTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.usuarioLoginTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.usuarioLoginTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.usuarioLoginTextBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold);
            this.usuarioLoginTextBox.ForeColor = System.Drawing.Color.Black;
            this.usuarioLoginTextBox.Location = new System.Drawing.Point(68, 167);
            this.usuarioLoginTextBox.MaxLength = 30;
            this.usuarioLoginTextBox.Name = "usuarioLoginTextBox";
            this.usuarioLoginTextBox.NearImage = global::Suportte.Properties.Resources.User;
            this.usuarioLoginTextBox.Size = new System.Drawing.Size(276, 26);
            this.usuarioLoginTextBox.TabIndex = 1;
            this.usuarioLoginTextBox.ThemeName = "Default";
            this.usuarioLoginTextBox.UseBorderColorOnFocus = true;
            this.usuarioLoginTextBox.Enter += new System.EventHandler(this.usuarioLoginTextBox_Enter);
            this.usuarioLoginTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.usuarioLoginTextBox_KeyDown);
            this.usuarioLoginTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.usuarioLoginTextBox_Validating);
            // 
            // acessarLoginButton
            // 
            this.acessarLoginButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.acessarLoginButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(91)))), ((int)(((byte)(149)))));
            this.acessarLoginButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.acessarLoginButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.acessarLoginButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.acessarLoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.acessarLoginButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.acessarLoginButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.acessarLoginButton.Location = new System.Drawing.Point(156, 273);
            this.acessarLoginButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.acessarLoginButton.Name = "acessarLoginButton";
            this.acessarLoginButton.OverrideFormManagedColor = true;
            this.acessarLoginButton.Size = new System.Drawing.Size(103, 34);
            this.acessarLoginButton.TabIndex = 6;
            this.acessarLoginButton.Text = "&Acessar";
            this.acessarLoginButton.ThemeName = "Metro";
            this.acessarLoginButton.UseVisualStyle = true;
            this.acessarLoginButton.Click += new System.EventHandler(this.acessarLoginButton_Click);
            // 
            // pictureBox48
            // 
            this.pictureBox48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(151)))), ((int)(((byte)(178)))));
            this.pictureBox48.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox48.Location = new System.Drawing.Point(0, 0);
            this.pictureBox48.Name = "pictureBox48";
            this.pictureBox48.Size = new System.Drawing.Size(407, 4);
            this.pictureBox48.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox48.TabIndex = 13;
            this.pictureBox48.TabStop = false;
            // 
            // pictureBox47
            // 
            this.pictureBox47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(151)))), ((int)(((byte)(178)))));
            this.pictureBox47.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox47.Location = new System.Drawing.Point(0, 314);
            this.pictureBox47.Name = "pictureBox47";
            this.pictureBox47.Size = new System.Drawing.Size(407, 4);
            this.pictureBox47.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox47.TabIndex = 12;
            this.pictureBox47.TabStop = false;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.label27.Location = new System.Drawing.Point(66, 194);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(41, 15);
            this.label27.TabIndex = 2;
            this.label27.Text = "Senha";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.label26.Location = new System.Drawing.Point(65, 149);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 15);
            this.label26.TabIndex = 0;
            this.label26.Text = "Usuário";
            // 
            // esqueceuSenhaLabel
            // 
            this.esqueceuSenhaLabel.AutoSize = true;
            this.esqueceuSenhaLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.esqueceuSenhaLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.esqueceuSenhaLabel.Location = new System.Drawing.Point(65, 245);
            this.esqueceuSenhaLabel.Name = "esqueceuSenhaLabel";
            this.esqueceuSenhaLabel.Size = new System.Drawing.Size(106, 15);
            this.esqueceuSenhaLabel.TabIndex = 4;
            this.esqueceuSenhaLabel.Text = "Esqueceu a senha";
            this.esqueceuSenhaLabel.Click += new System.EventHandler(this.esqueceuSenhaLabel_Click);
            this.esqueceuSenhaLabel.MouseLeave += new System.EventHandler(this.esqueceuSenhaLabel_MouseLeave);
            this.esqueceuSenhaLabel.MouseHover += new System.EventHandler(this.esqueceuSenhaLabel_MouseHover);
            // 
            // pictureBox41
            // 
            this.pictureBox41.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox41.Image")));
            this.pictureBox41.Location = new System.Drawing.Point(108, 18);
            this.pictureBox41.Name = "pictureBox41";
            this.pictureBox41.Size = new System.Drawing.Size(192, 107);
            this.pictureBox41.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox41.TabIndex = 4;
            this.pictureBox41.TabStop = false;
            // 
            // superToolTip1
            // 
            this.superToolTip1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            // 
            // TelaLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 318);
            this.ControlBox = false;
            this.Controls.Add(this.loginPanel);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaLogin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Login";
            this.Shown += new System.EventHandler(this.TelaLogin_Shown);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.senhaLoginTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usuarioLoginTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox48)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox47)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox41)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel loginPanel;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt senhaLoginTextBox;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt usuarioLoginTextBox;
        private Syncfusion.Windows.Forms.ButtonAdv acessarLoginButton;
        private System.Windows.Forms.PictureBox pictureBox48;
        private System.Windows.Forms.PictureBox pictureBox47;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label esqueceuSenhaLabel;
        private System.Windows.Forms.PictureBox pictureBox41;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.SuperToolTip superToolTip1;
    }
}