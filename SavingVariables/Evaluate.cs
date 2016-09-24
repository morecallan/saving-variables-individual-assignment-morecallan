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
        //Connection to Database
        VariablesRepository database = new VariablesRepository(new VariablesContext() );




        //Text output to display in CLI
        public string Output { get; set; }




        //Using regex to determine if user is attempting to interact with variable
        private bool CheckToSeeIfCommandIsInvolved(string input)
        {
            string pattern = @"^(?<command>clear|remove|delete|show)\s*(?<target>all|[a-zA-Z])$";
            Match match = Regex.Match(input, pattern);
            return (match.Success) ? true : false;
        }

        //Using regex to determine if user is trying to set or read a variable
        private bool CheckToSeeIfVariableIsBeingSet(string input)
        {
            string pattern = @"^(?<varSym>[a-zA-Z]{1})\s*(\s*[\=]\s*)?\s*(?<val>[-]?[0-9]+)?$";
            Match match = Regex.Match(input, pattern);
            return (match.Success) ? true : false;
        }

        //Main evaluation method
        public void Evaluation (string input)
        {
            if (CheckToSeeIfCommandIsInvolved(input)) 
            {
                // If user is trying to DELETE (single or all) or SHOW (single or all)
                string pattern = @"^(?<command>clear|remove|delete|show)\s*(?<target>all|[a-zA-Z])$";
                Match match = Regex.Match(input, pattern);
                string command = match.Groups["command"].Value;
                string target = match.Groups["target"].Value;

                //If command target is single variable
                if (target != "all")
                switch (command)
                {
                    case "clear": ClearSingleByVar(target);  break;
                    case "remove": ClearSingleByVar(target); break;
                    case "delete": ClearSingleByVar(target); break;
                    case "show": ShowSingleVar(target); break;
                }
                //If command target is multi variable
                else if (target == "all")
                {
                    switch(command)
                    {
                        case "clear": ClearAllVar(); break;
                        case "remove": ClearAllVar(); break;
                        case "delete": ClearAllVar(); break;
                        case "show": ShowAllVar();  break;
                    }
                    
                }
            }
            // If user is trying to SET or READ a single variable
            else if (CheckToSeeIfVariableIsBeingSet(input))
            {
                string pattern = @"^(?<varSym>[a-zA-Z]{1})\s*(\s*[\=]\s*)?\s*(?<val>[-]?[0-9]+)?$";
                Match match = Regex.Match(input, pattern);
                string varSym = match.Groups["varSym"].Value;
                string value = match.Groups["val"].Value;

                // If user wants to set the value of a variable
                if (value != null && value != "")
                {
                    SetVariable(varSym, value);
                }
                //If user simply wants to see the value of a variable (they may or may not have set).
                else
                {
                    ShowSingleVar(varSym);
                }
            }
            else
            {
                Output = OutputMessages.UnknownCommand();
            }       
        }

        /// Execution Functions ///
        private void ClearSingleByVar(string target)
        {
            try
            {
                database.RemoveVariablesWithVarParameter(target);
                Output = OutputMessages.VariableCleared(target);
            }
            catch
            {
                Output = OutputMessages.VariableClearError(target);
            }
        }

        private void ShowSingleVar(string varSym)
        {
            try
            {
                Variable found_var = database.FindVariablesGivenVarSym(varSym);
                Output = OutputMessages.VariableValue(found_var.Val);
            }
            catch
            {
                Output = OutputMessages.VariableValueError(varSym);
            }
        }

        private void ClearAllVar()
        {
            try
            {
                database.RemoveAllVariables();
                Output = OutputMessages.AllVariablesCleared();
            }
            catch
            {
                Output = OutputMessages.AllVariableClearError();
            }
        }

        private void ShowAllVar()
        {
            try
            {
                List<Variable> variables = database.GetCurrentVariables();
                Output = OutputMessages.PrintedList(variables);
            }
            catch
            {
                Output = OutputMessages.PrintAllVariablesError();
            }
        }

        private void SetVariable(string varSym, string value)
        {
            try //Make sure user is entering valid number
            {
                int valueInt = Int32.Parse(value);
                try //Make sure user is entering unique value
                {
                    database.AddVariablesWithVarAndValParameter(varSym, valueInt);
                    Output = OutputMessages.VariableSaved(varSym, valueInt);
                }
                catch
                {
                    Output = OutputMessages.VariableAlreadySavedError(varSym, valueInt);
                }
            }
            catch
            {
                Output = OutputMessages.UnknownCommand();
            }
            
        }
    }
}
