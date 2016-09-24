using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SavingVariables.Tests
{
    [TestClass]
    public class CommandsTests
    {

        [TestMethod]
        public void CommandsCanOnlyBeInstantiatedWithSessionStack()
        {
            //Arrange
            Stack session_stack = new Stack();
            //Act
            Commands my_session_commands = new Commands(session_stack);
            //Assert
            Assert.IsNotNull(my_session_commands);
        }

        [TestMethod]
        public void CommandsWillDisplayTheLastStackCommand()
        {
            //Arrange
            Stack session_stack = new Stack();
            //Act
            Commands my_session_commands = new Commands(session_stack);
            my_session_commands.Action("help");

            string expected_stack = "help";
            string actual_stack = session_stack.LastCommand;
            //Assert
            Assert.AreEqual(expected_stack, actual_stack);
        }

        [TestMethod]
        public void CommandsWillDisplayTheHelpMessageUponRequest()
        {
            //Arrange
            Stack session_stack = new Stack();
            //Act
            Commands my_session_commands = new Commands(session_stack);
            my_session_commands.Action("help");

            string expected_output = OutputMessages.Help();
            string actual_output = my_session_commands.Output;
            //Assert
            Assert.AreEqual(expected_output, actual_output);
        }
    }
}
