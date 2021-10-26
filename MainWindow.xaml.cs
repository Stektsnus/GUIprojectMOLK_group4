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
using System.Xml;

namespace GUIprojectMOLK_group4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, FileData> SelectedFiles = new Dictionary<string, FileData>();
        string recentDirectory = @"C:/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void molkButton_Click(object sender, RoutedEventArgs e)
        {
            //if (!CheckValidName(molkFolerName.Text))
            //{
            //    Trace.WriteLine("The given name was not valid: " + molkFolerName.Text);
            //    return;
            //}
            Process process = CreateProcess();
            MolkFiles(process, molkFileBox);
        }

        /// <summary>
        /// Creates a cmd process inside the Molk folder.
        /// </summary>
        /// <returns>The process.</returns>
        private Process CreateProcess()
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
            return process;
        }

        private bool MolkFiles(Process process, ItemsControl files)
        {
            
            foreach (FileData file in SelectedFiles.Values.ToList())
            {
                string commandString = $"molk -j \"{molkDestinationBox.Text}\\{molkFolerName.Text}.molk\" \"{file.Path}\"";
                process.StandardInput.WriteLine(commandString);
            }
            return true;
        }

        /// <summary>
        /// Checks the input of a given folder name.
        /// </summary>
        /// <param name="String"></param>
        /// <returns>true if its valid. false if its unvalid.</returns>
        private bool CheckValidName(string String)
        {
            if (String == "")
            {
                return false;
            }
            return true;
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
        private void fileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = recentDirectory;
            openFileDialog.Multiselect = true;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach(string fileName in openFileDialog.FileNames)
                {
                    if (SelectedFiles.ContainsKey(fileName))
                    {
                        continue;
                    }
                    FileData item = new FileData(fileName);
                    SelectedFiles.Add(fileName, item);
                    recentDirectory = System.IO.Path.GetDirectoryName(fileName);
                }
                molkFileBox.ItemsSource = SelectedFiles.Values.ToList();
            }
        }

        private void fileRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(FileData item in molkFileBox.SelectedItems)
            {
                SelectedFiles.Remove(item.Path);
            }
            molkFileBox.ItemsSource = SelectedFiles.Values.ToList();
        }
    }

    //                                 HERE IS UNMOLK CODE



}
