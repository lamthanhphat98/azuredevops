using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureDevops.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevops.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            Assert.AreEqual(2, 2);
        }

        [TestMethod()]
        public void GetTest2()
        {
            Assert.AreEqual(3, 3);
        }
    }
}