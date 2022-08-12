namespace Suportte.Avaliacao_de_desempenho
{
    partial class Radar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Radar));
            Syncfusion.Windows.Forms.Chart.ChartSeries chartSeries1 = new Syncfusion.Windows.Forms.Chart.ChartSeries();
            Syncfusion.Windows.Forms.Chart.ChartCustomShapeInfo chartCustomShapeInfo1 = new Syncfusion.Windows.Forms.Chart.ChartCustomShapeInfo();
            Syncfusion.Windows.Forms.Chart.ChartLineInfo chartLineInfo1 = new Syncfusion.Windows.Forms.Chart.ChartLineInfo();
            Syncfusion.Windows.Forms.Chart.ChartSeries chartSeries2 = new Syncfusion.Windows.Forms.Chart.ChartSeries();
            Syncfusion.Windows.Forms.Chart.ChartCustomShapeInfo chartCustomShapeInfo2 = new Syncfusion.Windows.Forms.Chart.ChartCustomShapeInfo();
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.mensagemSistemaLabel = new System.Windows.Forms.Label();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.GrupoDadosPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.descricaoCargoTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.pesquisaUsuarioButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.nomeTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.usuarioTextBox = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label26 = new System.Windows.Forms.Label();
            this.empresaComboBoxAdv = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label19 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.limparButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.graficoPanel = new System.Windows.Forms.Panel();
            this.chartControl1 = new Syncfusion.Windows.Forms.Chart.ChartControl();
            this.gridGroupingControl = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.GrupoDadosPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.descricaoCargoTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomeTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usuarioTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaComboBoxAdv)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).BeginInit();
            this.graficoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl)).BeginInit();
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
            this.tituloPanel.Size = new System.Drawing.Size(900, 40);
            this.tituloPanel.TabIndex = 14;
            this.tituloPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // mensagemSistemaLabel
            // 
            this.mensagemSistemaLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.mensagemSistemaLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensagemSistemaLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.mensagemSistemaLabel.Location = new System.Drawing.Point(553, 0);
            this.mensagemSistemaLabel.Name = "mensagemSistemaLabel";
            this.mensagemSistemaLabel.Size = new System.Drawing.Size(307, 40);
            this.mensagemSistemaLabel.TabIndex = 13;
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
            this.tituloLabel.Size = new System.Drawing.Size(61, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Radar";
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(860, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // GrupoDadosPanel
            // 
            this.GrupoDadosPanel.Controls.Add(this.label3);
            this.GrupoDadosPanel.Controls.Add(this.descricaoCargoTextBox);
            this.GrupoDadosPanel.Controls.Add(this.pesquisaUsuarioButton);
            this.GrupoDadosPanel.Controls.Add(this.nomeTextBox);
            this.GrupoDadosPanel.Controls.Add(this.usuarioTextBox);
            this.GrupoDadosPanel.Controls.Add(this.label26);
            this.GrupoDadosPanel.Controls.Add(this.empresaComboBoxAdv);
            this.GrupoDadosPanel.Controls.Add(this.label19);
            this.GrupoDadosPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.GrupoDadosPanel.Location = new System.Drawing.Point(0, 40);
            this.GrupoDadosPanel.Name = "GrupoDadosPanel";
            this.GrupoDadosPanel.Size = new System.Drawing.Size(900, 99);
            this.GrupoDadosPanel.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(472, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 31;
            this.label3.Text = "Cargo";
            // 
            // descricaoCargoTextBox
            // 
            this.descricaoCargoTextBox.AcceptsReturn = true;
            this.descricaoCargoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.descricaoCargoTextBox.BeforeTouchSize = new System.Drawing.Size(71, 23);
            this.descricaoCargoTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.descricaoCargoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descricaoCargoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.descricaoCargoTextBox.Enabled = false;
            this.descricaoCargoTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.descricaoCargoTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.descricaoCargoTextBox.Location = new System.Drawing.Point(475, 67);
            this.descricaoCargoTextBox.MaxLength = 50;
            this.descricaoCargoTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.descricaoCargoTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.descricaoCargoTextBox.Name = "descricaoCargoTextBox";
            this.descricaoCargoTextBox.Size = new System.Drawing.Size(415, 23);
            this.descricaoCargoTextBox.TabIndex = 30;
            this.descricaoCargoTextBox.ThemeName = "Default";
            this.descricaoCargoTextBox.UseBorderColorOnFocus = true;
            // 
            // pesquisaUsuarioButton
            // 
            this.pesquisaUsuarioButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.pesquisaUsuarioButton.BackColor = System.Drawing.SystemColors.Control;
            this.pesquisaUsuarioButton.BeforeTouchSize = new System.Drawing.Size(23, 23);
            this.pesquisaUsuarioButton.Enabled = false;
            this.pesquisaUsuarioButton.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.pesquisaUsuarioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pesquisaUsuarioButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.pesquisaUsuarioButton.ForeColor = System.Drawing.Color.White;
            this.pesquisaUsuarioButton.Image = ((System.Drawing.Image)(resources.GetObject("pesquisaUsuarioButton.Image")));
            this.pesquisaUsuarioButton.IsBackStageButton = false;
            this.pesquisaUsuarioButton.Location = new System.Drawing.Point(82, 67);
            this.pesquisaUsuarioButton.MetroColor = System.Drawing.Color.DimGray;
            this.pesquisaUsuarioButton.Name = "pesquisaUsuarioButton";
            this.pesquisaUsuarioButton.OverrideFormManagedColor = true;
            this.pesquisaUsuarioButton.Size = new System.Drawing.Size(23, 23);
            this.pesquisaUsuarioButton.TabIndex = 14;
            this.pesquisaUsuarioButton.TabStop = false;
            this.pesquisaUsuarioButton.ThemeName = "Metro";
            this.pesquisaUsuarioButton.Click += new System.EventHandler(this.pesquisaUsuarioButton_Click);
            // 
            // nomeTextBox
            // 
            this.nomeTextBox.AcceptsReturn = true;
            this.nomeTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.nomeTextBox.BeforeTouchSize = new System.Drawing.Size(71, 23);
            this.nomeTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.nomeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nomeTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.nomeTextBox.Enabled = false;
            this.nomeTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.nomeTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.nomeTextBox.Location = new System.Drawing.Point(109, 67);
            this.nomeTextBox.MaxLength = 50;
            this.nomeTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.nomeTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.nomeTextBox.Name = "nomeTextBox";
            this.nomeTextBox.Size = new System.Drawing.Size(360, 23);
            this.nomeTextBox.TabIndex = 15;
            this.nomeTextBox.ThemeName = "Default";
            this.nomeTextBox.UseBorderColorOnFocus = true;
            // 
            // usuarioTextBox
            // 
            this.usuarioTextBox.AcceptsReturn = true;
            this.usuarioTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.usuarioTextBox.BeforeTouchSize = new System.Drawing.Size(71, 23);
            this.usuarioTextBox.BorderColor = System.Drawing.Color.DimGray;
            this.usuarioTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.usuarioTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.usuarioTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.usuarioTextBox.FocusBorderColor = System.Drawing.Color.Navy;
            this.usuarioTextBox.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.usuarioTextBox.Location = new System.Drawing.Point(12, 67);
            this.usuarioTextBox.MaxLength = 6;
            this.usuarioTextBox.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.usuarioTextBox.MinimumSize = new System.Drawing.Size(10, 6);
            this.usuarioTextBox.Name = "usuarioTextBox";
            this.usuarioTextBox.Size = new System.Drawing.Size(71, 23);
            this.usuarioTextBox.TabIndex = 13;
            this.usuarioTextBox.ThemeName = "Default";
            this.usuarioTextBox.UseBorderColorOnFocus = true;
            this.usuarioTextBox.TextChanged += new System.EventHandler(this.usuarioTextBox_TextChanged);
            this.usuarioTextBox.Enter += new System.EventHandler(this.usuarioTextBox_Enter);
            this.usuarioTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.usuarioTextBox_KeyDown);
            this.usuarioTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.usuarioTextBox_Validating);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(9, 50);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(108, 15);
            this.label26.TabIndex = 12;
            this.label26.Text = "Registro no Globus";
            // 
            // empresaComboBoxAdv
            // 
            this.empresaComboBoxAdv.BeforeTouchSize = new System.Drawing.Size(878, 25);
            this.empresaComboBoxAdv.FlatBorderColor = System.Drawing.Color.DimGray;
            this.empresaComboBoxAdv.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.empresaComboBoxAdv.ForeColor = System.Drawing.SystemColors.WindowText;
            this.empresaComboBoxAdv.Location = new System.Drawing.Point(12, 24);
            this.empresaComboBoxAdv.MaxDropDownItems = 10;
            this.empresaComboBoxAdv.Name = "empresaComboBoxAdv";
            this.empresaComboBoxAdv.Size = new System.Drawing.Size(878, 25);
            this.empresaComboBoxAdv.Style = Syncfusion.Windows.Forms.VisualStyle.VS2010;
            this.empresaComboBoxAdv.TabIndex = 9;
            this.empresaComboBoxAdv.ThemeName = "VS2010";
            this.empresaComboBoxAdv.Enter += new System.EventHandler(this.empresaComboBoxAdv_Enter);
            this.empresaComboBoxAdv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.empresaComboBoxAdv_KeyDown);
            this.empresaComboBoxAdv.Validating += new System.ComponentModel.CancelEventHandler(this.empresaComboBoxAdv_Validating);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(9, 7);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 15);
            this.label19.TabIndex = 8;
            this.label19.Text = "Empresa";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.limparButton);
            this.panel3.Controls.Add(this.pictureBox44);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 614);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(900, 62);
            this.panel3.TabIndex = 16;
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
            this.limparButton.Location = new System.Drawing.Point(399, 14);
            this.limparButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.limparButton.Name = "limparButton";
            this.limparButton.OverrideFormManagedColor = true;
            this.limparButton.Size = new System.Drawing.Size(103, 34);
            this.limparButton.TabIndex = 19;
            this.limparButton.Text = "&Limpar";
            this.limparButton.ThemeName = "Metro";
            this.limparButton.Click += new System.EventHandler(this.limparButton_Click);
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox44.Location = new System.Drawing.Point(0, 0);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(900, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 18;
            this.pictureBox44.TabStop = false;
            // 
            // graficoPanel
            // 
            this.graficoPanel.Controls.Add(this.chartControl1);
            this.graficoPanel.Controls.Add(this.gridGroupingControl);
            this.graficoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graficoPanel.Location = new System.Drawing.Point(0, 139);
            this.graficoPanel.Name = "graficoPanel";
            this.graficoPanel.Size = new System.Drawing.Size(900, 475);
            this.graficoPanel.TabIndex = 17;
            // 
            // chartControl1
            // 
            this.chartControl1.BackInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.SystemColors.Control);
            this.chartControl1.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            this.chartControl1.ChartArea.BorderColor = System.Drawing.Color.Transparent;
            this.chartControl1.ChartArea.CursorLocation = new System.Drawing.Point(0, 0);
            this.chartControl1.ChartArea.CursorReDraw = false;
            this.chartControl1.ChartInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            this.chartControl1.DataSourceName = "[none]";
            this.chartControl1.DisplayChartContextMenu = false;
            this.chartControl1.DisplaySeriesContextMenu = false;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.ElementsSpacing = 30;
            this.chartControl1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartControl1.Indexed = true;
            this.chartControl1.IsWindowLess = false;
            // 
            // 
            // 
            this.chartControl1.Legend.Alignment = Syncfusion.Windows.Forms.Chart.ChartAlignment.Center;
            this.chartControl1.Legend.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartControl1.Legend.Location = new System.Drawing.Point(325, 417);
            this.chartControl1.Legend.Orientation = Syncfusion.Windows.Forms.Chart.ChartOrientation.Horizontal;
            this.chartControl1.Legend.Position = Syncfusion.Windows.Forms.Chart.ChartDock.Bottom;
            this.chartControl1.Legend.TextAlignment = System.Drawing.StringAlignment.Far;
            this.chartControl1.LegendsPlacement = Syncfusion.Windows.Forms.Chart.ChartPlacement.Outside;
            this.chartControl1.Localize = null;
            this.chartControl1.Location = new System.Drawing.Point(96, 0);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.Palette = Syncfusion.Windows.Forms.Chart.ChartColorPalette.EarthTone;
            //this.chartControl1.PrimaryXAxis.AutoValueType = true;
            this.chartControl1.PrimaryXAxis.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartControl1.PrimaryXAxis.ForceZero = true;
            this.chartControl1.PrimaryXAxis.GridLineType.ForeColor = System.Drawing.Color.LightGray;
            this.chartControl1.PrimaryXAxis.InterlacedGridInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            this.chartControl1.PrimaryXAxis.LineType.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.chartControl1.PrimaryXAxis.LineType.ForeColor = System.Drawing.Color.DarkGray;
            this.chartControl1.PrimaryXAxis.LogLabelsDisplayMode = Syncfusion.Windows.Forms.Chart.LogLabelsDisplayMode.Default;
            this.chartControl1.PrimaryXAxis.Margin = true;
            this.chartControl1.PrimaryXAxis.MinorGridLineType.ForeColor = System.Drawing.Color.Gray;
            this.chartControl1.PrimaryXAxis.TitleColor = System.Drawing.SystemColors.ControlText;
            this.chartControl1.PrimaryXAxis.TitleFont = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.chartControl1.PrimaryYAxis.AutoValueType = true;
            this.chartControl1.PrimaryYAxis.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartControl1.PrimaryYAxis.ForeColor = System.Drawing.Color.Transparent;
            this.chartControl1.PrimaryYAxis.GridLineType.BackColor = System.Drawing.Color.DimGray;
            this.chartControl1.PrimaryYAxis.GridLineType.ForeColor = System.Drawing.Color.LightGray;
            this.chartControl1.PrimaryYAxis.InterlacedGridInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            this.chartControl1.PrimaryYAxis.LineType.BackColor = System.Drawing.Color.Silver;
            this.chartControl1.PrimaryYAxis.LineType.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.chartControl1.PrimaryYAxis.LineType.ForeColor = System.Drawing.Color.DarkGray;
            this.chartControl1.PrimaryYAxis.LineType.Width = 0F;
            this.chartControl1.PrimaryYAxis.LogLabelsDisplayMode = Syncfusion.Windows.Forms.Chart.LogLabelsDisplayMode.Default;
            this.chartControl1.PrimaryYAxis.Margin = true;
            this.chartControl1.PrimaryYAxis.MinorGridLineType.BackColor = System.Drawing.Color.Silver;
            this.chartControl1.PrimaryYAxis.MinorGridLineType.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.chartControl1.PrimaryYAxis.MinorGridLineType.ForeColor = System.Drawing.Color.DarkGray;
            this.chartControl1.PrimaryYAxis.TitleColor = System.Drawing.SystemColors.ControlText;
            this.chartControl1.PrimaryYAxis.TitleFont = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartControl1.Rotation = 60F;
            chartSeries1.FancyToolTip.ResizeInsideSymbol = true;
            chartSeries1.Name = "Default0";
            chartSeries1.Resolution = 0D;
            chartSeries1.StackingGroup = "Default Group";
            chartSeries1.Style.AltTagFormat = "";
            chartSeries1.Style.Border.Color = System.Drawing.Color.Transparent;
            chartSeries1.Style.DrawTextShape = false;
            chartLineInfo1.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
            chartLineInfo1.Color = System.Drawing.SystemColors.ControlText;
            chartLineInfo1.DashPattern = null;
            chartLineInfo1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartLineInfo1.Width = 1F;
            chartCustomShapeInfo1.Border = chartLineInfo1;
            chartCustomShapeInfo1.Color = System.Drawing.SystemColors.HighlightText;
            chartCustomShapeInfo1.Type = Syncfusion.Windows.Forms.Chart.ChartCustomShape.Square;
            chartSeries1.Style.TextShape = chartCustomShapeInfo1;
            chartSeries1.Text = "Default0";
            chartSeries1.Type = Syncfusion.Windows.Forms.Chart.ChartSeriesType.Radar;
            chartSeries2.FancyToolTip.ResizeInsideSymbol = true;
            chartSeries2.Name = "Default1";
            chartSeries2.Resolution = 0D;
            chartSeries2.StackingGroup = "Default Group";
            chartSeries2.Style.AltTagFormat = "";
            chartSeries2.Style.Border.Color = System.Drawing.Color.Transparent;
            chartSeries2.Style.DrawTextShape = false;
            chartCustomShapeInfo2.Border = chartLineInfo1;
            chartCustomShapeInfo2.Color = System.Drawing.SystemColors.HighlightText;
            chartCustomShapeInfo2.Type = Syncfusion.Windows.Forms.Chart.ChartCustomShape.Square;
            chartSeries2.Style.TextShape = chartCustomShapeInfo2;
            chartSeries2.Text = "Default1";
            chartSeries2.Type = Syncfusion.Windows.Forms.Chart.ChartSeriesType.Radar;
            this.chartControl1.Series.Add(chartSeries1);
            this.chartControl1.Series.Add(chartSeries2);
            this.chartControl1.Series3D = true;
            this.chartControl1.SeriesHighlight = true;
            this.chartControl1.ShadowColor = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Transparent);
            this.chartControl1.Size = new System.Drawing.Size(804, 475);
            this.chartControl1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            this.chartControl1.SpacingBetweenPoints = 5F;
            this.chartControl1.SpacingBetweenSeries = 15F;
            this.chartControl1.Style3D = true;
            this.chartControl1.TabIndex = 2;
            // 
            // 
            // 
            this.chartControl1.Title.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.chartControl1.Title.Name = "Default";
            this.chartControl1.VisualTheme = "";
            // 
            // gridGroupingControl
            // 
            this.gridGroupingControl.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.SetCurrent;
            this.gridGroupingControl.AllowSetCurrentRecordOnFocus = true;
            this.gridGroupingControl.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(94)))), ((int)(((byte)(171)))), ((int)(((byte)(222)))));
            this.gridGroupingControl.BackColor = System.Drawing.SystemColors.Control;
            this.gridGroupingControl.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            this.gridGroupingControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridGroupingControl.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupingControl.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
            this.gridGroupingControl.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
            this.gridGroupingControl.Location = new System.Drawing.Point(0, 0);
            this.gridGroupingControl.Name = "gridGroupingControl";
            this.gridGroupingControl.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl.Size = new System.Drawing.Size(96, 475);
            this.gridGroupingControl.TabIndex = 0;
            this.gridGroupingControl.TableDescriptor.AllowEdit = false;
            this.gridGroupingControl.TableDescriptor.AllowNew = false;
            this.gridGroupingControl.TableDescriptor.AllowRemove = false;
            this.gridGroupingControl.TableDescriptor.ChildGroupOptions.CaptionText = "{CategoryCaption}: {Category} ";
            this.gridGroupingControl.TableDescriptor.ChildGroupOptions.IsExpandedInitialValue = true;
            this.gridGroupingControl.TableDescriptor.ChildGroupOptions.ShowCaption = true;
            this.gridGroupingControl.TableDescriptor.ChildGroupOptions.ShowCaptionPlusMinus = true;
            this.gridGroupingControl.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = false;
            this.gridGroupingControl.TableDescriptor.ChildGroupOptions.ShowColumnHeaders = false;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.CellValueType = typeof(string);
            gridColumnDescriptor1.HeaderText = "Referência";
            gridColumnDescriptor1.MappingName = "ReferenciaFormatada";
            gridColumnDescriptor1.Width = 62;
            this.gridGroupingControl.TableDescriptor.Columns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor[] {
            new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor("Ano"),
            gridColumnDescriptor1});
            this.gridGroupingControl.TableDescriptor.GroupedColumns.AddRange(new Syncfusion.Grouping.SortColumnDescriptor[] {
            new Syncfusion.Grouping.SortColumnDescriptor("Ano", System.ComponentModel.ListSortDirection.Ascending)});
            this.gridGroupingControl.TableDescriptor.TableOptions.CaptionRowHeight = 29;
            this.gridGroupingControl.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 25;
            this.gridGroupingControl.TableDescriptor.TableOptions.RecordRowHeight = 25;
            this.gridGroupingControl.TableDescriptor.TopLevelGroupOptions.CaptionText = "{TableName}";
            this.gridGroupingControl.TableDescriptor.TopLevelGroupOptions.ShowCaption = false;
            this.gridGroupingControl.TableDescriptor.TopLevelGroupOptions.ShowCaptionPlusMinus = false;
            this.gridGroupingControl.TableDescriptor.TopLevelGroupOptions.ShowCaptionSummaryCells = false;
            this.gridGroupingControl.TableDescriptor.TopLevelGroupOptions.ShowColumnHeaders = true;
            this.gridGroupingControl.TableDescriptor.TopLevelGroupOptions.ShowEmptyGroups = false;
            this.gridGroupingControl.TableDescriptor.TopLevelGroupOptions.ShowGroupHeader = false;
            this.gridGroupingControl.TableDescriptor.TopLevelGroupOptions.ShowGroupIndentAsCoveredRange = false;
            this.gridGroupingControl.TableDescriptor.TopLevelGroupOptions.ShowGroupPreview = false;
            this.gridGroupingControl.TableDescriptor.TopLevelGroupOptions.ShowGroupSummaryWhenCollapsed = false;
            this.gridGroupingControl.TableDescriptor.VisibleColumns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor[] {
            new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor("ReferenciaFormatada")});
            this.gridGroupingControl.TableOptions.GridLineBorder = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid, System.Drawing.Color.Black, Syncfusion.Windows.Forms.Grid.GridBorderWeight.ExtraThin);
            this.gridGroupingControl.TableOptions.IndentWidth = 10;
            this.gridGroupingControl.TableOptions.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.gridGroupingControl.TableOptions.SelectionTextColor = System.Drawing.SystemColors.HighlightText;
            this.gridGroupingControl.TableOptions.ShowRowHeader = false;
            this.gridGroupingControl.Text = "gridGroupingControl1";
            this.gridGroupingControl.TopLevelGroupOptions.ShowCaption = false;
            this.gridGroupingControl.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl.VersionInfo = "15.4460.0.17";
            this.gridGroupingControl.TableControlCurrentCellKeyUp += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlKeyEventHandler(this.gridGroupingControl_TableControlCurrentCellKeyUp);
            this.gridGroupingControl.TableControlCellClick += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventHandler(this.gridGroupingControl_TableControlCellClick);
            // 
            // Radar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 676);
            this.ControlBox = false;
            this.Controls.Add(this.graficoPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.GrupoDadosPanel);
            this.Controls.Add(this.tituloPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Radar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radar";
            this.Load += new System.EventHandler(this.Radar_Load);
            this.Shown += new System.EventHandler(this.Radar_Shown);
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.GrupoDadosPanel.ResumeLayout(false);
            this.GrupoDadosPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.descricaoCargoTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nomeTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usuarioTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaComboBoxAdv)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            this.graficoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        public System.Windows.Forms.Label mensagemSistemaLabel;
        public System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel GrupoDadosPanel;
        public Syncfusion.Windows.Forms.Tools.ComboBoxAdv empresaComboBoxAdv;
        private System.Windows.Forms.Label label19;
        private Syncfusion.Windows.Forms.ButtonAdv pesquisaUsuarioButton;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt nomeTextBox;
        public Syncfusion.Windows.Forms.Tools.TextBoxExt usuarioTextBox;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label3;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt descricaoCargoTextBox;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.Windows.Forms.ButtonAdv limparButton;
        private System.Windows.Forms.PictureBox pictureBox44;
        private System.Windows.Forms.Panel graficoPanel;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl;
        private Syncfusion.Windows.Forms.Chart.ChartControl chartControl1;
    }
}