using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;

namespace String_Comparison
{
    public class FileLoader
    {
        private string path;
        private Comparer comparer;

        public List<Record> records = new List<Record>();

        public FileLoader(string path, Comparer comparer)
        {
            this.path = path;
            this.comparer = comparer;
        }

        public void loadCsv(string delimiter, int positionID, int positionString1, int positionString2)
        {
            Console.WriteLine("Starting reading csv...");
            try
            {
                using (StreamReader reader = new StreamReader(path))
                using (var csv = new CsvReader(reader))
                {
                    Console.WriteLine("Csv has been read...");

                    csv.Configuration.Delimiter = delimiter;
                    csv.Read();
                    csv.ReadHeader();
                    Console.WriteLine("Starting processing...");
                    while (csv.Read())
                    {
                        try
                        {
                            var record = new Record
                            {
                                id = csv.GetField(positionID),
                                string1 = csv.GetField(positionString1),
                                string2 = csv.GetField(positionString2),
                                distance = comparer.LevenshteinDistance(csv.GetField(positionString1), csv.GetField(positionString2)),
                                similarity = comparer.Similarity(csv.GetField(positionString1), csv.GetField(positionString2))
                            };

                            records.Add(record);
                        } catch 
                        {
                            Console.WriteLine("Cannot process your data, check delimiter...");
                            Environment.Exit(0);
                        }
                        //Console.WriteLine(String.Format("Proccessing {0}; Distance: {1}; Similarity: {2}", string1, distance, similarity));
                    }
                }
            } catch
            {
                Console.WriteLine("Cannot find file...");
                //Console.WriteLine(a);
                Environment.Exit(0);
            }
            Console.WriteLine("Successfully processed " + records.Count + " records...");
        }


        public void saveToFile(string filename)
        {
            var savePath = Path.Combine(Directory.GetCurrentDirectory, filename);
            using (var writer = new StreamWriter(savePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(records);
            }

            Console.WriteLine("Exported to " + savePath);
        }
    }

    public class Record
    {
        public string id { get; set; }
        public string string1 { get; set; }
        public string string2 { get; set; }
        public int distance { get; set; }
        public double similarity { get; set; }
    }
}