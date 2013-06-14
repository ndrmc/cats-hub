using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumTests;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using NUnit.Core;

namespace CatsUnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            
            StartingBalance gift = new StartingBalance();
            gift.SetupTest();
            gift.TheStartingBalanceTest();
            gift.TeardownTest();
            //var testClasses = GetTestTypes();

            //foreach (var type in testClasses)
            //{
            //    var className = type.FullName;
            //    var classNameWithoutNamespace = type.Name;
            //    var testInstance = Assembly.GetExecutingAssembly().CreateInstance(className);
            //    var setupmethodInfo = type.GetMethod("SetupTest");
            //    var mainTestmethodInfo = type.GetMethod(GetMainTestMethod(classNameWithoutNamespace));
            //    var closeBrowserMethodInfo = type.GetMethod("TeardownTest");
            //    if (setupmethodInfo != null)
            //        setupmethodInfo.Invoke(testInstance, new object[] { });
            //    if (mainTestmethodInfo != null)
            //        mainTestmethodInfo.Invoke(testInstance, new object[] { });
            //    if (closeBrowserMethodInfo != null)
            //        closeBrowserMethodInfo.Invoke(testInstance, new object[] { });
            //     Invoke SetupTest
            //     Invoke GetMainTestMethod
            //}
            

        }

        public static string GetMainTestMethod(string className)
        {
            return string.Format("The{0}Test", className);
        }

        public static List<Type> GetTestTypes()
        {
            List<Type> testsInAssembly = new List<Type>();
            var types = Assembly.Load("CatsUnitTest").GetTypes();

            foreach (var type in types)
            {
                var methods = type.GetMethods();
                if (methods.Any(m => m.Name == "SetupTest"))
                    testsInAssembly.Add(type);

            }
            return testsInAssembly;
        }

        
    }
}
