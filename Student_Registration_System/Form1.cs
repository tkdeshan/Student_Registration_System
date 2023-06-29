using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Student_Registration_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load1();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-EG4GFB6\\SQLEXPRESS; Initial Catalog=Student_Registration_System_db; User ID=sa; Password=TkD58630");
        SqlCommand cmd;
        SqlDataReader read;
        string id;
        bool Mode = true;
        string sql;


        public void Load1()
        {
            try
            {
                sql = "SELECT * FROM student_tbl";
                
                cmd = new SqlCommand(sql, con);
                con.Open();
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while(read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public void getID(string id)
        {
            sql = "SELECT * FROM student_tbl WHERE id = '" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while(read.Read())
            {
                textStuNum.Text = read[1].ToString();
                textIdNum.Text = read[2].ToString();
                textCourse.Text = read[3].ToString();
            }

            con.Close();
        }




        //if the mode is true means add record otherwise update record
        private void button1_Click(object sender, EventArgs e)
        {
            string sNum = textStuNum.Text;
            string idNum = textIdNum.Text;
            string course = textCourse.Text;

            if(Mode == true)
            {
                sql = "INSERT INTO student_tbl (studentNumber, idNumber, course) VALUES (@studentNumber, @idNumber, @course)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@studentNumber", sNum);
                cmd.Parameters.AddWithValue("@idNumber", idNum);
                cmd.Parameters.AddWithValue("@course", course);
                MessageBox.Show("Succsesfully Recorded!!!");
                cmd.ExecuteNonQuery();

                textStuNum.Clear();
                textIdNum.Clear();
                textCourse.Clear();
                textStuNum.Focus();
            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "UPDATE student_tbl SET studentNumber = @studentNumber, idNumber = @idNumber, course = @course WHERE id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@studentNumber", sNum);
                cmd.Parameters.AddWithValue("@idNumber", idNum);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record Updated!!!");
                cmd.ExecuteNonQuery();

                textStuNum.Clear();
                textIdNum.Clear();
                textCourse.Clear();
                textStuNum.Focus();

                Mode = true;
            }
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >=0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();    
                getID(id);
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "DELETE FROM student_tbl WHERE id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted!!!");
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Load1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textStuNum.Clear();
            textIdNum.Clear();
            textCourse.Clear();
            textStuNum.Focus();

            Mode = true;
        }
    }
}
