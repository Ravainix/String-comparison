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

                            Console.WriteLine();
                            Console.WriteLine("Press any key to exit...");
                            Console.ReadKey();
                        }
                    }
                }
            } catch (Exception a)
            {
                Console.WriteLine("Cannot find file...");
                Console.WriteLine(a);

                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
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

                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
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

                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

            if(paths.Length > 1)
            {
                Console.WriteLine();

                for (var i = 0; i < paths.Length; ++i)
                    Console.WriteLine((i + 1) + ". " + Path.GetFileName(paths[i]));


                Console.WriteLine("Enter file index:");
                var index = InputHandler.ToNumber(Console.ReadLine(), paths.Length);
                path = paths[index];
            }
            else
            {
                path = paths[0];
            }
        }


        public void proccess()
        {
            for (var i = 0; i < headers.Length; ++i)
            {
                Console.Write(i + "." + headers[i] + "   ");
            }
            Console.WriteLine(records2[0]);
        }


        public void saveToFile()
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "output")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "output"));
                Console.WriteLine("Output folder created...");
            }

            var pathSave = Path.Combine(Directory.GetCurrentDirectory(), "output" , Path.GetFileNameWithoutExtension(path) + $"_{DateTime.Now:ddMMyyyy-HHmm}.csv");

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

            if (headers.Length == 1)
            {
                Console.WriteLine("Probably entered wrong delimiter...");
            }

            Console.WriteLine("Headers in file:");
            for (var i = 0; i < headers.Length; ++i)
            {
                Console.Write((i + 1) + "." + headers[i] + "   ");
            }
            Console.WriteLine();



            Console.WriteLine("Enter position of column with ID: ");
            posIdColumn = InputHandler.ToNumber(Console.ReadLine(), headers.Length);

            Console.WriteLine("Enter position of column with first string: ");
            posStr1Column = InputHandler.ToNumber(Console.ReadLine(), headers.Length);

            Console.WriteLine("Enter position of column with second string: ");
            posStr2Column = InputHandler.ToNumber(Console.ReadLine(), headers.Length);
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