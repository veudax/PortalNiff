namespace Suportte.Contabilidade
{
    partial class Selic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Selic));
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor2 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor3 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.mensagemSistemaLabel = new System.Windows.Forms.Label();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ImportarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.limparButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.gravarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.referenciaMaskedEditBox = new Syncfusion.Windows.Forms.Tools.MaskedEditBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PrevistoCurrency = new Syncfusion.Windows.Forms.Tools.CurrencyTextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.excluirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.UFGCurrencyTextBox = new Syncfusion.Windows.Forms.Tools.CurrencyTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.referenciaMaskedEditBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrevistoCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UFGCurrencyTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloPanel
            // 
            this.tituloPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.tituloPanel.Controls.Add(this.mensagemSistemaLabel);
            this.tituloPanel.Controls.Add(this.tituloLabel);
            this.tituloPanel.Controls.Add(this.powerPictureBox);
            this.tituloPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloPanel.Name = "tituloPanel";
            this.tituloPanel.Size = new System.Drawing.Size(387, 40);
            this.tituloPanel.TabIndex = 6;
            this.tituloPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // mensagemSistemaLabel
            // 
            this.mensagemSistemaLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.mensagemSistemaLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensagemSistemaLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.mensagemSistemaLabel.Location = new System.Drawing.Point(132, 0);
            this.mensagemSistemaLabel.Name = "mensagemSistemaLabel";
            this.mensagemSistemaLabel.Size = new System.Drawing.Size(215, 40);
            this.mensagemSistemaLabel.TabIndex = 14;
            this.mensagemSistemaLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.mensagemSistemaLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.mensagemSistemaLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.mensagemSistemaLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.tituloLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tituloLabel.Location = new System.Drawing.Point(12, 10);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(106, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "SELIC / UFG";
            this.tituloLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(347, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ImportarButton);
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Controls.Add(this.limparButton);
            this.panel3.Controls.Add(this.gravarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 556);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(387, 62);
            this.panel3.TabIndex = 5;
            // 
            // ImportarButton
            // 
            this.ImportarButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.ImportarButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.ImportarButton.BeforeTouchSize = new System.Drawing.Size(121, 34);
            this.ImportarButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.ImportarButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.ImportarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportarButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.ImportarButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ImportarButton.Location = new System.Drawing.Point(251, 14);
            this.ImportarButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.ImportarButton.Name = "ImportarButton";
            this.ImportarButton.OverrideFormManagedColor = true;
            this.ImportarButton.Size = new System.Drawing.Size(121, 34);
            this.ImportarButton.TabIndex = 2;
            this.ImportarButton.Text = "&Importar Selic";
            this.ImportarButton.ThemeName = "Metro";
            this.ImportarButton.Click += new System.EventHandler(this.ImportarButton_Click);
            this.ImportarButton.Enter += new System.EventHandler(this.ImportarButton_Enter);
            this.ImportarButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImportarButton_KeyDown);
            this.ImportarButton.Validating += new System.ComponentModel.CancelEventHandler(this.ImportarButton_Validating);
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(387, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 18;
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
            this.limparButton.Location = new System.Drawing.Point(133, 14);
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
            this.gravarButton.Location = new System.Drawing.Point(15, 14);
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
            // referenciaMaskedEditBox
            // 
            this.referenciaMaskedEditBox.BackColor = System.Drawing.SystemColors.Control;
            this.referenciaMaskedEditBox.BeforeTouchSize = new System.Drawing.Size(142, 23);
            this.referenciaMaskedEditBox.BorderColor = System.Drawing.Color.DimGray;
            this.referenciaMaskedEditBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.referenciaMaskedEditBox.DecimalSeparator = '.';
            this.referenciaMaskedEditBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.referenciaMaskedEditBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.referenciaMaskedEditBox.Location = new System.Drawing.Point(12, 62);
            this.referenciaMaskedEditBox.Mask = "99/9999";
            this.referenciaMaskedEditBox.MaxLength = 7;
            this.referenciaMaskedEditBox.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.referenciaMaskedEditBox.Metrocolor = System.Drawing.Color.DimGray;
            this.referenciaMaskedEditBox.Name = "referenciaMaskedEditBox";
            this.referenciaMaskedEditBox.Size = new System.Drawing.Size(64, 23);
            this.referenciaMaskedEditBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.referenciaMaskedEditBox.TabIndex = 1;
            this.referenciaMaskedEditBox.ThemeName = "Metro";
            this.referenciaMaskedEditBox.ThemesEnabled = false;
            this.referenciaMaskedEditBox.UseBorderColorOnFocus = true;
            this.referenciaMaskedEditBox.Enter += new System.EventHandler(this.referenciaMaskedEditBox_Enter);
            this.referenciaMaskedEditBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.referenciaMaskedEditBox_KeyDown);
            this.referenciaMaskedEditBox.Validating += new System.ComponentModel.CancelEventHandler(this.referenciaMaskedEditBox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mês/Ano";
            // 
            // PrevistoCurrency
            // 
            this.PrevistoCurrency.BackGroundColor = System.Drawing.SystemColors.Control;
            this.PrevistoCurrency.BeforeTouchSize = new System.Drawing.Size(142, 23);
            this.PrevistoCurrency.BorderColor = System.Drawing.Color.DimGray;
            this.PrevistoCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PrevistoCurrency.CurrencyDecimalDigits = 4;
            this.PrevistoCurrency.CurrencySymbol = "";
            this.PrevistoCurrency.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.PrevistoCurrency.FocusBorderColor = System.Drawing.Color.Navy;
            this.PrevistoCurrency.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.PrevistoCurrency.Location = new System.Drawing.Point(82, 62);
            this.PrevistoCurrency.MaxValue = new decimal(new int[] {
            -727379969,
            232,
            0,
            131072});
            this.PrevistoCurrency.Metrocolor = System.Drawing.Color.DimGray;
            this.PrevistoCurrency.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PrevistoCurrency.Name = "PrevistoCurrency";
            this.PrevistoCurrency.Size = new System.Drawing.Size(142, 23);
            this.PrevistoCurrency.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.PrevistoCurrency.TabIndex = 3;
            this.PrevistoCurrency.Text = " 0,0000";
            this.PrevistoCurrency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrevistoCurrency.ThemeName = "Metro";
            this.PrevistoCurrency.UseBorderColorOnFocus = true;
            this.PrevistoCurrency.Enter += new System.EventHandler(this.PrevistoCurrency_Enter);
            this.PrevistoCurrency.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrevistoCurrency_KeyDown);
            this.PrevistoCurrency.Validating += new System.ComponentModel.CancelEventHandler(this.PrevistoCurrency_Validating);
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Century Gothic", 7.75F);
            this.label70.Location = new System.Drawing.Point(189, 45);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(35, 16);
            this.label70.TabIndex = 2;
            this.label70.Text = "Valor";
            // 
            // gridGroupingControl1
            // 
            this.gridGroupingControl1.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.SetCurrent;
            this.gridGroupingControl1.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(94)))), ((int)(((byte)(171)))), ((int)(((byte)(222)))));
            this.gridGroupingControl1.BackColor = System.Drawing.SystemColors.Window;
            this.gridGroupingControl1.ChildGroupOptions.CaptionText = "{CategoryCaption}: {Category} ";
            this.gridGroupingControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridGroupingControl1.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            this.gridGroupingControl1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupingControl1.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
            this.gridGroupingControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
            this.gridGroupingControl1.Location = new System.Drawing.Point(12, 91);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl1.Size = new System.Drawing.Size(363, 459);
            this.gridGroupingControl1.TabIndex = 4;
            this.gridGroupingControl1.TableDescriptor.AllowNew = false;
            this.gridGroupingControl1.TableDescriptor.Appearance.AnyCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            this.gridGroupingControl1.TableDescriptor.ChildGroupOptions.IsExpandedInitialValue = true;
            gridColumnDescriptor1.Appearance.ColumnHeaderCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor1.HeaderText = "Mês/Ano";
            gridColumnDescriptor1.MappingName = "MesAno";
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.Format = "n4";
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right;
            gridColumnDescriptor2.Appearance.ColumnHeaderCell.AutoSize = true;
            gridColumnDescriptor2.Appearance.ColumnHeaderCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right;
            gridColumnDescriptor2.HeaderText = "Selic";
            gridColumnDescriptor2.MappingName = "Valor";
            gridColumnDescriptor3.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor3.Appearance.AnyRecordFieldCell.Format = "n4";
            gridColumnDescriptor3.Appearance.AnyRecordFieldCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right;
            gridColumnDescriptor3.Appearance.ColumnHeaderCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right;
            gridColumnDescriptor3.HeaderText = "UFG";
            gridColumnDescriptor3.MappingName = "ValorUFG";
            this.gridGroupingControl1.TableDescriptor.Columns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor[] {
            gridColumnDescriptor1,
            gridColumnDescriptor2,
            gridColumnDescriptor3,
            new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor("Ano")});
            this.gridGroupingControl1.TableDescriptor.GroupedColumns.AddRange(new Syncfusion.Grouping.SortColumnDescriptor[] {
            new Syncfusion.Grouping.SortColumnDescriptor("Ano", System.ComponentModel.ListSortDirection.Descending)});
            this.gridGroupingControl1.TableDescriptor.SortedColumns.AddRange(new Syncfusion.Grouping.SortColumnDescriptor[] {
            new Syncfusion.Grouping.SortColumnDescriptor("Referencia", System.ComponentModel.ListSortDirection.Descending)});
            this.gridGroupingControl1.TableDescriptor.TableOptions.CaptionRowHeight = 29;
            this.gridGroupingControl1.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 25;
            this.gridGroupingControl1.TableDescriptor.TableOptions.RecordRowHeight = 25;
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor[] {
            new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor("MesAno"),
            new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor("Valor"),
            new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor("ValorUFG")});
            this.gridGroupingControl1.TableOptions.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.gridGroupingControl1.TableOptions.SelectionTextColor = System.Drawing.SystemColors.HighlightText;
            this.gridGroupingControl1.Text = "gridGroupingControl1";
            this.gridGroupingControl1.TopLevelGroupOptions.ShowCaption = false;
            this.gridGroupingControl1.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl1.VersionInfo = "17.2460.0.34";
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Arquivos Excel (*.xlsx)|*.xlsx";
            // 
            // UFGCurrencyTextBox
            // 
            this.UFGCurrencyTextBox.BackGroundColor = System.Drawing.SystemColors.Control;
            this.UFGCurrencyTextBox.BeforeTouchSize = new System.Drawing.Size(142, 23);
            this.UFGCurrencyTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.UFGCurrencyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UFGCurrencyTextBox.CurrencyDecimalDigits = 4;
            this.UFGCurrencyTextBox.CurrencySymbol = "";
            this.UFGCurrencyTextBox.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.UFGCurrencyTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.UFGCurrencyTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.UFGCurrencyTextBox.Location = new System.Drawing.Point(233, 62);
            this.UFGCurrencyTextBox.MaxValue = new decimal(new int[] {
            -727379969,
            232,
            0,
            131072});
            this.UFGCurrencyTextBox.Metrocolor = System.Drawing.Color.DimGray;
            this.UFGCurrencyTextBox.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.UFGCurrencyTextBox.Name = "UFGCurrencyTextBox";
            this.UFGCurrencyTextBox.Size = new System.Drawing.Size(142, 23);
            this.UFGCurrencyTextBox.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Metro;
            this.UFGCurrencyTextBox.TabIndex = 8;
            this.UFGCurrencyTextBox.Text = " 0,0000";
            this.UFGCurrencyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UFGCurrencyTextBox.ThemeName = "Metro";
            this.UFGCurrencyTextBox.UseBorderColorOnFocus = true;
            this.UFGCurrencyTextBox.Enter += new System.EventHandler(this.PrevistoCurrency_Enter);
            this.UFGCurrencyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UFGCurrencyTextBox_KeyDown);
            this.UFGCurrencyTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.UFGCurrencyTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 7.75F);
            this.label2.Location = new System.Drawing.Point(340, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "UFG";
            // 
            // Selic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 618);
            this.ControlBox = false;
            this.Controls.Add(this.UFGCurrencyTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridGroupingControl1);
            this.Controls.Add(this.PrevistoCurrency);
            this.Controls.Add(this.label70);
            this.Controls.Add(this.referenciaMaskedEditBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tituloPanel);
            this.Font = new System.Drawing.Font("Symbol", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Selic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selic";
            this.Shown += new System.EventHandler(this.Selic_Shown);
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.referenciaMaskedEditBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrevistoCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UFGCurrencyTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.Windows.Forms.ButtonAdv ImportarButton;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv limparButton;
        private Syncfusion.Windows.Forms.ButtonAdv gravarButton;
        public Syncfusion.Windows.Forms.Tools.MaskedEditBox referenciaMaskedEditBox;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.CurrencyTextBox PrevistoCurrency;
        private System.Windows.Forms.Label label70;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem excluirToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.Label mensagemSistemaLabel;
        private Syncfusion.Windows.Forms.Tools.CurrencyTextBox UFGCurrencyTextBox;
        private System.Windows.Forms.Label label2;
    }
}