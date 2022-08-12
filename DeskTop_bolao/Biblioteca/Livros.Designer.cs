namespace Suportte.Biblioteca
{
    partial class Livros
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Livros));
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.mensagemSistemaLabel = new System.Windows.Forms.Label();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.excluirButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.limparButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.gravarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CategoriaComboBox = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label4 = new System.Windows.Forms.Label();
            this.ConservacaoComboBox = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label3 = new System.Windows.Forms.Label();
            this.tipoComboBox = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label19 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataDateTimePicker = new Syncfusion.Windows.Forms.Tools.DateTimePickerAdv();
            this.NomeColaboradorTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.PesquisaColaboradorButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.ColaboradorTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.pesquisaCategoriaButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.proximoButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.nomeTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.codigoTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label26 = new System.Windows.Forms.Label();
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategoriaComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConservacaoComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tipoComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataDateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomeColaboradorTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColaboradorTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomeTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codigoTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloPanel
            // 
            this.tituloPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.tituloPanel.Controls.Add(this.mensagemSistemaLabel);
            this.tituloPanel.Controls.Add(this.tituloLabel);
            this.tituloPanel.Controls.Add(this.powerPictureBox);
            this.tituloPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloPanel.Name = "tituloPanel";
            this.tituloPanel.Size = new System.Drawing.Size(509, 40);
            this.tituloPanel.TabIndex = 2;
            // 
            // mensagemSistemaLabel
            // 
            this.mensagemSistemaLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.mensagemSistemaLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensagemSistemaLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.mensagemSistemaLabel.Location = new System.Drawing.Point(162, 0);
            this.mensagemSistemaLabel.Name = "mensagemSistemaLabel";
            this.mensagemSistemaLabel.Size = new System.Drawing.Size(307, 40);
            this.mensagemSistemaLabel.TabIndex = 0;
            this.mensagemSistemaLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.tituloLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.tituloLabel.Location = new System.Drawing.Point(12, 10);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(55, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Livros";
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(469, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Controls.Add(this.excluirButton);
            this.panel3.Controls.Add(this.limparButton);
            this.panel3.Controls.Add(this.gravarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 236);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(509, 62);
            this.panel3.TabIndex = 1;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(509, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 19;
            this.pictureBox44.TabStop = false;
            // 
            // excluirButton
            // 
            this.excluirButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.excluirButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.excluirButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.excluirButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.excluirButton.Enabled = false;
            this.excluirButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.excluirButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.excluirButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.excluirButton.ForeColor = System.Drawing.Color.DarkGreen;
            this.excluirButton.IsBackStageButton = false;
            this.excluirButton.Location = new System.Drawing.Point(354, 14);
            this.excluirButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.excluirButton.Name = "excluirButton";
            this.excluirButton.OverrideFormManagedColor = true;
            this.excluirButton.Size = new System.Drawing.Size(103, 34);
            this.excluirButton.TabIndex = 2;
            this.excluirButton.Text = "&Excluir";
            this.excluirButton.Click += new System.EventHandler(this.excluirButton_Click);
            this.excluirButton.Enter += new System.EventHandler(this.excluirButton_Enter);
            this.excluirButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.excluirButton_KeyDown);
            this.excluirButton.Validating += new System.ComponentModel.CancelEventHandler(this.excluirButton_Validating);
            // 
            // limparButton
            // 
            this.limparButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.limparButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.limparButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.limparButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.limparButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.limparButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.limparButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.limparButton.ForeColor = System.Drawing.Color.DarkGreen;
            this.limparButton.IsBackStageButton = false;
            this.limparButton.Location = new System.Drawing.Point(203, 14);
            this.limparButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.limparButton.Name = "limparButton";
            this.limparButton.OverrideFormManagedColor = true;
            this.limparButton.Size = new System.Drawing.Size(103, 34);
            this.limparButton.TabIndex = 1;
            this.limparButton.Text = "&Limpar";
            this.limparButton.Click += new System.EventHandler(this.limparButton_Click);
            this.limparButton.Enter += new System.EventHandler(this.limparButton_Enter);
            this.limparButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.limparButton_KeyDown);
            this.limparButton.Validating += new System.ComponentModel.CancelEventHandler(this.limparButton_Validating);
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
            this.gravarButton.Location = new System.Drawing.Point(52, 14);
            this.gravarButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.gravarButton.Name = "gravarButton";
            this.gravarButton.OverrideFormManagedColor = true;
            this.gravarButton.Size = new System.Drawing.Size(103, 34);
            this.gravarButton.TabIndex = 0;
            this.gravarButton.Text = "&Gravar";
            this.gravarButton.Click += new System.EventHandler(this.gravarButton_Click);
            this.gravarButton.Enter += new System.EventHandler(this.gravarButton_Enter);
            this.gravarButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gravarButton_KeyDown);
            this.gravarButton.Validating += new System.ComponentModel.CancelEventHandler(this.gravarButton_Validating);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CategoriaComboBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.ConservacaoComboBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tipoComboBox);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dataDateTimePicker);
            this.panel1.Controls.Add(this.NomeColaboradorTextBox);
            this.panel1.Controls.Add(this.PesquisaColaboradorButton);
            this.panel1.Controls.Add(this.ColaboradorTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pesquisaCategoriaButton);
            this.panel1.Controls.Add(this.proximoButton);
            this.panel1.Controls.Add(this.nomeTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.codigoTextBox);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 196);
            this.panel1.TabIndex = 0;
            // 
            // CategoriaComboBox
            // 
            this.CategoriaComboBox.BackColor = System.Drawing.SystemColors.Control;
            this.CategoriaComboBox.BeforeTouchSize = new System.Drawing.Size(208, 25);
            this.CategoriaComboBox.FlatBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.CategoriaComboBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.CategoriaComboBox.Location = new System.Drawing.Point(283, 26);
            this.CategoriaComboBox.Name = "CategoriaComboBox";
            this.CategoriaComboBox.Size = new System.Drawing.Size(208, 25);
            this.CategoriaComboBox.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.CategoriaComboBox.TabIndex = 7;
            this.CategoriaComboBox.Enter += new System.EventHandler(this.tipoComboBox_Enter);
            this.CategoriaComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CategoriaComboBox_KeyDown);
            this.CategoriaComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.tipoComboBox_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(280, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Categoria";
            // 
            // ConservacaoComboBox
            // 
            this.ConservacaoComboBox.BackColor = System.Drawing.SystemColors.Control;
            this.ConservacaoComboBox.BeforeTouchSize = new System.Drawing.Size(162, 25);
            this.ConservacaoComboBox.FlatBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.ConservacaoComboBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.ConservacaoComboBox.Location = new System.Drawing.Point(16, 160);
            this.ConservacaoComboBox.Name = "ConservacaoComboBox";
            this.ConservacaoComboBox.Size = new System.Drawing.Size(162, 25);
            this.ConservacaoComboBox.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.ConservacaoComboBox.TabIndex = 15;
            this.ConservacaoComboBox.Enter += new System.EventHandler(this.tipoComboBox_Enter);
            this.ConservacaoComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConservacaoComboBox_KeyDown);
            this.ConservacaoComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.tipoComboBox_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Conservação";
            // 
            // tipoComboBox
            // 
            this.tipoComboBox.BackColor = System.Drawing.SystemColors.Control;
            this.tipoComboBox.BeforeTouchSize = new System.Drawing.Size(112, 25);
            this.tipoComboBox.FlatBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.tipoComboBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.tipoComboBox.Location = new System.Drawing.Point(165, 26);
            this.tipoComboBox.Name = "tipoComboBox";
            this.tipoComboBox.Size = new System.Drawing.Size(112, 25);
            this.tipoComboBox.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.tipoComboBox.TabIndex = 5;
            this.tipoComboBox.Enter += new System.EventHandler(this.tipoComboBox_Enter);
            this.tipoComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tipoComboBox_KeyDown);
            this.tipoComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.tipoComboBox_Validating);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(162, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 15);
            this.label19.TabIndex = 4;
            this.label19.Text = "Tipo cessão";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(377, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Data devolução";
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
            this.dataDateTimePicker.Location = new System.Drawing.Point(380, 160);
            this.dataDateTimePicker.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.dataDateTimePicker.MinValue = new System.DateTime(((long)(0)));
            this.dataDateTimePicker.Name = "dataDateTimePicker";
            this.dataDateTimePicker.ShowCheckBox = false;
            this.dataDateTimePicker.Size = new System.Drawing.Size(111, 23);
            this.dataDateTimePicker.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.dataDateTimePicker.TabIndex = 17;
            this.dataDateTimePicker.Value = new System.DateTime(2017, 9, 1, 0, 0, 0, 0);
            this.dataDateTimePicker.Enter += new System.EventHandler(this.dataDateTimePicker_Enter);
            this.dataDateTimePicker.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataDateTimePicker_PreviewKeyDown);
            this.dataDateTimePicker.Validating += new System.ComponentModel.CancelEventHandler(this.dataDateTimePicker_Validating);
            // 
            // NomeColaboradorTextBox
            // 
            this.NomeColaboradorTextBox.AcceptsReturn = true;
            this.NomeColaboradorTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.NomeColaboradorTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.NomeColaboradorTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.NomeColaboradorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NomeColaboradorTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.NomeColaboradorTextBox.Enabled = false;
            this.NomeColaboradorTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.NomeColaboradorTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.NomeColaboradorTextBox.Location = new System.Drawing.Point(138, 117);
            this.NomeColaboradorTextBox.MaxLength = 50;
            this.NomeColaboradorTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.NomeColaboradorTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.NomeColaboradorTextBox.Name = "NomeColaboradorTextBox";
            this.NomeColaboradorTextBox.Size = new System.Drawing.Size(353, 23);
            this.NomeColaboradorTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.NomeColaboradorTextBox.TabIndex = 13;
            this.NomeColaboradorTextBox.UseBorderColorOnFocus = true;
            // 
            // PesquisaColaboradorButton
            // 
            this.PesquisaColaboradorButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.PesquisaColaboradorButton.BackColor = System.Drawing.SystemColors.Control;
            this.PesquisaColaboradorButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.PesquisaColaboradorButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.PesquisaColaboradorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PesquisaColaboradorButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.PesquisaColaboradorButton.ForeColor = System.Drawing.Color.Black;
            this.PesquisaColaboradorButton.Image = ((System.Drawing.Image)(resources.GetObject("PesquisaColaboradorButton.Image")));
            this.PesquisaColaboradorButton.IsBackStageButton = false;
            this.PesquisaColaboradorButton.Location = new System.Drawing.Point(110, 117);
            this.PesquisaColaboradorButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.PesquisaColaboradorButton.Name = "PesquisaColaboradorButton";
            this.PesquisaColaboradorButton.OverrideFormManagedColor = true;
            this.PesquisaColaboradorButton.Size = new System.Drawing.Size(23, 23);
            this.PesquisaColaboradorButton.TabIndex = 12;
            this.PesquisaColaboradorButton.TabStop = false;
            this.PesquisaColaboradorButton.Click += new System.EventHandler(this.PesquisaColaboradorButton_Click);
            // 
            // ColaboradorTextBox
            // 
            this.ColaboradorTextBox.AcceptsReturn = true;
            this.ColaboradorTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.ColaboradorTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.ColaboradorTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.ColaboradorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColaboradorTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ColaboradorTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ColaboradorTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.ColaboradorTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.ColaboradorTextBox.Location = new System.Drawing.Point(16, 117);
            this.ColaboradorTextBox.MaxLength = 5;
            this.ColaboradorTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.ColaboradorTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.ColaboradorTextBox.Name = "ColaboradorTextBox";
            this.ColaboradorTextBox.Size = new System.Drawing.Size(95, 23);
            this.ColaboradorTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.ColaboradorTextBox.TabIndex = 11;
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
            this.label1.Location = new System.Drawing.Point(13, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Colaborador";
            // 
            // pesquisaCategoriaButton
            // 
            this.pesquisaCategoriaButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.pesquisaCategoriaButton.BackColor = System.Drawing.SystemColors.Control;
            this.pesquisaCategoriaButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.pesquisaCategoriaButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.pesquisaCategoriaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pesquisaCategoriaButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.pesquisaCategoriaButton.ForeColor = System.Drawing.Color.Black;
            this.pesquisaCategoriaButton.Image = ((System.Drawing.Image)(resources.GetObject("pesquisaCategoriaButton.Image")));
            this.pesquisaCategoriaButton.IsBackStageButton = false;
            this.pesquisaCategoriaButton.Location = new System.Drawing.Point(110, 28);
            this.pesquisaCategoriaButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.pesquisaCategoriaButton.Name = "pesquisaCategoriaButton";
            this.pesquisaCategoriaButton.OverrideFormManagedColor = true;
            this.pesquisaCategoriaButton.Size = new System.Drawing.Size(23, 23);
            this.pesquisaCategoriaButton.TabIndex = 2;
            this.pesquisaCategoriaButton.TabStop = false;
            this.pesquisaCategoriaButton.Click += new System.EventHandler(this.pesquisaCategoriaButton_Click);
            // 
            // proximoButton
            // 
            this.proximoButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.proximoButton.BackColor = System.Drawing.SystemColors.Control;
            this.proximoButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.proximoButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.proximoButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.proximoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.proximoButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.proximoButton.ForeColor = System.Drawing.Color.Black;
            this.proximoButton.IsBackStageButton = false;
            this.proximoButton.Location = new System.Drawing.Point(132, 28);
            this.proximoButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.proximoButton.Name = "proximoButton";
            this.proximoButton.OverrideFormManagedColor = true;
            this.proximoButton.Size = new System.Drawing.Size(23, 23);
            this.proximoButton.TabIndex = 3;
            this.proximoButton.TabStop = false;
            this.proximoButton.Text = "+";
            this.proximoButton.Click += new System.EventHandler(this.proximoButton_Click);
            // 
            // nomeTextBox
            // 
            this.nomeTextBox.AcceptsReturn = true;
            this.nomeTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.nomeTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.nomeTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.nomeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nomeTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.nomeTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.nomeTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.nomeTextBox.Location = new System.Drawing.Point(16, 72);
            this.nomeTextBox.MaxLength = 50;
            this.nomeTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.nomeTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.nomeTextBox.Name = "nomeTextBox";
            this.nomeTextBox.Size = new System.Drawing.Size(475, 23);
            this.nomeTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.nomeTextBox.TabIndex = 9;
            this.nomeTextBox.UseBorderColorOnFocus = true;
            this.nomeTextBox.Enter += new System.EventHandler(this.nomeTextBox_Enter);
            this.nomeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nomeTextBox_KeyDown);
            this.nomeTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nomeTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Nome";
            // 
            // codigoTextBox
            // 
            this.codigoTextBox.AcceptsReturn = true;
            this.codigoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.codigoTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.codigoTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.codigoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codigoTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.codigoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.codigoTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.codigoTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.codigoTextBox.Location = new System.Drawing.Point(16, 28);
            this.codigoTextBox.MaxLength = 5;
            this.codigoTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.codigoTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.codigoTextBox.Name = "codigoTextBox";
            this.codigoTextBox.Size = new System.Drawing.Size(95, 23);
            this.codigoTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.codigoTextBox.TabIndex = 1;
            this.codigoTextBox.UseBorderColorOnFocus = true;
            this.codigoTextBox.TextChanged += new System.EventHandler(this.codigoTextBox_TextChanged);
            this.codigoTextBox.Enter += new System.EventHandler(this.codigoTextBox_Enter);
            this.codigoTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.codigoTextBox_KeyDown);
            this.codigoTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.codigoTextBox_KeyPress);
            this.codigoTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.codigoTextBox_Validating);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(13, 10);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(46, 15);
            this.label26.TabIndex = 0;
            this.label26.Text = "Código";
            // 
            // Livros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 298);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tituloPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Livros";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Livros";
            this.Shown += new System.EventHandler(this.Livros_Shown);
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategoriaComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConservacaoComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tipoComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataDateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomeColaboradorTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColaboradorTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomeTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codigoTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        public System.Windows.Forms.Label mensagemSistemaLabel;
        public System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv excluirButton;
        private Syncfusion.Windows.Forms.ButtonAdv limparButton;
        private Syncfusion.Windows.Forms.ButtonAdv gravarButton;
        private System.Windows.Forms.Panel panel1;
        private Syncfusion.Windows.Forms.ButtonAdv pesquisaCategoriaButton;
        private Syncfusion.Windows.Forms.ButtonAdv proximoButton;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt nomeTextBox;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt codigoTextBox;
        private System.Windows.Forms.Label label26;
        private Syncfusion.Windows.Forms.ButtonAdv PesquisaColaboradorButton;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt ColaboradorTextBox;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt NomeColaboradorTextBox;
        private System.Windows.Forms.Label label5;
        private Syncfusion.Windows.Forms.Tools.DateTimePickerAdv dataDateTimePicker;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv tipoComboBox;
        private System.Windows.Forms.Label label19;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv ConservacaoComboBox;
        private System.Windows.Forms.Label label3;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv CategoriaComboBox;
        private System.Windows.Forms.Label label4;
    }
}