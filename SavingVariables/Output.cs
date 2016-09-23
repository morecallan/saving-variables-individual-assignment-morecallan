using SavingVariables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    public static class Output
    {

        // Leading Spaces
        static string LeadingSpaces = "   ";



        // Create Success
        static string VariableSaved(string var, int val)
        {
            return String.Format("{0}=  saved '{1}' as '{2}'", LeadingSpaces, var, val);
        }

        // Create Error
        static string VariableAlreadySavedError(string var, int val)
        {
            return String.Format("{0}=  Error! '{1}' is already defined! Remember, it's '{2}'?", LeadingSpaces, var, val);
        }

        // Read Single Success
        static string VariableValue(int val)
        {
            return String.Format("{0}=  {1}", LeadingSpaces, val);
        }

        // Read Single Error
        static string VariableValueError(string var)
        {
            return String.Format("{0}=  '{1}' has not yet been set.", LeadingSpaces, var);
        }

        // Read All Success - Table
        static string PrintedList(List<Variable> variables)
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

        static string CenterValue(string value, int width)
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
        static string PrintAllVariablesError()
        {
            return String.Format("{0}= Database empty! Nothing to show.", LeadingSpaces);
        }

        // Destroy Success 
        static string VariableCleared(string var)
        {
            return String.Format("{0}=  '{1}' is now free!", LeadingSpaces, var);
        }

        // Destroy Error
        static string VariableClearError(string var)
        {
            return String.Format("{0}=  {1} has not yet been declared.");
        }

        //Destroy All Success
        static string AllVariablesCleared()
        {
            return String.Format("{0}= deleted all items from database!", LeadingSpaces);
        }

        //Destroy All Error
        static string AllVariableClearError()
        {
            return String.Format("{0}= No items to delete from database.", LeadingSpaces);
        }

        //HALP Message
        static string Help()
        {
            return String.Format("{0} show all: prints out all variables (with their values) in tabular form saved within the database \n{0}lastq: prints the last entered command or expression **even if it was unsuccessful**.\n{0}quit|exit: exits the program", LeadingSpaces);
        }

    }
}
