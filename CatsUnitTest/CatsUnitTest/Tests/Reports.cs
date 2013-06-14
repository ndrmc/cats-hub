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
    public class Reports
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
            //Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheReportsTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin", "pass2pass");
            driver.Navigate().GoToUrl(baseURL + "/GiftCertificate/MonthlySummary");
            driver.FindElement(By.LinkText("Gift Date")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Monthly Gift Certificate Report[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Welcome Admin User ! Log Off | Settings [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            Thread.Sleep(3000);
            driver.FindElement(By.LinkText("Monthly Gift Certificate Chart")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Monthly Gift Certificate Chart[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("Gift Date")).Click();
            driver.FindElement(By.LinkText("Bin Card")).Click();
            
            driver.FindElement(By.CssSelector("button.t-button")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Bin Card : DRMFSS - Diredawa Hub [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Welcome Admin User ! Log Off | Settings [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Store [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Commodity [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Project[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
           
            driver.FindElement(By.CssSelector("option[value=\"70\"]")).Click();
            driver.FindElement(By.CssSelector("button.t-button")).Click();
            driver.FindElement(By.LinkText("Store Report")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Welcome Admin User ! Log Off | Settings [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Stock Status :[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Commodity [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.CssSelector("span.t-input")).Click();
            driver.FindElement(By.XPath("//div/ul/li[3]")).Click();
            driver.FindElement(By.CssSelector("span.t-input")).Click();
            driver.FindElement(By.XPath("//div/ul/li[5]")).Click();
            driver.FindElement(By.CssSelector("span.t-select > span.t-icon.t-arrow-down")).Click();
            driver.FindElement(By.XPath("//div/ul/li[4]")).Click();
            driver.FindElement(By.CssSelector("img")).Click();
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("DefaultHubId"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            // ERROR: Caught exception [ReferenceError: selectLocator is not defined]
            driver.FindElement(By.CssSelector("option[value=\"3\"]")).Click();
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            driver.FindElement(By.CssSelector("span.t-input")).Click();
            driver.FindElement(By.XPath("//div/ul/li[3]")).Click();
            driver.FindElement(By.CssSelector("span.t-input")).Click();
            driver.FindElement(By.CssSelector("span.t-input")).Click();
            driver.FindElement(By.XPath("//div/ul/li[5]")).Click();
            driver.FindElement(By.LinkText("Free Stock")).Click();
            driver.FindElement(By.CssSelector("img")).Click();
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("DefaultHubId"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            // ERROR: Caught exception [ReferenceError: selectLocator is not defined]
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            driver.FindElement(By.CssSelector("span.t-input")).Click();
            driver.FindElement(By.XPath("//div/ul/li[2]")).Click();
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
