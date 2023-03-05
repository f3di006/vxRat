
namespace vRatServer.Forms
{
    partial class FileManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileManagerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DriveCombo = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.FilesList = new System.Windows.Forms.ListView();
            this.NameFile = new System.Windows.Forms.ColumnHeader();
            this.SizeFile = new System.Windows.Forms.ColumnHeader();
            this.TypeFile = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drive";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Path";
            // 
            // DriveCombo
            // 
            this.DriveCombo.FormattingEnabled = true;
            this.DriveCombo.Location = new System.Drawing.Point(52, 15);
            this.DriveCombo.Name = "DriveCombo";
            this.DriveCombo.Size = new System.Drawing.Size(121, 23);
            this.DriveCombo.TabIndex = 2;
            this.DriveCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(259, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(271, 23);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // FilesList
            // 
            this.FilesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameFile,
            this.SizeFile,
            this.TypeFile});
            this.FilesList.ContextMenuStrip = this.contextMenuStrip1;
            this.FilesList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FilesList.FullRowSelect = true;
            this.FilesList.HideSelection = false;
            this.FilesList.Location = new System.Drawing.Point(0, 50);
            this.FilesList.Name = "FilesList";
            this.FilesList.Size = new System.Drawing.Size(664, 542);
            this.FilesList.TabIndex = 4;
            this.FilesList.UseCompatibleStateImageBehavior = false;
            this.FilesList.View = System.Windows.Forms.View.Details;
            this.FilesList.SelectedIndexChanged += new System.EventHandler(this.FilesList_SelectedIndexChanged);
            this.FilesList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FilesList_MouseDoubleClick);
            // 
            // NameFile
            // 
            this.NameFile.Text = "Name";
            this.NameFile.Width = 200;
            // 
            // SizeFile
            // 
            this.SizeFile.Text = "Size";
            this.SizeFile.Width = 200;
            // 
            // TypeFile
            // 
            this.TypeFile.Text = "Type";
            this.TypeFile.Width = 200;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.uploadToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.executeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 114);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uploadToolStripMenuItem.Text = "Upload";
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.uploadToolStripMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.executeToolStripMenuItem.Text = "Execute";
            this.executeToolStripMenuItem.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "Upload File";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.DriveCombo);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(664, 53);
            this.panel1.TabIndex = 5;
            // 
            // FileManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 592);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.FilesList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileManagerForm";
            this.Text = "FileManager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.FileManager_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox DriveCombo;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.ListView FilesList;
        private System.Windows.Forms.ColumnHeader NameFile;
        private System.Windows.Forms.ColumnHeader SizeFile;
        private System.Windows.Forms.ColumnHeader TypeFile;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
    }
}