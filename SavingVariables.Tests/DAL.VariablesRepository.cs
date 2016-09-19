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
            // Act
            List<Variable> actual_variables = repo.GetCurrentVariables();
            int expected_variables_count = 0;
            int actual_variables_count = actual_variables.Count;

            // Assert
            Assert.AreEqual(expected_variables_count, actual_variables_count);
        }

        [TestMethod]
        public void VariablesRepoShouldReturnFullListOfVariables()
        {
            //Arrange
            repo.AddVariablesWithVarAndValParameter("g", 9);
            repo.AddVariablesWithVarAndValParameter("z", 10);
            repo.AddVariablesWithVarAndValParameter("l", -7);

            //Act
            List<Variable> current_variables = repo.GetCurrentVariables();
            int expected_variables_count = 3;
            int actual_variables_count = current_variables.Count();

            //Assert
            Assert.AreEqual(expected_variables_count, actual_variables_count);
        }

        [TestMethod]
        public void VariablesRepoShouldBeAbleToAddVariables()
        {
            //Act
            Variable variable_to_add = new Variable { VarSym = "x", Val = 4 };
            repo.AddVariableAsEntity(variable_to_add);


            int expected_variable_count = 1;
            int actual_variable_count = repo.GetCurrentVariables().Count();

            //Assert
            Assert.AreEqual(expected_variable_count, actual_variable_count);
        }

        [TestMethod]
        public void VariablesRepoShouldBeAbleToAddedWithVarAndValArguments()
        {
            //Act
            Variable variable_to_add = new Variable { VarSym = "xa", Val = 4 };
            repo.AddVariableAsEntity(variable_to_add);

            int expected_variable_count = 1;
            int actual_variable_count = repo.GetCurrentVariables().Count();

            //Assert
            Assert.AreEqual(expected_variable_count, actual_variable_count);
        }

        [TestMethod]
        public void VariablesRepoShouldOnlyBeAbleToAddValidVarAndVal()
        {
            //Act

            int expected_variable_count = 0;
            int actual_variable_count = repo.GetCurrentVariables().Count();

            //Assert
            Assert.AreEqual(expected_variable_count, actual_variable_count);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void VariableRepoShouldThrowOperationExceptionIfThereIsAttemptToAddVariableTwiceWithParams()
        {
            // Arrange
            Variable variable_to_add = new Variable { VarSym = "x", Val = 4 };
            repo.AddVariableAsEntity(variable_to_add);
            repo.AddVariablesWithVarAndValParameter("x", 9);

            //Assert
            Assert.AreEqual(1, repo.GetCurrentVariables().Count());

        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void VariableRepoShouldThrowOperationExceptionIfThereIsAttemptToAddVariableTwiceWithEntity()
        {
            // Arrange
            repo.AddVariablesWithVarAndValParameter("h", 9);
            Variable variable_to_add = new Variable { VarSym = "h", Val = 4 };
            repo.AddVariableAsEntity(variable_to_add);

            //Assert
            Assert.AreEqual(1, repo.GetCurrentVariables().Count());
        }

        [TestMethod]
        public void VariablesRepoShouldBeAbleToFindSpecificVariables()
        {
            // Arrange
            Variable variable_to_add = new Variable { VarSym = "x", Val = 4 };
            repo.AddVariableAsEntity(variable_to_add);
            repo.AddVariablesWithVarAndValParameter("g", 9 );

            //Act
            int expected_variable_value = 9;
            int actual_expected_value = repo.FindVariablesGivenVarSym("g").Val;

            //Assert
            Assert.AreEqual(expected_variable_value, actual_expected_value);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void VariablesRepoShouldReturnNullReferenceExceptionForNonFoundVariables()
        {
            // Arrange
            Variable variable_to_add = new Variable { VarSym = "x", Val = 4 };
            repo.AddVariableAsEntity(variable_to_add);
            repo.AddVariablesWithVarAndValParameter("g", 9);

            //Act
            int expected_variable_value = 9;
            int actual_expected_value = repo.FindVariablesGivenVarSym("d").Val;

            //Assert
            Assert.AreEqual(expected_variable_value, actual_expected_value);
        }

        [TestMethod]
        public void VariablesRepoShouldBeAbleToDeleteSpecificVariable()
        {
            // Arrange
            Variable variable_to_add = new Variable { VarSym = "x", Val = 4 };
            repo.AddVariableAsEntity(variable_to_add);
            repo.AddVariablesWithVarAndValParameter("g", 9);
            repo.AddVariablesWithVarAndValParameter("d", 11);

            //Act
            Variable variable_deleted = repo.RemoveVariablesWithVarParameter("d");
            int expected_updated_variable_count = 2;
            int actual_updated_variable_count = repo.GetCurrentVariables().Count();

            Assert.AreEqual(expected_updated_variable_count, actual_updated_variable_count);
            Assert.AreEqual(variable_deleted.VarSym, "d");
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void VariablesRepoShouldNotBeAbleToDeleteAVariableThatDoesNotExit()
        {
            // Arrange
            Variable variable_to_add = new Variable { VarSym = "x", Val = 4 };
            repo.AddVariableAsEntity(variable_to_add);
            repo.AddVariablesWithVarAndValParameter("g", 9);
            repo.AddVariablesWithVarAndValParameter("d", 11);

            //Act
            Variable variable_deleted = repo.RemoveVariablesWithVarParameter("k");
            int expected_updated_variable_count = 3;
            int actual_updated_variable_count = repo.GetCurrentVariables().Count();

            Assert.AreEqual(expected_updated_variable_count, actual_updated_variable_count);
        }
    }
}
