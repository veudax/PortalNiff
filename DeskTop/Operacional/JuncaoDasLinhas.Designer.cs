namespace Suportte.Operacional
{
    partial class JuncaoDasLinhas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JuncaoDasLinhas));
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor2 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.empresaComboBoxAdv = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label19 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LinhaTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.PesquisaLinhaButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.NomeLinhaTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.DadosPanel = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.DescrcaoLinhaTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.PesquisaLinhaAssociadaButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.CodigoLinhaTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.excluirButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.limparButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.gravarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.excluirEssaLinhaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaComboBoxAdv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinhaTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomeLinhaTextBox)).BeginInit();
            this.DadosPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DescrcaoLinhaTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CodigoLinhaTextBox)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
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
            this.tituloPanel.Size = new System.Drawing.Size(604, 40);
            this.tituloPanel.TabIndex = 4;
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
            this.tituloLabel.Size = new System.Drawing.Size(205, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Agrupamento de linhas";
            this.tituloLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(564, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // empresaComboBoxAdv
            // 
            this.empresaComboBoxAdv.BeforeTouchSize = new System.Drawing.Size(574, 24);
            this.empresaComboBoxAdv.FlatBorderColor = System.Drawing.Color.DimGray;
            this.empresaComboBoxAdv.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.empresaComboBoxAdv.ForeColor = System.Drawing.SystemColors.WindowText;
            this.empresaComboBoxAdv.Location = new System.Drawing.Point(16, 26);
            this.empresaComboBoxAdv.MaxDropDownItems = 10;
            this.empresaComboBoxAdv.Name = "empresaComboBoxAdv";
            this.empresaComboBoxAdv.Size = new System.Drawing.Size(574, 24);
            this.empresaComboBoxAdv.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.empresaComboBoxAdv.TabIndex = 5;
            this.empresaComboBoxAdv.ThemeName = "VS2010";
            this.empresaComboBoxAdv.Enter += new System.EventHandler(this.empresaComboBoxAdv_Enter);
            this.empresaComboBoxAdv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.empresaComboBoxAdv_KeyDown);
            this.empresaComboBoxAdv.Validating += new System.ComponentModel.CancelEventHandler(this.empresaComboBoxAdv_Validating);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(13, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 15);
            this.label19.TabIndex = 4;
            this.label19.Text = "Empresa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 35;
            this.label1.Text = "Linha Principal";
            // 
            // LinhaTextBox
            // 
            this.LinhaTextBox.AcceptsReturn = true;
            this.LinhaTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.LinhaTextBox.BeforeTouchSize = new System.Drawing.Size(82, 23);
            this.LinhaTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.LinhaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LinhaTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.LinhaTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.LinhaTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.LinhaTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.LinhaTextBox.Location = new System.Drawing.Point(16, 67);
            this.LinhaTextBox.MaxLength = 10;
            this.LinhaTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.LinhaTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.LinhaTextBox.Name = "LinhaTextBox";
            this.LinhaTextBox.Size = new System.Drawing.Size(82, 23);
            this.LinhaTextBox.TabIndex = 36;
            this.LinhaTextBox.ThemeName = "Default";
            this.LinhaTextBox.UseBorderColorOnFocus = true;
            this.LinhaTextBox.TextChanged += new System.EventHandler(this.LinhaTextBox_TextChanged);
            this.LinhaTextBox.Enter += new System.EventHandler(this.LinhaTextBox_Enter);
            this.LinhaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LinhaTextBox_KeyDown);
            this.LinhaTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.LinhaTextBox_Validating);
            // 
            // PesquisaLinhaButton
            // 
            this.PesquisaLinhaButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.PesquisaLinhaButton.BackColor = System.Drawing.SystemColors.Control;
            this.PesquisaLinhaButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.PesquisaLinhaButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.PesquisaLinhaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PesquisaLinhaButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.PesquisaLinhaButton.ForeColor = System.Drawing.Color.Black;
            this.PesquisaLinhaButton.Image = ((System.Drawing.Image)(resources.GetObject("PesquisaLinhaButton.Image")));
            this.PesquisaLinhaButton.IsBackStageButton = false;
            this.PesquisaLinhaButton.Location = new System.Drawing.Point(97, 67);
            this.PesquisaLinhaButton.MetroColor = System.Drawing.Color.DimGray;
            this.PesquisaLinhaButton.Name = "PesquisaLinhaButton";
            this.PesquisaLinhaButton.OverrideFormManagedColor = true;
            this.PesquisaLinhaButton.Size = new System.Drawing.Size(23, 23);
            this.PesquisaLinhaButton.TabIndex = 37;
            this.PesquisaLinhaButton.TabStop = false;
            this.PesquisaLinhaButton.ThemeName = "Metro";
            this.PesquisaLinhaButton.Click += new System.EventHandler(this.PesquisaLinhaButton_Click);
            // 
            // NomeLinhaTextBox
            // 
            this.NomeLinhaTextBox.AcceptsReturn = true;
            this.NomeLinhaTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.NomeLinhaTextBox.BeforeTouchSize = new System.Drawing.Size(82, 23);
            this.NomeLinhaTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.NomeLinhaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NomeLinhaTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.NomeLinhaTextBox.Enabled = false;
            this.NomeLinhaTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.NomeLinhaTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.NomeLinhaTextBox.Location = new System.Drawing.Point(122, 67);
            this.NomeLinhaTextBox.MaxLength = 300;
            this.NomeLinhaTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.NomeLinhaTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.NomeLinhaTextBox.Name = "NomeLinhaTextBox";
            this.NomeLinhaTextBox.Size = new System.Drawing.Size(468, 23);
            this.NomeLinhaTextBox.TabIndex = 38;
            this.NomeLinhaTextBox.ThemeName = "Default";
            this.NomeLinhaTextBox.UseBorderColorOnFocus = true;
            // 
            // DadosPanel
            // 
            this.DadosPanel.Controls.Add(this.label9);
            this.DadosPanel.Controls.Add(this.gridGroupingControl1);
            this.DadosPanel.Controls.Add(this.DescrcaoLinhaTextBox);
            this.DadosPanel.Controls.Add(this.PesquisaLinhaAssociadaButton);
            this.DadosPanel.Controls.Add(this.CodigoLinhaTextBox);
            this.DadosPanel.Controls.Add(this.label2);
            this.DadosPanel.Controls.Add(this.NomeLinhaTextBox);
            this.DadosPanel.Controls.Add(this.PesquisaLinhaButton);
            this.DadosPanel.Controls.Add(this.LinhaTextBox);
            this.DadosPanel.Controls.Add(this.label1);
            this.DadosPanel.Controls.Add(this.empresaComboBoxAdv);
            this.DadosPanel.Controls.Add(this.label19);
            this.DadosPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DadosPanel.Location = new System.Drawing.Point(0, 40);
            this.DadosPanel.Name = "DadosPanel";
            this.DadosPanel.Size = new System.Drawing.Size(604, 348);
            this.DadosPanel.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Wingdings 3", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label9.Location = new System.Drawing.Point(14, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 12);
            this.label9.TabIndex = 44;
            this.label9.Text = "i";
            // 
            // gridGroupingControl1
            // 
            this.gridGroupingControl1.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.SetCurrent;
            this.gridGroupingControl1.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(94)))), ((int)(((byte)(171)))), ((int)(((byte)(222)))));
            this.gridGroupingControl1.BackColor = System.Drawing.SystemColors.Control;
            this.gridGroupingControl1.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            this.gridGroupingControl1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupingControl1.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
            this.gridGroupingControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
            this.gridGroupingControl1.Location = new System.Drawing.Point(16, 136);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl1.Size = new System.Drawing.Size(576, 201);
            this.gridGroupingControl1.TabIndex = 43;
            this.gridGroupingControl1.TableDescriptor.AllowNew = false;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor1.HeaderText = "Código";
            gridColumnDescriptor1.MappingName = "CodigoLinha";
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor2.HeaderText = "Nome";
            gridColumnDescriptor2.MappingName = "NomeLinha";
            this.gridGroupingControl1.TableDescriptor.Columns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor[] {
            gridColumnDescriptor1,
            gridColumnDescriptor2});
            this.gridGroupingControl1.TableDescriptor.TableOptions.CaptionRowHeight = 29;
            this.gridGroupingControl1.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 25;
            this.gridGroupingControl1.TableDescriptor.TableOptions.RecordRowHeight = 25;
            this.gridGroupingControl1.TableOptions.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.gridGroupingControl1.TableOptions.SelectionTextColor = System.Drawing.SystemColors.HighlightText;
            this.gridGroupingControl1.TableOptions.ShowRowHeader = true;
            this.gridGroupingControl1.Text = "gridGroupingControl1";
            this.gridGroupingControl1.TopLevelGroupOptions.ShowCaption = false;
            this.gridGroupingControl1.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl1.VersionInfo = "16.2460.0.41";
            // 
            // DescrcaoLinhaTextBox
            // 
            this.DescrcaoLinhaTextBox.AcceptsReturn = true;
            this.DescrcaoLinhaTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.DescrcaoLinhaTextBox.BeforeTouchSize = new System.Drawing.Size(82, 23);
            this.DescrcaoLinhaTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.DescrcaoLinhaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescrcaoLinhaTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DescrcaoLinhaTextBox.Enabled = false;
            this.DescrcaoLinhaTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.DescrcaoLinhaTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.DescrcaoLinhaTextBox.Location = new System.Drawing.Point(122, 107);
            this.DescrcaoLinhaTextBox.MaxLength = 300;
            this.DescrcaoLinhaTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.DescrcaoLinhaTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.DescrcaoLinhaTextBox.Name = "DescrcaoLinhaTextBox";
            this.DescrcaoLinhaTextBox.Size = new System.Drawing.Size(468, 23);
            this.DescrcaoLinhaTextBox.TabIndex = 42;
            this.DescrcaoLinhaTextBox.ThemeName = "Default";
            this.DescrcaoLinhaTextBox.UseBorderColorOnFocus = true;
            // 
            // PesquisaLinhaAssociadaButton
            // 
            this.PesquisaLinhaAssociadaButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.PesquisaLinhaAssociadaButton.BackColor = System.Drawing.SystemColors.Control;
            this.PesquisaLinhaAssociadaButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.PesquisaLinhaAssociadaButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.PesquisaLinhaAssociadaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PesquisaLinhaAssociadaButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.PesquisaLinhaAssociadaButton.ForeColor = System.Drawing.Color.Black;
            this.PesquisaLinhaAssociadaButton.Image = ((System.Drawing.Image)(resources.GetObject("PesquisaLinhaAssociadaButton.Image")));
            this.PesquisaLinhaAssociadaButton.IsBackStageButton = false;
            this.PesquisaLinhaAssociadaButton.Location = new System.Drawing.Point(97, 107);
            this.PesquisaLinhaAssociadaButton.MetroColor = System.Drawing.Color.DimGray;
            this.PesquisaLinhaAssociadaButton.Name = "PesquisaLinhaAssociadaButton";
            this.PesquisaLinhaAssociadaButton.OverrideFormManagedColor = true;
            this.PesquisaLinhaAssociadaButton.Size = new System.Drawing.Size(23, 23);
            this.PesquisaLinhaAssociadaButton.TabIndex = 41;
            this.PesquisaLinhaAssociadaButton.TabStop = false;
            this.PesquisaLinhaAssociadaButton.ThemeName = "Metro";
            this.PesquisaLinhaAssociadaButton.Click += new System.EventHandler(this.PesquisaLinhaAssociadaButton_Click);
            // 
            // CodigoLinhaTextBox
            // 
            this.CodigoLinhaTextBox.AcceptsReturn = true;
            this.CodigoLinhaTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.CodigoLinhaTextBox.BeforeTouchSize = new System.Drawing.Size(82, 23);
            this.CodigoLinhaTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.CodigoLinhaTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CodigoLinhaTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.CodigoLinhaTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.CodigoLinhaTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.CodigoLinhaTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.CodigoLinhaTextBox.Location = new System.Drawing.Point(16, 107);
            this.CodigoLinhaTextBox.MaxLength = 10;
            this.CodigoLinhaTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.CodigoLinhaTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.CodigoLinhaTextBox.Name = "CodigoLinhaTextBox";
            this.CodigoLinhaTextBox.Size = new System.Drawing.Size(82, 23);
            this.CodigoLinhaTextBox.TabIndex = 40;
            this.CodigoLinhaTextBox.ThemeName = "Default";
            this.CodigoLinhaTextBox.UseBorderColorOnFocus = true;
            this.CodigoLinhaTextBox.TextChanged += new System.EventHandler(this.CodigoLinhaTextBox_TextChanged);
            this.CodigoLinhaTextBox.Enter += new System.EventHandler(this.LinhaTextBox_Enter);
            this.CodigoLinhaTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodigoLinhaTextBox_KeyDown);
            this.CodigoLinhaTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.CodigoLinhaTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 15);
            this.label2.TabIndex = 39;
            this.label2.Text = "Linha a ser agrupada";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Controls.Add(this.excluirButton);
            this.panel3.Controls.Add(this.limparButton);
            this.panel3.Controls.Add(this.gravarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 388);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(604, 62);
            this.panel3.TabIndex = 50;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(604, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 18;
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
            this.excluirButton.Location = new System.Drawing.Point(462, 14);
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
            this.limparButton.Location = new System.Drawing.Point(251, 14);
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
            this.gravarButton.Location = new System.Drawing.Point(40, 14);
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excluirEssaLinhaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 26);
            // 
            // excluirEssaLinhaToolStripMenuItem
            // 
            this.excluirEssaLinhaToolStripMenuItem.Name = "excluirEssaLinhaToolStripMenuItem";
            this.excluirEssaLinhaToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.excluirEssaLinhaToolStripMenuItem.Text = "Excluir essa linha";
            this.excluirEssaLinhaToolStripMenuItem.Click += new System.EventHandler(this.excluirEssaLinhaToolStripMenuItem_Click);
            // 
            // JuncaoDasLinhas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 450);
            this.ControlBox = false;
            this.Controls.Add(this.DadosPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tituloPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JuncaoDasLinhas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agrupamento de linhas";
            this.Shown += new System.EventHandler(this.JuncaoDasLinhas_Shown);
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaComboBoxAdv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinhaTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NomeLinhaTextBox)).EndInit();
            this.DadosPanel.ResumeLayout(false);
            this.DadosPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DescrcaoLinhaTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CodigoLinhaTextBox)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        public Syncfusion.Windows.Forms.Tools.ComboBoxAdv empresaComboBoxAdv;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt LinhaTextBox;
        private Syncfusion.Windows.Forms.ButtonAdv PesquisaLinhaButton;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt NomeLinhaTextBox;
        private System.Windows.Forms.Panel DadosPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv excluirButton;
        private Syncfusion.Windows.Forms.ButtonAdv limparButton;
        private Syncfusion.Windows.Forms.ButtonAdv gravarButton;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt DescrcaoLinhaTextBox;
        private Syncfusion.Windows.Forms.ButtonAdv PesquisaLinhaAssociadaButton;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt CodigoLinhaTextBox;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem excluirEssaLinhaToolStripMenuItem;
        private System.Windows.Forms.Label label9;
    }
}