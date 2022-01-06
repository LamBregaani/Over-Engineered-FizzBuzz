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

            if (match.Success)
            {
                return true;
            }

            return false;
        }

        public static bool HasMatch(string pattern, string input, out string matchString, string matchGroup)
        {
            //This checks to see if a file name was included with the initail command
            //ie. the user inputs ".View(FileName)" instead of just ".View"
            Regex regex = new Regex(pattern);

            Match mat = regex.Match(input);

            matchString = "";

            if (mat.Success)
            {
                matchString = mat.Groups[matchGroup].Value;
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
            if(HasMatch(@"\.(?<command>\w+)", input, out string command, "command"))
            { 
                //Checks if the input is an existing command
                if (ConsoleCommands.commands.ContainsKey(command))
                {
                    if (ConsoleCommands.commands[command].takesParams)
                    {
                        if (Regex.IsMatch(input, @"^\.\w+\(\w+\)$"))
                        {
                            ConsoleCommands.commands[command].cmdDelegate(input);
                            return true;
                        }
                    }
                    if (Regex.IsMatch(input, @"^\.\w+$"))
                    {
                        ConsoleCommands.commands[command].cmdDelegate(input);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid command syntax");
                        return false;
                    }

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
            //Returns true if the input starts with a '.'
            if (Regex.IsMatch(input, @"^\."))
            {
                return TryCommand(input);
            }

            //Displays an error message if 
            else if(displayError)
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
