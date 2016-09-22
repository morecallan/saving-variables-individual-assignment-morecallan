using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.DAL;
using System.Text.RegularExpressions;
using SavingVariables.Models;

namespace SavingVariables
{
    public class Evaluate
    {
        VariablesRepository database = new VariablesRepository(new VariablesContext());

        public string Output { get; set; }

        //Output messages
        private string VariableCleared = "Variable successfully removed.";
        private string VariableAdded = "You successfully added a variable!!!";
        private string UnknownCommand = "I don't know what the hell you are talking about, dude. Try using a real command.";
        public string PrintedList (List<Variable> variables)
        {
            string printerString = "";
            printerString += "    ______________\n";
            printerString += "   | Name | Value |\n";
            printerString += "   |--------------|\n";

            foreach (var variable in variables)
            {
                printerString += CenterValue(variable.VarSym, 6);
                printerString += "|";
                printerString += CenterValue(variable.Val.ToString(), 6);
                printerString += "\n";
            }
            printerString += "   |______|_______|";
            return printerString;
        }

        public string CenterValue(string value, int width)
        {
            string centered_string = "";
            int leadingSpaces = (int)Math.Ceiling((double)(width - value.Length) / 2);
            int followingSpaces = (int)Math.Floor((double)(width - value.Length) / 2);

            int i = 0;
            int j = 0;
            while (i <= leadingSpaces)
            {
                centered_string += " ";
                i++;
            }
            centered_string += value;
            while (j <= followingSpaces)
            {
                centered_string += " ";
                j++;
            }
            return centered_string;
        }
        private bool CheckToSeeIfCommandIsInvolved(string input)
        {
            string pattern = @"^(?<command>clear|remove|delete|show)\s*(?<target>all|[a-zA-Z])$";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckToSeeIfVariableIsBeingSet(string input)
        {
            string pattern = @"^(?<varSym>[a-zA-Z]{1})(\s*[\=]\s*)?(?<val>[1-9])?$";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Evaluation (string input)
        {
            if (CheckToSeeIfCommandIsInvolved(input)) 
            {
                string pattern = @"^(?<command>clear|remove|delete|show)\s*(?<target>all|[a-zA-Z])$";
                Match match = Regex.Match(input, pattern);
                string command = match.Groups["command"].Value;
                string target = match.Groups["target"].Value;

                if (target != "all")
                switch (command)
                {
                    case "clear": database.RemoveVariablesWithVarParameter(target); Output = VariableCleared; break;
                    case "remove": database.RemoveVariablesWithVarParameter(target); Output = VariableCleared; break;
                    case "delete": database.RemoveVariablesWithVarParameter(target); Output = VariableCleared; break;
                    case "show": List<Variable> variables = database.GetCurrentVariables(); Output = PrintedList(variables); break;
                }
                else
                {
                    switch(command)
                    {
                        case "clear": database.RemoveAllVariables(); Output = VariableCleared; break;
                        case "remove": database.RemoveAllVariables(); Output = VariableCleared; break;
                        case "delete": database.RemoveAllVariables(); Output = VariableCleared; break;
                        case "show": List<Variable> variables = database.GetCurrentVariables(); Output = PrintedList(variables); break;
                    }
                    
                }
            }
            else if (CheckToSeeIfVariableIsBeingSet(input))
            {
                string pattern = @"^(?<varSym>[a-zA-Z]{1})(\s*[\=]\s*)?(?<val>[1-9])?$";
                Match match = Regex.Match(input, pattern);
                string varSym = match.Groups["varSym"].Value;
                string value = match.Groups["val"].Value;

                if (value != null || value != "")
                {
                    int valueInt = Int32.Parse(value);
                    database.AddVariablesWithVarAndValParameter(varSym, valueInt);
                    Output = VariableAdded;
                }
                else
                {
                    Variable found_var = database.FindVariablesGivenVarSym(varSym);
                    Output = found_var.Val.ToString();
                }
            }
            else
            {
                Output = UnknownCommand;
            }
        }


    }
}
