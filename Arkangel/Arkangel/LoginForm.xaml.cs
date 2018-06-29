using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
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
           
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * from current_user", connect);
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
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {

                    // check user in Server...


                    //SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT id,username,password FROM Users WHERE username='" + tb_username.Text + "' AND password = '" + tb_password.Password + "'", connect);
                    //SQLiteDataReader r = sqlComm.ExecuteReader();

                    // check user in Server...


                    int count = 0;
                   // string username_ = null;
                    //long Uid = 0;
                    //while (r.Read())
                    //{
                    count = 1;
                    //    username_ = ((string)r["username"]);
                    //    Uid = ((long)r["id"]);
                    //}
                    if (count == 1)
                    {
                        MainWindow bs = new MainWindow();
                        bs._username.Text = tb_username.Text;
                        //SQLiteCommand check_user = new SQLiteCommand(@"UPDATE Users SET username",connect);
                        //SQLiteDataReader check = check_user.ExecuteReader();
                        SQLiteCommand getid = new SQLiteCommand(@"UPDATE current_user SET id=1", connect);
                        getid.ExecuteNonQuery();
                        bs.Show();
                        Close();
                    }
                    else if (count == 0)
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
    }
}
