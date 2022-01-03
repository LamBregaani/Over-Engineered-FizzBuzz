using System;
using System.IO;
using System.Collections.Generic;

namespace Over_Engineered_FizzBuzz
{
    //This should probably be called FileManager or something tbh
    public static class FileCreator
    {
        //General default string that classes can access
       public static string DefaultPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FizzBuzzProgram/Data/";
        }

        /// <summary>
        /// Creates defualt folders to store the txt files in
        /// </summary>
        public static void CreateDefaultFolders()
        {
            var tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/FizzBuzzProgram";

            if (!File.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            tempPath += "/Data";

            if (!File.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

        }

        /// <summary>
        /// Creates a FizzBuzz properties file with default values
        /// </summary>
        public static void CreateDefault()
        {
            var data = new Dictionary<int, string>();

            data.Add(3, "Fizz");

            data.Add(5, "Buzz");

            var prop = new FizzBuzzProperties(100, data);

            CreateFile("FBDefault.txt", prop);
        }

        /// <summary>
        /// This overload prompts the user to input a file name
        /// </summary>
        public static void CreateNew()
        {
            //Used to set the name of the file
            while (true)
            {
                Console.WriteLine("Input the name of the new FizzBuzz property file\nMust contain alphanumeric characters only\nDo not include the file extension");

                var input = Console.ReadLine();

                //Exits the current command loop if a command was run
                if (InputManager.TryCommand(input))
                {
                    return;
                }

                //Checks for a valid file name that uses alphanumeric characters only
                if (InputManager.Validate(input, @"(^[a-zA-Z0-9]+$)"))
                {
                    CreateNew(input);
                }
                else
                {
                    Console.WriteLine("Invalid character detected\nMust not contain non-alphanumeric characters");
                }
            }
        }

        /// <summary>
        /// Creates a new FizzBuzz properties file based on user input
        /// A string is passed for the file name
        /// </summary>
        public static void CreateNew(string fileName)
        {
            int iterations = 0;

            int pairAmount = 0;

            fileName += ".txt";

            Dictionary<int, string> pairs = new Dictionary<int, string>();

            //Used to set the iteration amount
            while (true)
            {
                Console.WriteLine("Input the amount of iterations the file will have\nMust be apositive interger");

                var input = Console.ReadLine();

                //Exits the current command loop if a command was run
                if (InputManager.TryCommand(input))
                {
                    return;
                }

                //Checks for a valid interger > 0
                if (InputManager.Validate(input, @"^[1-9]\d*$"))
                {
                    iterations = int.Parse(input);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid character detected\nMust be apositive interger");
                }
            }

            //Used to set how many Divisor-Word pairs the file will have
            while (true)
            {
                Console.WriteLine("Input the amount of Divisor and string output pairs the file will have\nMust be apositive interger");

                var input = Console.ReadLine();

                //Exits the current command loop if a command was run
                if (InputManager.TryCommand(input))
                    return;

                //Checks for a valid interger > 0
                if (InputManager.Validate(input, @"^[1-9]\d*$"))
                {
                    pairAmount = int.Parse(input);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid character detected\nMust be apositive interger");
                }
            }

            //Iterate an amount based on the previous input
            for (int i = 0; i < pairAmount; i++)
            {
                var key = 0;

                var value = "";

                //Sets the divisor for the current pair
                while(true)
                {
                    Console.WriteLine($"Input Divisor for pair {i + 1}\nMust be a positive interger");

                    var input = Console.ReadLine();

                    //Exits the current command loop if a command was run
                    if (InputManager.TryCommand(input))
                    {
                        
                        return;
                    }
                       

                    //Checks for a valid interger > 0
                    if (InputManager.Validate(input, @"^[1-9]\d*$"))
                    {
                        key = int.Parse(input);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid character detected\nMust be a positive interger");
                    }
                }

                //Sets the word for the current pair
                Console.WriteLine($"Input string output for pair {i + 1}");

                value = Console.ReadLine();

                pairs.Add(key, value);
            }

            var properties = new FizzBuzzProperties(iterations, pairs);

            CreateFile(fileName, properties);
        }

        /// <summary>
        /// Create a .txt file using the passed properties
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="properties"></param>
        private static void CreateFile(string filePath, FizzBuzzProperties properties)
        {
            var fullPath = DefaultPath() + filePath;

            Console.WriteLine($"Creating {filePath} at {fullPath}");

            using (StreamWriter writer = File.CreateText(fullPath))
            {

                writer.WriteLine($"//The max value to iterate to\n"
                + $"Iterations {properties.Iterations}\n\n"
                + "//The Divisor values and the string to print\n");

                foreach (var pair in properties.DivisorWordPairs)
                {
                    writer.WriteLine($"Divisor {pair.Key}, String {pair.Value}\n");
                }
            }
        }

        public static bool DeleteFile(string fileName)
        {
            var path = Path.Combine(DefaultPath() + fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine($"'{fileName}' deleted at {path}");
                return true;
            }
            else
            {
                Console.WriteLine($"'{fileName}' not found");
                return false;
            }
        }
    }
}
