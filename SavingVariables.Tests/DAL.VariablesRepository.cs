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
        VariablesRepository repo { get; set; }


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

            // Add | Remove  stuff to our representatve table
            mock_variable_table.Setup(t => t.Add(It.IsAny<Variable>())).Callback((Variable v) => variable_list.Add(v));
            mock_variable_table.Setup(t => t.Remove(It.IsAny<Variable>())).Callback((Variable a) => variable_list.Remove(a));
            //mock_variable_table.Setup(t => t.Find(It.IsAny<Variable>())).Callback((Variable a) => variable_list.Contains(a));

        }

        // RESET before each test
        [TestInitialize]
        public void Initialize()
        {
            // Populate mock context
            mock_context = new Mock<VariablesContext>();
            mock_variable_table = new Mock<DbSet<Variable>>();
            variable_list = new List<Variable>();
            repo = new VariablesRepository(mock_context.Object);

            ConnectMocksToDatastore();
        }

        [TestCleanup]
        public void CleanUp()
        {
            repo = null;
        }

        [TestMethod]
        public void VariablesRepoIsCreatedInInitializeMethod()
        {
            //Assert
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void VariablesRepoHasContext()
        {
            //Act
            VariablesContext actual_context = repo.Context;
            //Assert
            Assert.IsInstanceOfType(actual_context, typeof(VariablesContext));
        }

        [TestMethod]
        public void VariablesRepoOrignallyReturnsNoVariables()
        {

        }

        [TestMethod]
        public void VariablesRepoShouldReturnFullListOfVariables()
        {

        }

        [TestMethod]
        public void VariablesRepoShouldBeAbleToAddVariables()
        {

        }

        [TestMethod]
        public void VariablesRepoShouldBeAbleToAddedWithVarAndValArguments()
        {

        }

        [TestMethod]
        public void VariablesRepoShouldOnlyBeAbleToAddValidVarAndVal()
        {

        }

        [TestMethod]
        public void VariablesRepoShouldBeAbleToFindSpecificVariables()
        {

        }

        [TestMethod]
        public void VariablesRepoShouldBeAbleToDeleteSpecificVariable()
        {

        }

        [TestMethod]
        public void VariablesRepoShouldNotBeAbleToDeleteAVariableThatDoesNotExit()
        {

        }
    }
}
