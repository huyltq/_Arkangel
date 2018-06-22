﻿using System;
using System.Windows;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Interop;
<<<<<<< HEAD
using System.Diagnostics;

=======
using System.Timers;
using System.Diagnostics;
using System.Data.SQLite;
>>>>>>> 2c6da9611ec36095b3686fefd8fddd14d1361ba8

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
        public static System.Timers.Timer aTimer_scrshot;
        public static void SetTimer(int _time)
        {
            // Create a timer with a two second interval.
            aTimer_scrshot = new System.Timers.Timer(_time);
            // Hook up the Elapsed event for the timer. 
            aTimer_scrshot.Elapsed += OnTimedEvent;
            aTimer_scrshot.AutoReset = true;
            aTimer_scrshot.Enabled = true;
        }
        public static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "screenshot.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }
        public MainWindow()
        {

            InitializeComponent();
           // ProcessStartInfo _startkeylog = startkeylog();
            if (mainPanel.Children.ToString() != "Dashboard")
            {
                Dashboard dashboard = new Dashboard();
                mainPanel.Children.Add(dashboard);
            }
<<<<<<< HEAD

            Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
=======
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

            int _hours=0,_minutes=0,enable=0;
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT * FROM Screenshot,current_user WHERE Screenshot.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {
                        
                        int.TryParse(data["enable"].ToString(),out enable);
                        int.TryParse(data["hours"].ToString(), out _hours);
                        int.TryParse(data["minutes"].ToString(), out _minutes);
                    }
                }
            }
            if (enable == 1)
            {
                SetTimer(_hours * 60 * 60 * 1000 + _minutes * 60 * 1000);
                aTimer_scrshot.Start();
            }

            //Mail


            //FTP

          //  Closing += new System.ComponentModel.CancelEventHandler(windowl);
>>>>>>> 2c6da9611ec36095b3686fefd8fddd14d1361ba8

        }
        private IntPtr _windowHandle;
        private HwndSource _source;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);
<<<<<<< HEAD

=======
>>>>>>> 2c6da9611ec36095b3686fefd8fddd14d1361ba8
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

<<<<<<< HEAD
      
=======
            
       
>>>>>>> 2c6da9611ec36095b3686fefd8fddd14d1361ba8

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
<<<<<<< HEAD
=======

>>>>>>> 2c6da9611ec36095b3686fefd8fddd14d1361ba8

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
<<<<<<< HEAD

=======
>>>>>>> 2c6da9611ec36095b3686fefd8fddd14d1361ba8
    }
}

