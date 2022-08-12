namespace Suportte.GestaoEstrategica
{
    partial class VisoesBI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisoesBI));
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.camposPanel = new System.Windows.Forms.Panel();
            this.ExclusivoCheckBox = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.panel6 = new System.Windows.Forms.Panel();
            this.gridGroupingControl = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.alterarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excluirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RubricaTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.pesquisaCategoriaButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.proximoButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.codigoTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.ativoCheckBox = new Syncfusion.Windows.Forms.Tools.CheckBoxAdv();
            this.nomeTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.excluirButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.limparButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.gravarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.camposPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExclusivoCheckBox)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RubricaTextBox)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.codigoTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ativoCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomeTextBox)).BeginInit();
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
            this.tituloPanel.Size = new System.Drawing.Size(506, 40);
            this.tituloPanel.TabIndex = 2;
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.tituloLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tituloLabel.Location = new System.Drawing.Point(12, 10);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(82, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Visões BI";
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(466, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // camposPanel
            // 
            this.camposPanel.Controls.Add(this.ExclusivoCheckBox);
            this.camposPanel.Controls.Add(this.panel6);
            this.camposPanel.Controls.Add(this.pesquisaCategoriaButton);
            this.camposPanel.Controls.Add(this.proximoButton);
            this.camposPanel.Controls.Add(this.codigoTextBox);
            this.camposPanel.Controls.Add(this.label3);
            this.camposPanel.Controls.Add(this.ativoCheckBox);
            this.camposPanel.Controls.Add(this.nomeTextBox);
            this.camposPanel.Controls.Add(this.label2);
            this.camposPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camposPanel.Location = new System.Drawing.Point(0, 40);
            this.camposPanel.Name = "camposPanel";
            this.camposPanel.Size = new System.Drawing.Size(506, 452);
            this.camposPanel.TabIndex = 0;
            // 
            // ExclusivoCheckBox
            // 
            this.ExclusivoCheckBox.BeforeTouchSize = new System.Drawing.Size(179, 21);
            this.ExclusivoCheckBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.ExclusivoCheckBox.Checked = true;
            this.ExclusivoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ExclusivoCheckBox.Location = new System.Drawing.Point(283, 38);
            this.ExclusivoCheckBox.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.ExclusivoCheckBox.Name = "ExclusivoCheckBox";
            this.ExclusivoCheckBox.Size = new System.Drawing.Size(179, 21);
            this.ExclusivoCheckBox.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.ExclusivoCheckBox.TabIndex = 8;
            this.ExclusivoCheckBox.Text = "Exclusivo de uma conta";
            this.ExclusivoCheckBox.ThemeName = "Metro";
            this.ExclusivoCheckBox.ThemesEnabled = false;
            this.ExclusivoCheckBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExclusivoCheckBox_KeyDown);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.gridGroupingControl);
            this.panel6.Controls.Add(this.RubricaTextBox);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Location = new System.Drawing.Point(16, 103);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(475, 343);
            this.panel6.TabIndex = 7;
            // 
            // gridGroupingControl
            // 
            this.gridGroupingControl.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.SetCurrent;
            this.gridGroupingControl.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(94)))), ((int)(((byte)(171)))), ((int)(((byte)(222)))));
            this.gridGroupingControl.BackColor = System.Drawing.SystemColors.Control;
            this.gridGroupingControl.ChildGroupOptions.ShowCaption = false;
            this.gridGroupingControl.ContextMenuStrip = this.contextMenuStrip1;
            this.gridGroupingControl.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            this.gridGroupingControl.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
            this.gridGroupingControl.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
            this.gridGroupingControl.Location = new System.Drawing.Point(11, 77);
            this.gridGroupingControl.Name = "gridGroupingControl";
            this.gridGroupingControl.ShowColumnHeaders = false;
            this.gridGroupingControl.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl.ShowNavigationBar = true;
            this.gridGroupingControl.ShowRowHeaders = false;
            this.gridGroupingControl.Size = new System.Drawing.Size(451, 261);
            this.gridGroupingControl.TabIndex = 2;
            this.gridGroupingControl.TableDescriptor.AllowNew = false;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor1.Appearance.ColumnHeaderCell.AutoSize = true;
            gridColumnDescriptor1.Appearance.ColumnHeaderCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor1.MappingName = "Descricao";
            this.gridGroupingControl.TableDescriptor.Columns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor[] {
            gridColumnDescriptor1});
            this.gridGroupingControl.TableDescriptor.TableOptions.CaptionRowHeight = 29;
            this.gridGroupingControl.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 25;
            this.gridGroupingControl.TableDescriptor.TableOptions.RecordRowHeight = 25;
            this.gridGroupingControl.TableOptions.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.gridGroupingControl.TableOptions.SelectionTextColor = System.Drawing.SystemColors.HighlightText;
            this.gridGroupingControl.Text = "gridGroupingControl1";
            this.gridGroupingControl.TopLevelGroupOptions.ShowCaption = false;
            this.gridGroupingControl.TopLevelGroupOptions.ShowColumnHeaders = false;
            this.gridGroupingControl.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl.VersionInfo = "16.2460.0.41";
            this.gridGroupingControl.QueryCellStyleInfo += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableCellStyleInfoEventHandler(this.gridGroupingControl_QueryCellStyleInfo);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alterarToolStripMenuItem,
            this.excluirToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(110, 48);
            // 
            // alterarToolStripMenuItem
            // 
            this.alterarToolStripMenuItem.Name = "alterarToolStripMenuItem";
            this.alterarToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.alterarToolStripMenuItem.Text = "Alterar";
            this.alterarToolStripMenuItem.Click += new System.EventHandler(this.alterarToolStripMenuItem_Click);
            // 
            // excluirToolStripMenuItem
            // 
            this.excluirToolStripMenuItem.Name = "excluirToolStripMenuItem";
            this.excluirToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.excluirToolStripMenuItem.Text = "Excluir";
            this.excluirToolStripMenuItem.Click += new System.EventHandler(this.excluirToolStripMenuItem_Click);
            // 
            // RubricaTextBox
            // 
            this.RubricaTextBox.AcceptsReturn = true;
            this.RubricaTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.RubricaTextBox.BeforeTouchSize = new System.Drawing.Size(475, 23);
            this.RubricaTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.RubricaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RubricaTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.RubricaTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.RubricaTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.RubricaTextBox.Location = new System.Drawing.Point(11, 48);
            this.RubricaTextBox.MaxLength = 1000;
            this.RubricaTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.RubricaTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.RubricaTextBox.Name = "RubricaTextBox";
            this.RubricaTextBox.Size = new System.Drawing.Size(451, 23);
            this.RubricaTextBox.TabIndex = 1;
            this.RubricaTextBox.ThemeName = "Default";
            this.RubricaTextBox.UseBorderColorOnFocus = true;
            this.RubricaTextBox.Enter += new System.EventHandler(this.nomeTextBox_Enter);
            this.RubricaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RubricaTextBox_KeyDown);
            this.RubricaTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.RubricaTextBox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome da rubrica/Cálculo";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.panel7.Controls.Add(this.label15);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(473, 26);
            this.panel7.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.label15.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label15.Location = new System.Drawing.Point(12, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 17);
            this.label15.TabIndex = 0;
            this.label15.Text = "Rúbricas";
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
            this.pesquisaCategoriaButton.Location = new System.Drawing.Point(120, 27);
            this.pesquisaCategoriaButton.MetroColor = System.Drawing.Color.DimGray;
            this.pesquisaCategoriaButton.Name = "pesquisaCategoriaButton";
            this.pesquisaCategoriaButton.OverrideFormManagedColor = true;
            this.pesquisaCategoriaButton.Size = new System.Drawing.Size(23, 23);
            this.pesquisaCategoriaButton.TabIndex = 2;
            this.pesquisaCategoriaButton.TabStop = false;
            this.pesquisaCategoriaButton.ThemeName = "Metro";
            this.pesquisaCategoriaButton.Click += new System.EventHandler(this.pesquisaCategoriaButton_Click);
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
            this.proximoButton.Location = new System.Drawing.Point(142, 27);
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
            // codigoTextBox
            // 
            this.codigoTextBox.AcceptsReturn = true;
            this.codigoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.codigoTextBox.BeforeTouchSize = new System.Drawing.Size(475, 23);
            this.codigoTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.codigoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codigoTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.codigoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.codigoTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.codigoTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.codigoTextBox.Location = new System.Drawing.Point(16, 27);
            this.codigoTextBox.MaxLength = 5;
            this.codigoTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.codigoTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.codigoTextBox.Name = "codigoTextBox";
            this.codigoTextBox.Size = new System.Drawing.Size(106, 23);
            this.codigoTextBox.TabIndex = 1;
            this.codigoTextBox.ThemeName = "Default";
            this.codigoTextBox.UseBorderColorOnFocus = true;
            this.codigoTextBox.TextChanged += new System.EventHandler(this.codigoTextBox_TextChanged);
            this.codigoTextBox.Enter += new System.EventHandler(this.codigoTextBox_Enter);
            this.codigoTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.codigoTextBox_KeyDown);
            this.codigoTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.codigoTextBox_KeyPress);
            this.codigoTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.codigoTextBox_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Código";
            // 
            // ativoCheckBox
            // 
            this.ativoCheckBox.BeforeTouchSize = new System.Drawing.Size(61, 21);
            this.ativoCheckBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.ativoCheckBox.Checked = true;
            this.ativoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ativoCheckBox.Location = new System.Drawing.Point(283, 17);
            this.ativoCheckBox.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.ativoCheckBox.Name = "ativoCheckBox";
            this.ativoCheckBox.Size = new System.Drawing.Size(61, 21);
            this.ativoCheckBox.Style = Syncfusion.Windows.Forms.Tools.CheckBoxAdvStyle.Metro;
            this.ativoCheckBox.TabIndex = 4;
            this.ativoCheckBox.Text = "Ativo";
            this.ativoCheckBox.ThemeName = "Metro";
            this.ativoCheckBox.ThemesEnabled = false;
            this.ativoCheckBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ativoCheckBox_KeyDown);
            // 
            // nomeTextBox
            // 
            this.nomeTextBox.AcceptsReturn = true;
            this.nomeTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.nomeTextBox.BeforeTouchSize = new System.Drawing.Size(475, 23);
            this.nomeTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.nomeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nomeTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.nomeTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.nomeTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.nomeTextBox.Location = new System.Drawing.Point(16, 74);
            this.nomeTextBox.MaxLength = 50;
            this.nomeTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.nomeTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.nomeTextBox.Name = "nomeTextBox";
            this.nomeTextBox.Size = new System.Drawing.Size(475, 23);
            this.nomeTextBox.TabIndex = 6;
            this.nomeTextBox.ThemeName = "Default";
            this.nomeTextBox.UseBorderColorOnFocus = true;
            this.nomeTextBox.Enter += new System.EventHandler(this.nomeTextBox_Enter);
            this.nomeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nomeTextBox_KeyDown);
            this.nomeTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.nomeTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Descrição";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Controls.Add(this.excluirButton);
            this.panel3.Controls.Add(this.limparButton);
            this.panel3.Controls.Add(this.gravarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 492);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(506, 62);
            this.panel3.TabIndex = 1;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(506, 4);
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
            this.excluirButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.excluirButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.excluirButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.excluirButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.excluirButton.IsBackStageButton = false;
            this.excluirButton.Location = new System.Drawing.Point(359, 16);
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
            this.limparButton.Location = new System.Drawing.Point(199, 16);
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
            this.gravarButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.gravarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gravarButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.gravarButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.gravarButton.IsBackStageButton = false;
            this.gravarButton.Location = new System.Drawing.Point(39, 16);
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
            // VisoesBI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 554);
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
            this.Name = "VisoesBI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisoesBI";
            this.Shown += new System.EventHandler(this.VisoesBI_Shown);
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.camposPanel.ResumeLayout(false);
            this.camposPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExclusivoCheckBox)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RubricaTextBox)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.codigoTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ativoCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomeTextBox)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel camposPanel;
        private Syncfusion.Windows.Forms.ButtonAdv pesquisaCategoriaButton;
        private Syncfusion.Windows.Forms.ButtonAdv proximoButton;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt codigoTextBox;
        private System.Windows.Forms.Label label3;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv ativoCheckBox;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt nomeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv excluirButton;
        private Syncfusion.Windows.Forms.ButtonAdv limparButton;
        private Syncfusion.Windows.Forms.ButtonAdv gravarButton;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label15;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt RubricaTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem alterarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excluirToolStripMenuItem;
        private Syncfusion.Windows.Forms.Tools.CheckBoxAdv ExclusivoCheckBox;
    }
}