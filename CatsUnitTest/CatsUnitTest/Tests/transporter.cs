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
    public class Transporter
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
        public void TheTransporterTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin","pass2pass");
            driver.FindElement(By.LinkText("Admin")).Click();
            WaitForHttpRequest();
            driver.FindElement(By.LinkText("Transporters")).Click();
            WaitForHttpRequest();
            Assert.IsTrue(IsElementPresent(By.LinkText("Create New")));
            driver.FindElement(By.LinkText("Create New")).Click();
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.CssSelector("div.editor-label"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            Assert.AreEqual("Name", driver.FindElement(By.CssSelector("div.editor-label")).Text);
            Assert.AreEqual("Name (Amharic)", driver.FindElement(By.CssSelector("label")).Text);
            Assert.IsTrue(IsElementPresent(By.Id("Name")));
            Assert.IsTrue(IsElementPresent(By.Id("NameAM")));
            Assert.IsTrue(IsElementPresent(By.Id("LongName")));
            Assert.IsTrue(IsElementPresent(By.Id("BiddingSystemID")));
            Assert.IsTrue(IsElementPresent(By.XPath("//button[@type='button']")));
            Assert.IsTrue(IsElementPresent(By.XPath("(//button[@type='button'])[2]")));
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("new transporters");
            driver.FindElement(By.Id("NameAM")).Click();
            driver.FindElement(By.Id("NameAM")).Clear();
            driver.FindElement(By.Id("NameAM")).SendKeys("አዲስ ትራንስፖርተር");
            driver.FindElement(By.Id("LongName")).Clear();
            driver.FindElement(By.Id("LongName")).SendKeys("new long name");
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            driver.WaitForHttpResponse(By.XPath("(//a[contains(text(),'Delete')])[67]"));
            driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[67]")).Click();
            WaitForHttpRequest();
            //Assert.AreEqual("Delete", driver.FindElement(By.CssSelector("h2")).Text);
           // Assert.IsTrue(IsElementPresent(By.CssSelector("h3")));
            //Assert.IsTrue(IsElementPresent(By.CssSelector("input[type=\"submit\"]")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Back to List")));
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Log Off")).Click();
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
