using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLayer.Helpers
{
    public class FileSpecs
    {
        public FileSpecs() {
            FileName = "";
        }

        public FileSpecs(string FileName)
        {
            WordCount = new Dictionary<string, int>();
            this.FileName = FileName;
        }

        public IDictionary<string, int> WordCount { get; set; }
        public string FileName { get; set; }

        public FileSpecs getSpecsFromBlobFile(byte[] data, string fileName)
        {

            var fileContents = Encoding.UTF8.GetString(data);
            return GetSpecsFromStringFile(fileContents);
        }

        public FileSpecs GetSpecsFromStringFile(string file)
        {
            file = Regex.Replace(file, "[^a-zA-Z0-9 ]+", "", RegexOptions.Compiled);

            string newinfo = file.ToString();
            string[] words = newinfo.Split(' ');

            for (int i = 0; i < words.Length; i++) {
                words[i] = words[i].Trim();
            }

            //Do the counts (could use map reduce)
            foreach(string current in words) {
                
                if (WordCount.ContainsKey(current)) {

                    int val = 0;
                    if (WordCount.TryGetValue(current, out val))
                    {
                        WordCount[current] = val + 1;

                    }                    
                }
                else {
                    if (current != "")
                    {
                        WordCount.TryAdd(current, 1);
                    }
                }
            }
            return this;
        }
    }
}
