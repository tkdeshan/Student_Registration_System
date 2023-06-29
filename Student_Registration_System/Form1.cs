﻿using System;
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
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-EG4GFB6\\SQLEXPRESS; Initial Catalog=Student_Registration_System_db; User ID=sa; Password=TkD58630");
        SqlCommand cmd;
        SqlDataReader read;
        string id;
        bool Mode = true;
        string sql;

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

            }
            con.Close();
        }
    }
}