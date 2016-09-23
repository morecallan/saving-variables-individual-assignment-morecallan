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



            //Loops until user exits the program
            while (running)
            {
                Console.Write(">>");
                session.Action(Console.ReadLine());
                Console.WriteLine(session.Output);
            }
        }
    }
}
