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
    /// Interaction logic for Email.xaml
    /// </summary>
    public partial class Email : UserControl
    {
        int check;
        Email_General general = new Email_General();
        Email_Server server = new Email_Server();
        public Email()
        {
            
            InitializeComponent();
            check = 1;
            GridMain.Children.Clear();
            GridMain.Children.Add(general);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            
            GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(general);
                    check = 1;
                    break;
                case 1:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(server);
                    check = 2;
                    break;

            }
        }

        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            if (check == 1)
            {
                //if (data["enable"].ToString() == "1") cb_enable.IsChecked = true; else cb_enable.IsChecked = false;
                //if (data["upKeystroke"].ToString() == "1") cb_upKeystroke.IsChecked = true; else cb_upKeystroke.IsChecked = false;
                //if (data["upScrshot"].ToString() == "1") cb_upScrshot.IsChecked = true; else cb_upScrshot.IsChecked = false;
                //if (data["upWebcam"].ToString() == "1") cb_upWebcam.IsChecked = true; else cb_upWebcam.IsChecked = false;
                //if (data["upWebsite"].ToString() == "1") cb_upWebsite.IsChecked = true; else cb_upWebsite.IsChecked = false;
                //tb_kbs.Text = data["limitSize"].ToString();
                int enable = 0;
                int upKeyStroke = 0;
                int upScrshot = 0;
                int upWebcam = 0;
                int upWebsite = 0;
                string limitSize ="";
                

                     
                if (general.cb_enable.IsChecked.Value == true) enable = 1;
                if (general.cb_upKeystroke.IsChecked.Value == true) upKeyStroke = 1;
                if (general.cb_upScrshot.IsChecked.Value == true) upWebcam = 1;
                if (general.cb_upWebsite.IsChecked.Value == true) upWebsite = 1;
                if (general.cb_upWebcam.IsChecked.Value == true) upWebcam = 1;
                //Console.WriteLine("OK");
                
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=C:\Users\8460p\Downloads\database.db"))
                {
                    connect.Open();
                    using (SQLiteCommand fmd = connect.CreateCommand())
                    {
                        
                        
                            //SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"UPDATE Email SET enable="+enable+",upKeystroke="+upKeyStroke+",upScrshot="+upScrshot+",upWebcam="+upWebcam+",upWebsite="+upWebsite+",limitSize="+limitSize+" FROM Email,current_user WHERE Email.id = current_user.id", connect);
                            //sqlComm_Alert.ExecuteNonQuery();
                    }
                }
            }
            else
            {

            }
        }
    }
}
