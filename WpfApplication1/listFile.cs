using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowRenamer
{
    public class listFile
    {
        public string fileName {get; set;}
        public string filePath {get; set;}
        public string fileExt {get; set;}
        public string newFileName {get; set; }
        public string newFilePath {get; set; }


        public listFile(string path,string name, string ext,string newName,string newPath)
        {
            this.fileName = name;
            this.filePath = path;
            this.fileExt = ext;
            this.newFileName = newName;
            this.newFilePath = newPath;

        }
        public override string ToString()
        {
            return "Path: " + filePath + " Name: " + fileName + " Extension: " + fileExt;
        }

        

    }




}
