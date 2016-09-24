using SavingVariables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    public static class OutputMessages
    {

        // Leading Spaces
        private static string LeadingSpaces = "   ";



        // Create Success
        public static string VariableSaved(string var, int val)
        {
            return String.Format("{0}=  saved '{1}' as '{2}'", LeadingSpaces, var, val);
        }

        // Create Error
        public static string VariableAlreadySavedError(string var, int val)
        {
            return String.Format("{0}=  Error! '{1}' is already defined! Remember, it's '{2}'?", LeadingSpaces, var, val);
        }

        // Read Single Success
        public static string VariableValue(int val)
        {
            return String.Format("{0}=  {1}", LeadingSpaces, val);
        }

        // Read Single Error
        public static string VariableValueError(string var)
        {
            return String.Format("{0}=  '{1}' has not yet been set.", LeadingSpaces, var);
        }

        // Read All Success - Table
        public static string PrintedList(List<Variable> variables)
        {
            string printerString = "";
            printerString += "    ______________\n";
            printerString += "   | Name | Value |\n";
            printerString += "   |--------------|\n";

            foreach (var variable in variables)
            {
                printerString += "   |" + CenterValue(variable.VarSym, 6);
                printerString += "|";
                printerString += CenterValue(variable.Val.ToString(), 7);
                printerString += "|\n";
            }
            printerString += "   |______|_______|";
            return printerString;
        }

        private static string CenterValue(string value, int width)
        {
            string centered_string = "";
            int leadingSpaces = (int)Math.Ceiling((double)((width - value.Length) / 2));
            int followingSpaces = width - (value.Length + leadingSpaces);

            int i = 0;
            int j = 0;
            while (i < leadingSpaces)
            {
                centered_string += " ";
                i++;
            }
            centered_string += value;
            while (j < followingSpaces)
            {
                centered_string += " ";
                j++;
            }
            return centered_string;
        }

        // Read All Error
        public static string PrintAllVariablesError()
        {
            return String.Format("{0}= Database empty! Nothing to show.", LeadingSpaces);
        }

        // Destroy Success 
        public static string VariableCleared(string var)
        {
            return String.Format("{0}=  '{1}' is now free!", LeadingSpaces, var);
        }

        // Destroy Error
        public static string VariableClearError(string var)
        {
            return String.Format("{0}=  '{1}' has not yet been declared.", LeadingSpaces, var);
        }

        //Destroy All Success
        public static string AllVariablesCleared()
        {
            return String.Format("{0}= deleted all items from database!", LeadingSpaces);
        }

        //Destroy All Error
        public static string AllVariableClearError()
        {
            return String.Format("{0}= No items to delete from database.", LeadingSpaces);
        }

        //HALP Message
        public static string Help()
        {
            return String.Format("{0}show all: prints out all variables (with their values) in tabular form saved within the database \n{0}lastq: prints the last entered command or expression **even if it was unsuccessful**.\n{0}quit|exit: exits the program", LeadingSpaces);
        }

        //Unrecognized command
        public static string UnknownCommand()
        {
            return String.Format("{0}Unrecognized command. Type 'help' for command assistance");
        }

        //No Last Command for Stack Class
        public static string NoLastCommand()
        {
            return String.Format("{0}No last command entered.", LeadingSpaces);
        }

    }
}
