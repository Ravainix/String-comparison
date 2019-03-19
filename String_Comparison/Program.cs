using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace String_Comparison
{
    class Program
    {
        const string path = @"/Users/lukasz/Desktop/Context/Files/sample22.csv";

        static void Main(string[] args)
        {

            Console.WriteLine("Enter delimiter: ");
            var delimiter = Console.ReadLine();
            Console.Clear();

            Comparer comparer = new Comparer();
            FileLoader loader = new FileLoader(path, comparer);

            loader.loadCsv(delimiter);
            loader.saveToFile();
        }
    }
}