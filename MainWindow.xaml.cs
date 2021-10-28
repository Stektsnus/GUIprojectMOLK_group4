using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace GUIprojectMOLK_group4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void Unmolk_Click(object sender, RoutedEventArgs e)
        {
            content.Visibility = Visibility.Hidden;
            mainFrame.NavigationService.Navigate(new Unmolk());
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            content.Visibility = Visibility.Hidden;
            mainFrame.NavigationService.Navigate(new settings());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            content.Visibility = Visibility.Visible;
            mainFrame.NavigationService.Navigate(new empty());


        }
    }
}
