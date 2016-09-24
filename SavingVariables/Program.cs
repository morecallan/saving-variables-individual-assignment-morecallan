using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initiates a new session and loop
            bool running = true;
            Stack session_stack = new Stack();
            Commands session = new Commands(session_stack);

            //Greeting
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the non-functioning calculator. You can save variables but if you've come here to do anything else, you are in the wrong place, my friend.");
            Console.ResetColor();

            //Loops until user exits the program
            while (running)
            {
                Console.Write(">> ");
                session.Action(Console.ReadLine().ToLower());
                Console.WriteLine(session.Output);
            }
        }
    }
}
