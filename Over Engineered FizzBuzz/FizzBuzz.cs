using System;
using System.Linq;

namespace Over_Engineered_FizzBuzz
{
    public static class FizzBuzz
    {
        /// <summary>
        /// Runs the FizzBuzz logic
        /// Counts up to 'properties.iterations' and prints a word form a DivisorWord pair when applicable
        /// </summary>
        /// <param name="properties"></param>
        public static void Iterator(FizzBuzzProperties properties)
        {

            //Prints the properties before iterating
            DisplayProperties(properties);

            //Counts up to 'properties.iterations' and prints a word form a DivisorWord pair when applicable
            for (int i = 0; i < properties.Iterations; i++)
            {
                var str = "";

                //Checks the divisor of each pair in the dictionary and adds the word if the remainder is 0
                foreach (var value in properties.DivisorWordPairs)
                {
                    if (i % value.Key == 0)
                    {
                        str += value.Value;
                    }
                }

                //If the string is unchaiged prints the current iteration value
                if (str == "")
                {
                    str = i.ToString();
                }

                Console.WriteLine(" " + str);
            }
        }

        /// <summary>
        /// Displays the properties of the file into the console
        /// </summary>
        /// <param name="props"></param>
        public static void DisplayProperties(FizzBuzzProperties properties)
        {
            Console.WriteLine($"\nIterates {properties.Iterations} Times \n");

            Console.WriteLine($"Includes {properties.DivisorWordPairs.Count} Divisor Word pairs");

            var keys = properties.DivisorWordPairs.Keys.ToArray();

            var values = properties.DivisorWordPairs.Values.ToArray();

            for (int i = 0; i < properties.DivisorWordPairs.Count; i++)
            {
                Console.WriteLine($"Pair {i + 1}: (Divisor '{keys[i]}': Word '{values[i]}')");
            }

            Console.WriteLine("");
        }
    }
}
