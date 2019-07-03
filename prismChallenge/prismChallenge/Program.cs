using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace prismChallenge
{
    class Program
    {
        static void Main(string[] args)
        {

            Thread t = new Thread(FindWord, 8388608);
            t.Start((Object)args);
        }

        static void FindWord(Object args)
        {
            String[] parameters = (String[])args;

           // Search search = new Search("clan", "dora", ""); // clsn, slam
            Search search = new Search(parameters[0], parameters[1], parameters[2], parameters[3]); // clsn, slam
            search.startSearch();
        } 
    }
}



