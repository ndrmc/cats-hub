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
    public class TransportaitonReport
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
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

        public static void WaitForHttpRequest()
        {
            Thread.Sleep(3000);
        }
        [Test]
        public void TheTransportaitonReportTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin","pass2pass");
            driver.Navigate().GoToUrl(baseURL +"/TransportationReport");
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Transportation Reports[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.XPath("//div[@id='body']/div/table/tbody/tr[2]/td[2]/div")));
            Assert.AreEqual("", driver.FindElement(By.Name("Go")).Text);
            Assert.IsTrue(IsElementPresent(By.Id("FromDateAm")));
            driver.FindElement(By.XPath("//div[@id='body']/div/table/tbody/tr[3]/td[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("ToDateAm")));
            driver.FindElement(By.Id("body")).Click();
            driver.FindElement(By.Name("Go")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("(//input[@name='operation'])[2]")).Click();
            driver.FindElement(By.Name("Go")).Click();
            driver.FindElement(By.Id("Month")).Click();
            driver.FindElement(By.Name("Go")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("Daily")).Click();
            driver.FindElement(By.Name("Go")).Click();
            driver.FindElement(By.Name("operation")).Click();
            driver.FindElement(By.Name("Go")).Click();
            driver.FindElement(By.Id("Month")).Click();
            driver.FindElement(By.Name("Go")).Click();
            driver.FindElement(By.Id("Quarter")).Click();
            driver.FindElement(By.Name("Go")).Click();
            driver.FindElement(By.Id("Year")).Click();
            driver.FindElement(By.Name("Go")).Click();
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
