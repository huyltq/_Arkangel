using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for Webcam.xaml
    /// </summary>
    public partial class Webcam : UserControl
    {
        public Webcam()
        {
            InitializeComponent();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT * FROM Webcam,current_user WHERE Webcam.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {
                        if (data["enable"].ToString() == "1") cb_enable.IsChecked = true; else cb_enable.IsChecked = false;
                        tb_hours.Text = data["hours"].ToString();
                        tb_minutes.Text = data["minutes"].ToString();
                    }
                }
            }
        }

        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    int enable = 0;
                    int hour = 0;
                    int minute = 0;
                    if (cb_enable.IsChecked.Value == true) enable = 1;
                    if (enable==1&&(!int.TryParse(tb_hours.Text,out hour)||hour<0||!int.TryParse(tb_minutes.Text,out minute)||minute<0||(minute==0&&hour==0)))
                    {
                        MessageBox.Show("Invalid hours, minutes", "Fail");
                    }
                    else
                    {
                        SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"UPDATE Webcam SET enable= "+enable+", hours="+hour+", minutes="+minute+ " WHERE Webcam.id = (SELECT current_user.id FROM current_user)", connect);
                        sqlComm_Alert.ExecuteNonQuery();
                    }
                }
            }
        }
    }

}
