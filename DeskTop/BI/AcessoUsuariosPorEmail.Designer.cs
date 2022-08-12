namespace Suportte.BI
{
    partial class AcessoUsuariosPorEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AcessoUsuariosPorEmail));
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.camposPanel = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.IncluirButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.PesquisaResponsavelButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.nomeResponsavelTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.UsuarioTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label18 = new System.Windows.Forms.Label();
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.excluirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pesquisaButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.EmailTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.limparButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.gravarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.camposPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nomeResponsavelTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsuarioTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmailTextBox)).BeginInit();
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
            this.tituloPanel.Size = new System.Drawing.Size(507, 40);
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
            this.tituloLabel.Size = new System.Drawing.Size(400, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Autoriza Usuários ao E-mail de Acesso Power BI";
            this.tituloLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(467, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // camposPanel
            // 
            this.camposPanel.Controls.Add(this.label9);
            this.camposPanel.Controls.Add(this.IncluirButton);
            this.camposPanel.Controls.Add(this.PesquisaResponsavelButton);
            this.camposPanel.Controls.Add(this.nomeResponsavelTextBox);
            this.camposPanel.Controls.Add(this.UsuarioTextBox);
            this.camposPanel.Controls.Add(this.label18);
            this.camposPanel.Controls.Add(this.gridGroupingControl1);
            this.camposPanel.Controls.Add(this.pesquisaButton);
            this.camposPanel.Controls.Add(this.EmailTextBox);
            this.camposPanel.Controls.Add(this.label2);
            this.camposPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camposPanel.Location = new System.Drawing.Point(0, 40);
            this.camposPanel.Name = "camposPanel";
            this.camposPanel.Size = new System.Drawing.Size(507, 452);
            this.camposPanel.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Wingdings 3", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label9.Location = new System.Drawing.Point(16, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 12);
            this.label9.TabIndex = 24;
            this.label9.Text = "i";
            // 
            // IncluirButton
            // 
            this.IncluirButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.IncluirButton.BackColor = System.Drawing.SystemColors.Control;
            this.IncluirButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.IncluirButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.IncluirButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.IncluirButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IncluirButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.IncluirButton.ForeColor = System.Drawing.Color.Black;
            this.IncluirButton.IsBackStageButton = false;
            this.IncluirButton.Location = new System.Drawing.Point(471, 66);
            this.IncluirButton.MetroColor = System.Drawing.Color.DimGray;
            this.IncluirButton.Name = "IncluirButton";
            this.IncluirButton.OverrideFormManagedColor = true;
            this.IncluirButton.Size = new System.Drawing.Size(23, 23);
            this.IncluirButton.TabIndex = 10;
            this.IncluirButton.TabStop = false;
            this.IncluirButton.Text = "+";
            this.IncluirButton.ThemeName = "Metro";
            this.IncluirButton.Click += new System.EventHandler(this.IncluirButton_Click);
            // 
            // PesquisaResponsavelButton
            // 
            this.PesquisaResponsavelButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.PesquisaResponsavelButton.BackColor = System.Drawing.SystemColors.Control;
            this.PesquisaResponsavelButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.PesquisaResponsavelButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.PesquisaResponsavelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PesquisaResponsavelButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.PesquisaResponsavelButton.ForeColor = System.Drawing.Color.Black;
            this.PesquisaResponsavelButton.Image = ((System.Drawing.Image)(resources.GetObject("PesquisaResponsavelButton.Image")));
            this.PesquisaResponsavelButton.IsBackStageButton = false;
            this.PesquisaResponsavelButton.Location = new System.Drawing.Point(121, 66);
            this.PesquisaResponsavelButton.MetroColor = System.Drawing.Color.DimGray;
            this.PesquisaResponsavelButton.Name = "PesquisaResponsavelButton";
            this.PesquisaResponsavelButton.OverrideFormManagedColor = true;
            this.PesquisaResponsavelButton.Size = new System.Drawing.Size(23, 23);
            this.PesquisaResponsavelButton.TabIndex = 5;
            this.PesquisaResponsavelButton.TabStop = false;
            this.PesquisaResponsavelButton.ThemeName = "Metro";
            this.PesquisaResponsavelButton.Click += new System.EventHandler(this.PesquisaResponsavelButton_Click);
            // 
            // nomeResponsavelTextBox
            // 
            this.nomeResponsavelTextBox.AcceptsReturn = true;
            this.nomeResponsavelTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.nomeResponsavelTextBox.BeforeTouchSize = new System.Drawing.Size(457, 23);
            this.nomeResponsavelTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.nomeResponsavelTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nomeResponsavelTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.nomeResponsavelTextBox.Enabled = false;
            this.nomeResponsavelTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.nomeResponsavelTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.nomeResponsavelTextBox.Location = new System.Drawing.Point(147, 66);
            this.nomeResponsavelTextBox.MaxLength = 50;
            this.nomeResponsavelTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.nomeResponsavelTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.nomeResponsavelTextBox.Name = "nomeResponsavelTextBox";
            this.nomeResponsavelTextBox.Size = new System.Drawing.Size(326, 23);
            this.nomeResponsavelTextBox.TabIndex = 6;
            this.nomeResponsavelTextBox.ThemeName = "Default";
            this.nomeResponsavelTextBox.UseBorderColorOnFocus = true;
            // 
            // UsuarioTextBox
            // 
            this.UsuarioTextBox.AcceptsReturn = true;
            this.UsuarioTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.UsuarioTextBox.BeforeTouchSize = new System.Drawing.Size(457, 23);
            this.UsuarioTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.UsuarioTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UsuarioTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.UsuarioTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.UsuarioTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.UsuarioTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.UsuarioTextBox.Location = new System.Drawing.Point(16, 66);
            this.UsuarioTextBox.MaxLength = 30;
            this.UsuarioTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.UsuarioTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.UsuarioTextBox.Name = "UsuarioTextBox";
            this.UsuarioTextBox.Size = new System.Drawing.Size(106, 23);
            this.UsuarioTextBox.TabIndex = 4;
            this.UsuarioTextBox.ThemeName = "Default";
            this.UsuarioTextBox.UseBorderColorOnFocus = true;
            this.UsuarioTextBox.TextChanged += new System.EventHandler(this.UsuarioTextBox_TextChanged);
            this.UsuarioTextBox.Enter += new System.EventHandler(this.EmailTextBox_Enter);
            this.UsuarioTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UsuarioTextBox_KeyDown);
            this.UsuarioTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.UsuarioTextBox_Validating);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(24, 48);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(47, 15);
            this.label18.TabIndex = 3;
            this.label18.Text = "Usuário";
            // 
            // gridGroupingControl1
            // 
            this.gridGroupingControl1.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.SetCurrent;
            this.gridGroupingControl1.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(94)))), ((int)(((byte)(171)))), ((int)(((byte)(222)))));
            this.gridGroupingControl1.BackColor = System.Drawing.SystemColors.Control;
            this.gridGroupingControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridGroupingControl1.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            this.gridGroupingControl1.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
            this.gridGroupingControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
            this.gridGroupingControl1.Location = new System.Drawing.Point(16, 99);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl1.Size = new System.Drawing.Size(478, 347);
            this.gridGroupingControl1.TabIndex = 7;
            this.gridGroupingControl1.TableDescriptor.AllowEdit = false;
            this.gridGroupingControl1.TableDescriptor.AllowNew = false;
            this.gridGroupingControl1.TableDescriptor.AllowRemove = false;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor1.Appearance.ColumnHeaderCell.AutoSize = true;
            gridColumnDescriptor1.Appearance.ColumnHeaderCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor1.HeaderText = "Usuário";
            gridColumnDescriptor1.MappingName = "Nome";
            gridColumnDescriptor1.ReadOnly = true;
            this.gridGroupingControl1.TableDescriptor.Columns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor[] {
            gridColumnDescriptor1});
            this.gridGroupingControl1.TableDescriptor.TableOptions.CaptionRowHeight = 29;
            this.gridGroupingControl1.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 25;
            this.gridGroupingControl1.TableDescriptor.TableOptions.RecordRowHeight = 25;
            this.gridGroupingControl1.TableOptions.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.gridGroupingControl1.TableOptions.SelectionTextColor = System.Drawing.SystemColors.HighlightText;
            this.gridGroupingControl1.Text = "gridGroupingControl1";
            this.gridGroupingControl1.TopLevelGroupOptions.ShowCaption = false;
            this.gridGroupingControl1.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl1.VersionInfo = "16.2460.0.41";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excluirToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 26);
            // 
            // excluirToolStripMenuItem
            // 
            this.excluirToolStripMenuItem.Name = "excluirToolStripMenuItem";
            this.excluirToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.excluirToolStripMenuItem.Text = "Excluir";
            this.excluirToolStripMenuItem.Click += new System.EventHandler(this.excluirToolStripMenuItem_Click);
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
            this.pesquisaButton.Location = new System.Drawing.Point(471, 22);
            this.pesquisaButton.MetroColor = System.Drawing.Color.DimGray;
            this.pesquisaButton.Name = "pesquisaButton";
            this.pesquisaButton.OverrideFormManagedColor = true;
            this.pesquisaButton.Size = new System.Drawing.Size(23, 23);
            this.pesquisaButton.TabIndex = 2;
            this.pesquisaButton.TabStop = false;
            this.pesquisaButton.ThemeName = "Metro";
            this.pesquisaButton.Click += new System.EventHandler(this.pesquisaButton_Click);
            // 
            // EmailTextBox
            // 
            this.EmailTextBox.AcceptsReturn = true;
            this.EmailTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.EmailTextBox.BeforeTouchSize = new System.Drawing.Size(457, 23);
            this.EmailTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.EmailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmailTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.EmailTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.EmailTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.EmailTextBox.Location = new System.Drawing.Point(16, 22);
            this.EmailTextBox.MaxLength = 50;
            this.EmailTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.EmailTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.EmailTextBox.Name = "EmailTextBox";
            this.EmailTextBox.Size = new System.Drawing.Size(457, 23);
            this.EmailTextBox.TabIndex = 1;
            this.EmailTextBox.ThemeName = "Default";
            this.EmailTextBox.UseBorderColorOnFocus = true;
            this.EmailTextBox.TextChanged += new System.EventHandler(this.EmailTextBox_TextChanged);
            this.EmailTextBox.Enter += new System.EventHandler(this.EmailTextBox_Enter);
            this.EmailTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EmailTextBox_KeyDown);
            this.EmailTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.EmailTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "E-mail";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Controls.Add(this.limparButton);
            this.panel3.Controls.Add(this.gravarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 492);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(507, 62);
            this.panel3.TabIndex = 1;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(507, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 19;
            this.pictureBox44.TabStop = false;
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
            this.limparButton.Location = new System.Drawing.Point(282, 14);
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
            this.limparButton.Validating += new System.ComponentModel.CancelEventHandler(this.gravarButton_Validating);
            // 
            // gravarButton
            // 
            this.gravarButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.gravarButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.gravarButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.gravarButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.gravarButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.gravarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gravarButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.gravarButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.gravarButton.IsBackStageButton = false;
            this.gravarButton.Location = new System.Drawing.Point(122, 14);
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
            // AcessoUsuariosPorEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 554);
            this.ControlBox = false;
            this.Controls.Add(this.camposPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tituloPanel);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AcessoUsuariosPorEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autoriza Usuários ao E-mail de Acesso Pover BI";
            this.Shown += new System.EventHandler(this.AcessoUsuariosPorEmail_Shown);
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.camposPanel.ResumeLayout(false);
            this.camposPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nomeResponsavelTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsuarioTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EmailTextBox)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel camposPanel;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
        private Syncfusion.Windows.Forms.ButtonAdv pesquisaButton;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt EmailTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv limparButton;
        private Syncfusion.Windows.Forms.ButtonAdv gravarButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem excluirToolStripMenuItem;
        private Syncfusion.Windows.Forms.ButtonAdv PesquisaResponsavelButton;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt nomeResponsavelTextBox;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt UsuarioTextBox;
        private System.Windows.Forms.Label label18;
        private Syncfusion.Windows.Forms.ButtonAdv IncluirButton;
        private System.Windows.Forms.Label label9;
    }
}