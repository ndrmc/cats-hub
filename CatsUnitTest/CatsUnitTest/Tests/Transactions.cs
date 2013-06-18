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
    public class Transactions
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = WebDriverExtension.BASE_URL;
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
        public void TheTransactionsTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin", "pass2pass");
            driver.Navigate().GoToUrl(baseURL + "/Admin/Home");
            driver.FindElement(By.LinkText("Journal")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Ledger")).Click();
            Assert.AreEqual("Ledger", driver.FindElement(By.CssSelector("h2")).Text);
            Assert.IsTrue(IsElementPresent(By.Id("LedgerID-input")));
            Assert.IsTrue(IsElementPresent(By.Id("CommodityID-input")));
            Assert.IsTrue(IsElementPresent(By.Id("goButton")));
            driver.FindElement(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")).Click();
            driver.FindElement(By.XPath("//li[12]")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/table/tbody/tr/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[4]/div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/table/tbody/tr/td[6]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[5]/div/ul/li")).Click();
            driver.FindElement(By.Id("goButton")).Click();
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
