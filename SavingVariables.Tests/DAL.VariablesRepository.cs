using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using Moq;
using System.Data.Entity;
using SavingVariables.Models;
using System.Collections.Generic;

namespace SavingVariables.Tests
{
    [TestClass]
    public class VariablesRepositoryTests
    {
        Mock<VariablesContext> mock_context { get; set; }
        Mock<DbSet<Variable>> mock_variable_table { get; set; }
        List<Variable> variable_list { get; set; }
    }
}
