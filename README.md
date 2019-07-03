# prismChallenge
Blue Prism's challenge

This program finds the quickest route from a starting word to an end word, by only changing one character at a time.

The entry point to the application is the static main method, which creates a thread that initialises the 
Search class with all the requiered arguments.

The Search class will get the word list from the dictionary file, check if the given startWord and EndWord are in this list,
and then start a recursive search.

the sudo code for the search it is as follows:

//Find all four character words of the list that differ from starting word by one character
//Save initial list 
//get first element from initial list

*Find endWord
-- recursively --
//get all words that differ by one character
//stop when endWord is found
//keep all starting words in a list
--------------------------------------
-- recursively --
//get next element from initial list
//find enWord
--------------------------------------

//check for the list with the minimum number of changes and assign it as the result


