using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureDevops.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using AzureDevops.Models;
using System.Linq;

namespace AzureDevops.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        WeatherContext context = new WeatherContext();
    
        //[TestMethod()]
        //public void GetIpTest()
        //{
        //    var getWeather = context.Weather.Where(w => w.Name.Equals("Bangkok")).FirstOrDefault();
        //    Assert.AreEqual("Bangkok", getWeather.Name);
            
        //}
        [TestMethod()]
        public void Test()
        {
            Assert.AreEqual(2, 2);

        }
    }
}