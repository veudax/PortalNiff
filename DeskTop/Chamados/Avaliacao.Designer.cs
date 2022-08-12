namespace Suportte.Chamados
{
    partial class Avaliacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Avaliacao));
            Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo1 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();
            Syncfusion.Windows.Forms.Tools.CustomImageCollection customImageCollection1 = new Syncfusion.Windows.Forms.Tools.CustomImageCollection();
            Syncfusion.Windows.Forms.Tools.ResetButton resetButton1 = new Syncfusion.Windows.Forms.Tools.ResetButton();
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.descricaoTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.gravarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.avaliacaoRatingControl = new Syncfusion.Windows.Forms.Tools.RatingControl();
            this.superToolTip = new Syncfusion.Windows.Forms.Tools.SuperToolTip(this);
            this.label1 = new System.Windows.Forms.Label();
            this.problemaResolvidoComboBox = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dentroDoPrazoBoxAdv1 = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.CortezComboBox = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.descricaoTextBox)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.problemaResolvidoComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dentroDoPrazoBoxAdv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CortezComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloPanel
            // 
            this.tituloPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.tituloPanel.Controls.Add(this.tituloLabel);
            this.tituloPanel.Controls.Add(this.powerPictureBox);
            this.tituloPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloPanel.Name = "tituloPanel";
            this.tituloPanel.Size = new System.Drawing.Size(566, 40);
            this.tituloPanel.TabIndex = 1;
            this.tituloPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.tituloLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tituloLabel.Location = new System.Drawing.Point(12, 10);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(167, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Avalie o chamado";
            this.tituloLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(526, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // descricaoTextBox
            // 
            this.descricaoTextBox.AcceptsReturn = true;
            this.descricaoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.descricaoTextBox.BeforeTouchSize = new System.Drawing.Size(542, 162);
            this.descricaoTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.descricaoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descricaoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.descricaoTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.descricaoTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.descricaoTextBox.Location = new System.Drawing.Point(12, 245);
            this.descricaoTextBox.MaxLength = 2000;
            this.descricaoTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.descricaoTextBox.Multiline = true;
            this.descricaoTextBox.Name = "descricaoTextBox";
            this.descricaoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descricaoTextBox.Size = new System.Drawing.Size(542, 162);
            this.descricaoTextBox.TabIndex = 9;
            this.descricaoTextBox.ThemeName = "Default";
            toolTipInfo1.Body.Size = new System.Drawing.Size(20, 20);
            toolTipInfo1.Body.Text = "Escreva o motivo para sua avaliação.\r\nNotas abaixo de 4 estrelas é obrigatório o " +
    "motivo.\r\n\r\n\r\n";
            toolTipInfo1.Footer.Size = new System.Drawing.Size(20, 20);
            toolTipInfo1.Header.Size = new System.Drawing.Size(20, 20);
            this.superToolTip.SetToolTip(this.descricaoTextBox, toolTipInfo1);
            this.descricaoTextBox.UseBorderColorOnFocus = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(391, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Comentários adicionais. Apenas a gerência terá acesso ao comentário";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Controls.Add(this.gravarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 413);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(566, 62);
            this.panel3.TabIndex = 10;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(566, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 18;
            this.pictureBox44.TabStop = false;
            // 
            // gravarButton
            // 
            this.gravarButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.gravarButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.gravarButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.gravarButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.gravarButton.Enabled = false;
            this.gravarButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.gravarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gravarButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.gravarButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.gravarButton.Location = new System.Drawing.Point(232, 16);
            this.gravarButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.gravarButton.Name = "gravarButton";
            this.gravarButton.OverrideFormManagedColor = true;
            this.gravarButton.Size = new System.Drawing.Size(103, 34);
            this.gravarButton.TabIndex = 0;
            this.gravarButton.Text = "&Confirmar";
            this.gravarButton.ThemeName = "Metro";
            this.gravarButton.Click += new System.EventHandler(this.gravarButton_Click);
            // 
            // avaliacaoRatingControl
            // 
            this.avaliacaoRatingControl.ApplyGradientColors = false;
            this.avaliacaoRatingControl.Images = customImageCollection1;
            this.avaliacaoRatingControl.ItemBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.avaliacaoRatingControl.ItemHighlightColor = System.Drawing.Color.SteelBlue;
            this.avaliacaoRatingControl.ItemHighlightEndColor = System.Drawing.Color.SkyBlue;
            this.avaliacaoRatingControl.ItemHighlightStartColor = System.Drawing.Color.SteelBlue;
            this.avaliacaoRatingControl.ItemSelectionColor = System.Drawing.Color.SteelBlue;
            this.avaliacaoRatingControl.ItemSelectionStartColor = System.Drawing.Color.SteelBlue;
            this.avaliacaoRatingControl.Location = new System.Drawing.Point(12, 194);
            this.avaliacaoRatingControl.MetroColor = System.Drawing.Color.SteelBlue;
            this.avaliacaoRatingControl.MinimumSize = new System.Drawing.Size(158, 29);
            this.avaliacaoRatingControl.Name = "avaliacaoRatingControl";
            this.avaliacaoRatingControl.ResetButton = resetButton1;
            this.avaliacaoRatingControl.ShowTooltip = false;
            this.avaliacaoRatingControl.Size = new System.Drawing.Size(158, 29);
            this.avaliacaoRatingControl.Style = Syncfusion.Windows.Forms.Tools.RatingControl.Styles.Metro;
            this.avaliacaoRatingControl.TabIndex = 7;
            this.avaliacaoRatingControl.Text = "ratingControl1";
            this.avaliacaoRatingControl.ThemeName = "Metro";
            this.avaliacaoRatingControl.ValueChanged += new Syncfusion.Windows.Forms.Tools.RatingValueChangedEventHandler(this.avaliacaoRatingControl_ValueChanged);
            // 
            // superToolTip
            // 
            this.superToolTip.MetroColor = System.Drawing.Color.LightBlue;
            this.superToolTip.ThemeName = "Metro";
            this.superToolTip.VisualStyle = Syncfusion.Windows.Forms.Tools.SuperToolTip.Appearance.Metro;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.label1.Location = new System.Drawing.Point(9, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Problema foi resolvido? ";
            // 
            // problemaResolvidoComboBox
            // 
            this.problemaResolvidoComboBox.BeforeTouchSize = new System.Drawing.Size(125, 25);
            this.problemaResolvidoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.problemaResolvidoComboBox.FlatBorderColor = System.Drawing.Color.DimGray;
            this.problemaResolvidoComboBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.problemaResolvidoComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.problemaResolvidoComboBox.Location = new System.Drawing.Point(12, 99);
            this.problemaResolvidoComboBox.Name = "problemaResolvidoComboBox";
            this.problemaResolvidoComboBox.Size = new System.Drawing.Size(125, 25);
            this.problemaResolvidoComboBox.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.problemaResolvidoComboBox.TabIndex = 1;
            this.problemaResolvidoComboBox.ThemeName = "VS2010";
            this.problemaResolvidoComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.problemaResolvidoComboBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.label2.Location = new System.Drawing.Point(257, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(304, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Seu chamado foi atendido em tempo hábil? ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.label3.Location = new System.Drawing.Point(9, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(439, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "De modo geral, como você avalia a qualidade do atendimento?";
            // 
            // dentroDoPrazoBoxAdv1
            // 
            this.dentroDoPrazoBoxAdv1.BeforeTouchSize = new System.Drawing.Size(125, 25);
            this.dentroDoPrazoBoxAdv1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dentroDoPrazoBoxAdv1.FlatBorderColor = System.Drawing.Color.DimGray;
            this.dentroDoPrazoBoxAdv1.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.dentroDoPrazoBoxAdv1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dentroDoPrazoBoxAdv1.Location = new System.Drawing.Point(260, 99);
            this.dentroDoPrazoBoxAdv1.Name = "dentroDoPrazoBoxAdv1";
            this.dentroDoPrazoBoxAdv1.Size = new System.Drawing.Size(125, 25);
            this.dentroDoPrazoBoxAdv1.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.dentroDoPrazoBoxAdv1.TabIndex = 3;
            this.dentroDoPrazoBoxAdv1.ThemeName = "VS2010";
            this.dentroDoPrazoBoxAdv1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.problemaResolvidoComboBox_KeyDown);
            // 
            // CortezComboBox
            // 
            this.CortezComboBox.BeforeTouchSize = new System.Drawing.Size(125, 25);
            this.CortezComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CortezComboBox.FlatBorderColor = System.Drawing.Color.DimGray;
            this.CortezComboBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.CortezComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CortezComboBox.Location = new System.Drawing.Point(12, 146);
            this.CortezComboBox.Name = "CortezComboBox";
            this.CortezComboBox.Size = new System.Drawing.Size(125, 25);
            this.CortezComboBox.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.CortezComboBox.TabIndex = 5;
            this.CortezComboBox.ThemeName = "VS2010";
            this.CortezComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.problemaResolvidoComboBox_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.label4.Location = new System.Drawing.Point(9, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Atendente foi cortês nas respostas? ";
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(0, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(566, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "Atenção: A avaliação deve ser restritamente profissional. ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Avaliacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 475);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CortezComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dentroDoPrazoBoxAdv1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.problemaResolvidoComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.avaliacaoRatingControl);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.descricaoTextBox);
            this.Controls.Add(this.tituloPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Avaliacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Avaliacao";
            this.Shown += new System.EventHandler(this.Avaliacao_Shown);
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.descricaoTextBox)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.problemaResolvidoComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dentroDoPrazoBoxAdv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CortezComboBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt descricaoTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv gravarButton;
        private Syncfusion.Windows.Forms.Tools.RatingControl avaliacaoRatingControl;
        private Syncfusion.Windows.Forms.Tools.SuperToolTip superToolTip;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv dentroDoPrazoBoxAdv1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv problemaResolvidoComboBox;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv CortezComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}