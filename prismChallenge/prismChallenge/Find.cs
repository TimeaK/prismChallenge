using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace prismChallenge
{
   public class Find
    {
        private const String WILDCARD = @"\D";

        private List<String> usedWords, dict;
        private String currentWord, pattern1, pattern2, pattern3, pattern4;
 
       public Find(String currentWord, List<String> usedWords) {

            this.currentWord = currentWord;
            this.usedWords = usedWords;
            dict = new List<String>();

            //set pattern to find all different words by one character
            setPattern();
        }

        public List<String> find(List<string> dict) {

            this.dict = dict;
            List<String> tmp = new List<String>();

            Regex regex1 = new Regex(pattern1);
            Regex regex2 = new Regex(pattern2);
            Regex regex3 = new Regex(pattern3);
            Regex regex4 = new Regex(pattern4);

            //get all words that differ from currentWord by one character
            foreach (String s in dict)
            {

                Match match1 = regex1.Match(s);
                Match match2 = regex2.Match(s);
                Match match3 = regex3.Match(s);
                Match match4 = regex4.Match(s);

                if ((match1.Success || match2.Success || match3.Success
                    || match4.Success) && !isUsedWord(s))
                {

                    tmp.Add(s);

                }
            }

            tmp = tmp.Distinct().ToList();

           
            return tmp;

        }

        private bool isUsedWord(string s)
        {
            return usedWords.Contains(s);
        }

        private void setPattern()
        {
            StringBuilder strB = new StringBuilder(currentWord);

            pattern1 = WILDCARD + currentWord.Substring(1);
            pattern2 = strB[0] + "" + WILDCARD + currentWord.Substring(2);
            pattern3 = strB[0] + "" + strB[1] + WILDCARD + currentWord.Substring(3);
            pattern4 = strB[0] + "" + strB[1] + "" + strB[2] + "" + WILDCARD;
        }
    }
}
