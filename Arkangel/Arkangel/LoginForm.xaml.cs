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

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            //    try
            //    {
            //        using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\8460p\Downloads\database.db;Version=3"))
            //        {
            //            conn.Open();
            //            using (SQLiteCommand cmd = new SQLiteCommand("SELECT username,password FROM Users WHERE username='@username' AND password = '@password'", conn))
            //            {
            //                cmd.Parameters.AddWithValue("@username", tb_username.Text);
            //                cmd.Parameters.AddWithValue("@password", tb_password.Password);
            //                SQLiteDataReader r = cmd.ExecuteReader();
            //                int count=0;
            //                //Console.WriteLine( tb_username.Text);
            //                //Console.WriteLine(tb_password.Password);
            //                ////Console.WriteLine(r.Read());
            //                //r.GetInt32(count);
            //                //Console.WriteLine(count);
            //                Console.WriteLine(r.Read());
            //                Console.WriteLine((string) r["username"]);
            //                while (r.Read())
            //                {
            //                    count = 1;
            //                }
            //                if (count == 1)
            //                {
            //                    MainWindow bs = new MainWindow();
            //                    bs.Show();
            //                    Hide();
            //                }
            //                else if (count == 0)
            //                {
            //                    MessageBox.Show("Invalid Username or Password");
            //                }

            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=C:\Users\8460p\Downloads\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT username,password FROM Users WHERE username='"+tb_username.Text+"' AND password = '"+tb_password.Password+"'",connect);
                    SQLiteDataReader r = sqlComm.ExecuteReader();
                    int count = 0;
                    while (r.Read())
                    {
                        count = 1;
                    }
                    if (count == 1)
                    {
                        MainWindow bs = new MainWindow();
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
    }
}
