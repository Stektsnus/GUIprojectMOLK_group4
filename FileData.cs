using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIprojectMOLK_group4
{
    class FileData
    {
        public string Name { get; set; }
        private float _size { get; set; }
        private float _compressedSize { get; set; }
        public string Path { get; set; }
        public FileData() {}
        public FileData(string path) 
        {
            Name = ExtractName(path);
            Path = path;
        }
        public FileData(string name, string path)
        {
            Name = name;
            Path = path;
        }
        public FileData(string name, float size, float compressedSize)
        {
            Name = name;
            _size = size;
            _compressedSize = compressedSize;
        }

        private string ExtractName(string path)
        {
            string[] nameSplit = path.Split('\\');
            return nameSplit[nameSplit.Length - 1];
        }
    }
}
