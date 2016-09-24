using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class Commands
    {
        public Stack SessionStack { get; set; }

        // Commands class is dependent on current session stack and can only be instantiated with this value.
        public Commands(Stack current_stack)
        {
            SessionStack = current_stack;
        }

        //Instantiates Evaluation class for all the "heavy lifting".
        public Evaluate evaluation = new Evaluate();

        //Output that displays on the screen for CLI
        public string Output { get; set; }


        public void Action(string input)
        {
                switch (input)
                 {
                    case "lastq": Output = SessionStack.LastCommand != null ? SessionStack.LastCommand : OutputMessages.NoLastCommand(); break;
                    case "quit": Environment.Exit(0); break;
                    case "exit": Environment.Exit(0); break;
                    case "shut up": Environment.Exit(0); break;
                    case "help": SessionStack.LastCommand = input; Output = OutputMessages.Help(); break;
                    default: evaluation.Evaluation(input); SessionStack.LastCommand = input; Output = evaluation.Output; break;
                }
        }


        //Commands via product owner:

        ////// GENERAL //////
        // lastq:  prints the last entered command or expression even if it was unsuccessful
        // quit | exit: exits the program
        // help : prints out all the information 

        ////// CREATE //////
        // <CHAR> = <INT>: creates an entry in the database where <CHAR> is <INT> //CREATE

        ////// DELETE //////
        // clear <CHAR> | remove <CHAR> | delete <CHAR>: removes the saved entry for a from the database //DELETE
        // clear all | remove all | delete all: removes all saved entries from the database //DELETE

        ////// READ //////
        // show all: prints out all variables (with their values) in tabular form saved within the database. Note: Variables should be listed in alphabitcal order. //READ
    }
}
