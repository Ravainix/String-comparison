using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace String_Comparison
{
    class Program
    {
        static void Main(string[] args)
        {
            Comparer comparer = new Comparer();



            //Console.WriteLine(comparer.LevenshteinDistance("123", "132"));
            //Console.WriteLine(comparer.Similarity("123", "122"));

            using (var reader = new StreamReader("/Users/lukasz/Desktop/Context/Files/sample22.csv"))
                //{
                //    while (!reader.EndOfStream)
                //    {
                //        var line = reader.ReadLine();
                //        Console.WriteLine(line);
                //    }
                //}


                //using (var reader = new StreamReader("sample.csv"))
            using (var csv = new CsvReader(reader))
            {
               
               csv.Configuration.Delimiter = ";";
               var records = new List<Foo>();

                //csv.ReadHeader();
                //Console.WriteLine(csv.Read());
                //Console.WriteLine(csv.ReadHeader());
                while (csv.Read())
                {
                    Console.WriteLine("Proccessing...");
                    var record = new Foo
                    {
                        string1 = csv.GetField(1),
                        string2 = csv.GetField(2)
                    };
                    records.Add(record);
                }

                //var records = csv.GetRecords<Foo>();

                Console.WriteLine(records.Count);
            }


        }
    }

    public class Foo
    {
        public string string1 { get; set; }
        public string string2 { get; set; }
    }
}
