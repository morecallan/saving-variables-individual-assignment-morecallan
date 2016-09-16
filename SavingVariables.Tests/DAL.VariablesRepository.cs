using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using Moq;

namespace SavingVariables.Tests
{
    [TestClass]
    public class VariablesRepositoryTests
    {
        Mock<VariablesContext> mock_context { get; set; }
        Mock<DbSet<Variable>> mock_author_table { get; set; }
        List<Variable> author_list { get; set; }
    }
}
