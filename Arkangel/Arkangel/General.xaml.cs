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
    /// Interaction logic for General.xaml
    /// </summary>
    
    public partial class General : UserControl
    {
        int check;
        public General()
        {
            check = 0;
            InitializeComponent();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=C:\Users\8460p\Downloads\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT disTaskManager,disRegedit,startup FROM General,current_user WHERE General.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {
                        if (data["disTaskManager"].ToString() == "1") cb_taskmanager.IsChecked = true;
                        else cb_taskmanager.IsChecked = false;
                        if (data["disRegedit"].ToString() == "1") cb_registry.IsChecked = true;
                        else cb_registry.IsChecked = false;
                        if (data["startup"].ToString() == "1") cb_runstart.IsChecked = true;
                        else cb_runstart.IsChecked = false;
                    }
                }
                connect.Close();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);

            //switch (index)
            //{
            //    case 0:
            //        GridMain.Background = Brushes.Aquamarine;
            //        break;
            //}
        }

        private void cb_taskmanager_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_registry_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_runstart_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_hotkey_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            if (check == 1)
            {
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=C:\Users\8460p\Downloads\database.db"))
                {
                    connect.Open();
                    using (SQLiteCommand fmd = connect.CreateCommand())
                    {
                        int task = 0;
                        int res = 0;
                        int start = 0;
                        if (cb_taskmanager.IsChecked.Value == true) task = 1;
                        if (cb_registry.IsChecked.Value == true) res = 1;
                        if (cb_runstart.IsChecked.Value == true) start = 1;
                        SQLiteCommand sqlComm = new SQLiteCommand(@"UPDATE General SET disTaskManager="+task+",disRegedit="+res+",startup="+start+ " WHERE General.id in (select current_user.id from current_user)", connect);
                        sqlComm.ExecuteNonQuery();
                        connect.Close();
                    }
                }
            }
        }
    }
}
