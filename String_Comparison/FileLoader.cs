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
        private int posIdColumn;
        private int posStr1Column;
        private int posStr2Column;
        private string delimiter;


        public List<Record> records = new List<Record>();

        public dynamic records2;

        public string[] headers;

        public FileLoader() { }

        public FileLoader(string path)
        {
            this.path = path;
        }


        public void loadCsv()
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                using (var csv = new CsvReader(reader))
                {
                    Console.WriteLine("CSV found...");
                    Console.WriteLine("Starting reading csv...");
                    Console.WriteLine();
                    Console.WriteLine("Enter delimiter: ");
                    delimiter = Console.ReadLine();

                    csv.Configuration.Delimiter = delimiter;
                    csv.Read();
                    csv.ReadHeader();
                    headers = csv.Context.HeaderRecord;

                    showHeaders();

                    Console.WriteLine("Starting processing...");
                    while (csv.Read())
                    {
                        try
                        {
                            var record = new Record
                            {
                                id = csv.GetField(posIdColumn),
                                string1 = csv.GetField(posStr1Column),
                                string2 = csv.GetField(posStr2Column),
                                distance = Comparer.LevenshteinDistance(csv.GetField(posStr1Column), csv.GetField(posStr2Column)),
                                similarity =  Decimal.Round((decimal)Comparer.Similarity(csv.GetField(posStr1Column), csv.GetField(posStr2Column))*100, MidpointRounding.AwayFromZero).ToString() + "%",
                                substring = Comparer.LongestCommonSubstring(csv.GetField(posStr1Column), csv.GetField(posStr2Column))
                            };
                                
                            records.Add(record);
                        } catch
                        {
                            Console.WriteLine("Cannot process your data, check delimiter...");
                            Console.ReadKey();

                            Environment.Exit(0);
                        }
                    }
                }
            } catch (Exception a)
            {
                Console.WriteLine("Cannot find file...");
                Console.WriteLine(a);

                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.WriteLine("Successfully processed " + records.Count + " records...");
        }


        public void loadCsv2()
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
                Console.ReadKey();

                Environment.Exit(0);
            }
            Console.WriteLine("Successfully loaded " + records2.Count + " records...");
        }


        public void searchForFile()
        {
            Console.WriteLine("Searching for csv file...");
            String[] paths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv");

            if(paths.Length == 0)
            {
                Console.WriteLine("No csv found...");


                Console.ReadKey();
                Environment.Exit(0);
            }

            foreach (var p in paths)
                Console.WriteLine(p);

            path = paths[0];
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
            Console.WriteLine(records2[0]);


        }


        public void saveToFile()
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "output")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "output"));
                Console.WriteLine("Output folder created...");
            }

            var pathSave = Path.Combine(Directory.GetCurrentDirectory(), "output" ,"output.csv");

            //filename = Path.Combine(Directory.GetCurrentDirectory(), filename + ".csv");
            try
            {
                using (var writer = new StreamWriter(pathSave))
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(records);
                }

                Console.WriteLine("Exported to " + pathSave);
            } catch (Exception a)
            {
                Console.WriteLine(a);
            }
            
        }


        private void showHeaders()
        {
            Console.WriteLine();
            Console.WriteLine("Headers in file:");
            for (var i = 0; i < headers.Length; ++i)
            {
                Console.Write((i + 1) + "." + headers[i] + "   ");
            }

            Console.WriteLine();



            Console.WriteLine("Enter position of column with ID: ");
            posIdColumn = InputHandler.ToNumber(Console.ReadLine());

            Console.WriteLine("Enter position of column with first string: ");
            posStr1Column = InputHandler.ToNumber(Console.ReadLine());

            Console.WriteLine("Enter position of column with second string: ");
            posStr2Column = InputHandler.ToNumber(Console.ReadLine());
        }
    }


    public class Record
    {
        public string id { get; set; }
        public string string1 { get; set; }
        public string string2 { get; set; }
        public int distance { get; set; }
        public string similarity { get; set; }
        public string substring { get; set; }
    }
}