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
    public class StackEvents
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
        
        [Test]
        public void TheStackEventsTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin","pass2pass");
            driver.Navigate().GoToUrl(baseURL + "/StackEvent/Index");
            Assert.AreEqual("Commodity Tracking System : Stack Events", driver.Title);
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Basic Stack Events[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Store[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Stack[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.Id("StoreId-input")));
            driver.FindElement(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")).Click();
            driver.FindElement(By.XPath("//div/ul/li[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("StackId-input")));
            driver.FindElement(By.XPath("//div[@id='body']/table/tbody/tr[2]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[7]/div/ul/li[3]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("addNewEvent")));
            driver.FindElement(By.Id("addNewEvent")).Click();
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Date[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Store[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Stack[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Event Type[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.Id("EventDate_am")));
           // driver.WaitForHttpResponse(By.XPath("(//a[contains(text(),'14')])[3]"));
           // driver.FindElement(By.XPath("(//a[contains(text(),'14')])[3]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("StackEventTypeId-input")));
            driver.FindElement(By.XPath("//form[@id='form0']/table/tbody/tr[4]/td[2]/div/div/span/span")).Click();
            driver.WaitForHttpResponse(By.XPath("//div[9]/div/ul/li"));
            driver.FindElement(By.XPath("//div[9]/div/ul/li")).Click();
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("some Description");
            driver.FindElement(By.Id("Recommendation")).Clear();
            driver.FindElement(By.Id("Recommendation")).SendKeys("Some Recommendation");
            Assert.IsTrue(IsElementPresent(By.CssSelector("input[type=\"submit\"]")));
            Assert.IsTrue(IsElementPresent(By.CssSelector("#form0 > div > input[type=\"button\"]")));
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
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
