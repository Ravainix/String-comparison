using System;
using System.Linq;

namespace String_Comparison
{
    public static class InputHandler
    {
        public static int ToNumber(string input)
        {
            int output = 0;
            while(!int.TryParse(input, out output))
            {
                Console.WriteLine("Not valid number. Try again:");
                input = Console.ReadLine();
            }
            return output - 1;
        }

        public static bool isDigit(string input)
        {
            return input.All(char.IsDigit);
        }
    }
}
