using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace prismChallenge
{
    class IO
    {
        private String filePath, writeToPath;
        public IO(String filePath, String writeToPath) {

            this.filePath = filePath;
            this.writeToPath = writeToPath;
        }


        public List<String> readFile() {
            
            string[] lines = File.ReadAllLines(filePath);

            List<String> words = new List<string>();

            foreach (String s in lines)
            {

                if (s.Length == 4)
                {
                    //get all four character words
                    words.Add(s.ToLower());

                }
            }

            return words;
        }

        public void writeFile(List<String> resultList, String startWord, String endWord) {

            if (!File.Exists(writeToPath))
            {
                File.Create(writeToPath).Dispose();

                using (TextWriter tw = new StreamWriter(writeToPath))
                {
                    tw.WriteLine(startWord);

                    foreach (String s in resultList) {

                        //write each word to file
                        tw.WriteLine(s);
                     }

                    tw.WriteLine(endWord);
                }

            }
            else if (File.Exists(writeToPath))
            {
                using (TextWriter tw = new StreamWriter(writeToPath))
                {
                    tw.WriteLine(startWord);

                    foreach (String s in resultList)
                    {
                        //write each word to file
                        tw.WriteLine(s);
                    }

                    tw.WriteLine(endWord);
                }
            }
        }

    }
}
