namespace Suportte.Chamados
{
    partial class Lembrete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lembrete));
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.focotextBox1 = new System.Windows.Forms.TextBox();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.DadosPanel = new System.Windows.Forms.Panel();
            this.sfCalendar1 = new Syncfusion.WinForms.Input.SfCalendar();
            this.SelecionarDatasCheckBox = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.descricaoTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.dataAberturaDateTimePicker = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.label8 = new System.Windows.Forms.Label();
            this.PrazoEntradaLabel = new System.Windows.Forms.Label();
            this.PrazoEntregaTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gravarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.skinManager1 = new Syncfusion.Windows.Forms.SkinManager(this.components);
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.DadosPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelecionarDatasCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.descricaoTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataAberturaDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrazoEntregaTextBox)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tituloPanel
            // 
            this.tituloPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.tituloPanel.Controls.Add(this.powerPictureBox);
            this.tituloPanel.Controls.Add(this.focotextBox1);
            this.tituloPanel.Controls.Add(this.tituloLabel);
            this.tituloPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloPanel.Name = "tituloPanel";
            this.tituloPanel.Size = new System.Drawing.Size(657, 40);
            this.tituloPanel.TabIndex = 1;
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(617, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 48;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // focotextBox1
            // 
            this.focotextBox1.Location = new System.Drawing.Point(618, -27);
            this.focotextBox1.Name = "focotextBox1";
            this.focotextBox1.Size = new System.Drawing.Size(100, 21);
            this.focotextBox1.TabIndex = 47;
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.tituloLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tituloLabel.Location = new System.Drawing.Point(12, 10);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(209, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Lembrete do Chamado";
            // 
            // DadosPanel
            // 
            this.DadosPanel.Controls.Add(this.sfCalendar1);
            this.DadosPanel.Controls.Add(this.SelecionarDatasCheckBox);
            this.DadosPanel.Controls.Add(this.descricaoTextBox);
            this.DadosPanel.Controls.Add(this.label1);
            this.DadosPanel.Controls.Add(this.dataAberturaDateTimePicker);
            this.DadosPanel.Controls.Add(this.label8);
            this.DadosPanel.Controls.Add(this.PrazoEntradaLabel);
            this.DadosPanel.Controls.Add(this.PrazoEntregaTextBox);
            this.DadosPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DadosPanel.Location = new System.Drawing.Point(0, 40);
            this.DadosPanel.Name = "DadosPanel";
            this.DadosPanel.Size = new System.Drawing.Size(657, 222);
            this.DadosPanel.TabIndex = 48;
            // 
            // sfCalendar1
            // 
            this.sfCalendar1.Location = new System.Drawing.Point(449, 12);
            this.sfCalendar1.Name = "sfCalendar1";
            this.sfCalendar1.Size = new System.Drawing.Size(196, 196);
            this.sfCalendar1.TabIndex = 55;
            this.sfCalendar1.ThemeName = "";
            this.sfCalendar1.Visible = false;
            this.sfCalendar1.SelectionChanged += new Syncfusion.WinForms.Input.Events.SelectionChangedEventHandler(this.sfCalendar1_SelectionChanged);
            // 
            // SelecionarDatasCheckBox
            // 
            this.SelecionarDatasCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelecionarDatasCheckBox.BeforeTouchSize = new System.Drawing.Size(223, 18);
            this.SelecionarDatasCheckBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.SelecionarDatasCheckBox.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelecionarDatasCheckBox.Location = new System.Drawing.Point(198, 29);
            this.SelecionarDatasCheckBox.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.SelecionarDatasCheckBox.Name = "SelecionarDatasCheckBox";
            this.SelecionarDatasCheckBox.Size = new System.Drawing.Size(223, 18);
            this.SelecionarDatasCheckBox.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.SelecionarDatasCheckBox.TabIndex = 54;
            this.SelecionarDatasCheckBox.Text = "Selecionar mais Datas (até 4 datas)";
            this.SelecionarDatasCheckBox.ThemeName = "Metro";
            this.SelecionarDatasCheckBox.ThemesEnabled = false;
            this.SelecionarDatasCheckBox.CheckedChanged += new System.EventHandler(this.SelecionarDatasCheckBox_CheckedChanged);
            // 
            // descricaoTextBox
            // 
            this.descricaoTextBox.AcceptsReturn = true;
            this.descricaoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.descricaoTextBox.BeforeTouchSize = new System.Drawing.Size(636, 139);
            this.descricaoTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.descricaoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descricaoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.descricaoTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.descricaoTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.descricaoTextBox.Location = new System.Drawing.Point(9, 71);
            this.descricaoTextBox.MaxLength = 2000;
            this.descricaoTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.descricaoTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.descricaoTextBox.Multiline = true;
            this.descricaoTextBox.Name = "descricaoTextBox";
            this.descricaoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descricaoTextBox.Size = new System.Drawing.Size(636, 139);
            this.descricaoTextBox.TabIndex = 53;
            this.descricaoTextBox.ThemeName = "Default";
            this.descricaoTextBox.UseBorderColorOnFocus = true;
            this.descricaoTextBox.Enter += new System.EventHandler(this.PrazoEntregaTextBox_Enter);
            this.descricaoTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.descricaoTextBox_KeyDown);
            this.descricaoTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.descricaoTextBox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 52;
            this.label1.Text = "Lembrete";
            this.label1.Visible = false;
            // 
            // dataAberturaDateTimePicker
            // 
            this.dataAberturaDateTimePicker.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dataAberturaDateTimePicker.BorderColor = System.Drawing.Color.Empty;
            this.dataAberturaDateTimePicker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataAberturaDateTimePicker.CalendarSize = new System.Drawing.Size(189, 176);
            this.dataAberturaDateTimePicker.Checked = false;
            this.dataAberturaDateTimePicker.ClipboardFormat = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataAberturaDateTimePicker.Culture = new System.Globalization.CultureInfo("pt-BR");
            this.dataAberturaDateTimePicker.DropDownImage = null;
            this.dataAberturaDateTimePicker.DropDownNormalColor = System.Drawing.SystemColors.Control;
            this.dataAberturaDateTimePicker.Enabled = false;
            this.dataAberturaDateTimePicker.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.dataAberturaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataAberturaDateTimePicker.Location = new System.Drawing.Point(55, 29);
            this.dataAberturaDateTimePicker.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.dataAberturaDateTimePicker.MinValue = new System.DateTime(((long)(0)));
            this.dataAberturaDateTimePicker.Name = "dataAberturaDateTimePicker";
            this.dataAberturaDateTimePicker.ShowCheckBox = false;
            this.dataAberturaDateTimePicker.Size = new System.Drawing.Size(126, 23);
            this.dataAberturaDateTimePicker.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.dataAberturaDateTimePicker.TabIndex = 51;
            this.dataAberturaDateTimePicker.Value = new System.DateTime(2017, 9, 1, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(55, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 16);
            this.label8.TabIndex = 50;
            this.label8.Text = "Data do lembrete";
            // 
            // PrazoEntradaLabel
            // 
            this.PrazoEntradaLabel.AutoSize = true;
            this.PrazoEntradaLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrazoEntradaLabel.Location = new System.Drawing.Point(5, 12);
            this.PrazoEntradaLabel.Name = "PrazoEntradaLabel";
            this.PrazoEntradaLabel.Size = new System.Drawing.Size(38, 16);
            this.PrazoEntradaLabel.TabIndex = 49;
            this.PrazoEntradaLabel.Text = "Prazo";
            // 
            // PrazoEntregaTextBox
            // 
            this.PrazoEntregaTextBox.AcceptsReturn = true;
            this.PrazoEntregaTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.PrazoEntregaTextBox.BeforeTouchSize = new System.Drawing.Size(636, 139);
            this.PrazoEntregaTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.PrazoEntregaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PrazoEntregaTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.PrazoEntregaTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.PrazoEntregaTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.PrazoEntregaTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.PrazoEntregaTextBox.Location = new System.Drawing.Point(8, 29);
            this.PrazoEntregaTextBox.MaxLength = 8;
            this.PrazoEntregaTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.PrazoEntregaTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.PrazoEntregaTextBox.Name = "PrazoEntregaTextBox";
            this.PrazoEntregaTextBox.Size = new System.Drawing.Size(41, 23);
            this.PrazoEntregaTextBox.TabIndex = 48;
            this.PrazoEntregaTextBox.ThemeName = "Default";
            this.PrazoEntregaTextBox.UseBorderColorOnFocus = true;
            this.PrazoEntregaTextBox.Enter += new System.EventHandler(this.PrazoEntregaTextBox_Enter);
            this.PrazoEntregaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrazoEntregaTextBox_KeyDown);
            this.PrazoEntregaTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.PrazoEntregaTextBox_Validating);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gravarButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 262);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(657, 62);
            this.panel2.TabIndex = 49;
            // 
            // gravarButton
            // 
            this.gravarButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.gravarButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.gravarButton.BeforeTouchSize = new System.Drawing.Size(85, 34);
            this.gravarButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.gravarButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.gravarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gravarButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.gravarButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.gravarButton.IsBackStageButton = false;
            this.gravarButton.Location = new System.Drawing.Point(286, 14);
            this.gravarButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.gravarButton.Name = "gravarButton";
            this.gravarButton.OverrideFormManagedColor = true;
            this.gravarButton.Size = new System.Drawing.Size(85, 34);
            this.gravarButton.TabIndex = 18;
            this.gravarButton.Text = "&Gravar";
            this.gravarButton.ThemeName = "Metro";
            this.gravarButton.Click += new System.EventHandler(this.gravarButton_Click);
            // 
            // skinManager1
            // 
            this.skinManager1.Controls = null;
            this.skinManager1.VisualTheme = Syncfusion.Windows.Forms.VisualTheme.Managed;
            // 
            // Lembrete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 324);
            this.ControlBox = false;
            this.Controls.Add(this.DadosPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tituloPanel);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Lembrete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lembrete do Chamado";
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.DadosPanel.ResumeLayout(false);
            this.DadosPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelecionarDatasCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.descricaoTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataAberturaDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrazoEntregaTextBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        private System.Windows.Forms.TextBox focotextBox1;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel DadosPanel;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt descricaoTextBox;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dataAberturaDateTimePicker;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label PrazoEntradaLabel;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt PrazoEntregaTextBox;
        private System.Windows.Forms.Panel panel2;
        private Syncfusion.Windows.Forms.ButtonAdv gravarButton;
        private Syncfusion.WinForms.Input.SfCalendar sfCalendar1;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv SelecionarDatasCheckBox;
        private Syncfusion.Windows.Forms.SkinManager skinManager1;
    }
}