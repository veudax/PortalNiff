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
            Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo2 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();
            Syncfusion.Windows.Forms.Tools.CustomImageCollection customImageCollection1 = new Syncfusion.Windows.Forms.Tools.CustomImageCollection();
            Syncfusion.Windows.Forms.Tools.ResetButton resetButton1 = new Syncfusion.Windows.Forms.Tools.ResetButton();
            Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo1 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();
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
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.descricaoTextBox)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.problemaResolvidoComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dentroDoPrazoBoxAdv1)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloPanel
            // 
            this.tituloPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.tituloPanel.Controls.Add(this.tituloLabel);
            this.tituloPanel.Controls.Add(this.powerPictureBox);
            this.tituloPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloPanel.Name = "tituloPanel";
            this.tituloPanel.Size = new System.Drawing.Size(493, 40);
            this.tituloPanel.TabIndex = 1;
            this.tituloPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.tituloLabel.ForeColor = System.Drawing.Color.GreenYellow;
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
            this.powerPictureBox.Location = new System.Drawing.Point(453, 0);
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
            this.descricaoTextBox.BeforeTouchSize = new System.Drawing.Size(210, 20);
            this.descricaoTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.descricaoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descricaoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.descricaoTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.descricaoTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.descricaoTextBox.Location = new System.Drawing.Point(12, 173);
            this.descricaoTextBox.MaxLength = 2000;
            this.descricaoTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.descricaoTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.descricaoTextBox.Multiline = true;
            this.descricaoTextBox.Name = "descricaoTextBox";
            this.descricaoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descricaoTextBox.Size = new System.Drawing.Size(469, 139);
            this.descricaoTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.descricaoTextBox.TabIndex = 7;
            toolTipInfo2.Body.Size = new System.Drawing.Size(20, 20);
            toolTipInfo2.Body.Text = "";
            toolTipInfo2.Footer.Size = new System.Drawing.Size(20, 20);
            toolTipInfo2.Header.Size = new System.Drawing.Size(20, 20);
            this.superToolTip.SetToolTip(this.descricaoTextBox, toolTipInfo2);
            this.descricaoTextBox.UseBorderColorOnFocus = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Comentários adicionais";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Controls.Add(this.gravarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 321);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(493, 62);
            this.panel3.TabIndex = 8;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(493, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 18;
            this.pictureBox44.TabStop = false;
            // 
            // gravarButton
            // 
            this.gravarButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.gravarButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.gravarButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.gravarButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.gravarButton.Enabled = false;
            this.gravarButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.gravarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gravarButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.gravarButton.ForeColor = System.Drawing.Color.DarkGreen;
            this.gravarButton.IsBackStageButton = false;
            this.gravarButton.Location = new System.Drawing.Point(192, 16);
            this.gravarButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.gravarButton.Name = "gravarButton";
            this.gravarButton.OverrideFormManagedColor = true;
            this.gravarButton.Size = new System.Drawing.Size(103, 34);
            this.gravarButton.TabIndex = 0;
            this.gravarButton.Text = "&Confirmar";
            this.gravarButton.Click += new System.EventHandler(this.gravarButton_Click);
            // 
            // avaliacaoRatingControl
            // 
            this.avaliacaoRatingControl.ApplyGradientColors = false;
            this.avaliacaoRatingControl.Images = customImageCollection1;
            this.avaliacaoRatingControl.ItemBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(165)))), ((int)(((byte)(28)))));
            this.avaliacaoRatingControl.Location = new System.Drawing.Point(12, 122);
            this.avaliacaoRatingControl.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(165)))), ((int)(((byte)(28)))));
            this.avaliacaoRatingControl.MinimumSize = new System.Drawing.Size(158, 29);
            this.avaliacaoRatingControl.Name = "avaliacaoRatingControl";
            resetButton1.BackgroundImage = null;
            this.avaliacaoRatingControl.ResetButton = resetButton1;
            this.avaliacaoRatingControl.ShowTooltip = false;
            this.avaliacaoRatingControl.Size = new System.Drawing.Size(158, 29);
            this.avaliacaoRatingControl.Style = Syncfusion.Windows.Forms.Tools.RatingControl.Styles.Metro;
            this.avaliacaoRatingControl.TabIndex = 5;
            this.avaliacaoRatingControl.Text = "ratingControl1";
            toolTipInfo1.Body.Size = new System.Drawing.Size(20, 20);
            toolTipInfo1.Body.Text = "Uma estrela = Ruim\r\nDuas estrelas = Bom\r\nTrês estrelas = Regular\r\nQuatro estrelas" +
    " = Muito bom\r\nCinco estrelas  Excelente";
            toolTipInfo1.Footer.Size = new System.Drawing.Size(20, 20);
            toolTipInfo1.Header.Size = new System.Drawing.Size(20, 20);
            this.superToolTip.SetToolTip(this.avaliacaoRatingControl, toolTipInfo1);
            this.avaliacaoRatingControl.ValueChanged += new Syncfusion.Windows.Forms.Tools.RatingValueChangedEventHandler(this.avaliacaoRatingControl_ValueChanged);
            // 
            // superToolTip
            // 
            this.superToolTip.MetroColor = System.Drawing.Color.LightBlue;
            this.superToolTip.VisualStyle = Syncfusion.Windows.Forms.Tools.SuperToolTip.Appearance.Metro;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Problema foi resolvido? ";
            // 
            // problemaResolvidoComboBox
            // 
            this.problemaResolvidoComboBox.BackColor = System.Drawing.SystemColors.Control;
            this.problemaResolvidoComboBox.BeforeTouchSize = new System.Drawing.Size(125, 25);
            this.problemaResolvidoComboBox.FlatBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.problemaResolvidoComboBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.problemaResolvidoComboBox.Location = new System.Drawing.Point(16, 71);
            this.problemaResolvidoComboBox.Name = "problemaResolvidoComboBox";
            this.problemaResolvidoComboBox.Size = new System.Drawing.Size(125, 25);
            this.problemaResolvidoComboBox.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.problemaResolvidoComboBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(218, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Seu chamado foi antendido dentro do prazo? ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(367, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "De modo geral, como você avalia a qualidade do atendimento?";
            // 
            // dentroDoPrazoBoxAdv1
            // 
            this.dentroDoPrazoBoxAdv1.BackColor = System.Drawing.SystemColors.Control;
            this.dentroDoPrazoBoxAdv1.BeforeTouchSize = new System.Drawing.Size(125, 25);
            this.dentroDoPrazoBoxAdv1.FlatBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.dentroDoPrazoBoxAdv1.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.dentroDoPrazoBoxAdv1.Location = new System.Drawing.Point(221, 71);
            this.dentroDoPrazoBoxAdv1.Name = "dentroDoPrazoBoxAdv1";
            this.dentroDoPrazoBoxAdv1.Size = new System.Drawing.Size(125, 25);
            this.dentroDoPrazoBoxAdv1.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.dentroDoPrazoBoxAdv1.TabIndex = 3;
            // 
            // Avaliacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 383);
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
    }
}