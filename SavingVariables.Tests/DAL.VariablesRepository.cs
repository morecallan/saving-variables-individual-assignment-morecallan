using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using Moq;
using System.Data.Entity;
using SavingVariables.Models;
using System.Collections.Generic;
using System.Linq;

namespace SavingVariables.Tests
{
    [TestClass]
    public class VariablesRepositoryTests
    {
        Mock<VariablesContext> mock_context { get; set; } 
        Mock<DbSet<Variable>> mock_variable_table { get; set; }
        List<Variable> variable_list { get; set; }


        public void ConnectMocksToDatastore()
        {
            var queryable_list = variable_list.AsQueryable();

            // Each time Linq tries to query our "table", redirect the query to point at our queirable list INSTEAD of the real database
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            // This is now our table the is representative of the REAL DATABASE.
            mock_context.Setup(c => c.Variables).Returns(mock_variable_table.Object);

            // Add stuff to our representatve table
            mock_variable_table.Setup(t => t.Add(It.IsAny<Variable>())).Callback((Variable v) => variable_list.Add(v));
        }

        // RESET before each test
        [TestInitialize]
        public void Initialize()
        {
            // Populate mock context
            mock_context = new Mock<VariablesContext>();
            mock_variable_table = new Mock<DbSet<Variable>>();
            variable_list = new List<Variable>();
        }
    }
}
