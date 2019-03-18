using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace String_Comparison
{
    class Program
    {
        private const string Path = @"C:\Users\PC1\Desktop\String\sample2.csv";

        static void Main(string[] args)
        {
            Comparer comparer = new Comparer();

            using (StreamReader reader = new StreamReader(Path))
            using (var csv = new CsvReader(reader))
            {
               
               csv.Configuration.Delimiter = ";";
               var records = new List<Foo>();
                csv.Read();
                csv.ReadHeader();
                //Console.WriteLine(csv.Read());
                //Console.WriteLine(csv.ReadHeader());
                while (csv.Read())
                {
                    var string1 = csv.GetField(0);
                    var distance = comparer.LevenshteinDistance(csv.GetField(1), csv.GetField(2));
                    var similarity = comparer.Similarity(csv.GetField(1), csv.GetField(2));

                    Console.WriteLine(String.Format("Proccessing {0}; {1}; {2}%", string1, distance, similarity*100));
                    //var record = new Foo
                    //{
                    //    string1 = csv.GetField(1),
                    //    string2 = csv.GetField(2)
                    //};
                    //records.Add(record);
                }

                //var records = csv.GetRecords<Foo>();

               // Console.WriteLine(records[0].string1);
            }

            Console.ReadKey();

        }
    }

    public class Foo
    {
        public string string1 { get; set; }
        public string string2 { get; set; }
    }
}
