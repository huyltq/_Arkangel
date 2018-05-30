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
    /// Interaction logic for Email.xaml
    /// </summary>
    public partial class Email : UserControl
    {
        public Email()
        {
            InitializeComponent();
            Email_General general = new Email_General();
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
                    Email_General general = new Email_General();
                    GridMain.Children.Clear();
                    GridMain.Children.Add(general);
                    break;
                case 1:
                    Email_Server server = new Email_Server();
                    GridMain.Children.Clear();
                    GridMain.Children.Add(server);
                    break;

            }
        }
    }
}
