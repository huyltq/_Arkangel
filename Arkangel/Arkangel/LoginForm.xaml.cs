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
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void btn_Login_Click(object sender, RoutedEventArgs e)
        {
           
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT id,username,password FROM Users WHERE username='"+tb_username.Text+"' AND password = '"+tb_password.Password+"'",connect);
                    SQLiteDataReader r = sqlComm.ExecuteReader();
                    int count = 0;
                    string username_=null;
                    int Uid = 0;
                    while (r.Read())
                    {
                        count = 1;
                        username_ = ((string)r["username"]);
                        Uid = ((int)r["id"]);
                    }
                    if (count == 1)
                    {
                        MainWindow bs = new MainWindow();
                        bs._username.Text = username_;
                        SQLiteCommand getid = new SQLiteCommand(@"UPDATE current_user SET id="+Uid, connect);
                        getid.ExecuteNonQuery();
                        bs.Show();
                        Hide();
                    }
                    else if (count == 0)
                    {
                        MessageBox.Show("Invalid Username or Password");
                    }
                    connect.Close();
                }
            }
        }
        public void enter ()
        {
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT id,username,password FROM Users WHERE username='" + tb_username.Text + "' AND password = '" + tb_password.Password + "'", connect);
                    SQLiteDataReader r = sqlComm.ExecuteReader();
                    int count = 0;
                    string username_ = null;
                    int Uid = 0;
                    while (r.Read())
                    {
                        count = 1;
                        username_ = ((string)r["username"]);
                        Uid = ((int)r["id"]);
                    }
                    if (count == 1)
                    {
                        MainWindow bs = new MainWindow();
                        bs._username.Text = username_;
                        SQLiteCommand getid = new SQLiteCommand(@"UPDATE current_user SET id=" + Uid, connect);
                        getid.ExecuteNonQuery();
                        bs.Show();
                        Hide();
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
