﻿using System;
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
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
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
                        if (data["enDelEvery"].ToString() == "1") cb_delete.IsChecked = true; else cb_delete.IsChecked = false;
                        tb_delete_days.Text = data["days"].ToString();
                        if (data["enDelAfterUpload"].ToString() == "1") cb_autodelete.IsChecked = true; else cb_autodelete.IsChecked = false;
                    }
                }
            }
        }

        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    int enable = 0;
                    int hour = 0;
                    int minute = 0;
                    int enDelEvery = 0;
                    int days = 0;
                    int enDelAfterUpload = 0;
                    if (cb_enable.IsChecked.Value == true) enable = 1;
                    if (cb_delete.IsChecked.Value == true) enDelEvery = 1;
                    if (cb_autodelete.IsChecked.Value == true) enDelAfterUpload = 1;
                    if (enable==1&&(!int.TryParse(tb_hours.Text,out hour)||hour<0||!int.TryParse(tb_minutes.Text,out minute)||minute<0||(minute==0&&hour==0)))
                    {
                        MessageBox.Show("Invalid hours, minutes", "Fail");
                    }else
                    if (enDelEvery==1 &&(!int.TryParse(tb_delete_days.Text,out days)||days<=0))
                    {
                        MessageBox.Show("Invalid days", "Fail");
                    }
                    else
                    {
                        SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"UPDATE Webcam SET enable= "+enable+", hours="+hour+", minutes="+minute+ ",enDelEvery="+enDelEvery+",days="+days+",enDelAfterUpload="+enDelAfterUpload+"  WHERE Webcam.id = (SELECT current_user.id FROM current_user)", connect);
                        sqlComm_Alert.ExecuteNonQuery();
                    }
                }
            }
        }

        private void cb_enable_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }

}
