using System;
using System.IO;
using CsvHelper;

namespace String_Comparison
{
    public class FileLoader
    {
        private string path;
        private TextReader reader;
        private CsvReader csvParser;

        public FileLoader(string path)
        {
            this.path = path;
            reader = new StreamReader(path);
            csvParser = new CsvReader(reader);
        }


    }

}
