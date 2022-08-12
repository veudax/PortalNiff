namespace Suportte.Pesquisas
{
    partial class Variaveis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Variaveis));
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor2 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor gridColumnDescriptor3 = new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor();
            this.tituloPanel = new System.Windows.Forms.Panel();
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
            this.tituloPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.tituloPanel.Controls.Add(this.tituloLabel);
            this.tituloPanel.Controls.Add(this.powerPictureBox);
            this.tituloPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloPanel.Name = "tituloPanel";
            this.tituloPanel.Size = new System.Drawing.Size(710, 40);
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
            this.tituloLabel.Size = new System.Drawing.Size(190, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Pesquisa de variáveis";
            this.tituloLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(670, 0);
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
            this.panel3.Location = new System.Drawing.Point(0, 424);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(710, 45);
            this.panel3.TabIndex = 5;
            // 
            // pictureBox44
            // 
            this.pictureBox44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.pictureBox44.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox44.Location = new System.Drawing.Point(0, 41);
            this.pictureBox44.Name = "pictureBox44";
            this.pictureBox44.Size = new System.Drawing.Size(710, 4);
            this.pictureBox44.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox44.TabIndex = 21;
            this.pictureBox44.TabStop = false;
            // 
            // confirmarButton
            // 
            this.confirmarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmarButton.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.confirmarButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.confirmarButton.BeforeTouchSize = new System.Drawing.Size(103, 34);
            this.confirmarButton.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.Flat;
            this.confirmarButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.confirmarButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(38)))), ((int)(((byte)(91)))));
            this.confirmarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirmarButton.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.confirmarButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.confirmarButton.IsBackStageButton = false;
            this.confirmarButton.Location = new System.Drawing.Point(595, 5);
            this.confirmarButton.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.confirmarButton.Name = "confirmarButton";
            this.confirmarButton.OverrideFormManagedColor = true;
            this.confirmarButton.Size = new System.Drawing.Size(103, 34);
            this.confirmarButton.TabIndex = 0;
            this.confirmarButton.Text = "&Confirmar";
            this.confirmarButton.ThemeName = "Metro";
            this.confirmarButton.Click += new System.EventHandler(this.confirmarButton_Click);
            this.confirmarButton.Enter += new System.EventHandler(this.confirmarButton_Enter);
            this.confirmarButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.confirmarButton_KeyDown);
            this.confirmarButton.Validating += new System.ComponentModel.CancelEventHandler(this.confirmarButton_Validating);
            // 
            // gridGroupingControl
            // 
            this.gridGroupingControl.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.SetCurrent;
            this.gridGroupingControl.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridGroupingControl.BackColor = System.Drawing.SystemColors.Control;
            this.gridGroupingControl.ChildGroupOptions.CaptionText = "{CategoryCaption}: {Category}";
            this.gridGroupingControl.ChildGroupOptions.ShowCaption = true;
            this.gridGroupingControl.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            this.gridGroupingControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGroupingControl.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
            this.gridGroupingControl.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
            this.gridGroupingControl.Location = new System.Drawing.Point(0, 40);
            this.gridGroupingControl.Name = "gridGroupingControl";
            this.gridGroupingControl.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl.ShowNavigationBar = true;
            this.gridGroupingControl.Size = new System.Drawing.Size(710, 384);
            this.gridGroupingControl.TabIndex = 6;
            this.gridGroupingControl.TableDescriptor.AllowNew = false;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Right;
            gridColumnDescriptor1.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor1.HeaderText = "Código";
            gridColumnDescriptor1.MappingName = "Codigo";
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor2.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor2.MappingName = "Nome";
            gridColumnDescriptor3.Appearance.AnyRecordFieldCell.AutoSize = true;
            gridColumnDescriptor3.Appearance.AnyRecordFieldCell.CellType = "CheckBox";
            gridColumnDescriptor3.Appearance.AnyRecordFieldCell.CellValueType = typeof(bool);
            gridColumnDescriptor3.Appearance.AnyRecordFieldCell.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center;
            gridColumnDescriptor3.Appearance.AnyRecordFieldCell.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            gridColumnDescriptor3.Appearance.ColumnHeaderCell.AutoSize = true;
            gridColumnDescriptor3.MappingName = "Ativo";
            this.gridGroupingControl.TableDescriptor.Columns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridColumnDescriptor[] {
            gridColumnDescriptor1,
            gridColumnDescriptor2,
            gridColumnDescriptor3});
            this.gridGroupingControl.TableDescriptor.TableOptions.CaptionRowHeight = 29;
            this.gridGroupingControl.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 25;
            this.gridGroupingControl.TableDescriptor.TableOptions.RecordRowHeight = 25;
            this.gridGroupingControl.TableOptions.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.gridGroupingControl.TableOptions.SelectionTextColor = System.Drawing.SystemColors.HighlightText;
            this.gridGroupingControl.Text = "gridGroupingControl1";
            this.gridGroupingControl.TopLevelGroupOptions.ShowCaption = false;
            this.gridGroupingControl.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl.VersionInfo = "16.2460.0.41";
            // 
            // Variaveis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 469);
            this.ControlBox = false;
            this.Controls.Add(this.gridGroupingControl);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tituloPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Variaveis";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bancos";
            this.Load += new System.EventHandler(this.Variaveis_Load);
            this.Shown += new System.EventHandler(this.Variaveis_Shown);
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
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox44;
        private Syncfusion.Windows.Forms.ButtonAdv confirmarButton;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl;
    }
}