
namespace vRatServer.Forms
{
    partial class RemoteDesktop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteDesktop));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Showbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Hidebtn = new System.Windows.Forms.Button();
            this.Stopbtn = new System.Windows.Forms.Button();
            this.Startbtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.keyboardOffbtn = new System.Windows.Forms.Button();
            this.keybOnbtn = new System.Windows.Forms.Button();
            this.MouseOffbtn = new System.Windows.Forms.Button();
            this.MouseOnbtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 450);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Showbtn
            // 
            this.Showbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Showbtn.Location = new System.Drawing.Point(337, 133);
            this.Showbtn.Name = "Showbtn";
            this.Showbtn.Size = new System.Drawing.Size(75, 23);
            this.Showbtn.TabIndex = 11;
            this.Showbtn.Text = "Show";
            this.Showbtn.UseVisualStyleBackColor = true;
            this.Showbtn.Visible = false;
            this.Showbtn.Click += new System.EventHandler(this.Showbtn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Hidebtn);
            this.panel1.Controls.Add(this.Stopbtn);
            this.panel1.Controls.Add(this.Startbtn);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.keyboardOffbtn);
            this.panel1.Controls.Add(this.keybOnbtn);
            this.panel1.Controls.Add(this.MouseOffbtn);
            this.panel1.Controls.Add(this.MouseOnbtn);
            this.panel1.Location = new System.Drawing.Point(210, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 127);
            this.panel1.TabIndex = 10;
            // 
            // Hidebtn
            // 
            this.Hidebtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Hidebtn.Location = new System.Drawing.Point(192, 101);
            this.Hidebtn.Name = "Hidebtn";
            this.Hidebtn.Size = new System.Drawing.Size(75, 23);
            this.Hidebtn.TabIndex = 8;
            this.Hidebtn.Text = "Hide";
            this.Hidebtn.UseVisualStyleBackColor = true;
            this.Hidebtn.Click += new System.EventHandler(this.Hidebtn_Click);
            // 
            // Stopbtn
            // 
            this.Stopbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Stopbtn.Location = new System.Drawing.Point(340, 55);
            this.Stopbtn.Name = "Stopbtn";
            this.Stopbtn.Size = new System.Drawing.Size(75, 23);
            this.Stopbtn.TabIndex = 7;
            this.Stopbtn.Text = "Stop";
            this.Stopbtn.UseVisualStyleBackColor = true;
            this.Stopbtn.Click += new System.EventHandler(this.Stopbtn_Click);
            // 
            // Startbtn
            // 
            this.Startbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Startbtn.Location = new System.Drawing.Point(248, 56);
            this.Startbtn.Name = "Startbtn";
            this.Startbtn.Size = new System.Drawing.Size(75, 23);
            this.Startbtn.TabIndex = 6;
            this.Startbtn.Text = "Start";
            this.Startbtn.UseVisualStyleBackColor = true;
            this.Startbtn.Click += new System.EventHandler(this.Startbtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(46, 56);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(156, 23);
            this.comboBox1.TabIndex = 5;
            // 
            // keyboardOffbtn
            // 
            this.keyboardOffbtn.Location = new System.Drawing.Point(329, 12);
            this.keyboardOffbtn.Name = "keyboardOffbtn";
            this.keyboardOffbtn.Size = new System.Drawing.Size(86, 23);
            this.keyboardOffbtn.TabIndex = 4;
            this.keyboardOffbtn.Text = "Keyboard Off";
            this.keyboardOffbtn.UseVisualStyleBackColor = true;
            this.keyboardOffbtn.Click += new System.EventHandler(this.keyboardOffbtn_Click);
            // 
            // keybOnbtn
            // 
            this.keybOnbtn.Location = new System.Drawing.Point(238, 12);
            this.keybOnbtn.Name = "keybOnbtn";
            this.keybOnbtn.Size = new System.Drawing.Size(85, 23);
            this.keybOnbtn.TabIndex = 3;
            this.keybOnbtn.Text = "Keyboard On";
            this.keybOnbtn.UseVisualStyleBackColor = true;
            this.keybOnbtn.Click += new System.EventHandler(this.keybOnbtn_Click);
            // 
            // MouseOffbtn
            // 
            this.MouseOffbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MouseOffbtn.Location = new System.Drawing.Point(127, 12);
            this.MouseOffbtn.Name = "MouseOffbtn";
            this.MouseOffbtn.Size = new System.Drawing.Size(75, 23);
            this.MouseOffbtn.TabIndex = 2;
            this.MouseOffbtn.Text = "MouseOff";
            this.MouseOffbtn.UseVisualStyleBackColor = true;
            this.MouseOffbtn.Click += new System.EventHandler(this.MouseOffbtn_Click);
            // 
            // MouseOnbtn
            // 
            this.MouseOnbtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MouseOnbtn.Location = new System.Drawing.Point(46, 12);
            this.MouseOnbtn.Name = "MouseOnbtn";
            this.MouseOnbtn.Size = new System.Drawing.Size(75, 23);
            this.MouseOnbtn.TabIndex = 1;
            this.MouseOnbtn.Text = "MouseOn";
            this.MouseOnbtn.UseVisualStyleBackColor = true;
            this.MouseOnbtn.Click += new System.EventHandler(this.MouseOnbtn_Click);
            // 
            // RemoteDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Showbtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "RemoteDesktop";
            this.Text = "RemoteDesktop";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RemoteDesktop_FormClosed);
            this.Load += new System.EventHandler(this.RemoteDesktop_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RemoteDesktop_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RemoteDesktop_KeyUp);
            this.Resize += new System.EventHandler(this.RemoteDesktop_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Showbtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Stopbtn;
        private System.Windows.Forms.Button Startbtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button keyboardOffbtn;
        private System.Windows.Forms.Button keybOnbtn;
        private System.Windows.Forms.Button MouseOffbtn;
        private System.Windows.Forms.Button MouseOnbtn;
        private System.Windows.Forms.Button Hidebtn;
    }
}