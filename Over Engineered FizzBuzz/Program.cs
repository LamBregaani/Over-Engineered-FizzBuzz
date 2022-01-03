using System;
using System.Collections.Generic;


namespace Over_Engineered_FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("         ---Welcome to the totally useless over-engineered FizzBuzz iterator programm!---");

            //Reads the users input
            while (true)
            {
                Console.WriteLine("Plase input a command\nType '.Help' for a list of commands");

                InputManager.TryCommand(Console.ReadLine(), true);




            }

        }

    }
}


