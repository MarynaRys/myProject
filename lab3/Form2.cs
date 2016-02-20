using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Npgsql;
namespace lab3
{
    public partial class Form2 : Form
    {
        private Form1 perent;
        public void setPerent(Form1 perent)
        {
            this.perent = perent;
        }

        public Form2()
        {   
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string server = textBox1.Text;
            int port = Int32.Parse(textBox2.Text);
            string database = textBox3.Text;
            string user = textBox4.Text;
            string passwd = textBox5.Text;
            NpgsqlConnection connection = perent.Connect(server, port, database, user, passwd);
            perent.setConnection(connection);
            perent.FillDataGridView1ByDoctor();
            this.Visible = false;

        }

       
      

      
    }
}
