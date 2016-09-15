using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class Commands
    {
        //Commands:

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
