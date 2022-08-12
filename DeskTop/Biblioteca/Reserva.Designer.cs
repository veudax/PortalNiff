namespace Suportte.Biblioteca
{
    partial class Reserva
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reserva));
            this.camposPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dataDateTimePicker = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.CategoriaComboBox = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label4 = new System.Windows.Forms.Label();
            this.PesquisaColaboradorButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.ColaboradorTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.NomeColaboradorTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.NomeTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.pesquisaCategoriaButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.codigoTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label26 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.excluirButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.limparButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.gravarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.camposPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoriaComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColaboradorTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomeColaboradorTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomeTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codigoTextBox)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).BeginInit();
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // camposPanel
            // 
            this.camposPanel.Controls.Add(this.label5);
            this.camposPanel.Controls.Add(this.dataDateTimePicker);
            this.camposPanel.Controls.Add(this.CategoriaComboBox);
            this.camposPanel.Controls.Add(this.label4);
            this.camposPanel.Controls.Add(this.PesquisaColaboradorButton);
            this.camposPanel.Controls.Add(this.ColaboradorTextBox);
            this.camposPanel.Controls.Add(this.label1);
            this.camposPanel.Controls.Add(this.NomeColaboradorTextBox);
            this.camposPanel.Controls.Add(this.NomeTextBox);
            this.camposPanel.Controls.Add(this.pesquisaCategoriaButton);
            this.camposPanel.Controls.Add(this.codigoTextBox);
            this.camposPanel.Controls.Add(this.label26);
            this.camposPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camposPanel.Location = new System.Drawing.Point(0, 40);
            this.camposPanel.Name = "camposPanel";
            this.camposPanel.Size = new System.Drawing.Size(760, 107);
            this.camposPanel.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(517, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "Data";
            // 
            // dataDateTimePicker
            // 
            this.dataDateTimePicker.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dataDateTimePicker.BorderColor = System.Drawing.Color.Empty;
            this.dataDateTimePicker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataDateTimePicker.CalendarSize = new System.Drawing.Size(189, 176);
            this.dataDateTimePicker.Checked = false;
            this.dataDateTimePicker.ClipboardFormat = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataDateTimePicker.Culture = new System.Globalization.CultureInfo("pt-BR");
            this.dataDateTimePicker.DropDownImage = null;
            this.dataDateTimePicker.DropDownNormalColor = System.Drawing.SystemColors.Control;
            this.dataDateTimePicker.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.dataDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dataDateTimePicker.Location = new System.Drawing.Point(520, 72);
            this.dataDateTimePicker.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.dataDateTimePicker.MinValue = new System.DateTime(((long)(0)));
            this.dataDateTimePicker.Name = "dataDateTimePicker";
            this.dataDateTimePicker.ShowCheckBox = false;
            this.dataDateTimePicker.Size = new System.Drawing.Size(111, 23);
            this.dataDateTimePicker.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.dataDateTimePicker.TabIndex = 34;
            this.dataDateTimePicker.Value = new System.DateTime(2017, 9, 1, 0, 0, 0, 0);
            this.dataDateTimePicker.Enter += new System.EventHandler(this.dataDateTimePicker_Enter);
            this.dataDateTimePicker.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataDateTimePicker_PreviewKeyDown);
            this.dataDateTimePicker.Validating += new System.ComponentModel.CancelEventHandler(this.dataDateTimePicker_Validating);
            // 
            // CategoriaComboBox
            // 
            this.CategoriaComboBox.BeforeTouchSize = new System.Drawing.Size(228, 25);
            this.CategoriaComboBox.FlatBorderColor = System.Drawing.Color.DimGray;
            this.CategoriaComboBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.CategoriaComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CategoriaComboBox.Location = new System.Drawing.Point(520, 29);
            this.CategoriaComboBox.Name = "CategoriaComboBox";
            this.CategoriaComboBox.ReadOnly = true;
            this.CategoriaComboBox.Size = new System.Drawing.Size(228, 25);
            this.CategoriaComboBox.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.CategoriaComboBox.TabIndex = 28;
            this.CategoriaComboBox.ThemeName = "VS2010";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(517, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 27;
            this.label4.Text = "Categoria";
            // 
            // PesquisaColaboradorButton
            // 
            this.PesquisaColaboradorButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.PesquisaColaboradorButton.BackColor = System.Drawing.SystemColors.Control;
            this.PesquisaColaboradorButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.PesquisaColaboradorButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.PesquisaColaboradorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PesquisaColaboradorButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.PesquisaColaboradorButton.ForeColor = System.Drawing.Color.Black;
            this.PesquisaColaboradorButton.Image = ((System.Drawing.Image)(resources.GetObject("PesquisaColaboradorButton.Image")));
            this.PesquisaColaboradorButton.IsBackStageButton = false;
            this.PesquisaColaboradorButton.Location = new System.Drawing.Point(110, 72);
            this.PesquisaColaboradorButton.MetroColor = System.Drawing.Color.DimGray;
            this.PesquisaColaboradorButton.Name = "PesquisaColaboradorButton";
            this.PesquisaColaboradorButton.OverrideFormManagedColor = true;
            this.PesquisaColaboradorButton.Size = new System.Drawing.Size(23, 23);
            this.PesquisaColaboradorButton.TabIndex = 31;
            this.PesquisaColaboradorButton.TabStop = false;
            this.PesquisaColaboradorButton.ThemeName = "Metro";
            this.PesquisaColaboradorButton.Click += new System.EventHandler(this.PesquisaColaboradorButton_Click);
            // 
            // ColaboradorTextBox
            // 
            this.ColaboradorTextBox.AcceptsReturn = true;
            this.ColaboradorTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.ColaboradorTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.ColaboradorTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.ColaboradorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColaboradorTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ColaboradorTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ColaboradorTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.ColaboradorTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.ColaboradorTextBox.Location = new System.Drawing.Point(16, 72);
            this.ColaboradorTextBox.MaxLength = 5;
            this.ColaboradorTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.ColaboradorTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.ColaboradorTextBox.Name = "ColaboradorTextBox";
            this.ColaboradorTextBox.Size = new System.Drawing.Size(95, 23);
            this.ColaboradorTextBox.TabIndex = 30;
            this.ColaboradorTextBox.ThemeName = "Default";
            this.ColaboradorTextBox.UseBorderColorOnFocus = true;
            this.ColaboradorTextBox.TextChanged += new System.EventHandler(this.ColaboradorTextBox_TextChanged);
            this.ColaboradorTextBox.Enter += new System.EventHandler(this.ColaboradorTextBox_Enter);
            this.ColaboradorTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ColaboradorTextBox_KeyDown);
            this.ColaboradorTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.ColaboradorTextBox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "Colaborador";
            // 
            // NomeColaboradorTextBox
            // 
            this.NomeColaboradorTextBox.AcceptsReturn = true;
            this.NomeColaboradorTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.NomeColaboradorTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.NomeColaboradorTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.NomeColaboradorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NomeColaboradorTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.NomeColaboradorTextBox.Enabled = false;
            this.NomeColaboradorTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.NomeColaboradorTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.NomeColaboradorTextBox.Location = new System.Drawing.Point(136, 72);
            this.NomeColaboradorTextBox.MaxLength = 50;
            this.NomeColaboradorTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.NomeColaboradorTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.NomeColaboradorTextBox.Name = "NomeColaboradorTextBox";
            this.NomeColaboradorTextBox.Size = new System.Drawing.Size(378, 23);
            this.NomeColaboradorTextBox.TabIndex = 32;
            this.NomeColaboradorTextBox.ThemeName = "Default";
            this.NomeColaboradorTextBox.UseBorderColorOnFocus = true;
            // 
            // NomeTextBox
            // 
            this.NomeTextBox.AcceptsReturn = true;
            this.NomeTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.NomeTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.NomeTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.NomeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NomeTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.NomeTextBox.Enabled = false;
            this.NomeTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.NomeTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.NomeTextBox.Location = new System.Drawing.Point(136, 29);
            this.NomeTextBox.MaxLength = 50;
            this.NomeTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.NomeTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.NomeTextBox.Name = "NomeTextBox";
            this.NomeTextBox.Size = new System.Drawing.Size(378, 23);
            this.NomeTextBox.TabIndex = 26;
            this.NomeTextBox.ThemeName = "Default";
            this.NomeTextBox.UseBorderColorOnFocus = true;
            // 
            // pesquisaCategoriaButton
            // 
            this.pesquisaCategoriaButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.pesquisaCategoriaButton.BackColor = System.Drawing.SystemColors.Control;
            this.pesquisaCategoriaButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.pesquisaCategoriaButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.pesquisaCategoriaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pesquisaCategoriaButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.pesquisaCategoriaButton.ForeColor = System.Drawing.Color.Black;
            this.pesquisaCategoriaButton.Image = ((System.Drawing.Image)(resources.GetObject("pesquisaCategoriaButton.Image")));
            this.pesquisaCategoriaButton.IsBackStageButton = false;
            this.pesquisaCategoriaButton.Location = new System.Drawing.Point(110, 29);
            this.pesquisaCategoriaButton.MetroColor = System.Drawing.Color.DimGray;
            this.pesquisaCategoriaButton.Name = "pesquisaCategoriaButton";
            this.pesquisaCategoriaButton.OverrideFormManagedColor = true;
            this.pesquisaCategoriaButton.Size = new System.Drawing.Size(23, 23);
            this.pesquisaCategoriaButton.TabIndex = 25;
            this.pesquisaCategoriaButton.TabStop = false;
            this.pesquisaCategoriaButton.ThemeName = "Metro";
            this.pesquisaCategoriaButton.Click += new System.EventHandler(this.pesquisaCategoriaButton_Click);
            // 
            // codigoTextBox
            // 
            this.codigoTextBox.AcceptsReturn = true;
            this.codigoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.codigoTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.codigoTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.codigoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codigoTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.codigoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.codigoTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.codigoTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.codigoTextBox.Location = new System.Drawing.Point(16, 29);
            this.codigoTextBox.MaxLength = 5;
            this.codigoTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.codigoTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.codigoTextBox.Name = "codigoTextBox";
            this.codigoTextBox.Size = new System.Drawing.Size(95, 23);
            this.codigoTextBox.TabIndex = 24;
            this.codigoTextBox.ThemeName = "Default";
            this.codigoTextBox.UseBorderColorOnFocus = true;
            this.codigoTextBox.TextChanged += new System.EventHandler(this.codigoTextBox_TextChanged);
            this.codigoTextBox.Enter += new System.EventHandler(this.codigoTextBox_Enter);
            this.codigoTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.codigoTextBox_KeyDown);
            this.codigoTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.codigoTextBox_Validating);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(13, 11);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(32, 15);
            this.label26.TabIndex = 23;
            this.label26.Text = "Livro";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Controls.Add(this.excluirButton);
            this.panel3.Controls.Add(this.limparButton);
            this.panel3.Controls.Add(this.gravarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 147);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(760, 62);
            this.panel3.TabIndex = 4;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(760, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 19;
            this.pictureBox44.TabStop = false;
            // 
            // excluirButton
            // 
            this.excluirButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.excluirButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.excluirButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.excluirButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.excluirButton.Enabled = false;
            this.excluirButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.excluirButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.excluirButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.excluirButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.excluirButton.IsBackStageButton = false;
            this.excluirButton.Location = new System.Drawing.Point(480, 14);
            this.excluirButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.excluirButton.Name = "excluirButton";
            this.excluirButton.OverrideFormManagedColor = true;
            this.excluirButton.Size = new System.Drawing.Size(103, 34);
            this.excluirButton.TabIndex = 2;
            this.excluirButton.Text = "&Excluir";
            this.excluirButton.ThemeName = "Metro";
            this.excluirButton.Click += new System.EventHandler(this.excluirButton_Click);
            this.excluirButton.Enter += new System.EventHandler(this.excluirButton_Enter);
            this.excluirButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.excluirButton_KeyDown);
            this.excluirButton.Validating += new System.ComponentModel.CancelEventHandler(this.excluirButton_Validating);
            // 
            // limparButton
            // 
            this.limparButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.limparButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.limparButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.limparButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.limparButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.limparButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.limparButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.limparButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.limparButton.IsBackStageButton = false;
            this.limparButton.Location = new System.Drawing.Point(329, 14);
            this.limparButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.limparButton.Name = "limparButton";
            this.limparButton.OverrideFormManagedColor = true;
            this.limparButton.Size = new System.Drawing.Size(103, 34);
            this.limparButton.TabIndex = 1;
            this.limparButton.Text = "&Limpar";
            this.limparButton.ThemeName = "Metro";
            this.limparButton.Click += new System.EventHandler(this.limparButton_Click);
            this.limparButton.Enter += new System.EventHandler(this.limparButton_Enter);
            this.limparButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.limparButton_KeyDown);
            this.limparButton.Validating += new System.ComponentModel.CancelEventHandler(this.limparButton_Validating);
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
            this.gravarButton.IsBackStageButton = false;
            this.gravarButton.Location = new System.Drawing.Point(178, 14);
            this.gravarButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.gravarButton.Name = "gravarButton";
            this.gravarButton.OverrideFormManagedColor = true;
            this.gravarButton.Size = new System.Drawing.Size(103, 34);
            this.gravarButton.TabIndex = 0;
            this.gravarButton.Text = "&Gravar";
            this.gravarButton.ThemeName = "Metro";
            this.gravarButton.Click += new System.EventHandler(this.gravarButton_Click);
            this.gravarButton.Enter += new System.EventHandler(this.gravarButton_Enter);
            this.gravarButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gravarButton_KeyDown);
            this.gravarButton.Validating += new System.ComponentModel.CancelEventHandler(this.gravarButton_Validating);
            // 
            // tituloPanel
            // 
            this.tituloPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.tituloPanel.Controls.Add(this.tituloLabel);
            this.tituloPanel.Controls.Add(this.powerPictureBox);
            this.tituloPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloPanel.Name = "tituloPanel";
            this.tituloPanel.Size = new System.Drawing.Size(760, 40);
            this.tituloPanel.TabIndex = 5;
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.tituloLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tituloLabel.Location = new System.Drawing.Point(12, 10);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(77, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Reserva";
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(720, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // Reserva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 209);
            this.ControlBox = false;
            this.Controls.Add(this.camposPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tituloPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reserva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reserva";
            this.Shown += new System.EventHandler(this.Reserva_Shown);
            this.camposPanel.ResumeLayout(false);
            this.camposPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoriaComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColaboradorTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomeColaboradorTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomeTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codigoTextBox)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel camposPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv excluirButton;
        private Syncfusion.Windows.Forms.ButtonAdv limparButton;
        private Syncfusion.Windows.Forms.ButtonAdv gravarButton;
        private System.Windows.Forms.Panel tituloPanel;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv CategoriaComboBox;
        private System.Windows.Forms.Label label4;
        private Syncfusion.Windows.Forms.ButtonAdv PesquisaColaboradorButton;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt NomeColaboradorTextBox;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt NomeTextBox;
        private Syncfusion.Windows.Forms.ButtonAdv pesquisaCategoriaButton;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label5;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dataDateTimePicker;
        public Syncfusion.Windows.Forms.Tools.TextBoxExt ColaboradorTextBox;
        public Syncfusion.Windows.Forms.Tools.TextBoxExt codigoTextBox;
    }
}