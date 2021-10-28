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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUIprojectMOLK_group4
{
    /// <summary>
    /// Interaction logic for Unmolk.xaml
    /// </summary>
    public partial class Unmolk : Page
    {
        public Unmolk()
        {
            InitializeComponent();
        }

        private void destinationButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                molkDestinationBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
