using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.AccessLayer
{
    public class FileInfo {

        public FileInfo() {}
        public FileInfo(string fileName){
            this.fileName = fileName;
        }
        public string fileName { get; set; }
    }


    //There could be many different implementations of this interface
    public interface IDataAccessObject {

        List<FileInfo> GetInfoOnFiles();
        string GetFileAsString(string fileName);
        void PutFile(string fileName, string fileContents);
    }

    public class DataAccessObject : IDataAccessObject
    {
        private IDictionary<string, string> fileCache = new Dictionary<string, string>();

        public DataAccessObject()
        {
            //build the cache which could be redis at some point
            //The source data could come from a database

            fileCache.Add("burt.txt", "Cool One Go Noles Noles Rule@@@");
            fileCache.Add("kathy.txt", "Keep Linc and Liam going! ");
            fileCache.Add("linc.txt", "I wish Summer Was Longer and Longer and Longer Video Games");
            fileCache.Add("liam.txt", "Cool One Go Noles Noles Rule@@@ Robox and Video Games!!");
            fileCache.Add("extra.txt", "But    Ho._$% Was here     today?d But$Burt?But But");
        }

        public List<FileInfo> GetInfoOnFiles() {

            //Linq
            return fileCache.Select(infz => new FileInfo(infz.Key)).ToList<FileInfo>();
        }

        public string GetFileAsString(string fileName) {

            string myFile = "";

            if (fileCache.TryGetValue(fileName, out myFile)) {
                return myFile;
            }
            else {
                return "";
            }
        }

        public void PutFile(string fileName, string fileContents) {

            if (!fileCache.ContainsKey(fileName))
            {
                fileCache.Add(fileName, fileContents);
            }
        }
    }
}
