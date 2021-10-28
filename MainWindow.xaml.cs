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
        public string currentPage = "Main";
        public bool leavePage;
        private void Unmolk_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeToWorkPage("Unmolk", "Molk"))
            {
                content.Visibility = Visibility.Hidden;
                mainFrame.NavigationService.Navigate(new Unmolk());
            }
        }

        private void Molk_Click(object sender, RoutedEventArgs e)
        {
            if(ChangeToWorkPage("Molk", "Unmolk"))
            {
                content.Visibility = Visibility.Hidden;
                mainFrame.NavigationService.Navigate(new Molk());
            }
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeToMiscPage())
            {
                content.Visibility = Visibility.Hidden;
                mainFrame.NavigationService.Navigate(new settings());
            }
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeToMiscPage())
            {
                content.Visibility = Visibility.Hidden;
                mainFrame.NavigationService.Navigate(new info());
            }
        }

        private bool ChangeToWorkPage(string workPage, string otherWorkPage)
        {
            if (currentPage == otherWorkPage)
            {
                leavePage = WarningMessage();
                if (leavePage)
                {
                    currentPage = workPage;
                    return true;
                }
            }
            else if (currentPage != workPage)
            {
                currentPage = workPage;
                return true;
            }
            return false;
        }
        private bool ChangeToMiscPage()
        {
            if (currentPage == "Molk" || currentPage == "Unmolk")
            {
                leavePage = WarningMessage();
                if (leavePage)
                {
                    currentPage = "Other";
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }
        
        private bool WarningMessage()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to leave your current progress?", "MOLK/UNMOLK", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
            }
            return false;
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeToMiscPage())
            {
                content.Visibility = Visibility.Visible;
                mainFrame.NavigationService.Navigate(new empty());
            }
        }
    }
}
