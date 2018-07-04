﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
            Functions.CheckUser();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {

                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * from current_user", connect);

                    

                //    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT id,username,password FROM Users WHERE username='"+tb_username.Text+"' AND password = '"+tb_password.Password+"'",connect);

                    SQLiteDataReader r = sqlComm.ExecuteReader();
                    while(r.Read())
                    {
                        if (r["id"].ToString()!= "")
                        {
                            SQLiteCommand sqlComm1 = new SQLiteCommand(@"SELECT * from Users", connect);
                            SQLiteDataReader data = sqlComm1.ExecuteReader();
                            MainWindow mainWindow = new MainWindow();
                            while (data.Read())
                            {
                                mainWindow._username.Text = data["username"].ToString();
                            }
                            mainWindow.Show();
                            //mainWindow.Hide();
                            Close();
                        }
                    }
                    connect.Close();
                }
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            enter();
        }
        public void enter ()
        {
            //byte[] salt = Encoding.UTF8.GetBytes("hello");
            //string pwd = Functions.ComputeHash(tb_password.Password, salt);
            //Console.WriteLine("pwd : " + pwd);
            //string exx = "pEVeZYG3jWnlaYlcK2Fd+pCmzVaTjkDKke3SbkTe7f5oZWxsbw==";
            //if (Functions.VerifyHash(tb_password.Password, exx))
            //    Console.WriteLine("Verify hash ok!");
            //else
            //    Console.WriteLine("Verify hash fail!");

            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {

                    // check user in Server...


                    //SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT id,username,password FROM Users WHERE username='" + tb_username.Text + "' AND password = '" + tb_password.Password + "'", connect);
                    //SQLiteDataReader r = sqlComm.ExecuteReader();

                    string _loginlog = Functions.Login(tb_username.Text, tb_password.Password);


                    if (_loginlog!="Error")
                    {
                        MainWindow bs = new MainWindow();

                        bs._username.Text = tb_username.Text;
                        //SQLiteCommand check_user = new SQLiteCommand(@"UPDATE Users SET username",connect);
                        //SQLiteDataReader check = check_user.ExecuteReader();
                        
                        SQLiteCommand getid = new SQLiteCommand(@"UPDATE current_user SET id=1", connect);
                        
                        getid.ExecuteNonQuery();
                        SQLiteCommand upusername = new SQLiteCommand(@"UPDATE Users SET id=1, username= '"+tb_username.Text+"', password='"+tb_password.Password+"' ", connect);

                        upusername.ExecuteNonQuery();
                        ////    bs._username.Text = tb_username.Text;
                        //    SQLiteCommand check_user = new SQLiteCommand(@"SELECT * FROM current_user",connect);
                        //    SQLiteDataReader check = check_user.ExecuteReader();
                        //    while (check.Read())
                        //    {
                        //        if (check["id"].ToString() == null)
                        //        {
                        //            SQLiteCommand getid = new SQLiteCommand(@"insert into current_user values (" + Uid + ")", connect);
                        //            getid.ExecuteNonQuery();
                        //        }
                        //        else
                        //        {
                        //            SQLiteCommand getid = new SQLiteCommand(@"UPDATE current_user SET id=" + Uid, connect);
                        //            getid.ExecuteNonQuery();
                        //        }
                        //    }


                        bs.Show();
                        Close();
                    }
                    else 
                    {
                        MessageBox.Show("Invalid Username or Password");
                    }
                    connect.Close();
                }
            }
        }
        private void tb_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                enter();
            }
        }

        private void tb_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                enter();
            }
        }

        private void bt_signin_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("chrome.exe", "http://www.arkangel.tk/sign-up");
        }
    }
}
