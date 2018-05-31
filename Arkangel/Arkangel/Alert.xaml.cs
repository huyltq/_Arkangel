﻿using System;
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
    /// Interaction logic for Alert.xaml
    /// </summary>
    public partial class Alert : UserControl
    {
        
        public Alert()
        {
            InitializeComponent();
        }

        private void bt_add_Click(object sender, RoutedEventArgs e)
        {
            Alert_Add alert_Add = new Alert_Add(this);
            alert_Add.Show();
        }

        private void bt_delete_Click(object sender, RoutedEventArgs e)
        {
            keyword_list.Items.Remove(keyword_list.SelectedItem);
        }
    }
    
}
