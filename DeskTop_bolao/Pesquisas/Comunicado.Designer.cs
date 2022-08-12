namespace Suportte.Pesquisas
{
    partial class Comunicado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Comunicado));
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor2 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor3 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor4 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor5 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.mensagemSistemaLabel = new System.Windows.Forms.Label();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox44 = new System.Windows.Forms.PictureBox();
            this.confirmarButton = new Syncfusion.Windows.Forms.ButtonAdv();
            this.gridGroupingControl = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.tituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl)).BeginInit();
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
            this.tituloPanel.Size = new System.Drawing.Size(810, 40);
            this.tituloPanel.TabIndex = 4;
            this.tituloPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // mensagemSistemaLabel
            // 
            this.mensagemSistemaLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.mensagemSistemaLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensagemSistemaLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.mensagemSistemaLabel.Location = new System.Drawing.Point(463, 0);
            this.mensagemSistemaLabel.Name = "mensagemSistemaLabel";
            this.mensagemSistemaLabel.Size = new System.Drawing.Size(307, 40);
            this.mensagemSistemaLabel.TabIndex = 1;
            this.mensagemSistemaLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.mensagemSistemaLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.mensagemSistemaLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.mensagemSistemaLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.tituloLabel.ForeColor = System.Drawing.Color.GreenYellow;
            this.tituloLabel.Location = new System.Drawing.Point(12, 10);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(232, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Pesquisa de Comunidados";
            this.tituloLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(770, 0);
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
            this.panel3.Controls.Add(this.confirmarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 482);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(810, 45);
            this.panel3.TabIndex = 5;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox44.Location = new System.Drawing.Point(0, 41);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(810, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 19;
            this.pictureBox44.TabStop = false;
            // 
            // confirmarButton
            // 
            this.confirmarButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.confirmarButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.confirmarButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.confirmarButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.confirmarButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.confirmarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirmarButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.confirmarButton.ForeColor = System.Drawing.Color.Black;
            this.confirmarButton.IsBackStageButton = false;
            this.confirmarButton.Location = new System.Drawing.Point(697, 5);
            this.confirmarButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(241)))), ((int)(((byte)(39)))));
            this.confirmarButton.Name = "confirmarButton";
            this.confirmarButton.OverrideFormManagedColor = true;
            this.confirmarButton.Size = new System.Drawing.Size(103, 34);
            this.confirmarButton.TabIndex = 0;
            this.confirmarButton.Text = "&Confirmar";
            this.confirmarButton.Click += new System.EventHandler(this.confirmarButton_Click);
            this.confirmarButton.Enter += new System.EventHandler(this.confirmarButton_Enter);
            this.confirmarButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.confirmarButton_KeyDown);
            this.confirmarButton.Validating += new System.ComponentModel.CancelEventHandler(this.confirmarButton_Validating);
            // 
            // gridGroupingControl
            // 
            this.gridGroupingControl.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.SetCurrent;
            this.gridGroupingControl.AllowSetCurrentRecordOnFocus = true;
            this.gridGroupingControl.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(94)))), ((int)(((byte)(171)))), ((int)(((byte)(222)))));
            this.gridGroupingControl.BackColor = System.Drawing.SystemColors.Window;
            this.gridGroupingControl.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            this.gridGroupingControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGroupingControl.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupingControl.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
            this.gridGroupingControl.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
            this.gridGroupingControl.Location = new System.Drawing.Point(0, 40);
            this.gridGroupingControl.Name = "gridGroupingControl";
            this.gridGroupingControl.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl.ShowNavigationBar = true;
            this.gridGroupingControl.Size = new System.Drawing.Size(810, 442);
            this.gridGroupingControl.TabIndex = 6;
            this.gridGroupingControl.TableDescriptor.AllowEdit = false;
            this.gridGroupingControl.TableDescriptor.AllowNew = false;
            this.gridGroupingControl.TableDescriptor.AllowRemove = false;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.CellValueType = typeof(string);
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor1.Appearance.ColumnHeaderCell.AutoSize = true;
            gridColumnDescriptor1.Appearance.ColumnHeaderCell.Font.Size = 9F;
            gridColumnDescriptor1.Appearance.ColumnHeaderCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor1.Appearance.ColumnHeaderWithFilterCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor1.Appearance.RecordFieldCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor1.Appearance.RecordFieldCell.TextAlign = Syncfusion.Windows.Forms.Grid.GridTextAlign.Left;
            gridColumnDescriptor1.Appearance.RecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor1.HeaderText = "Processo";
            gridColumnDescriptor1.MappingName = "Processo";
            gridColumnDescriptor1.Width = 250;
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.CellValueType = typeof(System.DateTime);
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.Format = "g";
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center;
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.ShowButtons = Syncfusion.Windows.Forms.Grid.GridShowButtons.Hide;
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor2.Appearance.ColumnHeaderCell.AutoSize = true;
            gridColumnDescriptor2.Appearance.ColumnHeaderCell.ShowButtons = Syncfusion.Windows.Forms.Grid.GridShowButtons.Hide;
            gridColumnDescriptor2.HeaderText = "Data de Abertura";
            gridColumnDescriptor2.MappingName = "Abertura";
            gridColumnDescriptor2.Width = 183;
            gridColumnDescriptor3.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor3.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor3.Appearance.ColumnHeaderCell.AutoSize = true;
            gridColumnDescriptor3.Appearance.ColumnHeaderCell.Font.Size = 9F;
            gridColumnDescriptor3.Appearance.ColumnHeaderCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor3.Appearance.RecordFieldCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor3.Appearance.RecordFieldCell.TextAlign = Syncfusion.Windows.Forms.Grid.GridTextAlign.Left;
            gridColumnDescriptor3.Appearance.RecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor3.Appearance.RecordFieldCell.WrapText = false;
            gridColumnDescriptor3.HeaderText = "Tipo";
            gridColumnDescriptor3.MappingName = "Tipo";
            gridColumnDescriptor3.Width = 351;
            gridColumnDescriptor4.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor4.Appearance.AnyRecordFieldCell.ShowButtons = Syncfusion.Windows.Forms.Grid.GridShowButtons.Hide;
            gridColumnDescriptor4.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor4.Appearance.ColumnHeaderCell.AutoSize = true;
            gridColumnDescriptor4.Appearance.ColumnHeaderCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor4.Appearance.ColumnHeaderCell.ShowButtons = Syncfusion.Windows.Forms.Grid.GridShowButtons.Hide;
            gridColumnDescriptor4.HeaderText = "Status";
            gridColumnDescriptor4.MappingName = "Status";
            gridColumnDescriptor4.Width = 120;
            gridColumnDescriptor5.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor5.Appearance.ColumnHeaderCell.AutoSize = true;
            gridColumnDescriptor5.Appearance.ColumnHeaderCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridColumnDescriptor5.MappingName = "Solicitante";
            this.gridGroupingControl.TableDescriptor.Columns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor[] {
            gridColumnDescriptor1,
            gridColumnDescriptor2,
            gridColumnDescriptor3,
            gridColumnDescriptor4,
            gridColumnDescriptor5});
            this.gridGroupingControl.TableDescriptor.TableOptions.CaptionRowHeight = 29;
            this.gridGroupingControl.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 25;
            this.gridGroupingControl.TableDescriptor.TableOptions.RecordRowHeight = 25;
            this.gridGroupingControl.TableDescriptor.VisibleColumns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor[] {
            new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor("Processo"),
            new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor("Abertura"),
            new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor("Tipo"),
            new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor("Status"),
            new Syncfusion.Windows.Forms.Grid.Grouping.GridVisibleColumnDescriptor("Solicitante")});
            this.gridGroupingControl.TableOptions.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.gridGroupingControl.TableOptions.AllowSortColumns = true;
            this.gridGroupingControl.TableOptions.GridLineBorder = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid, System.Drawing.Color.Black, Syncfusion.Windows.Forms.Grid.GridBorderWeight.ExtraThin);
            this.gridGroupingControl.TableOptions.IndentWidth = 10;
            this.gridGroupingControl.TableOptions.ShowRowHeader = true;
            this.gridGroupingControl.TableOptions.ShowTableIndent = true;
            this.gridGroupingControl.TableOptions.ShowTableIndentAsCoveredRange = false;
            this.gridGroupingControl.TableOptions.ShowTreeLines = false;
            this.gridGroupingControl.Text = "gridGroupingControl1";
            this.gridGroupingControl.TopLevelGroupOptions.ShowCaption = false;
            this.gridGroupingControl.TopLevelGroupOptions.ShowCaptionPlusMinus = true;
            this.gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            this.gridGroupingControl.TopLevelGroupOptions.ShowGroupFooter = false;
            this.gridGroupingControl.TopLevelGroupOptions.ShowGroupHeader = false;
            this.gridGroupingControl.TopLevelGroupOptions.ShowGroupIndentAsCoveredRange = false;
            this.gridGroupingControl.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl.VersionInfo = "15.3460.0.26";
            this.gridGroupingControl.TableControlCurrentCellKeyDown += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlKeyEventHandler(this.gridGroupingControl_TableControlCurrentCellKeyDown);
            this.gridGroupingControl.TableControlCurrentCellKeyUp += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlKeyEventHandler(this.gridGroupingControl_TableControlCurrentCellKeyUp);
            this.gridGroupingControl.TableControlCellClick += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventHandler(this.gridGroupingControl_TableControlCellClick);
            // 
            // Comunicado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 527);
            this.ControlBox = false;
            this.Controls.Add(this.gridGroupingControl);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tituloPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Comunicado";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comunicado";
            this.Shown += new System.EventHandler(this.Comunicado_Shown);
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        public System.Windows.Forms.Label mensagemSistemaLabel;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv confirmarButton;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl;
    }
}