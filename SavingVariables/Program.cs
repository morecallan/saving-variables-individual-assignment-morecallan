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
            bool running = true;
            Stack session_stack = new Stack();
            Commands session = new Commands(session_stack);

            while (running)
            {
                Console.WriteLine("Enter a command. >");
                session.Action(Console.ReadLine());
                Console.WriteLine(session.Output);
            }
        }
    }
}
