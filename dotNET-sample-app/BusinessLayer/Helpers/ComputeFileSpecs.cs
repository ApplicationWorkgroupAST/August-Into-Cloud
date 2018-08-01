using System;
using System.Text;
using System.Collections.Generic;

namespace BusinessLayer.Helpers
{
    public class ComputeFileSpecs
    {
        public ComputeFileSpecs()
        {
        }

        public FileSpecs getSpecsFromBlobFile(byte[] data, string fileName) {

            var fileContents = Encoding.UTF8.GetString(data);
            return GetSpecsFromStringFile(fileContents);
        }

        public FileSpecs GetSpecsFromStringFile(string file)  {

            Regex.Replace(file, "[^a-zA-Z0-9_. ]+", "", RegexOptions.Compiled);

            string newinfo = sb.ToString();
            string [] words = newinfo.Split(' ');

            Dictionary<string, int> 


        }
    }
}
