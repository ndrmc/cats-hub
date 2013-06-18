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
        public void AuthenticationTest()
        {
            Authentication authentication = new Authentication();
            authentication.SetupTest();
            authentication.TheAuthenticationTest();
            authentication.TeardownTest();
        }

        [TestMethod]
        public void GiftCertiificatesTest()
        {
            GiftCertiificates giftCertiificates = new GiftCertiificates();
            giftCertiificates.SetupTest();
            giftCertiificates.TheGiftCertiificatesTest();
            giftCertiificates.TeardownTest();
        }

        [TestMethod]
        public void HubManagementTest()
        {
            HubManagement hubManagement = new HubManagement();
            hubManagement.SetupTest();
            hubManagement.TheHubManagementTest();
            hubManagement.TeardownTest();
        }

        [TestMethod]
        public void InternalMovementTest()
        {
            InternalMovement internalMovement = new InternalMovement();
            internalMovement.SetupTest();
            internalMovement.TheInternalMovementTest();
            internalMovement.TeardownTest();
        }

        [TestMethod]
        public void LookUpsTest()
        {
            LookUps lookUps = new LookUps();
            lookUps.SetupTest();
            lookUps.TheLookUpsTest();
            lookUps.TeardownTest();
        }

        [TestMethod]
        public void LossAndAdjustmentsTest()
        {
            LossAndAdjustments lossAndAdjustments = new LossAndAdjustments();
            lossAndAdjustments.SetupTest();
            lossAndAdjustments.TheLossAndAdjustmentsTest();
            lossAndAdjustments.TeardownTest();
        }

        [TestMethod]
        public void ReceiptsTest()
        {
            Receipts receipts = new Receipts();
            receipts.SetupTest();
            receipts.TheReceiptsTest();
            receipts.TeardownTest();
        }


        [TestMethod]
        public void StackEventsTest()
        {
            StackEvents stackEvents = new StackEvents();
            stackEvents.SetupTest();
            stackEvents.TheStackEventsTest();
            stackEvents.TeardownTest();
        }

        [TestMethod]
        public void StartingBalanceTest()
        {
            StartingBalance startingBalance = new StartingBalance();
            startingBalance.SetupTest();
            startingBalance.TheStartingBalanceTest();
            startingBalance.TeardownTest();
        }

        [TestMethod]
        public void TransactionsTest()
        {
            Transactions transactions = new Transactions();
            transactions.SetupTest();
            transactions.TheTransactionsTest();
            transactions.TeardownTest();
        }

        [TestMethod]
        public void TranslationsTest()
        {
            Translations translations = new Translations();
            translations.SetupTest();
            translations.TheTranslationsTest();
            translations.TeardownTest();
        }

        [TestMethod]
        public void TransportaitonReportTest()
        {
            TransportaitonReport transportaitonReport = new TransportaitonReport();
            transportaitonReport.SetupTest();
            transportaitonReport.TheTransportaitonReportTest();
            transportaitonReport.TeardownTest();
        }

        [TestMethod]
        public void TransporterTest()
        {
            Transporter transporter = new Transporter();
            transporter.SetupTest();
            transporter.TheTransporterTest();
            transporter.TeardownTest();
        }

        [TestMethod]
        public void UserManagementTest()
        {

            UserManagement UserManagement = new UserManagement();
            UserManagement.SetupTest();
            UserManagement.TheUserManagementTest();
            UserManagement.TeardownTest();
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
            //    //Invoke SetupTest
            //    //Invoke GetMainTestMethod
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
