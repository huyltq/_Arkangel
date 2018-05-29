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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            string partOfDay;
            var hours = DateTime.Now.Hour;
            if (hours > 16)
            {
                partOfDay = "Good Evening";
            }
            else if (hours > 11)
            {
                partOfDay = "Good Afternoon";
            }
            else
            {
                partOfDay = "Good Morning";
            }
            tb_hello.Text = partOfDay;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            General general = new General();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(general);
        }

        

        private void bt_screenshot_Click(object sender, RoutedEventArgs e)
        {
            Screenshot screenshot = new Screenshot();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(screenshot);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Alert alert = new Alert();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(alert);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FTP ftp = new FTP();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(ftp);
        }

        private void btn_General_Click(object sender, RoutedEventArgs e)
        {
            General general = new General();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(general);
        }
    }
}
