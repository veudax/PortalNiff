namespace Suportte.DepartamentoPessoal
{
    partial class NotificacaoRetornoFerias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificacaoRetornoFerias));
            this.camposPanel = new System.Windows.Forms.Panel();
            this.mensagemTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.naoButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.simButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.imagemPictureBox = new System.Windows.Forms.PictureBox();
            this.camposPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mensagemTextBox)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagemPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // camposPanel
            // 
            this.camposPanel.Controls.Add(this.mensagemTextBox);
            this.camposPanel.Controls.Add(this.imagemPictureBox);
            this.camposPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camposPanel.Location = new System.Drawing.Point(0, 0);
            this.camposPanel.Name = "camposPanel";
            this.camposPanel.Size = new System.Drawing.Size(608, 139);
            this.camposPanel.TabIndex = 2;
            // 
            // mensagemTextBox
            // 
            this.mensagemTextBox.AcceptsReturn = true;
            this.mensagemTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.mensagemTextBox.BeforeTouchSize = new System.Drawing.Size(495, 122);
            this.mensagemTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.mensagemTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mensagemTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.mensagemTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.mensagemTextBox.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensagemTextBox.Location = new System.Drawing.Point(101, 8);
            this.mensagemTextBox.MaxLength = 2000;
            this.mensagemTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.mensagemTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.mensagemTextBox.Multiline = true;
            this.mensagemTextBox.Name = "mensagemTextBox";
            this.mensagemTextBox.ReadOnly = true;
            this.mensagemTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mensagemTextBox.Size = new System.Drawing.Size(495, 122);
            this.mensagemTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.mensagemTextBox.TabIndex = 6;
            this.mensagemTextBox.TabStop = false;
            this.mensagemTextBox.UseBorderColorOnFocus = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Controls.Add(this.naoButton);
            this.panel3.Controls.Add(this.simButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 139);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(608, 62);
            this.panel3.TabIndex = 3;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(608, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 21;
            this.pictureBox44.TabStop = false;
            // 
            // naoButton
            // 
            this.naoButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.naoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.naoButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.naoButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.naoButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.naoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.naoButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.naoButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.naoButton.IsBackStageButton = false;
            this.naoButton.Location = new System.Drawing.Point(342, 16);
            this.naoButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.naoButton.Name = "naoButton";
            this.naoButton.OverrideFormManagedColor = true;
            this.naoButton.Size = new System.Drawing.Size(103, 34);
            this.naoButton.TabIndex = 19;
            this.naoButton.Text = "&Não";
            this.naoButton.Click += new System.EventHandler(this.naoButton_Click);
            this.naoButton.Enter += new System.EventHandler(this.naoButton_Click);
            this.naoButton.Validating += new System.ComponentModel.CancelEventHandler(this.naoButton_Validating);
            // 
            // simButton
            // 
            this.simButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.simButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.simButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.simButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.simButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.simButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.simButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.simButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.simButton.IsBackStageButton = false;
            this.simButton.Location = new System.Drawing.Point(164, 16);
            this.simButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.simButton.Name = "simButton";
            this.simButton.OverrideFormManagedColor = true;
            this.simButton.Size = new System.Drawing.Size(103, 34);
            this.simButton.TabIndex = 1;
            this.simButton.Text = "&Sim";
            this.simButton.Click += new System.EventHandler(this.simButton_Click);
            this.simButton.Enter += new System.EventHandler(this.simButton_Enter);
            this.simButton.Validating += new System.ComponentModel.CancelEventHandler(this.simButton_Validating);
            // 
            // imagemPictureBox
            // 
            this.imagemPictureBox.Image = global::Suportte.Properties.Resources.Confirmacao;
            this.imagemPictureBox.Location = new System.Drawing.Point(16, 29);
            this.imagemPictureBox.Name = "imagemPictureBox";
            this.imagemPictureBox.Size = new System.Drawing.Size(75, 75);
            this.imagemPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imagemPictureBox.TabIndex = 5;
            this.imagemPictureBox.TabStop = false;
            // 
            // NotificacaoRetornoFerias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 201);
            this.ControlBox = false;
            this.Controls.Add(this.camposPanel);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotificacaoRetornoFerias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NotificacaoRetornoFerias";
            this.Shown += new System.EventHandler(this.NotificacaoRetornoFerias_Shown);
            this.camposPanel.ResumeLayout(false);
            this.camposPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mensagemTextBox)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagemPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel camposPanel;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt mensagemTextBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv naoButton;
        private Syncfusion.Windows.Forms.ButtonAdv simButton;
        private System.Windows.Forms.PictureBox imagemPictureBox;
    }
}