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

        /// READ | FIND ///
        public Variable FindVariablesGivenVarSym(string var_sym)
        {
            Variable my_found_var = Context.Variables.FirstOrDefault(x => x.VarSym == var_sym);
            return my_found_var;

            /*
            List<Variable> current_variable_list = GetCurrentVariables();
            Variable found_variable = null;
            foreach (var variable in current_variable_list)
            {
                if (variable.VarSym == var_sym)
                {
                    found_variable = variable;
                    return found_variable;
                }
                else
                {
                    found_variable = null;
                }
            }
            return found_variable;
            */
        }

        /// CREATE ////
        public void AddVariableAsEntity(Variable variable)
        {
            Variable variable_check = FindVariablesGivenVarSym(variable.VarSym);
            if (variable_check == null)
            {
                Context.Variables.Add(variable);
                Context.SaveChanges();
            } else
            {
                throw new InvalidOperationException();
            }
        }


        /// CREATE ////
        public void AddVariablesWithVarAndValParameter(string var, int val)
        {
            Variable variable_check = FindVariablesGivenVarSym(var);
            Console.WriteLine(variable_check);
            if (variable_check == null)
            {
                Variable my_new_variable = new Variable { VarSym = var, Val = val };
                Context.Variables.Add(my_new_variable);
                Context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }


        /// DESTROY ///
        public Variable RemoveVariablesWithVarParameter(string var)
        {
            Variable variable_to_delete = FindVariablesGivenVarSym(var);
            if (variable_to_delete != null)
            {
                Context.Variables.Remove(variable_to_delete);
                Context.SaveChanges();
            } else
            {
                throw new InvalidOperationException();
            }
            return variable_to_delete;
        }

        public void RemoveAllVariables()
        {
            List<Variable> variables_in_database = GetCurrentVariables();
            foreach (var variable in variables_in_database)
            {
                Context.Variables.Remove(variable);
            }
            Context.SaveChanges();
        }

    }
}