using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using CatsUnitTest;

namespace SeleniumTests
{
    [TestFixture]
    public class HubManagement
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            baseURL = "http://localhost:37068";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
           // Assert.AreEqual("", verificationErrors.ToString());
        }

        public static void WaitForHttpRequest()
        {
            Thread.Sleep(3000);
        }
        [Test]
        public void TheHubManagementTest()
        {
           
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin", "pass2pass");
            driver.FindElement(By.LinkText("Admin")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.LinkText("Hub Owners")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.LinkText("Create New")).Click();
            driver.WaitForHttpResponse(By.Id("Name"));
            //for (int second = 0;; second++) {
            //    if (second >= 60) Assert.Fail("timeout");
            //    try
            //    {
            //        if (IsElementPresent(By.Id("Name"))) break;
            //    }
            //    catch (Exception)
            //    {}
            //    Thread.Sleep(1000);
            //}
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("SampleHub");
            driver.WaitForHttpResponse(By.XPath("//button[@type='button']"));
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            //driver.WaitForHttpResponse(By.XPath("(//a[contains(text(),'Delete')])[4]"));
            //driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[4]")).Click();
            // Warning: assertTextPresent may require manual changes
            //Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Delete[\\s\\S]*$"));
            //driver.WaitForHttpResponse("input[type=\"submit\"]");
            //driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Hub")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.LinkText("Create New")).Click();
            driver.WaitForHttpResponse(By.Id("Name"));
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Sample Hub Name");
            driver.WaitForHttpResponse(By.Id("HubOwnerID"));
           driver.WaitForHttpResponse(By.XPath("//button[@type='button']"));
            driver.FindElement(By.XPath("//button[@type='button']")).Click();

            //driver.WaitForHttpResponse(By.XPath("(//a[contains(text(),'Delete')])[7]"));
            //driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[7]")).Click();
            //// Warning: assertTextPresent may require manual changes

            //Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "Delete"));
            //for (int second = 0;; second++) {
            //    if (second >= 60) Assert.Fail("timeout");
            //    try
            //    {
            //        if (IsElementPresent(By.CssSelector("input[type=\"submit\"]"))) break;
            //    }
            //    catch (Exception)
            //    {}
            //    Thread.Sleep(1000);
            //}
            //driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Store")).Click();
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "List of Stores"));
            driver.FindElement(By.LinkText("Create New")).Click();
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("Name"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Abebe Kebede");
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("StackCount"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            driver.FindElement(By.Id("StackCount")).Clear();
            driver.FindElement(By.Id("StackCount")).SendKeys("four");
            driver.FindElement(By.Id("StackCount")).Clear();
            driver.FindElement(By.Id("StackCount")).SendKeys("4");
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("StoreManName"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            driver.FindElement(By.Id("StoreManName")).Clear();
            driver.FindElement(By.Id("StoreManName")).SendKeys("Store man name");
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("IsTemporary"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            driver.FindElement(By.Id("IsTemporary")).Click();
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("IsActive"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            driver.FindElement(By.Id("IsActive")).Click();
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.XPath("//button[@type='button']"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            //for (int second = 0;; second++) {
            //    if (second >= 60) Assert.Fail("timeout");
            //    try
            //    {
            //        if (IsElementPresent(By.XPath("(//a[contains(text(),'Delete')])[29]"))) break;
            //    }
            //    catch (Exception)
            //    {}
            //    Thread.Sleep(1000);
            //}


            //driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[29]")).Click();
            // Warning: assertTextPresent may require manual changes
           // Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "Delete"));
            // Warning: verifyTextPresent may require manual changes
            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Are you sure you want to delete this[\\s\\S][\\s\\S]*$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}
            //for (int second = 0;; second++) {
            //    if (second >= 60) Assert.Fail("timeout");
            //    try
            //    {
            //        if (IsElementPresent(By.CssSelector("input[type=\"submit\"]"))) break;
            //    }
            //    catch (Exception)
            //    {}
            //    Thread.Sleep(1000);
            //}
            //driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();


            driver.FindElement(By.LinkText("Admin")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
