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
    /// Interaction logic for Email_General.xaml
    /// </summary>
    public partial class Email_General : UserControl
    {
        public Email_General()
        {
            //check = 1;
            InitializeComponent();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=C:\Users\8460p\Downloads\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT enable,time,upKeystroke,upScrshot,upWebcam,upWebsite,limitSize,clear,zipPasswd FROM Email,current_user WHERE Email.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while(data.Read())
                    {
                        if (data["enable"].ToString() == "1") cb_enable.IsChecked = true; else cb_enable.IsChecked = false;
                        if (data["upKeystroke"].ToString() == "1") cb_upKeystroke.IsChecked = true; else cb_upKeystroke.IsChecked = false;
                        if (data["upScrshot"].ToString() == "1") cb_upScrshot.IsChecked = true; else cb_upScrshot.IsChecked = false;
                        if (data["upWebcam"].ToString() == "1") cb_upWebcam.IsChecked = true; else cb_upWebcam.IsChecked = false;
                        if (data["upWebsite"].ToString() == "1") cb_upWebsite.IsChecked = true; else cb_upWebsite.IsChecked = false;
                        tb_kbs.Text= data["limitSize"].ToString();
                    }
                }
            }
            
        }
    }
}
