using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Qazi.Peptides;

namespace ResidueBasedPeptideGeneration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] sep = { ',' };
            string[] targetResidues = textBox1.Text.Split(sep);
            List<string> listOfTargetResidues = new List<string>();
            listOfTargetResidues.AddRange(targetResidues);
            DataTable dt = ResidueBasedPeptideGenerator.ExtractPeptide(richTextBox1.Text, listOfTargetResidues, int.Parse(textBox2.Text), checkBox1.Checked);
            dataGridView1.DataSource = dt;
            label3.Text = dt.Rows.Count.ToString();

        }
    }
}