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
    /// Interaction logic for _Clipboard.xaml
    /// </summary>
    public partial class _Clipboard : UserControl
    {
        public _Clipboard()
        {

            InitializeComponent();
            _text.Text = Clipboard.GetText();
            _text.IsReadOnly = true;
        }

        private void bt_change_Click(object sender, RoutedEventArgs e)
        {
            if (_text.IsReadOnly == false)
            {
                if (_text.Text != "")
                {
                    Clipboard.SetText(_text.Text);
                }
                _text.IsReadOnly = true;
            }
            else _text.IsReadOnly = false;
        }

        private void bt_file_Click(object sender, RoutedEventArgs e)
        {
            CB_File cB_File = new CB_File();
            cB_File.Show();
        }

        private void bt_Picture_Click(object sender, RoutedEventArgs e)
        {
            CB_Pic cB_Pic = new CB_Pic();
            cB_Pic.Show();
        }
    }
}
