using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for CB_Pic.xaml
    /// </summary>
    public partial class CB_Pic : Window
    {
        public CB_Pic()
        {
            InitializeComponent();
            InitializeComponent();
            BitmapSource image = null;
            image = System.Windows.Clipboard.GetImage();
            if (image != null)
            {
                _image.Source = image;
            }
        }
        public BitmapSource SwapClipboardImage(
            BitmapSource replacementImage)
        {
            BitmapSource returnImage = null;
            if (System.Windows.Clipboard.ContainsImage())
            {
                returnImage = System.Windows.Clipboard.GetImage();
                System.Windows.Clipboard.SetImage(replacementImage);
            }
            return returnImage;
        }
        private void bt_change_Click(object sender, RoutedEventArgs e)
        {
            BitmapSource image =null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _image.Source = new BitmapImage(new Uri (fileDialog.FileName));
                 image= new BitmapImage(new Uri(fileDialog.FileName));
                // image file path  
                SwapClipboardImage(image);
            }
        }
    }
}
