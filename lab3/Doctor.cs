using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab3
{
    public partial class Doctor : Form
    {
        private Form1 perent = null;
        private int row;
        private int rrr;
        private int ttt;

        public Doctor()
        {
            InitializeComponent();
        }

        public void setPerent(Form1 perent)
        {
            this.perent = perent;
        }

        public void setRow(int row)
        {
            this.row = row;
        }


        public void setTextBox1Text(string text)
        {
            textBox1.Text = text;
        }

        public void setTextBox2Text(string text)
        {
            textBox2.Text = text;
        }

        public void setTextBox3Text(string text)
        {
            textBox3.Text = text;
        }

        public void setButton1Visible(bool value)
        {
            this.button1.Visible = value;
        }

        public void setButton2Visible(bool value)
        {
            this.button2.Visible = value;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            perent.UpdateDoctor(row, textBox1.Text, textBox2.Text, textBox3.Text);
            perent.FillDataGridView1ByDoctor();
            this.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            perent.AddDoctor(textBox1.Text, textBox2.Text, textBox3.Text);
            perent.FillDataGridView1ByDoctor();
            this.Visible = false;

        }

        private void Doctor_Load(object sender, EventArgs e)
        {

        }
    }
}
