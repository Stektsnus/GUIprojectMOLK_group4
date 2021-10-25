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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();


            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Molk";
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = path,
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            process.StartInfo = startInfo;
            process.Start();

            string commandString = $"molk -j \"{destinationBox.Text}\\archive2.molk\" \"{fileBox.Text}\"";

            process.StandardInput.WriteLine(commandString);
            
        }

        private void destinationButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                destinationBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
        private void fileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileSelected = openFileDialog.FileName;
                fileBox.Text = fileSelected;
            }
        }
    }
}
