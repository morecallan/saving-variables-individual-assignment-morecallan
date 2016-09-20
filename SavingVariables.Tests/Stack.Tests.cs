using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SavingVariables.Tests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void StackCanBeInstantiated()
        {
            //Act
            Stack my_stack = new Stack();

            //Assert
            Assert.IsNotNull(my_stack);
        }

        [TestMethod]
        public void StackLastCommandCanBeAddedTo()
        {
            //Act
            Stack my_stack = new Stack();
            my_stack.LastCommand = "Foo";

            //Assert
            Assert.AreEqual("Foo", my_stack.LastCommand);
        }

        [TestMethod]
        public void StackLastCommandWillBeOverwrittenEachTime()
        {
            //Act
            Stack my_stack = new Stack();
            my_stack.LastCommand = "Foo";
            my_stack.LastCommand = "Bar";

            //Assert
            Assert.AreEqual("Bar", my_stack.LastCommand);
        }
    }
}
