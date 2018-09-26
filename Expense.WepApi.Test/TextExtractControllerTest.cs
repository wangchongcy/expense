using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expense.Test.MockObject;
using Expense.WebApi.Controllers;

namespace Expense.WepApi.Test
{
    [TestClass]
    public class TextExtractControllerTest
    {
        [TestMethod]
        public void TestMethodExtractText()
        {
            var textExtractController = new TextExtractController(
                new MockLogger(),
                new MockExpenseService());
            var expenseInfo = textExtractController.Post("Hello controller test");
            Assert.AreEqual(expenseInfo.ExpenseProperties.Count, 1);
            Assert.AreEqual(expenseInfo.ExpenseProperties[0].Name, "Mock Name");
            Assert.AreEqual(expenseInfo.ExpenseProperties[0].Value, "Mock Value");
        }
    }
}
