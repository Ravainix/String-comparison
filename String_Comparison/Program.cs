using System;
using System.IO;

namespace String_Comparison
{
    class Program
    {
        //const string path = @"/Users/lukasz/Projects/String_Comparison/String_Comparison/sample22.csv";

        static void Main(string[] args)
        {

            Console.WriteLine("Working directory: " + Directory.GetCurrentDirectory());
            Console.WriteLine();
            FileLoader loader = new FileLoader();


            loader.searchForFile();

            loader.loadCsv();
            loader.saveToFile();

            //loader.loadCsv2(delimiter);
            //loader.proccess();

            Console.ReadKey();

        }
    }
}