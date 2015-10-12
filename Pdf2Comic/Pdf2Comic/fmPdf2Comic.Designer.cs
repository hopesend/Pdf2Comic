namespace Pdf2Comic
{
    public partial class fmPdf2Comic
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmPdf2Comic));
            this.lbFile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbToComic = new System.Windows.Forms.RadioButton();
            this.rbToImage = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btConvert = new System.Windows.Forms.Button();
            this.btSelectFile = new System.Windows.Forms.Button();
            this.pbLoadImage = new System.Windows.Forms.PictureBox();
            this.ofdAbrirPDF = new System.Windows.Forms.OpenFileDialog();
            this.lbNumberPages = new System.Windows.Forms.Label();
            this.lbPages = new System.Windows.Forms.Label();
            this.lvImageList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.borrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pbBarraProgreso = new System.Windows.Forms.ProgressBar();
            this.lbStep = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoadImage)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFile
            // 
            this.lbFile.AutoSize = true;
            this.lbFile.Location = new System.Drawing.Point(38, 34);
            this.lbFile.Name = "lbFile";
            this.lbFile.Size = new System.Drawing.Size(16, 13);
            this.lbFile.TabIndex = 11;
            this.lbFile.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "File:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbToComic);
            this.groupBox1.Controls.Add(this.rbToImage);
            this.groupBox1.Location = new System.Drawing.Point(15, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 74);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conversion Formats";
            // 
            // rbToComic
            // 
            this.rbToComic.AutoSize = true;
            this.rbToComic.Image = ((System.Drawing.Image)(resources.GetObject("rbToComic.Image")));
            this.rbToComic.Location = new System.Drawing.Point(76, 19);
            this.rbToComic.Name = "rbToComic";
            this.rbToComic.Size = new System.Drawing.Size(64, 50);
            this.rbToComic.TabIndex = 1;
            this.rbToComic.TabStop = true;
            this.toolTip1.SetToolTip(this.rbToComic, "Convert To Comic");
            this.rbToComic.UseVisualStyleBackColor = true;
            // 
            // rbToImage
            // 
            this.rbToImage.AutoSize = true;
            this.rbToImage.Image = ((System.Drawing.Image)(resources.GetObject("rbToImage.Image")));
            this.rbToImage.Location = new System.Drawing.Point(6, 19);
            this.rbToImage.Name = "rbToImage";
            this.rbToImage.Size = new System.Drawing.Size(64, 50);
            this.rbToImage.TabIndex = 0;
            this.rbToImage.TabStop = true;
            this.toolTip1.SetToolTip(this.rbToImage, "Convert To Image");
            this.rbToImage.UseVisualStyleBackColor = true;
            // 
            // btConvert
            // 
            this.btConvert.Image = ((System.Drawing.Image)(resources.GetObject("btConvert.Image")));
            this.btConvert.Location = new System.Drawing.Point(496, 58);
            this.btConvert.Name = "btConvert";
            this.btConvert.Size = new System.Drawing.Size(71, 68);
            this.btConvert.TabIndex = 13;
            this.toolTip1.SetToolTip(this.btConvert, "Start Conversion");
            this.btConvert.UseVisualStyleBackColor = true;
            this.btConvert.Click += new System.EventHandler(this.btConvert_Click);
            // 
            // btSelectFile
            // 
            this.btSelectFile.Image = ((System.Drawing.Image)(resources.GetObject("btSelectFile.Image")));
            this.btSelectFile.Location = new System.Drawing.Point(537, 25);
            this.btSelectFile.Name = "btSelectFile";
            this.btSelectFile.Size = new System.Drawing.Size(30, 30);
            this.btSelectFile.TabIndex = 17;
            this.toolTip1.SetToolTip(this.btSelectFile, "Select Pdf File");
            this.btSelectFile.UseVisualStyleBackColor = true;
            this.btSelectFile.Click += new System.EventHandler(this.btAbrirPDF_Click);
            // 
            // pbLoadImage
            // 
            this.pbLoadImage.Location = new System.Drawing.Point(15, 132);
            this.pbLoadImage.Name = "pbLoadImage";
            this.pbLoadImage.Size = new System.Drawing.Size(179, 250);
            this.pbLoadImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLoadImage.TabIndex = 14;
            this.pbLoadImage.TabStop = false;
            // 
            // ofdAbrirPDF
            // 
            this.ofdAbrirPDF.FileName = "openFileDialog1";
            // 
            // lbNumberPages
            // 
            this.lbNumberPages.AutoSize = true;
            this.lbNumberPages.Location = new System.Drawing.Point(50, 387);
            this.lbNumberPages.Name = "lbNumberPages";
            this.lbNumberPages.Size = new System.Drawing.Size(16, 13);
            this.lbNumberPages.TabIndex = 19;
            this.lbNumberPages.Text = "...";
            // 
            // lbPages
            // 
            this.lbPages.AutoSize = true;
            this.lbPages.Location = new System.Drawing.Point(12, 387);
            this.lbPages.Name = "lbPages";
            this.lbPages.Size = new System.Drawing.Size(40, 13);
            this.lbPages.TabIndex = 18;
            this.lbPages.Text = "Pages:";
            // 
            // lvImageList
            // 
            this.lvImageList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvImageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvImageList.ContextMenuStrip = this.contextMenuStrip1;
            this.lvImageList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvImageList.Location = new System.Drawing.Point(200, 132);
            this.lvImageList.Name = "lvImageList";
            this.lvImageList.Size = new System.Drawing.Size(367, 250);
            this.lvImageList.TabIndex = 20;
            this.lvImageList.UseCompatibleStateImageBehavior = false;
            this.lvImageList.View = System.Windows.Forms.View.Details;
            this.lvImageList.ItemActivate += new System.EventHandler(this.lvImageList_ItemActivate);
            this.lvImageList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvImageList_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.borrarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(107, 26);
            // 
            // borrarToolStripMenuItem
            // 
            this.borrarToolStripMenuItem.Name = "borrarToolStripMenuItem";
            this.borrarToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.borrarToolStripMenuItem.Text = "Borrar";
            this.borrarToolStripMenuItem.Click += new System.EventHandler(this.borrarToolStripMenuItem_Click);
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.toolStripMenuItem1,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.archivoToolStripMenuItem.Text = "File";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.abrirToolStripMenuItem.Text = "Open";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.btAbrirPDF_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.salirToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(573, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pbBarraProgreso
            // 
            this.pbBarraProgreso.Location = new System.Drawing.Point(200, 387);
            this.pbBarraProgreso.Name = "pbBarraProgreso";
            this.pbBarraProgreso.Size = new System.Drawing.Size(367, 23);
            this.pbBarraProgreso.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbBarraProgreso.TabIndex = 21;
            this.pbBarraProgreso.Visible = false;
            // 
            // lbStep
            // 
            this.lbStep.AutoSize = true;
            this.lbStep.Location = new System.Drawing.Point(108, 393);
            this.lbStep.Name = "lbStep";
            this.lbStep.Size = new System.Drawing.Size(86, 13);
            this.lbStep.TabIndex = 22;
            this.lbStep.Text = "Saving Images...";
            this.lbStep.Visible = false;
            // 
            // fmPdf2Comic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 415);
            this.Controls.Add(this.lbStep);
            this.Controls.Add(this.pbBarraProgreso);
            this.Controls.Add(this.lvImageList);
            this.Controls.Add(this.lbNumberPages);
            this.Controls.Add(this.lbPages);
            this.Controls.Add(this.btSelectFile);
            this.Controls.Add(this.pbLoadImage);
            this.Controls.Add(this.btConvert);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmPdf2Comic";
            this.Text = "Pdf2Comic v1.0.0";
            this.Load += new System.EventHandler(this.fmPdf2Comic_Load_1);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoadImage)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbToComic;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton rbToImage;
        private System.Windows.Forms.Button btConvert;
        private System.Windows.Forms.PictureBox pbLoadImage;
        private System.Windows.Forms.Button btSelectFile;
        private System.Windows.Forms.OpenFileDialog ofdAbrirPDF;
        private System.Windows.Forms.Label lbNumberPages;
        private System.Windows.Forms.Label lbPages;
        private System.Windows.Forms.ListView lvImageList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem borrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ProgressBar pbBarraProgreso;
        private System.Windows.Forms.Label lbStep;
    }
}

