using SavingVariables.Models;
using System.Data.Entity;

namespace SavingVariables.DAL
{
    public class VariablesContext : DbContext
    {
        public virtual DbSet<Variable> Variables { get; set; }
    }
}


