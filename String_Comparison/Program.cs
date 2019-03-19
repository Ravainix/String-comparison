using System;
using System.IO;

namespace String_Comparison
{
    class Program
    {
        //const string path = @"/Users/lukasz/Desktop/Context/Files/sample22.csv";

        static void Main(string[] args)
        {
            Console.WriteLine("Working directory: " + Directory.GetCurrentDirectory());
            Console.WriteLine();
            Console.WriteLine("Enter file name:");
            var fileName = Console.ReadLine();
            string path = string.Concat(Directory.GetCurrentDirectory(), "/", fileName);

            Console.WriteLine("Enter delimiter: ");
            var delimiter = Console.ReadLine();

            Console.WriteLine("Enter position of column with ID: ");
            var posIdColumn = InputHandler.ToNumber(Console.ReadLine());

            Console.WriteLine("Enter position of column with first string: ");
            var posStr1Column = InputHandler.ToNumber(Console.ReadLine());

            Console.WriteLine("Enter position of column with second string: ");
            var posStr2Column = InputHandler.ToNumber(Console.ReadLine());

            Console.WriteLine("Enter file output: ");
            dynamic outFileName = Console.ReadLine();

            Console.Clear();

            Comparer comparer = new Comparer();
            FileLoader loader = new FileLoader(path, comparer);

            loader.loadCsv(delimiter, posIdColumn, posStr1Column, posStr2Column);
            loader.saveToFile(outFileName);
        }
    }
}