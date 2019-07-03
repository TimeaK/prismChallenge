using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace prismChallenge
{
    class Search
    {
        private const String VERTICALASCII = "\x007C\x007C";
        private const String HORIZONTALASCII = "\x003D\x003D\x003D\x003D\x003D\x003D\x003D\x003D";

        private List<String> currentList, usedWords, nextList, endWordList, completeList;
        private List<List<String>> allLists;
        private List<KeyValuePair<int, List<String>>> result;
        private String currentWord, startWord, endWord, startWordExist, endWordExist;
        private int iteration, minimumSteps, steps;
        private Boolean isFound, isEmptyList, stop;
        private String[] firstElements;
        private IO io;

        public Search(String startWord, String endWord, String filePath, String writeToPath) {

            this.startWord = startWord;
            this.endWord = endWord;
            allLists = new List<List<string>>();
            result = new List<KeyValuePair<int, List<String>>>();
            usedWords = new List<string>();
            nextList = new List<string>();
            endWordList = new List<string>();
            io = new IO(filePath, writeToPath);

            isFound = false;
            isEmptyList = true;
            stop = false;


            completeList = io.readFile();
            currentWord = startWord;
            usedWords.Add(currentWord);
            endWordList.Add(currentWord);

            setMinimumSteps();

        }

        public void startSearch() {


            if (ListContainsWords())
            {

                Find finder = new Find(currentWord, usedWords);
                currentList = finder.find(completeList);

                //save the first iteration's result
                if (iteration == 0)
                {

                    firstElements = new string[currentList.Count];
                    firstElements = currentList.ToArray();
                }

                if (!isFound)
                {
                    //search for word until endWord is found
                    Processor();
                }

            }
            else {

                Console.WriteLine("Dictionary does not contain: " + startWordExist + " " + endWordExist);
            }

        }

        private bool ListContainsWords()
        {
            //check if dictionary file contains start and end word
            if (completeList.Contains(startWord) &&
                completeList.Contains(endWord))
            {
                startWordExist = "";
                endWordExist = "";
                return true;
            }
            else {

                if (!completeList.Contains(startWord)) {
                    startWordExist = startWord;
                   
                }
                if (!completeList.Contains(endWord)) {

                    endWordExist = endWord;
                  
                }
            }
            return false;
        }

        private void Processor()
        {
            iteration++;
            steps++;

            if (currentList.Contains(endWord))
            {

                isFound = true;
                setStepsMap();
                //check if steps are equal to the minimum requiered
                stopSearch();
                steps = 0;

                if (!stop) {

                    reiterate();
                }

            }
            else {

                if (currentList.Count > 0)
                {

                    processNextElement();
                }
                else {

                    iterateUsedLists();
                }
            }
        }

        private void setStepsMap()
        {
            List<String> tmp = new List<String>(endWordList);
            // stepsMap.Add(steps, endWordList);
            result.Add(new KeyValuePair<int, List<String>>(steps, tmp));
            endWordList.Clear();
            setQuickestRoute();
        }

        private void setQuickestRoute()
        {
            if (result.Count > 1) {
                
                //get the key with the minimum value
                var tmp = result.OrderBy(k => k.Key).FirstOrDefault();
                result.Clear();
                //add the shortest route to result
                result.Add(new KeyValuePair<int, List<String>>(tmp.Key, tmp.Value));
               
            }
        }

        private void setMinimumSteps() {

            char[] start = startWord.ToCharArray();
            char[] end = endWord.ToCharArray();

            for (int i = 0; i <= start.Length - 1; i++) {

                if (start[i] != end[i])
                {

                    //increase number of requered steps
                    minimumSteps++;
                }
            }
        }

        private void stopSearch() {

            //if endWord found in minimum or less 
            //steps (sometimes all steps are found in one list) stop searching
            if (steps <= minimumSteps) { 

                isFound = true;
                stop = true;
                Console.WriteLine("Number of Steps: " + steps + " Number of changes: " + (steps + 1));
                Console.WriteLine("Finished...");
                Console.WriteLine(HORIZONTALASCII);
                printResult();
            }
        }

        private void processNextElement() {

            currentWord = currentList[0];
            endWordList.Add(currentWord);
            currentList.RemoveAt(0);
            allLists.Add(currentList);
            usedWords.Add(currentWord);
            startSearch();
        }

        private void iterateUsedLists()
        {
            if (isEmptyList) {

                //re-assign nextList if this is empty
                //then remove assigned list from collection
                allLists.Remove(assignList());
            }

            if (nextList.Count > 0)
            {
                //get next word for check
                currentWord = nextList[0];
                endWordList.Add(currentWord);
                nextList.RemoveAt(0);

                if (nextList.Count > 0)
                {

                    isEmptyList = false;
                }
                else {

                    isEmptyList = true;
                }

                startSearch();
            }
            
        }

        private List<string> assignList()
        {
            List<String> tmp = new List<String>();

            //assign next list to process if current list is empty
            foreach (List<String> list in allLists) {

                if (list.Count > 0) {

                    if (nextList.Count == 0) {

                        nextList = list;
                        tmp = list;
                    }
                }
            }

            return tmp;
        }

        private void reiterate()
        {
            if (firstElements.Length > 0)
            {

                isFound = false;
                currentWord = firstElements[0];
                endWordList.Add(currentWord);
                //remove first element from array
                removeElementFromArray(currentWord);
                usedWords.Clear();
                usedWords.Add(currentWord);

                startSearch();
            }
            else {

                isFound = true;
                Console.WriteLine("Finished");
                printResult();

            }
        }

        private void printResult() {

            Console.WriteLine(VERTICALASCII + startWord + VERTICALASCII);

            Array.ForEach(result.ToArray(), list => {

             foreach (String s in list.Value) {

                    //write result word on console output
                    Console.WriteLine(VERTICALASCII + s + VERTICALASCII);
                }

            });

            Console.WriteLine(VERTICALASCII + endWord + VERTICALASCII);
            Console.WriteLine(HORIZONTALASCII);

            var resultList = result.First();
            //write txt file to given path
            io.writeFile(resultList.Value, startWord, endWord);

        }

        private void removeElementFromArray(String word) {

            List<string> list = new List<string>(firstElements);

            //removed the used word from list
            list.Remove(word);

            firstElements = list.ToArray();
        }

    }
}
