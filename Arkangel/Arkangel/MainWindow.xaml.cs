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
using System.Diagnostics;
using System.IO;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (mainPanel.Children.ToString() != "Dashboard")
            {
                Dashboard dashboard = new Dashboard();
                mainPanel.Children.Add(dashboard);
            }

            Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
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
            Close();
        }

        private Process myProcess = new Process();

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                myProcess.StartInfo.UseShellExecute = true;
                // You can start any process, HelloWorld is a do-nothing example.
                myProcess.StartInfo.FileName = "..\\..\\Module_py\\keystroke.py";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
                // This code assumes the process you are starting will terminate itself. 
                // Given that is is started without a window so you cannot terminate it 
                // on the desktop, it must terminate itself or you can do it programmatically
                // from this application using the Kill method.
                //myProcess.Kill();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MESSAGE: " + ex.Message);
            }


        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            myProcess.Kill();
           // myProcess.Dispose();
        }
    }
}

