using SavingVariables.Models;
using System.Collections.Generic;
using System.Linq;

namespace SavingVariables.DAL
{
    public class VariablesRepository
    {
        public VariablesContext Context { get; set; }

        public VariablesRepository(VariablesContext _context)
        {
            Context = _context;
        }



        //// READ ////
        public List<Variable> GetCurrentVariables()
        {
            return Context.Variables.ToList();
        }

        /// CREATE ////
        public void AddVariableAsEntity(Variable variable)
        {
            Context.Variables.Add(variable);
            Context.SaveChanges();
        }

        /// CREATE ////
        public void AddVariablesWithVarAndValParameter(char var, int val)
        {
            Variable my_new_variable = new Variable { VarSym = var.ToString(), Val = val };
            Context.Variables.Add(my_new_variable);
            Context.SaveChanges();
        }
    }
}