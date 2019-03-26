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

            //Console.WriteLine("Enter file name:");
            //var fileName = Console.ReadLine();
            //string path = string.Concat(Directory.GetCurrentDirectory(), "/", fileName);

            Console.WriteLine("Enter delimiter: ");
            var delimiter = Console.ReadLine();



            //Console.WriteLine("Enter file output: ");
            //dynamic outFileName = Console.ReadLine();

            Console.Clear();


            loader.searchForFile();

            loader.loadCsv(delimiter);
            loader.saveToFile();

            //loader.loadCsv2(delimiter);
            //loader.proccess();

            Console.ReadKey();

        }
    }
}