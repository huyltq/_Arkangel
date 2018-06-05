using Microsoft.Win32;
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
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=.\database.db"))
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
        public static void RunStartUp()
        {
            RegistryKey regkey = Registry.CurrentUser.CreateSubKey("Software\\ListenToUser");
            RegistryKey regstart = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            string keyvalue = "1";
            try
            {
                regkey.SetValue("Index", keyvalue);
                regstart.SetValue("ListenToUser",System.Windows.Forms.Application.StartupPath + "\\" + System.Windows.Forms.Application.ProductName + ".exe");
                regkey.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void DontRunStartUp()
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey("Software\\ListenToUser", true);
            RegistryKey regstart = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            try
            {
                regkey.DeleteValue("Index", false);
                regstart.DeleteValue("ListenToUser", false);
                regkey.Close();
                regstart.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void ToggleTaskManager(string keyValue)
        {
            RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            objRegistryKey.SetValue("DisableTaskMgr", keyValue);
            objRegistryKey.Close();
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
