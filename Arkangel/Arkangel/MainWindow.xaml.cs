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

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}

