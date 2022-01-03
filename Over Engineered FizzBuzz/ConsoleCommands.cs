using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Over_Engineered_FizzBuzz
{
	public static class ConsoleCommands
	{


		public static Dictionary<string, Command> commands = new Dictionary<string, Command>();

		public static bool commandRunning;

		//Loads the dictionary with the commands when a static method of this class is fist accessed 
		static ConsoleCommands()
        {
			AddCommand(new Command("CreateNew", "Create a new FizzBuzz property file", CMDCreateNew));

			AddCommand(new Command("CreateDefault", "Create a FizzBuzz property file with default values", CMDCreateDefault));

			//Unused
			//AddCommand(new Command("Load", "Load an existing FizzBuzz property file", CMDLoadFile));

			AddCommand(new Command("Delete", "Delete an existing FizzBuzz property file", CMDDelete));

			AddCommand(new Command("Run", "Run the currently loaded FizzBuzz propety file", CMDIterate));

			AddCommand(new Command("ViewFiles", "View the names of created files", CMDViewFiles));

			AddCommand(new Command("View", "View the FizzBuzz properties of a file", CMDView));

			AddCommand(new Command("Help", "Display a list of commands", CMDHelp));

			AddCommand(new Command("Clear", "Clears the console", CMDClear));

			AddCommand(new Command("Close", "Closes the application", CMDClose));

			AddCommand(new Command("HackerMode", "Makes you feel like a real programmer", CMDHackerMode));

			AddCommand(new Command("Exit", "Exits the currently running command if applicable", CMDExit));
        }

		//Used by the static constructor to add all the commands
		private static void AddCommand(Command cmd)
        {
			commands.Add(cmd.name, cmd);
		}

		//Used to display a list of commands
		private static void CMDHelp(params object[] args)
        {
            foreach (var cmd in commands)
            {
				Console.WriteLine($"	.{cmd.Key} //{cmd.Value.description}");
            }

			
        }

		//Command to create a new file
		private static void CMDCreateNew(params object[] args)
        {
			//This checks to see if a file was included with the initail command
			//ie. the user inputs ".CreateNew(FileName)" instead of just ".CreateNew"
			Regex pattern = new Regex(@"\.CreateNew\((?<fileName>\w+)\)");

			Match match = pattern.Match(args[0].ToString());

			if (match.Success)
			{
				var fileName = match.Groups["fileName"].Value;

				//If a filename was included it passes the string the File Creator
				FileCreator.CreateNew(fileName);
				return;
			}

			//If a file name was not included no string is passed and the user will be prompted to input a file name in this method
			FileCreator.CreateNew();
        }

		//Command to create a file with default values
		private static void CMDCreateDefault(params object[] args)
		{
			FileCreator.CreateDefault();
		}

		//Unused
		//Command to load an existing file
		//private static void CMDLoadFile(params object[] args)
  //      {
		//	Regex pattern = new Regex(@"\.Load\((?<fileName>\w+)\)");

		//	Match match = pattern.Match(args[0].ToString());

		//	if (match.Success)
		//	{
		//		var fileName = match.Groups["fileName"].Value;

		//		var data = FileReader.LoadFile(fileName);

		//		//If the iterations are > 0 the file is valid
		//		if (data.Iterations != 0)
		//		{
		//			FizzBuzz.loadedData = data;
		//			Console.WriteLine("File loaded. Use '.Run' to run the loaded file");

		//			return;
		//		}
  //              else
  //              {
		//			Console.WriteLine($"{fileName}.txt not found");
  //              }
		//	}


		//	commandRunning = true;

		//	while (true)
  //          {
		//		Console.WriteLine("Input the name of the file to load\nDo not include the file extension");

		//		var fileName = Console.ReadLine();

		//		//Exits the current command loop if a command was run
		//		if (InputManager.TryCommand(fileName))
		//			break;


		//		var data = FileReader.LoadFile(fileName);

		//		//If the iterations are > 0 the file is valid
		//		if(data.Iterations != 0)
  //              {
		//			FizzBuzz.loadedData = data;
		//			Console.WriteLine("File loaded. Use '.Run' to run the loaded file");

		//			break;
  //              }
		//	}

		//	commandRunning = false;

		//}

		//Command to delete an exisitng file
		private static void CMDDelete(params object[] args)
        {
			//This checks to see if a file was included with the initail command
			//ie. the user inputs ".Delete(FileName)" instead of just ".Delete"
			Regex pattern = new Regex(@"\.Delete\((?<fileName>\w+)\)");

			Match match = pattern.Match(args[0].ToString());

			if (match.Success)
			{
				var input = match.Groups["fileName"].Value;

				if (FileCreator.DeleteFile(input + ".txt"))
				{
					return;
				}
				else
				{
					Console.WriteLine($"{input}.txt not found");
				}
			}



			commandRunning = true;

			//This is run if a file wasn't included with the initail command or if the user included an invalid file
			//eg. ".Delete" instead of ".Delete(fileName)"
			//Loops until a valid file name is input or a different command is run
			while (true)
			{
				Console.WriteLine("Input the name of the file to delete\nDo not include the file extension");

				var input = Console.ReadLine();

				//Exits the current command loop if a command was run
				if (InputManager.TryCommand(input))
					break;

				if (FileCreator.DeleteFile(input + ".txt"))
				{
					break;
				}
				else
                {
					Console.WriteLine($"{input}.txt not found");
                }


			}

			commandRunning = false;

		}

		/// <summary>
		/// Displays the file names in the default directory
		/// </summary>
		/// <param name="args"></param>
		private static void CMDViewFiles(params object[] args)
        {
			var files = System.IO.Directory.GetFiles(FileCreator.DefaultPath());

            foreach (var file in files)
            {
				//Splits the file name into the seperate directories
				var directories = file.Split("/");

				//Gets the last string ie. the name of the file
				var name = directories[directories.Length - 1];

				Console.WriteLine(name);
            }
        }

		private static void CMDView(params object[] args)
        {

			//This checks to see if a file was included with the initail command
			//ie. the user inputs ".View(FileName)" instead of just ".View"
			Regex pattern = new Regex(@"\.View\((?<fileName>\w+)\)");

			Match match = pattern.Match(args[0].ToString());

			if (match.Success)
			{
				var fileName = match.Groups["fileName"].Value;

				var data = FileReader.LoadFile(fileName);

				//If the iterations are > 0 the file is valid
				if (data.Iterations != 0)
				{
					Console.WriteLine($"Displaying {fileName}.txt properties");
					FizzBuzz.DisplayProperties(data);
					return;
				}
				else
				{
					Console.WriteLine($"{fileName}.txt not found");
				}
			}


			commandRunning = true;

			//This is run if a file wasn't included with the initail command or if the user included an invalid file
			//eg. ".View" instead of ".View(fileName)"
			//Loops until a valid file name is input or a different command is run
			while (true)
			{
				Console.WriteLine("Input the name of the file to view\nDo not include the file extension");

				var fileName = Console.ReadLine();

				//Exits the current command loop if a command was run
				if (InputManager.TryCommand(fileName))
					break;


				var data = FileReader.LoadFile(fileName);

				//If the iterations are > 0 the file is valid
				if (data.Iterations != 0)
				{
					Console.WriteLine($"Displaying {fileName}.txt properties");
					FizzBuzz.DisplayProperties(data);
					return;
				}
				else
				{
					Console.WriteLine($"{fileName}.txt not found");
				}
			}

			commandRunning = false;
		}

		private static void CMDIterate(params object[] args)
		{

			//This checks to see if a file was included with the initail command
			//ie. the user inputs ".Run(FileName)" instead of just ".Run"

			Regex pattern = new Regex(@"\.Run\((?<fileName>\w+)\)");

			Match match = pattern.Match(args[0].ToString());

			//IF a file was inlcuded it checks if it is a valid file
			if (match.Success)
			{
				var fileName = match.Groups["fileName"].Value;

				var data = FileReader.LoadFile(fileName);

				//If the iterations are > 0 the file is valid
				if (data.Iterations != 0)
				{
					Console.WriteLine($"Iterating {fileName}.txt");

					//Iterates the file
					FizzBuzz.Iterator(data);

					//Breaks out of the method
					return;
				}
				else
				{
					//Reports the error and continues to the next section
					Console.WriteLine($"{fileName}.txt not found");
				}
			}

			commandRunning = true;

			//This is run if a file wasn't included with the initail command or if the user included an invalid file
			//eg. ".Run" instead of ".Run(fileName)"
			//Loops until a valid file name is input or a different command is run
			while (true)
			{
				Console.WriteLine("Input the name of the file to iterate\nDo not include the file extension");

				//Gets the user's input for the file name
				var input = Console.ReadLine();

				//Exits the current command loop if a command was run
				if (InputManager.TryCommand(input))
					break;

				//Attempts to load a file 
				var data = FileReader.LoadFile(input);

				//If the iterations are > 0 the file is valid
				if (data.Iterations != 0)
				{
					FizzBuzz.Iterator(data);
					Console.WriteLine($"Iterating {input}.txt");

					return;
				}
				else
				{
					Console.WriteLine($"{input}.txt not found");
				}
			}

			commandRunning = false;

		}

		private static void CMDExit(params object[] args)
        {
			if(commandRunning == true)
            {
				Console.WriteLine("Exiting current command");
				commandRunning = false;
			}
			else
            {
				Console.WriteLine("No command is currently running");
            }


        }

		private static void CMDHackerMode(params object[] args)
        {
			Console.ForegroundColor = ConsoleColor.Green;
			CMDClear();
        }

		private static void CMDClear(params object[] args)
        {
			Console.Clear();
        }

		private static void CMDClose(params object[] args)
        {
			Environment.Exit(0);
        }
	}

	
	//I don't remember why this is here, but I don't know if I can move it without breaking everything


	public struct Command
    {
		public delegate void CMDDel(params object[] args);

		public string name;

		public string description;

		public CMDDel cmdDelegate;

		public Command(string nme,string desc, CMDDel del)
        {
			name = nme;

			description = desc;

			cmdDelegate = del;
        }
    }
}
