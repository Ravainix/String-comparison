using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace String_Comparison
{
    public class FileLoader
    {
        private string path;
        private Comparer comparer;

        public List<Record> records = new List<Record>();

        public dynamic records2;

        public string[] headers;

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
                    Console.WriteLine(csv.Context.HeaderRecord[1]);
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

        public void loadCsv2(string delimiter)
        {
            Console.WriteLine("Starting reading csv...");
            try
            {
                using (StreamReader reader = new StreamReader(path))
                using (var csv = new CsvReader(reader))
                {
                    Console.WriteLine("CSV found...");

                    csv.Configuration.Delimiter = delimiter;
                    csv.Read();
                    csv.ReadHeader();
                    headers = csv.Context.HeaderRecord;

                   
                    Console.WriteLine();

                    Console.WriteLine("Headers loaded...");

                    records2 = csv.GetRecords<dynamic>().ToList();
                        //Console.WriteLine(String.Format("Proccessing {0}; Distance: {1}; Similarity: {2}", string1, distance, similarity));
                }
            }
            catch
            {
                Console.WriteLine("Cannot find file...");
                //Console.WriteLine(a);
                Environment.Exit(0);
            }
            Console.WriteLine("Successfully loaded " + records2.Count + " records...");
        }

        public void proccess()
        {

            for (var i = 0; i < headers.Length; ++i)
            {
                Console.Write(i + "." + headers[i] + "   ");
            }

            //foreach (var item in records2)
            //{
            //    IDictionary<string, object> propertyValues = item;

            //    foreach (var property in propertyValues.Keys)
            //    {
            //        Console.WriteLine(String.Format("{0} : {1}", property, propertyValues[property]));
            //        Console.WriteLine();
            //    }
            //}

            //foreach(var obj in records2)
            //{
            //    var el = (IDictionary<string, object>)obj;

            //    Console.WriteLine(el["TABLE.OWN_PROD_NAME"]);
            //}

            //Console.WriteLine(records2[0].getProperty("TABLE.OWN_PROD_NAME"));
            Console.WriteLine(records2[0].OWN_PN);


        }


        public void saveToFile()
        {

            var pathSave = Path.Combine(Directory.GetCurrentDirectory(), "output.csv");
            
                //filename = Path.Combine(Directory.GetCurrentDirectory(), filename + ".csv");

            using (var writer = new StreamWriter(pathSave))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(records);
            }

            Console.WriteLine("Exported to " + pathSave);
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