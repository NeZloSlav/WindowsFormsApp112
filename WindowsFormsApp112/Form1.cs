using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApp112
{
    public partial class Form1 : Form
    {
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Database2.mdb";
        public Form1()
        {
            InitializeComponent();

            lblCapcha.Text = GetCapcha();
            
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand
                {
                    CommandText = "SELECT * FROM [Таблица1] WHERE [Логин] = @login AND [Пароль] = @password",
                    Connection = connection
                };

                OleDbParameter[] parameters =
                {
                    new OleDbParameter("@login", tbxLogin.Text),
                    new OleDbParameter("password", tbxPassword.Text)
                };

                command.Parameters.AddRange(parameters);
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.HasRows && (lblCapcha.Text == tbxCapcha.Text))
                {
                    MessageBox.Show("Добро пожаловать");
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует");
                }
                
            }
        }

        private string GetCapcha()
        {
            char[] charArray = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();
            char[] numArray = "1234567890".ToCharArray();

            string text = "";
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                int a = rnd.Next(1, 3);
                if (a == 1)
                {
                    text += charArray[rnd.Next(0, charArray.Length - 1)];
                }
                else
                {
                    text += numArray[rnd.Next(0, numArray.Length - 1)];
                }
            }

            return text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lblCapcha.Text = GetCapcha();
        }
    }
}
