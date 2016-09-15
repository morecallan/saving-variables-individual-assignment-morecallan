using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.Models
{
    public class Variable
    {
        //Properties: => Table columns.
        public char Var { get; set; }
        public int Val { get; set; }

        //Constructor: Can only be instantiated with dependencies.
        public Variable(char _var, int _val)
        {
            Var = _var;
            Val = _val;
        }

    }
}
