using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumTests;
using NUnit.Core;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var testClasses = GetTestTypes();

            foreach (var type in testClasses)
            {
                var className = type.FullName;
                var testInstance = Assembly.GetExecutingAssembly().CreateInstance(className);
                var setupmethodInfo = type.GetMethod("SetupTest");
                var mainTestmethodInfo = type.GetMethod(GetMainTestMethod(className));
                if (setupmethodInfo != null)
                    setupmethodInfo.Invoke(testInstance,null);
                //if (mainTestmethodInfo != null)
                   // mainTestmethodInfo.Invoke(testInstance,null);
                // Invoke SetupTest
                // Invoke GetMainTestMethod
            }
            
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
                var methods=type.GetMethods();
                if(methods.Any(m=>m.Name=="SetupTest"))
                    testsInAssembly.Add(type);
                
            }
            return testsInAssembly;
        }

    }
}
