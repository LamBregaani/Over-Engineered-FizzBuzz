using System;
using System.Text.RegularExpressions;

namespace Over_Engineered_FizzBuzz
{
    public static class InputManager
    {
        /// <summary>
        /// Validates the input string based on the passed pattern
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool Validate(string input, string pattern)
        {               

            Match match = Regex.Match(input, pattern);

            if(match.Success)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if a valid input is passed and runs the command
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool TryCommand(string input)
        {
            Regex pattern = new Regex(@"\.(?<word>\w+)");
            Match match = pattern.Match(input);

            if(match.Success)
            {
                var command = match.Groups["word"].Value;

                //Checks if the input is an existing command
                if (ConsoleCommands.commands.ContainsKey(command))
                {
                    ConsoleCommands.commands[command].cmdDelegate(input);
                    return true;

                }
                else
                {
                    Console.WriteLine($"Command '.{command}' not found\n");
                    return false;
                }
            }
            else
            {
                
                return false;
            }
        }

        /// <summary>
        /// Returns true if a valid input is passed and runs the command
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool TryCommand(string input, bool displayError)
        {
            Regex pattern = new Regex(@"\.(?<word>\w+)");
            Match match = pattern.Match(input);

            if (match.Success)
            {
                var command = match.Groups["word"].Value;

                //Checks if the input is an existing command
                if (ConsoleCommands.commands.ContainsKey(command))
                {
                    ConsoleCommands.commands[command].cmdDelegate(input);
                    return true;

                }
                else
                {
                    Console.WriteLine($"Command '.{command}' not found\n");
                    return false;
                }
            }
            if(displayError)
            {
                Console.WriteLine("Command must start with '.'\n");
            }
            return false;
        }

        /// <summary>
        /// Returns true if the input contains a pattern match
        /// sets the output to the first match in the string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool ParseInput(string input, ref string output, string pattern)
        {
            MatchCollection matches = Regex.Matches(input, pattern);

            if(matches.Count > 0)
            {
                output = matches[0].Value;

                return true;
            }

            return false;
        }
    }
}
