namespace Suportte.Biblioteca
{
    partial class Leitor
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
            Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo1 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();
            Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo2 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();
            Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings pdfViewerPrinterSettings1 = new Syncfusion.Windows.PdfViewer.PdfViewerPrinterSettings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Leitor));
            Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings textSearchSettings1 = new Syncfusion.Windows.Forms.PdfViewer.TextSearchSettings();
            this.tituloPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tituloMaximizarLabel = new System.Windows.Forms.Label();
            this.tituloMinimizarLabel = new System.Windows.Forms.Label();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.powerPictureBox = new System.Windows.Forms.PictureBox();
            this.pdfViewerControl = new Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl();
            this.superToolTip1 = new Syncfusion.Windows.Forms.Tools.SuperToolTip(this);
            this.tituloPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloPanel
            // 
            this.tituloPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(19)))), ((int)(((byte)(53)))));
            this.tituloPanel.Controls.Add(this.panel1);
            this.tituloPanel.Controls.Add(this.tituloLabel);
            this.tituloPanel.Controls.Add(this.powerPictureBox);
            this.tituloPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPanel.Location = new System.Drawing.Point(0, 0);
            this.tituloPanel.Name = "tituloPanel";
            this.tituloPanel.Size = new System.Drawing.Size(663, 40);
            this.tituloPanel.TabIndex = 3;
            this.tituloPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tituloMaximizarLabel);
            this.panel1.Controls.Add(this.tituloMinimizarLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(585, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(38, 40);
            this.panel1.TabIndex = 13;
            // 
            // tituloMaximizarLabel
            // 
            this.tituloMaximizarLabel.BackColor = System.Drawing.Color.Transparent;
            this.tituloMaximizarLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tituloMaximizarLabel.Font = new System.Drawing.Font("Webdings", 10F, System.Drawing.FontStyle.Bold);
            this.tituloMaximizarLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tituloMaximizarLabel.Location = new System.Drawing.Point(0, 18);
            this.tituloMaximizarLabel.Name = "tituloMaximizarLabel";
            this.tituloMaximizarLabel.Size = new System.Drawing.Size(38, 22);
            this.tituloMaximizarLabel.TabIndex = 12;
            this.tituloMaximizarLabel.Text = "2";
            this.tituloMaximizarLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            toolTipInfo1.Body.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            toolTipInfo1.Body.Size = new System.Drawing.Size(20, 20);
            toolTipInfo1.Body.Text = "Maximiza a tela";
            toolTipInfo1.Footer.Size = new System.Drawing.Size(20, 20);
            toolTipInfo1.Header.Size = new System.Drawing.Size(20, 20);
            this.superToolTip1.SetToolTip(this.tituloMaximizarLabel, toolTipInfo1);
            this.tituloMaximizarLabel.Click += new System.EventHandler(this.tituloMaximizarLabel_Click);
            // 
            // tituloMinimizarLabel
            // 
            this.tituloMinimizarLabel.BackColor = System.Drawing.Color.Transparent;
            this.tituloMinimizarLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloMinimizarLabel.Font = new System.Drawing.Font("Webdings", 10F, System.Drawing.FontStyle.Bold);
            this.tituloMinimizarLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tituloMinimizarLabel.Location = new System.Drawing.Point(0, 0);
            this.tituloMinimizarLabel.Name = "tituloMinimizarLabel";
            this.tituloMinimizarLabel.Size = new System.Drawing.Size(38, 18);
            this.tituloMinimizarLabel.TabIndex = 11;
            this.tituloMinimizarLabel.Text = "0";
            this.tituloMinimizarLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            toolTipInfo2.Body.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            toolTipInfo2.Body.Size = new System.Drawing.Size(20, 20);
            toolTipInfo2.Body.Text = "Minimiza a tela";
            toolTipInfo2.Footer.Size = new System.Drawing.Size(20, 20);
            toolTipInfo2.Header.Size = new System.Drawing.Size(20, 20);
            this.superToolTip1.SetToolTip(this.tituloMinimizarLabel, toolTipInfo2);
            this.tituloMinimizarLabel.Click += new System.EventHandler(this.tituloMinimizarLabel_Click);
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.tituloLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tituloLabel.Location = new System.Drawing.Point(12, 10);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(118, 21);
            this.tituloLabel.TabIndex = 0;
            this.tituloLabel.Text = "Leitor e-Book";
            this.tituloLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.tituloLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.tituloLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // powerPictureBox
            // 
            this.powerPictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.powerPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("powerPictureBox.Image")));
            this.powerPictureBox.Location = new System.Drawing.Point(623, 0);
            this.powerPictureBox.Name = "powerPictureBox";
            this.powerPictureBox.Size = new System.Drawing.Size(40, 40);
            this.powerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.powerPictureBox.TabIndex = 12;
            this.powerPictureBox.TabStop = false;
            this.powerPictureBox.Click += new System.EventHandler(this.powerPictureBox_Click);
            // 
            // pdfViewerControl
            // 
            this.pdfViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewerControl.EnableContextMenu = true;
            this.pdfViewerControl.EnableNotificationBar = true;
            this.pdfViewerControl.HorizontalScrollOffset = 0;
            this.pdfViewerControl.IsBookmarkEnabled = true;
            this.pdfViewerControl.Location = new System.Drawing.Point(0, 40);
            this.pdfViewerControl.Name = "pdfViewerControl";
            this.pdfViewerControl.PageBorderThickness = 1;
            pdfViewerPrinterSettings1.PageOrientation = Syncfusion.Windows.PdfViewer.PdfViewerPrintOrientation.Auto;
            pdfViewerPrinterSettings1.PageSize = Syncfusion.Windows.PdfViewer.PdfViewerPrintSize.ActualSize;
            pdfViewerPrinterSettings1.PrintLocation = ((System.Drawing.PointF)(resources.GetObject("pdfViewerPrinterSettings1.PrintLocation")));
            this.pdfViewerControl.PrinterSettings = pdfViewerPrinterSettings1;
            this.pdfViewerControl.ReferencePath = null;
            this.pdfViewerControl.ScrollDisplacementValue = 0;
            this.pdfViewerControl.ShowHorizontalScrollBar = true;
            this.pdfViewerControl.ShowToolBar = true;
            this.pdfViewerControl.ShowVerticalScrollBar = true;
            this.pdfViewerControl.Size = new System.Drawing.Size(663, 643);
            this.pdfViewerControl.SpaceBetweenPages = 8;
            this.pdfViewerControl.TabIndex = 5;
            this.pdfViewerControl.Text = "pdfViewerControl1";
            textSearchSettings1.CurrentInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(64)))));
            textSearchSettings1.HighlightAllInstance = true;
            textSearchSettings1.OtherInstanceColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.pdfViewerControl.TextSearchSettings = textSearchSettings1;
            this.pdfViewerControl.ThemeName = "Default";
            this.pdfViewerControl.VerticalScrollOffset = 0;
            this.pdfViewerControl.VisualStyle = Syncfusion.Windows.Forms.PdfViewer.VisualStyle.Default;
            this.pdfViewerControl.ZoomMode = Syncfusion.Windows.Forms.PdfViewer.ZoomMode.FitPage;
            // 
            // superToolTip1
            // 
            this.superToolTip1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            // 
            // Leitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 683);
            this.ControlBox = false;
            this.Controls.Add(this.pdfViewerControl);
            this.Controls.Add(this.tituloPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Leitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leitor";
            this.Load += new System.EventHandler(this.Leitor_Load);
            this.Shown += new System.EventHandler(this.Leitor_Shown);
            this.tituloPanel.ResumeLayout(false);
            this.tituloPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.powerPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tituloPanel;
        public System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.PictureBox powerPictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label tituloMaximizarLabel;
        private System.Windows.Forms.Label tituloMinimizarLabel;
        private Syncfusion.Windows.Forms.Tools.SuperToolTip superToolTip1;
        public Syncfusion.Windows.Forms.PdfViewer.PdfViewerControl pdfViewerControl;
    }
}