namespace Suportte.Cadastros
{
    partial class Campeonato
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Campeonato));
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.camposPanel = new System.Windows.Forms.Panel();
            this.MaximoTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.MinimoTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.pesquisaButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.proximoButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.panel4 = new System.Windows.Forms.Panel();
            this.PinballCheckBox = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.ArmsCheckBox = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.marioKartBattleCheckBox = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.ativoCheckBox = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.MarioKartGrandPrixCheckBox = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.tituloOpcoesPanel = new System.Windows.Forms.Panel();
            this.tituloOpcoesLabel = new System.Windows.Forms.Label();
            this.nomeTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.codigoTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label26 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.excluirButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.limparButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.gravarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.camposPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaximoTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimoTextBox)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PinballCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArmsCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marioKartBattleCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ativoCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarioKartGrandPrixCheckBox)).BeginInit();
            this.tituloOpcoesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nomeTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codigoTextBox)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).BeginInit();
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
            this.tituloPanel.Size = new System.Drawing.Size(502, 40);
            this.tituloPanel.TabIndex = 2;
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
            this.tituloLabel.Size = new System.Drawing.Size(69, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Torneio";
            this.tituloLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(462, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // camposPanel
            // 
            this.camposPanel.Controls.Add(this.MaximoTextBox);
            this.camposPanel.Controls.Add(this.label1);
            this.camposPanel.Controls.Add(this.MinimoTextBox);
            this.camposPanel.Controls.Add(this.label8);
            this.camposPanel.Controls.Add(this.pesquisaButton);
            this.camposPanel.Controls.Add(this.proximoButton);
            this.camposPanel.Controls.Add(this.panel4);
            this.camposPanel.Controls.Add(this.nomeTextBox);
            this.camposPanel.Controls.Add(this.label2);
            this.camposPanel.Controls.Add(this.codigoTextBox);
            this.camposPanel.Controls.Add(this.label26);
            this.camposPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camposPanel.Location = new System.Drawing.Point(0, 40);
            this.camposPanel.Name = "camposPanel";
            this.camposPanel.Size = new System.Drawing.Size(502, 257);
            this.camposPanel.TabIndex = 0;
            // 
            // MaximoTextBox
            // 
            this.MaximoTextBox.AcceptsReturn = true;
            this.MaximoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.MaximoTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.MaximoTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.MaximoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaximoTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.MaximoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MaximoTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.MaximoTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.MaximoTextBox.Location = new System.Drawing.Point(97, 223);
            this.MaximoTextBox.MaxLength = 2;
            this.MaximoTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.MaximoTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.MaximoTextBox.Name = "MaximoTextBox";
            this.MaximoTextBox.Size = new System.Drawing.Size(58, 23);
            this.MaximoTextBox.TabIndex = 10;
            this.MaximoTextBox.ThemeName = "Default";
            this.MaximoTextBox.UseBorderColorOnFocus = true;
            this.MaximoTextBox.Enter += new System.EventHandler(this.codigoTextBox_Enter);
            this.MaximoTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaximoTextBox_KeyDown);
            this.MaximoTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nomeTextBox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Máximo";
            // 
            // MinimoTextBox
            // 
            this.MinimoTextBox.AcceptsReturn = true;
            this.MinimoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.MinimoTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.MinimoTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.MinimoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MinimoTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.MinimoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MinimoTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.MinimoTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.MinimoTextBox.Location = new System.Drawing.Point(16, 223);
            this.MinimoTextBox.MaxLength = 2;
            this.MinimoTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.MinimoTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.MinimoTextBox.Name = "MinimoTextBox";
            this.MinimoTextBox.Size = new System.Drawing.Size(58, 23);
            this.MinimoTextBox.TabIndex = 8;
            this.MinimoTextBox.ThemeName = "Default";
            this.MinimoTextBox.UseBorderColorOnFocus = true;
            this.MinimoTextBox.Enter += new System.EventHandler(this.codigoTextBox_Enter);
            this.MinimoTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MinimoTextBox_KeyDown);
            this.MinimoTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nomeTextBox_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Minimo";
            // 
            // pesquisaButton
            // 
            this.pesquisaButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.pesquisaButton.BackColor = System.Drawing.SystemColors.Control;
            this.pesquisaButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.pesquisaButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.pesquisaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pesquisaButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.pesquisaButton.ForeColor = System.Drawing.Color.Black;
            this.pesquisaButton.Image = ((System.Drawing.Image)(resources.GetObject("pesquisaButton.Image")));
            this.pesquisaButton.IsBackStageButton = false;
            this.pesquisaButton.Location = new System.Drawing.Point(110, 28);
            this.pesquisaButton.MetroColor = System.Drawing.Color.DimGray;
            this.pesquisaButton.Name = "pesquisaButton";
            this.pesquisaButton.OverrideFormManagedColor = true;
            this.pesquisaButton.Size = new System.Drawing.Size(23, 23);
            this.pesquisaButton.TabIndex = 2;
            this.pesquisaButton.TabStop = false;
            this.pesquisaButton.ThemeName = "Metro";
            this.pesquisaButton.Click += new System.EventHandler(this.pesquisaButton_Click);
            // 
            // proximoButton
            // 
            this.proximoButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.proximoButton.BackColor = System.Drawing.SystemColors.Control;
            this.proximoButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.proximoButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.proximoButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.proximoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.proximoButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.proximoButton.ForeColor = System.Drawing.Color.Black;
            this.proximoButton.IsBackStageButton = false;
            this.proximoButton.Location = new System.Drawing.Point(132, 28);
            this.proximoButton.MetroColor = System.Drawing.Color.DimGray;
            this.proximoButton.Name = "proximoButton";
            this.proximoButton.OverrideFormManagedColor = true;
            this.proximoButton.Size = new System.Drawing.Size(23, 23);
            this.proximoButton.TabIndex = 3;
            this.proximoButton.TabStop = false;
            this.proximoButton.Text = "+";
            this.proximoButton.ThemeName = "Metro";
            this.proximoButton.Click += new System.EventHandler(this.proximoButton_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.PinballCheckBox);
            this.panel4.Controls.Add(this.ArmsCheckBox);
            this.panel4.Controls.Add(this.marioKartBattleCheckBox);
            this.panel4.Controls.Add(this.ativoCheckBox);
            this.panel4.Controls.Add(this.MarioKartGrandPrixCheckBox);
            this.panel4.Controls.Add(this.tituloOpcoesPanel);
            this.panel4.Location = new System.Drawing.Point(16, 104);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(475, 92);
            this.panel4.TabIndex = 6;
            // 
            // PinballCheckBox
            // 
            this.PinballCheckBox.BeforeTouchSize = new System.Drawing.Size(157, 21);
            this.PinballCheckBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.PinballCheckBox.Location = new System.Drawing.Point(313, 35);
            this.PinballCheckBox.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.PinballCheckBox.Name = "PinballCheckBox";
            this.PinballCheckBox.Size = new System.Drawing.Size(157, 21);
            this.PinballCheckBox.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.PinballCheckBox.TabIndex = 4;
            this.PinballCheckBox.Text = "Pinball";
            this.PinballCheckBox.ThemeName = "Metro";
            this.PinballCheckBox.ThemesEnabled = false;
            this.PinballCheckBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PinballCheckBox_KeyDown);
            // 
            // ArmsCheckBox
            // 
            this.ArmsCheckBox.BeforeTouchSize = new System.Drawing.Size(142, 21);
            this.ArmsCheckBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.ArmsCheckBox.Location = new System.Drawing.Point(165, 62);
            this.ArmsCheckBox.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.ArmsCheckBox.Name = "ArmsCheckBox";
            this.ArmsCheckBox.Size = new System.Drawing.Size(142, 21);
            this.ArmsCheckBox.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.ArmsCheckBox.TabIndex = 3;
            this.ArmsCheckBox.Text = "Arms";
            this.ArmsCheckBox.ThemeName = "Metro";
            this.ArmsCheckBox.ThemesEnabled = false;
            this.ArmsCheckBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ArmsCheckBox_KeyDown);
            // 
            // marioKartBattleCheckBox
            // 
            this.marioKartBattleCheckBox.BeforeTouchSize = new System.Drawing.Size(142, 21);
            this.marioKartBattleCheckBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.marioKartBattleCheckBox.Location = new System.Drawing.Point(165, 35);
            this.marioKartBattleCheckBox.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.marioKartBattleCheckBox.Name = "marioKartBattleCheckBox";
            this.marioKartBattleCheckBox.Size = new System.Drawing.Size(142, 21);
            this.marioKartBattleCheckBox.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.marioKartBattleCheckBox.TabIndex = 2;
            this.marioKartBattleCheckBox.Text = "Mario kart - Battle";
            this.marioKartBattleCheckBox.ThemeName = "Metro";
            this.marioKartBattleCheckBox.ThemesEnabled = false;
            this.marioKartBattleCheckBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.marioKartBattleCheckBox_KeyDown);
            // 
            // ativoCheckBox
            // 
            this.ativoCheckBox.BeforeTouchSize = new System.Drawing.Size(114, 21);
            this.ativoCheckBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.ativoCheckBox.Checked = true;
            this.ativoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ativoCheckBox.Location = new System.Drawing.Point(9, 35);
            this.ativoCheckBox.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.ativoCheckBox.Name = "ativoCheckBox";
            this.ativoCheckBox.Size = new System.Drawing.Size(114, 21);
            this.ativoCheckBox.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.ativoCheckBox.TabIndex = 0;
            this.ativoCheckBox.Text = "Ativo";
            this.ativoCheckBox.ThemeName = "Metro";
            this.ativoCheckBox.ThemesEnabled = false;
            this.ativoCheckBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ativoCheckBox_KeyDown);
            // 
            // MarioKartGrandPrixCheckBox
            // 
            this.MarioKartGrandPrixCheckBox.BeforeTouchSize = new System.Drawing.Size(142, 21);
            this.MarioKartGrandPrixCheckBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.MarioKartGrandPrixCheckBox.Location = new System.Drawing.Point(9, 62);
            this.MarioKartGrandPrixCheckBox.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.MarioKartGrandPrixCheckBox.Name = "MarioKartGrandPrixCheckBox";
            this.MarioKartGrandPrixCheckBox.Size = new System.Drawing.Size(142, 21);
            this.MarioKartGrandPrixCheckBox.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.MarioKartGrandPrixCheckBox.TabIndex = 1;
            this.MarioKartGrandPrixCheckBox.Text = "Mario kart - GrandPrix";
            this.MarioKartGrandPrixCheckBox.ThemeName = "Metro";
            this.MarioKartGrandPrixCheckBox.ThemesEnabled = false;
            this.MarioKartGrandPrixCheckBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MarioKartGrandPrixCheckBox_KeyDown);
            // 
            // tituloOpcoesPanel
            // 
            this.tituloOpcoesPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.tituloOpcoesPanel.Controls.Add(this.tituloOpcoesLabel);
            this.tituloOpcoesPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloOpcoesPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloOpcoesPanel.Name = "tituloOpcoesPanel";
            this.tituloOpcoesPanel.Size = new System.Drawing.Size(473, 26);
            this.tituloOpcoesPanel.TabIndex = 5;
            // 
            // tituloOpcoesLabel
            // 
            this.tituloOpcoesLabel.AutoSize = true;
            this.tituloOpcoesLabel.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.tituloOpcoesLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tituloOpcoesLabel.Location = new System.Drawing.Point(12, 6);
            this.tituloOpcoesLabel.Name = "tituloOpcoesLabel";
            this.tituloOpcoesLabel.Size = new System.Drawing.Size(58, 17);
            this.tituloOpcoesLabel.TabIndex = 13;
            this.tituloOpcoesLabel.Text = "Opções";
            // 
            // nomeTextBox
            // 
            this.nomeTextBox.AcceptsReturn = true;
            this.nomeTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.nomeTextBox.BeforeTouchSize = new System.Drawing.Size(95, 23);
            this.nomeTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.nomeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nomeTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.nomeTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.nomeTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.nomeTextBox.Location = new System.Drawing.Point(16, 75);
            this.nomeTextBox.MaxLength = 50;
            this.nomeTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.nomeTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.nomeTextBox.Name = "nomeTextBox";
            this.nomeTextBox.Size = new System.Drawing.Size(475, 23);
            this.nomeTextBox.TabIndex = 5;
            this.nomeTextBox.ThemeName = "Default";
            this.nomeTextBox.UseBorderColorOnFocus = true;
            this.nomeTextBox.Enter += new System.EventHandler(this.codigoTextBox_Enter);
            this.nomeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nomeTextBox_KeyDown);
            this.nomeTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nomeTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nome";
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
            this.codigoTextBox.Location = new System.Drawing.Point(16, 28);
            this.codigoTextBox.MaxLength = 5;
            this.codigoTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.codigoTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.codigoTextBox.Name = "codigoTextBox";
            this.codigoTextBox.Size = new System.Drawing.Size(95, 23);
            this.codigoTextBox.TabIndex = 1;
            this.codigoTextBox.ThemeName = "Default";
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
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Controls.Add(this.excluirButton);
            this.panel3.Controls.Add(this.limparButton);
            this.panel3.Controls.Add(this.gravarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 297);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(502, 62);
            this.panel3.TabIndex = 1;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(502, 4);
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
            this.excluirButton.Location = new System.Drawing.Point(358, 14);
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
            this.limparButton.Location = new System.Drawing.Point(200, 14);
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
            this.gravarButton.Location = new System.Drawing.Point(42, 14);
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
            // Campeonato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 359);
            this.ControlBox = false;
            this.Controls.Add(this.camposPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tituloPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Campeonato";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Campeonato";
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.camposPanel.ResumeLayout(false);
            this.camposPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaximoTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimoTextBox)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PinballCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArmsCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marioKartBattleCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ativoCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarioKartGrandPrixCheckBox)).EndInit();
            this.tituloOpcoesPanel.ResumeLayout(false);
            this.tituloOpcoesPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nomeTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codigoTextBox)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel camposPanel;
        private Syncfusion.Windows.Forms.ButtonAdv pesquisaButton;
        private Syncfusion.Windows.Forms.ButtonAdv proximoButton;
        private System.Windows.Forms.Panel panel4;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv ativoCheckBox;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv MarioKartGrandPrixCheckBox;
        private System.Windows.Forms.Panel tituloOpcoesPanel;
        private System.Windows.Forms.Label tituloOpcoesLabel;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt nomeTextBox;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt codigoTextBox;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv excluirButton;
        private Syncfusion.Windows.Forms.ButtonAdv limparButton;
        private Syncfusion.Windows.Forms.ButtonAdv gravarButton;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv PinballCheckBox;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv ArmsCheckBox;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv marioKartBattleCheckBox;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt MaximoTextBox;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt MinimoTextBox;
        private System.Windows.Forms.Label label8;
    }
}