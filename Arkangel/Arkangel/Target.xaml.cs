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
    /// Interaction logic for Target.xaml
    /// </summary>
    public partial class Target : UserControl
    {
        public Target()
        {
            InitializeComponent();
        }

        private void bt_ByApp_Click(object sender, RoutedEventArgs e)
        {
            Target_ByApp target_ByApp = new Target_ByApp(this);
            target_ByApp.Show();
        }

        private void bt_delete_Click(object sender, RoutedEventArgs e)
        {
            target_list.Items.Remove(target_list.SelectedItem);
        }

        private void bt_byname_Click(object sender, RoutedEventArgs e)
        {
            Target_ByName target_ByName = new Target_ByName(this);
            target_ByName.Show();
               
        }
    }
}
