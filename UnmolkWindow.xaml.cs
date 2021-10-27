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
    /// Interaction logic for UnmolkWindow.xaml
    /// </summary>
    public partial class UnmolkWindow : Window
    {
        Dictionary<string, FileData> SelectedFiles = new Dictionary<string, FileData>();
        string MolkFolderPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Molk";
        string recentDirectory = @"C:/";
        public UnmolkWindow()
        {
            InitializeComponent();
        }

        private void unmolkButton_Click(object sender, RoutedEventArgs e)
        {
            //if (!CheckValidName(unmolkFolerName.Text))
            //{
            //    Trace.WriteLine("The given name was not valid: " + unmolkFolerName.Text);
            //    return;
            //}
            Process process = CreateProcess();
            unmolkFiles(process, unmolkFileBox);
        }

        /// <summary>
        /// Creates a cmd process inside the molk folder.
        /// </summary>
        /// <returns>The process.</returns>
        private Process CreateProcess()
        {
            Process process = new Process();
            string path = MolkFolderPath;
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

        private bool unmolkFiles(Process process, ItemsControl files)
        {
            if(destinationBox.Text == "")
            {
                return false;
            }
            List<FileData> chosenFiles = new List<FileData>();
            foreach(FileData item in unmolkFileBox.SelectedItems)
            {
                chosenFiles.Add(item);
            }
            foreach (FileData file in SelectedFiles.Values.ToList().Except(chosenFiles))
            {
                string commandString = $"unmolk \"{chosenMolkFolder.Text}\" -d \"{destinationBox.Text}\" -x \"{file.Name}\"";
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
                destinationBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void removeFileButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(FileData item in unmolkFileBox.SelectedItems)
            {
                SelectedFiles.Remove(item.Path);
            }
            unmolkFileBox.ItemsSource = SelectedFiles.Values.ToList();
        }

        private void unmolkFileButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void addFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = recentDirectory;
            openFileDialog.Filter = "Molk Files|*.molk";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                chosenMolkFolder.Text = openFileDialog.FileName;
                SelectedFiles = new Dictionary<string, FileData>();
                ProcessMolkFileContent(openFileDialog.FileName);
                
                unmolkFileBox.ItemsSource = SelectedFiles.Values.ToList();
            }
        }

        private void ProcessMolkFileContent(string fileName)
        {
            Process process = CreateProcess();
            process.Start();
            ConvertOutputToFileDataObjects(process, fileName);
        }

        private void ConvertOutputToFileDataObjects(Process process, string fileName)
        {

            string commandString = $"unmolk -l {fileName}";
            string tempDataFileName = "tempfile.txt";
            using (StreamWriter sw = new StreamWriter($"{MolkFolderPath}\\{tempDataFileName}", true)) {
            }
            process.StandardInput.WriteLine($"{commandString} > {MolkFolderPath}\\{tempDataFileName}");
            Dictionary<string, FileData> newFileDataObjects = ExtractDataFromTextFile($"{MolkFolderPath}\\{tempDataFileName}");
            foreach(KeyValuePair<string, FileData> fileDataObject in newFileDataObjects)
            {
                fileDataObject.Value.Path = $"{fileName}\\{fileDataObject.Key}";
                SelectedFiles.Add(fileDataObject.Key, fileDataObject.Value);
            }
            string teststring = $"{MolkFolderPath}\\{tempDataFileName}";
            File.Delete($"{MolkFolderPath}\\{tempDataFileName}");
        }

        private Dictionary<string, FileData> ExtractDataFromTextFile(string filePath)
        {
            List<string> readData = new List<string>();
            StreamReader sr = new StreamReader(filePath);
            string line = sr.ReadLine();
            while(line != null)
            {
                readData.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
            Dictionary<string, FileData> newDataObjects = new Dictionary<string, FileData>();
            for(var i = 3; i < readData.Count - 2; i++)
            {
                string[] splitData = readData[i].Split(' ');
                string fileName = splitData[splitData.Length - 1];
                FileData extractedFile = new FileData(fileName);
                newDataObjects.Add(extractedFile.Name, extractedFile);
            }
            
            return newDataObjects;
        }
    }

    //                                 HERE IS UNMOLK CODE



}
