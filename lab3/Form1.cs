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
    public partial class Form1 : Form
    {   //Ссылка на подключение
        private NpgsqlConnection connection = null;
        //Ссылка на DataSet
        private DataSet dataSet = null;
        //Ссылки на DataAdapter для группы и студента
        private NpgsqlDataAdapter groupDataAdapter = null;
        private NpgsqlDataAdapter studentDataAdapter = null;
        //Ссылки на вспомогательные формы
        private Form2 form2 = null;
        private Doctor form3 = null;
        private Patient form4 = null;

        public void setConnection(NpgsqlConnection connection)
        {
            this.connection = connection;
        }


        public Form1()
        {
            InitializeComponent();
        }
        //Создание DataSet
        private DataSet getDataSet()
        {
            if (dataSet == null)
            {
                dataSet = new DataSet();
                dataSet.Tables.Add("Doctor");
                dataSet.Tables.Add("Patient");
            }
            return dataSet;
        }

        //Получить форму установления соединения
        public Form2 getForm2()
        {
            if (form2 == null)
            {
                form2 = new Form2();
                form2.setPerent(this);
            }
            return form2;
        }


        public NpgsqlConnection Connect(string host, int port, string database,string user, string parol)
        {
            NpgsqlConnectionStringBuilder stringBuilder = new NpgsqlConnectionStringBuilder();
            stringBuilder.Host = host;
            stringBuilder.Port = port;
            stringBuilder.UserName = user;
            stringBuilder.Password = parol;
            stringBuilder.Database = database;
            stringBuilder.Timeout = 30;
            NpgsqlConnection connection =new NpgsqlConnection(stringBuilder.ConnectionString);
            connection.Open();
            return connection;
        }


        //Заполнить DataGridView1 студентами
        public void FillDataGridView1ByDoctor()
        {
            getDataSet().Tables["Doctor"].Clear();
            groupDataAdapter = new NpgsqlDataAdapter("SELECT * FROM Doctor", connection);
            new NpgsqlCommandBuilder(groupDataAdapter);
            groupDataAdapter.Fill(getDataSet(), "Doctor");
            dataGridView1.DataSource = getDataSet().Tables["Doctor"];
        }

        //Заполнить DataGridView2 patient заданной группы
        public void FillDataGridView2ByPatient(string doctorName)
        {
            getDataSet().Tables["Patient"].Clear();
            studentDataAdapter = new NpgsqlDataAdapter(
                 "SELECT Patient.id, firstname, lastname, sex, age, doctorid " +
                 "FROM Doctor,Patient " +
                // "WHERE Doctor.id = Patient.doctorid AND fio = '"  + fio + "'", connection);
             "WHERE Doctor.id = Patient.doctorid ", connection);
                    
            new NpgsqlCommandBuilder(studentDataAdapter);
            studentDataAdapter.Fill(dataSet, "Patient");
            dataGridView2.DataSource = getDataSet().Tables["Patient"];
        }



        //Получить форму добавления и замены doctor
        public Doctor getForm3()
        {
            if (form3 == null)
            {
                form3 = new Doctor();
                form3.setPerent(this);
            }
            return form3;
        }


        //Добавить doctor
        public void AddDoctor(string fio,string speciality, string feat)
        {
            getDataSet().Tables["Doctor"].Rows.Add(0, fio,speciality, feat);
            groupDataAdapter.Update(getDataSet(), "Doctor");
        }

        //Заменить doctor
        public void UpdateDoctor(int row, string fio,string speciality, string feat)
        {
            getDataSet().Tables["Doctor"].Rows[row]["fio"] = fio;
            getDataSet().Tables["Doctor"].Rows[row]["speciality"] = speciality;
            getDataSet().Tables["Doctor"].Rows[row]["feat"] = feat;
            groupDataAdapter.Update(getDataSet(), "Doctor");
        }


        private void источникДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void подключениеКБазеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getForm2().Visible = true;
        }

        private void отключениеОтБазыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connection.Close();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getForm3().Visible = true;
            getForm3().setButton1Visible(true);
            getForm3().setButton2Visible(false);
            getForm3().setTextBox1Text("");
            getForm3().setTextBox2Text("");
            getForm3().setTextBox3Text("");

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
            DialogResult dr = MessageBox.Show("Удалить врача?", "",MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                getDataSet().Tables["Doctor"].Rows[selectedRow].Delete();
                groupDataAdapter.Update(getDataSet(), "Doctor");
                getDataSet().Clear();
                FillDataGridView1ByDoctor();
            }

        }

        private void заменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
            string fio = (string)getDataSet().Tables["Doctor"]
                 .Rows[selectedRow].ItemArray[1];
            string speciality = (string)getDataSet().Tables["Doctor"]
                 .Rows[selectedRow].ItemArray[2];
            string feat = (string)getDataSet().Tables["Doctor"]
                 .Rows[selectedRow].ItemArray[3];
            getForm3().Visible = true;
            getForm3().setButton1Visible(false);
            getForm3().setButton2Visible(true);
            getForm3().setTextBox1Text(fio);
            getForm3().setTextBox2Text(speciality);
            getForm3().setTextBox3Text(feat);
            getForm3().setRow(selectedRow);

        }

        //Получить форму добавления и замены студента
        public Patient getForm4()
        {
            if (form4 == null)
            {
                form4 = new Patient();
                form4.setPerent(this);
            }
            return form4;
        }


        //Добавить patient
        public void AddPatient(string firstName,string secondName, string sex, int age, long doctorId)
        {
            getDataSet().Tables["Patient"].Rows.Add(0, firstName, secondName, sex, age, doctorId);
            studentDataAdapter.Update(getDataSet(), "Patient");
        }

        //Заменить patient
        public void UpdatePatient(int row, string firstName,string secondName, string sex, int age)
        {
            getDataSet().Tables["Patient"].Rows[row]["firstname"] = firstName;
            getDataSet().Tables["Patient"].Rows[row]["lastname"] = secondName;
            getDataSet().Tables["Patient"].Rows[row]["sex"] = sex;
            getDataSet().Tables["Patient"].Rows[row]["age"] = age;
            studentDataAdapter.Update(getDataSet(), "Patient");
        }




        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            getForm4().Visible = true;
            getForm4().setButton1Visible(true);
            getForm4().setButton2Visible(false);
            getForm4().setTextBox1Text("");
            getForm4().setTextBox2Text("");
            getForm4().setTextBox3Text("");
            getForm4().setTextBox4Text("");
            int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
            long doctorId = (long)getDataSet().Tables["Doctor"] .Rows[selectedRow].ItemArray[0];
            string fio = (string)getDataSet().Tables["Doctor"] .Rows[selectedRow].ItemArray[1];
            getForm4().setFio(fio);
            getForm4().setDoctorId(doctorId);

        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView2.SelectedCells[0].RowIndex;
            DialogResult dr = MessageBox.Show("Удалить пациента?", "",MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                getDataSet().Tables["Patient"].Rows[selectedRow].Delete();
                studentDataAdapter.Update(getDataSet(), "Patient");
                string key = (string)dataGridView1.Rows[selectedRow].Cells[1].Value;
                FillDataGridView2ByPatient(key);
            }

        }

        private void заменитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView2.SelectedCells[0].RowIndex;
            string firstName = (string)getDataSet().Tables["Patient"]
                 .Rows[selectedRow].ItemArray[1];
            string secondName = (string)getDataSet().Tables["Patient"]
                 .Rows[selectedRow].ItemArray[2];
            string sex = (string)getDataSet().Tables["Patient"]
                 .Rows[selectedRow].ItemArray[3];
            int age = (int)getDataSet().Tables["Patient"]
                 .Rows[selectedRow].ItemArray[4];
            string sAge = System.Convert.ToString(age);
            getForm4().Visible = true;
            getForm4().setButton1Visible(false);
            getForm4().setButton2Visible(true);
            getForm4().setTextBox1Text(firstName);
            getForm4().setTextBox2Text(secondName);
            getForm4().setTextBox3Text(sex);
            getForm4().setTextBox4Text(sAge);
            getForm4().setRow(selectedRow);
            int selectedRow1 = dataGridView1.SelectedCells[0].RowIndex;
            string fio = (string)getDataSet().Tables["Doctor"]
                 .Rows[selectedRow1].ItemArray[1];
            getForm4().setFio(fio);

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
            string key = (string)dataGridView1.Rows[selectedRow].Cells[1].Value;
            FillDataGridView2ByPatient(key);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
            string key = (string)dataGridView1.Rows[selectedRow].Cells[1].Value;
            FillDataGridView2ByPatient(key);
        }

       

       
    }
}
