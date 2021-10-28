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
        private float Size { get; set; }
        private float CompressedSize { get; set; }
        public string Path { get; set; }
        public FileData() { }
        public FileData(string name)
        {
            Name = ExtractName(name);
            Path = name;
        }
        public FileData(string name, float size, float compressedSize)
        {
            Name = name;
            Size = size;
            CompressedSize = compressedSize;
        }

        private string ExtractName(string path)
        {
            string[] nameSplit = path.Split('\\');
            return nameSplit[nameSplit.Length - 1];
        }
    }
}