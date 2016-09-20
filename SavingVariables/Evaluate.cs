using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.DAL;
using System.Text.RegularExpressions;

namespace SavingVariables
{
    public class Evaluate
    {
        VariablesRepository database = new VariablesRepository(new VariablesContext());

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

    }
}
