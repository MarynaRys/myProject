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
    public partial class Patient : Form
    {
        private Form1 perent = null;
        private string fio;
        private long doctorId;
        private int row;
        private int row2;
        private int row3;

        public Patient()
        {
            InitializeComponent();
        }

        public void setPerent(Form1 perent)
        {
            this.perent = perent;
        }

        public void setFio(string fio)
        {
            this.fio = fio;
        }

        public void setDoctorId(long doctorId)
        {
            this.doctorId = doctorId;
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

        public void setTextBox4Text(string text)
        {
            textBox4.Text = text;
        }

        public void setButton1Visible(bool value)
        {
            this.button1.Visible = value;
        }

        public void setButton2Visible(bool value)
        {
            this.button2.Visible = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            perent.AddPatient(textBox1.Text, textBox2.Text,
          textBox3.Text, Int16.Parse(textBox4.Text), doctorId);
            perent.FillDataGridView2ByPatient(fio);
            this.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            perent.UpdatePatient(row, textBox1.Text, textBox2.Text,
     textBox3.Text, System.Convert.ToInt32(textBox4.Text));
            perent.FillDataGridView2ByPatient(fio);
            this.Visible = false;

        }

       
    }
}
