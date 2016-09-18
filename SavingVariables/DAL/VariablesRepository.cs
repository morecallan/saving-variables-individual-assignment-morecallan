using SavingVariables.Models;
using System;
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

        /// READ | FIND ///
        public Variable FindVariablesGivenVarSym(string var_sym)
        {
            //Variable my_found_var = Context.Variables.Find(var_sym);
            //return my_found_var;

            List<Variable> current_variable_list = Context.Variables.ToList();
            Variable found_variable = null;
            foreach (var variable in current_variable_list)
            {
                if (variable.VarSym == var_sym)
                {
                    found_variable = variable;
                    return found_variable;
                } else
                {
                    found_variable = null;
                }
            }
            return found_variable;
        }

        /// CREATE ////
        public void AddVariablesWithVarAndValParameter(string var, int val)
        {
            Variable my_new_variable = new Variable { VarSym = var, Val = val };
            Context.Variables.Add(my_new_variable);
            Context.SaveChanges();
        }


    }
}