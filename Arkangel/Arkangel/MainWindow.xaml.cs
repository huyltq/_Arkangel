using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Interop;

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
        public MainWindow()
        {
            InitializeComponent();
            if (mainPanel.Children.ToString() != "Dashboard")
            {
                Dashboard dashboard = new Dashboard();
                mainPanel.Children.Add(dashboard);
            }
            
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
        //mainpanel.Children.Clear();
        //Dashboard dashboard = new Dashboard();
        //mainpanel.Children.Add(dashboard);


        private void Button_OpenMenu_Click(object sender, RoutedEventArgs e)
        {
            CloseMenu.Visibility = Visibility.Visible;
            OpenMenu.Visibility = Visibility.Collapsed;
            allIcon.Visibility = Visibility.Visible;
        }

        private void Button_CloseMenu_Click(object sender, RoutedEventArgs e)
        {
            CloseMenu.Visibility = Visibility.Collapsed;
            OpenMenu.Visibility = Visibility.Visible;
            allIcon.Visibility = Visibility.Hidden;
            
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

                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                Close();

            }
        }

        private void bt_Quit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        
    }
}

