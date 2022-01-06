using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Over_Engineered_FizzBuzz
{
	public class FileReader
	{
        public static FizzBuzzProperties LoadFile(string fileName)
        {
            //List is used to store the lines of text with the applicable data
            List<string> data = new List<string>();

            var path = Path.Combine(FileCreator.DefaultPath() + fileName + ".txt");

            //Checks if the folders exisiting the default directory
            if(!Directory.Exists(FileCreator.DefaultPath()))
                FileCreator.CreateDefaultFolders();

            if (!File.Exists(path))
            {
                Console.WriteLine($"'{fileName}.txt' not found at {path}");

                //Returns blank data due to file not existing
                return CreateFBPRopertyData(data.ToArray());
            }

            //Creates a temporary Streamreader to read the text file
            using StreamReader reader = File.OpenText(path);

            string line;


            //Removes any commented lines from the file
            while ((line = reader.ReadLine()) != null)
            {
                
                if (!line.StartsWith("//") && line.Length != 0)
                    data.Add(line);
            }

            return CreateFBPRopertyData(data.ToArray());


        }
        /// <summary>
        /// Creates a FizzBuzzData instance based on the passed strings
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>

        public static FizzBuzzProperties CreateFBPRopertyData(string[] inputs)
        {
            //Creates a blank dictionary
           Dictionary<int, string> propertyInfo = new Dictionary<int, string>();

            int iterations = 0;

            //Returns a blank FizzBuzzData in case no strings were passed
            if(inputs.Length == 0)
            {
                return new FizzBuzzProperties(iterations, propertyInfo);
            }

            //Gets the iterations value from the passed strings
            for (int i = 0; i < inputs.Length; i++)
            {
                int value = 0;

                value = ParseData<int>(inputs[i], @"(?<=\bIterations\s+)\b(\d+)\b");

                if (value != 0)
                {
                    iterations = value;
                    break;
                }
            }

            //Gets the divisor word pairs form the passed strings
            for (int i = 0; i < inputs.Length; i++)
            {
                int divisor = 0;

                //Zero is used since I don't know how to differentiate a string form an int in the ParseData method
                string word = "0";

                divisor = ParseData<int>(inputs[i], @"(?<=\bDivisor\s+)\b(\d+)\b");

                word = ParseData<string>(inputs[i], @"(?<=\bString\s+)\b(\w+)\b");

                //Checks that the divisor is greater than 0 and that the string was changed
                if (divisor > 0 && word != "0")
                {
                    propertyInfo.Add(divisor, word);
                }
            }

            return new FizzBuzzProperties(iterations, propertyInfo);
        }

        /// <summary>
        /// Parses the passed string using the passed pattern and returns the match as type of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static T ParseData<T>(string input, string pattern)
        {

            var match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string result = match.Groups[1].Value;
                return (T)Convert.ChangeType(result, typeof(T));
            }

            return (T)Convert.ChangeType("0", typeof(T));

        }
    }
}
