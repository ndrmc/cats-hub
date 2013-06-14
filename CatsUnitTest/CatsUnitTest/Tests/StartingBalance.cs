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
    public class StartingBalance
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
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheStartingBalanceTest()
        {

            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin", "pass2pass");
            driver.Navigate().GoToUrl(baseURL + "/StartingBalance");
            
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Starting Balances : DRMFSS - Adama Hub(\n|\r\n)(\n|\r\n)(\n|\r\n)(\n|\r\n)Create New Starting Balance[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Create New Starting Balance[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Admin User[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Log Off[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Settings[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.CssSelector("img")));
            Assert.AreEqual("Commodity Tracking System : Starting Balances", driver.Title);
            Assert.AreEqual("Starting Balances : DRMFSS - Diredawa Hub", driver.FindElement(By.CssSelector("h2")).Text);

            driver.Navigate().GoToUrl(baseURL + "/StartingBalance");
            driver.FindElement(By.LinkText("Create New Starting Balance")).Click();
            driver.WaitForHttpResponse(By.XPath("//div[@id='StartingBalanceForm']/div[3]/label"));
            Assert.IsTrue(IsElementPresent(By.XPath("//div[@id='StartingBalanceForm']/div[3]/label")));
            Assert.IsTrue(IsElementPresent(By.XPath("//div[@id='StartingBalanceForm']/div[5]/label")));
            Assert.IsTrue(IsElementPresent(By.XPath("//div[@id='StartingBalanceForm']/div[7]/label")));
            Assert.IsTrue(IsElementPresent(By.XPath("//div[@id='StartingBalanceForm']/div[7]/label")));
            Assert.IsTrue(IsElementPresent(By.XPath("//div[@id='StartingBalanceForm']/div[9]/label")));
            Assert.AreEqual("select", driver.FindElement(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")).Text);
            Assert.AreEqual("select", driver.FindElement(By.XPath("//div[@id='StartingBalanceForm']/div[4]/div/div/span/span")).Text);
            Assert.IsTrue(IsElementPresent(By.XPath("//div[@id='StartingBalanceForm']/div[6]/div/div/span/span")));
            Assert.AreEqual("select", driver.FindElement(By.XPath("//div[@id='StartingBalanceForm']/div[8]/div/div/span/span")).Text);
            Assert.AreEqual("", driver.FindElement(By.CssSelector("input.t-button")).Text);
            Assert.AreEqual("", driver.FindElement(By.CssSelector("input.t-button.t-close")).Text);
            Assert.IsTrue(IsElementPresent(By.XPath("//div[@id='StartingBalanceForm']/div[20]/div/div/span/span")));
            Assert.IsTrue(IsElementPresent(By.Id("QuantityInMT")));
            Assert.IsTrue(IsElementPresent(By.Id("QuantityInUnit")));
            Assert.IsTrue(IsElementPresent(By.Id("SINumber")));
            Assert.IsTrue(IsElementPresent(By.Id("ProjectNumber")));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Project Code[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*SI / Batch Number[\\s\\S]*$"));
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Quantity In MT[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Unit[\\s\\S]*$"));
            Assert.AreEqual("Commodity Tracking System : Starting Balances", driver.Title);
            driver.FindElement(By.XPath("//input[@value='Cancel']")).Click();
            driver.FindElement(By.CssSelector("img")).Click();

            driver.WaitForHttpResponse(By.CssSelector("option[value=\"3\"]"));
            driver.FindElement(By.CssSelector("option[value=\"3\"]")).Click();
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            driver.FindElement(By.LinkText("Create New Starting Balance")).Click();
            driver.WaitForHttpResponse(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down"));
            driver.FindElement(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")).Click();
            driver.WaitForHttpResponse(By.XPath("//div/ul/li[4]"));
            driver.FindElement(By.XPath("//div/ul/li[4]")).Click();
            driver.FindElement(By.XPath("//div[@id='StartingBalanceForm']/div[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[6]/div/ul/li[3]")).Click();
            driver.FindElement(By.XPath("//div[@id='StartingBalanceForm']/div[6]/div/div/span")).Click();
            driver.FindElement(By.XPath("//div[7]/div/ul/li[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='StartingBalanceForm']/div[8]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[8]/div/ul/li[4]")).Click();
            driver.FindElement(By.XPath("//div[@id='StartingBalanceForm']/div[10]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[@id='StartingBalanceForm']/div[8]/div/div/span")).Click();
            driver.FindElement(By.XPath("//div[8]/div/ul/li[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='StartingBalanceForm']/div[10]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[9]/div/ul/li[6]")).Click();
            driver.FindElement(By.Id("ProjectNumber")).Clear();
            driver.FindElement(By.Id("ProjectNumber")).SendKeys("WFP-50");
            driver.FindElement(By.Id("SINumber")).Clear();
            driver.FindElement(By.Id("SINumber")).SendKeys("951753");
            driver.FindElement(By.Id("QuantityInUnit")).Clear();
            driver.FindElement(By.Id("QuantityInUnit")).SendKeys("852147");
            driver.FindElement(By.Id("QuantityInMT")).Clear();
            driver.FindElement(By.Id("QuantityInMT")).SendKeys("147");
            driver.FindElement(By.XPath("//div[@id='StartingBalanceForm']/div[20]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[10]/div/ul/li[2]")).Click();
            driver.FindElement(By.CssSelector("input.t-button")).Click();
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
